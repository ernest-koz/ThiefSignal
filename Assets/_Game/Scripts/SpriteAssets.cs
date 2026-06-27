using UnityEngine;

namespace ThiefSignal
{
    public class SpriteAssets
    {
        private const int BaseTextureSize = 4;

        public SpriteAssets()
        {
            Texture2D whiteTexture = CreateWhiteTexture();
            BaseSprite = CreateBaseSprite(whiteTexture);
        }

        public Sprite BaseSprite { get; }

        public SpriteRenderer CreateSprite(string name, Vector2 position, Vector2 size, Color color, int sortingOrder)
        {
            GameObject spriteObject = new GameObject(name);
            spriteObject.transform.position = position;
            spriteObject.transform.localScale = size;

            SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
            renderer.sprite = BaseSprite;
            renderer.color = color;
            renderer.sortingOrder = sortingOrder;

            return renderer;
        }

        private static Texture2D CreateWhiteTexture()
        {
            Texture2D texture = new Texture2D(BaseTextureSize, BaseTextureSize);
            Color[] pixels = new Color[BaseTextureSize * BaseTextureSize];

            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = Color.white;

            texture.SetPixels(pixels);
            texture.filterMode = FilterMode.Point;
            texture.Apply();

            return texture;
        }

        private static Sprite CreateBaseSprite(Texture2D whiteTexture)
        {
            Rect rect = new Rect(0f, 0f, BaseTextureSize, BaseTextureSize);
            Vector2 pivot = Vector2.one * 0.5f;

            return Sprite.Create(whiteTexture, rect, pivot, BaseTextureSize);
        }
    }
}
