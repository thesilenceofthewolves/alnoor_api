using System.Data;
using Microsoft.AspNetCore.Builder;

public static class ClientEndpoints
{
    public static void MapClientEndpoints(this WebApplication app)
    {
        // Add a new client
        app.MapPost("/client/add", async (Client client, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Name", client.Name },
                { "@Address", client.Address },
                { "@Postcode", client.Postcode },
                { "@SpecialRequirements", client.SpecialRequirements },
                { "@PreferredStaff", client.PreferredStaff },
                { "@Notes", client.Notes }
            };

            await exec.Execute("AddClient", parameters);
            return Results.Ok("Client added");
        });

        // Get client details
        app.MapGet("/client/{clientId}", async (int clientId, StoredProcedureExecutor exec) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "@ClientID", clientId }
            };

            var result = await exec.Execute("GetClient", parameters);
            return Results.Ok(result);
        });
    }
}
