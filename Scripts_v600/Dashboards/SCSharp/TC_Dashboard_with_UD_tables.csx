//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_with_UD_tables(){
  
  var MenuDataEntry = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "MenuA",
    "menuName" : "MenuA",
    "orderSequence" : 0,
    "menuType" : "Menu Item",
    "dll" : "Ice.UI.UD100Entry.dll"
  }

  var baqData1 = {
    "Id" : "baqUD1",
    "Description" : "baqUD1",
    "Table" : "UD100",
    "Columns" : "Company,Key1,Key2,Key3,Key4,Key5,Character01"
  }

  var baqData2 = {
    "Id" : "baqUD2",
    "Description" : "baqUD2",
    "Table" : "UD100A",
    "Columns" : "Company,Key1,Key2,Key3,Key4,Key5,ChildKey1,ChildKey2,ChildKey3,ChildKey4,ChildKey5,Character01"
  }

  StartSmartClient()

  Login(Project["Variables"]["username"], Project["Variables"]["password"])

  ActivateFullTree()

  ExpandComp("Epicor Education")

  ChangePlant("Main Plant") 

  //--- Create Menu ---------------------------------------------------------------------------------------------------------------------------'
      
    //Open Menu maintenance   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")
    
    //Creates Menu
    CreateMenu(MenuDataEntry)
    Log["Checkpoint"]("MenuA created")

  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Restart SmartClient -------------------------------------------------------------------------------------------------------------------'
    
    RestartSmartClient()
    Log["Checkpoint"]("Epicor10 restarted")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Create UD registers -------------------------------------------------------------------------------------------------------------------'
    
    //Open Menu created
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuDataEntry["menuName"])
    Log["Checkpoint"]("MenuA opened")

    //******* Create register UD100 Maintenance ***********
      //Create new parent register
      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Parent")
      Log["Message"]("Selected 'New parent' option")
      //Fill the data

      var detailsParent = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["detailPanel1"]["groupBox1"]

      detailsParent["txtKeyField"]["Keys"]("K1")
      detailsParent["txtKeyField2"]["Keys"]("K2")
      detailsParent["txtKeyField3"]["Keys"]("K3")
      detailsParent["txtKeyField4"]["Keys"]("K4")
      detailsParent["txtKeyField5"]["Keys"]("K5")
      detailsParent["txtDesc"]["Keys"]("Char01")
      Log["Message"]("Parent 1 created")
      //Switch to Child tab

      Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
      Log["Message"]("Tab Child Activated")

      //Create new child
      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Child")
      Log["Message"]("Selected 'New Child' option")

      var detailsChild = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDockPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDetailPanel1"]["grpUD100A"]

      detailsChild["txtChildKey1"]["Keys"]("CK1")
      detailsChild["txtChildKey2"]["Keys"]("CK2")
      detailsChild["txtChildKey3"]["Keys"]("CK3")
      detailsChild["txtChildKey4"]["Keys"]("CK4")
      detailsChild["txtChildKey5"]["Keys"]("CK5")
      detailsChild["txtDesc"]["Keys"]("Char01")
      Log["Message"]("Child 1 created")

        //Create new parent register
      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Parent")
      Log["Message"]("Selected 'New parent' option")

      //Fill the data

      //var detailsParent = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["detailPanel1"]["groupBox1"]

      detailsParent["txtKeyField"]["Keys"]("K11")
      detailsParent["txtKeyField2"]["Keys"]("K22")
      detailsParent["txtKeyField3"]["Keys"]("K33")
      detailsParent["txtKeyField4"]["Keys"]("K44")
      detailsParent["txtKeyField5"]["Keys"]("K55")
      detailsParent["txtDesc"]["Keys"]("Char011")
      Log["Message"]("Parent 2 created")

      //Switch to Child tab

      Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
      Log["Message"]("Tab Child Activated")

      //Create new child
      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Child")
      Log["Message"]("Selected 'New Child' option")
     // var detailsChild = Aliases["Epicor"]["UD100Form"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDockPanel1"]["windowDockingArea1"]["dockableWindow2"]["childDetailPanel1"]["grpUD100A"]

      detailsChild["txtChildKey1"]["Keys"]("CK11")
      detailsChild["txtChildKey2"]["Keys"]("CK22")
      detailsChild["txtChildKey3"]["Keys"]("CK33")
      detailsChild["txtChildKey4"]["Keys"]("CK44")
      detailsChild["txtChildKey5"]["Keys"]("CK55")
      detailsChild["txtDesc"]["Keys"]("Char011")
      Log["Message"]("Child 2 created")

      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      Aliases["Epicor"]["UD100Form"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      Log["Message"]("MenuA exited")

    //******* End creation register UD100 Maintenance *****

    Log["Checkpoint"]("Register UD100 Maintenance Created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //------ Create BAQ against UDs -------------------------------------------------------------------------------------------------------------'
    
    //******* Create BAQ against UD100 ********************
      //Open Business Activity Query to create BAQ   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      Log["Checkpoint"]("Business Activity Query opened")

      CreateSimpleBAQ(baqData1)
      Log["Message"]("BAQ1 created for UD100")
    //******* End creation register UD100 Maintenance *****
      
      Delay(5000)

    //******* Create BAQ against UD100A ********************
     //Open Business Activity Query to create BAQ   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
      CreateSimpleBAQ(baqData2)
      Log["Message"]("BAQ2 created for UD100A")
    //******* End creation register UD100 Maintenance *****

    Log["Checkpoint"]("BAQs Created")
  //----- Create Dashboard --------------------------------------------------------------------------------------------------------------------'
    //******* Create Dashboard to retrieve BAQs created ********************
      //Open Dashboard   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")
      Log["Checkpoint"]("Dashboard opened")

      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Message"]("Devmode activated on Dashboard")

      //Call function to create and deploy a dashboard
      NewDashboard("DashbUD", "DashbUD", "DashbUD", "Refresh All")
      Log["Message"]("General data for data filled")
      Log["Message"]("Dashboard created")

      SaveDashboard()
      Log["Message"]("Dashboard saved - no queries added")

      /***** QUERY 1 *****/
        AddQueriesDashboard(baqData1["Id"])
        Log["Message"]("BAQ1 added")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]

        //Right click on the query and click on Properties        
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("BAQ1 - right click")

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ1 - Properties was selected")

        Delay(1000)

        // Active Publish tab and select all columns to be published
        var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]
        // DashboardQueryProperties("Publish")
        DashboardPropertiesTabs(queryProperties, "Publish")
        Log["Message"]("BAQ1 - publish tab was selected")

        var publishColumns = queryProperties["tabPublish"]["eclpPublishedColumns"]["myCLB"]
        
        //Select all columns to publish
        for (var i = 0; i <= publishColumns["Items"]["Count"] -1; i++) {
          publishColumns["ClickItem"](i)
        }
        Log["Message"]("BAQ1 - Columns selected to publish")

        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("BAQ1 - properties 'ok' button was clicked")

      /***** QUERY 2 *****/
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
        DashboardPropertiesTabs(queryProperties, "Filter")
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
        
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("Cells were filled and 'ok' button was clicked")

      /******* end creation for Dashboard to retrieve BAQs created **********/

      //Save dashboard
      SaveDashboard()
      Log["Message"]("Dashboard was saved")

      //Deploy dashboard 
      DeployDashboard("Deploy Smart Client,Add Favorite Item")
      Log["Message"]("Dashboard was deployed")

      ExitDashboard()
      Log["Message"]("Dashboard Closed")

      Log["Checkpoint"]("Dashboard Created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //------- Restart Epicor10 ------------------------------------------------------------------------------------------------------------------' 
    RestartSmartClient()
    Log["Checkpoint"]("Smart Client was restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'     
        
  //----- User Account Security Maintenance  --------------------------------------------------------------------------------------------------'
    // Go to User Account Security Maintenance 
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;User Account Security Maintenance")
    Log["Checkpoint"]("User Account Security Maintenance opened")

    //Retrieve the user you are logged in
    Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["txtKeyField"]["Keys"]("epicor")
    Log["Message"]("epicor user was retrieved")

    //Go to Tracing tab
    Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["Activate"]()
    Log["Message"]("Tracing tab activated")

    //Check Enable Trace Logging
    var accountForm = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["tracingOptionsPanel1"]["groupBox1"]
    
    accountForm["chkTraceLoging"]["Checked"] = true
    Log["Message"]("Enable Trace Checked")

    //Check: Write Full DataSet, Include Server Trace, Write Call Context DataSet, Write Response Data
    accountForm["groupBox2"]["chkFullDataSet"]["Checked"] = true
    Log["Message"]("Write Full DataSet Checked")
    
    accountForm["groupBox2"]["chkIncludeServerTrace"]["Checked"] = true
    Log["Message"]("Include Server Trace Checked")
    
    accountForm["groupBox2"]["chkWriteCCDataSet"]["Checked"] = true
    Log["Message"](" Write Call Context DataSet Checked")
    
    accountForm["groupBox2"]["chkReturnData"]["Checked"] = true
    Log["Message"](" Write Response Data Checked")
    
    //Save 
    Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save") 
    Log["Checkpoint"]("User Account Form saved")

    //Take note of the Current Log Directory
    var currentLog = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow6"]["tracingOptionsPanel1"]["txtCurrentLogLocation"]["Text"]

    //Close
    Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit") 
    Log["Message"]("User Account Form closed")

  //-------------------------------------------------------------------------------------------------------------------------------------------'     
  
  //------- Main Menu -------------------------------------------------------------------------------------------------------------------------'             
    // On Smart Client Main Menu select Tracing Options
    Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Tracing Options...") 
    Log["Message"]("Tracing Options menu option selected")

    //Click Clear Log
    Aliases["Epicor"]["TracingOptions"]["btnClearLog"]["Click"]()
    Log["Message"]("button ClearLog clicked")

    Aliases["Epicor"]["TracingOptions"]["btnOk"]["Click"]()
    Log["Message"]("button btnOk clicked")

  //-------------------------------------------------------------------------------------------------------------------------------------------'     

  //----- Testing Data ------------------------------------------------------------------------------------------------------------------------'     
    //** Testing from Favorites **
      ActivateFavoritesMenuTab()

      OpenDashboardFavMenu("DashbUD")

      DashboardPanelTest()

      DeactivateFavoritesMenuTab()
    //****************************
  //-------------------------------------------------------------------------------------------------------------------------------------------'     
    Delay(1000)

    DeactivateFullTree()
    
    CloseSmartClient()
}

function DashboardPanelTest(){
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All");

  var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
  
  //Gives time to load the children inside the variable
  Delay(2000)

  var baq1Grid = gridDashboardPanelChildren[0]
  var baq2Grid = gridDashboardPanelChildren[1]

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
  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit");
}