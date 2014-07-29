# Function:          Clear-All-RecycleBin
# Description:       Clear all Recycle Bin for a site collection
# Parameters:       	SiteCollectionURL : URL for Site Collection
function Clear-All-RecycleBin([string]$SiteCollectionURL)
{
	[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SharePoint") > $null
	$site = new-object Microsoft.SharePoint.SPSite($SiteCollectionURL)
	Write-Host "SiteCollectionURL", $SiteCollectionURL

	$SitecollectionRecycleBin = $site.RecycleBin
	Write-Host "SitecollectionRecycleBin Number", $SitecollectionRecycleBin.Count

	for ($x = $SitecollectionRecycleBin.Count ; $x -gt 0 ; $x--) 
	{
		$Item = $SitecollectionRecycleBin.Item($x-1)
		$SitecollectionRecycleBin.Delete($Item.ID)
	}
	Write-Host "SitecollectionRecycleBin Number", $SitecollectionRecycleBin.Count
}

Clear-All-RecycleBin "http://myWebApplication/sites/mySiteCollection/"

