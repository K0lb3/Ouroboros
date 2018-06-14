// Decompiled with JetBrains decompiler
// Type: SRPG.ColorUtility
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public static class ColorUtility
  {
    private static Dictionary<string, Color32> mNamedColors = new Dictionary<string, Color32>();

    static ColorUtility()
    {
      ColorUtility.mNamedColors["aqua"] = new Color32((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["black"] = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["blue"] = new Color32((byte) 0, (byte) 0, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["brown"] = new Color32((byte) 165, (byte) 42, (byte) 42, byte.MaxValue);
      ColorUtility.mNamedColors["cyan"] = new Color32((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["darkblue"] = new Color32((byte) 0, (byte) 0, (byte) 160, byte.MaxValue);
      ColorUtility.mNamedColors["fuchsia"] = new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["green"] = new Color32((byte) 0, (byte) 128, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["grey"] = new Color32((byte) 128, (byte) 128, (byte) 128, byte.MaxValue);
      ColorUtility.mNamedColors["lightblue"] = new Color32((byte) 173, (byte) 216, (byte) 230, byte.MaxValue);
      ColorUtility.mNamedColors["lime"] = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["magenta"] = new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["maroon"] = new Color32((byte) 128, (byte) 0, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["navy"] = new Color32((byte) 0, (byte) 0, (byte) 128, byte.MaxValue);
      ColorUtility.mNamedColors["olive"] = new Color32((byte) 128, (byte) 128, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["orange"] = new Color32(byte.MaxValue, (byte) 165, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["purple"] = new Color32((byte) 128, (byte) 0, (byte) 128, byte.MaxValue);
      ColorUtility.mNamedColors["red"] = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
      ColorUtility.mNamedColors["silver"] = new Color32((byte) 192, (byte) 192, (byte) 192, byte.MaxValue);
      ColorUtility.mNamedColors["teal"] = new Color32((byte) 0, (byte) 128, (byte) 128, byte.MaxValue);
      ColorUtility.mNamedColors["white"] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      ColorUtility.mNamedColors["yellow"] = new Color32(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue);
    }

    public static Color32 GetColor(string name)
    {
      if (ColorUtility.mNamedColors.ContainsKey(name))
        return ColorUtility.mNamedColors[name];
      return new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
    }

    public static Color32 ParseColor(string src)
    {
      int result;
      if ((int) src[0] == 35 && int.TryParse(src.Substring(1), out result))
        return new Color32((byte) (result >> 24), (byte) (result >> 16 & (int) byte.MaxValue), (byte) (result >> 8 & (int) byte.MaxValue), (byte) (result & (int) byte.MaxValue));
      return ColorUtility.GetColor(src);
    }
  }
}
