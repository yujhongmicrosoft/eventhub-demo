---
services: government
platforms: aspnetcore2.1
author: yujhong
---

# Azure Government Event Hub Sample

### Description 
This sample consists of an Azure Functions application as well as a Asp.net Core Console application.
The Console application sends traffic data messages to an Event Hub. The Function app is triggered when the Event Hub receives messages, and then writes the messages to a Cosmos Database as well as to a Power BI Streaming endpoint. 

# How To Run This Sample
Getting started is simple!  To run this sample in Azure Government you will need:
- An Azure Government subscription 
- A Power BI account 
- Set up a [Power BI streaming endpoint with Power BI Rest API](https://docs.microsoft.com/power-bi/service-real-time-streaming#set-up-your-real-time-streaming-dataset-in-power-bi)

To run locally you will additionally need:
- Install [.NET Core](https://www.microsoft.com/net/core) 2.1.0 or later.
- Install [Visual Studio](https://www.visualstudio.com/vs/) 2017 version 15.3 or later with the following workloads:
    - **ASP.NET and web development**
    - **.NET Core cross-platform development**

### Setup

#### Step 1: Deploy Resources to Azure Government

After clicking on the "Deploy to Azure Gov" button below, you will be prompted with a ARM deployment template in the portal.  Enter the parameters and click Create.
This will set up the Event Hub, the Function application, and the Cosmos Database. 
<a href="https://portal.azure.us/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fyujhongmicrosoft%2Fgov-function-sample%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/AzureGov.png" />
</a> 

#### Step 2: Set Function application settings

Navigate to your Function application settings and for the "PowerBIEndpoint" setting replace the value with the URI of the Power BI Streaming endpoint you created.
#### Step 3: Publish and run Console application

The Console application should be published with this command (for Windows) publish -c Release -r win10-x64 and (for Ubuntu) dotnet publish -c Release -r ubuntu.16.10-x64.
Then run the executable with the parameters requested when running --help.
   
#### Step 4: Power BI Visualizations 

You can set up Power BI Visualizations by creating a [real-time streaming tile](https://docs.microsoft.com/power-bi/service-real-time-streaming#example-of-using-real-time-streaming-in-power-bi). 

   
    
    
