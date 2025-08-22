using Config.Base.Impl;
using Config.Player.Impl;
using UnityEngine.Windows.WebCam;

namespace Config.Base
{
    public interface IGeneralBase
    {
        PrefabsBase PrefabsBase { get; }
        PlayerMovementParameters PlayerMovementParameters { get; }
        CameraParameters CameraParameters { get; }
    }
}