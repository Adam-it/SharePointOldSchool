<#
this script creates a SPlist with some test items added to it on the target web
this script is a support script to create the needed architecture on target web for GetSpecifiedItemsWebpart
#>

#this function populates list with test items
#please be aware the function allows to populate text and number type columns
function New-ItemsForTestOnList([Parameter(Mandatory=$True)]$spList, $columnsDictionary, $howMuchItemsIShouldAdd = 1000)
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
            
            foreach ($column in $columnsDictionary.GetEnumerator()) 
            {
                if ($spList.Fields.ContainsField($column.key))
                {  
                    if ($column.value -eq [Microsoft.SharePoint.SPFieldType]::Text)
                    {
                        $columnName = $column.key
                        $spItem[$column.key] = "$columnName - $i"
                    }
                    elseif ($column.value -eq [Microsoft.SharePoint.SPFieldType]::Number)
                    {
                        $spItem[$column.key] = "$i"
                    }
                    else
                    {
                        Write-host "column type not supported"
                    }
                }
            }

            $spItem.Update()

			$percent = ($i / $howMuchItemsIShouldAdd) * 100
		}
	}
} 

#this function adds Columns to list
#please be aware the function adds all columns as indexed fields
function New-ColumnsOnSpList([Parameter(Mandatory=$True)]$spList, $columnsDictionary)
{
    if ($spList -ne $null)
    {
        foreach ($column in $columnsDictionary.GetEnumerator()) 
        {
            if (!$spList.Fields.ContainsField($column.key))
            {  
                $spList.Fields.Add($column.key, $column.value, $false)
                $spList.Update()
                $field = $spList.fields[$column.key]
                $field.Indexed = $true
                $field.update()
            }
        }
    }
}

##---------------------------------------------------------------------------------------------------------------------
#main body of script
if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}

$listsToCreate = @('EmployeesList')
$template = 'Custom List'
$web = Get-SPWeb -site [URL]
$spTemplate = $web.ListTemplates[$template]  
$listCollection = $web.Lists
$howMuchItemsIShouldAdd = 5300
$columnsDictionary = @{
    employeeId = [Microsoft.SharePoint.SPFieldType]::Number
    someOtherColumn = [Microsoft.SharePoint.SPFieldType]::Text
    anotherColumn = [Microsoft.SharePoint.SPFieldType]::Text
    }

foreach ($list in $listsToCreate) 
{
    #create list
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

    #add columns to list
    New-ColumnsOnSpList -spList $spList -columnsDictionary $columnsDictionary

    #add test items to list 
	New-ItemsForTestOnList -spList $spList -columnsDictionary $columnsDictionary -howMuchItemsIShouldAdd $howMuchItemsIShouldAdd
}

