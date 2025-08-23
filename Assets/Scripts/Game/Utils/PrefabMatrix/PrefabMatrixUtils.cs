using Game.Views.Enemy;
using UnityEngine;

namespace Game.Utils.PrefabMatrix
{
    public static class PrefabMatrixUtils
    {
        public static int ROW_AMOUNT = 6;
        
        public static GameObject[,] FromFlatArray(EnemyView[] views)
        {
            var rowCount = Mathf.CeilToInt((float)views.Length / ROW_AMOUNT);
            var matrix = new GameObject[ROW_AMOUNT, rowCount];

            for (var i = 0; i < views.Length; i++)
            {
                var x = i % ROW_AMOUNT;
                var y = i / ROW_AMOUNT;
                if (views[i] != null)
                    matrix[x, y] = views[i].gameObject;
            }

            return matrix;
        }
        
        public static bool ValidateMatrix<T>(IPrefabMatrix<T> prefabMatrixObject, GameObject[,] matrix) where T : Component
        {
            DeleteInvalidElements(matrix);
            ExpandMatrixIfHasNoMoreSpace(prefabMatrixObject, matrix);

            return true;

            static void DeleteInvalidElements(GameObject[,] matrix)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == null || matrix[i, j].TryGetComponent(out T _))
                            continue;
                    
                        matrix[i, j] = null;
                    }
                }
            }
        }
        
        public static void DeleteLastRow<T>(IPrefabMatrix<T> prefabMatrixObject)
        {
            var rowsCount = prefabMatrixObject.PrefabMatrix.GetLength(1);

            if (rowsCount == 1)
                return;

            var oldMatrix = prefabMatrixObject.PrefabMatrix;
            var rowAmount = oldMatrix.GetLength(0);
            prefabMatrixObject.PrefabMatrix = new GameObject[rowAmount, rowsCount - 1];
            
            for (var i = 0; i < prefabMatrixObject.PrefabMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < prefabMatrixObject.PrefabMatrix.GetLength(1); j++)
                {
                    prefabMatrixObject.PrefabMatrix[i, j] = oldMatrix[i, j];
                }
            }
            
            ExpandMatrixIfHasNoMoreSpace(prefabMatrixObject, prefabMatrixObject.PrefabMatrix);
        }
        
        private static void ExpandMatrixIfHasNoMoreSpace<T>(IPrefabMatrix<T> prefabMatrixObject, GameObject[,] matrix)
        {
            var hasSpace = false;

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == null)
                    {
                        hasSpace = true;
                    }
                }
            }

            if (hasSpace)
                return;

            var rowAmount = matrix.GetLength(0);
            var rowsCount = matrix.GetLength(1);
            
            prefabMatrixObject.PrefabMatrix = new GameObject[rowAmount, rowsCount + 1];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    prefabMatrixObject.PrefabMatrix[i, j] = matrix[i, j];
                }
            }
        }
    }
}