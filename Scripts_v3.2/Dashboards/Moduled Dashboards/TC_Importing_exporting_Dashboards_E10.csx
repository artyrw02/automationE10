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

  // Variables
  var baqID = "baqExport"
  var dahsbID = "DashBDExport"
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

  OpenPanelTab("Update->Update Processing")

  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")

  listBOs["ClickItem"]("Erp.Customer")
  ClickButton("OK")

  AnalyzeSyntaxisBAQ()

  ClickButton("Get List")
  ClickButton("Yes")

  Log["Message"]("Get List Button Clicked")
  
  Delay(2500)
  SaveBAQ()
  ExitBAQ()
}

function CreateDashboard(){
  Log["Message"]("Step 3")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  Log["Message"]("Dashboard opened")
  
  DevMode()
  Log["Message"]("DevMode activated")

  NewDashboard(dahsbID, dahsbID, dahsbID)  
}  

function AddQueryDashb(){
  AddQueriesDashboard(baqID)

  var dashboardTree =  GetTreePanel("DashboardTree")

  ClickPopupMenu("Queries|" + baqID + ": " + baqID + "|" + baqID + ": Summary", "Properties")

  CheckboxState("chkUpdatable", "true")

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

  DashboardGridPropertiesTabs("View Rules")

  // ** Go to View rules tab. Select New View Rule. On Select Field select Customer_CustNum, on Rule Condition select GreaterThan and on Rule Value write 20. Click on the arrow pointing to the left to add the rule
  ClickButton("New View Rule")

  EnterText("cboRowRulesFields", "Customer_CustNum" + "[Tab]")
  EnterText("cboRowRuleConditions", "GreaterThan" + "[Tab]")
  EnterText("cboRowConditionValue","20" + "[Tab]")

  ClickButton("", "btnAddRowRule")

  // ** Select New Rule Action. On Select Field select Customer_CustID and on Setting Styles select Warning, click on the arrow pointing to the left to add the rule action.
  ClickButton("New Rule Action")

  EnterText("cboRuleActionFields", "Customer_CustID" + "[Tab]")
  EnterText("cboExPropsSettingStyles", "Warning" + "[Tab]")

  ClickButton("", "btnAddRuleAction")

  // Select New View Rule again. On Select Field select Customer_Country, on Rule Condition select Equals and on Rule Value write USA. Click on the arrow pointing to the left to add the rule.
  ClickButton("New View Rule")
  EnterText("cboRowRulesFields", "Customer_Country" + "[Tab]")
  EnterText("cboRowRuleConditions", "Equals" + "[Tab]")
  EnterText("cboRowConditionValue","USA" + "[Tab]")

  ClickButton("", "btnAddRowRule")

  // Select New Rule Action again. On Select Field select Customer_CustNum and on Setting Styles select Highlight, click on the arrow pointing to the left to add the rule action. Click Ok.
  ClickButton("New Rule Action")

  EnterText("cboRuleActionFields", "Customer_CustNum" + "[Tab]")
  EnterText("cboExPropsSettingStyles", "Highlight" + "[Tab]")

  ClickButton("", "btnAddRuleAction")

  ClickButton("OK")

  SaveDashboard()  
}
      
function ExportDashb(){
  Log["Message"]("Step 4")
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
   
  Log["Message"]("Step 5")  

  ClickMenu("File->Import Dashboard Definition")
  
  var dashboardImportE10 = "C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export\\DashBDExport_E10.dbd"
  
  Log["Message"]("Step 6") 
  var windowExportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Import Dashboard*","*ComboBox*"], 30)
  if (windowExportDashBD["Exists"]) {
    var windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Open*","*Button*"], 30)
  
    windowExportDashBD["Keys"](dashboardImportE10)
    windowExportDashBDSaveBtn["Click"]()

    //Rename Window dialog
    if(Aliases["Epicor"]["CopyDashboardForm"]["Exists"]){
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
    ClickButton("OK")
  }else{
    Log["Error"]("BAQ Import dialog is not displayed")
  }
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
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

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
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

  ClickMenu("Edit->Refresh")

  var gridsMainPanel = RetrieveGridsMainPanel()

   if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
    Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
   }else{
    Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
   }
  
  ClickMenu("Edit->Refresh")
}


