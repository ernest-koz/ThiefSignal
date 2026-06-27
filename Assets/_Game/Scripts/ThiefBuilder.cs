using UnityEngine;

namespace ThiefSignal
{
    public static class ThiefBuilder
    {
        private const int ThiefSortingOrder = 5;
        private const int LabelSortingOrder = 10;

        private static readonly Color s_thiefColor = new Color(0.9f, 0.3f, 0.2f);

        public static void Build(SpriteAssets assets, ThiefConfig config, Vector2 houseSize)
        {
            GameObject thiefObject = new GameObject("Thief (Жулик)");

            Rigidbody2D rigidbody = thiefObject.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;

            BoxCollider2D collider = thiefObject.AddComponent<BoxCollider2D>();
            collider.size = Vector2.one;

            SpriteRenderer renderer = thiefObject.AddComponent<SpriteRenderer>();
            renderer.sprite = assets.BaseSprite;
            renderer.color = s_thiefColor;
            renderer.sortingOrder = ThiefSortingOrder;

            thiefObject.transform.localScale = Vector3.one * config.Size;

            Thief thief = thiefObject.AddComponent<Thief>();
            thief.Configure(BuildWaypoints(houseSize), config.Speed);

            CreateLabel(thiefObject.transform, config.Size, "Жулик");
        }

        private static Vector2[] BuildWaypoints(Vector2 houseSize)
        {
            float halfWidth = houseSize.x * 0.5f;
            float outsideX = halfWidth + 3f;
            float bottomY = -houseSize.y - 1f;

            return new Vector2[]
            {
                new Vector2(-outsideX, 0f),
                new Vector2(0f, 0f),
                new Vector2(outsideX, 0f),
                new Vector2(outsideX, bottomY),
                new Vector2(-outsideX, bottomY)
            };
        }

        private static void CreateLabel(Transform parent, float thiefSize, string text)
        {
            GameObject labelObject = new GameObject("Label");
            labelObject.transform.SetParent(parent, false);
            labelObject.transform.localPosition = new Vector3(0f, thiefSize * 0.5f + 0.2f, 0f);

            TextMesh textMesh = labelObject.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.anchor = TextAnchor.LowerCenter;
            textMesh.fontSize = 40;
            textMesh.characterSize = 0.18f;
            textMesh.color = Color.white;

            MeshRenderer meshRenderer = labelObject.GetComponent<MeshRenderer>();
            meshRenderer.sortingOrder = LabelSortingOrder;
        }
    }

    public readonly struct ThiefConfig
    {
        public ThiefConfig(float speed, float size)
        {
            Speed = speed;
            Size = size;
        }

        public float Speed { get; }
        public float Size { get; }
    }
}
