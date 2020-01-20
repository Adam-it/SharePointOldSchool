<#
this script creates a SPlist on the target web
#>

if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$listsToCreate = @('ListWithEventReceiver','LogList','SomeOtherListForCheck')
$template = 'Custom List'
$web = Get-SPWeb -site [URL]
$spTemplate = $web.ListTemplates[$template]  
$listCollection = $web.Lists

foreach ($list in $listsToCreate) {
   if($listCollection.TryGetList($list) -eq $null)
    {
        $listCollection.Add($list, $list, $spTemplate)  
        Write-Host "$list created"
    }
    else
    {
        Write-Host "$list already exists"
    }
}