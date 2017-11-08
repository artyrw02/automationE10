//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions

function TC_Dashboard_Updatable_Customized_Form(){}
  
  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashCust",
    "menuName" : "DashCustm",
    "orderSequence" : 5,
    "menuType" : "Dashboard-Assembly",
    "dll" : "TestDashBDCustom"
  }  

  // Variables
  var company1 = "Epicor Education"
  var plant1 = "Main"

  //Used to navigate thru the Main tree panel
  var treeMainPanel1 = setCompanyMainTree(company1,plant1)

  var baqID = "TestBAQ"
  var dashbID = "TestDashBDCustom"

function CreateBAQ1(){

  Log["Message"]("Step 2")

  // Move to EPIC06 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  // Enter the following in the "General" tab
  
  // QueryID: baqID
  // Description: baqID
  // Shared: Checked | Updatable: Checked
  CreateBAQ(baqID, baqID, "chkShared,chkUpdatable")
  // drag and drop the "Customer" table design area in "Phrase Build" tab
  AddTableBAQ("Erp.Customer", "Customer")
  // In the Display Fields> Column Select tab for the "Customer" table select the Company, CustID, CustNum, Name and Address1 columns and add them to "Display Columns" area
  AddColumnsBAQ("Customer", "Company,CustID,CustNum,Name,Address1")

  // Move to Update> General Properties tab and check "Customer_Name" and "Customer_Address1" column as updatable
  UpdateTabBAQ("Customer_Name", "Updatable")
  UpdateTabBAQ("Customer_Address1", "Updatable")

  // Move to Update processing, click BPM Update, then click the "Business Object..." button and select the "ERP.Customer" then click "Ok" button from the "Select Business Object" window
  OpenPanelTab("Update->Update Processing")
  // BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()

  // Select Erp.Customer business object
  // BAQFormDefinition["dockableWindow3"]["updatePanel1"]["windowDockingArea1"]["dockableWindow1"]["updProcess1"]["epiGroupBox1"]["updBOSettings1"]["windowDockingArea1"]["dockableWindow1"]["updBOInfo1"]["btnBusObj"]["Click"]()
  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")
  listBOs["ClickItem"]("Erp.Customer")
  ClickButton("OK")
  // Aliases["Epicor"]["GuessBOForm"]["btnOK"]["Click"]()

  // Save the  BAQ
  SaveBAQ()
  ExitBAQ()
  Log["Message"]("BAQ " + baqID +" created")  
}

function CreateDashboard(){
  Log["Message"]("Step 3")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
 
  Log["Message"]("Dashboard opened")

  DevMode()
  Log["Message"]("DevMode activated")

  Log["Message"]("Step 4")
  NewDashboard(dashbID,dashbID,dashbID)
}

function AddQueryDashboard(){
  Log["Message"]("Step 5") 
  Delay(2000)
  AddQueriesDashboard(baqID)
  
  SaveDashboard()
  Log["Message"]("Dashboard with " + baqID +" created")

  E10["Refresh"]()
  var dashboardTree =  GetTreePanel("DashboardTree")

  Log["Message"]("Step 6")
  
  ClickPopupMenu("Queries|" + baqID + ": " + baqID, "Properties")

  if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
    Log["Message"]("Dashboard properties dialog appears")
  }

  Log["Message"]("Step 7")

  DashboardPropertiesTabs("Filter")

  var ultraGrid = GetGrid("ultraGrid1")

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

  Log["Message"]("Step 8") 

  ComboboxSelect("cboDisplayColumn","CustNum")

  Log["Message"]("CustNum Added on Display column dropdown")

  var dropdownColumns = GetList("lstAvailable")
  dropdownColumns["ClickItem"]("CustNum")
  ClickButton("","btnSelect")

  CheckboxState("chkPrimaryBrowse", true)

  ClickButton("OK")
  Log["Message"]("Dashboard Browse Properties closed")   

  ClickButton("OK")
  Log["Message"]("Dashboard Query Properties closed")

  Log["Message"]("Step 9")

  ClickPopupMenu("Queries|" + baqID + ": " + baqID + "|"+ baqID + ": Summary", "Properties")

  if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
    Log["Message"]("Dashboard properties dialog appears")
  }

  Log["Message"]("Step 10")

  //Check the Updatable check box
  CheckboxState("chkUpdatable", "true")

  Log["Message"]("Step 11")
  var dashboardGrid = GetGrid("ultraGrid1")
  
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

  ClickButton("OK")
  
  Log["Message"]("Step 12")
  SaveDashboard()

  DeployDashboard("Deploy Smart Client,Generate Web Form")

  ExitDashboard()
}

function CreateMenuDashb(){
  Log["Message"]("Step 13")

  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)  
} 

function RestartE10(){
  Log["Message"]("Step 14")

  Delay(1000)
  RestartSmartClient()

  Log["Message"]("SmartClient Restarted")
}

function TestDashb(){

  Log["Message"]("Step 15")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  ClickMenu("Edit->Refresh")

  var grid = GetGridMainPanel(baqID)

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
  newValue1 = grid["Rows"]["Item"](0)["Cells"]["Item"](addressColumn)["EditorResolved"]["SelectedText"] = "Test2"

  //Activates row 1
  grid["Rows"]["Item"](1)["Cells"]["Item"](nameColumn)["Click"]()

  if(oldValue1 != newValue1){
    Log["Checkpoint"]("Data was modified.")
    grid["Rows"]["Item"](0)["Cells"]["Item"](nameColumn)["Click"]()
    newValue1 = grid["Rows"]["Item"](0)["Cells"]["Item"](addressColumn)["EditorResolved"]["SelectedText"] = oldValue1
    
    grid["Rows"]["Item"](1)["Cells"]["Item"](nameColumn)["Click"]()
    Log["Message"]("Data restablished")
    
  }else{
    Log["Error"]("Data was not modified.")
  }

  ClickMenu("File->Save")
  Log["Message"]("Data was saved.")
  ClickMenu("File->Exit")
}
  
function AddCustomizationDashb(){

    Log["Message"]("Step 17")
    ActivateMainDevMode()

    MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Customer")
    
    E10["Refresh"]()

    Delay(2500)
    CheckboxState("chkBaseOnly", true)

    ClickButton("OK")

    Delay(2500)
    E10["Refresh"]()
    ClickMenu("Tools->Customization")

    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    ClickButton("New Custom Sheet")

    var dockableSheetsList = GetList("lstStandardSheets")

    dockableSheetsList["ClickItem"]("customerDock1")

    Log["Message"]("customerDock1 sheet selected")

    EnterText("txtSheetName", "test")
    EnterText("txtSheetText", "test")
    EnterText("txtSheetTextTab", "test")

    ClickButton("Add Dashboard...")
    
    var radiobtn = GetRadioBtn("radAppBuilderPanel")
    radiobtn["Click"]()
    EnterText("txtDashboardID", "DashCust")

    CheckboxState("chkDisplayStatusBar", true)

    ClickButton("Next>")

    var radiobtn = GetRadioBtn("radRetrieveWButton")
    radiobtn["Click"]()

    ClickButton("Next>")

    var dataViewList = GetList("lstDataViews")
    dataViewList["ClickItem"]("Customer")

    var dataColumnList = GetList("lstDataColumns")
    dataColumnList["ClickItem"]("CustNum")

    ClickButton("Add Subscribe Column")

    ClickButton("Finish")

    ClickButton("", "btnAddCustomSheet")
    Log["Message"]("Sheet was added to Custom Sheets.")

    ClickMenu("File->Save Customization As ...")
    EnterText("txtKey1a","Automation test")
    EnterText("txtDescription", "Automation test")
    ClickButton("Save")
    ClickButton("OK")

    Log["Message"]("Customization saved.")
    ClickMenu("File->Close")
    ClickMenu("File->Exit") 
    Log["Message"]("Customization closed.")   
}

function OpenCustomizedForm() {
  Log["Message"]("Step 18")

  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Customer")
  E10["Refresh"]()

  var availableLayers = GetTreePanel("AvailableLayers")
  availableLayers["ClickItem"]("Base|EP|Customizations|Automation_test")

  ClickButton("OK")
}

function TestCustomizedForm(){
  Delay(2500)
  OpenPanelTab("Detail")

  EnterText("txtKeyField", "Addison" + "[Tab]")
  Log["Message"]("Addison customer was retrived")

  OpenPanelTab("test")

  Log["Message"]("Step 19")

  ClickButton("Retrieve")

  var grid = GetGridMainPanel(baqID)

  if(grid["wRowCount"] > 0){
    Log["Checkpoint"]("Dashboard retrived data for Addison Customer")
  }else {
    Log["Error"]("Dashboard didn't retrive data for Addison Customer")
  }
 
  Log["Message"]("Step 20")

  OpenPanelTab("Detail") 
  EnterText("txtKeyField", "Dalton" + "[Tab]")

  Log["Message"]("Dalton customer was retrived")
  OpenPanelTab("test")
  ClickButton("Retrieve")

  var grid = GetGridMainPanel(baqID)

  if(grid["wRowCount"] > 0){
    Log["Checkpoint"]("Dashboard retrived data for Dalton Customer")
  }else {
    Log["Error"]("Dashboard didn't retrive data for Dalton Customer")
  }

  Delay(2500)

  ClickMenu("File->Exit")
  DeactivateMainDevMode()
}
 


