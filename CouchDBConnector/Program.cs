﻿// See https://aka.ms/new-console-template for more information
using CouchDBConnector;
using CouchDBConnector.Interfaces;
using CouchDBConnector.Models;
using static System.Console;

WriteLine("Program Started");

//Init HttpClient and CouchProcessor objects
IApiHelper client = new ApiHelper();
client.InitializeCouchClient();
ICouchProcessor dataLoader = new CouchProcessor();

//simulate user input
//this will be done with user's input
DocumentModel userInput = new DocumentModel()
{
    _id = "H900-001-003",
    _rev = "",
    Title = "PAD OQPQ Protocol",
    Type = "Test Protocol",
    Revision = "AA",
    Product = "Product F",
};

////retriving database info
//try
//{
//    var receivedData = await dataLoader.LoadCouchData();
//    WriteLine("Database name: " + receivedData.DB_Name);
//    WriteLine("Number of documents in the databse: " + receivedData.Doc_Count);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}

//creating new document
try
{
    WriteLine("\nCreting New Document");
    var receivedData = await dataLoader.CreateNewDocument(userInput);
    WriteLine("Sucess! The Output is: ");
    WriteLine(receivedData);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("Error. Please Try Again.");
}

//read document data
//try
//{
//var receivedData = await dataLoader.ReadDocumentData(userInput);
//WriteLine("\nReading data for document " + receivedData._id);
//WriteLine("Document Number: " + receivedData._id);
//WriteLine("Title: " + receivedData.Title);
//WriteLine("Revision: " + receivedData.Revision);
//WriteLine("Type: " + recevedData.Type);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("There is no document with number " + userInput._id);
//}

//updating document
//try
//{
//    WriteLine("\nUpdating a Document");
//var receivedData = await dataLoader.UpdateDocument(userInput);
//WriteLine("Sucess! The Output is: ");
//WriteLine(receivedData);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}

//delete document
//try
//{
//    WriteLine("\nDeleting a Document");
//var receivedData = await dataLoader.DeleteDocument(userInput);
//WriteLine("Sucess! The Output is: ");
//WriteLine(receivedData);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}

//REPORTS
//Get All Documents
//try
//{
//    WriteLine("\n Getting all documents \n");
//    var receivedData = await dataLoader.ReportAllDocuments();
//    WriteLine("Number of documents in the DB: " + receivedData.Rows.Count());
//    //WriteLine(receivedData.Rows.Count());
//    for (var i = 0; i < receivedData.Rows.Count; i++)
//    {
//        WriteLine("\n" + receivedData.Rows[i].value._id + " -- " + receivedData.Rows[i].value.Title + " Rev: " + receivedData.Rows[i].value.Revision);
//    }
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//}

//Report Doc by product
//try
//{
//    var receivedData = await dataLoader.ReportByProduct(userInput);
//    WriteLine("\n Chosen Product: Product " + userInput.Product + "\n");
//    WriteLine(" Number of documents associated with chosen Product: " + receivedData.Rows.Count());
//    for (var i = 0; i < receivedData.Rows.Count; i++)
//    {
//        WriteLine("\n" + receivedData.Rows[i].value._id + " -- " + receivedData.Rows[i].value.Title + " Rev: " + receivedData.Rows[i].value.Revision);
//    }
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//}

//Report Doc by name
//try
//{
//    var receivedData = await dataLoader.SearchByDocumentName(userInput);
//    WriteLine("\n Document Details: \n");
//    WriteLine(receivedData.Rows.Count());
//    for (var i = 0; i < receivedData.Rows.Count; i++)
//    {
//        WriteLine("\n" + receivedData.Rows[i].value._id + " -- " + receivedData.Rows[i].value.Title + " Rev: " + receivedData.Rows[i].value.Revision);
//    }
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//}







