
echo "Stopping existing dotnet processes..."
pkill -f dotnet

sleep 2

echo "Building TrackingBle solution..."
dotnet build TrackingBle.sln
if [ $? -eq 0 ]; then
    echo "Build succeeded"
else
    echo "Build failed. Exiting..."
    exit 1
fi

ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/10.MstBuilding/10.MstBuilding.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/11.MstDepartment/11.MstDepartment.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/12.MstDistrict/12.MstDistrict.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/13.MstFloor/13.MstFloor.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/14.MstFloorplan/14.MstFloorplan.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/15.MstIntegration/15.MstIntegration.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/16.MstMember/16.MstMember.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/17.MstOrganization/17.MstOrganization.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/18.TrackingTransaction/18.TrackingTransaction.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/19.Visitor/19.Visitor.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/20.VisitorBlacklistArea/20.VisitorBlacklistArea.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/2.AlarmRecordTracking/2.AlarmRecordTracking.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/3.FloorplanDevice/3.FloorplanDevice.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/4.FloorplanMaskedArea/4.FloorplanMaskedArea.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/5.MstAccessCctv/5.MstAccessCctv.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/6.MstAccessControl/6.MstAccessControl.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/7.MstApplication/7.MstApplication.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/8.MstBleReader/8.MstBleReader.csproj --no-build &
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/9.MstBrand/9.MstBrand.csproj --no-build &

echo "All services started. Press Ctrl+C to stop."
wait