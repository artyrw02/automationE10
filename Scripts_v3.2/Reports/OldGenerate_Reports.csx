//USEUNIT ControlFunctions
//USEUNIT General_Functions
//USEUNIT Menu_Functions
//USEUNIT Grid_Functions

//Script to generate reports
//A function for each report is created and called on TC routine 

function Report_Testing(){
	//XML["XmlCheckpoint1"]["Check"]("C:\\Users\\Administrator\\Documents\\Reports\\Sales Order Acknowledgment00885.xml");
 	// XML["XmlCheckpoint1"]["Check"](pathFileReport);
  Log["Message"]("Starting - Generate Reports")

}


/*ARForm: Mass Print AR Invoices.
                Sales Management/Demand Management/Reports/Mass Print AR Invoices
                go to filter tab, click customers, select Addison, Inc.
                Click generate Only.*/

function ReportARInvoice() {
	var customer = "ADDISON"
	var reportStyle = "Standard - SSRS - ARFORM2"

	ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print AR Invoices")

	if(Aliases["Epicor"]["ARInvForm"]["Exists"]){
		Log["Message"]("Form 'Mass Print AR Invoices' opened.")
	}

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Activates 'Filter' Tab
	OpenPanelTab("Filter")

	ClickButton("Customers...")

	EnterText("txtStartWith1", customer)

	ClickButton("Search")

	var searchGrid = Aliases["Epicor"]["CustomerSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]
	var CustIDColumn = getColumn(searchGrid, "Cust. ID")

	if(searchGrid["Rows"]["Count"] > 0 ){
		for(var i = 0; i < searchGrid["Rows"]["Count"]; i++){
			var cell = searchGrid["Rows"]["Item"](i)["Cells"]["Item"](CustIDColumn)

			if (cell["Text"]["OleValue"] == customer) {
			    // Selecting cell
			    searchGrid["Rows"]["Item"](i)["Selected"] = true
			    // Click Ok to select customer
			    ClickButton("OK")
			    Log["Message"]("Customer selected")
			    break
			}
		}
	}else{
		Log["Error"]("Search for customer didn't retrieve records.")
	}

	var customersGrid = GetGrid("grdCustomers")
	
	if(customersGrid["Rows"]["Count"] > 0){
		var CustIDColumn = getColumn(customersGrid, "Cust. ID")

		for(var i = 0; i < customersGrid["Rows"]["Count"]; i++){
			var cell = customersGrid["Rows"]["Item"](i)["Cells"]["Item"](CustIDColumn)

			if (cell["Text"]["OleValue"] == customer) {
			    Log["Message"]("Customer " + customer + " appears on grid.")
			    break
			}
		}
	}else{
		Log["Error"]("Customer was not selected.")
	}

	Delay(2500)
		ClickMenu("File->Generate Only")
		Log["Message"]("'Generate Only' option clicked from menu")

		Delay(4000)
		//Close Form
		ClickMenu("File->Exit")

		if(!Aliases["Epicor"]["ARInvForm"]["Exists"]){
			Log["Message"]("Form 'Mass Print AR Invoices' closed.")
		}
}

/*Jobtrav: JobTraveler.
                Production Management/Job Management/Reports/Job Traveler
                Go to filter tab, Click on job button, select the first 3 jobs (005354-1-1, 2000, 2022) click ok.
                Click Generate Only*/

function ReportJobTraveler(){
	// var customer = "ADDISON"
	var reportStyle = "Standard - SSRS - JOBTRAV2"

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Production Management;Job Management;Reports;Job Traveler")

	if(Aliases["Epicor"]["JobTravForm"]["Exists"]){
		Log["Message"]("Form 'Job Traveler' opened.")
	}

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	OpenPanelTab("Filter")

	ClickButton("Job...")

	//Opens 'Job search' form 
	ClickButton("Search")	

	var jobEntrySearchForm = GetGrid("ugdSearchResults")

	//Select first three jobs
	while(true){
		jobEntrySearchForm["Rows"]["Item"](0)["Selected"] = true
		jobEntrySearchForm["Keys"]("![Down]![Down]")
		break
	}

	ClickButton("OK")

	// var gridJobs = Aliases["Epicor"]["JobTravForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["filterPanel1"]["windowDockingArea1"]["dockableWindow1"]["jobPanel1"]["grdJob"]
	var gridJobs = GetGrid("grdJob")

	if (gridJobs["wRowCount"] == 3) {
		Log["Message"]("Jobs were selected.")
	}else {
	    Log["Error"]("Jobs weren't selected.")
	}

	//Pending Generate only
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//Close Form
	ClickMenu("File->Exit")

	if(!Aliases["Epicor"]["JobTravForm"]["Exists"]){
		Log["Message"]("Form 'Job Traveler' closed.")
	}
}

/*OrderAck: Sales order entry.
                Sales Management/Customer Relationship Management/General Operations/Order Entry
                Order: 5428
                Actions > Print Sales Order Acknowledgement
                Go to filter tab, click new.
                Order: 5428*/

function ReportSalesOrder(){
	var order = "5428"
	var reportStyle = "Standard - SSRS - ORDERACK2"

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")

	if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
		Log["Message"]("Form 'Sales order Entry' opened.")
	}

	//Select Order
	EnterText("txtKeyField", order + "[Tab]")

	//Click on 'print'
	ClickMenu("Actions->Print Sales Order Acknowledgement")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Go to 'filter' tab
	OpenPanelTab("Filter")

	ClickMenu("File->New")

	var gridSalesOrder = GetGrid("grdOrders")

	var orderColumn = getColumn(gridSalesOrder, "Order")

	gridSalesOrder["ActiveRow"]["Cells"]["Item"](orderColumn)["Click"]()
	gridSalesOrder["Keys"](order + "[Del]" + "[Tab]")

	//Pending Validation
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	
	//closes OrderAck form (print)
	ClickMenu("File->Exit")

	if (!Aliases["Epicor"]["SalesOrderAckForm"]["Exists"]) {
	    Log["Message"]("Order Ack form closed")
	    //closes PO form (entry)
	    ClickMenu("File->Exit")
	}

	if (!Aliases["Epicor"]["SalesOrderForm"]["Exists"]) {
	    Log["Message"]("Sales Order form closed")
	}
}	


/* ProFormaInvc: Sales order entry.
                Sales Management/Customer Relationship Management/General Operations/Order Entry
                Order: 5428
                Actions > Print Sales Order Acknowledgement
                Go to filter tab, click new.
                Order: 5428

                For ProFormaInvc we can use the same script.
                Actions > Print Pro-Forma Invoice.
                Generate Only*/
function ReportProFormaInv(){
	var order = "5428"
	var reportStyle = "Standard - SSRS - ProFormaInvc2"

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")

	if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
		Log["Message"]("Form 'Sales order Entry' opened.")
	}

	//Select Order
	// Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"](order)
	// Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"]("[Tab]")
	EnterText("txtKeyField", order + "[Tab]")

	// Select Actions > Print Pro-Forma Invoice.
	// Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Print Pro-forma Invoice")
	ClickMenu("Actions->Print Pro-Forma Invoice")

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["ProFormaInvcReportForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["detailPanel1"]["grbSched"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	// Aliases["Epicor"]["ProFormaInvcReportForm"]["zProFormaInvcReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)

	//closes OrderAck form (print)
	// Aliases["Epicor"]["ProFormaInvcReportForm"]["zProFormaInvcReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	if (!Aliases["Epicor"]["ProFormaInvcReportForm"]["Exists"]) {
	    Log["Message"]("Order Ack form closed")
	    //closes PO form (entry)
		// Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
		ClickMenu("File->Exit")
	}

	if (!Aliases["Epicor"]["SalesOrderForm"]["Exists"]) {
	    Log["Message"]("Sales Order form closed")
	}
}	


/*POForm: Purchase Order Entry
                Material Management/Purchase Management/General Operations/ Purchase Order Entry
                PO Number: 4307
                Actions > Print
                Generate Only*/
function ReportPurchaseOrder(){
    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Material Management;Purchase Management;General Operations;Purchase Order Entry")
	
	var poNum = "4307"
	var reportStyle = "Standard - SSRS - POForm2"

	if(Aliases["Epicor"]["POEntryForm"]["Exists"]){
		Log["Message"]("Form 'Purchase Order Entry' opened.")
	}

	//Select PO number
	// Aliases["Epicor"]["POEntryForm"]["windowDockingArea2"]["dockableWindow1"]["mainDockPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["grpPO"]["txtPONumber"]["Keys"](poNum)
	// Aliases["Epicor"]["POEntryForm"]["windowDockingArea2"]["dockableWindow1"]["mainDockPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["grpPO"]["txtPONumber"]["Keys"]("[Tab]")
	EnterText("txtPONumber", poNum + "[Tab]")

	//Click on 'print'
	// Aliases["Epicor"]["POEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|&Print")
	ClickMenu("Actions->Print")

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["POForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["grp3"]["cboStyle"]

	Delay(1000)
	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["POForm"]["zPOForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
    //closes PO form (print)
	// Aliases["Epicor"]["POForm"]["zPOForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	if (!Aliases["Epicor"]["POForm"]["Exists"]) {
	    Log["Message"]("PO print form closed")
	    //closes PO form (entry)
		// Aliases["Epicor"]["POEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
		ClickMenu("File->Exit")
	}

	if (!Aliases["Epicor"]["POEntryForm"]["Exists"]) {
	    Log["Message"]("Purchase orderform closed")
	}
}

/*QuotForm2: Opportunity/QuotEntry.
                Sales Management/ Customer Relationship Management/ General Operations/ Opportunity / Quote Entry
                Opportunity/Quote: 1114
                Actions > Print Form
                Generate Only*/
function ReportQuoteform(){

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Opportunity / Quote Entry")
	
	var quote = "1114"
	var reportStyle = "Standard - SSRS - QuotForm2"

	if (Aliases["Epicor"]["QuoteForm"]["Exists"]) {
	    Log["Message"]("Form 'Opportunity / Quote Entry' opened.")
	}

	//Select Opportunity/Quote
	// Aliases["Epicor"]["QuoteForm"]["windowDockingArea1"]["dockableWindow7"]["topLevelSheets1"]["windowDockingArea1"]["dockableWindow6"]["summaryPanel1"]["grpQuote"]["txtQuoteNumber"]["Keys"](quote)
	// Aliases["Epicor"]["QuoteForm"]["windowDockingArea1"]["dockableWindow7"]["topLevelSheets1"]["windowDockingArea1"]["dockableWindow6"]["summaryPanel1"]["grpQuote"]["txtQuoteNumber"]["Keys"]("[Tab]")
	EnterText("txtQuoteNumber", quote + "[Tab]")

	//Print Form
	// Aliases["Epicor"]["QuoteForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Print Form")
	ClickMenu("Actions->Print Form")

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["QuotFormForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["epiGroupBox2"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["QuotFormForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Quote form (print)
	// Aliases["Epicor"]["QuotFormForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	
	if (!Aliases["Epicor"]["QuotFormForm"]["Exists"]) {
	    Log["Message"]("Quote print form closed")
		//closes Quote form
		// Aliases["Epicor"]["QuoteForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
		ClickMenu("File->Exit")
	}

	if (!Aliases["Epicor"]["QuoteForm"]["Exists"]) {
	    Log["Message"]("Quote Form closed")
	}
}
    

// ---------------------------------------------------------------------------------------------------------

// APCheck: AP Payment Entry -- PENDING
//                 Finantial Management/Cash Management/General Operations/Payment Entry
//                 Group: 

function ReportAPPaymentform(){
    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Cash Management;General Operations;Payment Entry")
	var reportStyle = "Standard - SSRS - PACKSLIP2"

	if (Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	    Log["Message"]("Form 'Mass Print Packing Slips' opened.")
	}

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox2"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	// Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
	OpenPanelTab("Filter")
	
	ClickButton("Packing Slips...")

	var manufacturing = "102"
	
	//enter 102 for customer Dalton Manufacturing
	// Aliases["Epicor"]["CustShipSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["tabSearchPacks"]["etpBasic"]["basicPanel1"]["gbSortBy"]["eneStartWith1"]["Keys"](manufacturing)
	EnterText("eneStartWith1", manufacturing)

	var customerShipGrid = Aliases["Epicor"]["CustShipSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

	var packColumn = getColumn(customerShipGrid, "Pack")

	for(var i = 0; i < customerShipGrid["wRowCount"]; i++){
		var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumn)

		if(cell["Text"]["OleValue"] == manufacturing){
			customerShipGrid["Rows"]["Item"](i)["Selected"] = true
			Log["Message"]("Customer pack " +  manufacturing + " was selected.")
			break
		}
	}
	
	ClickButton("OK")

	var packListGrid = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdPackSlipList"]

	var packColumnID = getColumn(packListGrid, "Pack ID")

	for(var i = 0; i < packListGrid["wRowCount"]; i++){
		var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumnID)

		if(cell["Text"]["OleValue"] == manufacturing){
			Log["Message"]("Customer pack " +  manufacturing + " was selected and displayed on pack slips list.")
			break
		}
	}

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	
	if (!Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	    Log["Message"]("Packing Slip Form closed")
	}
}
   

/*PackSlips: Mass PrintPacking Slips.
                Sales Management/Demand Management/Reports/Mass Print Packing Slips
                Go to filter tab. 
                Click Packing slips button.
                Select 102 Dalton Manufacturing.*/
function ReportPrintPackingform(){
    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print Packing Slips")
	var reportStyle = "Standard - SSRS - PACKSLIP2"

	if (Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	    Log["Message"]("Form 'Mass Print Packing Slips' opened.")
	}

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox2"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	// Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
	OpenPanelTab("Filter")
	
	ClickButton("Pack Slips...")

	var manufacturing = "102"
	
	//enter 102 for customer Dalton Manufacturing
	// Aliases["Epicor"]["CustShipSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["tabSearchPacks"]["etpBasic"]["basicPanel1"]["gbSortBy"]["eneStartWith1"]["Keys"](manufacturing)
	EnterText("eneStartWith1", manufacturing)
	ClickButton("Search")

	// var customerShipGrid = Aliases["Epicor"]["CustShipSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]
	var customerShipGrid = GetGrid("ugdSearchResults")

	var packColumn = getColumn(customerShipGrid, "Pack")

	for(var i = 0; i < customerShipGrid["Rows"]["Count"]; i++){
		var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumn)
		
		if(cell["Text"]["OleValue"] == manufacturing){
			customerShipGrid["Rows"]["Item"](i)["Selected"] = true
			Log["Message"]("Customer pack " +  manufacturing + " was selected.")
			break
		}else{
			customerShipGrid["Rows"]["Item"](i)["Selected"] = false
		}
	}
	
	ClickButton("OK")

	// var packListGrid = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdPackSlipList"]
	var packListGrid = GetGrid("grdPackSlipList")

	var packColumnID = getColumn(packListGrid, "Pack ID")

	for(var i = 0; i < packListGrid["wRowCount"]; i++){
		var cell = packListGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumnID)

		if(cell["Text"]["OleValue"] == manufacturing){
			Log["Message"]("Customer pack " +  manufacturing + " was selected and displayed on pack slips list.")
			break
		}
	}

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	
	if (!Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	    Log["Message"]("Packing Slip Form closed")
	}
}
   

/* Customer Statements
                Financial Management/Accounts Receivable/Reports/Customer Statements
                Go to filter tab. 
                Click Customer... button.
                Search - ADDISON
                Select 102 Dalton Manufacturing.*/
function ReportCustomerStatements(){
    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Accounts Receivable;Reports;Customer Statements")
	// var reportStyle = "Standard - SSRS - PACKSLIP2"

	// if (Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	//     Log["Message"]("Form 'Mass Print Packing Slips' opened.")
	// }

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox2"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	// Delay(1000)
	// ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	// Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
	OpenPanelTab("Filter")
	
	ClickButton("Customer...")

	var customer = "ADDISON"
	
	EnterText("txtStartWith1", customer)
	ClickButton("Search")

	var customerShipGrid = Aliases["Epicor"]["CustomerSearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

	var packColumn = getColumn(customerShipGrid, "Cust. ID")

	for(var i = 0; i < customerShipGrid["Rows"]["Count"]; i++){
		var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumn)

		if(cell["Text"]["OleValue"] == customer){
			customerShipGrid["Rows"]["Item"](i)["Selected"] = true
			Log["Message"]("Customer " +  customer + " was selected.")
			break
		}
	}
	
	ClickButton("OK")

	// var packListGrid = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdPackSlipList"]

	// var packColumnID = getColumn(packListGrid, "Pack ID")

	// for(var i = 0; i < packListGrid["wRowCount"]; i++){
	// 	var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumnID)

	// 	if(cell["Text"]["OleValue"] == customer){
	// 		Log["Message"]("Customer pack " +  customer + " was selected and displayed on pack slips list.")
	// 		break
	// 	}
	// }

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	
	// if (!Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	//     Log["Message"]("Packing Slip Form closed")
	// }
}

/* Sales Order Pick List
                Sales Management/Order Management/Reports/Sales Order Pick List
                Go to filter tab. 
                Click Customer... button.
                Search - ADDISON
                Select 102 Dalton Manufacturing.*/
function ReportSOPickList(){
    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Order Management;Reports;Sales Order Pick List")
	// var reportStyle = "Standard - SSRS - PACKSLIP2"

	// if (Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	//     Log["Message"]("Form 'Mass Print Packing Slips' opened.")
	// }

	//Select Report style
	// var reportStyleCombo = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["groupBox2"]["cboStyle"]

	//Activates combo
	// DropDownValue(reportStyleCombo, reportStyle)
	// Delay(1000)
	// ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	// Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()
	OpenPanelTab("Filter")
	
	ClickButton("Order...")

	var order = "5482"
	
	EnterText("eneStartsWith1", order)
	ClickButton("Search")

	var OrderGrid = Aliases["Epicor"]["WinFormsObject"]("SalesOrderSearchForm")["WinFormsObject"]("pnlSearchGrid")["WinFormsObject"]("ugdSearchResults")

	var salesOrderColumn = getColumn(OrderGrid, "Sales Order")

	for(var i = 0; i < OrderGrid["Rows"]["Count"]; i++){
		var cell = OrderGrid["Rows"]["Item"](i)["Cells"]["Item"](salesOrderColumn)

		if(cell["Text"]["OleValue"] == order){
			OrderGrid["Rows"]["Item"](i)["Selected"] = true
			Log["Message"]("Customer " +  order + " was selected.")
			break
		}
	}
	
	ClickButton("OK")

	OpenPanelTab("Selection")

	var actualDate = Aliases["Epicor"]["WinFormsObject"]("SOPickListForm")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow3")["WinFormsObject"]("mainPanel1")["WinFormsObject"]("windowDockingArea1")["WinFormsObject"]("dockableWindow2")["WinFormsObject"]("detailPanel1")["WinFormsObject"]("groupBox1")["WinFormsObject"]("tdtFrom")["WinFormsObject"]("dteActualDate")
	actualDate["Keys"]("10/09/2013" + "[Tab]")

	// var packListGrid = Aliases["Epicor"]["PackingSlipPrintForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdPackSlipList"]

	// var packColumnID = getColumn(packListGrid, "Pack ID")

	// for(var i = 0; i < packListGrid["wRowCount"]; i++){
	// 	var cell = customerShipGrid["Rows"]["Item"](i)["Cells"]["Item"](packColumnID)

	// 	if(cell["Text"]["OleValue"] == customer){
	// 		Log["Message"]("Customer pack " +  customer + " was selected and displayed on pack slips list.")
	// 		break
	// 	}
	// }

	//Pending Validation
	Delay(2500)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|Generate Only")
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	// Aliases["Epicor"]["PackingSlipPrintForm"]["zPackingSlipPrintForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	ClickMenu("File->Exit")
	
	// if (!Aliases["Epicor"]["PackingSlipPrintForm"]["Exists"]) {
	//     Log["Message"]("Packing Slip Form closed")
	// }
}