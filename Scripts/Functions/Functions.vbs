'Log in function
Function Login(username,password, shellMenu)

  'Activates Shell menu'
  if shellMenu = "Classic" then
      if  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").IsChecked = False then 
       Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").Keys(" ")
      End if
  else
      if  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").IsChecked = True then 
       Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").Keys(" ")
      End if
  end if

  'Enters the text 'epicor' in the 'txtUserID' text editor.

  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtUserID.Keys(username)

  'Enters 'Epicor123[Enter]' in the 'txtPassword' object.

  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtPassword.Keys(password+"[Enter]")

  Aliases.Epicor2.HwndSource_MainWindow

    'Activates Shell menu'
  if shellMenu = "Classic" then
      set menuSCType = Aliases.Epicor2.MenuForm
  else
      set menuSCType = Aliases.Epicor2.ShellMenuForm
  end if

  'Aliases.Epicor2.MenuForm.Visible
  While menuSCType.Exists = False
    Call Delay(5000)
    count = count + 1
    if (count = 90) then
      Call Log.Error("Menu not appear")
      Call Runner.Stop(False)
    End if      
  WEnd
  
End Function

'Change to dev mode function
Function DevMode()
  'Enable Dashboard Developer Mode  
  If (Not Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea2.dockableWindow5.Exists) Then
    Call Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.txtDefinitonID.txtDefinitonID_EmbeddableTextBox.Keys("~[ReleaseLast]t[Down][Down][Down][Enter]")
  End If
End Function

'Create simple BAQ (one table)
function simpleBAQ(baqID, baqDescription, table, columns)
  'Click New BAQ
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("[0]|&File|New")
  
  Set BAQFormDefinition = Aliases.Epicor2.BAQDiagramForm.windowDockingArea1.dockableWindow2.allPanels1.windowDockingArea1
  
  'Set ID and description
  Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtQueryID.Keys(baqID)
  Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtDescription.Keys(baqDescription)
  
  'Change to "Query Builder" tab
  Call BAQFormDefinition.dockableWindow2.Activate

  Set BAQFormPanel = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.diagramQueryPanel.splitMain.SplitterPanel.splitDiagramWhere.SplitterPanel.pnlQueryVisual.windowDockingArea1.dockableWindow1.diagramPanel.windowDockingArea1.dockableWindow2.palettePanel
  
  Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.ClickItemXY(" Filter|[0]", 49, 7)
  Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.zEpiBasePanel_Toolbars_Dock_Area_Top_EmbeddableTextBox.SetText(table)

  'Open Erp.Part table
  Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Click(49, 11)
  Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Keys("[Enter]")
  
  'Activate Display Fields
  Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.Activate

  'Select columns'
  Set fieldsAvailable = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.availColTreePanel1.treeAvailCol
  
  'Scplit string columns into an array  
  Dim arrColumns, arrColCount, countTreeList, j
  arrColumns = Split(columns, ",")
  
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

  'Activate Analyze tab
  Call BAQFormDefinition.dockableWindow4.Activate

  'Analyze and run test for BAQ
  Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnAnalyze.Click
  Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnTest.Click
  
  if BAQFormDefinition.dockableWindow4.analyzePanel1.WinFormsObject("queryExecPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("queryResultsPanel1").WinFormsObject("grdResults").Rows.Count < 0 then
    Call log.Warning("Returned 0 Records")    
  end if
  
  'Save BAQ
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|&Save")  
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|E&xit") 
    
End Function 

'Basic deployment for dashboards'
Function Dashboards(dashboardID, dashboardCaption, dashDescription, baqQuery)

  'Click New Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&New...|New Dashboard")

  'Type Definition, Caption and Description
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.txtDefinitonID.Keys(dashboardID)
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtCaption").Keys(dashboardCaption)
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtDescription").Keys(dashDescription)
  
  Dim i, arrBaqQueries
  arrBaqQueries = Split(baqQuery, ",")
  
  arrQueriesCount = ubound(arrBaqQueries)

  for i = 0 to arrQueriesCount
    'Add BAQ
    Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&New...|New Query")
    Aliases.Epicor2.WinFormsObject("DashboardProperties").WinFormsObject("FillPanel").WinFormsObject("QueryPropsPanel").WinFormsObject("PropertiesPanel_Fill_Panel").WinFormsObject("txtQueryID").Keys(arrBaqQueries(i))
    Aliases.Epicor2.WinFormsObject("DashboardProperties").WinFormsObject("btnOkay").Click
  Next

  'Save the Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&Save")
  if dashboardCaption = "" OR dashDescription = "" then 
    if Aliases.Epicor2.ExceptionDialog.exceptionDialogFillPanel.WinFormsObject("rtbMessage").Exists then
      Call Log.Message("Validated correctly: Cancelling Save operation: Dashboard Description and Caption is required.")
      Aliases.Epicor2.ExceptionDialog.exceptionDialogFillPanel.btnOk.Click
    end if
  end if

  Call Delay(1000)

  'Deploy Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&Tools|Deploy Dashboard")
  
  if dashboardCaption = "" OR dashDescription = "" then 
    if Aliases.Epicor2.ExceptionDialog.exceptionDialogFillPanel.WinFormsObject("rtbMessage").Exists then
      Call Log.Message("Validated correctly: Cancelling AppBuilder operation: Dashboard Description and caption is required.")
      Aliases.Epicor2.ExceptionDialog.exceptionDialogFillPanel.btnOk.Click
      Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtCaption").Keys("DashB Testing")
      Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtDescription").Keys("DashB Testing")
    end if
  end if
  
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkDeployApplication").Checked =true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkAddMenuTab").checked = true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkAddFavItem").Checked = true 
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWebAccess").WinFormsObject("chkGenWebForm").Checked = true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("btnOk").Click
  Call Delay(10000)
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").Close
  
End Function

' Create menu from menu maintenance for dashboards
Function CreateMenu(menuLocation, menuID, menuName, menuType, dashboardDll)
  Set MenuMFormTree = Aliases.Epicor2.MenuMForm.windowDockingArea2.dockableWindow2.jobTreeViewPanel.methodTree
  CountElem = MenuMFormTree.SelectedNodes.Item(0).Nodes.Count -1 

  Dim i, arrMenuLocation
  arrMenuLocation = Split(menuLocation, ">")
  
  arrMenuOptCount = ubound(arrMenuLocation)

  for i = 1 to arrMenuOptCount
    for j = 0 to CountElem
      MenuMFormTree.SelectedNodes.Item(0).Nodes.Item(j).Expanded = true
      if arrMenuLocation(i) = MenuMFormTree.SelectedNodes.Item(0).Nodes.Item(j).Text then
          MenuMFormTree.ActiveNode = MenuMFormTree.SelectedNodes.Item(0).Nodes.Item(j)
          MenuMFormTree.SelectedNodes.Item(0).Nodes.Item(j).Selected = true
          exit for
      end if 
    Next
  Next

  'Add New Item
  Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|New...|New Menu")

  'Fill the information
  Set MenuFormObject = Aliases.Epicor2.MenuMForm.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow3").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("detailPanel1")
  MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtMenuID").Keys(menuID)
  MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtName").Keys(menuName)
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboType").Keys("[BS][BS][BS][BS][BS][BS][BS][BS][BS]")
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboType").Keys(menuType)
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys(dashboardDll)
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys("[Tab]")
  
    Call Delay(1000)
      'Save Menu Item
    Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|&Save")
    Call Delay(1000)
    Call Aliases.Epicor2.MenuMForm.Close
  
End Function

' deploy from Dashboard maintenance
Function dashboardMaintenance(dashboardID, actions)
  Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys(dashboardID)
  Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("[Tab]")

  'Selection the option for the Actions listed

  Dim i, arrActions
  arrActions = Split(actions, ",")

  arrActionsCount = ubound(arrActions)

  for i = 0 to arrActionsCount
      Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|" + arrActions(i))

      if arrActions(i) = "Modify Dashboard" then
        Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|Copy Dashboard") 
        Aliases.Epicor2.WinFormsObject("CopyDashboardForm").WinFormsObject("txtDefinitionId").WinFormsObject("txtDefinitionId_EmbeddableTextBox").Keys(dashboardID + "Copy")
        Aliases.Epicor2.WinFormsObject("CopyDashboardForm").WinFormsObject("btnOkay").Click

        Aliases.Epicor2.WinFormsObject("Dashboard").WinFormsObject("dbPanel").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").Activate

        Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("chkMobile").CheckState = "Checked"

        Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&Save")
        Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&Tools|Deploy Dashboard")

        Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpMobile").WinFormsObject("chkGenMobile").Checked = true
        Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpMobile").WinFormsObject("chkMobileMenuAvail").Checked = true
        Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("btnOk").Click
        Call Delay(5000)
        Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").Close
        Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|E&xit")
    else 
      If arrActions(i) = "Deploy UI Application" Then
        call log.Message("Action Deploy UI Application")
      End If
      end if
  Next

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&File|E&xit")  
  
End Function

'Test refresh dashboard result records'
Function refreshDashboard(shellMenu)
  Call Delay(25000)
  
If shellMenu = "Classic" Then
  Call Aliases.Epicor2.MenuForm.windowDockingArea2.DockableWindow.z.zMyForm_Toolbars_Dock_Area_Top.ClickItem("[0]|Refresh")

  Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpFields").WinFormsObject("pnlFields").WinFormsObject("edt0").Keys("23")
  Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpButtons").WinFormsObject("btn0").Click

  if Aliases.Epicor2.MenuForm.windowDockingArea2.DockableWindow.z.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("MainPanel").WinFormsObject("MainDockPanel").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq2_1ViewDockPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq2_1ViewListPanel1").WinFormsObject("myGrid").Rows.Count < 0 then
    Call log.Warning("Returned 0 Records")    
  else
    Call log.Message("Returned more than 0 Records")    
  end if
  Call Delay(1000)
  Call Aliases.Epicor2.MenuMForm.Close
  
else
  Call Aliases.Epicor2.MainController.Activate
  While Aliases.Epicor2.MainController.Exists = False
    Call Delay(5000)
    count = count + 1
    if (count = 90) then
      Call Log.Error("Menu not appear")
      Call Runner.Stop(False)
    End if      
  WEnd

  Call Aliases.Epicor2.MainController.windowDockingArea1.dockableWindow1.FillPanel.AppControllerPanel.zMyForm_Toolbars_Dock_Area_Top.ClickItem("[0]|&Edit|Refresh")
  
  Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpFields").WinFormsObject("pnlFields").WinFormsObject("edt0").Keys("23")
  Aliases.Epicor2.WinFormsObject("BaseForm").WinFormsObject("pnlMain").WinFormsObject("grpButtons").WinFormsObject("btn0").Click
  
  If Aliases.Epicor2.MainController.windowDockingArea1.dockableWindow1.FillPanel.AppControllerPanel.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("MainPanel").WinFormsObject("MainDockPanel").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq2_1ViewDockPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq2_1ViewListPanel1").WinFormsObject("myGrid").Rows.Count < 0 Then
    Call log.Warning("Returned 0 Records")    
  else
    Call log.Message("Returned more than 0 Records")    
  end if
  
  Call Delay(1000)
  Call Aliases.Epicor2.ShellMenuForm.Close
End If

End Function

Function addTableCriteriaBAQ(column, dropValue, epiUltraGrid)
  '--- Get the column index
  set allColumns = epiUltraGrid.DisplayLayout.Bands.Item(0).Columns

  if allColumns.Count = null OR allColumns.Count = 0 then
    Call log.Message("Grid has no columns")
  end if

  for i = 0 to allColumns.Count  
    colCurrent = allColumns.Item(i).ColumnChooserCaptionResolved.OleValue

    if allColumns.Item(i).IsVisibleInLayout AND colCurrent = column then
      columnIndex = allColumns.Item(i).Index    
      exit for
   end if                 
  Next

  Call log.Message("Column " & column & " Index " & columnIndex)

  '--- Get the row index
  set rows = epiUltraGrid.DisplayLayout.Rows

  if rows.Count = null OR rows.Count = 0 then
    Call log.Message("Grid has no rows")
  end if

  ' for i = 0 to rows.Count                  
  'if rows.Item(i).Cells.Item(columnIndex).Text.OleValue = epiUltraGrid.Rows.Count -1  then
  'Used only for this test case only one criteria added
  rowIndex = epiUltraGrid.Rows.Count -1    
  'exit for
  'end if                 
  'Next
  Call log.Message("Row Index " & rowIndex)

  '--- Get Cell after finding Column and row index
  set cell = epiUltraGrid.Rows.Item(rowIndex).Cells.Item(columnIndex)

  '--- Activate cell
  Call epiUltraGrid.Rows.Item(cell.Row.Index).Activate

  set rect = cell.GetUIElement().Rect

  Call epiUltraGrid.DblClick(rect.X + rect.Width - 5, rect.Y + rect.Height/2)

  Call cell.ShowDropDown()

  while cell.DroppedDown = False
    Call Delay(5000)
    if cell.DroppedDown = False then
      Log.Message("DROPDOWN CONTROL NOT SHOWN, SECOND ATTEMPT")      
      Call epiUltraGrid.DblClick(rect.X + rect.Width - 5, rect.Y + rect.Height/2)
      Call cell.ShowDropDown()             
    while cell.DroppedDown = False
      Call Delay(500)
      if cell.DroppedDown = False then
        Call log.Error("Dropdown list not found")
      end if
    Wend
    end if 
  Wend

  'Gather the items in the dropdown into an array
  set ddValueList = cell.EditorResolved.ValueList               
  dim listItemsCount

  if ddValueList.ClrClassName = "UltraCombo" then
    listItemsCount = ddValueList.Rows.Count
    redim aRes(listItemsCount)
    for  i = 0 to listItemsCount
      aRes(i) = ddValueList.Rows.Item(i).Cells.Item(0).Text.OleValue
    Next
  end if
  if ddValueList.ClrClassName = "ValueList" then
    listItemsCount = ddValueList.ValueListItems.Count
    redim aRes(listItemsCount)
    for i = 0 to listItemsCount - 1
      aRes(i) = ddValueList.ValueListItems.Item(i).DisplayText.OleValue
    Next
    end if
    if ddValueList.ClrClassName = "EpiCombo" then 
      listItemsCount = ddValueList.Rows.Count
      redim aRes(listItemsCount)
      for i = 0 to listItemsCount
        aRes(i) = ddValueList.Rows.Item(i).Cells.Item(1).Text.OleValue
      Next
    else 
      Call log.Message("Unable to get items from value list. Method for this class is not implemented")
  end if

  'find value index in dropdown list 
  for i = 0 to ubound(aRes) -1  
    comboValue = aqString.Trim(aRes(i))
    call epiUltraGrid.Keys("[Down]")
    if  comboValue = aqString.Trim(dropValue) then
      'Select the value from the combo
      '
      indexValue = i
      epiUltraGrid.Keys("[PageUp]")
      exit for
    end if
  next                      

For i = 0 to indexValue - 1

  call epiUltraGrid.Keys("[Down]")
Next
  
  'close dropdown list
  Call epiUltraGrid.Click(rect.X + 1, rect.Y + rect.Height/2)

  if comboValue <> aqString.Trim(dropValue) then      
    Call log.Error("Cell_DropDownSelect failed ")   
  end if 

  Call Log.Message("Value '" & dropValue & "' selected from dropdown list") 
  
  'Click on link for Filter Value
  if column = "Filter Value" then 
    Call cell.Activate()
    Call cell.Column.PerformAutoResize()
  
    set aLinks = epiUltraGrid.GetLinks(cell)
  
    if aLinks.Count = 0 then
      Call log.Message("Cell has no links")
    else
      Call log.Message("Cell has links")
    end if 
  
    set link = aLinks.Item(0)
    linkText = "specified"
    for i = 0 to aLinks.Count
      set link = aLinks.Item(i) 'get current link
      if link.Text = linkText then
        exit for
      end if
    Next
  
   'all links in the cell checked. No link with <linkText> found
    if (link.Text <> linkText) then
       Call log.Error( "Unable to find link '" & linkText & "'")
    end if
            
    call epiUltraGrid.Click(link.Bounds.Left + link.Bounds.Width/2, link.Bounds.Top + link.Bounds.Height/2)
   end if

End Function