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
  Log["Message"]("Step 2")
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  Log["Message"]("Dashboard opened")
  
  DevMode()
  Log["Message"]("DevMode activated")
 
  Log["Message"]("Step 3")

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
  }else{
    Log["Error"]("BAQ Import dialog is not displayed")
  }

  Delay(25000)

  OpenPanelTab("General")

  var txtDefinitonID = GetText("txtDefinitonID")
  if(txtDefinitonID != ""){
    Log["Message"]("Dashboard Imported")
  }else{
    Log["Error"]("Dashboard wasn't imported")
  }

  Log["Message"]("Step 4")    
  SaveDashboard()
  ExitDashboard()
}

function RegenerateImportedBAQ(){
  Log["Message"]("Step 5")      
  MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Updatable BAQ Maintenance")
  
  Log["Message"]("Step 6") 
  ClickButton("Query ID...")
  EnterText("txtStartWith", baqE9)

  CheckboxState("chkShared", "Indeterminate")
  ClickButton("Search")

  var searchGrid = GetGrid("Search")
  var queryID = getColumn(searchGrid, "Query ID")

  for(var i = 0; i < searchGrid["Rows"]["Count"];i++){
    if(searchGrid["Rows"]["Item"](i)["Cells"]["Item"](queryID)["Text"]["OleValue"] == baqE9){
      searchGrid["Rows"]["Item"](i)["Activate"]()
      ClickButton("OK")
    }
  }

  //Used to activate query
  var treeView = GetTreePanel("treeView")
  treeView["ClickItem"]("Updatable Queries|" + baqE9)

  Log["Message"]("Step 7") 
  ClickMenu("Actions->Regenerate Selected")
  Delay(1500)

  Log["Message"]("BAQ regenerated")  
  ClickMenu("File->Exit")
}

function OpenImportedDashb(){
  
  Log["Message"]("Step 8")   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  OpenDashboard(dashboardID)
  Log["Message"]("Dashboard retrived")

  ClickPopupMenu("Queries|" + baqE9 + ": TestBAQ Caro" + "|" + baqE9 + ": Summary", "Properties")

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

  ClickButton("OK")
  Log["Checkpoint"]("Grid validated correctly.")

  Log["Message"]("Step 9")   

  ClickPopupMenu("Queries|" + baqE9 + ": TestBAQ Caro" + "|" + baqE9 + ": Tracker", "Properties")

  if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
    Log["Message"]("Dashboard properties dialog appears")
  }

  // Validates data of dashboard
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

  ClickButton("OK")
}

function AddTrackerView(){
  Log["Message"]("Step 10")     

  var zPOLine = "zPOLine" 
  AddQueriesDashboard(zPOLine)

  Log["Message"]("Step 12")

  ClickPopupMenu("Queries|" + zPOLine + ": " + zPOLine, "New Tracker View")

 // Visible & Prompt for PartNum and Description
  var dashboardTrackerView = GetGrid("ultraGrid1")
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
  
  CheckboxState("chkInputPrompts", true)
  
  ClickButton("OK")

  Log["Message"]("Step 13")        
  DeployDashboard("Deploy Smart Client")

  SaveDashboard()
  ExitDashboard()
}   

function CreateMenu1(){
  Log["Message"]("Step 14")  
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

  CreateMenu(MenuData)
}

function RestartE10(){
  Log["Message"]("Step 15")  
  Delay(1000)
  RestartSmartClient()
  Log["Checkpoint"]("SmartClient Restarted")
}

function TestMenu(){
  Log["Message"]("Step 16")    
  MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  var gridsMainPanel = RetrieveGridsMainPanel()
  var trackerMainPanel = RetrieveTrackerMainPanel()

  gridsMainPanel[0]["Click"]()

  Log["Message"]("Step 17")
  ClickMenu("Edit->Refresh")
  
  if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
    Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
  }else{
    Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
  }

  Log["Message"]("Step 18")
  var custIDField = GetTextBox("*CustID", trackerMainPanel[0])

  custIDField["Keys"]("E")

  ClickMenu("Edit->Refresh")

  var columnCustID = getColumn(gridsMainPanel[0], "Cust. ID") 

  findValueInStringGrids(gridsMainPanel[0], columnCustID, "E")

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

  Log["Message"]("Step 20")     
  gridsMainPanel[1]["Click"]()
  ClickMenu("Edit->Refresh")

  // 21- On Part field from tracker view enter "*00*" and click Refresh       
  Log["Message"]("Step 21")
  var partField = GetTextBox("*Part*", trackerMainPanel[1])

  partField["keys"]("*00*")
  ClickMenu("Edit->Refresh")
  
  var columnPart = getColumn(gridsMainPanel[1], "Part Num") 

  findValueInStringGrids(gridsMainPanel[1], columnPart, "00")

  // 22- On Part field from tracker view enter "0*" and click Refresh       
  Log["Message"]("Step 22")

  partField["keys"]("^a[Del]"+"0*")
  ClickMenu("Edit->Refresh")

  var columnPart = getColumn(gridsMainPanel[1], "Part Num") 

  findValueInStringGrids(gridsMainPanel[1], columnPart, "0")

  // 23- On Description field from tracker view enter "CRS ROUND 1.00" BAR STOCK" and click Refresh        
  var descriptionField = GetTextBox("*Desc*", trackerMainPanel[1])

  var columnDescription = getColumn(gridsMainPanel[1], "Description") 

  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1.00")

  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.00")

  // 24- On Description field from tracker view enter "CRS ROUND 1.50" BAR STOCK" and click Refresh  
  Log["Message"]("Step 24")      
  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1.50")

  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.50")
  
  // 25- On Description field from tracker view enter "CRS ROUND 1..0" BAR STOCK" and click Refresh        
  Log["Message"]("Step 25")
  partField["keys"]("^a[Del]")
  descriptionField["keys"]("CRS ROUND 1..0")

  ClickMenu("Edit->Refresh")

  findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.")

  ClickMenu("File->Exit")

  if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
    ClickButton("No")
  }
}
