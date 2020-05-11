﻿using RestSharp;

namespace IqOptionApi.Http
{
    public class ChangeBalanceRequest : IqOptionRequest
    {
        public ChangeBalanceRequest(long balanceId) : base("profile/changebalance", Method.POST)
        {
            AddParameter("balance_id", balanceId, ParameterType.QueryString);
        }
    }
}