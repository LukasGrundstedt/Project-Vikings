//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Soldier))]
//public class SoldierEditor : Editor
//{
//    private Soldier soldierInstance;
//    private ClassPreset classPreset;

//    private void OnEnable()
//    {
//        soldierInstance = (Soldier)target;
//        classPreset = soldierInstance.ClassPreset;
//    }

//    public override void OnInspectorGUI()
//    {
//        classPreset = (ClassPreset)EditorGUILayout.ObjectField("Class Preset", soldierInstance.ClassPreset, typeof(ClassPreset), false);

//        base.OnInspectorGUI();

//        if (classPreset != soldierInstance.ClassPreset)
//        {
//            soldierInstance.ApplyPreset(classPreset);
//        }


//    }
//}