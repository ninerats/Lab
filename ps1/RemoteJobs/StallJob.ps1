#
# StallJob.ps1
#
[CmdletBinding()]
Param([int]$Seconds)
Write-Information "info"
Write-Output "Stall job starting, will sleep for $seconds seconds."
for ($i=1; $i -le $Seconds;$i++)
{
	Write-Verbose "Increment $i of $seconds."
	sleep -Seconds 1
}
Write-Output "Stall job complete."