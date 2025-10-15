using UnityEngine;

namespace WF.Common.Extensions
{
    public static class Extensions
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            if (texture == null)
                return null;

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f) // Pivot: ���
            );
        }
    }
}