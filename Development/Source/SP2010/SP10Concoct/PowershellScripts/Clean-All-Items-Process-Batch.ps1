function QuickCleanAllListItems([string]$SiteURL, [string]$ListName)
{
   [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SharePoint") > $null
   $site = new-object Microsoft.SharePoint.SPSite($SiteURL)
   Write-Host "SiteURL", $SiteURL

   $sw = New-Object System.Diagnostics.StopWatch
   $sw.Start()

   $web = $site.OpenWeb()   
   $myList = $web.Lists[$ListName]
   Write-Host "Items Number before delete: ", $myList.Items.Count

    $guid = $myList.ID
    $strGuid = $guid.ToString()

   $strPost = "<?xml version=""1.0"" encoding=""UTF-8""?><ows:Batch OnError='Return'>"
   foreach($item in $myList.Items)
   {
      $strPost += "<Method><SetList Scope=""Request"">"+ $strGuid +"</SetList>"
      $strPost += "<SetVar Name=""ID"">"+ $item.ID +"</SetVar><SetVar Name=""Cmd"">Delete</SetVar>"
      $strPost += "</Method>"
   }
   $strPost += "</ows:Batch>"

#   Write-Host "Batch: " $strPost
   $strProcessBatch = $web.ProcessBatchData($strPost)
   
   Write-Host "Result: " $strProcessBatch 

   $sw.Stop()
   Write-Host "Items total after delete: ", $myList.Items.Count
   write-host "$y Items add in " $sw.Elapsed.ToString()
   
   $web.Dispose()
   $site.Dispose()
}

QuickCleanAllListItems "http://myWebApplication/sites/mySiteCollection/" "myList"
