//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: Deployment
    //Script File: TC_Dashboard_Deployment.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//Dashboards used for Test
var dashb1 = "SalesPersonWorkbench"
var dashb2 = "Dash2Deploy"
var dashb3 = "PartOnHandStatus"

//Queries
var dashb2Query1 = "COM-90daysSORel"

//Renamed Dashboards
var dashb1Copy = "Dash1Deploy"

//Data for Menu
 var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashDepl",
    "menuName" : "DashDepl",
    "orderSequence" : 6,
    "menuType" : "Dashboard-Assembly",
    "dll" : dashb2
  }


var MenuData2 = {
  "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
  "menuID" : "DashDep2",
  "menuName" : "DashDepl2",
  "orderSequence" : 7,
  "menuType" : "Dashboard-Assembly",
  "dll" : dashb3
}