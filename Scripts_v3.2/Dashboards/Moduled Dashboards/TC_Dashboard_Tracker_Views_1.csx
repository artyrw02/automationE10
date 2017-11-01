//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT Data_Dashboard_TrackerViews_1

function TC_Dashboard_Tracker_Views_1(){}

  
function CreateBAQ1(){
  ExpandComp(company1)

  ChangePlant(plant1)

  Log["Message"]("Step 2")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  Log["Message"]("BAQ opened")

  CopyBAQ(baq, baq1Copy)
  Log["Message"]("BAQ '" + baq + "' copied to '" + baq1Copy + "'")
  
  AddColumnsBAQ("Customer", "GroupCode")

  AnalyzeSyntaxisBAQ()
  TestResultsBAQ()
  SaveBAQ()
  ExitBAQ()
  Log["Message"](baq1Copy + " created")
}

function CreateDashboard(){
  //Step3- Navigate and open Dashboard
  Log["Message"]("Step 3")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  Log["Message"]("Dashboard opened")
  //Enable Dashboard Developer Mode  
  DevMode()
  Log["Message"]("DevMode activated")

  //Step4- Creating dashboard
  Log["Message"]("Step 4")
  NewDashboard(dashb1, dashb1, dashb1, "chkInhibitRefreshAll")
}

function AddQuery1Dashb(){
  //Step5- Add query
  Log["Message"]("Step 5")
  
  Delay(2000)
  AddQueriesDashboard(baq1Copy)
}  


function AddTrackerView1Query1(){
  //Step6- Add a New Tracker View       
  Log["Message"]("Step 6")

  var dashboardTree = GetTreePanel("DashboardTree")
  //Right click on the query summary and click on the Query        
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

  // Step7- Select Clear All button  
  Log["Message"]("Step 7")      
  if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
    // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnClearAll"]["Click"]()
    ClickButton("Clear All")
  }

  //Step8- Click Ok to close Properties
  Log["Message"]("Step 8")
  Delay(2000)
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
  ClickButton("OK")

  //Save dashboard
  SaveDashboard()

  E10["Refresh"]()
  
  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)

  //Step9- click 'Properties' option from menu
  Log["Message"]("Step 9")
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
  Log["Message"]("BAQTrackerV1 Summary - Properties was selected from Menu")

  //Step10 - Select Select All   
  Log["Message"]("Step 10")    
  if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
    // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnSelectAll"]["Click"]()
    ClickButton("Select All")
  }
  
  //Step11 - Check Prompt check box on GroupCode field        
  Log["Message"]("Step 11")
  // var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
  var TrackerViewsGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")

  //find the row where GroupCode is located
  for (var i = 0; i <= TrackerViewsGrid["Rows"]["Count"] - 1; i++) {
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
  Log["Message"]("Step 12")
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
  ClickButton("OK")

  //Save dashboard
  SaveDashboard()

  //Step13- Click Refresh  
  Log["Message"]("Step 13")     
  Delay(2000)
  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")
}

function AddTrackerView2Query1(){
  //Step15- Right click on the query summary and click on the Query to add a New Tracker View    
  Delay(2500)
  
  E10["Refresh"]()

  Log["Message"]("Step 15, 16")   
  
  var dashboardTree = GetTreePanel("DashboardTree")
  Delay(2500)
  //Right click on the query summary and click on the Query        
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  Log["Message"]("New Tracker View was selected from Menu")

  var TrackerViewsGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")

  //Step16- Check Prompt check box on GroupCode field       
  for (var i = 0; i <= TrackerViewsGrid["Rows"]["Count"] - 1; i++) {
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
  Log["Message"]("Step 17")
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
  ClickButton("OK")

  //Save dashboard
  SaveDashboard()

  //Step18- Click Refresh       
  // Log["Message"]("Step 18")
  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  // ClickMenu("Edit->Refresh")
}
    
function AddQuery2Dashb(){
  //Step20- Add a New Query and select the same Query you previously created - second query
  Log["Message"]("Step 20")
  Delay(2000)
  AddQueriesDashboard(baq1Copy)
}

function AddTrackerView1Query2(){

  //Right click on the query summary and click on the Query  
  var dashboardTree = GetTreePanel("DashboardTree")      
  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)
  Log["Message"]("BAQ - right click")

  //Step21- click 'New Tracker View' option from menu
  Log["Message"]("Step 21")
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

  //Step22- Check Prompt check box for CustID field and on Condition select StartsWith    
  Log["Message"]("Step 22")   
  // var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"] 
  var TrackerViewsGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")
  var columnCondition = getColumn(TrackerViewsGrid, "Condition")


  //find the row where CustID is located
  for (var i = 0; i <= TrackerViewsGrid["Rows"]["Count"] - 1; i++) {
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
  Log["Message"]("Step 23")
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
  ClickButton("OK")

  //Save dashboard
  SaveDashboard()

  //Step24- Click Refresh    
  Log["Message"]("Step 24")   
  // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All")
  ClickMenu("Refresh All")
  
  // Maximize Dashboard
  var form = GetForm(dashb1)
  form["Maximize"]()
}

function CustomizeTrackerView(){
  //Step25- Right Click on your tracker view and select the option Customize Tracker View.        
  Log["Message"]("Step 25")
  Delay(2000)
  var dashboardTree = GetTreePanel("DashboardTree")

  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)["Nodes"]["Item"](1)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)

  // click 'Customize Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Customize Tracker View");
  Log["Message"]("BAQTrackerV1 Summary - Customize Tracker View was selected from Menu")

  Delay(2500)
  //Step26- Go to Wizards> Sheet Wizard tab and click on button New Custom Sheet, and select the available parent docking sheet.   
  Log["Message"]("Step 26")     
  var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
  //Wizards
  CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
  //Sheet Wizard
  CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true
  
  if (CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["Exists"]) {
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()
    ClickButton("New Custom Sheet")

    // select the available parent docking sheet.
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"](0)
    var dockableSheetsList = GetList("lstStandardSheets")
    dockableSheetsList["ClickItem"](0)

    //Step27- Add a Name, Text and Tab Text for the new sheet.
    Log["Message"]("Step 27")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("test")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("test")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("test")
    var customData = "testDashb"
    EnterText("txtSheetName", customData)
    EnterText("txtSheetText", customData)
    EnterText("txtSheetTextTab", customData)

    // Click on the arrow pointing to the right       
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
    ClickButton("", "btnAddCustomSheet")
    
    // Select the newly added tab on your tracker
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstCustomSheets"]["ClickItem"](0)
    var custSheetsList = GetList("lstCustomSheets")
    custSheetsList["ClickItem"](0)
    
    // Aliases["Epicor"]["Dashboard"]["Activate"]()
    ActivateForm(dashb1)

    // var dashboardPanel = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow3"]["dbFillPanel1"]

    // custTrackerTestTabDashbPanelChild[0]["Parent"]["Activate"]()
    OpenPanelTab(customData, dashb1)

    var wnd = GetForm(dashb1)
    
    // Aliases["Epicor"]["CustomToolsDialog"]["Activate"]()
    ActivateForm("Customization Tool")
    
    //Step28- Select Tools>Tool box from the customization tools dialog
    Log["Message"]("Step 28")
    // Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|ToolBox")
    ClickMenu("Tools->ToolBox")

    Delay(2000)

    //Step29- On your new tab drop a label, a text box, add a combo box and a date time editor. Then Save and close the customization window  
    var lvwItems = GetList("lvwItems")      
    // Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiLabel", -1, 50, 10);
    lvwItems["ClickItemXY"]("EpiLabel", -1, 50, 10);
    epiBasePanel = FindObject("*BasePanel*", "Name", "*" + customData + "*", wnd)
    epiBasePanel["Click"](90, 35);
    // epiBasePanel["Click"](90, 35);
    // Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|ToolBox")
    ClickMenu("Tools->ToolBox")
    // Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiTextBox", -1, 74, 11);
    lvwItems["ClickItemXY"]("EpiTextBox", -1, 74, 11);
    epiBasePanel["Click"](190, 35);
    // epiBasePanel["Click"](232, 34);
    // epiBasePanel["epiTextBox12"]["Click"](0, 0);
    // Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|ToolBox")
    ClickMenu("Tools->ToolBox")
    // Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiCombo", -1, 63, 8);
    lvwItems["ClickItemXY"]("EpiCombo", -1, 63, 8);
    epiBasePanel["Click"](290, 35);
    // epiBasePanel["Click"](99, 91);
    // Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|ToolBox")
    ClickMenu("Tools->ToolBox")
    // Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiDateTimeEditor", -1, 89, 12);
    lvwItems["ClickItemXY"]("EpiDateTimeEditor", -1, 89, 12);
    epiBasePanel["Click"](390, 35);
    // epiBasePanel["Click"](239, 89);

  }
  Log["Message"]("Step 29")
  ActivateForm("Customization Tool")
  //Save Customization and close
  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Save Customization");
  ClickMenu("File->Save Customization")
  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close");
  ClickMenu("File->Close")

  //Step30- Save dashboard
  Log["Message"]("Step 30")
  SaveDashboard()

  //Step31- Right Click on your tracker view and select the option Properties.        
  Log["Message"]("Step 31")
  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)["Nodes"]["Item"](1)["UIElement"]["Rect"]
  dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)

  // click 'Properties' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
  Log["Message"]("BAQTrackerV1 Summary - Customize Tracker View was selected from Menu")

  //Step32- Modify any Label caption and click Ok.
  Log["Message"]("Step 32")
  var TrackerViewsGrid = GetGrid("ultraGrid1")
  var columnLabel = getColumn(TrackerViewsGrid, "Label Caption")

  //find the row where Cust. ID is located
  for (var i = 0; i <= TrackerViewsGrid["Rows"]["Count"] - 1; i++) {
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
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
  ClickButton("OK")

  //Step33- Save dashboard
  Log["Message"]("Step 33")
  SaveDashboard()
}

function DeployDashb(){
  Log["Message"]("Step 34")
  DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")
  
  ExitDashboard()

  Log["Message"]("Dashboard created")
}

function CreateMenu1() {
  Log["Message"]("Step 35")
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)
}


function RestartE10(){
  Log["Message"]("Step 36")
  Delay(1000)
  RestartSmartClient()
  Log["Message"]("SmartClient Restarted")
}
  
function OpenMenuTestDashb(){
  Log["Message"]("Step 37")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  // Step38- Refresh Data
  Log["Message"]("Step 38")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All") 
  ClickMenu("Refresh All", "", true)
  
  // Test data from menu
  Log["Message"]("Step 39")
  testingDashboard("tracker") 
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  ClickMenu("File->Exit")  
}  

function AddNewQueryDashboard(){
  Log["Message"]("Step 46")
    //Navigate and open Dashboard
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
    Log["Checkpoint"]("Dashboard opened") 
    
    // Retrieve the previous dashboard       
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("DashTracker")
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")
    EnterText("txtDefinitonID", dashb1 + "[Tab]")

    Delay(2500)
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
    OpenPanelTab("General")

    //Step47-  Add a New Query and select the zAttribute query.    
    Log["Message"]("Step 47")  
    Delay(2000)
    AddQueriesDashboard("zAttribute")
    
      //Right click on the query summary and click on the Query   
      var dashboardTree = GetTreePanel("DashboardTree")

      rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
      dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // click 'New Tracker View' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
      Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

      var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]
      // DashboardQueryProperties("General")
      DashboardPropertiesTabs("General")

      //Check Auto Refresh on Load and change the Refresh Interval to 10  
      // queryProperties["tabGeneral"]["chkAutoRefresh"]["Checked"] = true
      CheckboxState("chkAutoRefresh", true)
      // queryProperties["tabGeneral"]["numRefresh"]["Click"]()
      // queryProperties["tabGeneral"]["numRefresh"]["Keys"]("[Del][Del][Del]10")
      EnterText("numRefresh", "[Del][Del][Del]10")
      // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
      ClickButton("OK")

      //Save dashboard
      SaveDashboard()

      rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
      dashboardTree["ClickR"](rect.X, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // Step48- click 'New Tracker View' option from menu
      Log["Message"]("Step 48")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("BAQTrackerV1 Summary - New Tracker View was selected from Menu")

      // var TrackerViewsGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
      var TrackerViewsGrid = GetGrid("ultraGrid1")
      
      // Step49- Check Prompt for AttrCode field and Inputs Prompt Only     
      Log["Message"]("Step 49")  
      var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")    

      //find the row where Attribut_AttrCode is located
      for (var i = 0; i <= TrackerViewsGrid["Rows"]["Count"] - 1; i++) {
        //Select Attribut_AttrCode row and check Prompt checkbox
        var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)

        if (cell["Text"] == "Attribut_AttrCode") {
          Log["Message"]("Data matchs with the parameter given = columnPrompt > "+ columnPrompt )
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
          // Check Prompt check box on Attribut_AttrCode field
          TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
          // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
          CheckboxState("chkInputPrompts", true)
        }
      }

      //Step50- Click Ok to close Properties
      Log["Message"]("Step 50")
      // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
      ClickButton("OK")

      SaveDashboard()
      Log["Checkpoint"]("Dashboard saved")
      
      //Step51- Deploy dashboard
      Log["Message"]("Step 51")
      DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")
      Log["Checkpoint"]("Dashboard deployed")

      ExitDashboard() 
}

  
function E10CacheRestart(){
  // Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|Clear Client Cache")
  ClickMenu("Options->Clear Client Cache")

  if (Aliases["Epicor"]["dlgEpicor"]["Exists"]) {
    // Aliases["Epicor"]["dlgEpicor"]["btnYes"]["Click"]()
    ClickButton("Yes")
  }
  RestartSmartClient()
  Log["Checkpoint"]("SmartClient Restarted")  
}  


function CreateAttributesQuery(){
  Log["Message"]("Step 53")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Attribute")

  // >Select New Attribute
  // Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|&New")
  ClickMenu("File->New")

  // >Enter ISO9000 on attribute field and description
  var attributePanel = Aliases["Epicor"]["AttributForm"]["windowDockingArea1"]["dockableWindow1"]["mainFillPanel1"]["windowDockingArea1"]["dockableWindow1"]["attributDetailPanel1"]

  attributePanel["grpAttribut"]["txtAttrCode"]["Keys"]("ISO9000")
  attributePanel["grpAttribut"]["txtDesc"]["Keys"]("ISO9000")

  // >Save
  // Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
  ClickMenu("File->Save")

  // Exit
  // Aliases["Epicor"]["AttributForm"]["zAttributMaintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")  
  ClickMenu("File->Exit")
}

function TestAttributeData(){
  Log["Message"]("Step 54")
  //Step54- Return to your menu with the dashboard        
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

  // validate if zAttribute (third query)  has records after opening dash menu and the record added ISO9000 is located in the records
  testingDashboard("Attribute") 
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  ClickMenu("File->Exit")

}


function testingDashboard(typeTesting) {
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All")
  ClickMenu("Refresh All", "", true)
  
  // var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]
    
  var gridDashboardPanelChildren = RetrieveGridsMainPanel()
  var trackerPDashboardChildren = RetrieveTrackerMainPanel()


  if (typeTesting == "tracker") {
    // Display the GroupCode combo box from the tracker views of added queries        

      //Get Children from the first two tracker Panels of the firsy Query
      var groupCodeTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var groupCodeTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      // var groupCodeTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

      // if (groupCodeTrackerP1["Exists"] == true && groupCodeTrackerP2["Exists"] == true && groupCodeTrackerP3["Exists"] == true) {
      if (groupCodeTrackerP1["Exists"] == true && groupCodeTrackerP2["Exists"] == true) {
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

      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      ClickMenu("Edit->Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        // Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
        ClickButton("Yes")
      }

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*txtCustomer_CustID", 1);
      var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

      if (custCompanyTrackerP1["Text"] == "" && custCompanyTrackerP2["Text"] == "") {
        Log["Checkpoint"]("Data from the Tracker Views of first query was cleared correctly")
      }else {
        Log["Error"]("Data from one of the Tracker Views of first query was  not cleared correctly")
      }

    //-From the first query select an option from the GroupCode drop down and click Refresh 

      //Validates if the tracker View data is cleared
      var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

      custCompanyTrackerP1["Keys"]("Aerospace")
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      ClickMenu("Edit->Refresh")

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

      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      ClickMenu("Edit->Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        // Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
        ClickButton("Yes")
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
    // Pending as there's a problem activating Tracker Tab
     /* var custIDTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*txtCustomer_CustID", 1);
      
      trackerPDashboardChildren[2]["Parent"]["Activate"]()
      custIDTrackerP3["Keys"]("A")
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      ClickMenu("Edit->Refresh")

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
      }*/
      
    //--------------------------------------
    
    // Change to the customized tab of your second tracker view   
      OpenPanelTab("testDashb")

      var trackerTestDashbPanelChildren = RetrieveTrackerMainPanel("testDashb")
      
      if(Aliases["Epicor"]["ExceptionDialog"]['Exists']){
        Log["Error"]("There is an error on the tab created")
      } else{
        if(trackerTestDashbPanelChildren[0]["Controls"]["Count"] > 1 ){
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



