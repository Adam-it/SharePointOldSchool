<#
this script creates a SPlist with some test items added to it on the target web
#>

function New-ItemsForTestOnList([Parameter(Mandatory=$True)]$spList, $howMuchItemsIShouldAdd = 1000)
{
	if ($spList -ne $null)
	{
		$percent = 0
		$listNameTitle = $spList.Title
		for ($i = 1; $i -le $howMuchItemsIShouldAdd; $i++) 
		{
			Write-Progress -Activity "Adding Items Progress" -Status "Added $i out of $howMuchItemsIShouldAdd - $Percent% Complete:" -PercentComplete $Percent;
			
			$spItem = $spList.Items.Add()
			$spItem["Title"] = "$listNameTitle - Item$i"
			$spItem.Update()

			$percent = ($i / $howMuchItemsIShouldAdd) * 100
		}
	}
} 

if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$listsToCreate = @('ListA')
$template = 'Custom List'
$web = Get-SPWeb -site [URL]
$spTemplate = $web.ListTemplates[$template]  
$listCollection = $web.Lists

foreach ($list in $listsToCreate) 
{
	$spList = $listCollection.TryGetList($list)	
    if ($spList -eq $null)
	{
		$listCollection.Add($list, $list, $spTemplate)  
		$spList = $listCollection.TryGetList($list)	
		Write-Host "$list created"
	}
	else
	{
		Write-Host "$list already exists"
	}
	New-ItemsForTestOnList -spList $spList
}

