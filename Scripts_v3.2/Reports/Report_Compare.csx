//Script for report compare
//Variables are defined with the full path of its location
//A function for each report is created and called on TC routine 

var pathFileReportARInvoice = "C:\\EpicorData\\Reports\\manager\\AR Invoice Form29568.xml"
var pathFileReportSalesOrder = "C:\\EpicorData\\Reports\\manager\\Sales Order Acknowledgment29571.xml"
var pathFileReportQuoteform = "C:\\EpicorData\\Reports\\manager\\Quote Form29572.xml"
var pathFileReportPurchaseOrder = "C:\\EpicorData\\Reports\\manager\\Purchase Order29570.xml"
var pathFileReportProFormaInv = "C:\\EpicorData\\Reports\\manager\\Pro-Forma Invoice29569.xml"
var pathFileReportJobTraveler = "C:\\EpicorData\\Reports\\manager\\Job Traveler29567.xml"
var pathFileReportCustomerStatements = "C:\\EpicorData\\Reports\\manager\\Customer Statement29573.xml"
var pathFileReportSOPickList = "C:\\EpicorData\\Reports\\manager\\SO Pick list29574.xml"

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
	XML["XMLJobTraveler"]["Check"](pathFileReportJobTraveler)
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
	XML["XmlARInvoice"]["Check"](pathFileReportSOPickList)
}

