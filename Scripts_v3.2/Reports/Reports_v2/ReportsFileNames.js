//USEUNIT Sys_Functions
//USEUNIT ObjectLib

var pathFileReportARInvoice 
var pathFileReportSalesOrder
var pathFileReportQuoteform 
var pathFileReportPurchaseOrder 
var pathFileReportProFormaInv
var pathFileReportJobTraveler
var pathFileReportCustomerStatements
var pathFileReportSOPickList
var pathFileReportPackingSlips

function ReportsFileNames(){  
    
    // var command = "start C:\\EpicorData\\Reports\\epicor"
    var command = "start \\\\TOOLSSANDBOX\\epicor"
    
    OpenCMD(command)

    var explorer = Sys["Process"]("explorer")

    var Items_View = FindObject("*DirectUIHWND*", "Name", "*Items_View*", explorer)

    if (Items_View["Exists"]) {
        Log["Message"]("Object explorer exists and contains " + Items_View["ItemCount"] + " items.")
    } else { 
        Log["Message"]("Object explorer does not exist.")
    }

   /* pathFileReportARInvoice = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*AR*")["Value"]
    pathFileReportSalesOrder = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Sales*")["Value"]
    pathFileReportQuoteform = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Quote*")["Value"]
    pathFileReportPurchaseOrder = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Purchase*")["Value"]
    pathFileReportProFormaInv = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Forma*")["Value"]
    pathFileReportJobTraveler = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Job*")["Value"]
    pathFileReportCustomerStatements = "C:\\EpicorData\\Reports\\epicor\\" + Items_View["UIAObject"]("*Customer*")["Value"]
    pathFileReportSOPickList = "C:\\EpicorData\\Reports\\epicor\\"  + Items_View["UIAObject"]("*SO*")["Value"]
    */
    pathFileReportARInvoice = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*AR*")["Value"]
    pathFileReportSalesOrder = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Sales*")["Value"]
    pathFileReportQuoteform = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Quote*")["Value"]
    pathFileReportPurchaseOrder = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Purchase*")["Value"]
    pathFileReportProFormaInv = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Forma*")["Value"]
    pathFileReportJobTraveler = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Job*")["Value"]
    pathFileReportCustomerStatements = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Customer*")["Value"]
    pathFileReportSOPickList = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*SO*")["Value"]
    pathFileReportPackingSlips = "\\\\TOOLSSANDBOX\\epicor\\" + Items_View["UIAObject"]("*Packing*")["Value"]

    Log["Message"](pathFileReportARInvoice)
    Log["Message"](pathFileReportSalesOrder)
    Log["Message"](pathFileReportQuoteform)
    Log["Message"](pathFileReportPurchaseOrder)
    Log["Message"](pathFileReportProFormaInv)
    Log["Message"](pathFileReportJobTraveler)
    Log["Message"](pathFileReportCustomerStatements)
    Log["Message"](pathFileReportSOPickList)
    Log["Message"](pathFileReportPackingSlips)

    explorer["Close"]()

    CloseCMD()
} 
    
    
