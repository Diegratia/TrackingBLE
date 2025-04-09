
Write-Host "Stopping existing dotnet processes..." -ForegroundColor Yellow
Stop-Process -Name "dotnet" -Force -ErrorAction SilentlyContinue

Start-Sleep -Seconds 2

$services = @(
    "src\10.MstBuilding\10.MstBuilding.csproj",
    "src\1.Auth\1.Auth.csproj",
    "src\11.MstDepartment\11.MstDepartment.csproj",
    "src\12.MstDistrict\12.MstDistrict.csproj",
    "src\13.MstFloor\13.MstFloor.csproj",
    "src\14.MstFloorplan\14.MstFloorplan.csproj",
    "src\15.MstIntegration\15.MstIntegration.csproj",
    "src\16.MstMember\16.MstMember.csproj",
    "src\17.MstOrganization\17.MstOrganization.csproj",
    "src\18.TrackingTransaction\18.TrackingTransaction.csproj",
    "src\19.Visitor\19.Visitor.csproj",
    "src\20.VisitorBlacklistArea\20.VisitorBlacklistArea.csproj",
    "src\2.AlarmRecordTracking\2.AlarmRecordTracking.csproj",
    "src\3.FloorplanDevice\3.FloorplanDevice.csproj",
    "src\4.FloorplanMaskedArea\4.FloorplanMaskedArea.csproj",
    "src\5.MstAccessCctv\5.MstAccessCctv.csproj",
    "src\6.MstAccessControl\6.MstAccessControl.csproj",
    "src\7.MstApplication\7.MstApplication.csproj",
    "src\8.MstBleReader\8.MstBleReader.csproj",
    "src\9.MstBrand\9.MstBrand.csproj"
)

Write-Host "Cleaning TrackingBle solution..." -ForegroundColor Yellow
dotnet clean TrackingBle.sln
if ($LASTEXITCODE -ne 0) {
    Write-Host "Clean failed. Exiting..." -ForegroundColor Red
    exit 1
}

Write-Host "Restoring dependencies for TrackingBle solution..." -ForegroundColor Yellow
dotnet restore TrackingBle.sln
if ($LASTEXITCODE -ne 0) {
    Write-Host "Restore failed. Exiting..." -ForegroundColor Red
    exit 1
}

Write-Host "Building TrackingBle solution..." -ForegroundColor Yellow
dotnet build TrackingBle.sln --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed. Exiting..." -ForegroundColor Red
    exit 1
}

Write-Host "Clean, restore, and build completed successfully." -ForegroundColor Green

Write-Host "Press Enter to exit..." -ForegroundColor Yellow
$null = Read-Host