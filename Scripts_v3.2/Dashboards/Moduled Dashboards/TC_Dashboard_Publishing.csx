//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT ControlFunctions
//USEUNIT FormLib
//USEUNIT Data_Dashboard_Publishing

function Dashboard_Publishing(){

}

//Step 1
function CreateBAQ1(){
  ExpandComp(company1)

  ChangePlant(plant1)
  
  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  //1- Call function to create a simple BAQ
  Log["Message"]("Step 1")
  CreateSimpleBAQ(baqData1)
  Log["Message"]("BAQ " + baqData1["Id"] + " created correctly.")

}

//Step 2
function CreateBAQ2(){
  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  //2- Call function to create a simple BAQ
  Log["Message"]("Step 2")
  CreateSimpleBAQ(baqData2)
  Log["Message"]("BAQ " + baqData2["Id"] + " created correctly.")
}

// Steps 3
function CreateDashboard(){
  //Open Dashboard   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  Log["Message"]("Devmode activated on Dashboard")

  //3- Call function to create and deploy a dashboard
  Log["Message"]("Step 3")

  NewDashboard(DashbData["dashboardID"],DashbData["dashboardCaption"],DashbData["dashDescription"])
  Log["Message"]("General data for data filled")

  // Adding query
  AddQueriesDashboard(baqData1["Id"])
  Log["Message"](baqData1["Id"] + " added to the dashboard")
}    

// step 4, 5
function PublishView(){
  // var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
  var dashboardTree = GetTreePanel("DashboardTree")
  
  //4- Right click on the query summary and click on Publish View        
  Log["Message"]("Step 4")
  
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("Right click on " + baqData1["Id"])

  // click 'Publish View' option from menu
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Publish View");
  Log["Message"]("Publish View was selected from Menu")

  Delay(1000)

  //Enter Published caption, Group and Description, and take note of the description
  EnterText("cmbGroup", publishDetails["group"])
  EnterText("txtDescription", publishDetails["description"])

  ClickButton("OK")
  Log["Message"]("Published view added.")

  //Save dashboard
  SaveDashboard()
  Log["Message"]("Dashboard was saved")

  //5- Clear/Close the dashboard 
  Log["Message"]("Step 5")
  ExitDashboard()
}

// Steps 6, 7
function RetrieveDashbForPublish(){
  //Open Dashboard   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //retrieve the dashboard created
  OpenDashboard(DashbData["dashboardID"])
  Log["Message"]("Dashboard " + DashbData["dashboardID"] + " loaded")

  //checks if the 'Available Views' panel is available
  if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["Exists"]){
    ClickMenu("View->Published Views")
  }

  //Checks if there is any published view available on the 'Available Views' panel
  if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
    Log["Message"]("There are BAQs on published views")
  }

  /*MODIFY TO ADAPT DRAG FUNCTION*/
  //6- Drags the published view created
  Log["Message"]("Step 6")
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);
  Log["Message"]("BAQ from published views was dragged to the queries area")

  //7- Right click on the query summary and click on Properties        
  Log["Message"]("Step 7")

  var dashboardTree = GetTreePanel("DashboardTree")
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("Right click on " + baqData1["Id"] + "dragged from published views")

  // click 'Properties' option
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
  Log["Message"]("Properties was selected from " + baqData1["Id"] + " dragged - published views")

  Delay(1000)

  // Active Publish tab and select all columns to be published
  var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]
  // DashboardPropertiesTabs(queryProperties, "Publish")
  DashboardPropertiesTabs("Publish")

  Log["Message"]("Publish tab was selected from "+ baqData1["Id"] + " dragged - published views")

  var publishColumns = queryProperties["tabPublish"]["eclpPublishedColumns"]["myCLB"]
  
  //Select Customer_CustNum column to publish
  for (var i = 0; i <= publishColumns["Items"]["Count"] -1; i++) {
    if(publishColumns["Items"]["Item_2"](i)["Text"] == "Customer_CustNum"){
      publishColumns["ClickItem"](i)
      Log["Message"]("BAQ1 - Column(s) "+ publishColumns["Items"]["Item_2"](i)["Text"] +" selected to publish")
      break
    }
  }
  
  ClickButton("OK")
  Log["Message"]("BAQ1 - properties 'ok' button was clicked")

  SaveDashboard()
}

// Step 8
function AddQuery2Dashb(){
  //8- Add second BAQ created 
  Log["Message"]("Step 8")
  AddQueriesDashboard(baqData2["Id"])
  Log["Message"](baqData2["Id"] + " added")

  //Right click on the query and click on Properties        

  var dashboardTree = GetTreePanel("DashboardTree")
  var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](2)
  dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
  Log["Message"]("Right click on " + baqData2["Id"])

  // click 'Properties' option
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
  Log["Message"](baqData2["Id"] + " - Properties was selected")

  //active to 'Filter' tab
  DashboardPropertiesTabs("Filter")
  Log["Message"](baqData2["Id"] + " - filter tab was selected")

  var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]

  var ultraGrid = queryProperties["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")

  SelectCellDropdownGrid("ColumnName", "OrderHed_CustNum", ultraGrid)
  SelectCellDropdownGrid("Condition", "=", ultraGrid)
  SelectCellDropdownGrid("Value", baqData1["Id"]+"- "+ baqData1["Description"] + ": Customer_CustNum", ultraGrid)
  ultraGrid["Keys"]("[Enter]")

  //click 'OK' button
  ClickButton("OK")
  Log["Message"]("Cells were filled and 'ok' button was clicked")

  //Activate dashboard general panel
  OpenPanelTab("General")

  //Enable 'refresh all' checkbox
  CheckboxState("chkInhibitRefreshAll", true)

  SaveDashboard()    
}

// Step 9
function TestBeforeDeploy1(){

  //Operan 'Deploy dashboard' from menu
  ClickMenu("Tools->Deploy Dashboard")
  
  //9- Click 'Test Application'
  ClickButton("Test Application")
  Delay(1500)
  ClickButton("Cancel")

  /*9- Test data before deployment */
  Log["Message"]("Step 9")
  Log["Message"]("Ready to test data")

  var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

  var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
  
  //Gives time to load the children inside the variable
  Delay(2000)

  var baq1Grid = gridDashboardPanelChildren[1]
  var baq2Grid = gridDashboardPanelChildren[2]

  baq1Grid["Click"]()
  ClickMenu("Edit->Refresh")
  baq2Grid["Click"]()
  ClickMenu("Edit->Refresh")
   Log["Message"]("Dashboard refreshed")


  //Select first record on BAQ1 results to notice change of data on BAQ2
  baq1Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Activate"]()

  var cell = baq1Grid["Rows"]["Item"](0)["Cells"]["Item"](1)
  var rect = cell["GetUIElement"]()["Rect"]

  //Select on active cell
  baq1Grid["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

  var baqCellValueKey1 = baq1Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Value"]
  var baqCellValueKey2 = baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Value"]
  
  if( baqCellValueKey1["OleValue"] == baqCellValueKey2["OleValue"]){
    Log["Message"]("Data from "+ baqData1["Id"] +" row 0 matches the result on baq2")
  }

  //Select second record on BAQ1 results to notice change of data on BAQ2
  baq1Grid["Rows"]["Item"](1)["Cells"]["Item"](1)["Activate"]()

  cell = baq1Grid["Rows"]["Item"](1)["Cells"]["Item"](1)
  rect = cell["GetUIElement"]()["Rect"]

  //Select on active cell
  baq1Grid["DblClick"](rect["X"] + rect["Width"] - 5, rect["Y"] + rect["Height"]/2)

  baqCellValueKey1 = baq1Grid["Rows"]["Item"](1)["Cells"]["Item"](1)["Value"]
  baqCellValueKey2 = baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](1)["Value"]
  
  if( baqCellValueKey1["OleValue"] == baqCellValueKey2["OleValue"]){
    Log["Message"]("Data from "+ baqData1["Id"] + " row 1 matches the result on baq2"+ baqData2["Id"] )
  }

  Delay(1500)

}

// Step 10
function TestBeforeDeploy2(){

    var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

    var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
    
    //Gives time to load the children inside the variable
    Delay(2000)

    var baq1Grid = gridDashboardPanelChildren[1]
    var baq2Grid = gridDashboardPanelChildren[2]

  /* 10- In the second grid from the dashboard right click on any Order from "Order" column
   select open with > Sales Order Entry  (Or do the same with any CustID from the first grid and open it with Customer Maintenance) */
    Log["Message"]("Step 10")
    //Retrieve cell      
    cell = baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](0)
    rect = cell["GetUIElement"]()["Rect"]

    baq2Grid["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
    Log["Message"]("Cell from grid 2 "+ baqData2["Id"] +" was selected")

    // click 'Properties' option
    baq2Grid["UltraPopupMenu"]["Click"]("Open With...|Sales Order Entry");

    Delay(2500)
    if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
      Log["Message"]("|Sales Order Entry opened")
    }else{
      Log["Error"]("There was a problem opening Sales Order Entry")
    }
    Delay(2500)
  /* end Open Sales order */
}

// Step 11, 12
function TestBeforeDeploy3(){

    var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

    var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();

    //Gives time to load the children inside the variable
    Delay(2000)

    var baq1Grid = gridDashboardPanelChildren[1]
    var baq2Grid = gridDashboardPanelChildren[2]
    
  /* 11- change between orders from second grid and see what happens to Sales Order Entry form that is already opened */
    Log["Message"]("Step 11")
    //Activate Dash window
    Aliases["Epicor"]["MainController"]["Activate"]()
    Delay(2500)

    //Retrieve cell 'test 1' row 2
    cell = baq2Grid["Rows"]["Item"](1)["Cells"]["Item"](0)
    rect = cell["GetUIElement"]()["Rect"]

    baq2Grid["Click"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
    Log["Message"]("Left click on second record")
    
    //Activate Sales Order Window
    Aliases["Epicor"]["SalesOrderForm"]["Activate"]()
    Delay(1500)
    var salesOrderValue = Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Value"]
    
    // cell["Value"] is numeric and salesOrderValue is string
    if( aqConvert["IntToStr"](cell["Value"]) == salesOrderValue){
      Log["Message"]("Order " + cell["Value"] + " from grid 3 is loaded in Sales Order " + salesOrderValue)
    }else{
      Log["Warning"]("Order " + cell["Value"] + " from grid 3 is not loaded in Sales Order " + salesOrderValue)
    }
    
    //Activate Dash window
    Aliases["Epicor"]["MainController"]["Activate"]()

    //Retrieve cell 'test 2'  row 3
    cell = baq2Grid["Rows"]["Item"](2)["Cells"]["Item"](0)
    rect = cell["GetUIElement"]()["Rect"]

    baq2Grid["Click"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
    Log["Message"]("Left click on second record")

    //Activate Sales Order Window
    Aliases["Epicor"]["SalesOrderForm"]["Activate"]()        
    
    var salesOrderValue = Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Value"]
    
    // cell["Value"] is numeric and salesOrderValue is string
    if( aqConvert["IntToStr"](cell["Value"]) == salesOrderValue){
      Log["Message"]("Order " + cell["Value"] + " from grid 3 is loaded in Sales Order " + salesOrderValue)
    }else{
      Log["Warning"]("Order " + cell["Value"] + " from grid 3 is not loaded in Sales Order " + salesOrderValue)
    }

    //Close Sales Order
    ClickMenu("File->Exit")

    //Close Dashboard Panel
    ClickMenu("File->Exit")      

    Log["Checkpoint"]("Data was validated, no errors found")

    DeployDashboard(DashbData["deploymentOptions"])
    Log["Message"]("Dashboard was deployed")

    Log["Message"]("Step 12") 
    
    //Exit dashboard
    ExitDashboard()
    Log["Message"]("Dashboard was created correctly.")

}

// Step 13,14
function CreateDashboard2(){
  //Open Dashboard   
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  //Call function to create and deploy a dashboard
  NewDashboard(DashbData2["dashboardID"],DashbData2["dashboardCaption"],DashbData2["dashDescription"])
  Log["Message"]("General data for data filled")

  AddQueriesDashboard(baqData1["Id"])
  Log["Message"](baqData1["Id"] + " added")
  
  SaveDashboard()

  Log["Message"]("Dashboard saved")

  if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["Exists"]){
    ClickMenu("View->Published Views")
  }

  if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
    Log["Message"]("There are BAQs on available views")
  }

  /*MODIFY TO ADAPT DRAG FUNCTION*/
  // 13- drag that element with the name of your description from published view in step 4, to the Dashboard Area       
  Log["Message"]("Step 13")
  Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);

  //14- Save Dashboard and deploy 
  Log["Message"]("Step 14")
  SaveDashboard()
  Log["Message"]("Dashboard saved")

  //Deactivate Published Views
  ClickMenu("View->Published Views")

  DeployDashboard(DashbData2["deploymentOptions"])
  Log["Checkpoint"]("Dashboard was deployed")

  //Exit dashboard
  ExitDashboard()

  Delay(1000)  
}