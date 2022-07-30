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
                //�ҵ�scene.path������һ�� / ��Ȼ��Ӻ�һ��λ�ÿ�ʼ��ȡ
                string str1 = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                string str2 = str1.Substring(0, str1.Length - 6);
                stringList.Add(str2);
            }
        }
        //��listת��Ϊarray
        return stringList.ToArray();
    }
}