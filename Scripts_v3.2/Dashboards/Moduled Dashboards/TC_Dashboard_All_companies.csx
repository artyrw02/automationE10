//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT ControlFunctions
//USEUNIT FormLib
//USEUNIT Data_Dashboard_All_companies

function TC_Dashboard_All_companies(){
  
}


//Step 2
function CreateBAQ(){
  ExpandComp(company1)

  ChangePlant(plant1)

  Log["Message"]("Step 2")

  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  CreateSimpleBAQ(baqData1)
}

//Steps from 3 to 11
function CreateDashboard(){
  Log["Message"]("Step 3")

  //Navigate and open Dashboard
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  Log["Message"]("Step 4")
  NewDashboard(dashb1, dashb1, dashb1, dashb1Config)
    
  Log["Message"]("Step 5")
  AddQueriesDashboard(baqData1["Id"])
        
  SaveDashboard()

  Log["Message"]("Step 9")
  // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
  var dashboardTree = GetTreePanel("DashboardTree")

  dashboardTree["ClickItem"]("Queries|" + baqData1["Id"] + ": " + baqData1["Id"])

  // var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
  // dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)


  // click 'Properties' option from menu
  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  // Log["Message"]("'New Tracker View' was selected from Menu")
  ClickMenu("File->New...->New Tracker View")
  Log["Message"]("BAQ - right click")

  ClickButton("OK")
 
  Log["Message"]("Step 10, 11")
  DeployDashboard("Deploy Smart Client,Generate Web Form")

  ExitDashboard()
}

//Step 12
function CreateMenuDashboard(){

  Log["Message"]("Step 12")
  
  //Open Menu maintenance   
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu(MenuData1)
}

//Step 13
function RestartE10(){
  Log["Message"]("Step 13")
  
  //Restart SmartClient
  RestartSmartClient()
}

// Steps 14 to 16
function TestMenuDataDB1_EPIC06(){

  ExpandComp(company1)

  ChangePlant(plant1)

  Log["Message"]("Step 14")

  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData1["menuName"])

  Log["Message"]("Step 15")

  ClickMenu("Edit->Refresh")

  var gridPanel = GetGridMainPanel(baqData1["Id"])

  if(gridPanel["Rows"]["Count"] > 0){
    Log["Checkpoint"]("Data from grid was populated")
  }else{
    Log["Error"]("Data from grid was not populated")
  }

  Log["Message"]("Step 16")

  ClickMenu("File->Exit")
}

//Step 17, 18
function Dashb1Exists_EPIC07(){

    Log["Message"]("Step 17, 18")
    ExpandComp(company2)

    MainMenuTreeViewSelect(treeMainPanel2 + "Sales Management;Customer Relationship Management;Setup;" + MenuData1["menuName"])

    var verifyForm = VerifyForm(MenuData1["menuName"])

    if(verifyForm){
      Log["Checkpoint"]("Menu is available for this company")
    }else{
      Log["Error"]("Menu is not available for this company")
    }

    ClickMenu("File->Exit")
}

// Steps 19 to 21
function VerifyAllCompaniesChk() {

    Log["Message"]("Step 19")
    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

    // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
    Log["Message"]("Dashboard opened")

    //Enable Dashboard Developer Mode  
    DevMode()
    Log["Message"]("DevMode activated")

    Log["Message"]("Step 20")
    OpenDashboard(dashb1)

    //Verify dialog message
    CheckWindowMessage("Dashboard")

    Delay(3500)
    OpenPanelTab("General")

    // Verify checkbox 'All companies' 
    var chkAllCompanies = GetCheckbox("chkAllCompanies")

    if(chkAllCompanies["Checked"]){
     Log["Message"]("Checkbox 'All Companies is checked")
    }else{
      Log["Error"]("Checkbox 'All Companies is not checked")
    }      

    Log["Message"]("Step 21")
    CloseDashboard()
}

// Steps 22 to 27
function CreateDashboard2(){

  Log["Message"]("Step 22")

  NewDashboard(dashb1, dashb1, dashb1)

  var chkAllCompanies = GetCheckbox("chkAllCompanies")

  if(chkAllCompanies["ReadOnly"]){
    Log["Message"]("Checkbox 'All Companies' is disabled")
  }else{
    Log["Error"]("Checkbox 'All Companies' is not disabled")
  }

  Log["Message"]("Step 23, 24")
  AddQueriesDashboard(baqData1["Id"])
  
  SaveDashboard()

  Log["Message"]("Step 26, 27")
  DeployDashboard("Deploy Smart Client,Generate Web Form")

  ExitDashboard()  
}

//Steps 28
function CreateMenu2(){

  Log["Message"]("Step 28")

  //Open Menu maintenance   
  MainMenuTreeViewSelect(treeMainPanel2 + "System Setup;Security Maintenance;Menu Maintenance")

  Delay(2000)

  //Creates Menu
  CreateMenu(MenuData2)  
}    

//Step 29
function RestartE10_2(){
  Log["Message"]("Step 19")
  
  //Restart SmartClient
  RestartSmartClient()
}

//Steps 30, 31
function TestMenuDataDB2_EPIC07(){  
  Log["Message"]("Step 30")
  ExpandComp(company2)

  MainMenuTreeViewSelect(treeMainPanel2 + "Sales Management;Customer Relationship Management;Setup;"+MenuData2["menuName"])

  Log["Message"]("Step 31")

  ClickMenu("Edit->Refresh")

  var gridPanel = GetGridMainPanel(baqData1["Id"])

  if(gridPanel["Rows"]["Count"] > 0){
    Log["Checkpoint"]("Data from grid was populated")
  }else{
    Log["Error"]("Data from grid was not populated")
  }

  ClickMenu("File->Exit")   
}   

//Steps 32, 33
function RetrieveMenuDataDB1_EPIC06(){

  Log["Message"]("Step 32")
  ExpandComp(company1)
  ChangePlant(plant1)
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData1["menuName"])

  Log["Message"]("Step 33")

  ClickMenu("Edit->Refresh")

  var gridPanel = GetGridMainPanel(baqData1["Id"])

  if(gridPanel["Rows"]["Count"] > 0){
    Log["Checkpoint"]("Data from grid was populated")
  }else{
    Log["Error"]("Data from grid was not populated")
  }

  ClickMenu("File->Exit")
}    

//Steps 34 to 42
function DashbMaintenance(){

    Log["Message"]("Step 34")
    ExpandComp(company1)
    ChangePlant(plant1)
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

    Log["Message"]("Step 35")

    EnterText("txtKeyField", dashb1 + "[Tab]", "Adding ID of dashboard")
      
    //Description field used to validate
    var descrField = GetTextBox("epiTextBox1")

    if(descrField["Text"]["OleValue"] != ""){
      Log["Checkpoint"]("Data from dashboard " + dashb1 + " was loaded correctly")
    }else {
      Log["Error"]("Data from dashboard " + dashb1 + " was not loaded correctly")
    }

    ClickMenu("Edit->Clear")
    ClickButton("Yes")
   
    Log["Message"]("Step 36")
    ClickButton("Dashboard ID...")

    Log["Message"]("Step 37")

    ClickButton("Options")

    Log["Message"]("Step 38")           

    EnterText("neRecordCnt", 20, "Record count for search parameters")
    ClickButton("OK")
 
    Log["Message"]("Step 39")

    CheckboxState("chkInUse", "Indeterminate")
    CheckboxState("chkSystem", "Indeterminate")

    Delay(2000)
 
    Log["Message"]("Step 40")

    ClickButton("Search")

    var gridSearchResults = Aliases["Epicor"]["DashboardSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

    if(gridSearchResults["Rows"]["Count"] <= 20){
      Log["Checkpoint"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
    }else{
      Log["Error"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
    }

    Log["Message"]("Step 41")

    EnterText("txtStartWith1", dashb1, "Adding dashboard to search")
    ClickButton("Search")

    var idColumnGrid = getColumn(gridSearchResults, "ID")

    for (var i = 0; i < gridSearchResults["Rows"]["Count"]; i++) {
      if(gridSearchResults["Rows"]["Item"](i)["Cells"]["Item"](idColumnGrid)["Text"]["OleValue"] == dashb1){
          // Aliases["Epicor"]["DashboardSearchForm"]["ultraStatusBar2"]["btnOK"]["Click"]()
          ClickButton("OK")
          break
      }else{
        Log["Error"]("Dashboard " + dashb1 + " not found")
      }
    }
    
    var descrField = GetTextBox("txtKeyField")

    if(descrField["Text"] != ""){
      Log["Message"]("Dashboard " + dashb1 + " loaded")
    }else{
      Log["Error"]("Dashboard " + dashb1 + " was not loaded")
    }

    Log["Message"]("Step 42")

    ClickMenu("Actions->Modify Dashboard")

    Delay(5000)
    
    var verifyForm = VerifyForm(dashb1)
    // if(Aliases["Epicor"]["Dashboard"]["Exists"] == true && Aliases["Epicor"]["Dashboard"]["WndCaption"] == dashb1){
    if (verifyForm) {
      Log["Checkpoint"]("dashboard designer is opened")
    }else{
      Log["Error"]("dashboard designer was not opened")
    }  
}
    
//Steps 43 to 45
function CopyDashb1() {
 
  Log["Message"]("Step 43")
  Delay(2000)

  ClickMenu("File->Copy Dashboard")

  Log["Message"]("Step 44")

  EnterText("txtDefinitionId", dashb1Copy + "[Tab]", "Adding Id for dashboard Copy")
  ClickButton("OK")

  Log["Message"]("Step 45")
  SaveDashboard()
  ExitDashboard()
}

// Steps 47 to 51
function CreateCopySysDashb(){
    // var test2 = QueryDatabaseDashboards("TestDashBD-2")

    Log["Message"]("Step 47")

    ClickMenu("Edit->Clear")
    ClickButton("Yes")

    EnterText("txtKeyField", dashb2 + "[Tab]", "Adding text of dashboard") 

    Log["Message"]("Step 48")

    ClickMenu("Actions->Modify Dashboard")
   
    CheckWindowMessage("System Dashboards")
    
    Log["Message"]("Step 49")
    Delay(2000)

    ClickMenu("File->Copy Dashboard")

    Log["Message"]("Step 50")
    Delay(2000)

    EnterText("txtDefinitionId", dashb2Copy + "[Tab]", "Adding text of dashboard") 
    ClickButton("OK")
         
    Log["Message"]("Step 51")
    SaveDashboard()
}   
   
//Steps 52 to 55   
function TestSQLDashbs(){
  // Step No: 52
 
  //Query on SQL the dashboards
    Log["Message"]("Step 52")
    var test1 = QueryDatabaseDashboards(dashb1)
    Log["Message"]("Query with Dashboard ID " + dashb1 + " retrieved " + test1["RecordCount"] + " records.")

    var test2 = QueryDatabaseDashboards(dashb1Copy)
    Log["Message"]("Query with Dashboard ID " + dashb1Copy + " retrieved " + test2["RecordCount"] + " records.")
    
    Log["Message"]("Step 53,54,55")
    DeleteDashboard(dashb2Copy)
    
    var test3 = QueryDatabaseDashboards(dashb2Copy)
    Log["Message"]("Query with Dashboard ID " + dashb2Copy + " retrieved " + test3["RecordCount"] + " records.")

    ExitDashboard()
}   

  