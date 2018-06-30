namespace SRPG
{
    using System;
    using UnityEngine;

    [AddComponentMenu("Scripts/SRPG/Camera/Utility")]
    public class CameraUtility : MonoBehaviour
    {
        private float mFixedWidth;
        private float mFixedHeight;

        public CameraUtility()
        {
            this.mFixedWidth = 720f;
            this.mFixedHeight = 1280f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.Reset();
            Object.Destroy(this);
            return;
        }

        public float CalcAspectRatio(float w, float h)
        {
            return ((1f * w) / h);
        }

        private unsafe Rect CalcScreenAspect()
        {
            float num;
            float num2;
            float num3;
            Rect rect;
            num = this.CalcAspectRatio(this.mFixedWidth, this.mFixedHeight);
            num3 = this.CalcAspectRatio((float) Screen.get_width(), (float) Screen.get_height()) / num;
            &rect..ctor(0f, 0f, 1f, 1f);
            if (1f <= num3)
            {
                goto Label_0089;
            }
            &rect.set_x(0f);
            &rect.set_y((1f - num3) / 2f);
            &rect.set_width(1f);
            &rect.set_height(num3);
            goto Label_00C5;
        Label_0089:
            num3 = 1f / num3;
            &rect.set_x((1f - num3) / 2f);
            &rect.set_y(0f);
            &rect.set_width(num3);
            &rect.set_height(1f);
        Label_00C5:
            return rect;
        }

        public void Reset()
        {
            Camera camera;
            camera = base.GetComponent<Camera>();
            if ((camera != null) == null)
            {
                goto Label_001F;
            }
            camera.set_rect(this.CalcScreenAspect());
        Label_001F:
            return;
        }
    }
}

