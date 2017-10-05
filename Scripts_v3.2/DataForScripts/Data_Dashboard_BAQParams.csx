//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: BAQParams
    //Script File: TC_Dashboard_BAQParams.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//BAQs used for Test
  var baqData = {
    "Id" : "baqParams1",
    "Description" : "baqParams1",
    "Table" : "Customer",
    "Columns" : "CustNum"
  }

//Dashboards used for Test
  var DashbData = {
    "dashboardID" : "DashBBAQ",
    "dashboardCaption" : "DashBBAQ",
    "dashDescription" : "DashBBAQ",
    "baqQuery" : "baqParams2",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }
