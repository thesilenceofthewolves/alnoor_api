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
app.MapGet("/test-db", async (SqlConnectionFactory factory) =>
{
    using var conn = factory.Create();
    await conn.OpenAsync();

    return Results.Ok("Database connection SUCCESS");
});

app.Run();