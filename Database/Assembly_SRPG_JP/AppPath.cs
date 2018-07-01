// Decompiled with JetBrains decompiler
// Type: SRPG.AppPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public static class AppPath
  {
    public static string persistentDataPath
    {
      get
      {
        return Application.get_dataPath() + "/../data";
      }
    }

    public static string temporaryCachePath
    {
      get
      {
        return Application.get_dataPath() + "/../temp";
      }
    }

    public static string assetCachePath
    {
      get
      {
        return Application.get_dataPath() + "/..";
      }
    }

    public static string assetCachePathOld
    {
      get
      {
        return Application.get_dataPath() + "/..";
      }
    }
  }
}
