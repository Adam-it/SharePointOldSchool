<#
this script add property bag property to target web
#>

if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$property = 'RunCustomTimerJobHere'
$value = 'true'

$web = Get-SPWeb -site [URL]
if ($web.properties[$property] -ne $value)
{	
	Write-Host 'adding custom property to property bag';
	$web.properties.Add($property,$value);
	$web.properties.Update();
	$web.Update();
	Write-Host 'property added';
}
else
{
	Write-Host 'property already exists';
}
$web.Dispose();