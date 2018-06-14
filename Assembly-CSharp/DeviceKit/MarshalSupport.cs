// Decompiled with JetBrains decompiler
// Type: DeviceKit.MarshalSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DeviceKit
{
  internal static class MarshalSupport
  {
    [DllImport("devicekit")]
    private static extern void devicekit_get_rawdata_from_string(IntPtr intptr, out IntPtr data, out int size);

    [DllImport("devicekit")]
    private static extern void devicekit_purge_string(ref IntPtr intptr);

    public static string ToString(IntPtr intptr)
    {
      if (!(intptr != IntPtr.Zero))
        return (string) null;
      string str = (string) null;
      IntPtr data;
      int size;
      MarshalSupport.devicekit_get_rawdata_from_string(intptr, out data, out size);
      if (data != IntPtr.Zero && size > 0)
      {
        byte[] numArray = new byte[size];
        Marshal.Copy(data, numArray, 0, size);
        str = Encoding.UTF8.GetString(numArray);
      }
      MarshalSupport.devicekit_purge_string(ref intptr);
      return str;
    }
  }
}
