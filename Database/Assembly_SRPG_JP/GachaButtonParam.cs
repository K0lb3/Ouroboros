// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButtonParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GachaButtonParam
  {
    private string m_ButtonText = string.Empty;
    private string m_AppealText = string.Empty;
    private bool m_IsShowStepup = true;
    private int m_Cost;
    private int m_StepIndex;
    private int m_StepMax;
    private int m_TicketNum;
    private int m_ExecNum;
    private int m_AppealType;
    private bool m_IsNoUseFree;
    private GachaCostType m_CostType;
    private GachaCategory m_Category;

    public GachaButtonParam()
    {
      this.m_Cost = 0;
      this.m_StepIndex = 0;
      this.m_StepMax = 0;
      this.m_TicketNum = 0;
      this.m_ExecNum = 0;
      this.m_ButtonText = string.Empty;
      this.m_AppealText = string.Empty;
      this.m_IsShowStepup = true;
      this.m_IsNoUseFree = false;
      this.m_CostType = GachaCostType.NONE;
      this.m_Category = GachaCategory.NONE;
    }

    public GachaButtonParam(GachaButtonParam _param)
    {
      this.m_Cost = _param.Cost;
      this.m_StepIndex = _param.StepIndex;
      this.m_StepMax = _param.StepMax;
      this.m_TicketNum = _param.TicketNum;
      this.m_ExecNum = _param.ExecNum;
      this.m_AppealType = _param.AppealType;
      this.m_ButtonText = _param.ButtonText;
      this.m_AppealText = _param.AppealText;
      this.m_IsShowStepup = _param.IsShowStepup;
      this.m_IsNoUseFree = _param.IsNoUseFree;
      this.m_CostType = _param.CostType;
      this.m_Category = _param.Category;
    }

    public GachaButtonParam(int _cost, int _step_index, int _step_max, int _ticket_num, int _exec_num, int _appeal_type, string _button_text, string _appeal_text, bool _is_show_stepup, bool _is_nouse_free, GachaCostType _cost_type, GachaCategory _category)
    {
      this.m_Cost = _cost;
      this.m_StepIndex = _step_index;
      this.m_StepMax = _step_max;
      this.m_TicketNum = _ticket_num;
      this.m_ExecNum = _exec_num;
      this.m_AppealType = _appeal_type;
      this.m_ButtonText = _button_text;
      this.m_AppealText = _appeal_text;
      this.m_IsShowStepup = _is_show_stepup;
      this.m_IsNoUseFree = _is_nouse_free;
      this.m_CostType = _cost_type;
      this.m_Category = _category;
    }

    public int Cost
    {
      get
      {
        return this.m_Cost;
      }
    }

    public int StepIndex
    {
      get
      {
        return this.m_StepIndex;
      }
    }

    public int StepMax
    {
      get
      {
        return this.m_StepMax;
      }
    }

    public int TicketNum
    {
      get
      {
        return this.m_TicketNum;
      }
    }

    public int ExecNum
    {
      get
      {
        return this.m_ExecNum;
      }
    }

    public int AppealType
    {
      get
      {
        return this.m_AppealType;
      }
    }

    public string ButtonText
    {
      get
      {
        return this.m_ButtonText;
      }
    }

    public string AppealText
    {
      get
      {
        return this.m_AppealText;
      }
    }

    public bool IsShowStepup
    {
      get
      {
        return this.m_IsShowStepup;
      }
    }

    public GachaCostType CostType
    {
      get
      {
        return this.m_CostType;
      }
    }

    public GachaCategory Category
    {
      get
      {
        return this.m_Category;
      }
    }

    public bool IsNoUseFree
    {
      get
      {
        if (this.m_Category != GachaCategory.NONE)
          return this.m_IsNoUseFree;
        return false;
      }
    }

    public bool IsStepUp()
    {
      return this.m_StepMax > 0;
    }
  }
}
