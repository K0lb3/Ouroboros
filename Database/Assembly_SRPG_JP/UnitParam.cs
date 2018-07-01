// Decompiled with JetBrains decompiler
// Type: SRPG.UnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class UnitParam
  {
    public static int MASTER_QUEST_RARITY = 5;
    public static int MASTER_QUEST_LV = 1;
    public static string AI_TYPE_DEFAULT = "AI_ENEMY";
    private static EElement[] WeakElements = new EElement[7]
    {
      EElement.None,
      EElement.Water,
      EElement.Thunder,
      EElement.Fire,
      EElement.Wind,
      EElement.Dark,
      EElement.Shine
    };
    private static EElement[] ResistElements = new EElement[7]
    {
      EElement.None,
      EElement.Wind,
      EElement.Fire,
      EElement.Thunder,
      EElement.Water,
      EElement.None,
      EElement.None
    };
    public OString birth = (OString) ((string) null);
    public OString grow = (OString) ((string) null);
    public DateTime available_at = DateTime.MinValue;
    public UnitParam.Status ini_status = new UnitParam.Status();
    public UnitParam.Status max_status = new UnitParam.Status();
    public string[] leader_skills = new string[RarityParam.MAX_RARITY];
    public string[] recipes = new string[RarityParam.MAX_RARITY];
    public FlagManager flag;
    public string iname;
    public string name;
    public string model;
    public int birthID;
    public string[] jobsets;
    public string[] tags;
    public string piece;
    public string ability;
    public string ma_quest;
    public ESex sex;
    public byte rare;
    public byte raremax;
    public EUnitType type;
    public EElement element;
    public byte search;
    public short height;
    public short weight;
    public string image;
    public string voice;
    public UnitParam.NoJobStatus no_job_status;
    public string[] skins;
    private JobSetParam[] mJobSetCache;
    public string unlock_time;

    public bool IsHero()
    {
      return this.flag.Is(0);
    }

    public bool IsSummon()
    {
      return this.flag.Is(1);
    }

    public bool IsThrow()
    {
      return this.flag.Is(2);
    }

    public bool IsKnockBack()
    {
      return this.flag.Is(3);
    }

    public bool IsStopped()
    {
      if (this.type != EUnitType.Gem)
        return this.type == EUnitType.Treasure;
      return true;
    }

    public void CacheReferences(MasterParam master)
    {
      if (this.jobsets == null || this.mJobSetCache != null)
        return;
      this.mJobSetCache = new JobSetParam[this.jobsets.Length];
      for (int index = 0; index < this.jobsets.Length; ++index)
        this.mJobSetCache[index] = master.GetJobSetParam(this.jobsets[index]);
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
      this.iname = json.iname;
      this.name = json.name;
      this.model = json.mdl;
      this.grow = (OString) json.grow;
      this.piece = json.piece;
      this.birth = (OString) json.birth;
      this.birthID = json.birth_id;
      this.ability = json.ability;
      this.ma_quest = json.ma_quest;
      this.sex = (ESex) json.sex;
      this.rare = (byte) json.rare;
      this.raremax = (byte) json.raremax;
      this.type = (EUnitType) json.type;
      this.element = (EElement) json.elem;
      this.flag.Set(0, json.hero != 0);
      this.search = (byte) json.search;
      this.flag.Set(1, json.notsmn == 0);
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
      this.height = (short) json.height;
      this.weight = (short) json.weight;
      this.jobsets = (string[]) null;
      this.mJobSetCache = (JobSetParam[]) null;
      this.tags = (string[]) null;
      if (json.skins != null && json.skins.Length >= 1)
      {
        this.skins = new string[json.skins.Length];
        for (int index = 0; index < json.skins.Length; ++index)
          this.skins[index] = json.skins[index];
      }
      if (UnitParam.NoJobStatus.IsExistParam(json))
      {
        this.no_job_status = new UnitParam.NoJobStatus();
        this.no_job_status.SetParam(json);
      }
      if (this.type == EUnitType.EventUnit)
        return true;
      if (json.jobsets != null)
      {
        this.jobsets = new string[json.jobsets.Length];
        for (int index = 0; index < this.jobsets.Length; ++index)
          this.jobsets[index] = json.jobsets[index];
      }
      if (json.tag != null)
        this.tags = json.tag.Split(',');
      if (this.ini_status == null)
        this.ini_status = new UnitParam.Status();
      this.ini_status.SetParamIni(json);
      this.ini_status.SetEnchentParamIni(json);
      if (this.max_status == null)
        this.max_status = new UnitParam.Status();
      this.max_status.SetParamMax(json);
      this.max_status.SetEnchentParamMax(json);
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
      this.flag.Set(2, json.no_trw == 0);
      this.flag.Set(3, json.no_kb == 0);
      this.unlock_time = json.unlck_t;
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
      {
        growParam.CalcLevelCurveStatus(unitLv, ref status, unit.ini_status, unit.max_status);
      }
      else
      {
        unit.ini_status.param.CopyTo(status.param);
        if (unit.ini_status.enchant_resist != null)
          unit.ini_status.enchant_resist.CopyTo(status.enchant_resist);
      }
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      status.enchant_assist.esa_fire = (OShort) fixParam.EsaAssist;
      status.enchant_assist.esa_water = (OShort) fixParam.EsaAssist;
      status.enchant_assist.esa_wind = (OShort) fixParam.EsaAssist;
      status.enchant_assist.esa_thunder = (OShort) fixParam.EsaAssist;
      status.enchant_assist.esa_shine = (OShort) fixParam.EsaAssist;
      status.enchant_assist.esa_dark = (OShort) fixParam.EsaAssist;
      status.enchant_resist.esa_fire = (OShort) fixParam.EsaResist;
      status.enchant_resist.esa_water = (OShort) fixParam.EsaResist;
      status.enchant_resist.esa_wind = (OShort) fixParam.EsaResist;
      status.enchant_resist.esa_thunder = (OShort) fixParam.EsaResist;
      status.enchant_resist.esa_shine = (OShort) fixParam.EsaResist;
      status.enchant_resist.esa_dark = (OShort) fixParam.EsaResist;
      status.param.rec = (OShort) fixParam.IniValRec;
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

    public static string GetBirthplaceName(int birth_id)
    {
      if (birth_id == 0)
        return string.Empty;
      return LocalizedText.Get(string.Format("sys.BIRTH_PLACE_{0}", (object) birth_id));
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
      return !string.IsNullOrEmpty(this.piece) && this.IsSummon() && (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this) == null && MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.piece) >= this.GetUnlockNeedPieces()) && (this.CheckAvailable(TimeManager.ServerTime) && new UnitListWindow.Data(this).unlockable);
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
      return string.Empty;
    }

    public string GetJobId(int jobIndex)
    {
      JobSetParam jobSetFast = this.GetJobSetFast(jobIndex);
      if (jobSetFast != null)
        return jobSetFast.job;
      return string.Empty;
    }

    private enum FLAG_TYPE
    {
      HERO,
      SUMMON,
      THROW,
      KNOCK_BACK,
    }

    public class Status
    {
      public StatusParam param = new StatusParam();
      public EnchantParam enchant_resist;

      public void SetParamIni(JSON_UnitParam json)
      {
        this.param.hp = (OInt) json.hp;
        this.param.mp = (OShort) json.mp;
        this.param.atk = (OShort) json.atk;
        this.param.def = (OShort) json.def;
        this.param.mag = (OShort) json.mag;
        this.param.mnd = (OShort) json.mnd;
        this.param.dex = (OShort) json.dex;
        this.param.spd = (OShort) json.spd;
        this.param.cri = (OShort) json.cri;
        this.param.luk = (OShort) json.luk;
      }

      public void SetParamMax(JSON_UnitParam json)
      {
        this.param.hp = (OInt) json.mhp;
        this.param.mp = (OShort) json.mmp;
        this.param.atk = (OShort) json.matk;
        this.param.def = (OShort) json.mdef;
        this.param.mag = (OShort) json.mmag;
        this.param.mnd = (OShort) json.mmnd;
        this.param.dex = (OShort) json.mdex;
        this.param.spd = (OShort) json.mspd;
        this.param.cri = (OShort) json.mcri;
        this.param.luk = (OShort) json.mluk;
      }

      public void SetEnchentParamIni(JSON_UnitParam json)
      {
        if (!this.IsExistEnchentParamIni(json))
          return;
        this.enchant_resist = new EnchantParam();
        this.enchant_resist.poison = (OShort) json.rpo;
        this.enchant_resist.paralyse = (OShort) json.rpa;
        this.enchant_resist.stun = (OShort) json.rst;
        this.enchant_resist.sleep = (OShort) json.rsl;
        this.enchant_resist.charm = (OShort) json.rch;
        this.enchant_resist.stone = (OShort) json.rsn;
        this.enchant_resist.blind = (OShort) json.rbl;
        this.enchant_resist.notskl = (OShort) json.rns;
        this.enchant_resist.notmov = (OShort) json.rnm;
        this.enchant_resist.notatk = (OShort) json.rna;
        this.enchant_resist.zombie = (OShort) json.rzo;
        this.enchant_resist.death = (OShort) json.rde;
        this.enchant_resist.knockback = (OShort) json.rkn;
        this.enchant_resist.resist_debuff = (OShort) json.rdf;
        this.enchant_resist.berserk = (OShort) json.rbe;
        this.enchant_resist.stop = (OShort) json.rcs;
        this.enchant_resist.fast = (OShort) json.rcu;
        this.enchant_resist.slow = (OShort) json.rcd;
        this.enchant_resist.donsoku = (OShort) json.rdo;
        this.enchant_resist.rage = (OShort) json.rra;
        this.enchant_resist.single_attack = (OShort) json.rsa;
        this.enchant_resist.area_attack = (OShort) json.raa;
        this.enchant_resist.dec_ct = (OShort) json.rdc;
        this.enchant_resist.inc_ct = (OShort) json.ric;
      }

      private bool IsExistEnchentParamIni(JSON_UnitParam json)
      {
        return json.rpo != 0 || json.rpa != 0 || (json.rst != 0 || json.rsl != 0) || (json.rch != 0 || json.rsn != 0 || (json.rbl != 0 || json.rns != 0)) || (json.rnm != 0 || json.rna != 0 || (json.rzo != 0 || json.rde != 0) || (json.rkn != 0 || json.rdf != 0 || (json.rbe != 0 || json.rcs != 0))) || (json.rcu != 0 || json.rcd != 0 || (json.rdo != 0 || json.rra != 0) || (json.rsa != 0 || json.raa != 0 || (json.rdc != 0 || json.ric != 0)));
      }

      public void SetEnchentParamMax(JSON_UnitParam json)
      {
        if (!this.IsExistEnchentParamMax(json))
          return;
        this.enchant_resist = new EnchantParam();
        this.enchant_resist.poison = (OShort) json.mrpo;
        this.enchant_resist.paralyse = (OShort) json.mrpa;
        this.enchant_resist.stun = (OShort) json.mrst;
        this.enchant_resist.sleep = (OShort) json.mrsl;
        this.enchant_resist.charm = (OShort) json.mrch;
        this.enchant_resist.stone = (OShort) json.mrsn;
        this.enchant_resist.blind = (OShort) json.mrbl;
        this.enchant_resist.notskl = (OShort) json.mrns;
        this.enchant_resist.notmov = (OShort) json.mrnm;
        this.enchant_resist.notatk = (OShort) json.mrna;
        this.enchant_resist.zombie = (OShort) json.mrzo;
        this.enchant_resist.death = (OShort) json.mrde;
        this.enchant_resist.knockback = (OShort) json.mrkn;
        this.enchant_resist.resist_debuff = (OShort) json.mrdf;
        this.enchant_resist.berserk = (OShort) json.mrbe;
        this.enchant_resist.stop = (OShort) json.mrcs;
        this.enchant_resist.fast = (OShort) json.mrcu;
        this.enchant_resist.slow = (OShort) json.mrcd;
        this.enchant_resist.donsoku = (OShort) json.mrdo;
        this.enchant_resist.rage = (OShort) json.mrra;
        this.enchant_resist.single_attack = (OShort) json.mrsa;
        this.enchant_resist.area_attack = (OShort) json.mraa;
        this.enchant_resist.dec_ct = (OShort) json.mrdc;
        this.enchant_resist.inc_ct = (OShort) json.mric;
      }

      private bool IsExistEnchentParamMax(JSON_UnitParam json)
      {
        return json.mrpo != 0 || json.mrpa != 0 || (json.mrst != 0 || json.mrsl != 0) || (json.mrch != 0 || json.mrsn != 0 || (json.mrbl != 0 || json.mrns != 0)) || (json.mrnm != 0 || json.mrna != 0 || (json.mrzo != 0 || json.mrde != 0) || (json.mrkn != 0 || json.mrdf != 0 || (json.mrbe != 0 || json.mrcs != 0))) || (json.mrcu != 0 || json.mrcd != 0 || (json.mrdo != 0 || json.mrra != 0) || (json.mrsa != 0 || json.mraa != 0 || (json.mrdc != 0 || json.mric != 0)));
      }

      public BaseStatus CreateBaseStatus()
      {
        BaseStatus baseStatus = new BaseStatus();
        this.param.CopyTo(baseStatus.param);
        if (this.enchant_resist != null)
          this.enchant_resist.CopyTo(baseStatus.enchant_resist);
        return baseStatus;
      }
    }

    public class NoJobStatus
    {
      public string default_skill = string.Empty;
      public JobTypes jobtype;
      public RoleTypes role;
      public byte mov;
      public byte jmp;
      public int inimp;

      public void SetParam(JSON_UnitParam json)
      {
        this.default_skill = json.dskl;
        this.jobtype = (JobTypes) json.jt;
        this.role = (RoleTypes) json.role;
        this.mov = (byte) json.mov;
        this.jmp = (byte) json.jmp;
        this.inimp = json.inimp;
      }

      public static bool IsExistParam(JSON_UnitParam json)
      {
        return json.dskl != null || json.jt != 0 || (json.role != 0 || json.mov != 0) || (json.jmp != 0 || json.inimp != 0);
      }
    }
  }
}
