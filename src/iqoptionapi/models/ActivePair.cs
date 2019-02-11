using System.Runtime.Serialization;

namespace IqOptionApi.Models {
    public enum ActivePair {
        EURUSD = 1,
        EURGBP = 2,
        GBPJPY = 3,
        EURJPY = 4,
        GBPUSD = 5,
        USDJPY = 6,
        AUDCAD = 7,
        NZDUSD = 8,
        EURRUB = 9,
        USDRUB = 10,

        COMMBK = 13,
        DAIM = 14,
        DBFRA = 15,
        EOAN = 16,

        BPLON = 23,

        GAZPROM = 27,
        ROSNEFT = 28,
        SBERS = 29,

        AMAZON = 31,
        APPLE = 32,
        BAIDU = 33,
        CISCO = 34,
        FACEBOOK = 35,
        GOOGLE = 36,
        INTEL = 37,
        MSFT = 38,

        YAHOO = 40,
        AIG = 41,
        BOA = 42,

        CITI = 45,
        COKE = 46,

        GM = 49,
        GS = 50,
        JPM = 51,
        MCDON = 52,
        MORSTAN = 53,
        NIKE = 54,

        VERIZON = 56,
        WMART = 57,

        DAX30 = 66,
        DJIA = 67,
        FTSE = 68,
        NSDQ = 69,
        NK = 70,
        SP = 71,
        USDCHF = 72,
        BTCX = 73,
        XAUUSD = 74,
        XAGUSD = 75,

        [EnumMember(Value = "EURUSD-OTC")] EURUSD_OTC = 76,
        [EnumMember(Value = "EURGBP-OTC")] EURGBP_OTC = 77,
        [EnumMember(Value = "USDCHF-OTC")] USDCHF_OTC = 78,
        [EnumMember(Value = "EURJPY-OTC")] EURJPY_OTC = 79,
        [EnumMember(Value = "NZDUSD-OTC")] NZDUSD_OTC = 80,
        [EnumMember(Value = "GBPUSD-OTC")] GBPUSD_OTC = 81,
        [EnumMember(Value = "EURRUB-OTC")] EURRUB_OTC = 82,
        [EnumMember(Value = "USDRUB-OTC")] USDRUB_OTC = 83,
        [EnumMember(Value = "GBPJPY-OTC")] GBPJPY_OTC = 84,
        [EnumMember(Value = "USDJPY-OTC")] USDJPY_OTC = 85,
        [EnumMember(Value = "AUDCAD-OTC")] AUDCAD_OTC = 86,
        ALIBABA = 87,

        YANDEX = 95,

        PAN = 97,

        AUDUSD = 99,
        USDCAD = 100,
        AUDJPY = 101,
        GBPCAD = 102,
        GBPCHF = 103,
        GBPAUD = 104,
        EURCAD = 105,
        CHFJPY = 106,
        CADCHF = 107,
        EURAUD = 108,

        BMW = 110,
        LUFTHANSA = 111,

        TWITTER = 113,

        FERRARI = 133,

        SMI_INDEX = 166,
        TESLA = 167,
        USDNOK = 168,
        SSE_INDEX = 169,
        HANG_SENG = 170,

        SPASX200 = 208,
        TOPIX500 = 209,
        DX = 210,

        EURNZD = 212,
        SIN_FAKE = 213,

        BRENT_OIL_JUL_16 = 215,

        NTDOY = 218,
        USDSEK = 219,
        USDTRY = 220
    }
}