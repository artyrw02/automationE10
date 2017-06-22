'USEUNIT Functions
'USEUNIT Expand_Tree

Sub Dashboard_deployment()

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

  'Expand Executive Analysis
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).expanded = true
  'Expand Business Activity Management
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).expanded = true
  'Expand General Operation
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).expanded = true
  'Open Dashboard
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(5).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

  'Creates BAQ'
  Call simpleBAQ("baq1", "baq", "Part", "Company,PartNum,PartDescription,TypeCode,UnitPrice")

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

  'Creates simple Dashboard'
  Call Dashboards("DashB", "DashB Testing", "DashB Testing", "baq1")

  'Expand System Setup
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).expanded = true
  'Expand Security Maintenance
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).expanded = true
  'Open Menu Maintenance 
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(6).Nodes.Item(1).Nodes.Item(2).Selected = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

  'Creates Menu'
  Call CreateMenu("Main Menu>Sales Management>Customer Relationship Management>Setup", "DashMenu", "Dash Menu", "Dashboard-Assembly", "Dash")

  'Close Epicor
  Call Aliases.Epicor2.Close
  Call Delay(5000)
  
  'Runs the "Epicor" tested application.
  Call TestedApps.Epicor.Run

  'log in
  Call Login("epicor", "Epicor123", "Classic")

  'Select Company
  Call ExpandComp("Epicor Education")
  'Select Plant
  Call ChangePlant("Main Plant")

  'Execute created menu to test dashboard 
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Nodes.Item(0).Selected = True
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

  Aliases.Epicor2.WinFormsObject("MainController").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("FillPanel").WinFormsObject("AppControllerPanel").WinFormsObject("_MyForm_Toolbars_Dock_Area_Top").ClickItem("[0]|&Edit|Refresh") 

  'Count rows for the dashboard
  if Aliases.Epicor2.MainController.windowDockingArea1.dockableWindow1.FillPanel.AppControllerPanel.WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("MainPanel").WinFormsObject("MainDockPanel").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq1_1ViewDockPanel1").WinFormsObject("windowDockingArea1").WinFormsObject("dockableWindow1").WinFormsObject("V_baq1_1ViewListPanel1").WinFormsObject("myGrid").Rows.Count > 0 then
    log.Message("Data displayed.")
  end if

  'Open dashboard maintenance

  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).expanded = true
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Nodes.Item(5).Nodes.Item(2).Nodes.Item(7).Nodes.Item(10).Nodes.Item(3).Selected = True
  Aliases.Epicor2.MenuForm.windowDockingArea1.dockableWindow3.systemMenu.ultraExplorerBarContainerControl1.treeView.Keys("[Enter]")

  'Manage dashboard
  Call dashboardMaintenance("DashB", "Modify Dashboard")

  'Close Epicor
  Call Aliases.Epicor2.Close
  Call Delay(5000)

End Sub