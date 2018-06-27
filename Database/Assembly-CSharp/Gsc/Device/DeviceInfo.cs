// Decompiled with JetBrains decompiler
// Type: Gsc.Device.DeviceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Gsc.Device
{
  public static class DeviceInfo
  {
    public const string OSNAME = "android";
    private static readonly string deviceModel;
    private static readonly string deviceVendor;

    static DeviceInfo()
    {
      Match match = new Regex("(?<model>.*)\\((?<vendor>.*)\\)").Match(SystemInfo.get_deviceModel());
      if (match.Success)
      {
        DeviceInfo.deviceModel = match.Groups["model"].Captures[0].Value.Trim();
        DeviceInfo.deviceVendor = match.Groups["vendor"].Captures[0].Value.Trim();
      }
      else
      {
        DeviceInfo.deviceModel = SystemInfo.get_deviceModel();
        DeviceInfo.deviceVendor = "<unknown>";
      }
    }

    public static string DeviceModel
    {
      get
      {
        return DeviceInfo.deviceModel;
      }
    }

    public static string DeviceVendor
    {
      get
      {
        return DeviceInfo.deviceVendor;
      }
    }

    public static string OperatingSystem
    {
      get
      {
        return SystemInfo.get_operatingSystem();
      }
    }

    public static string ProcessorType
    {
      get
      {
        return SystemInfo.get_processorType();
      }
    }

    public static int SystemMemorySize
    {
      get
      {
        return SystemInfo.get_systemMemorySize() << 10;
      }
    }

    public static void SetGraphicsInfo(Dictionary<string, object> data)
    {
      data.Add("graphics_device_name", (object) SystemInfo.get_graphicsDeviceName());
      data.Add("graphics_device_type", (object) SystemInfo.get_graphicsDeviceType());
      data.Add("graphics_device_vendor", (object) SystemInfo.get_graphicsDeviceVendor());
      data.Add("graphics_device_version", (object) SystemInfo.get_graphicsDeviceVersion());
      data.Add("graphics_memory_size", (object) SystemInfo.get_graphicsMemorySize());
      data.Add("graphics_multi_threaded", (object) SystemInfo.get_graphicsMultiThreaded());
      data.Add("graphics_shader_level", (object) SystemInfo.get_graphicsShaderLevel());
    }
  }
}
