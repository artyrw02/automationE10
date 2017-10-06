//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Importing_exporting_Dashboards_E9E10(){
  var dashboardImportedE10 = "C:\\ProgramData\\Epicor\\tyrell.playground.local-80\\3.2.100.0\\EPIC06\\shared\\Export\\DashDef -Regression.dbd"  
  var dashboardID = "DashCaro" // Dashboard ID from imported dashboard
  var baqE9 = "EPIC06-testBAQCaro" //BAQ ID from imported dashboard

  var MenuData = {
    "menuLocation" : "Main Menu>Sales Management>Customer Relationship Management>Setup",
    "menuID" : "DashbE9",
    "menuName" : "DashbE9",
    "orderSequence" : 2,
    "menuType" : "Dashboard-Assembly",
    "dll" : dashboardID,
    "validations" : "Enable"
  }

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])

    ActivateFullTree()

    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Creates Dashboards --------------------------------------------------------------------------------------------------------------------'

    // 2- Log in to E10. Go to Executive Analysis> Business Activity Management> Setup>Dashboard.  Go to Tools>Developer Mode       
      //Navigate and open Dashboard

      MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")

      var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
      Log["Checkpoint"]("Dashboard opened")
      
      //Enable Dashboard Developer Mode  
      DevMode()
      Log["Checkpoint"]("DevMode activated")
     
    // 3- Click File> Import Dashboard Definition and select the dashboard that you created. On Import BAQ Options click Ok        

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["zDashboardPanel_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Import Dashboard Definition")
       
      var windowImportDashBD = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*Import Dashboard*","*ComboBox*"], 30)
      if (windowImportDashBD["Exists"]) {
        var windowImportDashBDSaveBtn = Aliases["Epicor"]["FindChild"](["FullName", "WndClass"],["*&Open*","*Button*"], 30)
        windowImportDashBD["Keys"](dashboardImportedE10)
        windowImportDashBDSaveBtn["Click"]()
        Log["Message"]("Dashboard imported correctly")
      }else{
        Log["Error"]("Dashboard wasn't imported correctly, Object doesn't exists")    
      }

      if (Aliases["Epicor"]["DashboardBAQImportDialog"]["Exists"]) {
        Log["Message"]("BAQ Import dialog is displayed")
        Aliases["Epicor"]["DashboardBAQImportDialog"]["btnOkay"]["Click"]()
      }else{
        Log["Error"]("BAQ Import dialog is not displayed")
      }

      Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
      if(Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea1"]["dockableWindow2"]["pnlGeneral"]["windowDockingArea1"]["dockableWindow1"]["pnlGenProps"]["txtDefinitonID"]["Text"] != ""){
        Log["Message"]("Dashboard Imported")
      }else{
        Log["Error"]("Dashboard wasn't imported")
      }

    // 4- Save your dashboard       
      SaveDashboard()
      ExitDashboard()

    // 5- Go to Updatable BAQ Maintenance (System Management> Upgrade/Mass Regeneration)        
      MainMenuTreeViewSelect("Epicor Education;Main Plant;System Management;Upgrade/Mass Regeneration;Updatable BAQ Maintenance")
      
    // 6- On Query ID retrieve the created query for your dashboard that you previously imported  
      Aliases["Epicor"]["UBAQMaintForm"]["windowDockingArea2"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["groupBox1"]["btnKeyField"]["Click"]()
      Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["epiTabPage"]["basicPanel1"]["txtStartWith"]["Keys"](baqE9)

      Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["epiTabControl1"]["epiTabPage"]["basicPanel1"]["WinFormsObject"]("chkShared")["CheckState"] = "Indeterminate"
      Aliases["Epicor"]["BAQDesignerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["btnSearch"]["Click"]()
      var searchGrid = Aliases["Epicor"]["BAQDesignerSearchForm"]["FindChild"](["WndCaption","ClrClassName"], ["*Search Results*", "*Grid*"],30)
      var queryID = getColumn(searchGrid, "Query ID")

      for(var i = 0; i < searchGrid["Rows"]["Count"];i++){
        if(searchGrid["Rows"]["Item"](i)["Cells"]["Item"](queryID)["Text"]["OleValue"] == baqE9){
          searchGrid["Rows"]["Item"](i)["Activate"]()
          Aliases["Epicor"]["BAQDesignerSearchForm"]["ultraStatusBar2"]["btnOK"]["Click"]()
        }
      }

      //Used to activate query
      var treeView = Aliases["Epicor"]["UBAQMaintForm"]["FindChild"]("ClrClassName", "*TreeView", 30)
      treeView["ClickItem"]("Updatable Queries|"+baqE9)

    // 7- Select Actions>Regenerate selected  
    Aliases["Epicor"]["UBAQMaintForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Regenerate Selected")
    Delay(1500)
    Log["Checkpoint"]("BAQ regenerated")

    /*
      Step No: 8 - 9
      Step:  Validate dashboard has all the configurations imported      
    */

    // 8- Go to Query properties on Dashboard designer        
    Aliases["Epicor"]["UBAQMaintForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Executive Analysis;Business Activity Management;General Operations;Dashboard")
    OpenDashboard(dashboardID)
    Log["Checkpoint"]("Dashboard retrived")

    var dashboardTree = Aliases["Epicor"]["Dashboard"]["dbPanel"]["windowDockingArea2"]["dockableWindow5"]["dbTreePanel"]["windowDockingArea1"]["dockableWindow1"]["DashboardTree"]
         
    var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](0)
    dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
    Log["Message"]("BAQ - right click")

    // click 'Properties' option from menu
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    Log["Message"]("EPIC06-testBAQCaro:  Summary - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    //Check the Updatable check box
    if(Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["chkUpdatable"]["Checked"]){
      Log["Checkpoint"]("Updatable checkbox is Checked")
    }else{
      Log["Error"]("Updatable checkbox is not checked")
    }

    var dashboardGrid = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
    
    var column = getColumn(dashboardGrid, "Column")
    var columnPrompt = getColumn(dashboardGrid, "Prompt")

    // for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
    //   var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

    //   if (cell["Text"] == "Customer_Address1") {
    //     if(dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"]){
    //       Log["Checkpoint"]("Prompt checkbox on Address1 is Checked")
    //     }else{
    //       Log["Error"]("Prompt checkbox on Address1 is not checked")
    //     }
    //   }
    //   if (cell["Text"] == "Customer_City") {
    //     if(dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"]){
    //       Log["Checkpoint"]("Prompt checkbox on City is Checked")
    //     }else{
    //       Log["Error"]("Prompt checkbox on City is not checked")
    //     }
    //   }
    //   if (cell["Text"] == "Customer_Country") {
    //     if(dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"]){
    //       Log["Checkpoint"]("Prompt checkbox on Country is Checked")
    //     }else{
    //       Log["Error"]("Prompt checkbox on Country is not checked")
    //     }
    //   }            
    // }
    var fields = "Customer_Address1,Customer_City,Customer_Country"

    fields = fields.split(",")

    //find the row where GroupCode is located
    for (var i = 0; i <= dashboardGrid["wRowCount"] - 1; i++) {
      //Select row and check Prompt checkbox
      var cell = dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](column)

      for (var j = 0; j < fields["length"]; j++) {
        if(cell["Text"] == fields[j]) {
          if(dashboardGrid["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"]){
            Log["Checkpoint"]("Prompt checkbox on " + fields[j] + " is Checked")
          }else{
            Log["Error"]("Prompt checkbox on " + fields[j] + " is not checked")
            break
          }
        }
      }
    } 

    Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()
    // //Go to View Rules tab
    //   var gridPaneldialog = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]
      
    //   DashboardPropertiesTabs(gridPaneldialog, "View Rules")

    //   var viewRulesPanel = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["GridViewPropsPanel"]["viewPropsTabCtrl"]["tabViewRules"]["viewRuleWizardPanel1"]
    Log["Checkpoint"]("Grid validated correctly.")

    // 9- Go to tracker view properties on Dashboard designer        
    var rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](0)["Nodes"]["Item"](1)
    dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
    Log["Message"]("BAQ - right click")

    // click 'Properties' option from menu
    Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("Properties");
    Log["Message"]("Tracker View - Properties was selected from Menu")

    if (Aliases["Epicor"]["DashboardProperties"]["Exists"]) {
      Log["Message"]("Dashboard properties dialog appears")
    }

    var dashboardTrackerView = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
        
    var column = getColumn(dashboardTrackerView, "Column")
    var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
    var columnCondition = getColumn(dashboardTrackerView, "Condition")
    
    var fields = "Customer_CustID"

    fields = fields.split(",")

    for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
      var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

      for (var j = 0; j < fields["length"]; j++) {
        if (cell["Text"] == fields[j]) {
          if(dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked" && 
              dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Text"]["OleValue"] == "StartsWith"){
            Log["Checkpoint"]("Prompt checkbox on " + fields[j] + " is Checked")
          }else{
            Log["Error"]("Prompt checkbox on " + fields[j] + " is not checked")
            break
          }
        }
      }
    }

    //Click Ok to close Properties
    Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()  

    // 10- On Dashboard designer add the zPOLine query       
    AddQueriesDashboard("zPOLine")

    // 12- "Add a new tracker View. Select Clear All and set the following:

      rect = dashboardTree["Nodes"]["Item"](0)["Nodes"]["Item"](1)
      dashboardTree["ClickR"]((rect["Bounds"]["Left"]+ rect["Bounds"]["Right"])/2, (rect["Bounds"]["Top"]+ rect["Bounds"]["Bottom"])/2)
      Log["Message"]("BAQ - right click")

      // click 'New Tracker View' option from menu
      Aliases["Epicor"]["Dashboard"]["dbPanel"]["UltraPopupMenu"]["Click"]("New Tracker View");
      Log["Message"]("New Tracker View was selected from Menu")

     // Visible & Prompt for PartNum and Description
      var dashboardTrackerView = Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["ultraGrid1"]
      Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["pnlTrackerControls"]["btnClearAll"]["Click"]()
      var column = getColumn(dashboardTrackerView, "Column")
      var columnPrompt = getColumn(dashboardTrackerView, "Prompt")
      var columnVisible = getColumn(dashboardTrackerView, "Visible")
      var columnCondition = getColumn(dashboardTrackerView, "Condition")
      
      var fieldsVisible = "PODetail_PartNum,PODetail_LineDesc"

      fieldsVisible = fieldsVisible.split(",")

      for (var i = 0; i <= dashboardTrackerView["wRowCount"] - 1; i++) {
        var cell = dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](column)

        for (var j = 0; j < fieldsVisible["length"]; j++) {
          if (cell["Text"]["OleValue"] == fieldsVisible[j]) {
            dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["Click"]()
            dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnVisible)["EditorResolved"]["CheckState"] = "Checked" 

            dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["Click"]()
            dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnPrompt)["EditorResolved"]["CheckState"] = "Checked" 

           //Activates 'Condition' column
              dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["Click"]()

              while(true){
                dashboardTrackerView["Keys"]("[Down]")
                if (dashboardTrackerView["Rows"]["Item"](i)["Cells"]["Item"](columnCondition)["EditorResolved"]["SelectedText"] == "StartsWith") {
                  break
                }
              }
            Log["Checkpoint"]("Checkbox visible & prompt where checked for " + fieldsVisible[j])             
          }
        }
      }
    
    // Check Input Prompts Only
    Aliases["Epicor"]["DashboardProperties"]["FillPanel"]["TrackerViewPropsPanel"]["viewPropsTabCtrl"]["GeneralTab"]["chkInputPrompts"]["Checked"] = true
    
    //Click Ok to close Properties
    Aliases["Epicor"]["DashboardProperties"]["btnOkay"]["Click"]()   

    //13- Click on Tools>Deploy Dashboard. Select Deploy Smart Client Application checkbox        
    DeployDashboard("Deploy Smart Client")

    SaveDashboard()
    ExitDashboard()

  //-------------------------------------------------------------------------------------------------------------------------------------------'
  
  //--- Creates Menu --------------------------------------------------------------------------------------------------------------------------'
   /*
      Step No: 14
      Step:  "Create the menu
              Go to System Setup>Security Maintenance> Menu Maintenance. In Menu Maintenance tree select Main Menu> Sales Management> CRM> Setup
              Select New Menu.
              Write a Menu ID, select module UD, write a Name for the menu, write an Order Sequence (the position where you will find the menu), 
              in Program Type select Dashboard-Assembly and in Dashboard select the previously created one. 
              Be sure the Enabled check box is selected. Click Save."       

      Result: Verify the dashboard is saved. Verify the Caption and Description fields have the values you previously gave        
    */ 

    //Open Menu maintenance   
    MainMenuTreeViewSelect("Epicor Education;Main Plant;System Setup;Security Maintenance;Menu Maintenance")

    //Creates Menu
    CreateMenu(MenuData)
  
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Restart Smart Client  -----------------------------------------------------------------------------------------------------------------'
    /*
      Step No: 15
      Step: Restart Smart Client        
      Result: E10 is restarted        
    */    

    Delay(1000)
    RestartSmartClient()
    Log["Checkpoint"]("SmartClient Restarted")
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //--- Test Menu -----------------------------------------------------------------------------------------------------------------------------'
    // 16- On Main Menu go to Sales Management> CRM> Setup and open the previously created menu       
      MainMenuTreeViewSelect("Epicor Education;Main Plant;Sales Management;Customer Relationship Management;Setup;"+MenuData["menuName"])

      var gridsMainPanel = RetrieveGridsMainPanel()
      var trackerMainPanel = RetrieveTrackerMainPanel()

      gridsMainPanel[0]["Click"]()
  
    //  17- Refresh
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
      
      if (gridsMainPanel[0]["Rows"]["Count"] > 0) {
        Log["Message"]("Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
      }else{
        Log["Error"]("There was a problem. Grid displays " + gridsMainPanel[0]["Rows"]["Count"] + " records")
      }

    // 18- Go to the Tracker View and in Cust.ID field write E, then refresh       
      var custIDField = trackerMainPanel[0]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*CustID"], 30)

      custIDField["Keys"]("E")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

      var columnCustID = getColumn(gridsMainPanel[0], "Cust. ID") 

      for (var i = 0; i < gridsMainPanel[0]["Rows"]["Count"]; i++) {
        //Points to column Cust. ID
        var aString = gridsMainPanel[0]["Rows"]["Item"](i)["Cells"]["Item"](columnCustID)["Text"]["OleValue"]
        var aSubString = "E"
        var Res, flag = true;

        Res = aqString["Find"](aString, aSubString)
        if (Res != -1) {
          flag = true
        }
        else{
          Log["Message"]("There are no occurrences of '" + aSubString + "' in '" + aString + "'.");
          flag = false
          break
        }
      }
      
      if (flag) {
        Log["Checkpoint"]("Grid retrieved just records starting with E " )
      }
        
    // 19- Update any register on column Address        
      var columnAddress = getColumn(gridsMainPanel[0], "Address") 
      
      gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["Click"]()

      var oldValueAddress = gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["Text"]["OleValue"]
      var newValueAddress = gridsMainPanel[0]["Rows"]["Item"](0)["Cells"]["Item"](columnAddress)["EditorResolved"]["SelectedText"] = "TEST"
      gridsMainPanel[0]["Rows"]["Item"](1)["Cells"]["Item"](columnAddress)["Click"]()

      if(oldValueAddress != newValueAddress){
        Log["Checkpoint"]("Data was updated.")
      }else{
        Log["Error"]("Data was not updated.")
      }

    // 20- Move to the zPOLine tab and click refresh        
      gridsMainPanel[1]["Click"]()
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

    // 21- On Part field from tracker view enter "*00*" and click Refresh       
      var partField = trackerMainPanel[1]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*Part*"], 30)

      partField["keys"]("*00*")
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
    // 22- On Part field from tracker view enter "0*" and click Refresh       
      var columnPart = getColumn(gridsMainPanel[1], "Part") 

      for (var i = 0; i < gridsMainPanel[1]["Rows"]["Count"]; i++) {
        //Points to column Part
        var aString = gridsMainPanel[1]["Rows"]["Item"](i)["Cells"]["Item"](columnPart)["Text"]["OleValue"]
        var aSubString = "00"
        var Res, flag = true;

        Res = aqString["Find"](aString, aSubString)
        if (Res != -1) {
          flag = true
        }
        else{
          Log["Message"]("There are no occurrences of '" + aSubString + "' in '" + aString + "'.");
          flag = false
          break
        }
      }
      
      if (flag) {
        Log["Checkpoint"]("Grid retrieved just records with 00 " )
      }

     // 23- On Part field from tracker view enter "0*" and click Refresh        
     
      partField["keys"]("^a[Del]"+"0*")
      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
      var columnPart = getColumn(gridsMainPanel[1], "Part") 

      findValueInStringGrids(gridsMainPanel[1], columnPart, "0")

    // 23- On Description field from tracker view enter "CRS ROUND 1.00" BAR STOCK" and click Refresh        
      var descriptionField = trackerMainPanel[1]["FindChild"](["ClrClassName", "FullName"], ["*TextBox","*Desc*"], 30)

      var columnDescription = getColumn(gridsMainPanel[1], "Description") 

      partField["keys"]("^a[Del]")
      descriptionField["keys"]("CRS ROUND 1.00")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
      findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.00")

    // 24- On Description field from tracker view enter "CRS ROUND 1.50" BAR STOCK" and click Refresh        
      partField["keys"]("^a[Del]")
      descriptionField["keys"]("CRS ROUND 1.50")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
      findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.50")
    // 25- On Description field from tracker view enter "CRS ROUND 1..0" BAR STOCK" and click Refresh        
      partField["keys"]("^a[Del]")
      descriptionField["keys"]("CRS ROUND 1..0")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
      findValueInStringGrids(gridsMainPanel[1], columnDescription, "CRS ROUND 1.")

      Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["zMyForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

      if(Aliases["Epicor"]["EpiCheckMessageBox"]["Exists"]){
        Aliases["Epicor"]["EpiCheckMessageBox"]["groupBox1"]["pnlYesNo"]["WinFormsObject"]("btnNo1")["Click"]()
      }
  //-------------------------------------------------------------------------------------------------------------------------------------------'

   DeactivateFullTree()

   CloseSmartClient()
}

function RetrieveTrackerMainPanel(){
  var dashboardMainPanel = Aliases["Epicor"]["MainController"]["windowDockingArea1"]["dockableWindow1"]["FillPanel"]["AppControllerPanel"]["windowDockingArea1"]["dockableWindow1"]["MainPanel"]["MainDockPanel"]
    
  var trackerPDashboardChildren = dashboardMainPanel["FindAllChildren"]("FullName", "*TrackerPanel", 9)["toArray"]();
  return trackerPDashboardChildren
}


