<#
this script creates a SPlist on the target web
#>

if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$template = 'Custom List'
$listName = 'ListWithEventReceiver'
$logListName = 'LogList'

$web = Get-SPWeb -site [URL]
$spTemplate = $web.ListTemplates[$template]  
$listCollection = $web.Lists
if($listCollection.TryGetList($listName) -eq $null)
{
    $listCollection.Add($listName, $listName, $spTemplate)  
    Write-Host "$listName created"
}
else
{
    Write-Host "$listName already exists"
}

if($listCollection.TryGetList($logListName) -eq $null)
{
    $listCollection.Add($logListName, $logListName, $spTemplate)  
    Write-Host "$logListName created"
}
else
{
    Write-Host "$logListName already exists"
}