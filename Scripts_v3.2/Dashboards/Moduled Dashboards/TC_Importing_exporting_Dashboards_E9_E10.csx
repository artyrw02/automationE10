//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions

function TC_Importing_exporting_Dashboards_E9E10(){}
  var dashboardImportedE10 = "C:\\Users\\Administrator\\Documents\\DashDef -Regression.dbd"  
  var dashboardID = "DashCaro" // Dashboard ID from imported dashboard
  var baqE9 = "EPIC06-testBAQCaro" //BAQ ID from imported dashboard

  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashbE9",
    "menuName" : "DashbE9",
    "orderSequence" : 2,
    "menuType" : "Dashboard-Assembly",
    "dll" : dashboardID,
    "validations" : "Enable"
  }

  // Variables
  var company1 = "Epicor Education"
  var plant1 = "Main"

  //Used to navigate thru the Main tree panel
  var treeMainPanel1 = setCompanyMainTree(company1,plant1)


function ImportDashboardE9(){

  // 2- Log in to E10. Go to Executive Analysis> Business Activity Management> Setup>Dashboard.  Go to Tools>Developer Mode       
    //Navigate and open Dashboard
    Log["Message"]("Step 2")
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

    // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
    Log["Message"]("Dashboard opened")
    
    //Enable Dashboard Developer Mode  
    DevMode()
    Log["Message"]("DevMode activated")
   
  // 3- Click File> Import Dashboard Definition and select the dashboard that you created. On Import BAQ Options click Ok        
    Log["Message"]("Step 3")

    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Import Dashboard Definition")
    ClickMenu("File->Import Dashboard Definition")
     
    var windowImportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Import Dashboard*","*ComboBox*"], 30)
    if (windowImportDashBD["Exists"]) {
      var windowImportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Open*","*Button*"], 30)
      windowImportDashBD["Keys"](dashboardImportedE10)
      windowImportDashBDSaveBtn["Click"]()
      Log["Message"]("Dashboard imported correctly")
    }else{
      Log["Error"]("Dashboard wasn't imported correctly, Object doesn't exists")    
    }

    if (Aliases["Epicor"]["DashboardBAQImportDialog"]["Exists"]) {
      ClickButton("OK")
      Log["Message"]("BAQ Import dialog is displayed")
      // Aliases["Epicor"]["DashboardBAQImportDialog"]["btnOkay"]["Click"]()
    }else{
      Log["Error"]("BAQ Import dialog is not displayed")
    }

    // temporal
    Delay(25000)
 
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    OpenPanelTab("General")

    var txtDefinitonID = GetText("txtDefinitonID")
    if(txtDefinitonID != ""){
      Log["Message"]("Dashboard Imported")
    }else{
      Log["Error"]("Dashboard wasn't imported")
    }

  // 4- Save your dashboard   
    Log["Message"]("Step 4")    
    SaveDashboard()
    ExitDashboard()
}

function RegenerateImportedBAQ(){
  // 5- Go to Updatable BAQ Maintenance (System Management> Upgrade/Mass Regeneration)   
    Log["Message"]("Step 5")      
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Updatable BAQ Maintenance")
    
  // 6- On Query ID retrieve the created query for your dashboard that you previously imported  
    // Aliases["Epicor"]["UBAQMaintForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["btnKeyField"]["Click"]()
    Log["Message"]("Step 6") 
    ClickButton("Query ID...")
    // Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["epiTabPage"]["basicPanel1"]["txtStartWith"]["Keys"](baqE9)
    EnterText("txtStartWith", baqE9)

    // Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["epiTabPage"]["basicPanel1"]["WinFormsObject"]("chkShared")["CheckState"] = "Indeterminate"
    CheckboxState("chkShared", "Indeterminate")
    // Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()
    ClickButton("Search")

    // var searchGrid = Aliases["Epicor"]["BAQDesignerSearchForm"]["FindChild"](["WndCaption","ClrClassName"], ["*Search Results*", "*Grid*"],30)
    var searchGrid = GetGrid("Search")
    var queryID = getColumn(searchGrid, "Query ID")

    for(var i = 0; i < searchGrid["Rows"]["Count"];i++){
      if(searchGrid["Rows"]["Item"](i)["Cells"]["Item"](queryID)["Text"]["OleValue"] == baqE9){
        searchGrid["Rows"]["Item"](i)["Activate"]()
        // Aliases["Epicor"]["BAQDesignerSearchForm"]["ultraStatusBar2"]["btnOK"]["Click"]()
        ClickButton("OK")
      }
    }

    //Used to activate query
    // var treeView = Aliases["Epicor"]["UBAQMaintForm"]["FindChild"]("ClrClassName", "*TreeView", 30)
    var treeView = GetTreePanel("treeView")
    treeView["ClickItem"]("Updatable Queries|" + baqE9)

  // 7- Select Actions>Regenerate selected  
  // Aliases["Epicor"]["UBAQMaintForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Regenerate Selected")
  Log["Message"]("Step 7") 
  ClickMenu("Actions->Regenerate Selected")
  Delay(1500)
  Log["Message"]("BAQ regenerated")  


  // Aliases["Epicor"]["UBAQMaintForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  ClickMenu("File->Exit")
}

function OpenImportedDashb(){
  
  Log["Message"]("Step 8")   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  OpenDashboard(dashboardID)
  Log["Message"]("Dashboard retrived")

    // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
    var dashboardTree = GetTreePanel("DashboardTree")

    var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
    dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
    Log["Message"]("BAQ - right click")

    // click 'Properties' option from menu of tracker view
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    Log["Message"]("EPIC06-testBAQCaro:  Summary - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    //Check the Updatable check box
    var chkUpdatable = verifyStateCheckbox("chkUpdatable")
    if(chkUpdatable){
      Log["Checkpoint"]("Updatable checkbox is Checked")
    }else{
      Log["Error"]("Updatable checkbox is not checked")
    }

    // var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
    var dashboardGrid = GetGrid("ultraGrid1")
    
    var column = getColumn(dashboardGrid, "Column")
    var columnPrompt = getColumn(dashboardGrid, "Prompt")

    var fields = "Customer_Address1,Customer_City,Customer_Country"

    fields = fields.split(",")

    //find the row where GroupCode is located
    for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
      //Select row and check Prompt checkbox
      var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

      for (var j = 0; j < fields["length"]; j++) {
        if(cell["Text"] == fields[j]) {
          if(dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"]){
            Log["Checkpoint"]("Prompt checkbox on " + fields[j] + " is Checked")
          }else{
            Log["Error"]("Prompt checkbox on " + fields[j] + " is not checked")
            break
          }
        }
      }
    } 

    // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
    ClickButton("OK")
    // //Go to View Rules tab
    //   var gridPaneldialog = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]
      
    //   DashboardPropertiesTabs(gridPaneldialog, "View Rules")

    //   var viewRulesPanel = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["tabViewRules"]["viewRuleWizardPanel1"]
    Log["Checkpoint"]("Grid validated correctly.")

    // 9- Go to tracker view properties on Dashboard designer     
    Log["Message"]("Step 9")   
    var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](1)
    dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
    Log["Message"]("BAQ - right click")

    // click 'Properties' option from menu
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    Log["Message"]("Tracker View - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    // Validates data of dashboard
    //Verify it only has CustID check box Prompt selected and on Condition it has StartsWith

    // var dashboardTrackerView = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
    var dashboardTrackerView = GetGrid("ultraGrid1")
        
    var column = getColumn(dashboardTrackerView, "Column")
    var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
    var columnCondition = getColumn(dashboardTrackerView, "Condition")
    
    var fields = "Customer_CustID"

    fields = fields.split(",")

    for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
      var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

      for (var j = 0; j < fields["length"]; j++) {
        if (cell["Text"] == fields[j]) {
          if(dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked" && 
              dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Text"]["OleValue"] == "StartsWith"){
            Log["Checkpoint"]("Prompt checkbox on " + fields[j] + " is Checked")
          }else{
            Log["Error"]("Prompt checkbox on " + fields[j] + " is not checked")
            break
          }
        }
      }
    }

    //Click Ok to close Properties
    // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()    
    ClickButton("OK")
}


function AddTrackerView(){
  // 10- On Dashboard designer add the zPOLine query 
  Log["Message"]("Step 10")      
  AddQueriesDashboard("zPOLine")

  var dashboardTree = GetTreePanel("DashboardTree")

  // 12- "Add a new tracker View. Select Clear All and set the following:
  Log["Message"]("Step 12")
  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"] + rect["Bounds"]["Bottom"])/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  Log["Message"]("New Tracker View was selected from Menu")

 // Visible & Prompt for PartNum and Description
  // var dashboardTrackerView = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
  var dashboardTrackerView = GetGrid("ultraGrid1")
  // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnClearAll"]["Click"]()
  ClickButton("Clear All")

  var column = getColumn(dashboardTrackerView, "Column")
  var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
  var columnVisible = getColumn(dashboardTrackerView, "Visible")
  var columnCondition = getColumn(dashboardTrackerView, "Condition")
  
  var fieldsVisible = "PODetail_PartNum,PODetail_LineDesc"

  fieldsVisible = fieldsVisible.split(",")

  for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
    var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

    for (var j = 0; j < fieldsVisible["length"]; j++) {
      if (cell["Text"]["OleValue"] == fieldsVisible[j]) {
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Checked" 

        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked" 

       //Activates 'Condition' column
          dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Click"]()

          while(true){
            dashboardTrackerView["Keys"]("[Down]")
            if (dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["EditorResolved"]["SelectedText"] == "StartsWith") {
              break
            }
          }
        Log["Checkpoint"]("Checkbox visible & prompt where checked for " + fieldsVisible[j])             
      }
    }
  }
  
  // Check Input Prompts Only
  // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
  CheckboxState("chkInputPrompts", true)
  
  //Click Ok to close Properties
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()   
  ClickButton("OK")

  //13- Click on Tools>Deploy Dashboard. Select Deploy Smart Client Application checkbox   
  Log["Message"]("Step 13")        
  DeployDashboard("Deploy Smart Client")

  SaveDashboard()
  ExitDashboard()
}   


function CreateMenu1(){
  Log["Message"]("Step 14")  
  //Open Menu maintenance   
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu(MenuData)
}

function RestartE10(){
  Log["Message"]("Step 15")  
  Delay(1000)
  RestartSmartClient()
  Log["Checkpoint"]("SmartClient Restarted")
}

function TestMenu(){
  // 16- On Main Menu go to Sales Management> CRM> Setup and open the previously created menu     
  Log["Message"]("Step 16")    
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

  var gridsMainPanel = RetrieveGridsMainPanel()
  var trackerMainPanel = RetrieveTrackerMainPanel()

  gridsMainPanel[0]["Click"]()

  //  17- Refresh
  Log["Message"]("Step 17")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")
  
  if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
    Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
  }else{
    Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
  }

  // 18- Go to the Tracker View and in Cust.ID field write E, then refresh  
  Log["Message"]("Step 18")
  // var custIDField = trackerMainPanel[0]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*CustID"], 30)
  var custIDField = GetTextBox("*CustID", trackerMainPanel[0])

  custIDField["Keys"]("E")

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  var columnCustID = getColumn(gridsMainPanel[0], "Cust. ID") 

  findValueInStringGrids(gridsMainPanel[0], columnCustID, "E")

  // 19- Update any register on column Address        
  Log["Message"]("Step 19")
  var columnAddress = getColumn(gridsMainPanel[0], "Address") 
  
  gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["Click"]()

  var oldValueAddress = gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["Text"]["OleValue"]
  var newValueAddress = gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["EditorResolved"]["SelectedText"] = "TEST"
  gridsMainPanel[0]["Rows"]["Item"](1)["Cells"]["Item"](columnAddress)["Click"]()

  if(oldValueAddress != newValueAddress){
    Log["Checkpoint"]("Data was updated.")
  }else{
    Log["Error"]("Data was not updated.")
  }

  // 20- Move to the zPOLine tab and click refresh   
  Log["Message"]("Step 20")     
  gridsMainPanel[1]["Click"]()
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  // 21- On Part field from tracker view enter "*00*" and click Refresh       
  Log["Message"]("Step 21")
  // var partField = trackerMainPanel[1]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*Part*"], 30)
  var partField = GetTextBox("*Part*", trackerMainPanel[1])

  partField["keys"]("*00*")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")
  
  var columnPart = getColumn(gridsMainPanel[1], "Part Num") 

  findValueInStringGrids(gridsMainPanel[1], columnPart, "00")

  // 22- On Part field from tracker view enter "0*" and click Refresh       
  Log["Message"]("Step 22")

  partField["keys"]("^a[Del]"+"0*")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  var columnPart = getColumn(gridsMainPanel[1], "Part Num") 

  findValueInStringGrids(gridsMainPanel[1], columnPart, "0")

  // 23- On Description field from tracker view enter "CRS ROUND 1.00" BAR STOCK" and click Refresh        
  // var descriptionField = trackerMainPanel[1]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*Desc*"], 30)
  var descriptionField = GetTextBox("*Desc*", trackerMainPanel[1])

  var columnDescription = getColumn(gridsMainPanel[1], "Description") 

  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1.00")

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.00")

  // 24- On Description field from tracker view enter "CRS ROUND 1.50" BAR STOCK" and click Refresh  
  Log["Message"]("Step 24")      
  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1.50")

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.50")
  
  // 25- On Description field from tracker view enter "CRS ROUND 1..0" BAR STOCK" and click Refresh        
  Log["Message"]("Step 25")
  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1..0")

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.")

  ClickMenu("File->Exit")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
    // Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["WinFormsObject"]("btnNo1")["Click"]()
    ClickButton("No")
  }
}


function RetrieveTrackerMainPanel(){
  // var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]
    
  // var trackerPDashboardChildren = dashboardMainPanel["FindAllChildren"]("FullName", "*TrackerPanel", 9)["toArray"]();

  var trackerPDashboardChildren = FindObjects("*EpiBasePanel*","Name", "*TrackerPanel")

  return trackerPDashboardChildren
}
