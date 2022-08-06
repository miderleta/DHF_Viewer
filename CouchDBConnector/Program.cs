// See https://aka.ms/new-console-template for more information
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
    _id = "T000-000-0010",
    _rev = "",
    Title = "Test Document 10",
    Type = "Test 4",
    Revision = "AA",
    Product = "T004",
};

//retriving database info
try
{
    var receivedData = await dataLoader.LoadCouchData();
    WriteLine("Database name: " + receivedData.DB_Name);
    WriteLine("Number of documents in the databse: " + receivedData.Doc_Count);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("Access to DB closed. Unathorised");
}

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
//    var receivedData = await dataLoader.ReadDocumentData(userInput);
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







