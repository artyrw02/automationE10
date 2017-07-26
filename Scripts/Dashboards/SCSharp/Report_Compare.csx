//USEUNIT General_Functions
//USEUNIT Dashboards_Functions
//USEUNIT BAQs_Functions
//USEUNIT Grid_Functions
//USEUNIT Sys_Functions

function Report_Testing(){

//XML["XmlCheckpoint1"]["Check"]("C:\\Users\\Administrator\\Documents\\Reports\\Sales Order Acknowledgment00885.xml");
var pathFileReport = "C:\\EpicorData\\Reports\\epicor\\Sales Order Acknowledgment00145.xml"

  XML["XmlCheckpoint1"]["Check"](pathFileReport);


}

