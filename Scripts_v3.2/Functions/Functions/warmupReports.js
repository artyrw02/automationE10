//USEUNIT FormLib
//USEUNIT General_Functions

function warmupReports(){  
	// ReportARInvoice
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print AR Invoices")
	CloseTopForm()

	// ReportJobTraveler
	MainMenuTreeViewSelect("Epicor USA;Chicago;Production Management;Job Management;Reports;Job Traveler")
	CloseTopForm()
	
	// ReportSalesOrder | ReportProFormaInv
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Order Entry")
	CloseTopForm()
	
	// ReportPurchaseOrder
	MainMenuTreeViewSelect("Epicor USA;Chicago;Material Management;Purchase Management;General Operations;Purchase Order Entry")
	CloseTopForm()
	
	// ReportQuoteform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Customer Relationship Management;General Operations;Opportunity / Quote Entry")
	CloseTopForm()
	
	// ReportAPPaymentform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Cash Management;General Operations;Payment Entry")
	CloseTopForm()
	
	// ReportPrintPackingform
	MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Demand Management;Reports;Mass Print Packing Slips")
	CloseTopForm()
	
	// ReportCustomerStatements
	MainMenuTreeViewSelect("Epicor USA;Chicago;System Management;Reporting;Report Style")
	CloseTopForm()
	
	MainMenuTreeViewSelect("Epicor USA;Chicago;Financial Management;Accounts Receivable;Reports;Customer Statements")
	CloseTopForm()
	// ReportSOPickList
    MainMenuTreeViewSelect("Epicor USA;Chicago;Sales Management;Order Management;Reports;Sales Order Pick List")
    CloseTopForm()
} 
