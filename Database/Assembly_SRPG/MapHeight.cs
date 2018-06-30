namespace SRPG
{
    using System;
    using UnityEngine;

    public class MapHeight : MonoBehaviour
    {
        private int oldHeight;
        public int Height;
        public BitmapText MapHeightText;
        private Unit mFocusUnit;

        public MapHeight()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            this.MapHeightText.text = &this.Height.ToString();
            return;
        }

        private unsafe void Update()
        {
            if (this.mFocusUnit == null)
            {
                goto Label_0021;
            }
            this.Height = SceneBattle.Instance.GetDisplayHeight(this.mFocusUnit);
        Label_0021:
            if (this.oldHeight == this.Height)
            {
                goto Label_0048;
            }
            this.MapHeightText.text = &this.Height.ToString();
        Label_0048:
            this.oldHeight = this.Height;
            return;
        }

        public Unit FocusUnit
        {
            set
            {
                this.mFocusUnit = value;
                return;
            }
        }
    }
}

