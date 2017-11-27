//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions

function TC_Dashboard_Updatable_Customized_Form_sys(){
  //Test Case -UD Dashboard

    // Variables
    var company1 = "Epicor Education"
    var plant1 = "Main"

    //Used to navigate thru the Main tree panel
    var treeMainPanel1 = setCompanyMainTree(company1,plant1)

  //--- Start Smart Client and log in ---------------------------------------------------------------------------------------------------------'
   
    StartSmartClient()

    Login(Project["Variables"]["username"], Project["Variables"]["password"])
    ActivateFullTree()

    Delay(1500)
    ExpandComp(company1)

    ChangePlant(plant1)
  //-------------------------------------------------------------------------------------------------------------------------------------------'

  //---- Add the dashboard in a Customization --------------------------------------------------------------------------------------------------'

    // 2- Developer mode 
    Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")

    // 3- Go to Production Management> Job Management> Setup> Part
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

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
    Log["Message"]("Sheet was added to Custom Sheets.")
    // 18- Click File> Save customization As  
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization As ...")

    // 19- On Name enter EmbDash and on Description enter Embedded Dashboard and click Save        
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtKey1a")["Keys"]("EmbDash")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtDescription")["Keys"]("Embedded Dashboard")
    Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("btnOk")["Click"]()
    Aliases["Epicor"]["WinFormsObject"]("CustomCommentDialog")["WinFormsObject"]("btnOK")["Click"]()

    Log["Message"]("Customization saved.")
    // 21- close the customization 
    Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
    Log["Message"]("Customization closed.")   
    // 22- Close the form"  
    Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")   
    Log["Message"]("Part Form closed.")   


    // 23- Go to Production Management> Job Management> Setup> Part (Open again Part Maintenance (using developer mode)
    ActivateMainDevMode()
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

    Delay(2000)

    /*(FUTURE REFERENCE FOR TREE LIST ITEMS)*/
    // 24- Select the created customizacion       
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["etvAvailableLayers"]["ClickItem"]("Base|EP|Customizations|EmbDash")
    Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()

    var testPart = "00P1"
    // 25, 26 - Click on Part button - Click Search and select a Part and click Ok                              
    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["partDetailPanel1"]["groupBox1"]["tbPart"]["Keys"](testPart)
    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["partDetailPanel1"]["groupBox1"]["tbPart"]["Keys"]("[Tab]")

    Log["Message"]("00P1 customer was retrived")

    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["WinFormsObject"]("DockableWindow", "", 7)["Activate"]()
    Log["Message"]("PartStatus tab Activated")

    Delay(4500)
    
    Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    
    var PartTxtfield = Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["WinFormsObject"]("DockableWindow", "", 7)["WinFormsObject"]("PartStatus")["WinFormsObject"]("PartStatusDashboardPanel")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow3")["WinFormsObject"]("dbFillPanel1")["WinFormsObject"]("WindowDockingArea", "", 5)["WinFormsObject"]("DockableWindow", "", 1)["WinFormsObject"]("a01b8010-d16b-4b6c-8e86-337ac824f218")["WinFormsObject"]("QueryFillPanel")["WinFormsObject"]("WindowDockingArea", "")["WinFormsObject"]("DockableWindow", "", 1)["WinFormsObject"]("b96666ad-9cbc-482e-8793-97d8650c6b0c")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow1")["WinFormsObject"]("TrackerPanel")["WinFormsObject"]("txtPart_PartNum")

    if(PartTxtfield["Text"]["OleValue"] == testPart){
        Log["Checkpoint"]("Part " + testPart + " was retrieved and displayed on PartStatus")
        ClickButton("Retrieve")
    }else{
        Log["Error"]("Part " + testPart + " was not retrieved and displayed on PartStatus")
    }
    
    var searchGrid = Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]
    
    var searchResultGrid = searchGrid["FindChild"](["FullName", "WndCaption"], ["*grid*","*Search Results*"], 15)

    if (searchResultGrid["Rows"]["Count"] > 0) {
       Log["Checkpoint"]("There is a part retrieved and displayed on grid")
    }else{
        Log["Error"]("There is not a part retrieved and displayed on grid")
    }

    var columnPartGrid = searchResultGrid["Rows"]["Item"](0)["Cells"]["Item"](0)

    if (columnPartGrid["Text"]["OleValue"] == testPart) {
       Log["Checkpoint"]("Part "+ testPart+" is displayed on grid")
    }else{
       Log["Error"]("Part "+ testPart+" is not displayed on grid")
    }

    //Activate Warehouse tab
    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["DockableWindow"]["PartStatus"]["PartStatusDashboardPanel"]["windowDockingArea1"]["dockableWindow3"]["dbFillPanel1"]["WindowDockingArea"]["DockableWindow"]["Activate"]()

    var warehouseResultGrid = searchGrid["FindChild"](["FullName", "WndCaption"], ["*grid*","*All*"], 20)

    if (warehouseResultGrid["Rows"]["Count"] > 0) {
       Log["Checkpoint"]("There is a part retrieved and displayed on warhouse grid")
    }else{
        Log["Error"]("There is not a part retrieved and displayed on warhouse grid")
    }

    Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    Log["Message"]("Part Form closed")

    DeactivateMainDevMode()
  //-------------------------------------------------------------------------------------------------------------------------------------------' 

   DeactivateFullTree()

   CloseSmartClient()

}


