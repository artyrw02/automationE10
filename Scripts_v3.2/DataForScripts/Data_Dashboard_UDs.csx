//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: UDFields
    //Script File: TC_Dashboard_UDFields.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//Menu Data
var MenuDataEntry = {
  "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
  "menuID" : "MenuA",
  "menuName" : "MenuA",
  "orderSequence" : 0,
  "menuType" : "Menu item",
  "dll" : "Ice.UI.UD100Entry.dll"
}

//BAQs used for testing
var baqData1 = {
  "Id" : "baqUD1",
  "Description" : "baqUD1",
  "Table" : "Ice.UD100",
  "Alias" : "UD100",
  "Columns" : "Company,Key1,Key2,Key3,Key4,Key5,Character01"
}

var baqData2 = {
  "Id" : "baqUD2",
  "Description" : "baqUD2",
  "Table" : "Ice.UD100A",
  "Alias" : "UD100A",
  "Columns" : "Company,Key1,Key2,Key3,Key4,Key5,ChildKey1,ChildKey2,ChildKey3,ChildKey4,ChildKey5,Character01"
}

//Dashboards used for Test
  var DashbData = {
    "dashboardID" : "DashbUD",
    "dashboardCaption" : "DashbUD",
    "dashDescription" : "DashbUD",
    "generalOptions" : "chkInhibitRefreshAll",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }