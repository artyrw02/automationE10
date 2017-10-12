//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT FormLib
//USEUNIT Data_Dashboard_BAQParams

function Dashboard_BAQ_Parameters(){

}

// Steps 2
function CreateBAQ1(){

  ExpandComp(company1)

  ChangePlant(plant1)

  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  Log["Message"]("Business Activity Query form called")

  Log["Message"]("Step 2")

  //Call function to create a simple BAQ
  CreateSimpleBAQ(baqData)
  Log["Message"](baqData + " created")

}

// Steps 2
function CreateBAQ2(){
  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
  
  var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
  
  CreateBAQ(baqData2["Id"], baqData2["Description"])

  AddTableBAQ(BAQFormDefinition, "OrderDtl")
  
  AddColumnsBAQ(BAQFormDefinition, "OrderDtl", "Company,OrderNum,PartNum,CustNum")
  
  AddTableBAQ(BAQFormDefinition, "Customer")
  
  AddColumnsBAQ(BAQFormDefinition, "Customer", "CustNum")
        
  //Activate Phrase Build
  OpenPanelTab("Phrase Build")

  //---------- Setting params BAQ 2 --------'
    var diagram = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow2"]["subQueryPanel1"]["windowDockingArea1"]["dockableWindow1"]["diagramQueryPanel"]["splitMain"]["SplitterPanel"]["splitDiagramWhere"]["SplitterPanel"]["pnlQueryVisual"]["windowDockingArea1"]["dockableWindow1"]["diagramPanel"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["windowDockingArea2"]["dockableWindow2"]["diagCanvas"]
    diagram.HScroll.Pos = 0
    diagram.VScroll.Pos = 0
    diagram.Click(258, 55)

    var queryCondPanel = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow2"]["subQueryPanel1"]["windowDockingArea1"]["dockableWindow1"]["diagramQueryPanel"]["splitMain"]["SplitterPanel"]["splitDiagramWhere"]["SplitterPanel2"]["pnlQueryTabs"]

    // queryCondPanel["zEpiDockManagerPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("Filtering toolbar|Add Row")
    ClickMenu("Add Row")

    var tableWhereClausePanel = queryCondPanel["windowDockingArea1"]["dockableWindow3"]["tableWhereClausePanel"]
    var epiUltraGrid = tableWhereClausePanel["grdTableWhere"]
    
    SelectCellDropdownGrid2("Field", "CustNum", epiUltraGrid)
    SelectCellDropdownGrid2("Operation", "MATCHES", epiUltraGrid)
    SelectCellDropdownGrid2("Filter Value", "specified parameter", epiUltraGrid)

    //Select Parameter
    // Aliases["Epicor"]["ParameterForm"]["btnDefine"]["Click"]()
    ClickButton("Define...")

    //File New Parameter
    // Aliases["Epicor"]["CtrlDesignerForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&New")
    ClickMenu("File->New")

    var queryParametersdialog = Aliases["Epicor"]["CtrlDesignerForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["windowDockingArea1"]
    
    //Parameter name dialog
    queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["txtFldName"]["Keys"]("CustNum")
    queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataType"]["Keys"]("int")
    queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataType"]["Keys"]("[Tab]")
    queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbEditorType"]["Keys"]("DropDown List")
    queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataFrom"]["Keys"]("BAQ")

    var queryParamsValueEditor = queryParametersdialog["dockableWindow2"]["ctrlCustomEditor1"]["windowDockingArea1"]["dockableWindow2"]["ctrlBAQList1"]

    //Query ID
    queryParamsValueEditor["txtExportID"]["Keys"](baqData["Id"])
    //Display Column
    queryParamsValueEditor["cmbDisplay"]["Keys"]("Customer_CustNum")
    //DisplayValue
    queryParamsValueEditor["cmbValue"]["Keys"]("Customer_CustNum")

    //Save//
    ClickMenu("File->Save")
    Log["Message"]("Parameters where set for the filter value.")
    //Exit
    ClickMenu("File->Exit")

    //Select parameter
    ClickButton("Select")

  //---------- END Params BAQ 2 ------------'
      
  //Activate Analyze tab
  AnalyzeSyntaxisBAQ()

  //Test Data
  TestResultsBAQ("23")

  //Save BAQ
  SaveBAQ()

  //Exit BAQ
  ExitBAQ()

  Log["Message"](baqData2["Id"] + " created")

}

// Steps 3 to 9
function CreateDashboard1(){
  Log["Message"]("Step 3")
  
  //Navigate and open Dashboard
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
  Log["Message"]("Dashboard opened")
  
  //Enable Dashboard Developer Mode  
  DevMode()
  Log["Message"]("DevMode activated")

  //Creating dashboard
  Log["Message"]("Step 4 - 9")
  CreateSimpleDashboards(DashbData)
  Log["Message"]("Dashboard created")
}
  
// Steps 10
function RestartE10(){
  Delay(10000)
  Log["Message"]("Step 10")
  RestartSmartClient()
  Log["Message"]("SmartClient Restarted")
}
  
// Steps 11
function TestDeployedDashb(){

  ActivateFavoritesMenuTab()
  Log["Message"]("FavoritesMenuTab Activated")
  Log["Message"]("Step 11")
  
  OpenDashboardFavMenu(DashbData["dashDescription"])

  Log["Message"]("Dashboard opened")

  DashboardPanelTest()
  Log["Message"]("Dashboard tested")

  DeactivateFavoritesMenuTab()
  Log["Message"]("FavoritesMenuTab deactivated")
}

//Steps 12 to 14
function DashboardPanelTest(){
  Log["Message"]("Step 12")

  ClickMenu("Edit->Refresh")

  var parameterValue = "23"

  if (Aliases["Epicor"]["BaseForm"]["Exists"]) {
      Log["Message"]("Step 13")
      ComboboxSelect("edt0", parameterValue)
      ClickButton("OK")
  }
  
  // var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  // var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
  
  //Gives time to load the children inside the variable
  Delay(2000)

  var baqGrid = GetGridMainPanelDashB(baqData2["Id"])

  if(baqGrid["Rows"]["Count"] > 0){
    Log["Checkpoint"]("Grid retrieved " + baqGrid["Rows"]["Count"] + " records.")
  }else{
    Log["Error"]("Grid retrieved " + baqGrid["Rows"]["Count"] + " records.")
  }

  for (var i = 0; i <= baqGrid["Rows"]["Count"] - 1; i++) {
      
    var cell1Customer = baqGrid["Rows"]["Item"](i)["Cells"]["Item"](3)
    var cell2Customer = baqGrid["Rows"]["Item"](i)["Cells"]["Item"](4)

    if (cell1Customer["Text"]["OleValue"] != parameterValue && cell1Customer["Text"]["OleValue"] != cell2Customer["Text"]["OleValue"] ) {
      Log["Message"]("Data doesn't match with the parameter given ( " + parameterValue + " ) -> Order " + baqGrid["Rows"]["Item"](i)["Cells"]["Item"](1)["Text"]["OleValue"] + " Customer " + cell1Customer["Text"]["OleValue"])
    }else{
      Log["Message"]("Data match with the parameter given ( " + parameterValue + " ) -> Order " + baqGrid["Rows"]["Item"](i)["Cells"]["Item"](1)["Text"]["OleValue"] + " Customer " + cell1Customer["Text"]["OleValue"])
    }
  }

  //Closes panel
  ClickMenu("File->Exit")
}
