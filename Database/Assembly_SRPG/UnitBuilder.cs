// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  internal class UnitBuilder
  {
    private string m_UnitIname;
    private int m_Exp;
    private int m_Awake;
    private string m_JobIname;
    private int m_Rarity;
    private int m_JobRank;

    public UnitBuilder(string unitIname)
    {
      this.m_UnitIname = unitIname;
    }

    public UnitBuilder SetLevelByExp(int exp)
    {
      this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.CalcUnitLevel(exp, 250);
      this.m_Exp = exp;
      return this;
    }

    public UnitBuilder SetExpByLevel(int level)
    {
      this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(level);
      return this;
    }

    public UnitBuilder SetRarity(int value)
    {
      this.m_Rarity = value;
      return this;
    }

    public UnitBuilder SetAwake(int value)
    {
      this.m_Awake = value;
      return this;
    }

    public UnitBuilder SetJob(string jobIname, int jobRank)
    {
      this.m_JobIname = jobIname;
      this.m_JobRank = jobRank;
      return this;
    }

    public UnitData Build()
    {
      UnitData unitData = new UnitData();
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.m_UnitIname);
      unitData.Setup(this.m_UnitIname, this.m_Exp, this.m_Rarity, this.m_Awake, this.m_JobIname, this.m_JobRank, unitParam.element);
      return unitData;
    }
  }
}
