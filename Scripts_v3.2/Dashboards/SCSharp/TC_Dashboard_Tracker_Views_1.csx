//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
var continueTest = true

function TC_Dashboard_Tracker_Views_1(){
  
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashT1",
    "menuName" : "DashTrack1",
    "orderSequence" : 100,
    "menuType" : "Dashboard-Assembly",
    "dll" : "DashTracker"
  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
    
    // Step1- Log in  
      StartSmartClient()

      Login("epicor","Epicor123") 

      ActivateFullTree()

      ExpandComp("Epicor Education")

      ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates BAQs --------------------------------------------------------------------------------------------------------------------------'
    //Step2- Copy zCustomer01 BAQ
      //Open Business Activity Query to create BAQ   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      Log["Checkpoint"]("BAQ opened")

      //Create a copy of 'zCustomer01' table
      CopyBAQ("zCustomer01", "zCust01")
      Log["Checkpoint"]("BAQ 'zCustomer01' copied to 'zCust01'")
      
      //Go to Query Builder Tab and on Display Fields tab add GroupCode to Display Column(s)
      var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
      
      AddColumnsBAQ(BAQFormDefinition, "Customer", "GroupCode")

      AnalyzeSyntaxisBAQ(BAQFormDefinition)
      TestResultsBAQ(BAQFormDefinition)
      SaveBAQ()
      ExitBAQ()
      Log["Checkpoint"]("zCust01 created")

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'
    //Step3- Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")
      Log["Checkpoint"]("Dashboard opened")
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

    //Step4- Creating dashboard
      NewDashboard("DashTracker", "DashTracker", "DashTracker description", "Refresh All")
    
    //Step5- Add query
      var rect 
    
      AddQueriesDashboard("zCust01")
      
    //Step6- Add a New Tracker View       
      if(!Aliases["Epicor"]["ExceptionDialog"]['Exists']){
        //Right click on the query summary and click on the Query        
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](0)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)
        Log["Message"]("BAQ - right click")

        // click 'New Tracker View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
        Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

        // Step7- Select Clear All button        
        if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
          Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnClearAll"]["Click"]()
        }

        //Step8- Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

        //Save dashboard
        SaveDashboard("DashTracker", "DashTracker description")

        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)

        //Step9- click 'Properties' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQTrackerV1 Summary - Properties was selected from Menu")

        //Step10 - Select Select All       
        if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
          Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnSelectAll"]["Click"]()
        }
        
        //Step11 - Check Prompt check box on GroupCode field        
        var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
        
        var column = getColumn(TrackerViewsGrid, "Column")
        var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")

        //find the row where GroupCode is located
        for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
          //Select Customer_GroupCode row and check Prompt checkbox
          var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

          if (cell["Text"] == "Customer_GroupCode") {
            Log["Message"]("Data doesn't match with the parameter given = promptIndex > "+ columnPrompt )
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            // Check Prompt check box on GroupCode field
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          }
        }

        //Step12- Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

        //Save dashboard
        SaveDashboard("DashTracker", "DashTracker description")

        //Step13- Click Refresh       
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

        //Step15- Right click on the query summary and click on the Query to add a New Tracker View       
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](0)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)
        Log["Message"]("BAQ - right clicked")

        // click 'New Tracker View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
        Log["Message"]("New Tracker View was selected from Menu")

        //Step16- Check Prompt check box on GroupCode field       
        for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
          //Select Customer_GroupCode row and check Prompt checkbox
          var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

          if (cell["Text"] == "Customer_GroupCode") {
            Log["Message"]("Data doesn't match with the parameter given = promptIndex > "+ columnPrompt )
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            // Check Prompt check box on GroupCode field
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          }
        }

        //Step17- Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

        //Save dashboard
        SaveDashboard("DashTracker", "DashTracker description")

        //Step18- Click Refresh       
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      }else{
        Log["Error"]("Exception dialog appeared with the following text > " + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"])
        continueTest = false
      }
    //   end of first query

    //Step20- Add a New Query and select the same Query you previously created - second query
      AddQueriesDashboard("zCust01")

      //Validates if there is no Exception Dialog before continue the script execution
      if(!Aliases["Epicor"]["ExceptionDialog"]['Exists']){
        //Right click on the query summary and click on the Query        
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)
        Log["Message"]("BAQ - right click")

        //Step21- click 'New Tracker View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
        Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

        //Step22- Check Prompt check box for CustID field and on Condition select StartsWith        
        var columnCondition = getColumn(TrackerViewsGrid, "Condition")

        //find the row where CustID is located
        for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
          //Select Customer_CustID row and check Prompt checkbox
          var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

          if (cell["Text"] == "Customer_CustID") {
            Log["Message"]("Data doesn't match with the parameter given = promptIndex > "+ columnPrompt )
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            // Check Prompt check box on GroupCode field
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"

            //Activates 'Condition' column
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Click"]()

            while(true){
              TrackerViewsGrid["Keys"]("[Down]")
              if (TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["EditorResolved"]["SelectedText"] == "StartsWith") {
                break
              }
            }
          }
        }

        //Step23- Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

        //Save dashboard
        SaveDashboard("DashTracker", "DashTracker description")

        //Step24- Click Refresh       
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All")
        
        // Maximize Dashboard
        Aliases["Epicor"]["Dashboard"]["Maximize"]();

      }else{
        Log["Error"]("Exception dialog appeared with the following text > " + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"])
        continueTest = false
      }

      if(continueTest){
        //Step25- Right Click on your tracker view and select the option Customize Tracker View.        
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](1)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)

        // click 'Customize Tracker View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Customize Tracker View");
        Log["Message"]("BAQTrackerV1 Summary - Customize Tracker View was selected from Menu")

        //Step26- Go to Wizards> Sheet Wizard tab and click on button New Custom Sheet, and select the available parent docking sheet.        
        var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
        //Wizards
        CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
        //Sheet Wizard
        CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true
        
        if (CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["Exists"]) {
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()

          // select the available parent docking sheet.
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"](0)

          //Step27- Add a Name, Text and Tab Text for the new sheet.
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("test")
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("test")
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("test")
          
          // Click on the arrow pointing to the right       
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
          
          // Select the newly added tab on your tracker
          CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstCustomSheets"]["ClickItem"](0)
          
          Aliases["Epicor"]["Dashboard"]["Activate"]()

          var dashboardPanel = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow3"]["dbFillPanel1"]

          var custTrackerTestTabDashbPanelChild = dashboardPanel["FindAllChildren"]("FullName", "*test*", 30)["toArray"]();
          custTrackerTestTabDashbPanelChild[0]["Parent"]["Activate"]()
          
          Aliases["Epicor"]["CustomToolsDialog"]["Activate"]()
          
          //Step28- Select Tools>Tool box from the customization tools dialog
          Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");

          //Step29- On your new tab drop a label, a text box, add a combo box and a date time editor. Then Save and close the customization window        
          Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiLabel", -1, 50, 10);
          epiBasePanel =  custTrackerTestTabDashbPanelChild[0]
          epiBasePanel["Click"](90, 35);
          // epiBasePanel["Click"](90, 35);
          Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
          Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiTextBox", -1, 74, 11);
          epiBasePanel["Click"](190, 35);
          // epiBasePanel["Click"](232, 34);
          // epiBasePanel["epiTextBox12"]["Click"](0, 0);
          Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
          Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiCombo", -1, 63, 8);
          epiBasePanel["Click"](290, 35);
          // epiBasePanel["Click"](99, 91);
          Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
          Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiDateTimeEditor", -1, 89, 12);
          epiBasePanel["Click"](390, 35);
          // epiBasePanel["Click"](239, 89);

        }

        //Save Customization and close
        Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Save Customization");
        Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close");

        //Step30- Save dashboard
        SaveDashboard()

        //Step31- Right Click on your tracker view and select the option Properties.        
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](1)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)

        // click 'Properties' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQTrackerV1 Summary - Customize Tracker View was selected from Menu")

        //Step32- Modify any Label caption and click Ok.
        var columnLabel = getColumn(TrackerViewsGrid, "Label Caption")

        //find the row where Cust. ID is located
        for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
          //Select Label caption cell
          var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnLabel)

          if (cell["Text"] == "Cust. ID") {
            Log["Message"]("Data doesn't match with the parameter given = promptIndex > "+ columnLabel )
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnLabel)["Click"]()
            // Modify any Label caption
            TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnLabel)["EditorResolved"]["Value"] = "Customer ID"
          }
        }

        //Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

        //Step33- Save dashboard
        SaveDashboard()

        //Step34- Deploy dashboard
        DeployDashboard("Deploy Smart Client,Generate Web Form")
        
        ExitDashboard()

        Log["Checkpoint"]("Dashboard created")
      }
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Menu maintenance ----------------------------------------------------------------------------------------------------------------------'
    // Step35- Create menu
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    CreateMenu(MenuData)
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    // Step36- Restart Smart Client
    Delay(1000)
    RestartSmartClient()
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Open Menu created --- -----------------------------------------------------------------------------------------------------------------'
    // Step37- Open Menu created
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

    // Step38- Refresh Data
    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All") 
  
    // Test data from menu
    testingDashboard("tracker") 
    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- RETURN Deployed Dashboard -------------------------------------------------------------------------------------------------------------'
    
    /*Step46- Return to Dashboard designer on Executive Analysis> Business Activity Management> General Operations. Retrieve the previous dashboard*/

    //Navigate and open Dashboard
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")
    Log["Checkpoint"]("Dashboard opened") 
    
    // Retrieve the previous dashboard       
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("DashTracker")
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

    Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

    //Step47-  Add a New Query and select the zAttribute query.      
    AddQueriesDashboard("zAttribute")
    
    if(!Aliases["Epicor"]["ExceptionDialog"]['Exists']){
      //Right click on the query summary and click on the Query        
      rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // click 'New Tracker View' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
      Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")


      var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]
      // DashboardQueryProperties("General")
      DashboardPropertiesTabs(queryProperties, "General")

      //Check Auto Refresh on Load and change the Refresh Interval to 10  
      queryProperties["tabGeneral"]["chkAutoRefresh"]["Checked"] = true
      queryProperties["tabGeneral"]["numRefresh"]["Click"]()
      queryProperties["tabGeneral"]["numRefresh"]["Keys"]("[Del][Del][Del]10")
      Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

      //Save dashboard
      SaveDashboard()

      rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // Step48- click 'New Tracker View' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

      var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
      
      // Step49- Check Prompt for AttrCode field and Inputs Prompt Only       
      var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")    
      /*var allColumns = TrackerViewsGrid["DisplayLayout"]["Bands"]["Item"](0)["Columns"]

      if (allColumns["Count"] == null || allColumns["Count"] == 0) {
        Log["Message"]("Grid has no columns")
      }

      var colCurrent, columnIndex, promptIndex*/

      //Used to find prompt index 
      /*for (var i = 0; i <= allColumns["Count"] -1 ; i++) {
        colCurrent = allColumns["Item"](i)["ColumnChooserCaptionResolved"]["OleValue"]
        if ( allColumns["Item"](i)["IsVisibleInLayout"] && colCurrent == "Prompt" ) {
          promptIndex = allColumns["Item"](i)["Index"]
          break
        }
      }
       Log["Message"]("romptIndex > "+ promptIndex )*/

      //find the row where Attribut_AttrCode is located
      for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
        //Select Attribut_AttrCode row and check Prompt checkbox
        var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)

        if (cell["Text"] == "Attribut_AttrCode") {
          Log["Message"]("Data matchs with the parameter given = columnPrompt > "+ columnPrompt )
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
          // Check Prompt check box on Attribut_AttrCode field
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
        }
      }

      //Step50- Click Ok to close Properties
      Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

      SaveDashboard()
      Log["Checkpoint"]("Dashboard saved")
      
      //Step51- DEploy dashboard
      DeployDashboard("Deploy Smart Client,Generate Web Form")
      Log["Checkpoint"]("Dashboard deployed")

      ExitDashboard() 

    }else{
      Log["Error"]("Exception dialog appeared with the following text > " + Aliases["Epicor"]["ExceptionDialog"]["exceptionDialogFillPanel"]["rtbMessage"]["Text"])
    }
     
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    //Delete cache
    Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|Clear Client Cache")

    if (Aliases["Epicor"]["dlgEpicor"]["Exists"]) {
      Aliases["Epicor"]["dlgEpicor"]["btnYes"]["Click"]()
    }
    RestartSmartClient()
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //---- Create an Attribute ------------------------------------------------------------------------------------------------------------------'
   //Step53- Create an Attribute
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Attribute")

    // >Select New Attribute
    Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|&New")

    // >Enter ISO9000 on attribute field and description
    var attributePanel = Aliases["Epicor"]["AttributForm"]["windowDockingArea1"]["dockableWindow1"]["mainFillPanel1"]["windowDockingArea1"]["dockableWindow1"]["attributDetailPanel1"]

    attributePanel["grpAttribut"]["txtAttrCode"]["Keys"]("ISO9000")
    attributePanel["grpAttribut"]["txtDesc"]["Keys"]("ISO9000")
  
    // >Save
    Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

    // Exit
    Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //---- open Menu ----------------------------------------------------------------------------------------------------------------------------'
      //Step54- Return to your menu with the dashboard        
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

      // validate if zAttribute (third query)  has records after opening dash menu and the record added ISO9000 is located in the records
      testingDashboard("Attribute") 
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Close Smart Client --------------------------------------------------------------------------------------------------------------------'
    
    Delay(1000)
    
    DeactivateFullTree()
    Log["Checkpoint"]("FullTree Deactivated")

    CloseSmartClient()
    Log["Checkpoint"]("SmartClient Closed")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
}

function testingDashboard(typeTesting) {
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All")
  
  var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]
    
  var gridDashboardPanelChildren = dashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 7)["toArray"]();
  var trackerPDashboardChildren = dashboardMainPanel["FindAllChildren"]("FullName", "*TrackerPanel", 9)["toArray"]();

  if (typeTesting == "tracker") {
    // Display the GroupCode combo box from the tracker views of added queries        

      //Get Children from the first two tracker Panels of the firsy Query
      var groupCodeTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var groupCodeTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var groupCodeTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

      if (groupCodeTrackerP1["Exists"] == true && groupCodeTrackerP2["Exists"] == true && groupCodeTrackerP3["Exists"] == true) {
        Log["Checkpoint"]("Group code is displayed on the Tracker Views from first query")
      }else {
        Log["Error"]("One of the group code is not being displayed on the Tracker Views from first query")
      }
    //--------------------------------------

    //-Positionate on the first query and click on Clear button       

      var columnCustID = getColumn(gridDashboardPanelChildren[0], "Cust. ID") 
      var columnGroup = getColumn(gridDashboardPanelChildren[0], "Group")

      //Select first record on BAQTrackerV1 results to notice change of data on BAQ2
      gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnCustID)["Activate"]()

      var cell = gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnCustID)
      var rect = cell["GetUIElement"]()["Rect"]

      //Select on active cell
      gridDashboardPanelChildren[0]["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
      }

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*txtCustomer_CustID", 1);
      var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

      if (custCompanyTrackerP1["Text"] == "" && custCompanyTrackerP2["Text"] == "") {
        Log["Checkpoint"]("Data from the Tracker Views of first query was cleared correctly")
      }else {
        Log["Error"]("Data from one of the Tracker Views of first query was  not cleared correctly")
      }

    //--------------------------------------

    /*-Positionate on the second query and click on Clear button CHECK WITH CAROLINA        

      //Select first record on BAQTrackerV1 results to notice change of data on BAQ2
      gridDashboardPanelChildren[1]["Rows"]["Item"](0)["Cells"]["Item"](2)["Activate"]()

      var cell = gridDashboardPanelChildren[1]["Rows"]["Item"](0)["Cells"]["Item"](2)
      var rect = cell["GetUIElement"]()["Rect"]

      //Select on active cell
      gridDashboardPanelChildren[1]["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
      }

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

      if (custCompanyTrackerP3["Text"] == "") {
        Log["Checkpoint"]("Data from the Tracker Views of first query was cleared correctly")
      }else {
        Log["Error"]("Data from one of the Tracker Views of first query was  not cleared correctly")
      }

    //--------------------------------------*/

    //-From the first query select an option from the GroupCode drop down and click Refresh 

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

      custCompanyTrackerP1["Keys"]("Aerospace")
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      //Select first record on BAQTrackerV1 results to notice change of data on BAQ2
      gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnGroup)["Activate"]()    

      cell = gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnGroup)

      //find the row where Attribut_AttrCode is located

      for (var i = 0; i < gridDashboardPanelChildren[0]["Rows"]["Count"]; i++) {
        //Points to column Cust. ID
        var columnGroupValue = gridDashboardPanelChildren[0]["Rows"]["Item"](i)["Cells"]["Item"](columnGroup)["Text"]["OleValue"]
        
        if(custCompanyTrackerP1["SelectedRow"]["ListObject"]["Row"]["GroupCode"]["OleValue"] == cell["Text"]["OleValue"]){
          flag = true
        }else{
            Log["Message"]("Grid didn't retrieved only records with group code  " + custCompanyTrackerP1["Text"])
            flag = false
            break
        }
      }
      
      if (flag) {
        Log["Checkpoint"]("Grid retrieved just records with group code  " + custCompanyTrackerP1["Text"])
      }

      if ( custCompanyTrackerP1["Text"] != "" && custCompanyTrackerP2["Text"] != "" ) {
        Log["Checkpoint"]("Data from the Tracker Views of first query was loaded correctly")
        if ( custCompanyTrackerP1["Text"]["OleValue"] == custCompanyTrackerP2["Text"]["OleValue"] ) {
          Log["Checkpoint"]("Data from group code matches with the data of the grid")
        }else {
          Log["Error"]("Data from group code matches with the data of the grid")
        }
      }else {
        Log["Error"]("Data from one of the Tracker Views of first query was not loaded correctly")
      }

    //--------------------------------------

    // Positionate on the first query and click on Clear button   
      //Select first record on BAQTrackerV1 results to notice change of data on BAQ2
      gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnCustID)["Activate"]()

      cell = gridDashboardPanelChildren[0]["Rows"]["Item"](0)["Cells"]["Item"](columnCustID)
      rect = cell["GetUIElement"]()["Rect"]

      //Select on active cell
      gridDashboardPanelChildren[0]["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
      }

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*txtCustomer_CustID", 1);
      var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

      if (custCompanyTrackerP1["Text"] == "" && custCompanyTrackerP2["Text"] == "") {
        Log["Checkpoint"]("Data from the Tracker Views of first query was cleared correctly")
      }else {
        Log["Error"]("Data from one of the Tracker Views of first query was  not cleared correctly")
      }   
    //--------------------------------------
    
    // On the second query, on its tracker view enter A on CustID field and click Refresh 
      var custIDTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*txtCustomer_CustID", 1);
      
      trackerPDashboardChildren[2]["Parent"]["Activate"]()
      custIDTrackerP3["Keys"]("A")
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      //Select first record on BAQTrackerV1 results to notice change of data on BAQ2

      columnCustID = getColumn(gridDashboardPanelChildren[1], "Cust. ID") 
      
      for (var i = 0; i < gridDashboardPanelChildren[1]["Rows"]["Count"]; i++) {
        //Points to column Cust. ID
        var aString = gridDashboardPanelChildren[1]["Rows"]["Item"](i)["Cells"]["Item"](columnCustID)["Text"]["OleValue"]
        var aSubString = custIDTrackerP3["Text"]["OleValue"]
        var Res, flag = true;

        Res = aqString["Find"](aString, aSubString)
        if (Res != -1) {
            flag = true
        }
        else{
            Log["Message"]("There are no occurrences of '" + aSubString + "' in '" + aString + "'.");
            flag = false
            break
        }
      }
      
      if (flag) {
        Log["Checkpoint"]("Grid retrieved just records starting with A " )
      }
      
    //--------------------------------------
    
    // Change to the customized tab of your second tracker view   
      var custTrackerTestTabDashbPanelChild = dashboardMainPanel["FindAllChildren"]("FullName", "*test*", 9)["toArray"]();
      var trackerTestDashbPanelChildren = custTrackerTestTabDashbPanelChild[0]["FindAllChildren"]("FullName", "*test*", 9)["toArray"]();

      custTrackerTestTabDashbPanelChild[0]["Parent"]["Activate"]()
      
        if(Aliases["Epicor"]["ExceptionDialog"]['Exists']){
          Log["Error"]("There is an error on the tab created")
        } else{
          if(trackerTestDashbPanelChildren["length"] > 1 ){
            Log["Message"]("Controllers are displayed")
          }else{
            Log["Error"]("Controllers are not displayed")
          }
        } 

    //--------------------------------------
  }else if(typeTesting == "Attribute"){
    var columnAttr = getColumn(gridDashboardPanelChildren[2], "Attr Code") 
    var columnDescription = getColumn(gridDashboardPanelChildren[2], "Description")

    if (gridDashboardPanelChildren[2]["Rows"]["Count"] > 0) {
      Log["Checkpoint"]("Grid with title '" + gridDashboardPanelChildren[2]["WndCaption"] + "' returned " + gridDashboardPanelChildren[2]["Rows"]["Count"]  + " records")
    }else{
      Log["Error"]("Grid with title '" + gridDashboardPanelChildren[2]["WndCaption"] + "' returned 0 records")
    }
  
    flag = true
    //find the row where GroupCode is located
    for (var i = 0; i <= gridDashboardPanelChildren[2]["wRowCount"] - 1; i++) {
      //Select Customer_GroupCode row and check Prompt checkbox
      var cellAttrCode = gridDashboardPanelChildren[2]["Rows"]["Item"](i)["Cells"]["Item"](columnAttr)
      var cellDescription = gridDashboardPanelChildren[2]["Rows"]["Item"](i)["Cells"]["Item"](columnDescription)

      if (cellAttrCode["Text"]["OleValue"] == "ISO9000" && cellDescription["Text"]["OleValue"] == "ISO9000") {
        flag = true
        break
      }else{
        flag = false
      }
    }

    if(flag){
      Log["Checkpoint"]("Record was found on '" + gridDashboardPanelChildren[2]["WndCaption"] + "' grid" )
    }else{
       Log["Error"]("Record was not found on '" + gridDashboardPanelChildren[2]["WndCaption"] + "' grid" )
    }
  }

}
