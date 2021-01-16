﻿using System.IO;
using Examine.RemoteDirectory;
using Lucene.Net.Store;

namespace Examine.AzureDirectory
{
    /// <summary>
    /// A lock factory used for azure blob storage using Simple Locking (file based)
    /// </summary>
    public class RemoteDirectorySimpleLockFactory : LockFactory
    {
        private readonly AzureLuceneDirectory _azureDirectory;
        private readonly IRemoteDirectory _remoteDirectory;

        public RemoteDirectorySimpleLockFactory(AzureLuceneDirectory azureDirectory, IRemoteDirectory remoteDirectory)
        {
            _azureDirectory = azureDirectory;
            _remoteDirectory = remoteDirectory;
        }
        
        public override Lock MakeLock(string name)
        {
            if (LockPrefix != null)
                name = LockPrefix + "-" + name;

            return new AzureSimpleLock(name, _azureDirectory, _remoteDirectory);           
        }

        public override void ClearLock(string name)
        {
            if (LockPrefix != null)
                name = LockPrefix + "-" + name;

            var lockFile = _azureDirectory.RootFolder + name;

            var blob = _azureDirectory.BlobContainer.GetBlobClient(lockFile);
            var flag1Response = blob.Exists();
            var flag1 = flag1Response.Value;
            bool flag2;

            var flag2Response = blob.Exists();
            if (flag2Response.Value)
            {
                blob.Delete();
                flag2 = true;
            }
            else
                flag2 = false;
            if (flag1 && !flag2)
                throw new IOException("Cannot delete " + lockFile);            
        }
    }

   
}