// Decompiled with JetBrains decompiler
// Type: SRPG.FirstChargeReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class FirstChargeReward
  {
    private string m_Iname = string.Empty;
    private long m_Type;
    private int m_Num;

    public FirstChargeReward()
    {
      this.m_Iname = string.Empty;
      this.m_Type = 0L;
      this.m_Num = 0;
    }

    public FirstChargeReward(string _iname, GiftTypes _type, int _num)
    {
      this.m_Iname = _iname;
      this.SetGiftTypes(_type);
      this.m_Num = _num;
    }

    public FirstChargeReward(FlowNode_ReqFirstChargeBonus.Reward _reward)
    {
      this.m_Iname = _reward.iname;
      this.SetGiftTypes(_reward.GetGiftType());
      this.m_Num = _reward.num;
    }

    public string iname
    {
      get
      {
        return this.m_Iname;
      }
    }

    public long type
    {
      get
      {
        return this.m_Type;
      }
    }

    public int num
    {
      get
      {
        return this.m_Num;
      }
    }

    public bool CheckGiftTypes(GiftTypes flag)
    {
      return ((GiftTypes) this.m_Type & flag) != (GiftTypes) 0;
    }

    public void SetGiftTypes(GiftTypes flag)
    {
      this.m_Type |= (long) flag;
    }
  }
}
