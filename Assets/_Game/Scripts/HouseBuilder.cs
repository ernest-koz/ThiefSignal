using UnityEngine;

namespace ThiefSignal
{
    public static class HouseBuilder
    {
        private const int FloorSortingOrder = 0;
        private const int WallSortingOrder = 1;

        private static readonly Color s_floorColor = new Color(0.4f, 0.4f, 0.45f);
        private static readonly Color s_wallColor = new Color(0.7f, 0.7f, 0.75f);

        public static void Build(SpriteAssets assets, HouseConfig config, AlarmController alarm)
        {
            BuildFloor(assets, config);
            BuildWalls(assets, config);
            BuildInterior(config, alarm);
        }

        private static void BuildFloor(SpriteAssets assets, HouseConfig config) =>
            assets.CreateSprite("Floor", config.Center, config.Size, s_floorColor, FloorSortingOrder);

        private static void BuildWalls(SpriteAssets assets, HouseConfig config)
        {
            float halfWidth = config.Size.x * 0.5f;
            float halfHeight = config.Size.y * 0.5f;

            float top = config.Center.y + halfHeight;
            float bottom = config.Center.y - halfHeight;
            float left = config.Center.x - halfWidth;
            float right = config.Center.x + halfWidth;

            assets.CreateSprite("WallTop", new Vector2(config.Center.x, top),
                new Vector2(config.Size.x, config.WallThickness), s_wallColor, WallSortingOrder);
            assets.CreateSprite("WallBottom", new Vector2(config.Center.x, bottom),
                new Vector2(config.Size.x, config.WallThickness), s_wallColor, WallSortingOrder);

            BuildWallWithDoor(assets, "WallLeft", left, top, bottom, config);
            BuildWallWithDoor(assets, "WallRight", right, top, bottom, config);
        }

        private static void BuildWallWithDoor(SpriteAssets assets, string wallName, float x, float top, float bottom, HouseConfig config)
        {
            float doorTop = config.Center.y + config.DoorWidth * 0.5f;
            float doorBottom = config.Center.y - config.DoorWidth * 0.5f;

            Vector2 upperSize = new Vector2(config.WallThickness, top - doorTop);
            Vector2 upperCenter = new Vector2(x, (top + doorTop) * 0.5f);
            assets.CreateSprite(wallName + "Upper", upperCenter, upperSize, s_wallColor, WallSortingOrder);

            Vector2 lowerSize = new Vector2(config.WallThickness, doorBottom - bottom);
            Vector2 lowerCenter = new Vector2(x, (bottom + doorBottom) * 0.5f);
            assets.CreateSprite(wallName + "Lower", lowerCenter, lowerSize, s_wallColor, WallSortingOrder);
        }

        private static void BuildInterior(HouseConfig config, AlarmController alarm)
        {
            GameObject interiorObject = new GameObject("HouseInterior");
            interiorObject.transform.position = config.Center;

            BoxCollider2D collider = interiorObject.AddComponent<BoxCollider2D>();
            collider.size = config.Size;
            collider.isTrigger = true;

            HouseInterior interior = interiorObject.AddComponent<HouseInterior>();
            interior.Init(alarm);
        }
    }

    public readonly struct HouseConfig
    {
        public HouseConfig(Vector2 center, Vector2 size, float wallThickness, float doorWidth)
        {
            Center = center;
            Size = size;
            WallThickness = wallThickness;
            DoorWidth = doorWidth;
        }

        public Vector2 Center { get; }
        public Vector2 Size { get; }
        public float WallThickness { get; }
        public float DoorWidth { get; }
    }
}
