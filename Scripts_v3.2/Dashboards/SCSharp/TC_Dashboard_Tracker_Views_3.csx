//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT Sys_Functions

function TC_Dashboard_Tracker_Views_3(){
  
  //--- Setup Estonian culture for Windows -----------------------------------------------------------------------------------------------------'

    /* Step 1: 
      Setup Estonian culture for Windows

      Go to Windows Control Panel-Region-Format
      Select Estonian (Estonia)
      Press Additional Settings button at the bottom of the form->Customize Format window appears
      Select Decimal Symbol=comma. 
      Save */       

      //opens control cmd and opens control panel
      OpenCMD("control")
      
      ChangeRegionControlPanel("Estonian (Estonia)")

      CloseCMD()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
    
    // Step2- Log in  
      StartSmartClient()

      Login("epicor","Epicor123") 

      ActivateFullTree()

      ExpandComp("Epicor Education")

      ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- User Account Security Maintenance -----------------------------------------------------------------------------------------------------'

    /*
      Step No: 3
      Step: "Setup invariant culture on Smart Client
          Go to System Setup-Security Maintenance-User Account Maintenance
          Select your user and check that Format Culture= Invariant Language (Invariant Country)"        
    */
      
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;User Account Security Maintenance")

      var accountMgmtPanel = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]

      //Activate Details tab
      accountMgmtPanel["dockableWindow1"]["Activate"]()

      var detailsTab = accountMgmtPanel["dockableWindow1"]["detailPanel1"]
      detailsTab["txtKeyField"]["Keys"]("epicor")
      detailsTab["txtKeyField"]["Keys"]("[Tab]")

      if (detailsTab["txtName"]["Text"] != ""){
        Log["Checkpoint"]("Data loaded correctly")
      }else{
        Log["Error"]("Data is not loaded correctly")
      }
 
      var formatCulture = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["WinFormsObject"]("cmbFormatCulture")

      if(formatCulture["Text"]["OleValue"] == "Invariant Language (Invariant Country)"){
        Log["Message"]("Invariant culture is set for your user")
      }else{
        Log["Message"]("There was a problem. Invariant culture was not set for your user")
        formatCulture["Keys"]("Invariant Language (Invariant Country)")
        formatCulture["Keys"]("[Tab]")
        Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
        Log["Message"]("Invariant culture is set for your user")
      }

      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Setup BAQs ----------------------------------------------------------------------------------------------------------------------------'
    
    // Step 4
      //Go to Executive Analysis-Business Activity Management-Setup-BAQ 
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      Log["Checkpoint"]("BAQ opened")


      //Click New-New BAQ, tick Shared , Updatableand All Companies checkboxes
      CreateBAQ("BAQtracker3", "BAQtracker3", "Shared,Updatable,Companies")
      
      var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]

      //Go to tab Query Builder and select Part in Phrase Build
      AddTableBAQ(BAQFormDefinition, "Part")

      //Go to tab Display Fields
      //Select PartNum and NetWeight
      AddColumnsBAQ(BAQFormDefinition, "Part", "PartNum,NetWeight")

      //On Update-General Properties check NetWeight to be updatable
        UpdateTabBAQ("Part_NetWeight", "Updatable")

      //On Update-Update Processing select Erp.Part on Business Object...
        //Activate 'Update Processing' tab
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()

        // Select Erp.Part business object
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["updProcess1"]["epiGroupBox1"]["updBOSettings1"]["windowDockingArea1"]["dockableWindow1"]["updBOInfo1"]["btnBusObj"]["Click"]()
        Aliases["Epicor"]["GuessBOForm"]["guessBOPanel"]["epiGroupBox1"]["lbBOs"]["ClickItem"]("Erp.Part")
        Aliases["Epicor"]["GuessBOForm"]["btnOK"]["Click"]()
        Log["Message"]("Business Object was selected")
        
        Delay(1500)
      //Save      
      SaveBAQ()
      Delay(5000)
      ExitBAQ()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    //Step 5
      //Go to Executive Analysis-Business Activity Management-General Operations-Dashboard       
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      
    //Step 6
      //Enable Dashboard Developer Mode
      DevMode()
      Log["Checkpoint"]("DevMode activated")

    // Step 7
      //Click New-New Dashboard, enter any name ID, caption and description and tick All Companies checkbox
      NewDashboard("DashTracker3", "DashTracker3", "DashTracker3", "All Companies")
      
    // Step 8
      //Click New-New Query, select your BAQ, press OK       
      AddQueriesDashboard("BAQtracker3")  

    // Step 9
      // Right click on BAQ name from the tree and select New Tracker View       
      var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
      dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      Log["Message"]("BAQ - right click")

      // click 'New Tracker View' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("BAQtracker3 - New Tracker View was selected from Menu")

    // Step 10
      // Untick Visible checkbox for PartNum. Tick checkbox Prompt for NetWeight. In Condition field select 'GreaterThanOrEqualTo'
      // Tick checkbox Input Prompts Only at the bottom of the window. Press OK"       

      var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
          
      var column = getColumn(TrackerViewsGrid, "Column")
      var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")
      var columnVisible = getColumn(TrackerViewsGrid, "Visible")
      var columnCondition = getColumn(TrackerViewsGrid, "Condition")

      for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
        //Select row and check Prompt checkbox
        var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

        if (cell["Text"] == "Part_PartNum") {
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
          // Uncheck Visible check box on field
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Unchecked"
        }
        if (cell["Text"] == "Part_NetWeight") {
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
          // Check Prompt check box on field
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"

          //Activates 'Condition' column
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Click"]()

          while(true){
            TrackerViewsGrid["Keys"]("[Down]")
            if (TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["EditorResolved"]["SelectedText"] == "GreaterThanOrEqualTo") {
              break
            }
          }
        }
      }
      Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
      Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]() 

    // Step 11
      // >Go to Tools- Deploy Dashboard and tick Deploy Smart Client Application, Add Favorite Item and Generate Webform and click Deploy       
      DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")

    // Step 12 - 13
      // Close Smart Client        
      // Log in again to E10       
      RestartSmartClient()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Testing Dashboard / Favorites ---------------------------------------------------------------------------------------------------------'

    // Step 14
      // On the Home Page from Smart Client on favorites tiles look for the created dashboard under Dashboard Assembly tile and open it
      ActivateFavoritesMenuTab()
      Log["Checkpoint"]("FavoritesMenuTab Activated")

      OpenDashboardFavMenu("DashTracker3")
      Log["Checkpoint"]("Dashboard opened from Favorite Menu")

    // Step 15  
      // On NetWeight field enter 0,5 and click Refresh

      var netWeightFieldTV = Aliases["Epicor"]["MainController"]['FindChild'](["ClrClassName", "FullName"], ["*Editor*", "*numPart_NetWeight*"], 30)
      netWeightFieldTV["Keys"]("^a[Del]"+"0,5")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      
      var grid = RetrieveGridsMainPanel()

      var columnNetWeight = getColumn(grid[0], "Unit Net Weight")
      var flag = true

      for(var i = 0; i < grid[0]["wRowCount"]; i++ ){
        var cell = grid[0]["Rows"]["Item"](i)["Cells"]["Item"](columnNetWeight)
        if (cell["value"] < netWeightFieldTV["value"] ) {
          flag = false
          break
        }
      }

      if(flag){
        Log["Checkpoint"]("Grid retrieved records greater than the value set on tracker view.")
      }else{
        Log["Error"]("Grid didn't retrieve records greater than the value set on tracker view.")
      }

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")


    // Step 16
      // Close Smart Client        
      DeactivateFavoritesMenuTab()
      CloseSmartClient()
      Log["Checkpoint"]("SmartClient Closed")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Setup Invariant culture for Windows ---------------------------------------------------------------------------------------------------'

    /* Step 21: 
        Go to Windows Control Panel-Region-Format
        Select Match Windows display language (recommended)
        Press Additional Settings button at the bottom of the form->Customize Format window appears
        Select Decimal Symbol=dot. 
        Save */       

      //opens control cmd and opens control panel
      OpenCMD("control")
      
      ChangeRegionControlPanel("Match Windows display language (recommended)")

      CloseCMD()

  //-------------------------------------------------------------------------------------------------------------------------------------------'
      Delay(2500)

    // Step 22 - Log in  
      StartSmartClient()

      Login("epicor","Epicor123") 

      ActivateFullTree()

      ExpandComp("Epicor Education")

      ChangePlant("Main Plant")

  //--- User Account Security Maintenance -----------------------------------------------------------------------------------------------------'
    // Aliases["Epicor"]["MenuForm"]["windowDockingArea1"]["dockableWindow3"]["Activate"]()
    /*
      Step No: 23
      Step: "Setup invariant culture on Smart Client
          Go to System Setup-Security Maintenance-User Account Maintenance
          Select your user and check that Format Culture= Estonian(Estonia)
    */
      
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;User Account Security Maintenance")

      var accountMgmtPanel = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]

      //Activate Details tab
      accountMgmtPanel["dockableWindow1"]["Activate"]()

      var detailsTab = accountMgmtPanel["dockableWindow1"]["detailPanel1"]
      detailsTab["txtKeyField"]["Keys"]("epicor")
      detailsTab["txtKeyField"]["Keys"]("[Tab]")

      if (detailsTab["txtName"]["Text"] != ""){
        Log["Checkpoint"]("Data loaded correctly")
      }else{
        Log["Error"]("Data is not loaded correctly")
      }
 
      var formatCulture = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["WinFormsObject"]("cmbFormatCulture")

      formatCulture["Keys"]("Estonian(Estonia)")
      formatCulture["Keys"]("[Tab]")

      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      Log["Message"]("Format Culture changed")

      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Testing Dashboard / Favorites ---------------------------------------------------------------------------------------------------------'

    // Step 24
      // On the Home Page from Smart Client on favorites tiles look for the created dashboard under Dashboard Assembly tile and open it
      ActivateFavoritesMenuTab()
      Log["Checkpoint"]("FavoritesMenuTab Activated")

      OpenDashboardFavMenu("DashTracker3")
      Log["Checkpoint"]("Dashboard opened from Favorite Menu")

    // Step 25  
      // On NetWeight field enter 1,5 and click Refresh

      var netWeightFieldTV = Aliases["Epicor"]["MainController"]['FindChild'](["ClrClassName", "FullName"], ["*Editor*", "*numPart_NetWeight*"], 30)
      netWeightFieldTV["Keys"]("^a[Del]"+"1,5")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      
      var grid = RetrieveGridsMainPanel()

      var columnNetWeight = getColumn(grid[0], "Unit Net Weight")
      var flag = true

      for(var i = 0; i < grid[0]["wRowCount"]; i++ ){
        var cell = grid[0]["Rows"]["Item"](i)["Cells"]["Item"](columnNetWeight)
        // if (cell["value"] < netWeightFieldTV["value"] ) {
        if (cell["value"] < netWeightFieldTV["value"] ) {
          flag = false
          break
        }
      }

      if(flag){
        Log["Checkpoint"]("Grid retrieved records greater than the value set on tracker view.")
      }else{
        Log["Error"]("Grid didn't retrieve records greater than the value set on tracker view.")
      }

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'
      DeactivateFavoritesMenuTab()
      Log["Checkpoint"]("Favorites MenuTab deactivated")

      DeactivateFullTree()
      Log["Checkpoint"]("FullTree Deactivated")

      CloseSmartClient()
      Log["Checkpoint"]("SmartClient Closed")

}

