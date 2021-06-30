# "Enter the site URL instead http://serverurl" 
$SITEURL = "http://serverurl"

$site = new-object Microsoft.SharePoint.SPSite ( $SITEURL ) 
$web = $site.OpenWeb() 
"Web is : " + $web.Title

# Enter name of the List below insted of LIST NAME
$oList = $web.Lists["LIST NAME"];

"List is :" + $oList.Title + " with item count " + $oList.ItemCount

$collListItems = $oList.Items; 
$count = $collListItems.Count - 1

for($intIndex = $count; $intIndex -gt -1; $intIndex--) 
{ 
        "Deleting record: " + $intIndex 
        $collListItems.Delete($intIndex); 
}