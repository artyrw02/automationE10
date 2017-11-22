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
  Log["Message"]("Step 3")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  DevMode()
  Log["Message"]("DevMode activated")

  Log["Message"]("Step 4")
  NewDashboard(dashb1, dashb1, dashb1, "chkInhibitRefreshAll")
}

function AddQuery1Dashb(){
  Log["Message"]("Step 5")
  
  Delay(2000)
  AddQueriesDashboard(baq1Copy, baq1Copy + " 0")
}  

function AddTrackerView1Query1(){
  Log["Message"]("Step 6")

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 0", "New Tracker View")
   
  // Sets an alias for the tracker view
  EnterText("txtCaption", baq1Copy + ": Tracker" + " 0") 
  
  Log["Message"]("Step 7")      
  var trackerViewPropsPanel = VerifyForm("Dashboard Tracker View Properties")
  if (trackerViewPropsPanel) {
    ClickButton("Clear All")
  }

  Log["Message"]("Step 8")
  Delay(2000)
  ClickButton("OK")

  SaveDashboard()

  E10["Refresh"]()
  
  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 0" + "|" + baq1Copy + ": Tracker 0", "Properties")

  Log["Message"]("Step 10")    
  var trackerViewPropsPanel = VerifyForm("Dashboard Tracker View Properties")
  if (trackerViewPropsPanel) {
    ClickButton("Select All")
  }
  
  Log["Message"]("Step 11")
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

  Log["Message"]("Step 12")
  ClickButton("OK")

  SaveDashboard()
}

function AddTrackerView2Query1(){
  Delay(2500)
  
  E10["Refresh"]()

  Log["Message"]("Step 15, 16")   
  
  Delay(2500)

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 0", "New Tracker View")
  // Sets an alias for the tracker view
  EnterText("txtCaption", baq1Copy + ": Tracker" + " 1") 

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

  Log["Message"]("Step 17")
  ClickButton("OK")

  SaveDashboard()
}
    
function AddQuery2Dashb(){
  Log["Message"]("Step 20")
  Delay(2000)
  AddQueriesDashboard(baq1Copy, baq1Copy + " 1")
}

function AddTrackerView1Query2(){

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 1", "New Tracker View")
  // Sets an alias for the tracker view
  EnterText("txtCaption", baq1Copy + ": Tracker" + " 0") 

  Log["Message"]("Step 22")   
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

  Log["Message"]("Step 23")
  ClickButton("OK")

  SaveDashboard()
  
  Delay(2500)
  DeployDashboard("Deploy Smart Client,Add Favorite Item")

  ExitDashboard()
}

function CreateMenu1() {
  Log["Message"]("Step 35")
  Delay(1500)
  E10["Refresh"]()
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)
}

function RestartE10() {
  Log["Message"]("Step 36")
  Delay(1000)
  RestartSmartClient()
  Log["Message"]("SmartClient Restarted")
}

//Used to test tracker view before customizing
function OpenMenuTestDashbTracker3() {
  Log["Message"]("Step 37")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  // Step38- Refresh Data
  Log["Message"]("Testing tracker view for second query")
  ClickMenu("Refresh All", "", true)

  // Test data from menu
  Log["Message"]("Step 39")
  testingDashboard("tracker2")
  ClickMenu("File->Exit")
}  

function OpenDashb() {
  Log["Message"]("Step 24")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  
  OpenDashboard(dashb1)

  var form = GetForm(dashb1)
  form["Maximize"]()
}

function CustomizeTrackerView(){
  Log["Message"]("Step 25")
  Delay(2000)

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 1" + "|"+ baq1Copy + ": Tracker" + " 0", "Customize Tracker View")

  Delay(2500)
  Log["Message"]("Step 26")     
  var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
  //Wizards
  CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
  //Sheet Wizard
  CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true
  
  if (CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["Exists"]) {
    ClickButton("New Custom Sheet")

    // select the available parent docking sheet.
    var dockableSheetsList = GetList("lstStandardSheets")
    dockableSheetsList["ClickItem"](0)

    //Step27- Add a Name, Text and Tab Text for the new sheet.
    Log["Message"]("Step 27")
    var customData = "testDashb"
    EnterText("txtSheetName", customData)
    EnterText("txtSheetText", customData)
    EnterText("txtSheetTextTab", customData)

    // Click on the arrow pointing to the right       
    ClickButton("", "btnAddCustomSheet")
    
    // Select the newly added tab on your tracker
    var custSheetsList = GetList("lstCustomSheets")
    custSheetsList["ClickItem"](0)
    
    ActivateForm(dashb1)

    OpenPanelTab(customData, dashb1)

    var wnd = GetForm(dashb1)
    
    ActivateForm("Customization Tool")
    
    Log["Message"]("Step 28")
    ClickMenu("Tools->ToolBox")

    Delay(2000)

    //Step29- On your new tab drop a label, a text box, add a combo box and a date time editor. Then Save and close the customization window  
    var lvwItems = GetList("lvwItems")      
    lvwItems["ClickItemXY"]("EpiLabel", -1, 50, 10);
    
    epiBasePanel = FindObject("*BasePanel*", "Name", "*" + customData + "*", wnd)
    epiBasePanel["Click"](90, 35);
    
    ClickMenu("Tools->ToolBox")
    lvwItems["ClickItemXY"]("EpiTextBox", -1, 74, 11);
    epiBasePanel["Click"](190, 35);

    ClickMenu("Tools->ToolBox")
    lvwItems["ClickItemXY"]("EpiCombo", -1, 63, 8);
    epiBasePanel["Click"](290, 35);

    ClickMenu("Tools->ToolBox")
    lvwItems["ClickItemXY"]("EpiDateTimeEditor", -1, 89, 12);
    epiBasePanel["Click"](390, 35);

  }
  Log["Message"]("Step 29")
  ActivateForm("Customization Tool")
  ClickMenu("File->Save Customization")
  ClickMenu("File->Close")

  //Step30- Save dashboard
  Log["Message"]("Step 30")
  SaveDashboard()

  //Step31- Right Click on your tracker view and select the option Properties.        
  Log["Message"]("Step 31")

  ClickPopupMenu("Queries|" + baq1Copy + ": " + baq1Copy + " 1" + "|"+ baq1Copy + ": Tracker" + " 0", "Properties")

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

  ClickButton("OK")

  Log["Message"]("Step 33")
  SaveDashboard()
}

function DeployDashb(){
  Log["Message"]("Step 34")
  DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")
  
  Delay(2500)
  ExitDashboard()

  Log["Message"]("Dashboard created")
}

function E10CacheRestart() {
  Delay(2500)
  ClickMenu("Options->Clear Client Cache")

  ClickButton("Yes")

  RestartSmartClient()
  Log["Checkpoint"]("SmartClient Restarted")
}  

function OpenMenuTestDashb(){
  Log["Message"]("Step 37")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  // Step38- Refresh Data
  Log["Message"]("Step 38")
  ClickMenu("Refresh All", "", true)
  
  // Test data from menu
  Log["Message"]("Step 39")
  testingDashboard("tracker") 
  ClickMenu("File->Exit")  
}  

function AddNewQueryDashboard(){
  Log["Message"]("Step 46")
  //Navigate and open Dashboard
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  Log["Checkpoint"]("Dashboard opened") 

  // Retrieve the previous dashboard       
  EnterText("txtDefinitonID", dashb1 + "[Tab]")

  Delay(2500)
  OpenPanelTab("General")

  //Step47-  Add a New Query and select the zAttribute query.    
  Log["Message"]("Step 47")  
  Delay(2000)
  var query2 = "Attribute"
  AddQueriesDashboard("z"+query2)

  //Right click on the query summary and click on the Query   
  var dashboardTree = GetTreePanel("DashboardTree")
  ClickPopupMenu("Queries|z" + query2  + ": " + query2, "Properties")

  DashboardPropertiesTabs("General")

  //Check Auto Refresh on Load and change the Refresh Interval to 10  
  CheckboxState("chkAutoRefresh", true)
  EnterText("numRefresh", "[Del][Del][Del]10")
  ClickButton("OK")

  //Save dashboard
  SaveDashboard()

  Delay(2500)
  
  ClickPopupMenu("Queries|z" + query2  + ": " + query2, "New Tracker View")

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
      CheckboxState("chkInputPrompts", true)
    }
  }

  //Step50- Click Ok to close Properties
  Log["Message"]("Step 50")
  ClickButton("OK")

  SaveDashboard()
  Log["Message"]("Dashboard saved")
  
  //Step51- Deploy dashboard
  Log["Message"]("Step 51")
  DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")

  ExitDashboard() 
}
  
function E10CacheRestart2(){
  Delay(2500)
  ClickMenu("Options->Clear Client Cache")

  ClickButton("Yes")
  
  RestartSmartClient()
  Log["Checkpoint"]("SmartClient Restarted")  
}  


function CreateAttributesQuery(){
  ClickMenu("Options->Clear Client Cache")

  ClickButton("Yes")
  
  Log["Message"]("Step 53")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Attribute")

  // >Select New Attribute
  ClickMenu("File->New")

  // >Enter ISO9000 on attribute field and description
  EnterText("txtAttrCode", "ISO9000" + "[Tab]")
  EnterText("txtDesc","ISO9000" + "[Tab]")

  // >Save
  ClickMenu("File->Save")

  // Exit
  ClickMenu("File->Exit")
}

function TestAttributeData(){
  Log["Message"]("Step 54")
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  // validate if zAttribute (third query)  has records after opening dash menu and the record added ISO9000 is located in the records
  testingDashboard("Attribute") 
  ClickMenu("File->Exit")
}

function testingDashboard(typeTesting) {
  Delay(2500)
  ClickMenu("Refresh All", "", true)
    
  var gridDashboardPanelChildren = RetrieveGridsMainPanel()
  var trackerPDashboardChildren = RetrieveTrackerMainPanel()


  if (typeTesting == "tracker") {

    E10["Refresh"]()
    // Display the GroupCode combo box from the tracker views of added queries        

      //Get Children from the first two tracker Panels of the firsy Query
      var groupCodeTrackerP1 = trackerPDashboardChildren[0]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);
      var groupCodeTrackerP2 = trackerPDashboardChildren[1]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

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

      ClickMenu("Edit->Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
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

      ClickMenu("Edit->Clear")
      
      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
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
      // Change to the customized tab of your second tracker view   
      OpenPanelTab("testDashb")

      var trackerTestDashbPanelChildren = RetrieveTrackerMainPanel("testDashb")

      if (Aliases["Epicor"]["ExceptionDialog"]['Exists']) {
        Log["Error"]("There is an error on the tab created")
      } else {
        if (trackerTestDashbPanelChildren[0]["Controls"]["Count"] > 1) {
          Log["Message"]("Controllers are displayed")
        } else {
          Log["Error"]("Controllers are not displayed")
        }
      } 

    //--------------------------------------
  }else if (typeTesting == "tracker2"){
    var groupCodeTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*eucCustomer_GroupCode", 1);

      if (groupCodeTrackerP3["Exists"] == true) {
        Log["Checkpoint"]("Group code is displayed on the Tracker Views from first query")
      }else {
        Log["Error"]("One of the group code is not being displayed on the Tracker Views from first query")
      }
      // On the second query, on its tracker view enter A on CustID field and click Refresh 
      var custIDTrackerP3 = trackerPDashboardChildren[2]["FindChild"]("FullName", "*txtCustomer_CustID", 1);

      trackerPDashboardChildren[2]["Parent"]["Activate"]()
      custIDTrackerP3["Keys"]("A")
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
    }

  }else if(typeTesting == "Attribute"){
    E10["Refresh"]()
    Delay(1500)
    
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



