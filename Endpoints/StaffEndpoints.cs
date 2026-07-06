public static class StaffEndpoints
{
    public static void MapStaffEndpoints(this WebApplication app)
    {
        app.MapPost("/staff/availability", async (Availability availability, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@StaffID", availability.StaffID },
                { "@Date", availability.Date },
                { "@StartTime", availability.StartTime },
                { "@EndTime", availability.EndTime },
                { "@Available", availability.Available },
                { "@EmergencyAvailable", availability.EmergencyAvailable }
            };

            await exec.Execute("AddStaffAvailability", parameters);
            return Results.Ok("Availability added");
        });
    }
}
