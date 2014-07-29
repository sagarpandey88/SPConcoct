

 #Source Web Url
 $sourceWebUrl = "http://172.20.1.99:5000/CEATEJS/"


 #comma delimited list of List Names to delete
$lists = @("AccountClearanceMaster", "AdminClearanceMaster")



 foreach($list in $lists)
   {

  "Deleting " + $sourceWebUrl + "/lists/" + $list
Get-SPWeb $sourceWebUrl |  Where-Object { $_.Lists.Delete([System.Guid]$_.Lists[$list].ID) }


}