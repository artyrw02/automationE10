//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions

function Dashboard_Deployment()
{

  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashDepl",
    "menuName" : "DashDepl",
    "orderSequence" : 6,
    "menuType" : "Dashboard-Assembly",
    "dll" : "Dash2Deploy"
  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
    StartSmartClient()

    Login("epicor","Epicor123") 

    ActivateFullTree()

    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  // Step 2
    // Open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    //Enable Dashboard Developer Mode  
      DevMode()

  // Step 3 
    // - Retrieve SalesPersonWorkbench dashboard       
      OpenDashboard("SalesPersonWorkbench")

      var count = 0
      while(true){
        Delay(2000)
        var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

        if (windowsExceptionDialogBtn[0] != null && windowsExceptionDialogBtn[0] != null || windowsExceptionDialogBtn[0] != undefined && windowsExceptionDialogBtn[0] != undefined) {
          if(windowsExceptionDialogBtn[0]["Exists"]){
            Log["Message"]("Validating Warning - System Dashboards may not be modified. - Clicked OK on message ")
            windowsExceptionDialogBtn[0]["Click"]()
            break
          }
        }
        count++

        if(count == 5){
          break
          Runner["Stop"]()
          Log["Error"]("Check")
        }
      }

  // Step 4 
    // - Click on File>Copy Dashboard and enter Dash1 as new ID. Click Ok        
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard") 

      Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("Dash1Deploy")
      ClickButton("OK")
      Delay(2500)

  // Step 5, 6
    // - Click on Tools>Deploy Dashboard. Check mark the "Deploy Smart Client Application" checkbox and click on Deploy        
      DeployDashboard("Deploy Smart Client")
      Log["message"]("Dashboard Dash1Deploy deployed")

  // Step 7
    //  - Click Close All and Ok
      CloseDashboard()

  //  Step 8
    /*
      > Click File > New> New Dashboard
      > Type Dash2 as DefinitionID (and take note of it)and tab out, then leave the Caption and Description fields empty
      > Then add any BAQ clicking file> New> New Query, then click OK to add it to the dashboard
      > Click Tools> Deploy Dashboard"        
    */

    NewDashboard("Dash2Deploy","","","")

    AddQueriesDashboard("COM-90daysSORel")

    Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Deploy Dashboard")

    Delay(1500)
    if (Aliases["Epicor"]["ExceptionDialog"]){
      var eDialog = findValueInString(Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"], "Cancelling AppBuilder operation: Dashboard Description and Caption is required.")  
      if (eDialog) {
        Log['Message']("Validated correctly: Cancelling AppBuilder operation: Dashboard Description and Caption is required.")
      }else{
        Log["Error"]("There is another message on dialog - '" + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"]["OleValue"] + "'")
      }
    }else{
      Log["Error"]("The Application Error dialog didn't appear.")
    }

  // Step 9
    // Close the error message and then click “Save” button(do not fill caption or description field for this step)        
      ClickButton("OK")
      
      // ClickMenu("Save")
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
    // Fill “Caption” field with “MyCaption” text but leave the “Description” field empty
    ClickButton("OK")

    var dashboardCaption = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"]
    
    dashboardCaption["Keys"]("Dash2Deploy")

    // Click “Save” icon"        
    SaveDashboard()

    var dashDescription = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDescription"]

    if(dashDescription["Text"]["OleValue"] == dashboardCaption["Text"]["OleValue"]){
      Log["Checkpoint"]("Dashboard Description was filled with Caption data")
    }else{
      Log["Error"]("Dashboard Description wasn't filled with Caption data")
    }

  // Step 11
    // Clear the “Description” field to leave it empty
    dashDescription["Keys"]("^a[Del]")

  // Step 12
    // Click Tools> Deploy Dashboard"        
    // Select Deploy Smart Client Application, Add Menu Tab, Add Favorite Item and Generate Web Form check boxes. Click on Deploy button and when finished click Ok.       
    DeployDashboard("Deploy Smart Client,Add Favorite Item")

  // Step 13
    // Create the menu

    // Go to System Setup>Security Maintenance> Menu Maintenance. In Menu Maintenance tree select Main Menu> Sales Management> CRM> Setup
    // Select New Menu.
    // Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. Be sure the Enabled check box is selected. Click Save."       

    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    //Creates Menu
    CreateMenu(MenuData)
    
  // Step 14
   // Restart SmartClient
    RestartSmartClient()

 // Step 15
  // On the Home Page from Smart Client on favorites tiles look for the created dashboard under Dashboard Assembly tile and open it
    ActivateFavoritesMenuTab()
    Log["Checkpoint"]("FavoritesMenuTab Activated")

  // Step 16
    OpenDashboardFavMenu("Dash2Deploy")
    Log["Checkpoint"]("Dashboard opened from Favorite Menu")

  //Step 17
    DashboardPanelTest()
    Log["Checkpoint"]("Dashboard tested")

    DeactivateFavoritesMenuTab()
    Log["Checkpoint"]("Favorites MenuTab deactivated")

  //Step 18
    // Open Menu created
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

   //Step 19
    // Test Dashboard
      DashboardPanelTest()
      Log["Checkpoint"]("Dashboard tested from menu")

    /*STEPS 20 TO 33 ARE FOR EWA */

  //Step 34
    // Open Dashboard maintenance
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

   //Step 35 - 36
    // Click Dashboard ID       
    // Search for PartOnHandStatus dashboard and retrieve it       
    Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("PartOnHandStatus")
    Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

  // Step 37
    // Click on Actions> Deploy UI Application        
    // ClickMenu("Actions->Deploy UI Application")
    Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Deploy UI Application")

    Delay(1500)

    var statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
    
    while(statusBar != "Ready"){
      Delay(1500)
      statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
    }
    Log["Message"]("Status - " + statusBar)

  // Step 38
    // Click on Actions> Modify Dashboard       
    // ClickMenu("Actions->Modify Dashboard")
    Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Modify Dashboard")

    var count = 0
      while(true){
        Delay(2000)
        var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

        if (windowsExceptionDialogBtn[0] != null && windowsExceptionDialogBtn[0] != null || windowsExceptionDialogBtn[0] != undefined && windowsExceptionDialogBtn[0] != undefined) {
          if(windowsExceptionDialogBtn[0]["Exists"]){
            Log["Message"]("Validating Warning - System Dashboards may not be modified. - Clicked OK on message ")
            windowsExceptionDialogBtn[0]["Click"]()
            break
          }
        }
        count++

        if(count == 5){
          break
          Runner["Stop"]()
          Log["Error"]("Check")
        }
      }

    // Delay(2500)
    // ClickButton("OK")

  // Step 39
    // Click on Tools> Deploy Dashboard       
    // ClickMenu("Tools->Deploy Dashboard")
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Deploy Dashboard")

  // Step 40
    // Select Deploy Smart Client Application. Click on Deploy button and when finished click Ok.       
    ChangeCheckboxState("chkDeployApplication", true)
    ClickButton("Deploy")
    Delay(5000)
    ClickButton("OK")

    Log["Message"]("Dashboard 'PartOnHandStatus' deployed")
    ExitDashboard()

  // Step 41
    //Return to system management> Upgrade/Mass regeneration> Dashboard maintenance       

  // Step 42
    //Click Actions> Generate web form/Generate All web forms       
    // ClickMenu("Tools->Generate Web Form")
    // ClickMenu("Tools->Generate All Web Forms")
    // Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Generate Web Form")
    Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Generate All Web Forms")
    Delay(3500)
  // Step 43
    // >Go to System Setup> Security Maintenance> Menu Maintenance and open it
    // >Create a new menu, enter MenuID, Name and Order Sequence
    // >Select Dashboard Assembly Menu Type and on Dashboard search for PartOnHandStatus
    // >Save

    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    var MenuData2 = {
      "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
      "menuID" : "DashDep2",
      "menuName" : "DashDepl2",
      "orderSequence" : 7,
      "menuType" : "Dashboard-Assembly",
      "dll" : "PartOnHandStatus"
    }
    //Creates Menu
    CreateMenu(MenuData2)
    Log["Checkpoint"]("Menu creaded correctly.")

  // Step 44  
    // Return to system management> Upgrade/Mass regeneration> Dashboard maintenance       

  // Step 45  
    // Search for SalesPersonWorkBench dashboard and retrieve it       
    // ClickMenu("Clear")
    Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
    ClickButton("Yes")

    Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("SalesPersonWorkBench")
    Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

  // Step 46  
    // Click on Actions> Deploy UI Application        
    // ClickMenu("Actions->Deploy UI Application")
    Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Deploy UI Application")

    Delay(60000)
    var statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
    
    while(statusBar != "Ready"){
      Delay(1500)
      statusBar = Aliases["Epicor"]["DashboardForm"]["WinFormsObject"]("baseStatusBar")["Panels"]["Item"](0)["DisplayText"]
    }
    Log["Message"]("Status - " + statusBar)

  //--- Close Smart Client --------------------------------------------------------------------------------------------------------------------'
    Delay(1000)
    
    DeactivateFullTree()
    Log["Checkpoint"]("FullTree Deactivated")

    CloseSmartClient()
    Log["Checkpoint"]("SmartClient Closed")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
}


function DashboardPanelTest(){
  // ClickMenu("Edit->Refresh")
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Edit|Refresh") 

  var gridDashboard = RetrieveGridsMainPanel()
  
  if(gridDashboard[0]["Rows"]["Count"] > 0 ){
    Log["Checkpoint"]("Grid retrieved " + gridDashboard[0]["Rows"]["Count"]  + " records.")
  }else{
    Log["Error"]("Grid didn't retrieve " + gridDashboard[0]["Rows"]["Count"]  + " records.")
  }

  // ClickMenu("File->Exit")
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
}