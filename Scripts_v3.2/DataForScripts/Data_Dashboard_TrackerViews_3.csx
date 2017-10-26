//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: TrackerViews_3
    //Script File: TC_Dashboard_Tracker_View_3.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)

// BAQ data
var baq = "BAQtracker3"

//Dashboard for testing
var dashb = "DashTracker3"
