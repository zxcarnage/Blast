using Sirenix.OdinInspector;

namespace Game.Utils.PrefabMatrix
{
    public class PrefabMatrixAttribute : TableMatrixAttribute
    {
        public PrefabMatrixAttribute()
        {
            SquareCells = true;
            HideColumnIndices = true;
            HideRowIndices = true;
            ResizableColumns = false;
        }
    }
}