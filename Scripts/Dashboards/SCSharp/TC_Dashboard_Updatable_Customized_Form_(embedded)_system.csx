//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Dashboard_Updatable_Customized_Form(){
  //Test Case -UD Dashboard

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login("epicor","Epicor123", "Classic") 

    ActivateFullTree()

    Delay(1500)
    ExpandComp("Epicor Education")

    ChangePlant("Main Plant")
  //-------------------------------------------------------------------------------------------------------------------------------------------'


  //---- Add the dashboard in a Customization --------------------------------------------------------------------------------------------------'

    // 2- Developer mode 
    Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")

    // 3- Go to Production Management> Job Management> Setup> Part
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Production Management;Job Management;Setup;Part")

    // 4- Check Base Only and click Ok       
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["grpNoLayer"]["chkBaseOnly"]["Checked"] = true

    Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    // 5- click Tools > Customization
    Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Customization")

    // 6- go to Wizards > Sheet Wizard  tab
    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()

    // 7- Select mainPanel1 from Dockable Sheets Listing
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"]("mainPanel1")
    Log["Message"]("mainPanel1 sheet selected")

    // 8- Name, text, tab text = ""TEST""
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("PartStatus")
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("PartStatus")
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("PartStatus") 

    // 9- Click Add Dashboard Button
    Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddDashboard"]["Click"]()
    
    // 10- Click on Dashboard ID button       
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["txtDashboardID"]["Keys"]("PartOnHandStatus")

    // 12- click ""Next"" button
    Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()

    // 13- Select Subscribe to UI data (include retrieve button)
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep2"]["radRetrieveWButton"]["ultraOptionSet1"]["Click"]()

    // click ""Next"" button
    Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()

    // 14- Choose ""Part"" data view
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataViews"]["ClickItem"]("Part")
    // 15- Choose ""PartNum"" column
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataColumns"]["ClickItem"]("PartNum")
    // click ""Add Subscribe column"" button
    Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["btnAddSubscribeColumn"]["Click"]()
    // 16- click "Finish" button
    Aliases["Epicor"]["CustomWizardDialog"]["WinFormsObject"]("btnFinish")["Click"]()
    // 17- Press Right arrow to move tab to ""PartStatus Sheets"" panel
    Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
    Log["Checkpoint"]("Sheet was added to Custom Sheets.")
    // 18- Click File> Save customization As  
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization As ...")

    // 19- On Name enter EmbDash and on Description enter Embedded Dashboard and click Save        
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtKey1a")["Keys"]("EmbDash")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtDescription")["Keys"]("Embedded Dashboard")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("btnOk")["Click"]()
    Aliases["Epicor"]["WinFormsObject"]("CustomCommentDialog")["WinFormsObject"]("btnOK")["Click"]()

    Log["Checkpoint"]("Customization saved.")
    // 21- close the customization 
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
    // 22- Close the form"  
    Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")   
    Log["Checkpoint"]("Customization closed.")   

    // 23- Go to Production Management> Job Management> Setup> Part
    MainMenuTreeViewSelect("Epicor Education;Main Plant;Production Management;Job Management;Setup;Part")

    /*(FUTURE REFERENCE FOR TREE LIST ITEMS)*/
    // 24- Select the created customizacion       
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["etvAvailableLayers"]["ClickItem"]("Base|EP|Customizations|EmbDash")
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    // In the Customer> Detail tab retrieve ""Addison""
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Addison")
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")

    Log["Checkpoint"]("Addison customer was retrived")
    // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
    Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();
    
    /*
      Step No: 19
      Step: in the "TEST" tab click "Retrieve" button
      Result: The dashboard opens without errors        
    */ 

    var retrieveBtn = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*Retrieve*", "*EpiButton*"], 30)
    retrieveBtn["Click"]()

    var grid = Aliases["Epicor"]["CustomerEntryForm"]["FindChild"](["WndCaption", "ClrClassName"], ["*TestBAQ: Summary*", "*Grid*"], 30)
    if(grid["wRowCount"] > 0){
      Log["Checkpoint"]("Dashboard retrived data")
    }else {
      Log["Error"]("Dashboard didn't retrive data")
    }

    /*
      Step No: 20
      Step: Go back to Customer> Detail  tab and retrieve ""Dalton"" customer
      Result: The customer is retrieved without errors        
    */ 
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("Dalton")
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox1"]["txtKeyField"]["Keys"]("[Tab]")
       
      Log["Checkpoint"]("Dalton customer was retrived")
      // Go to the ""TEST"" tab (where the dashboard is contained) and take a look at its content"    
      Aliases["Epicor"]["CustomerEntryForm"]["windowDockingArea1"]["dockableWindow1"]["mainDock1"]["windowDockingArea1"]["dockableWindow1"]["customerDock1"]["windowDockingArea1"]["DockableWindow"]["Activate"]();

  //-------------------------------------------------------------------------------------------------------------------------------------------' 

   DeactivateFullTree()

   CloseSmartClient()

}


