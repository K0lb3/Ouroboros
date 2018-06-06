// Decompiled with JetBrains decompiler
// Type: SRPG.MyEncrypt
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
  }
}
