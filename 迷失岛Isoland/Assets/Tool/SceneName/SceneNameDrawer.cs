using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] nameList = (this.attribute as SceneNameAttribute).NameList;
        //Debug.Log((int)property.propertyType);
        if ((int)property.propertyType == 3)
        {
            //��ȡ��¼�ĳ������������е��±겢ѡ�����ֵ��numΪ��¼�ĳ������±꣬���û�м�¼���ֵ�����0
            int num = Mathf.Max(0, Array.IndexOf<string>(nameList, property.stringValue));
            int index = EditorGUI.Popup(position, property.displayName, num, nameList);
            property.stringValue = nameList[index];
        }
        //else if (property.propertyType == null)
        //    property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, nameList);
        else
            base.OnGUI(position, property, label);
    }
}
