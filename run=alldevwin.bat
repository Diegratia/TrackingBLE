@echo off
echo Stopping existing dotnet processes...
taskkill /IM dotnet.exe /F

:: Tunggu beberapa detik agar semua proses mati
timeout /t 2 /nobreak >nul

echo Building TrackingBle solution...
dotnet build TrackingBle.sln
if %ERRORLEVEL% NEQ 0 (
    echo Build failed. Exiting...
    exit /b 1
)

echo Starting microservices...
start dotnet run --project src\10.MstBuilding\10.MstBuilding.csproj --no-build
start dotnet run --project src\11.MstDepartment\11.MstDepartment.csproj --no-build
start dotnet run --project src\12.MstDistrict\12.MstDistrict.csproj --no-build
start dotnet run --project src\13.MstFloor\13.MstFloor.csproj --no-build
start dotnet run --project src\14.MstFloorplan\14.MstFloorplan.csproj --no-build
start dotnet run --project src\15.MstIntegration\15.MstIntegration.csproj --no-build
start dotnet run --project src\16.MstMember\16.MstMember.csproj --no-build
start dotnet run --project src\17.MstOrganization\17.MstOrganization.csproj --no-build
start dotnet run --project src\18.TrackingTransaction\18.TrackingTransaction.csproj --no-build
start dotnet run --project src\19.Visitor\19.Visitor.csproj --no-build
start dotnet run --project src\20.VisitorBlacklistArea\20.VisitorBlacklistArea.csproj --no-build
start dotnet run --project src\2.AlarmRecordTracking\2.AlarmRecordTracking.csproj --no-build
start dotnet run --project src\3.FloorplanDevice\3.FloorplanDevice.csproj --no-build
start dotnet run --project src\4.FloorplanMaskedArea\4.FloorplanMaskedArea.csproj --no-build
start dotnet run --project src\5.MstAccessCctv\5.MstAccessCctv.csproj --no-build
start dotnet run --project src\6.MstAccessControl\6.MstAccessControl.csproj --no-build
start dotnet run --project src\7.MstApplication\7.MstApplication.csproj --no-build
start dotnet run --project src\8.MstBleReader\8.MstBleReader.csproj --no-build
start dotnet run --project src\9.MstBrand\9.MstBrand.csproj --no-build

echo All services started. Press any key to exit.
pause
