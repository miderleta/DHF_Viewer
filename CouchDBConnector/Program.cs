// See https://aka.ms/new-console-template for more information
using CouchDBConnector;
using CouchDBConnector.Interfaces;
using static System.Console;

WriteLine("Program Started");

//For connecting to CouchDB
IApiHelper client = new ApiHelper();
client.InitializeCouchClient();
var db_name = "dhf_viewer";
var dataLoader = new CouchProcessor();
dataLoader.setEndpointAddress(db_name);

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
    var receivedData = await dataLoader.CreateNewDocument();
    WriteLine("Sucess! The Output is: ");
    WriteLine(receivedData);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("Access to DB closed. Unathorised");
}







