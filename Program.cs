var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SqlConnectionFactory>();
builder.Services.AddSingleton<StoredProcedureExecutor>();

var app = builder.Build();

// Health check
app.MapGet("/", () => "Alnoor is running");

// Database connection test
app.MapGet("/test-db", async (SqlConnectionFactory factory) =>
{
    try
    {
        using var conn = factory.Create();
        await conn.OpenAsync();
        return Results.Ok("DB CONNECTED");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.ToString());
    }
});

// API endpoints
app.MapStaffEndpoints();
app.MapClientEndpoints();
app.MapShiftEndpoints();
app.MapBillingEndpoints();
app.MapPayslipEndpoints();

app.Run();