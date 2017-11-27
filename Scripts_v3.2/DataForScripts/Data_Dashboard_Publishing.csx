//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: Deployment
    //Script File: TC_Dashboard_Deployment.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//BAQs used for Test
  var baqData1 = {
    "Id" : "baqPublishing",
    "Description" : "baqPublishing",
    "Table" : "Erp.Customer",
    "Alias" : "Customer",
    "Columns" : "CustID,CustNum,Name,State,Country"
  }

  var baqData2 = {
    "Id" : "baqPublishing2",
    "Description" : "baqPublishing2",
    "Table" : "Erp.OrderHed",
    "Alias" : "OrderHed",
    "Columns" : "OrderNum,CustNum,PONum"
  }

//Dashboards used for Test
  var DashbData = {
    "dashboardID" : "DashBPublishing",
    "dashboardCaption" : "Publishing",
    "dashDescription" : "Publishing",
    "generalOptions" : "",
    "baqQuery" : "",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }

  var DashbData2 = {
    "dashboardID" : "DashBPublishing2",
    "dashboardCaption" : "Publishing2",
    "dashDescription" : "Publishing2",
    "generalOptions" : "",
    "baqQuery" : "baq1",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }

// Publish View definition
  var publishDetails = {
    "group" : "BaqGroup",
    "description" : "BAQDescription"
  }