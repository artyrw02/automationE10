//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT Data_Dashboard_UDs

function Dashboard_with_UD_tables(){

  //--- Create Menu ---------------------------------------------------------------------------------------------------------------------------'
      
    //Open Menu maintenance   
    Log["Message"]("Step 2")
    MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")
    
    //Creates Menu
    CreateMenu(MenuDataEntry)
    Log["Message"]("MenuA created")

  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Restart SmartClient -------------------------------------------------------------------------------------------------------------------'
    Log["Message"]("Step 3")
    RestartSmartClient()
    Log["Message"]("Epicor10 restarted")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Create UD registers -------------------------------------------------------------------------------------------------------------------'
    //Open Menu created
    Log["Message"]("Step 4")
    MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuDataEntry["menuName"])
    Log["Message"]("MenuA opened")

    //******* Create register UD100 Maintenance ***********
      //Create new parent register
      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Parent")
      Log["Message"]("Step 5")

      ClickMenu("File->New...->New Parent")
      Log["Message"]("Selected 'New parent' option")
      //Fill the data

      // var detailsParent = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["detailPanel1"]["groupBox1"]

      EnterText("txtKeyField", "K1" + "[Tab]" )
      EnterText("txtKeyField2","K2" + "[Tab]" )
      EnterText("txtKeyField3","K3" + "[Tab]" )
      EnterText("txtKeyField4","K4" + "[Tab]" )
      EnterText("txtKeyField5","K5" + "[Tab]" )
      EnterText("txtDesc","Char01" + "[Tab]" )
      // detailsParent["txtKeyField"]["Keys"]("K1")
      // detailsParent["txtKeyField2"]["Keys"]("K2")
      // detailsParent["txtKeyField3"]["Keys"]("K3")
      // detailsParent["txtKeyField4"]["Keys"]("K4")
      // detailsParent["txtKeyField5"]["Keys"]("K5")
      // detailsParent["txtDesc"]["Keys"]("Char01")
      Log["Message"]("Parent 1 created")
      //Switch to Child tab

      // Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
      OpenPanelTab("Child")
      Log["Message"]("Tab Child Activated")

      //Create new child
      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Child")
      ClickMenu("File->New...->New Child")
      Log["Message"]("Selected 'New Child' option")

      // var detailsChild = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDockPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDetailPanel1"]["grpUD100A"]

      EnterText("txtChildKey1", "CK1" + "[Tab]")
      EnterText("txtChildKey2", "CK2" + "[Tab]")
      EnterText("txtChildKey3", "CK3" + "[Tab]")
      EnterText("txtChildKey4", "CK4" + "[Tab]")
      EnterText("txtChildKey5", "CK5" + "[Tab]")
      EnterText("txtDesc", "Char01" + "[Tab]")
      Log["Message"]("Child 1 created")

        //Create new parent register
      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Parent")
      ClickMenu("File->New...->New Parent")
      Log["Message"]("Selected 'New parent' option")

      //Fill the data
      //var detailsParent = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["detailPanel1"]["groupBox1"]

      EnterText("txtKeyField", "K11" + "[Tab]")
      EnterText("txtKeyField2","K22" + "[Tab]")
      EnterText("txtKeyField3","K33" + "[Tab]")
      EnterText("txtKeyField4","K44" + "[Tab]")
      EnterText("txtKeyField5","K55" + "[Tab]")
      EnterText("txtDesc", "Char011" + "[Tab]")
      Log["Message"]("Parent 2 created")

      //Switch to Child tab

      // Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
      OpenPanelTab("Child")
      Log["Message"]("Tab Child Activated")

      //Create new child
      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Child")
      ClickMenu("File->New...->New Child")
      Log["Message"]("Selected 'New Child' option")
     // var detailsChild = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDockPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDetailPanel1"]["grpUD100A"]

      EnterText("txtChildKey1", "CK11" + "[Tab]")
      EnterText("txtChildKey2", "CK22" + "[Tab]")
      EnterText("txtChildKey3", "CK33" + "[Tab]")
      EnterText("txtChildKey4", "CK44" + "[Tab]")
      EnterText("txtChildKey5", "CK55" + "[Tab]")
      EnterText("txtDesc", "Char011" + "[Tab]")
      Log["Message"]("Child 2 created")

      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      // Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      ClickMenu("File->Save")
      ClickMenu("File->Exit")
      Log["Message"]("MenuA exited")

    //******* End creation register UD100 Maintenance *****

    Log["Message"]("Register UD100 Maintenance Created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //------ Create BAQ against UDs -------------------------------------------------------------------------------------------------------------'
    
    //******* Create BAQ against UD100 ********************
      Log["Message"]("Step 6")
      //Open Business Activity Query to create BAQ   
      MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      Log["Message"]("Business Activity Query opened")

      CreateSimpleBAQ(baqData1)
      Log["Message"]("BAQ1 created for UD100")
    //******* End creation register UD100 Maintenance *****
      
      Delay(5000)

    //******* Create BAQ against UD100A ********************
      Log["Message"]("Step 7")
     //Open Business Activity Query to create BAQ   
      MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      CreateSimpleBAQ(baqData2)
      Log["Message"]("BAQ2 created for UD100A")
    //******* End creation register UD100 Maintenance *****

    Log["Message"]("BAQs Created")
  //----- Create Dashboard --------------------------------------------------------------------------------------------------------------------'
    //******* Create Dashboard to retrieve BAQs created ********************
      //Open Dashboard   
      Log["Message"]("Step 8")
      MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
      Log["Message"]("Dashboard opened")

      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Message"]("Devmode activated on Dashboard")

      //Call function to create and deploy a dashboard
      Log["Message"]("Step 9")
      NewDashboard(DashbData["dashboardID"], DashbData["dashboardCaption"], DashbData["dashDescription"], DashbData["generalOptions"])
      Log["Message"]("General data for data filled")
      Log["Message"]("Dashboard created")

      /***** QUERY 1 *****/
        Log["Message"]("Step 10")
        AddQueriesDashboard(baqData1["Id"])
        Log["Message"]("BAQ1 added")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]

        //Right click on the query and click on Properties        
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("BAQ1 - right click")

        // click 'Properties' option
        Log["Message"]("Step 11")
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ1 - Properties was selected")

        Delay(1000)

        // Active Publish tab and select all columns to be published
        // DashboardQueryProperties("Publish")
        Log["Message"]("Step 12")
        DashboardPropertiesTabs("Publish")
        Log["Message"]("BAQ1 - publish tab was selected")

        var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]
        var publishColumns = queryProperties["tabPublish"]["eclpPublishedColumns"]["myCLB"]
        
        //Select all columns to publish
        for (var i = 0; i <= publishColumns["Items"]["Count"] -1; i++) {
          publishColumns["ClickItem"](i)
        }
        Log["Message"]("BAQ1 - Columns selected to publish")

        // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        ClickButton("OK")
        Log["Message"]("BAQ1 - properties 'ok' button was clicked")

      /***** QUERY 2 *****/
        Log["Message"]("Step 13")
        AddQueriesDashboard(baqData2["Id"])
        Log["Message"]("BAQ2 added")

        //Right click on the query and click on Properties        
        rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("BAQ2 - right click")

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ2 - Properties was selected")

        //active to 'Filter' tab
        // DashboardQueryProperties("Filter")
        Log["Message"]("Step 14")
        DashboardPropertiesTabs("Filter")
        Log["Message"]("BAQ1 - filter tab was selected")

        var ultraGrid = queryProperties["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")

        //GRID 

        /*UD100A_Company = baq1 - baq1: UD100_Company
         UD100A_Key1 = baq1 - baq1: UD100_Key1
         UD100A_Key2= baq1 - baq1: UD100_Key2
         UD100A_Key3= baq1 - baq1: UD100_Key3
         UD100A_Key4= baq1 - baq1: UD100_Key4
         UD100A_Key5= baq1 - baq1: UD100_Key5
         UD100A_Char01 = baq1 - baq1: UD100_Char01*/

        SelectCellDropdownGrid("ColumnName", "UD100A_Company", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Company", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Key1", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Key1", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Key2", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Key2", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Key3", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Key3", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Key4", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Key4", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Key5", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Key5", ultraGrid)
        ultraGrid["Keys"]("[Enter]")
        SelectCellDropdownGrid("ColumnName", "UD100A_Character01", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"] +"- " + baqData1["Description"] + ": UD100_Character01", ultraGrid)
        
        // Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        ClickButton("OK")
        Log["Message"]("Cells were filled and 'ok' button was clicked")

      /******* end creation for Dashboard to retrieve BAQs created **********/

      Log["Message"]("Step 15")
      //Save dashboard
      SaveDashboard()
      Log["Message"]("Dashboard was saved")

      Log["Message"]("Step 16")
      //Deploy dashboard 
      DeployDashboard("Deploy Smart Client,Add Favorite Item")
      Log["Message"]("Dashboard was deployed")

      ExitDashboard()
      Log["Message"]("Dashboard Closed")

      Log["Message"]("Dashboard Created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //------- Restart Epicor10 ------------------------------------------------------------------------------------------------------------------' 
    Log["Message"]("Step 17")  
    RestartSmartClient()
    Log["Message"]("Smart Client was restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'     
        
  //----- User Account Security Maintenance  --------------------------------------------------------------------------------------------------'
    Log["Message"]("Step 18")
    // Go to User Account Security Maintenance 
    MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;User Account Security Maintenance")
    Log["Message"]("User Account Security Maintenance opened")

    //Retrieve the user you are logged in
    // Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["txtKeyField"]["Keys"]()
    EnterText("txtKeyField", Project["Variables"]["username"] + "[Tab]")
    Log["Message"]("epicor user was retrieved")

    //Go to Tracing tab
    OpenPanelTab("Tracing")
    // Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["Activate"]()
    Log["Message"]("Tracing tab activated")

    //Check Enable Trace Logging
    // var accountForm = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["tracingOptionsPanel1"]["groupBox1"]
    
    // accountForm["chkTraceLoging"]["Checked"] = true
    CheckboxState("chkTraceLoging", true)
    Log["Message"]("Enable Trace Checked")

    //Check: Write Full DataSet, Include Server Trace, Write Call Context DataSet, Write Response Data
    // accountForm["groupBox2"]["chkFullDataSet"]["Checked"] = true
    CheckboxState("chkFullDataSet", true)
    Log["Message"]("Write Full DataSet Checked")
    
    // accountForm["groupBox2"]["chkIncludeServerTrace"]["Checked"] = true
    CheckboxState("chkIncludeServerTrace", true)
    Log["Message"]("Include Server Trace Checked")
    
    // accountForm["groupBox2"]["chkWriteCCDataSet"]["Checked"] = true
    CheckboxState("chkWriteCCDataSet", true)
    Log["Message"](" Write Call Context DataSet Checked")
    
    // accountForm["groupBox2"]["chkReturnData"]["Checked"] = true
    CheckboxState("chkReturnData", true)
    Log["Message"](" Write Response Data Checked")
    
    //Save 
    // Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save") 
    ClickMenu("File->Save")
    Log["Checkpoint"]("User Account Form saved")

    //Take note of the Current Log Directory
    // var currentLog = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["tracingOptionsPanel1"]["txtCurrentLogLocation"]["Text"]

    //Close
    ClickMenu("File->Exit")
    // Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit") 
    Log["Message"]("User Account Form closed")

  //-------------------------------------------------------------------------------------------------------------------------------------------'     
  
  //------- Main Menu -------------------------------------------------------------------------------------------------------------------------'             
    // On Smart Client Main Menu select Tracing Options
    Log["Message"]("Step 19")
    // Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Tracing Options...") 
    ClickMenu("Options->Tracing Options...")
    Log["Message"]("Tracing Options menu option selected")

    //Click Clear Log
    // Aliases["Epicor"]["TracingOptions"]["btnClearLog"]["Click"]()
    ClickButton("Clear Log")
    Log["Message"]("button ClearLog clicked")

    // Aliases["Epicor"]["TracingOptions"]["btnOk"]["Click"]()
    ClickButton("OK")
    Log["Message"]("button btnOk clicked")

  //-------------------------------------------------------------------------------------------------------------------------------------------'     

  //----- Testing Data ------------------------------------------------------------------------------------------------------------------------'     
    //** Testing from Favorites **
      Log["Message"]("Step 20")
      ActivateFavoritesMenuTab()

      OpenDashboardFavMenu(DashbData["dashboardID"])

      DashboardPanelTest()

      DeactivateFavoritesMenuTab()
    //****************************
  //-------------------------------------------------------------------------------------------------------------------------------------------'     
    Delay(1000)

    DeactivateFullTree()
    
    CloseSmartClient()
}

function DashboardPanelTest(){
  Log["Message"]("Step 21")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All");

  var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
  
  //Gives time to load the children inside the variable
  Delay(2000)

  var baq1Grid = gridDashboardPanelChildren[0]
  var baq2Grid = gridDashboardPanelChildren[1]

  baq1Grid["Click"]()
  ClickMenu("Edit->Refresh")
  baq2Grid["Click"]()
  ClickMenu("Edit->Refresh")

  Log["Message"]("Step 22")  
  if(baq1Grid["Rows"]["Count"] > 0){
    Log["Message"]("Grid 1 has " + baq1Grid["Rows"]["Count"] + " records")

    for (var i = 0; i <= baq1Grid["Rows"]["Count"] - 1; i++) {
      //Select first record on BAQ1 results to notice change of data on BAQ2      

      //Selecting 'K1' value
      var cell1Key = baq1Grid["Rows"]["Item"](i)["Cells"]["Item"](1)

      cell1Key["Activate"]()

      var rect = cell1Key["GetUIElement"]()["Rect"]
      
      // Click on active cell
      baq1Grid["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

      var valueGrid1 = cell1Key["Text"]["OleValue"] 
      var valueGrid2 = baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Text"]["OleValue"]

      if(baq2Grid["Rows"]["Count"] > 0){
        Log["Message"]("Grid 2 has " + baq2Grid["Rows"]["Count"] + " records")
        if(valueGrid1 == valueGrid2){
          Log["Message"]("Key 1 from Grid 1: " + cell1Key["Text"] + " matches with Key 1 from Grid 2: " + baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Text"])
        }
      }else{
        Log["Warning"]("Grid 2 has 0 records")
        break
      }
    }
  }else{
    Log["Warning"]("Grid 1 has 0 records")
  }

  //Closes panel
  ClickMenu("File->Exit")
  // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit");
}