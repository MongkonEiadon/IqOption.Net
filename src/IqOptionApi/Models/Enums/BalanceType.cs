namespace IqOptionApi.Models
{
    /// <summary>
    /// Account Balance type indicator
    /// </summary>
    public enum BalanceType
    {
        /// <summary>
        /// Trading with real account balance
        /// </summary>
        Real = 1,

        /// <summary>
        /// Trading with demo account balance
        /// </summary>
        Practice = 4,

        /// <summary>
        /// Trading with unknown balance type
        /// </summary>
        Unknown
    }
}