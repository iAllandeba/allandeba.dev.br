using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;

namespace allandeba.dev.br.Core.Handlers;

public interface IGithubHandler
{
    Task<Response<GithubProjectResponse?>> GetFavoriteProjectsAsync(GetGithubProjectRequest request);
}