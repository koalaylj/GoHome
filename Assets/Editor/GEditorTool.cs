using UnityEngine;
using UnityEditor;

public class GEditorTool {

    static public Texture2D blankTexture {
        get {
            return EditorGUIUtility.whiteTexture;
        }
    }

    static public void SetLabelWidth(float width) {
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
		EditorGUIUtility.LookLikeControls(width);
#else
        EditorGUIUtility.labelWidth = width;
#endif
    }

    static public void DrawSeparator() {

        GUILayout.Space(12f);

        if (Event.current.type == EventType.Repaint) {
            Texture2D tex = blankTexture;
            Rect rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 4f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 9f, Screen.width, 1f), tex);
            GUI.color = Color.white;
        }
    }
}