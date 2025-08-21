using UnityEngine;

namespace Config.GameField.Impl
{
    public class GameFieldParameters : ScriptableObject, IGameFieldParameters
    {
        [field: SerializeField]
        public Vector2Int Size { get; private set; }
    }
}