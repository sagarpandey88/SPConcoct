 #This is the source web that is hosting the lists to move
   $sourceWebUrl = "http://172.20.1.99:5000//"
   
   #This is the destination web, where the lists will be copied to
  $destWebUrl = "http://172.20.1.99:5000"
    
  #Location to store the export file
   $path = "\\Server\Share\"
    
 #comma delimited list of List Names to copy
 $lists = @("AccountClearanceMaster", "AdminClearanceMaster")
   
   
  #Loop through the lists, export the list from the source, and import the list into the destination
 foreach($list in $lists)
   {
      "Exporting " + $sourceWebUrl + "/lists/" + $list
   
          export-spweb $sourceWebUrl -ItemUrl ("lists/" + $list) -IncludeUserSecurity -IncludeVersions All -path ($path + $list + ".cmp") -nologfile
 
     "Exporting complete."
  
    
       "Importing " + $destWebUrl + "/lists/" + $list
    
           import-spweb $destWebUrl -IncludeUserSecurity -path ($path + $list + ".cmp") -nologfile
    
       "Importing Complete"
       "`r`n`r`n"
 }