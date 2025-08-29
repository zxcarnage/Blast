using Core.Utils;
using UnityEditor;

public static class ClearSavesTool
{
    [MenuItem("Tools/Saves/Clear All")] 
    private static void ClearAll()
    {
        SavesCleaner.DeleteAllSaveFiles();
        EditorUtility.DisplayDialog("Clear Saves", "Все сохранения удалены.", "OK");
    }
}


