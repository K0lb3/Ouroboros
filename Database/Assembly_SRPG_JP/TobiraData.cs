// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;

namespace SRPG
{
  public class TobiraData
  {
    private int mLv;
    private SkillData mParameterBuffSkill;
    private TobiraParam mTobiraParam;
    private string mLearnedLeaderSkillIname;

    public int Lv
    {
      get
      {
        return this.mLv;
      }
      set
      {
        this.mLv = value;
      }
    }

    public int ViewLv
    {
      get
      {
        return this.mLv - 1;
      }
    }

    public SkillData ParameterBuffSkill
    {
      get
      {
        return this.mParameterBuffSkill;
      }
    }

    public string LearnedLeaderSkillIname
    {
      get
      {
        return this.mLearnedLeaderSkillIname;
      }
    }

    public bool IsUnlocked
    {
      get
      {
        return this.mLv > 0;
      }
    }

    public TobiraParam Param
    {
      get
      {
        return this.mTobiraParam;
      }
    }

    public bool IsLearnedLeaderSkill
    {
      get
      {
        return !string.IsNullOrEmpty(this.mLearnedLeaderSkillIname);
      }
    }

    public bool IsMaxLv
    {
      get
      {
        return this.Lv >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
      }
    }

    public bool Setup(string unit_iname, TobiraParam.Category category, int lv)
    {
      this.mLv = lv;
      this.mTobiraParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraParam(unit_iname, category);
      if (this.mTobiraParam == null)
        return false;
      this.mParameterBuffSkill = TobiraUtility.CreateParameterBuffSkill(this.mTobiraParam, this.mLv);
      if (this.mTobiraParam.HasLeaerSkill && lv >= this.mTobiraParam.OverwriteLeaderSkillLevel)
        this.mLearnedLeaderSkillIname = this.mTobiraParam.OverwriteLeaderSkillIname;
      return this.mParameterBuffSkill != null;
    }

    public Json_Tobira ToJson()
    {
      return new Json_Tobira()
      {
        category = (int) this.Param.TobiraCategory,
        lv = this.Lv
      };
    }

    public string ToJsonString()
    {
      StringBuilder stringBuilder = new StringBuilder(512);
      stringBuilder.Append("{\"lv\":");
      stringBuilder.Append(this.Lv);
      stringBuilder.Append(",");
      stringBuilder.Append("\"category\":");
      stringBuilder.Append((int) this.Param.TobiraCategory);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
