using UnityEditor;
using UnityEngine;
using WF.Core.Network;

namespace WF.Editor
{

    [CustomPropertyDrawer(typeof(SPlatformUserInfo))]
    public class SPlatformUserInfoDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float padding = 4f;
            float avatarSize = 64f;

            // 필드 가져오기
            var nameProp = property.FindPropertyRelative("Name");
            var statusProp = property.FindPropertyRelative("Status");
            var avatarProp = property.FindPropertyRelative("Avatar");

            // Avatar (완전 보기 전용, 버튼 없음, 꽉 찬 정사각형)
            Texture2D avatar = avatarProp.objectReferenceValue as Texture2D;
            if (avatar != null)
            {
                Rect avatarRect = new Rect(position.x, position.y, avatarSize, avatarSize);
                GUI.DrawTexture(avatarRect, avatar, ScaleMode.ScaleToFit);
            }

            // Name
            Rect nameRect = new Rect(position.x + avatarSize + padding, position.y, position.width - avatarSize - padding, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(nameRect, "Name: " + nameProp.stringValue);

            // Status
            Rect statusRect = new Rect(position.x + avatarSize + padding, nameRect.yMax + 2, position.width - avatarSize - padding, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(statusRect, "Status: " + statusProp.stringValue);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float avatarSize = 64f;
            float padding = 4f;
            float lineHeight = EditorGUIUtility.singleLineHeight;
            return Mathf.Max(avatarSize, lineHeight * 2 + 2) + padding;
        }
    }
}
