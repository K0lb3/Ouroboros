// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TowerParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TowerParam
  {
    public string iname;
    public string name;
    public string expr;
    public string banr;
    public string item;
    public string bg;
    public string eventURL;
    public short unit_recover_minute;
    public short unit_recover_coin;
    public byte can_unit_recover;
    public byte is_down;
    public byte is_view_ranking;
  }
}
