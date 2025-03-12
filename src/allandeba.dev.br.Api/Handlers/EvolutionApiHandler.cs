using allandeba.dev.br.Api.Models.Environments;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.EvolutionApi;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.EvolutionApi;
using Deba.EvolutionApi.Interfaces;
using Deba.EvolutionApi.Models.Requests.SendText;
using Microsoft.Extensions.Options;

namespace allandeba.dev.br.Api.Handlers;

public class EvolutionApiHandler(IEvolutionApiService evolutionApiService, IOptions<EvolutionApiOptions> options): IEvolutionApiHandler
{
    private readonly EvolutionApiOptions _options = options.Value;

    public async Task<Response<EvolutionApiResponse?>> SendText(EvolutionApiRequest request)
    {
        try
        {
            var body = new SendTextRequest
            {
                Number = _options.SendToNumber,
                Text = request.Message
            };
            
            var response = await evolutionApiService.SendTextAsync(_options.ApiInstance!, body);
            
            return response.IsSuccess
                ? new Response<EvolutionApiResponse?> { Message = "Mensagem enviada com sucesso" }
                : new Response<EvolutionApiResponse?>(null, 404, response.Message, response.Details);
        }
        catch (Exception ex)
        {
            return new Response<EvolutionApiResponse?>(null, 500, $"Erro não esperado ao enviar a mensagem: {ex.Message}");
        }
    }
}