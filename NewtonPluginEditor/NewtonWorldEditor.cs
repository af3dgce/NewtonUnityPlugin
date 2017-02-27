﻿/*
* This software is provided 'as-is', without any express or implied
* warranty. In no event will the authors be held liable for any damages
* arising from the use of this software.
* 
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 
* 1. The origin of this software must not be misrepresented; you must not
* claim that you wrote the original software. If you use this software
* in a product, an acknowledgment in the product documentation would be
* appreciated but is not required.
* 
* 2. Altered source versions must be plainly marked as such, and must not be
* misrepresented as being the original software.
* 
* 3. This notice may not be removed or altered from any source distribution.
*/

using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(NewtonWorld))]
public class NewtonWorldEditor : Editor
{
    void OnEnable()
    {
        // Setup the SerializedProperties
        m_gravityProp = serializedObject.FindProperty("m_gravity");
        m_subStepsProp = serializedObject.FindProperty("m_subSteps");
        m_updateRateProp = serializedObject.FindProperty("m_updateRate");
        m_asyncUpdateProp = serializedObject.FindProperty("m_asyncUpdate");
        m_saveSceneNameProp = serializedObject.FindProperty("m_saveSceneName");
        m_serializeSceneOnceProp = serializedObject.FindProperty("m_serializeSceneOnce");
        m_numThreadsProp = serializedObject.FindProperty("m_numberOfThreads");
        m_broadPhaseTypeProp = serializedObject.FindProperty("m_broadPhaseType");
        m_solverIterationsCountProp = serializedObject.FindProperty("m_solverIterationsCount");

        m_defaultRestitutionProp = serializedObject.FindProperty("m_defaultRestitution");
        m_defaultStaticFrictionProp = serializedObject.FindProperty("m_defaultStaticFriction");
        m_defaultKineticFrictionProp = serializedObject.FindProperty("m_defaultKineticFriction");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // Show the custom GUI controls
        EditorGUILayout.PropertyField(m_asyncUpdateProp, new GUIContent("Asynchronous update"));
        EditorGUILayout.PropertyField(m_serializeSceneOnceProp, new GUIContent("Serialize scene once"));
        EditorGUILayout.PropertyField(m_saveSceneNameProp, new GUIContent("Serialize scene name"));
        EditorGUILayout.IntPopup(m_numThreadsProp, m_numberOfThreadsOptions, m_numberOfThreadsValues, new GUIContent("Worker threads"));
        EditorGUILayout.IntSlider(m_solverIterationsCountProp, 1, 10, new GUIContent("Solver iterations count"));
        EditorGUILayout.IntSlider(m_updateRateProp, 60, 1000, new GUIContent("Update rate"));
        EditorGUILayout.IntSlider(m_subStepsProp, 1, 4, new GUIContent("Number of update sub steps"));
        EditorGUILayout.IntPopup(m_broadPhaseTypeProp, m_broadPhaseOptions, m_broadPhaseValues, new GUIContent("Broad phase type"));
        EditorGUILayout.PropertyField(m_gravityProp, new GUIContent("Gravity"));

        EditorGUILayout.PropertyField(m_defaultRestitutionProp, new GUIContent("Default restitution"));
        EditorGUILayout.PropertyField(m_defaultStaticFrictionProp, new GUIContent("Default static friction"));
        EditorGUILayout.PropertyField(m_defaultKineticFrictionProp, new GUIContent("Default kinetic Friction"));

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

    
    SerializedProperty m_gravityProp;
    SerializedProperty m_subStepsProp;
    SerializedProperty m_updateRateProp;
    SerializedProperty m_numThreadsProp;
    SerializedProperty m_asyncUpdateProp;
    SerializedProperty m_saveSceneNameProp;
    SerializedProperty m_serializeSceneOnceProp;
    SerializedProperty m_broadPhaseTypeProp;
    SerializedProperty m_solverIterationsCountProp;
    

    SerializedProperty m_defaultRestitutionProp;
    SerializedProperty m_defaultStaticFrictionProp;
    SerializedProperty m_defaultKineticFrictionProp;

    static private GUIContent[] m_broadPhaseOptions = { new GUIContent("Mixed static dynamic"), new GUIContent("dynamics")};
    static private GUIContent[] m_numberOfThreadsOptions = { new GUIContent("Single threaded"), new GUIContent("2 worker threads"), new GUIContent("3 worker threads"), new GUIContent("4 worker threads"), new GUIContent("8 worker threads") };
    static private int[] m_broadPhaseValues = { 0, 1};
    static private int[] m_numberOfThreadsValues = { 0, 2, 3, 4, 8 };
}



