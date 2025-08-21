using System;
using UnityEngine;
using Utils.Drawer.Key;

namespace Config.Base.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PrefabsBase), fileName = nameof(PrefabsBase))]
    public class PrefabsBase : ScriptableObject, IPrefabsBase
    {
        [KeyValue(nameof(Prefab.Name))]
        [SerializeField]
        private Prefab[] _prefabs;
        
        public GameObject Get(string prefabName)
        {
            foreach (var prefab in _prefabs)
            {
                if (prefab.Name == prefabName)
                    return prefab.GameObject;
            }

            throw new Exception($"[PrefabsBase] Can't find prefab with name: {name}");
        }

        [Serializable]
        public struct Prefab
        {
            public string Name;
            public GameObject GameObject;
        }
    }
}