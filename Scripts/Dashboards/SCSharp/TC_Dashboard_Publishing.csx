//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_Publishing(){
  
  StartSmartClient()

  Login("epicor","Epicor123") 

  ActivateFullTree()

  ExpandComp("Epicor Education")

  ChangePlant("Main Plant")

  var baqData1 = {
    "Id" : "baqPublishing",
    "Description" : "baqPublishing",
    "Table" : "Customer",
    "Columns" : "CustID,CustNum,Name,State,Country"
  }

  var baqData2 = {
    "Id" : "baqPublishing2",
    "Description" : "baqPublishing2",
    "Table" : "OrderHed",
    "Columns" : "OrderNum,CustNum,PONum"
  }
  var DashbData = {
    "dashboardID" : "DashBPublishing",
    "dashboardCaption" : "Publishing",
    "dashDescription" : "Publishing",
    "generalOptions" : "",
    "baqQuery" : "",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item"
  }

  var DashbData2 = {
    "dashboardID" : "DashBPublishing2",
    "dashboardCaption" : "Publishing2",
    "dashDescription" : "Publishing2",
    "generalOptions" : "",
    "baqQuery" : "baq1",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }

  //--- Creates BAQs ---------------------------------------------------------------------------------------------------------------------------'
    
    //Open Business Activity Query to create BAQ   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    //****** Creating BAQ1 ***************'
    //1- Call function to create a simple BAQ
    CreateSimpleBAQ(baqData1)
    Log["Checkpoint"]("BAQ " + baqData1["Id"] + " created correctly.")

    //****** End of BAQ1 creation ********'

    //Open Business Activity Query to create BAQ   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    //****** Creating BAQ2 ***************'
    //2- Call function to create a simple BAQ
    CreateSimpleBAQ(baqData2)
    Log["Checkpoint"]("BAQ " + baqData2["Id"] + " created correctly.")
    //****** End of BAQ2 creation ******

  //--------------------------------------------------------------------------------------------------------------------------------------------'
    
  //--- CREATE DASHBOARD -----------------------------------------------------------------------------------------------------------------------'
  
    //Open Dashboard   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    //Enable Dashboard Developer Mode  
    DevMode()

    Log["Message"]("Devmode activated on Dashboard")

    //3- Call function to create and deploy a dashboard
    NewDashboard(DashbData["dashboardID"],DashbData["dashboardCaption"],DashbData["dashDescription"])
    Log["Message"]("General data for data filled")

    SaveDashboard()
    Log["Message"]("Dashboard saved - no queries added")

    /***** QUERIES *****/
      /*** ADDING BAQ1 ******/
        AddQueriesDashboard(baqData1["Id"])
        Log["Message"](baqData1["Id"] + " added to the dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        //4- Right click on the query summary and click on Publish View        
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("Right click on " + baqData1["Id"])

        // click 'Publish View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Publish View");
        Log["Message"]("Publish View was selected from Menu")

        Delay(1000)

        //Enter Published caption, Group and Description, and take note of the description
        Aliases["Epicor"]["PublishViewPropsDialog"]["pnlPubViews"]["cmbGroup"]["Keys"]("BaqGroup")
        Aliases["Epicor"]["PublishViewPropsDialog"]["pnlPubViews"]["txtDescription"]["Keys"]("BAQDescription")

        //Click Ok
        Aliases["Epicor"]["PublishViewPropsDialog"]["btnOk"]["Click"]()
        Log["Checkpoint"]("Published view added.")

        //Save dashboard
        SaveDashboard()
        Log["Message"]("Dashboard was saved")

        //5- Clear/Close the dashboard 
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

        //Open Dashboard   
        MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        //retrieve the dashboard created
        OpenDashboard(DashbData["dashboardID"])
        Log["Message"]("Dashboard " + DashbData["dashboardID"] + " loaded")

        //checks if the 'Available Views' panel is available
        if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["Exists"]){
          Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&View|Published Views")
        }

        //Checks if there is any published view available on the 'Available Views' panel
        if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
          Log["Message"]("There are BAQs on published views")
        }

        /*MODIFY TO ADAPT DRAG FUNCTION*/
        //6- Drags the published view created
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);
        Log["Message"]("BAQ from published views was dragged to the queries area")

        //7- Right click on the query summary and click on Properties        
        // rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
        // Log["Message"]("Right click on BAQ1 dragged from published views")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("Right click on " + baqData1["Id"] + "dragged from published views")

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("Properties was selected from " + baqData1["Id"] + " dragged - published views")

        Delay(1000)

        // Active Publish tab and select all columns to be published
        DashboardQueryProperties("Publish")

        Log["Message"]("Publish tab was selected from "+ baqData1["Id"] + " dragged - published views")
        var queryProperties = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["QueryPropsPanel"]["PropertiesPanel_Fill_Panel"]["tcQueryProps"]

        var publishColumns = queryProperties["tabPublish"]["eclpPublishedColumns"]["myCLB"]
        
        //Select Customer_CustNum column to publish
        for (var i = 0; i <= publishColumns["Items"]["Count"] -1; i++) {
          if(publishColumns["Items"]["Item_2"](i)["Text"] == "Customer_CustNum"){
            publishColumns["ClickItem"](i)
            Log["Message"]("BAQ1 - Column(s) "+ publishColumns["Items"]["Item_2"](i)["Text"] +" selected to publish")
            break
          }
        }
        
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("BAQ1 - properties 'ok' button was clicked")

        SaveDashboard()

      /**********************/

      /*** ADDING BAQ2 ******/
      //8- Add second BAQ created 
        AddQueriesDashboard(baqData2["Id"])
        Log["Message"](baqData2["Id"] + " added")

        //Right click on the query and click on Properties        
        // var rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
        // Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](2)
        dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
        Log["Message"]("Right click on " + baqData2["Id"])

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"](baqData2["Id"] + " - Properties was selected")

        //active to 'Filter' tab
        DashboardQueryProperties("Filter")
        Log["Message"](baqData2["Id"] + " - filter tab was selected")

        var ultraGrid = queryProperties["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")

        SelectCellDropdownGrid("ColumnName", "OrderHed_CustNum", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", baqData1["Id"]+"- "+ baqData1["Description"] + ": Customer_CustNum", ultraGrid)
        ultraGrid["Keys"]("[Enter]")

        //click 'OK' button
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("Cells were filled and 'ok' button was clicked")

        //Activate dashboard general panel
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

        //Enable 'refresh all' checkbox
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkInhibitRefreshAll"]["Checked"] = true

        SaveDashboard()
        
        //Operan 'Deploy dashboard' from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Deploy Dashboard")
        
        //9- Click 'Test Application'
        Aliases["Epicor"]["AppBuilderDeployDialog"]["deployOptionsPanel1"]["grpWinAssembly"]["btnTestLaunch"]["Click"]()
        Delay(1500)
        Aliases["Epicor"]["AppBuilderDeployDialog"]["deployOptionsPanel1"]["btnCancel"]["Click"]()
    
      /**********************/
    
    /*******************/

    /*9- Test data before deployment */

      Log["Message"]("Ready to test data")
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh All")
      Log["Message"]("Dashboard refreshed")

      var DashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]

      var gridDashboardPanelChildren = DashboardMainPanel["FindAllChildren"]("FullName", "*grid*", 15)["toArray"]();
      
      //Gives time to load the children inside the variable
      Delay(2000)

      var baq1Grid = gridDashboardPanelChildren[1]
      var baq2Grid = gridDashboardPanelChildren[2]
      
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
    /******************************/

    /* 10- In the second grid from the dashboard right click on any Order from "Order" column
     select open with > Sales Order Entry  (Or do the same with any CustID from the first grid and open it with Customer Maintenance) */
      
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

    /* 11- change between orders from second grid and see what happens to Sales Order Entry form that is already opened */

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
      Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

      //Close Dashboard Panel
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      Log["Checkpoint"]("Data was validated, no errors found")

      DeployDashboard(DashbData["deploymentOptions"])
      Log["Message"]("Dashboard was deployed")
    /* end */

    /* 12- 
       > Close the dashboard from dashboard designer form (Use ""Close All"" button)
       > Click on New button to create a new dashboard
       > Enter Dashboard ID , Caption and Description
       > Select File / New / New Query
       >select any existing BAQ
       > Click Ok
       > Save Dashboard
       > click View > Published Views " */

      //Exit dashboard
      ExitDashboard()
      Log["Checkpoint"]("Dashboard was created correctly.")
        
      //Open Dashboard   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

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
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&View|Published Views")
      }

      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
        Log["Message"]("There are BAQs on available views")
      }

      /*MODIFY TO ADAPT DRAG FUNCTION*/
      // 13- drag that element with the name of your description from published view in step 4, to the Dashboard Area       
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);

      //14- Save Dashboard and deploy 
      SaveDashboard()
      Log["Message"]("Dashboard saved")

      //Deactivate Published Views
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&View|Published Views")

      DeployDashboard(DashbData2["deploymentOptions"])
      Log["Checkpoint"]("Dashboard was deployed")

      //Exit dashboard
      ExitDashboard()

    /* END STEP */
    Delay(1000)
  //--------------------------------------------------------------------------------------------------------------------------------------------'

    DeactivateFullTree()
    Log["Checkpoint"]("FullTree Deactivated")

    CloseSmartClient()
    Log["Checkpoint"]("SmartClient Closed")
}




