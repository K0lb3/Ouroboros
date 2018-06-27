// Decompiled with JetBrains decompiler
// Type: DeviceKit.Sys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace DeviceKit
{
  public static class Sys
  {
    public static string GetUserAgent()
    {
      return Sys.devicekit_getUserAgent();
    }

    public static string GetSystemProxyURL()
    {
      return Sys.devicekit_getSystemProxyURL();
    }

    public static string GetLanguageLocale()
    {
      return Sys.devicekit_getLanguageLocale();
    }

    public static ulong GetAvailableMemoryBytes()
    {
      return Sys.devicekit_getAvailableMemoryBytes();
    }

    public static ulong GetAvailableStorageBytes()
    {
      return Sys.devicekit_getAvailableStorageBytes();
    }

    public static ulong GetAvailableStorageBytes(string localPath)
    {
      return Sys.devicekit_getAvailableStorageBytes();
    }

    [DllImport("devicekit")]
    private static extern string devicekit_getUserAgent();

    [DllImport("devicekit")]
    private static extern string devicekit_getSystemProxyURL();

    [DllImport("devicekit")]
    private static extern string devicekit_getLanguageLocale();

    [DllImport("devicekit")]
    private static extern ulong devicekit_getAvailableStorageBytes();

    [DllImport("devicekit")]
    private static extern ulong devicekit_getAvailableMemoryBytes();
  }
}
