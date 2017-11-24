//USEUNIT FormLib
//USEUNIT General_Functions

function warmupReports(){  

	ExpandComp("Epicor USA")

	ChangePlant("Chicago")
	
	// ReportARInvoice
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print AR Invoices")
	Delay(2500)
	CloseTopForm()

	// ReportJobTraveler
	MainMenuTreeViewSelect("Epicor USA;Chicago;Production Management;Job Management;Reports;Job Traveler")
	Delay(2500)
	CloseTopForm()
	
	// ReportSalesOrder | ReportProFormaInv
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")
	Delay(2500)
	CloseTopForm()
	
	// ReportPurchaseOrder
	MainMenuTreeViewSelect("Epicor USA;Chicago;Material Management;Purchase Management;General Operations;Purchase Order Entry")
	Delay(2500)
	CloseTopForm()
	
	// ReportQuoteform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Opportunity / Quote Entry")
	Delay(2500)
	CloseTopForm()
	
	// ReportAPPaymentform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Cash Management;General Operations;Payment Entry")
	Delay(2500)
	CloseTopForm()
	
	// ReportPrintPackingform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print Packing Slips")
	Delay(2500)
	CloseTopForm()
	
	// ReportCustomerStatements
	MainMenuTreeViewSelect("Epicor USA;Chicago;System Management;Reporting;Report Style")
	Delay(2500)
	CloseTopForm()
	
	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Accounts Receivable;Reports;Customer Statements")
	Delay(2500)
	CloseTopForm()
	// ReportSOPickList
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Order Management;Reports;Sales Order Pick List")
	Delay(2500)
    CloseTopForm()
} 
