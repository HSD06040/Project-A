using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ItemData),true)]
//public class ItemDataEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        if (GUILayout.Button("이름을 에셋 이름과 동기화"))
//        {
//            var item = (ItemData)target;
//            string path = AssetDatabase.GetAssetPath(item);
//            AssetDatabase.RenameAsset(path, item.itemName);
//            AssetDatabase.SaveAssets();
//        }
//    }
//}
