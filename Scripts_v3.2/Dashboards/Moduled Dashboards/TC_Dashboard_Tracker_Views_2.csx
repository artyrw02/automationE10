//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions
//USEUNIT Data_Dashboard_TrackerViews_2

function TC_Dashboard_Tracker_Views_2(){}
    
function CreateBAQ1(){
  ExpandComp(company1)

  ChangePlant(plant1)  

  Log["Message"]("Step 2")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  Log["Message"]("BAQ opened")

  Delay(2000)
  CopyBAQ("zCustomer01", baq1Copy)
  Log["Message"]("BAQ 'zCustomer01' copied to 'zCustomer01Copy'")
  
  CheckboxState("chkShared", true)
  CheckboxState("chkUpdatable", true)

  AddColumnsBAQ("Customer", "GroupCode")
  Log["Message"]("BAQ1 created")

  OpenPanelTab("Update->General Properties")

  UpdateTabBAQ("Customer_PhoneNum", "Updatable")
  UpdateTabBAQ("Customer_EMailAddress", "Updatable")

  OpenPanelTab("Update->Update Processing")

  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")
  listBOs["ClickItem"]("Erp.Customer")
  
  ClickButton("OK")
  
  SaveBAQ()      
}

function CreateBAQ2(){
  Log["Message"]("Step 3")
  ClickMenu("Edit->Clear")
  Log["Message"]("BAQ opened")

  Delay(2000)
  CopyBAQ("zPartTracker01", baq2Copy)
  Log["Message"]("BAQ 'zPartTracker01' copied to '" + baq2Copy + "'")
  
  CheckboxState("chkShared", true)
  CheckboxState("chkUpdatable", true)

  OpenPanelTab("Update->General Properties")

  UpdateTabBAQ("Part_ClassID", "Updatable")
  UpdateTabBAQ("Part_ProdCode", "Updatable")
  UpdateTabBAQ("Part_TaxCatID", "Updatable")

  OpenPanelTab("Update->Update Processing")

  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")
  listBOs["ClickItem"]("Erp.Part")

  ClickButton("OK")
  SaveBAQ()      
}

function CreateBAQ3(){
  Log["Message"]("Step 3A")

  ClickMenu("Edit->Clear")

  CreateBAQ(baq3["baq"], baq3["baq"], baq3["config"])

  AddTableBAQ(baq3["Table"], baq3["Alias"])

  AddColumnsBAQ(baq3["Alias"], baq3["Columns"])
  
  OpenPanelTab("Update->General Properties")
  CheckboxState("chkSupportMDR", true)

  UpdateTabBAQ("Customer_Country", "Updatable")
  UpdateTabBAQ("Customer_City", "Updatable")
  UpdateTabBAQ("Customer_State", "Updatable")

  ClickButton("Advanced Column Editor Configuration...")

  var treePanel = GetTreePanel("treeView")
  treePanel["ClickItem"]("Updatable Fields|Customer_Country")

  ComboboxSelect("cmbEditorType", "DropDown List")
  
  ComboboxSelect("cmbDataFrom", "BAQ")
  
  EnterText("txtExportID", "zCustomer01" + "[Tab]")
  
  ComboboxSelect("cmbDisplay", "Customer_Country")
  ComboboxSelect("cmbValue", "Customer_Country")

  Delay(1500)
  ClickMenu("File->Save")
  Delay(1500)
  ClickMenu("File->Exit")

  Delay(1500)
  E10["Refresh"]()
  OpenPanelTab("Update->Update Processing")
  
  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")
  listBOs["ClickItem"]("Erp.Customer")
  
  ClickButton("OK")

  AnalyzeSyntaxisBAQ()

  Delay(6500)

  ClickButton("Get List")
  Delay(1500)
  ClickButton("Yes")

  E10["Refresh"]()

  SaveBAQ()
  ExitBAQ()  
}      
        
function CreateDashboard(){

  Log["Message"]("Step 4")
  
  E10["Refresh"]()
  
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  Log["Message"]("Dashboard opened")
  DevMode()
  Log["Message"]("DevMode activated")

  Log["Message"]("Step 5")
  NewDashboard(dashbID, dashbID, dashbID, "chkInhibitRefreshAll")  
}
  
function AddQuery1(){
  Log["Message"]("Step 6")
  Delay(2500)
  AddQueriesDashboard(baq1Copy, baq1Copy)
    
  Log["Message"]("Step 7")

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + "|" + baq1Copy + ": Summary", "Properties")
   
  if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
    Log["Message"]("Dashboard properties dialog appears")
  }
    
  Log["Message"]("Step 8")
  CheckboxState("chkUpdatable", true)

  var dashboardGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(dashboardGrid, "Column")
  var columnPrompt = getColumn(dashboardGrid, "Prompt")

   //find the row where GroupCode is located
  for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

    if (cell["Text"] == "Customer_PhoneNum") {
      dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
    if (cell["Text"] == "Customer_EMailAddress") {
      dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
  }

  ClickButton("OK")
}    
   
function AddTrackerView1Query1(){
  Log["Message"]("Step 9")

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy, "New Tracker View")
   
  // Sets an alias for the tracker view
  EnterText("txtCaption", baq1Copy + ": Tracker" + " 0") 

  Log["Message"]("Step 10")

  var TrackerViewsGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")
  var columnCondition = getColumn(TrackerViewsGrid, "Condition")

  /*Check Prompt for CustID and enter condition: StartsWith and check prompt for Phone, EMailAddress and GroupCode. Click Ok        */
  for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

    if (cell["Text"] == "Customer_CustID") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
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
    if (cell["Text"] == "Customer_PhoneNum") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
    if (cell["Text"] == "Customer_EMailAddress") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
    if (cell["Text"] == "Customer_GroupCode") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }            
  }

  ClickButton("OK") 
}      
    
function AddTrackerView2Query1(){ 
  Log["Message"]("Step 11")
  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy, "New Tracker View")
   
  // Sets an alias for the tracker view
  EnterText("txtCaption", baq1Copy + ": Tracker" + " 1") 

  Log["Message"]("Step 12")

  var TrackerViewsGrid = GetGrid("ultraGrid1")
  
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")
  var columnCondition = getColumn(TrackerViewsGrid, "Condition")

  for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

    if (cell["Text"] == "Customer_CustID") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
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
    if (cell["Text"] == "Customer_PhoneNum") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
    if (cell["Text"] == "Customer_EMailAddress") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }
    if (cell["Text"] == "Customer_GroupCode") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
    }            
  }

  ClickButton("OK")
}
          
function ValidateDisplayGroupTV(){
  Log["Message"]("Step 15")

  var trackerPDashboardChildren = RetrieveTrackerMainPanel()

  //Validates if the tracker View data is cleared
  var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
  var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

  custCompanyTrackerP1["Keys"]("Aerospace")
  custCompanyTrackerP2["Keys"]("Aerospace")

  if(custCompanyTrackerP1["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 1 has values")
  }

  if(custCompanyTrackerP2["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 2 has values")
  }

  SaveDashboard()

  Log["Message"]("Step 16")
  ExitDashboard()
  
  if (!Aliases["Epicor"]["Dashboard"]["Exists"]) {
    Log["Message"]("Dashboard closed")
  }  
}
               
function ReopenDashbValidateGroup(){
  Log["Message"]("Step 17")
  
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  
  OpenDashboard(dashbID)

  if(Aliases["Epicor"]["Dashboard"]["Text"] != ""){
    Log["Message"]("Dashboard retrieved")
  }

  Log["Message"]("Step 18")
  
  var trackerPDashboardChildren = RetrieveTrackerMainPanel()

  //Validates if the tracker View data is cleared
  var custCompanyTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
  var custCompanyTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

  custCompanyTrackerP1["Keys"]("Aerospace")
  custCompanyTrackerP2["Keys"]("Aerospace")

  if(custCompanyTrackerP1["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 1 has values")
  }

  if(custCompanyTrackerP2["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 2 has values")
  }  
}  

function AddQuery2(){
  Log["Message"]("Step 19")
  Delay(2500)

  AddQueriesDashboard(baq2Copy, baq2Copy)  

  Log["Message"]("Step 21")

  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy + "|" + baq2Copy + ": Summary", "Properties")
   
  if(Aliases["Epicor"]["DashboardProperties"]["Exists"]){
    EnterText("txtCaption" , "All")

    ClickButton("OK")
  }
}

function AddGrid1Query2(){
  Log["Message"]("Step 22")

  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy, "New Grid View")
  
  Log["Message"]("Step 23")
  if(Aliases["Epicor"]["DashboardProperties"]["Exists"]){
    EnterText("txtCaption", "Manufactured Parts")
    
    Log["Message"]("Step 24")
    DashboardGridPropertiesTabs("Filter")

    var ultraGrid = GetGrid("ultraGrid1")

    SelectCellDropdownGrid("ColumnName", "Part_TypeCode", ultraGrid)
    SelectCellDropdownGrid("Condition", "=", ultraGrid)
    
    var columnValue = getColumn(ultraGrid, "Value")
    
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["Click"]()
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["EditorResolved"]["SelectedText"]  = "M"
    
    ClickButton("OK")

  }  
}
  
function AddGrid2Query2(){
  Log["Message"]("Step 26")
  //Right click on the query summary and click on the Query 
  /*var dashboardTree =  GetTreePanel("DashboardTree")

  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("Right click on query")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Grid View");
  Log["Message"]("New Grid View was selected from Menu")*/
  
  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy, "New Grid View")

  Log["Message"]("Step 27")
  if(Aliases["Epicor"]["DashboardProperties"]["Exists"]){
    // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["txtCaption"]["Text"] = "Purchased Parts"
    EnterText("txtCaption", "Purchased Parts")
    // var gridPaneldialog = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]
      
    Log["Message"]("Step 28")
    DashboardGridPropertiesTabs("Filter")

    // var ultraGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["FilterTab"]["pnlFilter"]["ultraGrid1"]
    var ultraGrid = GetGrid("ultraGrid1")

    SelectCellDropdownGrid("ColumnName", "Part_TypeCode", ultraGrid)
    SelectCellDropdownGrid("Condition", "=", ultraGrid)
    
    var columnValue = getColumn(ultraGrid, "Value")
    
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["Click"]()
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["EditorResolved"]["SelectedText"]  = "P"
    ultraGrid["keys"]("[Del]")
    
    // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
    ClickButton("OK")

  }  
}

function AddGrid3Query2(){
  Log["Message"]("Step 30")

  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy, "New Grid View")
     
  Log["Message"]("Step 31")
  if(Aliases["Epicor"]["DashboardProperties"]["Exists"]){
    EnterText("txtCaption", "Sales Kit Parts")

    Log["Message"]("Step 32")
    DashboardGridPropertiesTabs("Filter")

    var ultraGrid = GetGrid("ultraGrid1")

    SelectCellDropdownGrid("ColumnName", "Part_TypeCode", ultraGrid)
    SelectCellDropdownGrid("Condition", "=", ultraGrid)
    
    var columnValue = getColumn(ultraGrid, "Value")
    
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["Click"]()
    ultraGrid["Rows"]["Item"](0)["Cells"]["Item"](columnValue)["EditorResolved"]["SelectedText"]  = "K"
    
    ClickButton("OK")
  } 
}

function AddTrackerView1Query2(){
 Log["Message"]("Step 34")

  //Right click on the query summary and click on the Query   
  /*var dashboardTree =  GetTreePanel("DashboardTree")     
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
  Log["Message"]("BAQ1 Summary - New Tracker View was selected from Menu")*/
  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy, "New Tracker View")

  //Check the 'Updatable' check box
  // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["chkUpdatable"]["Checked"] = true
  CheckboxState("chkUpdatable", true)

  // select 'prompt' for Phone and EMailAddress. Click Ok
  // var dashboardTrackerView = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
  var dashboardTrackerView = GetGrid("ultraGrid1")
  
  var column = getColumn(dashboardTrackerView, "Column")
  var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
  var columnLabel = getColumn(dashboardTrackerView, "Label Caption")
  var columnVisible = getColumn(dashboardTrackerView, "Visible")
  
  var promptFields = "Part_ClassID,Part_ProdCode,Part_TaxCatID"
  var uncheckVisible = "InActive,Global,Non-Stock Item,Track Lots,Track Dim,Track Serial,Method,Company"

  promptFields = promptFields.split(",")

   //find the row where GroupCode is located
  for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

    for (var j = 0; j < promptFields["length"]; j++) {
      if (cell["Text"] == promptFields[j]) {
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
        // Check Prompt check box on field
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"
        break
      }
    }
  }

  uncheckVisible = uncheckVisible.split(",")

   //find the row where Label matches the specified in order to uncheck visible checkbox
  for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnLabel)

    for (var j = 0; j < uncheckVisible["length"]; j++) {
      if (cell["Text"] == uncheckVisible[j]) {
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
        // Check Prompt check box on field
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Unchecked"
        break
      }
    }
  }
  //Click Ok to close Properties
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()    
  ClickButton("OK")

  SaveDashboard()

  ExitDashboard()
} 
       
function UpdateBAQ(){
  Log["Message"]("Step 39")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  
  Log["Message"]("BAQ opened")

  OpenBAQ(baq2Copy)
  UpdateTabBAQ("Part_PartDescription", "Updatable")

  SaveBAQ()
  ExitBAQ()  
}

function CustomizeTrackerViewQuery2(){
  Log["Message"]("Step 40")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  Log["Message"]("BAQ opened")
  //  Maximize Dashboard
  Aliases["Epicor"]["Dashboard"]["Maximize"]();
  
  OpenDashboard(dashbID)
  // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]

  Log["Message"]("Step 41")
  var dashboardTree =  GetTreePanel("DashboardTree")
  
  var trackerPanelsDashboard = RetrieveTrackerMainPanel()
  /*
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)["Nodes"]["Item"](4)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Customize Tracker View");
  Log["Message"]("BAQ1 Summary - Customize Tracker View was selected from Menu")*/
  ClickPopupMenu("Queries|" + baq2Copy + ": " + baq2Copy + "|" + baq2Copy + ": Tracker", "Customize Tracker View")

  /*
    Step No: 42
    Step: Select Description label (lblPart_PartDescription) and change the Width property to '0' so the Size now is '0,20'       
    Result: The changes are set       
  */ 
  Log["Message"]("Step 42")
  // var trackerNode = Aliases["Epicor"]["CustomToolsDialog"]["grpControlTree"]["utrControls"]["Nodes"]["Item"](0)
  Delay(2500)
  var trackerNode = GetTreePanel("utrControls")
  trackerNode = trackerNode["Nodes"]["Item"](0)
  trackerNode["Expanded"] = true

  for (var i = 0; i < trackerNode["Nodes"]["Count"]; i++) {
    if (trackerNode["Nodes"]["Item"](i)["Text"] == "TrackerPanel") {
      trackerNode["Nodes"]["Item"](i)["Selected"] = true
      trackerNode["Nodes"]["Item"](i)["Expanded"] = true
      for (var j = 0; j < trackerNode["Nodes"]["Item"](i)["Nodes"]["Count"]; j++) {
        if(trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Text"] == "lblPart_PartDescription"){
          trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Selected"] = true
          break
        }
      }
    }
  }
  
  // var epiPropertyGrid = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgProperties"]["pnlControlProperties"]["pnlProperties"]["pgdProperties"];

   var epiPropertyGrid = FindObject("*PropertyGrid*", "Name", "*pgdProperties*")

  epiPropertyGrid["wItems"]("Layout")["Expand"]("Size");
  epiPropertyGrid["wItems"]("Layout")["wItems"]("Size")["ClickLabel"]("Width");
  epiPropertyGrid["Keys"]("^a[Del]" + "0" + "[Enter]");
  epiPropertyGrid["wItems"]("Layout")["wItems"]("Size")["ClickLabel"]("Height");
  epiPropertyGrid["Keys"]("^a[Del]" + "20" + "[Enter]");
  
  /*
    Step No: 43
    Step: Click Tools>Toolbox and select an EpiTextBox and drop it in the tracker below the other fields        
    Result: An EpiTextBox appears now, below the other fields    
  */ 
  Log["Message"]("Step 43")
  // var dashboardPanel = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]
  // var trackerPanelsDashboard = dashboardPanel["FindAllChildren"]("FullName", "*TrackerPanel", 20)["toArray"]();     
  

  // Aliases["Epicor"]["CustomToolsDialog"]["UltraMainMenu"]["Click"]("Tools|ToolBox");
  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|ToolBox")
  ClickMenu("Tools->ToolBox")

  // Aliases["Epicor"]["ToolboxForm"]["toolbox"]["ToolboxTab"]["tableLayoutPanel1"]["lvwItems"]["ClickItemXY"]("EpiTextBox", -1, 74, 11);
  var toolboxList = GetList("lvwItems")
  toolboxList["ClickItemXY"]("EpiTextBox", -1, 74, 11)

  
  trackerPanelsDashboard[2]["Click"](300, 35);

  /*
    Step No: 44
    Step: "On Properties tab set the followin for the EpiTextBox:
          set IsTrackerQueryControl = true, 
          set QueryColumn = Part_PartDescription
          set DashboardPrompt = true
          Save and close"       
    Result: An EpiTextBox appears now, below the other fields    
  */ 
  Log["Message"]("Step 44")
  Delay(2500)
  for (var i = 0; i < trackerNode["Nodes"]["Count"]; i++) {
    if (trackerNode["Nodes"]["Item"](i)["Text"] == "TrackerPanel") {
      trackerNode["Nodes"]["Item"](i)["Selected"] = true
      trackerNode["Nodes"]["Item"](i)["Expanded"] = true
      for (var j = 0; j < trackerNode["Nodes"]["Item"](i)["Nodes"]["Count"]; j++) {
        if(trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Text"] == "[C]epiTextBox1"){
          trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Selected"] = true
          break
        }
      }
    }
  }
  
  epiPropertyGrid["wItems"]("Misc")["ClickLabel"]("IsTrackerQueryControl");
  epiPropertyGrid["Keys"]("^a[Del]" + "True" + "[Enter]");
  epiPropertyGrid["wItems"]("Misc")["ClickLabel"]("QueryColumn");
  epiPropertyGrid["Keys"]("^a[Del]" + "Part_PartDescription" + "[Enter]");
  epiPropertyGrid["wItems"]("Dashboard")["ClickLabel"]("DashboardPrompt");
  epiPropertyGrid["Keys"]("^a[Del]" + "True" + "[Enter]");

  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization")
  ClickMenu("File->Save Customization")
  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")  
  ClickMenu("File->Close")
}

function AddQuery3(){
  Log["Message"]("Step 45")
  Delay(2500)
  var invDtlQuery = "zAPInvDtl"
  AddQueriesDashboard(invDtlQuery, invDtlQuery)
}
 
function AddTrackerView1Query3(){
  Log["Message"]("Step 46")
  var invDtlQuery = "zAPInvDtl"
  ClickPopupMenu("Queries|" + invDtlQuery + ": " + invDtlQuery, "New Tracker View")

  if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
    ClickButton("Clear All")
  }

  var dashboardTrackerView = GetGrid("ultraGrid1")
  
  var column = getColumn(dashboardTrackerView, "Column")
  var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
  var columnVisible = getColumn(dashboardTrackerView, "Visible")
  
  var promptFields = "APInvDtl_InvoiceNum"

  promptFields = promptFields.split(",")

  for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

    for (var j = 0; j < promptFields["length"]; j++) {
      if (cell["Text"] == promptFields[j]) {
        // Makes field visible
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Checked"

        // Check Prompt check box on field
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"              
        break
      }
    }
  }        

  // Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
  CheckboxState("chkInputPrompts", true)
  //Click Ok to close Properties
  // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()    
  ClickButton("OK")

  //used to recreate object on testcomplete
  /*var gridDashboard = dashboardPanel["FindAllChildren"]("FullName", "*grid*", 7)["toArray"]();
  
  for (var i = 0; i < gridDashboard["length"]; i++) {
    if (gridDashboard[i]["WndCaption"] == 'zAPInvDtl: Summary') {
      gridInvDtlPanel = gridDashboard[i]
      break
    }
  }*/  
}

function CustomizeTrackerViewQuery3(){

  Log["Message"]("Step 47")
  var invDtlQuery = "zAPInvDtl"
  
  var trackerPanelsDashboard = RetrieveTrackerMainPanel()  

  ClickPopupMenu("Queries|" + invDtlQuery + ": " + invDtlQuery + "|" + invDtlQuery + ": Tracker", "Customize Tracker View")

  Log["Message"]("Step 48")
  Delay(2500)
  ClickMenu("Tools->ToolBox")
  Delay(2500)
  
  var toolboxList = GetList("lvwItems")
  toolboxList["ClickItemXY"]("EpiCombo", -1, 63, 8)

  trackerPanelsDashboard[3]["Click"](300, 35);

  Log["Message"]("Step 49")

  Delay(2500)

  E10["Refresh"]()
  ActivateForm("Customization Tools Dialog")
  var trackerNode = GetTreePanel("utrControls")
  Delay(2500)
  
  trackerNode = trackerNode["Nodes"]["Item"](0)
  trackerNode["Expanded"] = true

  for (var i = 0; i < trackerNode["Nodes"]["Count"]; i++) {
    if (trackerNode["Nodes"]["Item"](i)["Text"] == "TrackerPanel") {
      trackerNode["Nodes"]["Item"](i)["Selected"] = true
      trackerNode["Nodes"]["Item"](i)["Expanded"] = true
      for (var j = 0; j < trackerNode["Nodes"]["Item"](i)["Nodes"]["Count"]; j++) {
        if(trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Text"] == "[C]epiCombo1"){
          trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Selected"] = true
          break
        }
      }
    }
  }
  
  var epiPropertyGrid = FindObject("*PropertyGrid*", "Name", "*pgdProperties*")

  delay(1000)
  epiPropertyGrid["wItems"]("Misc")["ClickLabel"]("IsTrackerQueryControl");
  epiPropertyGrid["Keys"]("^a[Del]" + "True" + "[Enter]");
  delay(1000)
  epiPropertyGrid["wItems"]("Dashboard")["ClickLabel"]("DashboardPrompt");
  epiPropertyGrid["Keys"]("^a[Del]" + "True" + "[Enter]");
  delay(1000)
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("EpiBOName");
  epiPropertyGrid["Keys"]("^a[Del]" + "Erp:BO:Part" + "[Tab]" + "[Enter]" + "[Enter]");
  
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("EpiColumns");
  epiPropertyGrid["Keys"]("^a[Tab]" + "[Enter]");
  if (Aliases["Epicor"]["StringCollectionEditor"]["Exists"]) {
    EnterText("textEntry", "^a[Del]" + "PartNum")
    ClickButton("OK")
  }
  delay(1000)
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("DisplayMember");
  epiPropertyGrid["Keys"]("^a[Del]" + "PartNum" + "[Enter]");
  delay(1000)
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("EpiSort");
  epiPropertyGrid["Keys"]("^a[Del]" + "PartNum" + "[Enter]");
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("EpiTableName");
  delay(1000)
  epiPropertyGrid["Keys"]("^a[Del]" + "Part" + "[Enter]");
  epiPropertyGrid["wItems"]("EpiCombo")["ClickLabel"]("ValueMember");
  epiPropertyGrid["Keys"]("^a[Del]" + "PartNum" + "[Enter]");      
  delay(1000)
  epiPropertyGrid["wItems"]("Misc")["ClickLabel"]("QueryColumn");
  epiPropertyGrid["Keys"]("^a[Del]" + "APInvDtl_PartNum" + "[Enter]");

  ClickMenu("File->Save Customization")
  ClickMenu("File->Close")
}      

function AddQuery4(){
  Log["Message"]("Step 55A")
  
  AddQueriesDashboard(baq3["baq"])  
}

function AddTrackerView1Query4(){
  
  Log["Message"]("Step 55B")

  ClickPopupMenu("Queries|" + baq3["baq"] + ": " + baq3["baq"], "New Tracker View")

  EnterText("txtCaption", "Hidden Country")

  if (Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["Exists"]) {
    ClickButton("Clear All")
  }

  // select 'prompt' for Phone and EMailAddress. Click Ok
  var dashboardTrackerView = GetGrid("ultraGrid1")
  
  var column = getColumn(dashboardTrackerView, "Column")
  var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
  var columnVisible = getColumn(dashboardTrackerView, "Visible")
  
  var promptFields = "Customer_Country"

  promptFields = promptFields.split(",")

  for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

    for (var j = 0; j < promptFields["length"]; j++) {
      if (cell["Text"] == promptFields[j]) {
        // Makes field visible
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Checked"

        // Check Prompt check box on field
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
        dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"              
        break
      }
    }
  }        

  ClickButton("OK")  
}

function CustomizeTrackerViewQuery4(){
  Log["Message"]("Step 55C")
  /*var dashboardTree =  GetTreePanel("DashboardTree")
  
  rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](3)["Nodes"]["Item"](1)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("BAQ - right click")

  // click 'New Tracker View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Customize Tracker View");
  Log["Message"]("BAQ1 Summary - Customize Tracker View was selected from Menu")*/

  ClickPopupMenu("Queries|" + baq3["baq"] + ": " + baq3["baq"] + "|" + "Hidden Country", "Customize Tracker View")


  // var trackerNode = Aliases["Epicor"]["CustomToolsDialog"]["grpControlTree"]["utrControls"]["Nodes"]["Item"](0)
  var trackerNode = GetTreePanel("utrControls")
  Delay(2500)
  trackerNode = trackerNode["Nodes"]["Item"](0)
  trackerNode["Expanded"] = true

  for (var i = 0; i < trackerNode["Nodes"]["Count"]; i++) {
    if (trackerNode["Nodes"]["Item"](i)["Text"] == "TrackerPanel") {
      trackerNode["Nodes"]["Item"](i)["Selected"] = true
      trackerNode["Nodes"]["Item"](i)["Expanded"] = true
      for (var j = 0; j < trackerNode["Nodes"]["Item"](i)["Nodes"]["Count"]; j++) {
        if(trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Text"] == "cboCustomer_Country"){
          trackerNode["Nodes"]["Item"](i)["Nodes"]["Item"](j)["Selected"] = true
          break
        }
      }
    }
  }
  
  // var epiPropertyGrid = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgProperties"]["pnlControlProperties"]["pnlProperties"]["pgdProperties"]
  var epiPropertyGrid = FindObject("*PropertyGrid*", "Name", "*pgdProperties*")

  Delay(1000)
  epiPropertyGrid["wItems"]("Behavior")["ClickLabel"]("Visible");
  epiPropertyGrid["Keys"]("^a[Del]" + "False" + "[Enter]");
  Delay(1000)

  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization")
  ClickMenu("File->Save Customization")
  // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
  ClickMenu("File->Close")
}

function DeployDashbClose(){
  Log["Message"]("Step 56")
  SaveDashboard()
  Delay(2500)
  DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")
  ExitDashboard()  
}

function CreateMenu1(){
  Log["Message"]("Step 57")
  Delay(2500)
  E10["Refresh"]()
  
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)  
}

function RestartE10(){
  ClickMenu("Options->Clear Client Cache")
  ClickButton("Yes")

  Log["Message"]("Step 60")
  Delay(1000)
  RestartSmartClient()
  Log["Message"]("SmartClient Restarted")  
}

function OpenMenu(){
  Log["Message"]("Step 61")
  Delay(1000)

  E10["Refresh"]()
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])
}

function testQuery1(){
  
  Log["Message"]("Step 62")
  Delay(2500)
  ClickMenu("Refresh All", "", true)

  var trackerMainPanel = RetrieveTrackerMainPanel()

  var gridPanel = GetGridMainPanel(baq1Copy)

  Log["Message"]("Step 63")
  if (gridPanel["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanel["WndCaption"] + " retrieved " + gridPanel["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanel["WndCaption"] + " retrieved " + gridPanel["Rows"]["Count"] + " records.")
  }

  var columnCustID = getColumn(gridPanel, "Cust. ID") 
  var columnGroup = getColumn(gridPanel, "Group")

  var custGroupCodeTrackerP1 = trackerMainPanel[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
  var custGroupCodeTrackerP2 = trackerMainPanel[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

  //Move through the first 5 rows from grid to check the groupcode are updated
  for (var i = 0; i < 5; i++) {
    gridPanel["Rows"]["Item"](i)["Cells"]["Item"](columnCustID)["Activate"]()
    if (custGroupCodeTrackerP1["Text"]["OleValue"] == custGroupCodeTrackerP2["Text"]["OleValue"]) {
      Log["Message"]("Group codes are the same. Tracker 1 = " + custGroupCodeTrackerP1["Text"]["OleValue"] + " | Tracker 2 = " + custGroupCodeTrackerP2["Text"]["OleValue"])
      break
    }else{
      Log["Error"]("Group codes are not the same. Tracker 1 = " + custGroupCodeTrackerP1["Text"]["OleValue"] + " | Tracker 2 = " + custGroupCodeTrackerP2["Text"]["OleValue"])
    }
  }
     
  Log["Message"]("Step 64")
  
  custGroupCodeTrackerP1["Keys"]("Aerospace")
  custGroupCodeTrackerP2["Keys"]("Aerospace")

  if(custGroupCodeTrackerP1["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 1 has values")
  }

  if(custGroupCodeTrackerP2["SelectedRow"]["ListObject"]["Row"]["Table"]["Count"] > 0){
    Log["Checkpoint"]("Dropdown from GroupCode of Tracker View 2 has values")
  }

  Log["Message"]("Step 65")

  ClickMenu("Edit->Clear")

  if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
    ClickButton("Yes")
  }

  if (gridPanel["Rows"]["Count"] == 0 && custGroupCodeTrackerP1["Text"]["OleValue"] == "" && custGroupCodeTrackerP2["Text"]["OleValue"] == "" ) {
    Log["Checkpoint"]("Form cleared.")
  }else{
    Log["Error"]("Form still have records.")
  }

  Log["Message"]("Step 66")
  var custIDTrackerP1 = trackerMainPanel[0]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

  custIDTrackerP1["Keys"]("A")
  ClickMenu("Edit->Refresh")

  //Select first record on BAQ1 results to notice change of data on BAQ2

  columnCustID = getColumn(gridPanel, "Cust. ID") 
  
  for (var i = 0; i < gridPanel["Rows"]["Count"]; i++) {
    //Points to column Cust. ID
    var aString = gridPanel["Rows"]["Item"](i)["Cells"]["Item"](columnCustID)["Text"]["OleValue"]
    var aSubString = custIDTrackerP1["Text"]["OleValue"]
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
      
  Log["Message"]("Step 67")
  ClickMenu("Edit->Clear")

  if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
    ClickButton("Yes")
  }  

  if (gridPanel["Rows"]["Count"] == 0 && custGroupCodeTrackerP1["Text"]["OleValue"] == "" && custGroupCodeTrackerP2["Text"]["OleValue"] == "" ) {
    Log["Checkpoint"]("Form cleared.")
  }else{
    Log["Error"]("Form still have records.")
  }

  Log["Message"]("Step 68")
  var custIDTrackerP2 = trackerMainPanel[1]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

  custIDTrackerP2["Keys"]("DALTON")
  ClickMenu("Edit->Refresh")

  //Select first record on BAQ1 results to notice change of data on BAQ2
  columnCustID = getColumn(gridPanel, "Cust. ID") 
  
  for (var i = 0; i < gridPanel["Rows"]["Count"]; i++) {
    //Points to column Cust. ID
    var aString = gridPanel["Rows"]["Item"](i)["Cells"]["Item"](columnCustID)["Text"]["OleValue"]
    var aSubString = custIDTrackerP2["Text"]["OleValue"]
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
    Log["Checkpoint"]("Grid retrieved just records with dalton " )
  }

  Log["Message"]("Step 69")
  ClickMenu("Edit->Clear")

  if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
    ClickButton("Yes")
  }  

  if (gridPanel["Rows"]["Count"] == 0 && custGroupCodeTrackerP1["Text"]["OleValue"] == "" && custGroupCodeTrackerP2["Text"]["OleValue"] == "" ) {
    Log["Checkpoint"]("Form cleared.")
  }else{
    Log["Error"]("Form still have records.")
  }

  Log["Message"]("Step 70")
  ClickMenu("Refresh All", "", true)

  Log["Message"]("Step 71")

  var phoneNum = getColumn(gridPanel, "Phone")

  gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum)["Activate"]()

  var oldValuePhoneN = gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum)["Text"]["OleValue"]

  var custPhoneNTrackerP2 = trackerMainPanel[0]["FindChild"]("FullName", "*txtCustomer_PhoneNum", 1);

  custPhoneNTrackerP2["Keys"]("012012012")
  gridPanel["Rows"]["Item"](1)["Cells"]["Item"](phoneNum)["Activate"]()

  if (gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum)["Text"]["OleValue"] == oldValuePhoneN) {
    Log["Checkpoint"]("Column 'Phone Number' was not updated")
  }else{
    Log["Error"]("Column 'Phone Number' was updated")
  }

  Log["Message"]("Step 72")

  var oldValueEmail = gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum+1)["Text"]["OleValue"]

  gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum+1)["Activate"]()
  var custEmailTrackerP2 = trackerMainPanel[0]["FindChild"]("FullName", "*txtCustomer_EMailAddress", 1);

  custEmailTrackerP2["Keys"]("012012012")

  gridPanel["Rows"]["Item"](1)["Cells"]["Item"](phoneNum+1)["Activate"]()
  if (gridPanel["Rows"]["Item"](0)["Cells"]["Item"](phoneNum+1)["Text"]["OleValue"] == custEmailTrackerP2["Text"]["OleValue"]) {
    Log["Checkpoint"]("Column 'Email Address' was not updated")
  }else{
    Log["Error"]("Column 'Email Address' was updated")
  }

}

function testQuery2(){
  ClickMenu("Refresh All", "", true)

  var trackerMainPanel = RetrieveTrackerMainPanel()

  var gridPanelAll = GetGridMainPanel("All")
  var gridPanelManufactured = GetGridMainPanel("Manufactured Parts")
  var gridPanelPurchased = GetGridMainPanel("Purchased Parts")
  var gridPanelSales = GetGridMainPanel("Sales Kit Parts")

  Log["Message"]("Step 74")

  if (gridPanelAll["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanelAll["WndCaption"] + " retrieved " + gridPanelAll["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanelAll["WndCaption"] + " retrieved " + gridPanelAll["Rows"]["Count"] + " records.")
  }  
  if (gridPanelManufactured["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanelManufactured["WndCaption"] + " retrieved " + gridPanelManufactured["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanelManufactured["WndCaption"] + " retrieved " + gridPanelManufactured["Rows"]["Count"] + " records.")
  }   
  if (gridPanelPurchased["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanelPurchased["WndCaption"] + " retrieved " + gridPanelPurchased["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanelPurchased["WndCaption"] + " retrieved " + gridPanelPurchased["Rows"]["Count"] + " records.")
  }   
  if (gridPanelSales["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanelSales["WndCaption"] + " retrieved " + gridPanelSales["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanelSales["WndCaption"] + " retrieved " + gridPanelSales["Rows"]["Count"] + " records.")
  }

  var columnTypeCodeA = getColumn(gridPanelAll, "Type") 
  var columnTypeCodeM = getColumn(gridPanelManufactured, "Type") 
  var columnTypeCodeP = getColumn(gridPanelPurchased, "Type") 
  var columnTypeCodeK = getColumn(gridPanelSales, "Type") 
  var flag

  //Move through the rows from grid Manufactured to check the type code "M"
  for (var i = 0; i < gridPanelManufactured["Rows"]["Count"]; i++) {
    gridPanelManufactured["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeM)["Activate"]()
    var type = gridPanelManufactured["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeM)["Text"]["OleValue"]
    flag = true
    if (type == "M" ) {
      flag = true
    }else{
      flag = false
      break
    }
  }

  if(flag){
    Log["Message"]("Grid displays only records with type code " + type)
  }else{
    Log["Error"]("Grid doesn't display only records with type code " + type)
  }

  //Move through the rows from grid Manufactured to check the type code "P"
  for (var i = 0; i < gridPanelPurchased["Rows"]["Count"]; i++) {
    gridPanelPurchased["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeP)["Activate"]()
    var type = gridPanelPurchased["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeP)["Text"]["OleValue"]
    flag = true
    if (type == "P" )  {
      flag = true
    }else{
      flag = false
      break
    }
  }

  if(flag){
    Log["Message"]("Grid displays only records with type code " + type)
  }else{
    Log["Error"]("Grid doesn't display only records with type code " + type)
  }
  
  //Move through the rows from grid Manufactured to check the type code "K"
  for (var i = 0; i < gridPanelSales["Rows"]["Count"]; i++) {
    gridPanelSales["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeK)["Activate"]()
    var type = gridPanelSales["Rows"]["Item"](i)["Cells"]["Item"](columnTypeCodeK)["Text"]["OleValue"]
    if (type == "K" )  {
      flag = true
    }else{
      flag = false
      break
    }
  }

  if(flag){
    Log["Message"]("Grid displays only records with type code " + type)
  }else{
    Log["Error"]("Grid doesn't display only records with type code " + type)
  }

  Log["Message"]("Step 76")

  var columnPartSales = getColumn(gridPanelSales, "Part") 
  var columnDescPart = getColumn(gridPanelSales, "Description") 

  gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnPartSales)["Activate"]()

  var epiTextBox = trackerMainPanel[2]["FindChild"]("FullName", "*epiTextBox1", 1);
  var txtPart_PartNum = trackerMainPanel[2]["FindChild"]("FullName", "*txtPart_PartNum", 1);

  if(gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnPartSales)["Text"]["OleValue"] == txtPart_PartNum["Text"]["OleValue"]
      && gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnDescPart)["Text"]["OleValue"] == epiTextBox["Text"]["OleValue"]){
    Log["Message"]("Tracker View Part field displays the selected row data from the grid")
  }else{
    Log["Error"]("Tracker View Part field doesn't display the selected row data from the grid")
  }

  Log["Message"]("Step 77")

  gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnPartSales)["Activate"]()
  var oldEpiTextBoxValue = epiTextBox["Text"]["OleValue"]
  Log["Message"]("Step 78")

  epiTextBox["Keys"]("test1" + '[Tab]') 

  if(gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnDescPart)["Text"]["OleValue"] != oldEpiTextBoxValue 
    && gridPanelSales["Rows"]["Item"](0)["Cells"]["Item"](columnDescPart)["Text"]["OleValue"]  == epiTextBox["Text"]["OleValue"]){
    Log["Checkpoint"]("Description was updated from tracker view field")
  }else{
    Log["Error"]("Description was not updated from tracker view field")
  }
}

function testQuery3_Query4(){
  ClickMenu("Refresh All", "", true)

  var trackerMainPanel = RetrieveTrackerMainPanel()
  var gridPanel = GetGridMainPanel("zAPInvDtl")

  Log["Message"]("Step 80")

  if (gridPanel["Rows"]["Count"] > 0) {
    Log["Message"]("Grid " + gridPanel["WndCaption"] + " retrieved " + gridPanel["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid " + gridPanel["WndCaption"] + " retrieved " + gridPanel["Rows"]["Count"] + " records.")
  }

  Log["Message"]("Step 81")
  var partComboTracker = trackerMainPanel[3]["FindChild"]("FullName", "*epiCombo1", 1);
  partComboTracker["setFocus"]()
  partComboTracker["Click"]()

  while(true){
    partComboTracker["Keys"]("[Down]")
    if (partComboTracker["Value"] == "0LP3A") {
      partComboTracker["Keys"]("[Tab]")
      break
    }
  }

  Log["Message"]("Step 82")
  var columnInvoice = getColumn(gridPanel, "Invoice") 
  var columnPart = getColumn(gridPanel, "Part")

  var realValuePartNum = gridPanel["Rows"]["Item"](0)["Cells"]["Item"](columnPart)["Text"]["OleValue"]

  gridPanel["Rows"]["Item"](0)["Cells"]["Item"](columnInvoice)["Activate"]()

  if(gridPanel["Rows"]["Item"](0)["Cells"]["Item"](columnPart)["Text"]["OleValue"] == realValuePartNum){
    Log["Message"]("Invoice was not updated with selected value from epicombo")
  }else{
    Log["Error"]("Invoice was updated with selected value from epicombo")
  } 

  Log["Message"]("Step 83")
  
  ClickMenu("Edit->Refresh")

  var flag = true
  for (var i = 0; i < gridPanel["Rows"]["Count"] ; i++) {
    gridPanel["Rows"]["Item"](i)["Cells"]["Item"](columnPart)["Activate"]()

    if( gridPanel["Rows"]["Item"](i)["Cells"]["Item"](columnPart)["Text"]["OleValue"] != partComboTracker["Value"]){
      flag = false
      break
    }else{
      flag = true
    }
  }

  if (flag) {
    Log["Message"]("Grid retrieved records only for part number " +  partComboTracker["Value"])
  }else{
    Log["Error"]("Grid didn't retrieve records only for part number " +  partComboTracker["Value"])
  } 

  // BAQA Country combo visibility
  var combo_Country = FindObject("*Combo*", "Name", "*cboCustomer_Country*", trackerMainPanel[4])

  if(!combo_Country["Exists"]){
    Log["Message"]("Combo is not visible")
  } else{
    Log["Error"]("Combo is visible. Visible behavior not modified.")
  } 

  ClickMenu("File->Exit")
}
