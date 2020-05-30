namespace IqOptionApi.Models.BinaryOptions
{
    public enum BinaryOptionsDuration
    {
        M1 = 1,
        M2 = 2,
        M3 = 3,
        M4 = 4,
        M5 = 5,
        
        M15 = 15,
        M30 = 30,
        M45 = 45,
        M60 = 60,
        
        H1 = 60,
        
        EndOfMonth = int.MaxValue
    }
}