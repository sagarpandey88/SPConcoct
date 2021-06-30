$farm = Get-SPFarm
$file = $farm.Solutions.Item("agess_leavnew.wsp").SolutionFile
$file.SaveAs("D:\WSP\agess_leavnew.wsp")