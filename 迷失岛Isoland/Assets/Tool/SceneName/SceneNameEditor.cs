//using UnityEngine;
//using UnityEditor;

//[CustomPropertyDrawer(typeof(SceneName))]
//public class SceneNameEditor : PropertyDrawer
//{
//    static GUIContent[] scenes;
//    GUIContent[] GetSceneNames()
//    {
//        GUIContent[] g = new GUIContent[EditorBuildSettings.scenes.Length];
//        for (int i = 0; i < g.Length; ++i)
//        {
//            //scenes的名字为路径形式 Scenes/场景名字
//            string[] splitResult = EditorBuildSettings.scenes[i].path.Split('/');
//            //数组最后一位获取场景名字
//            string nameWithSuffix = splitResult[splitResult.Length - 1];
//            //Debug.Log(nameWithSuffix);
//            //测试之后发现结果为 场景名字.unity
//            g[i] = new GUIContent(nameWithSuffix.Substring(0, nameWithSuffix.Length - ".unity".Length));
//        }
//        return g;
//    }
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        SceneName sceneName = attribute as SceneName;
//        scenes = GetSceneNames();
//        string cntString = (string)fieldInfo.GetValue(property.serializedObject.targetObject);
//        sceneName.selected = 0;
//        sceneName.name = scenes[0].text;
//        for (int i = 0; i < scenes.Length; ++i)
//        {
//            if (scenes[i].text.Equals(cntString))
//            {
//                sceneName.selected = i;
//                sceneName.name = cntString;
//                break;
//            }
//        }
//        sceneName.selected = EditorGUI.Popup(position, label, sceneName.selected, scenes);
//        sceneName.name = scenes[sceneName.selected].text;
//        fieldInfo.SetValue(property.serializedObject.targetObject, sceneName.name);
//        ///
//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(property.serializedObject.targetObject);
//        }
//        //
//    }
//}
