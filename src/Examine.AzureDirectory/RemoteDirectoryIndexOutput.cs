﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Examine.LuceneEngine.Directories;
using Lucene.Net.Store;

namespace Examine.AzureDirectory
{

    /// <summary>
    /// Implements IndexOutput semantics for a write/append only file
    /// </summary>
    public class RemoteDirectoryIndexOutput : IndexOutput
    {
        private readonly AzureLuceneDirectory _azureDirectory;
        //private CloudBlobContainer _blobContainer;
        private readonly string _name;
        private IndexOutput _indexOutput;
        private readonly Mutex _fileMutex;
        private BlobClient _blob;
        
        public Lucene.Net.Store.Directory CacheDirectory => _azureDirectory.CacheDirectory;

        public RemoteDirectoryIndexOutput(AzureLuceneDirectory azureDirectory, BlobClient blob, string name)
        {
            //NOTE: _name was null here, is this intended? https://github.com/azure-contrib/AzureDirectory/issues/19 I have changed this to be correct now
            _name = name;
            _azureDirectory = azureDirectory ?? throw new ArgumentNullException(nameof(azureDirectory));
            _fileMutex = SyncMutexManager.GrabMutex(_azureDirectory, _name); 
            _fileMutex.WaitOne();
            try
            {                
                _blob = blob;

                // create the local cache one we will operate against...
                _indexOutput = CacheDirectory.CreateOutput(_name);
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        public override void Flush()
        {
            _indexOutput.Flush();
        }

        protected override void Dispose(bool isDisposing)
        {
            _fileMutex.WaitOne();
            try
            {
                var fileName = _name;

                long originalLength = 0;
                
                //this can be null in some odd cases so we need to check
                if (_indexOutput != null)
                {
                    try
                    {
                        // make sure it's all written out
                        _indexOutput.Flush();
                        originalLength = _indexOutput.Length;
                    }
                    finally
                    {
                        _indexOutput.Dispose();
                    }
                }

                if (originalLength > 0)
                {
                    Stream blobStream;

                    // optionally put a compressor around the blob stream
                    if (_azureDirectory.ShouldCompressFile(_name))
                    {
                        blobStream = CompressStream(fileName, originalLength);
                    }
                    else
                    {
                        blobStream = new StreamInput(CacheDirectory.OpenInput(fileName));
                    }

                    try
                    {
                        // push the blobStream up to the cloud
                        _blob.Upload(blobStream,overwrite:true);

                        // set the metadata with the original index file properties
                        var metadata = new Dictionary<string, string>();
                        metadata.Add("CachedLength", originalLength.ToString());
                        metadata.Add("CachedLastModified", CacheDirectory.FileModified(fileName).ToString());

                        var response = _blob.SetMetadata(metadata);
#if FULLDEBUG
                        Trace.WriteLine($"PUT {blobStream.Length} bytes to {_name} in cloud");
#endif
                    }
                    catch(Azure.RequestFailedException ex) when (ex.Status == 409)
                    {
                        //File already exists
                        throw;
                    }
                    finally
                    {
                        blobStream.Dispose();
                    }

#if FULLDEBUG
                    Trace.WriteLine($"CLOSED WRITESTREAM {_name}");
#endif
                }

                // clean up
                _indexOutput = null;
                //_blobContainer = null;
                _blob = null;
                GC.SuppressFinalize(this);
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        protected virtual MemoryStream CompressStream(string fileName, long originalLength)
        {
            // unfortunately, deflate stream doesn't allow seek, and we need a seekable stream
            // to pass to the blob storage stuff, so we compress into a memory stream
            MemoryStream compressedStream = new MemoryStream();

            IndexInput indexInput = null;
            try
            {
                indexInput = CacheDirectory.OpenInput(fileName);
                using (var compressor = new DeflateStream(compressedStream, CompressionMode.Compress, true))
                {
                    // compress to compressedOutputStream
                    byte[] bytes = new byte[indexInput.Length()];
                    indexInput.ReadBytes(bytes, 0, (int) bytes.Length);
                    compressor.Write(bytes, 0, (int) bytes.Length);
                }

                // seek back to beginning of comrpessed stream
                compressedStream.Seek(0, SeekOrigin.Begin);
#if FULLDEBUG
                Trace.WriteLine($"COMPRESSED {originalLength} -> {compressedStream.Length} {((float) compressedStream.Length / (float) originalLength) * 100}% to {_name}");
#endif
            }
            catch
            {
                // release the compressed stream resources if an error occurs
                compressedStream.Dispose();
                throw;
            }
            finally
            {
                indexInput?.Close();
            }
            return compressedStream;
        }

        public override long Length => _indexOutput.Length;

        public override void WriteByte(byte b)
        {
            _indexOutput.WriteByte(b);
        }

        public override void WriteBytes(byte[] b, int length)
        {
            _indexOutput.WriteBytes(b, length);
        }

        public override void WriteBytes(byte[] b, int offset, int length)
        {
            _indexOutput.WriteBytes(b, offset, length);
        }

        public override long FilePointer => _indexOutput.FilePointer;

        public override void Seek(long pos)
        {
            _indexOutput.Seek(pos);
        }
    }
}