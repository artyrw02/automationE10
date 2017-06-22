//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_Publishing(){
  
  StartSmartClient()

  Login("epicor","Epicor123", "Classic") 

  ActivateFullTree()

  //--- Creates BAQs ---------------------------------------------------------------------------------------------------------------------------'
    
    //Open Business Activity Query to create BAQ   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    //****** Creating BAQ1 ***************'
    //Call function to create a simple BAQ
    CreateSimpleBAQ("baq1", "baq", "Customer", "CustID,CustNum,Name,State,Country")

    //****** End of BAQ1 creation ********'

    //Open Business Activity Query to create BAQ   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    //****** Creating BAQ2 ***************'
    //Call function to create a simple BAQ
    CreateSimpleBAQ("baq2", "baq", "OrderHed", "OrderNum,CustNum,PONum")

    //****** End of BAQ2 creation ******

  //--------------------------------------------------------------------------------------------------------------------------------------------'
    
  //--- CREATE DASHBOARD -----------------------------------------------------------------------------------------------------------------------'
  
    //Open Dashboard   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    //Enable Dashboard Developer Mode  
    DevMode()

    Log["Message"]("Devmode activated on Dashboard")

    //Call function to create and deploy a dashboard
    NewDashboard("DashB", "Dashb", "Dashb")
    Log["Message"]("General data for data filled")

    SaveDashboard("Dashb", "Dashb")
    Log["Message"]("Dashboard saved - no queries added")

    /***** QUERIES *****/
      /*** ADDING BAQ1 ******/
        AddQueriesDashboard("baq1")
        Log["Message"]("BAQ1 added to the dashboard")

        //Right click on the query summary and click on Publish View        
        var rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
        Log["Message"]("BAQ1 - right click")

        // click 'Publish View' option from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Publish View");
        Log["Message"]("BAQ1 Summary - Publish View was selected from Menu")

        Delay(1000)

        //Enter Published caption, Group and Description, and take note of the description
        Aliases["Epicor"]["PublishViewPropsDialog"]["pnlPubViews"]["cmbGroup"]["Keys"]("BaqGroup")
        Aliases["Epicor"]["PublishViewPropsDialog"]["pnlPubViews"]["txtDescription"]["Keys"]("BAQDescription")

        //Click Ok
        Aliases["Epicor"]["PublishViewPropsDialog"]["btnOk"]["Click"]()

        //Save dashboard
        SaveDashboard("Dashb","Dashb")
        Log["Message"]("Dashboard was saved")

        // Clear/Close the dashboard 
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

        //Open Dashboard   
        MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        //retrieve the dashboard created
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("Dashb")
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")
        Log["Message"]("Dashboard 'DashB' loaded")

        //checks if the 'Available Views' panel is available
        if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["Exists"]){
          Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&View|Published Views")
        }

        //Checks if there is any published view available on the 'Available Views' panel
        if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
          Log["Message"]("There are BAQs on published views")
        }

        /*MODIFY TO ADAPT DRAG FUNCTION*/
        //Drags the published view created
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);
        Log["Message"]("BAQ from published views was dragged to the queries area")

        //Right click on the query summary and click on Properties        
        rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](1)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
        Log["Message"]("Right click on BAQ1 dragged from published views")

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("Properties was selected from BAQ1 dragged - published views")

        Delay(1000)

        // Active Publish tab and select all columns to be published
        DashboardQueryProperties("Publish")

        Log["Message"]("Publish tab was selected from BAQ1 dragged - published views")
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

      /**********************/

      /*** ADDING BAQ2 ******/
        AddQueriesDashboard("baq2")
        Log["Message"]("BAQ2 added")

        //Right click on the query and click on Properties        
        var rect = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["Nodes"]["Item"](0)["Nodes"]["Item"](2)["UIElement"]["Rect"]
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
        Log["Message"]("Right click on BAQ2")

        // click 'Properties' option
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
        Log["Message"]("BAQ2 - Properties was selected")

        //active to 'Filter' tab
        DashboardQueryProperties("Filter")
        Log["Message"]("BAQ1 - filter tab was selected")

        var ultraGrid = queryProperties["tabFilter"]["WinFormsObject"]("pnlFilter")["WinFormsObject"]("ultraGrid1")

        SelectCellDropdownGrid("ColumnName", "OrderHed_CustNum", ultraGrid)
        SelectCellDropdownGrid("Condition", "=", ultraGrid)
        SelectCellDropdownGrid("Value", "baq1- baq: Customer_CustNum", ultraGrid)
        ultraGrid["Keys"]("[Enter]")

        //click 'OK' button
        Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
        Log["Message"]("Cells were filled and 'ok' button was clicked")

        //Activate dashboard general panel
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

        //Enable 'refresh all' checkbox
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkInhibitRefreshAll"]["Checked"] = true

        SaveDashboard("Dashb", "Dashb")
        
        //Operan 'Deploy dashboard' from menu
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Deploy Dashboard")
        
        //Click 'Test Application'
        Aliases["Epicor"]["AppBuilderDeployDialog"]["deployOptionsPanel1"]["grpWinAssembly"]["btnTestLaunch"]["Click"]()
        Delay(1500)
        Aliases["Epicor"]["AppBuilderDeployDialog"]["deployOptionsPanel1"]["btnCancel"]["Click"]()
    
      /**********************/
    
    /*******************/


    /*Test data before deployment */

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
        Log["Message"]("Data from BAQ1 row 0 matches the result on baq2")
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
        Log["Message"]("Data from BAQ1 row 1 matches the result on baq2")
      }

      Delay(1500)
    /******************************/

    /* In the second grid from the dashboard right click on any Order from "Order" column
     select open with > Sales Order Entry  (Or do the same with any CustID from the first grid and open it with Customer Maintenance) */
      
      //Retrieve cell      
      cell = baq2Grid["Rows"]["Item"](0)["Cells"]["Item"](0)
      rect = cell["GetUIElement"]()["Rect"]

      baq2Grid["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Log["Message"]("Cell from grid 2 (baq 2) was selected")

      // click 'Properties' option
      baq2Grid["UltraPopupMenu"]["Click"]("Open With...|Sales Order Entry");
      Log["Message"]("|Sales Order Entry opened")

      Delay(2500)
    /* end Open Sales order */

    /* change between orders from second grid and see what happens to Sales Order Entry form that is already opened */

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

      DeployDashboard("Dashb","Dashb", "Deploy Smart Client,Add Favorite Item")
      Log["Message"]("Dashboard was deployed")
    /* end */

    /* 
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
        
      //Open Dashboard   
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      //Enable Dashboard Developer Mode  
      DevMode()
    
      //Call function to create and deploy a dashboard
      NewDashboard("DashB2", "Dashb2", "Dashb2")
      Log["Message"]("General data for data filled")

      AddQueriesDashboard("baq1")
      Log["Message"]("BAQ1 added")
      
      SaveDashboard("Dashb2", "Dashb2")
      Log["Message"]("Dashboard saved")

      if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["Exists"]){
        Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&View|Published Views")
      }

      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Rows"]["Count"] > 0){
        Log["Message"]("There are BAQs on available views")
      }

      /*MODIFY TO ADAPT DRAG FUNCTION*/
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow2"]["pnlPubViews"]["grdPubViews"]["Drag"](77, 30, 11, -127);

      SaveDashboard("Dashb2", "Dashb2")
      Log["Message"]("Dashboard saved")

      DeployDashboard("DashB2","Dashb", "Deploy Smart Client,Add Favorite Item,Generate Web Form")
      Log["Message"]("Dashboard was deployed")

      //Exit dashboard
      ExitDashboard()

    /* END STEP */
    Delay(1000)
  //--------------------------------------------------------------------------------------------------------------------------------------------'

  DeactivateFullTree()

  CloseSmartClient()
}




