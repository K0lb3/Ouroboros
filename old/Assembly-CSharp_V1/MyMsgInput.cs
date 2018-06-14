// Decompiled with JetBrains decompiler
// Type: MyMsgInput
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Globalization;
using System.Text.RegularExpressions;

public static class MyMsgInput
{
  public static bool isLegal(string name)
  {
    return new StringInfo(name).LengthInTextElements >= name.Length && !new Regex("^[0-9０-９\\-]+$").IsMatch(name) && !new Regex("^\\s+$").IsMatch(name);
  }
}
