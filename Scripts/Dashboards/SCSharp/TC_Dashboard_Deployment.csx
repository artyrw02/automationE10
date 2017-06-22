//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_Deployment()
{

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    // StartSmartClient()

    // Login("epicor","Epicor123", "Classic") 

    // ActivateFullTree()
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

  //Call function to create a simple BAQ
  CreateSimpleBAQ("baq1", "baq", "Part", "Company,PartNum,PartDescription,TypeCode,UnitPrice")

  //Open Dashboard   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

  //Enable Dashboard Developer Mode  
  DevMode()

  //Call function to create and deploy a dashboard
  CreateSimpleDashboards("DashB", "dashboardCaption", "dashDescription", "", "baq1", "Deploy Smart Client,Add Menu tab,Add Favorite Item,Generate Web Form")

  //Open Menu maintenance   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

  //Creates Menu
  CreateMenu("Main Menu>Sales Management>Customer Relationship Management>Setup", "DashMenu", "Dash Menu", 1, "Dashboard-Assembly", "dashDescription")

  //Restart SmartClient
  RestartSmartClient("Classic")

  //Open Menu created
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;Dash Menu")

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Edit|Refresh") 
  
  Delay(1000)

  var gridDashboard = RetrieveGridsMainPanel()
  
  for (var i = 0; i < gridDashboard["length"]; i++) {
    if (gridDashboard[i]["WndCaption"] == 'baq1: Summary') {
      gridBaq1Panel = gridDashboard[i]
      break
    }
  }

  if(gridBaq1Panel["Rows"]["Count"] > 0 ){
    Log["Checkpoint"]("Grid retrieved records.")
  }else{
    Log["Error"]("Grid didn't retrieve records.")
  }

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //Open Dashboard maintenance
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  //Manage dashboard
  DashboardMaintenance("DashB", "Modify Dashboard")

  //--- Close Smart Client --------------------------------------------------------------------------------------------------------------------'
  
  Delay(1000)
  
  DeactivateFullTree()
  Log["Checkpoint"]("FullTree Deactivate")

  CloseSmartClient()
  Log["Checkpoint"]("SmartClient Closed")
//-------------------------------------------------------------------------------------------------------------------------------------------'
}