Sub Dashboard_deployment()

'Runs the "Epicor" tested application.
  Call TestedApps.Epicor.Run
  
'Activates Shell menu'
if  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").IsChecked = False then 
  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").Keys(" ")
End if

'Enters the text 'epicor' in the 'txtUserID' text editor.

Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtUserID.Keys("epicor")

'Enters 'Epicor123[Enter]' in the 'txtPassword' object.

Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtPassword.Keys("Epicor123[Enter]")

  Aliases.Epicor2.HwndSource_MainWindow
  
  'Aliases.Epicor2.MenuForm.Visible
  While Aliases.Epicor2.MenuForm.Exists = False
      Call Delay(5000)
      count = count + 1
      if (count = 90) then
        Call Log.Error("Menu not appear")
        Call Runner.Stop(False)
      End if      
  WEnd
  
  if Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.SelectedNodes.Item(0).HasNodes = false then
    Call Aliases.Epicor2.MenuForm.UltraMainMenu.Check("View|Full Tree", True)
  end if
  
 'Expand Executive Analysis
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).expanded = true
  
  'Expand Business Activity Management
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).expanded = true
  
  'Expand General Operation
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).expanded = true
  
  'Open Dashboard
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")
      
  'Click New BAQ
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("[0]|&File|New")
  
  Set BAQFormDefinition = Aliases.Epicor2.BAQDiagramForm.windowDockingArea1.dockableWindow2.allPanels1.windowDockingArea1
  
  'Set ID and description
  Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtQueryID.Keys("baq1")
  Call BAQFormDefinition.dockableWindow1.optionsPanel1.gbID.txtDescription.Keys("baq")
  
  'Change to "Query Builder" tab
  Call BAQFormDefinition.dockableWindow2.Activate

  Set BAQFormPanel = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow1.diagramQueryPanel.splitMain.SplitterPanel.splitDiagramWhere.SplitterPanel.pnlQueryVisual.windowDockingArea1.dockableWindow1.diagramPanel.windowDockingArea1.dockableWindow2.palettePanel
  
  Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.ClickItemXY(" Filter|[0]", 49, 7)
  Call BAQFormPanel.zEpiBasePanel_Toolbars_Dock_Area_Top.zEpiBasePanel_Toolbars_Dock_Area_Top_EmbeddableTextBox.SetText("Part")

  'Open Erp.Part table
  Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Click(49, 11)
  Call BAQFormPanel.EpiBasePanel_Fill_Panel.lbTables.Keys("[Enter]")
  
  'Activate Display Fields
  Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.Activate

  'Select columns'
  Set fieldsAvailable = BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.availColTreePanel1.treeAvailCol
  
  fieldsAvailable.Nodes.Item(0).expanded = true
  fieldsAvailable.Nodes.Item(0).Nodes.Item(0).Selected = true 'Company
  fieldsAvailable.Nodes.Item(0).Nodes.Item(1).Selected = true 'ParnNum
  fieldsAvailable.Nodes.Item(0).Nodes.Item(3).Selected = true 'PartDescription
  fieldsAvailable.Nodes.Item(0).Nodes.Item(7).Selected = true 'TypeCode
  fieldsAvailable.Nodes.Item(0).Nodes.Item(10).Selected = true 'UnitPrice

  'Move selected columns to right panel
  Call BAQFormDefinition.dockableWindow2.subQueryPanel1.windowDockingArea1.dockableWindow2.displayPanel.windowDockingArea1.dockableWindow1.fieldsChooserPanel.gbColumns.btnMoveRight.Click
  
  'Activate Analyze tab
  Call BAQFormDefinition.dockableWindow4.Activate

  'Analyze and run test for BAQ
  Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnAnalyze.Click
  Call BAQFormDefinition.dockableWindow4.analyzePanel1.pnlButtons.btnTest.Click
  
  'Save BAQ
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|&Save")  
  Call Aliases.Epicor2.BAQDiagramForm.ToolbarsDockAreaTop.ClickItem("Main Menu|&File|E&xit")  
    
  '-------------------------------------------------------------------

Sub Dashboard_deployment()

'Runs the "Epicor" tested application.
  Call TestedApps.Epicor.Run
  
'Activates Shell menu'
if  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").IsChecked = False then 
  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").Keys(" ")
End if

'Enters the text 'epicor' in the 'txtUserID' text editor.

Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtUserID.Keys("epicor")

'Enters 'Epicor123[Enter]' in the 'txtPassword' object.

Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtPassword.Keys("Epicor123[Enter]")

  Aliases.Epicor2.HwndSource_MainWindow
  
  'Aliases.Epicor2.MenuForm.Visible
  While Aliases.Epicor2.MenuForm.Exists = False
      Call Delay(5000)
      count = count + 1
      if (count = 90) then
        Call Log.Error("Menu not appear")
        Call Runner.Stop(False)
      End if      
  WEnd
  
  if Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.SelectedNodes.Item(0).HasNodes = false then
    Call Aliases.Epicor2.MenuForm.UltraMainMenu.Check("View|Full Tree", True)
  end if

 '---------------- erase above
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
  If (Not Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea2.dockableWindow5.Exists) Then
    Call Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.txtDefinitonID.txtDefinitonID_EmbeddableTextBox.Keys("~[ReleaseLast]t[Down][Down][Down][Enter]")
  End If
  
  'Click New Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&New...|New Dashboard")

  'Type Definition, Caption and Description
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.txtDefinitonID.Keys("DashB")
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtCaption").Keys("DashB Testing")
  Aliases.Epicor2.Dashboard.dbPanel.windowDockingArea1.dockableWindow2.pnlGeneral.windowDockingArea1.dockableWindow1.pnlGenProps.WinFormsObject("txtDescription").Keys("DashB Testing")
  
  'Add BAQ
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&New...|New Query")
  Aliases.Epicor2.WinFormsObject("DashboardProperties").WinFormsObject("FillPanel").WinFormsObject("QueryPropsPanel").WinFormsObject("PropertiesPanel_Fill_Panel").WinFormsObject("txtQueryID").Keys("baq1")
  Aliases.Epicor2.WinFormsObject("DashboardProperties").WinFormsObject("btnOkay").Click

  'Save the Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|&Save")
  
  'Deploy Dashboard
  Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&Tools|Deploy Dashboard")
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkDeployApplication").Checked =true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkAddMenuTab").checked = true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkAddFavItem").Checked = true 
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWebAccess").WinFormsObject("chkGenWebForm").Checked = true
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("btnOk").Click
  Call Delay(5000)
  Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").Close
  
  'Close Dashboard Maintenance
  Call Aliases.Epicor2.Dashboard.Close
  
  'Create menu
    
 'Open Menu Maintenance
  'Expand System Setup
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).expanded = true
  'Expand Security Maintenance
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).expanded = true
  'Open Menu Maintenance 
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).Nodes.Item(2).Selected = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")
  
  'Select "Main Menu> Sales Management> CRM> Setup" in Menu Maintenance
  Set MenuMFormTree = Aliases.Epicor2.MenuMForm.windowDockingArea2.dockableWindow2.jobTreeViewPanel.methodTree
  MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Expanded = True
  MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Expanded = True
  MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Expanded = True
  MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = true
  MenuMFormTree.ActiveNode = MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0)
        
  'Add New Item
  Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|New...|New Menu")
  
  'Fill the information
  Set MenuFormObject = Aliases.Epicor2.MenuMForm.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow3").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("detailPanel1")
  MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtMenuID").Keys("DashMenu")
  MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtName").Keys("Dash Menu")
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboType").Keys("[BS][BS][BS][BS][BS][BS][BS][BS][BS]Dashboard-Assembly")
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys("Dash")
  MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys("[Tab]")
  
     'Save Menu Item
  Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|&Save")
  Call Aliases.Epicor2.MenuMForm.Close
  
  'Close Epicor
  Call Aliases.Epicor2.Close
  Call Delay(5000)

  '----

  'Re run epicor erp to test deploymeny

  'Runs the "Epicor" tested application.
  Call TestedApps.Epicor.Run
  
  'Activates Shell menu'
  if  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").IsChecked = False then 
    Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.WPFObject("chkClassic").Keys(" ")
  End if

  'Enters the text 'epicor' in the 'txtUserID' text editor.

  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtUserID.Keys("epicor")

  'Enters 'Epicor123[Enter]' in the 'txtPassword' object.

  Aliases.Epicor2.ShellMenuForm.shellControlHost.HwndSource_AdornerDecorator.AdornerDecorator.AvalonAdapter.Shell.Grid.ShellGrid.ContentPanel.userControl.MainGrid.logon.LogonPopup.Grid.LoginGrid.txtPassword.Keys("Epicor123[Enter]")

    Aliases.Epicor2.HwndSource_MainWindow
    
    'Aliases.Epicor2.MenuForm.Visible
    While Aliases.Epicor2.MenuForm.Exists = False
        Call Delay(5000)
        count = count + 1
        if (count = 90) then
          Call Log.Error("Menu not appear")
          Call Runner.Stop(False)
        End if      
    WEnd
    

    if Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.SelectedNodes.Item(0).HasNodes = false then
      Call Aliases.Epicor2.MenuForm.UltraMainMenu.Check("View|Full Tree", True)
    end if

    'Execute created menu
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = True
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    Aliases.Epicor2.WinFormsObject("MainController").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("FillPanel").WinFormsObject("AppControllerPanel").WinFormsObject("_MyForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Edit|Refresh") 

    Call Delay(5000)

     Aliases.Epicor2.WinFormsObject("MainController").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("FillPanel").WinFormsObject("AppControllerPanel").WinFormsObject("_MyForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&File|E&xit") 

  'Open dashboard maintenance

    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).Nodes.Item(3).Selected = True
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("DashB")
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("[Tab]")

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|Modify Dashboard")

    Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&File|Copy Dashboard") 
    Aliases.Epicor2.WinFormsObject("CopyDashboardForm").WinFormsObject("txtDefinitionId").WinFormsObject("txtDefinitionId_EmbeddableTextBox").Keys("DashBCopy")
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



  'Test PartOnHandStatus Dashboard deployment

    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).Nodes.Item(3).Selected = True
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("PartOnHandStatus")
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("[Tab]")
    
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|Deploy UI Application")
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|Modify Dashboard")

    'Deploy Dashboard
    Call Aliases.Epicor2.Dashboard.dbPanel.zDashboardPanel_Toolbars_Dock_Area_Top.ClickItem("[0]|&Tools|Deploy Dashboard")
    Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("grpWinAssembly").WinFormsObject("chkDeployApplication").Checked = true
    Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").WinFormsObject("deployOptionsPanel1").WinFormsObject("btnOk").Click
    Call Delay(5000)
    Aliases.Epicor2.WinFormsObject("AppBuilderDeployDialog").Close

    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).Nodes.Item(3).Selected = True
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|Generate All Web Forms")    

    'Test SalesPersonWorkBench Dashboard deployment

    'Open Menu Maintenance
    
    'Expand System Setup
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).expanded = true
    'Expand Security Maintenance
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).expanded = true
    'Open Menu Maintenance 
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).Nodes.Item(2).Selected = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")
    
    'Select "Main Menu> Sales Management> CRM> Setup" in Menu Maintenance
    Set MenuMFormTree = Aliases.Epicor2.MenuMForm.windowDockingArea2.dockableWindow2.jobTreeViewPanel.methodTree
    MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Expanded = True
    MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Expanded = True
    MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Expanded = True
    MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = true
    MenuMFormTree.ActiveNode = MenuMFormTree.Nodes.Item(1).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0)
          
    'Add New Item
    Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|New...|New Menu")

    'Fill the information
      Set MenuFormObject = Aliases.Epicor2.MenuMForm.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow3").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("detailPanel1")
      MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtMenuID").Keys("PartsMenu")
      MenuFormObject.WinFormsObject("dbxDetail").WinFormsObject("txtName").Keys("Parts Menu")
      MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboType").Keys("[BS][BS][BS][BS][BS][BS][BS][BS][BS]Dashboard-Assembly")
      MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys("PartOnHandStatus")
      MenuFormObject.WinFormsObject("grpProgramMain").WinFormsObject("cboDashboard").Keys("[Tab]")
      
     'Save Menu Item
      Call Aliases.Epicor2.MenuMForm.zSonomaForm_Toolbars_Dock_Area_Top.ClickItem("Main Menu|&File|&Save")
      Call Aliases.Epicor2.MenuMForm.Close

    '-Return to system management> Upgrade/Mass regeneration> Dashboard maintenance
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).expanded = true
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).Nodes.Item(3).Selected = True
    Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("PartOnHandStatus")
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("windowDockingArea2").WinFormsObject("dockableWindow4").WinFormsObject("mainPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow2").WinFormsObject("detailPanel1").WinFormsObject("groupBox1").WinFormsObject("txtKeyField").Keys("[Tab]")
    Aliases.Epicor2.WinFormsObject("DashboardForm").WinFormsObject("_EpiForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Actions|Deploy UI Application")    

End Sub


