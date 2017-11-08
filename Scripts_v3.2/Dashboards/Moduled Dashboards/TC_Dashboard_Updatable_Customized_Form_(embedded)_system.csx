//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT DataBase_Functions
//USEUNIT ControlFunctions
//USEUNIT Menu_Functions


function TC_Dashboard_Updatable_Customized_Form_sys(){}

    // Variables
    var company1 = "Epicor Education"
    var plant1 = "Main"

    //Used to navigate thru the Main tree panel
    var treeMainPanel1 = setCompanyMainTree(company1,plant1)

//Steps 2 to 4
function OpenFormDevMode(){
    Log["Message"]("Step 2")
    ClickMenu("Options->Developer Mode")

    ExpandComp(company1)
    ChangePlant(plant1)

    Log["Message"]("Step 3")
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

    DElay(2500)
    Log["Message"]("Step 4")
    CheckboxState("chkBaseOnly", true)

    ClickButton("OK")
    Delay(10000)
    
    E10["Refresh"]()
    Log["Message"]("Step 5")
    
    ClickMenu("Tools->Customization")
}

// Steps 5 to 22
function CreateCustomization() {
    Delay(2500)  
    Log["Message"]("Step 6")
    
    var CustomToolsDialog = Aliases["Epicor"]["CustomToolsDialog"]["tabCustomToolsDialog"]
    CustomToolsDialog["tpgCodeWizards"]["Tab"]["Selected"] = true
    CustomToolsDialog["tpgCodeWizards"]["tabEventWizard"]["tpgSheetWizard"]["Tab"]["Selected"] = true

    ClickButton("New Custom Sheet")
    
    Log["Message"]("Step 7")

    var dockableSheetsList = FindObject("*ListBox*", "Name", "*lstStandardSheets*")
    dockableSheetsList["ClickItem"]("mainPanel1")
    Log["Message"]("mainPanel1 sheet selected")

    // 8- Name, text, tab text = ""TEST""
    Log["Message"]("Step 8")

    EnterText("txtSheetName", "PartStatus")
    EnterText("txtSheetText", "PartStatus")
    EnterText("txtSheetTextTab", "PartStatus")

    Log["Message"]("Step 9")
    ClickButton("Add Dashboard...")
    
    Log["Message"]("Step 10")
    EnterText("txtDashboardID", "PartOnHandStatus")

    Log["Message"]("Step 12")
    ClickButton("Next>")

    Log["Message"]("Step 13")

    var dockableSheetsList = FindObject("*RadioButton*", "Name", "*radRetrieveWButton*")
    dockableSheetsList["Click"]()

    ClickButton("Next>")

    Log["Message"]("Step 14")
    var dataViewList = FindObject("*ListBox*", "Name", "*lstDataViews*")
    dataViewList["ClickItem"]("Part")

    Log["Message"]("Step 15")
    var dataColumnList = FindObject("*ListBox*", "Name", "*lstDataColumns*")
    dataColumnList["ClickItem"]("PartNum")

    ClickButton("Add Subscribe Column")
    
    Log["Message"]("Step 16")
    ClickButton("Finish")
    
    Log["Message"]("Step 17")
    ClickButton("", "btnAddCustomSheet")
    Log["Message"]("Sheet was added to Custom Sheets.")

    Log["Message"]("Step 18")
    ClickMenu("File->Save Customization As ...")

    Log["Message"]("Step 19")
    EnterText("txtKey1a","EmbDash")
    EnterText("txtDescription", "Embedded Dashboard")
    ClickButton("Save")
    ClickButton("OK")

    Log["Message"]("Customization saved.")
    
    Log["Message"]("Step 21")
    ClickMenu("File->Close")
    Log["Message"]("Customization closed.")   

    Log["Message"]("Step 22")
    ClickMenu("File->Exit") 
    Log["Message"]("Part Form closed.")   
}

function OpenCustomedForm(){
    Log["Message"]("Step 23")
    MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")

    Delay(2500)
    
    E10["Refresh"]()
    Log["Message"]("Step 24")

    var availableLayers = GetTreePanel("AvailableLayers")
    availableLayers["ClickItem"]("Base|EP|Customizations|EmbDash")
    ClickButton("OK")
    
    Delay(2500)
}    

function TestCustomedForm(){
    E10["Refresh"]()
    var testPart = "00P1"

    Log["Message"]("Step 25,26")

    OpenPanelTab("Part")
    EnterText("tbPart", testPart + "[Tab]")
    Log["Message"]("00P1 customer was retrived")
    
    Delay(2500)
    
    OpenPanelTab("PartStatus")
    Log["Message"]("PartStatus tab Activated")

     Aliases["Epicor"]["PartForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Refresh")

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

    ClickMenu("File->Exit")
    Log["Message"]("Part Form closed")

    DeactivateMainDevMode()
}

    


