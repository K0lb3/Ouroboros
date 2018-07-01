// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConceptCardParam
  {
    public string iname;
    public string name;
    public string expr;
    public eCardType type;
    public string icon;
    public int rare;
    public int lvcap;
    public int sell;
    public int en_cost;
    public int en_exp;
    public int en_trust;
    public string trust_reward;
    public string first_get_unit;
    public bool is_override_lvcap;
    public ConceptCardEffectsParam[] effects;
    public bool not_sale;

    public bool IsEnableAwake
    {
      get
      {
        return !this.is_override_lvcap;
      }
    }

    public int AwakeCountCap
    {
      get
      {
        if (this.IsEnableAwake)
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
          if (rarityParam != null)
            return (int) rarityParam.ConceptCardAwakeCountMax;
        }
        return 0;
      }
    }

    public int AwakeLevelCap
    {
      get
      {
        if (this.IsEnableAwake)
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
          if (rarityParam != null)
            return (int) rarityParam.ConceptCardAwakeCountMax * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
        }
        return 0;
      }
    }

    public bool Deserialize(JSON_ConceptCardParam json, MasterParam master = null)
    {
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.type = (eCardType) json.type;
      this.icon = json.icon;
      this.rare = json.rare;
      this.sell = json.sell;
      this.en_cost = json.en_cost;
      this.en_exp = json.en_exp;
      this.en_trust = json.en_trust;
      this.trust_reward = json.trust_reward;
      this.first_get_unit = json.first_get_unit;
      this.is_override_lvcap = true;
      this.lvcap = json.lvcap;
      if (json.lvcap <= 0)
      {
        this.is_override_lvcap = false;
        RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
        if (rarityParam != null)
          this.lvcap = (int) rarityParam.ConceptCardLvCap;
      }
      if (json.effects != null)
      {
        this.effects = new ConceptCardEffectsParam[json.effects.Length];
        for (int index = 0; index < json.effects.Length; ++index)
        {
          ConceptCardEffectsParam cardEffectsParam = new ConceptCardEffectsParam();
          if (!cardEffectsParam.Deserialize(json.effects[index]))
            return false;
          this.effects[index] = cardEffectsParam;
        }
      }
      this.not_sale = json.not_sale == 1;
      return true;
    }

    public static void GetSkillStatus(string statusup_skill, int awakeLvCap, int lv, ref BaseStatus add, ref BaseStatus scale)
    {
      SkillData skill = new SkillData();
      skill.Setup(statusup_skill, lv, awakeLvCap, (MasterParam) null);
      SkillData.GetHomePassiveBuffStatus(skill, EElement.None, ref add, ref scale);
    }

    public static void GetSkillAllStatus(string statusup_skill, int awakeLvCap, int lv, ref BaseStatus add, ref BaseStatus scale)
    {
      SkillData skill = new SkillData();
      skill.Setup(statusup_skill, lv, awakeLvCap, (MasterParam) null);
      SkillData.GetPassiveBuffStatus(skill, (BuffEffect[]) null, EElement.None, ref add, ref scale);
    }

    public bool IsExistAddCardSkillBuffAwake()
    {
      if (this.effects != null)
      {
        for (int index = 0; index < this.effects.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.effects[index].card_skill) && !string.IsNullOrEmpty(this.effects[index].add_card_skill_buff_awake))
            return true;
        }
      }
      return false;
    }

    public bool IsExistAddCardSkillBuffLvMax()
    {
      if (this.effects != null)
      {
        for (int index = 0; index < this.effects.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.effects[index].card_skill) && !string.IsNullOrEmpty(this.effects[index].add_card_skill_buff_lvmax) || !string.IsNullOrEmpty(this.effects[index].abil_iname) && !string.IsNullOrEmpty(this.effects[index].abil_iname_lvmax))
            return true;
        }
      }
      return false;
    }

    public string GetLocalizedTextFlavor()
    {
      return GameUtility.GetExternalLocalizedText("ConceptCard", this.iname, "flavor");
    }
  }
}
