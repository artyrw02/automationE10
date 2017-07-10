//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function Dashboard_Deployment()
{
  var baqData = {
    "Id" : "baqDeployment",
    "Description" : "baq",
    "Table" : "Part",
    "Columns" : "Company,PartNum,PartDescription,TypeCode,UnitPrice"
  }

  var DashbData = {
    "dashboardID" : "DashBDeployment",
    "dashboardCaption" : "dashboardCaption",
    "dashDescription" : "dashDescription",
    "generalOptions" : "",
    "baqQuery" : "baqDeployment",
    "deploymentOptions" : "Deploy Smart Client,Add Favorite Item,Generate Web Form"
  }

  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashDepl",
    "menuName" : "Dash Menu",
    "orderSequence" : 0,
    "menuType" : "Dashboard-Assembly",
    "dll" : "dashDescription"
  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login("epicor","Epicor123") 

    ActivateFullTree()

    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //2- Open Business Activity Query to create BAQ   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;Setup;Business Activity Query")

    //Call function to create a simple BAQ
    CreateSimpleBAQ(baqData)

    Log["Checkpoint"]("BAQ creaded correctly.")
  //3- Open Dashboard   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    //Enable Dashboard Developer Mode  
    DevMode()

  //4- Call function to create and deploy a dashboard (5,6,7,8)
  CreateSimpleDashboards(DashbData)
    Log["Checkpoint"]("Dashboard creaded correctly.")

  //9- Open Menu maintenance   
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    //Creates Menu
    CreateMenu(MenuData)
    Log["Checkpoint"]("Menu creaded correctly.")

  //10- Restart SmartClient
  RestartSmartClient()

  //14- Open Menu created
  MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;" + MenuData["menuName"])

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Edit|Refresh") 
  
  Delay(1000)

  var gridDashboard = RetrieveGridsMainPanel()
  
  for (var i = 0; i < gridDashboard["length"]; i++) {
    if (gridDashboard[i]["WndCaption"] == baqData["Id"] +': Summary') {
      gridBaq1Panel = gridDashboard[i]
      break
    }
  }

  if(gridBaq1Panel["Rows"]["Count"] > 0 ){
    Log["Checkpoint"]("Grid retrieved " + gridBaq1Panel["Rows"]["Count"]  + " records.")
  }else{
    Log["Error"]("Grid didn't retrieve " + gridBaq1Panel["Rows"]["Count"]  + " records.")
  }

  Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //19- Open Dashboard maintenance
  MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

  // steps: 20, 21, 22, 23, 24, 25
  // Manage dashboard

  DashboardMaintenance(DashbData["dashboardID"], "Modify Dashboard")
  Log["Checkpoint"]("Dashboard was manipulated through Dashboard Maintenance correctly.")

  //--- Close Smart Client --------------------------------------------------------------------------------------------------------------------'
    Delay(1000)
    
    DeactivateFullTree()
    Log["Checkpoint"]("FullTree Deactivated")

    CloseSmartClient()
    Log["Checkpoint"]("SmartClient Closed")
  //-------------------------------------------------------------------------------------------------------------------------------------------'
}