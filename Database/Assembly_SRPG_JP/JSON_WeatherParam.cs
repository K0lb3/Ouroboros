// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_WeatherParam
  {
    public string iname;
    public string name;
    public string expr;
    public string icon;
    public string effect;
    public string[] buff_ids;
    public string[] cond_ids;
  }
}
