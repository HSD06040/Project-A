using UnityEditor;

[CustomEditor(typeof(SkillBase), true)]
public class SkillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var skill = (SkillBase)target;

        var overlapProp = serializedObject.FindProperty("overlap");
        var overlapTypeProp = overlapProp.FindPropertyRelative("overlapType");

        EditorGUILayout.LabelField("Overlap", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(overlapTypeProp);

        OverlapType overlapType = (OverlapType)overlapTypeProp.enumValueIndex;

        EditorGUILayout.PropertyField(overlapProp.FindPropertyRelative("target"));

        if (overlapType == OverlapType.Box)
            EditorGUILayout.PropertyField(overlapProp.FindPropertyRelative("boxRatio"));

        if (overlapType == OverlapType.Capsule)
            EditorGUILayout.PropertyField(overlapProp.FindPropertyRelative("capsuleHeight"));

        EditorGUI.indentLevel--;

        var prop = serializedObject.GetIterator();
        prop.NextVisible(true);

        while (prop.NextVisible(false))
        {
            if (prop.name == "overlap")
                continue;

            if (!skill.isTick && prop.name == "tickTime")
                continue;

            if (!skill.isMove && prop.name == "velocity")
                continue;

            EditorGUILayout.PropertyField(prop, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
