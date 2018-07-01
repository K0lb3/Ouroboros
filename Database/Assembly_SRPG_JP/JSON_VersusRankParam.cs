// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusRankParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_VersusRankParam
  {
    public int id;
    public int btl_mode;
    public string name;
    public int limit;
    public string begin_at;
    public string end_at;
    public int win_pt_base;
    public int lose_pt_base;
    public string[] disabledate;
    public string hurl;
  }
}
