//***************************************************************************
    //Description: definition of the variables used in the script scenario.
    //Testcase type: 'Dashboard'
    //TC Name: Solution Workbench scenario 2
    //Script File: TC_Dashboard_Solution_Workbench_2.csx
//***************************************************************************

//Companies that will be used thru the test case
var company1 = "Epicor Europe"
var company2 = "Epicor Education"
var plant2 = "Main"
var company3 = "Epicor Mexico"

//Used to navigate thru the Main tree panel
var treeMainPanel1 = setCompanyMainTree(company1)
var treeMainPanel2 = setCompanyMainTree(company2,plant2)
var treeMainPanel3 = setCompanyMainTree(company3)

//BAQs used for Test
var baqs1 = "Test2BAQ1"
var baqs2 = "Test2BAQ2"
var baqs3 = "Test2BAQ3"

//Dashboards used for Test
var dashb1 = "SW2TestDashBD1"
var dashb2 = "SW2TestDashBD2"
var dashb3 = "SW2TestDashBD3"
var dashb4 = "SW2TestDashBD4"

//External Datasource definition
var dsInfo = "Test2DT"
var dsType = "Test2DTType"

var ADOprovider = "SqlClient Data Provider"

//External DS connection properties
var dsName = " TYRELL"
var initialCatalog = "erp10Staging"
var userID = "sa"
var password = "Epicor123"

//Solution por EPIC05 definition
var solutionDefEpic05 = "Sol2EPIC05"
var solDefEpic05Type = "Sol2EPIC05Type"

//Solution por EPIC06 definition
var solutionDefEpic06 = "Sol2EPIC06"
var solDefEpic06Type = "Sol2EPIC06Type"

//Solution por EPIC07 definition
var solutionDefEpic07 = "Sol2EPIC07"
var solDefEpic07Type = "Sol2EPIC07Type"

//Location of exported solution
var solutionEPIC05 = "C:\\Users\\Administrator\\Documents\\"+solutionDefEpic05+"_Customer Solution_3.1.600.0"
var solutionEPIC06 = "C:\\Users\\Administrator\\Documents\\"+solutionDefEpic06+"_Customer Solution_3.1.600.0"
var solutionEPIC07 = "C:\\Users\\Administrator\\Documents\\"+solutionDefEpic07+"_Customer Solution_3.1.600.0"