using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        public Balance CurrentBalance { get; private set; }

        [SubscribeForTopicName(MessageType.BalanceChanged, typeof(BalanceChanged))]
        public void Subscribe(BalanceChanged type)
        {
            CurrentBalance = type.CurrentBalance;
        }
    }

    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<WsMessageBase<BinaryOptionsResult>> _buyResultSubject = new Subject<WsMessageBase<BinaryOptionsResult>>();

        private readonly Subject<Leaderboard> _leaderboardSubject = new Subject<Leaderboard>();

        private readonly Subject<UserProfile> _userProfileSubject = new Subject<UserProfile>();

        private readonly Subject<InitializationData> _initializationDataSubject = new Subject<InitializationData>();

        public IObservable<WsMessageBase<BinaryOptionsResult>> BinaryOptionPlacedResultObservable =>
            _buyResultSubject.AsObservable();

        public IObservable<Leaderboard> LeaderboardObservable => _leaderboardSubject.AsObservable();

        public IObservable<UserProfile> UserProfileObservable => _userProfileSubject.AsObservable();

        public IObservable<InitializationData> InitializationDataObservable => _initializationDataSubject.AsObservable();

        #region [Leaderboard]
        [SubscribeForTopicName(MessageType.LeaderboardDealsClient, typeof(Leaderboard))]
        public void SubscribeLeaderboard(Leaderboard value)
        {
            _leaderboardSubject.OnNext(value);
        }

        [Predisposable]
        internal void OnLeaderboardDisposal()
        {
            _leaderboardSubject.OnCompleted();
        }
        #endregion

        #region [UserProfile]
        [SubscribeForTopicName(MessageType.UserProfile, typeof(UserProfile))]
        public void SubscribeUserProfile(UserProfile value)
        {
            _userProfileSubject.OnNext(value);
        }

        [Predisposable]
        internal void OnUserProfileDisposal()
        {
            _userProfileSubject.OnCompleted();
        }
        #endregion

        #region [Initialization Data]
        [SubscribeForTopicName(MessageType.InitializationData, typeof(InitializationData))]
        public void SubscribeInitializationData(InitializationData value)
        {
           _initializationDataSubject.OnNext(value);
        }

        [Predisposable]
        internal void OnInitializationDataDisposal()
        {
            _initializationDataSubject.OnCompleted();
        }
        #endregion

        [SubscribeForTopicName(MessageType.PlacedBinaryOptions, typeof(WsMessageBase<BinaryOptionsResult>), true)]
        public void Subscribe(WsMessageBase<BinaryOptionsResult> value)
        {
            _buyResultSubject.OnNext(value);
        }

        [Predisposable]
        internal void OnPlacedBinaryOptionsDisposal()
        {
            _buyResultSubject.OnCompleted();
        }
        
    }
}