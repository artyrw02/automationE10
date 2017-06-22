//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_BAQ_Parameters(){
  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    // StartSmartClient()

    // Login("epicor","Epicor123", "Classic") 

    // ActivateFullTree()
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates BAQs --------------------------------------------------------------------------------------------------------------------------'
    
    //Open Business Activity Query to create BAQ   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
    Log["Checkpoint"]("BAQ opened")

    //****** Creating BAQ1 ***************'
      //Call function to create a simple BAQ
      CreateSimpleBAQ("baq1", "baq", "Customer", "CustNum")
      Log["Checkpoint"]("BAQ1 created")
    //****** End of BAQ1 creation ********'

    //****** Creating BAQ2 ***************'
        
        //Open Business Activity Query to create BAQ   
        MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
        
        var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
        
        CreateBAQ("baq2", "baqDescription")

        AddTableBAQ(BAQFormDefinition, "OrderDtl")
        
        AddColumnsBAQ(BAQFormDefinition, "OrderDtl", "Company,OrderNum,PartNum,CustNum")
        
        AddTableBAQ(BAQFormDefinition, "Customer")
        
        AddColumnsBAQ(BAQFormDefinition, "Customer", "CustNum")
        
        //Activate Phrase Build
        BAQFormDefinition["dockableWindow2"]["subQueryPanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()

        //---------- Setting params BAQ 2 --------'
             var diagram = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow2"]["subQueryPanel1"]["windowDockingArea1"]["dockableWindow1"]["diagramQueryPanel"]["splitMain"]["SplitterPanel"]["splitDiagramWhere"]["SplitterPanel"]["pnlQueryVisual"]["windowDockingArea1"]["dockableWindow1"]["diagramPanel"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["windowDockingArea2"]["dockableWindow2"]["diagCanvas"]
              diagram.HScroll.Pos = 0
              diagram.VScroll.Pos = 0
              diagram.Click(258, 55)

              var queryCondPanel = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow2"]["subQueryPanel1"]["windowDockingArea1"]["dockableWindow1"]["diagramQueryPanel"]["splitMain"]["SplitterPanel"]["splitDiagramWhere"]["SplitterPanel2"]["pnlQueryTabs"]

              queryCondPanel["zEpiDockManagerPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("Filtering toolbar|Add Row")

              var tableWhereClausePanel = queryCondPanel["windowDockingArea1"]["dockableWindow3"]["tableWhereClausePanel"]
              var epiUltraGrid = tableWhereClausePanel["grdTableWhere"]
              
              SelectCellDropdownGrid("Field", "CustNum", epiUltraGrid)
              SelectCellDropdownGrid("Operation", "MATCHES", epiUltraGrid)
              SelectCellDropdownGrid("Filter Value", "specified parameter", epiUltraGrid)

              //Select Parameter
              Aliases["Epicor"]["ParameterForm"]["btnDefine"]["Click"]()

              //File New Parameter
              Aliases["Epicor"]["CtrlDesignerForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&New")

              var queryParametersdialog = Aliases["Epicor"]["CtrlDesignerForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["windowDockingArea1"]
              
              //Parameter name dialog
              queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["txtFldName"]["Keys"]("CustNum")
              queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataType"]["Keys"]("int")
              queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataType"]["Keys"]("[Tab]")
              queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbEditorType"]["Keys"]("DropDown List")
              queryParametersdialog["dockableWindow1"]["ctrlDescrPanel1"]["cmbDataFrom"]["Keys"]("BAQ")

              var queryParamsValueEditor = queryParametersdialog["dockableWindow2"]["ctrlCustomEditor1"]["windowDockingArea1"]["dockableWindow2"]["ctrlBAQList1"]

              //Query ID
              queryParamsValueEditor["txtExportID"]["Keys"]("baq1")
              //Display Column
              queryParamsValueEditor["cmbDisplay"]["Keys"]("Customer_CustNum")
              //DisplayValue
              queryParamsValueEditor["cmbValue"]["Keys"]("Customer_CustNum")

              //Save//
              Aliases["Epicor"]["CtrlDesignerForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")

              //Exit
              Aliases["Epicor"]["CtrlDesignerForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")

              //Select parameter
              Aliases["Epicor"]["ParameterForm"]["btnOK"]["Click"]()

        //---------- END Params BAQ 2 ------------'
      
        //Activate Analyze tab
        AnalyzeSyntaxisBAQ(BAQFormDefinition)

        //Test Data
        TestResultsBAQ(BAQFormDefinition, "23")

        //Save BAQ
        SaveBAQ()
      
        //Exit BAQ
        ExitBAQ()

    //****** End of BAQ2 Creation ********'
      Log["Checkpoint"]("BAQ2 created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    //Navigate and open Dashboard
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")
    Log["Checkpoint"]("Dashboard opened")
    //Enable Dashboard Developer Mode  
    DevMode()
    Log["Checkpoint"]("DevMode activated")
    //Creating dashboard
    CreateSimpleDashboards("DashBBAQ", "DashBBAQ", "DashBBAQ description", "", "baq2", "Deploy Smart Client,Add Favorite Item,Generate Web Form")
    Log["Checkpoint"]("Dashboard created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    Delay(10000)
    RestartSmartClient("Classic")
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Test Deployed Dashboard ---------------------------------------------------------------------------------------------------------------'
    
    //****** Favorite tab ***************'
        ActivateFavoritesMenuTab()
        Log["Checkpoint"]("FavoritesMenuTab Activated")

        OpenDashboardFavMenu("DashBBAQ")
        Log["Checkpoint"]("Dashboard opened")

        DashboardPanelTest()
        Log["Checkpoint"]("Dashboard tested")

        DeactivateFavoritesMenuTab()
        Log["Checkpoint"]("FavoritesMenuTab deactivated")
    //***********************************'

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Close Smart Client --------------------------------------------------------------------------------------------------------------------'
    
    Delay(1000)
    
    DeactivateFullTree()
    Log["Checkpoint"]("FullTree Deactivate")

    CloseSmartClient()
    Log["Checkpoint"]("SmartClient Closed")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
}


function DashboardPanelTest(){
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh");

  var parameterValue = "23"

  if (Aliases["Epicor"]["BaseForm"]["Exists"]) {
    var pnlParametersBAQField = Aliases["Epicor"]["BaseForm"]["pnlMain"]["grpFields"]["pnlFields"]
    var pnlParametersBAQBtn = Aliases["Epicor"]["BaseForm"]["pnlMain"]["grpButtons"]
    
    var fieldParameter = pnlParametersBAQField["FindAllChildren"]("FullName", "*edt*", 5)["toArray"]();
    var btnParameter = pnlParametersBAQBtn["FindAllChildren"]("FullName", "*btn*", 5)["toArray"]();

    fieldParameter[0]["Keys"](parameterValue)

    for (var i = 0; i <= btnParameter.length -1; i++) {
      if(btnParameter[i]["Text"] == "OK"){
        btnParameter[i]["Click"]()
        break
      }
      
    }
  }
  
  var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
  
  //Gives time to load the children inside the variable
  Delay(2000)

  var baqGrid = gridDashboardPanelChildren[0]

  for (var i = 0; i <= baqGrid["Rows"]["Count"] - 1; i++) {
    //Select first record on BAQ1 results to notice change of data on BAQ2      

    var cell1Customer = baqGrid["Rows"]["Item"](i)["Cells"]["Item"](3)
    var cell2Customer = baqGrid["Rows"]["Item"](i)["Cells"]["Item"](4)

    if (cell1Customer["Text"] != parameterValue && cell1Customer["Text"] != cell2Customer["Text"] ) {
      Log["Message"]("Data doesn't match with the parameter given ( " + parameterValue + " ) -> Order " + baqGrid["Rows"]["Item"](i)["Cells"]["Item"](1)["Text"] + " Customer " + cell1Customer["Text"])
    }else{
      Log["Message"]("Data match with the parameter given ( " + parameterValue + " ) -> Order " + baqGrid["Rows"]["Item"](i)["Cells"]["Item"](1)["Text"] + " Customer " + cell1Customer["Text"])
    }
  }

  //Closes panel
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit");
}
