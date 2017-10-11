//USEUNIT General_Functions
//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: All_companies
    //Script File: TC_Dashboard_All_companies.csx
//***************************************************************************

//Companies that will be used through the test case
var company1 = "Epicor Education"
var plant1 = "Main Plant"
var company2 = "Epicor Mexico"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1,plant1)
var treeMainPanel2 = setCompanyMainTree(company2)

//BAQ definition

var baqData1 = {
	"Id" : "baqAllcomp",
	"Description" : "baqAllcomp",
	"Table" : "Customer",
	"Columns" : "Company,CustID,CustNum,Name,Address1",
	"GeneralConfig" : "chkShared,chkAllCompanies"
}

//Dashboards used for Test
var dashb1 = "TestDashBD"
var dashb2 = "JobStatusPlus"

//Dashboards configurations
var dashb1Config = "chkAllCompanies"

//Queries
var dashb1Query1 = baqData1["Id"]

//Renamed Dashboards
var dashb1Copy = "TestDashBD-2"
var dashb2Copy = "TestDashBD-3"

//Data for Menu
var MenuData1 = {
	"menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
	"menuID" : "DashMenu",
	"menuName" : "DashMenu",
	"orderSequence" : 3,
	"menuType" : "Dashboard-Assembly",
	"dll" : dashb1,
	"validations" : "Companies,Enable,Web Access"
}    
var MenuData2 = {
	"menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
	"menuID" : "DashMenu2",
	"menuName" : "DashMenu2",
	"orderSequence" : 4,
	"menuType" : "Dashboard-Assembly",
	"dll" : dashb1,
	"validations" : "Enable,Web Access"
}
