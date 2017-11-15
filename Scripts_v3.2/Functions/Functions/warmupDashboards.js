//USEUNIT FormLib
//USEUNIT General_Functions

function warmupDashboard(){  

	var company1 = "Epicor Education"
	var plant1 = "Main"
	
	var treeMainPanel1 = setCompanyMainTree(company1,plant1)

	MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;Setup;Business Activity Query")
	CloseTopForm()

	MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
	OpenDashboard("JobStatusPlus")
	CheckWindowMessageModals()
	Delay(2500)
	ClickButton("OK")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
	OpenDashboard("EstimateTrims")
	CheckWindowMessageModals()
	Delay(2500)
	ClickButton("OK")
	CloseTopForm()

	MainMenuTreeViewSelect(treeMainPanel1 + "Executive Analysis;Business Activity Management;General Operations;Dashboard")
	OpenDashboard("PartOnHandStatus")
	CheckWindowMessageModals()
	Delay(2500)
	ClickButton("OK")
	CloseTopForm()

	MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;Menu Maintenance")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Dashboard Maintenance")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Attribute")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "System Setup;Security Maintenance;User Account Security Maintenance")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "Sales Management;Customer Relationship Management;Setup;Customer")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "Production Management;Job Management;Setup;Part")
	CloseTopForm()
	
	MainMenuTreeViewSelect(treeMainPanel1 + "System Management;Upgrade/Mass Regeneration;Updatable BAQ Maintenance")
	CloseTopForm()

} 
