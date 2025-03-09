using allandeba.dev.br.Core.Requests.ChatWoot;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.ChatWoot;

namespace allandeba.dev.br.Core.Handlers;

public interface IChatWootHandler
{
    Task<Response<ChatWootResponse?>> Notify(ChatWootRequest request);
}