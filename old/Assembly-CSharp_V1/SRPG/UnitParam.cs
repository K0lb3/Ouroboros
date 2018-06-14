// Decompiled with JetBrains decompiler
// Type: SRPG.UnitParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class UnitParam
  {
    private static EElement[] WeakElements = new EElement[7]{ EElement.None, EElement.Water, EElement.Thunder, EElement.Fire, EElement.Wind, EElement.Dark, EElement.Shine };
    private static EElement[] ResistElements = new EElement[7]{ EElement.None, EElement.Wind, EElement.Fire, EElement.Thunder, EElement.Water, EElement.None, EElement.None };
    public OString ai = (OString) ((string) null);
    public OString birth = (OString) ((string) null);
    public OString grow = (OString) ((string) null);
    public OString piece = (OString) ((string) null);
    public OString skill = (OString) ((string) null);
    public OString ability = (OString) ((string) null);
    public OString ma_quest = (OString) ((string) null);
    public OInt sw = (OInt) 1;
    public OInt sh = (OInt) 1;
    public OInt rare = (OInt) 0;
    public OInt raremax = (OInt) 0;
    public OInt hero = (OInt) 0;
    public OInt search = (OInt) 0;
    public OBool stopped = (OBool) false;
    public DateTime available_at = DateTime.MinValue;
    public string[] leader_skills = new string[RarityParam.MAX_RARITY];
    public string[] recipes = new string[RarityParam.MAX_RARITY];
    private string localizedNameID;
    private string localizedTagID;
    private string tag;
    public int no;
    public string iname;
    public string name;
    public string model;
    public OString[] jobsets;
    public string[] tags;
    public ESex sex;
    public EUnitType type;
    public EElement element;
    public OInt height;
    public OInt weight;
    public bool summon;
    public string image;
    public string voice;
    public BaseStatus ini_status;
    public BaseStatus max_status;
    public OString default_skill;
    public OString[] default_abilities;
    public OString djob;
    public OString dbuki;
    public JobTypes jobtype;
    public RoleTypes role;
    public OInt mov;
    public OInt jmp;
    public OInt inimp;
    public OInt ma_rarity;
    public OInt ma_lv;
    public OString[] skins;
    public OString[] job_option_index;
    public OString[] job_images;
    public OString[] job_voices;
    public OBool is_throw;
    private JobSetParam[] mJobSetCache;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
    }

    public void Deserialize(string language, JSON_UnitParam json)
    {
      this.Deserialize(json);
      if (json.tag != null)
        this.tag = json.tag;
      this.localizeFields(language);
    }

    public void CacheReferences(MasterParam master)
    {
      if (this.jobsets == null || this.mJobSetCache != null)
        return;
      this.mJobSetCache = new JobSetParam[this.jobsets.Length];
      for (int index = 0; index < this.jobsets.Length; ++index)
        this.mJobSetCache[index] = master.GetJobSetParam((string) this.jobsets[index]);
    }

    public JobSetParam GetJobSetFast(int index)
    {
      if (this.mJobSetCache != null && 0 <= index && index < this.mJobSetCache.Length)
        return this.mJobSetCache[index];
      return (JobSetParam) null;
    }

    public string SexPrefix
    {
      get
      {
        return this.sex.ToPrefix();
      }
    }

    public bool Deserialize(JSON_UnitParam json)
    {
      if (json == null)
        return false;
      this.no = json.no;
      this.iname = json.iname;
      this.name = json.name;
      this.ai = (OString) json.ai;
      this.model = json.mdl;
      this.grow = (OString) json.grow;
      this.piece = (OString) json.piece;
      this.birth = (OString) json.birth;
      this.skill = (OString) json.skill;
      this.ability = (OString) json.ability;
      this.ma_quest = (OString) json.ma_quest;
      this.sw = (OInt) Math.Max(json.sw, 1);
      this.sh = (OInt) Math.Max(json.sh, 1);
      this.sex = (ESex) json.sex;
      this.rare = (OInt) json.rare;
      this.raremax = (OInt) json.raremax;
      this.type = (EUnitType) json.type;
      this.element = (EElement) json.elem;
      this.hero = (OInt) json.hero;
      this.search = (OInt) json.search;
      this.stopped = (OBool) (json.stop != 0);
      this.summon = json.notsmn == 0;
      if (!string.IsNullOrEmpty(json.available_at))
      {
        try
        {
          this.available_at = DateTime.Parse(json.available_at);
        }
        catch
        {
          this.available_at = DateTime.MaxValue;
        }
      }
      this.height = (OInt) json.height;
      this.weight = (OInt) json.weight;
      this.jobsets = (OString[]) null;
      this.mJobSetCache = (JobSetParam[]) null;
      this.tags = (string[]) null;
      this.jobtype = (JobTypes) json.jt;
      this.role = (RoleTypes) json.role;
      this.mov = (OInt) json.mov;
      this.jmp = (OInt) json.jmp;
      this.inimp = (OInt) json.inimp;
      this.ma_rarity = (OInt) json.ma_rarity;
      this.ma_lv = (OInt) json.ma_lv;
      if (json.skins != null && json.skins.Length >= 1)
      {
        this.skins = new OString[json.skins.Length];
        for (int index = 0; index < json.skins.Length; ++index)
          this.skins[index] = (OString) json.skins[index];
      }
      this.djob = (OString) json.djob;
      this.dbuki = (OString) json.dbuki;
      this.default_skill = (OString) json.dskl;
      this.default_abilities = (OString[]) null;
      if (json.dabi != null && json.dabi.Length > 0)
      {
        this.default_abilities = new OString[json.dabi.Length];
        for (int index = 0; index < json.dabi.Length; ++index)
          this.default_abilities[index] = (OString) json.dabi[index];
      }
      if (this.type == EUnitType.EventUnit)
        return true;
      if (json.jobsets != null)
      {
        this.jobsets = new OString[json.jobsets.Length];
        for (int index = 0; index < this.jobsets.Length; ++index)
          this.jobsets[index] = (OString) json.jobsets[index];
      }
      if (json.tag != null)
        this.tags = json.tag.Split(',');
      if (this.ini_status == null)
        this.ini_status = new BaseStatus();
      this.ini_status.param.hp = (OInt) json.hp;
      this.ini_status.param.mp = (OShort) json.mp;
      this.ini_status.param.atk = (OShort) json.atk;
      this.ini_status.param.def = (OShort) json.def;
      this.ini_status.param.mag = (OShort) json.mag;
      this.ini_status.param.mnd = (OShort) json.mnd;
      this.ini_status.param.dex = (OShort) json.dex;
      this.ini_status.param.spd = (OShort) json.spd;
      this.ini_status.param.cri = (OShort) json.cri;
      this.ini_status.param.luk = (OShort) json.luk;
      this.ini_status.enchant_resist.poison = (OInt) json.rpo;
      this.ini_status.enchant_resist.paralyse = (OInt) json.rpa;
      this.ini_status.enchant_resist.stun = (OInt) json.rst;
      this.ini_status.enchant_resist.sleep = (OInt) json.rsl;
      this.ini_status.enchant_resist.charm = (OInt) json.rch;
      this.ini_status.enchant_resist.stone = (OInt) json.rsn;
      this.ini_status.enchant_resist.blind = (OInt) json.rbl;
      this.ini_status.enchant_resist.notskl = (OInt) json.rns;
      this.ini_status.enchant_resist.notmov = (OInt) json.rnm;
      this.ini_status.enchant_resist.notatk = (OInt) json.rna;
      this.ini_status.enchant_resist.zombie = (OInt) json.rzo;
      this.ini_status.enchant_resist.death = (OInt) json.rde;
      this.ini_status.enchant_resist.knockback = (OInt) json.rkn;
      this.ini_status.enchant_resist.resist_buff = (OInt) 0;
      this.ini_status.enchant_resist.resist_debuff = (OInt) json.rdf;
      this.ini_status.enchant_resist.berserk = (OInt) json.rbe;
      this.ini_status.enchant_resist.stop = (OInt) json.rcs;
      this.ini_status.enchant_resist.fast = (OInt) json.rcu;
      this.ini_status.enchant_resist.slow = (OInt) json.rcd;
      this.ini_status.enchant_resist.donsoku = (OInt) json.rdo;
      this.ini_status.enchant_resist.rage = (OInt) json.rra;
      if (this.max_status == null)
        this.max_status = new BaseStatus();
      this.max_status.param.hp = (OInt) json.mhp;
      this.max_status.param.mp = (OShort) json.mmp;
      this.max_status.param.atk = (OShort) json.matk;
      this.max_status.param.def = (OShort) json.mdef;
      this.max_status.param.mag = (OShort) json.mmag;
      this.max_status.param.mnd = (OShort) json.mmnd;
      this.max_status.param.dex = (OShort) json.mdex;
      this.max_status.param.spd = (OShort) json.mspd;
      this.max_status.param.cri = (OShort) json.mcri;
      this.max_status.param.luk = (OShort) json.mluk;
      this.max_status.enchant_resist.poison = (OInt) json.mrpo;
      this.max_status.enchant_resist.paralyse = (OInt) json.mrpa;
      this.max_status.enchant_resist.stun = (OInt) json.mrst;
      this.max_status.enchant_resist.sleep = (OInt) json.mrsl;
      this.max_status.enchant_resist.charm = (OInt) json.mrch;
      this.max_status.enchant_resist.stone = (OInt) json.mrsn;
      this.max_status.enchant_resist.blind = (OInt) json.mrbl;
      this.max_status.enchant_resist.notskl = (OInt) json.mrns;
      this.max_status.enchant_resist.notmov = (OInt) json.mrnm;
      this.max_status.enchant_resist.notatk = (OInt) json.mrna;
      this.max_status.enchant_resist.zombie = (OInt) json.mrzo;
      this.max_status.enchant_resist.death = (OInt) json.mrde;
      this.max_status.enchant_resist.knockback = (OInt) json.mrkn;
      this.max_status.enchant_resist.resist_buff = (OInt) 0;
      this.max_status.enchant_resist.resist_debuff = (OInt) json.mrdf;
      this.max_status.enchant_resist.berserk = (OInt) json.mrbe;
      this.max_status.enchant_resist.stop = (OInt) json.mrcs;
      this.max_status.enchant_resist.fast = (OInt) json.mrcu;
      this.max_status.enchant_resist.slow = (OInt) json.mrcd;
      this.max_status.enchant_resist.donsoku = (OInt) json.mrdo;
      this.max_status.enchant_resist.rage = (OInt) json.mrra;
      this.leader_skills[0] = json.ls1;
      this.leader_skills[1] = json.ls2;
      this.leader_skills[2] = json.ls3;
      this.leader_skills[3] = json.ls4;
      this.leader_skills[4] = json.ls5;
      this.leader_skills[5] = json.ls6;
      this.recipes[0] = json.recipe1;
      this.recipes[1] = json.recipe2;
      this.recipes[2] = json.recipe3;
      this.recipes[3] = json.recipe4;
      this.recipes[4] = json.recipe5;
      this.recipes[5] = json.recipe6;
      this.image = json.img;
      this.voice = json.vce;
      if (json.jidx != null)
      {
        this.job_option_index = new OString[json.jidx.Length];
        for (int index = 0; index < json.jidx.Length; ++index)
          this.job_option_index[index] = (OString) json.jidx[index];
      }
      if (json.jimgs != null)
      {
        this.job_images = new OString[json.jimgs.Length];
        for (int index = 0; index < json.jimgs.Length; ++index)
          this.job_images[index] = (OString) json.jimgs[index];
      }
      if (json.jvcs != null)
      {
        this.job_voices = new OString[json.jvcs.Length];
        for (int index = 0; index < json.jvcs.Length; ++index)
          this.job_voices[index] = (OString) json.jvcs[index];
      }
      this.is_throw = (OBool) (json.no_trw == 0);
      return true;
    }

    public RecipeParam GetRarityUpRecipe(int rarity)
    {
      if (this.recipes == null)
        return (RecipeParam) null;
      if (rarity < 0 || rarity >= RarityParam.MAX_RARITY || rarity >= (int) this.raremax)
        return (RecipeParam) null;
      string recipe = this.recipes[rarity];
      if (string.IsNullOrEmpty(recipe))
        return (RecipeParam) null;
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(recipe);
    }

    public static int GetLeaderSkillLevel(int rarity, int awakeLv)
    {
      return 1;
    }

    public static void CalcUnitLevelStatus(UnitParam unit, int unitLv, ref BaseStatus status)
    {
      GrowParam growParam = MonoSingleton<GameManager>.GetInstanceDirect().GetGrowParam((string) unit.grow);
      if (growParam != null && growParam.curve != null)
        growParam.CalcLevelCurveStatus(unitLv, ref status, unit.ini_status, unit.max_status);
      else
        unit.ini_status.CopyTo(status);
    }

    public static void CalcUnitElementStatus(EElement element, ref BaseStatus status)
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      int elementResistUpRate = (int) fixParam.ElementResistUpRate;
      int elementResistDownRate = (int) fixParam.ElementResistDownRate;
      EElement weakElement = UnitParam.GetWeakElement(element);
      EElement resistElement = UnitParam.GetResistElement(element);
      ElementParam elementResist1;
      EElement index1;
      (elementResist1 = status.element_resist)[index1 = weakElement] = (OShort) ((int) elementResist1[index1] + elementResistDownRate);
      ElementParam elementResist2;
      EElement index2;
      (elementResist2 = status.element_resist)[index2 = resistElement] = (OShort) ((int) elementResist2[index2] + elementResistUpRate);
    }

    public static EElement GetWeakElement(EElement type)
    {
      return UnitParam.WeakElements[(int) type];
    }

    public static EElement GetResistElement(EElement type)
    {
      return UnitParam.ResistElements[(int) type];
    }

    public int GetUnlockNeedPieces()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam((int) this.rare);
      if (rarityParam != null)
        return (int) rarityParam.UnitUnlockPieceNum;
      return 0;
    }

    public bool CheckEnableUnlock()
    {
      return !string.IsNullOrEmpty((string) this.piece) && this.summon && (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this) == null && MonoSingleton<GameManager>.Instance.Player.GetItemAmount((string) this.piece) >= this.GetUnlockNeedPieces());
    }

    public bool CheckAvailable(DateTime now)
    {
      return !(now < this.available_at);
    }

    public string GetJobVoice(string jobName)
    {
      return this.GetJobOptions(jobName, false);
    }

    public string GetJobImage(string jobName)
    {
      return this.GetJobOptions(jobName, true);
    }

    private string GetJobOptions(string jobName, bool isImage)
    {
      OString[] ostringArray = !isImage ? this.job_voices : this.job_images;
      if (string.IsNullOrEmpty(jobName) || ostringArray == null || this.job_option_index == null)
        return string.Empty;
      int index = Array.FindIndex<OString>(this.job_option_index, (Predicate<OString>) (p => (string) p == jobName));
      if (index < 0 || index >= ostringArray.Length)
        return string.Empty;
      return (string) ostringArray[index];
    }

    public string GetJobId(int jobIndex)
    {
      JobSetParam jobSetFast = this.GetJobSetFast(jobIndex);
      if (jobSetFast != null)
        return jobSetFast.job;
      return string.Empty;
    }
  }
}
