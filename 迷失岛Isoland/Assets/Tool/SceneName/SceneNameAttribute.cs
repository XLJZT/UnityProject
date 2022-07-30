using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneNameAttribute : PropertyAttribute
{
    public string[] NameList => SceneNameAttribute.AllSceneNames();

    public static string[] AllSceneNames()
    {
        List<string> stringList = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                //找到scene.path里的最后一个 / ，然后从后一个位置开始截取
                string str1 = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                string str2 = str1.Substring(0, str1.Length - 6);
                stringList.Add(str2);
            }
        }
        //从list转化为array
        return stringList.ToArray();
    }
}