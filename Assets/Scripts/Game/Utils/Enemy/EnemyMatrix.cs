#if UNITY_EDITOR
using System.Linq;
using Game.Utils.PrefabMatrix;
using Game.Views.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Enemy
{
    [HideReferenceObjectPicker]
    public class EnemyMatrix : IPrefabMatrix<EnemyView>
    {
        [ValidateInput("@PrefabMatrixUtils.ValidateMatrix<EnemyView>(this, PrefabMatrix)")]
        [HideReferenceObjectPicker]
        [PrefabMatrix]
        [SerializeField]
        private GameObject[,] _prefabMatrix = new GameObject[PrefabMatrixUtils.ROW_AMOUNT, 1];
        
        public GameObject[,] PrefabMatrix 
        {
            get => _prefabMatrix;
            set => _prefabMatrix = value;
        }
        
        [ShowIf("@PrefabMatrix.GetLength(1) > 1")]
        [Button("Delete last row")]
        private void DeleteLastRow()
        {
            PrefabMatrixUtils.DeleteLastRow(this);
        }
        
        public EnemyView[] ToArray()
        {
            return PrefabMatrix.Cast<GameObject>()
                .Where(wall => wall != null)
                .Select(wall => wall.GetComponent<EnemyView>())
                .ToArray();
        }
    }
}
#endif