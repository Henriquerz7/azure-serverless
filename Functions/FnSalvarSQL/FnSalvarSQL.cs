
using System;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static class FnSalvarSQL
{
    [FunctionName("FnSalvarSQL")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        string conteudo = data?.conteudo;

        string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var text = "INSERT INTO Mensagens (Conteudo) VALUES (@Conteudo)";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    cmd.Parameters.AddWithValue("@Conteudo", conteudo);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return new StatusCodeResult(201);
        }
        catch (Exception ex)
        {
            log.LogError($"Erro ao salvar no banco: {ex.Message}");
            return new StatusCodeResult(500);
        }
    }
}
