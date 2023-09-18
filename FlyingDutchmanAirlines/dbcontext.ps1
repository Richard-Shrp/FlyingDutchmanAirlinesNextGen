$connectstring = "Server=tcp:codelikeacsharppro.database.windows.net,1433;
Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;
User Id=dev;Password=FlyingDutchmanAirlines1972!;
MultipleActiveResultSets=False;Encrypt=True;
TrustServerCertificate=False;Connection Timeout=30;"
	
$dbdriver = 'Microsoft.EntityFrameworkCore.SqlServer'
#$flags = "-–context-dir DatabaseLayer –-output-dir DatabaseLayer/Models"

$conn = @(
	$connectstring
	$dbdriver
)
dotnet ef dbcontext scaffold $conn --context-dir DatabaseLayer -o DatabaseLayer/Models