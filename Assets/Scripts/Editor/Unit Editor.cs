//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Unit))]
//public class UnitEditor : Editor
//{
//    private Unit unitInstance;
//    private ClassPreset classPreset;

//    private void OnEnable()
//    {
//        unitInstance = (Unit)target;
//        classPreset = unitInstance.ClassPreset;
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.ApplyModifiedProperties();
//        classPreset = (ClassPreset)EditorGUILayout.ObjectField("Class Preset", unitInstance.ClassPreset, typeof(ClassPreset), false);

//        base.OnInspectorGUI();


//        if (classPreset != unitInstance.ClassPreset)
//        {
//            unitInstance.ApplyPreset(classPreset);
//        }
//        Debug.Log("applied");
//    }
//}