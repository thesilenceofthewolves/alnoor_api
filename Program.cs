var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SqlConnectionFactory>();
builder.Services.AddSingleton<StoredProcedureExecutor>();

var app = builder.Build();

// TEST ROUTE (critical)
app.MapGet("/", () => "Alnoor is running");

app.MapStaffEndpoints();
app.MapClientEndpoints();
app.MapShiftEndpoints();
app.MapBillingEndpoints();
app.MapPayslipEndpoints();

app.Run();