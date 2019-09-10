using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum enSpinWheelStickPos // your custom enumeration
{
    Top,
    Down,
    Left,
    Right
};

[CustomEditor(typeof(SpinWheel))]
public class SpinWheelEditor : Editor
{
    string[] _choices = new[] { "top (90)", "down (270)", "left (180)", "right (0)" };
    int _choiceIndex = 0;

    enSpinWheelStickPos enSpinWheelStickPos;

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        _choiceIndex = EditorGUILayout.Popup("Angle End", _choiceIndex, _choices);
        var someClass = target as SpinWheel;
        // Update the selected choice in the underlying object
        someClass.SetAngleEnd(_choiceIndex);
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}
