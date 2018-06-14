// Decompiled with JetBrains decompiler
// Type: SRPG.Unit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class Unit : BaseObject
  {
    public static readonly int MAX_AI = 2;
    public static OInt MAX_UNIT_CONDITION = (OInt) Enum.GetNames(typeof (EUnitCondition)).Length;
    private static OInt UNIT_INDEX = (OInt) 0;
    public static OInt UNIT_CAST_INDEX = (OInt) 0;
    public static readonly int[,] DIRECTION_OFFSETS = new int[4, 2]{ { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
    private static BaseStatus BuffWorkStatus = new BaseStatus();
    private static BaseStatus BuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus DebuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus PassiveWorkScaleStatus = new BaseStatus();
    private static BaseStatus BuffDupliScaleStatus = new BaseStatus();
    private static BaseStatus DebuffDupliScaleStatus = new BaseStatus();
    private OInt mUnitFlag = (OInt) 0;
    private OInt mCommandFlag = (OInt) 0;
    private OIntVector2 mGridPosition = new OIntVector2();
    private OIntVector2 mGridPositionTurnStart = new OIntVector2();
    private OInt mSearched = (OInt) 0;
    private BaseStatus mMaximumBaseStatus = new BaseStatus();
    private BaseStatus mMaximumStatus = new BaseStatus();
    private BaseStatus mCurrentStatus = new BaseStatus();
    private List<AIParam> mAI = new List<AIParam>(Unit.MAX_AI);
    private OInt mAITop = (OInt) 0;
    private AIActionTable mAIActionTable = new AIActionTable();
    private OInt mAIActionIndex = (OInt) 0;
    private OInt mAIActionTurnCount = (OInt) 0;
    private AIPatrolTable mAIPatrolTable = new AIPatrolTable();
    private OInt mAIPatrolIndex = (OInt) 0;
    private List<BuffAttachment> mBuffAttachments = new List<BuffAttachment>(8);
    private List<CondAttachment> mCondAttachments = new List<CondAttachment>(8);
    private Unit.UnitDrop mDrop = new Unit.UnitDrop();
    private Unit.UnitSteal mSteal = new Unit.UnitSteal();
    private List<Unit.UnitShield> mShields = new List<Unit.UnitShield>();
    private OBool mEntryTriggerAndCheck = (OBool) false;
    private OInt mWaitEntryClock = (OInt) 0;
    private OInt mMoveWaitTurn = (OInt) 0;
    private OInt mActionCount = (OInt) 0;
    private OInt mDeathCount = (OInt) 0;
    private OInt mAutoJewel = (OInt) 0;
    private Dictionary<SkillData, OInt> mSkillUseCount = new Dictionary<SkillData, OInt>();
    public const int DIRECTION_MAX = 4;
    private string mUnitName;
    private string mUniqueName;
    private UnitData mUnitData;
    private EUnitSide mSide;
    private OInt mTurnStartDir;
    private OInt mUnitIndex;
    private SkillData mAIForceSkill;
    private OInt mCurrentCondition;
    private OInt mDisableCondition;
    public Unit Target;
    public Grid TreasureGainTarget;
    public EUnitDirection Direction;
    public bool IsPartyMember;
    public bool IsSub;
    private EventTrigger mEventTrigger;
    private List<UnitEntryTrigger> mEntryTriggers;
    private OInt mChargeTime;
    private SkillData mCastSkill;
    private OInt mCastTime;
    private OInt mCastIndex;
    private Unit mUnitTarget;
    private Grid mGridTarget;
    private GridMap<bool> mCastSkillGridMap;
    private Unit mRageTarget;
    private Unit mGuardTarget;
    private OInt mGuardTurn;
    private string mParentUniqueName;
    private int mTowerStartHP;
    public List<OString> mNotifyUniqueNames;
    private int mKillCount;
    public int OwnerPlayerIndex;
    private int mMoveTurn;
    private bool mEntryUnit;
    private MySound.Voice mBattleVoice;

    public OInt AIActionIndex
    {
      get
      {
        return this.mAIActionIndex;
      }
    }

    public OInt AIActionTurnCount
    {
      get
      {
        return this.mAIActionTurnCount;
      }
    }

    public OInt AIPatrolIndex
    {
      get
      {
        return this.mAIPatrolIndex;
      }
    }

    public int Gems
    {
      set
      {
        this.CurrentStatus.param.mp = (OShort) Math.Max(Math.Min(value, (int) this.MaximumStatus.param.mp), 0);
      }
      get
      {
        return (int) this.CurrentStatus.param.mp;
      }
    }

    public int WaitClock
    {
      set
      {
        this.mWaitEntryClock = (OInt) value;
      }
      get
      {
        return (int) this.mWaitEntryClock;
      }
    }

    public int WaitMoveTurn
    {
      set
      {
        this.mMoveWaitTurn = (OInt) value;
      }
      get
      {
        return (int) this.mMoveWaitTurn;
      }
    }

    public Dictionary<SkillData, OInt> GetSkillUseCount()
    {
      return this.mSkillUseCount;
    }

    public int MoveTurn
    {
      get
      {
        return this.mMoveTurn;
      }
    }

    public bool EntryUnit
    {
      get
      {
        return this.mEntryUnit;
      }
    }

    public string UniqueName
    {
      get
      {
        return this.mUniqueName;
      }
    }

    public string UnitName
    {
      get
      {
        return this.mUnitName;
      }
      set
      {
        this.mUnitName = value;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public UnitParam UnitParam
    {
      get
      {
        return this.mUnitData.UnitParam;
      }
    }

    public EUnitType UnitType
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.type;
        return EUnitType.Unit;
      }
    }

    public int Lv
    {
      get
      {
        return this.mUnitData.Lv;
      }
    }

    public JobData Job
    {
      get
      {
        return this.mUnitData.CurrentJob;
      }
    }

    public SkillData LeaderSkill
    {
      get
      {
        return this.mUnitData.LeaderSkill;
      }
    }

    public List<AbilityData> BattleAbilitys
    {
      get
      {
        return this.mUnitData.BattleAbilitys;
      }
    }

    public List<SkillData> BattleSkills
    {
      get
      {
        return this.mUnitData.BattleSkills;
      }
    }

    public List<BuffAttachment> BuffAttachments
    {
      get
      {
        return this.mBuffAttachments;
      }
    }

    public List<CondAttachment> CondAttachments
    {
      get
      {
        return this.mCondAttachments;
      }
    }

    public EquipData[] CurrentEquips
    {
      get
      {
        return this.mUnitData.CurrentEquips;
      }
    }

    public AIParam AI
    {
      get
      {
        if (this.mAI.Count > 0)
          return this.mAI[(int) this.mAITop];
        return (AIParam) null;
      }
    }

    public SkillData AIForceSkill
    {
      get
      {
        return this.mAIForceSkill;
      }
    }

    public BaseStatus MaximumStatus
    {
      get
      {
        return this.mMaximumStatus;
      }
    }

    public BaseStatus CurrentStatus
    {
      get
      {
        return this.mCurrentStatus;
      }
    }

    public BaseStatus MaximumStatusWithMap
    {
      get
      {
        return this.mMaximumBaseStatus;
      }
    }

    public bool IsDead
    {
      get
      {
        if ((int) this.CurrentStatus.param.hp == 0)
          return (int) this.MaximumStatus.param.hp > 0;
        return false;
      }
    }

    public bool IsEntry
    {
      get
      {
        return this.IsUnitFlag(EUnitFlag.Entried);
      }
    }

    public bool IsControl
    {
      get
      {
        if (!this.IsEnableControlCondition())
          return false;
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus)
          return true;
        if (this.mSide == EUnitSide.Player)
          return this.IsPartyMember;
        return false;
      }
    }

    public bool IsGimmick
    {
      get
      {
        return this.UnitType != EUnitType.Unit;
      }
    }

    public bool IsDestructable
    {
      get
      {
        if (this.IsGimmick && (int) this.MaximumStatus.param.hp > 0)
          return !this.IsDead;
        return false;
      }
    }

    public bool IsIntoUnit
    {
      get
      {
        return this.mUnitData.IsIntoUnit;
      }
    }

    public bool IsJump
    {
      get
      {
        if (this.mCastSkill != null)
          return this.mCastSkill.CastType == ECastTypes.Jump;
        return false;
      }
    }

    public EUnitSide Side
    {
      get
      {
        return this.mSide;
      }
      set
      {
        this.mSide = value;
      }
    }

    public Unit.UnitDrop Drop
    {
      get
      {
        return this.mDrop;
      }
    }

    public Unit.UnitSteal Steal
    {
      get
      {
        return this.mSteal;
      }
    }

    public List<Unit.UnitShield> Shields
    {
      get
      {
        return this.mShields;
      }
    }

    public int DisableMoveGridHeight
    {
      get
      {
        return Math.Max(this.GetMoveHeight() + 1, BattleMap.MAP_FLOOR_HEIGHT);
      }
    }

    public EElement Element
    {
      get
      {
        return this.UnitData.Element;
      }
    }

    public JobTypes JobType
    {
      get
      {
        return this.UnitData.JobType;
      }
    }

    public RoleTypes RoleType
    {
      get
      {
        return this.UnitData.RoleType;
      }
    }

    public int x
    {
      get
      {
        return (int) this.mGridPosition.x;
      }
      set
      {
        this.mGridPosition.x = (OInt) value;
      }
    }

    public int y
    {
      get
      {
        return (int) this.mGridPosition.y;
      }
      set
      {
        this.mGridPosition.y = (OInt) value;
      }
    }

    public int startX
    {
      get
      {
        return (int) this.mGridPositionTurnStart.x;
      }
      set
      {
        this.mGridPositionTurnStart.x = (OInt) value;
      }
    }

    public int startY
    {
      get
      {
        return (int) this.mGridPositionTurnStart.y;
      }
      set
      {
        this.mGridPositionTurnStart.y = (OInt) value;
      }
    }

    public EUnitDirection startDir
    {
      get
      {
        return (EUnitDirection) (int) this.mTurnStartDir;
      }
      set
      {
        this.mTurnStartDir = (OInt) ((int) value);
      }
    }

    public int SizeX
    {
      get
      {
        if (this.UnitParam != null)
          return (int) this.UnitParam.sw;
        return 1;
      }
    }

    public int SizeY
    {
      get
      {
        if (this.UnitParam != null)
          return (int) this.UnitParam.sh;
        return 1;
      }
    }

    public EventTrigger EventTrigger
    {
      get
      {
        return this.mEventTrigger;
      }
    }

    public List<UnitEntryTrigger> EntryTriggers
    {
      get
      {
        return this.mEntryTriggers;
      }
    }

    public OBool IsEntryTriggerAndCheck
    {
      get
      {
        return this.mEntryTriggerAndCheck;
      }
    }

    public int ActionCount
    {
      get
      {
        return (int) this.mActionCount;
      }
    }

    public int DeathCount
    {
      get
      {
        return (int) this.mDeathCount;
      }
    }

    public int AutoJewel
    {
      get
      {
        return (int) this.mAutoJewel;
      }
    }

    public OInt ChargeTime
    {
      get
      {
        return this.mChargeTime;
      }
      set
      {
        this.mChargeTime = value;
      }
    }

    public OInt ChargeTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        if (fixParam == null)
          return (OInt) 0;
        return (OInt) SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.ChargeTimeRate], (int) fixParam.ChargeTimeMax);
      }
    }

    public SkillData CastSkill
    {
      get
      {
        return this.mCastSkill;
      }
    }

    public OInt CastTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        if (fixParam == null)
          return (OInt) 0;
        return (OInt) SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.CastTimeRate], (int) fixParam.ChargeTimeMax);
      }
    }

    public OInt CastTime
    {
      get
      {
        return this.mCastTime;
      }
    }

    public OInt CastIndex
    {
      get
      {
        return this.mCastIndex;
      }
    }

    public Unit UnitTarget
    {
      get
      {
        return this.mUnitTarget;
      }
    }

    public Grid GridTarget
    {
      get
      {
        return this.mGridTarget;
      }
    }

    public GridMap<bool> CastSkillGridMap
    {
      get
      {
        return this.mCastSkillGridMap;
      }
      set
      {
        this.mCastSkillGridMap = value;
      }
    }

    public Unit RageTarget
    {
      get
      {
        return this.mRageTarget;
      }
    }

    public OInt UnitIndex
    {
      get
      {
        return this.mUnitIndex;
      }
    }

    public string ParentUniqueName
    {
      get
      {
        return this.mParentUniqueName;
      }
    }

    public List<OString> NotifyUniqueNames
    {
      get
      {
        return this.mNotifyUniqueNames;
      }
    }

    public int TowerStartHP
    {
      get
      {
        return this.mTowerStartHP;
      }
      set
      {
        this.mTowerStartHP = value;
      }
    }

    public int KillCount
    {
      get
      {
        return this.mKillCount;
      }
      set
      {
        this.mKillCount = value;
      }
    }

    private void SetupStatus()
    {
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
        if (currentQuest != null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Inequality((Object) instance, (Object) null) && !string.IsNullOrEmpty(currentQuest.MapBuff))
          {
            BaseStatus status1 = new BaseStatus();
            BaseStatus status2 = new BaseStatus();
            BaseStatus status3 = new BaseStatus();
            BaseStatus status4 = new BaseStatus();
            BuffEffect buffEffect = BuffEffect.CreateBuffEffect(instance.MasterParam.GetBuffEffectParam(currentQuest.MapBuff), 0, 0);
            buffEffect.CalcBuffStatus(ref status1, BuffTypes.Buff, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status2, BuffTypes.Buff, SkillParamCalcTypes.Scale);
            buffEffect.CalcBuffStatus(ref status3, BuffTypes.Debuff, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status4, BuffTypes.Debuff, SkillParamCalcTypes.Scale);
            status2.Add(status3);
            dsc.AddRate(status2);
            dsc.Add(status1);
            dsc.Add(status3);
          }
        }
      }
      dsc.CopyTo(this.mMaximumBaseStatus);
      dsc.CopyTo(this.mMaximumStatus);
      dsc.CopyTo(this.mCurrentStatus);
    }

    public bool Setup(UnitData unitdata = null, UnitSetting setting = null, Unit.DropItem dropitem = null, Unit.DropItem stealitem = null)
    {
      if (setting == null)
        return false;
      if (setting is NPCSetting)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        Unit.\u003CSetup\u003Ec__AnonStorey292 setupCAnonStorey292 = new Unit.\u003CSetup\u003Ec__AnonStorey292();
        // ISSUE: reference to a compiler-generated field
        setupCAnonStorey292.npc = setting as NPCSetting;
        // ISSUE: reference to a compiler-generated field
        string iname1 = (string) setupCAnonStorey292.npc.iname;
        // ISSUE: reference to a compiler-generated field
        int unitLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp((int) setupCAnonStorey292.npc.lv);
        // ISSUE: reference to a compiler-generated field
        int rare = (int) setupCAnonStorey292.npc.rare;
        // ISSUE: reference to a compiler-generated field
        int awake = (int) setupCAnonStorey292.npc.awake;
        // ISSUE: reference to a compiler-generated field
        EElement elem = (EElement) (int) setupCAnonStorey292.npc.elem;
        this.mUnitData = new UnitData();
        if (!this.mUnitData.Setup(iname1, unitLevelExp, rare, awake, (string) null, 1, elem))
          return false;
        // ISSUE: reference to a compiler-generated field
        if (setupCAnonStorey292.npc.abilities != null)
        {
          // ISSUE: reference to a compiler-generated field
          for (int index1 = 0; index1 < setupCAnonStorey292.npc.abilities.Length; ++index1)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            Unit.\u003CSetup\u003Ec__AnonStorey28F setupCAnonStorey28F = new Unit.\u003CSetup\u003Ec__AnonStorey28F();
            // ISSUE: reference to a compiler-generated field
            string iname2 = (string) setupCAnonStorey292.npc.abilities[index1].iname;
            // ISSUE: reference to a compiler-generated field
            int rank = (int) setupCAnonStorey292.npc.abilities[index1].rank;
            int val1 = rank - 1;
            if (!string.IsNullOrEmpty(iname2))
            {
              // ISSUE: reference to a compiler-generated field
              setupCAnonStorey28F.ab_param = MonoSingleton<GameManager>.Instance.GetAbilityParam(iname2);
              // ISSUE: reference to a compiler-generated field
              if (setupCAnonStorey28F.ab_param != null)
              {
                // ISSUE: reference to a compiler-generated field
                if (setupCAnonStorey28F.ab_param.skills != null)
                {
                  // ISSUE: reference to a compiler-generated method
                  AbilityData abilityData = this.mUnitData.BattleAbilitys.Find(new Predicate<AbilityData>(setupCAnonStorey28F.\u003C\u003Em__332));
                  if (abilityData != null)
                  {
                    if (abilityData.Rank < rank)
                    {
                      // ISSUE: reference to a compiler-generated field
                      abilityData.Setup(this.mUnitData, abilityData.UniqueID, setupCAnonStorey28F.ab_param.iname, Math.Min(val1, 0));
                    }
                    else
                      continue;
                  }
                  else
                  {
                    abilityData = new AbilityData();
                    // ISSUE: reference to a compiler-generated field
                    abilityData.Setup(this.mUnitData, (long) this.mUnitData.BattleAbilitys.Count, setupCAnonStorey28F.ab_param.iname, Math.Min(val1, 0));
                    this.mUnitData.BattleAbilitys.Add(abilityData);
                  }
                  abilityData.UpdateLearningsSkill(false);
                  // ISSUE: object of a compiler-generated type is created
                  // ISSUE: variable of a compiler-generated type
                  Unit.\u003CSetup\u003Ec__AnonStorey290 setupCAnonStorey290 = new Unit.\u003CSetup\u003Ec__AnonStorey290();
                  // ISSUE: reference to a compiler-generated field
                  setupCAnonStorey290.\u003C\u003Ef__ref\u0024655 = setupCAnonStorey28F;
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  for (setupCAnonStorey290.j = 0; setupCAnonStorey290.j < setupCAnonStorey28F.ab_param.skills.Length; ++setupCAnonStorey290.j)
                  {
                    // ISSUE: reference to a compiler-generated method
                    SkillData skillData = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(setupCAnonStorey290.\u003C\u003Em__333));
                    if (skillData == null)
                    {
                      skillData = new SkillData();
                      this.mUnitData.BattleSkills.Add(skillData);
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    skillData.Setup(setupCAnonStorey28F.ab_param.skills[setupCAnonStorey290.j].iname, rank, abilityData.GetRankMaxCap(), (MasterParam) null);
                  }
                }
                // ISSUE: reference to a compiler-generated field
                if (setupCAnonStorey292.npc.abilities[index1].skills != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  for (int index2 = 0; index2 < setupCAnonStorey292.npc.abilities[index1].skills.Length; ++index2)
                  {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: variable of a compiler-generated type
                    Unit.\u003CSetup\u003Ec__AnonStorey291 setupCAnonStorey291 = new Unit.\u003CSetup\u003Ec__AnonStorey291();
                    // ISSUE: reference to a compiler-generated field
                    if (setupCAnonStorey292.npc.abilities[index1].skills[index2] != null)
                    {
                      // ISSUE: reference to a compiler-generated field
                      // ISSUE: reference to a compiler-generated field
                      setupCAnonStorey291.sk_iname = (string) setupCAnonStorey292.npc.abilities[index1].skills[index2].iname;
                      // ISSUE: reference to a compiler-generated method
                      SkillData skillData = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(setupCAnonStorey291.\u003C\u003Em__334));
                      if (skillData != null)
                      {
                        // ISSUE: reference to a compiler-generated field
                        skillData.UseRate = setupCAnonStorey292.npc.abilities[index1].skills[index2].rate;
                        // ISSUE: reference to a compiler-generated field
                        skillData.UseCondition = setupCAnonStorey292.npc.abilities[index1].skills[index2].cond;
                      }
                    }
                  }
                }
              }
            }
          }
          this.mUnitData.CalcStatus();
          this.mAIActionTable.Clear();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (setupCAnonStorey292.npc.acttbl != null && setupCAnonStorey292.npc.acttbl.actions != null)
          {
            // ISSUE: reference to a compiler-generated field
            setupCAnonStorey292.npc.acttbl.CopyTo(this.mAIActionTable);
          }
          this.mAIPatrolTable.Clear();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (setupCAnonStorey292.npc.patrol != null && setupCAnonStorey292.npc.patrol.routes != null && setupCAnonStorey292.npc.patrol.routes.Length > 0)
          {
            // ISSUE: reference to a compiler-generated field
            setupCAnonStorey292.npc.patrol.CopyTo(this.mAIPatrolTable);
          }
          // ISSUE: reference to a compiler-generated field
          if (!string.IsNullOrEmpty((string) setupCAnonStorey292.npc.fskl))
          {
            // ISSUE: reference to a compiler-generated method
            this.mAIForceSkill = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(setupCAnonStorey292.\u003C\u003Em__335));
          }
        }
        if (dropitem != null)
        {
          this.mDrop.items.Clear();
          this.mDrop.items.Add(dropitem);
        }
        // ISSUE: reference to a compiler-generated field
        this.mDrop.exp = setupCAnonStorey292.npc.exp;
        // ISSUE: reference to a compiler-generated field
        this.mDrop.gems = setupCAnonStorey292.npc.gems;
        // ISSUE: reference to a compiler-generated field
        this.mDrop.gold = setupCAnonStorey292.npc.gold;
        this.mDrop.gained = false;
        if (this.UnitType == EUnitType.Gem)
          this.mDrop.gems = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue;
        if (stealitem != null)
        {
          this.mSteal.items.Clear();
          this.mSteal.items.Add(stealitem);
        }
        this.mSteal.is_item_steeled = false;
        this.mSteal.is_gold_steeled = false;
        // ISSUE: reference to a compiler-generated field
        this.mSearched = setupCAnonStorey292.npc.search;
        // ISSUE: reference to a compiler-generated field
        this.SetUnitFlag(EUnitFlag.DamagedActionStart, (int) setupCAnonStorey292.npc.notice_damage != 0);
        // ISSUE: reference to a compiler-generated field
        if (setupCAnonStorey292.npc.notice_members != null)
        {
          // ISSUE: reference to a compiler-generated field
          this.mNotifyUniqueNames = new List<OString>((IEnumerable<OString>) setupCAnonStorey292.npc.notice_members);
        }
        if ((int) setting.side == 0)
        {
          // ISSUE: reference to a compiler-generated field
          this.IsPartyMember = (bool) setupCAnonStorey292.npc.control;
        }
      }
      else
      {
        if (unitdata == null)
          return false;
        this.mUnitData = unitdata;
        this.mSearched = this.UnitParam.search;
      }
      this.mUnitName = this.UnitParam.name;
      this.mUniqueName = (string) setting.uniqname;
      this.mParentUniqueName = (string) setting.parent;
      this.SetupStatus();
      this.mSide = !this.IsGimmick ? (EUnitSide) (int) setting.side : EUnitSide.Neutral;
      string ai;
      if (!string.IsNullOrEmpty((string) setting.ai))
      {
        ai = (string) setting.ai;
      }
      else
      {
        ai = (string) this.UnitParam.ai;
        JobData currentJob = this.mUnitData.CurrentJob;
        if (currentJob != null && !string.IsNullOrEmpty(currentJob.Param.ai))
          ai = currentJob.Param.ai;
      }
      if (!string.IsNullOrEmpty(ai))
      {
        AIParam aiParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAIParam(ai);
        DebugUtility.Assert(aiParam != null, "ai " + ai + " not found");
        this.mAI.Add(aiParam);
      }
      if (setting.trigger != null)
      {
        this.mEventTrigger = new EventTrigger();
        this.mEventTrigger.Setup(setting.trigger);
      }
      if (setting.entries != null && setting.entries.Count > 0)
      {
        this.mEntryTriggers = new List<UnitEntryTrigger>((IEnumerable<UnitEntryTrigger>) setting.entries);
        this.mEntryTriggerAndCheck = (OBool) ((int) setting.entries_and != 0);
      }
      this.x = (int) setting.pos.x;
      this.y = (int) setting.pos.y;
      this.Direction = (EUnitDirection) (int) setting.dir;
      this.mWaitEntryClock = setting.waitEntryClock;
      this.mMoveWaitTurn = setting.waitMoveTurn;
      if (setting.DisableFirceVoice)
        this.SetUnitFlag(EUnitFlag.DisableFirstVoice, true);
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.SetUnitFlag(EUnitFlag.Entried, this.CheckEnableEntry());
      this.IsInitialized = true;
      return true;
    }

    public bool SetupDummy(EUnitSide side, string iname, int lv = 1, int rare = 0, int awake = 0, string job_iname = null, int job_rank = 1)
    {
      int unitLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(lv);
      this.mUnitData = new UnitData();
      if (!this.mUnitData.Setup(iname, unitLevelExp, rare, awake, job_iname, job_rank, EElement.None))
        return false;
      this.mSide = side;
      this.mUnitName = this.UnitParam.name;
      this.mUnitData.Status.CopyTo(this.mMaximumStatus);
      this.mUnitData.Status.CopyTo(this.mCurrentStatus);
      this.SetUnitFlag(EUnitFlag.Entried, false);
      this.SetUnitFlag(EUnitFlag.Entried, this.CheckEnableEntry());
      this.SetupStatus();
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.IsInitialized = true;
      return true;
    }

    public bool SetupResume(MultiPlayResumeUnitData data, Unit target, Unit rage, Unit casttarget, List<MultiPlayResumeSkillData> buffskill, List<MultiPlayResumeSkillData> condskill)
    {
      if (this.UnitName != data.name)
        return false;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if (Object.op_Inequality((Object) instance, (Object) null))
        battleCore = instance.Battle;
      BaseStatus baseStatus1 = new BaseStatus();
      BaseStatus baseStatus2 = new BaseStatus();
      BaseStatus baseStatus3 = new BaseStatus();
      BaseStatus baseStatus4 = new BaseStatus();
      this.CurrentStatus.param.hp = (OInt) data.hp;
      this.Gems = data.gem;
      this.Direction = (EUnitDirection) data.dir;
      int x1 = data.x;
      this.x = x1;
      this.startX = x1;
      int y1 = data.y;
      this.y = y1;
      this.startY = y1;
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.Target = target;
      this.mRageTarget = rage;
      Dictionary<SkillData, OInt>.KeyCollection keys = this.mSkillUseCount.Keys;
      if (!string.IsNullOrEmpty(data.castskill))
      {
        this.mCastSkill = this.GetSkillData(data.castskill);
        this.mCastTime = (OInt) data.casttime;
        this.mCastIndex = (OInt) data.castindex;
        this.mUnitTarget = casttarget;
        if (data.targetgrid != null)
        {
          Grid grid = new Grid();
          grid.x = data.targetgrid.x;
          grid.y = data.targetgrid.y;
          grid.height = data.targetgrid.height;
          grid.cost = data.targetgrid.cost;
          grid.step = data.targetgrid.step;
          grid.tile = data.targetgrid.tile;
          if (data.targetgrid.geo != null)
          {
            grid.geo = new GeoParam();
            grid.geo.iname = data.targetgrid.geo.iname;
            grid.geo.name = data.targetgrid.geo.name;
            grid.geo.cost = data.targetgrid.geo.cost;
            grid.geo.DisableStopped = data.targetgrid.geo.DisableStopped;
          }
          this.mGridTarget = grid;
        }
        if (data.castgrid != null)
        {
          GridMap<bool> gridMap = new GridMap<bool>(data.grid_w, data.grid_h);
          for (int x2 = 0; x2 < data.grid_w; ++x2)
          {
            for (int y2 = 0; y2 < data.grid_h; ++y2)
              gridMap.set(x2, y2, data.castgrid[x2 + y2 * data.grid_w] != 0);
          }
          this.CastSkillGridMap = gridMap;
        }
      }
      this.mChargeTime = (OInt) data.chargetime;
      this.mDeathCount = (OInt) data.deathcnt;
      this.mAutoJewel = (OInt) data.autojewel;
      this.mWaitEntryClock = (OInt) data.waitturn;
      this.mMoveWaitTurn = (OInt) data.moveturn;
      this.mActionCount = (OInt) data.actcnt;
      this.mAIActionIndex = (OInt) data.aiindex;
      this.mAIActionTurnCount = (OInt) data.aiturn;
      this.mAIPatrolIndex = (OInt) data.aipatrol;
      this.mMoveTurn = data.turncnt;
      if (this.mEventTrigger != null)
        this.mEventTrigger.Count = data.trgcnt;
      this.mKillCount = data.killcnt;
      for (int index1 = 0; index1 < data.skillname.Length; ++index1)
      {
        for (int index2 = 0; index2 < keys.Count; ++index2)
        {
          if (keys.ToArray<SkillData>()[index2].SkillParam.iname == data.skillname[index1])
          {
            this.mSkillUseCount[keys.ToArray<SkillData>()[index2]] = (OInt) data.skillcnt[index1];
            break;
          }
        }
      }
      if (data.buff != null && battleCore != null)
      {
        for (int index1 = 0; index1 < data.buff.Length; ++index1)
        {
          BuffAttachment dba = (BuffAttachment) null;
          if (buffskill[index1].skill != null)
          {
            SkillData skill = buffskill[index1].skill;
            SkillEffectTargets skilltarget = (SkillEffectTargets) data.buff[index1].skilltarget;
            BuffEffect buffEffect = skill.GetBuffEffect(skilltarget);
            if (buffEffect != null)
            {
              int turn = data.buff[index1].turn;
              ESkillCondition cond = buffEffect.param.cond;
              EffectCheckTargets chkTarget = buffEffect.param.chk_target;
              EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
              int duplicateCount = skill.DuplicateCount;
              baseStatus1.Clear();
              baseStatus2.Clear();
              baseStatus3.Clear();
              baseStatus4.Clear();
              skill.BuffSkill(skill.Timing, baseStatus1, baseStatus2, baseStatus3, baseStatus4, (RandXorshift) null, skilltarget);
              if (data.buff[index1].type == 0)
              {
                if (buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add))
                  dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, (BuffTypes) data.buff[index1].type, (SkillParamCalcTypes) data.buff[index1].calc, baseStatus1, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                if (buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale))
                  dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, (BuffTypes) data.buff[index1].type, (SkillParamCalcTypes) data.buff[index1].calc, baseStatus2, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
              }
              else
              {
                if (buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add))
                  dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Add, baseStatus3, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                if (buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale))
                  dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Scale, baseStatus4, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
              }
              if (dba != null)
              {
                dba.turn = (OInt) turn;
                if (dba.skill != null)
                {
                  int num1 = 1;
                  if (dba.DuplicateCount > 0)
                  {
                    int num2 = 0;
                    for (int index2 = 0; index2 < this.BuffAttachments.Count; ++index2)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index2], dba))
                        ++num2;
                    }
                    num1 = Math.Max(1 + num2 - dba.DuplicateCount, 0);
                  }
                  if (num1 > 0)
                  {
                    int num2 = 0;
                    for (int index2 = 0; index2 < this.BuffAttachments.Count; ++index2)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index2], dba))
                      {
                        this.BuffAttachments.RemoveAt(index2--);
                        if (++num2 >= num1)
                          break;
                      }
                    }
                  }
                }
                this.BuffAttachments.Add(dba);
              }
            }
          }
        }
      }
      if (data.cond != null && battleCore != null)
      {
        for (int index1 = 0; index1 < data.cond.Length; ++index1)
        {
          CondAttachment condAttachment1 = battleCore.CreateCondAttachment(condskill[index1].user, this, condskill[index1].skill, (ConditionEffectTypes) data.cond[index1].type, (ESkillCondition) data.cond[index1].condition, (EUnitCondition) data.cond[index1].calc, EffectCheckTargets.Target, (EffectCheckTimings) data.cond[index1].timing, data.cond[index1].turn, data.cond[index1].passive, data.cond[index1].curse != 0);
          if (condAttachment1 != null)
          {
            condAttachment1.CheckTarget = condskill[index1].check;
            if (condAttachment1.skill != null)
            {
              for (int index2 = 0; index2 < this.CondAttachments.Count; ++index2)
              {
                CondAttachment condAttachment2 = this.CondAttachments[index2];
                if (condAttachment2.skill != null && condAttachment2.skill.SkillID == condAttachment1.skill.SkillID && (condAttachment2.CondType == condAttachment1.CondType && condAttachment2.Condition == condAttachment1.Condition))
                {
                  List<CondAttachment> condAttachments = this.CondAttachments;
                  int index3 = index2;
                  int num = index3 - 1;
                  condAttachments.RemoveAt(index3);
                  break;
                }
              }
            }
            this.CondAttachments.Add(condAttachment1);
          }
        }
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus(false);
      this.SetUnitFlag(EUnitFlag.Searched, data.search != 0);
      this.mEntryUnit = data.entry != 0;
      return true;
    }

    public string[] GetTags()
    {
      return this.UnitParam.tags;
    }

    public bool CheckExistMap()
    {
      if (!this.IsDead && this.IsEntry)
        return !this.IsSub;
      return false;
    }

    public bool CheckCollision(Grid grid)
    {
      return this.CheckCollision(grid.x, grid.y);
    }

    public bool CheckCollision(int cx, int cy)
    {
      if (!this.CheckExistMap())
        return false;
      for (int index1 = 0; index1 < this.SizeY; ++index1)
      {
        for (int index2 = 0; index2 < this.SizeX; ++index2)
        {
          if (this.x + index2 == cx && this.y + index1 == cy)
            return true;
        }
      }
      return false;
    }

    public bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      return this.CheckExistMap() && x1 >= this.x && (this.x + this.SizeX - 1 >= x0 && y1 >= this.y) && this.y + this.SizeY - 1 >= y0;
    }

    private bool isSameBuffAttachment(BuffAttachment sba, BuffAttachment dba)
    {
      return sba != null && dba != null && (sba.skill != null && dba.skill != null) && (sba.skill.SkillID == dba.skill.SkillID && sba.BuffType == dba.BuffType && (sba.CalcType == dba.CalcType && sba.CheckTiming == dba.CheckTiming));
    }

    public int GetBuffAttachmentDuplicateCount(BuffAttachment buff)
    {
      int num = 0;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.isSameBuffAttachment(this.BuffAttachments[index], buff))
          ++num;
      }
      return num;
    }

    public void CalcCurrentStatus(bool is_initialized = false)
    {
      int hp = (int) this.CurrentStatus.param.hp;
      int mp = (int) this.CurrentStatus.param.mp;
      Unit.BuffWorkStatus.Clear();
      Unit.BuffWorkScaleStatus.Clear();
      Unit.DebuffWorkScaleStatus.Clear();
      Unit.PassiveWorkScaleStatus.Clear();
      Unit.BuffDupliScaleStatus.Clear();
      Unit.DebuffDupliScaleStatus.Clear();
      this.mAutoJewel = (OInt) 0;
      this.mMaximumBaseStatus.CopyTo(this.MaximumStatus);
      int val1 = 0;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance) && instance.Battle != null)
        val1 = instance.Battle.GetDeadCountEnemy();
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      for (int index1 = 0; index1 < this.BuffAttachments.Count; ++index1)
      {
        BuffAttachment buffAttachment1 = this.BuffAttachments[index1];
        if (!buffAttachment1.IsCalculated)
        {
          SkillData skill = buffAttachment1.skill;
          BaseStatus baseStatus = new BaseStatus(buffAttachment1.status);
          if (((bool) buffAttachment1.IsPassive || skill != null && skill.IsPassiveSkill() || this.IsEnableBuffEffect(buffAttachment1.BuffType)) && (buffAttachment1.UseCondition == ESkillCondition.None || buffAttachment1.UseCondition != ESkillCondition.Dying || this.IsDying()))
          {
            if (buffAttachment1.Param != null)
            {
              int num = 0;
              if (buffAttachment1.Param.mAppType == EAppType.AllKillCount)
              {
                if (val1 != 0)
                  num = Math.Min(val1, buffAttachment1.Param.mAppMct) - 1;
                else
                  continue;
              }
              else if (buffAttachment1.Param.mAppType == EAppType.SelfKillCount)
              {
                if (this.mKillCount != 0)
                  num = Math.Min(this.mKillCount, buffAttachment1.Param.mAppMct) - 1;
                else
                  continue;
              }
              if (buffAttachment1.Param.mEffRange == EEffRange.SelfNearAlly && buffAttachment1.Param.mAppMct > 0)
              {
                int allyUnitNum = this.GetAllyUnitNum(buffAttachment1.user);
                if (allyUnitNum != 0)
                  num += Math.Min(allyUnitNum, buffAttachment1.Param.mAppMct) - 1;
                else
                  continue;
              }
              if (num > 0)
              {
                for (int index2 = 0; index2 < num; ++index2)
                  baseStatus.Add(buffAttachment1.status);
              }
            }
            int attachmentDuplicateCount = this.GetBuffAttachmentDuplicateCount(buffAttachment1);
            if (attachmentDuplicateCount > 1)
            {
              switch (buffAttachment1.CalcType)
              {
                case SkillParamCalcTypes.Add:
                case SkillParamCalcTypes.Fixed:
                  for (int index2 = 0; index2 < attachmentDuplicateCount; ++index2)
                    Unit.BuffWorkStatus.Add(baseStatus);
                  break;
                case SkillParamCalcTypes.Scale:
                  if (buffAttachment1.BuffType == BuffTypes.Buff)
                  {
                    Unit.BuffDupliScaleStatus.Clear();
                    for (int index2 = 0; index2 < attachmentDuplicateCount; ++index2)
                      Unit.BuffDupliScaleStatus.Add(baseStatus);
                    Unit.BuffWorkScaleStatus.ReplaceHighest(Unit.BuffDupliScaleStatus);
                    break;
                  }
                  Unit.DebuffDupliScaleStatus.Clear();
                  for (int index2 = 0; index2 < attachmentDuplicateCount; ++index2)
                    Unit.DebuffDupliScaleStatus.Add(baseStatus);
                  Unit.DebuffWorkScaleStatus.ReplaceLowest(Unit.DebuffDupliScaleStatus);
                  break;
              }
              for (int index2 = 0; index2 < this.BuffAttachments.Count; ++index2)
              {
                BuffAttachment buffAttachment2 = this.BuffAttachments[index2];
                if (this.isSameBuffAttachment(buffAttachment2, buffAttachment1))
                  buffAttachment2.IsCalculated = true;
              }
            }
            else
            {
              switch (buffAttachment1.CalcType)
              {
                case SkillParamCalcTypes.Add:
                  Unit.BuffWorkStatus.Add(baseStatus);
                  continue;
                case SkillParamCalcTypes.Scale:
                  if (buffAttachment1.skill != null && buffAttachment1.skill.IsPassiveSkill())
                  {
                    Unit.PassiveWorkScaleStatus.Add(baseStatus);
                    continue;
                  }
                  if (buffAttachment1.BuffType == BuffTypes.Buff)
                  {
                    Unit.BuffWorkScaleStatus.ReplaceHighest(baseStatus);
                    continue;
                  }
                  Unit.DebuffWorkScaleStatus.ReplaceLowest(baseStatus);
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      if (this.IsUnitCondition(EUnitCondition.Blindness))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Blindness))
          {
            int num3 = 0;
            int num4 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                int num5 = (int) ((int) condEffect1.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect1.param.v_blink_hit);
                int num6 = (int) ((int) condEffect1.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect1.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
                flag = true;
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                int num5 = (int) ((int) condEffect2.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect2.param.v_blink_hit);
                int num6 = (int) ((int) condEffect2.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect2.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
                flag = true;
              }
            }
            else
            {
              if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs((int) fixParam.BlindnessHitRate) + Math.Abs((int) fixParam.BlindnessAvoidRate))
              {
                num3 = (int) fixParam.BlindnessHitRate;
                num4 = (int) fixParam.BlindnessAvoidRate;
              }
              flag = true;
            }
            if (Math.Abs(num1) + Math.Abs(num2) < Math.Abs(num3) + Math.Abs(num4))
            {
              num1 = num3;
              num2 = num4;
            }
          }
        }
        if (!flag)
        {
          num1 = (int) fixParam.BlindnessHitRate;
          num2 = (int) fixParam.BlindnessAvoidRate;
        }
        BaseStatus buffWorkStatus1;
        BattleBonus index1;
        (buffWorkStatus1 = Unit.BuffWorkStatus)[index1 = BattleBonus.HitRate] = (OInt) ((int) buffWorkStatus1[index1] + num1);
        BaseStatus buffWorkStatus2;
        BattleBonus index2;
        (buffWorkStatus2 = Unit.BuffWorkStatus)[index2 = BattleBonus.AvoidRate] = (OInt) ((int) buffWorkStatus2[index2] + num2);
      }
      if (this.IsUnitCondition(EUnitCondition.Berserk))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Berserk))
          {
            int num3 = 0;
            int num4 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                int num5 = (int) ((int) condEffect1.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect1.param.v_berserk_atk);
                int num6 = (int) ((int) condEffect1.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect1.param.v_berserk_def);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                int num5 = (int) ((int) condEffect2.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect2.param.v_berserk_atk);
                int num6 = (int) ((int) condEffect2.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect2.param.v_berserk_def);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
              }
            }
            else if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs((int) fixParam.BerserkAtkRate) + Math.Abs((int) fixParam.BerserkDefRate))
            {
              num3 = (int) fixParam.BerserkAtkRate;
              num4 = (int) fixParam.BerserkDefRate;
            }
            if (Math.Abs(num1) + Math.Abs(num2) < Math.Abs(num3) + Math.Abs(num4))
            {
              num1 = num3;
              num2 = num4;
            }
            flag = true;
          }
        }
        if (!flag)
        {
          num1 = (int) fixParam.BerserkAtkRate;
          num2 = (int) fixParam.BerserkDefRate;
        }
        BaseStatus passiveWorkScaleStatus1;
        StatusTypes index1;
        (passiveWorkScaleStatus1 = Unit.PassiveWorkScaleStatus)[index1 = StatusTypes.Atk] = (OInt) ((int) passiveWorkScaleStatus1[index1] + num1);
        BaseStatus passiveWorkScaleStatus2;
        StatusTypes index2;
        (passiveWorkScaleStatus2 = Unit.PassiveWorkScaleStatus)[index2 = StatusTypes.Mag] = (OInt) ((int) passiveWorkScaleStatus2[index2] + num1);
        BaseStatus passiveWorkScaleStatus3;
        StatusTypes index3;
        (passiveWorkScaleStatus3 = Unit.PassiveWorkScaleStatus)[index3 = StatusTypes.Def] = (OInt) ((int) passiveWorkScaleStatus3[index3] + num2);
        BaseStatus passiveWorkScaleStatus4;
        StatusTypes index4;
        (passiveWorkScaleStatus4 = Unit.PassiveWorkScaleStatus)[index4 = StatusTypes.Mnd] = (OInt) ((int) passiveWorkScaleStatus4[index4] + num2);
      }
      Unit.BuffWorkScaleStatus.Add(Unit.DebuffWorkScaleStatus);
      Unit.BuffWorkScaleStatus.Add(Unit.PassiveWorkScaleStatus);
      this.MaximumStatus.Add(Unit.BuffWorkStatus);
      this.MaximumStatus.AddRate(Unit.BuffWorkScaleStatus);
      this.MaximumStatus.CopyTo(this.CurrentStatus);
      this.mAutoJewel = this.CurrentStatus[BattleBonus.AutoJewel];
      if (is_initialized)
      {
        this.CurrentStatus.param.mp = (OShort) this.GetStartGems();
      }
      else
      {
        if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
          GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
        this.CurrentStatus.param.hp = (OInt) Math.Min(hp, (int) this.MaximumStatus.param.hp);
        this.CurrentStatus.param.mp = (OShort) Math.Min(mp, (int) this.MaximumStatus.param.mp);
        if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
          GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      }
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if ((int) this.mShields[index].hp == 0)
          this.mShields.RemoveAt(index--);
      }
      if (this.mRageTarget == null || !this.mRageTarget.IsDeadCondition())
        return;
      this.CureCondEffects(EUnitCondition.Rage, true, true);
    }

    public void SetUnitFlag(EUnitFlag tgt, bool sw)
    {
      if (sw)
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) ((int) ((EUnitFlag) (int) unit.mUnitFlag | tgt));
      }
      else
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) ((int) ((EUnitFlag) (int) unit.mUnitFlag & ~tgt));
      }
    }

    public bool IsUnitFlag(EUnitFlag tgt)
    {
      return ((EUnitFlag) (int) this.mUnitFlag & tgt) != (EUnitFlag) 0;
    }

    public void SetCommandFlag(EUnitCommandFlag tgt, bool sw)
    {
      if (sw)
      {
        Unit unit = this;
        unit.mCommandFlag = (OInt) ((int) ((EUnitCommandFlag) (int) unit.mCommandFlag | tgt));
      }
      else
      {
        Unit unit = this;
        unit.mCommandFlag = (OInt) ((int) ((EUnitCommandFlag) (int) unit.mCommandFlag & ~tgt));
      }
    }

    public bool IsCommandFlag(EUnitCommandFlag tgt)
    {
      return ((EUnitCommandFlag) (int) this.mCommandFlag & tgt) != (EUnitCommandFlag) 0;
    }

    public bool IsDying()
    {
      return (int) this.MaximumStatus.param.hp * (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.HpDyingRate / 100 >= (int) this.CurrentStatus.param.hp;
    }

    public int GetBaseAvoidRate()
    {
      if (this.Job != null)
        return this.Job.GetJobRankAvoidRate();
      return 0;
    }

    public int GetStartGems()
    {
      int num = 100;
      return (int) this.MaximumStatus.param.mp * (this.Job != null ? num + this.Job.GetJobRankInitJewelRate() : num + (int) this.UnitParam.inimp) / 100 + (int) this.CurrentStatus.param.imp;
    }

    public int GetGoodSleepHpHealValue()
    {
      if (this.IsUnitCondition(EUnitCondition.DisableHeal))
        return 0;
      return Math.Max((int) this.MaximumStatus.param.hp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepHpHealRate / 100, 1);
    }

    public int GetGoodSleepMpHealValue()
    {
      return Math.Max((int) this.MaximumStatus.param.mp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepMpHealRate / 100, 1);
    }

    public void ClearSkillUseCount()
    {
      this.mSkillUseCount.Clear();
      for (int index1 = 0; index1 < this.BattleAbilitys.Count; ++index1)
      {
        AbilityData battleAbility = this.BattleAbilitys[index1];
        for (int index2 = 0; index2 < battleAbility.Skills.Count; ++index2)
        {
          SkillData skill = battleAbility.Skills[index2];
          if ((int) skill.SkillParam.count > 0 && !this.mSkillUseCount.ContainsKey(skill))
            this.mSkillUseCount.Add(skill, this.GetSkillUseCountMax(skill));
        }
      }
    }

    public bool CheckDamageActionStart()
    {
      return this.IsUnitFlag(EUnitFlag.DamagedActionStart) || this.NotifyUniqueNames != null && this.NotifyUniqueNames.Count > 0;
    }

    public bool CheckEnableSkillUseCount(SkillData skill)
    {
      return (skill.SkillType == ESkillType.Skill || skill.SkillType == ESkillType.Reaction) && this.mSkillUseCount.ContainsKey(skill);
    }

    public OInt GetSkillUseCount(SkillData skill)
    {
      if (!this.mSkillUseCount.ContainsKey(skill))
        return (OInt) 0;
      return this.mSkillUseCount[skill];
    }

    public OInt GetSkillUseCountMax(SkillData skill)
    {
      return (OInt) ((int) skill.SkillParam.count + (int) this.MaximumStatus[BattleBonus.SkillUseCount]);
    }

    public void UpdateSkillUseCount(SkillData skill, int count)
    {
      if (!this.CheckEnableSkillUseCount(skill))
        return;
      int skillUseCount = (int) this.GetSkillUseCount(skill);
      this.mSkillUseCount[skill] = (OInt) Math.Min(Math.Max(skillUseCount + count, 0), (int) this.GetSkillUseCountMax(skill));
    }

    public bool IsEnableBuffEffect(BuffTypes type)
    {
      switch (type)
      {
        case BuffTypes.Buff:
          return (this.IsUnitCondition(EUnitCondition.DisableBuff) ? 1 : (this.IsDisableUnitCondition(EUnitCondition.DisableBuff) ? 1 : 0)) == 0;
        case BuffTypes.Debuff:
          return (this.IsUnitCondition(EUnitCondition.DisableDebuff) ? 1 : (this.IsDisableUnitCondition(EUnitCondition.DisableDebuff) ? 1 : 0)) == 0;
        default:
          return false;
      }
    }

    public bool IsEnableSteal()
    {
      return !this.IsGimmick && this.Steal != null;
    }

    public bool IsEnableControlCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.ForceAuto) && (!this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie)) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Rage));
    }

    public bool IsEnableMoveCondition(bool bCondOnly = false)
    {
      return this.IsUnitFlag(EUnitFlag.ForceMoved) || (bCondOnly || !this.IsUnitFlag(EUnitFlag.Moved)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableMove))) && !this.IsUnitCondition(EUnitCondition.Stop);
    }

    public bool IsEnableActionCondition()
    {
      if (!this.IsEnableAttackCondition(false) && !this.IsEnableSkillCondition(false))
        return this.IsEnableItemCondition(false);
      return true;
    }

    public bool IsEnableAttackCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableAttack))) && (!this.IsUnitCondition(EUnitCondition.Stop) && this.GetAttackSkill() != null);
    }

    public bool IsEnableHelpCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep)) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableAttack))) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop) && (!this.IsUnitCondition(EUnitCondition.Rage) && this.GetAttackSkill() != null));
    }

    public bool IsEnableSkillCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm))) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableSkill) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop)) && !this.IsUnitCondition(EUnitCondition.Rage));
    }

    public bool IsEnableItemCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm))) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.Berserk) && (!this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Rage)));
    }

    public bool IsEnableReactionCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Stop)) && (!this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableSkill));
    }

    public bool IsEnableReactionSkill(SkillData skill)
    {
      if (!skill.IsReactionSkill() || (int) skill.SkillParam.count > 0 && (int) this.GetSkillUseCount(skill) == 0)
        return false;
      switch (skill.Timing)
      {
        case ESkillTiming.DamageCalculate:
        case ESkillTiming.DamageControl:
        case ESkillTiming.Reaction:
        case ESkillTiming.FirstReaction:
          return this.IsEnableReactionCondition() && (!skill.IsDamagedSkill() || !this.IsUnitCondition(EUnitCondition.DisableAttack));
        default:
          return false;
      }
    }

    public bool IsEnableSelectDirectionCondition()
    {
      return this.CheckExistMap() && !this.IsUnitCondition(EUnitCondition.Stun) && (!this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Sleep)) && !this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool IsEnableAutoHealCondition()
    {
      return !this.IsDeadCondition() && (this.mCastSkill == null || this.mCastSkill.CastType != ECastTypes.Jump);
    }

    public bool IsEnableGimmickCondition()
    {
      return true;
    }

    public bool IsDeadCondition()
    {
      return this.IsDead || this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool IsDisableGimmick()
    {
      if (!this.IsGimmick)
        return true;
      if (this.IsDestructable)
        return this.IsDead;
      if (this.mEventTrigger != null)
        return this.mEventTrigger.Count == 0;
      return false;
    }

    public bool IsEnableAutoMode()
    {
      return !this.IsUnitFlag(EUnitFlag.ForceAuto) && this.IsControl && (this.IsEntry && !this.IsSub) && !this.IsDeadCondition() && ((!this.IsUnitCondition(EUnitCondition.Charm) || !this.IsUnitCondition(EUnitCondition.Zombie) || !this.IsUnitCondition(EUnitCondition.Berserk)) && this.GetRageTarget() == null);
    }

    public bool CheckCancelSkillFailCondition(EUnitCondition condition)
    {
      return this.CastSkill != null && ((condition & EUnitCondition.Stun) != (EUnitCondition) 0 || (condition & EUnitCondition.Sleep) != (EUnitCondition) 0 || ((condition & EUnitCondition.Stone) != (EUnitCondition) 0 || (condition & EUnitCondition.DisableSkill) != (EUnitCondition) 0) || ((condition & EUnitCondition.Rage) != (EUnitCondition) 0 || (condition & EUnitCondition.Charm) != (EUnitCondition) 0 || (condition & EUnitCondition.Berserk) != (EUnitCondition) 0) || this.CastSkill.IsDamagedSkill() && (condition & EUnitCondition.DisableAttack) != (EUnitCondition) 0);
    }

    public bool CheckCancelSkillCureCondition(EUnitCondition condition)
    {
      return this.CastSkill != null && ((condition & EUnitCondition.Rage) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Rage) || (condition & EUnitCondition.Charm) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Charm) || (condition & EUnitCondition.Berserk) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Berserk));
    }

    public bool CheckEnableCureCondition(EUnitCondition condition)
    {
      if (!this.IsUnitCondition(condition) || this.IsPassiveUnitCondition(condition))
        return false;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && (condAttachment.Condition == condition && condAttachment.IsCurse))
          return false;
      }
      return true;
    }

    public bool CheckEnableFailCondition(EUnitCondition condition)
    {
      return !this.IsUnitCondition(condition) && !this.IsDisableUnitCondition(condition);
    }

    public bool CheckEnableUseSkill(SkillData skill, bool bCheckCondOnly = false)
    {
      if (skill == null)
        return false;
      switch (skill.SkillType)
      {
        case ESkillType.Attack:
          if (!this.IsEnableAttackCondition(bCheckCondOnly))
            return false;
          break;
        case ESkillType.Skill:
          if (!this.IsEnableSkillCondition(bCheckCondOnly))
            return false;
          break;
        case ESkillType.Item:
          if (!this.IsEnableItemCondition(bCheckCondOnly))
            return false;
          break;
      }
      if (this.Gems - this.GetSkillUsedCost(skill) < 0 || skill.HpCostRate == 100 && skill.GetHpCost(this) >= (int) this.CurrentStatus.param.hp || this.CheckEnableSkillUseCount(skill) && (int) this.GetSkillUseCount(skill) == 0)
        return false;
      if (skill.IsDamagedSkill())
      {
        if (this.IsUnitCondition(EUnitCondition.DisableAttack))
          return false;
      }
      else if (this.GetRageTarget() != null)
        return false;
      return true;
    }

    public void SetGuardTarget(Unit target, int turn)
    {
      this.mGuardTarget = target;
      this.mGuardTurn = (OInt) turn;
    }

    public Unit GetGuardTarget()
    {
      if (this.IsDeadCondition())
        return (Unit) null;
      if (!this.CheckExistMap() || this.IsUnitCondition(EUnitCondition.Stun) || (this.IsUnitCondition(EUnitCondition.Paralysed) || this.IsUnitCondition(EUnitCondition.Sleep)))
        return (Unit) null;
      if (this.mGuardTarget == null || this.CheckEnemySide(this.mGuardTarget) || (this.mGuardTarget.IsDeadCondition() || !this.mGuardTarget.CheckExistMap()))
        return (Unit) null;
      return this.mGuardTarget;
    }

    public void CancelGuradTarget()
    {
      this.mGuardTarget = (Unit) null;
      this.mGuardTurn = (OInt) 0;
    }

    private void UpdateGuardTurn()
    {
      if (this.mGuardTarget == null)
        return;
      this.mGuardTurn = (OInt) Math.Max((int) (--this.mGuardTurn), 0);
      if ((int) this.mGuardTurn != 0 && !this.CheckEnemySide(this.mGuardTarget) && (!this.mGuardTarget.IsDeadCondition() && this.mGuardTarget.CheckExistMap()))
        return;
      this.CancelGuradTarget();
    }

    public void AddShield(SkillData skill)
    {
      if (skill == null || skill.EffectType != SkillEffectTypes.Shield || skill.ShieldType == ShieldTypes.None)
        return;
      int shieldValue = (int) skill.ShieldValue;
      if (shieldValue == 0)
        return;
      int shieldTurn = (int) skill.ShieldTurn;
      Unit.UnitShield unitShield = new Unit.UnitShield();
      unitShield.shieldType = skill.ShieldType;
      unitShield.damageType = skill.ShieldDamageType;
      unitShield.hp = (OInt) shieldValue;
      unitShield.hpMax = (OInt) shieldValue;
      unitShield.turn = (OInt) (shieldTurn <= 0 ? -1 : shieldTurn);
      unitShield.turnMax = unitShield.turn;
      unitShield.skill = skill;
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if (this.mShields[index].shieldType == unitShield.shieldType && this.mShields[index].damageType == unitShield.damageType)
          this.mShields.RemoveAt(index--);
      }
      this.mShields.Add(unitShield);
      MySort<Unit.UnitShield>.Sort(this.mShields, (Comparison<Unit.UnitShield>) ((src, dsc) =>
      {
        if (src.shieldType != dsc.shieldType)
          return src.shieldType == ShieldTypes.UseCount ? -1 : 1;
        if (src.damageType != dsc.damageType)
        {
          if (src.damageType == DamageTypes.TotalDamage)
            return -1;
          if (dsc.damageType == DamageTypes.TotalDamage)
            return 1;
        }
        return 0;
      }));
    }

    public bool CheckEnableShieldType(DamageTypes type)
    {
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if ((int) this.mShields[index].hp > 0 && this.mShields[index].damageType == type)
          return true;
      }
      return false;
    }

    public void CalcShieldDamage(DamageTypes damageType, ref int damage, bool bEnableShieldBreak)
    {
      if (damage <= 0)
        return;
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        Unit.UnitShield mShield = this.mShields[index];
        if ((int) mShield.hp > 0)
        {
          if (mShield.shieldType == ShieldTypes.UseCount)
          {
            switch (mShield.damageType)
            {
              case DamageTypes.TotalDamage:
                damage = 0;
                break;
              case DamageTypes.PhyDamage:
              case DamageTypes.MagDamage:
                if (mShield.damageType == DamageTypes.TotalDamage)
                {
                  damage = 0;
                  break;
                }
                continue;
            }
            if (!bEnableShieldBreak)
              break;
            --mShield.hp;
            break;
          }
          if (mShield.shieldType == ShieldTypes.Hp)
          {
            int num = (int) mShield.hp;
            switch (mShield.damageType)
            {
              case DamageTypes.TotalDamage:
                num = Math.Min((int) mShield.hp - damage, 0);
                damage = Math.Max(damage - (int) mShield.hp, 0);
                break;
              case DamageTypes.PhyDamage:
              case DamageTypes.MagDamage:
                if (mShield.damageType == DamageTypes.TotalDamage)
                {
                  num = Math.Min((int) mShield.hp - damage, 0);
                  damage = Math.Max(damage - (int) mShield.hp, 0);
                  break;
                }
                continue;
            }
            if (!bEnableShieldBreak)
              break;
            mShield.hp = (OInt) num;
            break;
          }
        }
      }
    }

    private void UpdateShieldTurn()
    {
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if ((int) this.mShields[index].turn > 0)
        {
          --this.mShields[index].turn;
          if ((int) this.mShields[index].turn <= 0)
            this.mShields.RemoveAt(index--);
        }
      }
    }

    public bool ContainsSkillAttachment(SkillData skill)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID)
          return true;
      }
      return false;
    }

    public void SetBuffAttachment(BuffAttachment buff, bool is_duplicate = false)
    {
      if (buff == null)
        return;
      SkillData skill = buff.skill;
      if (skill != null)
      {
        if (this.mCastSkill != null && this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump)
          return;
        if (!is_duplicate)
        {
          int num1 = 1;
          if (buff.DuplicateCount > 0)
            num1 = Math.Max(1 + this.GetBuffAttachmentDuplicateCount(buff) - buff.DuplicateCount, 0);
          if (num1 > 0)
          {
            int index = 0;
            int num2 = 0;
            for (; index < this.BuffAttachments.Count; ++index)
            {
              if (this.isSameBuffAttachment(this.BuffAttachments[index], buff))
              {
                this.BuffAttachments.RemoveAt(index--);
                if (++num2 >= num1)
                  break;
              }
            }
          }
        }
      }
      this.BuffAttachments.Add(buff);
      this.CalcCurrentStatus(false);
      this.UpdateBuffEffects();
    }

    public void ClearPassiveBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if ((bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public void ClearBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public void UpdateBuffEffectTurnCount(EffectCheckTimings timing, Unit current)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!(bool) buffAttachment.IsPassive)
        {
          EffectCheckTimings checkTiming = buffAttachment.CheckTiming;
          switch (checkTiming)
          {
            case EffectCheckTimings.ClockCountUp:
            case EffectCheckTimings.Moment:
              if (checkTiming == timing && checkTiming != EffectCheckTimings.Eternal)
              {
                --buffAttachment.turn;
                continue;
              }
              continue;
            default:
              if (buffAttachment.CheckTarget != null)
              {
                if (buffAttachment.CheckTarget == current)
                  goto case EffectCheckTimings.ClockCountUp;
                else
                  continue;
              }
              else if (current == null || current == this)
                goto case EffectCheckTimings.ClockCountUp;
              else
                continue;
          }
        }
      }
    }

    public void UpdateBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!(bool) buffAttachment.IsPassive)
        {
          if ((buffAttachment.skill == null || !buffAttachment.skill.IsPassiveSkill()) && !this.IsEnableBuffEffect(buffAttachment.BuffType))
          {
            this.BuffAttachments.RemoveAt(index--);
          }
          else
          {
            if (buffAttachment.CheckTarget != null && buffAttachment.CheckTarget.IsDeadCondition())
              this.BuffAttachments.RemoveAt(index--);
            if (buffAttachment.CheckTiming != EffectCheckTimings.Eternal && (int) buffAttachment.turn <= 0)
              this.BuffAttachments.RemoveAt(index--);
          }
        }
      }
    }

    public bool IsUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (int) this.mCurrentCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsDisableUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (int) this.mDisableCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsCurseUnitCondition(EUnitCondition condition)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment.IsFailCondition() && condAttachment.Condition == condition && condAttachment.IsCurse)
          return true;
      }
      return false;
    }

    public bool IsAutoCureCondition(EUnitCondition condition)
    {
      switch (condition)
      {
        case EUnitCondition.Stone:
        case EUnitCondition.Zombie:
        case EUnitCondition.DeathSentence:
        case EUnitCondition.DisableKnockback:
        case EUnitCondition.GoodSleep:
          return false;
        default:
          return true;
      }
    }

    public bool IsClockCureCondition(EUnitCondition condition)
    {
      switch (condition)
      {
        case EUnitCondition.Stop:
        case EUnitCondition.Fast:
        case EUnitCondition.Slow:
          return true;
        default:
          return false;
      }
    }

    public bool IsUnitConditionDamage()
    {
      return this.IsUnitCondition(EUnitCondition.Poison);
    }

    public bool IsPassiveUnitCondition(EUnitCondition condition)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && (bool) condAttachment.IsPassive && (condAttachment.IsFailCondition() && condAttachment.Condition == condition))
          return true;
      }
      return false;
    }

    public void SetCondAttachment(CondAttachment cond)
    {
      if (cond == null)
        return;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      if (cond.IsFailCondition() && cond.CheckTiming != EffectCheckTimings.Eternal && !(bool) cond.IsPassive)
      {
        if (this.IsClockCureCondition(cond.Condition))
        {
          cond.CheckTarget = this;
          cond.CheckTiming = EffectCheckTimings.ClockCountUp;
          cond.turn = fixParam.DefaultCondTurns[cond.Condition];
        }
        if ((int) cond.turn == 0)
          cond.turn = fixParam.DefaultCondTurns[cond.Condition];
        if (cond.user != null && cond.Condition == EUnitCondition.Poison)
        {
          CondAttachment condAttachment = cond;
          condAttachment.turn = (OInt) ((int) condAttachment.turn + (int) cond.user.CurrentStatus[BattleBonus.PoisonTurn]);
        }
      }
      SkillData skill = cond.skill;
      if (skill != null && this.mCastSkill != null && (this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump))
        return;
      EUnitCondition condition = cond.Condition;
      switch (cond.CondType)
      {
        case ConditionEffectTypes.CureCondition:
          this.CureCondEffects(condition, true, false);
          break;
        case ConditionEffectTypes.FailCondition:
        case ConditionEffectTypes.ForcedFailCondition:
        case ConditionEffectTypes.RandomFailCondition:
          if (!this.IsDisableUnitCondition(condition) || cond.CondType == ConditionEffectTypes.ForcedFailCondition)
          {
            if (!(bool) cond.IsPassive)
            {
              if (this.IsUnitCondition(EUnitCondition.Stone))
                return;
              if ((condition & EUnitCondition.DeathSentence) != (EUnitCondition) 0)
              {
                bool flag = false;
                int num = 999;
                if (cond.skill != null)
                {
                  CondEffect condEffect1 = cond.skill.GetCondEffect(SkillEffectTargets.Target);
                  if (condEffect1 != null && (int) condEffect1.param.v_death_count > 0)
                  {
                    num = Math.Min(num, (int) condEffect1.param.v_death_count);
                    flag = true;
                  }
                  CondEffect condEffect2 = cond.user != this ? (CondEffect) null : cond.skill.GetCondEffect(SkillEffectTargets.Self);
                  if (condEffect2 != null && (int) condEffect2.param.v_death_count > 0)
                  {
                    num = Math.Min(num, (int) condEffect2.param.v_death_count);
                    flag = true;
                  }
                }
                if (!flag)
                  num = (int) fixParam.DefaultDeathCount;
                if ((int) this.mDeathCount > 0)
                  this.mDeathCount = (OInt) Math.Min((int) this.mDeathCount, num);
                if (this.IsUnitCondition(EUnitCondition.DeathSentence))
                  return;
                this.mDeathCount = (OInt) num;
              }
              if ((condition & EUnitCondition.Fast) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Slow, true, false);
              if ((condition & EUnitCondition.Slow) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Fast, true, false);
              if ((condition & EUnitCondition.Rage) != (EUnitCondition) 0)
              {
                if (cond.user == null || cond.user == this)
                  return;
                this.mRageTarget = cond.user;
              }
            }
            if (this.CheckCancelSkillFailCondition(condition))
              this.CancelCastSkill();
            this.CondAttachments.Add(cond);
            break;
          }
          break;
        case ConditionEffectTypes.DisableCondition:
          this.CondAttachments.Add(cond);
          break;
        default:
          return;
      }
      this.UpdateCondEffects();
    }

    public void CureCondEffects(EUnitCondition target, bool updated = true, bool forced = false)
    {
      if (this.CheckCancelSkillCureCondition(target))
        this.CancelCastSkill();
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && condAttachment.UseCondition == ESkillCondition.None && (!condAttachment.IsCurse || forced) && condAttachment.Condition == target)
        {
          switch (condAttachment.CondType)
          {
            case ConditionEffectTypes.FailCondition:
            case ConditionEffectTypes.ForcedFailCondition:
            case ConditionEffectTypes.RandomFailCondition:
              this.CondAttachments.RemoveAt(index--);
              continue;
            default:
              continue;
          }
        }
      }
      if (!updated)
        return;
      this.UpdateCondEffects();
    }

    public void UpdateCondEffects()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && condAttachment.CheckTiming != EffectCheckTimings.Eternal)
        {
          if (condAttachment.IsCurse)
          {
            if (condAttachment.user != null && condAttachment.user.IsDead)
              this.CondAttachments.RemoveAt(index--);
          }
          else if ((int) condAttachment.turn <= 0 && (!condAttachment.IsFailCondition() || this.IsAutoCureCondition(condAttachment.Condition)))
            this.CondAttachments.RemoveAt(index--);
        }
      }
      for (int index1 = 0; index1 < this.CondAttachments.Count; ++index1)
      {
        CondAttachment condAttachment = this.CondAttachments[index1];
        if (condAttachment.UseCondition == ESkillCondition.None || condAttachment.UseCondition != ESkillCondition.Dying || this.IsDying())
        {
          for (int index2 = 0; index2 < (int) Unit.MAX_UNIT_CONDITION; ++index2)
          {
            EUnitCondition eunitCondition = (EUnitCondition) (1 << index2);
            if (condAttachment.Condition == eunitCondition)
            {
              switch (condAttachment.CondType)
              {
                case ConditionEffectTypes.FailCondition:
                case ConditionEffectTypes.ForcedFailCondition:
                case ConditionEffectTypes.RandomFailCondition:
                  if (condAttachment.IsCurse)
                    num3 |= 1 << index2;
                  num1 |= 1 << index2;
                  continue;
                case ConditionEffectTypes.DisableCondition:
                  num2 |= 1 << index2;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      this.mCurrentCondition = (OInt) 0;
      this.mDisableCondition = (OInt) 0;
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        int num4 = 1 << index;
        if ((num2 & num4) != 0)
        {
          this.CureCondEffects((EUnitCondition) num4, false, false);
          if ((num3 & num4) == 0)
            continue;
        }
        if ((num1 & num4) != 0)
        {
          Unit unit = this;
          unit.mCurrentCondition = (OInt) ((int) unit.mCurrentCondition | num4);
        }
      }
      this.mDisableCondition = (OInt) num2;
      if (!this.IsUnitCondition(EUnitCondition.DeathSentence))
        this.mDeathCount = (OInt) -1;
      if (!this.IsUnitCondition(EUnitCondition.Rage))
        this.mRageTarget = (Unit) null;
      if (!this.IsUnitCondition(EUnitCondition.Paralysed))
        this.SetUnitFlag(EUnitFlag.Paralysed, false);
      if (!this.IsUnitCondition(EUnitCondition.Stone))
        return;
      this.mCurrentCondition = (OInt) 32;
    }

    public void UpdateCondEffectTurnCount(EffectCheckTimings timing, Unit current)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive)
        {
          if (condAttachment.CheckTiming != EffectCheckTimings.ClockCountUp && condAttachment.CheckTiming != EffectCheckTimings.Moment)
          {
            if (condAttachment.CheckTarget != null)
            {
              if (condAttachment.CheckTarget != current)
                continue;
            }
            else if (current != null && current != this)
              continue;
          }
          EffectCheckTimings checkTiming = condAttachment.CheckTiming;
          if (checkTiming == timing && checkTiming != EffectCheckTimings.Eternal)
          {
            if (condAttachment.IsFailCondition())
            {
              EUnitCondition condition = condAttachment.Condition;
              if (this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump && condAttachment.CheckTiming == EffectCheckTimings.ActionStart || (this.IsUnitCondition(EUnitCondition.Stone) && condition != EUnitCondition.Stone || !this.IsAutoCureCondition(condition)))
                continue;
            }
            --condAttachment.turn;
          }
        }
      }
    }

    private void UpdateDeathSentence()
    {
      if (!this.IsUnitCondition(EUnitCondition.DeathSentence) || this.IsUnitCondition(EUnitCondition.Stone) || this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump)
        return;
      this.mDeathCount = (OInt) Math.Max((int) (--this.mDeathCount), 0);
    }

    public bool CheckEventTrigger(EEventTrigger trigger)
    {
      if (this.mEventTrigger != null && this.mEventTrigger.Count > 0)
        return this.mEventTrigger.Trigger == trigger;
      return false;
    }

    public void DecrementTriggerCount()
    {
      if (this.mEventTrigger == null)
        return;
      this.mEventTrigger.Count = Math.Min(--this.mEventTrigger.Count, 0);
    }

    public bool CheckWinEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Win)
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return (int) this.MaximumStatus.param.hp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckLoseEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Lose)
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return (int) this.MaximumStatus.param.hp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckNeedEscaped()
    {
      return this.AI != null && (int) this.AI.escape_border != 0 && (!this.IsUnitCondition(EUnitCondition.DisableHeal) && (int) this.MaximumStatus.param.hp != 0) && (int) this.MaximumStatus.param.hp * (int) this.AI.escape_border >= (int) this.CurrentStatus.param.hp * 100;
    }

    public bool CheckEnableEntry()
    {
      if (this.IsSub || this.IsEntry || this.IsDead)
        return false;
      if (this.EntryUnit)
        return true;
      if (this.mEntryTriggers == null)
        return (int) this.mWaitEntryClock == 0;
      bool flag = true;
      for (int index = 0; index < this.mEntryTriggers.Count; ++index)
      {
        UnitEntryTrigger mEntryTrigger = this.mEntryTriggers[index];
        if (!(bool) this.IsEntryTriggerAndCheck && mEntryTrigger.on)
          return true;
        flag &= mEntryTrigger.on;
      }
      if ((bool) this.IsEntryTriggerAndCheck)
        return flag;
      return false;
    }

    public void UpdateClockTime()
    {
      if (!this.IsEntry)
        this.mWaitEntryClock = (OInt) Math.Max((int) (--this.mWaitEntryClock), 0);
      this.UpdateBuffEffectTurnCount(EffectCheckTimings.ClockCountUp, this);
      this.UpdateCondEffectTurnCount(EffectCheckTimings.ClockCountUp, this);
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
    }

    public OInt GetChargeSpeed()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return (OInt) 0;
      int spd = (int) this.CurrentStatus.param.spd;
      if (this.IsUnitCondition(EUnitCondition.Fast))
      {
        int val1 = 0;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Fast))
          {
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect1.param.v_fast));
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect2.param.v_fast));
            }
            else
              val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
          }
        }
        if (val1 == 0)
          val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
        spd += spd * val1 / 100;
      }
      if (this.IsUnitCondition(EUnitCondition.Slow))
      {
        int val1 = 0;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Slow))
          {
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect1.param.v_slow));
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect2.param.v_slow));
            }
            else
              val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
          }
        }
        if (val1 == 0)
          val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
        spd -= spd * val1 / 100;
      }
      return (OInt) spd;
    }

    public void UpdateChargeTime()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return;
      Unit unit = this;
      unit.mChargeTime = (OInt) ((int) unit.mChargeTime + (int) this.GetChargeSpeed());
    }

    public bool CheckChargeTimeFullOver()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return false;
      return (int) this.mChargeTime >= (int) this.ChargeTimeMax;
    }

    public OInt GetCastSpeed()
    {
      return this.GetCastSpeed(this.mCastSkill);
    }

    public OInt GetCastSpeed(SkillData skill)
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return (OInt) 0;
      int castSpeed = (int) skill.CastSpeed;
      if (this.IsUnitCondition(EUnitCondition.Fast))
        castSpeed += castSpeed * 50 / 100;
      if (this.IsUnitCondition(EUnitCondition.Slow))
        castSpeed -= castSpeed * 50 / 100;
      return (OInt) castSpeed;
    }

    public void UpdateCastTime()
    {
      if (this.mCastSkill == null || this.IsUnitCondition(EUnitCondition.Stop))
        return;
      Unit unit = this;
      unit.mCastTime = (OInt) ((int) unit.mCastTime + (int) this.GetCastSpeed());
    }

    public bool CheckCastTimeFullOver()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop) || this.CastSkill == null)
        return false;
      return (int) this.mCastTime >= (int) this.CastTimeMax;
    }

    public void SetCastSkill(SkillData skill, Unit unit, Grid grid)
    {
      this.mCastSkill = skill;
      this.mCastTime = (OInt) 0;
      this.mCastIndex = Unit.UNIT_CAST_INDEX++;
      this.mUnitTarget = unit;
      this.mGridTarget = grid;
      this.mCastSkillGridMap = (GridMap<bool>) null;
    }

    public void CancelCastSkill()
    {
      this.mCastSkill = (SkillData) null;
      this.mCastTime = (OInt) 0;
      this.mCastIndex = (OInt) 0;
      this.mUnitTarget = (Unit) null;
      this.mGridTarget = (Grid) null;
      this.mCastSkillGridMap = (GridMap<bool>) null;
    }

    public void SetCastTime(int time)
    {
      this.mCastTime = (OInt) time;
    }

    public void NotifyContinue()
    {
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CurrentStatus.param.hp = this.MaximumStatus.param.hp;
      this.CurrentStatus.param.mp = this.MaximumStatus.param.mp;
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CancelCastSkill();
      this.CancelGuradTarget();
      this.NotifyMapStart();
      this.mChargeTime = this.ChargeTimeMax;
    }

    public void NotifyMapStart()
    {
      this.SetUnitFlag(EUnitFlag.Moved, false);
      this.SetUnitFlag(EUnitFlag.Action, false);
      this.SetUnitFlag(EUnitFlag.Sneaking, false);
      this.SetUnitFlag(EUnitFlag.Paralysed, false);
      this.SetUnitFlag(EUnitFlag.ForceMoved, false);
      this.SetUnitFlag(EUnitFlag.EntryDead, false);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        if (!(bool) this.CondAttachments[index].IsPassive && (this.CondAttachments[index].skill == null || !this.CondAttachments[index].skill.IsPassiveSkill()))
          this.CondAttachments.RemoveAt(index--);
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus(false);
      this.ClearSkillUseCount();
    }

    public void NotifyActionStart(List<Unit> others)
    {
      if (others != null)
      {
        for (int index = 0; index < others.Count; ++index)
        {
          others[index].UpdateCondEffectTurnCount(EffectCheckTimings.ActionStart, this);
          others[index].UpdateCondEffects();
          others[index].UpdateBuffEffectTurnCount(EffectCheckTimings.ActionStart, this);
          others[index].UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
          others[index].UpdateBuffEffects();
          others[index].setRelatedBuff();
          others[index].CalcCurrentStatus(false);
        }
      }
      this.UpdateShieldTurn();
      this.UpdateGuardTurn();
      this.UpdateDeathSentence();
      if (this.mRageTarget != null && this.mRageTarget.IsDeadCondition())
        this.CureCondEffects(EUnitCondition.Rage, true, true);
      this.startX = this.x;
      this.startY = this.y;
      this.startDir = this.Direction;
      if (!this.IsDead && this.IsEntry && !this.IsUnitFlag(EUnitFlag.FirstAction))
      {
        if (this.IsPartyMember && !this.IsUnitFlag(EUnitFlag.DisableFirstVoice))
          this.PlayBattleVoice("battle_0030");
        this.SetUnitFlag(EUnitFlag.FirstAction, true);
      }
      this.SetUnitFlag(EUnitFlag.Moved, (int) this.mMoveWaitTurn > 0);
      this.SetUnitFlag(EUnitFlag.Action, false);
      this.SetUnitFlag(EUnitFlag.Escaped, false);
      this.SetUnitFlag(EUnitFlag.Sneaking, false);
      this.SetUnitFlag(EUnitFlag.Paralysed, false);
      this.SetUnitFlag(EUnitFlag.ForceMoved, false);
      this.SetCommandFlag(EUnitCommandFlag.Move, false);
      this.SetCommandFlag(EUnitCommandFlag.Action, false);
      this.mMoveWaitTurn = (OInt) Math.Max((int) (--this.mMoveWaitTurn), 0);
      ++this.mActionCount;
      if (this.mAIActionTable == null || this.mAIActionTable.actions == null || (this.mAIActionTable.actions.Count <= 0 || this.mAIActionTable.actions.Count <= (int) this.mAIActionIndex))
        return;
      ++this.mAIActionTurnCount;
    }

    public void NotifyActionEnd()
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      Unit unit1 = this;
      unit1.mChargeTime = (OInt) ((int) unit1.mChargeTime - (int) fixParam.ChargeTimeDecWait);
      if (this.IsCommandFlag(EUnitCommandFlag.Move))
      {
        Unit unit2 = this;
        unit2.mChargeTime = (OInt) ((int) unit2.mChargeTime - (int) fixParam.ChargeTimeDecMove);
      }
      if (this.IsCommandFlag(EUnitCommandFlag.Action))
      {
        Unit unit2 = this;
        unit2.mChargeTime = (OInt) ((int) unit2.mChargeTime - (int) fixParam.ChargeTimeDecAction);
      }
      this.mChargeTime = (OInt) Math.Max((int) this.mChargeTime, 0);
      this.CalcCurrentStatus(false);
    }

    public void NotifyCommandStart()
    {
      if (this.mUnitData.Lv > (int) GlobalVars.MaxLevelInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxLevelInBattle = (OInt) this.mUnitData.Lv;
      this.CalcCurrentStatus(false);
    }

    public void RefleshMomentBuff(List<Unit> others)
    {
      if (others == null)
        return;
      using (List<Unit>.Enumerator enumerator = others.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit current = enumerator.Current;
          current.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
          current.UpdateBuffEffects();
          current.setRelatedBuff();
          current.CalcCurrentStatus(false);
        }
      }
    }

    public bool CheckEnemySide(Unit target)
    {
      if (this == target)
        return false;
      if (!this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie))
        return this.Side != target.Side;
      if (target.IsUnitCondition(EUnitCondition.Charm) || target.IsUnitCondition(EUnitCondition.Zombie))
        return false;
      return this.Side == target.Side;
    }

    public Unit GetRageTarget()
    {
      if (this.IsUnitCondition(EUnitCondition.Rage))
        return this.mRageTarget;
      return (Unit) null;
    }

    public SkillData GetAttackSkill()
    {
      return this.UnitData.GetAttackSkill();
    }

    public void Heal(int value)
    {
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CurrentStatus.param.hp = (OInt) Math.Min((int) this.CurrentStatus.param.hp + value, (int) this.MaximumStatus.param.hp);
      if ((int) this.CurrentStatus.param.hp <= (int) GlobalVars.MaxHpInBattle || this.mSide != EUnitSide.Player)
        return;
      GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
    }

    public void Damage(int value)
    {
      if (value > (int) GlobalVars.MaxDamageInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxDamageInBattle = (OInt) value;
      this.CurrentStatus.param.hp = (OInt) Math.Max((int) this.CurrentStatus.param.hp - value, 0);
      if ((int) this.CurrentStatus.param.hp <= (int) GlobalVars.MaxHpInBattle || this.mSide != EUnitSide.Player)
        return;
      GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
    }

    public void ForceDead()
    {
      this.CurrentStatus.param.hp = (OInt) 0;
      this.mDeathCount = (OInt) 0;
      this.mChargeTime = (OInt) 0;
      this.CancelCastSkill();
      this.CancelGuradTarget();
      this.SetUnitFlag(EUnitFlag.EntryDead, true);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        if (!(bool) this.CondAttachments[index].IsPassive && (this.CondAttachments[index].skill == null || !this.CondAttachments[index].skill.IsPassiveSkill()))
          this.CondAttachments.RemoveAt(index--);
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
    }

    public void SetDeathCount(int count)
    {
      this.mDeathCount = (OInt) count;
    }

    public int GetPoisonDamage()
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      bool flag = false;
      int val1_1 = 0;
      int val1_2 = 0;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Poison))
        {
          int num1 = 0;
          int num2 = 0;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect1 != null)
            {
              if ((int) condEffect1.param.v_poison_rate != 0)
                num1 = Math.Max(num1, (int) condEffect1.param.v_poison_rate);
              if ((int) condEffect1.param.v_poison_fix != 0)
                num2 = Math.Max(num2, (int) condEffect1.param.v_poison_fix);
            }
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null)
            {
              if ((int) condEffect2.param.v_poison_rate != 0)
                num1 = Math.Max(num1, (int) condEffect2.param.v_poison_rate);
              if ((int) condEffect2.param.v_poison_fix != 0)
                num2 = Math.Max(num2, (int) condEffect2.param.v_poison_fix);
            }
          }
          if (num1 == 0 && num2 == 0)
            num1 = (int) fixParam.PoisonDamageRate;
          if (condAttachment.user != null)
          {
            if (num1 != 0)
              num1 += (int) condAttachment.user.CurrentStatus[BattleBonus.PoisonDamage];
            if (num2 != 0)
              num2 += num2 * (int) condAttachment.user.CurrentStatus[BattleBonus.PoisonDamage] / 100;
          }
          flag = true;
          val1_1 = Math.Max(val1_1, num1);
          val1_2 = Math.Max(val1_2, num2);
        }
      }
      if (!flag)
        val1_1 = (int) fixParam.PoisonDamageRate;
      int val2 = Math.Max(val1_1 == 0 ? 0 : (int) this.MaximumStatus.param.hp * val1_1 / 100, 1);
      return Math.Max(val1_2, val2);
    }

    public int GetParalyseRate()
    {
      if (!this.IsUnitCondition(EUnitCondition.Paralysed))
        return 0;
      int val1 = 0;
      bool flag = false;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Paralysed))
        {
          int num = 0;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect1 != null && (int) condEffect1.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect1.param.v_paralyse_rate);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect2.param.v_paralyse_rate);
          }
          if (num == 0)
            num = (int) fixParam.ParalysedRate;
          flag = true;
          val1 = Math.Max(val1, num);
        }
      }
      if (!flag)
        val1 = (int) fixParam.ParalysedRate;
      return val1;
    }

    public int GetAttackRangeMin()
    {
      return this.GetAttackRangeMin(this.GetAttackSkill());
    }

    public int GetAttackRangeMin(SkillData skill)
    {
      if (skill == null || skill.SkillParam.select_range == ESelectType.All)
        return 0;
      return this.UnitData.GetAttackRangeMin(skill);
    }

    public int GetAttackRangeMax()
    {
      return this.GetAttackRangeMax(this.GetAttackSkill());
    }

    public int GetAttackRangeMax(SkillData skill)
    {
      if (skill == null)
        return 0;
      int num = this.UnitData.GetAttackRangeMax(skill);
      if (skill.IsEnableChangeRange())
        num += (int) this.mCurrentStatus[BattleBonus.EffectRange];
      if (skill.SkillParam.select_range == ESelectType.All)
        num = 99;
      return num;
    }

    public int GetAttackScope()
    {
      return this.GetAttackScope(this.GetAttackSkill());
    }

    public int GetAttackScope(SkillData skill)
    {
      if (skill == null)
        return 0;
      int attackScope = this.UnitData.GetAttackScope(skill);
      if (skill.IsEnableChangeRange())
        attackScope += (int) this.mCurrentStatus[BattleBonus.EffectScope];
      return attackScope;
    }

    public int GetAttackHeight()
    {
      return this.GetAttackHeight(this.GetAttackSkill());
    }

    public int GetAttackHeight(SkillData skill)
    {
      if (skill == null)
        return 0;
      int attackHeight = this.UnitData.GetAttackHeight(skill);
      if (skill.IsEnableChangeRange())
        attackHeight += (int) this.mCurrentStatus[BattleBonus.EffectHeight];
      return Math.Max(attackHeight, 1);
    }

    public int GetMoveCount(bool bCondOnly = false)
    {
      if (!this.IsEnableMoveCondition(bCondOnly))
        return 0;
      if (!this.IsUnitCondition(EUnitCondition.Donsoku))
        return (int) this.mCurrentStatus.param.mov;
      int val1 = (int) byte.MaxValue;
      bool flag = false;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Donsoku))
        {
          int num = (int) byte.MaxValue;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect1 != null && (int) condEffect1.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect1.param.v_donmov);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect2.param.v_donmov);
          }
          if (num == (int) byte.MaxValue)
            num = 1;
          val1 = Math.Min(val1, num);
          flag = true;
        }
      }
      if (flag)
        return val1;
      return 1;
    }

    public int GetMoveHeight()
    {
      return Math.Max((int) this.mCurrentStatus.param.jmp, 1);
    }

    public void SetSearchRange(int value)
    {
      this.mSearched = (OInt) value;
    }

    public int GetSearchRange()
    {
      return (int) this.mSearched;
    }

    public int GetCombination()
    {
      return this.UnitData.GetCombination();
    }

    public int GetCombinationRange()
    {
      return this.UnitData.GetCombinationRange() + (int) this.CurrentStatus[BattleBonus.CombinationRange];
    }

    public AbilityData GetAbilityData(long iid)
    {
      if (iid <= 0L)
        return (AbilityData) null;
      for (int index = 0; index < this.BattleAbilitys.Count; ++index)
      {
        if (iid == this.BattleAbilitys[index].UniqueID)
          return this.BattleAbilitys[index];
      }
      return (AbilityData) null;
    }

    public SkillData GetSkillData(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SkillData) null;
      SkillData attackSkill = this.GetAttackSkill();
      if (attackSkill != null && iname == attackSkill.SkillID)
        return attackSkill;
      for (int index = 0; index < this.BattleSkills.Count; ++index)
      {
        if (iname == this.BattleSkills[index].SkillID)
          return this.BattleSkills[index];
      }
      return (SkillData) null;
    }

    public int GetSkillUsedCost(SkillData skill)
    {
      int cost = skill.Cost;
      if (skill.EffectType != SkillEffectTypes.GemsGift)
        cost += cost * (int) this.CurrentStatus[BattleBonus.UsedJewelRate] / 100;
      return cost;
    }

    public EElement GetWeakElement()
    {
      return UnitParam.GetWeakElement(this.Element);
    }

    public int GetAutoHpHealValue()
    {
      if (this.IsUnitCondition(EUnitCondition.DisableHeal))
        return 0;
      int num1 = 0;
      if (this.IsUnitCondition(EUnitCondition.AutoHeal))
      {
        int val1 = 0;
        bool flag = false;
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.AutoHeal))
          {
            int num2 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                if ((int) condEffect1.param.v_auto_hp_heal > 0)
                  num2 = Math.Max(num2, (int) this.MaximumStatus.param.hp * (int) condEffect1.param.v_auto_hp_heal / 100);
                if ((int) condEffect1.param.v_auto_hp_heal_fix > 0)
                  num2 = Math.Max(num2, (int) condEffect1.param.v_auto_hp_heal_fix);
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                if ((int) condEffect2.param.v_auto_hp_heal > 0)
                  num2 = Math.Max(num2, (int) this.MaximumStatus.param.hp * (int) condEffect2.param.v_auto_hp_heal / 100);
                if ((int) condEffect2.param.v_auto_hp_heal_fix > 0)
                  num2 = Math.Max(num2, (int) condEffect2.param.v_auto_hp_heal_fix);
              }
            }
            if (num2 == 0)
              num2 = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
            flag = true;
            val1 = Math.Max(val1, num2);
          }
        }
        if (!flag)
          val1 = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
        num1 += val1;
      }
      if (this.IsUnitCondition(EUnitCondition.Sleep) && this.IsUnitCondition(EUnitCondition.GoodSleep))
        num1 += this.GetGoodSleepHpHealValue();
      return num1;
    }

    public bool CheckAutoHpHeal()
    {
      return this.GetAutoHpHealValue() > 0;
    }

    public int GetAutoMpHealValue()
    {
      int autoJewel = this.AutoJewel;
      if (this.IsUnitCondition(EUnitCondition.AutoJewel))
      {
        bool flag = false;
        int val1 = 0;
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.AutoJewel))
          {
            int num = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                if ((int) condEffect1.param.v_auto_mp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.mp * (int) condEffect1.param.v_auto_mp_heal / 100);
                if ((int) condEffect1.param.v_auto_mp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect1.param.v_auto_mp_heal_fix);
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                if ((int) condEffect2.param.v_auto_mp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.mp * (int) condEffect2.param.v_auto_mp_heal / 100);
                if ((int) condEffect2.param.v_auto_mp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect2.param.v_auto_mp_heal_fix);
              }
            }
            if (num == 0)
              num = (int) this.MaximumStatus.param.mp * (int) fixParam.MpAutoHealRate / 100;
            flag = true;
            val1 = Math.Max(val1, num);
          }
        }
        if (!flag)
          val1 = (int) this.MaximumStatus.param.mp * (int) fixParam.MpAutoHealRate / 100;
        autoJewel += val1;
      }
      if (this.IsUnitCondition(EUnitCondition.Sleep) && this.IsUnitCondition(EUnitCondition.GoodSleep))
        autoJewel += this.GetGoodSleepMpHealValue();
      return autoJewel;
    }

    public bool CheckAutoMpHeal()
    {
      return this.GetAutoMpHealValue() > 0;
    }

    public bool CheckItemDrop()
    {
      if (this.Side == EUnitSide.Player)
        return false;
      if (this.IsGimmick)
      {
        if (!this.IsDisableGimmick() || this.EventTrigger == null || this.EventTrigger.EventType != EEventType.Treasure)
          return false;
      }
      else if (!this.IsDead)
        return false;
      return true;
    }

    public bool CheckActionSkillBuffAttachments(BuffTypes type)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && this.BuffAttachments[index].BuffType == type)
          return true;
      }
      return false;
    }

    public bool IsAIActionTable()
    {
      return this.mAIActionTable.actions.Count != 0 && (int) this.mAIActionIndex >= 0 && this.mAIActionTable.actions.Count > (int) this.mAIActionIndex;
    }

    public AIAction GetCurrentAIAction()
    {
      if (!this.IsAIActionTable())
        return (AIAction) null;
      if ((int) this.mAIActionTurnCount < (int) this.mAIActionTable.actions[(int) this.mAIActionIndex].turn)
        return (AIAction) null;
      return this.mAIActionTable.actions[(int) this.mAIActionIndex];
    }

    public void NextAIAction()
    {
      if (!this.IsAIActionTable())
        return;
      ++this.mAIActionIndex;
      this.mAIActionTurnCount = (OInt) 0;
      if (this.mAIActionTable.looped == 0)
        return;
      this.mAIActionIndex = (OInt) ((int) this.mAIActionIndex % this.mAIActionTable.actions.Count);
    }

    public bool IsAIPatrolTable()
    {
      return this.mAIPatrolTable != null && this.mAIPatrolTable.routes != null && ((int) this.mAIPatrolIndex >= 0 && this.mAIPatrolTable.routes.Length > (int) this.mAIPatrolIndex) && (this.mAIPatrolTable.keeped != 0 || !this.IsUnitFlag(EUnitFlag.Searched));
    }

    public AIPatrolPoint GetCurrentPatrolPoint()
    {
      if (!this.IsAIPatrolTable())
        return (AIPatrolPoint) null;
      return this.mAIPatrolTable.routes[(int) this.mAIPatrolIndex];
    }

    public void NextPatrolPoint()
    {
      if (!this.IsAIPatrolTable())
        return;
      ++this.mAIPatrolIndex;
      if (this.mAIPatrolTable.looped == 0)
        return;
      this.mAIPatrolIndex = (OInt) ((int) this.mAIPatrolIndex % this.mAIPatrolTable.routes.Length);
    }

    public int GetConditionPriority(SkillData skill, SkillEffectTargets target)
    {
      int val1 = (int) byte.MaxValue;
      if (this.AI == null || this.AI.ConditionPriorities == null)
        return val1;
      CondEffect condEffect = skill.GetCondEffect(target);
      if (condEffect != null && condEffect.param != null && condEffect.param.conditions != null)
      {
        for (int index = 0; index < condEffect.param.conditions.Length; ++index)
        {
          int val2 = Array.IndexOf<EUnitCondition>(this.AI.ConditionPriorities, condEffect.param.conditions[index]);
          if (val2 != -1)
            val1 = Math.Max(Math.Min(val1, val2), 0);
        }
      }
      return val1;
    }

    public int GetBuffPriority(SkillData skill, SkillEffectTargets target)
    {
      int val1 = (int) byte.MaxValue;
      if (this.AI == null || this.AI.BuffPriorities == null)
        return val1;
      BuffEffect buffEffect = skill.GetBuffEffect(target);
      if (buffEffect != null && buffEffect.targets != null)
      {
        for (int index = 0; index < buffEffect.targets.Count; ++index)
        {
          int val2 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, buffEffect.targets[index].paramType);
          if (val2 != -1)
            val1 = Math.Max(Math.Min(val1, val2), 0);
        }
      }
      if ((int) skill.ControlChargeTimeValue != 0)
      {
        int val2 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, ParamTypes.ChargeTimeRate);
        if (val2 != -1)
          val1 = Math.Max(Math.Min(val1, val2), 0);
      }
      return val1;
    }

    public int GetActionSkillBuffValue(BuffTypes type, SkillParamCalcTypes calc, ParamTypes param)
    {
      int val2 = 0;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!(bool) buffAttachment.IsPassive && buffAttachment.BuffType == type && buffAttachment.CalcType == calc)
          val2 = Math.Max(Math.Abs((int) buffAttachment.status[param]), val2);
      }
      return val2;
    }

    public void GetEnableBetterBuffEffect(Unit self, SkillData skill, SkillEffectTargets effect_target, out int buff_count, out int buff_value, bool bRequestOnly = false)
    {
      buff_count = 0;
      buff_value = 0;
      BuffEffect buffEffect = skill.GetBuffEffect(effect_target);
      if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
      {
        for (int index1 = 0; index1 < buffEffect.targets.Count; ++index1)
        {
          BuffEffect.BuffTarget target = buffEffect.targets[index1];
          if (target.buffType == BuffTypes.Buff)
          {
            if (this.IsUnitCondition(EUnitCondition.DisableBuff) || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_buff, 0) <= (self == null || self.AI == null ? 0 : (int) self.AI.buff_border))
              continue;
          }
          else if (target.buffType == BuffTypes.Debuff && (this.IsUnitCondition(EUnitCondition.DisableDebuff) || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_debuff, 0) <= (self == null || self.AI == null ? 0 : (int) self.AI.buff_border)))
            continue;
          if (this.BuffAttachments.Find((Predicate<BuffAttachment>) (p => p.skill == skill)) == null)
          {
            int actionSkillBuffValue = this.GetActionSkillBuffValue(target.buffType, target.calcType, target.paramType);
            int num = Math.Max(Math.Abs((int) target.value) - actionSkillBuffValue, 0);
            if (num != 0)
            {
              if (bRequestOnly && this.AI.BuffPriorities != null)
              {
                for (int index2 = 0; index2 < this.AI.BuffPriorities.Length; ++index2)
                {
                  if (target.paramType == this.AI.BuffPriorities[index2])
                  {
                    buff_value += num;
                    break;
                  }
                }
              }
              else
                buff_value += num;
              ++buff_count;
            }
          }
        }
      }
      if ((int) skill.ControlChargeTimeValue == 0)
        return;
      buff_value += Math.Abs((int) skill.ControlChargeTimeValue);
      ++buff_count;
    }

    public bool ReqRevive { get; set; }

    public string GetUnitSkinVoiceSheetName(int jobIndex = -1)
    {
      return this.UnitData.GetUnitSkinVoiceSheetName(jobIndex);
    }

    public string GetUnitSkinVoiceCueName(int jobIndex = -1)
    {
      return this.UnitData.GetUnitSkinVoiceCueName(jobIndex);
    }

    public void LoadBattleVoice()
    {
      if (this.mBattleVoice != null)
        return;
      string skinVoiceSheetName = this.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
        return;
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.GetUnitSkinVoiceCueName(-1) + "_";
      this.mBattleVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
    }

    public void PlayBattleVoice(string cueID)
    {
      if (this.mBattleVoice == null)
        return;
      this.mBattleVoice.Play(cueID, 0.0f);
    }

    public int CalcTowerDamege()
    {
      return this.mTowerStartHP - (int) this.CurrentStatus.param.hp;
    }

    public Unit GetUnitUseCollaboSkill(SkillData skill, bool is_use_tuc = false)
    {
      int x = this.x;
      int y = this.y;
      if (is_use_tuc)
      {
        SceneBattle instance = SceneBattle.Instance;
        if (Object.op_Equality((Object) instance, (Object) null))
          return (Unit) null;
        TacticsUnitController unitController = instance.FindUnitController(this);
        if (Object.op_Inequality((Object) unitController, (Object) null) && !this.IsUnitFlag(EUnitFlag.Moved))
        {
          IntVector2 intVector2 = instance.CalcCoord(unitController.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
      }
      return this.GetUnitUseCollaboSkill(skill, x, y);
    }

    public Unit GetUnitUseCollaboSkill(SkillData skill, int ux, int uy)
    {
      string partnerIname = CollaboSkillParam.GetPartnerIname(this.mUnitData.UnitParam.iname, skill.SkillParam.iname);
      if (string.IsNullOrEmpty(partnerIname))
        return (Unit) null;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return (Unit) null;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return (Unit) null;
      if (battle.CurrentMap == null)
        return (Unit) null;
      Grid current1 = battle.CurrentMap[ux, uy];
      for (int index1 = current1.y - 1; index1 <= current1.y + 1; ++index1)
      {
        for (int index2 = current1.x - 1; index2 <= current1.x + 1; ++index2)
        {
          if (index2 != current1.x || index1 != current1.y)
          {
            Grid current2 = battle.CurrentMap[index2, index1];
            if (current2 != null)
            {
              Unit unitAtGrid = battle.FindUnitAtGrid(current2);
              if (unitAtGrid != null && unitAtGrid.mSide == this.mSide && (!(unitAtGrid.UnitParam.iname != partnerIname) && unitAtGrid.GetSkillUseCount(skill.SkillParam.iname) != null) && Math.Abs(current1.height - current2.height) <= (int) skill.SkillParam.CollaboHeight)
                return unitAtGrid;
            }
          }
        }
      }
      return (Unit) null;
    }

    public bool IsUseSkillCollabo(SkillData skill, bool is_use_tuc = false)
    {
      if (!(bool) skill.IsCollabo)
        return true;
      Unit unitUseCollaboSkill = this.GetUnitUseCollaboSkill(skill, is_use_tuc);
      if (unitUseCollaboSkill == null)
        return false;
      return unitUseCollaboSkill.Gems - unitUseCollaboSkill.GetSkillUsedCost(skill) >= 0;
    }

    public SkillData GetSkillUseCount(string skill_iname)
    {
      using (Dictionary<SkillData, OInt>.KeyCollection.Enumerator enumerator = this.mSkillUseCount.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillData current = enumerator.Current;
          if (current != null && current.SkillParam.iname == skill_iname)
            return current;
        }
      }
      return (SkillData) null;
    }

    public bool IsNormalSize
    {
      get
      {
        return this.SizeX == 1 && this.SizeY == 1;
      }
    }

    public bool IsThrow
    {
      get
      {
        return this.mUnitData.IsThrow;
      }
    }

    private void setRelatedBuff()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.CurrentMap == null)
        return;
      BattleMap currentMap = battle.CurrentMap;
      if (currentMap == null)
        return;
      List<Unit> unitList = new List<Unit>(battle.Units.Count);
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[this.x + Unit.DIRECTION_OFFSETS[index, 0], this.y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unitAtGrid = battle.FindUnitAtGrid(grid);
          if (unitAtGrid != null)
          {
            using (List<BuffAttachment>.Enumerator enumerator = unitAtGrid.BuffAttachments.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                BuffAttachment current = enumerator.Current;
                if (current.CheckTiming != EffectCheckTimings.Moment && current.Param != null && (current.Param.mEffRange == EEffRange.SelfNearAlly && unitAtGrid.mSide == this.mSide))
                {
                  unitList.Add(unitAtGrid);
                  break;
                }
              }
            }
          }
        }
      }
      if (unitList.Count == 0)
        return;
      using (List<Unit>.Enumerator enumerator1 = unitList.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          Unit current1 = enumerator1.Current;
          using (List<BuffAttachment>.Enumerator enumerator2 = current1.BuffAttachments.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              BuffAttachment current2 = enumerator2.Current;
              if (current2.CheckTiming != EffectCheckTimings.Moment && current2.Param != null && (current2.Param.mEffRange == EEffRange.SelfNearAlly && current1.mSide == this.mSide))
              {
                BuffAttachment buffAttachment = new BuffAttachment();
                current2.CopyTo(buffAttachment);
                buffAttachment.CheckTiming = EffectCheckTimings.Moment;
                buffAttachment.IsPassive = (OBool) false;
                buffAttachment.turn = (OInt) 1;
                bool flag = false;
                using (List<BuffAttachment>.Enumerator enumerator3 = this.BuffAttachments.GetEnumerator())
                {
                  while (enumerator3.MoveNext())
                  {
                    BuffAttachment current3 = enumerator3.Current;
                    if (this.isSameBuffAttachment(current3, buffAttachment) && current3.user == buffAttachment.user)
                    {
                      flag = true;
                      break;
                    }
                  }
                }
                if (!flag)
                  this.SetBuffAttachment(buffAttachment, false);
              }
            }
          }
        }
      }
    }

    public int GetAllyUnitNum(Unit target_unit)
    {
      if (target_unit == null || target_unit.IsDead)
        return 0;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return 0;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.CurrentMap == null)
        return 0;
      BattleMap currentMap = battle.CurrentMap;
      if (currentMap == null)
        return 0;
      int num = 0;
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[target_unit.x + Unit.DIRECTION_OFFSETS[index, 0], target_unit.y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unitAtGrid = battle.FindUnitAtGrid(grid);
          if (unitAtGrid != null && unitAtGrid.mSide == target_unit.mSide)
            ++num;
        }
      }
      return num;
    }

    public class DropItem
    {
      public ItemParam param;
      public OInt num;
    }

    public class UnitDrop
    {
      public List<Unit.DropItem> items = new List<Unit.DropItem>();
      public OInt exp;
      public OInt gems;
      public OInt gold;
      public bool gained;

      public bool IsEnableDrop()
      {
        if (this.gained)
          return false;
        if ((int) this.exp <= 0 && (int) this.gems <= 0 && (int) this.gold <= 0)
          return this.items.Count > 0;
        return true;
      }

      public void Drop()
      {
        this.gained = true;
      }

      public void CopyTo(Unit.UnitDrop other)
      {
        other.exp = this.exp;
        other.gems = this.gems;
        other.gold = this.gold;
        other.gained = this.gained;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (this.items[index] != null && this.items[index].param != null && (int) this.items[index].num != 0)
            other.items.Add(new Unit.DropItem()
            {
              param = this.items[index].param,
              num = this.items[index].num
            });
        }
      }
    }

    public class UnitSteal
    {
      public List<Unit.DropItem> items = new List<Unit.DropItem>();
      public OInt gold;
      public bool is_item_steeled;
      public bool is_gold_steeled;

      public bool IsEnableItemSteal()
      {
        if (!this.is_item_steeled)
          return this.items.Count > 0;
        return false;
      }

      public bool IsEnableGoldSteal()
      {
        return !this.is_gold_steeled;
      }

      public void CopyTo(Unit.UnitSteal other)
      {
        other.gold = this.gold;
        other.is_item_steeled = this.is_item_steeled;
        other.is_gold_steeled = this.is_gold_steeled;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (this.items[index] != null && this.items[index].param != null && (int) this.items[index].num != 0)
            other.items.Add(new Unit.DropItem()
            {
              param = this.items[index].param,
              num = this.items[index].num
            });
        }
      }
    }

    public class UnitShield
    {
      public ShieldTypes shieldType;
      public DamageTypes damageType;
      public OInt hp;
      public OInt hpMax;
      public OInt turn;
      public OInt turnMax;
      public SkillData skill;

      public void CopyTo(Unit.UnitShield other)
      {
        other.shieldType = this.shieldType;
        other.damageType = this.damageType;
        other.hp = this.hp;
        other.hpMax = this.hpMax;
        other.turn = this.turn;
        other.turnMax = this.turnMax;
        other.skill = this.skill;
      }
    }
  }
}
