// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTopParamNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaTopParamNew
  {
    public string iname = string.Empty;
    public string category = string.Empty;
    public int coin = -1;
    public int coin_p = -1;
    public int gold = -1;
    public string ticket_iname = string.Empty;
    public string type = string.Empty;
    public string asset_title = string.Empty;
    public string asset_bg = string.Empty;
    public string group = string.Empty;
    public string btext = string.Empty;
    public string confirm = string.Empty;
    public List<GachaBonusParam> bonus_items = new List<GachaBonusParam>();
    public string detail_url = string.Empty;
    public string bonus_msg = string.Empty;
    public string appeal_message = string.Empty;
    public long startat;
    public long endat;
    public List<UnitParam> units;
    public int num;
    public int ticket_num;
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
    public bool is_stepupui_hide;
    public bool is_stepup_loop;
    public bool is_free_pause;
    public bool redraw;
    public int redraw_rest;
    public int redraw_num;

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
      this.limit_cnt = param.limit_cnt;
      this.limit_cnt_rest = param.limit_cnt_rest;
      this.limit_cnt_num = param.limit_cnt_num;
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
      this.bonus_msg = param.bonus_msg;
      this.appeal_type = param.appeal_type;
      this.appeal_message = param.appeal_message;
      this.is_stepupui_hide = param.is_hide;
      this.is_stepup_loop = param.is_loop;
      this.is_free_pause = param.is_free_pause;
      this.redraw = param.redraw;
      this.redraw_rest = param.redraw_rest;
      this.redraw_num = param.redraw_num;
    }

    public long GetTimerAt()
    {
      if (this.reset_at > 0L)
        return this.reset_at;
      return this.endat;
    }

    public GachaCostType CostType
    {
      get
      {
        GachaCostType gachaCostType = GachaCostType.NONE;
        if (this.coin >= 0)
          gachaCostType = GachaCostType.COIN;
        else if (this.coin_p >= 0)
          gachaCostType = GachaCostType.COIN_P;
        else if (this.gold >= 0)
          gachaCostType = GachaCostType.GOLD;
        else if (!string.IsNullOrEmpty(this.ticket_iname) && this.ticket_num > 0)
          gachaCostType = GachaCostType.TICKET;
        return gachaCostType;
      }
    }

    public GachaCategory Category
    {
      get
      {
        GachaCategory gachaCategory = GachaCategory.NONE;
        if (this.category.Contains("gold"))
          gachaCategory = GachaCategory.DEFAULT_NORMAL;
        else if (this.category.Contains("coin"))
          gachaCategory = GachaCategory.DEFAULT_RARE;
        return gachaCategory;
      }
    }

    public bool IsStepUpUIHide
    {
      get
      {
        return this.is_stepupui_hide;
      }
    }

    public bool IsStepUpLoop
    {
      get
      {
        return this.is_stepup_loop;
      }
    }

    public bool IsFreePause
    {
      get
      {
        bool flag = false;
        if (this.Category != GachaCategory.NONE)
          flag = this.is_free_pause;
        return flag;
      }
    }

    public int Cost
    {
      get
      {
        if (this.CostType == GachaCostType.COIN)
          return this.coin;
        if (this.CostType == GachaCostType.COIN_P)
          return this.coin_p;
        if (this.CostType == GachaCostType.GOLD)
          return this.gold;
        return 0;
      }
    }
  }
}
