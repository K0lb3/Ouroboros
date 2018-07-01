// Decompiled with JetBrains decompiler
// Type: SRPG.GachaParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaParam
  {
    public string iname = string.Empty;
    public string category = string.Empty;
    public int gold = -1;
    public int coin = -1;
    public int coin_p = -1;
    public string ticket_iname = string.Empty;
    public string msg = string.Empty;
    public string name = string.Empty;
    public string movie = string.Empty;
    public string bg = string.Empty;
    public string asset_bg = string.Empty;
    public string asset_title = string.Empty;
    public string detail_url = string.Empty;
    public string group = string.Empty;
    public string btext = string.Empty;
    public string confirm = string.Empty;
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public string bonus_msg = string.Empty;
    public string appeal_message = string.Empty;
    public long startat;
    public long endat;
    public int ticket_num;
    public int num;
    public string[] ext_type;
    public List<UnitData> units2;
    public List<UnitParam> units;
    public bool step;
    public int step_num;
    public int step_index;
    public bool limit;
    public int limit_num;
    public int limit_stock;
    public bool limit_cnt;
    public int limit_cnt_rest;
    public int limit_cnt_num;
    public List<ArtifactParam> artifacts;
    public long reset_at;
    public bool disabled;
    public int appeal_type;
    public bool is_hide;
    public bool is_loop;
    public bool is_free_pause;
    public bool redraw;
    public int redraw_rest;
    public int redraw_num;

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
      this.limit_cnt = false;
      this.limit_cnt_rest = 0;
      this.limit_cnt_num = 0;
      this.reset_at = 0L;
      this.disabled = false;
      this.redraw = false;
      this.redraw_rest = 0;
      this.redraw_num = 0;
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
        if (json.ext_param.limit_cnt != null)
        {
          this.limit_cnt = true;
          this.limit_cnt_rest = json.ext_param.limit_cnt.rest;
          this.limit_cnt_num = json.ext_param.limit_cnt.num;
        }
        if (json.ext_param.redraw != null)
        {
          this.redraw = true;
          this.redraw_rest = json.ext_param.redraw.rest;
          this.redraw_num = json.ext_param.redraw.num;
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
      this.bonus_msg = json.bonus_msg;
      if (json.appeal != null)
      {
        this.appeal_type = json.appeal.type;
        this.appeal_message = json.appeal.message;
      }
      this.is_hide = json.isHide == 1;
      this.is_loop = json.isLoop == 1;
      this.is_free_pause = json.isFreePause == 1;
    }
  }
}
