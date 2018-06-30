namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class ColorUtility
    {
        private static Dictionary<string, Color32> mNamedColors;

        static ColorUtility()
        {
            mNamedColors = new Dictionary<string, Color32>();
            mNamedColors["aqua"] = new Color32(0, 0xff, 0xff, 0xff);
            mNamedColors["black"] = new Color32(0, 0, 0, 0xff);
            mNamedColors["blue"] = new Color32(0, 0, 0xff, 0xff);
            mNamedColors["brown"] = new Color32(0xa5, 0x2a, 0x2a, 0xff);
            mNamedColors["cyan"] = new Color32(0, 0xff, 0xff, 0xff);
            mNamedColors["darkblue"] = new Color32(0, 0, 160, 0xff);
            mNamedColors["fuchsia"] = new Color32(0xff, 0, 0xff, 0xff);
            mNamedColors["green"] = new Color32(0, 0x80, 0, 0xff);
            mNamedColors["grey"] = new Color32(0x80, 0x80, 0x80, 0xff);
            mNamedColors["lightblue"] = new Color32(0xad, 0xd8, 230, 0xff);
            mNamedColors["lime"] = new Color32(0, 0xff, 0, 0xff);
            mNamedColors["magenta"] = new Color32(0xff, 0, 0xff, 0xff);
            mNamedColors["maroon"] = new Color32(0x80, 0, 0, 0xff);
            mNamedColors["navy"] = new Color32(0, 0, 0x80, 0xff);
            mNamedColors["olive"] = new Color32(0x80, 0x80, 0, 0xff);
            mNamedColors["orange"] = new Color32(0xff, 0xa5, 0, 0xff);
            mNamedColors["purple"] = new Color32(0x80, 0, 0x80, 0xff);
            mNamedColors["red"] = new Color32(0xff, 0, 0, 0xff);
            mNamedColors["silver"] = new Color32(0xc0, 0xc0, 0xc0, 0xff);
            mNamedColors["teal"] = new Color32(0, 0x80, 0x80, 0xff);
            mNamedColors["white"] = new Color32(0xff, 0xff, 0xff, 0xff);
            mNamedColors["yellow"] = new Color32(0xff, 0xff, 0, 0xff);
            return;
        }

        public static Color32 GetColor(string name)
        {
            if (mNamedColors.ContainsKey(name) == null)
            {
                goto Label_001C;
            }
            return mNamedColors[name];
        Label_001C:
            return new Color32(0, 0, 0, 0xff);
        }

        public static unsafe Color32 ParseColor(string src)
        {
            int num;
            if (src[0] != 0x23)
            {
                goto Label_0049;
            }
            if (int.TryParse(src.Substring(1), &num) == null)
            {
                goto Label_0049;
            }
            return new Color32((byte) (num >> 0x18), (byte) ((num >> 0x10) & 0xff), (byte) ((num >> 8) & 0xff), (byte) (num & 0xff));
        Label_0049:
            return GetColor(src);
        }
    }
}

