// Decompiled with JetBrains decompiler
// Type: DeviceKit.Path
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace DeviceKit
{
  public static class Path
  {
    public static string documentPath
    {
      get
      {
        return Path.devicekit_documentPath();
      }
    }

    public static string applicationDataPath
    {
      get
      {
        return Path.devicekit_applicationDataPath();
      }
    }

    public static string cachePath
    {
      get
      {
        return Path.devicekit_cachePath();
      }
    }

    [DllImport("devicekit")]
    private static extern string devicekit_documentPath();

    [DllImport("devicekit")]
    private static extern string devicekit_applicationDataPath();

    [DllImport("devicekit")]
    private static extern string devicekit_cachePath();
  }
}
