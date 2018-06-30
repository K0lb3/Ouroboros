namespace SRPG
{
    using System;
    using UnityEngine;

    public class CameraLayers : MonoBehaviour
    {
        public Camera Overlay;

        public CameraLayers()
        {
            base..ctor();
            return;
        }

        private void LateUpdate()
        {
            Camera camera;
            Camera camera2;
            Camera[] cameraArray;
            int num;
            camera = base.GetComponent<Camera>();
            cameraArray = base.GetComponentsInChildren<Camera>();
            num = 0;
            goto Label_0029;
        Label_0015:
            camera2 = cameraArray[num];
            camera2.set_fieldOfView(camera.get_fieldOfView());
            num += 1;
        Label_0029:
            if (num < ((int) cameraArray.Length))
            {
                goto Label_0015;
            }
            return;
        }

        public static void Setup(Camera parent)
        {
            if ((parent == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((parent.GetComponent<CameraLayers>() != null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            parent.get_gameObject().AddComponent<CameraLayers>();
            return;
        }

        private unsafe void Start()
        {
            GameSettings settings;
            settings = GameSettings.Instance;
            this.Overlay = (Camera) Object.Instantiate(&settings.Cameras.OverlayCamera, Vector3.get_zero(), Quaternion.get_identity());
            this.Overlay.get_transform().SetParent(base.get_transform(), 0);
            return;
        }
    }
}

