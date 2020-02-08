<#
this script turns on the developer dashboard
#>

$contentService = [Microsoft.SharePoint.Administration.SPWebService]::ContentService$dashboardSetting = $contentService.DeveloperDashboardSettings$dashboardSetting.DisplayLevel = [Microsoft.SharePoint.Administration.SPDeveloperDashboardLevel]::On$dashboardSetting.Update()
