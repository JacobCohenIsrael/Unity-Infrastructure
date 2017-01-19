using UnityEngine;
using UnityEditor;
using CWO;

namespace CWO.Editor
{
    [CustomEditor(typeof(Market.Resources))]
    public class ResourcesEditor : UnityEditor.Editor
    {
        private bool[] showResourceSlots = new bool[Market.Resources.numResourceSlots];
        private SerializedProperty resourceImagesProperty;
        private SerializedProperty resourcesProperty;


        private const string resourcesPropResourceImagesName = "ResourceImages";
        private const string resourcesPropResourcesName = "resources";


        private void OnEnable ()
        {
            resourceImagesProperty = serializedObject.FindProperty (resourcesPropResourceImagesName);
            resourcesProperty = serializedObject.FindProperty (resourcesPropResourcesName);
        }


        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            for (int i = 0; i < Market.Resources.numResourceSlots; i++)
            {
                ItemSlotGUI (i);
            }

            serializedObject.ApplyModifiedProperties ();
        }


        private void ItemSlotGUI (int index)
        {
            EditorGUILayout.BeginVertical (GUI.skin.box);
            EditorGUI.indentLevel++;

            showResourceSlots[index] = EditorGUILayout.Foldout (showResourceSlots[index], "Resource slot " + index);

            if (showResourceSlots[index])
            {
                EditorGUILayout.PropertyField (resourceImagesProperty.GetArrayElementAtIndex (index));
                EditorGUILayout.PropertyField (resourcesProperty.GetArrayElementAtIndex (index));
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical ();
        }
    }
}