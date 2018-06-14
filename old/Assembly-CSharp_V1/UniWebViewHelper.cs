// Decompiled with JetBrains decompiler
// Type: UniWebViewHelper
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
