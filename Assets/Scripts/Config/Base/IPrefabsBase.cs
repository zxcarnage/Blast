using UnityEngine;

namespace Config.Base
{
    public interface IPrefabsBase
    {
        GameObject Get(string prefabName);
    }
}