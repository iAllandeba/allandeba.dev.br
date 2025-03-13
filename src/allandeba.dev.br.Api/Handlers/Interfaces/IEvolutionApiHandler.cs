using allandeba.dev.br.Api.Requests.EvolutionApi;
using allandeba.dev.br.Api.Responses.EvolutionApi;
using allandeba.dev.br.Core.Responses;

namespace allandeba.dev.br.Api.Handlers.Interfaces;

public interface IEvolutionApiHandler
{
    Task<Response<EvolutionApiResponse?>> SendText(EvolutionApiRequest request);
}