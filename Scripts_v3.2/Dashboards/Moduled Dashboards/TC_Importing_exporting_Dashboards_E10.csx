//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions

function TC_Importing_exporting_Dashboards_E10(){}
  
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashbE10",
    "menuName" : "DashbE10",
    "orderSequence" : 1,
    "menuType" : "Dashboard-Assembly",
    "dll" : "DashBDExport",
    "validations" : "Enable"
  }

  var baqID = "baqExport"
    // Variables
  var company1 = "Epicor Education"
  var plant1 = "Main"

  //Used to navigate thru the Main tree panel
  var treeMainPanel1 = setCompanyMainTree(company1,plant1)

function CreateBAQ1(){
    Log["Message"]("Step 2")
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    CreateBAQ(baqID, baqID, "chkUpdatable")
    
    AddTableBAQ("Erp.Customer", "Customer")

    AddColumnsBAQ("Customer", "Company,CustID,CustNum,Name,Country")

    UpdateTabBAQ("Customer_Name", "Updatable", "chkSupportMDR")

   //Activate 'Update Processing' tab
    // BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    OpenPanelTab("Update->Update Processing")

    ClickButton("Business Object...")

    var listBOs = GetList("lbBOs")

    listBOs["ClickItem"]("Erp.Customer")
    ClickButton("OK")

    // Select Erp.Customer business object
    // BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["updProcess1"]["epiGroupBox1"]["updBOSettings1"]["windowDockingArea1"]["dockableWindow1"]["updBOInfo1"]["btnBusObj"]["Click"]()
    // Aliases["Epicor"]["GuessBOForm"]["guessBOPanel"]["epiGroupBox1"]["lbBOs"]["ClickItem"]("Erp.Customer")
    // Aliases["Epicor"]["GuessBOForm"]["btnOK"]["Click"]()

    AnalyzeSyntaxisBAQ(BAQFormDefinition)

    //Get List button
    // Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow4"]["analyzePanel1"]["pnlButtons"]["grpUpd"]["btnGetList"]["Click"]()
    ClickButton("Get List")
    ClickButton("Yes")

    Log["Message"]("Get List Button Clicked")
    
    Delay(2500)
    SaveBAQ()
    ExitBAQ()
}

function CreateDashboard(){
  Log["Message"]("Step 3")
  //Navigate and open Dashboard
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
  Log["Message"]("Dashboard opened")
  
  //Enable Dashboard Developer Mode  
  DevMode()
  Log["Message"]("DevMode activated")

  //Create a new Dashboard, fill up the ID, Caption and Description fields
  NewDashboard("DashBDExport", "DashBDExport", "DashBDExport")  
}  

function AddQueryDashb(){
  AddQueriesDashboard(baqID)
      var dashboardTree =  GetTreePanel("DashboardTree")

      // var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
      // dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      // click 'Properties' option from menu
      // Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
      // Log["Message"]("Properties was selected from Menu")

      dashboardTree["ClickItem"]("Queries|" + baqID + ": " + baqID + "|"+ baqID + ": Summary")
      ClickMenu("Edit->Properties")
      // dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // Select the Updatable checkbox
      // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["chkUpdatable"]["Checked"] = true
      CheckboxState("chkUpdatable", "true")

      // var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
      var dashboardGrid = GetGrid("ultraGrid1")

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
          Log["Message"](cell["Text"] + " Prompt Checked")
        }
      }

      //Go to View Rules tab
      // var gridPaneldialog = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]
      
      DashboardGridPropertiesTabs("View Rules")

      // var viewRulesPanel = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["tabViewRules"]["viewRuleWizardPanel1"]

      // ** Go to View rules tab. Select New View Rule. On Select Field select Customer_CustNum, on Rule Condition select GreaterThan and on Rule Value write 20. Click on the arrow pointing to the left to add the rule
        // viewRulesPanel["pnlCustomRowRule"]["btnNewRowRule"]["Click"]()
        ClickButton("New View Rule")

        // var comboSelectField = viewRulesPanel["pnlCustomRowRule"]["cboRowRulesFields"]
        // comboSelectField["setFocus"]()
        // comboSelectField["Keys"]("Customer_CustNum")

        EnterText("cboRowRulesFields", "Customer_CustNum" + "[Tab]")

        // var comboRuleConditions = viewRulesPanel["pnlCustomRowRule"]["cboRowRuleConditions"]

        // comboRuleConditions["setFocus"]()
        // comboRuleConditions["Keys"]("GreaterThan")
        EnterText("cboRowRuleConditions", "GreaterThan" + "[Tab]")

        // var comboRuleValue = viewRulesPanel["pnlCustomRowRule"]["cboRowConditionValue"]

        // comboRuleValue["setFocus"]()
        // comboRuleValue["Keys"]("20"+"[Tab]")
        EnterText("cboRowConditionValue","20" + "[Tab]")

        // Click on arrow
        // viewRulesPanel["pnlCustomRowRule"]["btnAddRowRule"]["Click"]()
        ClickButton("", "btnAddRowRule")

      // ** Select New Rule Action. On Select Field select Customer_CustID and on Setting Styles select Warning, click on the arrow pointing to the left to add the rule action.

        // viewRulesPanel["pnlCustomRuleAction"]["btnNewRuleAction"]["Click"]()
        ClickButton("New Rule Action")

        // var cboRuleActionFields = viewRulesPanel["pnlCustomRuleAction"]["cboRuleActionFields"]

        // cboRuleActionFields["setFocus"]()
        // cboRuleActionFields["Keys"]("Customer_CustID")

        EnterText("cboRuleActionFields", "Customer_CustID" + "[Tab]")

        // var cboExPropsSettingStyles = viewRulesPanel["pnlCustomRuleAction"]["cboExPropsSettingStyles"]

        // cboExPropsSettingStyles["setFocus"]()
        // cboExPropsSettingStyles["Keys"]("Warning")
        EnterText("cboExPropsSettingStyles", "Warning" + "[Tab]")

        // Click on arrow
        // viewRulesPanel["pnlCustomRuleAction"]["btnAddRuleAction"]["Click"]()
        ClickButton("", "btnAddRuleAction")

      // Select New View Rule again. On Select Field select Customer_Country, on Rule Condition select Equals and on Rule Value write USA. Click on the arrow pointing to the left to add the rule.
        // viewRulesPanel["pnlCustomRowRule"]["btnNewRowRule"]["Click"]()
        ClickButton("New View Rule")

        // var comboSelectField = viewRulesPanel["pnlCustomRowRule"]["cboRowRulesFields"]

        // comboSelectField["setFocus"]()
        // comboSelectField["Keys"]("Customer_Country")
        EnterText("cboRowRulesFields", "Customer_Country" + "[Tab]")

        // var comboRuleConditions = viewRulesPanel["pnlCustomRowRule"]["cboRowRuleConditions"]

        // comboRuleConditions["setFocus"]()
        // comboRuleConditions["Keys"]("Equals")
        EnterText("cboRowRuleConditions", "Equals" + "[Tab]")
        
        // var comboRuleValue = viewRulesPanel["pnlCustomRowRule"]["cboRowConditionValue"]

        // comboRuleValue["setFocus"]()
        // comboRuleValue["Keys"]("USA"+"[Tab]")
        EnterText("cboRowConditionValue","USA" + "[Tab]")

        // Click on arrow
        // viewRulesPanel["pnlCustomRowRule"]["btnAddRowRule"]["Click"]()
        ClickButton("", "btnAddRowRule")

      // Select New Rule Action again. On Select Field select Customer_CustNum and on Setting Styles select Highlight, click on the arrow pointing to the left to add the rule action. Click Ok.

        // viewRulesPanel["pnlCustomRuleAction"]["btnNewRuleAction"]["Click"]()
        ClickButton("New Rule Action")

        // var cboRuleActionFields = viewRulesPanel["pnlCustomRuleAction"]["cboRuleActionFields"]

        // cboRuleActionFields["setFocus"]()
        // cboRuleActionFields["Keys"]("Customer_CustNum")
        EnterText("cboRuleActionFields", "Customer_CustNum" + "[Tab]")

        // var cboExPropsSettingStyles = viewRulesPanel["pnlCustomRuleAction"]["cboExPropsSettingStyles"]

        // cboExPropsSettingStyles["setFocus"]()
        // cboExPropsSettingStyles["Keys"]("Highlight")
        EnterText("cboExPropsSettingStyles", "Highlight" + "[Tab]")

        // Click on arrow
        // viewRulesPanel["pnlCustomRuleAction"]["btnAddRuleAction"]["Click"]()
        ClickButton("", "btnAddRuleAction")

      // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
      ClickButton("OK")

      SaveDashboard()  
}
      
function ExportDashb(){
    Log["Message"]("Step 4")
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Export Dashboard and BAQs")
    ClickMenu("File->Export Dashboard and BAQs")

    //stores directly on C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export
    
    var windowExportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Export Dashboard*","*ComboBox*"], 30)
    if (windowExportDashBD["Exists"]) {
      var windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*","*Button*"], 30)
    
      windowExportDashBD["Keys"]("DashBDExport_E10")
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
}

function ImportDashb(){
    /*
      Step No: 5
      Step:  Open the dashboard form again. Go to File>Import Dashboard Definition. Select the dashboard that you previously exported and click Open.
      Result: Verify that after you click Open a dialog asking you to rename the dashboard definition appears.
    */       
      Log["Message"]("Step 5")  
      // OpenDashboard("DashBDExport")

      // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Import Dashboard Definition")
      ClickMenu("File->Import Dashboard Definition")
      
      var dashboardImportE10 = "C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export\\DashBDExport_E10.dbd"
    /*
      Step No: 6
      Step:  Write a new dashboard definition ID and click Ok.        
      Result: Verify that after clicking Ok a window named Import BAQ Options appear        
    */        
      Log["Message"]("Step 6") 
      var windowExportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Import Dashboard*","*ComboBox*"], 30)
      if (windowExportDashBD["Exists"]) {
        var windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Open*","*Button*"], 30)
      
        windowExportDashBD["Keys"](dashboardImportE10)
        windowExportDashBDSaveBtn["Click"]()

        //Rename Window dialog
        if(Aliases["Epicor"]["CopyDashboardForm"]["Exists"]){
          // Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("DashBDExport2")
          // Aliases["Epicor"]["CopyDashboardForm"]["btnOkay"]["Click"]()
          EnterText("txtDefinitionId", "DashBDExport2")
          ClickButton("OK")
        }

        Log["Message"]("Dashboard id added correctly")
      }else{
        Log["Error"]("Dashboard id wasn't added correctly, Object doesn't exists")    
      }

}

function ImportBAQs(){
 Delay(2500)
  Log["Message"]("Step 7")
  if (Aliases["Epicor"]["DashboardBAQImportDialog"]["Exists"]) {
    Log["Message"]("BAQ Import dialog is displayed")
    var importBAQGrid = Aliases["Epicor"]["DashboardBAQImportDialog"]["FindChild"](["FullName", "WndCaption"], ["*Grid*", "*Import Option*"],30)

    var currentBAQId = getColumn(importBAQGrid,"Current BAQ ID")
    var replaceExistingBAQCbx = getColumn(importBAQGrid, "Replace existing")
    var newBAQId = getColumn(importBAQGrid, "New BAQ ID")

    for (var i = 0; i < importBAQGrid["wRowCount"]; i++) {
      var cell = importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](currentBAQId)

      if (cell["Text"] == baqID) {
        importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](replaceExistingBAQCbx)["Click"]()
        importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](replaceExistingBAQCbx)["EditorResolved"]["CheckState"] = "Unchecked"

        importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](newBAQId)["Click"]()
        importBAQGrid["Rows"]["item"](i)["Cells"]["Item"](newBAQId)["EditorResolved"]["SelectedText"] = "baqExport2"
      }
    }
    // Aliases["Epicor"]["DashboardBAQImportDialog"]["btnOkay"]["Click"]()
    ClickButton("OK")
  }else{
    Log["Error"]("BAQ Import dialog is not displayed")
  }

  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
  //OpenPanelTab("General")

  /*var defIDDashb = GetText("txtDefinitonID")
  if(defIDDashb == "DashBDExport2"){
    Log["Message"]("Dashboard Imported")
  }else{
    Log["Error"]("Dashboard wasn't imported")
  }*/

} 

function DeployDashb(){
  Delay(5000)
  
  Log["Message"]("Step 8")
  SaveDashboard()

  Log["Message"]("Step 9")
  DeployDashboard("Deploy Smart Client")

  ExitDashboard()
} 

function CreateMenu1(){
  Log["Message"]("Step 10")
  //Open Menu maintenance   
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu(MenuData)  
}

function RestartE10(){
  Log["Message"]("Step 11")
  Delay(1000)
  RestartSmartClient()
  Log["Message"]("SmartClient Restarted")
}

function TestMenu(){
  Log["Message"]("Step 12")
  //Open Menu created   
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  var gridsMainPanel = RetrieveGridsMainPanel()

   if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
    Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
   }else{
    Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
   }
  
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")  
  ClickMenu("Edit->Refresh")
}


