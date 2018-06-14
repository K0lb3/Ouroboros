// Decompiled with JetBrains decompiler
// Type: gu3.Device.DeviceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using DeviceKit;
using System;

namespace gu3.Device
{
  [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
  public class DeviceManager
  {
    private const string OBSOLATE_TEXT = "Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.";

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static void SetAutoSleep(bool active)
    {
      App.SetAutoSleep(active);
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static string GetBundleIdentifier()
    {
      return App.GetBundleIdentifier();
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static string GetBundleVersion()
    {
      return App.GetBundleVersion();
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static bool OpenUrl(string url)
    {
      return App.OpenUrl(url);
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static bool OpenStore(string appId)
    {
      return App.OpenStore(appId);
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static bool LaunchMailer(string mailto, string subject, string body)
    {
      return App.LaunchMailer(mailto, subject, body);
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static string GetUserAgent()
    {
      return Sys.GetUserAgent();
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static string GetSystemProxyURL()
    {
      return Sys.GetSystemProxyURL();
    }

    [Obsolete("Should be call \"DeviceKit.App\" or \"DeviceKit.Sys\" instead.")]
    public static string GetLanguageLocale()
    {
      return Sys.GetLanguageLocale();
    }
  }
}
