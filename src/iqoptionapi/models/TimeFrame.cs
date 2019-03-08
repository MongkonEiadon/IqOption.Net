namespace IqOptionApi.Models {
    public enum TimeFrame {
        Min1 = 1,
        Min5 = 5,
        Min10 = 10,
        Min30 = 30,
        Min60 = 60,
        Min120 = 120,
        Min300 = 300,

        Hour1 = 60,
        Hour2 = 120,
        Hour3 = 180,
        Hour5 = 300,
        Hour15 = 900,
        Hour30 = 1800,
        Hour60 = 3600,

        Day1 = 1440,
        Day5 = 7200,
        Day7 = 10080,
        Day10 = 14400,
        Day20 = 28800,
        Day30 = 43200,
        Day60 = 86400,


        Week1 = 10080,
        Week60 = 604800,
        Year5 = 2592000
    }
}