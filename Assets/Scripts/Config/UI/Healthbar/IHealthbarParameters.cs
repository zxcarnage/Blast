using DG.Tweening;

namespace Config.UI.Healthbar
{
    public interface IHealthbarParameters
    {
        float AnimationTime { get; }
        Ease AnimationEase { get; }
    }
}