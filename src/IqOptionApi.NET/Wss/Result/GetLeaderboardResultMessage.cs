using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws.result
{
    class GetLeaderboardResultMessage : WsMessageBase<WsMessageWithSuccessfulResult<Leaderboard>>
    {
    }
}
