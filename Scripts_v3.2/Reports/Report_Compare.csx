//Script for report compare
//Variables are defined with the full path of its location
//A function for each report is created and called on TC routine 

// var pathFileReportARInvoice = "C:\\EpicorData\\Reports\\epicor\\AR Invoice Form29581.xml"
// var pathFileReportSalesOrder = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment29584.xml"
// var pathFileReportQuoteform = "C:\\EpicorData\\Reports\\epicor\\Quote Form29585.xml"
// var pathFileReportPurchaseOrder = "C:\\EpicorData\\Reports\\epicor\\Purchase Order29583.xml"
// var pathFileReportProFormaInv = "C:\\EpicorData\\Reports\\epicor\\Pro-Forma Invoice29582.xml"
// var pathFileReportJobTraveler = "C:\\EpicorData\\Reports\\epicor\\Job Traveler29580.xml"
// var pathFileReportCustomerStatements = "C:\\EpicorData\\Reports\\epicor\\Customer Statement29587.xml"
// var pathFileReportSOPickList = "C:\\EpicorData\\Reports\\epicor\\SO Pick list29588.xml"

// var pathFileReportARInvoice = "C:\\EpicorData\\TenOne\\Reports\\epicor\\AR Invoice Form29561.xml"
// var pathFileReportSalesOrder = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Sales Order Acknowledgment00892.xml"
// var pathFileReportQuoteform = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Quote Form00893.xml"
// var pathFileReportPurchaseOrder = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Purchase Order00891.xml"
// var pathFileReportProFormaInv = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Pro-Forma Invoice00890.xml"
// var pathFileReportJobTraveler = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Job Traveler00889.xml"
// var pathFileReportCustomerStatements = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Customer Statement29587.xml"
// var pathFileReportSOPickList = "C:\\EpicorData\\TenOne\\Reports\\epicor\\SO Pick list29588.xml"

function Report_Testing(){
	//XML["XmlCheckpoint1"]["Check"]("C:\\Users\\Administrator\\Documents\\Reports\\Sales Order Acknowledgment00885.xml");
 	// XML["XmlCheckpoint1"]["Check"](pathFileReport);
  Log["Message"]("Starting Report comparing")
}

function ReportSalesOrder(){
	XML["XmlSalesOrder"]["Check"](pathFileReportSalesOrder)
}

function ReportQuoteform(){
	XML["XmlQuoteForm"]["Check"](pathFileReportQuoteform)
}

function ReportPurchaseOrder(){
	XML["XmlPurchaseOrder"]["Check"](pathFileReportPurchaseOrder)
}

function ReportProFormaInv(){
	XML["XmlProFormaInvoice"]["Check"](pathFileReportProFormaInv)
}

function ReportJobTraveler(){
	XML["XmlJobTraveler"]["Check"](pathFileReportJobTraveler)
}

function ReportARInvoice(){
	XML["XmlARInvoice"]["Check"](pathFileReportARInvoice)
}

//--

// function ReportPackingForm(){
// 	XML["XmlARInvoice"]["Check"](pathFileReportARInvoice)

// 	// EERRORES
// }
// function ReportAPPayment(){
// 	XML["XmlARInvoice"]["Check"](pathFileReportARInvoice)
// }
function ReportCustomerStatements(){
	XML["XmlCustomerStatements"]["Check"](pathFileReportCustomerStatements)
}

function ReportSOPickList(){
	XML["XmlSOPickList"]["Check"](pathFileReportSOPickList)
}

