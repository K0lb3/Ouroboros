// Decompiled with JetBrains decompiler
// Type: NativePlugin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

public static class NativePlugin
{
  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern IntPtr DecompressFile(string path, out int size);

  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern void FreePtr(IntPtr ptr);

  [DllImport("NativePlugin")]
  public static extern int GetGLExtensions([MarshalAs(UnmanagedType.LPArray)] byte[] dest, int destLen);
}
