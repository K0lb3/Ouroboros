// Decompiled with JetBrains decompiler
// Type: SRPG.GachaRequsetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GachaRequsetParam
  {
    private string m_iname;
    private string m_input;
    private int m_cost;
    private string m_type;
    private int m_free;
    private string m_ticket;
    private int m_num;
    private bool m_paid;
    private string m_confirm_text;
    private bool m_use_onemore;
    private GachaCategory m_category;

    public GachaRequsetParam()
    {
      this.m_iname = (string) null;
      this.m_input = (string) null;
      this.m_cost = 0;
      this.m_type = (string) null;
      this.m_free = 0;
      this.m_ticket = (string) null;
      this.m_num = 0;
      this.m_paid = false;
      this.m_confirm_text = (string) null;
      this.m_use_onemore = false;
    }

    public GachaRequsetParam(GachaRequsetParam _request)
    {
      this.m_iname = _request.Iname;
      this.m_input = _request.Input;
      this.m_cost = _request.Cost;
      this.m_type = _request.Type;
      this.m_free = _request.Free;
      this.m_ticket = _request.Ticket;
      this.m_num = _request.Num;
      this.m_paid = _request.IsPaid;
      this.m_confirm_text = _request.ConfirmText;
      this.m_use_onemore = _request.IsUseOneMore;
      this.m_category = _request.Category;
    }

    public GachaRequsetParam(string _iname, string _input, int _cost, string _type, int _free, string _ticket, int _num, bool _paid, string _confirm_text, bool _use_onemore, GachaCategory _category)
    {
      this.m_iname = _iname;
      this.m_input = _input;
      this.m_cost = _cost;
      this.m_type = _type;
      this.m_free = _free;
      this.m_ticket = _ticket;
      this.m_num = _num;
      this.m_paid = _paid;
      this.m_confirm_text = _confirm_text;
      this.m_use_onemore = _use_onemore;
      this.m_category = _category;
    }

    public string Iname
    {
      get
      {
        return this.m_iname;
      }
    }

    public string Input
    {
      get
      {
        return this.m_input;
      }
    }

    public int Cost
    {
      get
      {
        return this.m_cost;
      }
    }

    public string Type
    {
      get
      {
        return this.m_type;
      }
    }

    public int Free
    {
      get
      {
        return this.m_free;
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
        return this.m_paid;
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

    public void SetFree(int _free)
    {
      this.m_free = _free;
    }

    public void SetNum(int _num)
    {
      this.m_num = _num;
    }
  }
}
