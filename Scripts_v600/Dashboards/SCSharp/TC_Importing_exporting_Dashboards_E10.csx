//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Importing_exporting_Dashboards_E10(){
  
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashbE10",
    "menuName" : "DashbE10",
    "orderSequence" : 1,
    "menuType" : "Dashboard-Assembly",
    "dll" : "DashBDExport",
    "validations" : "Enable"
  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])

    ActivateFullTree()

    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates BAQs --------------------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 2
      Step: > Go to Executive Analysis> Business Activity Management> Setup>Business Activity Query. Create a new BAQ, fill up the Description and check Updatable
            > Go to Query Builder Tab and drag the table Erp.Customer to the work area
            > Go to Display Fields tab. Display the fields of the previously selected table and add Company, CustID, CustNum, Name and Country to Display Column(s)
            > Go to Update Tab and in General Properties tab check on Allow Multiple Row Update. In the grid on Updatable column check  CustName to be updatable.
            > Go to Update Processing tab and select BPM Update. Select Business Object… button and select a business object Erp.Customer
            > Go to Analyze tab and click on Analyze… button to check the syntax. Check you get ""Syntax is OK""
            > Then click on Get List button to test the query and click Save.               
                             
      Result:  Verify the BAQ is created        
    */ 
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
  
      CreateBAQ("baqExport", "baqExport", "Updatable")
      
      AddTableBAQ(BAQFormDefinition, "Customer")

      AddColumnsBAQ(BAQFormDefinition, "Customer", "Company,CustID,CustNum,Name,Country")

      UpdateTabBAQ("Customer_Name", "Updatable", "Multiple Row")

       //Activate 'Update Processing' tab
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()

        // Select Erp.Customer business object
        BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["updProcess1"]["epiGroupBox1"]["updBOSettings1"]["windowDockingArea1"]["dockableWindow1"]["updBOInfo1"]["btnBusObj"]["Click"]()
        Aliases["Epicor"]["GuessBOForm"]["guessBOPanel"]["epiGroupBox1"]["lbBOs"]["ClickItem"]("Erp.Customer")
        Aliases["Epicor"]["GuessBOForm"]["btnOK"]["Click"]()

      AnalyzeSyntaxisBAQ(BAQFormDefinition)

      //Get List button
      Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow4"]["analyzePanel1"]["pnlButtons"]["grpUpd"]["btnGetList"]["Click"]()
      Log["Message"]("Get List Button Clicked")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
      }
      
      Delay(2500)
      SaveBAQ()
      Log["Message"]("BAQ Saved")

      ExitBAQ()

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 3
      Step: > Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools>Developer Mode
            > Create a new Dashboard, fill up the ID, Caption and Description fields
            > Click on New Query. Search for the BAQ that was previously created and click Ok.
            > On the three view, right click on the BAQ and select Properties. On General tab change the Grid Caption (optional) 
            > Select the Updatable checkbox
            > On Prompt column check on Name.
            > Go to View rules tab. Select New View Rule. On Select Field select Customer_CustNum, on Rule Condition select GreaterThan and on Rule Value write 20. Click on the arrow pointing to the left to add the rule
            > Select New Rule Action. On Select Field select Customer_CustID and on Setting Styles select Warning, click on the arrow pointing to the left to add the rule action.
            > Select New View Rule again. On Select Field select Customer_Country, on Rule Condition select Equals and on Rule Value write USA. Click on the arrow pointing to the left to add the rule.
            > Select New Rule Action again. On Select Field select Customer_CustNum and on Setting Styles select Highlight, click on the arrow pointing to the left to add the rule action. Click Ok.
            > Save your Dashboard  
      Result: Verify the developer mode is activated        
    */

      //Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

      //Create a new Dashboard, fill up the ID, Caption and Description fields
      NewDashboard("DashBDExport", "DashBDExport", "DashBDExport")
      
      // Click on New Query. Search for the BAQ that was previously created and click Ok.  
      AddQueriesDashboard("baqExport")
      
      var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
      dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      // dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // click 'Properties' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
      Log["Message"]("Properties was selected from Menu")

      // Select the Updatable checkbox
      Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["chkUpdatable"]["Checked"] = true

      var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
          
      //Get column index          
      var column = getColumn(dashboardGrid, "Column")
      var columnPrompt = getColumn(dashboardGrid, "Prompt")

       //find the row where Name is located
      for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
        //Select row and check Prompt checkbox
        var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

        if (cell["Text"] == "Customer_Name") {
          dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
          // Check Prompt check box on field
          dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
        }
      }

      //Go to View Rules tab
      var gridPaneldialog = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]
      
      DashboardPropertiesTabs(gridPaneldialog, "View Rules")

      var viewRulesPanel = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["tabViewRules"]["viewRuleWizardPanel1"]

      // ** Go to View rules tab. Select New View Rule. On Select Field select Customer_CustNum, on Rule Condition select GreaterThan and on Rule Value write 20. Click on the arrow pointing to the left to add the rule
        viewRulesPanel["pnlCustomRowRule"]["btnNewRowRule"]["Click"]()

        var comboSelectField = viewRulesPanel["pnlCustomRowRule"]["cboRowRulesFields"]

        comboSelectField["setFocus"]()
        comboSelectField["Keys"]("Customer_CustNum")

        var comboRuleConditions = viewRulesPanel["pnlCustomRowRule"]["cboRowRuleConditions"]

        comboRuleConditions["setFocus"]()
        comboRuleConditions["Keys"]("GreaterThan")

        var comboRuleValue = viewRulesPanel["pnlCustomRowRule"]["cboRowConditionValue"]

        comboRuleValue["setFocus"]()
        comboRuleValue["Keys"]("20"+"[Tab]")

        // Click on arrow
        viewRulesPanel["pnlCustomRowRule"]["btnAddRowRule"]["Click"]()

      // ** Select New Rule Action. On Select Field select Customer_CustID and on Setting Styles select Warning, click on the arrow pointing to the left to add the rule action.

        viewRulesPanel["pnlCustomRuleAction"]["btnNewRuleAction"]["Click"]()

        var cboRuleActionFields = viewRulesPanel["pnlCustomRuleAction"]["cboRuleActionFields"]

        cboRuleActionFields["setFocus"]()
        cboRuleActionFields["Keys"]("Customer_CustID")

        var cboExPropsSettingStyles = viewRulesPanel["pnlCustomRuleAction"]["cboExPropsSettingStyles"]

        cboExPropsSettingStyles["setFocus"]()
        cboExPropsSettingStyles["Keys"]("Warning")

        // Click on arrow
        viewRulesPanel["pnlCustomRuleAction"]["btnAddRuleAction"]["Click"]()

      // Select New View Rule again. On Select Field select Customer_Country, on Rule Condition select Equals and on Rule Value write USA. Click on the arrow pointing to the left to add the rule.
        viewRulesPanel["pnlCustomRowRule"]["btnNewRowRule"]["Click"]()

        var comboSelectField = viewRulesPanel["pnlCustomRowRule"]["cboRowRulesFields"]

        comboSelectField["setFocus"]()
        comboSelectField["Keys"]("Customer_Country")

        var comboRuleConditions = viewRulesPanel["pnlCustomRowRule"]["cboRowRuleConditions"]

        comboRuleConditions["setFocus"]()
        comboRuleConditions["Keys"]("Equals")
        
        var comboRuleValue = viewRulesPanel["pnlCustomRowRule"]["cboRowConditionValue"]

        comboRuleValue["setFocus"]()
        comboRuleValue["Keys"]("USA"+"[Tab]")

        // Click on arrow
        viewRulesPanel["pnlCustomRowRule"]["btnAddRowRule"]["Click"]()

      // Select New Rule Action again. On Select Field select Customer_CustNum and on Setting Styles select Highlight, click on the arrow pointing to the left to add the rule action. Click Ok.

        viewRulesPanel["pnlCustomRuleAction"]["btnNewRuleAction"]["Click"]()

        var cboRuleActionFields = viewRulesPanel["pnlCustomRuleAction"]["cboRuleActionFields"]

        cboRuleActionFields["setFocus"]()
        cboRuleActionFields["Keys"]("Customer_CustNum")

        var cboExPropsSettingStyles = viewRulesPanel["pnlCustomRuleAction"]["cboExPropsSettingStyles"]

        cboExPropsSettingStyles["setFocus"]()
        cboExPropsSettingStyles["Keys"]("Highlight")

        // Click on arrow
        viewRulesPanel["pnlCustomRuleAction"]["btnAddRuleAction"]["Click"]()

      Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

      SaveDashboard()

    /*
      Step No: 4
      Step:  Click on File>Export Dashboard and BAQs. Select an available location , write a name on File Name and click Save              
      Result: Verify the window to export your dashboard appears. The dashboard is saved in the selected location       
    */ 
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Export Dashboard and BAQs")

      //stores directly on C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export
      
      var windowExportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Export Dashboard*","*ComboBox*"], 30)
      if (windowExportDashBD["Exists"]) {
        var windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*","*Button*"], 30)
      
        windowExportDashBD["Keys"]("C:\\DB Demo\\epicorData\\ExportedFiles\\DashBDExport_E10600")
        windowExportDashBDSaveBtn["Click"]()
        
        var windowExportReplaceDashBD = Aliases["Epicor"]["FindChild"](["FullName", "ClassName"],["*_already_exists*","*Element*"], 30)
        if (windowExportReplaceDashBD["Exists"]) {
          windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["WndCaption", "WndClass"],["*Yes*","*Button*"], 30)
          windowExportDashBDSaveBtn["Click"]()
        }
        Log["Message"]("Dashboard exported correctly")
      }else{
        Log["Error"]("Dashboard wasn't exported correctly, Object doesn't exists")    
      }

      CloseDashboard()
      
    /*
      Step No: 5
      Step:  Open the dashboard form again. Go to File>Import Dashboard Definition. Select the dashboard that you previously exported and click Open.
      Result: Verify that after you click Open a dialog asking you to rename the dashboard definition appears.
    */       
      
      // OpenDashboard("DashBDExport")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Import Dashboard Definition")
      // var dashboardImportE10 = "C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export\\DashBDExport_E10600.dbd"
      var dashboardImportE10 = "C:\\DB Demo\\epicorData\\ExportedFiles\\DashBDExport_E10600.dbd"
    /*
      Step No: 6
      Step:  Write a new dashboard definition ID and click Ok.        
      Result: Verify that after clicking Ok a window named Import BAQ Options appear        
    */         
      var windowExportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Import Dashboard*","*ComboBox*"], 30)
      if (windowExportDashBD["Exists"]) {
        var windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Open*","*Button*"], 30)
      
        windowExportDashBD["Keys"](dashboardImportE10)
        windowExportDashBDSaveBtn["Click"]()

        //Rename Window dialog
        if(Aliases["Epicor"]["CopyDashboardForm"]["Exists"]){
          Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("DashBDExport2")
          Aliases["Epicor"]["CopyDashboardForm"]["btnOkay"]["Click"]()
        }

        Log["Message"]("Dashboard imported correctly")
      }else{
        Log["Error"]("Dashboard wasn't imported correctly, Object doesn't exists")    
      }
    
    /*
      Step No: 7
      Step:  On Import BAQ Options window uncheck Replace existing checkbox and on New BAQ ID write a new name for the BAQ and click Ok       
      Result: Verify the Dashboard is imported without errors       
    */  

      if (Aliases["Epicor"]["DashboardBAQImportDialog"]["Exists"]) {
        Log["Message"]("BAQ Import dialog is displayed")
        var importBAQGrid = Aliases["Epicor"]["DashboardBAQImportDialog"]["FindChild"](["FullName", "WndCaption"], ["*Grid*", "*Import Option*"],30)

        var currentBAQId = getColumn(importBAQGrid,"Current BAQ ID")
        var replaceExistingBAQCbx = getColumn(importBAQGrid, "Replace existing")
        var newBAQId = getColumn(importBAQGrid, "New BAQ ID")

        for (var i = 0; i < importBAQGrid["wRowCount"]; i++) {
          var cell = importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](currentBAQId)

          if (cell["Text"] == "baqExport") {
            importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](replaceExistingBAQCbx)["Click"]()
            importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](replaceExistingBAQCbx)["EditorResolved"]["CheckState"] = "Unchecked"

            importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](newBAQId)["Click"]()
            importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](newBAQId)["EditorResolved"]["SelectedText"] = "baqExport2"
          }
        }
        Aliases["Epicor"]["DashboardBAQImportDialog"]["btnOkay"]["Click"]()
      }else{
        Log["Error"]("BAQ Import dialog is not displayed")
      }

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Text"]["OleValue"] == "DashBDExport2"){
        Log["Message"]("Dashboard Imported")
      }else{
        Log["Error"]("Dashboard wasn't imported")
      }

    /*
      Step No: 8
      Step:  Click Save       
      Result: Verify the dashboard is saved. Verify the Caption and Description fields have the values you previously gave        
    */ 
      SaveDashboard()

    /*
      Step No: 9
      Step:  Click on Tools>Deploy Dashboard. Select Deploy Smart Client Application checkbox       
      Result: Verify the dashboard is deployed without errors       
    */ 

      DeployDashboard("Deploy Smart Client")

      ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Creates Menu --------------------------------------------------------------------------------------------------------------------------'
   /*
      Step No: 10
      Step:  Go to System Setup>Security Maintenance> Menu Maintenance. In Menu Maintenance tree select Main Menu> Sales Management> CRM> Setup
             Select New Menu.
             Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), in Program Type select Dashboard-Assembly and 
             in Dashboard select the previously created one. Be sure the Enabled check box is selected. Click Save." 
      Result: Verify the dashboard is saved. Verify the Caption and Description fields have the values you previously gave        
    */ 

    //Open Menu maintenance   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    //Creates Menu
    CreateMenu(MenuData)
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 11
      Step: Restart Smart Client        
      Result: E10 is restarted        
    */    

    Delay(1000)
    RestartSmartClient()
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Test Menu -----------------------------------------------------------------------------------------------------------------------------'
   /*
      Step No: 12
      Step:  Click refresh        
      Result: Verify the data is retrieved on the grid, respecting the given rules       
    */ 

    //Open Menu created   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")


    var gridsMainPanel = RetrieveGridsMainPanel()

     if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
      Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
     }else{
      Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
     }
    
    Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")  

  //-------------------------------------------------------------------------------------------------------------------------------------------'

   DeactivateFullTree()

   CloseSmartClient()

}


