// Decompiled with JetBrains decompiler
// Type: SRPG.GachaParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaParam
  {
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public string iname;
    public string category;
    public long startat;
    public long endat;
    public int gold;
    public int coin;
    public int coin_p;
    public string ticket_iname;
    public int ticket_num;
    public int num;
    public string msg;
    public string name;
    public string movie;
    public string bg;
    public string asset_bg;
    public string asset_title;
    public string detail_url;
    public string[] ext_type;
    public List<UnitData> units2;
    public List<UnitParam> units;
    public bool step;
    public int step_num;
    public int step_index;
    public bool limit;
    public int limit_num;
    public int limit_stock;
    public string group;
    public string btext;
    public string confirm;
    public List<ArtifactParam> artifacts;
    public long reset_at;
    public bool disabled;

    public void Deserialize(Json_GachaParam json)
    {
      if (json == null)
        throw new InvalidCastException();
      if (this.units == null && json.units != null)
      {
        this.units = new List<UnitParam>(json.units.Length);
        for (int index = 0; index < json.units.Length; ++index)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.units[index]);
          if (unitParam != null)
            this.units.Add(unitParam);
        }
      }
      if (this.artifacts == null && json.pickup_art != null)
      {
        this.artifacts = new List<ArtifactParam>(json.pickup_art.Length);
        for (int index = 0; index < json.pickup_art.Length; ++index)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.pickup_art[index]);
          if (artifactParam != null)
            this.artifacts.Add(artifactParam);
        }
      }
      this.iname = json.iname;
      this.category = json.cat;
      this.startat = json.startat;
      this.endat = json.endat;
      this.num = json.num;
      this.msg = json.msg;
      this.gold = 0;
      this.coin = 0;
      this.coin_p = 0;
      this.ticket_iname = (string) null;
      this.ticket_num = 0;
      if (json.cost != null)
      {
        this.gold = json.cost.gold;
        this.coin = json.cost.coin;
        this.coin_p = json.cost.coin_p;
        if (json.cost.ticket != null)
        {
          this.ticket_iname = json.cost.ticket.iname;
          this.ticket_num = json.cost.ticket.num;
        }
      }
      this.name = json.name;
      this.movie = json.movie;
      this.bg = json.bg;
      this.asset_bg = json.asset_bg;
      this.asset_title = json.asset_title;
      this.detail_url = json.detail_url;
      if (json.ext_type != null)
      {
        this.ext_type = new string[json.ext_type.Length];
        for (int index = 0; index < json.ext_type.Length; ++index)
          this.ext_type[index] = json.ext_type[index];
      }
      this.step = false;
      this.step_num = 0;
      this.step_index = 0;
      this.limit = false;
      this.limit_num = 0;
      this.limit_stock = 0;
      this.reset_at = 0L;
      this.disabled = false;
      if (json.ext_param != null)
      {
        if (json.ext_param.step != null)
        {
          this.step = true;
          this.step_num = json.ext_param.step.num;
          this.step_index = json.ext_param.step.index;
        }
        if (json.ext_param.limit != null)
        {
          this.limit = true;
          this.limit_num = json.ext_param.limit.num;
          this.limit_stock = json.ext_param.limit.stock;
        }
        this.reset_at = json.ext_param.next_reset_time;
        this.disabled = json.disabled == 1;
      }
      this.group = json.group;
      this.btext = json.btext;
      this.confirm = json.confirm;
      if (json.bonus_items != null && json.bonus_items.Length > 0)
      {
        for (int index = 0; index < json.bonus_items.Length; ++index)
          this.bonus_items.Add(new GachaBonusParam()
          {
            iname = json.bonus_items[index].iname,
            num = json.bonus_items[index].num
          });
      }
      this.detail_url = json.detail_url;
    }
  }
}
