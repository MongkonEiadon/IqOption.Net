using iqoptionapi.models;
using IqOptionApi;
using IqOptionApi.Models;

namespace iqoptionapi.builders
{
    public class PlaceOrderBuilder
    {
        public static PlaceOrder BuildPlaceOrderMarket(OrderDirection direction, Balance balance, decimal amount, ws.model.Instrument instrument)
        {
            return new PlaceOrder()
            {
                user_balance_id = balance.Id,
                client_platform_id = 9, // ?

                instrument_id = instrument.Name,
                instrument_type = "forex",
                side = direction == OrderDirection.Call ? "buy" : "sell",
                type = "market",
                amount = amount.ToString(),
                leverage = 30,
                limit_price = 0m,
                stop_price = 0m,
                use_token_for_commission = false,
                auto_margin_call = false,
                use_trail_stop = false,
                take_profit_value = 2.0f,
                take_profit_kind = "percent",
                stop_lose_value = 50.0f,
                stop_lose_kind = "percent"
            };
        }


        public static PlaceOrder BuildPlaceOrderCrypto(ActivePair pair, OrderDirection direction, Balance balance, decimal amount, ws.model.Instrument instrument)
        {
            return new PlaceOrder()
            {
                user_balance_id = balance.Id,
                client_platform_id = 9, // ?

                instrument_id = "BTCUSD",
                instrument_type = "crypto",
                side = direction == OrderDirection.Call ? "buy" : "sell",
                type = "market",
                amount = amount.ToString(),
                leverage = 2,
                limit_price = 0m,
                stop_price = 0m,
                use_token_for_commission = false,
                auto_margin_call = false,
                use_trail_stop = false,
                take_profit_value = 100.0f,
                take_profit_kind = "percent",
                stop_lose_value = 50.0f,
                stop_lose_kind = "percent"
            };
        }
    }
}
