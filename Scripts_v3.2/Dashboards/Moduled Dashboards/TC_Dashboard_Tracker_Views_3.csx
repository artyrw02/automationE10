//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT Sys_Functions
//USEUNIT ControlFunctions
//USEUNIT Data_Dashboard_TrackerViews_3

function TC_Dashboard_Tracker_Views_3(){}

function ChangeRegionControlPanel1(){
  Log["Message"]("Step 1")
  //opens control cmd and opens control panel
  OpenCMD("control")
  
  ChangeRegionControlPanel("Estonian (Estonia)")

  CloseCMD()  
}
  
function ModifyCultureAccountMngmt(){
  Log["Message"]("Step 2 - log in")
  ExpandComp(company1)

  ChangePlant(plant1)

  Log["Message"]("Step 3")
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;User Account Security Maintenance")

  EnterText("txtKeyField", "epicor" + "[Tab]")
  var txtName = GetText("txtName")

  if (txtName != ""){
    Log["Checkpoint"]("Data loaded correctly")
  }else{
    Log["Error"]("Data is not loaded correctly")
  }

  var formatCulture = GetText("cmbFormatCulture")

  if(formatCulture == "Invariant Language (Invariant Country)"){
    Log["Message"]("Invariant culture is set for your user")
  }else{
    Log["Message"]("There was a problem. Invariant culture was not set for your user")
    EnterText("cmbFormatCulture", "Invariant Language (Invariant Country)" + "[Tab]")
    ClickMenu("File->Save")
    Log["Message"]("- Invariant culture is set for your user")
  }

  ClickMenu("File->Exit")
}

function CreateBAQ1(){
  Log["Message"]("Step 4")

  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  Log["Message"]("BAQ opened")

  CreateBAQ(baq, baq, "chkShared,chkUpdatable,chkAllCompanies")
  
  AddTableBAQ("Erp.Part", "Part")

  AddColumnsBAQ( "Part", "PartNum,NetWeight")

  UpdateTabBAQ("Part_NetWeight", "Updatable")

  OpenPanelTab("Update->Update Processing")

  ClickButton("Business Object...")

  var listBOs = GetList("lbBOs")
  listBOs["ClickItem"]("Erp.Part")

  ClickButton("OK")

  Log["Message"]("Business Object was selected")
  
  Delay(1500)
  SaveBAQ()
  Delay(5000)
  ExitBAQ()  
}

function CreateDashboard(){
  Log["Message"]("Step 5")

  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  Log["Message"]("Dashboard opened")

  Log["Message"]("Step 6")
  DevMode() 

  Log["Message"]("Step 7")
  NewDashboard(dashb, dashb, dashb, "chkAllCompanies")  
}
  
function AddQuery1Dashb1(){
  Log["Message"]("Step 8")
  AddQueriesDashboard(baq)    
}
      
function CreateTrackerViewQuery1(){
  Log["Message"]("Step 9")

  var dashboardTree =  GetTreePanel("DashboardTree")

  // Right click on BAQ name from the tree and select New Tracker View       
  ClickPopupMenu("Queries|" + baq + ": " + baq, "New Tracker View")

  // Step 10
  Log["Message"]("Step 10")

  var TrackerViewsGrid = GetGrid("ultraGrid1")
      
  var column = getColumn(TrackerViewsGrid, "Column")
  var columnPrompt = getColumn(TrackerViewsGrid, "Prompt")
  var columnVisible = getColumn(TrackerViewsGrid, "Visible")
  var columnCondition = getColumn(TrackerViewsGrid, "Condition")

  for (var i = 0; i <= TrackerViewsGrid["wRowCount"] - 1; i++) {
    //Select row and check Prompt checkbox
    var cell = TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

    if (cell["Text"] == "Part_PartNum") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
      // Uncheck Visible check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Unchecked"
    }
    if (cell["Text"] == "Part_NetWeight") {
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
      // Check Prompt check box on field
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked"

      //Activates 'Condition' column
      TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Click"]()

      while(true){
        TrackerViewsGrid["Keys"]("[Down]")
        if (TrackerViewsGrid["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["EditorResolved"]["SelectedText"] == "GreaterThanOrEqualTo") {
          break
        }
      }
    }
  }
  CheckboxState("chkInputPrompts", true)
  ClickButton("OK")
}

function DeployDashb(){
  Log["Message"]("Step 11")

  DeployDashboard("Deploy Smart Client,Add Favorite Item,Generate Web Form")
}    

function RestartE10(){
  Log["Message"]("Step 12, 13")
  RestartSmartClient()  
}

function TestDashboard(){
  Log["Message"]("Step 14")

  Log["Message"]("FavoritesMenuTab Activated")

  OpenDashboardFavMenu(dashb)
  Log["Message"]("Dashboard opened from Favorite Menu")

  Log["Message"]("Step 15")

  EnterText("numPart_NetWeight", "^a[Del]"+"0,5")

  ClickMenu("Edit->Refresh")
  
  var grid = GetGridMainPanel(baq)

  var columnNetWeight = getColumn(grid, "Unit Net Weight")
  var flag = true
  var netWeightFieldTV = GetText("numPart_NetWeight")

  for(var i = 0; i < grid["wRowCount"]; i++ ){
    var cell = grid["Rows"]["Item"](i)["Cells"]["Item"](columnNetWeight)
    if (cell["value"] < netWeightFieldTV ) {
      flag = false
      break
    }
  }

  if(flag){
    Log["Checkpoint"]("Grid retrieved records greater than the value set on tracker view.")
  }else{
    Log["Error"]("Grid didn't retrieve records greater than the value set on tracker view.")
  }

  ClickMenu("File->Exit")
}
  
function CloseE10(){
  Log["Message"]("Step 16")
  CloseSmartClient()
  Log["Message"]("SmartClient Closed")  
}

function ChangeRegionControlPanel2(){
  Log["Message"]("Step 21")

  OpenCMD("control")

  ChangeRegionControlPanel("Match Windows display language (recommended)")

  CloseCMD()
}

function LogInE10(){
  Log["Message"]("Step 22")
  StartSmartClient()

  Login(Project["Variables"]["username"], Project["Variables"]["password"])
  
  ActivateFullTree()
  
  OpenPanelTab("Menu Groups")

  ExpandComp(company1)

  ChangePlant(plant1)
}

function ModifyCultureAccountMngmnt2(){
  Delay(2500)
  Log["Message"]("Step 23")
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;User Account Security Maintenance")

  EnterText("txtKeyField", "epicor" + "[Tab]")

  var txtName = GetText("txtName")

  if (txtName != ""){
    Log["Checkpoint"]("Data loaded correctly")
  }else{
    Log["Error"]("Data is not loaded correctly")
  }

  EnterText("cmbFormatCulture", "Estonian(Estonia)" + "[Tab]")

  ClickMenu("File->Save")

  Log["Message"]("Format Culture changed")

  ClickMenu("File->Exit")
}

function RestartE102(){
   RestartSmartClient()
}

function TestDashboard2(){
  Log["Message"]("Step 24")

  OpenDashboardFavMenu(dashb)
  Log["Message"]("Dashboard opened from Favorite Menu")

  Log["Message"]("Step 25")

  EnterText("numPart_NetWeight", "^a[Del]"+"1,5")

  ClickMenu("Edit->Refresh")
  
  var grid = GetGridMainPanel(baq)

  var columnNetWeight = getColumn(grid, "Unit Net Weight")
  var flag = true
  var netWeightFieldTV = GetText("numPart_NetWeight")

  for(var i = 0; i < grid["wRowCount"]; i++ ){
    var cell = grid["Rows"]["Item"](i)["Cells"]["Item"](columnNetWeight)
    if (cell["value"] < netWeightFieldTV["value"] ) {
      flag = false
      break
    }
  }

  if(flag){
    Log["Checkpoint"]("Grid retrieved records greater than the value set on tracker view.")
  }else{
    Log["Error"]("Grid didn't retrieve records greater than the value set on tracker view.")
  }

  ClickMenu("File->Exit")
}

function ModifyCultureAccountMngmnt3(){
  Delay(2500)
  Log["Message"]("Step 23")
  MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;User Account Security Maintenance")

  EnterText("txtKeyField", "epicor" + "[Tab]")

  var txtName = GetText("txtName")

  if (txtName != ""){
    Log["Checkpoint"]("Data loaded correctly")
  }else{
    Log["Error"]("Data is not loaded correctly")
  }

  EnterText("cmbFormatCulture", "Invariant Language (Invariant Country)" + "[Tab]")

  ClickMenu("File->Save")

  Log["Message"]("Format Culture changed")

  ClickMenu("File->Exit")
}
