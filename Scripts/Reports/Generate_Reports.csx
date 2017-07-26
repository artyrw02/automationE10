//USEUNIT ControlFunctions
//USEUNIT General_Functions
//USEUNIT Grid_Functions

//Script to generate reports
//A function for each report is created and called on TC routine 

var pathFileReportSalesOrder = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"
var pathFileReportQuoteform = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"
var pathFileReportPurchaseOrder = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"
var pathFileReportProFormaInv = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"
var pathFileReportJobTraveler = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"
var pathFileReportARInvoice = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"

function Report_Testing(){
	//XML["XmlCheckpoint1"]["Check"]("C:\\Users\\Administrator\\Documents\\Reports\\Sales Order Acknowledgment00885.xml");
 	// XML["XmlCheckpoint1"]["Check"](pathFileReport);
  Log["Message"]("Starting - Generate Reports")
}


/*ARForm: Mass Print AR Invoices.
                Sales Management/Demand Management/Reports/Mass Print AR Invoices
                go to filter tab, click customers, select Addison, Inc.
                Click generate Only.*/

function ReportARInvoice(){
	var customer = "ADDISON"
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print AR Invoices")

	if(Aliases["Epicor"]["ARInvForm"]["Exists"]){
		Log["Message"]("Form 'Mass Print AR Invoices' opened.")
	}

	//Activates 'Filter' Tab
	Aliases["Epicor"]["ARInvForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

	ClickButton("Customers...")

	Aliases["Epicor"]["CustomerSearchForm"]["windowDockingArea1"]["dockableWindow1"]["pnlSearchCrit"]["searchTabPanel1"]["etcSearch"]["etpBasic"]["basicPanel1"]["gbBasic"]["txtStartWith1"]["Keys"](customer)
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
			    break
			    Log["Message"]("Customer selected")
			}
		}
	}else{
		Log["Error"]("Search for customer didn't retrieve records.")
	}

	var customersGrid = Aliases["Epicor"]["ARInvForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea1"]["dockableWindow1"]["listPanel1"]["grdCustomers"]
	

	if(customersGrid["wRowCount"] > 0){
		var CustIDColumn = getColumn(customersGrid, "Cust. ID")

		for(var i = 0; i < customersGrid["Rows"]["Count"]; i++){
			var cell = customersGrid["Rows"]["Item"](i)["Cells"]["Item"](CustIDColumn)

			if (cell["Text"]["OleValue"] == customer) {
			    Log["Message"]("Customer " + customer + " appears on grid.")
			    //Click on menu 'Generate only' PENDING
			    //Aliases["Epicor"]["ARInvForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Generate Only")
			    break
			}
		}

	}else{
		Log["Error"]("Customer was not selected.")
	}

	//Close Form
	Aliases["Epicor"]["ARInvForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

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
	MainMenuTreeViewSelect("Epicor USA;Chicago;Production Management;Job Management;Reports;Job Traveler")

	if(Aliases["Epicor"]["JobTravForm"]["Exists"]){
		Log["Message"]("Form 'Job Traveler' opened.")
	}

	// Activates 'Filter' tab
	Aliases["Epicor"]["JobTravForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["Activate"]()

	ClickButton("Job...")

	//Opens 'Job search' form 
	ClickButton("Search")	

	var jobEntrySearchForm = Aliases["Epicor"]["JobEntrySearchForm"]["pnlSearchGrid"]["ugdSearchResults"]

	//Select first three jobs
	while(true){
		jobEntrySearchForm["Rows"]["Item"](0)["Selected"] = true
		jobEntrySearchForm["Keys"]("![Down]![Down]")
		break
	}

	ClickButton("OK")

	var gridJobs = Aliases["Epicor"]["JobTravForm"]["windowDockingArea1"]["dockableWindow3"]["mainPanel1"]["windowDockingArea1"]["dockableWindow3"]["filterPanel1"]["windowDockingArea1"]["dockableWindow1"]["jobPanel1"]["grdJob"]

	if (gridJobs["wRowCount"] == 3) {
		Log["Message"]("Jobs were selected.")
	}else {
	    Log["Error"]("Jobs weren't selected.")
	}

	//Pending Generate only

	//Close Form
	Aliases["Epicor"]["JobTravForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")

	if(!Aliases["Epicor"]["JobTravForm"]["Exists"]){
		Log["Message"]("Form 'Job Traveler' closed.")
	}
}

/*OrderAck: Sales order entry.
                Sales Management/Customer Relationship Management/General Operations/Order Entry
                Order: 5428
                Actions > Print Sales Order Acknowledgement
                Go to filter tab, click new.
                Order: 5428

                For ProFormaInvc we can use the same script.
                Actions > Print Pro-Forma Invoice.
                Generate Only*/

function ReportSalesOrder(){
	var order = "5428"
	var reportStyle = ""

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")

	if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
		Log["Message"]("Form 'Sales order Entry' opened.")
	}

	//Select Order
	Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"](order)
	Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"]("[Tab]")

	//Click on 'print'
	Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Print Sales Order Acknowledgement")

	//Select Report style
	var reportStyleCombo = Aliases["Epicor"]["POForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["grp3"]["cboStyle"]

	//Activates combo
	// reportStyleCombo["Click"]()
	// var count = 0
	// while(true){
	// 	if(reportStyleCombo["Text"]["OleValue"] == reportStyle){
	// 		break
	// 		Log["Message"]("Report style " + reportStyle + " selected from combo.")
	// 	}
	// 	reportStyleCombo["Keys"]("[Down]")
	// 	count++
	// 	if (count == 5) {
	// 	    Log["Error"]("Report Style not found.")
	// 	    break
	// 	}
	// }

	//Go to 'filter' tab
	Aliases["Epicor"]["SalesOrderAckForm"]["windowDockingArea1"]["dockableWindow1"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|New")

	var gridSalesOrder = Aliases["Epicor"]["SalesOrderAckForm"]["windowDockingArea1"]["dockableWindow1"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdOrders"]

	var orderColumn = getColumn(gridSalesOrder, "Order")

	gridSalesOrder["ActiveRow"]["Cells"]["Item"](orderColumn)["Click"]()
	gridSalesOrder["Keys"]("^a[Del]" + order + "[Tab]")

	//Pending Validation
	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Generate Only")

	//PENDING

	//closes OrderAck form (print)
	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	
	//closes PO form (entry)
	Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
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

function ReportProFormaInv(){ PENDING DOUBTS ON SECOND ACTIONS PART
	var order = "5428"
	var reportStyle = ""

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")

	if(Aliases["Epicor"]["SalesOrderForm"]["Exists"]){
		Log["Message"]("Form 'Sales order Entry' opened.")
	}

	//Select Order
	Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"](order)
	Aliases["Epicor"]["SalesOrderForm"]["windowDockingArea2"]["dockableWindow3"]["sheetTopLevelPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["txtKeyField"]["Keys"]("[Tab]")

	//Click on 'print'
	Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Print Sales Order Acknowledgement")

	//Select Report style
	var reportStyleCombo = Aliases["Epicor"]["POForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["grp3"]["cboStyle"]

	//Activates combo
	// reportStyleCombo["Click"]()
	// var count = 0
	// while(true){
	// 	if(reportStyleCombo["Text"]["OleValue"] == reportStyle){
	// 		break
	// 		Log["Message"]("Report style " + reportStyle + " selected from combo.")
	// 	}
	// 	reportStyleCombo["Keys"]("[Down]")
	// 	count++
	// 	if (count == 5) {
	// 	    Log["Error"]("Report Style not found.")
	// 	    break
	// 	}
	// }

	//Go to 'filter' tab
	Aliases["Epicor"]["SalesOrderAckForm"]["windowDockingArea1"]["dockableWindow1"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["Activate"]()

	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|New")

	var gridSalesOrder = Aliases["Epicor"]["SalesOrderAckForm"]["windowDockingArea1"]["dockableWindow1"]["mainPanel1"]["windowDockingArea1"]["dockableWindow2"]["filter1"]["windowDockingArea2"]["dockableWindow1"]["listPanel1"]["grdOrders"]

	var orderColumn = getColumn(gridSalesOrder, "Order")

	gridSalesOrder["ActiveRow"]["Cells"]["Item"](orderColumn)["Click"]()
	gridSalesOrder["Keys"]("^a[Del]" + order + "[Tab]")

	//Pending Validation
	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Generate Only")

	//PENDING

	//closes OrderAck form (print)
	Aliases["Epicor"]["SalesOrderAckForm"]["zSalesOrderAckForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	
	//closes PO form (entry)
	Aliases["Epicor"]["SalesOrderForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
}	


/*POForm: Purchase Order Entry
                Material Management/Purchase Management/General Operations/ Purchase Order Entry
                PO Number: 4307
                Actions > Print
                Generate Only*/

function ReportPurchaseOrder(){
	MainMenuTreeViewSelect("Epicor USA;Chicago;Material Management;Purchase Management;General Operations;Purchase Order Entry")
	var poNum = "4307"
	var reportStyle = ""
	if(Aliases["Epicor"]["POEntryForm"]["Exists"]){
		Log["Message"]("Form 'Purchase Order Entry' opened.")
	}

	//Select PO number
	Aliases["Epicor"]["POEntryForm"]["windowDockingArea2"]["dockableWindow1"]["mainDockPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["grpPO"]["txtPONumber"]["Keys"](poNum)
	Aliases["Epicor"]["POEntryForm"]["windowDockingArea2"]["dockableWindow1"]["mainDockPanel1"]["windowDockingArea1"]["dockableWindow4"]["summaryPanel1"]["grpPO"]["txtPONumber"]["Keys"]("[Tab]")

	//Click on 'print'
	Aliases["Epicor"]["POEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|&Print")

	//Select Report style
	var reportStyleCombo = Aliases["Epicor"]["POForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["grp3"]["cboStyle"]

	//Activates combo
	reportStyleCombo["Click"]()
	var count = 0
	while(true){
		if(reportStyleCombo["Text"]["OleValue"] == reportStyle){
			break
			Log["Message"]("Report style " + reportStyle + " selected from combo.")
		}
		reportStyleCombo["Keys"]("[Down]")
		count++
		if (count == 5) {
		    Log["Error"]("Report Style not found.")
		    break
		}
	}

	//Pending Validation
	Aliases["Epicor"]["POForm"]["zPOForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Generate Only")

	//PENDING

	//closes PO form (print)
	Aliases["Epicor"]["POForm"]["zPOForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	
	//closes PO form (entry)
	Aliases["Epicor"]["POEntryForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
}

/*QuotForm2: Opportunity/QuotEntry.
                Sales Management/ Customer Relationship Management/ General Operations/ Opportunity / Quote Entry
                Opportunity/Quote: 1114
                Actions > Print Form
                Generate Only*/

function ReportQuoteform(){
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Opportunity / Quote Entry")
	var quote = "1114"
	var reportStyle = ""

	if (Aliases["Epicor"]["QuoteForm"]["Exists"]) {
	    Log["Message"]("Form 'Opportunity / Quote Entry' opened.")
	}

	//Select Opportunity/Quote
	Aliases["Epicor"]["QuoteForm"]["windowDockingArea1"]["dockableWindow7"]["topLevelSheets1"]["windowDockingArea1"]["dockableWindow6"]["summaryPanel1"]["grpQuote"]["txtQuoteNumber"]["Keys"](quote)
	Aliases["Epicor"]["QuoteForm"]["windowDockingArea1"]["dockableWindow7"]["topLevelSheets1"]["windowDockingArea1"]["dockableWindow6"]["summaryPanel1"]["grpQuote"]["txtQuoteNumber"]["Keys"]("[Tab]")

	//Print Form
	Aliases["Epicor"]["QuoteForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&Actions|Print Form")

	//Select Report style
	var reportStyleCombo = Aliases["Epicor"]["QuotFormForm"]["windowDockingArea1"]["dockableWindow1"]["detailPanel1"]["epiGroupBox2"]["cboStyle"]

	//Activates combo
	reportStyleCombo["Click"]()
	var count = 0
	while(true){
		if(reportStyleCombo["Text"]["OleValue"] == reportStyle){
			break
			Log["Message"]("Report style " + reportStyle + " selected from combo.")
		}
		reportStyleCombo["Keys"]("[Down]")
		count++
		if (count == 5) {
		    Log["Error"]("Report Style not found.")
		    break
		}
	}

	//Pending Validation
	Aliases["Epicor"]["QuotFormForm"]["zReportForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[1]|Generate Only")

	//PENDING

	//closes PO form (print)
	Aliases["Epicor"]["QuoteForm"]["zSonomaForm_Toolbars_Dock_Area_Top"]["ClickItem"]("[0]|&File|E&xit")
	
}
    

---------------------------------------------------------------------------------------------------------

APCheck: AP Payment Entry.
                Finantial Management/Cash Management/General Operations/Payment Entry
                Group: 

PackSlips: Mass PrintPacking Slips.
                Sales Management/Demand Management/Reports/Mass Print Packing Slips
                Go to filter tab. 
                Click Packing slips button.
                Select 102 Dalton Manufacturing.
