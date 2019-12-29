<#
this script may be helpful to get schema xml of view that was created manually
#>

if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$web = Get-SPWeb -site [URL]
$list = $web.Lists.TryGetList("Element List 1")
if ($list) 
{
    foreach ($view in $list.views) 
    {
        if ($view.Title -eq "CustomView1")
        {
            $view.HtmlSchemaXml
        }
    }
}