
using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

public static class FnLerFila
{
    private static readonly HttpClient client = new HttpClient();

    [FunctionName("FnLerFila")]
    public static async void Run(
        [QueueTrigger("mensagens", Connection = "AzureWebJobsStorage")] string mensagem,
        ILogger log)
    {
        log.LogInformation($"Mensagem recebida: {mensagem}");

        var response = await client.PostAsync(
            Environment.GetEnvironmentVariable("SalvarSqlEndpoint"),
            new StringContent(mensagem));

        if (response.IsSuccessStatusCode)
        {
            log.LogInformation("Mensagem processada e salva no banco.");
        }
        else
        {
            log.LogError($"Erro ao salvar a mensagem no banco: {response.StatusCode}");
            throw new Exception("Erro ao processar a mensagem.");
        }
    }
}
