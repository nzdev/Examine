---
uid: quickstart
---

Examine Documentation
===

## What is Examine?

<img align="right" src="https://github.com/Shazwazza/Examine/raw/master/assets/logo-round-small.png?raw=true"> Examine allows you to index and search data easily and wraps the [Lucene.NET](https://lucenenet.apache.org/) indexing/searching engine. [Lucene](https://lucene.apache.org/) is _super_ fast and allows for very fast searching even on very large amounts of data. Examine is very extensible and allows you to configure as many indexes as you like and each may be configured individually. Out of the box Examine gives you a Lucene based index implementation as well as a Fluent API that can be used to search for your data.

Examine is installed via Nuget: [https://www.nuget.org/packages/Examine](https://www.nuget.org/packages/Examine)

## Quick Start

**Tip**: [`IExamineManager`](xref:Examine.IExamineManager) is the gateway to working with examine. It is registered in DI as a singleton and can be injected into your services.

1. Install

    ```powershell
    > dotnet add package Examine --version 3.0.1
    ```

1. Configure Services and create an index

    ```cs
    // Adds Examine Core services
    services.AddExamine();

    // Create a Lucene based index
    services.AddExamineLuceneIndex("MyIndex");
    ```

1. Populate the index

    ```cs
    // Add a "ValueSet" (document) to the index 
    // which can contain any data you want.
    myIndex.IndexItem(new ValueSet(
        Guid.NewGuid().ToString(),  //Give the doc an ID of your choice
        "MyCategory",               //Each doc has a "Category"
        new Dictionary<string, object>()
        {
            {"Name", "Frank" },
            {"Address", "Beverly Hills, 90210" }
        }));
    ```

1. Search the index

    ```cs
    // Create a query
    var results = myIndex.Searcher.CreateQuery()
        .Field("Address", "Hills")        // Look for any "Hills" addresses
        .Execute();                       // Execute the search
    ```
