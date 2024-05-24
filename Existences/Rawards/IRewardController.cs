namespace UniSharp.Common.Existences.Rawards
{
    public interface IRewardController
    {
        Reward Data { get; }

        void Claim();
    }
}
