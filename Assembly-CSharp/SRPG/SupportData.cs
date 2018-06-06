// Decompiled with JetBrains decompiler
// Type: SRPG.SupportData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class SupportData
  {
    public UnitData Unit;
    public string FUID;
    public string PlayerName;
    public int PlayerLevel;
    public string UnitID;
    public int UnitLevel;
    public int UnitRarity;
    public string JobID;
    public int LeaderSkillLevel;
    public int Cost;
    public int mIsFriend;

    public UnitParam UnitParam
    {
      get
      {
        if (!string.IsNullOrEmpty(this.UnitID))
          return MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID);
        return (UnitParam) null;
      }
    }

    public SkillParam LeaderSkill
    {
      get
      {
        UnitParam unitParam = this.UnitParam;
        if (unitParam == null || string.IsNullOrEmpty((string) unitParam.skill))
          return (SkillParam) null;
        return MonoSingleton<GameManager>.Instance.GetSkillParam((string) unitParam.skill);
      }
    }

    public string UnitName
    {
      get
      {
        return this.UnitParam.name;
      }
    }

    public EElement UnitElement
    {
      get
      {
        return this.Unit.Element;
      }
    }

    public string IconPath
    {
      get
      {
        UnitParam unitParam = this.UnitParam;
        if (unitParam == null)
          return (string) null;
        return AssetPath.UnitSkinIconSmall(unitParam, this.Unit.GetSelectedSkin(-1), this.Unit.CurrentJob.JobID);
      }
    }

    public bool IsFriend()
    {
      return this.mIsFriend == 1;
    }

    public int GetCost()
    {
      return this.Cost;
    }

    public void Deserialize(Json_Support json)
    {
      this.FUID = json.fuid;
      this.PlayerName = json.name;
      this.PlayerLevel = json.lv;
      this.Cost = json.cost;
      this.UnitID = json.unit.iname;
      this.UnitLevel = json.unit.lv;
      this.UnitRarity = json.unit.rare;
      this.mIsFriend = json.isFriend;
      this.LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(this.UnitRarity, json.unit.plus);
      if (json.unit.select != null)
      {
        this.JobID = (string) null;
        for (int index = 0; index < json.unit.jobs.Length; ++index)
        {
          if (json.unit.jobs[index].iid == json.unit.select.job)
          {
            this.JobID = json.unit.jobs[index].iname;
            break;
          }
        }
      }
      UnitData unitData = new UnitData();
      unitData.Deserialize(json.unit);
      this.Unit = unitData;
    }
  }
}
