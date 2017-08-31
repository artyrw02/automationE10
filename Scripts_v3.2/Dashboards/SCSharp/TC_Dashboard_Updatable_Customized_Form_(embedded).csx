//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Dashboard_Updatable_Customized_Form(){
  
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashCust",
    "menuName" : "DashCustm",
    "orderSequence" : 5,
    "menuType" : "Dashboard-Assembly",
    "dll" : "TestDashBDCustom"
  }  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])

    ActivateFullTree()

    Delay(1500)
    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates BAQs --------------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 2
      step: Create an updatable BAQ
      Result: Verify the BAQ is created       
    */ 

    // Move to EPIC06 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      // Enter the following in the "General" tab
      var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
      
      // QueryID: TestBAQ
      // Description: TestBAQ
      // Shared: Checked | Updatable: Checked
      CreateBAQ("TestBAQ", "TestBAQ", "Shared,Updatable")
      // drag and drop the "Customer" table design area in "Phrase Build" tab
      AddTableBAQ(BAQFormDefinition, "Customer")
      // In the Display Fields> Column Select tab for the "Customer" table select the Company, CustID, CustNum, Name and Address1 columns and add them to "Display Columns" area
      AddColumnsBAQ(BAQFormDefinition, "Customer", "Company,CustID,CustNum,Name,Address1")

      // Move to Update> General Properties tab and check "Customer_Name" and "Customer_Address1" column as updatable
      UpdateTabBAQ("Customer_Name", "Updatable")
      UpdateTabBAQ("Customer_Address1", "Updatable")

      // Move to Update processing, click BPM Update, then click the "Business Object..." button and select the "ERP.Customer" then click "Ok" button from the "Select Business Object" window
      
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()

        // Select Erp.Customer business object
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["updProcess1"]["epiGroupBox1"]["updBOSettings1"]["windowDockingArea1"]["dockableWindow1"]["updBOInfo1"]["btnBusObj"]["Click"]()
        Aliases["Epicor"]["GuessBOForm"]["guessBOPanel"]["epiGroupBox1"]["lbBOs"]["ClickItem"]("Erp.Customer")
        Aliases["Epicor"]["GuessBOForm"]["btnOK"]["Click"]()

      // Save the  BAQ
      SaveBAQ()
      ExitBAQ()
      Log["Checkpoint"]("BAQ TestBAQ created")

    /*
      Step No: 3
      step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result:  Verify the developer mode is activated       
    */ 

      //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Checkpoint"]("Dashboard opened")
        
        //Enable Dashboard Developer Mode  
        DevMode()
        Log["Checkpoint"]("DevMode activated")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Creates DashBoard ---------------------------------------------------------------------------------------------------------------------'
      /*
        Step No: 4
        Descr: 
          // Definition ID: TestDashBDCustom
          // Caption: TestDashBDCustom
          // Description: TestDashBDCustom
        Result: Verify the Dashboard is created       
      */ 

        NewDashboard("TestDashBDCustom","TestDashBDCustom","TestDashBDCustom")
         
      /*
        Step No: 5
        Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
      */ 
          AddQueriesDashboard("TestBAQ")
          
          SaveDashboard()
          Log["Checkpoint"]("Dashboard with TestBAQ created")

      /*
        Step No: 6
        Result:  Right click on the BAQ and select Properties - Properties dialog open     
      */ 
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("BAQ - right click")

        // click 'Properties' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ1 Summary - Properties was selected from Menu")

        if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
          Log["Message"]("Dashboard properties dialog appears")
        }

      /*
        Step No: 7
        Result:  Open dialog and filter data
      */ 
        var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]

        // Go to Filter tab and add the condition: Customer_CustNum = Dashboard Browse        
        DashboardPropertiesTabs(queryProperties, "Filter")

        var ultraGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")

        if(ultraGrid["Exists"]){
          SelectCellDropdownGrid("ColumnName", "Customer_CustNum", ultraGrid)
          SelectCellDropdownGrid("Condition", "=", ultraGrid)

          var columnValue = getColumn(ultraGrid, "Value")

          ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["Click"]()
          ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["EditorResolved"]["SelectedText"]  = "Dashboard Browse"

          Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
          Log["Message"]("Conditions added on BAQ")
        }

        if(Aliases["Epicor"]["QueryNavProperties"]["Exists"]){
          Log["Message"]("Dashboard Browse Properties dialog appear")
        }else{
          Log["Error"]("Dashboard Browse Properties dialog didn't appear")
        }

      /*
        Step No: 8
        Result:  Open dialog and filter data
      */       
        //On Dashboard Browse Properties on Display column dropdown select CustNum       
        Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["SetFocus"]()
        while(Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["Text"] != "CustNum"){
          Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["Keys"]("[Down]")
        }
        Log["Message"]("CustNum Added on Display column dropdown")

        var dropdownColumns = Aliases["Epicor"]["QueryNavProperties"]["grpMediaProps"]["lstAvailable"]

        //add CustNum on Drop Down Columns and check Primary Browse option. Click Ok to both dialogs
        for(var i = 0; i < dropdownColumns["Items"]["Count"]; i++){
          if (dropdownColumns["Items"]["Item_2"](i)["Text"]["OleValue"] == "CustNum") {
            dropdownColumns["ClickItem"]("CustNum")
            Aliases["Epicor"]["QueryNavProperties"]["grpMediaProps"]["WinFormsObject"]("btnSelect")["Click"]()
            Aliases["Epicor"]["QueryNavProperties"]["chkPrimaryBrowse"]["Checked"] = true
          }
        }

        //Close Dashboard Browse Properties 
        Aliases["Epicor"]["QueryNavProperties"]["WinFormsObject"]("btnOkay")["Click"]()
        Log["Message"]("Dashboard Browse Properties closed")   

        //Close Dashboard Query Properties 
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("Dashboard Query Properties closed")

      /*
        Step No: 9
        Result:  Right click on the BAQ grid and select Properties - Properties dialog open     
      */ 
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("BAQ grid - right click")

        // click 'Properties' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ1 Summary - Properties was selected from Menu")

        if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
          Log["Message"]("Dashboard properties dialog appears")
        }

      /*
        Step No: 10
        Step: Check the Updatable check box
        Result: Verify the changes are applied and the dialog is closed       
      */    
        //Check the Updatable check box
        Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["chkUpdatable"]["Checked"] = true

      /*
        Step No: 11
        Step: Check the Updatable check box
        Result: Verify the changes are applied and the dialog is closed       
      */    
        // Check the ""prompt"" option for ""Name"" and ""Address"" field 
        var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
        
        var column = getColumn(dashboardGrid, "Column")
        var columnPrompt = getColumn(dashboardGrid, "Prompt")
        
        for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
          //Select row and check Prompt checkbox
          var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

          if (cell["Text"] == "Customer_Name") {
            dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            // Check Prompt check box on field
            dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          }
          if (cell["Text"] == "Customer_Address1") {
            dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            // Check Prompt check box on field
            dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          }
        }

        //Click Ok to close Properties
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
      
      /*
        Step No: 12
        Step: Save the dashboard , then Click on Tools>Deploy Dashboard. 
        Result: Verify the changes are applied and the dialog is closed       
      */    

        // Save the dashboard
        SaveDashboard()

        // Tools>Deploy Dashboard. Select Deploy Smart Client Application  check box. Click on Deploy button and when finished click Ok.        
        DeployDashboard("Deploy Smart Client,Generate Web Form")

        ExitDashboard()
  //-------------------------------------------------------------------------------------------------------------------------------------------

  //---  Menu Maintenance ---------------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 13
      Descr: Go to System Setup>Security Maintenance> Menu Maintenance.
      Result: Verify the Dashboard is created       
    */ 
    // In Menu Maintenance tree select Main Menu>Sales Management>Customer Relationship Management > Setup, 
    // then Select File> New>New Menu
    // Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
    // in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. 
    // Be sure the Enabled check box is selected. Click Save.


    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    CreateMenu(MenuData)
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 14
      Step: Restart E10       
      Result: E10 is restarted        
    */    

    Delay(1000)
    RestartSmartClient()
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //---- Test Dashboard -----------------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 15
      Step: Refresh
      Result: The dashboard opens without errors        
    */   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

    var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]
  
    var grid = dashboardMainPanel["FindChild"]("FullName", "*grid*", 15);

    if (grid["wRowCount"] > 0) {
      Log["Checkpoint"]("Data populated")
    }else{
      Log["Error"]("Data wasn't populated")
    }

    var nameColumn = getColumn(grid, "Name")
    var addressColumn = getColumn(grid, "Address")

    //Activates row 0
    grid["Rows"]["Item"](0)["Cells"]["Item"](nameColumn)["Click"]()

    oldValue1 = grid["Rows"]["Item"](0)["Cells"]["Item"](nameColumn)["Text"]["OleValue"]
    newValue1 = grid["Rows"]["Item"](0)["Cells"]["Item"](addressColumn)["EditorResolved"]["SelectedText"] = "Test"


    //Activates row 1
    grid["Rows"]["Item"](1)["Cells"]["Item"](nameColumn)["Click"]()

    if(oldValue1 != newValue1){
      Log["Checkpoint"]("Data was modified.")
    }else{
      Log["Error"]("Data was not modified.")
    }

    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Save")
    Log["Checkpoint"]("Data was saved.")
    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  //-------------------------------------------------------------------------------------------------------------------------------------------'


  //---- Add the dashboard in a Customization --------------------------------------------------------------------------------------------------'
    /*
      Step No: 17
      Step: Refresh
      Result: The dashboard opens without errors        
    */ 
    //Validate if it's on dev mode already

    //Developer mode 
    Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")

    // Open Sales Management > Customer Relationship Management > Setup > Customer(select Base only option and click OK in the ""Select Customization"" dialog)
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Customer")

    Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["grpNoLayer"]["chkBaseOnly"]["Checked"] = true

    Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    // click Tools > Customization
    Aliases["Epicor"]["CustomerEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Customization")

    // go to Wizards > Sheet Wizard  tab
    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    // click ""New Custom Sheet""
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()

    // Select Dockable Sheets (choose Parent Docking Sheet) as customerDock1
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"]("customerDock1")

    Log["Message"]("customerDock1 sheet selected")

    // Name, text, tab text = ""TEST""
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("test")
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("test")
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("test") 

    // Click Add Dashboard Button
    Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddDashboard"]["Click"]()
    
    // Choose AppBuilder Panel option and type or search and retrieve for the the Menu ID created above
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["radAppBuilderPanel"]["ultraOptionSet1"]["Click"]()
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["txtDashboardID"]["Keys"]("DashCust")

    // Tick only ""display dashboard status bar"" option
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["chkDisplayStatusBar"]["Checked"] = true

    // click ""Next"" button
    Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()

    // Select Subscribe to UI data (include retrieve button)
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep2"]["radRetrieveWButton"]["ultraOptionSet1"]["Click"]()

    // click ""Next"" button
    Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()

    // Choose ""Customer"" data view
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataViews"]["ClickItem"]("Customer")
    // Choose ""CustNum"" column
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataColumns"]["ClickItem"]("CustNum")
    // click ""Add Subscribe column"" button
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["btnAddSubscribeColumn"]["Click"]()
    // click ""Finish"" button
    Aliases["Epicor"]["CustomWizardDialog"]["WinFormsObject"]("btnFinish")["Click"]()
    // Press Right arrow to move tab to ""Custom Sheets"" panel
    Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
    Log["Checkpoint"]("Sheet was added to Custom Sheets.")
    // Click File> Save customization as  and give it a name and Description and click ""Save"" button (Add a comment if you wish then click last ""OK"" button)
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtKey1a")["Keys"]("Automation test")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtDescription")["Keys"]("Automation test")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("btnOk")["Click"]()
    Aliases["Epicor"]["WinFormsObject"]("CustomCommentDialog")["WinFormsObject"]("btnOK")["Click"]()

    Log["Checkpoint"]("Customization saved.")
    // close the customization 
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
    // Close the form"  
    Aliases["Epicor"]["CustomerEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")   
    Log["Checkpoint"]("Customization closed.")   

    /*
      Step No: 18
      Step: Test embedded dasboard
      Result: The dashboard opens without errors        
    */ 

    // Go to Sales Management> Customer Relationship Management> Setup> Customer and select the customization from previous step
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Customer")
    /*(FUTURE REFERENCE FOR TREE LIST ITEMS)*/
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["etvAvailableLayers"]["ClickItem"]("Base|EP|Customizations|Automation_test")
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    // In the Customer> Detail tab retrieve ""Addison""
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Addison")
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

    Log["Checkpoint"]("Addison customer was retrived")
    // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();
    
    /*
      Step No: 19
      Step: in the "TEST" tab click "Retrieve" button
      Result: The dashboard opens without errors        
    */ 

    var retrieveBtn = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*Retrieve*", "*EpiButton*"], 30)
    retrieveBtn["Click"]()

    var grid = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*TestBAQ: Summary*", "*Grid*"], 30)
    if(grid["wRowCount"] > 0){
      Log["Checkpoint"]("Dashboard retrived data")
    }else {
      Log["Error"]("Dashboard didn't retrive data")
    }

    /*
      Step No: 20
      Step: Go back to Customer> Detail  tab and retrieve ""Dalton"" customer
      Result: The customer is retrieved without errors        
    */ 
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Dalton")
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")
       
      Log["Checkpoint"]("Dalton customer was retrived")
      // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

  // deactivate dev mode
   Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")
   
   DeactivateFullTree()

   CloseSmartClient()

}


