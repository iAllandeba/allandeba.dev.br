using allandeba.dev.br.Core.Requests.EvolutionApi;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.EvolutionApi;

namespace allandeba.dev.br.Core.Handlers;

public interface IEvolutionApiHandler
{
    Task<Response<EvolutionApiResponse?>> SendText(EvolutionApiRequest request);
}