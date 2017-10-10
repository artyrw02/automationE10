//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT FormLib
//USEUNIT Data_Dashboard_Deployment

function Dashboard_Deployment()
{
 
}

//Steps 2 to 7
function RetrieveSysDashb(){
  ExpandComp(company1)

  ChangePlant(plant1)

  // Step 2
  Log["Message"]("Step 2 - Open Dashboard and enable DevMode")

  // Open Dashboard
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  // Step 3 
  Log["Message"]("Step 3 - Retrieve " + dashb1 + " dashboard")

  // - Retrieve SalesPersonWorkbench dashboard       
  OpenDashboard(dashb1)

  CheckWindowMessage("System Dashboards")

  // Step 4 
  Log["Message"]("Step 4 - Create a copy of Dashboard")

  // - Click on File>Copy Dashboard and enter Dash1 as new ID. Click Ok        
  ClickMenu("File->Copy Dashboard")
  EnterText("txtDefinitionId", dashb1Copy + "[Tab]", "Adding Dashboard Name")
  ClickButton("OK")

  Delay(3500)

  // Step 5, 6
  Log["Message"]("Step 5, 6 - Deploy Dashboard")

  // - Click on Tools>Deploy Dashboard. Check mark the "Deploy Smart Client Application" checkbox and click on Deploy        
  DeployDashboard("Deploy Smart Client")
  Log["message"]("Dashboard " + dashb1Copy + " deployed")

  // Step 7
  Log["Message"]("Step 7 - Close Dashboard")
  //  - Click Close All and Ok
  CloseDashboard()
}
    
// Steps 8 to 12
function CreateDashb(){
 // Step 8
    Log["Message"]("Step 8 - Create new Dashboard '"+ dashb2 + "'")

    NewDashboard(dashb2,"","")
    
    Delay(2500)
    AddQueriesDashboard(dashb2Query1)

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

  // Step 9
    Log["Message"]("Step 9 - Validation of dialog messages")
    // Close the error message and then click “Save” button(do not fill caption or description field for this step)        
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

   // Step 10
    Log["Message"]("Step 10 - Validation of dialog messages")
    // Fill “Caption” field with “MyCaption” text but leave the “Description” field empty
    Delay(2500)
    ClickButton("OK")

    // var dashboardCaption = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"]
    var dashboardCaption = GetTextBox("txtCaption")
    
    EnterText("txtCaption", dashb2 + "[Tab]", "Adding Dashboard Name")

    // Click “Save” icon"        
    SaveDashboard()

    // var dashDescription = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDescription"]
    var dashDescription = GetTextBox("txtDescription")

    if(dashDescription["Text"]["OleValue"] == dashboardCaption["Text"]["OleValue"]){
      Log["Checkpoint"]("Dashboard Description was filled with Caption data")
    }else{
      Log["Error"]("Dashboard Description wasn't filled with Caption data")
    }

  // Step 11
    Log["Message"]("Step 11 - Clear 'Description' field Validation of dialog messages")
    // Clear the “Description” field to leave it empty
    dashDescription["Keys"]("^a[Del]")

  // Step 12
    Log["Message"]("Step 12 - Deploy Dashboard. Validation of dialog messages")
    // Click Tools> Deploy Dashboard"        
    // Select Deploy Smart Client Application, Add Menu Tab, Add Favorite Item and Generate Web Form check boxes. Click on Deploy button and when finished click Ok.       
    DeployDashboard("Deploy Smart Client,Add Favorite Item")  
}
 
// Steps 13
function CreateMenu1(){
  // Step 13
  Log["Message"]("Step 13 - Creating menu")
  
  // Create the menu
  // Go to System Setup>Security Maintenance> Menu Maintenance. In Menu Maintenance tree select Main Menu> Sales Management> CRM> Setup
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  // Select New Menu.
  // Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. Be sure the Enabled check box is selected. Click Save."       
  CreateMenu(MenuData)
}

// Steps 14
function RestartE10(){
  // Step 14
  Log["Message"]("Step 14 - Restart SmartClient")

   // Restart SmartClient
  RestartSmartClient()  
}

// Steps 15 to 17
function TestDeployedDbFavMenu(){
  // Step 15
  Log["Message"]("Step 15 - Activate Favorites Tab")

  // On the Home Page from Smart Client on favorites tiles look for the created dashboard under Dashboard Assembly tile and open it
  ActivateFavoritesMenuTab()
  Log["Message"]("FavoritesMenuTab Activated")

  // Step 16
  Log["Message"]("Step 15 - Opening Dashboard from Favorites Tab")
  OpenDashboardFavMenu(dashb2)
  Log["Message"]("Dashboard opened from Favorite Menu")

  // Step 17
  Log["Message"]("Step 17 - Refresh Dashboard and test data")
  DashboardPanelTest()
  Log["Message"]("Dashboard tested")

  DeactivateFavoritesMenuTab()
  Log["Message"]("Favorites MenuTab deactivated")  
}

// Steps 18,19
function TestDeployedDbMenu(){
  // Step 18
  Log["Message"]("Step 18 - Open menu created")
  
  // Open Menu created
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  //Step 19
  Log["Message"]("Step 19 - Open menu created")
    
  // Test Dashboard
  DashboardPanelTest()
  Log["Message"]("Dashboard tested from menu")  

  /*STEPS 20 TO 33 ARE FOR EWA */
  Log["Message"]("Step 20 to 33 - EWA testing")
}

// Steps 34 to 37
function SysDashbMaintenanceDeploy(){
  // Step 34
  Log["Message"]("Step 34 - Open Dashboard Maintenance")

  // Open Dashboard maintenance
  MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

 //Step 35 - 36
  Log["Message"]("Step 35,36 - Retrieve '"+ dashb3 + "' Dashboard")

  // Search for PartOnHandStatus dashboard and retrieve it  
  EnterText("txtKeyField", dashb3 + "[Tab]", "Adding Name")   
  
  Delay(10000)

  // Step 37
  Log["Message"]("Step 37 - Click on Actions> Deploy UI Application")
  
  // Click on Actions> Deploy UI Application        
  ClickMenu("Actions->Deploy UI Application")
  
  /*
  var statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
  
  while(statusBar != "Ready"){
    Delay(1500)
    statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
  }
  Log["Message"]("Status - " + statusBar)*/
  
  Delay(10000)
  
  Delay(2500)
}

// Steps 38 to 40
function SysDashbMaintenanceModify(){
  // Step 38
  Log["Message"]("Step 38 - Click on Actions> Modify Dashboard")

  // Click on Actions> Modify Dashboard       
  ClickMenu("Actions->Modify Dashboard")

  CheckWindowMessage("System Dashboards")

  // Step 39
  Log["Message"]("Step 39 - Tools> Deploy Dashboard")
  // Click on Tools> Deploy Dashboard       
  ClickMenu("Tools->Deploy Dashboard")
  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Deploy Dashboard")

  // Step 40
  Log["Message"]("Step 40 - Select Deploy Smart Client Application")
  // Select Deploy Smart Client Application. Click on Deploy button and when finished click Ok.       
  CheckboxState("chkDeployApplication", true)
  ClickButton("Deploy")
  
  Delay(5000)
  
  ClickButton("OK")

  Log["Message"]("Dashboard '" + dashb3 + "' deployed")
  ExitDashboard()  
}

// Steps 41, 42
function SysDashbMaintenanceGenerateWeb(){
  // Step 41
  //Return to system management> Upgrade/Mass regeneration> Dashboard maintenance       

  // Step 42
  Log["Message"]("Step 42 - Select Deploy Smart Client Application")

  ClickMenu("Actions->Generate All Web Forms")

  Delay(3500)

  ClickMenu("File->Exit")
}

// Step 43
function CreateMenu2(){
  Log["Message"]("Step 43 - Open Menu Maintenance and create new menu")

  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu(MenuData2)
  Log["Message"]("Menu creaded correctly.")  
}

//Steps 44 to 46
function SysDashbMaintenanceDeploy2(){

  // Step 44  
  // Return to system management> Upgrade/Mass regeneration> Dashboard maintenance       
  MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  // Step 45  
  Log["Message"]("Step 45 - retrieve '" + dashb1 +"'")
  
  EnterText("txtKeyField", dashb1 + "[Tab]", "Adding dashboard Name")

  Delay(2500)

  // Step 46  
  Log["Message"]("Step 46 - Click on Actions> Deploy UI Application")
  ClickMenu("Actions->Deploy UI Application")

  // Delay(60000)
  // var statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
  
  // while(statusBar != "Ready"){
  //   Delay(1500)
  //   statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
  // }
  // Log["Message"]("Status - " + statusBar)

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