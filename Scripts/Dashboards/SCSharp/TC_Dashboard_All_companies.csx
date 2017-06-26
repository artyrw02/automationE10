//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
var continueTest = true

function TC_Dashboard_Tracker_Views_2(){
  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login("epicor","Epicor123", "Classic") 

    ActivateFullTree()
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
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      CreateSimpleBAQ("baqTest", "baqDescription", "Customer", "Company,CustID,CustNum,Name,Address1", "Shared,All Companies")

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 3
      Step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result: Verify the developer mode is activated        
    */

      //Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

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
      AddQueriesDashboard("baqTest")
      
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
      DeployDashboard("TestDashBD", "TestDashBD", "Deploy Smart Client,Generate Web Form")

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
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

      //Creates Menu
      CreateMenu("Main Menu>Sales Management>Customer Relationship Management>Setup", "DashMenu", "Dash Menu", 1, "Dashboard-Assembly", "TestDashBD", "All Companies,Enable,Web Access")


    /*
      Step No: 13
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */

      //Restart SmartClient
      RestartSmartClient("Classic")

    /*
      Step No: 14
      Step: Go to the created menu on Sales Management>Customer Relationship Management > Setup       
      Result: Verify the menu with the dashboard is loaded   
    */

      MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Dash Menu")

    /*
      Step No: 15
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == 'baqTest: Summary') {
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

      MainMenuTreeViewSelect("Epicor Mexico;Sales Management;Customer Relationship Management;Setup;Dash Menu")

      if(Aliases["Epicor"]["MainController"]["Exists"]){
        if(Aliases["Epicor"]["MainController"]["WndCaption"] == "TestDashBD"){
          Log["Checkpoint"]("Menu is available for this company")
        }
      }else{
        Log["Error"]("Menu is not available for this company")
      }

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

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"] = true

      //Finds the windows dialog and button object
      var windowsExceptionDialog = Aliases["Epicor"]["FindAllChildren"]("FullName", "*TestDashBD*", 15)["toArray"]();
      var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

      //Validates if the checkbox is disabled
      if(windowsExceptionDialog[0]["Exists"]){
        Log["Message"]("Checkbox 'All Companies' is disabled")
        windowsExceptionDialogBtn[0]["Click"]()
      }else{
        Log["Error"]("Checkbox 'All Companies' is not disabled")
      }

    /*
      Step No: 23 & 24
      Step: Save your dashboard       
      Result: Verify the dashboard is saved       
    */    
      AddQueriesDashboard("baqTest")
      
      SaveDashboard()

    /*
      Step No: 26 & 27
      Step: On Dashboard designer click on Tools> Deploy Dashboard   
            Check Deploy Smart Client Application and Generate Web Form. Click Deploy       
      Result: Verify the Dashboard Deploy dialog opens        
              Verify the dashboard is deployed without problems       
    */  
      DeployDashboard("TestDashBD", "TestDashBD", "Deploy Smart Client,Generate Web Form")

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
      CreateMenu("Main Menu>Sales Management>Customer Relationship Management>Setup", "DashMenu2", "Dash Menu2", 2, "Dashboard-Assembly", "TestDashBD", "Enable,Web Access")


    /*
      Step No: 29
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */

      //Restart SmartClient
      RestartSmartClient("Classic")      

    /*
      Step No: 30
      Step: On EPIC07 go to Main Menu>Sales Management>Customer Relationship Management > Setup and open the menu       
      Result: Verify the menu with the dashboard you just deploy is loaded        

    */    
      ExpandComp("Epicor Mexico")

      MainMenuTreeViewSelect("Epicor Mexico;Sales Management;Customer Relationship Management;Setup;Dash Menu2")

 /*
      Step No: 31
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == 'baqTest: Summary') {
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

      MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Dash Menu")

    /*
      Step No: 33
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var gridsMainPanel = RetrieveGridsMainPanel()

      var gridPanel
  
      for (var i = 0; i < gridsMainPanel["length"]; i++) {
        if (gridsMainPanel[i]["WndCaption"] == 'baqTest: Summary') {
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

      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

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

      var test2 = QueryDatabaseDashboards("TestDashBD-3")
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

      SaveDashboard()
      ExitDashboard()

      //Finds the windows dialog and button object
      // var windowsExceptionDialog = Aliases["Epicor"]["FindAllChildren"]("FullName", "*System Dashboard Warning*", 15)["toArray"]();
      var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

      windowsExceptionDialogBtn[0]["Click"]()

    /*
      Step No: 51
      Step: Click File> Copy Dashboard        
      Result: Verify the Copy Dashboard dialog opens        
    */  

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard")

    /*
      Step No: 52
      Step: Enter Definition ID: TestDashBD-3. Click Ok
      Result: Verify the dashboard is copied
    */  
      Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("TestDashBD-3")
      Aliases["Epicor"]["CopyDashboardForm"]["btnOkay"]["Click"]()

    /*
      Step No: 53
      Step: Save your dashboard       
      Result: Verify the dashboard is saved
    */      

      SaveDashboard()

    // Step No: 54
    //Query on SQL the dashboards

      var test1 = QueryDatabaseDashboards("TestDashBD")
      var test2 = QueryDatabaseDashboards("TestDashBD-3")

    /*
      Step No: 53
      Step: Click File>Delete Dashboard Definition 
      Result: Verify the dashboard is deleted       
    */      

      DeleteDashboard("TestDashBD-3")
      
      var test2 = QueryDatabaseDashboards("TestDashBD-3")
  
      if(test2["RecordCount"] == 0){
        Log["Message"]("Query with Dashboard ID TestDashBD-3 retrieved " + test2["RecordCount"] + " records.")
        return test2["RecordCount"]
      }else{
        Log["Message"]("Query with Dashboard ID TestDashBD-3 retrieved " + test2["RecordCount"] + " records.")
        return test2["RecordCount"]
      }
  
  