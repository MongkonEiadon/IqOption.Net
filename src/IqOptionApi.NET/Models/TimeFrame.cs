namespace IqOptionApi.Models
{
    public enum TimeFrame : int
    {
        Sec5 = 5,
        Sec10 = 10,
        Sec15 = 15,
        Sec30 = 30,

        Min1 = 60,
        Min2 = 120,
        Min5 = 300,
        Min10 = 600,
        Min15 = 900,
        Min30 = 1800,

        Hour1 = 3600,
        Hour2 = 7200,
        Hour4 = 14400,

        Day1 = 86400,

        Week1 = 604800,

        Month1 = 2592000

    }
}