// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.Misc
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GooglePlayGames.OurUtils
{
  public static class Misc
  {
    public static bool BuffersAreIdentical(byte[] a, byte[] b)
    {
      if (a == b)
        return true;
      if (a == null || b == null || a.Length != b.Length)
        return false;
      for (int index = 0; index < a.Length; ++index)
      {
        if ((int) a[index] != (int) b[index])
          return false;
      }
      return true;
    }

    public static byte[] GetSubsetBytes(byte[] array, int offset, int length)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (offset < 0 || offset >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (length < 0 || array.Length - offset < length)
        throw new ArgumentOutOfRangeException(nameof (length));
      if (offset == 0 && length == array.Length)
        return array;
      byte[] numArray = new byte[length];
      Array.Copy((Array) array, offset, (Array) numArray, 0, length);
      return numArray;
    }

    public static T CheckNotNull<T>(T value)
    {
      if ((object) value == null)
        throw new ArgumentNullException();
      return value;
    }

    public static T CheckNotNull<T>(T value, string paramName)
    {
      if ((object) value == null)
        throw new ArgumentNullException(paramName);
      return value;
    }
  }
}
