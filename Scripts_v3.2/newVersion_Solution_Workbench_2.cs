//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT Data_Solution_Workbench_sc2

function TC_Dashboard_Solution_Workbench_2()
{
    //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'

    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])

    ActivateFullTree()

    Delay(1500)
    ExpandComp(company1)

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- Creates External BAQs -----------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 3
      Result:  Verify the external BAQ is created       
    */

    //Go to System Management> External Business Activity Query> External Datasource Type
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;External Business Activity Query;External Datasource Types")

        //Create a new Datasource type
    Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|New...|New Datasource Type")

        var DsTypeForm = Aliases["Epicor"]["DsTypeForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]

        //Datasource Type: testDTType
    DsTypeForm["typeDetailPanel1"]["epiGroupBox1"]["txtDsType"]["Keys"](dsType)
        // Description: testDTType
        DsTypeForm["typeDetailPanel1"]["epiGroupBox1"]["txtDescr"]["Keys"](dsType)

        // Save
    Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"])
    {
        Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")
        }
    else
    {
        Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

    //Go to System Management> External Business Activity Query> External Datasource
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;External Business Activity Query;External Datasources")

        // Create a new datasource
    Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&New")

        var ExternalDSForm = Aliases["Epicor"]["DatasourceForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]

        // Datasource: testDT
    ExternalDSForm["connectionDetailPanel"]["groupBox1"]["txtKeyField"]["Keys"](dsInfo)
        // Description: testDT
        ExternalDSForm["connectionDetailPanel"]["groupBox1"]["txtDescr"]["Keys"](dsInfo)
        // Datasource type: testDTType
        ExternalDSForm["connectionDetailPanel"]["groupBox1"]["cmbDsType"]["Keys"](dsType)

        // ADO .Net provider: SqlClient Data Provider
    ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["cmbDBProvider"]["Keys"](ADOprovider)
        ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["cmbDBProvider"]["Keys"]("[Tab]")

        // >On Select adapter properties, Key properties tab enter the following:
    var GridAdapterProperties = ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["grpConnectionEditor"]["SpecificConnectionDataControl"]["connectionDefinitionPanel"]["windowDockingArea2"]["dockableWindow2"]["mainCnnProperties"]["grdPropGrid"]

        // Data Source: MX0416-MJ014ZZ
    GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Data Source");
    GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + dsName + "[Enter]");

    // Initial Catalog: Demo DB
    GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Initial Catalog");
    GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + initialCatalog + "[Enter]");

    // UserID: sa
    GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("User ID");
    GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + userID + "[Enter]");

    // Password: Epicor123
    GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Password");
    GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + password + "[Enter]");

    Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"])
    {
        Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")
        }
    else
    {
        Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

    //Go to System Setup> Company/Site Maintenance> Company Maintenance
    MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Company/Site Maintenance;Company Maintenance")

        var CompanyMaintenanceForm = Aliases["Epicor"]["CompanyMaintenanceForm"]["windowDockingArea2"]["dockableWindow1"]["systemDockPanel1"]["windowDockingArea1"]

        //Move to BAQ External Datasources tab
    CompanyMaintenanceForm["dockableWindow4"]["Activate"]()

        // Check on the Enabled checkbox of the created datasource
    var gridDatasources = CompanyMaintenanceForm["dockableWindow4"]["FindChild"](["FullName", "WndCaption"], ["*grd*" || "*grid*", "*Datasource*"], 30)

        var enabledCol = getColumn(gridDatasources, "Enabled")
        var DSName = getColumn(gridDatasources, "Datasource Name")

        for (var i = 0; i < gridDatasources["wRowCount"]; i++)
    {
        var cell = gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](DSName)

          if (cell["Text"]["OleValue"] == dsInfo)
        {
            gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](enabledCol)["Click"]()
            gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](enabledCol)["EditorResolved"]["CheckState"] = "Checked"
            Log["Message"]("External source enabled")
          }
        else
        {
            Log["Message"]("checkbox was not checked")
          }
    }

    // Save
    Aliases["Epicor"]["CompanyMaintenanceForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"])
    {
        Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["CompanyMaintenanceForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
        }
    else
    {
        Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

    // Go to System Management> External Business Activity Query> External business Activity Query
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;External Business Activity Query;External Business Activity Query")

        // Create a new query
    Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|New")

        var BAQDiagramForm = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]

        // QueryID: TestBAQ1v2
    BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtQueryID"]["Keys"](baqs1)
        // Description: TestBAQ1v2
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtDescription"]["Keys"](baqs1)
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtDescription"]["Keys"]("[Tab]")
        // Shared: true
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["chkShared"]["Checked"] = true
        // External Datasource: testDT
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["cmbExtDs"]["Keys"](dsInfo)

        Delay(2500)
        // Move to Query builder tab and select Erp.Part table
        AddTableBAQ(BAQDiagramForm, "Part")
        Delay(2500)

        // On Displat fields select the following fields: Company, PartNum, PartDescription, TypeCode, UnitPrice
    AddColumnsBAQ(BAQDiagramForm, "Part", "Company,PartNum,PartDescription,TypeCode,UnitPrice")

        // Save your Query
    Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")
        Log["Message"]("External BAQ created.")
        Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")

    /*
      Step No: 4
      Result:  Verify the form with developer mode activated, is loaded       
    */
    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Message"]("Dashboard opened")

        //Enable Dashboard Developer Mode  
    DevMode()
        Log["Message"]("DevMode activated")

    /*
      Step No: 5
      Descr: 
            Create a new Dashboard
            Definition ID: SW2TestDashBD1
            Caption: SW2TestDashBD1
            Description: SW2TestDashBD1  
      Result: Verify the Dashboard is created       
    */
        NewDashboard(dashb1, dashb1, dashb1)

    /*
      Step No: 6
      Result:  Click on New Query. Search for the BAQ TestBAQ1v2 and click Ok. Save
    */
        AddQueriesDashboard(baqs1)

        SaveDashboard()
        Log["Message"]("Dashboard " + dashb1 + " created")
        ExitDashboard()

    /*
      Step No: 7
      step: On EPIC06 create a CGCCode BAQ:
      Result:  Click on New Query. Search for the BAQ TestBAQ1v2 and click Ok. Save
    */

      ExpandComp(company2)

      ChangePlant(plant2)

      // Move to EPIC06 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

        // Enter the following in the "General" tab
    var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]

        // QueryID: baqs2
        // Description: baqs2
        // Shared: Checked
    CreateBAQ(baqs2, baqs2, "Shared")
        // Country/Group Code: MX
        Delay(2500)
        // Drag and drop the "Customer" table design area in "Phrase Build" tab
        AddTableBAQ(BAQFormDefinition, "Customer")
        Delay(2500)
        // In the Display Fields> Column Select tab for the "Customer" table select the Company, CustID, CustNum, Name and Address1 columns and add them to "Display Columns" area
        AddColumnsBAQ(BAQFormDefinition, "Customer", "Company,CustID,CustNum,Name,Address1")
        // Save the  BAQ
        SaveBAQ()
        Log["Message"]("BAQ " + baqs2 + " created")
        ExitBAQ()

    /*
      Step No: 8
      step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result:  Click on New Query. Search for the BAQ TestBAQ1v2 and click Ok. Save
    */

    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Message"]("Dashboard opened")

        //Enable Dashboard Developer Mode  
    DevMode()
        Log["Message"]("DevMode activated")

    /*
      Step No: 9
      Descr: 
        // Definition ID: SW2TestDashBD2
        // Caption: SW2TestDashBD2
        // Description: SW2TestDashBD2
        // Country/Group Code: MX"  
      Result: Verify the Dashboard is created       
    */

      NewDashboard(dashb2, dashb2, dashb2)

    /*
      Step No: 10
      Result:  Click on New Query. Search for the BAQ Test2BAQ2 and click Ok. Save       
    */
        AddQueriesDashboard(baqs2)

        SaveDashboard()
        Log["Message"]("Dashboard " + dashb2 + " created")
        ExitDashboard()

    /*
      Step No: 11
      Note: "On EPIC07 create an All Companies BAQ:
      Result:  Click on New Query. Search for the BAQ Test2BAQ2 and click Ok. Save       
    */

      ExpandComp(company3)

    // Move to EPIC07 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
      MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

        // Enter the following in the "General" tab
    var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]

        // QueryID: TestBAQ3v2
        // Description: TestBAQ3v2
        // Shared: Checked
    CreateBAQ(baqs3, baqs3, "All Companies,Shared")
        Delay(2500)
        // Drag and drop the "Part" table design area in "Phrase Build" tab
        AddTableBAQ(BAQFormDefinition, "Part")
        Delay(2500)
        // In the Display Fields> Column Select tab for the "Customer" table select the Company, PartNum, PartDescription and TypeCode 
        AddColumnsBAQ(BAQFormDefinition, "Part", "Company,PartNum,PartDescription,TypeCode")
        // Save the  BAQ
        SaveBAQ()
        Log["Message"]("BAQ " + baqs3 + " created")
        ExitBAQ()

    /*
      Step No: 12 --CHECKING ALL COMPANIES CHECKbox---
      step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result:  Click on New Query. Search for the BAQ TestBAQ1v2 and click Ok. Save
    */

    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Message"]("Dashboard opened")

        //Enable Dashboard Developer Mode  
    DevMode()
        Log["Message"]("DevMode activated")

        /*
          Step No: 9
          Descr: 
            // Definition ID: SW2TestDashBD3
            // Caption: SW2TestDashBD3
            // Description: SW2TestDashBD3
            // Country/Group Code: MX"  
          Result: Verify the Dashboard is created       
        */

          NewDashboard(dashb3, dashb3, dashb3, "All Companies")

        /*
          Step No: 10
          Result:  Click on New Query. Search for the BAQ TestBAQ2v2 and click Ok. Save       
        */
            AddQueriesDashboard(baqs3)

            SaveDashboard()
            Log["Message"]("Dashboard " + dashb3 + " created")
            ExitDashboard()

      MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Message"]("Dashboard opened")

        //Enable Dashboard Developer Mode  
    DevMode()
        Log["Message"]("DevMode activated")

      /*
        Step No: 9
        Descr: 
          // Definition ID: SW2TestDashBD4
          // Caption: SW2TestDashBD4
          // Description: SW2TestDashBD4
          // Country/Group Code: MX"  
        Result: Verify the Dashboard is created       
      */

        NewDashboard(dashb4, dashb4, dashb4)

      /*
        Step No: 10
        Result:  Click on New Query. Search for the BAQ zCustomer01 and click Ok. Save       
      */
          AddQueriesDashboard("zCustomer01")

          SaveDashboard()
          Log["Message"]("Dashboard " + dashb4 + " created")
          ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //---  EPIC05 create the solution -----------------------------------------------------------------------------------------------------------'

    /*
      Step No: 10
      Result:  Click on New Query. Search for the BAQ TestBAQ2v2 and click Ok. Save       
    */

    ExpandComp(company1)

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
    Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"](solDefEpic05Type)
      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtDescription"]["Keys"](solDefEpic05Type)

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      Log["Message"]("Solution Type was created " + solDefEpic05Type)

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

      // Go to System Management> Solution Management> Solution Workbench
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"](solutionDefEpic05)
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtSolutionType"]["Keys"](solDefEpic05Type)
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtDescription"]["Keys"](solutionDefEpic05)
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      // Click on Add To Solution
      // Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]()
    ClickButton("Add To Solution")

      // On Solution Element Search select Dashboard and click Search
      //Search for dashboard item and select dashboards to add to the solution
    SearchSolutionItemsDashboard(dashb1 + "," + dashb3, "SW2")

      while (true)
    {
        //find button of the "add dependency" dialog
        var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName", "WndCaption"],["*Add Dependency*", "*&Yes*"], 5)
        var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Add Dependency*", "*Static*"], 5)

        if (addDepDialogBtnYes["Exists"])
        {
            addDepDialogBtnYes["Click"]()
          Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")
        }
        else
        {
            break
        }
    }

    // Click Actions>Build Solution
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|Actions|Build Solution")

      // Check all the options and click Build
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"](["FullName", "Style"], ["*chk*","*Check*"], 30)
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"]("FullName", "*chk*", 30)

    Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"]("chkEncryptSource")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"]("chkCreateCodeDocs")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"]("chkPromtFileDetails")["Checked"] = true

      /*for (var i = 0; i < BuildSolChks["length"]; i++) {
        BuildSolChks[i]["Checked"] = true
      }*/

      Log["Message"]("All options where checked")

      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCreate")["Click"]()

      // When Save CAB file dialog opens select a path to save your file
      //Stestv2_Customer Solution_3.1.600.0
    var windowSaveCABFile = Aliases["Epicor"]["FindChild"]("FullName", "*Save*", 30)

        if (windowSaveCABFile["Exists"])
    {
        var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*", "*Button*"], 30)
          windowSaveCABFileSaveBtn["Click"]()
          var windowExportReplaceDashBD = Aliases["Epicor"]["FindChild"](["FullName", "ClassName"],["*_already_exists*", "*Element*"], 30)
          if (windowExportReplaceDashBD["Exists"])
        {
            windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["WndCaption", "WndClass"],["*Yes*","*Button*"], 30)
            windowExportDashBDSaveBtn["Click"] ()
          }
          Log["Message"] ("CAB File saved correctly")
        }else{
          Log["Error"] ("CAB File wasn't saved correctly, Object doesn't exists")    
        }
        Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"] ("btnCancel")["Click"]
()

       // Click Save
       Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- Delete the dashboard and BAQ you've created on EPIC05 (SW2TestDashBD1 and Test2BAQ1) --------------------------------------------------'
    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Message"] ("DevMode activated")

      DeleteDashboard(dashb1)

      ExitDashboard()

      //Delete BAQ

      ExpandComp(company1)

      //Go to System Management> External Business Activity Query> External Datasource Type
      MainMenuTreeViewSelect(treeMainPanel1 + "System Management;External Business Activity Query;External Business Activity Query")

       DeleteBAQ(baqs1)

       ExitBAQ()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //---  EPIC06 create the solution -----------------------------------------------------------------------------------------------------------'

    /*
      Step No: 19
      Result:  Click on New Query. Search for the BAQ TestBAQ2v2 and click Ok. Save       
    */

    ExpandComp(company2)

    ChangePlant(plant2)

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect(treeMainPanel2 + "System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"] (solDefEpic06Type)
       Aliases["Epicor"]["SolutionTypeForm"]
["windowDockingArea1"]
["dockableWindow4"]
["mainPanelControl"]
["windowDockingArea1"]
["dockableWindow2"]
["solutionTypePanel1"]
["grpSolutionType"]
["txtDescription"]
["Keys"]
(solDefEpic06Type)

       Aliases["Epicor"]["SolutionTypeForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")
      
      Log["Checkpoint"] ("Solution Type was created 'SType2v2'")

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|E&xit")

    // Go to System Management> Solution Management> Solution Workbench
    MainMenuTreeViewSelect(treeMainPanel2 + "System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|New...|Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"] (solutionDefEpic06)
       Aliases["Epicor"]["SolutionWorkbenchForm"]
["windowDockingArea1"]
["dockableWindow3"]
["mainPanel1"]
["windowDockingArea1"]
["dockableWindow1"]
["detailPanel1"]
["groupBox1"]
["txtSolutionType"]
["Keys"]
(solDefEpic06Type)
       Aliases["Epicor"]["SolutionWorkbenchForm"]
["windowDockingArea1"]
["dockableWindow3"]
["mainPanel1"]
["windowDockingArea1"]
["dockableWindow1"]
["detailPanel1"]
["groupBox1"]
["txtDescription"]
["Keys"]
(solutionDefEpic06)
       Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")

      // Click on Add To Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]

()

      SearchSolutionItemsDashboard(dashb2+","+dashb3, "SW2")

      while(true) {
        //find button of the "add dependency" dialog
        var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName", "WndCaption"],["*Add Dependency*", "*&Yes*"], 5)
        var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Add Dependency*", "*Static*"], 5)

        if (addDepDialogBtnYes["Exists"]) {
          addDepDialogBtnYes["Click"] ()
           Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")  
        }else{
          break
        } 
      }
        
      // Click Actions>Build Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|Actions|Build Solution")

      // Check all the options and click Build
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"](["FullName", "Style"], ["*chk*","*Check*"], 30)
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"]("FullName", "*chk*", 30)

      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkEncryptSource")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkCreateCodeDocs")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkPromtFileDetails")["Checked"] = true

      /*for (var i = 0; i < BuildSolChks["length"]; i++) {
        BuildSolChks[i]["Checked"] = true
      }*/

      Log["Checkpoint"] ("All options where checked")
      
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"] ("btnCreate")["Click"]
()

         // When Save CAB file dialog opens select a path to save your file
         //Stestv2_Customer Solution_3.1.600.0
         var windowSaveCABFile = Aliases["Epicor"]["FindChild"] ("FullName","*Save*", 30)
        if (windowSaveCABFile["Exists"]) {
          var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*", "*Button*"], 30)
          windowSaveCABFileSaveBtn["Click"] ()
           var windowExportReplaceDashBD = Aliases["Epicor"]["FindChild"] (["FullName", "ClassName"],["*_already_exists*", "*Element*"], 30)
          if (windowExportReplaceDashBD["Exists"]) {
            windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"] (["WndCaption", "WndClass"],["*Yes*", "*Button*"], 30)
            windowExportDashBDSaveBtn["Click"] ()
          }
          Log["Message"] ("CAB File saved correctly")
        }else{
          Log["Error"] ("CAB File wasn't saved correctly, Object doesn't exists")    
        }
        Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"] ("btnCancel")["Click"]
()

       // Click Save
       Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- Delete the dashboards and BAQs you've created on EPIC06 (SW2TestDashBD2 and Test2BAQ1) ------------------------------------------------'
    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        

    /*
      Step: 20
      Note: Delete Dashboard and BAQ
    */

    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Message"] ("DevMode activated")

      DeleteDashboard(dashb2)

      ExitDashboard()

      //Delete BAQ

      ExpandComp(company2)

    //Go to System Management> External Business Activity Query> External Datasource Type
    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

     DeleteBAQ(baqs2)

     ExitBAQ()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //---  EPIC07 create the solution -----------------------------------------------------------------------------------------------------------'

    /*
      Step No: 21
      Result:       
    */

    ExpandComp(company3)

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect(treeMainPanel3 + "System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"] (solDefEpic07Type)
       Aliases["Epicor"]["SolutionTypeForm"]
["windowDockingArea1"]
["dockableWindow4"]
["mainPanelControl"]
["windowDockingArea1"]
["dockableWindow2"]
["solutionTypePanel1"]
["grpSolutionType"]
["txtDescription"]
["Keys"]
(solDefEpic07Type)

       Aliases["Epicor"]["SolutionTypeForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")
      
      Log["Checkpoint"] ("Solution Type was created 'SType3v2'")

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|E&xit")

    // Go to System Management> Solution Management> Solution Workbench
    MainMenuTreeViewSelect(treeMainPanel3 + "System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|New...|Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"] (solutionDefEpic07)
       Aliases["Epicor"]["SolutionWorkbenchForm"]
["windowDockingArea1"]
["dockableWindow3"]
["mainPanel1"]
["windowDockingArea1"]
["dockableWindow1"]
["detailPanel1"]
["groupBox1"]
["txtSolutionType"]
["Keys"]
(solDefEpic07Type)
       Aliases["Epicor"]["SolutionWorkbenchForm"]
["windowDockingArea1"]
["dockableWindow3"]
["mainPanel1"]
["windowDockingArea1"]
["dockableWindow1"]
["detailPanel1"]
["groupBox1"]
["txtDescription"]
["Keys"]
(solutionDefEpic07)
       Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")

      // Click on Add To Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]

()

       SearchSolutionItemsDashboard(dashb3+","+dashb4, "SW2")

      while(true) {
        //find button of the "add dependency" dialog
        var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName", "WndCaption"],["*Add Dependency*", "*&Yes*"], 5)
        var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Add Dependency*", "*Static*"], 5)

        if (addDepDialogBtnYes["Exists"]) {
          addDepDialogBtnYes["Click"] ()
           Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")  
        }else{
          break
        } 
      }
        
      // Click Actions>Build Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|Actions|Build Solution")

      // Check all the options and click Build
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"](["FullName", "Style"], ["*chk*","*Check*"], 30)
      // var BuildSolChks = Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["FindAllChildren"]("FullName", "*chk*", 30)

      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkEncryptSource")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkCreateCodeDocs")["Checked"] = true
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["grpSettings"]["WinFormsObject"] ("chkPromtFileDetails")["Checked"] = true

      /*for (var i = 0; i < BuildSolChks["length"]; i++) {
        BuildSolChks[i]["Checked"] = true
      }*/

      Log["Checkpoint"] ("All options where checked")
      
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"] ("btnCreate")["Click"]
()

         // When Save CAB file dialog opens select a path to save your file
         //Stestv2_Customer Solution_3.1.600.0
         var windowSaveCABFile = Aliases["Epicor"]["FindChild"] ("FullName","*Save*", 30)
        if (windowSaveCABFile["Exists"]) {
          var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*", "*Button*"], 30)
          windowSaveCABFileSaveBtn["Click"] ()
           var windowExportReplaceDashBD = Aliases["Epicor"]["FindChild"] (["FullName", "ClassName"],["*_already_exists*", "*Element*"], 30)
          if (windowExportReplaceDashBD["Exists"]) {
            windowExportDashBDSaveBtn = Aliases["Epicor"]["FindChild"] (["WndCaption", "WndClass"],["*Yes*", "*Button*"], 30)
            windowExportDashBDSaveBtn["Click"] ()
          }
          Log["Message"] ("CAB File saved correctly")
        }else{
          Log["Error"] ("CAB File wasn't saved correctly, Object doesn't exists")    
        }
        Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"] ("btnCancel")["Click"]
()

       // Click Save
       Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- Delete the dashboards and BAQs you've created on EPIC06 (SWTestDashBD2v2 and TestBAQ2v2, SWTestDashBD3v2) ---------------------------------------'
    //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        

    /*
      Step: 22
      Note: Delete Dashboard and BAQ
    */

    MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

    var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
    Log["Message"] ("Dashboard opened")

    //Enable Dashboard Developer Mode  
    DevMode()
    Log["Message"] ("DevMode activated")

    DeleteDashboard(dashb3+","+dashb4)

    ExitDashboard()

    //Delete BAQ

    //Go to System Management> External Business Activity Query> External Datasource Type
    MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")

     DeleteBAQ(baqs3)

     ExitBAQ()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- On EPIC05 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 23
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|Actions|Install Solution")

    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"] ()

     var openSolutionWindow = Aliases["Epicor"]["FindChild"] (["FullName", "WndClass"], ["*Open*", "*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*", "*Button*"],30)

    openSolutionWindow["Keys"] (solutionEPIC05)

     openSolutionWindowBtn["Click"]()

     // Select Automatically overwrite duplicate files and Automatically overwrite duplicate data
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpFileConflicts"]
["radOverwriteDupFiles"]
["ultraOptionSet1"]
["Click"]
()
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpDataConflicts"]
["radButtonOverwriteDupData"]
["ultraOptionSet1"]
["Click"]
()

     //Check the check box "Only Target Current Company" and click Install
     Aliases["Epicor"]["InstallSolutionForm"] ["pnlInstallSolution"] ["grpSettings"] ["chkCurrentCompanyOnly"] ["Checked"] = true
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"] ()
    
    if (Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["Exists"]) {
      Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["btnOK"]["Click"] ()
    }
 
    // Click Close
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["WinFormsObject"] ("btnAbort")["Click"]
()
     Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|E&xit")

    Log["Checkpoint"] ("Exported solution Installed on " + company1)

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

    //--- EPIC05 Retrieve SW2TestDashBD1, Test2BAQ1 and SW2TestDashBD3 --------------------------------------------------------------------------'

    /*
      Step: 24 - 25
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb1)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb1 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb1 + " wasn't retrieved")
      }

      ExitDashboard()

    /*
      Step: 26 - 27
      Note: Retrieve BAQ
    */

    //Go to System Management> Business Activity Management;Setup;Business Activity Query
    MainMenuTreeViewSelect(treeMainPanel1 + "System Management;External Business Activity Query;External Business Activity Query")

      Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["txtQueryID"]["Keys"] (baqs1)
       Aliases["Epicor"]["BAQDiagramForm"] ["windowDockingArea1"] ["dockableWindow2"] ["allPanels1"] ["windowDockingArea1"] ["dockableWindow1"] ["optionsPanel1"] ["gbID"] ["txtQueryID"] ["Keys"] ("[Tab]")

      if(Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["chkShared"]["Checked"]){
         Log["Checkpoint"] ("BAQ " + baqs1 + " retrieved and 'Shared' checkbox is checked")
      }else{
        Log["Error"] ("BAQ " + baqs1 + " wasn't retrieved or 'Shared' checkbox is not checked")
      }

      if(Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["cmbExtDs"]["Text"] != ""){
         Log["Checkpoint"] ("BAQ " + baqs1 + " retrieved and 'External Datasource' is not empty")
      }else{
        Log["Error"] ("BAQ " + baqs1 + " wasn't retrieved or 'External Datasource' is empty")
      }

      ExitBAQ()

    /*
      Step: 28 - 29
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb3)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + " wasn't retrieved")
      }
      
     if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " 'All Companies' checkbox is not checked")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + " 'All Companies' checkbox is checked solution not imported correctly")
      }

      ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- On EPIC06 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 30
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect(treeMainPanel2 + "System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|Actions|Install Solution")

    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"] ()

     var openSolutionWindow = Aliases["Epicor"]["FindChild"] (["FullName", "WndClass"], ["*Open*", "*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*", "*Button*"],30)

    openSolutionWindow["Keys"] (solutionEPIC06)

     openSolutionWindowBtn["Click"]()

     // Select Automatically overwrite duplicate files and Automatically overwrite duplicate data
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpFileConflicts"]
["radOverwriteDupFiles"]
["ultraOptionSet1"]
["Click"]
()
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpDataConflicts"]
["radButtonOverwriteDupData"]
["ultraOptionSet1"]
["Click"]
()

     //Check the check box "Only Target Current Company" and click Install
     Aliases["Epicor"]["InstallSolutionForm"] ["pnlInstallSolution"] ["grpSettings"] ["chkCurrentCompanyOnly"] ["Checked"] = true
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"] ()

     // Click Close
     Aliases["Epicor"]["InstallSolutionForm"] ["pnlInstallSolution"] ["WinFormsObject"] ("btnAbort")["Click"]
()
        Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|E&xit")
  
    Log["Checkpoint"] ("Exported solution Installed on " + company2)

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

    //--- EPIC06 Retrieve SW2TestDashBD2, SW2TestDashBD3  ---------------------------------------------------------------------------------------'

    /*
      Step: 31 - 32
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel2 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb2)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb2 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb2 + " wasn't retrieved")
      }
      
      if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"] ("Dashboard " + dashb2 + " 'All Companies' checkbox is not checked")
      }else{
        Log["Error"] ("Dashboard " + dashb2 + "'All Companies' checkbox is checked solution not imported correctly")
      }

      ExitDashboard()

    /*
      Step: 33
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb3)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["Exists"]) {
        Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["click"] ()
      }

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + " wasn't retrieved")
      }
      
     if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " 'All Companies' checkbox is not checked")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + "'All Companies' checkbox is checked solution not imported correctly")
      }

      ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

    //--- On EPIC07 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 34
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect(treeMainPanel3 + "System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"] ("[0]|Actions|Install Solution")

    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"] ()

     var openSolutionWindow = Aliases["Epicor"]["FindChild"] (["FullName", "WndClass"], ["*Open*", "*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*", "*Button*"],30)

    openSolutionWindow["Keys"] (solutionEPIC07)

     openSolutionWindowBtn["Click"]()

     // Select Automatically overwrite duplicate files and Automatically overwrite duplicate data
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpFileConflicts"]
["radOverwriteDupFiles"]
["ultraOptionSet1"]
["Click"]
()
     Aliases["Epicor"]["InstallSolutionForm"]
["pnlInstallSolution"]
["grpConflicts"]
["grpDataConflicts"]
["radButtonOverwriteDupData"]
["ultraOptionSet1"]
["Click"]
()

     //Check the check box "Only Target Current Company" and click Install
     Aliases["Epicor"]["InstallSolutionForm"] ["pnlInstallSolution"] ["grpSettings"] ["chkCurrentCompanyOnly"] ["Checked"] = true
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"] ()

     // Click Close
     Aliases["Epicor"]["InstallSolutionForm"] ["pnlInstallSolution"] ["WinFormsObject"] ("btnAbort")["Click"]
()
        Aliases["Epicor"]["SolutionWorkbenchForm"] ["zSonomaForm_Toolbars_Dock_Area_Top"] ["ClickItem"] ("[0]|&File|E&xit")
    
    Log["Checkpoint"] ("Exported solution Installed on " + company3)

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

    //--- EPIC07 Retrieve SW2TestDashBD3, SW2TestDashBD4  ---------------------------------------------------------------------------------------'

    /*
      Step: 35 - 36
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb3)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["Exists"]) {
        Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["click"] ()
      }

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + " wasn't retrieved")
      }
      
     if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"] ("Dashboard " + dashb3 + " 'All Companies' checkbox is not checked")
      }else{
        Log["Error"] ("Dashboard " + dashb3 + "'All Companies' checkbox is checked solution not imported correctly")
      }

      ExitDashboard()

    /*
      Step: 37
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect(treeMainPanel3 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Message"] ("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"] (dashb4)
       Aliases["Epicor"]["Dashboard"] ["dbPanel"] ["windowDockingArea1"] ["dockableWindow2"] ["pnlGeneral"] ["windowDockingArea1"] ["dockableWindow1"] ["pnlGenProps"] ["txtDefinitonID"] ["Keys"] ("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"] ("Dashboard " + dashb4 + " retrieved")
      }else{
        Log["Error"] ("Dashboard " + dashb4 + " wasn't retrieved")
      }
      
      if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"] ("Dashboard " + dashb4 + " 'All Companies' checkbox is not checked")
      }else{
        Log["Error"] ("Dashboard " + dashb4 + "'All Companies' checkbox is checked")
      }

      var grid = Aliases["Epicor"]["Dashboard"]["dbPanel"]["FindChild"]("WndCaption", "*zCustomer01*", 30)
      if(grid["Exists"]){
        Log["Checkpoint"] ("" + dashb4 + " is retrieved and it includes the zCustomer01 query")
      }else{
        Log["error"] ("" + dashb4 + " is retrieved and it doesn't include the zCustomer01 query")
      }

      ExitDashboard()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

   // DeactivateFullTree()

   // CloseSmartClient()
}

function SearchSolutionItemsDashboard(stringDashboards, findDashboardStartId)
{
    // Finds grid for the available elemnts on solution element search dialog
    var avElementsGrid = Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["tabSearchItems"]["ultraTabPageControl7"]["epiGroupBox5"]["FindChild"](["WndCaption", "ClrClassName"], ["*Available*", "*Grid*"], 30)

  var elemColum = getColumn(avElementsGrid, "ElementHeaderID")

  for (var i = 0; i < avElementsGrid["wRowCount"]; i++)
    {
        var cell = avElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

    if (cell["Text"]["OleValue"] == "Dashboard")
        {
            cell["Click"]()
    }
    }

    //Search button
    //Aliases["Epicor"]["SolutionElementSearch"]["SWTestDashBD"]["btnSearch"]["Click"]()
    ClickButton("Search")

  var findDashboard = findDashboardStartId //"SW1"

  // On Advanced Element Search enter SWTestDashBD on Starting At and click Search
    Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["panel1"]["oETC"]["oETP"]["pnlBasicSrch"]["groupBox1"]["txtStartWith"]["Keys"](findDashboard)
  //Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()
  ClickButton("Search")

  // The results should be the SW1TestDashBD1 and SW1TestDashBD3
    var advElementsGrid = Aliases["Epicor"]["AdvancedElementSearch"]["FindChild"](["WndCaption", "ClrClassName"], ["*Search*", "*Grid*"], 30)

  //********
    var colDefID = getColumn(advElementsGrid, "DefinitionID")

    var dashboardGrid = stringDashboards.split(",")

    var i, j

    //dashboardGrid[0] = dashb1
    //dashboardGrid[1] = dashb3

    for (i = 0; i < advElementsGrid["Rows"]["Count"]; i++)
    {
        var cellDefID = advElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](colDefID)

      for (j = 0; j < dashboardGrid["length"]; j++)
        {
            if (cellDefID["Text"]["OleValue"] == dashboardGrid[j])
            {
                advElementsGrid["Keys"]("^")
                advElementsGrid["Rows"]["Item"](i)["Selected"] = true
                advElementsGrid["Rows"]["Item"](i)["Activated"] = true
            }
        }

        if (j == dashboardGrid["length"] - 1)
        {
            break
        }
    }

    Log["Message"]("Dashboards " + stringDashboards + " selected.")

    ClickButton("OK")

    Log["Checkpoint"]("Dashboards selected and clicked ok on Advanced Element Search dialog")

  // Click Add to Solution and click Yes to the Add Dependency messages to also add the BAQs to the solution
  //Aliases["Epicor"]["SolutionElementSearch"]["grpSelectedSolutionItems"]["btnAddToSolution"]["Click"]()
    ClickButton("Add To Solution")

}