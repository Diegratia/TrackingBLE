using Bogus;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using System;
using System.Linq;

namespace TrackingBle.Seeding
{
    public static class DatabaseSeeder
    {
        public static void Seed(TrackingBleDbContext context)
        {
            //MstBrand 
            if (!context.MstBrands.Any())
            {
                var brandFaker = new Faker<MstBrand>()
                    .RuleFor(b => b.Id, f => Guid.NewGuid())
                    .RuleFor(b => b.Name, f => f.Company.CompanyName())
                    .RuleFor(b => b.Tag, f => f.Commerce.Product())
                    .RuleFor(b => b.Status, f => 1);

                var brands = brandFaker.Generate(3);
                context.MstBrands.AddRange(brands);
                context.SaveChanges();
            }

            //MstApplication 
            if (!context.MstApplications.Any())
            {
                var appFaker = new Faker<MstApplication>()
                    .RuleFor(a => a.Id, f => Guid.NewGuid())
                    .RuleFor(a => a.ApplicationName, f => f.Company.CompanyName() + " App")
                    .RuleFor(a => a.OrganizationType, f => f.PickRandom<OrganizationType>())
                    .RuleFor(a => a.OrganizationAddress, f => f.Address.FullAddress())
                    .RuleFor(a => a.ApplicationType, f => f.PickRandom<ApplicationType>())
                    .RuleFor(a => a.ApplicationRegistered, f => f.Date.Past(2))
                    .RuleFor(a => a.ApplicationExpired, f => f.Date.Future(2))
                    .RuleFor(a => a.HostName, f => f.Internet.DomainName())
                    .RuleFor(a => a.HostPhone, f => f.Phone.PhoneNumber())
                    .RuleFor(a => a.HostEmail, f => f.Internet.Email())
                    .RuleFor(a => a.HostAddress, f => f.Address.FullAddress())
                    .RuleFor(a => a.ApplicationCustomName, f => f.Commerce.ProductName())
                    .RuleFor(a => a.ApplicationCustomDomain, f => f.Internet.DomainName())
                    .RuleFor(a => a.ApplicationCustomPort, f => f.Random.Number(1000, 9999).ToString())
                    .RuleFor(a => a.LicenseCode, f => f.Random.AlphaNumeric(10))
                    .RuleFor(a => a.LicenseType, f => f.PickRandom<LicenseType>())
                    .RuleFor(a => a.ApplicationStatus, f => 1);

                var applications = appFaker.Generate(5);
                context.MstApplications.AddRange(applications);
                context.SaveChanges();
            }

            //MstFloor
            if (!context.MstFloors.Any())
            {
                var floorFaker = new Faker<MstFloor>()
                    .RuleFor(f => f.Id, f => Guid.NewGuid())
                    .RuleFor(f => f.BuildingId, f => "BLD-" + f.Random.Number(100, 999))
                    .RuleFor(f => f.Name, f => f.Address.StreetName())
                    .RuleFor(f => f.FloorImage, f => $"https://example.com/floorplans/{f.Random.Word()}.png")
                    .RuleFor(f => f.PixelX, f => f.Random.Long(1280, 1920))
                    .RuleFor(f => f.PixelY, f => f.Random.Long(720, 1080))
                    .RuleFor(f => f.FloorX, f => f.Random.Long(20, 100))
                    .RuleFor(f => f.FloorY, f => f.Random.Long(20, 100))
                    .RuleFor(f => f.MeterPerPx, f => f.Random.Decimal(0.01m, 0.1m))
                    .RuleFor(f => f.EngineFloorId, f => f.Random.Long(10000, 99999))
                    .RuleFor(f => f.CreatedBy, f => "System")
                    .RuleFor(f => f.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(f => f.UpdatedBy, f => "System")
                    .RuleFor(f => f.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(f => f.Status, f => 1);

                var floors = floorFaker.Generate(3);
                context.MstFloors.AddRange(floors);
                context.SaveChanges();
            }

            //MstIntegration 
            if (!context.MstIntegrations.Any())
            {
                var intFaker = new Faker<MstIntegration>()
                    .RuleFor(i => i.Id, f => Guid.NewGuid())
                    .RuleFor(i => i.BrandId, f => context.MstBrands.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(i => i.IntegrationType, f => f.PickRandom<IntegrationType>())
                    .RuleFor(i => i.ApiTypeAuth, f => f.PickRandom<ApiTypeAuth>())
                    .RuleFor(i => i.ApiUrl, f => f.Internet.Url())
                    .RuleFor(i => i.ApiAuthUsername, f => f.Internet.UserName())
                    .RuleFor(i => i.ApiAuthPasswd, f => f.Internet.Password())
                    .RuleFor(i => i.ApiKeyField, f => "Key" + f.Random.Word())
                    .RuleFor(i => i.ApiKeyValue, f => f.Random.AlphaNumeric(20))
                    .RuleFor(i => i.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(i => i.CreatedBy, f => "System")
                    .RuleFor(i => i.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(i => i.UpdatedBy, f => "System")
                    .RuleFor(i => i.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(i => i.Status, f => 1);

                var integrations = intFaker.Generate(5);
                context.MstIntegrations.AddRange(integrations);
                context.SaveChanges();
            }

            //MstOrganization 
            if (!context.MstOrganizations.Any())
            {
                var orgFaker = new Faker<MstOrganization>()
                    .RuleFor(o => o.Id, f => Guid.NewGuid())
                    .RuleFor(o => o.Code, f => "ORG" + f.Random.Number(100, 999))
                    .RuleFor(o => o.Name, f => f.Company.CompanyName())
                    .RuleFor(o => o.OrganizationHost, f => f.Internet.DomainName())
                    .RuleFor(o => o.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(o => o.CreatedBy, f => "System")
                    .RuleFor(o => o.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(o => o.UpdatedBy, f => "System")
                    .RuleFor(o => o.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(o => o.Status, f => 1);

                var orgs = orgFaker.Generate(3);
                context.MstOrganizations.AddRange(orgs);
                context.SaveChanges();
            }

            //MstDepartment 
            if (!context.MstDepartments.Any())
            {
                var deptFaker = new Faker<MstDepartment>()
                    .RuleFor(d => d.Id, f => Guid.NewGuid())
                    .RuleFor(d => d.Code, f => "DEPT" + f.Random.Number(100, 999))
                    .RuleFor(d => d.Name, f => f.Commerce.Department())
                    .RuleFor(d => d.DepartmentHost, f => f.Internet.DomainName())
                    .RuleFor(d => d.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(d => d.CreatedBy, f => "System")
                    .RuleFor(d => d.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(d => d.UpdatedBy, f => "System")
                    .RuleFor(d => d.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(d => d.Status, f => 1);

                var depts = deptFaker.Generate(4);
                context.MstDepartments.AddRange(depts);
                context.SaveChanges();
            }

            //MstDistrict 
            if (!context.MstDistricts.Any())
            {
                var distFaker = new Faker<MstDistrict>()
                    .RuleFor(d => d.Id, f => Guid.NewGuid())
                    .RuleFor(d => d.Code, f => "DIST" + f.Random.Number(100, 999))
                    .RuleFor(d => d.Name, f => f.Address.City())
                    .RuleFor(d => d.DistrictHost, f => f.Internet.DomainName())
                    .RuleFor(d => d.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(d => d.CreatedBy, f => "System")
                    .RuleFor(d => d.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(d => d.UpdatedBy, f => "System")
                    .RuleFor(d => d.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(d => d.Status, f => 1);

                var districts = distFaker.Generate(4);
                context.MstDistricts.AddRange(districts);
                context.SaveChanges();
            }

            //MstBleReader
            if (!context.MstBleReaders.Any())
            {
                var readerFaker = new Faker<MstBleReader>()
                    .RuleFor(r => r.Id, f => Guid.NewGuid())
                    .RuleFor(r => r.BrandId, f => context.MstBrands.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(r => r.Name, f => "Reader " + f.Random.Word())
                    .RuleFor(r => r.Mac, f => f.Internet.Mac())
                    .RuleFor(r => r.Ip, f => f.Internet.Ip())
                    .RuleFor(r => r.LocationX, f => f.Random.Decimal(0, 100))
                    .RuleFor(r => r.LocationY, f => f.Random.Decimal(0, 100))
                    .RuleFor(r => r.LocationPxX, f => f.Random.Long(0, 1920))
                    .RuleFor(r => r.LocationPxY, f => f.Random.Long(0, 1080))
                    .RuleFor(r => r.EngineReaderId, f => "RDR" + f.Random.Number(1000, 9999))
                    .RuleFor(r => r.CreatedBy, f => "System")
                    .RuleFor(r => r.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(r => r.UpdatedBy, f => "System")
                    .RuleFor(r => r.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(r => r.Status, f => 1);

                var readers = readerFaker.Generate(5);
                context.MstBleReaders.AddRange(readers);
                context.SaveChanges();
            }

            //FloorplanMaskedArea
            if (!context.FloorplanMaskedAreas.Any())
            {
                var areaFaker = new Faker<FloorplanMaskedArea>()
                    .RuleFor(a => a.Id, f => Guid.NewGuid())
                    .RuleFor(a => a.FloorplanId, f => context.MstFloors.OrderBy(r => Guid.NewGuid()).First().Id.ToString())
                    .RuleFor(a => a.FloorId, f => context.MstFloors.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(a => a.Name, f => f.Address.City() + " Area")
                    .RuleFor(a => a.AreaShape, f => f.PickRandom("Rectangle", "Circle", "Polygon"))
                    .RuleFor(a => a.ColorArea, f => f.Internet.Color())
                    .RuleFor(a => a.RestrictedStatus, f => f.PickRandom<RestrictedStatus>())
                    .RuleFor(a => a.EngineAreaId, f => "ENG" + f.Random.Number(100, 999))
                    .RuleFor(a => a.WideArea, f => f.Random.Long(50, 200))
                    .RuleFor(a => a.PositionPxX, f => f.Random.Long(0, 1920))
                    .RuleFor(a => a.PositionPxY, f => f.Random.Long(0, 1080))
                    .RuleFor(a => a.CreatedBy, f => "System")
                    .RuleFor(a => a.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(a => a.UpdatedBy, f => "System")
                    .RuleFor(a => a.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(a => a.Status, f => 1);

                var areas = areaFaker.Generate(10);
                context.FloorplanMaskedAreas.AddRange(areas);
                context.SaveChanges();
            }

            //MstAccessCctv 
            if (!context.MstAccessCctvs.Any())
            {
                var cctvFaker = new Faker<MstAccessCctv>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => "CCTV " + f.Random.Word())
                    .RuleFor(c => c.Rtsp, f => $"rtsp://{f.Internet.Ip()}/live")
                    .RuleFor(c => c.IntegrationId, f => context.MstIntegrations.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(c => c.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(c => c.CreatedBy, f => "System")
                    .RuleFor(c => c.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(c => c.UpdatedBy, f => "System")
                    .RuleFor(c => c.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(c => c.Status, f => 1);

                var cctvs = cctvFaker.Generate(5);
                context.MstAccessCctvs.AddRange(cctvs);
                context.SaveChanges();
            }

            //MstAccessControl 
            if (!context.MstAccessControls.Any())
            {
                var ctrlFaker = new Faker<MstAccessControl>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.ControllerBrandId, f => context.MstBrands.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(c => c.Name, f => "Control " + f.Random.Word())
                    .RuleFor(c => c.Type, f => f.PickRandom("Door", "Gate"))
                    .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Channel, f => "CH" + f.Random.Number(1, 10))
                    .RuleFor(c => c.DoorId, f => "DOOR" + f.Random.Number(100, 999))
                    .RuleFor(c => c.Raw, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.IntegrationId, f => context.MstIntegrations.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(c => c.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(c => c.CreatedBy, f => "System")
                    .RuleFor(c => c.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(c => c.UpdatedBy, f => "System")
                    .RuleFor(c => c.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(c => c.Status, f => 1);

                var controls = ctrlFaker.Generate(5);
                context.MstAccessControls.AddRange(controls);
                context.SaveChanges();
            }

            //MstMember 
            if (!context.MstMembers.Any())
            {
                var memberFaker = new Faker<MstMember>()
                    .RuleFor(m => m.Id, f => Guid.NewGuid())
                    .RuleFor(m => m.PersonId, f => "EMP" + f.Random.Number(1000, 9999))
                    .RuleFor(m => m.OrganizationId, f => context.MstOrganizations.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(m => m.DepartmentId, f => context.MstDepartments.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(m => m.DistrictId, f => context.MstDistricts.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(m => m.IdentityId, f => "ID" + f.Random.Number(100, 999))
                    .RuleFor(m => m.CardNumber, f => "CARD" + f.Random.Number(1000, 9999))
                    .RuleFor(m => m.BleCardNumber, f => "BLE" + f.Random.Number(100, 999))
                    .RuleFor(m => m.Name, f => f.Name.FullName())
                    .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(m => m.Email, f => f.Internet.Email())
                    .RuleFor(m => m.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(m => m.Address, f => f.Address.FullAddress())
                    .RuleFor(m => m.FaceImage, f => $"https://example.com/faces/{f.Random.Word()}.jpg")
                    .RuleFor(m => m.UploadFr, f => f.Random.Int(0, 2))
                    .RuleFor(m => m.UploadFrError, f => f.Random.Bool() ? "" : "Upload failed")
                    .RuleFor(m => m.BirthDate, f => f.Date.Past(30, DateTime.UtcNow.AddYears(-18)))
                    .RuleFor(m => m.JoinDate, f => f.Date.Past(2))
                    .RuleFor(m => m.ExitDate, f => f.Date.Future(10))
                    .RuleFor(m => m.HeadMember1, f => f.Name.FullName())
                    .RuleFor(m => m.HeadMember2, f => f.Name.FullName())
                    .RuleFor(m => m.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(m => m.StatusEmployee, f => f.PickRandom<StatusEmployee>())
                    .RuleFor(m => m.CreatedBy, f => "System")
                    .RuleFor(m => m.CreatedAt, f => DateTime.UtcNow)
                    .RuleFor(m => m.UpdatedBy, f => "System")
                    .RuleFor(m => m.UpdatedAt, f => DateTime.UtcNow)
                    .RuleFor(m => m.Status, f => 1);

                var members = memberFaker.Generate(10);
                context.MstMembers.AddRange(members);
                context.SaveChanges();
            }

            //Visitor 
            if (!context.Visitors.Any())
            {
                var visitorFaker = new Faker<Visitor>()
                    .RuleFor(v => v.Id, f => Guid.NewGuid())
                    .RuleFor(v => v.PersonId, f => "VIS" + f.Random.Number(1000, 9999))
                    .RuleFor(v => v.IdentityId, f => "VID" + f.Random.Number(100, 999))
                    .RuleFor(v => v.CardNumber, f => "VCARD" + f.Random.Number(1000, 9999))
                    .RuleFor(v => v.BleCardNumber, f => "VBLE" + f.Random.Number(100, 999))
                    .RuleFor(v => v.Name, f => f.Name.FullName())
                    .RuleFor(v => v.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(v => v.Email, f => f.Internet.Email())
                    .RuleFor(v => v.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(v => v.Address, f => f.Address.FullAddress())
                    .RuleFor(v => v.FaceImage, f => $"https://example.com/faces/{f.Random.Word()}.jpg")
                    .RuleFor(v => v.UploadFr, f => f.Random.Int(0, 2))
                    .RuleFor(v => v.UploadFrError, f => f.Random.Bool() ? "" : "Upload failed")
                    .RuleFor(v => v.ApplicationId, f => context.MstApplications.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(v => v.RegisteredDate, f => f.Date.Past(1))
                    .RuleFor(v => v.VisitorArrival, f => f.Date.Recent(1))
                    .RuleFor(v => v.VisitorEnd, f => f.Date.Future(1))
                    .RuleFor(v => v.PortalKey, f => f.Random.Long(1000, 9999))
                    .RuleFor(v => v.TimestampPreRegistration, f => f.Date.Past(1))
                    .RuleFor(v => v.TimestampCheckedIn, f => f.Date.Recent(1))
                    .RuleFor(v => v.TimestampCheckedOut, f => f.Date.Future(1))
                    .RuleFor(v => v.TimestampDeny, f => f.Date.Past(1))
                    .RuleFor(v => v.TimestampBlocked, f => f.Date.Past(1))
                    .RuleFor(v => v.TimestampUnblocked, f => f.Date.Future(1))
                    .RuleFor(v => v.CheckinBy, f => "System")
                    .RuleFor(v => v.CheckoutBy, f => "System")
                    .RuleFor(v => v.DenyBy, f => "System")
                    .RuleFor(v => v.BlockBy, f => "System")
                    .RuleFor(v => v.UnblockBy, f => "System")
                    .RuleFor(v => v.ReasonDeny, f => f.Lorem.Sentence())
                    .RuleFor(v => v.ReasonBlock, f => f.Lorem.Sentence())
                    .RuleFor(v => v.ReasonUnblock, f => f.Lorem.Sentence())
                    .RuleFor(v => v.Status, f => f.PickRandom<VisitorStatus>());

                var visitors = visitorFaker.Generate(5);
                context.Visitors.AddRange(visitors);
                context.SaveChanges();
            }

            //TrackingTransaction
            if (!context.TrackingTransactions.Any())
            {
                var transFaker = new Faker<TrackingTransaction>()
                    .RuleFor(t => t.Id, f => Guid.NewGuid())
                    .RuleFor(t => t.TransTime, f => f.Date.Recent(1))
                    .RuleFor(t => t.ReaderId, f => context.MstBleReaders.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(t => t.CardId, f => f.Random.Long(1000, 9999))
                    .RuleFor(t => t.FloorplanId, f => context.FloorplanMaskedAreas.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(t => t.CoordinateX, f => f.Random.Decimal(0, 100))
                    .RuleFor(t => t.CoordinateY, f => f.Random.Decimal(0, 100))
                    .RuleFor(t => t.CoordinatePxX, f => f.Random.Long(0, 1920))
                    .RuleFor(t => t.CoordinatePxY, f => f.Random.Long(0, 1080))
                    .RuleFor(t => t.AlarmStatus, f => f.PickRandom<AlarmStatus>())
                    .RuleFor(t => t.Battery, f => f.Random.Long(0, 100));

                var transactions = transFaker.Generate(20);
                context.TrackingTransactions.AddRange(transactions);
                context.SaveChanges();
            }

            //VisitorBlacklistArea 
            if (!context.VisitorBlacklistAreas.Any())
            {
                var blacklistFaker = new Faker<VisitorBlacklistArea>()
                    .RuleFor(v => v.Id, f => Guid.NewGuid())
                    .RuleFor(v => v.FloorplanId, f => context.FloorplanMaskedAreas.OrderBy(r => Guid.NewGuid()).First().Id)
                    .RuleFor(v => v.VisitorId, f => context.Visitors.OrderBy(r => Guid.NewGuid()).First().Id);

                var blacklists = blacklistFaker.Generate(5);
                context.VisitorBlacklistAreas.AddRange(blacklists);
                context.SaveChanges();
            }
        }
    }
}