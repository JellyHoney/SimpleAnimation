﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleAnimation))]
public class SimpleAnimationEditor : Editor
{
    static class Styles
    {
        public static GUIContent animation = new GUIContent("Animation", "The clip that will be played if Play() is called, or if \"Play Automatically\" is enabled");
        public static GUIContent animations = new GUIContent("Animations", "These clips will define the States the component will start with");
        public static GUIContent playAutomatically = new GUIContent("Play Automatically", "If checked, the default clip will automatically be played");
    }

    SerializedProperty clip;
    SerializedProperty states;
    SerializedProperty playAutomatically;

    void OnEnable()
    {
        clip = serializedObject.FindProperty("m_Clip");
        states = serializedObject.FindProperty("m_States");
        playAutomatically = serializedObject.FindProperty("m_PlayAutomatically");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(clip, Styles.animation);
        EditorGUILayout.PropertyField(states, Styles.animations, true);
        EditorGUILayout.PropertyField(playAutomatically, Styles.playAutomatically);
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomPropertyDrawer(typeof(SimpleAnimation.EditorState))]
class StateDrawer : PropertyDrawer
{
    class Styles
    {
        public static readonly GUIContent disabledTooltip = new GUIContent("", "The Default state cannot be edited, change the Animation clip to change the Default State");
    }

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUILayout.BeginHorizontal();
        // Calculate rects
        Rect clipRect = new Rect(position.x, position.y, position.width/2 - 5, position.height);
        Rect nameRect = new Rect(position.x + position.width/2 + 5, position.y, position.width/2 - 5, position.height);

        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("clip"), GUIContent.none);
        EditorGUI.PropertyField(clipRect, property.FindPropertyRelative("name"), GUIContent.none);

        EditorGUILayout.EndHorizontal();
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
