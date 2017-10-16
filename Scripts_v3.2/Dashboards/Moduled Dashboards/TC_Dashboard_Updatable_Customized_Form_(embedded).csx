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

function CreateBAQ1(){

  Log["Message"]("Step 2")

  // Move to EPIC06 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  // Enter the following in the "General" tab
  var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
  
  // QueryID: baqID
  // Description: baqID
  // Shared: Checked | Updatable: Checked
  CreateBAQ(baqID, baqID, "chkShared,chkUpdatable")
  // drag and drop the "Customer" table design area in "Phrase Build" tab
  AddTableBAQ(BAQFormDefinition, "Customer")
  // In the Display Fields> Column Select tab for the "Customer" table select the Company, CustID, CustNum, Name and Address1 columns and add them to "Display Columns" area
  AddColumnsBAQ(BAQFormDefinition, "Customer", "Company,CustID,CustNum,Name,Address1")

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
  //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
 
  Log["Message"]("Dashboard opened")

  //Enable Dashboard Developer Mode  
  DevMode()
  Log["Message"]("DevMode activated")

  Log["Message"]("Step 4")
  NewDashboard("TestDashBDCustom","TestDashBDCustom","TestDashBDCustom")

}

function AddQueryDashboard(){

    Log["Message"]("Step 5") 
    Delay(2000)
    AddQueriesDashboard(baqID)
    
    SaveDashboard()
    Log["Message"]("Dashboard with " + baqID +" created")

    var dashboardTree =  GetTreePanel("DashboardTree")

    Log["Message"]("Step 6")
    // var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
    // dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)

    dashboardTree["ClickItem"]("Queries|" + baqID + ": " + baqID)
    ClickMenu("Edit->Properties")
    Log["Message"]("BAQ - right click")

    // click 'Properties' option from menu
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    Log["Message"]("BAQ1 Summary - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    Log["Message"]("Step 7")

    // Go to Filter tab and add the condition: Customer_CustNum = Dashboard Browse        
    DashboardPropertiesTabs("Filter")

    // var ultraGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")
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

    //On Dashboard Browse Properties on Display column dropdown select CustNum 
    ComboboxSelect("cboDisplayColumn","CustNum")

    // Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["SetFocus"]()
    // while(Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["Text"] != "CustNum"){
    //   Aliases["Epicor"]["QueryNavProperties"]["cboDisplayColumn"]["Keys"]("[Down]")
    // }
    Log["Message"]("CustNum Added on Display column dropdown")

    // var dropdownColumns = Aliases["Epicor"]["QueryNavProperties"]["grpMediaProps"]["lstAvailable"]
    var dropdownColumns = GetList("lstAvailable")
    dropdownColumns["ClickItem"]("CustNum")
    ClickButton("","btnSelect")

    CheckboxState("chkPrimaryBrowse", true)

    //add CustNum on Drop Down Columns and check Primary Browse option. Click Ok to both dialogs
    // for(var i = 0; i < dropdownColumns["Items"]["Count"]; i++){
    //   if (dropdownColumns["Items"]["Item_2"](i)["Text"]["OleValue"] == "CustNum") {
    //     dropdownColumns["ClickItem"]("CustNum")
    //     Aliases["Epicor"]["QueryNavProperties"]["grpMediaProps"]["WinFormsObject"]("btnSelect")["Click"]()
    //     Aliases["Epicor"]["QueryNavProperties"]["chkPrimaryBrowse"]["Checked"] = true
    //   }
    // }

    //Close Dashboard Browse Properties 
    // Aliases["Epicor"]["QueryNavProperties"]["WinFormsObject"]("btnOkay")["Click"]()
    ClickButton("OK")
    Log["Message"]("Dashboard Browse Properties closed")   

    //Close Dashboard Query Properties 
    // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
    ClickButton("OK")
    Log["Message"]("Dashboard Query Properties closed")

    Log["Message"]("Step 9")
    dashboardTree["ClickItem"]("Queries|" + baqID + ": " + baqID + "|"+ baqID + ": Summary")
    ClickMenu("Edit->Properties")
    Log["Message"]("BAQ1 Summary - Properties was selected from Menu")

    // var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
    // dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
    // Log["Message"]("BAQ grid - right click")

    // click 'Properties' option from menu
    // Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    // Log["Message"]("BAQ1 Summary - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    Log["Message"]("Step 10")

    //Check the Updatable check box
    CheckboxState("chkUpdatable", "true")


    Log["Message"]("Step 11")
    // Check the ""prompt"" option for ""Name"" and ""Address"" field 
    // var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
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

    //Click Ok to close Properties
    // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
    ClickButton("OK")
    
    Log["Message"]("Step 12")
    // Save the dashboard
    SaveDashboard()

    // Tools>Deploy Dashboard. Select Deploy Smart Client Application  check box. Click on Deploy button and when finished click Ok.        
    DeployDashboard("Deploy Smart Client,Generate Web Form")

    ExitDashboard()
}


function CreateMenuDashb(){
  Log["Message"]("Step 13")
  // In Menu Maintenance tree select Main Menu>Sales Management>Customer Relationship Management > Setup, 
  // then Select File> New>New Menu
  // Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
  // in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. 
  // Be sure the Enabled check box is selected. Click Save.

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

  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
  ClickMenu("Edit->Refresh")

  // var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  // var grid = dashboardMainPanel["FindChild"]("FullName", "*grid*", 15);
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


  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Save")
  ClickMenu("File->Save")
  Log["Message"]("Data was saved.")
  ClickMenu("File->Exit")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

}
  
function AddCustomizationDashb(){

    Log["Message"]("Step 17")
    //Validate if it's on dev mode already

    //Developer mode 
    // Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")
    ClickMenu("Options->Developer Mode")

    // Open Sales Management > Customer Relationship Management > Setup > Customer(select Base only option and click OK in the ""Select Customization"" dialog)
    MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Customer")
    
    Delay(2500)
    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["grpNoLayer"]["chkBaseOnly"]["Checked"] = true
    CheckboxState("chkBaseOnly", true)

    ClickButton("OK")
    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    Delay(2500)
    // click Tools > Customization
    // Aliases["Epicor"]["CustomerEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Customization")
    ClickMenu("Tools->Customization")

    // go to Wizards > Sheet Wizard  tab
    // var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    // CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    // click ""New Custom Sheet""
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()

    ClickButton("New Custom Sheet")

    // Select Dockable Sheets (choose Parent Docking Sheet) as customerDock1
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"]("customerDock1")
    var dockableSheetsList = GetList("lstStandardSheets")

    dockableSheetsList["ClickItem"]("customerDock1")

    Log["Message"]("customerDock1 sheet selected")

    // Name, text, tab text = ""TEST""
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("test")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("test")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("test") 
    EnterText("txtSheetName", "test")
    EnterText("txtSheetText", "test")
    EnterText("txtSheetTextTab", "test")

    // Click Add Dashboard Button
    // Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddDashboard"]["Click"]()
    ClickButton("Add Dashboard...")
    
    // Choose AppBuilder Panel option and type or search and retrieve for the the Menu ID created above
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["radAppBuilderPanel"]["ultraOptionSet1"]["Click"]()
    var radiobtn = GetRadioBtn("radAppBuilderPanel")
    radiobtn["Click"]()
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["txtDashboardID"]["Keys"]("DashCust")
    EnterText("txtDashboardID", "DashCust")

    // Tick only ""display dashboard status bar"" option
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["chkDisplayStatusBar"]["Checked"] = true
    CheckboxState("chkDisplayStatusBar", true)

    // click ""Next"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()
    ClickButton("Next>")

    // Select Subscribe to UI data (include retrieve button)
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep2"]["radRetrieveWButton"]["ultraOptionSet1"]["Click"]()
    var radiobtn = GetRadioBtn("radRetrieveWButton")
    radiobtn["Click"]()

    // click ""Next"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()
    ClickButton("Next>")

    // Choose ""Customer"" data view
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataViews"]["ClickItem"]("Customer")
    var dataViewList = GetList("lstDataViews")
    dataViewList["ClickItem"]("Customer")
    // Choose ""CustNum"" column
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataColumns"]["ClickItem"]("CustNum")
    var dataColumnList = GetList("lstDataColumns")
    dataColumnList["ClickItem"]("CustNum")

    // click ""Add Subscribe column"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["btnAddSubscribeColumn"]["Click"]()
    ClickButton("Add Subscribe Column")
    // click ""Finish"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["WinFormsObject"]("btnFinish")["Click"]()
    ClickButton("Finish")
    // Press Right arrow to move tab to ""Custom Sheets"" panel
    // Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
    ClickButton("", "btnAddCustomSheet")
    Log["Message"]("Sheet was added to Custom Sheets.")
    // Click File> Save customization as  and give it a name and Description and click ""Save"" button (Add a comment if you wish then click last ""OK"" button)
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization")
    ClickMenu("File->Save Customization As ...")
    EnterText("txtKey1a","Automation test")
    EnterText("txtDescription", "Automation test")
    ClickButton("Save")
    ClickButton("OK")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtKey1a")["Keys"]("Automation test")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtDescription")["Keys"]("Automation test")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("btnOk")["Click"]()
    // Aliases["Epicor"]["WinFormsObject"]("CustomCommentDialog")["WinFormsObject"]("btnOK")["Click"]()

    Log["Message"]("Customization saved.")
    // close the customization 
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
    ClickMenu("File->Close")
    // Close the form"  
    // Aliases["Epicor"]["CustomerEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")   
    ClickMenu("File->Exit") 
    Log["Message"]("Customization closed.")   

    
}

function OpenCustomizedForm() {
  Log["Message"]("Step 18")
  // Go to Sales Management> Customer Relationship Management> Setup> Customer and select the customization from previous step
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Customer")
  /*(FUTURE REFERENCE FOR TREE LIST ITEMS)*/
  // Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["etvAvailableLayers"]["ClickItem"]("Base|EP|Customizations|Automation_test")
  var availableLayers = GetTreePanel("AvailableLayers")
  availableLayers["ClickItem"]("Base|EP|Customizations|Automation_test")
  // Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()
  ClickButton("OK")
}

function TestCustomizedForm(){
  Delay(1500)
  OpenPanelTab("Detail")

  // In the Customer> Detail tab retrieve ""Addison""
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Addison")
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

  EnterText("txtKeyField", "Addison" + "[Tab]")
  Log["Message"]("Addison customer was retrived")
  // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();
  OpenPanelTab("test")

  Log["Message"]("Step 19")
  // var retrieveBtn = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*Retrieve*", "*EpiButton*"], 30)
  // retrieveBtn["Click"]()
  ClickButton("Retrieve")

  // var grid = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*baqID: Summary*", "*Grid*"], 30)
  var grid = GetGridMainPanel(baqID)

  if(grid["wRowCount"] > 0){
    Log["Checkpoint"]("Dashboard retrived data for Addison Customer")
  }else {
    Log["Error"]("Dashboard didn't retrive data for Addison Customer")
  }
 
  Log["Message"]("Step 20")
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Dalton")
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")
  OpenPanelTab("Detail") 
  EnterText("txtKeyField", "Dalton" + "[Tab]")

  Log["Message"]("Dalton customer was retrived")
  // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
  // Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();
  OpenPanelTab("test")
  ClickButton("Retrieve")

    // var grid = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*baqID: Summary*", "*Grid*"], 30)
  var grid = GetGridMainPanel(baqID)

  if(grid["wRowCount"] > 0){
    Log["Checkpoint"]("Dashboard retrived data for Dalton Customer")
  }else {
    Log["Error"]("Dashboard didn't retrive data for Dalton Customer")
  }

  // deactivate dev mode
  // Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")  
  ClickMenu("Options->Developer Mode")  

}
 


