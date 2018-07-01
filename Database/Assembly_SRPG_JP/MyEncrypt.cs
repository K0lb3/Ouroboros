// Decompiled with JetBrains decompiler
// Type: SRPG.MyEncrypt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class MyEncrypt
  {
    public static int EncryptCount;
    public static int DecryptCount;

    public static byte[] Encrypt(int seed, string msg, bool compress = false)
    {
      if (string.IsNullOrEmpty(msg))
        return (byte[]) null;
      byte[] bytes = Encoding.UTF8.GetBytes(msg);
      GUtility.Encrypt(bytes, bytes.Length);
      return bytes;
    }

    public static string Decrypt(int seed, byte[] data, bool decompress = false)
    {
      if (data == null)
        return (string) null;
      GUtility.Decrypt(data, data.Length);
      return Encoding.UTF8.GetString(data);
    }

    public static byte[] Encrypt(byte[] msg)
    {
      if (msg == null)
        return (byte[]) null;
      byte[] numArray = new byte[msg.Length];
      Array.Copy((Array) msg, (Array) numArray, msg.Length);
      GUtility.Encrypt(numArray, numArray.Length);
      return numArray;
    }

    public static byte[] Decrypt(byte[] data)
    {
      if (data == null)
        return (byte[]) null;
      byte[] numArray = new byte[data.Length];
      Array.Copy((Array) data, (Array) numArray, data.Length);
      GUtility.Decrypt(numArray, numArray.Length);
      return numArray;
    }
  }
}
