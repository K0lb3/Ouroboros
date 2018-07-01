// Decompiled with JetBrains decompiler
// Type: SRPG.GachaRequestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GachaRequestParam
  {
    private string m_iname;
    private int m_cost;
    private int m_free;
    private string m_ticket;
    private int m_num;
    private string m_confirm_text;
    private bool m_use_onemore;
    private bool m_no_use_free;
    private int m_redraw_rest;
    private int m_redraw_num;
    private GachaCategory m_category;
    private GachaCostType m_costtype;

    public GachaRequestParam()
    {
      this.m_iname = (string) null;
      this.m_cost = 0;
      this.m_free = 0;
      this.m_ticket = (string) null;
      this.m_num = 0;
      this.m_confirm_text = (string) null;
      this.m_use_onemore = false;
    }

    public GachaRequestParam(GachaRequestParam _request)
    {
      this.m_iname = _request.Iname;
      this.m_cost = _request.Cost;
      this.m_free = _request.Free;
      this.m_ticket = _request.Ticket;
      this.m_num = _request.Num;
      this.m_confirm_text = _request.ConfirmText;
      this.m_use_onemore = _request.IsUseOneMore;
      this.m_category = _request.Category;
    }

    public GachaRequestParam(string _iname, string _input, int _cost, string _type, int _free, string _ticket, int _num, bool _paid, string _confirm_text, bool _use_onemore, GachaCategory _category)
    {
      this.m_iname = _iname;
      this.m_cost = _cost;
      this.m_free = _free;
      this.m_ticket = _ticket;
      this.m_num = _num;
      this.m_confirm_text = _confirm_text;
      this.m_use_onemore = _use_onemore;
      this.m_category = _category;
    }

    public GachaRequestParam(string _iname, int _cost, string _confirm_text, GachaCostType _cost_type, GachaCategory _category, bool _use_onemore, bool _no_use_free = false)
    {
      this.m_iname = _iname;
      this.m_cost = _cost;
      this.m_confirm_text = _confirm_text;
      this.m_costtype = _cost_type;
      this.m_category = _category;
      this.m_use_onemore = _use_onemore;
      this.m_no_use_free = _no_use_free;
    }

    public GachaRequestParam(string _iname)
    {
      this.m_iname = _iname;
      this.m_cost = 0;
      this.m_confirm_text = string.Empty;
      this.m_costtype = GachaCostType.NONE;
      this.m_category = GachaCategory.NONE;
      this.m_use_onemore = false;
      this.m_no_use_free = false;
    }

    public bool IsGold
    {
      get
      {
        if (this.m_costtype != GachaCostType.GOLD)
          return this.m_costtype == GachaCostType.FREE_GOLD;
        return true;
      }
    }

    public bool IsSingle
    {
      get
      {
        return this.m_num == 1;
      }
    }

    public string Iname
    {
      get
      {
        return this.m_iname;
      }
    }

    public int Cost
    {
      get
      {
        return this.m_cost;
      }
    }

    public int Free
    {
      get
      {
        return this.m_free;
      }
    }

    public bool IsFree
    {
      get
      {
        return this.m_free == 1;
      }
    }

    public string Ticket
    {
      get
      {
        return this.m_ticket;
      }
    }

    public int Num
    {
      get
      {
        return this.m_num;
      }
    }

    public bool IsPaid
    {
      get
      {
        return this.m_costtype == GachaCostType.COIN_P;
      }
    }

    public string ConfirmText
    {
      get
      {
        return this.m_confirm_text;
      }
    }

    public bool IsTicketGacha
    {
      get
      {
        return !string.IsNullOrEmpty(this.Ticket);
      }
    }

    public bool IsUseOneMore
    {
      get
      {
        return this.m_use_onemore;
      }
    }

    public GachaCategory Category
    {
      get
      {
        return this.m_category;
      }
    }

    public GachaCostType CostType
    {
      get
      {
        return this.m_costtype;
      }
    }

    public bool IsUseFree
    {
      get
      {
        return !this.m_no_use_free;
      }
    }

    public bool IsRedrawGacha
    {
      get
      {
        return this.m_redraw_num > 0;
      }
    }

    public int RedrawRest
    {
      get
      {
        return this.m_redraw_rest;
      }
    }

    public int RedrawNum
    {
      get
      {
        return this.m_redraw_num;
      }
    }

    public bool IsRedrawConfirm
    {
      get
      {
        return this.m_redraw_rest < this.m_redraw_num;
      }
    }

    public void SetFree(int _free)
    {
      this.m_free = _free;
    }

    public void SetNum(int _num)
    {
      this.m_num = _num;
    }

    public void SetTicketInfo(string _ticket_name, int _num = 0)
    {
      this.m_ticket = _ticket_name;
      this.m_num = _num;
    }

    public void SetConfirmText(string _confirm_text)
    {
      this.m_confirm_text = _confirm_text;
    }

    public void SetUseOneMore(bool _use_onemore)
    {
      this.m_use_onemore = _use_onemore;
    }

    public void SetNoUseFree(bool _no_use_free)
    {
      this.m_no_use_free = _no_use_free;
    }

    public void SetRedraw(int _rest, int _num)
    {
      this.m_redraw_rest = _rest;
      this.m_redraw_num = _num;
    }
  }
}
