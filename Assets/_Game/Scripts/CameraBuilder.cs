using UnityEngine;

namespace ThiefSignal
{
    public static class CameraBuilder
    {
        public static void Configure(float size, Vector3 position, Color backgroundColor)
        {
            Camera camera = Camera.main;

            if (camera == null)
                camera = CreateCamera();

            camera.orthographic = true;
            camera.orthographicSize = size;
            camera.transform.position = position;
            camera.backgroundColor = backgroundColor;
        }

        private static Camera CreateCamera()
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";

            Camera camera = cameraObject.AddComponent<Camera>();
            cameraObject.AddComponent<AudioListener>();

            return camera;
        }
    }
}
