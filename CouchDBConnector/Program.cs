// See https://aka.ms/new-console-template for more information
using CouchDBConnector;
using static System.Console;

WriteLine("Program Started");
//ApiHelper.InitializeClient();

//int comicNumber = 2604;
//var ComicLoader = new ComicProcessor();
//var comicData = await ComicLoader.LoadComic(comicNumber);

//WriteLine(comicData.Num);
//WriteLine(comicData.Img);

//For connecting to CouchDB
ApiHelper.InitializeCouchClient();
var db_name = "dhf_viewer";
var dataLoader = new CouchProcessor();
try
{
    var receivedData = await dataLoader.LoadCouchData(db_name);
    WriteLine("Database name: " + receivedData.DB_Name);
    WriteLine("Number of documents in the databse: " + receivedData.Doc_Count);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("Access to DB closed. Unathorised");
}






