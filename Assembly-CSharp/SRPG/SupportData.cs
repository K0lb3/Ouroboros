// Decompiled with JetBrains decompiler
// Type: SRPG.SupportData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
        if (this.Unit != null)
        {
          SkillData leaderSkill = this.Unit.LeaderSkill;
          if (leaderSkill != null)
            return leaderSkill.SkillParam;
        }
        return (SkillParam) null;
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
      if (json.unit != null)
      {
        Json_Unit unit = json.unit;
        this.UnitID = unit.iname;
        this.UnitLevel = unit.lv;
        this.UnitRarity = unit.rare;
        if (unit.select != null)
        {
          this.JobID = (string) null;
          for (int index = 0; index < unit.jobs.Length; ++index)
          {
            if (unit.jobs[index].iid == unit.select.job)
            {
              this.JobID = unit.jobs[index].iname;
              break;
            }
          }
        }
        this.LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(this.UnitRarity, unit.plus);
        UnitData unitData = new UnitData();
        unitData.Deserialize(unit);
        this.Unit = unitData;
      }
      this.mIsFriend = json.isFriend;
    }
  }
}
