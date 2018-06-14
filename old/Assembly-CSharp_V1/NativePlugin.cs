// Decompiled with JetBrains decompiler
// Type: NativePlugin
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

public static class NativePlugin
{
  [DllImport("NativePlugin")]
  public static extern int UnZip(string prefix, [MarshalAs(UnmanagedType.LPArray)] byte[] src, int len);

  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern IntPtr DecompressFile(string path, out int size);

  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern void FreePtr(IntPtr ptr);

  [DllImport("NativePlugin")]
  public static extern int GetGLExtensions([MarshalAs(UnmanagedType.LPArray)] byte[] dest, int destLen);
}
