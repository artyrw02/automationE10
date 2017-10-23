//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions


function TC_Dashboard_Updatable_Customized_Form_sys(){}

  //Test Case -UD Dashboard

    // Variables
    var company1 = "Epicor Education"
    var plant1 = "Main"

    //Used to navigate thru the Main tree panel
    var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//Steps 2 to 4
function OpenFormDevMode(){
    // 2- Developer mode 
    Log["Message"]("Step 2")
    // Aliases["Epicor"]["MenuForm"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Options|&Developer Mode")
    ClickMenu("Options->Developer Mode")

    ExpandComp(company1)

    ChangePlant(plant1)

    // 3- Go to Production Management> Job Management> Setup> Part
    Log["Message"]("Step 3")
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

    // 4- Check Base Only and click Ok       
    Log["Message"]("Step 4")
    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["grpNoLayer"]["chkBaseOnly"]["Checked"] = true
    CheckboxState("chkBaseOnly", true)

    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()
    ClickButton("OK")
    Delay(10000)
      
    // 5- click Tools > Customization
    Log["Message"]("Step 5")
    
    // Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Tools|Customization")
    ClickMenu("Tools->Customization")
}

// Steps 5 to 22
function CreateCustomization() {
  Delay(2500)  
    // 6- go to Wizards > Sheet Wizard  tab
    Log["Message"]("Step 6")
    
    // var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnNewCustomSheet"]["Click"]()
    ClickButton("New Custom Sheet")
    
    // 7- Select mainPanel1 from Dockable Sheets Listing
    Log["Message"]("Step 7")

    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["lstStandardSheets"]["ClickItem"]("mainPanel1")
    var dockableSheetsList = FindObject("*ListBox*", "Name", "*lstStandardSheets*")
    dockableSheetsList["ClickItem"]("mainPanel1")
    Log["Message"]("mainPanel1 sheet selected")

    // 8- Name, text, tab text = ""TEST""
    Log["Message"]("Step 8")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetName"]["Keys"]("PartStatus")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetText"]["Keys"]("PartStatus")
    // CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["txtSheetTextTab"]["Keys"]("PartStatus") 
    EnterText("txtSheetName", "PartStatus")
    EnterText("txtSheetText", "PartStatus")
    EnterText("txtSheetTextTab", "PartStatus")

    // 9- Click Add Dashboard Button
    Log["Message"]("Step 9")
    // Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddDashboard"]["Click"]()
    ClickButton("Add Dashboard...")
    
    // 10- Click on Dashboard ID button       
    Log["Message"]("Step 10")
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep1"]["txtDashboardID"]["Keys"]("PartOnHandStatus")
    EnterText("txtDashboardID", "PartOnHandStatus")

    // 12- click ""Next"" button
    Log["Message"]("Step 12")
    // Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()
    ClickButton("Next>")

    // 13- Select Subscribe to UI data (include retrieve button)
    Log["Message"]("Step 13")

    var dockableSheetsList = FindObject("*RadioButton*", "Name", "*radRetrieveWButton*")
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep2"]["radRetrieveWButton"]["ultraOptionSet1"]["Click"]()
    dockableSheetsList["Click"]()

    // click ""Next"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["btnNext"]["Click"]()
    ClickButton("Next>")

    // 14- Choose ""Part"" data view
    Log["Message"]("Step 14")
    var dataViewList = FindObject("*ListBox*", "Name", "*lstDataViews*")
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataViews"]["ClickItem"]("Part")
    dataViewList["ClickItem"]("Part")

    // 15- Choose ""PartNum"" column
    Log["Message"]("Step 15")
    var dataColumnList = FindObject("*ListBox*", "Name", "*lstDataColumns*")
    dataColumnList["ClickItem"]("PartNum")
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["lstDataColumns"]["ClickItem"]("PartNum")

    // click ""Add Subscribe column"" button
    // Aliases["Epicor"]["CustomWizardDialog"]["CustomEmbeddedDashboardPanelWizardPanel"]["grpStep3a"]["btnAddSubscribeColumn"]["Click"]()
    ClickButton("Add Subscribe Column")
    
    // 16- click "Finish" button
    Log["Message"]("Step 16")
    // Aliases["Epicor"]["CustomWizardDialog"]["WinFormsObject"]("btnFinish")["Click"]()
    ClickButton("Finish")
    
    // 17- Press Right arrow to move tab to ""PartStatus Sheets"" panel
    Log["Message"]("Step 17")
    // Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["customSheetWizard"]["btnAddCustomSheet"]["Click"]()
    ClickButton("", "btnAddCustomSheet")
    Log["Message"]("Sheet was added to Custom Sheets.")

    // 18- Click File> Save customization As  
    Log["Message"]("Step 18")
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Save Customization As ...")
    ClickMenu("File->Save Customization As ...")

    // 19- On Name enter EmbDash and on Description enter Embedded Dashboard and click Save        
    Log["Message"]("Step 19")
    EnterText("txtKey1a","EmbDash")
    EnterText("txtDescription", "Embedded Dashboard")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtKey1a")["Keys"]("EmbDash")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("grpNewCustInfo")["WinFormsObject"]("txtDescription")["Keys"]("Embedded Dashboard")
    // Aliases["Epicor"]["WinFormsObject"]("CustomSaveDialog")["WinFormsObject"]("btnOk")["Click"]()
    ClickButton("Save")
    // Aliases["Epicor"]["WinFormsObject"]("CustomCommentDialog")["WinFormsObject"]("btnOK")["Click"]()
    ClickButton("OK")

    Log["Message"]("Customization saved.")
    
    // 21- close the customization 
    Log["Message"]("Step 21")
    // Aliases["Epicor"]["CustomToolsDialog"]["zEpiForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|&Close")
    ClickMenu("File->Close")
    Log["Message"]("Customization closed.")   

    // 22- Close the form"  
    Log["Message"]("Step 22")
    // Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")  
    ClickMenu("File->Exit") 
    Log["Message"]("Part Form closed.")   
}

  
function OpenCustomedForm(){
    // 23- Go to Production Management> Job Management> Setup> Part (Open again Part Maintenance (using developer mode)
    Log["Message"]("Step 23")
    //ActivateMainDevMode()
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

    Delay(2500)

    /*(FUTURE REFERENCE FOR TREE LIST ITEMS)*/
    // 24- Select the created customizacion       
    Log["Message"]("Step 24")
    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["grpCustomization"]["etvAvailableLayers"]["ClickItem"]("Base|EP|Customizations|EmbDash")
    var availableLayers = GetTreePanel("AvailableLayers")
    availableLayers["ClickItem"]("Base|EP|Customizations|EmbDash")
    // Aliases["Epicor"]["CustomSelectCustTransDialog"]["btnOK"]["Click"]()
    ClickButton("OK")
    
    Delay(2500)
}    



function TestCustomedForm(){

    var testPart = "00P1"
    // 25, 26 - Click on Part button - Click Search and select a Part and click Ok                              
    Log["Message"]("Step 25,26")
    // Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["Activate"]()
    // Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["partDetailPanel1"]["groupBox1"]["tbPart"]["Keys"](testPart)
    // Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["dockableWindow3"]["partDockPanel1"]["windowDockingArea1"]["dockableWindow1"]["partDetailPanel1"]["groupBox1"]["tbPart"]["Keys"]("[Tab]")
    OpenPanelTab("Part")
    EnterText("tbPart", testPart + "[Tab]")
    Log["Message"]("00P1 customer was retrived")
    
    Delay(2500)
    
    // Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["WinFormsObject"]("DockableWindow", "", 7)["Activate"]()
    OpenPanelTab("PartStatus")
    Log["Message"]("PartStatus tab Activated")

     Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")
    //ClickMenu("Edit->Refresh")
    
    // var PartTxtfield = Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["WinFormsObject"]("DockableWindow", "", 7)["WinFormsObject"]("PartStatus")["WinFormsObject"]("PartStatusDashboardPanel")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow3")["WinFormsObject"]("dbFillPanel1")["WinFormsObject"]("WindowDockingArea", "", 5)["WinFormsObject"]("DockableWindow", "", 1)["WinFormsObject"]("a01b8010-d16b-4b6c-8e86-337ac824f218")["WinFormsObject"]("QueryFillPanel")["WinFormsObject"]("WindowDockingArea", "")["WinFormsObject"]("DockableWindow", "", 1)["WinFormsObject"]("b96666ad-9cbc-482e-8793-97d8650c6b0c")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow1")["WinFormsObject"]("TrackerPanel")["WinFormsObject"]("txtPart_PartNum")
    var PartTxtfield = GetTextBox("txtPart_PartNum")

    if(PartTxtfield["Text"]["OleValue"] == testPart){
        Log["Checkpoint"]("Part " + testPart + " was retrieved and displayed on PartStatus")
        ClickButton("Retrieve")
    }else{
        Log["Error"]("Part " + testPart + " was not retrieved and displayed on PartStatus")
    }
    
    ClickButton("Retrieve")

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

    Delay(2500)
    Aliases["Epicor"]["PartForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea3"]["DockableWindow"]["PartStatus"]["PartStatusDashboardPanel"]["windowDockingArea1"]["dockableWindow3"]["dbFillPanel1"]["WindowDockingArea"]["DockableWindow"]["Activate"]()

    var warehouseResultGrid = searchGrid["FindChild"](["FullName", "WndCaption"], ["*grid*","*All*"], 20)

    if (warehouseResultGrid["Rows"]["Count"] > 0) {
       Log["Checkpoint"]("There is a part retrieved and displayed on warhouse grid")
    }else{
        Log["Error"]("There is not a part retrieved and displayed on warhouse grid")
    }

    // Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
    ClickMenu("File->Exit")
    Log["Message"]("Part Form closed")

    DeactivateMainDevMode()
}

    


