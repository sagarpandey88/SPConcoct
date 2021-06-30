$siteUrl = "http://sharepoint/schools/test"
$listName = "Students Picture Library"

[system.reflection.assembly]::LoadWithPartialName("Microsoft.Sharepoint")
$site = New-Object Microsoft.SharePoint.SPSite($siteUrl)
$web = $site.OpenWeb()$list = $web.Lists[$listName]
$fileCollection = $list.RootFolder.Files

$files = get-childItem -Exclude *.ps1

foreach ($file in $files)
{
    $stream = $file.OpenRead()
    $uploaded = $fileCollection.Add($file.Name, $stream, $TRUE)
    "Uploaded " + $file.Name

    if ($stream) {$stream.Dispose()}
}

if ($web) {$web.Dispose()}
if ($site) {$site.Dispose()}