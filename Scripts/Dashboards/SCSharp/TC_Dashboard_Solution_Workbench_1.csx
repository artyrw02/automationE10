//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Dashboard_Solution_Workbench_1(){
  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login("epicor","Epicor123") 

    ActivateFullTree()

    Delay(1500)
    ExpandComp("Epicor Europe")

    // ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates External BAQs -----------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 3
      Result:  Verify the external BAQ is created       
    */ 

      //Go to System Management> External Business Activity Query> External Datasource Type
      MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Datasource Types")

        //Create a new Datasource type
        Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|New...|New Datasource Type")
          
        var DsTypeForm = Aliases["Epicor"]["DsTypeForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]

        //Datasource Type: testDTType
        DsTypeForm["typeDetailPanel1"]["epiGroupBox1"]["txtDsType"]["Keys"]("TestDTType")
        // Description: testDTType
        DsTypeForm["typeDetailPanel1"]["epiGroupBox1"]["txtDescr"]["Keys"]("TestDTType")

        // Save
        Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"]) {
          Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["DsTypeForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")
        }else{
          Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

      //Go to System Management> External Business Activity Query> External Datasource
      MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Datasources")

        // Create a new datasource
        Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&New")

        var ExternalDSForm = Aliases["Epicor"]["DatasourceForm"]["windowDockingArea2"]["dockableWindow4"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]
        
        // Datasource: testDT
        ExternalDSForm["connectionDetailPanel"]["groupBox1"]["txtKeyField"]["Keys"]("TestDT")
        // Description: testDT
        ExternalDSForm["connectionDetailPanel"]["groupBox1"]["txtDescr"]["Keys"]("TestDT")
        // Datasource type: testDTType
        ExternalDSForm["connectionDetailPanel"]["groupBox1"]["cmbDsType"]["Keys"]("TestDTType")

        // ADO .Net provider: SqlClient Data Provider
        ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["cmbDBProvider"]["Keys"]("SqlClient Data Provider")
        ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["cmbDBProvider"]["Keys"]("[Tab]")

        // >On Select adapter properties, Key properties tab enter the following:
        var GridAdapterProperties = ExternalDSForm["connectionDetailPanel"]["grpCnnEditor"]["extCnnEditPanel"]["grpConnectionEditor"]["SpecificConnectionDataControl"]["connectionDefinitionPanel"]["windowDockingArea2"]["dockableWindow2"]["mainCnnProperties"]["grdPropGrid"]

        // Data Source: MX0416-MJ014ZZ
        GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Data Source");
        GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + "(local)" + "[Enter]");

        // Initial Catalog: Demo DB
        GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Initial Catalog");
        GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + "erp10Staging" + "[Enter]");

        // UserID: sa
        GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("User ID");
        GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + "sa" + "[Enter]");

        // Password: Epicor123
        GridAdapterProperties["wItems"]("Connection specific")["ClickLabel"]("Password");
        GridAdapterProperties["PropertyGridView"]["Keys"]("^a[Del]" + "Epicor123" + "[Enter]");      

        Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"]) {
          Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["DatasourceForm"]["sonomaFormToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")
        }else{
          Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

      //Go to System Setup> Company/Site Maintenance> Company Maintenance
      MainMenuTreeViewSelect("Epicor Europe;System Setup;Company/Site Maintenance;Company Maintenance")

        var CompanyMaintenanceForm = Aliases["Epicor"]["CompanyMaintenanceForm"]["windowDockingArea2"]["dockableWindow1"]["systemDockPanel1"]["windowDockingArea1"]
        
        //Move to BAQ External Datasources tab
        CompanyMaintenanceForm["dockableWindow4"]["Activate"]()

        // Check on the Enabled checkbox of the created datasource
        var gridDatasources = CompanyMaintenanceForm["dockableWindow4"]["FindChild"](["FullName", "WndCaption"], ["*grd*" || "*grid*", "*Datasource*"], 30)

        var enabledCol = getColumn(gridDatasources, "Enabled")
        var DSName = getColumn(gridDatasources, "Datasource Name")

        for (var i = 0; i < gridDatasources["wRowCount"]; i++) {
          var cell = gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](DSName)

          if (cell["Text"]["OleValue"] == "TestDT") {
            gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](enabledCol)["Click"]()
            gridDatasources["Rows"]["Item"](i)["Cells"]["Item"](enabledCol)["EditorResolved"]["CheckState"] = "Checked"
          }
        }

        // Save
        Aliases["Epicor"]["CompanyMaintenanceForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

        if (!Aliases["Epicor"]["ExceptionDialog"]["Exists"]) {
          Log["Checkpoint"]("Datasource created correctly")
          Aliases["Epicor"]["CompanyMaintenanceForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
        }else{
          Log["Error"]("There was a problem. Datasource wasn't created correctly")
        }

      // Go to System Management> External Business Activity Query> External business Activity Query
      MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Business Activity Query")

        // Create a new query
        Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|New")

        var BAQDiagramForm = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]

        // QueryID: TestBAQ1
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtQueryID"]["Keys"]("TestBAQ1")
        // Description: TestBAQ1
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtDescription"]["Keys"]("TestBAQ1")
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["txtDescription"]["Keys"]("[Tab]")
        // Shared: true
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["chkShared"]["Checked"] = true
        // External Datasource: testDT
        BAQDiagramForm["dockableWindow1"]["optionsPanel1"]["gbID"]["cmbExtDs"]["Keys"]("testDT")

        Delay(2000)

        // Move to Query builder tab and select Erp.Part table
        AddTableBAQ(BAQDiagramForm, "Part")

        Delay(2000)
        // On Displat fields select the following fields: Company, PartNum, PartDescription, TypeCode, UnitPrice
        AddColumnsBAQ(BAQDiagramForm, "Part", "Company,PartNum,PartDescription,TypeCode,UnitPrice")
        
        Log["Checkpoint"]("External BAQ created")

        // Save your Query
        Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|&Save")
        Aliases["Epicor"]["BAQDiagramForm"]["ToolbarsDockAreaTop"]["ClickItem"]("[0]|&File|E&xit")

    /*
      Step No: 4
      Result:  Verify the form with developer mode activated, is loaded       
    */ 
      //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Checkpoint"]("Dashboard opened")
        
        //Enable Dashboard Developer Mode  
        DevMode()
        Log["Checkpoint"]("DevMode activated")

    /*
      Step No: 5
      Descr: 
            Create a new Dashboard
            Definition ID: TestDashBD1
            Caption: TestDashBD1
            Description: TestDashBD1  
      Result: Verify the Dashboard is created       
    */ 
        NewDashboard("TestDashBD1","TestDashBD1","TestDashBD1")
       
    /*
      Step No: 6
      Result:  Click on New Query. Search for the BAQ TestBAQ1 and click Ok. Save
    */ 
        AddQueriesDashboard("TestBAQ1")
        
        SaveDashboard()
        Log["Checkpoint"]("Dashboard TestDashBD1 created")     
        ExitDashboard()

    /*
      Step No: 7
      step: On EPIC06 create a CGCCode BAQ:
      Result:  Click on New Query. Search for the BAQ TestBAQ1 and click Ok. Save
    */         

      ExpandComp("Epicor Education")

      ChangePlant("Main Plant")

      // Move to EPIC06 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

        // Enter the following in the "General" tab
        var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
        
        // QueryID: TestBAQ2
        // Description: TestBAQ2
        // Shared: Checked
        CreateBAQ("TestBAQ2", "TestBAQ2", "Shared")
        // Country/Group Code: MX
        // Drag and drop the "Customer" table design area in "Phrase Build" tab
        Delay(2000)
        AddTableBAQ(BAQFormDefinition, "Customer")
        Delay(2000)
        // In the Display Fields> Column Select tab for the "Customer" table select the Company, CustID, CustNum, Name and Address1 columns and add them to "Display Columns" area
        AddColumnsBAQ(BAQFormDefinition, "Customer", "Company,CustID,CustNum,Name,Address1")
        // Save the  BAQ
        SaveBAQ()
        Log["Checkpoint"]("BAQ TestBAQ2 created")     
        ExitBAQ()

    /*
      Step No: 8
      step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result:  Click on New Query. Search for the BAQ TestBAQ1 and click Ok. Save
    */ 

      //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Checkpoint"]("Dashboard opened")
        
        //Enable Dashboard Developer Mode  
        DevMode()
        Log["Checkpoint"]("DevMode activated")

    /*
      Step No: 9
      Descr: 
        // Definition ID: TestDashBD2
        // Caption: TestDashBD2
        // Description: TestDashBD2
        // Country/Group Code: MX"  
      Result: Verify the Dashboard is created       
    */ 

      NewDashboard("TestDashBD2","TestDashBD2","TestDashBD2")
       
    /*
      Step No: 10
      Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
    */ 
        AddQueriesDashboard("TestBAQ2")
        
        SaveDashboard()
        Log["Checkpoint"]("Dashboard TestDashBD2 created")
        ExitDashboard()

    /*
      Step No: 11
      Note: "On EPIC07 create an All Companies BAQ:
      Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
    */ 
     
      ExpandComp("Epicor Mexico")

    // Move to EPIC07 company and open Executive analysis> Business Activity Management> Setup> Business Activity Query
      MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

        // Enter the following in the "General" tab
        var BAQFormDefinition = Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]
        
        // QueryID: TestBAQ3
        // Description: TestBAQ3
        // Shared: Checked
        CreateBAQ("TestBAQ3", "TestBAQ3", "All Companies,Shared")
        Delay(2000)
        // Drag and drop the "Part" table design area in "Phrase Build" tab
        AddTableBAQ(BAQFormDefinition, "Part")
        Delay(2000)
        // In the Display Fields> Column Select tab for the "Customer" table select the Company, PartNum, PartDescription and TypeCode 
        AddColumnsBAQ(BAQFormDefinition, "Part", "Company,PartNum,PartDescription,TypeCode")
        // Save the  BAQ
        SaveBAQ()
        Log["Checkpoint"]("BAQ TestBAQ3 created")
        ExitBAQ()


    /*
      Step No: 12
      step: Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      Result:  Click on New Query. Search for the BAQ TestBAQ1 and click Ok. Save
    */ 

      //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
      MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Checkpoint"]("Dashboard opened")
        
        //Enable Dashboard Developer Mode  
        DevMode()
        Log["Checkpoint"]("DevMode activated")

        /*
          Step No: 9
          Descr: 
            // Definition ID: TestDashBD3
            // Caption: TestDashBD3
            // Description: TestDashBD3
            // Country/Group Code: MX"  
          Result: Verify the Dashboard is created       
        */ 

          NewDashboard("TestDashBD3","TestDashBD3","TestDashBD3", "All Companies")
           
        /*
          Step No: 10
          Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
        */ 
            AddQueriesDashboard("TestBAQ3")
            
            SaveDashboard()
            Log["Checkpoint"]("Dashboard TestDashBD3 created")
            ExitDashboard()

      MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

        var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
        Log["Checkpoint"]("Dashboard opened")
        
        //Enable Dashboard Developer Mode  
        DevMode()
        Log["Checkpoint"]("DevMode activated")

      /*
        Step No: 9
        Descr: 
          // Definition ID: TestDashBD3
          // Caption: TestDashBD3
          // Description: TestDashBD3
          // Country/Group Code: MX"  
        Result: Verify the Dashboard is created       
      */ 

        NewDashboard("TestDashBD4","TestDashBD4","TestDashBD4")
         
      /*
        Step No: 10
        Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
      */ 
          AddQueriesDashboard("zCustomer01")
          
          SaveDashboard()
          Log["Checkpoint"]("Dashboard TestDashBD4 created")
          ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //---  EPIC05 create the solution -----------------------------------------------------------------------------------------------------------'
     
    /*
      Step No: 10
      Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
    */

     ExpandComp("Epicor Europe")

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect("Epicor Europe;System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"]("SType")
      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtDescription"]["Keys"]("SType")
      
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      
      Log["Checkpoint"]("Solution Type was created 'SType'")

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

      // Go to System Management> Solution Management> Solution Workbench
      MainMenuTreeViewSelect("Epicor Europe;System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Stest")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtSolutionType"]["Keys"]("SType")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtDescription"]["Keys"]("Stest")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      // Click on Add To Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]()

      // On Solution Element Search select Dashboard and click Search

      var avElementsGrid = Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["tabSearchItems"]["ultraTabPageControl7"]["epiGroupBox5"]["FindChild"](["WndCaption","ClrClassName"], ["*Available*","*Grid*"], 30)

      var elemColum = getColumn(avElementsGrid,"ElementHeaderID")

      for (var i = 0; i < avElementsGrid["wRowCount"]; i++) {
        var cell = avElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

        if(cell["Text"]["OleValue"] == "Dashboard"){
          cell["Click"]()
        }
      }

      //Search button
      Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["btnSearch"]["Click"]()

      // On Advanced Element Search enter TestDashBD on Starting At and click Search
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["panel1"]["oETC"]["oETP"]["pnlBasicSrch"]["groupBox1"]["txtStartWith"]["Keys"]("TestDashBD")
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()

      // The first results should be the TestDashBD1 and TestDashBD3
      // Select them using Ctrl key and click Ok

      var advElementsGrid = Aliases["Epicor"]["AdvancedElementSearch"]["FindChild"](["WndCaption","ClrClassName"], ["*Search*","*Grid*"], 30)

      // var elemColum = getColumn(advElementsGrid,"DefinitionID")
      // var selectIndex = []
      // advElementsGrid["Rows"]["Item"](0)["Cells"]["Item"](elemColum)["Click"]()

      // for (var i = 0; i < advElementsGrid["Rows"]["Count"]; i++) {
      //   var cell = advElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

      //   var aString = cell["Text"]["OleValue"]
      //   var aSubString = "TestDashBD"
      //   var Res

      //   Res = aqString["Find"](aString, aSubString)

      //   if (Res != -1) {
      //     selectIndex.push(i)
      //   }else{
      //     break
      //   }
      // }

      // for (var i = 0; i < selectIndex["length"]-1; i++) {
      //   advElementsGrid["Keys"]("!"+"[Down]")
      // }

      advElementsGrid["Click"](87, 49);
      advElementsGrid["Keys"]("![Down]![Down]![Down]");
      Log["Checkpoint"]("Dashboards selected")


      Aliases["Epicor"]["AdvancedElementSearch"]["ultraStatusBar2"]["btnOK"]["Click"]()
      Log["Checkpoint"]("Dashboards selected and clicked ok on Advanced Element Search dialog")

      // Click Add to Solution and click Yes to the Add Dependency messages to also add the BAQs to the solution
      Aliases["Epicor"]["SolutionElementSearch"]["grpSelectedSolutionItems"]["btnAddToSolution"]["Click"]()
         
        while(true) {
          //find button of the "add dependency" dialog
          var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName","WndCaption"],["*Add Dependency*","*&Yes*"], 5)
          var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName","WndClass"],["*Add Dependency*","*Static*"], 5)

          if (addDepDialogBtnYes["Exists"]) {
            addDepDialogBtnYes["Click"]()
            Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")  
          }else{
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

      Log["Checkpoint"]("All options where checked")
      
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCreate")["Click"]()

      // When Save CAB file dialog opens select a path to save your file
        //Stest_Customer Solution_3.2.100.0
          var windowSaveCABFile = Aliases["Epicor"]["FindChild"]("FullName","*Save*", 30)
          if (windowSaveCABFile["Exists"]) {
            var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*","*Button*"], 30)
            windowSaveCABFileSaveBtn["Click"]()
            Log["Message"]("CAB File saved correctly")
          }else{
            Log["Error"]("CAB File wasn't saved correctly, Object doesn't exists")    
          }
          Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCancel")["Click"]()

      // Click Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      Log["Checkpoint"]("Solution for EPIC05 created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Delete the dashboard and BAQ you've created on EPIC05 (TestDashBD1 and TestBAQ1) ------------------------------------------------------'
   //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

      DeleteDashboard("TestDashBD1")
      ExitDashboard()

      //Delete BAQ

      ExpandComp("Epicor Europe")
      //Go to System Management> External Business Activity Query> External Datasource Type
      MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Business Activity Query")
        
       DeleteBAQ("TestBAQ1")
       ExitBAQ()
    
    Log["Message"]("Dashboards TestDashBD1 and baq TestBAQ1 deleted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //---  EPIC06 create the solution -----------------------------------------------------------------------------------------------------------'
     
    /*
      Step No: 19
      Result:  Click on New Query. Search for the BAQ TestBAQ2 and click Ok. Save       
    */

    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"]("SType2")
      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtDescription"]["Keys"]("SType2")
      
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      
      Log["Checkpoint"]("Solution Type was created 'SType2'")

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

    // Go to System Management> Solution Management> Solution Workbench
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Stest2")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtSolutionType"]["Keys"]("SType2")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtDescription"]["Keys"]("Stest2")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      // Click on Add To Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]()

      // On Solution Element Search select Dashboard and click Search

      var avElementsGrid = Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["tabSearchItems"]["ultraTabPageControl7"]["epiGroupBox5"]["FindChild"](["WndCaption","ClrClassName"], ["*Available*","*Grid*"], 30)

      var elemColum = getColumn(avElementsGrid,"ElementHeaderID")

      for (var i = 0; i < avElementsGrid["wRowCount"]; i++) {
        var cell = avElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

        if(cell["Text"]["OleValue"] == "Dashboard"){
          cell["Click"]()
        }
      }

      //Search button
      Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["btnSearch"]["Click"]()

      // On Advanced Element Search enter TestDashBD on Starting At and click Search
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["panel1"]["oETC"]["oETP"]["pnlBasicSrch"]["groupBox1"]["txtStartWith"]["Keys"]("TestDashBD")
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()

      // The first results should be the TestDashBD1 and TestDashBD3
      // Select them using Ctrl key and click Ok

      var advElementsGrid = Aliases["Epicor"]["AdvancedElementSearch"]["FindChild"](["WndCaption","ClrClassName"], ["*Search*","*Grid*"], 30)

      // var elemColum = getColumn(advElementsGrid,"DefinitionID")
      // var selectIndex = []
      // advElementsGrid["Rows"]["Item"](0)["Cells"]["Item"](elemColum)["Click"]()

      // for (var i = 0; i < advElementsGrid["Rows"]["Count"]; i++) {
      //   var cell = advElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

      //   var aString = cell["Text"]["OleValue"]
      //   var aSubString = "TestDashBD"
      //   var Res

      //   Res = aqString["Find"](aString, aSubString)

      //   if (Res != -1) {
      //     selectIndex.push(i)
      //   }else{
      //     break
      //   }
      // }

      // for (var i = 0; i < selectIndex["length"]-1; i++) {
      //   advElementsGrid["Keys"]("!"+"[Down]")
      // }

      advElementsGrid["Click"](87, 49);
      advElementsGrid["Keys"]("![Down]![Down]");
      Log["Checkpoint"]("Dashboards selected")


      Aliases["Epicor"]["AdvancedElementSearch"]["ultraStatusBar2"]["btnOK"]["Click"]()
      Log["Checkpoint"]("Dashboards selected and clicked ok on Advanced Element Search dialog")

      // Click Add to Solution and click Yes to the Add Dependency messages to also add the BAQs to the solution
      Aliases["Epicor"]["SolutionElementSearch"]["grpSelectedSolutionItems"]["btnAddToSolution"]["Click"]()

      while(true) {
        //find button of the "add dependency" dialog
        var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName","WndCaption"],["*Add Dependency*","*&Yes*"], 5)
        var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName","WndClass"],["*Add Dependency*","*Static*"], 5)

        if (addDepDialogBtnYes["Exists"]) {
          addDepDialogBtnYes["Click"]()
          Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")  
        }else{
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

      Log["Checkpoint"]("All options where checked")
      
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCreate")["Click"]()

      // When Save CAB file dialog opens select a path to save your file
        //Stest_Customer Solution_3.2.100.0
          var windowSaveCABFile = Aliases["Epicor"]["FindChild"]("FullName","*Save*", 30)
          if (windowSaveCABFile["Exists"]) {
            var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*","*Button*"], 30)
            windowSaveCABFileSaveBtn["Click"]()
            Log["Message"]("CAB File saved correctly")
          }else{
            Log["Error"]("CAB File wasn't saved correctly, Object doesn't exists")    
          }
          Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCancel")["Click"]()

      // Click Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      Log["Checkpoint"]("Solution for EPIC06 created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Delete the dashboards and BAQs you've created on EPIC06 (TestDashBD2 and TestBAQ2, TestDashBD3) ---------------------------------------'
   //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    
    /*
      Step: 20
      Note: Delete Dashboard and BAQ
    */

    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")

      DeleteDashboard("TestDashBD2")
      ExitDashboard()

      //Delete BAQ

      ExpandComp("Epicor Europe")
      //Go to System Management> External Business Activity Query> External Datasource Type
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
        
       DeleteBAQ("TestBAQ2")
       ExitBAQ()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //---  EPIC07 create the solution -----------------------------------------------------------------------------------------------------------'
     
    /*
      Step No: 21
      Result:       
    */

    ExpandComp("Epicor Mexico")

    // Go to System Management> Solution Management> Solution Type Maintenance
    MainMenuTreeViewSelect("Epicor Mexico;System Management;Solution Management;Solution Type Maintenance")

      // Create a new type, enter Solution Type and Description. Save
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution Type")

      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtKeyField"]["Keys"]("SType3")
      Aliases["Epicor"]["SolutionTypeForm"]["windowDockingArea1"]["dockableWindow4"]["mainPanelControl"]["windowDockingArea1"]["dockableWindow2"]["solutionTypePanel1"]["grpSolutionType"]["txtDescription"]["Keys"]("SType3")
      
      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      
      Log["Checkpoint"]("Solution Type was created 'SType3'")

      Aliases["Epicor"]["SolutionTypeForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

    // Go to System Management> Solution Management> Solution Workbench
    MainMenuTreeViewSelect("Epicor Mexico;System Management;Solution Management;Solution Workbench")

      // Create a new Solution, enter Type and Description and Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|New...|New Solution")

      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Stest3")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtSolutionType"]["Keys"]("SType3")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtDescription"]["Keys"]("Stest3")
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")

      // Click on Add To Solution
      Aliases["Epicor"]["SolutionWorkbenchForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["btnAddToSolution"]["click"]()

      // On Solution Element Search select Dashboard and click Search

      var avElementsGrid = Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["tabSearchItems"]["ultraTabPageControl7"]["epiGroupBox5"]["FindChild"](["WndCaption","ClrClassName"], ["*Available*","*Grid*"], 30)

      var elemColum = getColumn(avElementsGrid,"ElementHeaderID")

      for (var i = 0; i < avElementsGrid["wRowCount"]; i++) {
        var cell = avElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

        if(cell["Text"]["OleValue"] == "Dashboard"){
          cell["Click"]()
        }
      }

      //Search button
      Aliases["Epicor"]["SolutionElementSearch"]["grpSearchSolutionItems"]["btnSearch"]["Click"]()

      // On Advanced Element Search enter TestDashBD on Starting At and click Search
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["panel1"]["oETC"]["oETP"]["pnlBasicSrch"]["groupBox1"]["txtStartWith"]["Keys"]("TestDashBD")
      Aliases["Epicor"]["AdvancedElementSearch"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()

      // The first results should be the TestDashBD1 and TestDashBD3
      // Select them using Ctrl key and click Ok

      var advElementsGrid = Aliases["Epicor"]["AdvancedElementSearch"]["FindChild"](["WndCaption","ClrClassName"], ["*Search*","*Grid*"], 30)

      // var elemColum = getColumn(advElementsGrid,"DefinitionID")
      // var selectIndex = []
      // advElementsGrid["Rows"]["Item"](0)["Cells"]["Item"](elemColum)["Click"]()

      // for (var i = 0; i < advElementsGrid["Rows"]["Count"]; i++) {
      //   var cell = advElementsGrid["Rows"]["Item"](i)["Cells"]["Item"](elemColum)

      //   var aString = cell["Text"]["OleValue"]
      //   var aSubString = "TestDashBD"
      //   var Res

      //   Res = aqString["Find"](aString, aSubString)

      //   if (Res != -1) {
      //     selectIndex.push(i)
      //   }else{
      //     break
      //   }
      // }

      // for (var i = 0; i < selectIndex["length"]-1; i++) {
      //   advElementsGrid["Keys"]("!"+"[Down]")
      // }

      advElementsGrid["Click"](87, 49);
      advElementsGrid["Keys"]("![Down]");
      Log["Checkpoint"]("Dashboards selected")


      Aliases["Epicor"]["AdvancedElementSearch"]["ultraStatusBar2"]["btnOK"]["Click"]()
      Log["Checkpoint"]("Dashboards selected and clicked ok on Advanced Element Search dialog")

      // Click Add to Solution and click Yes to the Add Dependency messages to also add the BAQs to the solution
      Aliases["Epicor"]["SolutionElementSearch"]["grpSelectedSolutionItems"]["btnAddToSolution"]["Click"]()

      while(true) {
        //find button of the "add dependency" dialog
        var addDepDialogBtnYes = Aliases["Epicor"]["FindChild"](["FullName","WndCaption"],["*Add Dependency*","*&Yes*"], 5)
        var addDepDialog = Aliases["Epicor"]["FindChild"](["FullName","WndClass"],["*Add Dependency*","*Static*"], 5)

        if (addDepDialogBtnYes["Exists"]) {
          addDepDialogBtnYes["Click"]()
          Log["Checkpoint"]("Dialog " + addDepDialog["WndCaption"] + " clicked")  
        }else{
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

      Log["Checkpoint"]("All options where checked")
      
      Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCreate")["Click"]()

      // When Save CAB file dialog opens select a path to save your file
        //Stest_Customer Solution_3.2.100.0
          var windowSaveCABFile = Aliases["Epicor"]["FindChild"]("FullName","*Save*", 30)
          if (windowSaveCABFile["Exists"]) {
            var windowSaveCABFileSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Save*","*Button*"], 30)
            windowSaveCABFileSaveBtn["Click"]()
            Log["Message"]("CAB File saved correctly")
          }else{
            Log["Error"]("CAB File wasn't saved correctly, Object doesn't exists")    
          }
          Aliases["Epicor"]["BuildSolutionForm"]["pnlBuildSolution"]["WinFormsObject"]("btnCancel")["Click"]()

      // Click Save
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      // Click Close
      Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
      Log["Checkpoint"]("Solution for EPIC07 created")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Delete the dashboards and BAQs you've created on EPIC06 (TestDashBD2 and TestBAQ2, TestDashBD3) ---------------------------------------'
   //Go to Executive Analysis> Business Activity Management> General Operations> Dashboard. Go to Tools> Developer Mode        
    
    /*
      Step: 22
      Note: Delete Dashboard and BAQ
    */

    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
    Log["Checkpoint"]("Dashboard opened")
    
    //Enable Dashboard Developer Mode  
    DevMode()
    Log["Checkpoint"]("DevMode activated")

    //Delete BAQ
    DeleteDashboard("TestDashBD3,TestDashBD4")
    ExitDashboard()

    //Go to System Management> External Business Activity Query> External Datasource Type
    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
        
     DeleteBAQ("TestBAQ3")
     ExitBAQ()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- On EPIC05 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 23
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect("Epicor Europe;System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|Actions|Install Solution")
 
    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"]()

    var openSolutionWindow = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*Button*"],30)

    openSolutionWindow["Keys"]("Stest_Customer Solution_3.2.100.0")
    
    openSolutionWindowBtn["Click"]()

    // Leave defaults and click Install
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"]()
    
    if (Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["Exists"]) {
      Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["btnOK"]["Click"]()
    }
 
    // Click Close
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["WinFormsObject"]("btnAbort")["Click"]()
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    Log["Checkpoint"]("Solution for EPIC05 installed")
  
  //-------------------------------------------------------------------------------------------------------------------------------------------' 

  //--- EPIC05 Retrieve TestDashBD1, TestBAQ1 and TestDashBD3  --------------------------------------------------\-----------------------------'
    
    /*
      Step: 24 - 25
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD1")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD1 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD1 wasn't retrieved")
      }
      
      ExitDashboard()
    
    /*
      Step: 26 - 27
      Note: Retrieve BAQ
    */

   //Go to System Management> Business Activity Management;Setup;Business Activity Query
    MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Business Activity Query")

      Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["txtQueryID"]["Keys"]("TestBAQ1")
      Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["txtQueryID"]["Keys"]("[Tab]")

      if(Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["chkShared"]["Checked"]){
         Log["Checkpoint"]("BAQ TestBAQ1 retrieved and 'Shared' checkbox is checked")
      }else{
        Log["Error"]("BAQ TestBAQ1 wasn't retrieved or 'Shared' checkbox is not checked")
      }

      if(Aliases["Epicor"]["BAQDiagramForm"]["windowDockingArea1"]["dockableWindow2"]["allPanels1"]["windowDockingArea1"]["dockableWindow1"]["optionsPanel1"]["gbID"]["cmbExtDs"]["Text"] != ""){
         Log["Checkpoint"]("BAQ TestBAQ1 retrieved and 'External Datasource' is not empty")
      }else{
        Log["Error"]("BAQ TestBAQ1 wasn't retrieved or 'External Datasource' is empty")
      }

      ExitBAQ()

    /*
      Step: 28 - 29
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD3")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD3 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD3 wasn't retrieved")
      }
      
     if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"]("Dashboard TestDashBD3 'All Companies' checkbox is checked")
      }else{
        Log["Error"]("Dashboard TestDashBD3'All Companies' checkbox is not checked")
      }

      ExitDashboard()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- EPIC05 Delete TestDashBD1, TestBAQ1 and TestDashBD3  ----------------------------------------------------------------------------------'
    
    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD1")
      ExitDashboard()
    
   //Go to System Management> Business Activity Management;Setup;Business Activity Query
    MainMenuTreeViewSelect("Epicor Europe;System Management;External Business Activity Query;External Business Activity Query")

      DeleteBAQ("TestBAQ1")
      ExitBAQ()

    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD3")
      ExitDashboard()

    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
        
     DeleteBAQ("TestBAQ3")
     ExitBAQ()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'  

  //--- On EPIC06 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 30
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|Actions|Install Solution")
 
    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"]()

    var openSolutionWindow = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*Button*"],30)

    openSolutionWindow["Keys"]("Stest2_Customer Solution_3.2.100.0")
    
    openSolutionWindowBtn["Click"]()

    // Leave defaults and click Install
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"]()
    
    if (Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["Exists"]) {
      Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["btnOK"]["Click"]()
    }
 
    // Click Close
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["WinFormsObject"]("btnAbort")["Click"]()
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    Log["Checkpoint"]("Solution for EPIC06 installed")
  //-------------------------------------------------------------------------------------------------------------------------------------------' 

  //--- EPIC06 Retrieve TestDashBD2, TestDashBD3  ---------------------------------------------------------------------------------------------'
    
    /*
      Step: 31 - 32
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD2")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD2 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD2 wasn't retrieved")
      }
      
      if(!Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"]("Dashboard TestDashBD2 'All Companies' checkbox is not checked")
      }else{
        Log["Error"]("Dashboard TestDashBD2'All Companies' checkbox is checked")
      }

      ExitDashboard()
    
    /*
      Step: 33
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Europe;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD3")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["Exists"]) {
        Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["click"]()
      }

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD3 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD3 wasn't retrieved")
      }
      
     if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"]("Dashboard TestDashBD3 'All Companies' checkbox is checked")
      }else{
        Log["Error"]("Dashboard TestDashBD3'All Companies' checkbox is not checked")
      }

      ExitDashboard()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- EPIC06 Delete TestDashBD2, TestDashBD3  -----------------------------------------------------------------------------------------------'
    
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD2")
      ExitDashboard()
    
   //Go to System Management> Business Activity Management;Setup;Business Activity Query
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      DeleteBAQ("TestBAQ2")
      ExitBAQ()

    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD3")
      ExitDashboard()

    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;Setup;Business Activity Query")
        
     DeleteBAQ("TestBAQ3")
     ExitBAQ()
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'  

  //--- On EPIC07 Install the exported solution: ----------------------------------------------------------------------------------------------'
    /*
      Step: 34
      Note: Install the exported solution
    */

    //Go to System Management> Solution Management> Solution Type(Solution Workbench) Maintenance
    MainMenuTreeViewSelect("Epicor Mexico;System Management;Solution Management;Solution Workbench")

    // Click on Actions> Install Solution
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|Actions|Install Solution")
 
    // Click on Solution File and search for the exported file
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnBrowseSolutionFile"]["Click"]()

    var openSolutionWindow = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*ComboBox*"],30)
    var openSolutionWindowBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"], ["*Open*","*Button*"],30)

    openSolutionWindow["Keys"]("Stest3_Customer Solution_3.2.100.0")
    
    openSolutionWindowBtn["Click"]()

    // Leave defaults and click Install
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["btnInstall"]["Click"]()
    
    if (Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["Exists"]) {
      Aliases["Epicor"]["ExtDsSelectionForm"]["pnlButtons"]["btnOK"]["Click"]()
    }
    
    if (Aliases["Epicor"]["dlgWarning"]["Exists"]) {
      Aliases["Epicor"]["dlgWarning"]["btnYes"]["Click"]()
    }

    // Click Close
    Aliases["Epicor"]["InstallSolutionForm"]["pnlInstallSolution"]["WinFormsObject"]("btnAbort")["Click"]()
    Aliases["Epicor"]["SolutionWorkbenchForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    Log["Checkpoint"]("Solution for EPIC07 installed")
  //-------------------------------------------------------------------------------------------------------------------------------------------' 

  //--- EPIC07 Retrieve TestDashBD3, TestDashBD4  ---------------------------------------------------------------------------------------------'
    
    /*
      Step: 35 - 36
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD3")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["Exists"]) {
        Aliases["Epicor"]["dlgDashboardCompanyMismatchWarning"]["btnOK"]["click"]()
      }

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD3 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD3 wasn't retrieved")
      }
      
     if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"]("Dashboard TestDashBD3 'All Companies' checkbox is checked")
      }else{
        Log["Error"]("Dashboard TestDashBD3'All Companies' checkbox is not checked")
      }

      ExitDashboard()

    /*
      Step: 37
      Note: Retrieve Dashboard
    */
    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("TestDashBD4")
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Keys"]("[Tab]")

      if (Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtCaption"] != "") {
        Log["Checkpoint"]("Dashboard TestDashBD4 retrieved")
      }else{
        Log["Error"]("Dashboard TestDashBD4 wasn't retrieved")
      }
      
      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["chkAllCompanies"]["Checked"]) {
        Log["Checkpoint"]("Dashboard TestDashBD4 'All Companies' checkbox is checked")
      }else{
        Log["Error"]("Dashboard TestDashBD4'All Companies' checkbox is not checked")
      }

      var grid = Aliases["Epicor"]["Dashboard"]["dbPanel"]["FindChild"]("WndCaption", "*zCustomer01*", 30)
      if(grid["Exists"]){
        Log["Checkpoint"]("TestDashBD4 is retrieved and it includes the zCustomer01 query")
      }else{
        Log["error"]("TestDashBD4 is retrieved and it doesn't include the zCustomer01 query")
      }

      ExitDashboard()      
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- EPIC06 Delete TestBAQ3, TestDashBD3,TestDashBD4 ---------------------------------------------------------------------------------------'
    
    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD3")
      ExitDashboard()
    
   //Go to System Management> Business Activity Management;Setup;Business Activity Query
    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

      DeleteBAQ("TestBAQ3")
      ExitBAQ()

    MainMenuTreeViewSelect("Epicor Mexico;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      DeleteDashboard("TestDashBD4")
      ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

   DeactivateFullTree()

   CloseSmartClient()

}


