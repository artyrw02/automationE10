//Script for report compare
//Variables are defined with the full path of its location
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
  Log["Message"]("Starting Report comparing")
}

function ReportSalesOrder(pathFileReportSalesOrder){
	XML["XmlARInvoice"]["Check"](pathFileReportSalesOrder)
}

function ReportQuoteform(pathFileReportQuoteform){
	XML["XMLJobTraveler"]["Check"](pathFileReportQuoteform)
}

function ReportPurchaseOrder(pathFileReportPurchaseOrder){
	XML["XmlProFormaInvoice"]["Check"](pathFileReportPurchaseOrder)
}

function ReportProFormaInv(pathFileReportProFormaInv){
	XML["XmlPurchaseOrder"]["Check"](pathFileReportProFormaInv)
}

function ReportJobTraveler(pathFileReportJobTraveler){
	XML["XmlQuoteForm"]["Check"](pathFileReportJobTraveler)
}

function ReportARInvoice(pathFileReportARInvoice){
	XML["XmlSalesOrderReport"]["Check"](pathFileReportARInvoice)
}