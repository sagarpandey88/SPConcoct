#
$webURL = “http://spurl/site/&#8221;
$spSite = [Microsoft.SharePoint.SPSite]($webURL)
$spWeb = $spSite.OpenWeb()
#
# Create a custom list (ID is 100)
$myListGuid = $spWeb.Lists.Add(“CustInfo”, “Customer Information for Northern Projects”, 100)
$myList = $spweb.Lists[$myListGuid]
#
# Update the List Title
$myListObj = $spWeb.Lists["CustInfo"]
$myListObj.Title = “Customer Information”
$myListObj.Update()
#
# Adding fields
$myFieldCustomerName = $myList.Fields.Add(“CustomerName”, “Text”, 0)
$myList.Fields[$myFieldCustomerName].Title = “Customer Name”
$myList.Fields[$myFieldCustomerName].Update()
#
# Adding fields
$myFieldCustomerAvailable = $myList.Fields.Add(“CustomerAvailable”, “Boolean”, 0)
$myList.Fields[$myFieldCustomerAvailable].Title = “Customer Available”
$myList.Fields[$myFieldCustomerAvailable].Update()
#
# Adding fields
$myFieldCustomerDate = $myList.Fields.Add(“CustomerDate”, “DateTime”, 0)
$myList.Fields[$myFieldCustomerDate].Title = “Customer Date”
$myList.Fields[$myFieldCustomerDate].DisplayFormat = “DateOnly”
$myList.Fields[$myFieldCustomerDate].Update()
#
# Setting the Query for the View
$viewQuery = “<OrderBy><FieldRef Name=”"Modified”" Ascending=”"False”" /></OrderBy>”
#
# Adding fields to the view
$viewFields = New-Object System.Collections.Specialized.StringCollection
$viewFields.Add(“Attachments”)
$viewFields.Add(“LinkTitle”)
$viewFields.Add($myFieldCustomerName)
$viewFields.Add($myFieldCustomerDate)
#
# View Name
$viewName = “view_name_here”
#
# Finally – Provisioning the View
$myListView = $myList.Views.Add($viewName, $viewFields, $viewQuery, 100, $True, $False, “HTML”, $False)
#
# You need to Update the View for changes made to the view
# Updating the List is not enough
$myListView.DefaultView = $True
$myListView.Update()
#
$myList.OnQuickLaunch = $True
$myList.Update()
#
$spWeb.Dispose()
$spSite.Dispose()