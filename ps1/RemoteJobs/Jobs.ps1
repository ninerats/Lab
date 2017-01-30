#
# Jobs.ps1
#


function JobExample2
{
	$cmd = {
		if($_.message -like "*bios*") 
		{
			$out=$out + $_.Message
		}
		$I = $I+1;
		Write-Progress -Activity "Searching Events" -Status "Progress:" -PercentComplete ($I/$Events.count*100)
	}
	$Events = Get-EventLog -logname system
	$Events | foreach-object -begin {clear-host;$I=0;$out=""}  -process $cmd  -end {$out}
}

