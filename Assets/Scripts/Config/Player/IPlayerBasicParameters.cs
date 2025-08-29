using DG.Tweening;

namespace Config.Player
{
    public interface IPlayerBasicParameters
    {
        int Health { get; }
        float HealthbarAnimationDuration { get; }
        Ease HealthbarAnimationEase { get; }
    }
}