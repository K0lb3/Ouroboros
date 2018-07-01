// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuerrillaShopScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_GuerrillaShopScheduleParam
  {
    public int id;
    public string begin_at;
    public string end_at;
    public int accum_ap;
    public string open_time;
    public string cool_time;
    public JSON_GuerrillaShopScheduleAdventParam[] advent;
  }
}
