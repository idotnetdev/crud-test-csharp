dotnet build
dotnet ef --startup-project ../Mc2.CrudTest.Presentation/Server database update --context Data.Context.ApplicationDbContext
pause