//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: TrackerViews_1
    //Script File: TC_Dashboard_Tracker_View_1.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//Menu used for Test
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashT2",
    "menuName" : "DashTrack2",
    "orderSequence" : 102,
    "menuType" : "Dashboard-Assembly",
    "dll" : "DashTracker2"
  }


//BAQs

var baq1Copy = "zCustomer01Copy"
var baq2Copy = "zPartTracker01Copy"

var baq3 = {
  "baq" : "BAQA",
  "config" : "chkShared,chkUpdatable",
  "Table" : "Erp.Customer",
  "Alias" : "Customer",
  "Columns" :  "Company,CustID,CustNum,State,City,Country,TerritoryID"
}

//Dashboard
var dashbID = "DashTracker2"