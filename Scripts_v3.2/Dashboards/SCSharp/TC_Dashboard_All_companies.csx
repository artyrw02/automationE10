//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT ControlFunctions
//USEUNIT Data_Dashboard_All_companies

function TC_Dashboard_All_companies(){
  

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
      Log["Message"]("Step 2")

      MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      CreateSimpleBAQ(baqData1)

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 3
      Step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result: Verify the developer mode is activated        
    */
      Log["Message"]("Step 3")

      //Navigate and open Dashboard
      MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      //Enable Dashboard Developer Mode  
      DevMode()

    /*
      Step No: 4
      Step: Create a new Dashboard
            Definition ID: TestDashBD
            Caption: TestDashBD
            Description: TestDashBD"        
      Result: Verify the Dashboard is created       
    */
      Log["Message"]("Step 4")
      NewDashboard(dashb1, dashb1, dashb1, dashb1Config)
      

    /*
      Step No: 5
      Step: Click on New Query. Search for the BAQ that was previously created and click Ok. Save
      Result: Verify the created query is retrieved and the dashboard is saved        
    */    
      Log["Message"]("Step 5")
      AddQueriesDashboard(baqData1["Id"])
            
      SaveDashboard()

    /*
      Step No: 9
      Step: Return to Smart Client. Add a tracker view
      Result: Verify the tracker view appears       
    */ 
      Log["Message"]("Step 9")
      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
   
      var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)
      dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      // dashboardTree["ClickR"](rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Log["Message"]("BAQ - right click")

      // click 'Properties' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("'New Tracker View' was selected from Menu")

      ClickButton("OK")

    /*
      Step No: 10 & 11
      Step:  On Dashboard designer click on Tools> Deploy Dashboard       
      Result: Verify the Dashboard Deploy dialog opens        
    */ 
      Log["Message"]("Step 10, 11")
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
      Log["Message"]("Step 12")
      //Open Menu maintenance   
      MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")

      //Creates Menu
      CreateMenu(MenuData1)


    /*
      Step No: 13
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */
      Log["Message"]("Step 13")
      //Restart SmartClient
      RestartSmartClient()

    /*
      Step No: 14
      Step: Go to the created menu on Sales Management>Customer Relationship Management > Setup       
      Result: Verify the menu with the dashboard is loaded   
    */
      Log["Message"]("Step 14")
      MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

    /*
      Step No: 15
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */
      Log["Message"]("Step 15")

      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      ClickMenu("Edit->Refresh")

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
      Log["Message"]("Step 16")
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      ClickMenu("File->Exit")
   
    /*
      Step No: 17 & 18
      Step: Change to company EPIC07        
            Go to Main Menu>Sales Management>Customer Relationship Management > Setup         
      Result: Verify the menu is also there        
    */
      Log["Message"]("Step 17, 18")
      ExpandComp(company2)

      MainMenuTreeViewSelect(treeMainPanel2 + "Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

      if(Aliases["Epicor"]["MainController"]["Exists"]){
        if(Aliases["Epicor"]["MainController"]["WndCaption"] == dashb1){
          Log["Checkpoint"]("Menu is available for this company")
        }
      }else{
        Log["Error"]("Menu is not available for this company")
      }

      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      ClickMenu("File->Exit")

    /*
      Step No: 19
      Step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      
            Go to Main Menu>Sales Management>Customer Relationship Management > Setup         
      Result: Verify the menu is also there        
    */
      Log["Message"]("Step 19")
      MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"]("Dashboard opened")
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Message"]("DevMode activated")

    /*
      Step No: 20
      Step: Search for your dashboard, and retrieve it (using Basic Search or entering it directly and tabbing out)       
      Result: Search for your dashboard, and retrieve it (using Basic Search or entering it directly and tabbing out)         
              Verify  that the All companies check box is checked , also that you get the message "Dashboards created in remote company may not be modified"  and click Ok        
    */
      Log["Message"]("Step 20")
      OpenDashboard(dashb1)

      //Verify dialog message
      if(Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]){
       Log["Message"]("message 'Dashboards created in remote company may not be modified' is displayed")
       // Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["Click"]()
       ClickButton("OK")
      }else{
        Log["Error"]("message 'Dashboards created in remote company may not be modified' is not displayed")
      }

      OpenPanelTab("General")

      // Verify checkbox 'All companies' 
      // if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]){
      var chkAllCompanies = GetCheckbox("chkAllCompanies")

      if(chkAllCompanies["Checked"]){
       Log["Message"]("Checkbox 'All Companies is checked")
      }else{
        Log["Error"]("Checkbox 'All Companies is not checked")
      }      
    /*
      Step No: 21
      Step: Clear the form        
      Result: Verify the form is cleared        
    */
      Log["Message"]("Step 21")
      CloseDashboard()

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
      Log["Message"]("Step 22")
      NewDashboard(dashb1, dashb1, dashb1)

      var chkAllCompanies = GetCheckbox("chkAllCompanies")

      if(chkAllCompanies["ReadOnly"]){
        Log["Message"]("Checkbox 'All Companies' is disabled")
      }else{
        Log["Error"]("Checkbox 'All Companies' is not disabled")
      }

      /*
        Step No: 23 & 24
        Step: Add the same query. Save your dashboard       
        Result: Verify the dashboard is saved       
      */    
        Log["Message"]("Step 23, 24")
        AddQueriesDashboard(baqData1["Id"])
        
        SaveDashboard()

      /*
        Step No: 26 & 27
        Step: On Dashboard designer click on Tools> Deploy Dashboard   
              Check Deploy Smart Client Application and Generate Web Form. Click Deploy       
        Result: Verify the Dashboard Deploy dialog opens        
                Verify the dashboard is deployed without problems       
      */  
        Log["Message"]("Step 26, 27")
        DeployDashboard("Deploy Smart Client,Generate Web Form")

        ExitDashboard()

      /*
        Step No: 28
        Step: Go to System Setup>Security Maintenance> Menu Maintenance.
              In Menu Maintenance tree select Main Menu>Sales Management>Customer Relationship Management > Setup, then Select File> New>  New Menu
              Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
              in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. Be sure the Enabled check box is selected. Click Save."        
                     
        Result: Verify the menu is created with the given parameters        
      */ 
      Log["Message"]("Step 28")
      //Open Menu maintenance   
      MainMenuTreeViewSelect(treeMainPanel2 + "System Setup;Security Maintenance;Menu Maintenance")

      Delay(2000)

      //Creates Menu
      CreateMenu(MenuData2)

    /*
      Step No: 29
      Step: Restart Smart Client
      Result: Verify the Smart Client is restarted        
    */
      Log["Message"]("Step 29")
      //Restart SmartClient
      RestartSmartClient()      

    /*
      Step No: 30
      Step: On EPIC07 go to Main Menu>Sales Management>Customer Relationship Management > Setup and open the menu       
      Result: Verify the menu with the dashboard you just deploy is loaded        

    */    
      Log["Message"]("Step 30")
      ExpandComp(company2)

      MainMenuTreeViewSelect(treeMainPanel2 + "Sales Management;Customer Relationship Management;Setup;"+MenuData2["menuName"])

   /*
      Step No: 31
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */
      Log["Message"]("Step 31")
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      ClickMenu("Edit->Refresh")

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

      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")   
      ClickMenu("File->Exit")   

    /*
      Step No: 32
      Step: On EPIC06 go to Main Menu>Sales Management>Customer Relationship Management > Setup and open the menu       
      Result: Verify the menu with the dashboard you just deploy is loaded        

    */    
      Log["Message"]("Step 32")
      ExpandComp(company1)
      ChangePlant(plant1)
      MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;"+MenuData1["menuName"])

    /*
      Step No: 33
      Step: Click Refresh       
      Result: Verify the dashboard is populated with customers data       
    */
      Log["Message"]("Step 33")
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      ClickMenu("Edit->Refresh")

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
 
      // Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")      
      ClickMenu("File->Exit")

    /*
      Step No: 34
      Step: Go to Main Menu> System Management> Upgrade/Mass Regeneration       
      Result: Verify the form loads       
    */
      Log["Message"]("Step 34")
      ExpandComp(company1)
      ChangePlant(plant1)
      MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

    /*
      Step No: 35
      Step: Search for your dashboard, and retrieve it entering the ID directly and tabbing out
      Result: Verify the info from the dashboard is displayed       
    */
      Log["Message"]("Step 35")
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"](dashb1)
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")
      EnterText("txtKeyField", dashb1 + "[Tab]", "Adding ID of dashboard")
        
      //Description field used to validate
      var descrField = GetTextBox("epiTextBox1")

      if(descrField["Text"]["OleValue"] != ""){
        Log["Checkpoint"]("Data from dashboard " + dashb1 + " was loaded correctly")
      }else {
        Log["Error"]("Data from dashboard " + dashb1 + " was not loaded correctly")
      }

      ClickMenu("Edit->Clear")
      ClickButton("Yes")
    /*

      Step No: 36
      Step: Click on Definition ID button       
      Result: Verify the Search opens       
    */      
      Log["Message"]("Step 36")
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["epiButton1"]["Click"]()
      ClickButton("Dashboard ID...")

    /*
      Step No: 37
      Step: Click on Options button
      Result: -          
    */          
      Log["Message"]("Step 37")
      // Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnOptions"]["Click"]()
      ClickButton("Options")

    /*
      Step No: 38
      Step: On Maximum Rows Returned enter 20 and click Ok   
      Result: -          
    */   
      Log["Message"]("Step 38")           
      // Aliases["Epicor"]["SearchOptionsForm"]["epiPanel1"]["neRecordCnt"]["Text"] = 20
      // Aliases["Epicor"]["SearchOptionsForm"]["ultraStatusBar1"]["btnOK"]["Click"]()
      EnterText("neRecordCnt", 20, "Record count for search parameters")
      ClickButton("OK")

    /*
      Step No: 39
      Step: Click on Deployed Dashboards and System Dashboards check boxes until they appear filled by a black dot
      Result: -          
    */   
      Log["Message"]("Step 39")
      // var sortdialogDashboard = Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["etpBasic"]["basicPanel1"]["gbSortBy"]
      // sortdialogDashboard["chkInUse"]["CheckState"] = "Indeterminate"
      // sortdialogDashboard["chkSystem"]["CheckState"] = "Indeterminate"
      CheckboxState("chkInUse", "Indeterminate")
      CheckboxState("chkSystem", "Indeterminate")

      Delay(2000)
    /*
      Step No: 40
      Step: Click Search     
      Result: -Verify the first 20 results are thrown       
    */   
    Log["Message"]("Step 40")
      // Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()
      ClickButton("Search")

      var gridSearchResults = Aliases["Epicor"]["DashboardSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

      if(gridSearchResults["Rows"]["Count"] <= 20){
        Log["Checkpoint"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
      }else{
        Log["Error"]("Search results returned " + gridSearchResults["Rows"]["Count"] + " records")
      }

    /*
      Step No: 41
      Step: Search for your dashboard TestDashDB        
      Result: Verify the info of the dashboard is retrieved       
    */   
    Log["Message"]("Step 41")
      //Starts with field to write dashboard created
      // Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["etpBasic"]["basicPanel1"]["gbSortBy"]["txtStartWith1"]["Keys"](dashb1)
      // Aliases["Epicor"]["DashboardSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()
      EnterText("txtStartWith1", dashb1, "Adding dashboard to search")
      ClickButton("Search")

      var idColumnGrid = getColumn(gridSearchResults, "ID")

      for (var i = 0; i < gridSearchResults["Rows"]["Count"]; i++) {
        if(Aliases["Epicor"]["DashboardSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]["Rows"]["Item"](i)["Cells"]["Item"](idColumnGrid)["Text"]["OleValue"] == dashb1){
            // Aliases["Epicor"]["DashboardSearchForm"]["ultraStatusBar2"]["btnOK"]["Click"]()
            ClickButton("OK")
            break
        }else{
          Log["Error"]("Dashboard " + dashb1 + " not found")
        }
      }
      
      var descrField = GetTextBox("epiTextBox1")

      if(Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Text"] != ""){
        Log["Message"]("Dashboard " + dashb1 + " loaded")
      }else{
        Log["Error"]("Dashboard " + dashb1 + " was not loaded")
      }

    /*
      Step No: 42
      Step: Click on Actions>Modify Dashboard        
      Result: Verify the dashboard designer is opened       
    */  
      Log["Message"]("Step 42")
      // Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Modify Dashboard")
      ClickMenu("Actions->Modify Dashboard")

      if(Aliases["Epicor"]["Dashboard"]["Exists"] == true && Aliases["Epicor"]["Dashboard"]["WndCaption"] == dashb1){
        Log["Checkpoint"]("dashboard designer is opened")
      }else{
        Log["Error"]("dashboard designer was not opened")
      }

    /*
      Step No: 43
      Step: Click File> Copy Dashboard        
      Result: Verify the Copy Dashboard dialog opens        
    */  
      Log["Message"]("Step 43")
      Delay(2000)
      // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard")
      ClickMenu("File->Copy Dashboard")

    /*
      Step No: 44
      Step: Enter Definition ID: TestDashBD-2. Click Ok
      Result: Verify the dashboard is copied
    */  
      Log["Message"]("Step 44")
      // Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("TestDashBD-2")
      EnterText("txtDefinitionId", dashb1Copy + "[Tab]", "Adding Id for dashboard Copy")
      ClickButton("OK")

    /*
      Step No: 45
      Step: Save your dashboard       
      Result: Verify the dashboard is saved
    */      
      Log["Message"]("Step 45")
      SaveDashboard()
      ExitDashboard()

      // var test2 = QueryDatabaseDashboards("TestDashBD-2")
    /*
      Step No: 47
      Step: On Dashboard Maintenance search for system dashboard JobStatusPlus and retrieve it        
      Result: Verify the info from the dashboard is displayed       
    */      
      Log["Message"]("Step 47")
      // Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Clear")
      // Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["btnYes2"]["Click"]()
      ClickMenu("Edit->Clear")
      ClickButton("Yes")

      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("JobStatusPlus")
      // Aliases["Epicor"]["DashboardForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")
      EnterText("txtKeyField", dashb2 + "[Tab]", "Adding text of dashboard") 

    /*
      Step No: 48
      Step: Click on Actions>Modify Dashboard
      Result: Verify the info from the dashboard is displayed       
    */
      Log["Message"]("Step 48")
      // Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Modify Dashboard")
      ClickMenu("Actions->Modify Dashboard")
     
      var count = 0
      while(true){
        var windowsExceptionDialogBtn = Aliases["Epicor"]["FindAllChildren"]("FullName", "*Button*", 2)["toArray"]();

        if (windowsExceptionDialogBtn[0] != null && windowsExceptionDialogBtn[0] != null || windowsExceptionDialogBtn[0] != undefined && windowsExceptionDialogBtn[0] != undefined) {
          if(windowsExceptionDialogBtn[0]["Exists"]){
            Log["Message"]("Validating Warning - System Dashboards may not be modified. - Clicked OK on message ")
            // windowsExceptionDialogBtn[0]["Click"]()
             ClickButton("OK")
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
      Log["Message"]("Step 49")
      Delay(2000)

      // Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Copy Dashboard")
      ClickMenu("File->Copy Dashboard")

    /*
      Step No: 50
      Step: Save your dashboard        
      Result: Verify the dashboard is saved
    */  
      Log["Message"]("Step 50")
      Delay(2000)
      // Aliases["Epicor"]["CopyDashboardForm"]["txtDefinitionId"]["Keys"]("TestDashBD-3")
      EnterText("txtDefinitionId", dashb2Copy + "[Tab]", "Adding text of dashboard") 
      ClickButton("OK")
        
    /*
      Step No: 51
      Step: Save your dashboard       
      Result: Verify the dashboard is saved
    */      
      Log["Message"]("Step 51")
      SaveDashboard()

    // Step No: 52
    //Query on SQL the dashboards
      Log["Message"]("Step 52")
      var test1 = QueryDatabaseDashboards(dashb1)
      Log["Message"]("Query with Dashboard ID " + dashb1 + " retrieved " + test1["RecordCount"] + " records.")

      var test2 = QueryDatabaseDashboards(dashb1Copy)
      Log["Message"]("Query with Dashboard ID " + dashb1Copy + " retrieved " + test2["RecordCount"] + " records.")

    /*
      Step No: 53
      Step: Click File>Delete Dashboard Definition 
      Result: Verify the dashboard is deleted       
    */      
      Log["Message"]("Step 53,54,55")
      DeleteDashboard(dashb2Copy)
      
      var test3 = QueryDatabaseDashboards(dashb2Copy)
      Log["Message"]("Query with Dashboard ID " + dashb2Copy + " retrieved " + test3["RecordCount"] + " records.")

      ExitDashboard()
      // Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

}