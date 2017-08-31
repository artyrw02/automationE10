//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Dashboard_All_companies(){
  
  var MenuData1 = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashMenu",
    "menuName" : "DashMenu",
    "orderSequence" : 3,
    "menuType" : "Dashboard-Assembly",
    "dll" : "TestDashBD",
    "validations" : "Companies,Enable,Web Access"
  }    
  var MenuData2 = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashMen2",
    "menuName" : "DashMenu2",
    "orderSequence" : 4,
    "menuType" : "Dashboard-Assembly",
    "dll" : "TestDashBD",
    "validations" : "Enable,Web Access"
  }
  var baqData1 = {
    "Id" : "baqAllcomp",
    "Description" : "baqAllcomp",
    "Table" : "Customer",
    "Columns" : "Company,CustID,CustNum,Name,Address1",
    "GeneralConfig" : "Shared,Companies"

  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])

    ActivateFullTree()

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates BAQs --------------------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 2
      Step: "On EPIC06 create an external all companies BAQ:
            > Log in to Smart client and open Executive analysis> Business Activity Management> Setup> Business Activity Query
            > enter the following in the ""General"" tab
            QueryID: TestBAQ
            Description: TestBAQ
            Shared: Checked
            All Companies: Checked
            > drag and drop the ""Customer"" table design area in ""Phrase Build"" tab
            > In the Display Fields> Column Select tab for the ""Customer"" table select the Company, CustID, CustNum, Name and Address1 columns and add them to ""Display Columns"" area
            > Save the  BAQ"        
                 
      Result:  Verify the BAQ is created        
    */ 
      MainMenuTreeViewSelect("Epicor USA;Chicago;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      CreateSimpleBAQ(baqData1)

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 3
      Step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result: Verify the developer mode is activated        
    */

      //Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor USA;Chicago;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

    /*
      Step No: 4 & 7
      Step: Create a new Dashboard
            Definition ID: TestDashBD
            Caption: TestDashBD
            Description: TestDashBD"        
      Result: Verify the Dashboard is created       
    */

      NewDashboard("TestDashBD", "TestDashBD", "TestDashBD", "All Companies")
      

    /*
      Step No: 5
      Step: Click on New Query. Search for the BAQ that was previously created and click Ok. Save
      Result: Verify the created query is retrieved and the dashboard is saved        
    */    
      AddQueriesDashboard(baqData1["Id"])
      
      
      SaveDashboard()

    /*
      Step No: 9
      Step: Return to Smart Client. Add a tracker view
      Result: Verify the tracker view appears       
    */ 

      var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
      dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      // dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // click 'Properties' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("'New Tracker View' was selected from Menu")

      Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()

    /*
      Step No: 10 & 11
      Step:  On Dashboard designer click on Tools> Deploy Dashboard       
      Result: Verify the Dashboard Deploy dialog opens        
    */ 
      DeployDashboard("Deploy Smart Client,Generate Web Form")

    /*
      Step No: 12
      Step:  "Go to System Setup>Security Maintenance> Menu Maintenance.
              In Menu Maintenance tree select Main Menu>Sales Management>Customer Relationship Management > Setup, then Select File> New>  New Menu
              Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
              in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. Be sure the ""Enabled"" and ""All companies"" check boxes are selected.
              Click Save."       
      Result: Verify the menu is created with the given parameters        
    */ 
    
      //Open Menu maintenance   
      MainMenuTreeViewSelect("Epicor USA;Chicago;System Setup;Security Maintenance;Menu Maintenance")

      //Creates Menu
      CreateMenu(MenuData1)


    /*
      Step No: 13
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */

      //Restart SmartClient
      RestartSmartClient()

    /*
      Step No: 14
      Step: Go to the created menu on Sales Management>Customer Relationship Management > Setup       
      Result: Verify the menu with the dashboard is loaded   
    */

      MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

    /*
      Step No: 15
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == baqData1["Id"]+': Summary') {
          gridPanel = gridsMainPanel[i]
          break
        }
      }

      if(gridPanel["Rows"]["Count"] > 0){
        Log["Checkpoint"]("Data from grid was populated")
      }else{
        Log["Error"]("Data from grid was not populated")
      }

    /*
      Step No: 16
      Step: Close the dashboard       
      Result: Verify the dashboard is closed        
    */
 
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
   
    /*
      Step No: 17 & 18
      Step: Change to company EPIC07        
            Go to Main Menu>Sales Management>Customer Relationship Management > Setup         
      Result: Verify the menu is also there        
    */
      ExpandComp("Epicor Mexico")

      MainMenuTreeViewSelect("Epicor Mexico;Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

      if(Aliases["Epicor"]["MainController"]["Exists"]){
        if(Aliases["Epicor"]["MainController"]["WndCaption"] == "TestDashBD"){
          Log["Checkpoint"]("Menu is available for this company")
        }
      }else{
        Log["Error"]("Menu is not available for this company")
      }

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    
    /*
      Step No: 19
      Step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      
            Go to Main Menu>Sales Management>Customer Relationship Management > Setup         
      Result: Verify the menu is also there        
    */

      MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

    /*
      Step No: 20
      Step: Search for your dashboard, and retrieve it (using Basic Search or entering it directly and tabbing out)       
      Result: Search for your dashboard, and retrieve it (using Basic Search or entering it directly and tabbing out)         
              Verify  that the All companies check box is checked , also that you get the message "Dashboards created in remote company may not be modified"  and click Ok        
    */

      OpenDashboard("TestDashBD")

      // Verify checkbox 'All companies' 
      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]){
       Log["Message"]("Checkbox 'All Companies is checked")
      }else{
        Log["Error"]("Checkbox 'All Companies is not checked")
      }

      //Verify dialog message
      if(Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]){
       Log["Message"]("message 'Dashboards created in remote company may not be modified' is displayed")
       Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["Click"]()
      }else{
        Log["Error"]("message 'Dashboards created in remote company may not be modified' is not displayed")
      }

    /*
      Step No: 21
      Step: Clear the form        
      Result: Verify the form is cleared        
    */
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Close All")
      Aliases["Epicor"]["dlgWarning"]["btnOK"]["Click"]()

    /*
      Step No: 22
      Step: Create a new Dashboard, with the name ID as the previous one and also fill up the Caption and Description
            Definition ID: TestDashBD
            Caption: TestDashBD
            Description: TestDashBD"        

      Result: Verify you get the message: Warning - Dashboard: TestA set for All Companies already exists.  New Dashboard will only be available in current Company: EPIC07
              and click Ok
              Also verify that after getting this message the All companies check box appears disabled"       
    */

      NewDashboard("TestDashBD", "TestDashBD", "TestDashBD")

      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["ReadOnly"]){
        Log["Message"]("Checkbox 'All Companies' is disabled")
      }else{
        Log["Error"]("Checkbox 'All Companies' is not disabled")
      }

      /*
        Step No: 23 & 24
        Step: Add the same query. Save your dashboard       
        Result: Verify the dashboard is saved       
      */    
        AddQueriesDashboard(baqData1["Id"])
        
        SaveDashboard()

      /*
        Step No: 26 & 27
        Step: On Dashboard designer click on Tools> Deploy Dashboard   
              Check Deploy Smart Client Application and Generate Web Form. Click Deploy       
        Result: Verify the Dashboard Deploy dialog opens        
                Verify the dashboard is deployed without problems       
      */  
        DeployDashboard("Deploy Smart Client,Generate Web Form")

      /*
        Step No: 28
        Step: Go to System Setup>Security Maintenance> Menu Maintenance.
              In Menu Maintenance tree select Main Menu>Sales Management>Customer Relationship Management > Setup, then Select File> New>  New Menu
              Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
              in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. Be sure the Enabled check box is selected. Click Save."        
                     
        Result: Verify the menu is created with the given parameters        
      */ 
 
      //Open Menu maintenance   
      MainMenuTreeViewSelect("Epicor Mexico;System Setup;Security Maintenance;Menu Maintenance")

      //Creates Menu
      CreateMenu(MenuData2)


    /*
      Step No: 29
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */

      //Restart SmartClient
      RestartSmartClient()      

    /*
      Step No: 30
      Step: On EPIC07 go to Main Menu>Sales Management>Customer Relationship Management > Setup and open the menu       
      Result: Verify the menu with the dashboard you just deploy is loaded        

    */    
      ExpandComp("Epicor Mexico")

      MainMenuTreeViewSelect("Epicor Mexico;Sales Management;Customer Relationship Management;Setup;"+MenuData2["menuName"])

   /*
      Step No: 31
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == baqData1["Id"]+': Summary') {
          gridPanel = gridsMainPanel[i]
          break
        }
      }

      if(gridPanel["Rows"]["Count"] > 0){
        Log["Checkpoint"]("Data from grid was populated")
      }else{
        Log["Error"]("Data from grid was not populated")
      }

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")      

    /*
      Step No: 32
      Step: On EPIC06 go to Main Menu>Sales Management>Customer Relationship Management > Setup and open the menu       
      Result: Verify the menu with the dashboard you just deploy is loaded        

    */    

      ExpandComp("Epicor USA")
      ChangePlant("Chicago")
      MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

    /*
      Step No: 33
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == baqData1["Id"]+': Summary') {
          gridPanel = gridsMainPanel[i]
          break
        }
      }

      if(gridPanel["Rows"]["Count"] > 0){
        Log["Checkpoint"]("Data from grid was populated")
      }else{
        Log["Error"]("Data from grid was not populated")
      }
 
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")      

    /*
      Step No: 34
      Step: Go to Main Menu> System Management> Upgrade/Mass Regeneration       
      Result: Verify the form loads       
    */
      ExpandComp("Epicor USA")
      ChangePlant("Chicago")
      MainMenuTreeViewSelect("Epicor USA;Chicago;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

    /*
      Step No: 35
      Step: Search for your dashboard, and retrieve it entering the ID directly and tabbing out
      Result: Verify the info from the dashboard is displayed       
    */
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("TestDashBD")
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

    /*
      Step No: 36
      Step: Click on Definition ID button       
      Result: Verify the Search opens       
    */      
      Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["epiButton1"]["Click"]()

    /*
      Step No: 37
      Step: Click on Options button
      Result: -          
    */          

      Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnOptions"]["Click"]()

    /*
      Step No: 38
      Step: On Maximum Rows Returned enter 20 and click Ok   
      Result: -          
    */   
               
      Aliases["Epicor"]["SearchOptionsForm"]["epiPanel1"]["neRecordCnt"]["Text"] = 20
      Aliases["Epicor"]["SearchOptionsForm"]["ultraStatusBar1"]["btnOK"]["Click"]()

    /*
      Step No: 39
      Step: Click on Deployed Dashboards and System Dashboards check boxes until they appear filled by a black dot
      Result: -          
    */   

      var sortdialogDashboard = Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["etpBasic"]["basicPanel1"]["gbSortBy"]
      sortdialogDashboard["chkInUse"]["CheckState"] = "Indeterminate"
      sortdialogDashboard["chkSystem"]["CheckState"] = "Indeterminate"

    /*
      Step No: 40
      Step: Click Search     
      Result: -Verify the first 20 results are thrown       
    */   

      Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()

      var gridSearchResults = Aliases["Epicor"]["DashboardSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

      if(gridSearchResults["Rows"]["Count"] <= 20){
        Log["Message"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
      }else{
        Log["Error"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
      }

    /*
      Step No: 41
      Step: Search for your dashboard TestDashDB        
      Result: Verify the info of the dashboard is retrieved       
    */   
      //Starts with field to write dashboard created
      Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["etpBasic"]["basicPanel1"]["gbSortBy"]["txtStartWith1"]["Keys"]("TestDashBD")
      Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()

      var idColumnGrid = getColumn(gridSearchResults, "ID")

      for (var i = 0; i < gridSearchResults["Rows"]["Count"]; i++) {
        if(Aliases["Epicor"]["DashboardSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]["Rows"]["Item"](i)["Cells"]["Item"](idColumnGrid)["Text"]["OleValue"] == "TestDashBD"){
            Aliases["Epicor"]["DashboardSearchForm"]["ultraStatusBar2"]["btnOK"]["Click"]()
            break
        }else{
          Log["Error"]("Dashboard TestDashBD not found")
        }
      }
     
      if(Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Text"] != ""){
        Log["Message"]("Dashboard TestDashBD loaded")
      }else{
        Log["Error"]("Dashboard TestDashBD was not loaded")
      }

    /*
      Step No: 42
      Step: Click on Actions>Modify Dashboard        
      Result: Verify the dashboard designer is opened       
    */  
      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Modify Dashboard")

      if(Aliases["Epicor"]["Dashboard"]["Exists"] == true && Aliases["Epicor"]["Dashboard"]["WndCaption"] == "TestDashBD"){
        Log["Message"]("dashboard designer is opened")
      }else{
        Log["Error"]("dashboard designer was not opened")
      }

    /*
      Step No: 43
      Step: Click File> Copy Dashboard        
      Result: Verify the Copy Dashboard dialog opens        
    */  

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard")

    /*
      Step No: 44
      Step: Enter Definition ID: TestDashBD-2. Click Ok
      Result: Verify the dashboard is copied
    */  
      Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("TestDashBD-2")
      Aliases["Epicor"]["CopyDashboardForm"]["btnOkay"]["Click"]()

    /*
      Step No: 45
      Step: Save your dashboard       
      Result: Verify the dashboard is saved
    */      

      SaveDashboard()
      ExitDashboard()

      // var test2 = QueryDatabaseDashboards("TestDashBD-2")
    /*
      Step No: 47
      Step: On Dashboard Maintenance search for system dashboard JobStatusPlus and retrieve it        
      Result: Verify the info from the dashboard is displayed       
    */      
      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()

      Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("JobStatusPlus")
      Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

    /*
      Step No: 48
      Step: Click on Actions>Modify Dashboard
      Result: Verify the info from the dashboard is displayed       
    */
      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Modify Dashboard")
        
      var count = 0
      while(true){
        var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

        if (windowsExceptionDialogBtn[0] != null && windowsExceptionDialogBtn[0] != null || windowsExceptionDialogBtn[0] != undefined && windowsExceptionDialogBtn[0] != undefined) {
          if(windowsExceptionDialogBtn[0]["Exists"]){
            Log["Message"]("Validating Warning - System Dashboards may not be modified. - Clicked OK on message ")
            windowsExceptionDialogBtn[0]["Click"]()
            break
          }
        }
        count++

        if(count == 5){
          break
          Runner["Stop"]()
          Log["Error"]("Check")
        }
      }
      
        
    /*
      Step No: 49
      Step: Click File> Copy Dashboard        
      Result: Verify the Copy Dashboard dialog opens        
    */  

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard")

    /*
      Step No: 50
      Step: Save your dashboard        
      Result: Verify the dashboard is saved
    */  
      Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("TestDashBD-3")
      Aliases["Epicor"]["CopyDashboardForm"]["btnOkay"]["Click"]()      
        
    /*
      Step No: 51
      Step: Save your dashboard       
      Result: Verify the dashboard is saved
    */      

      SaveDashboard()

    // Step No: 54
    //Query on SQL the dashboards

      var test1 = QueryDatabaseDashboards("TestDashBD")
      Log["Message"]("Query with Dashboard ID TestDashBD retrieved " + test1["RecordCount"] + " records.")

      var test2 = QueryDatabaseDashboards("TestDashBD-3")
      Log["Message"]("Query with Dashboard ID TestDashBD-3 retrieved " + test2["RecordCount"] + " records.")

    /*
      Step No: 53
      Step: Click File>Delete Dashboard Definition 
      Result: Verify the dashboard is deleted       
    */      

      DeleteDashboard("TestDashBD-3")
      
      var test3 = QueryDatabaseDashboards("TestDashBD-3")
      Log["Message"]("Query with Dashboard ID TestDashBD-3 retrieved " + test3["RecordCount"] + " records.")

      ExitDashboard()
      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
  //-------------------------------------------------------------------------------------------------------------------------------------------' 

   DeactivateFullTree()

   CloseSmartClient()

}