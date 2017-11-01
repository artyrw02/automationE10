//USEUNIT BAQs_Functions
//USEUNIT ControlFunctions
//USEUNIT Dashboards_Functions
//USEUNIT Data_Dashboard_Deployment
//USEUNIT FormLib
//USEUNIT General_Functions
//USEUNIT Grid_Functions

function Dashboard_Deployment()
{
 
}

//Steps 2 to 7
function RetrieveSysDashb(){
  ExpandComp(company1)

  ChangePlant(plant1)

  Log["Message"]("Step 2 - Open Dashboard and enable DevMode")

  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  DevMode()

  Log["Message"]("Step 3 - Retrieve " + dashb1 + " dashboard")

  OpenDashboard(dashb1)

  CheckWindowMessageModals()
  Delay(2500)
  ClickButton("OK")

  Log["Message"]("Step 4 - Create a copy of Dashboard")

  ClickMenu("File->Copy Dashboard")
  EnterText("txtDefinitionId", dashb1Copy + "[Tab]", "Adding Dashboard Name")
  ClickButton("OK")

  Delay(3500)

  Log["Message"]("Step 5, 6 - Deploy Dashboard")

  DeployDashboard("Deploy Smart Client")
  Log["message"]("Dashboard " + dashb1Copy + " deployed")


  Log["Message"]("Step 7 - Close Dashboard")

  CloseDashboard()
}
    
// Steps 8 to 12
function CreateDashb(){
    Log["Message"]("Step 8 - Create new Dashboard '"+ dashb2 + "'")

    NewDashboard(dashb2,"","")
    
    Delay(2500)
    AddQueriesDashboard(dashb2Query1)
    
    Delay(2500)
    ClickMenu("Tools->Deploy Dashboard")

    Delay(1500)
    if (Aliases["Epicor"]["ExceptionDialog"]){
      var eDialog = findValueInString(Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"], "Cancelling AppBuilder operation: Dashboard Description and Caption is required.")  
      if (eDialog) {
        Log["Message"]("Validated correctly: Cancelling AppBuilder operation: Dashboard Description and Caption is required.")
      }else{
        Log["Error"]("There is another message on dialog - '" + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"] + "'")
      }
    }else{
      Log["Error"]("The Application Error dialog didn't appear.")
    }

    Log["Message"]("Step 9 - Validation of dialog messages")
    ClickButton("OK")
    
    SaveDashboard()

    if (Aliases["Epicor"]["ExceptionDialog"]){
      var eDialog = findValueInString(Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"], "Cancelling Save operation: Dashboard Description and Caption is required.")
        
      if (eDialog) {
        Log['Message']("Validated correctly: Cancelling Save operation: Dashboard Description and Caption is required.")
      }else{
        Log["Error"]("There is another message on dialog - '" + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"] + "'")
      }
    }else{
      Log["Error"]("The Application Error dialog didn't appear.")
    }

    Log["Message"]("Step 10 - Validation of dialog messages")
    Delay(2500)
    ClickButton("OK")

    var dashboardCaption = GetTextBox("txtCaption")
    
    EnterText("txtCaption", dashb2 + "[Tab]", "Adding Dashboard Name")

    SaveDashboard()

    var dashDescription = GetTextBox("txtDescription")

    if(dashDescription["Text"]["OleValue"] == dashboardCaption["Text"]["OleValue"]){
      Log["Checkpoint"]("Dashboard Description was filled with Caption data")
    }else{
      Log["Error"]("Dashboard Description wasn't filled with Caption data")
    }

    Log["Message"]("Step 11 - Clear 'Description' field Validation of dialog messages")
    
    dashDescription["Keys"]("^a[Del]")
    
    Delay(2500)
    Log["Message"]("Step 12 - Deploy Dashboard. Validation of dialog messages")
    DeployDashboard("Deploy Smart Client,Add Favorite Item")  
}
 
function CreateMenu1(){
  Log["Message"]("Step 13 - Creating menu")
  
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)
}

function RestartE10(){
  Log["Message"]("Step 14 - Restart SmartClient")

  RestartSmartClient()  
}

function TestDeployedDbFavMenu(){
  Log["Message"]("Step 15 - Activate Favorites Tab")

  Log["Message"]("Step 15 - Opening Dashboard from Favorites Tab")
  OpenDashboardFavMenu(dashb2)
  Log["Message"]("Dashboard opened from Favorite Menu")

  Log["Message"]("Step 17 - Refresh Dashboard and test data")
  DashboardPanelTest()
  Log["Message"]("Dashboard tested")
}

function TestDeployedDbMenu(){
  Log["Message"]("Step 18 - Open menu created")
  
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  Log["Message"]("Step 19 - Open menu created")
    
  DashboardPanelTest()
  Log["Message"]("Dashboard tested from menu")  

  Log["Message"]("Step 20 to 33 - EWA testing")
}

function SysDashbMaintenanceDeploy(){
  Log["Message"]("Step 34 - Open Dashboard Maintenance")

  MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  Log["Message"]("Step 35,36 - Retrieve '"+ dashb3 + "' Dashboard")

  EnterText("txtKeyField", dashb3 + "[Tab]", "Adding Name")   
  
  Delay(10000)

  Log["Message"]("Step 37 - Click on Actions> Deploy UI Application")
  
  ClickMenu("Actions->Deploy UI Application")
  
  Delay(10000)
  
  Delay(2500)
}

function SysDashbMaintenanceModify(){
  Log["Message"]("Step 38 - Click on Actions> Modify Dashboard")

  E10["Refresh"]()

  Delay(2500)
  
  ClickMenu("Actions->Modify Dashboard")

  CheckWindowMessageModals()
  Delay(2500)
  ClickButton("OK")

  Log["Message"]("Step 39 - Tools> Deploy Dashboard")
  E10["Refresh"]()
  
  Delay(2500)
  ClickMenu("Tools->Deploy Dashboard")

  Log["Message"]("Step 40 - Select Deploy Smart Client Application")
  CheckboxState("chkDeployApplication", true)
  ClickButton("Deploy")
  
  Delay(5000)
  
  ClickButton("OK")

  Log["Message"]("Dashboard '" + dashb3 + "' deployed")
  ExitDashboard()  
}

function SysDashbMaintenanceGenerateWeb(){
  Log["Message"]("Step 42 - Select Deploy Smart Client Application")

  ClickMenu("Actions->Generate All Web Forms")

  Delay(3500)

  ClickMenu("File->Exit")
}

function CreateMenu2(){
  Log["Message"]("Step 43 - Open Menu Maintenance and create new menu")

  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData2)
  Log["Message"]("Menu creaded correctly.")  
}

function SysDashbMaintenanceDeploy2(){

  MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  Log["Message"]("Step 45 - retrieve '" + dashb1 +"'")
  
  EnterText("txtKeyField", dashb1 + "[Tab]", "Adding dashboard Name")

  Delay(2500)

  Log["Message"]("Step 46 - Click on Actions> Deploy UI Application")
  ClickMenu("Actions->Deploy UI Application")

  ClickMenu("File->Exit")
}


function DashboardPanelTest(){
  ClickMenu("Edit->Refresh")
  Delay(5500)
  var gridDashboard = RetrieveGridsMainPanel()
  
  if(gridDashboard[0]["Rows"]["Count"] > 0 ){
    Log["Checkpoint"]("Grid retrieved " + gridDashboard[0]["Rows"]["Count"]  + " records.")
  }else{
    Log["Error"]("Grid retrieved " + gridDashboard[0]["Rows"]["Count"]  + " records.")
  }

  ClickMenu("File->Exit")
}