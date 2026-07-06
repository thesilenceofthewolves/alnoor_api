public static class PayslipEndpoints
{
    public static void MapPayslipEndpoints(this WebApplication app)
    {
        app.MapPost("/payslip/generate", async (Payslip payslip, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@StaffID", payslip.StaffID },
                { "@ShiftID", payslip.ShiftID },
                { "@Rate", payslip.Rate }
            };

            await exec.Execute("GeneratePayslip", parameters);
            return Results.Ok("Payslip generated");
        });
    }
}
