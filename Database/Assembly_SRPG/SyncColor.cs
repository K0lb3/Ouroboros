namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Graphic))]
    public class SyncColor : MonoBehaviour
    {
        private Graphic mGraphic;
        private Color mOriginColor;
        public CanvasRenderer Source;
        [BitMask]
        public ColorMasks ColorMask;
        public SyncType Type;

        public SyncColor()
        {
            this.ColorMask = 15;
            base..ctor();
            return;
        }

        public void ForceOriginColorChange(Color color)
        {
            this.mOriginColor = color;
            return;
        }

        private void LateUpdate()
        {
            this.Sync();
            return;
        }

        private void Start()
        {
            this.mGraphic = base.GetComponent<Graphic>();
            this.mOriginColor = this.mGraphic.get_color();
            this.Sync();
            return;
        }

        private unsafe void Sync()
        {
            Color color;
            Color color2;
            SyncType type;
            if ((this.Source == null) != null)
            {
                goto Label_0022;
            }
            if ((this.mGraphic == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            color = this.Source.GetColor();
            color2 = new Color();
            type = this.Type;
            if (type == null)
            {
                goto Label_0050;
            }
            if (type == 1)
            {
                goto Label_00CD;
            }
            goto Label_0161;
        Label_0050:
            color2 = this.mGraphic.get_color();
            if ((this.ColorMask & 1) == null)
            {
                goto Label_0077;
            }
            &color2.r = &color.r;
        Label_0077:
            if ((this.ColorMask & 2) == null)
            {
                goto Label_0092;
            }
            &color2.g = &color.g;
        Label_0092:
            if ((this.ColorMask & 4) == null)
            {
                goto Label_00AD;
            }
            &color2.b = &color.b;
        Label_00AD:
            if ((this.ColorMask & 8) == null)
            {
                goto Label_0161;
            }
            &color2.a = &color.a;
            goto Label_0161;
        Label_00CD:
            color2 = this.mOriginColor;
            if ((this.ColorMask & 1) == null)
            {
                goto Label_00F6;
            }
            &color2.r *= &color.r;
        Label_00F6:
            if ((this.ColorMask & 2) == null)
            {
                goto Label_0118;
            }
            &color2.g *= &color.g;
        Label_0118:
            if ((this.ColorMask & 4) == null)
            {
                goto Label_013A;
            }
            &color2.b *= &color.b;
        Label_013A:
            if ((this.ColorMask & 8) == null)
            {
                goto Label_0161;
            }
            &color2.a *= &color.a;
        Label_0161:
            this.mGraphic.set_color(color2);
            return;
        }

        [Flags]
        public enum ColorMasks
        {
            R = 1,
            G = 2,
            B = 4,
            A = 8
        }

        public enum SyncType
        {
            Override,
            Multi
        }
    }
}

