#
# Progress.ps1
#
function Example1
{
	for ($i=1;$i -le 20; $i++)
	{
		sleep -Milliseconds 200
		Write-Progress -Activity Stuff -PercentComplete ($i*5) -Status "step $i of 10" -Id 4
	}
	Write-Progress -Activity Stuff -Completed -Status "Complete."
}


function Example2
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

function Nested
{
	 
	for($I = 1; $I -lt 101; $I++ )
	{
		Write-Progress -Id 1 -Activity Updating -Status 'Progress->' -PercentComplete $I -CurrentOperation OuterLoop
		for($j = 1; $j -lt 101; $j++ )
		{
			Write-Progress -Id 2 -Activity Updating -Status 'Progress' -PercentComplete $j -CurrentOperation InnerLoop -ParentId 1
		} 
	}
}

function NestedSleep
{
	 
	for($I = 1; $I -le 10; $I++ )
	{
		Write-Progress -Id 1 -Activity Updating -Status 'Progress->' -PercentComplete ($I*10) -CurrentOperation OuterLoop
		for($j = 1; $j -le 10; $j++ )
		{
			sleep 1
			Write-Progress -Id 2 -Activity Updating -Status 'Progress' -PercentComplete ($j*10) -CurrentOperation InnerLoop -ParentId 1
		} 
	}
}