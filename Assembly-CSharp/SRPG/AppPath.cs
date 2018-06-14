// Decompiled with JetBrains decompiler
// Type: SRPG.AppPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public static class AppPath
  {
    public static string persistentDataPath
    {
      get
      {
        return Application.get_persistentDataPath();
      }
    }

    public static string temporaryCachePath
    {
      get
      {
        return Application.get_temporaryCachePath();
      }
    }

    public static string assetCachePath
    {
      get
      {
        return Application.get_persistentDataPath();
      }
    }

    public static string assetCachePathOld
    {
      get
      {
        return Application.get_temporaryCachePath();
      }
    }
  }
}
