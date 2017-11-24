//USEUNIT ControlFunctions
//USEUNIT General_Functions
//USEUNIT Menu_Functions
//USEUNIT Grid_Functions

//Script to generate reports
//A function for each report is created and called on TC routine 

function Report_Testing(){
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

	ValidatesFormOpened("Mass Print AR Invoices")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Activates 'Filter' Tab
	OpenPanelTab("Filter")

	ClickButton("Customers...")

	EnterText("txtStartWith1", customer)

	ClickButton("Search")

	var searchGrid = GetGrid("ugdSearchResults")
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

	//Close Form
	Delay(4000)
	ClickMenu("File->Exit")

	ValidatesFormClosed("Mass Print AR Invoices")
}

/*Jobtrav: JobTraveler.
                Production Management/Job Management/Reports/Job Traveler
                Go to filter tab, Click on job button, select the first 3 jobs (005354-1-1, 2000, 2022) click ok.
                Click Generate Only*/

function ReportJobTraveler(){
	var reportStyle = "Standard - SSRS - JOBTRAV2"

    ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Production Management;Job Management;Reports;Job Traveler")

	//Validates if form was opened
	ValidatesFormOpened("Job Traveler Report")

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

	//Validates if form is still on screen
	ValidatesFormClosed("Job Traveler")
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

	ValidatesFormOpened("Sales Order Entry")

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

	ValidatesFormClosed("Order Ack")
	ClickMenu("File->Exit")

	ValidatesFormClosed("Sales Order")
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

	ValidatesFormOpened("Sales Order Entry")

	//Select Order
	EnterText("txtKeyField", order + "[Tab]")

	// Select Actions > Print Pro-Forma Invoice.
	ClickMenu("Actions->Print Pro-Forma Invoice")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)

	//closes OrderAck form (print)
	ClickMenu("File->Exit")

	ValidatesFormClosed("Order Ack")

	ClickMenu("File->Exit")

	ValidatesFormClosed("Sales Order")
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

	ValidatesFormOpened("Purchase Order Entry")

	//Select PO number
	EnterText("txtPONumber", poNum + "[Tab]")

	//Click on 'print'
	ClickMenu("Actions->Print")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
    //closes PO form (print)
	ClickMenu("File->Exit")
	
	ValidatesFormClosed("PO Print")

	ClickMenu("File->Exit")

	ValidatesFormClosed("Purchase Order Entry")
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

	ValidatesFormOpened("Opportunity/Quote Entry")

	//Select Opportunity/Quote
	EnterText("txtQuoteNumber", quote + "[Tab]")

	//Print Form
	ClickMenu("Actions->Print Form")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	//Pending Validation
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Quote form (print)
	ClickMenu("File->Exit")
	
	ValidatesFormClosed("Quote Print")

	ClickMenu("File->Exit")

	ValidatesFormClosed("Opportunity / Quote Entry")
}
    

// ---------------------------------------------------------------------------------------------------------

// APCheck: AP Payment Entry -- PENDING
//                 Finantial Management/Cash Management/General Operations/Payment Entry
//                 Group: 

function ReportAPPaymentform(){
    /*ExpandComp("Epicor USA")

    ChangePlant("Chicago")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Cash Management;General Operations;Payment Entry")

	ValidatesFormOpened("AP Payment Entry")

	//Select Report style
	Delay(1000)
	var reportStyle = "Standard - SSRS - PACKSLIP2"
	ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	OpenPanelTab("Filter")
	
	ClickButton("Packing Slips...")

	var manufacturing = "102"
	
	//enter 102 for customer Dalton Manufacturing
	EnterText("eneStartWith1", manufacturing)

	var customerShipGrid = GetGrid("ugdSearchResults")

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
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	ClickMenu("File->Exit")
	
	ValidatesFormClosed("AP Payment Entry")*/
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

	ValidatesFormOpened("Mass Print Packing Slips")

	//Select Report style
	Delay(1000)
	ComboboxSelect("cboStyle", reportStyle)

	// Activates 'Filter' tab
	OpenPanelTab("Filter")
	
	ClickButton("Pack Slips...")

	var manufacturing = "100"
	
	//enter 102 for customer Dalton Manufacturing
	EnterText("eneStartWith1", manufacturing)
	ClickButton("Search")

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
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	ClickMenu("File->Exit")
	
	ValidatesFormClosed("Mass Print Packing Slips")
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
    
    MainMenuTreeViewSelect("Epicor USA;Chicago;System Management;Reporting;Report Style")

    EnterText("txtKeyField", "CustSt" + "[Tab]")

    OpenPanelTab("Styles")

    ComboboxSelect("cboOutputType", "XML")

    ClickMenu("File->Save")
    ClickMenu("File->Exit")


	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Accounts Receivable;Reports;Customer Statements")
	ValidatesFormOpened("Customer Statements Report")
	
	OpenPanelTab("Filter")
	
	ClickButton("Customer...")

	var customer = "ADDISON"
	
	EnterText("txtStartWith1", customer)
	ClickButton("Search")

	var customerShipGrid = GetGrid("ugdSearchResults")

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

	//Pending Validation
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	ClickMenu("File->Exit")
	ValidatesFormClosed("Customer Statements")


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
    // Modify report style
    MainMenuTreeViewSelect("Epicor USA;Chicago;System Management;Reporting;Report Style")

    EnterText("txtKeyField", "SOPick" + "[Tab]")

    OpenPanelTab("Styles")

    ComboboxSelect("cboOutputType", "XML")

    ClickMenu("File->Save")
    ClickMenu("File->Exit")

	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Order Management;Reports;Sales Order Pick List")

	ValidatesFormOpened("Sales Order Pick List")

	OpenPanelTab("Filter")
	
	ClickButton("Order...")

	var order = "5482"
	
	EnterText("eneStartsWith1", order)
	ClickButton("Search")

	var OrderGrid = GetGrid("ugdSearchResults")

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

	var groupDateFrom = FindObject("*Date*", "Name", "*tdtFrom*" )
	var groupDateTo = FindObject("*Date*", "Name", "*tdtTo*" )
	var dteActualDate = FindObject("*Date*", "Name", "*dteActualDate*", groupDateFrom)
	var dteActualDateTo = FindObject("*Date*", "Name", "*dteActualDate*", groupDateTo)

	dteActualDate["Keys"]("10/09/2013" + "[Tab]")
	dteActualDateTo["Keys"]("11/13/2017" + "[Tab]")

	//Pending Validation
	Delay(2500)
	ClickMenu("File->Generate Only")
	Log["Message"]("'Generate Only' option clicked from menu")

	Delay(4000)
	//closes Packing Slip form (print)
	ClickMenu("File->Exit")
	
	ValidatesFormClosed("Sales Order Pick List")
}