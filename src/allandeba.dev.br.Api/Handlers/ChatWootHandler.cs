using allandeba.dev.br.Api.Models.Environments;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.ChatWoot;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.ChatWoot;
using Deba.EvolutionApi.Interfaces;
using Deba.EvolutionApi.Models.Requests.SendText;
using Microsoft.Extensions.Options;

namespace allandeba.dev.br.Api.Handlers;

public class ChatWootHandler(IEvolutionApiService evolutionApiService, IOptions<EvolutionApiOptions> options): IChatWootHandler
{
    private readonly EvolutionApiOptions _options = options.Value;

    public async Task<Response<ChatWootResponse?>> Notify(ChatWootRequest request)
    {
        try
        {
            var body = new SendTextRequest
            {
                Number = _options.SendToNumber,
                Text = $"Oba, chegou uma nova mensagem no website. Acesse e responda: {Api.ApiConfiguration.ChatWootUrl}"
            };
            
            var response = await evolutionApiService.SendTextAsync(_options.ApiInstance!, body);
            
            return response.IsSuccess
                ? new Response<ChatWootResponse?> { Message = "Mensagem enviada com sucesso" }
                : new Response<ChatWootResponse?>(null, 404, response.Message, response.Details);
        }
        catch (Exception ex)
        {
            return new Response<ChatWootResponse?>(null, 500, $"Erro não esperado ao enviar a mensagem: {ex.Message}");
        }
    }
}