// See https://aka.ms/new-console-template for more information
using CouchDBConnector;
using CouchDBConnector.Interfaces;
using static System.Console;

WriteLine("Program Started");

//Init HttpClient and CouchProcessor objects
IApiHelper client = new ApiHelper();
client.InitializeCouchClient();
var dataLoader = new CouchProcessor();

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

////creating new document
//try
//{
//    WriteLine("Creting New Document");
//    var receivedData = await dataLoader.CreateNewDocument();
//    WriteLine("Sucess! The Output is: ");
//    WriteLine(receivedData);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}

//read document data
//try
//{
//    var receivedData = await dataLoader.ReadDocumentData();
//    WriteLine("Document Number: " + receivedData._id);
//    WriteLine("Title: " + receivedData.Title);
//    WriteLine("Revision: " + receivedData.Revision);
//    WriteLine("Type: " + receivedData.Type);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}

//updating document
try
{
    WriteLine("\nUpdating a Document");
    var receivedData = await dataLoader.UpdateDocument();
    WriteLine("Sucess! The Output is: ");
    WriteLine(receivedData);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("Access to DB closed. Unathorised");
}

////delete document
//try
//{
//    WriteLine("\nDeleting a Document");
//    var receivedData = await dataLoader.DeleteDocument();
//    WriteLine("Sucess! The Output is: ");
//    WriteLine(receivedData);
//}
//catch (Exception ex)
//{
//    WriteLine(ex.Message);
//    WriteLine("Access to DB closed. Unathorised");
//}







