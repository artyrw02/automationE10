'USEUNIT Functions
'USEUNIT Expand_Tree

Sub Dashboard_BAQ_Parameters()

  'Runs the "Epicor" tested application.
  Call TestedApps.Epicor.Run

  Call Login("epicor", "Epicor123", "Classic")
  
  'Select Company
  Call ExpandComp("Epicor Education")
  'Select Plant
  Call ChangePlant("Main Plant") 

  if Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.SelectedNodes.Item(0).HasNodes = false then
    Call Aliases.Epicor2.MenuForm.UltraMainMenu.Check("View|Full Tree", True)
  end if

  '--- Creates BAQs ---------------------------------------------------------------------------------------------------------------------------'
    
      'Expand Executive Analysis
      Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).expanded = true
      'Expand Business Activity Management
      Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).expanded = true
      'Expand General Operation
      Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).expanded = true
      'Open Business query Analysis
      Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = true
      Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

      '****** Creating BAQ1 ***************'
        'Click New BAQ
        Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("[0]|&File|New")
        
        Set BAQFormDefinition = Aliases.Epicor2.BAQDiagramForm.windowDockingArea1.dockableWindow2.allPanels1.windowDockingArea1
        
        'Set ID and description
        Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtQueryID.Keys("baq2")
        Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtDescription.Keys("baq2")
        
        'Change to "Query Builder" tab
        Call BAQFormDefinition.dockableWindow2.Activate

        Set BAQFormPanel = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.diagramQueryPanel.splitMain.SplitterPanel.splitDiagramWhere.SplitterPanel.pnlQueryVisual.windowDockingArea1.dockableWindow1.diagramPanel.windowDockingArea1.dockableWindow2.palettePanel
        
        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.ClickItemXY(" Filter|[0]", 49, 7)
        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.zEpiBasePanel_Toolbars_Dock_Area_Top_EmbeddableTextBox.SetText("Customer")

        'Open Erp.Customer table
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Click(49, 11)
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Keys("[Enter]")
        
        'Activate Display Fields
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.Activate

        'Select columns'
        Set fieldsAvailable = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.availColTreePanel1.treeAvailCol
        
        columnsBAQ = "CustNum"

        'Split string columns into an array  
        Dim arrColumns, arrColCount, countTreeList, j
        arrColumns = Split(columnsBAQ, ",")
        
        arrColCount = ubound(arrColumns)
          
        'Expand table's items for selection
        fieldsAvailable.Nodes.Item(0).expanded = true
        
        'Count number of items in table
        countTreeList = fieldsAvailable.Nodes.Item(0).Nodes.Count -1
        
        for i = 0 to arrColCount
          for j = 0 to countTreeList
            if arrColumns(i) = fieldsAvailable.Nodes.Item(0).Nodes.Item(j).Text then
              fieldsAvailable.Nodes.Item(0).Nodes.Item(j).Selected = true      
              exit for
            end if 
          Next
        Next

        'Move selected columns to right panel
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.btnMoveRight.Click  

        'Save BAQ1
        Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|&Save")  

      '****** End of BAQ1 creation ********'

      '****** Creating BAQ2 ***************'
        'Click New BAQ2
        Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("[0]|&File|New")
        
        'Set ID and description
        Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtQueryID.Keys("baq3")
        Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtDescription.Keys("baq3")
        
        'Change to "Query Builder" tab
        Call BAQFormDefinition.dockableWindow2.Activate

       'Activate Phrase Build
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.Activate
       
        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.ClickItemXY(" Filter|[0]", 49, 7)
        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.zEpiBasePanel_Toolbars_Dock_Area_Top_EmbeddableTextBox.SetText("OrderDtl")

        'Open Erp.OrderDtl table
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Click(49, 11)
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Keys("[Enter]")

        'Activate Display Fields
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.Activate

        'Select columns'
        set fieldsAvailable = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.availColTreePanel1.treeAvailCol      

        'Select columns for BAQ2'
        columnsBAQ = "Company,OrderNum,PartNum,CustNum"

        'Split string columns into an array  
       
        arrColumns = Split(columnsBAQ, ",")
        
        arrColCount = ubound(arrColumns)
          
        'Expand table's items for selection
        fieldsAvailable.Nodes.Item(0).expanded = true
        
        'Count number of items in table
        countTreeList = fieldsAvailable.Nodes.Item(0).Nodes.Count -1
        
        for i = 0 to arrColCount
          for j = 0 to countTreeList
            if arrColumns(i) = fieldsAvailable.Nodes.Item(0).Nodes.Item(j).Text then
              fieldsAvailable.Nodes.Item(0).Nodes.Item(j).Selected = true      
              exit for
            end if 
          Next
        Next

        'Move selected columns to right panel
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.btnMoveRight.Click  

        'Activate Phrase Build
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.Activate

        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.ClickItemXY(" Filter|[0]", 49, 7)
        Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.zEpiBasePanel_Toolbars_Dock_Area_Top_EmbeddableTextBox.SetText("Customer")

        'Open Erp.OrderDtl table
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Click(49, 11)
        Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Keys("[Enter]")

              'Activate Display Fields
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.Activate

        'Select columns'
        set fieldsAvailable = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.availColTreePanel1.treeAvailCol
        
        columnsBAQ = "CustNum"

        'Split string columns into an array  
        arrColumns = Split(columnsBAQ, ",")
        
        arrColCount = ubound(arrColumns)
          
        'Expand table's items for selection
        fieldsAvailable.Nodes.Item(1).expanded = true
        
        'Count number of items in table
        countTreeList = fieldsAvailable.Nodes.Item(1).Nodes.Count -1
        
        for i = 0 to arrColCount
          for j = 0 to countTreeList
            if arrColumns(i) = fieldsAvailable.Nodes.Item(1).Nodes.Item(j).Text then
              fieldsAvailable.Nodes.Item(1).Nodes.Item(j).Selected = true      
              exit for
            end if 
          Next
        Next

        'Move selected columns to right panel
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.btnMoveRight.Click  

        'Activate Phrase Build tab
        Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.Activate

        '---------- Setting params BAQ 2 --------'
             Set diagram = Aliases.Epicor2.BAQDiagramForm.windowDockingArea1.dockableWindow2.allPanels1.windowDockingArea1.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.diagramQueryPanel.splitMain.SplitterPanel.splitDiagramWhere.SplitterPanel.pnlQueryVisual.windowDockingArea1.dockableWindow1.diagramPanel.windowDockingArea1.dockableWindow1.detailPanel1.windowDockingArea2.dockableWindow2.diagCanvas
              diagram.HScroll.Pos = 0
              diagram.VScroll.Pos = 0
              Call diagram.Click(258, 55)

              Set queryCondPanel = Aliases.Epicor2.BAQDiagramForm.windowDockingArea1.dockableWindow2.allPanels1.windowDockingArea1.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.diagramQueryPanel.splitMain.SplitterPanel.splitDiagramWhere.SplitterPanel2.pnlQueryTabs
              Call queryCondPanel.zEpiDockManagerPanel_Toolbars_Dock_Area_Top.ClickItem("Filtering toolbar|Add Row")
              Set tableWhereClausePanel = queryCondPanel.windowDockingArea1.dockableWindow3.tableWhereClausePanel
              Set epiUltraGrid = tableWhereClausePanel.grdTableWhere
              
              Call addTableCriteriaBAQ("Field", "CustNum", epiUltraGrid)
              Call addTableCriteriaBAQ("Operation", "MATCHES", epiUltraGrid)
              Call addTableCriteriaBAQ("Filter Value", "specified parameter", epiUltraGrid)

              'Select Parameter'
              Aliases.Epicor2.ParameterForm.btnDefine.Click

              'File New Parameter'
              Aliases.Epicor2.CtrlDesignerForm.WinFormsObject("sonomaFormToolbarsDockAreaTop").ClickItem("[0]|&File|&New")

              set queryParametersdialog = Aliases.Epicor2.CtrlDesignerForm.windowDockingArea2.dockableWindow3.mainPanel1.windowDockingArea1.dockableWindow2.detailPanel1.windowDockingArea1
              'Parameter name
              queryParametersdialog.dockableWindow1.ctrlDescrPanel1.txtFldName.Keys("CustNum")
              queryParametersdialog.dockableWindow1.ctrlDescrPanel1.WinFormsObject("cmbDataType").Keys("int")
              queryParametersdialog.dockableWindow1.ctrlDescrPanel1.WinFormsObject("cmbDataType").Keys("[TAB]")
              queryParametersdialog.dockableWindow1.ctrlDescrPanel1.WinFormsObject("cmbEditorType").Keys("DropDown List")
              queryParametersdialog.dockableWindow1.ctrlDescrPanel1.WinFormsObject("cmbDataFrom").Keys("BAQ")

              'Query ID'
              queryParametersdialog.WinFormsObject("dockableWindow2").WinFormsObject("ctrlCustomEditor1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("ctrlBAQList1").WinFormsObject("txtExportID").Keys("baq2")
              'Display Column
              queryParametersdialog.WinFormsObject("dockableWindow2").WinFormsObject("ctrlCustomEditor1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("ctrlBAQList1").WinFormsObject("cmbDisplay").Keys("Customer_CustNum")
              'DisplayValue
              queryParametersdialog.WinFormsObject("dockableWindow2").WinFormsObject("ctrlCustomEditor1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("ctrlBAQList1").WinFormsObject("cmbValue").Keys("Customer_CustNum")

              'Save'
              Aliases.Epicor2.CtrlDesignerForm.WinFormsObject("sonomaFormToolbarsDockAreaTop").ClickItem("[0]|&File|&Save")

              'Exit
              Aliases.Epicor2.CtrlDesignerForm.WinFormsObject("sonomaFormToolbarsDockAreaTop").ClickItem("[0]|&File|E&xit")

              'Select parameter
              Aliases.Epicor2.ParameterForm.WinFormsObject("btnOK").Click

        '---------- END Params BAQ 2 ------------'
      
        'Activate Analyze tab
        Call BAQFormDefinition.dockableWindow4.Activate
        'Analyze and run test for BAQ
        Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnAnalyze.Click
        Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnTest.Click

        Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpFields").WinFormsObject("pnlFields").WinFormsObject("edt0").Keys("23")
        Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpButtons").WinFormsObject("btn0").Click

        if BAQFormDefinition.dockableWindow4.analyzePanel1.WinFormsObject("queryExecPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("queryResultsPanel1").WinFormsObject("grdResults").Rows.Count < 0 then
          Call log.Warning("Returned 0 Records")    
        end if

      '****** End of BAQ2 Creation ********'

      'Save BAQ
      Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|&Save")  
      Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|E&xit") 

  '--- End creation of BAQs -------------------------------------------------------------------------------------------------------------------'  
  
  '--- Creates Dashboards ---------------------------------------------------------------------------------------------------------------------'

    'Expand Executive Analysis
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).expanded = true
    'Expand Business Activity Management
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).expanded = true
    'Expand General Operation
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(1).expanded = true
    'Open Dashboard
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(1).Nodes.Item(1).Selected = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    'Enable Dashboard Developer Mode  
    Call DevMode()

    'Creating dashboard'
    Call Dashboards("DashBBAQ", "DashBBAQ", "DashBBAQ description", "baq2")

  '--- End Creates Dashboards -----------------------------------------------------------------------------------------------------------------'
  
  '--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'

    'Close Epicor
    Call Aliases.Epicor2.Close
    Call Delay(5000)
    
    'Runs the "Epicor" tested application.
    Call TestedApps.Epicor.Run

    'log in
    Call Login("epicor", "Epicor123", "NonClassic")

  '--- End Restart Smart client --------------------------------------------------------------------------------------------------------------'
  
  '--- Test Deployed Dashboard ---------------------------------------------------------------------------------------------------------------'
    
    Call refreshDashboard("NClassic")

  '--- End Test Deployed Dashboard -----------------------------------------------------------------------------------------------------------'

End Sub

