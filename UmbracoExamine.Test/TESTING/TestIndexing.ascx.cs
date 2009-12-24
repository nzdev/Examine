﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UmbracoExamine.Core;
using UmbracoExamine.Providers;
using System.Drawing;

namespace UmbracoExamine.Test.TESTING
{
    public partial class TestIndexing : TestControl
    {

        

        protected void TestIndexButton_Click(object sender, EventArgs e)
        {
            AddTrace("Indexing Content", "Start all content indexing", Color.Green);
            ExamineManager.Instance.IndexAll(IndexType.Content);
            AddTrace("Indexing Media", "Start all media indexing", Color.Green);
            ExamineManager.Instance.IndexAll(IndexType.Media);
        }

        protected void TestRebuildButton_Click(object sender, EventArgs e)
        {
            AddTrace("Rebuilding Index", "Start rebuilding the indexes", Color.Green);
            ExamineManager.Instance.RebuildIndex();
        }

        

    }
}