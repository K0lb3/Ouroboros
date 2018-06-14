// Decompiled with JetBrains decompiler
// Type: DeviceKit.App
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace DeviceKit
{
  public static class App
  {
    public static void SetAutoSleep(bool active)
    {
      App.devicekit_setAutoSleep(active);
    }

    public static string GetBundleIdentifier()
    {
      return App.devicekit_getBundleIdentifier();
    }

    public static string GetBundleVersion()
    {
      return App.devicekit_getBundleVersion();
    }

    public static bool OpenUrl(string url)
    {
      return App.devicekit_openUrl(url);
    }

    public static bool OpenStore(string appId)
    {
      return App.devicekit_openStore(appId);
    }

    public static bool LaunchMailer(string mailto, string subject, string body)
    {
      return App.devicekit_launchMailer(mailto, subject, body);
    }

    public static string GetClientId()
    {
      return App.devicekit_getClientId();
    }

    public static string GetIdfa()
    {
      return App.devicekit_getIdfa();
    }

    public static void SetClipboard(string text)
    {
      App.devicekit_setClipboard(text);
    }

    public static string GetClipboard()
    {
      IntPtr intptr;
      App.devicekit_getClipboard(out intptr);
      return MarshalSupport.ToString(intptr);
    }

    [DllImport("devicekit")]
    private static extern void devicekit_setAutoSleep(bool active);

    [DllImport("devicekit")]
    private static extern string devicekit_getBundleIdentifier();

    [DllImport("devicekit")]
    private static extern string devicekit_getBundleVersion();

    [DllImport("devicekit")]
    private static extern bool devicekit_openUrl(string url);

    [DllImport("devicekit")]
    private static extern bool devicekit_openStore(string appId);

    [DllImport("devicekit")]
    private static extern bool devicekit_launchMailer(string mailto, string subject, string body);

    [DllImport("devicekit")]
    private static extern string devicekit_getClientId();

    [DllImport("devicekit")]
    private static extern void devicekit_getAuthKeys(out IntPtr secretKey, out IntPtr deviceId, string suffix);

    [DllImport("devicekit")]
    private static extern void devicekit_setAuthKeys(string secretKey, string deviceId, string suffix);

    [DllImport("devicekit")]
    private static extern void devicekit_delAuthKeys(string suffix);

    [DllImport("devicekit")]
    private static extern string devicekit_getIdfa();

    [DllImport("devicekit")]
    private static extern void devicekit_setClipboard(string text);

    [DllImport("devicekit")]
    private static extern void devicekit_getClipboard(out IntPtr intptr);

    private static string PtrToHexString(IntPtr intptr)
    {
      if (!(intptr != IntPtr.Zero))
        return (string) null;
      byte[] numArray = new byte[36];
      Marshal.Copy(intptr, numArray, 0, numArray.Length);
      return Encoding.ASCII.GetString(numArray);
    }

    public static void GetAuthKeys(out string secretKey, out string deviceId, string suffix)
    {
      IntPtr secretKey1;
      IntPtr deviceId1;
      App.devicekit_getAuthKeys(out secretKey1, out deviceId1, suffix);
      secretKey = App.PtrToHexString(secretKey1);
      deviceId = App.PtrToHexString(deviceId1);
    }

    public static void SetAuthKeys(string secretKey, string deviceId, string suffix)
    {
      App.devicekit_setAuthKeys(secretKey, deviceId, suffix);
    }

    public static void DeleteAuthKeys(string suffix)
    {
      App.devicekit_delAuthKeys(suffix);
    }

    public static class Hardkey
    {
      public static void Init(GameObject serviceNode = null)
      {
        HardkeyHandler.Init(serviceNode);
      }

      public static void SetListener(IHardkeyListener listener)
      {
        HardkeyHandler.SetListener(listener);
      }
    }
  }
}
