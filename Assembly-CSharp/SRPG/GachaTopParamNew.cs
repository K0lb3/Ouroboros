// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTopParamNew
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaTopParamNew
  {
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public string iname;
    public string category;
    public long startat;
    public long endat;
    public int coin;
    public int coin_p;
    public int gold;
    public List<UnitParam> units;
    public int num;
    public string ticket_iname;
    public int ticket_num;
    public bool step;
    public int step_num;
    public int step_index;
    public bool limit;
    public int limit_num;
    public int limit_stock;
    public string type;
    public string asset_title;
    public string asset_bg;
    public string group;
    public string btext;
    public string confirm;
    public List<ArtifactParam> artifacts;
    public string detail_url;
    public long reset_at;
    public bool disabled;

    public void Deserialize(GachaParam param)
    {
      if (param == null)
        throw new InvalidCastException();
      this.iname = param.iname;
      this.category = param.category;
      this.startat = param.startat;
      this.endat = param.endat;
      this.coin = param.coin;
      this.gold = param.gold;
      this.coin_p = param.coin_p;
      this.units = param.units;
      this.num = param.num;
      this.ticket_iname = param.ticket_iname;
      this.ticket_num = param.ticket_num;
      this.step = param.step;
      this.step_num = param.step_num;
      this.step_index = param.step_index;
      this.limit = param.limit;
      this.limit_num = param.limit_num;
      this.limit_stock = param.limit_stock;
      this.type = string.Empty;
      this.asset_title = param.asset_title;
      this.asset_bg = param.asset_bg;
      this.group = param.group;
      this.btext = param.btext;
      this.confirm = param.confirm;
      this.bonus_items = param.bonus_items;
      this.artifacts = param.artifacts;
      this.detail_url = param.detail_url;
      this.reset_at = param.reset_at;
      this.disabled = param.disabled;
    }

    public long GetTimerAt()
    {
      if (this.reset_at > 0L)
        return this.reset_at;
      return this.endat;
    }
  }
}
