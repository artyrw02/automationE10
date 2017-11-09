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
  
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  Log["Message"]("Step 1")
  CreateSimpleBAQ(baqData1)
  Log["Message"]("BAQ " + baqData1["Id"] + " created correctly.")
}

//Step 2
function CreateBAQ2(){
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  Log["Message"]("Step 2")
  CreateSimpleBAQ(baqData2)
  Log["Message"]("BAQ " + baqData2["Id"] + " created correctly.")
}

// Steps 3
function CreateDashboard(){
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  DevMode()

  Log["Message"]("Devmode activated on Dashboard")

  Log["Message"]("Step 3")

  NewDashboard(DashbData["dashboardID"],DashbData["dashboardCaption"],DashbData["dashDescription"])
  Log["Message"]("General data for data filled")

  AddQueriesDashboard(baqData1["Id"], baqData1["Id"] + "0")
  Log["Message"](baqData1["Id"] + " added to the dashboard")
}    

// step 4, 5
function PublishView(){
  
  Log["Message"]("Step 4")

  var publishedViewsGrid = GetGrid("grdPubViews")
  //checks if the 'Available Views' panel is available
  if(!publishedViewsGrid["Exists"]){
    ClickMenu("View->Published Views")
    
    var publishedViewsGrid = GetGrid("grdPubViews")
    //Checks if there is any published view available on the 'Available Views' panel
    if(publishedViewsGrid["Rows"]["Count"] > 0){
      Log["Message"]("BAQs " + publishedViewsGrid["Rows"]["Count"] +  " count of published views")
      var countPublishView = publishedViewsGrid["Rows"]["Count"] - 1 + "_"
    }else{
      var countPublishView = "0_"
    }
  }

  ClickPopupMenu("Queries|" + baqData1["Id"] + ": " + baqData1["Id"] + "0" + "|" + baqData1["Id"] + ": Summary", "Publish View")

  Delay(1000)

  //Enter Published caption, Group and Description, and take note of the description
  EnterText("txtCaption", countPublishView + publishDetails["group"])
  EnterText("cmbGroup", countPublishView + publishDetails["group"])
  EnterText("txtDescription", countPublishView + publishDetails["description"])

  ClickButton("OK")
  Log["Message"]("Published view added.")

  SaveDashboard()
  Log["Message"]("Dashboard was saved")

  Log["Message"]("Step 5")
  ExitDashboard()
}

// Steps 6, 7
function RetrieveDashbForPublish(){
  MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

  OpenDashboard(DashbData["dashboardID"])
  Log["Message"]("Dashboard " + DashbData["dashboardID"] + " loaded")

  var publishedViewsGrid = GetGrid("grdPubViews")
  //checks if the 'Available Views' panel is available
  if(!publishedViewsGrid["Exists"]){
    ClickMenu("View->Published Views")
  }

  //Checks if there is any published view available on the 'Available Views' panel
  if(publishedViewsGrid["Rows"]["Count"] > 0){
    Log["Message"]("There are BAQs on published views")
  }

  /*MODIFY TO ADAPT DRAG FUNCTION*/
  //6- Drags the published view created
  Log["Message"]("Step 6")
  SetToolButtonText("pnlPubViews", "Filtering", "0_")

  E10["Refresh"]()

  publishedViewsGrid["Drag"](77, 30, 11, -127);
  Log["Message"]("BAQ from published views was dragged to the queries area")

  //7- Right click on the query summary and click on Properties        
  Log["Message"]("Step 7")

  ClickPopupMenu("Queries|" + baqData1["Id"] + ": " + baqData1["Id"], "Properties")

  Delay(1000)

  // Active Publish tab and select all columns to be published
  DashboardPropertiesTabs("Publish")

  Log["Message"]("Publish tab was selected from "+ baqData1["Id"] + " dragged - published views")

  var publishColumns = GetList("myCLB")
  
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
  Log["Message"]("Step 8")
  AddQueriesDashboard(baqData2["Id"])
  Log["Message"](baqData2["Id"] + " added")

  ClickPopupMenu("Queries|" + baqData2["Id"] + ": " + baqData2["Id"], "Properties")
  Log["Message"](baqData2["Id"] + " - Properties was selected")

  //active to 'Filter' tab
  DashboardPropertiesTabs("Filter")
  Log["Message"](baqData2["Id"] + " - filter tab was selected")

  var ultraGrid = GetGrid("ultraGrid1")

  SelectCellDropdownGrid("ColumnName", "OrderHed_CustNum", ultraGrid)
  SelectCellDropdownGrid("Condition", "=", ultraGrid)
  SelectCellDropdownGrid("Value", baqData1["Id"]+"- "+ baqData1["Description"] + ": Customer_CustNum", ultraGrid)
  ultraGrid["Keys"]("[Enter]")

  ClickButton("OK")
  Log["Message"]("Cells were filled and 'ok' button was clicked")

  OpenPanelTab("General")

  CheckboxState("chkInhibitRefreshAll", true)

  SaveDashboard()    
}

// Step 9
function TestBeforeDeploy1(){

  ClickMenu("Tools->Deploy Dashboard")
  
  ClickButton("Test Application")

  Log["Message"]("Step 9")
  Log["Message"]("Ready to test data")

  Aliases["Epicor"]["MainController"]["Activate"]()
  var gridDashboardPanelChildren = RetrieveGridsMainPanel()

  //Gives time to load the children inside the variable
  Delay(2000)

  var baq1Grid = gridDashboardPanelChildren[1]
  var baq2Grid = gridDashboardPanelChildren[2]

  ClickMenu("Refresh All", "", true)

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
    E10["Refresh"]()
    // Aliases["Epicor"]["MainController"]["Activate"]()
    ActivateForm("", "MainController")

    var gridDashboardPanelChildren = RetrieveGridsMainPanel()
    
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

    var count
    while(true){
      Delay(2500)
      if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
        Log["Message"]("|Sales Order Entry opened")
        break
      }else{
        count++
        Log["Message"]("Trying to open: " + count)
      }

      if (count == 10 && Aliases["Epicor"]["SalesOrderForm"]["Exists"] == false) {
        Log["Error"]("There was a problem opening Sales Order Entry")
      }
    }
  /* end Open Sales order */
}

// Step 11, 12
function TestBeforeDeploy3(){

    // Aliases["Epicor"]["MainController"]["Activate"]()
    ActivateForm("", "MainController")

    E10["Refresh"]()

    var gridDashboardPanelChildren = RetrieveGridsMainPanel()

    //Gives time to load the children inside the variable
    Delay(2000)

    var baq1Grid = gridDashboardPanelChildren[1]
    var baq2Grid = gridDashboardPanelChildren[2]
    
    /* 11- change between orders from second grid and see what happens to Sales Order Entry form that is already opened */
    Log["Message"]("Step 11")
    //Activate Dash window
    // Aliases["Epicor"]["MainController"]["Activate"]()
    ActivateForm("", "MainController")
    Delay(2500)

    //Retrieve cell 'test 1' row 2
    cell = baq2Grid["Rows"]["Item"](1)["Cells"]["Item"](0)
    rect = cell["GetUIElement"]()["Rect"]

    baq2Grid["Click"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
    Log["Message"]("Left click on second record")
    
    //Activate Sales Order Window
    // Aliases["Epicor"]["SalesOrderForm"]["Activate"]()
    ActivateForm("", "SalesOrderForm")
    Delay(1500)
    var salesOrderValue = GetText("txtKeyField")
    
    // cell["Value"] is numeric and salesOrderValue is string
    if( aqConvert["IntToStr"](cell["Value"]) == salesOrderValue){
      Log["Message"]("Order " + cell["Value"] + " from grid 3 is loaded in Sales Order " + salesOrderValue)
    }else{
      Log["Warning"]("Order " + cell["Value"] + " from grid 3 is not loaded in Sales Order " + salesOrderValue)
    }
    
    //Activate Dash window
    // Aliases["Epicor"]["MainController"]["Activate"]()
    ActivateForm("", "MainController")

    //Retrieve cell 'test 2'  row 3
    cell = baq2Grid["Rows"]["Item"](2)["Cells"]["Item"](0)
    rect = cell["GetUIElement"]()["Rect"]

    baq2Grid["Click"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
    Log["Message"]("Left click on second record")

    //Activate Sales Order Window
    Aliases["Epicor"]["SalesOrderForm"]["Activate"]() 
    ActivateForm("", "SalesOrderForm")       
    
    var salesOrderValue = GetText("txtKeyField")
    
    // cell["Value"] is numeric and salesOrderValue is string
    if( aqConvert["IntToStr"](cell["Value"]) == salesOrderValue){
      Log["Message"]("Order " + cell["Value"] + " from grid 3 is loaded in Sales Order " + salesOrderValue)
    }else{
      Log["Warning"]("Order " + cell["Value"] + " from grid 3 is not loaded in Sales Order " + salesOrderValue)
    }

    Delay(2500)

    ClickMenu("File->Exit")

    // Aliases["Epicor"]["MainController"]["Activate"]()
    ActivateForm("", "MainController")       
    ClickMenu("File->Exit")      

    Log["Message"]("Data was validated, no errors found")
}

function DeployDashb(){
  E10["Refresh"]()
  Delay(2500)
  ClickButton("Cancel")
  
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

  DevMode()

  //Call function to create and deploy a dashboard
  NewDashboard(DashbData2["dashboardID"],DashbData2["dashboardCaption"],DashbData2["dashDescription"])
  Log["Message"]("General data for data filled")

  AddQueriesDashboard(baqData1["Id"])
  Log["Message"](baqData1["Id"] + " added")
  
  SaveDashboard()

  Log["Message"]("Dashboard saved")

  var publishedViewsGrid = GetGrid("grdPubViews")
  //checks if the 'Available Views' panel is available
  if(!publishedViewsGrid["Exists"]){
    ClickMenu("View->Published Views")
  }

  //Checks if there is any published view available on the 'Available Views' panel
  if(publishedViewsGrid["Rows"]["Count"] > 0){
    Log["Message"]("BAQs " + publishedViewsGrid["Rows"]["Count"] +  " count of published views")
  }

  /*MODIFY TO ADAPT DRAG FUNCTION*/
  // 13- drag that element with the name of your description from published view in step 4, to the Dashboard Area       
  Log["Message"]("Step 13")
  SetToolButtonText("pnlPubViews", "Filtering", "0_")

  E10["Refresh"]()

  publishedViewsGrid["Drag"](77, 30, 11, -127);

  Log["Message"]("Step 14")
  SaveDashboard()
  Log["Message"]("Dashboard saved")

  //Deactivate Published Views
  if(publishedViewsGrid["Exists"]){
    ClickMenu("View->Published Views")
  }

  Delay(2500)
  DeployDashboard(DashbData2["deploymentOptions"])
  Log["Message"]("Dashboard was deployed")

  ExitDashboard()

  Delay(1000)  
}