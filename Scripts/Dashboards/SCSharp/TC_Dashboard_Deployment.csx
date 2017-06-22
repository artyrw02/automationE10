//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_Deployment()
{
  TestedApps["Epicor"]["Run"]();

  Login("epicor","Epicor123", "Classic")   

  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  //Call function to create a simple BAQ
  simpleBAQ("baq1", "baq", "Part", "Company,PartNum,PartDescription,TypeCode,UnitPrice")

  //Open Dashboard   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  //Call function to create and deploy a dashboard
  Dashboards("DashB", "dashboardCaption", "dashDescription", "", "baq1", "Deploy Smart Client,Add Menu tab,Add Favorite Item,Generate Web Form")

  //Open Menu maintenance   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu("Main Menu>Sales Management>Customer Relationship Management>Setup", "DashMenu", "Dash Menu", "Dashboard-Assembly", "Dash")
  'Set break point inside CreateMenu function to check checkbox'

  //Restart SmartClient
  restartSmartClient("Classic")

  //Open Menu created
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Dash Menu")

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Edit|Refresh") 
  
  Delay(1000)
  /*if(Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]["windowDockingArea1"]["dockableWindow1"]["V_baq1_1ViewDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["V_baq1_1ViewListPanel1"]["myGrid"]["Rows"]["Count"] > 0){
    Log["Message"]("Data displayed.")
  } */

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")


  //Open Dashboard maintenance
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  //Manage dashboard
  dashboardMaintenance("DashB", "Modify Dashboard")
}