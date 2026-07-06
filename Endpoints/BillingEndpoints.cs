public static class BillingEndpoints
{
    public static void MapBillingEndpoints(this WebApplication app)
    {
        app.MapPost("/billing/generate", async (Billing bill, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ClientID", bill.ClientID },
                { "@ShiftID", bill.ShiftID },
                { "@Rate", bill.Rate }
            };

            await exec.Execute("GenerateBilling", parameters);
            return Results.Ok("Billing generated");
        });
    }
}
