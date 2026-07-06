public static class ShiftEndpoints
{
    public static void MapShiftEndpoints(this WebApplication app)
    {
        app.MapPost("/shift/create", async (Shift shift, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ClientID", shift.ClientID },
                { "@Date", shift.Date },
                { "@StartTime", shift.StartTime },
                { "@EndTime", shift.EndTime },
                { "@SpecialRequest", shift.SpecialRequest },
                { "@Emergency", shift.Emergency }
            };

            await exec.Execute("CreateShiftRequest", parameters);
            return Results.Ok("Shift created");
        });

        app.MapGet("/shift/available/{shiftId}", async (int shiftId, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ShiftID", shiftId }
            };

            var result = await exec.Execute("AutoFindReplacement", parameters);
            return Results.Ok(result);
        });

        app.MapPost("/shift/accept", async (ShiftAssignment assignment, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ShiftID", assignment.ShiftID },
                { "@StaffID", assignment.StaffID }
            };

            var result = await exec.Execute("AssignShift", parameters);
            return Results.Ok(result);
        });

        app.MapPost("/shift/decline", async (ShiftAssignment assignment, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ShiftID", assignment.ShiftID },
                { "@StaffID", assignment.StaffID }
            };

            await exec.Execute("DeclineShift", parameters);
            return Results.Ok("Declined");
        });
    }
}
