//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions


JIRA - FW: EATN-577 - Cr
https://community.smartbear.com/t5/TestComplete-General-Discussions/Accessing-the-Visible-property-of-Menu-Subitems-of-DevX-Menu/td-p/54358
https://support.smartbear.com/testcomplete/docs/app-objects/specific-tasks/standard/tool-bar/checking-button-state.html

function TC_Dashboard_Edition_Permissions_users(){
  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
    /*
      Step No: 1
      Step: Log in to smart client using "manager" user, and open System Setup>Security Maintenance> User Account Security Maintenance        
      Result: > The "User Account Maintenance" form opens       
    */

      StartSmartClient()

      Login(Project["Variables"]["username"], Project["Variables"]["password"])

      ActivateFullTree()

      ExpandComp("Epicor Education")

      ChangePlant("Main Plant")

      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;User Account Security Maintenance")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- User Account Security Maintenance -----------------------------------------------------------------------------------------------------'
    /*
      Step No: 2
      Step: Type the "manager" userID in the corresponfing fields and Tab out to retrieve its information
      Result: detailed Information related to "manager" user loads in the form        
    */

      var accountMgmtPanel = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]

      //Activate Details tab
      accountMgmtPanel["dockableWindow1"]["Activate"]()

      var detailsTab = accountMgmtPanel["dockableWindow1"]["detailPanel1"]
      detailsTab["txtKeyField"]["Keys"]("manager")
      detailsTab["txtKeyField"]["Keys"]("[Tab]")

      if (detailsTab["txtName"]["Text"] != ""){
        Log["Checkpoint"]("Data loaded correctly")
      }else{
        Log["Error"]("Data is not loaded correctly")
      }

    /*
      Step No: 3
      Step: Navigate to Options Tab and uncheck the "Dashboard Developer" option(If the option is already unchecked ignore this step) then click "Save"       
      Result: the Dashboard Developer uncheck remain check after saving 
    */
      //Activate 'Options' tab
      accountMgmtPanel["dockableWindow3"]["Activate"]()

      accountMgmtPanel["dockableWindow3"]["securityPanel1"]["gbPrivileges"]["chkQueries"]["Checked"] = false
   
      //Save form and exit
      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

  //-------------------------------------------------------------------------------------------------------------------------------------------'  
  
  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    /*
      Step No: 4
      Step: Open Executive analysis> Business Activity Management> General Operations> Dashboard
      Result: The dashboard form opens
    */

      //Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    /*
      Step No: 5
      Step: Take a look at the items from Tools and File options from menu bar        
      Result: In the Tools menu ""Developer"" option is disabled and is deactivated too
              in the File menu you don't have any ""New"",  ""import"", ""export"" or ""saving"" options for dashboard definitions available"       
    */

      ExitDashboard()

    /*
      Step No: 6
      Step: > Open System management> Upgrade/Mass Regeneration> Dashboard Maintenance        
      Result: > Dashboard maintenance form opens        
    */    
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
   
    /*
      Step No: 8
      Step: Go back to User account security maintenance> Options Tab and check the "Dashboard Developer" option then click "Save"
      Result: > Dashboard maintenance form opens        
    */  

      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;User Account Security Maintenance")

      var accountMgmtPanel = Aliases["Epicor"]["UserAccountForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]

     //Activate Details tab
      accountMgmtPanel["dockableWindow1"]["Activate"]()

      var detailsTab = accountMgmtPanel["dockableWindow1"]["detailPanel1"]
      detailsTab["txtKeyField"]["Keys"]("manager")
      detailsTab["txtKeyField"]["Keys"]("[Tab]")

      if (detailsTab["txtName"]["Text"] != ""){
        Log["Checkpoint"]("Data loaded correctly")
      }else{
        Log["Error"]("Data is not loaded correctly")
      }

    /*
      Step No: 3
      Step: Navigate to Options Tab and uncheck the "Dashboard Developer" option(If the option is already unchecked ignore this step) then click "Save"       
      Result: the Dashboard Developer uncheck remain check after saving 
    */
      //Activate 'Options' tab
      accountMgmtPanel["dockableWindow3"]["Activate"]()

      accountMgmtPanel["dockableWindow3"]["securityPanel1"]["gbPrivileges"]["chkQueries"]["Checked"] = true
   
      //Save form and exit
      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Save")
      Aliases["Epicor"]["UserAccountForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

    /*
      Step No: 9
      Step: Open Executive analysis> Business Activity Management> General Operations> Dashboard
      Result: The dashboard form opens
    */

      //Navigate and open Dashboard
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

    /*
      Step No: 10
      Step: Take a look at the items from Tools and File options from menu bar        
      Result: In the Tools menu ""Developer"" option is disabled and is deactivated too
              in the File menu you don't have any ""New"",  ""import"", ""export"" or ""saving"" options for dashboard definitions available"       
    */

      ExitDashboard()      

    /*
      Step No: 11
      Step: > Open System management> Upgrade/Mass Regeneration> Dashboard Maintenance        
      Result: > Dashboard maintenance form opens        
    */    
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")

      Aliases["Epicor"]["DashboardForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
}
/*//new

//options in File
var count = Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"]["Item"](0)["Tools"]["Count"]


for (var i = 0; i < count; i++) {
  var option = Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"]["Item"](0)["Tools"]["Item"](i)["ToolTipTextResolved"]
  Log["Message"]("1"+option)
}





Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Count"] = 4

//View
Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"]["Item"](2)["VisibleResolved"] //true
Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"]["Item"](2)["Tools"]["Item"](0)["EnabledResolved"] // false NOTES



checkMenuOptions(){

}


//Dashboard Main Menu


Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)

//Menu Options | File View Tools Help - use ["VisibleResolved"] to know if option it's available
Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"](x)

//Menu Options | File View Tools Help - use ["VisibleResolved"] to know if option it's available inside menu option lvl 1
Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Toolbars"]["Item"](0)["Tools"](0)["Tools"]["Item"](0)["ToolTipTextResolved"]




Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Tools"]["Item"](0)


//options in File
var count = Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Tools"]["Count"]


for (var i = 0; i < count; i++) {
  var option = Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ToolbarsManager"]["Container"]["Components"]["Item_2"](0)["Tools"](i)
  Log["Message"]("1"+option)
}



i = 83 - File
i = 85 - Edit
i = 86 - View
i = 87 - Help
i = 90 - Tools  15:12:24  Normal    
i = 88 - Actions*/