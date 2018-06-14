// Decompiled with JetBrains decompiler
// Type: UniWebViewHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UniWebViewHelper
{
  public static int screenHeight
  {
    get
    {
      return Screen.get_height();
    }
  }

  public static int screenWidth
  {
    get
    {
      return Screen.get_width();
    }
  }

  public static int screenScale
  {
    get
    {
      return 1;
    }
  }

  public static string streamingAssetURLForPath(string path)
  {
    return "file:///android_asset/" + path;
  }
}
