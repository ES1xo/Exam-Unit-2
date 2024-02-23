﻿using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System.Xml.XPath;

Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "54d1e5a5246fa2339bf9080a09d9fb60182bab8ce39ccfdc5f7667e0c48ac9cd"; 
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
string taskID = "rEu25ZX"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
// Fetch the details of the task from the server.
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Console.WriteLine(task1Response);
Task task1 = JsonSerializer.Deserialize<Task>(task1Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task1?.title}{ANSICodes.Reset}\n{task1?.description}\nParameters: {Colors.Yellow}{task1?.parameters}{ANSICodes.Reset}");

string numerals = task1.parameters;

Dictionary<string, int> numeralTranslation = new Dictionary<string, int>
{ 
 {"I", 1},
 {"V", 5},
 {"X", 10},
 {"L", 50},
 {"C", 100},
 {"D", 500},
 {"M", 1000}
};      

int ConvertNumeral(string numerals)
{
    int result = 0;
   
    for (int i = 0; i < task1.parameters.Length; i++)
    {
        int numberTranslated = numeralTranslation[numerals[i].ToString()];

        if (i + 1 < numerals.Length && numeralTranslation[numerals[i + 1].ToString()] > numberTranslated)
        {
            result -= numberTranslated;
        }
        else
        {
            result += numberTranslated;
        }
    }
    return result;
}


var answer = ConvertNumeral(task1.parameters).ToString();
Console.WriteLine($"Answer: {Colors.Green}{answer}{ANSICodes.Reset}");


Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{ANSICodes.Reset}");



//#### SECOND TASK

string task2ID = "aAaa23";

Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task2ID);
Console.WriteLine(task2Response);
Task task2 = JsonSerializer.Deserialize<Task>(task2Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task2?.title}{ANSICodes.Reset}\n{task2?.description}\nParameters: {Colors.Yellow}{task2?.parameters}{ANSICodes.Reset}");




double ConvertUnits(string units)
{
    double result = 0;
    int farenheit = int.Parse(units);
    result = Math.Round((farenheit - 32) * 5 / 9.0, 2);

    return result;
}

var answer2 = ConvertUnits(task2.parameters).ToString();
Console.WriteLine($"Answer: {Colors.Green}{answer2}{ANSICodes.Reset}");

Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task2ID, answer2.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task2AnswerResponse}{ANSICodes.Reset}");

//#### THIRD TASK

string task3ID = "kuTw53L";

Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task3ID);
Console.WriteLine(task3Response);
Task task3 = JsonSerializer.Deserialize<Task>(task3Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task3?.title}{ANSICodes.Reset}\n{task3?.description}\nParameters: {Colors.Yellow}{task3?.parameters}{ANSICodes.Reset}");



class Task
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? taskID { get; set; }
    public string? usierID { get; set; }
    public string? parameters { get; set; }
}