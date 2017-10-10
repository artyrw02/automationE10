//Script for report compare
//Variables are defined with the full path of its location
//A function for each report is created and called on TC routine 

// var pathFileReportSalesOrder = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00965.xml"
// var pathFileReportQuoteform = "C:\\EpicorData\\Reports\\epicor\\Quote Form00966.xml"
// var pathFileReportPurchaseOrder = "C:\\EpicorData\\Reports\\epicor\\Purchase Order00967.xml"
// var pathFileReportProFormaInv = "C:\\EpicorData\\Reports\\epicor\\Pro-Forma Invoice00968.xml"
// var pathFileReportJobTraveler = "C:\\EpicorData\\Reports\\epicor\\Job Traveler00969.xml"

var pathFileReportARInvoice = "C:\\EpicorData\\TenOne\\Reports\\epicor\\AR Invoice Form29561.xml"
var pathFileReportSalesOrder = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Sales Order Acknowledgment00892.xml"
var pathFileReportQuoteform = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Quote Form00893.xml"
var pathFileReportPurchaseOrder = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Purchase Order00891.xml"
var pathFileReportProFormaInv = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Pro-Forma Invoice00890.xml"
var pathFileReportJobTraveler = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Job Traveler00889.xml"
var pathFileReportCustomerStatements = "C:\\EpicorData\\TenOne\\Reports\\epicor\\Customer Statement29587.xml"
var pathFileReportSOPickList = "C:\\EpicorData\\TenOne\\Reports\\epicor\\SO Pick list29588.xml"

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
	XML["XmlCustomerStatements"]["Check"](pathFileReportARInvoice)
}

function ReportSOPickList(){
	XML["XmlARInvoice"]["Check"](pathFileReportARInvoice)
}


customer and so