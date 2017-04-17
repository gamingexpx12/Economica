using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
class EnumFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
        Enum targetEnum = GetBaseProperty<Enum>(property);

        string propName = flagSettings.enumName;
        if (String.IsNullOrEmpty(propName))
        {
            propName = property.name;
        }

        EditorGUI.BeginProperty(position, label, property);
        Enum enumNew = EditorGUI.EnumMaskField(position, propName, targetEnum);

        position = EditorGUI.PrefixLabel(position, label);
        property.intValue = (int)Convert.ChangeType(enumNew, targetEnum.GetType());
        EditorGUI.EndProperty();
    }

    static T GetBaseProperty<T>(SerializedProperty prop)
    {
        string[] seperatedpaths = prop.propertyPath.Split('.');

        System.Object reflectionTarget = prop.serializedObject.targetObject as object;

        foreach (var path in seperatedpaths)
        {
            FieldInfo fieldInfo = reflectionTarget.GetType().GetField(path);
            reflectionTarget = fieldInfo.GetValue(reflectionTarget);
        }

        return (T)reflectionTarget;
    }
}
