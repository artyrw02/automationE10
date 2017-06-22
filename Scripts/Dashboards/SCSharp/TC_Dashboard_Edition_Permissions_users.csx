//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions

function TC_Dashboard_Edition_Permissions_users(){
  
  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
    /*
      Step No: 1
      Step: Log in to smart client using "manager" user, and open System Setup>Security Maintenance> User Account Security Maintenance        
      Result: > The "User Account Maintenance" form opens       
    */

      StartSmartClient()

      Login("manager","Epicor123", "Classic") 

      ActivateFullTree()

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

   
}