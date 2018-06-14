// Decompiled with JetBrains decompiler
// Type: MyMsgInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Globalization;
using System.Text.RegularExpressions;

public static class MyMsgInput
{
  public static bool isLegal(string name)
  {
    return string.IsNullOrEmpty(name) || new StringInfo(name).LengthInTextElements >= name.Length && !new Regex("^[0-9０-９\\-]+$").IsMatch(name) && !new Regex("^\\s+$").IsMatch(name);
  }
}
