// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCore
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class BattleCore
  {
    private static List<BattleCore.SkillResult> mSkillResults = new List<BattleCore.SkillResult>();
    public static readonly int MAX_MAP = 3;
    public static readonly int MAX_PARTY = 7;
    public static readonly int MAX_ENEMY = 16;
    public static readonly int MAX_ORDER = BattleCore.MAX_PARTY + BattleCore.MAX_ENEMY;
    public static readonly int MAX_UNITS = BattleCore.MAX_PARTY + BattleCore.MAX_ENEMY;
    public static readonly int MAX_GEMS = 99;
    public static bool DEBUG_IS_NPC_CONTROL = false;
    public static bool DEBUG_IS_FORCE_ACTION = false;
    public static bool DEBUG_IS_FORCE_REACTION = false;
    public static bool DEBUG_IS_FORCE_AVOID = false;
    public static bool DEBUG_IS_FORCE_COMBINATION = false;
    public static bool DEBUG_IS_CAST_SHORTCUT = false;
    public static bool DEBUG_IS_FORCE_AVOID_REACTION = false;
    public static bool DEBUG_MOVJMP_MAX = false;
    private static BaseStatus BuffWorkStatus = new BaseStatus();
    private static BaseStatus BuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus DebuffWorkStatus = new BaseStatus();
    private static BaseStatus DebuffWorkScaleStatus = new BaseStatus();
    public static readonly string SUSPENDDATA_FILENAME = "suspend.bin";
    private List<List<SkillData>> mUseSkillLists = new List<List<SkillData>>();
    private List<SkillData> mForceSkillList = new List<SkillData>();
    private List<SkillData> mHealSkills = new List<SkillData>(5);
    private List<SkillData> mDamageSkills = new List<SkillData>(5);
    private List<SkillData> mSupportSkills = new List<SkillData>(5);
    private List<SkillData> mCureConditionSkills = new List<SkillData>(5);
    private List<SkillData> mFailConditionSkills = new List<SkillData>(5);
    private List<SkillData> mDisableConditionSkills = new List<SkillData>(5);
    private QuestParam mQuestParam = new QuestParam();
    private List<Unit> mAllUnits = new List<Unit>(BattleCore.MAX_UNITS);
    private List<Unit> mUnits = new List<Unit>(BattleCore.MAX_UNITS);
    private List<Unit> mPlayer = new List<Unit>(BattleCore.MAX_PARTY);
    private OInt mLeaderIndex = (OInt) -1;
    private OInt mEnemyLeaderIndex = (OInt) -1;
    private OInt mFriendIndex = (OInt) -1;
    private List<Unit> mStartingMembers = new List<Unit>();
    private List<Unit> mHelperUnits = new List<Unit>(BattleCore.MAX_ENEMY);
    private List<BattleMap> mMap = new List<BattleMap>(BattleCore.MAX_MAP);
    private OInt mClockTime = (OInt) 0;
    private OInt mClockTimeTotal = (OInt) 0;
    public ItemData[] mInventory = new ItemData[5];
    private List<BattleCore.OrderData> mOrder = new List<BattleCore.OrderData>(BattleCore.MAX_ORDER);
    private BattleLogServer mLogs = new BattleLogServer();
    private RandXorshift mRand = new RandXorshift(nameof (mRand));
    private RandXorshift mRandDamage = new RandXorshift(nameof (mRandDamage));
    private BattleCore.Record mRecord = new BattleCore.Record();
    private List<Unit> mTreasures = new List<Unit>();
    private List<GimmickSkill> mGimmickSkills = new List<GimmickSkill>();
    private List<BattleCore.SuspendData> mSuspendData = new List<BattleCore.SuspendData>();
    private List<BattleCore.SuspendLog> mSuspendLogLists = new List<BattleCore.SuspendLog>();
    private List<string> mSuspendMsgLists = new List<string>();
    private uint mArenaActionCountMax = 25;
    private const int SUSPEND_ERROR_CTR_MAX = 3;
    private long mBtlID;
    private int mBtlFlags;
    private int mWinTriggerCount;
    private int mLoseTriggerCount;
    private int mActionCount;
    private int mKillstreak;
    private int mMaxKillstreak;
    private int mTotalHeal;
    private int mTotalDamagesTaken;
    private int mTotalDamages;
    private int mNumUsedItems;
    private int mNumUsedSkills;
    private int mNpcStartIndex;
    private List<Unit>[] mEnemys;
    private FriendStates mFriendStates;
    private int mMapIndex;
    private int mContinueCount;
    public bool IsSuspendStart;
    public bool IsSuspendSaveRequest;
    public bool IsFirstUnitStart;
    private uint mSeed;
    private uint mSeedDamage;
    private RandXorshift CurrentRand;
    private List<Grid> mGridLines;
    private string[] mQuestCampaignIds;
    private int mMyPlayerIndex;
    private bool mFinishLoad;
    private BattleCore.RESUME_STATE mResumeState;
    public BattleCore.LogCallback LogHandler;
    public BattleCore.LogCallback WarningHandler;
    public BattleCore.LogCallback ErrorHandler;
    private int mSuspendIndex;
    private int mSuspendErrorCtr;
    private string mSuspendUseSkillID;
    private string mSuspendUseSkillUnitID;
    private bool mIsArenaSkip;
    private uint mArenaActionCount;
    private string mArenaQuestID;
    private BattleCore.Json_Battle mArenaQuestJsonBtl;
    private bool mIsArenaCalc;
    private BattleCore.QuestResult mArenaCalcResult;
    private BattleCore.eArenaCalcType mArenaCalcTypeNext;
    private List<Unit> mEnemyPriorities;
    private GridMap<int> mMoveMap;
    private GridMap<bool> mRangeMap;
    private GridMap<bool> mScopeMap;
    private GridMap<bool> mSearchMap;
    private GridMap<int> mSafeMap;

    public int ActionCount
    {
      get
      {
        return this.mActionCount;
      }
    }

    public int TotalClockTime
    {
      get
      {
        return (int) this.mClockTimeTotal;
      }
    }

    public RandXorshift CloneRand()
    {
      return this.mRand.Clone();
    }

    public RandXorshift CloneRandDamage()
    {
      RandXorshift randXorshift = this.mRandDamage.Clone();
      if (randXorshift != null)
        randXorshift.Seed(this.mSeedDamage);
      return randXorshift;
    }

    public uint Seed
    {
      get
      {
        return this.mSeed;
      }
      set
      {
        this.mSeed = value;
      }
    }

    public uint DamageSeed
    {
      get
      {
        return this.mSeedDamage;
      }
      set
      {
        this.mSeedDamage = value;
      }
    }

    public void SetRandSeed(int index, uint seed)
    {
      this.mRand.SetSeed(index, seed);
    }

    public void SetRandDamageSeed(int index, uint seed)
    {
      this.mRandDamage.SetSeed(index, seed);
    }

    public bool SyncStart { get; set; }

    public int MyPlayerIndex
    {
      get
      {
        return this.mMyPlayerIndex;
      }
    }

    public bool IsMultiPlay { get; private set; }

    public bool IsMultiVersus { get; private set; }

    public bool IsVSForceWin { get; set; }

    public bool IsVSForceWinComfirm { get; set; }

    public bool FinishLoad
    {
      get
      {
        return this.mFinishLoad;
      }
      set
      {
        this.mFinishLoad = value;
      }
    }

    public BattleCore.RESUME_STATE ResumeState
    {
      get
      {
        return this.mResumeState;
      }
      set
      {
        this.mResumeState = value;
      }
    }

    public bool IsResume
    {
      get
      {
        return this.mResumeState == BattleCore.RESUME_STATE.WAIT;
      }
    }

    public void SetResumeWait()
    {
      this.mResumeState = BattleCore.RESUME_STATE.WAIT;
    }

    private void DebugAssert(bool condition, string msg)
    {
      if (!condition)
      {
        if (this.ErrorHandler != null)
          this.ErrorHandler(msg);
        throw new Exception("[Assertion Failed] " + msg);
      }
    }

    private void DebugLog(string s)
    {
      if (this.LogHandler == null)
        return;
      this.LogHandler(s);
    }

    private void DebugWarn(string s)
    {
      if (this.WarningHandler == null)
        return;
      this.WarningHandler(s);
    }

    private void DebugErr(string s)
    {
      if (this.ErrorHandler == null)
        return;
      this.ErrorHandler(s);
    }

    public void DEBUG_ADD_GEMS(int amount)
    {
      if (this.IsMultiPlay)
        return;
      Unit currentUnit = this.CurrentUnit;
      if (currentUnit == null)
        return;
      this.AddGems(currentUnit, amount);
    }

    public string QuestID
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.iname;
        return (string) null;
      }
    }

    public string QuestName
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.name;
        return (string) null;
      }
    }

    public string QuestTerms
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.cond;
        return (string) null;
      }
    }

    public QuestTypes QuestType
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type;
        return QuestTypes.Story;
      }
    }

    public string QuestClearEventName
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.event_clear;
        return (string) null;
      }
    }

    public List<BattleMap> Map
    {
      get
      {
        return this.mMap;
      }
    }

    public List<Unit> AllUnits
    {
      get
      {
        return this.mAllUnits;
      }
    }

    public List<Unit> Units
    {
      get
      {
        return this.mUnits;
      }
    }

    public List<BattleCore.OrderData> Order
    {
      get
      {
        return this.mOrder;
      }
    }

    public List<Unit> HelperUnits
    {
      get
      {
        return this.mHelperUnits;
      }
    }

    public int MapIndex
    {
      get
      {
        return this.mMapIndex;
      }
    }

    public BattleMap CurrentMap
    {
      get
      {
        if (this.mMap != null && 0 <= this.mMapIndex && this.mMapIndex < this.mMap.Count)
          return this.mMap[this.mMapIndex];
        return (BattleMap) null;
      }
    }

    public Unit CurrentUnit
    {
      get
      {
        if (this.mOrder.Count > 0)
          return this.mOrder[0].Unit;
        return (Unit) null;
      }
    }

    public BattleCore.OrderData CurrentOrderData
    {
      get
      {
        if (this.mOrder.Count > 0)
          return this.mOrder[0];
        return (BattleCore.OrderData) null;
      }
    }

    public BattleLogServer Logs
    {
      get
      {
        return this.mLogs;
      }
    }

    public List<Unit> Player
    {
      get
      {
        return this.mPlayer;
      }
    }

    public List<Unit> Enemys
    {
      get
      {
        if (this.MapIndex >= 0 && this.MapIndex < this.mEnemys.Length)
          return this.mEnemys[this.MapIndex];
        return (List<Unit>) null;
      }
    }

    public Unit Leader
    {
      get
      {
        if (!this.IsMultiPlay && (int) this.mLeaderIndex != -1 || this.IsMultiVersus && (int) this.mLeaderIndex != -1)
          return this.mPlayer[(int) this.mLeaderIndex];
        return (Unit) null;
      }
    }

    public Unit Friend
    {
      get
      {
        if (!this.IsMultiPlay && (int) this.mFriendIndex != -1)
          return this.mPlayer[(int) this.mFriendIndex];
        return (Unit) null;
      }
    }

    public Unit EnemyLeader
    {
      get
      {
        if (!this.IsMultiPlay && (int) this.mEnemyLeaderIndex != -1 || this.IsMultiVersus && (int) this.mEnemyLeaderIndex != -1)
          return this.Enemys[(int) this.mEnemyLeaderIndex];
        return (Unit) null;
      }
    }

    public long BtlID
    {
      get
      {
        return this.mBtlID;
      }
    }

    public bool RequestAutoBattle { get; set; }

    public bool IsAutoBattle { get; set; }

    public string[] QuestCampaignIds
    {
      get
      {
        return this.mQuestCampaignIds;
      }
    }

    ~BattleCore()
    {
      this.Release();
    }

    public void Release()
    {
      if (this.mLogs != null)
      {
        this.mLogs.Release();
        this.mLogs = (BattleLogServer) null;
      }
      if (this.mOrder != null)
      {
        this.mOrder.Clear();
        this.mOrder = (List<BattleCore.OrderData>) null;
      }
      this.mRecord = (BattleCore.Record) null;
      this.mRand = (RandXorshift) null;
      if (this.mAllUnits != null)
      {
        this.mAllUnits.Clear();
        this.mAllUnits = (List<Unit>) null;
      }
      if (this.mMap != null)
      {
        for (int index = 0; index < this.mMap.Count; ++index)
        {
          if (this.mMap[index] != null)
            this.mMap[index].Release();
        }
        this.mMap.Clear();
        this.mMap = (List<BattleMap>) null;
      }
      this.mMapIndex = 0;
      this.mEnemys = (List<Unit>[]) null;
      this.mPlayer = (List<Unit>) null;
      this.mQuestParam = (QuestParam) null;
    }

    public bool SetupMultiPlayUnit(UnitData[] units, int[] ownerPlayerIndex, int[] placementIndex)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      List<UnitSetting> partyUnitSettings = this.mMap[0].PartyUnitSettings;
      List<UnitSetting> arenaUnitSettings = this.mMap[0].ArenaUnitSettings;
      PlayerPartyTypes playerPartyType;
      PlayerPartyTypes enemyPartyType;
      this.mQuestParam.GetPartyTypes(out playerPartyType, out enemyPartyType);
      if (instance.IsMultiVersus)
      {
        this.IsMultiVersus = instance.IsMultiVersus;
        this.mLeaderIndex = (OInt) 0;
        this.mEnemyLeaderIndex = (OInt) 0;
        this.VersusTurnMax = (uint) this.mQuestParam.VersusMoveCount;
        this.RemainVersusTurnCount = 0U;
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            Unit unit = new Unit();
            if (!unit.Setup(units[index], ownerPlayerIndex[index] != 1 ? arenaUnitSettings[placementIndex[index]] : partyUnitSettings[placementIndex[index]], (Unit.DropItem) null, (Unit.DropItem) null))
            {
              this.DebugErr("failed unit Setup");
              return false;
            }
            unit.IsPartyMember = true;
            unit.SetUnitFlag(EUnitFlag.Searched, true);
            unit.OwnerPlayerIndex = ownerPlayerIndex[index];
            unit.Side = ownerPlayerIndex[index] == instance.MyPlayerIndex ? EUnitSide.Player : EUnitSide.Enemy;
            if (unit.Side == EUnitSide.Player)
              this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
          }
        }
      }
      else
      {
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            UnitData unitData = new UnitData();
            unitData.Setup(units[index]);
            unitData.SetJob(playerPartyType);
            if (this.mQuestParam.IsUnitAllowed(units[index]))
            {
              Unit unit = new Unit();
              if (!unit.Setup(units[index], partyUnitSettings[index], (Unit.DropItem) null, (Unit.DropItem) null))
              {
                this.DebugErr("failed unit Setup");
                return false;
              }
              unit.IsPartyMember = true;
              unit.SetUnitFlag(EUnitFlag.Searched, true);
              unit.OwnerPlayerIndex = ownerPlayerIndex[index];
              this.mPlayer.Add(unit);
              this.mAllUnits.Add(unit);
              this.mStartingMembers.Add(unit);
            }
          }
        }
      }
      return true;
    }

    public bool Deserialize(string questID, BattleCore.Json_Battle jsonBtl, int myPlayerIndex, UnitData[] units, int[] ownerPlayerIndex, bool is_restart = false, int[] placementIndex = null)
    {
      if (jsonBtl == null | string.IsNullOrEmpty(questID))
        return false;
      this.mMyPlayerIndex = myPlayerIndex;
      if (this.mMyPlayerIndex <= 0)
      {
        this.DebugLog("[PUN]this is singleplay");
      }
      else
      {
        this.IsMultiPlay = true;
        this.DebugLog("[PUN]this is multiplay");
      }
      this.mBtlID = jsonBtl.btlid;
      this.mMapIndex = 0;
      this.mLeaderIndex = (OInt) -1;
      this.mFriendIndex = (OInt) -1;
      this.mFriendStates = FriendStates.None;
      this.mWinTriggerCount = 0;
      this.mLoseTriggerCount = 0;
      this.mSuspendIndex = 0;
      this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      DebugUtility.Assert(this.mQuestParam != null, "mQuestParam == null");
      PlayerPartyTypes playerPartyType;
      PlayerPartyTypes enemyPartyType;
      this.mQuestParam.GetPartyTypes(out playerPartyType, out enemyPartyType);
      this.mSeed = (uint) jsonBtl.btlinfo.seed;
      this.mRand.Seed(this.mSeed);
      this.CurrentRand = this.mRand;
      for (int index = 0; index < this.mQuestParam.map.Count; ++index)
      {
        BattleMap battleMap = new BattleMap();
        battleMap.mRandDeckResult = jsonBtl.btlinfo.GetDeck();
        if (!battleMap.Initialize(this, this.mQuestParam.map[index]))
          return false;
        this.mMap.Add(battleMap);
      }
      List<UnitSetting> partyUnitSettings = this.mMap[0].PartyUnitSettings;
      if (partyUnitSettings != null && partyUnitSettings.Count > 0)
      {
        if (this.IsMultiPlay)
        {
          if (!this.SetupMultiPlayUnit(units, ownerPlayerIndex, placementIndex))
            return false;
        }
        else
        {
          int index1 = 0;
          if (this.mQuestParam.units != null)
          {
            for (int index2 = 0; index2 < this.mQuestParam.units.Length; ++index2)
            {
              string unit1 = this.mQuestParam.units[index2];
              if (!string.IsNullOrEmpty(unit1))
              {
                UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(unit1);
                if (unitDataByUnitId == null)
                {
                  this.DebugErr("player uniqueid not equal");
                  return false;
                }
                UnitData unitdata = new UnitData();
                unitdata.Setup(unitDataByUnitId);
                unitdata.SetJob(playerPartyType);
                Unit unit2 = new Unit();
                if (!unit2.Setup(unitdata, partyUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  this.DebugErr("failed unit Setup");
                  return false;
                }
                unit2.IsPartyMember = true;
                unit2.SetUnitFlag(EUnitFlag.Searched, true);
                unit2.SetUnitFlag(EUnitFlag.ForceEntried, true);
                this.mPlayer.Add(unit2);
                this.mAllUnits.Add(unit2);
                this.mStartingMembers.Add(unit2);
                ++index1;
              }
            }
          }
          if (jsonBtl.btlinfo.units != null)
          {
            for (int index2 = 0; index2 < jsonBtl.btlinfo.units.Length; ++index2)
            {
              long iid = (long) jsonBtl.btlinfo.units[index2].iid;
              if (iid > 0L)
              {
                UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iid);
                if (unitDataByUniqueId == null)
                {
                  this.DebugErr("player uniqueid not equal");
                  return false;
                }
                UnitData unitData = new UnitData();
                unitData.Setup(unitDataByUniqueId);
                if (this.mQuestParam.type == QuestTypes.Tower)
                {
                  int num = !MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestParam.iname).can_help ? -1 : 0;
                  if (partyUnitSettings.Count <= index1 + num)
                    continue;
                }
                else if (partyUnitSettings.Count <= index1)
                  continue;
                if (this.mQuestParam.IsUnitAllowed(unitData))
                {
                  PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
                  bool flag = index2 < partyCurrent.MAX_MAINMEMBER;
                  if (!flag || index2 < this.mQuestParam.GetSelectMainMemberNum())
                  {
                    UnitSetting setting = this.mQuestParam.type != QuestTypes.Tower ? partyUnitSettings[index1] : (flag ? partyUnitSettings[index1] : partyUnitSettings[partyUnitSettings.Count - 1]);
                    if (!flag)
                    {
                      setting = new UnitSetting();
                      setting.side = (OInt) 0;
                    }
                    Unit unit = new Unit();
                    if (!unit.Setup(unitData, setting, (Unit.DropItem) null, (Unit.DropItem) null))
                    {
                      this.DebugErr("failed unit Setup");
                      return false;
                    }
                    if ((int) this.mLeaderIndex == -1)
                      this.mLeaderIndex = (OInt) index1;
                    if (flag)
                    {
                      this.mStartingMembers.Add(unit);
                      ++index1;
                    }
                    unit.SetUnitFlag(EUnitFlag.Searched, true);
                    unit.IsPartyMember = true;
                    unit.IsSub = !flag;
                    this.mPlayer.Add(unit);
                    this.mAllUnits.Add(unit);
                  }
                }
              }
            }
          }
          if (jsonBtl.btlinfo.help != null && index1 < partyUnitSettings.Count)
          {
            UnitData unitdata = new UnitData();
            try
            {
              unitdata.Deserialize(jsonBtl.btlinfo.help.unit);
            }
            catch (Exception ex)
            {
              this.DebugErr("<EXCEPTION> " + ex.Message + "\n-----------------------\n" + ex.StackTrace);
            }
            Unit unit = new Unit();
            if (!unit.Setup(unitdata, partyUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
            {
              this.DebugErr("failed unit Setup");
              return false;
            }
            unit.IsPartyMember = true;
            unit.IsSub = false;
            unit.SetUnitFlag(EUnitFlag.Searched, true);
            this.mFriendIndex = (OInt) this.mPlayer.Count;
            this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
            this.mFriendStates = (FriendStates) jsonBtl.btlinfo.help.isFriend;
            int num = index1 + 1;
          }
        }
        this.mNpcStartIndex = this.mAllUnits.Count;
      }
      if ((int) this.mLeaderIndex == (int) (OInt) -1 && this.mPlayer.Count >= 1)
        this.mLeaderIndex = (OInt) 0;
      this.mEnemys = new List<Unit>[this.mMap.Count];
      switch (this.mQuestParam.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Tutorial:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
          int index3 = 0;
          for (int index1 = 0; index1 < this.mMap.Count; ++index1)
          {
            this.mEnemys[index1] = new List<Unit>(BattleCore.MAX_ENEMY);
            List<NPCSetting> npcUnitSettings = this.mMap[index1].NPCUnitSettings;
            if (npcUnitSettings != null)
            {
              for (int index2 = 0; index2 < npcUnitSettings.Count; ++index2)
              {
                Unit unit = new Unit();
                Unit.DropItem dropitem = (Unit.DropItem) null;
                BattleCore.Json_BtlDrop[] drops = jsonBtl.btlinfo.drops;
                if (drops != null && index3 < drops.Length && (!string.IsNullOrEmpty(drops[index3].iname) && drops[index3].num > 0))
                {
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(drops[index3].iname);
                  if (itemParam != null)
                  {
                    dropitem = new Unit.DropItem();
                    dropitem.param = itemParam;
                    dropitem.num = (OInt) drops[index3].num;
                  }
                }
                Unit.DropItem stealitem = (Unit.DropItem) null;
                BattleCore.Json_BtlSteal[] steals = jsonBtl.btlinfo.steals;
                if (steals != null && index3 < steals.Length && (!string.IsNullOrEmpty(steals[index3].iname) && steals[index3].num > 0))
                {
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(steals[index3].iname);
                  if (itemParam != null)
                  {
                    stealitem = new Unit.DropItem();
                    stealitem.param = itemParam;
                    stealitem.num = (OInt) steals[index3].num;
                  }
                }
                if (!unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings[index2], dropitem, stealitem))
                {
                  this.DebugErr("enemy unit setup failed");
                  return false;
                }
                switch (unit.Side)
                {
                  case EUnitSide.Player:
                    this.mPlayer.Add(unit);
                    this.mStartingMembers.Add(unit);
                    break;
                  case EUnitSide.Enemy:
                  case EUnitSide.Neutral:
                    this.mEnemys[index1].Add(unit);
                    break;
                }
                this.mAllUnits.Add(unit);
                ++index3;
              }
            }
          }
          break;
        case QuestTypes.Arena:
          ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
          if (selectedArenaPlayer != null)
          {
            List<UnitSetting> arenaUnitSettings = this.mMap[0].ArenaUnitSettings;
            this.mEnemys[0] = new List<Unit>(selectedArenaPlayer.Unit.Length);
            for (int index1 = 0; index1 < selectedArenaPlayer.Unit.Length; ++index1)
            {
              if (selectedArenaPlayer.Unit[index1] != null)
              {
                UnitData unitdata = new UnitData();
                unitdata.Setup(selectedArenaPlayer.Unit[index1]);
                unitdata.SetJob(enemyPartyType);
                Unit unit = new Unit();
                if (!unit.Setup(unitdata, arenaUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  this.DebugErr("failed unit Setup");
                  return false;
                }
                unit.SetUnitFlag(EUnitFlag.Searched, true);
                this.mEnemys[0].Add(unit);
                this.mAllUnits.Add(unit);
              }
            }
            this.mEnemyLeaderIndex = (OInt) 0;
          }
          for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
            this.mAllUnits[index1].SetUnitFlag(EUnitFlag.ForceAuto, true);
          break;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          this.mEnemyLeaderIndex = (OInt) 0;
          this.mEnemys[0] = new List<Unit>(BattleCore.MAX_ENEMY);
          using (List<Unit>.Enumerator enumerator = this.AllUnits.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Unit current = enumerator.Current;
              if (current.Side == EUnitSide.Enemy)
                this.mEnemys[0].Add(current);
            }
            break;
          }
      }
      if (jsonBtl.btlinfo.atkmags != null)
        this.mQuestParam.SetAtkTypeMag(jsonBtl.btlinfo.atkmags);
      if (jsonBtl.btlinfo.campaigns != null)
        this.mQuestCampaignIds = jsonBtl.btlinfo.campaigns;
      if (!is_restart)
      {
        int length = this.mAllUnits.Count - this.mNpcStartIndex;
        if (length != 0)
        {
          this.mRecord.drops = new OInt[length];
          Array.Clear((Array) this.mRecord.drops, 0, length);
          this.mRecord.item_steals = new OInt[length];
          Array.Clear((Array) this.mRecord.item_steals, 0, length);
          this.mRecord.gold_steals = new OInt[length];
          Array.Clear((Array) this.mRecord.gold_steals, 0, length);
        }
      }
      this.UpdateUnitName();
      if (this.CurrentMap != null && this.mQuestParam.IsNoStartVoice)
      {
        for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
          this.mAllUnits[index1].SetUnitFlag(EUnitFlag.DisableFirstVoice, true);
      }
      this.UpdateBattlePassiveSkill();
      if (this.mQuestParam.type == QuestTypes.Tower)
      {
        MonoSingleton<GameManager>.Instance.TowerResuponse.CalcDamage(this.Player);
        MonoSingleton<GameManager>.Instance.TowerResuponse.CalcEnemyDamage(this.Enemys);
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        this.mInventory[index1] = (ItemData) null;
        ItemData itemData1 = MonoSingleton<GameManager>.GetInstanceDirect().Player.Inventory[index1];
        if (itemData1 != null)
        {
          ItemData itemData2 = new ItemData();
          itemData2.Setup(itemData1.UniqueID, itemData1.Param, Math.Min(itemData1.Num, (int) itemData1.Param.invcap));
          this.mInventory[index1] = itemData2;
        }
      }
      this.mRand.Seed(this.mSeed);
      this.SetBattleFlag(EBattleFlag.Initialized, true);
      return true;
    }

    public uint GetRandom()
    {
      return this.CurrentRand.Get();
    }

    private void UpdateUnitName()
    {
      if (this.mPlayer != null)
      {
        for (int index1 = 0; index1 < this.mPlayer.Count; ++index1)
        {
          char ch = 'A';
          for (int index2 = 0; index2 < this.mPlayer.Count && this.mPlayer[index1] != this.mPlayer[index2]; ++index2)
          {
            if (this.mPlayer[index1].UnitParam.iname == this.mPlayer[index2].UnitParam.iname)
              ++ch;
          }
          this.mPlayer[index1].UnitName = this.mPlayer[index1].UnitParam.name + (object) ch;
        }
      }
      if (this.mEnemys == null)
        return;
      for (int index1 = 0; index1 < this.mMap.Count; ++index1)
      {
        List<Unit> mEnemy = this.mEnemys[index1];
        for (int index2 = 0; index2 < mEnemy.Count; ++index2)
        {
          char ch = 'A';
          for (int index3 = 0; index3 < mEnemy.Count && mEnemy[index2] != mEnemy[index3]; ++index3)
          {
            if (mEnemy[index2].UnitParam.iname == mEnemy[index3].UnitParam.iname)
              ++ch;
          }
          mEnemy[index2].UnitName = mEnemy[index2].UnitParam.name + (object) ch;
        }
      }
    }

    public QuestParam GetQuest()
    {
      return MonoSingleton<GameManager>.Instance.FindQuest(this.QuestID);
    }

    public ItemData FindInventoryByItemID(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ItemData) null;
      for (int index = 0; index < this.mInventory.Length; ++index)
      {
        if (this.mInventory[index] != null && iname == this.mInventory[index].ItemID)
          return this.mInventory[index];
      }
      return (ItemData) null;
    }

    private void UpdateBattlePassiveSkill()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].ClearPassiveBuffEffects();
      if (this.Leader != null)
        this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, true);
      if (this.Friend != null && this.mFriendStates == FriendStates.Friend)
        this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, true);
      if (this.EnemyLeader != null)
        this.InternalBattlePassiveSkill(this.EnemyLeader, this.EnemyLeader.LeaderSkill, true);
      for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
      {
        Unit mAllUnit = this.mAllUnits[index1];
        EquipData[] currentEquips = mAllUnit.CurrentEquips;
        if (currentEquips != null)
        {
          for (int index2 = 0; index2 < currentEquips.Length; ++index2)
          {
            EquipData equipData = currentEquips[index2];
            if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
              this.InternalBattlePassiveSkill(mAllUnit, equipData.Skill, false);
          }
        }
        for (int index2 = 0; index2 < mAllUnit.BattleSkills.Count; ++index2)
          this.InternalBattlePassiveSkill(mAllUnit, mAllUnit.BattleSkills[index2], false);
      }
      for (int index = 0; index < this.Player.Count; ++index)
        this.Player[index].CalcCurrentStatus(this.mMapIndex == 0);
      for (int index = 0; index < this.Enemys.Count; ++index)
        this.Enemys[index].CalcCurrentStatus(true);
    }

    private void InternalBattlePassiveSkill(Unit self, SkillData skill, bool is_duplicate = false)
    {
      if (skill == null || !skill.IsPassiveSkill())
        return;
      for (int index = 0; index < this.Player.Count; ++index)
        this.InternalBattlePassiveSkill(self, this.Player[index], skill, is_duplicate);
      for (int index = 0; index < this.Enemys.Count; ++index)
        this.InternalBattlePassiveSkill(self, this.Enemys[index], skill, is_duplicate);
    }

    private void InternalBattlePassiveSkill(Unit self, Unit target, SkillData skill, bool is_duplicate = false)
    {
      if (target == null || target.IsGimmick || !this.CheckSkillTarget(self, target, skill) || !is_duplicate && target.ContainsSkillAttachment(skill))
        return;
      SkillEffectTypes effectType = skill.EffectType;
      switch (effectType)
      {
        case SkillEffectTypes.Equipment:
        case SkillEffectTypes.Buff:
        case SkillEffectTypes.Debuff:
label_4:
          bool flag = !string.IsNullOrEmpty(skill.SkillParam.tokkou);
          BuffEffect buffEffect = skill.GetBuffEffect(SkillEffectTargets.Target);
          if (buffEffect != null && buffEffect.param != null && (buffEffect.param.mAppType != EAppType.Standard || buffEffect.param.mEffRange != EEffRange.Self))
            flag = true;
          if (skill.Target != ESkillTarget.Self || skill.Condition != ESkillCondition.None || flag)
            this.BuffSkill(ESkillTiming.Passive, self, target, skill, true, (LogSkill) null, SkillEffectTargets.Target, is_duplicate);
          this.CondSkill(ESkillTiming.Passive, self, target, skill, true, (LogSkill) null, SkillEffectTargets.Target);
          break;
        default:
          switch (effectType - 11)
          {
            case SkillEffectTypes.None:
            case SkillEffectTypes.Attack:
              goto label_4;
            case SkillEffectTypes.Equipment:
              return;
            default:
              return;
          }
      }
    }

    private void SetBattleFlag(EBattleFlag tgt, bool sw)
    {
      if (sw)
        this.mBtlFlags |= 1 << (int) (tgt & (EBattleFlag) 31 & (EBattleFlag) 31);
      else
        this.mBtlFlags &= ~(1 << (int) (tgt & (EBattleFlag) 31 & (EBattleFlag) 31));
    }

    private bool IsBattleFlag(EBattleFlag tgt)
    {
      return (this.mBtlFlags & 1 << (int) (tgt & (EBattleFlag) 31)) != 0;
    }

    public bool IsInitialized
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.Initialized);
      }
    }

    public bool IsMapCommand
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.MapCommand);
      }
    }

    public bool IsBattle
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.Battle);
      }
    }

    public bool IsUnitAuto(Unit unit)
    {
      if (unit.IsControl)
        return this.IsAutoBattle;
      if (GameUtility.IsDebugBuild)
        return !BattleCore.DEBUG_IS_NPC_CONTROL;
      return true;
    }

    public void RemoveLog()
    {
      if (this.IsBattleFlag(EBattleFlag.SuspendStart))
        return;
      this.mLogs.RemoveLog();
    }

    private LogType Log<LogType>() where LogType : BattleLog, new()
    {
      if (this.IsBattleFlag(EBattleFlag.SuspendStart))
        return Activator.CreateInstance<LogType>();
      if (this.mIsArenaCalc)
        return Activator.CreateInstance<LogType>();
      return this.mLogs.Log<LogType>();
    }

    public void CalcOrder()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        this.UpdateEntryTriggers(UnitEntryTypes.DecrementHp, this.mUnits[index], (SkillParam) null);
        this.UpdateEntryTriggers(UnitEntryTypes.OnGridEnemy, this.mUnits[index], (SkillParam) null);
      }
      this.UpdateEntryTriggers(UnitEntryTypes.DecrementMember, (Unit) null, (SkillParam) null);
      this.UpdateCancelCastSkill();
      this.UpdateGimmickSkillStart();
      while (true)
      {
        this.mOrder.Clear();
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (!mUnit.IsDeadCondition() && mUnit.IsEntry && (mUnit.Side != EUnitSide.Player || !mUnit.IsSub) && (!mUnit.IsUnitCondition(EUnitCondition.Stop) && (!mUnit.IsGimmick || mUnit.AI != null)))
          {
            this.mOrder.Add(new BattleCore.OrderData()
            {
              Unit = mUnit,
              IsCharged = mUnit.CheckChargeTimeFullOver()
            });
            if (mUnit.CastSkill != null)
              this.mOrder.Add(new BattleCore.OrderData()
              {
                Unit = mUnit,
                IsCastSkill = true,
                IsCharged = mUnit.CheckCastTimeFullOver()
              });
          }
        }
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit.CheckEnableEntry())
          {
            this.EntryUnit(mUnit);
            this.mOrder.Add(new BattleCore.OrderData()
            {
              Unit = mUnit
            });
          }
        }
        if (this.mOrder.Count == 0 || this.CheckEnableNextClockTime())
          this.NextClockTime();
        else
          break;
      }
      MySort<BattleCore.OrderData>.Sort(this.mOrder, (Comparison<BattleCore.OrderData>) ((src, dsc) =>
      {
        if (src.IsCharged && dsc.IsCharged)
        {
          if (src.IsCastSkill && dsc.IsCastSkill)
          {
            if ((int) src.Unit.CastIndex < (int) dsc.Unit.CastIndex)
              return -1;
            if ((int) src.Unit.CastIndex > (int) dsc.Unit.CastIndex)
              return 1;
          }
        }
        else
        {
          if (src.IsCharged)
            return -1;
          if (dsc.IsCharged)
            return 1;
        }
        OInt oint1 = (OInt) ((int) src.GetChargeTime() * 1000 / (int) src.GetChargeTimeMax());
        OInt oint2 = (OInt) ((int) dsc.GetChargeTime() * 1000 / (int) dsc.GetChargeTimeMax());
        if ((int) oint2 != (int) oint1)
          return (int) oint2 - (int) oint1;
        if (src.IsCastSkill != dsc.IsCastSkill)
          return src.IsCastSkill ? -1 : 1;
        if (src.IsCastSkill && dsc.IsCastSkill)
        {
          if ((int) src.Unit.CastIndex < (int) dsc.Unit.CastIndex)
            return -1;
          if ((int) src.Unit.CastIndex > (int) dsc.Unit.CastIndex)
            return 1;
        }
        if ((int) dsc.Unit.UnitIndex != (int) src.Unit.UnitIndex)
          return (int) src.Unit.UnitIndex - (int) dsc.Unit.UnitIndex;
        return 0;
      }));
    }

    public void StartOrder(bool sync = true, bool force = true)
    {
      this.Logs.Reset();
      this.ResumeState = BattleCore.RESUME_STATE.NONE;
      this.NextOrder(sync, force);
      this.ClearAI();
    }

    private void NextOrder(bool sync = true, bool forceSync = false)
    {
      if (this.CheckJudgeBattle())
      {
        this.CalcQuestRecord();
        this.MapEnd();
      }
      else
      {
        bool flag = this.CurrentUnit != null && this.CurrentUnit.OwnerPlayerIndex > 0;
        this.CalcOrder();
        this.UseAutoSkills();
        BattleCore.OrderData currentOrderData = this.CurrentOrderData;
        if (this.IsMultiPlay && (sync && flag || forceSync))
          this.Log<LogSync>();
        if (currentOrderData.IsCastSkill)
          this.Log<LogCastSkillStart>();
        else
          this.Log<LogUnitStart>();
      }
    }

    private void UseAutoSkills()
    {
      for (int index = 0; index < this.mAllUnits.Count; ++index)
      {
        Unit mAllUnit = this.mAllUnits[index];
        if (mAllUnit.IsUnitFlag(EUnitFlag.Entried) && !mAllUnit.IsUnitFlag(EUnitFlag.TriggeredAutoSkills))
        {
          mAllUnit.SetUnitFlag(EUnitFlag.TriggeredAutoSkills, true);
          this.UseAutoSkills(mAllUnit);
        }
      }
    }

    private void UseAutoSkills(Unit unit)
    {
      for (int index = 0; index < unit.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = unit.BattleSkills[index];
        if (battleSkill.Timing == ESkillTiming.Auto)
        {
          Debug.Log((object) "自動スキル発見");
          this.UseSkill(unit, unit.x, unit.y, battleSkill, false, 0, 0);
        }
      }
    }

    private bool CheckEnableNextClockTime()
    {
      for (int index = 0; index < this.mOrder.Count; ++index)
      {
        if (this.mOrder[index].CheckChargeTimeFullOver())
          return false;
      }
      return true;
    }

    private void NextClockTime()
    {
      ++this.mClockTime;
      ++this.mClockTimeTotal;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        mUnit.UpdateClockTime();
        if (mUnit.CheckEnableEntry())
        {
          this.EntryUnit(mUnit);
          this.mOrder.Add(new BattleCore.OrderData()
          {
            Unit = mUnit
          });
        }
      }
      for (int index = 0; index < this.mOrder.Count; ++index)
        this.mOrder[index].UpdateChargeTime();
    }

    private void CreateGimmickSkills()
    {
      BattleMap currentMap = this.CurrentMap;
      if (currentMap.GimmickSkills == null)
        return;
      for (int index1 = 0; index1 < currentMap.GimmickSkills.Count; ++index1)
      {
        JSON_GimmickSkill gimmickSkill1 = currentMap.GimmickSkills[index1];
        if (!string.IsNullOrEmpty(gimmickSkill1.skill) && gimmickSkill1.type != 0)
        {
          GimmickSkill gimmickSkill2 = new GimmickSkill();
          bool is_starter1 = false;
          this.GetConditionUnitByUnitID(gimmickSkill2.users, gimmickSkill1.su_iname);
          this.GetConditionUnitByUniqueName(gimmickSkill2.users, gimmickSkill1.su_tag, out is_starter1);
          if (string.IsNullOrEmpty(gimmickSkill1.su_iname) && string.IsNullOrEmpty(gimmickSkill1.su_tag) || gimmickSkill2.users.Count != 0)
          {
            this.GetConditionUnitByUnitID(gimmickSkill2.targets, gimmickSkill1.st_iname);
            bool is_starter2 = false;
            this.GetConditionUnitByUniqueName(gimmickSkill2.targets, gimmickSkill1.st_tag, out is_starter2);
            gimmickSkill2.IsStarter = is_starter2;
            string[] strArray = gimmickSkill1.skill.Split(',');
            if (strArray != null && strArray.Length > 0)
            {
              for (int index2 = 0; index2 < strArray.Length; ++index2)
                gimmickSkill2.skills.Add(strArray[index2]);
            }
            gimmickSkill2.condition.type = (GimmickSkillTriggerTypes) gimmickSkill1.type;
            gimmickSkill2.condition.count = gimmickSkill1.count;
            gimmickSkill2.condition.grids = new List<Grid>();
            for (int index2 = 0; index2 < gimmickSkill1.x.Length && index2 < gimmickSkill1.y.Length; ++index2)
            {
              Grid grid = currentMap[gimmickSkill1.x[index2], gimmickSkill1.y[index2]];
              if (grid != null)
                gimmickSkill2.condition.grids.Add(grid);
            }
            this.GetConditionUnitByUnitID(gimmickSkill2.condition.units, gimmickSkill1.cu_iname);
            this.GetConditionUnitByUniqueName(gimmickSkill2.condition.units, gimmickSkill1.cu_tag, out is_starter1);
            if (string.IsNullOrEmpty(gimmickSkill1.cu_iname) && string.IsNullOrEmpty(gimmickSkill1.cu_tag) || gimmickSkill2.condition.units.Count != 0)
            {
              this.GetConditionUnitByUnitID(gimmickSkill2.condition.targets, gimmickSkill1.ct_iname);
              this.GetConditionUnitByUniqueName(gimmickSkill2.condition.targets, gimmickSkill1.ct_tag, out is_starter1);
              this.mGimmickSkills.Add(gimmickSkill2);
            }
          }
        }
      }
    }

    public void GetConditionUnitByUnitID(List<Unit> results, string inames)
    {
      if (results == null || string.IsNullOrEmpty(inames))
        return;
      string[] strArray = inames.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.Units.Count; ++index2)
        {
          if (strArray[index1] == this.Units[index2].UnitParam.iname && !results.Contains(this.Units[index2]))
            results.Add(this.Units[index2]);
        }
      }
    }

    public void GetConditionUnitByUniqueName(List<Unit> results, string tags, out bool is_starter)
    {
      is_starter = false;
      if (results == null || string.IsNullOrEmpty(tags))
        return;
      string[] strArray = tags.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.Units.Count; ++index2)
        {
          if (this.CheckMatchUniqueName(this.Units[index2], strArray[index1]) && !results.Contains(this.Units[index2]))
            results.Add(this.Units[index2]);
          if (strArray[index1] == "starter")
            is_starter = true;
        }
      }
    }

    private void GimmickSkillDamageCount(Unit attacker, Unit defender)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index = 0; index < this.mGimmickSkills.Count; ++index)
      {
        GimmickSkill mGimmickSkill = this.mGimmickSkills[index];
        if (!mGimmickSkill.IsCompleted && mGimmickSkill.condition.type == GimmickSkillTriggerTypes.DamageCount)
        {
          GimmickSkillCondition condition = mGimmickSkill.condition;
          if ((condition.units.Count <= 0 || condition.units.Contains(attacker)) && condition.targets.Contains(defender))
          {
            ++mGimmickSkill.count;
            if (mGimmickSkill.IsStarter && condition.count <= mGimmickSkill.count)
              mGimmickSkill.starter = attacker;
          }
        }
      }
    }

    private void GimmickSkillDeadCount(Unit self, Unit target)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult) || target == null || !target.IsDead)
        return;
      for (int index = 0; index < this.mGimmickSkills.Count; ++index)
      {
        GimmickSkill mGimmickSkill = this.mGimmickSkills[index];
        if (!mGimmickSkill.IsCompleted && mGimmickSkill.condition.type == GimmickSkillTriggerTypes.DeadUnit)
        {
          GimmickSkillCondition condition = mGimmickSkill.condition;
          if ((condition.units.Count <= 0 || condition.units.Contains(self)) && condition.targets.Contains(target))
          {
            ++mGimmickSkill.count;
            if (mGimmickSkill.IsStarter && condition.count <= mGimmickSkill.count)
              mGimmickSkill.starter = self;
          }
        }
      }
    }

    private void GimmickSkillOnGrid()
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index1 = 0; index1 < this.mGimmickSkills.Count; ++index1)
      {
        GimmickSkill mGimmickSkill = this.mGimmickSkills[index1];
        if (!mGimmickSkill.IsCompleted && mGimmickSkill.IsStarter && (mGimmickSkill.starter == null && mGimmickSkill.condition.type == GimmickSkillTriggerTypes.OnGrid))
        {
          for (int index2 = 0; index2 < mGimmickSkill.condition.grids.Count; ++index2)
          {
            int x = mGimmickSkill.condition.grids[index2].x;
            int y = mGimmickSkill.condition.grids[index2].y;
            for (int index3 = 0; index3 < this.Units.Count; ++index3)
            {
              Unit unit = this.Units[index3];
              if (!unit.IsGimmick && unit.CheckExistMap() && (mGimmickSkill.condition.units.Count <= 0 || mGimmickSkill.condition.units.Contains(unit)) && (unit.x == x && unit.y == y))
                mGimmickSkill.starter = unit;
            }
          }
        }
      }
    }

    private bool CheckEnableGimmickSkill(GimmickSkill gimmick)
    {
      if (gimmick.IsCompleted)
        return false;
      if (gimmick.condition.count != 0)
        return gimmick.condition.count <= gimmick.count;
      if (gimmick.condition.type != GimmickSkillTriggerTypes.OnGrid)
        return false;
      for (int index1 = 0; index1 < gimmick.condition.grids.Count; ++index1)
      {
        int x = gimmick.condition.grids[index1].x;
        int y = gimmick.condition.grids[index1].y;
        for (int index2 = 0; index2 < this.Units.Count; ++index2)
        {
          Unit unit = this.Units[index2];
          if (!unit.IsGimmick && unit.CheckExistMap() && (gimmick.condition.units.Count <= 0 || gimmick.condition.units.Contains(unit)) && ((!gimmick.IsStarter || unit == gimmick.starter) && (unit.x == x && unit.y == y)))
            return true;
        }
      }
      return false;
    }

    private void UpdateGimmickSkillStart()
    {
      this.GimmickSkillOnGrid();
      for (int index1 = 0; index1 < this.mGimmickSkills.Count; ++index1)
      {
        GimmickSkill mGimmickSkill = this.mGimmickSkills[index1];
        if (this.CheckEnableGimmickSkill(mGimmickSkill))
        {
          if (mGimmickSkill.IsStarter && mGimmickSkill.starter != null && (mGimmickSkill.starter.CheckExistMap() && !mGimmickSkill.targets.Contains(mGimmickSkill.starter)))
            mGimmickSkill.targets.Add(mGimmickSkill.starter);
          int num = 0;
          for (int index2 = 0; index2 < mGimmickSkill.targets.Count; ++index2)
          {
            if (mGimmickSkill.targets[index2].CheckExistMap())
              ++num;
          }
          if (num != 0)
          {
            if (mGimmickSkill.users.Count > 0)
            {
              for (int index2 = 0; index2 < mGimmickSkill.users.Count; ++index2)
              {
                Unit user = mGimmickSkill.users[index2];
                if (user.CheckExistMap())
                {
                  for (int index3 = 0; index3 < mGimmickSkill.skills.Count; ++index3)
                  {
                    SkillData skill = user.GetSkillData(mGimmickSkill.skills[index3]);
                    if (skill == null)
                    {
                      skill = new SkillData();
                      skill.Setup(mGimmickSkill.skills[index3], 1, 1, (MasterParam) null);
                    }
                    LogSkill log = this.Log<LogSkill>();
                    log.self = user;
                    log.skill = skill;
                    if (mGimmickSkill.targets.Count == 1 && !skill.IsAllEffect())
                    {
                      log.pos.x = mGimmickSkill.targets[0].x;
                      log.pos.y = mGimmickSkill.targets[0].y;
                    }
                    else
                    {
                      log.pos.x = log.self.x;
                      log.pos.y = log.self.y;
                    }
                    log.is_append = !skill.IsCutin();
                    log.is_gimmick = true;
                    for (int index4 = 0; index4 < mGimmickSkill.targets.Count; ++index4)
                    {
                      if (mGimmickSkill.targets[index4].CheckExistMap())
                        log.SetSkillTarget(log.self, mGimmickSkill.targets[index4]);
                    }
                    this.ExecuteSkill(ESkillTiming.Used, log, skill);
                  }
                }
              }
            }
            else
            {
              for (int index2 = 0; index2 < mGimmickSkill.targets.Count; ++index2)
              {
                if (mGimmickSkill.targets[index2].CheckExistMap())
                {
                  for (int index3 = 0; index3 < mGimmickSkill.skills.Count; ++index3)
                  {
                    Unit target = mGimmickSkill.targets[index2];
                    SkillData skill = target.GetSkillData(mGimmickSkill.skills[index3]);
                    if (skill == null)
                    {
                      skill = new SkillData();
                      skill.Setup(mGimmickSkill.skills[index3], 1, 1, (MasterParam) null);
                    }
                    LogSkill log = this.Log<LogSkill>();
                    log.self = target;
                    log.skill = skill;
                    log.pos.x = log.self.x;
                    log.pos.y = log.self.y;
                    log.is_append = !skill.IsCutin();
                    log.is_gimmick = true;
                    log.SetSkillTarget(log.self, log.self);
                    this.ExecuteSkill(ESkillTiming.Used, log, skill);
                  }
                }
              }
            }
            mGimmickSkill.IsCompleted = true;
          }
        }
      }
    }

    private bool CheckMatchUniqueName(Unit self, string tag)
    {
      if (!string.IsNullOrEmpty(tag))
      {
        if (tag == self.UniqueName)
          return true;
        if (tag == "pall")
          return self.Side == EUnitSide.Player;
        if (tag == "eall")
          return self.Side == EUnitSide.Enemy;
      }
      return false;
    }

    private void UpdateEntryTriggers(UnitEntryTypes type, Unit target, SkillParam skill = null)
    {
      int num1 = (int) type;
      int num2 = 0;
      int num3 = 0;
      if (type == UnitEntryTypes.DecrementMember)
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (!mUnit.IsGimmick && mUnit.IsDeadCondition())
          {
            if (mUnit.Side == EUnitSide.Player)
              ++num2;
            else if (mUnit.Side == EUnitSide.Enemy)
              ++num3;
          }
        }
      }
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsGimmick && !mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsSub) && mUnit.EntryTriggers != null)
        {
          for (int index2 = 0; index2 < mUnit.EntryTriggers.Count; ++index2)
          {
            UnitEntryTrigger entryTrigger = mUnit.EntryTriggers[index2];
            if (!entryTrigger.on && entryTrigger.type == num1)
            {
              switch (type)
              {
                case UnitEntryTypes.DecrementMember:
                  if (mUnit.Side == EUnitSide.Player)
                  {
                    entryTrigger.on = num2 >= entryTrigger.value;
                    continue;
                  }
                  if (mUnit.Side == EUnitSide.Enemy)
                  {
                    entryTrigger.on = num3 >= entryTrigger.value;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.DecrementHp:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && entryTrigger.value >= (int) target.CurrentStatus.param.hp)
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.DeadEnemy:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && target.IsDead)
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.UsedSkill:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && skill != null && !(skill.iname != entryTrigger.skill))
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.OnGridEnemy:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && target.x == entryTrigger.x && target.y == entryTrigger.y)
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    private bool IsAllDead(EUnitSide side)
    {
      bool flag = true;
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
          {
            Unit unit = this.mPlayer[index];
            if (unit.IsEntry && !unit.IsGimmick)
              flag &= unit.IsDeadCondition();
          }
          break;
        case EUnitSide.Enemy:
          for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
          {
            Unit unit = this.mEnemys[this.MapIndex][index];
            if (unit.IsEntry && !unit.IsGimmick)
              flag &= unit.IsDeadCondition();
          }
          break;
      }
      return flag;
    }

    public BattleCore.QuestResult GetQuestResult()
    {
      BattleMap currentMap = this.CurrentMap;
      if (this.CheckMonitorGoalUnit(currentMap.WinMonitorCondition))
        return BattleCore.QuestResult.Win;
      if (this.CheckMonitorGoalUnit(currentMap.LoseMonitorCondition))
        return BattleCore.QuestResult.Lose;
      if (this.CheckMonitorActionCount(currentMap.WinMonitorCondition))
        return BattleCore.QuestResult.Win;
      if (this.CheckMonitorActionCount(currentMap.LoseMonitorCondition))
        return BattleCore.QuestResult.Lose;
      int mWinTriggerCount = this.mWinTriggerCount;
      int loseTriggerCount = this.mLoseTriggerCount;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].CheckWinEventTrigger())
          ++mWinTriggerCount;
        if (this.mUnits[index].CheckLoseEventTrigger())
          ++loseTriggerCount;
      }
      if ((int) this.mQuestParam.win != 0 && (int) this.mQuestParam.win <= mWinTriggerCount)
        return BattleCore.QuestResult.Win;
      if (this.IsMultiVersus)
      {
        if (this.IsVSForceWin)
          return this.IsVSForceWinComfirm ? BattleCore.QuestResult.Win : BattleCore.QuestResult.Pending;
        if (this.IsAllDead(EUnitSide.Player) && this.IsAllDead(EUnitSide.Enemy))
          return BattleCore.QuestResult.Draw;
        if (this.IsAllDead(EUnitSide.Player))
          return BattleCore.QuestResult.Lose;
        if (this.IsAllDead(EUnitSide.Enemy))
          return BattleCore.QuestResult.Win;
      }
      if (this.mEnemys != null)
      {
        bool flag = true;
        for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
        {
          Unit unit = this.mEnemys[this.MapIndex][index];
          if (unit.IsEntry && !unit.IsGimmick)
            flag &= unit.IsDead;
        }
        if (flag)
          return BattleCore.QuestResult.Win;
      }
      if (this.mQuestParam.OverClockTimeWin > 0 && (int) this.mClockTimeTotal > this.mQuestParam.OverClockTimeWin)
        return BattleCore.QuestResult.Win;
      if ((int) this.mQuestParam.lose != 0 && (int) this.mQuestParam.lose <= loseTriggerCount)
        return BattleCore.QuestResult.Lose;
      if (this.mPlayer != null)
      {
        bool flag = true;
        for (int index = 0; index < this.mPlayer.Count; ++index)
        {
          Unit unit = this.mPlayer[index];
          if (unit.IsEntry && !unit.IsGimmick)
            flag &= unit.IsDeadCondition();
        }
        if (flag)
          return BattleCore.QuestResult.Lose;
      }
      if (this.mQuestParam.OverClockTimeLose > 0 && (int) this.mClockTimeTotal > this.mQuestParam.OverClockTimeLose)
        return BattleCore.QuestResult.Lose;
      if (this.mQuestParam.type == QuestTypes.Arena)
      {
        if ((int) this.mArenaActionCount == 0)
          return BattleCore.QuestResult.Lose;
        if (this.mIsArenaSkip)
          return this.mArenaCalcResult;
      }
      if (!this.IsMultiVersus || (int) this.RemainVersusTurnCount != 0)
        return BattleCore.QuestResult.Pending;
      int hpRate1 = this.GetHpRate(EUnitSide.Player);
      int hpRate2 = this.GetHpRate(EUnitSide.Enemy);
      if (hpRate1 > hpRate2)
        return BattleCore.QuestResult.Win;
      return hpRate2 > hpRate1 ? BattleCore.QuestResult.Lose : BattleCore.QuestResult.Draw;
    }

    private int GetHpRate(EUnitSide side)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
          {
            num1 += (int) this.mPlayer[index].MaximumStatus.param.hp;
            num2 += (int) this.mPlayer[index].CurrentStatus.param.hp;
          }
          break;
        case EUnitSide.Enemy:
          List<Unit> mEnemy = this.mEnemys[this.MapIndex];
          for (int index = 0; index < mEnemy.Count; ++index)
          {
            num1 += (int) mEnemy[index].MaximumStatus.param.hp;
            num2 += (int) mEnemy[index].CurrentStatus.param.hp;
          }
          break;
      }
      if (num1 > 0)
        num3 = num2 * 100 / num1;
      return num3;
    }

    private bool CheckMonitorActionCount(QuestMonitorCondition cond)
    {
      if (cond.actions.Count > 0)
      {
        for (int index1 = 0; index1 < cond.actions.Count; ++index1)
        {
          UnitMonitorCondition action = cond.actions[index1];
          if (!string.IsNullOrEmpty(action.tag))
          {
            if (action.tag == "pall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Player && this.CheckMonitorActionCountCondition(this.mUnits[index2], action))
                  return true;
              }
              continue;
            }
            if (action.tag == "eall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Enemy && this.CheckMonitorActionCountCondition(this.mUnits[index2], action))
                  return true;
              }
              continue;
            }
            if (this.CheckMonitorActionCountCondition(this.FindUnitByUniqueName(action.tag), action))
              return true;
          }
          if (!string.IsNullOrEmpty(action.iname))
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              Unit mUnit = this.mUnits[index2];
              if (!(mUnit.UnitParam.iname != action.iname) && this.CheckMonitorActionCountCondition(mUnit, action))
                return true;
            }
          }
        }
      }
      return false;
    }

    private bool CheckMonitorActionCountCondition(Unit unit, UnitMonitorCondition monitor)
    {
      return unit != null && !unit.IsGimmick && (unit.CheckExistMap() && unit.ActionCount >= monitor.turn);
    }

    public bool CheckEnableRemainingActionCount(QuestMonitorCondition cond)
    {
      if (cond == null || cond.actions.Count == 0)
        return false;
      for (int index1 = 0; index1 < cond.actions.Count; ++index1)
      {
        UnitMonitorCondition action = cond.actions[index1];
        if (action.turn > 0)
        {
          if (!string.IsNullOrEmpty(action.tag) && (action.tag == "pall" || action.tag == "eall" || this.FindUnitByUniqueName(action.tag) != null))
            return true;
          if (!string.IsNullOrEmpty(action.iname))
          {
            for (int index2 = 0; index2 < this.Units.Count; ++index2)
            {
              if (this.Units[index2].UnitParam.iname == action.iname)
                return true;
            }
          }
        }
      }
      return false;
    }

    public int GetRemainingActionCount(QuestMonitorCondition cond)
    {
      if (cond == null || cond.actions.Count == 0)
        return -1;
      int val1 = (int) byte.MaxValue;
      for (int index1 = 0; index1 < cond.actions.Count; ++index1)
      {
        UnitMonitorCondition action = cond.actions[index1];
        if (!string.IsNullOrEmpty(action.tag))
        {
          if (action.tag == "pall")
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              if (this.mUnits[index2].Side == EUnitSide.Player)
                val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
            }
          }
          if (action.tag == "eall")
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              if (this.mUnits[index2].Side == EUnitSide.Enemy)
                val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
            }
          }
          Unit unitByUniqueName = this.FindUnitByUniqueName(action.tag);
          if (unitByUniqueName != null)
            val1 = Math.Min(val1, action.turn - unitByUniqueName.ActionCount);
        }
        else if (!string.IsNullOrEmpty(action.iname))
        {
          for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
          {
            if (!(this.mUnits[index2].UnitParam.iname != action.iname))
              val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
          }
        }
      }
      if (val1 != (int) byte.MaxValue)
        return Math.Max(val1, 0);
      return -1;
    }

    private bool CheckMonitorGoalUnit(QuestMonitorCondition cond)
    {
      if (cond.goals.Count > 0)
      {
        for (int index1 = 0; index1 < cond.goals.Count; ++index1)
        {
          UnitMonitorCondition goal = cond.goals[index1];
          if (!string.IsNullOrEmpty(goal.tag))
          {
            if (goal.tag == "pall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Player && this.CheckMonitorGoalCondition(this.mUnits[index2], goal))
                  return true;
              }
              continue;
            }
            if (goal.tag == "eall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Enemy && this.CheckMonitorGoalCondition(this.mUnits[index2], goal))
                  return true;
              }
              continue;
            }
            if (this.CheckMonitorGoalCondition(this.FindUnitByUniqueName(goal.tag), goal))
              return true;
          }
          if (!string.IsNullOrEmpty(goal.iname))
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              Unit mUnit = this.mUnits[index2];
              if (!(mUnit.UnitParam.iname != goal.iname) && this.CheckMonitorGoalCondition(mUnit, goal))
                return true;
            }
          }
        }
      }
      return false;
    }

    private bool CheckMonitorGoalCondition(Unit unit, UnitMonitorCondition monitor)
    {
      return unit != null && !unit.IsGimmick && (unit.CheckExistMap() && unit.x == monitor.x) && (unit.y == monitor.y && (monitor.turn <= 0 || monitor.turn >= unit.ActionCount));
    }

    public bool IsBonusObjectiveComplete(QuestBonusObjective bonus)
    {
      switch (bonus.Type)
      {
        case EMissionType.KillAllEnemy:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Enemy && !this.Units[index].IsDead)
              return false;
          }
          return true;
        case EMissionType.NoDeath:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && this.Units[index].IsDead)
              return false;
          }
          return true;
        case EMissionType.LimitedTurn:
          int result1;
          if (int.TryParse(bonus.TypeParam, out result1))
            return this.mActionCount <= result1;
          return false;
        case EMissionType.MaxSkillCount:
          int result2;
          if (int.TryParse(bonus.TypeParam, out result2))
            return this.mNumUsedSkills <= result2;
          return false;
        case EMissionType.MaxItemCount:
          int result3;
          if (int.TryParse(bonus.TypeParam, out result3))
            return this.mNumUsedItems <= result3;
          return false;
        case EMissionType.MaxPartySize:
          int num = 0;
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember)
              ++num;
          }
          int result4;
          if (int.TryParse(bonus.TypeParam, out result4))
            return num <= result4;
          return false;
        case EMissionType.LimitedUnitElement:
          EElement eelement;
          try
          {
            eelement = (EElement) Enum.Parse(typeof (EElement), bonus.TypeParam, true);
          }
          catch (Exception ex)
          {
            return false;
          }
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && this.Units[index].Element != eelement)
              return false;
          }
          return true;
        case EMissionType.LimitedUnitID:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && this.Units[index].UnitParam.iname == bonus.TypeParam)
              return true;
          }
          return false;
        case EMissionType.NoMercenary:
          return this.Friend == null;
        case EMissionType.Killstreak:
          int result5;
          if (int.TryParse(bonus.TypeParam, out result5))
            return this.mMaxKillstreak >= result5;
          return false;
        case EMissionType.TotalHealHPMax:
          int result6;
          if (int.TryParse(bonus.TypeParam, out result6))
            return this.mTotalHeal <= result6;
          return false;
        case EMissionType.TotalHealHPMin:
          int result7;
          if (int.TryParse(bonus.TypeParam, out result7))
            return this.mTotalHeal >= result7;
          return false;
        case EMissionType.TotalDamagesTakenMin:
          int result8;
          if (int.TryParse(bonus.TypeParam, out result8))
            return this.mTotalDamagesTaken >= result8;
          return false;
        case EMissionType.TotalDamagesTakenMax:
          int result9;
          if (int.TryParse(bonus.TypeParam, out result9))
            return this.mTotalDamagesTaken <= result9;
          return false;
        case EMissionType.TotalDamagesMin:
          int result10;
          if (int.TryParse(bonus.TypeParam, out result10))
            return this.mTotalDamages >= result10;
          return false;
        case EMissionType.TotalDamagesMax:
          int result11;
          if (int.TryParse(bonus.TypeParam, out result11))
            return this.mTotalDamages <= result11;
          return false;
        case EMissionType.LimitedCT:
          int result12;
          if (int.TryParse(bonus.TypeParam, out result12))
            return (int) this.mClockTimeTotal <= result12;
          return false;
        case EMissionType.LimitedContinue:
          int result13;
          if (int.TryParse(bonus.TypeParam, out result13))
            return this.mContinueCount <= result13;
          return false;
        case EMissionType.NoNpcDeath:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && !this.Units[index].IsPartyMember && this.Units[index].IsDead)
              return false;
          }
          return true;
        default:
          return false;
      }
    }

    private void CalcQuestRecord()
    {
      this.mRecord.result = this.GetQuestResult();
      this.mRecord.playerexp = this.mQuestParam.pexp;
      this.mRecord.gold = this.mQuestParam.gold;
      this.mRecord.unitexp = this.mQuestParam.uexp;
      this.mRecord.multicoin = this.mQuestParam.mcoin;
      this.mRecord.items.Clear();
      this.mRecord.bonusFlags = 0;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (this.mRecord.result == BattleCore.QuestResult.Win && this.mQuestParam.bonusObjective != null)
      {
        for (int index1 = 0; index1 < this.mQuestParam.bonusObjective.Length; ++index1)
        {
          if (((int) this.mQuestParam.clear_missions & 1 << index1) == 0 && this.IsBonusObjectiveComplete(this.mQuestParam.bonusObjective[index1]))
          {
            ItemParam itemParam = instance.GetItemParam(this.mQuestParam.bonusObjective[index1].item);
            if (itemParam != null)
            {
              for (int index2 = 0; index2 < this.mQuestParam.bonusObjective[index1].itemNum; ++index2)
                this.mRecord.items.Add(itemParam);
            }
            this.mRecord.bonusFlags |= 1 << index1;
          }
        }
      }
      this.GainUnitSteal();
      this.GainUnitDrop();
      this.mRecord.units.Clear();
      this.GainRankMatchItem();
    }

    private void GainUnitDrop()
    {
      int length = this.mAllUnits.Count - this.mNpcStartIndex;
      if (length == 0)
        return;
      Array.Clear((Array) this.mRecord.drops, 0, length);
      int mNpcStartIndex = this.mNpcStartIndex;
      int index1 = 0;
      while (mNpcStartIndex < this.mAllUnits.Count)
      {
        Unit mAllUnit = this.mAllUnits[mNpcStartIndex];
        if (mAllUnit.CheckItemDrop())
        {
          Unit.UnitDrop drop = mAllUnit.Drop;
          for (int index2 = 0; index2 < drop.items.Count; ++index2)
          {
            for (int index3 = 0; index3 < (int) drop.items[index2].num; ++index3)
              this.mRecord.items.Add(drop.items[index2].param);
          }
          BattleCore.Record mRecord = this.mRecord;
          mRecord.gold = (OInt) ((int) mRecord.gold + (int) drop.gold);
          this.mRecord.drops[index1] = (OInt) 1;
        }
        ++mNpcStartIndex;
        ++index1;
      }
    }

    private void GainUnitSteal()
    {
      int length = this.mAllUnits.Count - this.mNpcStartIndex;
      if (length == 0)
        return;
      Array.Clear((Array) this.mRecord.item_steals, 0, length);
      Array.Clear((Array) this.mRecord.gold_steals, 0, length);
      int mNpcStartIndex = this.mNpcStartIndex;
      int index1 = 0;
      while (mNpcStartIndex < this.mAllUnits.Count)
      {
        Unit mAllUnit = this.mAllUnits[mNpcStartIndex];
        if (mAllUnit.IsGimmick)
          break;
        Unit.UnitSteal steal = mAllUnit.Steal;
        if (steal.is_item_steeled)
        {
          for (int index2 = 0; index2 < steal.items.Count; ++index2)
            this.mRecord.items.Add(steal.items[index2].param);
          this.mRecord.item_steals[index1] = (OInt) 1;
        }
        if (steal.is_gold_steeled)
        {
          BattleCore.Record mRecord = this.mRecord;
          mRecord.gold = (OInt) ((int) mRecord.gold + (int) steal.gold);
          this.mRecord.gold_steals[index1] = (OInt) 1;
        }
        ++mNpcStartIndex;
        ++index1;
      }
    }

    private void GainConvertedGold()
    {
      if (this.mRecord.items == null)
        return;
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      for (int index1 = 0; index1 < this.mRecord.items.Count; ++index1)
      {
        string iname = this.mRecord.items[index1].iname;
        if (dictionary1.ContainsKey(iname))
        {
          Dictionary<string, int> dictionary2;
          string index2;
          (dictionary2 = dictionary1)[index2 = iname] = dictionary2[index2] + 1;
        }
        else
          dictionary1[iname] = 1;
      }
      using (Dictionary<string, int>.Enumerator enumerator = dictionary1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, int> current = enumerator.Current;
          int num1 = current.Value;
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(current.Key);
          if (itemParam != null)
          {
            ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(current.Key);
            if (itemDataByItemId != null)
              num1 += itemDataByItemId.Num;
            int num2 = num1 - (int) itemParam.cap;
            if (num2 > 0)
            {
              BattleCore.Record mRecord = this.mRecord;
              mRecord.gold = (OInt) ((int) mRecord.gold + num2 * (int) itemParam.sell);
            }
          }
        }
      }
    }

    private void GainRankMatchItem()
    {
      if (!this.IsMultiVersus || GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Tower)
        return;
      BattleCore.QuestResult questResult = this.GetQuestResult();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      List<string> Items = new List<string>();
      List<int> Nums = new List<int>();
      instance.GetTowerMatchItems(player.VersusTowerFloor - 1, Items, Nums, questResult == BattleCore.QuestResult.Win);
      instance.GetVersusTopFloorItems(player.VersusTowerFloor - 1, Items, Nums);
      for (int index1 = 0; index1 < Items.Count; ++index1)
      {
        ItemParam itemParam = instance.GetItemParam(Items[index1]);
        if (itemParam != null)
        {
          for (int index2 = 0; index2 < Nums[index1]; ++index2)
            this.mRecord.items.Add(itemParam);
        }
      }
    }

    public BattleCore.Record GetQuestRecord()
    {
      return this.mRecord;
    }

    private bool CheckJudgeBattle()
    {
      switch (this.GetQuestResult())
      {
        case BattleCore.QuestResult.Win:
        case BattleCore.QuestResult.Lose:
        case BattleCore.QuestResult.Draw:
          return true;
        default:
          return false;
      }
    }

    public bool CheckNextMap()
    {
      return this.mMapIndex < this.mMap.Count - 1;
    }

    public int GetGems(Unit unit)
    {
      return unit.Gems;
    }

    public void SetGems(Unit unit, int gems)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      unit.Gems = Math.Max(Math.Min(gems, (int) unit.MaximumStatus.param.mp), 0);
    }

    private int AddGems(Unit unit, int gems)
    {
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        unit.Gems = Math.Max(Math.Min(unit.Gems + gems, (int) unit.MaximumStatus.param.mp), 0);
      return unit.Gems;
    }

    private int SubGems(Unit unit, int gems)
    {
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        unit.Gems = Math.Max(Math.Min(unit.Gems - gems, (int) unit.MaximumStatus.param.mp), 0);
      return unit.Gems;
    }

    public Grid GetUnitGridPosition(Unit unit)
    {
      if (unit == null)
        return (Grid) null;
      BattleMap currentMap = this.CurrentMap;
      if (currentMap == null)
        return (Grid) null;
      return currentMap[unit.x, unit.y];
    }

    public Unit FindUnitByUniqueName(string uniqname)
    {
      if (string.IsNullOrEmpty(uniqname))
        return (Unit) null;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (!(uniqname != this.mUnits[index].UniqueName))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public Unit FindUnitAtGrid(int x, int y)
    {
      return this.FindUnitAtGrid(this.CurrentMap[x, y]);
    }

    public Unit FindUnitAtGrid(Grid grid)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UnitType == EUnitType.Unit && this.mUnits[index].CheckCollision(grid))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public Unit FindUnitAtGridIgnoreSide(Grid grid, EUnitSide ignoreSide)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UnitType == EUnitType.Unit && this.mUnits[index].Side == ignoreSide && this.mUnits[index].CheckCollision(grid))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public Unit FindGimmickAtGrid(Grid grid, bool is_valid_disable = false)
    {
      if (grid == null)
        return (Unit) null;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].IsGimmick && (is_valid_disable || !this.mUnits[index].IsDisableGimmick()) && this.mUnits[index].CheckCollision(grid))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public bool CommandWait(EUnitDirection dir)
    {
      if (!this.CurrentUnit.IsDead)
        this.CurrentUnit.Direction = dir;
      return this.CommandWait();
    }

    public bool CommandWait()
    {
      this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
      this.Log<LogMapWait>().self = this.CurrentUnit;
      this.ExecuteEventTriggerOnGrid(this.CurrentUnit, EEventTrigger.Stop, false);
      this.InternalLogUnitEnd();
      if (this.CheckEnableSuspendSave() && !this.IsUnitAuto(this.CurrentUnit))
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.Wait,
          uid = this.CurrentUnit.UnitData.UniqueID,
          x = this.CurrentUnit.x,
          y = this.CurrentUnit.y,
          dir = (int) this.CurrentUnit.Direction
        });
      return true;
    }

    private void InternalLogUnitEnd()
    {
      this.Log<LogUnitEnd>().self = this.CurrentUnit;
    }

    public static EUnitDirection UnitDirectionFromVector(Unit self, int x, int y)
    {
      int num1 = Math.Abs(x);
      int num2 = Math.Abs(y);
      if (num1 > num2)
      {
        if (x < 0)
          return EUnitDirection.NegativeX;
        if (x > 0)
          return EUnitDirection.PositiveX;
      }
      if (num1 < num2)
      {
        if (y < 0)
          return EUnitDirection.NegativeY;
        if (y > 0)
          return EUnitDirection.PositiveY;
      }
      if (x > 0)
        return EUnitDirection.PositiveX;
      if (x < 0)
        return EUnitDirection.NegativeX;
      if (y > 0)
        return EUnitDirection.PositiveY;
      if (y < 0)
        return EUnitDirection.NegativeY;
      if (self != null)
        return self.Direction;
      return EUnitDirection.PositiveY;
    }

    public bool Move(Unit self, Grid goal, bool forceAI = false)
    {
      return this.Move(self, goal, EUnitDirection.Auto, false, forceAI);
    }

    public bool CheckMove(Unit self, Grid goal)
    {
      this.DebugAssert(self != null, "self == null");
      if (goal == null)
        return false;
      this.DebugAssert(this.CurrentMap != null, "map == null");
      Unit unitAtGrid = this.FindUnitAtGrid(goal);
      return unitAtGrid == null || self == unitAtGrid;
    }

    public bool MoveMultiPlayer(Unit self, int x, int y, EUnitDirection direction)
    {
      if (!this.CheckMove(self, this.CurrentMap[x, y]))
        return false;
      int step = (self.x >= x ? self.x - x : x - self.x) + (self.y >= y ? self.y - y : y - self.y);
      self.x = x;
      self.y = y;
      self.Direction = direction;
      this.DebugLog("[PUN]MoveMultiPlayer unitID:" + (object) this.mPlayer.FindIndex((Predicate<Unit>) (p => p == self)) + " x:" + (object) x + " y:" + (object) y + " d:" + (object) direction);
      this.MoveEnd(self, step, true);
      return true;
    }

    public bool Move(Unit self, Grid goal, EUnitDirection direction, bool isStickControl = false, bool forceAI = false)
    {
      this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
      this.DebugAssert(self != null, "self == null");
      if (goal == null)
        return false;
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      bool flag1 = forceAI || this.IsUnitAuto(self);
      if (!flag1 && !self.IsUnitFlag(EUnitFlag.ForceMoved) && !this.CheckMove(self, goal))
        return false;
      if (isStickControl)
      {
        int step = (self.x >= goal.x ? self.x - goal.x : goal.x - self.x) + (self.y >= goal.y ? self.y - goal.y : goal.y - self.y);
        self.x = goal.x;
        self.y = goal.y;
        self.Direction = direction;
        LogMapMove logMapMove = this.Log<LogMapMove>();
        logMapMove.self = self;
        logMapMove.ex = self.x;
        logMapMove.ey = self.y;
        logMapMove.dir = self.Direction;
        logMapMove.auto = flag1;
        this.MoveEnd(self, step, false);
        return true;
      }
      currentMap.ResetMoveRoutes();
      currentMap.CalcMoveSteps(self, goal);
      currentMap.CalcMoveRoutes(self);
      int moveRoutesCount = currentMap.GetMoveRoutesCount();
      if (currentMap.GetNextMoveRoutes() == null)
        self.Direction = direction == EUnitDirection.Auto ? BattleCore.UnitDirectionFromVector(self, goal.x - self.x, goal.y - self.y) : direction;
      LogMapMove logMapMove1 = this.Log<LogMapMove>();
      logMapMove1.self = self;
      logMapMove1.ex = self.x;
      logMapMove1.ey = self.y;
      logMapMove1.dir = self.Direction;
      logMapMove1.auto = flag1;
      this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) currentMap[self.x, self.y].height + " から移動開始");
      while (true)
      {
        int x1 = self.x;
        int y1 = self.y;
        Grid nextMoveRoutes = currentMap.GetNextMoveRoutes();
        if (nextMoveRoutes != null)
        {
          bool flag2 = currentMap.IsLastMoveGrid(nextMoveRoutes);
          self.x = nextMoveRoutes.x;
          self.y = nextMoveRoutes.y;
          this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) nextMoveRoutes.height + " へ移動");
          if (flag2 && direction != EUnitDirection.Auto)
          {
            self.Direction = direction;
          }
          else
          {
            int x2 = nextMoveRoutes.x - x1;
            int y2 = nextMoveRoutes.y - y1;
            if (flag2 && flag1)
            {
              if (self.IsUnitFlag(EUnitFlag.Escaped))
              {
                x2 = x1 - nextMoveRoutes.x;
                y2 = y1 - nextMoveRoutes.y;
              }
              int num1 = this.mSafeMap.get(x1, y1);
              for (int index = 0; index < 4; ++index)
              {
                int num2 = Unit.DIRECTION_OFFSETS[index, 0];
                int num3 = Unit.DIRECTION_OFFSETS[index, 1];
                if (this.mSafeMap.isValid(nextMoveRoutes.x + num2, nextMoveRoutes.y + num3))
                {
                  int num4 = this.mSafeMap.get(nextMoveRoutes.x + num2, nextMoveRoutes.y + num3);
                  if (num4 < num1)
                  {
                    num1 = num4;
                    x2 = num2;
                    y2 = num3;
                  }
                }
              }
            }
            self.Direction = BattleCore.UnitDirectionFromVector(self, x2, y2);
          }
          LogMapMove logMapMove2 = this.Log<LogMapMove>();
          logMapMove2.self = self;
          logMapMove2.ex = nextMoveRoutes.x;
          logMapMove2.ey = nextMoveRoutes.y;
          logMapMove2.dir = self.Direction;
          logMapMove2.auto = flag1;
          currentMap.IncrementMoveStep();
        }
        else
          break;
      }
      this.MoveEnd(self, moveRoutesCount, false);
      this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) currentMap[self.x, self.y].height + " で停止");
      self.SetUnitFlag(EUnitFlag.Escaped, false);
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetCommandFlag(EUnitCommandFlag.Move, true);
      return true;
    }

    private void MoveEnd(Unit self, int step, bool isMultiPlayer = false)
    {
      self.SetUnitFlag(EUnitFlag.Escaped, false);
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetCommandFlag(EUnitCommandFlag.Move, true);
      this.EndMoveSkill(self, step);
      for (int index = 0; index < this.Logs.Num; ++index)
      {
        if (this.Logs[index] is LogMapEnd || this.Logs[index] is LogUnitEnd)
          return;
      }
      int index1 = Math.Max(this.Logs.Num - 1, 0);
      if (self.CastSkill != null && self.CastSkill.LineType != ELineType.None)
        self.CancelCastSkill();
      if (!(this.Logs[index1] is LogMapMove))
      {
        if (self.IsDead)
          this.InternalLogUnitEnd();
        else if (!isMultiPlayer)
          this.Log<LogMapCommand>();
      }
      self.RefleshMomentBuff(this.Units);
      if (!this.CheckEnableSuspendSave() || this.IsUnitAuto(self))
        return;
      this.mSuspendData.Add(new BattleCore.SuspendData()
      {
        timing = BattleCore.SuspendTiming.Move,
        x = self.x,
        y = self.y,
        dir = (int) self.Direction
      });
    }

    public void MapCommandEnd(Unit self)
    {
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetUnitFlag(EUnitFlag.Action, true);
    }

    public static int Sqrt(int v)
    {
      if (v <= 0)
        return 0;
      int num1 = v <= 1 ? 1 : v;
      int num2;
      do
      {
        num2 = num1;
        num1 = (v / num1 + num1) / 2;
      }
      while (num1 < num2);
      return num2;
    }

    public static int Sin(int v)
    {
      int num1 = v - v / 628 * 628;
      int num2 = num1;
      int num3 = num1;
      int num4 = 10;
      for (int index = 1; index <= num4; ++index)
      {
        num2 *= -(num1 * num1) / ((2 * num4 + 1) * (2 * num4));
        num3 += num2;
      }
      return num3;
    }

    public static int Atan(int x)
    {
      int num1 = 0;
      for (int index = 0; index < 100; ++index)
      {
        int num2 = -1 ^ index * (1 / x ^ 2 * index + 1) / (2 * index + 1);
        num1 += num2;
      }
      return (9000 - 180 * num1 * 100) / 314;
    }

    public static int Pow(int val, int n)
    {
      int num = 1;
      for (int index = 1; index <= n; ++index)
        num *= val;
      return num;
    }

    public void GetSkillGridLines(Unit self, int ex, int ey, SkillData skill, ref List<Grid> results)
    {
      DebugUtility.Assert(self != null, "self == null");
      DebugUtility.Assert(skill != null, "skill == null");
      int attackRangeMax = self.GetAttackRangeMax(skill);
      if (attackRangeMax <= 0)
        return;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackHeight = self.GetAttackHeight(skill);
      ELineType lineType = skill.LineType;
      bool bHeightBonus = skill.IsEnableHeightRangeBonus();
      this.GetSkillGridLines(self.x, self.y, ex, ey, attackRangeMin, attackRangeMax, attackHeight, lineType, bHeightBonus, ref results);
    }

    public void GetSkillGridLines(int sx, int sy, int ex, int ey, int range_min, int range_max, int attack_height, ELineType line_type, bool bHeightBonus, ref List<Grid> results)
    {
      results.Clear();
      BattleMap currentMap = this.CurrentMap;
      Grid start = currentMap[sx, sy];
      Grid grid = currentMap[ex, ey];
      if (range_max == 0)
        return;
      int num1 = range_max;
      if (bHeightBonus)
        num1 += this.GetAttackRangeBonus(start.height, 0);
      int num2 = 100;
      int _x_ = (ex - sx) * num2;
      int _y_ = (ey - sy) * num2;
      BattleCore.SVector2 svector2 = new BattleCore.SVector2(_x_, _y_);
      int num3 = svector2.Length();
      if (range_min > 0 && range_max > 0 && num3 <= range_min)
        return;
      switch (line_type)
      {
        case ELineType.Direct:
          int tx1 = sx;
          int ty1 = sy;
          if (num3 != 0)
          {
            svector2.x = svector2.x * num2 / num3;
            svector2.y = svector2.y * num2 / num3;
            svector2 *= num1;
            tx1 += svector2.x / num2;
            ty1 += svector2.y / num2;
          }
          this.GetGridOnLine(start, tx1, ty1, ref results);
          for (int index = 0; index < results.Count; ++index)
          {
            int num4 = this.CalcGridDistance(start, results[index]);
            int num5 = range_max - num4;
            int attackRangeBonus = this.GetAttackRangeBonus(start.height, results[index].height);
            if (bHeightBonus)
              num5 += attackRangeBonus;
            if (num5 < 0)
              results.RemoveRange(index, results.Count - index);
          }
          break;
        case ELineType.Curved:
          this.GetGridOnLine(start, ex, ey, ref results);
          for (int index1 = 0; index1 < results.Count; ++index1)
          {
            int num4 = this.CalcGridDistance(start, results[index1]);
            int num5 = num1 - num4;
            int val1 = 0;
            for (int index2 = 0; index2 <= index1; ++index2)
              val1 = Math.Min(val1, this.GetAttackRangeBonus(start.height, results[index2].height));
            if (num5 + val1 < 0)
              results.RemoveRange(index1, results.Count - index1);
          }
          break;
        case ELineType.Stab:
          int num6 = (grid.height - start.height) * num2;
          if (num1 * num2 + 50 < BattleCore.Sqrt(_x_ * _x_ + _y_ * _y_ + num6 * num6))
            return;
          int tx2 = sx;
          int ty2 = sy;
          if (num3 != 0)
          {
            svector2.x = svector2.x * num2 / num3;
            svector2.y = svector2.y * num2 / num3;
            svector2 *= num1;
            tx2 += svector2.x / num2;
            ty2 += svector2.y / num2;
          }
          this.GetGridOnLine(start, tx2, ty2, ref results);
          break;
        default:
          return;
      }
      switch (line_type)
      {
        case ELineType.Direct:
        case ELineType.Stab:
          int num7 = (grid.height - start.height) * num2;
          int num8 = BattleMap.MAP_FLOOR_HEIGHT * num2 / 2;
          for (int index = 0; index < results.Count; ++index)
          {
            int num4 = new BattleCore.SVector2((results[index].x - sx) * num2, (results[index].y - sy) * num2).Length();
            int num5 = 0;
            if (num7 != 0 && num3 != 0)
            {
              int num9 = num4 * num2 / num3;
              num5 = num7 * num9 / num2;
            }
            int num10 = start.height * num2 + num8 + num5;
            int num11 = results[index].height * num2;
            int num12 = num11 + num8;
            if (num10 < num11)
            {
              results.RemoveRange(index, results.Count - index);
              break;
            }
            if (num10 > num12)
              results.RemoveAt(index--);
          }
          break;
        case ELineType.Curved:
          if (!results.Contains(grid))
          {
            results.Clear();
            break;
          }
          double num13 = 0.0;
          for (int index = 0; index < results.Count; ++index)
          {
            if (num13 < (double) results[index].height)
              num13 = (double) results[index].height;
          }
          double num14 = num13 + 1.0;
          bool flag = true;
          double val2 = (double) num3 / 100.0;
          double num15 = (double) (start.height - grid.height);
          double num16 = 9.8;
          for (int index1 = 0; index1 < attack_height; ++index1)
          {
            double num4 = num14 + (double) index1;
            double d1 = 2.0 * num16 * (num4 - num15);
            double d2 = 2.0 * num16 * num4;
            double num5 = d1 <= 0.0 ? 0.0 : Math.Sqrt(d1);
            double num9 = d2 <= 0.0 ? 0.0 : Math.Sqrt(d2);
            double num10 = (num5 + num9) / num16;
            double d3 = Math.Pow(val2 / num10, 2.0) + 2.0 * num16 * (num4 - num15);
            double num11 = d3 <= 0.0 ? 0.0 : Math.Sqrt(d3);
            double num12 = num10 / val2;
            double a = Math.Atan(num10 * num5 / val2);
            flag = true;
            for (int index2 = 0; index2 < results.Count; ++index2)
            {
              int num17 = this.mGridLines[index2].x - start.x;
              int num18 = this.mGridLines[index2].y - start.y;
              double num19 = Math.Min((double) BattleCore.Sqrt(num17 * num17 + num18 * num18), val2);
              double x = num12 * num19;
              double num20 = Math.Sin(a);
              double num21 = Math.Pow(x, 2.0);
              double num22 = num16 * num21 * 0.5;
              if (num11 * x * num20 - num22 < (double) (results[index2].height - start.height) - 0.01)
              {
                flag = false;
                break;
              }
            }
            if (flag)
              break;
          }
          if (flag)
            break;
          results.Clear();
          break;
        default:
          if (grid.height - start.height <= attack_height)
            break;
          results.Remove(grid);
          break;
      }
    }

    private int GetCriticalRate(Unit self, Unit target, SkillData skill)
    {
      if (skill == null || !skill.IsNormalAttack() && !(bool) skill.SkillParam.IsCritical)
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int num1 = 1000;
      int criticalRateCriMultiply = (int) fixParam.CriticalRate_Cri_Multiply;
      int criticalRateCriDivision = (int) fixParam.CriticalRate_Cri_Division;
      int num2 = BattleCore.Sqrt((int) self.CurrentStatus.param.cri * num1 * num1);
      if (num2 != 0)
      {
        if (criticalRateCriMultiply != 0)
          num2 *= criticalRateCriMultiply;
        if (criticalRateCriDivision != 0)
          num2 /= criticalRateCriDivision;
      }
      int criticalRateLukMultiply = (int) fixParam.CriticalRate_Luk_Multiply;
      int criticalRateLukDivision = (int) fixParam.CriticalRate_Luk_Division;
      int num3 = BattleCore.Sqrt((int) target.CurrentStatus.param.luk * num1 * num1);
      if (num3 != 0)
      {
        if (criticalRateLukMultiply != 0)
          num3 *= criticalRateLukMultiply;
        if (criticalRateLukDivision != 0)
          num3 /= criticalRateLukDivision;
      }
      int num4 = num2 - num3;
      if (num4 != 0)
        num4 /= num1;
      Grid unitGridPosition1 = this.GetUnitGridPosition(self);
      Grid unitGridPosition2 = this.GetUnitGridPosition(target);
      if (unitGridPosition1 != null && unitGridPosition2 != null)
      {
        int num5 = unitGridPosition1.height - unitGridPosition2.height;
        if (num5 > 0)
          num4 += (int) fixParam.HighGridCriRate;
        else if (num5 < 0)
          num4 += (int) fixParam.DownGridCriRate;
      }
      return Math.Min(Math.Max(num4 + (int) self.CurrentStatus[BattleBonus.CriticalRate], 0), 100);
    }

    private bool CheckCritical(Unit self, Unit target, SkillData skill)
    {
      this.DebugAssert(self != null, "self == null");
      return (int) (this.GetRandom() % 100U) <= this.GetCriticalRate(self, target, skill);
    }

    public int GetCriticalDamage(Unit self, int damage)
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int criticalDamageRate1 = (int) fixParam.MinCriticalDamageRate;
      int criticalDamageRate2 = (int) fixParam.MaxCriticalDamageRate;
      int num = criticalDamageRate1 + (int) ((long) this.mRandDamage.Get() % (long) (criticalDamageRate2 - criticalDamageRate1));
      damage += 100 * damage * num / 10000;
      return damage;
    }

    private int GetAvoidRate(Unit self, Unit target, SkillData skill)
    {
      if (target.IsUnitCondition(EUnitCondition.Sleep) || target.IsUnitCondition(EUnitCondition.Stun) || (target.IsUnitCondition(EUnitCondition.Stone) || target.IsUnitCondition(EUnitCondition.Stop)) || skill.IsForceHit())
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int avoidBaseRate = (int) fixParam.AvoidBaseRate;
      int num1 = (int) target.CurrentStatus.param.spd - (int) self.CurrentStatus.param.dex / 2;
      if (num1 != 0)
        avoidBaseRate += num1 * (int) fixParam.AvoidParamScale / 100;
      int num2 = avoidBaseRate + BattleCore.Sqrt((target.Lv - self.Lv) / 2) + target.GetBaseAvoidRate() + (int) target.CurrentStatus[BattleBonus.AvoidRate] - (int) self.CurrentStatus[BattleBonus.HitRate];
      if (self.IsUnitFlag(EUnitFlag.SideAttack))
        num2 -= (int) fixParam.AddHitRateSide;
      if (self.IsUnitFlag(EUnitFlag.BackAttack))
        num2 -= (int) fixParam.AddHitRateBack;
      int num3 = 0;
      if (skill.IsReactionSkill())
        num3 += skill.CalcBuffEffectValue(ParamTypes.Avoid_Reaction, (int) self.CurrentStatus[BattleBonus.Avoid_Reaction], SkillEffectTargets.Target);
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Slash];
          break;
        case AttackDetailTypes.Stab:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Pierce];
          break;
        case AttackDetailTypes.Blow:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Blow];
          break;
        case AttackDetailTypes.Shot:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Shot];
          break;
        case AttackDetailTypes.Magic:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Magic];
          break;
        case AttackDetailTypes.Jump:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Jump];
          break;
      }
      return Math.Max(Math.Min(num2 + num3, (int) fixParam.MaxAvoidRate), 0);
    }

    private bool CheckAvoid(Unit self, Unit target, SkillData skill)
    {
      if (target.AI != null && target.AI.CheckFlag(AIFlags.DisableAvoid) || this.IsCombinationAttack(skill))
        return false;
      if (GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_AVOID)
        return true;
      int randomHitRate = (int) skill.SkillParam.random_hit_rate;
      if (randomHitRate > 0)
      {
        int num = (int) (this.GetRandom() % 100U);
        if (randomHitRate > num)
          return true;
      }
      return this.GetAvoidRate(self, target, skill) > (int) (this.GetRandom() % 100U);
    }

    private bool CheckGuts(Unit self)
    {
      if (self == null || !self.IsDead)
        return false;
      int currentStatu = (int) self.CurrentStatus[BattleBonus.GutsRate];
      return currentStatu > 0 && (int) (this.CurrentRand.Get() % 100U) < currentStatu || this.mQuestParam.type == QuestTypes.Tutorial && self.Side == EUnitSide.Player;
    }

    private int GetSkillEffectValue(Unit self, Unit target, SkillData skill)
    {
      int num1 = (int) skill.EffectValue;
      if (skill.IsSuicide())
      {
        int num2 = (int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp;
        num1 = num1 * num2 / 100;
      }
      int num3 = (int) skill.EffectRange;
      if (num3 != 0)
        num3 = (int) ((long) this.GetRandom() % (long) Math.Abs(num3)) * ((int) skill.EffectRange <= 0 ? -1 : 1);
      int num4 = ((int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp) * (int) skill.EffectHpMaxRate / 100;
      int num5 = 100 * (int) self.CurrentStatus.param.mp / (int) self.MaximumStatus.param.mp * (int) skill.EffectGemsMaxRate / 100;
      int num6 = 0;
      int effectDeadRate = (int) skill.SkillParam.effect_dead_rate;
      if (effectDeadRate != 0)
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          if (this.mUnits[index].IsDead && !this.mUnits[index].IsGimmick && !this.CheckEnemySide(self, this.mUnits[index]))
            num6 += effectDeadRate;
        }
      }
      int num7 = 0;
      if ((int) skill.SkillParam.effect_lvrate != 0 && target != null)
      {
        int num2 = target.Lv - self.Lv;
        if (num2 > 0)
          num7 = num2 * (int) skill.SkillParam.effect_lvrate;
      }
      return num1 + num3 + num4 + num5 + num6 + num7;
    }

    private int getSkillAttackerPowerBase(Unit attacker, Unit defender, SkillData skill)
    {
      int num1 = 1;
      StatusParam statusParam1 = attacker.CurrentStatus.param;
      StatusParam statusParam2 = defender == null ? (StatusParam) null : defender.CurrentStatus.param;
      int num2;
      if (!string.IsNullOrEmpty(skill.SkillParam.weapon))
      {
        WeaponParam weaponParam = MonoSingleton<GameManager>.Instance.GetWeaponParam(skill.SkillParam.weapon);
        switch (weaponParam.FormulaType)
        {
          case WeaponFormulaTypes.Atk:
            num2 = (int) weaponParam.atk * (100 * (int) statusParam1.atk / 10) / 100;
            break;
          case WeaponFormulaTypes.Mag:
            num2 = (int) weaponParam.atk * (100 * (int) statusParam1.mag / 10) / 100;
            break;
          case WeaponFormulaTypes.AtkSpd:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.spd)) / 15 / 100;
            break;
          case WeaponFormulaTypes.MagSpd:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.spd)) / 15 / 100;
            break;
          case WeaponFormulaTypes.AtkDex:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.dex)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagDex:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.dex)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkLuk:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.luk)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagLuk:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.luk)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkMag:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.mag)) / 20 / 100;
            break;
          case WeaponFormulaTypes.SpAtk:
            if ((int) statusParam1.atk > 0)
              num1 += (int) ((long) this.GetRandom() % (long) (int) statusParam1.atk);
            num2 = (int) weaponParam.atk * (100 * (int) statusParam1.atk / 10) * (50 + 100 * num1 / (int) statusParam1.atk) / 10000;
            break;
          case WeaponFormulaTypes.SpMag:
            int num3 = 0;
            if (statusParam2 != null)
              num3 = (int) statusParam2.mnd;
            num2 = (int) weaponParam.atk * (100 * (int) statusParam1.mag / 10) * (20 + 100 / ((int) statusParam1.mag + num3) * (int) statusParam1.mag) / 10000;
            break;
          case WeaponFormulaTypes.AtkSpdDex:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.spd / 2 + (int) statusParam1.spd * attacker.Lv / 100 + (int) statusParam1.dex / 4)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagSpdDex:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.spd / 2 + (int) statusParam1.spd * attacker.Lv / 100 + (int) statusParam1.dex / 4)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkDexLuk:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.dex / 2 + (int) statusParam1.luk / 2)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagDexLuk:
            num2 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.dex / 2 + (int) statusParam1.luk / 2)) / 20 / 100;
            break;
          default:
            num2 = (int) statusParam1.atk;
            break;
        }
      }
      else
        num2 = !skill.IsPhysicalAttack() ? (int) statusParam1.mag : (int) statusParam1.atk;
      return num2;
    }

    private int GetSkillAttackerPower(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int target1 = this.getSkillAttackerPowerBase(attacker, defender, skill);
      if ((bool) skill.IsCollabo)
      {
        Unit unitUseCollaboSkill = attacker.GetUnitUseCollaboSkill(skill, false);
        if (unitUseCollaboSkill != null)
        {
          int attackerPowerBase = this.getSkillAttackerPowerBase(unitUseCollaboSkill, defender, skill);
          target1 = (target1 + attackerPowerBase) / 2;
        }
        else
          DebugUtility.LogError(string.Format("BattleCore/GetSkillAttackerPower collabo unit not found! unit_iname={0}, skill_iname={1}", (object) attacker.UnitParam.iname, (object) skill.SkillParam.iname));
      }
      int skillEffectValue = this.GetSkillEffectValue(attacker, defender, skill);
      int num1 = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, target1);
      if (skill.IsSuicide())
        num1 += (int) attacker.CurrentStatus.param.hp / 2;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      if (num1 != 0)
      {
        int attackTypeBonus = this.GetAttackTypeBonus(attacker, skill);
        if (defender != null)
        {
          if (attacker.Element != EElement.None)
          {
            ElementParam elementAssist = attacker.CurrentStatus.element_assist;
            ElementParam elementResist = defender.CurrentStatus.element_resist;
            num2 += (int) elementAssist[attacker.Element] - (int) elementResist[attacker.Element];
          }
          if (skill.ElementType != EElement.None)
          {
            ElementParam elementResist = defender.CurrentStatus.element_resist;
            num2 += (int) skill.ElementValue - (int) elementResist[skill.ElementType];
          }
          if (log != null && log.targets != null)
          {
            LogSkill.Target target2 = log.targets.Find((Predicate<LogSkill.Target>) (p => p.target == defender));
            if (target2 != null)
              target2.element_effect_rate = num2;
          }
          string tokkou1 = skill.SkillParam.tokkou;
          if (!string.IsNullOrEmpty(tokkou1))
          {
            string[] tags = defender.GetTags();
            if (tags != null)
            {
              for (int index = 0; index < tags.Length; ++index)
              {
                if (!string.IsNullOrEmpty(tags[index]) && !(tokkou1 != tags[index]))
                {
                  if (skill.SkillParam.tk_rate != 0)
                  {
                    num3 += skill.SkillParam.tk_rate;
                    break;
                  }
                  num3 += (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.TokkouDamageRate;
                  break;
                }
              }
            }
          }
          using (List<BuffAttachment>.Enumerator enumerator = attacker.BuffAttachments.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              BuffAttachment current = enumerator.Current;
              if (current.skill != null && !string.IsNullOrEmpty(current.skill.SkillParam.tokkou))
              {
                List<string> stringList = new List<string>((IEnumerable<string>) defender.GetTags());
                if (stringList != null && stringList.Contains(current.skill.SkillParam.tokkou))
                {
                  if (current.skill.SkillParam.tk_rate != 0)
                    num3 += current.skill.SkillParam.tk_rate;
                  else
                    num3 += (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.TokkouDamageRate;
                }
              }
            }
          }
          if (skill.IsEnableHeightParamAdjust())
          {
            Grid unitGridPosition1 = this.GetUnitGridPosition(attacker);
            Grid unitGridPosition2 = this.GetUnitGridPosition(defender);
            if (unitGridPosition1 != null && unitGridPosition2 != null)
            {
              int num6 = unitGridPosition1.height - unitGridPosition2.height;
              if (num6 > 0)
                num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridAtkRate;
              if (num6 < 0)
                num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridAtkRate;
            }
          }
          if (skill.IsNormalAttack())
          {
            JobData job = attacker.Job;
            if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
            {
              List<ArtifactData> artifactDataList = new List<ArtifactData>();
              if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
                artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
              if (!string.IsNullOrEmpty(job.SelectedSkin))
              {
                ArtifactData selectedSkinData = job.GetSelectedSkinData();
                if (selectedSkinData != null)
                  artifactDataList.Add(selectedSkinData);
              }
              for (int index1 = 0; index1 < artifactDataList.Count; ++index1)
              {
                ArtifactData artifactData = artifactDataList[index1];
                if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
                {
                  string tokkou2 = artifactData.BattleEffectSkill.SkillParam.tokkou;
                  if (!string.IsNullOrEmpty(tokkou2))
                  {
                    string[] tags = defender.GetTags();
                    if (tags != null)
                    {
                      for (int index2 = 0; index2 < tags.Length; ++index2)
                      {
                        if (!string.IsNullOrEmpty(tags[index2]) && !(tokkou2 != tags[index2]))
                        {
                          if (artifactData.BattleEffectSkill.SkillParam.tk_rate != 0)
                          {
                            num3 += artifactData.BattleEffectSkill.SkillParam.tk_rate;
                            break;
                          }
                          num3 += (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.TokkouDamageRate;
                          break;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        if (this.IsCombinationAttack(skill))
          num5 = this.mHelperUnits.Count * 50;
        int num7 = attackTypeBonus + num2 + num3 + num4 + num5;
        num1 += 100 * num1 * num7 / 10000;
      }
      return num1;
    }

    private int GetSkillDefenderPower(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int num1 = 0;
      this.DefendSkill(attacker, defender, skill, log);
      if (skill.IsPhysicalAttack())
        num1 = (int) defender.CurrentStatus.param.def;
      if (skill.IsMagicalAttack())
        num1 = (int) defender.CurrentStatus.param.mnd;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      if (num1 > 0)
      {
        int ignoreDefenseRate = (int) skill.SkillParam.ignore_defense_rate;
        if (attacker.IsUnitFlag(EUnitFlag.BackAttack) && skill.BackAttackDefenseDownRate != 0)
          num2 = skill.BackAttackDefenseDownRate;
        if (attacker.IsUnitFlag(EUnitFlag.SideAttack) && skill.SideAttackDefenseDownRate != 0)
          num3 = skill.SideAttackDefenseDownRate;
        if (skill.IsEnableHeightParamAdjust())
        {
          Grid unitGridPosition1 = this.GetUnitGridPosition(attacker);
          Grid unitGridPosition2 = this.GetUnitGridPosition(defender);
          if (unitGridPosition1 != null && unitGridPosition2 != null)
          {
            int num5 = unitGridPosition1.height - unitGridPosition2.height;
            if (num5 > 0)
              num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridDefRate;
            if (num5 < 0)
              num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridDefRate;
          }
        }
        int num6 = ignoreDefenseRate + num2 + num3 + num4;
        num1 += 100 * num1 * num6 / 10000;
      }
      return num1;
    }

    private int CalcDamage(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int num = Math.Max(this.GetSkillAttackerPower(attacker, defender, skill, log) - this.GetSkillDefenderPower(attacker, defender, skill, log), 0);
      return !skill.IsJewelAttack() ? this.GetAttackTypeDamageCut(defender, skill, num) : BattleCore.Sqrt(num) * 2;
    }

    private int GetAttackTypeBonus(Unit self, SkillData skill)
    {
      int num = 0;
      if (skill.IsReactionSkill())
        num += (int) self.CurrentStatus[BattleBonus.ReactionAttack];
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num = num + (int) self.CurrentStatus[BattleBonus.SlashAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Slash);
          break;
        case AttackDetailTypes.Stab:
          num = num + (int) self.CurrentStatus[BattleBonus.PierceAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Stab);
          break;
        case AttackDetailTypes.Blow:
          num = num + (int) self.CurrentStatus[BattleBonus.BlowAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Blow);
          break;
        case AttackDetailTypes.Shot:
          num = num + (int) self.CurrentStatus[BattleBonus.ShotAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Shot);
          break;
        case AttackDetailTypes.Magic:
          num = num + (int) self.CurrentStatus[BattleBonus.MagicAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Magic);
          break;
        case AttackDetailTypes.Jump:
          num = num + (int) self.CurrentStatus[BattleBonus.JumpAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Jump);
          break;
      }
      return num;
    }

    private int GetAttackTypeDamageCut(Unit defender, SkillData skill, int damage)
    {
      int num1 = damage;
      int num2 = 0;
      if (skill.IsReactionSkill())
        num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Reaction];
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Slash];
          break;
        case AttackDetailTypes.Stab:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Pierce];
          break;
        case AttackDetailTypes.Blow:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Blow];
          break;
        case AttackDetailTypes.Shot:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Shot];
          break;
        case AttackDetailTypes.Magic:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Magic];
          break;
        case AttackDetailTypes.Jump:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Jump];
          break;
      }
      if (num2 != 0)
        num1 = damage - damage * num2 / 100;
      return num1;
    }

    private bool CheckWeakPoint(Unit self, Unit target, SkillData skill)
    {
      if (skill.ElementType == EElement.None)
        return false;
      return skill.ElementType == target.GetWeakElement();
    }

    private int CalcHeal(Unit self, Unit target, SkillData skill)
    {
      if (skill.EffectType == SkillEffectTypes.Heal)
      {
        int skillEffectValue = this.GetSkillEffectValue(self, target, skill);
        return SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, (int) self.CurrentStatus.param.mag);
      }
      if (skill.EffectType != SkillEffectTypes.RateHeal)
        return 0;
      int skillEffectValue1 = this.GetSkillEffectValue(self, target, skill);
      return (int) target.MaximumStatus.param.hp * skillEffectValue1 / 100;
    }

    private void Heal(Unit target, int value)
    {
      target.Heal(value);
    }

    private int CalcGainedGems(Unit self, Unit target, SkillData skill, int damage, bool bCritical, bool bWeakPoint)
    {
      if (skill == null || !skill.IsNormalAttack())
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int gainNormalAttack = (int) fixParam.GemsGainNormalAttack;
      if (bCritical)
        gainNormalAttack += (int) fixParam.GemsGainCriticalAttack;
      if (bWeakPoint)
        gainNormalAttack += (int) fixParam.GemsGainWeakAttack;
      if (self.IsUnitFlag(EUnitFlag.SideAttack))
        gainNormalAttack += (int) fixParam.GemsGainSideAttack;
      if (self.IsUnitFlag(EUnitFlag.BackAttack))
        gainNormalAttack += (int) fixParam.GemsGainBackAttack;
      if (target.IsDead)
        gainNormalAttack += (int) fixParam.GemsGainKillBonus;
      if ((int) fixParam.GemsGainDiffFloorCount > 0)
      {
        int num = this.CurrentMap[self.x, self.y].height - this.CurrentMap[target.x, target.y].height;
        if (num > 0)
          gainNormalAttack += Math.Min(num / (int) fixParam.GemsGainDiffFloorCount, (int) fixParam.GemsGainDiffFloorMax);
      }
      return gainNormalAttack + (int) self.CurrentStatus[BattleBonus.GainJewel];
    }

    private void DamageCureCondition(Unit target, LogSkill log = null)
    {
      if (target.IsUnitCondition(EUnitCondition.Sleep))
        this.CureCondition(target, EUnitCondition.Sleep, log);
      if (!target.IsUnitCondition(EUnitCondition.Charm))
        return;
      this.CureCondition(target, EUnitCondition.Charm, log);
    }

    private void HealCureCondition(Unit target, LogSkill log = null)
    {
    }

    private Unit GetSubMemberFirst()
    {
      for (int index = 0; index < this.Player.Count; ++index)
      {
        if (this.Player[index].IsSub && !this.Player[index].IsDead && this.Player[index] != this.Friend)
          return this.Player[index];
      }
      return (Unit) null;
    }

    public void ResumeDead(Unit target)
    {
      this.Dead((Unit) null, target, DeadTypes.Damage);
    }

    private void Dead(Unit self, Unit target, DeadTypes type)
    {
      if (target.IsUnitFlag(EUnitFlag.EntryDead))
        return;
      target.SetUnitFlag(EUnitFlag.EntryDead, true);
      this.DeadSkill(target, self);
      if (target.Side == EUnitSide.Enemy)
      {
        ++this.mKillstreak;
        this.mMaxKillstreak = Math.Max(this.mMaxKillstreak, this.mKillstreak);
      }
      if (self != null && self != target)
        ++self.KillCount;
      if (!target.IsDead)
        return;
      LogDead logDead = this.Log<LogDead>();
      logDead.self = target;
      logDead.type = type;
      target.ForceDead();
      this.GridEventStart(self, target, EEventTrigger.Dead);
      if (target.IsPartyMember && this.Friend != target && this.GetQuestResult() == BattleCore.QuestResult.Pending)
      {
        Unit subMemberFirst = this.GetSubMemberFirst();
        if (subMemberFirst != null)
        {
          Grid duplicatePosition = this.GetCorrectDuplicatePosition(target);
          subMemberFirst.x = duplicatePosition.x;
          subMemberFirst.y = duplicatePosition.y;
          subMemberFirst.IsSub = false;
          this.Log<LogUnitEntry>().self = subMemberFirst;
        }
      }
      this.GimmickSkillDeadCount(self, target);
      this.UpdateEntryTriggers(UnitEntryTypes.DeadEnemy, target, (SkillParam) null);
    }

    public void ForceDead(Unit unit)
    {
    }

    private void Revive(Unit target, int hp)
    {
      LogRevive logRevive = this.Log<LogRevive>();
      logRevive.self = target;
      logRevive.hp = hp;
    }

    private void EntryUnit(Unit self)
    {
      Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
      self.x = duplicatePosition.x;
      self.y = duplicatePosition.y;
      self.SetUnitFlag(EUnitFlag.Entried, true);
      this.Log<LogUnitEntry>().self = self;
    }

    private void CureCondition(Unit target, EUnitCondition condition, LogSkill logskl)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      LogCureCondition logCureCondition = this.Log<LogCureCondition>();
      logCureCondition.self = target;
      logCureCondition.condition = condition;
      bool flag = target.IsUnitCondition(condition);
      target.CureCondEffects(condition, true, false);
      if (logskl == null || target == null || (!flag || target.IsUnitCondition(condition)))
        return;
      LogSkill.Target target1 = logskl.FindTarget(target);
      if (target1 == null)
        return;
      target1.cureCondition |= condition;
    }

    private void FailCondition(Unit self, Unit target, SkillData skill, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chk_target, EffectCheckTimings chk_timing, int turn, bool is_passive, bool is_curse, LogSkill logskl)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      LogFailCondition logFailCondition = this.Log<LogFailCondition>();
      logFailCondition.self = target;
      logFailCondition.source = self;
      logFailCondition.condition = condition;
      CondAttachment condAttachment = this.CreateCondAttachment(self, target, skill, type, cond, condition, chk_target, chk_timing, turn, is_passive, is_curse);
      target.SetCondAttachment(condAttachment);
      if (logskl == null || !target.IsUnitCondition(condition))
        return;
      LogSkill.Target target1 = logskl.FindTarget(target);
      if (target1 == null)
        return;
      target1.failCondition |= condition;
    }

    private void BuffSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive = false, LogSkill log = null, SkillEffectTargets buff_target = SkillEffectTargets.Target, bool is_duplicate = false)
    {
      if (timing != skill.Timing)
        return;
      BattleCore.BuffWorkStatus.Clear();
      BattleCore.BuffWorkScaleStatus.Clear();
      BattleCore.DebuffWorkStatus.Clear();
      BattleCore.DebuffWorkScaleStatus.Clear();
      BuffEffect buffEffect = skill.GetBuffEffect(buff_target);
      if (buffEffect == null || buffEffect.targets.Count == 0 || !buffEffect.CheckEnableBuffTarget(target))
        return;
      bool flag1 = false;
      bool flag2 = true;
      bool flag3 = true;
      if (!skill.IsPassiveSkill())
      {
        if (!target.IsEnableBuffEffect(BuffTypes.Buff))
          flag2 = false;
        else if ((int) target.CurrentStatus.enchant_resist.resist_buff > 0)
        {
          if ((int) target.CurrentStatus.enchant_resist.resist_buff < 100)
          {
            if ((int) (this.GetRandom() % 100U) < (int) target.CurrentStatus.enchant_resist.resist_buff && (!GameUtility.IsDebugBuild || !BattleCore.DEBUG_IS_FORCE_ACTION))
              flag2 = false;
          }
          else
            flag2 = false;
        }
        if (!target.IsEnableBuffEffect(BuffTypes.Debuff))
          flag3 = false;
        else if ((int) target.CurrentStatus.enchant_resist.resist_debuff > 0)
        {
          if ((int) target.CurrentStatus.enchant_resist.resist_buff < 100)
          {
            if ((int) (this.GetRandom() % 100U) < (int) target.CurrentStatus.enchant_resist.resist_debuff && (!GameUtility.IsDebugBuild || !BattleCore.DEBUG_IS_FORCE_ACTION))
              flag3 = false;
          }
          else
            flag2 = false;
        }
      }
      skill.BuffSkill(timing, BattleCore.BuffWorkStatus, BattleCore.BuffWorkScaleStatus, BattleCore.DebuffWorkStatus, BattleCore.DebuffWorkScaleStatus, this.CurrentRand, buff_target);
      int turn = (int) buffEffect.param.turn;
      ESkillCondition cond = buffEffect.param.cond;
      EffectCheckTargets chkTarget = buffEffect.param.chk_target;
      EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
      int duplicateCount = skill.DuplicateCount;
      if (flag2)
      {
        if (buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add))
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, buff_target, buffEffect.param, BuffTypes.Buff, SkillParamCalcTypes.Add, BattleCore.BuffWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          target.SetBuffAttachment(buffAttachment, is_duplicate);
          flag1 = true;
        }
        if (buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale))
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, buff_target, buffEffect.param, BuffTypes.Buff, SkillParamCalcTypes.Scale, BattleCore.BuffWorkScaleStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          target.SetBuffAttachment(buffAttachment, is_duplicate);
          flag1 = true;
        }
      }
      if (flag3)
      {
        if (buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add))
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, buff_target, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Add, BattleCore.DebuffWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          target.SetBuffAttachment(buffAttachment, is_duplicate);
          flag1 = true;
        }
        if (buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale))
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, buff_target, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Scale, BattleCore.DebuffWorkScaleStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          target.SetBuffAttachment(buffAttachment, is_duplicate);
          flag1 = true;
        }
      }
      if (!flag1 || log == null)
        return;
      BattleCore.BuffWorkStatus.Add(BattleCore.DebuffWorkStatus);
      BattleCore.BuffWorkScaleStatus.Add(BattleCore.DebuffWorkScaleStatus);
      BuffBit buff = new BuffBit();
      BuffBit debuff = new BuffBit();
      BattleCore.SetBuffBits(BattleCore.BuffWorkStatus, ref buff, ref debuff);
      BattleCore.SetBuffBits(BattleCore.BuffWorkScaleStatus, ref buff, ref debuff);
      LogSkill.Target target1 = (LogSkill.Target) null;
      switch (buff_target)
      {
        case SkillEffectTargets.Target:
          target1 = log.FindTarget(target);
          break;
        case SkillEffectTargets.Self:
          if (self == target)
          {
            target1 = log.self_effect;
            target1.target = target;
            break;
          }
          break;
      }
      if (target1 == null)
        return;
      buff.CopyTo(target1.buff);
      debuff.CopyTo(target1.debuff);
    }

    public static void SetBuffBits(BaseStatus status, ref BuffBit buff, ref BuffBit debuff)
    {
      for (int index = 0; index < status.param.Length; ++index)
      {
        if ((int) status.param[(StatusTypes) index] != 0)
        {
          ParamTypes paramTypes = status.param.GetParamTypes(index);
          if ((int) status.param[(StatusTypes) index] > 0)
            buff.SetBit(paramTypes);
          else
            debuff.SetBit(paramTypes);
        }
      }
      for (int index = 0; index < (int) ElementParam.MAX_ELEMENT; ++index)
      {
        if ((int) status.element_assist.values[index] != 0)
        {
          ParamTypes assistParamTypes = status.element_assist.GetAssistParamTypes(index);
          if ((int) status.element_assist.values[index] > 0)
            buff.SetBit(assistParamTypes);
          else
            debuff.SetBit(assistParamTypes);
        }
        if ((int) status.element_resist.values[index] != 0)
        {
          ParamTypes resistParamTypes = status.element_resist.GetResistParamTypes(index);
          if ((int) status.element_resist.values[index] > 0)
            buff.SetBit(resistParamTypes);
          else
            debuff.SetBit(resistParamTypes);
        }
      }
      for (int index = 0; index < (int) EnchantParam.MAX_ENCHANGT; ++index)
      {
        if ((int) status.enchant_assist.values[index] != 0)
        {
          ParamTypes resistParamTypes = status.enchant_assist.GetResistParamTypes(index);
          if ((int) status.enchant_assist.values[index] > 0)
            buff.SetBit(resistParamTypes);
          else
            debuff.SetBit(resistParamTypes);
        }
        if ((int) status.enchant_resist.values[index] != 0)
        {
          ParamTypes resistParamTypes = status.enchant_resist.GetResistParamTypes(index);
          if ((int) status.enchant_resist.values[index] > 0)
            buff.SetBit(resistParamTypes);
          else
            debuff.SetBit(resistParamTypes);
        }
      }
      for (int index = 0; index < status.bonus.values.Length; ++index)
      {
        if ((int) status.bonus.values[index] != 0)
        {
          ParamTypes paramTypes = status.bonus.GetParamTypes(index);
          if ((int) status.bonus.values[index] > 0)
            buff.SetBit(paramTypes);
          else
            debuff.SetBit(paramTypes);
        }
      }
    }

    public BuffAttachment CreateBuffAttachment(Unit user, Unit target, SkillData skill, SkillEffectTargets skilltarget, BuffEffectParam param, BuffTypes buffType, SkillParamCalcTypes calcType, BaseStatus status, ESkillCondition cond, int turn, EffectCheckTargets chktgt, EffectCheckTimings timing, bool is_passive, int dupli)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return (BuffAttachment) null;
      BuffAttachment buffAttachment1 = new BuffAttachment(param);
      buffAttachment1.user = user;
      buffAttachment1.turn = (OInt) turn;
      buffAttachment1.skill = skill;
      buffAttachment1.skilltarget = skilltarget;
      buffAttachment1.IsPassive = (OBool) is_passive;
      buffAttachment1.BuffType = buffType;
      buffAttachment1.CalcType = calcType;
      buffAttachment1.CheckTiming = timing;
      buffAttachment1.CheckTarget = (Unit) null;
      buffAttachment1.UseCondition = cond;
      buffAttachment1.DuplicateCount = dupli;
      switch (chktgt)
      {
        case EffectCheckTargets.Target:
          buffAttachment1.CheckTarget = target;
          break;
        case EffectCheckTargets.User:
          buffAttachment1.CheckTarget = user;
          break;
      }
      if (user != null && timing != EffectCheckTimings.Eternal && !is_passive)
      {
        if (buffType == BuffTypes.Buff)
        {
          BuffAttachment buffAttachment2 = buffAttachment1;
          buffAttachment2.turn = (OInt) ((int) buffAttachment2.turn + (int) user.CurrentStatus[BattleBonus.BuffTurn]);
        }
        if (buffType == BuffTypes.Debuff)
        {
          BuffAttachment buffAttachment2 = buffAttachment1;
          buffAttachment2.turn = (OInt) ((int) buffAttachment2.turn + (int) user.CurrentStatus[BattleBonus.DebuffTurn]);
        }
      }
      status.CopyTo(buffAttachment1.status);
      return buffAttachment1;
    }

    private void CondSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive = false, LogSkill log = null, SkillEffectTargets cond_target = SkillEffectTargets.Target)
    {
      if (timing != skill.Timing)
        return;
      CondEffect condEffect = skill.GetCondEffect(cond_target);
      ConditionEffectTypes type = ConditionEffectTypes.None;
      if (condEffect != null && condEffect.param != null)
      {
        if (!condEffect.CheckEnableCondTarget(target))
          return;
        if (condEffect.param.type != ConditionEffectTypes.None && condEffect.param.conditions != null)
        {
          int rate = (int) condEffect.rate;
          if (rate > 0 && rate < 100 && (int) (this.GetRandom() % 100U) > rate && (!GameUtility.IsDebugBuild || !BattleCore.DEBUG_IS_FORCE_ACTION))
            return;
          type = condEffect.param.type;
        }
      }
      LogSkill.Target target1 = (LogSkill.Target) null;
      if (log != null)
      {
        switch (cond_target)
        {
          case SkillEffectTargets.Target:
            target1 = log.FindTarget(target);
            break;
          case SkillEffectTargets.Self:
            if (condEffect == null || condEffect.param == null)
              return;
            if (self == target)
            {
              log.self_effect.target = self;
              break;
            }
            break;
          default:
            return;
        }
      }
      switch (type)
      {
        case ConditionEffectTypes.None:
          if (!skill.IsDamagedSkill())
            break;
          EnchantParam enchantAssist1 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist1 = target.CurrentStatus.enchant_resist;
          for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
          {
            EUnitCondition condition = (EUnitCondition) (1 << index);
            if (!target.IsDisableUnitCondition(condition) && this.CheckFailCondition(target, (int) enchantAssist1[condition], (int) enchantResist1[condition], condition))
              this.FailCondition(self, target, skill, ConditionEffectTypes.FailCondition, ESkillCondition.None, condition, EffectCheckTargets.Target, EffectCheckTimings.ActionStart, 0, is_passive, false, log);
          }
          break;
        case ConditionEffectTypes.CureCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            this.CureCondition(target, condition, log);
          }
          break;
        case ConditionEffectTypes.FailCondition:
          if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || (int) condEffect.value == 0))
            break;
          EnchantParam enchantAssist2 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist2 = target.CurrentStatus.enchant_resist;
          self.CurrentStatus.enchant_assist.CopyTo(enchantAssist2);
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            if (!target.IsDisableUnitCondition(condition) && this.CheckFailCondition(target, (int) enchantAssist2[condition] + (int) condEffect.value, (int) enchantResist2[condition], condition))
              this.FailCondition(self, target, skill, condEffect.param.type, condEffect.param.cond, condition, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log);
          }
          break;
        case ConditionEffectTypes.ForcedFailCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            this.FailCondition(self, target, skill, condEffect.param.type, condEffect.param.cond, condition, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log);
          }
          break;
        case ConditionEffectTypes.RandomFailCondition:
          if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || (int) condEffect.value == 0))
            break;
          EnchantParam enchantAssist3 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist3 = target.CurrentStatus.enchant_resist;
          int index1 = (int) ((long) this.GetRandom() % (long) condEffect.param.conditions.Length);
          EUnitCondition condition1 = condEffect.param.conditions[index1];
          if (target.IsDisableUnitCondition(condition1) || !this.CheckFailCondition(target, (int) enchantAssist3[condition1] + (int) condEffect.value, (int) enchantResist3[condition1], condition1))
            break;
          this.FailCondition(self, target, skill, condEffect.param.type, condEffect.param.cond, condition1, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log);
          break;
        case ConditionEffectTypes.DisableCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
          {
            CondAttachment condAttachment = this.CreateCondAttachment(self, target, skill, type, condEffect.param.cond, condEffect.param.conditions[index2], condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, false);
            target.SetCondAttachment(condAttachment);
          }
          break;
      }
    }

    public CondAttachment CreateCondAttachment(Unit user, Unit target, SkillData skill, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chktgt, EffectCheckTimings timing, int turn, bool is_passive, bool is_curse)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return (CondAttachment) null;
      if (type == ConditionEffectTypes.None && !skill.IsDamagedSkill())
        return (CondAttachment) null;
      CondAttachment condAttachment = new CondAttachment();
      condAttachment.user = user;
      condAttachment.turn = (OInt) turn;
      condAttachment.skill = skill;
      condAttachment.IsPassive = (OBool) is_passive;
      condAttachment.CondType = type;
      condAttachment.Condition = condition;
      condAttachment.CheckTarget = (Unit) null;
      condAttachment.CheckTiming = timing;
      condAttachment.UseCondition = cond;
      switch (chktgt)
      {
        case EffectCheckTargets.Target:
          condAttachment.CheckTarget = target;
          break;
        case EffectCheckTargets.User:
          condAttachment.CheckTarget = user;
          break;
      }
      if (condAttachment.IsFailCondition())
        condAttachment.IsCurse = is_curse;
      return condAttachment;
    }

    private bool CheckFailCondition(Unit target, int val, int resist, EUnitCondition condition)
    {
      if (val <= 0)
        return false;
      int num1 = val - resist;
      if (num1 <= 0)
        return false;
      int num2 = (int) (this.GetRandom() % 100U);
      if (GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_ACTION)
        return true;
      return num1 > num2;
    }

    private void EndMoveSkill(Unit self, int step)
    {
    }

    private bool CheckEnableUseSkill(Unit self, SkillData skill, bool bCheckCondOnly = false)
    {
      return self.CheckEnableUseSkill(skill, bCheckCondOnly);
    }

    public bool CheckEnemySide(Unit self, Unit target)
    {
      if (self == target)
        return false;
      if (!self.IsUnitCondition(EUnitCondition.Charm) && !self.IsUnitCondition(EUnitCondition.Zombie))
        return self.Side != target.Side;
      if (target.IsUnitCondition(EUnitCondition.Charm) || target.IsUnitCondition(EUnitCondition.Zombie))
        return false;
      return self.Side == target.Side;
    }

    public bool CheckSkillTarget(Unit self, Unit target, SkillData skill)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(skill != null, "failed. skill != null");
      if (target == null || target.IsGimmick || target.CastSkill != null && target.CastSkill.CastType == ECastTypes.Jump)
        return false;
      bool flag = false;
      switch (skill.Target)
      {
        case ESkillTarget.Self:
          flag = self == target;
          break;
        case ESkillTarget.SelfSide:
          flag = !this.CheckEnemySide(self, target);
          break;
        case ESkillTarget.EnemySide:
          flag = this.CheckEnemySide(self, target);
          break;
        case ESkillTarget.UnitAll:
          flag = true;
          break;
        case ESkillTarget.NotSelf:
          flag = self != target;
          break;
        case ESkillTarget.GridNoUnit:
          flag = false;
          break;
      }
      if (!flag)
        return false;
      if (skill.EffectType == SkillEffectTypes.Revive)
        return target.IsDead;
      return !target.IsDead;
    }

    public int GetAttackRangeBonus(int selfHeight, int targetHeight)
    {
      int num = selfHeight - targetHeight;
      if (Math.Abs(num) < BattleMap.MAP_FLOOR_HEIGHT)
        return 0;
      return num / BattleMap.MAP_FLOOR_HEIGHT;
    }

    public GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateSelectGridMap(self, targetX, targetY, skill, ref result, false);
      return result;
    }

    public GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped = false)
    {
      SkillParam skillParam = skill.SkillParam;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackRangeMax = self.GetAttackRangeMax(skill);
      ELineType lineType = skillParam.line_type;
      ESelectType selectRange = skillParam.select_range;
      int attackScope = self.GetAttackScope(skill);
      int attackHeight = self.GetAttackHeight(skill);
      bool bCheckGridLine = skill.CheckGridLineSkill();
      bool bHeightBonus = skill.IsEnableHeightRangeBonus();
      bool bSelfEffect = skill.IsSelfTargetSelect();
      return this.CreateSelectGridMap(self, targetX, targetY, attackRangeMin, attackRangeMax, selectRange, lineType, attackScope, bCheckGridLine, bHeightBonus, attackHeight, bSelfEffect, ref result, keeped);
    }

    private GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, int range_min, int range_max, ESelectType rangetype, ELineType linetype, int scope, bool bCheckGridLine, bool bHeightBonus, int attack_height, bool bSelfEffect, ref GridMap<bool> result, bool keeped = false)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result1 = keeped ? new GridMap<bool>(currentMap.Width, currentMap.Height) : result;
      result1.fill(false);
      Grid grid = currentMap[targetX, targetY];
      if (range_max > 0 || rangetype == ESelectType.All)
      {
        int range_max1 = range_max;
        if (bHeightBonus)
          range_max1 += this.GetAttackRangeBonus(grid.height, 0);
        switch (rangetype)
        {
          case ESelectType.Diamond:
            this.CreateGridMapDiamond(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.Square:
            this.CreateGridMapSquare(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.Laser:
            for (int index1 = 0; index1 < 4; ++index1)
            {
              int num1 = Unit.DIRECTION_OFFSETS[index1, 0];
              int num2 = Unit.DIRECTION_OFFSETS[index1, 1];
              int index2 = grid.x + num1;
              int index3 = grid.y + num2;
              this.CreateGridMapLaser(grid, currentMap[index2, index3], range_min, range_max, scope, ref result1);
            }
            break;
          case ESelectType.All:
            result1.fill(true);
            break;
          case ESelectType.Bishop:
            this.CreateGridMapBishop(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.LaserSpread:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserSpread(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.LaserWide:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserWide(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.Horse:
            this.CreateGridMapHorse(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.LaserTwin:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserTwin(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.LaserTriple:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserTriple(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          default:
            this.CreateGridMapCross(grid, range_min, range_max1, ref result1);
            break;
        }
        if (SkillParam.IsTypeLaser(rangetype))
        {
          for (int x = 0; x < result1.w; ++x)
          {
            for (int y = 0; y < result1.h; ++y)
            {
              if (result1.get(x, y) && !this.CheckEnableAttackHeight(grid, currentMap[x, y], attack_height))
                result1.set(x, y, false);
            }
          }
        }
        else
        {
          for (int index1 = -range_max1; index1 <= range_max1; ++index1)
          {
            for (int index2 = -range_max1; index2 <= range_max1; ++index2)
            {
              int num1 = rangetype != ESelectType.Square ? Math.Abs(index2) + Math.Abs(index1) : Math.Max(Math.Abs(index2), Math.Abs(index1));
              if (num1 <= range_max1)
              {
                int index3 = index2 + grid.x;
                int index4 = index1 + grid.y;
                Grid goal = currentMap[index3, index4];
                if (goal != null && goal != grid && result1.get(index3, index4))
                {
                  int num2 = 0;
                  if (bHeightBonus)
                    num2 = this.GetAttackRangeBonus(grid.height, goal.height);
                  if (num1 > range_max + num2)
                    result1.set(index3, index4, false);
                  else if (goal.geo != null && (bool) goal.geo.DisableStopped)
                    result1.set(goal.x, goal.y, false);
                  else if (linetype == ELineType.None)
                  {
                    if (!this.CheckEnableAttackHeight(grid, goal, attack_height))
                      result1.set(goal.x, goal.y, false);
                  }
                  else
                  {
                    this.GetSkillGridLines(grid.x, grid.y, index3, index4, range_min, range_max, attack_height, linetype, bHeightBonus, ref this.mGridLines);
                    result1.set(goal.x, goal.y, this.mGridLines.Contains(goal));
                  }
                }
              }
            }
          }
        }
      }
      result1.set(grid.x, grid.y, bSelfEffect);
      for (int x = 0; x < result1.w; ++x)
      {
        for (int y = 0; y < result1.h; ++y)
        {
          if (!result.get(x, y) && result1.get(x, y))
            result.set(x, y, true);
        }
      }
      return result;
    }

    private bool CheckGridOnLine(int x1, int y1, int x2, int y2, int sx, int sy, int tx, int ty)
    {
      long num1 = (long) (tx - sx);
      long num2 = (long) (ty - sy);
      long num3 = (long) (x1 - sx);
      long num4 = (long) (y1 - sy);
      long num5 = (long) (x2 - sx);
      long num6 = (long) (y2 - sy);
      if ((num1 * num4 - num2 * num3) * (num1 * num6 - num2 * num5) > 0L)
        return false;
      long num7 = (long) (x2 - x1);
      long num8 = (long) (y2 - y1);
      long num9 = (long) (sx - x1);
      long num10 = (long) (sy - y1);
      long num11 = (long) (tx - x1);
      long num12 = (long) (ty - y1);
      return (num7 * num10 - num8 * num9) * (num7 * num12 - num8 * num11) <= 0L;
    }

    private void GetGridOnLine(Grid start, Grid target, ref List<Grid> results)
    {
      this.GetGridOnLine(start, target.x, target.y, ref results);
    }

    private void GetGridOnLine(Grid start, int tx, int ty, ref List<Grid> results)
    {
      BattleMap currentMap = this.CurrentMap;
      results.Clear();
      int num1 = 100;
      int num2 = start.x * num1;
      int num3 = start.y * num1;
      int num4 = tx * num1;
      int num5 = ty * num1;
      for (int index1 = 0; index1 < currentMap.Width; ++index1)
      {
        for (int index2 = 0; index2 < currentMap.Height; ++index2)
        {
          Grid grid = currentMap[index1, index2];
          if (grid != start)
          {
            int num6 = grid.y * num1 + 45;
            int num7 = grid.y * num1 - 45;
            int num8 = grid.x * num1 - 45;
            int num9 = grid.x * num1 + 45;
            if (Math.Min(num8, num9) <= Math.Max(num2, num4) && Math.Min(num6, num7) <= Math.Max(num3, num5) && (Math.Min(num2, num4) <= Math.Max(num8, num9) && Math.Min(num3, num5) <= Math.Max(num6, num7)) && ((this.CheckGridOnLine(num8, num6, num9, num7, num2, num3, num4, num5) || this.CheckGridOnLine(num9, num6, num8, num7, num2, num3, num4, num5)) && !this.mGridLines.Contains(grid)))
              this.mGridLines.Add(grid);
          }
        }
      }
      MySort<Grid>.Sort(results, (Comparison<Grid>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        return this.CalcGridDistance(start, src) - this.CalcGridDistance(start, dsc);
      }));
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, skill, ref result, false);
      return result;
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped = false)
    {
      SkillParam skillParam = skill.SkillParam;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackRangeMax = self.GetAttackRangeMax(skill);
      int attackScope = self.GetAttackScope(skill);
      int attackHeight = self.GetAttackHeight(skill);
      ESelectType selectScope = skillParam.select_scope;
      this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, attackRangeMin, attackRangeMax, attackScope, attackHeight, selectScope, ref result, keeped);
      return result;
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, int range_min, int range_max, int scope, int enable_height, ESelectType scopetype, ref GridMap<bool> result, bool keeped = false)
    {
      if (!keeped)
        result.fill(false);
      bool flag = false;
      if (scope < 1)
      {
        if (SkillParam.IsTypeLaser(scopetype))
          return result;
        if (scopetype != ESelectType.All)
        {
          result.set(targetX, targetY, true);
          flag = true;
        }
      }
      BattleMap currentMap = this.CurrentMap;
      Grid grid = currentMap[targetX, targetY];
      Grid start = grid;
      if (!flag)
      {
        Grid self1 = currentMap[selfX, selfY];
        switch (scopetype)
        {
          case ESelectType.Diamond:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapDiamond(grid, 0, scope, ref result);
            break;
          case ESelectType.Square:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapSquare(grid, 0, scope, ref result);
            break;
          case ESelectType.Laser:
            this.CreateGridMapLaser(self1, grid, range_min, range_max, scope, ref result);
            start = self1;
            break;
          case ESelectType.All:
            result.fill(true);
            break;
          case ESelectType.Wall:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapWall(self1, grid, 0, scope, ref result);
            break;
          case ESelectType.WallPlus:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapWallPlus(self1, grid, 0, scope, ref result);
            break;
          case ESelectType.Bishop:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapBishop(grid, 0, scope, ref result);
            break;
          case ESelectType.Victory:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapVictory(self1, grid, 0, scope, ref result);
            break;
          case ESelectType.LaserSpread:
            this.CreateGridMapLaserSpread(self1, grid, range_min, range_max, ref result, true);
            start = self1;
            break;
          case ESelectType.LaserWide:
            this.CreateGridMapLaserWide(self1, grid, range_min, range_max, ref result, true);
            start = self1;
            break;
          case ESelectType.Horse:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapHorse(grid, 0, scope, ref result);
            break;
          case ESelectType.LaserTwin:
            this.CreateGridMapLaserTwin(self1, grid, range_min, range_max, ref result, true);
            start = self1;
            break;
          case ESelectType.LaserTriple:
            this.CreateGridMapLaserTriple(self1, grid, range_min, range_max, ref result, true);
            start = self1;
            break;
          case ESelectType.SquareOutline:
            this.CreateGridMapSquare(grid, 0, scope, ref result);
            result.set(grid.x, grid.y, false);
            break;
          default:
            this.SetGridMap(ref result, grid, grid);
            this.CreateGridMapCross(grid, 0, scope, ref result);
            break;
        }
      }
      for (int y = result.h - 1; y >= 0; --y)
      {
        for (int x = 0; x < result.w; ++x)
        {
          if (result.get(x, y))
          {
            Grid goal = currentMap[x, y];
            if (!this.CheckEnableAttackHeight(start, goal, enable_height))
              result.set(x, y, false);
            if (goal.geo != null && (bool) goal.geo.DisableStopped)
              result.set(x, y, false);
          }
        }
      }
      return result;
    }

    public void CreateGridMapCross(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = range_min + 1; index1 <= range_max; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index2, 0] * index1;
          int num2 = Unit.DIRECTION_OFFSETS[index2, 1] * index1;
          int index3 = target.x + num1;
          int index4 = target.y + num2;
          Grid goal = currentMap[index3, index4];
          this.SetGridMap(ref result, target, goal);
        }
      }
    }

    private void CreateGridMapDiamond(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        for (int index2 = -range_max; index2 <= range_max; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= range_max)
          {
            int index3 = target.x + index2;
            int index4 = target.y + index1;
            Grid goal = currentMap[index3, index4];
            if (range_min <= 0 || range_max <= 0 || this.CalcGridDistance(target, goal) > range_min)
              this.SetGridMap(ref result, target, goal);
          }
        }
      }
    }

    private void CreateGridMapSquare(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2))
            {
              int index3 = target.x + index2;
              int index4 = target.y + index1;
              Grid goal = currentMap[index3, index4];
              this.SetGridMap(ref result, target, goal);
            }
          }
        }
      }
    }

    private void CreateGridMapLaser(Grid self, Grid target, int range_min, int range_max, int scope, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = 0;
      int num1 = target.x - self.x;
      int num2 = target.y - self.y;
      if (num1 > 0)
      {
        index1 = 0;
        num2 = 0;
      }
      if (num1 < 0)
      {
        index1 = 2;
        num2 = 0;
      }
      int num3;
      if (num2 > 0)
      {
        index1 = 1;
        num3 = 0;
      }
      if (num2 < 0)
      {
        index1 = 3;
        num3 = 0;
      }
      int num4 = Math.Max(scope - 1, 0);
      for (int index2 = range_min + 1; index2 <= range_max; ++index2)
      {
        for (int index3 = -num4; index3 <= num4; ++index3)
        {
          int num5 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num6 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int index4 = self.x + num5 + Unit.DIRECTION_OFFSETS[index1, 1] * index3;
          int index5 = self.y + num6 + Unit.DIRECTION_OFFSETS[index1, 0] * index3;
          Grid goal = currentMap[index4, index5];
          this.SetGridMap(ref result, target, goal);
        }
      }
      if (result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private EUnitDirection unitDirectionFromGrid(Grid self, Grid target)
    {
      int num1 = target.x - self.x;
      int num2 = target.y - self.y;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 > num4)
      {
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        if (num1 > 0)
          return EUnitDirection.PositiveX;
      }
      if (num3 < num4)
      {
        if (num2 < 0)
          return EUnitDirection.NegativeY;
        if (num2 > 0)
          return EUnitDirection.PositiveY;
      }
      if (num1 > 0)
        return EUnitDirection.PositiveX;
      if (num1 < 0)
        return EUnitDirection.NegativeX;
      return num2 > 0 || num2 >= 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeY;
    }

    private EUnitDirection unitDirectionFromGridLaserTwin(Grid self, Grid target)
    {
      int num1 = target.x - self.x;
      int num2 = target.y - self.y;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 > num4)
      {
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        if (num1 > 0)
          return EUnitDirection.PositiveX;
      }
      if (num3 < num4)
      {
        if (num2 < 0)
          return EUnitDirection.NegativeY;
        if (num2 > 0)
          return EUnitDirection.PositiveY;
      }
      if (num1 > 0)
        return num2 < 0 ? EUnitDirection.NegativeY : EUnitDirection.PositiveX;
      if (num1 < 0)
        return num2 > 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeX;
      return num2 > 0 || num2 >= 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeY;
    }

    private void CreateGridMapWall(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          this.SetGridMap(ref result, target, currentMap[target.x + num1, target.y + num2]);
        }
      }
    }

    private void CreateGridMapWallPlus(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          this.SetGridMap(ref result, target, currentMap[target.x + num1, target.y + num2]);
          int num3 = num1 + Unit.DIRECTION_OFFSETS[index1, 0];
          int num4 = num2 + Unit.DIRECTION_OFFSETS[index1, 1];
          this.SetGridMap(ref result, target, currentMap[target.x + num3, target.y + num4]);
        }
      }
    }

    private void CreateGridMapBishop(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if ((range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2)) && Math.Abs(index2) == Math.Abs(index1))
              this.SetGridMap(ref result, target, currentMap[target.x + index2, target.y + index1]);
          }
        }
      }
    }

    private void CreateGridMapVictory(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num3 = num1 + Unit.DIRECTION_OFFSETS[index1, 0] * Math.Abs(index2);
          int num4 = num2 + Unit.DIRECTION_OFFSETS[index1, 1] * Math.Abs(index2);
          this.SetGridMap(ref result, target, currentMap[target.x + num3, target.y + num4]);
        }
      }
    }

    private void CreateGridMapLaserSpread(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 1);
        int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 1);
        for (int index3 = -index2; index3 <= index2; ++index3)
        {
          int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
          int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
          this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapLaserWide(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        if (index2 != 0)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 1);
          int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 1);
          this.SetGridMap(ref result, target, currentMap[self.x + num1, self.y + num2]);
          for (int index3 = 0; index3 < 4; ++index3)
          {
            int num3 = Unit.DIRECTION_OFFSETS[index3, 0];
            int num4 = Unit.DIRECTION_OFFSETS[index3, 1];
            this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapHorse(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      ++range_max;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if ((range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2)) && (Math.Abs(index2) == Math.Abs(index1) || Math.Abs(index2) <= 1 && Math.Abs(index1) <= 1))
              this.SetGridMap(ref result, target, currentMap[target.x + index2, target.y + index1]);
          }
        }
      }
    }

    private void CreateGridMapLaserTwin(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGridLaserTwin(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        if (index2 != 0)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          for (int index3 = -1; index3 <= 1; ++index3)
          {
            if (index3 != 0)
            {
              int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
              int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
              this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
            }
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapLaserTriple(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.unitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 2);
        int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 2);
        if (index2 == 0)
        {
          int num3 = Unit.DIRECTION_OFFSETS[index1, 0];
          int num4 = Unit.DIRECTION_OFFSETS[index1, 1];
          this.SetGridMap(ref result, target, currentMap[self.x + num3, self.y + num4]);
          for (int index3 = -1; index3 <= 1; ++index3)
          {
            int num5 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
            int num6 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
            this.SetGridMap(ref result, target, currentMap[self.x + num1 + num5, self.y + num2 + num6]);
          }
        }
        else
        {
          for (int index3 = -2; index3 <= 2; ++index3)
          {
            if (Math.Abs(index3) != 1)
            {
              int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
              int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
              this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
            }
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private bool CheckEnableAttackHeight(Grid start, Grid goal, int diff_ok)
    {
      return Math.Abs(goal.height - start.height) <= diff_ok;
    }

    private void SetGridMap(ref GridMap<bool> gridmap, Grid start, Grid goal)
    {
      if (goal == null || gridmap.get(goal.x, goal.y))
        return;
      gridmap.set(goal.x, goal.y, true);
    }

    private GridMap<bool> CreateSkillRangeMapAll(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      result.fill(false);
      if (is_move && !self.IsUnitFlag(EUnitFlag.Moved) && self.IsEnableMoveCondition(false))
      {
        GridMap<int> moveMap = this.CreateMoveMap(self, 0);
        for (int index1 = 0; index1 < moveMap.w; ++index1)
        {
          for (int index2 = 0; index2 < moveMap.h; ++index2)
          {
            if (moveMap.get(index1, index2) >= 0)
              this.CreateSelectGridMap(self, index1, index2, skill, ref result, true);
          }
        }
      }
      else
        this.CreateSelectGridMap(self, self.x, self.y, skill, ref result, true);
      return result;
    }

    private GridMap<bool> CreateSkillScopeMapAll(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> skillRangeMapAll = this.CreateSkillRangeMapAll(self, skill, is_move);
      if (skill.SkillParam.select_scope == ESelectType.Laser)
        return skillRangeMapAll;
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      result.fill(false);
      for (int index1 = 0; index1 < skillRangeMapAll.w; ++index1)
      {
        for (int index2 = 0; index2 < skillRangeMapAll.h; ++index2)
        {
          if (skillRangeMapAll.get(index1, index2))
          {
            result.set(index1, index2, true);
            this.CreateScopeGridMap(self, index1, index2, index1, index2, skill, ref result, true);
          }
        }
      }
      return result;
    }

    public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap)
    {
      List<Unit> targets = new List<Unit>(this.mOrder.Count);
      this.SearchTargetsInGridMap(self, skill, areamap, targets);
      return targets;
    }

    public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap, List<Unit> targets)
    {
      BattleMap currentMap = this.CurrentMap;
      targets.Clear();
      if (areamap == null)
      {
        Grid start = currentMap[self.x, self.y];
        int unitMaxAttackHeight = this.GetUnitMaxAttackHeight(self, skill);
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if ((skill == null || this.CheckSkillTarget(self, mUnit, skill)) && this.CheckEnableAttackHeight(start, currentMap[mUnit.x, mUnit.y], unitMaxAttackHeight))
            targets.Add(mUnit);
        }
        return targets;
      }
      for (int x = 0; x < areamap.w; ++x)
      {
        for (int y = 0; y < areamap.h; ++y)
        {
          if (areamap.get(x, y))
          {
            Unit unitAtGrid = this.FindUnitAtGrid(currentMap[x, y]);
            if (unitAtGrid != null && !targets.Contains(unitAtGrid) && (skill == null || this.CheckSkillTarget(self, unitAtGrid, skill)))
              targets.Add(unitAtGrid);
          }
        }
      }
      return targets;
    }

    private void GetExecuteSkillLineTarget(Unit self, int target_x, int target_y, SkillData skill, ref List<Unit> targets, ref BattleCore.ShotTarget shot)
    {
      if (targets == null)
        targets = new List<Unit>(this.Enemys.Count);
      shot = (BattleCore.ShotTarget) null;
      this.mGridLines.Clear();
      BattleMap currentMap = this.CurrentMap;
      if ((self.x != target_x || self.y != target_y) && skill.CheckGridLineSkill())
      {
        if (skill.CheckUnitSkillTarget() && self.CastSkill != skill)
        {
          if (!skill.IsAreaSkill())
          {
            Unit unitAtGrid = this.FindUnitAtGrid(currentMap[target_x, target_y]);
            if (!this.CheckSkillTarget(self, unitAtGrid, skill))
              return;
          }
          else
          {
            this.CreateScopeGridMap(self, self.x, self.y, target_x, target_y, skill, ref this.mScopeMap, false);
            List<Unit> unitList = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
            if (unitList == null || unitList.Count == 0)
              return;
          }
        }
        this.GetSkillGridLines(self, target_x, target_y, skill, ref this.mGridLines);
        switch (skill.LineType)
        {
          case ELineType.Direct:
          case ELineType.Stab:
            if (shot == null)
              shot = new BattleCore.ShotTarget();
            shot.end = currentMap[target_x, target_y];
            for (int index = 0; index < this.mGridLines.Count; ++index)
            {
              Unit unitAtGrid = this.FindUnitAtGrid(this.mGridLines[index]);
              if (unitAtGrid != null && !unitAtGrid.IsJump)
              {
                shot.piercers.Add(unitAtGrid);
                targets.Add(unitAtGrid);
                if (!skill.IsPierce())
                {
                  shot.end = this.mGridLines[index];
                  break;
                }
              }
            }
            break;
          case ELineType.Curved:
            double num1 = (double) (self.GetAttackHeight() + 2);
            List<BattleCore.ShotTarget> shotTargetList = new List<BattleCore.ShotTarget>();
            Grid grid1 = currentMap[self.x, self.y];
            Grid grid2 = currentMap[target_x, target_y];
            double num2 = 0.0;
            if (num2 < (double) grid1.height)
              num2 = (double) grid1.height;
            for (int index = 0; index < this.mGridLines.Count; ++index)
            {
              if (num2 < (double) this.mGridLines[index].height)
                num2 = (double) this.mGridLines[index].height;
            }
            double num3 = num2 + 1.0;
            int num4 = grid2.x - grid1.x;
            int num5 = grid2.y - grid1.y;
            double val2 = (double) BattleCore.Sqrt(num4 * num4 + num5 * num5);
            double num6 = (double) (grid1.height - grid2.height);
            double num7 = 9.8;
            for (int index1 = 0; (double) index1 <= num1; ++index1)
            {
              double num8 = num3 + (double) index1;
              double d1 = 2.0 * num7 * (num8 - num6);
              double d2 = 2.0 * num7 * num8;
              double num9 = d1 <= 0.0 ? 0.0 : Math.Sqrt(d1);
              double num10 = d2 <= 0.0 ? 0.0 : Math.Sqrt(d2);
              double num11 = (num9 + num10) / num7;
              double d3 = Math.Pow(val2 / num11, 2.0) + 2.0 * num7 * (num8 - num6);
              double num12 = d3 <= 0.0 ? 0.0 : Math.Sqrt(d3);
              double a = Math.Atan(num11 * num9 / val2);
              double num13 = a * 180.0 / Math.PI;
              double num14 = num11 / val2;
              BattleCore.ShotTarget shotTarget = new BattleCore.ShotTarget();
              shotTarget.rad = num13;
              shotTarget.height = num8;
              shotTarget.end = grid2;
              for (int index2 = 0; index2 < this.mGridLines.Count; ++index2)
              {
                int num15 = this.mGridLines[index2].x - grid1.x;
                int num16 = this.mGridLines[index2].y - grid1.y;
                double num17 = Math.Min((double) BattleCore.Sqrt(num15 * num15 + num16 * num16), val2);
                double x = num14 * num17;
                double num18 = Math.Sin(a);
                double num19 = Math.Pow(x, 2.0);
                double num20 = num7 * num19 * 0.5;
                double num21 = num12 * x * num18 - num20;
                double num22 = (double) (this.mGridLines[index2].height - grid1.height) - 0.01;
                double num23 = num22 + 2.0;
                if (num21 >= num22)
                {
                  if (num21 < num23)
                  {
                    Unit unitAtGrid = this.FindUnitAtGrid(this.mGridLines[index2]);
                    if (unitAtGrid != null && !unitAtGrid.IsJump)
                    {
                      shotTarget.piercers.Add(unitAtGrid);
                      if (!skill.IsPierce())
                      {
                        shotTarget.end = this.mGridLines[index2];
                        break;
                      }
                    }
                  }
                }
                else
                  break;
              }
              shotTargetList.Add(shotTarget);
            }
            Unit unitAtGrid1 = this.FindUnitAtGrid(grid2);
            if (unitAtGrid1 != null && !unitAtGrid1.IsJump)
            {
              for (int index1 = 0; index1 < shotTargetList.Count; ++index1)
              {
                if (shotTargetList[index1].piercers.Contains(unitAtGrid1))
                {
                  for (int index2 = 0; index2 < shotTargetList[index1].piercers.Count; ++index2)
                    targets.Add(shotTargetList[index1].piercers[index2]);
                  shot = shotTargetList[index1];
                  break;
                }
              }
            }
            if (shot == null)
            {
              shot = shotTargetList[0];
              for (int index = 0; index < shotTargetList.Count; ++index)
              {
                if (grid2 == shotTargetList[index].end)
                {
                  shot = shotTargetList[index];
                  break;
                }
              }
              break;
            }
            break;
        }
      }
      else
      {
        Unit unitAtGrid = this.FindUnitAtGrid(currentMap[target_x, target_y]);
        if (unitAtGrid != null && this.CheckSkillTarget(self, unitAtGrid, skill) && skill.SkillParam.select_scope != ESelectType.SquareOutline)
          targets.Add(unitAtGrid);
      }
      if (!skill.IsAreaSkill())
        return;
      Grid grid = shot != null ? shot.end : currentMap[target_x, target_y];
      this.CreateScopeGridMap(self, self.x, self.y, grid.x, grid.y, skill, ref this.mScopeMap, false);
      List<Unit> unitList1 = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
      for (int index = 0; index < unitList1.Count; ++index)
      {
        if (!targets.Contains(unitList1[index]))
          targets.Add(unitList1[index]);
      }
    }

    public BattleCore.CommandResult GetCommandResult(Unit self, int x, int y, int tx, int ty, SkillData skill)
    {
      this.SetBattleFlag(EBattleFlag.PredictResult, true);
      BattleCore.CommandResult commandResult = new BattleCore.CommandResult();
      commandResult.self = self;
      commandResult.skill = skill;
      this.mRandDamage.Seed(this.mSeedDamage);
      this.CurrentRand = this.mRandDamage;
      int x1 = self.x;
      int y1 = self.y;
      EUnitDirection direction = self.Direction;
      self.x = x;
      self.y = y;
      if (tx != x || ty != y)
        self.Direction = BattleCore.UnitDirectionFromVector(self, tx - x, ty - y);
      if (!this.CheckEnableUseSkill(self, skill, false) || !this.IsUseSkillCollabo(self, skill))
      {
        this.DebugErr("スキル使用条件を満たせなかった");
      }
      else
      {
        List<Unit> targets = (List<Unit>) null;
        BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
        this.GetExecuteSkillLineTarget(self, tx, ty, skill, ref targets, ref shot);
        if (targets != null && targets.Count > 0)
        {
          LogSkill log = new LogSkill();
          log.self = self;
          log.skill = skill;
          log.pos.x = tx;
          log.pos.y = ty;
          log.reflect = (LogSkill.Reflection) null;
          log.is_append = !skill.IsCutin();
          for (int index = 0; index < targets.Count; ++index)
            log.SetSkillTarget(self, targets[index]);
          if (shot != null)
          {
            log.pos.x = shot.end.x;
            log.pos.y = shot.end.y;
            log.rad = (int) (shot.rad * 100.0);
            log.height = (int) (shot.height * 100.0);
          }
          List<LogSkill> results = new List<LogSkill>();
          commandResult.targets = new List<BattleCore.UnitResult>(log.targets.Count);
          commandResult.reactions = new List<BattleCore.UnitResult>(log.targets.Count);
          this.ExecuteFirstReactionSkill(self, targets, skill, tx, ty, results);
          log.CheckAliveTarget();
          this.ExecuteSkill(ESkillTiming.Used, log, skill);
          this.ExecuteReactionSkill(log, results);
          for (int index = 0; index < log.targets.Count; ++index)
          {
            Unit target = log.targets[index].target;
            BattleCore.UnitResult unitResult = new BattleCore.UnitResult();
            unitResult.unit = target;
            unitResult.hp_damage += log.targets[index].GetTotalHpDamage();
            unitResult.hp_heal += log.targets[index].GetTotalHpHeal();
            unitResult.mp_damage += log.targets[index].GetTotalMpDamage();
            unitResult.mp_heal += log.targets[index].GetTotalMpHeal();
            unitResult.critical = this.GetCriticalRate(self, target, skill);
            unitResult.avoid = this.GetAvoidRate(self, target, skill);
            commandResult.targets.Add(unitResult);
          }
          if (log.self_effect != null)
          {
            commandResult.self_effect = new BattleCore.UnitResult();
            commandResult.self_effect.unit = log.self_effect.target;
            commandResult.self_effect.hp_damage += log.self_effect.GetTotalHpDamage();
            commandResult.self_effect.hp_heal += log.self_effect.GetTotalHpHeal();
            commandResult.self_effect.mp_damage += log.self_effect.GetTotalMpDamage();
            commandResult.self_effect.mp_heal += log.self_effect.GetTotalMpHeal();
          }
          for (int index1 = 0; index1 < results.Count; ++index1)
          {
            for (int index2 = 0; index2 < results[index1].targets.Count; ++index2)
            {
              LogSkill.Target target1 = results[index1].targets[index2];
              Unit target2 = target1.target;
              BattleCore.UnitResult unitResult = new BattleCore.UnitResult();
              unitResult.react_unit = results[index1].self;
              unitResult.unit = target2;
              unitResult.hp_damage += target1.GetTotalHpDamage();
              unitResult.hp_heal += target1.GetTotalHpHeal();
              unitResult.mp_damage += target1.GetTotalMpDamage();
              unitResult.mp_heal += target1.GetTotalMpHeal();
              unitResult.critical = this.GetCriticalRate(self, target2, skill);
              unitResult.avoid = this.GetAvoidRate(self, target2, skill);
              unitResult.reaction = (int) results[index1].skill.EffectRate <= 0 || (int) results[index1].skill.EffectRate >= 100 ? 100 : (int) results[index1].skill.EffectRate;
              commandResult.reactions.Add(unitResult);
            }
          }
        }
      }
      self.x = x1;
      self.y = y1;
      self.Direction = direction;
      this.CurrentRand = this.mRand;
      self.SetUnitFlag(EUnitFlag.SideAttack, false);
      self.SetUnitFlag(EUnitFlag.BackAttack, false);
      this.SetBattleFlag(EBattleFlag.PredictResult, false);
      return commandResult;
    }

    public bool UseSkill(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget = false, int ux = 0, int uy = 0)
    {
      DebugUtility.Assert(self != null, "self == null");
      DebugUtility.Assert(skill != null, "skill == null");
      Unit unit1 = (Unit) null;
      if (skill.EffectType == SkillEffectTypes.Throw)
      {
        unit1 = this.FindUnitAtGrid(ux, uy);
        if (unit1 == null)
          return false;
      }
      if (this.CheckEnableSuspendSave() && !this.IsUnitAuto(self) && (self.CastSkill != skill && skill.SkillType != ESkillType.Item))
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.UseSkill,
          uid = this.CurrentUnit.UnitData.UniqueID,
          x = self.x,
          y = self.y,
          dir = (int) self.Direction,
          skill = skill.SkillID,
          tx = gx,
          ty = gy,
          locked = !bUnitLockTarget ? 0 : 1,
          ux = ux,
          uy = uy
        });
      this.mSuspendUseSkillID = skill.SkillID;
      if (self.UnitParam != null)
        this.mSuspendUseSkillUnitID = self.UnitParam.iname;
      if (skill.IsCastSkill() && self.CastSkill != skill)
      {
        if (!this.CheckEnableUseSkill(self, skill, false) || !this.IsUseSkillCollabo(self, skill))
        {
          this.DebugErr("スキル使用条件を満たせなかった");
          return false;
        }
        this.CastStart(self, gx, gy, skill, bUnitLockTarget);
        return true;
      }
      if (self.Side == EUnitSide.Player)
      {
        if (skill.SkillType == ESkillType.Item)
          ++this.mNumUsedItems;
        else if (skill.SkillType == ESkillType.Skill)
          ++this.mNumUsedSkills;
      }
      this.mRandDamage.Seed(this.mSeedDamage);
      this.CurrentRand = this.mRandDamage;
      if (gx != self.x || gy != self.y)
        self.Direction = BattleCore.UnitDirectionFromVector(self, gx - self.x, gy - self.y);
      int effectRate = (int) skill.EffectRate;
      if (effectRate > 0 && effectRate < 100 && (int) (this.GetRandom() % 100U) > effectRate)
      {
        self.SetUnitFlag(EUnitFlag.Action, true);
        self.SetCommandFlag(EUnitCommandFlag.Action, true);
        if (skill.Timing != ESkillTiming.Auto)
          this.Log<LogMapCommand>();
        return true;
      }
      int skillUsedCost = self.GetSkillUsedCost(skill);
      int target_x = gx;
      int target_y = gy;
      List<Unit> targets = (List<Unit>) null;
      BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
      if (skill.EffectType == SkillEffectTypes.Throw)
      {
        targets = new List<Unit>(1);
        targets.Add(unit1);
      }
      else
        this.GetExecuteSkillLineTarget(self, target_x, target_y, skill, ref targets, ref shot);
      int x = self.x;
      int y = self.y;
      this.ExecuteFirstReactionSkill(self, targets, skill, gx, gy, (List<LogSkill>) null);
      this.mRandDamage.Seed(this.mSeedDamage);
      this.CurrentRand = this.mRandDamage;
      if (self.x != x || self.y != y)
      {
        this.CreateSelectGridMap(self, self.x, self.y, skill, ref this.mRangeMap, false);
        List<Unit> unitList = new List<Unit>(targets.Count);
        using (List<Unit>.Enumerator enumerator = targets.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Unit current = enumerator.Current;
            if (this.mRangeMap.get(current.x, current.y))
              unitList.Add(current);
          }
        }
        if (targets.Count != unitList.Count)
          targets = unitList;
      }
      if (!self.IsDead)
      {
        LogSkill log = this.Log<LogSkill>();
        log.self = self;
        log.skill = skill;
        log.pos.x = target_x;
        log.pos.y = target_y;
        log.is_append = !skill.IsCutin();
        if (shot != null)
        {
          log.pos.x = shot.end.x;
          log.pos.y = shot.end.y;
          log.rad = (int) (shot.rad * 100.0);
          log.height = (int) (shot.height * 100.0);
        }
        for (int index = 0; index < targets.Count; ++index)
          log.SetSkillTarget(self, targets[index]);
        if (skill.IsDamagedSkill())
        {
          for (int index = 0; index < targets.Count; ++index)
          {
            Unit guardMan = this.GetGuardMan(targets[index]);
            if (guardMan != null && guardMan != self && !targets.Contains(guardMan))
            {
              LogSkill.Target target = log.FindTarget(targets[index]);
              target.guard = targets[index];
              target.target = guardMan;
              targets[index] = guardMan;
            }
          }
        }
        if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        {
          Unit unit2 = (Unit) null;
          if ((bool) skill.IsCollabo)
            unit2 = self.GetUnitUseCollaboSkill(skill, false);
          if (skill.EffectType != SkillEffectTypes.GemsGift)
          {
            this.SubGems(self, skillUsedCost);
            if (unit2 != null)
              this.SubGems(unit2, skillUsedCost);
          }
          self.UpdateSkillUseCount(skill, -1);
          if (unit2 != null)
          {
            SkillData skillUseCount = unit2.GetSkillUseCount(skill.SkillParam.iname);
            if (skillUseCount != null)
              unit2.UpdateSkillUseCount(skillUseCount, -1);
          }
        }
        log.CheckAliveTarget();
        this.ExecuteSkill(skill.Timing != ESkillTiming.Auto ? ESkillTiming.Used : ESkillTiming.Auto, log, skill);
        self.CancelCastSkill();
        this.ExecuteReactionSkill(log, (List<LogSkill>) null);
      }
      self.SetUnitFlag(EUnitFlag.Action, true);
      self.SetCommandFlag(EUnitCommandFlag.Action, true);
      this.CurrentRand = this.mRand;
      if (this.GetQuestResult() != BattleCore.QuestResult.Pending)
      {
        this.ExecuteEventTriggerOnGrid(self, EEventTrigger.Stop, false);
        this.CalcQuestRecord();
        this.MapEnd();
      }
      else if (this.CurrentOrderData.IsCastSkill)
      {
        this.Log<LogCastSkillEnd>();
      }
      else
      {
        bool flag = false;
        if (self == this.CurrentUnit)
          flag = self.IsDead || (!self.IsUnitFlag(EUnitFlag.Moved) ? !self.IsEnableMoveCondition(false) && !self.IsEnableSelectDirectionCondition() : !self.IsEnableSelectDirectionCondition());
        if (flag)
          this.InternalLogUnitEnd();
        else if (skill.Timing != ESkillTiming.Auto)
          this.Log<LogMapCommand>();
      }
      return true;
    }

    private void ExecuteSkill(ESkillTiming timing, LogSkill log, SkillData skill)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CExecuteSkill\u003Ec__AnonStorey1B3 skillCAnonStorey1B3 = new BattleCore.\u003CExecuteSkill\u003Ec__AnonStorey1B3();
      if (timing != skill.Timing)
        return;
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self = log.self;
      // ISSUE: reference to a compiler-generated field
      if (!this.CheckSkillCondition(skillCAnonStorey1B3.self, skill))
        return;
      if (skill.Target == ESkillTarget.GridNoUnit)
      {
        if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        {
          if (skill.EffectType == SkillEffectTypes.Teleport)
          {
            // ISSUE: reference to a compiler-generated field
            skillCAnonStorey1B3.self.x = log.pos.x;
            // ISSUE: reference to a compiler-generated field
            skillCAnonStorey1B3.self.y = log.pos.y;
          }
          else if (skill.EffectType == SkillEffectTypes.Throw && log.targets != null && log.targets.Count > 0)
          {
            Unit target = log.targets[0].target;
            target.x = log.pos.x;
            target.y = log.pos.y;
            this.ExecuteEventTriggerOnGrid(target, EEventTrigger.Stop, true);
          }
        }
      }
      else
      {
        int num1 = Math.Max((int) skill.SkillParam.ComboNum, 1);
        for (int index1 = 0; index1 < log.targets.Count; ++index1)
        {
          Unit target = log.targets[index1].target;
          bool flag = true;
          SkillEffectTypes effectType = skill.EffectType;
          switch (effectType)
          {
            case SkillEffectTypes.ReflectDamage:
            case SkillEffectTypes.RateDamage:
label_13:
              for (int index2 = 0; index2 < num1; ++index2)
              {
                // ISSUE: reference to a compiler-generated field
                this.DamageSkill(skillCAnonStorey1B3.self, target, skill, log);
              }
              break;
            case SkillEffectTypes.GemsGift:
              // ISSUE: reference to a compiler-generated field
              int skillEffectValue = this.GetSkillEffectValue(skillCAnonStorey1B3.self, target, skill);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              int num2 = Math.Min(SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, skillCAnonStorey1B3.self.Gems) + skill.Cost, skillCAnonStorey1B3.self.Gems);
              // ISSUE: reference to a compiler-generated field
              log.Hit(skillCAnonStorey1B3.self, target, 0, 0, 0, 0, 0, num2, 0, 0, 0, false, false, false, false, 0);
              log.ToSelfSkillEffect(0, num2, 0, 0, 0, 0, 0, 0, 0, false, false, false, false);
              // ISSUE: reference to a compiler-generated field
              this.SubGems(skillCAnonStorey1B3.self, num2);
              this.AddGems(target, num2);
              break;
            case SkillEffectTypes.GemsIncDec:
              // ISSUE: reference to a compiler-generated field
              int num3 = this.GetSkillEffectValue(skillCAnonStorey1B3.self, target, skill);
              if (skill.EffectCalcType == SkillParamCalcTypes.Scale)
                num3 = (int) target.MaximumStatus.param.mp * num3 / 100;
              if (num3 < 0)
              {
                // ISSUE: reference to a compiler-generated field
                log.Hit(skillCAnonStorey1B3.self, target, 0, Math.Abs(num3), 0, 0, 0, 0, 0, 0, 0, false, false, false, false, 0);
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                log.Hit(skillCAnonStorey1B3.self, target, 0, 0, 0, 0, 0, num3, 0, 0, 0, false, false, false, false, 0);
              }
              this.AddGems(target, num3);
              break;
            case SkillEffectTypes.Guard:
              if (!this.IsBattleFlag(EBattleFlag.PredictResult))
              {
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey1B3.self.SetGuardTarget(target, 3);
                break;
              }
              break;
            case SkillEffectTypes.Changing:
              if (!this.IsBattleFlag(EBattleFlag.PredictResult))
              {
                int x = target.x;
                int y = target.y;
                // ISSUE: reference to a compiler-generated field
                target.x = skillCAnonStorey1B3.self.x;
                // ISSUE: reference to a compiler-generated field
                target.y = skillCAnonStorey1B3.self.y;
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey1B3.self.x = x;
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey1B3.self.y = y;
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey1B3.self.startX = x;
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey1B3.self.startY = y;
                break;
              }
              break;
            case SkillEffectTypes.RateHeal:
label_16:
              // ISSUE: reference to a compiler-generated field
              this.HealSkill(skillCAnonStorey1B3.self, target, skill, log);
              break;
            default:
              switch (effectType - 2)
              {
                case SkillEffectTypes.None:
                  goto label_13;
                case SkillEffectTypes.Attack:
                  goto label_16;
              }
          }
          if (flag && !log.targets[index1].IsAvoid() && target.CheckDamageActionStart())
          {
            // ISSUE: reference to a compiler-generated field
            this.NotifyDamagedActionStart(skillCAnonStorey1B3.self, target);
          }
        }
        if (this.isKnockBack(skill) && skill.IsDamagedSkill())
        {
          List<LogSkill.Target> l = new List<LogSkill.Target>((IEnumerable<LogSkill.Target>) log.targets);
          if (l.Count > 1)
          {
            // ISSUE: reference to a compiler-generated method
            MySort<LogSkill.Target>.Sort(l, new Comparison<LogSkill.Target>(skillCAnonStorey1B3.\u003C\u003Em__152));
          }
          using (List<LogSkill.Target>.Enumerator enumerator = l.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LogSkill.Target current = enumerator.Current;
              // ISSUE: reference to a compiler-generated field
              if ((current.GetTotalHpDamage() > 0 || current.GetTotalMpDamage() > 0) && this.checkKnockBack(skillCAnonStorey1B3.self, current.target, skill))
              {
                // ISSUE: reference to a compiler-generated field
                Grid gridKnockBack = this.getGridKnockBack(skillCAnonStorey1B3.self, current.target, skill);
                if (gridKnockBack != null)
                {
                  current.KnockBackGrid = gridKnockBack;
                  if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                  {
                    current.target.x = gridKnockBack.x;
                    current.target.y = gridKnockBack.y;
                    this.ExecuteEventTriggerOnGrid(current.target, EEventTrigger.Stop, true);
                    // ISSUE: reference to a compiler-generated field
                    skillCAnonStorey1B3.self.RefleshMomentBuff(this.Units);
                  }
                }
              }
            }
          }
        }
        int gainJewel = log.GetGainJewel();
        if (gainJewel > 0)
        {
          // ISSUE: reference to a compiler-generated field
          this.AddGems(skillCAnonStorey1B3.self, gainJewel);
        }
        for (int index1 = 0; index1 < log.targets.Count; ++index1)
        {
          Unit target = log.targets[index1].target;
          if (!target.IsDead && (!skill.IsDamagedSkill() || !log.targets[index1].IsAvoid()))
          {
            SkillEffectTypes effectType = skill.EffectType;
            switch (effectType)
            {
              case SkillEffectTypes.Attack:
                if (log.targets[index1].GetTotalHpDamage() > 0)
                {
                  this.DamageCureCondition(target, log);
                  break;
                }
                break;
              case SkillEffectTypes.Heal:
                if (log.targets[index1].GetTotalHpHeal() > 0)
                {
                  this.HealCureCondition(target, log);
                  break;
                }
                break;
              default:
                if (effectType != SkillEffectTypes.RateHeal)
                {
                  if (effectType == SkillEffectTypes.RateDamage)
                    goto case SkillEffectTypes.Attack;
                  else
                    break;
                }
                else
                  goto case SkillEffectTypes.Heal;
            }
            // ISSUE: reference to a compiler-generated field
            this.BuffSkill(timing, skillCAnonStorey1B3.self, target, skill, false, log, SkillEffectTargets.Target, false);
            // ISSUE: reference to a compiler-generated field
            this.CondSkill(timing, skillCAnonStorey1B3.self, target, skill, false, log, SkillEffectTargets.Target);
            if (skill.IsNormalAttack())
            {
              // ISSUE: reference to a compiler-generated field
              JobData job = skillCAnonStorey1B3.self.Job;
              if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
              {
                List<ArtifactData> artifactDataList = new List<ArtifactData>();
                if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
                  artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
                if (!string.IsNullOrEmpty(job.SelectedSkin))
                {
                  ArtifactData selectedSkinData = job.GetSelectedSkinData();
                  if (selectedSkinData != null)
                    artifactDataList.Add(selectedSkinData);
                }
                for (int index2 = 0; index2 < artifactDataList.Count; ++index2)
                {
                  ArtifactData artifactData = artifactDataList[index2];
                  if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    this.BuffSkill(timing, skillCAnonStorey1B3.self, target, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Target, false);
                    // ISSUE: reference to a compiler-generated field
                    this.CondSkill(timing, skillCAnonStorey1B3.self, target, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Target);
                  }
                }
              }
            }
            // ISSUE: reference to a compiler-generated field
            this.StealSkill(skillCAnonStorey1B3.self, target, skill, log);
            this.ShieldSkill(target, skill);
            if (!this.IsBattleFlag(EBattleFlag.PredictResult))
            {
              if (skill.IsCastBreak() && target.CastSkill != null)
              {
                target.CancelCastSkill();
                log.targets[index1].hitType |= LogSkill.EHitTypes.CastBreak;
              }
              target.ChargeTime = (OInt) SkillParam.CalcSkillEffectValue(skill.ControlChargeTimeCalcType, (int) skill.ControlChargeTimeValue, (int) target.ChargeTime);
            }
          }
        }
      }
      // ISSUE: reference to a compiler-generated field
      int hpCost = this.GetHpCost(skillCAnonStorey1B3.self, skill);
      if (hpCost > 0)
      {
        if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        {
          // ISSUE: reference to a compiler-generated field
          skillCAnonStorey1B3.self.Damage(hpCost);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (skillCAnonStorey1B3.self.IsPartyMember && hpCost > 0 && skillCAnonStorey1B3.self.IsPartyMember)
            this.mTotalDamagesTaken += hpCost;
        }
        log.hp_cost = hpCost;
      }
      // ISSUE: reference to a compiler-generated field
      if (!skillCAnonStorey1B3.self.IsDead)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.BuffSkill(timing, skillCAnonStorey1B3.self, skillCAnonStorey1B3.self, skill, false, log, SkillEffectTargets.Self, false);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.CondSkill(timing, skillCAnonStorey1B3.self, skillCAnonStorey1B3.self, skill, false, log, SkillEffectTargets.Self);
        if (skill.IsNormalAttack())
        {
          // ISSUE: reference to a compiler-generated field
          JobData job = skillCAnonStorey1B3.self.Job;
          if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
          {
            List<ArtifactData> artifactDataList = new List<ArtifactData>();
            if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
              artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
            if (!string.IsNullOrEmpty(job.SelectedSkin))
            {
              ArtifactData selectedSkinData = job.GetSelectedSkinData();
              if (selectedSkinData != null)
                artifactDataList.Add(selectedSkinData);
            }
            for (int index = 0; index < artifactDataList.Count; ++index)
            {
              ArtifactData artifactData = artifactDataList[index];
              if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                this.BuffSkill(timing, skillCAnonStorey1B3.self, skillCAnonStorey1B3.self, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Self, false);
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                this.CondSkill(timing, skillCAnonStorey1B3.self, skillCAnonStorey1B3.self, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Self);
              }
            }
          }
        }
      }
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index = 0; index < log.targets.Count; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        this.GridEventStart(skillCAnonStorey1B3.self, log.targets[index].target, EEventTrigger.HpDownBorder);
      }
      for (int index = 0; index < log.targets.Count; ++index)
      {
        if (log.targets[index].target.IsDead)
        {
          // ISSUE: reference to a compiler-generated field
          this.Dead(skillCAnonStorey1B3.self, log.targets[index].target, DeadTypes.Damage);
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (skillCAnonStorey1B3.self.IsDead)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.Dead(skillCAnonStorey1B3.self, skillCAnonStorey1B3.self, DeadTypes.Damage);
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (skillCAnonStorey1B3.self.CastSkill != null && skillCAnonStorey1B3.self.CastSkill == skill && skillCAnonStorey1B3.self.CastSkill.CastType == ECastTypes.Jump)
      {
        // ISSUE: reference to a compiler-generated field
        log.landing = this.GetCorrectDuplicatePosition(skillCAnonStorey1B3.self);
        if (log.landing != null)
        {
          // ISSUE: reference to a compiler-generated field
          skillCAnonStorey1B3.self.x = log.landing.x;
          // ISSUE: reference to a compiler-generated field
          skillCAnonStorey1B3.self.y = log.landing.y;
        }
      }
      if (skill.IsDamagedSkill())
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        skillCAnonStorey1B3.self.UpdateBuffEffectTurnCount(EffectCheckTimings.AttackEnd, skillCAnonStorey1B3.self);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        skillCAnonStorey1B3.self.UpdateCondEffectTurnCount(EffectCheckTimings.AttackEnd, skillCAnonStorey1B3.self);
        for (int index = 0; index < log.targets.Count; ++index)
        {
          // ISSUE: reference to a compiler-generated field
          log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.AttackEnd, skillCAnonStorey1B3.self);
          // ISSUE: reference to a compiler-generated field
          log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.AttackEnd, skillCAnonStorey1B3.self);
          if (!log.targets[index].IsAvoid())
          {
            log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.DamageEnd, log.targets[index].target);
            log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.DamageEnd, log.targets[index].target);
            if ((log.targets[index].hitType & LogSkill.EHitTypes.Guts) != (LogSkill.EHitTypes) 0)
            {
              log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.GutsEnd, log.targets[index].target);
              log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.GutsEnd, log.targets[index].target);
            }
          }
        }
      }
      for (int index = 0; index < log.targets.Count; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.SkillEnd, skillCAnonStorey1B3.self);
        // ISSUE: reference to a compiler-generated field
        log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.SkillEnd, skillCAnonStorey1B3.self);
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self.UpdateBuffEffectTurnCount(EffectCheckTimings.SkillEnd, skillCAnonStorey1B3.self);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self.UpdateCondEffectTurnCount(EffectCheckTimings.SkillEnd, skillCAnonStorey1B3.self);
      for (int index = 0; index < log.targets.Count; ++index)
      {
        log.targets[index].target.UpdateBuffEffects();
        log.targets[index].target.UpdateCondEffects();
        log.targets[index].target.CalcCurrentStatus(false);
      }
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self.UpdateBuffEffects();
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self.UpdateCondEffects();
      // ISSUE: reference to a compiler-generated field
      skillCAnonStorey1B3.self.CalcCurrentStatus(false);
      // ISSUE: reference to a compiler-generated field
      this.UpdateEntryTriggers(UnitEntryTypes.UsedSkill, skillCAnonStorey1B3.self, skill.SkillParam);
    }

    private void CastStart(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget)
    {
      Unit unit = (Unit) null;
      Grid grid = this.CurrentMap[gx, gy];
      if (bUnitLockTarget)
      {
        unit = this.FindUnitAtGrid(grid);
        grid = (Grid) null;
      }
      LogCast logCast = this.Log<LogCast>();
      logCast.self = self;
      logCast.type = skill.CastType;
      logCast.dx = gx;
      logCast.dy = gy;
      self.SetCastSkill(skill, unit, grid);
      self.SetUnitFlag(EUnitFlag.Action, true);
      self.SetCommandFlag(EUnitCommandFlag.Action, true);
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      if (skill.IsAreaSkill())
        this.CreateScopeGridMap(self, self.x, self.y, gx, gy, skill, ref result, false);
      else
        result.set(gx, gy, true);
      self.CastSkillGridMap = result;
      if (GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_CAST_SHORTCUT)
        self.SetCastTime((int) self.CastTimeMax);
      this.Log<LogMapCommand>();
    }

    private bool IsCombinationAttack(SkillData skill)
    {
      if (skill != null && skill.IsNormalAttack())
        return this.mHelperUnits.Count > 0;
      return false;
    }

    private void GetYuragiDamage(ref int damage)
    {
      if (damage < 2)
        return;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if ((int) fixParam.RandomEffectMax == 0)
        return;
      int num = (int) (this.GetRandom() % 100U) % ((int) fixParam.RandomEffectMax * 2 + 1) - (int) fixParam.RandomEffectMax;
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      damage = Math.Max(damage + num, 0);
    }

    private Unit GetGuardMan(Unit self)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].GetGuardTarget() == self)
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    private bool CheckSkillCondition(Unit self, SkillData skill)
    {
      if (skill.Condition == ESkillCondition.Dying)
        return self.IsDying();
      return true;
    }

    private void DamageSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
      bool is_guts = false;
      bool flag = false;
      bool bWeakPoint = false;
      int damage = 0;
      int absorbed = 0;
      switch (skill.EffectType)
      {
        case SkillEffectTypes.Attack:
          if (this.CheckBackAttack(self, target, skill))
            self.SetUnitFlag(EUnitFlag.BackAttack, true);
          else if (this.CheckSideAttack(self, target, skill))
            self.SetUnitFlag(EUnitFlag.SideAttack, true);
          damage = this.CalcDamage(self, target, skill, log);
          if (!skill.IsJewelAttack())
          {
            if (!this.IsCombinationAttack(skill) && (skill.IsNormalAttack() || (bool) skill.SkillParam.IsCritical))
            {
              flag = this.CheckCritical(self, target, skill);
              if (flag && !this.IsBattleFlag(EBattleFlag.PredictResult))
                damage = this.GetCriticalDamage(self, damage);
            }
            this.GetYuragiDamage(ref damage);
            bWeakPoint = this.CheckWeakPoint(self, target, skill);
            break;
          }
          break;
        case SkillEffectTypes.ReflectDamage:
          if (log.reflect != null)
          {
            int skillEffectValue = this.GetSkillEffectValue(self, target, skill);
            damage = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, log.reflect.damage);
            break;
          }
          break;
        case SkillEffectTypes.RateDamage:
          int skillEffectValue1 = this.GetSkillEffectValue(self, target, skill);
          damage = !skill.IsJewelAttack() ? (int) target.MaximumStatus.param.hp * skillEffectValue1 * 100 / 10000 : (int) target.MaximumStatus.param.mp * skillEffectValue1 * 100 / 10000;
          break;
        default:
          return;
      }
      damage = damage * (int) skill.SkillParam.ComboDamageRate / 100;
      this.DamageControlSkill(self, target, skill, ref damage, log);
      damage = Math.Max(damage, 1);
      bool is_avoid;
      if (this.CheckPerfectAvoidSkill(self, target, skill, log))
      {
        if (log != null)
        {
          LogSkill.Target target1 = log.FindTarget(target);
          if (target1 != null)
            target1.SetPerfectAvoid(true);
        }
        is_avoid = true;
      }
      else
        is_avoid = this.CheckAvoid(self, target, skill);
      if (!is_avoid)
      {
        int num = damage;
        if (skill.IsPhysicalAttack())
        {
          target.CalcShieldDamage(DamageTypes.PhyDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult));
          damage = Math.Max(damage * this.mQuestParam.PhysBonus / 100, 0);
        }
        if (skill.IsMagicalAttack())
        {
          target.CalcShieldDamage(DamageTypes.MagDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult));
          damage = Math.Max(damage * this.mQuestParam.MagBonus / 100, 0);
        }
        target.CalcShieldDamage(DamageTypes.TotalDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult));
        absorbed = num - damage;
      }
      int hp_damage = 0;
      int num1 = 0;
      int hp_heal = 0;
      int num2 = 0;
      int dropgems = 0;
      if (!skill.IsJewelAttack())
      {
        hp_damage = damage;
        if (skill.DamageAbsorbRate > 0)
          hp_heal = damage * skill.DamageAbsorbRate * 100 / 10000;
        if (!is_avoid)
        {
          switch (skill.SkillParam.JewelDamageType)
          {
            case JewelDamageTypes.Calc:
              num1 += BattleCore.Sqrt(damage) * 2;
              break;
            case JewelDamageTypes.Scale:
              num1 += (int) target.MaximumStatus.param.mp * (int) skill.SkillParam.JewelDamageValue / 100;
              break;
            case JewelDamageTypes.Fixed:
              num1 += (int) skill.SkillParam.JewelDamageValue;
              break;
          }
          if ((bool) skill.SkillParam.IsJewelAbsorb)
            num2 = num1;
        }
      }
      else
      {
        num1 = damage;
        if (skill.DamageAbsorbRate > 0)
          num2 = damage * skill.DamageAbsorbRate * 100 / 10000;
      }
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
      {
        if (!is_avoid)
        {
          if (self.IsPartyMember && hp_heal > 0 && target.Side == EUnitSide.Enemy)
            this.mTotalHeal += Math.Min((int) self.CurrentStatus.param.hp + hp_heal, (int) self.MaximumStatus.param.hp) - (int) self.CurrentStatus.param.hp;
          target.Damage(hp_damage);
          this.SubGems(target, num1);
          self.Heal(hp_heal);
          this.AddGems(self, num2);
          if (this.CheckGuts(target))
          {
            is_guts = true;
            target.Heal(1);
          }
          dropgems = this.CalcGainedGems(self, target, skill, damage, flag, bWeakPoint);
          if (self.IsPartyMember && damage > 0 && target.Side == EUnitSide.Enemy)
            this.mTotalDamages += damage;
          if (target.IsPartyMember && damage > 0 && self.Side == EUnitSide.Enemy)
            this.mTotalDamagesTaken += damage;
          this.GimmickSkillDamageCount(self, target);
        }
        else
        {
          hp_damage = 0;
          num1 = 0;
          hp_heal = 0;
          num2 = 0;
          dropgems = 0;
          flag = false;
        }
      }
      bool is_combination = this.IsCombinationAttack(skill);
      log.Hit(self, target, hp_damage, num1, 0, 0, 0, 0, 0, 0, dropgems, flag, is_avoid, is_combination, is_guts, absorbed);
      if (hp_heal != 0 || num2 != 0)
        log.ToSelfSkillEffect(0, 0, 0, 0, hp_heal, num2, 0, 0, 0, false, false, false, false);
      self.SetUnitFlag(EUnitFlag.BackAttack, false);
      self.SetUnitFlag(EUnitFlag.SideAttack, false);
    }

    private void NotifyDamagedActionStart(Unit attacker, Unit defender)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult) || !this.CheckEnemySide(attacker, defender))
        return;
      if (defender.IsUnitFlag(EUnitFlag.DamagedActionStart))
      {
        defender.SetUnitFlag(EUnitFlag.DamagedActionStart, false);
        defender.SetUnitFlag(EUnitFlag.Searched, true);
      }
      if (defender.NotifyUniqueNames == null)
        return;
      for (int index1 = 0; index1 < defender.NotifyUniqueNames.Count; ++index1)
      {
        if ((string) defender.NotifyUniqueNames[index1] == "pall")
        {
          for (int index2 = 0; index2 < this.mPlayer.Count; ++index2)
          {
            this.mPlayer[index2].SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            this.mPlayer[index2].SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
        else if ((string) defender.NotifyUniqueNames[index1] == "eall")
        {
          for (int index2 = 0; index2 < this.Enemys.Count; ++index2)
          {
            this.Enemys[index2].SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            this.Enemys[index2].SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
        else
        {
          Unit unitByUniqueName = this.FindUnitByUniqueName((string) defender.NotifyUniqueNames[index1]);
          if (unitByUniqueName != null)
          {
            unitByUniqueName.SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            unitByUniqueName.SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
      }
    }

    private int GetHpCost(Unit self, SkillData skill)
    {
      if (skill.IsSuicide())
        return (int) self.MaximumStatus.param.hp;
      int hpCost = skill.GetHpCost(self);
      int hpCostRate = skill.HpCostRate;
      if (hpCostRate == 0)
        return 0;
      if (hpCostRate >= 100)
        return hpCost;
      if ((int) (this.GetRandom() % 100U) > hpCostRate || (int) self.CurrentStatus.param.hp <= 1)
        return 0;
      if ((int) self.CurrentStatus.param.hp > hpCost)
        return hpCost;
      return (int) self.CurrentStatus.param.hp - 1;
    }

    private void DefendSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition())
        return;
      StatusParam statusParam = defender.CurrentStatus.param;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && battleSkill.EffectType == SkillEffectTypes.Defend && ((battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageCalculate) && defender.IsEnableReactionSkill(battleSkill)))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate || GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_REACTION) && !this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            int skillEffectValue = this.GetSkillEffectValue(defender, attacker, battleSkill);
            if (skillEffectValue != 0)
            {
              bool flag = false;
              switch (battleSkill.ReactionDamageType)
              {
                case DamageTypes.TotalDamage:
                  if (atkskl.IsDamagedSkill() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.def = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.def);
                    statusParam.mnd = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.mnd);
                    break;
                  }
                  break;
                case DamageTypes.PhyDamage:
                  if (atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.def = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.def);
                    break;
                  }
                  break;
                case DamageTypes.MagDamage:
                  if (atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.mnd = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.mnd);
                    break;
                  }
                  break;
              }
              if (log != null && flag)
              {
                LogSkill.Target target = log.FindTarget(defender);
                if (target != null)
                {
                  target.SetDefend(true);
                  target.defSkill = battleSkill;
                  target.defSkillUseCount = 0;
                  if ((int) battleSkill.SkillParam.count > 0)
                  {
                    defender.UpdateSkillUseCount(battleSkill, -1);
                    target.defSkillUseCount = (int) defender.GetSkillUseCount(battleSkill);
                  }
                }
              }
            }
          }
        }
      }
    }

    private bool CheckPerfectAvoidSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition())
        return false;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && battleSkill.EffectType == SkillEffectTypes.PerfectAvoid && ((battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageCalculate) && defender.IsEnableReactionSkill(battleSkill)))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate || GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_REACTION) && !this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            bool flag = false;
            switch (battleSkill.ReactionDamageType)
            {
              case DamageTypes.TotalDamage:
                flag = atkskl.IsDamagedSkill() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
              case DamageTypes.PhyDamage:
                flag = atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
              case DamageTypes.MagDamage:
                flag = atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
            }
            if (flag)
            {
              if ((int) battleSkill.SkillParam.count > 0)
                defender.UpdateSkillUseCount(battleSkill, -1);
              return true;
            }
          }
        }
      }
      return false;
    }

    private void DamageControlSkill(Unit attacker, Unit defender, SkillData atkskl, ref int damage, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition())
        return;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && (battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageControl) && defender.IsEnableReactionSkill(battleSkill))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate || GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_REACTION) && (!this.IsBattleFlag(EBattleFlag.PredictResult) && (int) battleSkill.ControlDamageValue != 0))
          {
            bool flag = false;
            switch (battleSkill.ReactionDamageType)
            {
              case DamageTypes.TotalDamage:
                if (battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
              case DamageTypes.PhyDamage:
                if (atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
              case DamageTypes.MagDamage:
                if (atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
            }
            if (log != null && flag)
            {
              LogSkill.Target target = log.FindTarget(defender);
              if (target != null)
              {
                target.SetDefend(true);
                target.defSkill = battleSkill;
                target.defSkillUseCount = 0;
                if ((int) battleSkill.SkillParam.count > 0)
                {
                  defender.UpdateSkillUseCount(battleSkill, -1);
                  target.defSkillUseCount = (int) defender.GetSkillUseCount(battleSkill);
                }
                if (battleSkill.Timing == ESkillTiming.Reaction)
                  target.SetForceReaction(true);
              }
            }
          }
        }
      }
    }

    private void HealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
      int hp_heal = 0;
      if (!target.IsUnitCondition(EUnitCondition.DisableHeal))
        hp_heal = this.CalcHeal(self, target, skill);
      log.Hit(self, target, 0, 0, 0, 0, hp_heal, 0, 0, 0, 0, false, false, false, false, 0);
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      if (target.IsPartyMember)
        this.mTotalHeal += Math.Min((int) target.CurrentStatus.param.hp + hp_heal, (int) target.MaximumStatus.param.hp) - (int) target.CurrentStatus.param.hp;
      this.Heal(target, hp_heal);
    }

    private void AutoHealSkill(Unit self)
    {
      if (!self.IsEnableAutoHealCondition())
        return;
      int autoHpHealValue = self.GetAutoHpHealValue();
      if (autoHpHealValue > 0)
      {
        LogAutoHeal logAutoHeal = this.Log<LogAutoHeal>();
        logAutoHeal.self = self;
        logAutoHeal.type = LogAutoHeal.HealType.Hp;
        logAutoHeal.value = autoHpHealValue;
        logAutoHeal.beforeHp = (int) self.CurrentStatus.param.hp;
        self.Heal(autoHpHealValue);
        if (self.IsPartyMember)
          this.mTotalHeal += Math.Max((int) self.CurrentStatus.param.hp - logAutoHeal.beforeHp, 0);
      }
      int autoMpHealValue = self.GetAutoMpHealValue();
      if (autoMpHealValue <= 0)
        return;
      LogAutoHeal logAutoHeal1 = this.Log<LogAutoHeal>();
      logAutoHeal1.self = self;
      logAutoHeal1.type = LogAutoHeal.HealType.Jewel;
      logAutoHeal1.value = autoMpHealValue;
      logAutoHeal1.beforeMp = (int) self.CurrentStatus.param.mp;
      this.AddGems(self, autoMpHealValue);
    }

    private void StealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
    }

    private void ShieldSkill(Unit target, SkillData skill)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      target.AddShield(skill);
    }

    private void ExecuteFirstReactionSkill(Unit attacker, List<Unit> targets, SkillData skill, int tx, int ty, List<LogSkill> results = null)
    {
      if (skill == null || !skill.IsNormalAttack() || (!skill.IsDamagedSkill() || attacker == null) || (attacker.IsDeadCondition() || targets == null || targets.Count == 0))
        return;
      Grid current = this.CurrentMap[tx, ty];
      if (current == null)
        return;
      Unit unitAtGrid = this.FindUnitAtGrid(current);
      if (unitAtGrid == null)
        return;
      for (int index = 0; index < targets.Count; ++index)
      {
        Unit target = targets[index];
        if (attacker != target && target.IsEnableReactionCondition() && target == unitAtGrid)
          this.InternalReactionSkill(ESkillTiming.FirstReaction, attacker, target, target, skill, 0, false, results);
      }
    }

    private void ExecuteReactionSkill(LogSkill log, List<LogSkill> results = null)
    {
      if (log == null || log.targets.Count == 0)
        return;
      SkillData skill = log.skill;
      if (!skill.IsNormalAttack() || !skill.IsDamagedSkill())
        return;
      Unit self = log.self;
      Grid current = this.CurrentMap[log.pos.x, log.pos.y];
      if (current == null)
        return;
      Unit unitAtGrid = this.FindUnitAtGrid(current);
      if (unitAtGrid == null || self.IsDead)
        return;
      for (int index = 0; index < log.targets.Count; ++index)
      {
        if (log.targets[index].guard == null && !log.targets[index].IsAvoid())
        {
          Unit target = log.targets[index].target;
          if (self != target && target.IsEnableReactionCondition() && target == unitAtGrid)
          {
            int totalHpDamage = log.targets[index].GetTotalHpDamage();
            this.InternalReactionSkill(ESkillTiming.Reaction, self, target, unitAtGrid, skill, totalHpDamage, log.targets[index].is_force_reaction, results);
          }
        }
      }
    }

    private void InternalReactionSkill(ESkillTiming timing, Unit attacker, Unit defender, Unit main_target, SkillData received_skill, int damage, bool is_forced, List<LogSkill> results)
    {
      for (int index1 = 0; index1 < defender.BattleSkills.Count; ++index1)
      {
        SkillData battleSkill = defender.BattleSkills[index1];
        if (timing == battleSkill.Timing && defender.IsEnableReactionSkill(battleSkill))
        {
          SkillEffectTypes effectType = battleSkill.EffectType;
          switch (effectType)
          {
            case SkillEffectTypes.Guard:
            case SkillEffectTypes.Teleport:
            case SkillEffectTypes.PerfectAvoid:
            case SkillEffectTypes.Throw:
              continue;
            default:
              switch (effectType - 7)
              {
                case SkillEffectTypes.None:
                case SkillEffectTypes.Defend:
                  continue;
                default:
                  switch (effectType - 1)
                  {
                    case SkillEffectTypes.None:
                    case SkillEffectTypes.Attack:
                      continue;
                    default:
                      switch (battleSkill.ReactionDamageType)
                      {
                        case DamageTypes.TotalDamage:
                          if (!received_skill.IsDamagedSkill() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                            continue;
                          break;
                        case DamageTypes.PhyDamage:
                          if (!received_skill.IsPhysicalAttack() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                            continue;
                          break;
                        case DamageTypes.MagDamage:
                          if (!received_skill.IsMagicalAttack() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                            continue;
                          break;
                        default:
                          continue;
                      }
                      if (battleSkill.UseCondition == null || battleSkill.UseCondition.type == 0 || battleSkill.UseCondition.unlock)
                      {
                        Unit self = defender;
                        Unit unit;
                        switch (battleSkill.Target)
                        {
                          case ESkillTarget.Self:
                          case ESkillTarget.SelfSide:
                            unit = defender;
                            break;
                          case ESkillTarget.EnemySide:
                          case ESkillTarget.UnitAll:
                          case ESkillTarget.NotSelf:
                            unit = attacker;
                            break;
                          case ESkillTarget.GridNoUnit:
                            return;
                          default:
                            DebugUtility.LogError("リアクションスキル\"" + battleSkill.Name + "\"に不相応なスキル効果対象が設定されている");
                            return;
                        }
                        if (this.CheckSkillCondition(self, battleSkill))
                        {
                          int effectRate = (int) battleSkill.EffectRate;
                          if (effectRate <= 0 || effectRate >= 100 || ((int) (this.GetRandom() % 100U) <= effectRate || is_forced) || GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_REACTION)
                          {
                            LogSkill.Reflection reflection = (LogSkill.Reflection) null;
                            if (battleSkill.EffectType == SkillEffectTypes.ReflectDamage)
                            {
                              reflection = new LogSkill.Reflection();
                              reflection.damage = damage;
                            }
                            List<Unit> targets = (List<Unit>) null;
                            BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
                            int x = unit.x;
                            int y = unit.y;
                            if (self.GetAttackRangeMax(battleSkill) > 0)
                            {
                              this.CreateSelectGridMap(self, self.x, self.y, battleSkill, ref this.mRangeMap, false);
                              targets = this.SearchTargetsInGridMap(self, battleSkill, this.mRangeMap);
                              if (targets.Contains(unit))
                              {
                                targets.Clear();
                                this.GetExecuteSkillLineTarget(self, x, y, battleSkill, ref targets, ref shot);
                              }
                              else
                                continue;
                            }
                            else
                            {
                              x = self.x;
                              y = self.y;
                              this.CreateScopeGridMap(self, self.x, self.y, x, y, battleSkill, ref this.mScopeMap, false);
                              targets = this.SearchTargetsInGridMap(self, battleSkill, this.mScopeMap);
                              if (!targets.Contains(unit))
                                continue;
                            }
                            if (targets != null && targets.Count != 0)
                            {
                              LogSkill log = !this.IsBattleFlag(EBattleFlag.PredictResult) ? this.Log<LogSkill>() : new LogSkill();
                              log.self = self;
                              log.skill = battleSkill;
                              log.pos.x = x;
                              log.pos.y = y;
                              log.reflect = reflection;
                              log.is_append = !battleSkill.IsCutin();
                              log.CauseOfReaction = attacker;
                              if (shot != null)
                              {
                                log.pos.x = shot.end.x;
                                log.pos.y = shot.end.y;
                                log.rad = (int) (shot.rad * 100.0);
                                log.height = (int) (shot.height * 100.0);
                              }
                              for (int index2 = 0; index2 < targets.Count; ++index2)
                                log.SetSkillTarget(defender, targets[index2]);
                              this.ExecuteSkill(timing, log, battleSkill);
                              if (results != null)
                                results.Add(log);
                              if ((int) battleSkill.SkillParam.count > 0)
                              {
                                defender.UpdateSkillUseCount(battleSkill, -1);
                                continue;
                              }
                              continue;
                            }
                            continue;
                          }
                          continue;
                        }
                        continue;
                      }
                      continue;
                  }
              }
          }
        }
      }
    }

    private void DeadSkill(Unit self, Unit target)
    {
    }

    public bool UseItem(Unit self, int gx, int gy, ItemData item)
    {
      this.DebugAssert(item != null, "item == null");
      if (this.CheckEnableSuspendSave() && !this.IsUnitAuto(self))
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.UseItem,
          uid = this.CurrentUnit.UnitData.UniqueID,
          x = self.x,
          y = self.y,
          dir = (int) self.Direction,
          item = item.ItemID,
          tx = gx,
          ty = gy
        });
      if (!this.UseSkill(self, gx, gy, item.Skill, false, 0, 0))
        return false;
      if (!this.IsMultiPlay || this.mMyPlayerIndex == self.OwnerPlayerIndex)
      {
        item.Used(1);
        if (!this.mRecord.used_items.ContainsKey((OString) item.ItemID))
          this.mRecord.used_items[(OString) item.ItemID] = (OInt) 1;
        else
          ++this.mRecord.used_items[(OString) item.ItemID];
      }
      return true;
    }

    public void ContinueStart(long btlid, int seed)
    {
      this.mBtlID = btlid;
      ++this.mContinueCount;
      this.mSeed = (uint) seed;
      this.mRand.Seed(this.mSeed);
      this.CurrentRand = this.mRand;
      this.mClockTime = (OInt) 0;
      for (int index = 0; index < this.Units.Count; ++index)
        this.Units[index].ChargeTime = (OInt) 0;
      for (int index = 0; index < this.mPlayer.Count; ++index)
      {
        Unit self = this.mPlayer[index];
        bool isDead = self.IsDead;
        self.NotifyContinue();
        Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
        self.x = duplicatePosition.x;
        self.y = duplicatePosition.y;
        if (isDead)
        {
          self.SetUnitFlag(EUnitFlag.Entried, true);
          if (!this.mStartingMembers.Contains(self))
          {
            self.ChargeTime = (OInt) 0;
            self.IsSub = true;
          }
          else
            this.Log<LogUnitEntry>().self = self;
        }
      }
      this.mRecord.result = BattleCore.QuestResult.Pending;
      this.SetBattleFlag(EBattleFlag.MapStart, true);
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      if (this.CheckEnableSuspendSave())
      {
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.Continued,
          seed = this.mSeed
        });
        this.IsSuspendSaveRequest = true;
      }
      this.NextOrder(true, false);
    }

    public void MapStart()
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(!this.IsBattleFlag(EBattleFlag.MapStart), "マップ未開始のみコール可");
      this.CurrentRand = this.mRand;
      this.mUnits.Clear();
      if (this.IsMultiVersus)
      {
        for (int index = 0; index < this.mAllUnits.Count; ++index)
          this.mUnits.Add(this.mAllUnits[index]);
      }
      else
      {
        for (int index = 0; index < this.mPlayer.Count; ++index)
          this.mUnits.Add(this.mPlayer[index]);
        for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
          this.mUnits.Add(this.mEnemys[this.MapIndex][index]);
      }
      this.mTreasures.Clear();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UnitType == EUnitType.Treasure && this.mUnits[index].EventTrigger != null && this.mUnits[index].EventTrigger.EventType == EEventType.Treasure)
          this.mTreasures.Add(this.mUnits[index]);
      }
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        this.mUnits[index].NotifyMapStart();
        this.mUnits[index].TowerStartHP = (int) this.mUnits[index].MaximumStatus.param.hp;
      }
      this.SetBattleFlag(EBattleFlag.MapStart, true);
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      this.mGridLines = new List<Grid>(this.CurrentMap.Width * this.CurrentMap.Height);
      this.mGridLines.Clear();
      this.CreateGimmickSkills();
      this.mSuspendIndex = 0;
      if (this.CheckEnableSuspendSave())
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.MapStart,
          seed = this.mSeed
        });
      this.NextOrder(true, false);
      DebugUtility.Log("rnd:" + (object) this.CloneRand().Get() + "rndDmg:" + (object) this.CloneRandDamage().Get());
    }

    private void UpdateCancelCastSkill()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.CastSkill != null && mUnit.GetSkillUsedCost(mUnit.CastSkill) > mUnit.Gems)
          mUnit.CancelCastSkill();
      }
    }

    private void UpdateUnitCondition(Unit self)
    {
      if (self == null)
        return;
      self.SetUnitFlag(EUnitFlag.Paralysed, false);
      if ((int) (this.mRand.Get() % 100U) >= self.GetParalyseRate())
        return;
      self.SetUnitFlag(EUnitFlag.Paralysed, true);
    }

    public void UnitStart()
    {
      if (this.IsSuspendStart && !this.IsFirstUnitStart && !this.IsBattleFlag(EBattleFlag.SuspendStart))
      {
        this.IsFirstUnitStart = true;
        this.Log<LogMapCommand>();
      }
      else
      {
        this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
        this.DebugAssert(this.IsBattleFlag(EBattleFlag.MapStart), "マップ開始済みのみコール可");
        this.DebugAssert(!this.IsBattleFlag(EBattleFlag.UnitStart), "ユニット未開始のみコール可");
        Unit currentUnit = this.CurrentUnit;
        this.IsAutoBattle = this.RequestAutoBattle;
        this.mSeedDamage = this.mRand.Get();
        this.CurrentRand = this.mRand;
        this.ClearAI();
        currentUnit.NotifyActionStart(this.Units);
        this.UpdateSkillUseCondition();
        this.UpdateUnitCondition(currentUnit);
        this.AutoHealSkill(currentUnit);
        this.ActuatedSneaking(currentUnit);
        this.UpdateMoveMap(currentUnit);
        this.UpdateSafeMap(currentUnit);
        this.UpdateHelperUnits(currentUnit);
        this.mKillstreak = 0;
        this.SetBattleFlag(EBattleFlag.UnitStart, true);
        this.SetBattleFlag(EBattleFlag.MapCommand, true);
        bool flag = this.CheckEnableSuspendSave();
        if (flag)
          this.mSuspendData.Add(new BattleCore.SuspendData()
          {
            timing = BattleCore.SuspendTiming.UnitStart,
            x = currentUnit.x,
            y = currentUnit.y,
            dir = (int) currentUnit.Direction,
            uid = currentUnit.UnitData.UniqueID,
            rnd = this.mSeedDamage
          });
        if (currentUnit.IsUnitCondition(EUnitCondition.DeathSentence) && currentUnit.DeathCount <= 0)
        {
          currentUnit.CurrentStatus.param.hp = (OInt) 0;
          this.Dead(currentUnit, currentUnit, DeadTypes.DeathSentence);
          this.InternalLogUnitEnd();
        }
        else
        {
          if (currentUnit.IsPartyMember)
            ++this.mActionCount;
          this.Log<LogMapCommand>();
          if (flag && currentUnit.IsPartyMember)
          {
            int suspendSaveInterval = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.SuspendSaveInterval;
            if (suspendSaveInterval == 0 || this.mActionCount % suspendSaveInterval == 0 || this.IsSuspendSaveRequest)
              this.SaveSuspendData();
          }
          this.DebugAssert(((int) currentUnit.ChargeTimeMax <= (int) currentUnit.ChargeTime ? 1 : 0) != 0, "CTが溜まってないのに行動が行われた。【現在】" + (object) currentUnit.ChargeTime + " /【最大】" + (object) currentUnit.ChargeTimeMax);
        }
      }
    }

    private void UpdateSkillUseCondition()
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsGimmick && this.Units[index].CheckExistMap())
        {
          if (this.Units[index].Side == EUnitSide.Player)
            ++num1;
          else if (this.Units[index].Side == EUnitSide.Enemy)
            ++num2;
        }
      }
      for (int index1 = 0; index1 < this.Units.Count; ++index1)
      {
        Unit unit = this.Units[index1];
        if (!unit.IsGimmick && unit.CheckExistMap())
        {
          for (int index2 = 0; index2 < unit.BattleSkills.Count; ++index2)
          {
            SkillLockCondition useCondition = unit.BattleSkills[index2].UseCondition;
            if (useCondition != null)
            {
              SkillLockTypes type = (SkillLockTypes) useCondition.type;
              switch (type)
              {
                case SkillLockTypes.PartyMemberUpper:
                case SkillLockTypes.PartyMemberLower:
                  useCondition.unlock = type != SkillLockTypes.PartyMemberUpper ? useCondition.value >= num1 : useCondition.value <= num1;
                  continue;
                case SkillLockTypes.EnemyMemberUpper:
                case SkillLockTypes.EnemyMemberLower:
                  useCondition.unlock = type != SkillLockTypes.EnemyMemberUpper ? useCondition.value >= num2 : useCondition.value <= num2;
                  continue;
                case SkillLockTypes.HpUpper:
                  useCondition.unlock = (int) unit.CurrentStatus.param.hp >= useCondition.value;
                  continue;
                case SkillLockTypes.HpLower:
                  useCondition.unlock = (int) unit.CurrentStatus.param.hp <= useCondition.value;
                  continue;
                case SkillLockTypes.OnGrid:
                  if (!useCondition.unlock && useCondition.x != null)
                  {
                    bool flag = false;
                    for (int index3 = 0; index3 < useCondition.x.Count; ++index3)
                    {
                      if (useCondition.x[index3] == unit.x && useCondition.y[index3] == unit.y)
                      {
                        flag = true;
                        break;
                      }
                    }
                    useCondition.unlock = flag;
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    private void ActuatedSneaking(Unit unit)
    {
      if (unit.AI == null || !unit.AI.CheckFlag(AIFlags.Sneaking))
        return;
      unit.SetUnitFlag(EUnitFlag.Sneaking, true);
      this.UpdateSearchMap(unit);
      if (!this.CheckEnemyIntercept(unit))
        return;
      unit.SetUnitFlag(EUnitFlag.Sneaking, false);
    }

    public void NotifyMapCommand()
    {
      Unit currentUnit = this.CurrentUnit;
      currentUnit.NotifyCommandStart();
      if (currentUnit.IsUnitFlag(EUnitFlag.Moved))
        return;
      this.UpdateMoveMap(currentUnit);
    }

    public bool ConditionalUnitEnd(bool ignoreMoveAndAction)
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.MapStart), "マップ開始済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.UnitStart), "ユニット開始済みのみコール可");
      if (this.CheckJudgeBattle())
      {
        this.CalcQuestRecord();
        this.MapEnd();
        return true;
      }
      Unit currentUnit = this.CurrentUnit;
      this.DebugAssert(currentUnit != null, "unit == null");
      if (currentUnit.IsDead)
      {
        this.InternalLogUnitEnd();
        return true;
      }
      bool flag1 = currentUnit.IsEnableMoveCondition(false);
      bool flag2 = currentUnit.IsEnableActionCondition();
      if (ignoreMoveAndAction || flag2 || flag1)
        return false;
      this.CommandWait();
      return true;
    }

    public void UnitEnd()
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.MapStart), "マップ開始済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.UnitStart), "ユニット開始済みのみコール可");
      this.DebugAssert(this.CurrentOrderData != null, "order == null");
      Unit currentUnit = this.CurrentUnit;
      this.DebugAssert(currentUnit != null, "self == null");
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      this.SetBattleFlag(EBattleFlag.MapCommand, false);
      if (this.CheckEnableSuspendSave())
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.UnitEnd,
          x = currentUnit.x,
          y = currentUnit.y,
          dir = (int) currentUnit.Direction
        });
      currentUnit.NotifyActionEnd();
      for (int index = 0; index < this.Units.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        BattleCore.\u003CUnitEnd\u003Ec__AnonStorey1B4 endCAnonStorey1B4 = new BattleCore.\u003CUnitEnd\u003Ec__AnonStorey1B4();
        // ISSUE: reference to a compiler-generated field
        endCAnonStorey1B4.unit = this.Units[index];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (endCAnonStorey1B4.unit.CastSkill != null && endCAnonStorey1B4.unit.UnitTarget != null && (!endCAnonStorey1B4.unit.IsDeadCondition() && currentUnit.CastSkillGridMap != null))
        {
          // ISSUE: reference to a compiler-generated method
          Unit unit = this.Units.Find(new Predicate<Unit>(endCAnonStorey1B4.\u003C\u003Em__153));
          if (unit != null)
          {
            GridMap<bool> castSkillGridMap = currentUnit.CastSkillGridMap;
            castSkillGridMap.fill(false);
            // ISSUE: reference to a compiler-generated field
            if (endCAnonStorey1B4.unit.CastSkill.IsAreaSkill())
            {
              // ISSUE: reference to a compiler-generated field
              this.CreateScopeGridMap(currentUnit, currentUnit.x, currentUnit.y, unit.x, unit.y, endCAnonStorey1B4.unit.CastSkill, ref castSkillGridMap, false);
            }
            else
              castSkillGridMap.set(unit.x, unit.y, true);
            currentUnit.CastSkillGridMap = castSkillGridMap;
          }
        }
      }
      if (!currentUnit.IsDeadCondition() && currentUnit.IsUnitCondition(EUnitCondition.Poison))
      {
        int poisonDamage = currentUnit.GetPoisonDamage();
        currentUnit.Damage(poisonDamage);
        if (currentUnit.Side == EUnitSide.Enemy && poisonDamage > 0)
          this.mTotalDamages += poisonDamage;
        if (currentUnit.Side == EUnitSide.Player && currentUnit.IsPartyMember && poisonDamage > 0)
          this.mTotalDamagesTaken += poisonDamage;
        LogDamage logDamage = this.Log<LogDamage>();
        logDamage.self = currentUnit;
        logDamage.damage = poisonDamage;
        if (currentUnit.IsDead)
        {
          if (this.CheckGuts(currentUnit))
          {
            currentUnit.Heal(1);
            currentUnit.UpdateBuffEffectTurnCount(EffectCheckTimings.GutsEnd, currentUnit);
            currentUnit.UpdateCondEffectTurnCount(EffectCheckTimings.GutsEnd, currentUnit);
          }
          else
            this.Dead(currentUnit, currentUnit, DeadTypes.Poison);
        }
      }
      if (this.mQuestParam.type == QuestTypes.Arena && currentUnit.Side == EUnitSide.Player)
      {
        int num = (int) this.ArenaSubActionCount(1U);
      }
      this.NextOrder(true, false);
    }

    public void CastSkillStart()
    {
      Unit currentUnit = this.CurrentUnit;
      if (this.CheckEnableSuspendSave())
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.CastSkillStart,
          uid = currentUnit.UnitData.UniqueID,
          x = currentUnit.x,
          y = currentUnit.y,
          dir = (int) currentUnit.Direction
        });
      SkillData castSkill = currentUnit.CastSkill;
      if (castSkill != null)
      {
        if (currentUnit.UnitTarget != null)
        {
          this.UseSkill(currentUnit, currentUnit.UnitTarget.x, currentUnit.UnitTarget.y, castSkill, false, 0, 0);
          return;
        }
        if (currentUnit.GridTarget != null)
        {
          this.UseSkill(currentUnit, currentUnit.GridTarget.x, currentUnit.GridTarget.y, castSkill, false, 0, 0);
          return;
        }
      }
      currentUnit.CancelCastSkill();
      this.Log<LogCastSkillEnd>();
    }

    public void CastSkillEnd()
    {
      if (this.CheckEnableSuspendSave())
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.CastSkillEnd,
          uid = this.CurrentUnit.UnitData.UniqueID,
          x = this.CurrentUnit.x,
          y = this.CurrentUnit.y,
          dir = (int) this.CurrentUnit.Direction
        });
      this.NextOrder(true, false);
    }

    private void MapEnd()
    {
      this.Log<LogMapEnd>();
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      this.SetBattleFlag(EBattleFlag.MapStart, false);
    }

    public void IncrementMap()
    {
      this.DebugAssert(!this.IsBattleFlag(EBattleFlag.MapStart), "マップ未開始のみコール可");
      ++this.mMapIndex;
    }

    public int CalcGridDistance(Grid start, Grid goal)
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      if (start == null || goal == null)
        return (int) byte.MaxValue;
      return Math.Abs(start.x - goal.x) + Math.Abs(start.y - goal.y);
    }

    public int CalcGridDistance(Unit self, Unit target)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      return Math.Abs(self.x - target.x) + Math.Abs(self.y - target.y);
    }

    private int FindNearGridAndDistance(Unit self, Unit target, out Grid self_grid, out Grid target_grid)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      int num1 = (int) byte.MaxValue;
      self_grid = (Grid) null;
      target_grid = (Grid) null;
      for (int index1 = 0; index1 < self.SizeX; ++index1)
      {
        for (int index2 = 0; index2 < self.SizeY; ++index2)
        {
          Grid start = currentMap[self.x + index1, self.y + index2];
          this.DebugAssert(start != null, "start == null");
          for (int index3 = 0; index3 < target.SizeX; ++index3)
          {
            for (int index4 = 0; index4 < target.SizeY; ++index4)
            {
              Grid goal = currentMap[target.x + index3, target.y + index4];
              this.DebugAssert(goal != null, "goal == null");
              int num2 = this.CalcGridDistance(start, goal);
              if (num2 < num1)
              {
                num1 = num2;
                self_grid = start;
                target_grid = goal;
              }
            }
          }
        }
      }
      return num1;
    }

    public int CalcNearGridDistance(Unit self, Unit target)
    {
      Grid self_grid;
      Grid target_grid;
      return this.FindNearGridAndDistance(self, target, out self_grid, out target_grid);
    }

    public int CalcNearGridDistance(Unit self, Grid target)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      int num1 = (int) byte.MaxValue;
      for (int index1 = 0; index1 < self.SizeX; ++index1)
      {
        for (int index2 = 0; index2 < self.SizeY; ++index2)
        {
          Grid start = currentMap[self.x + index1, self.y + index2];
          this.DebugAssert(start != null, "start == null");
          Grid goal = currentMap[target.x, target.y];
          this.DebugAssert(goal != null, "goal == null");
          int num2 = this.CalcGridDistance(start, goal);
          if (num2 < num1)
            num1 = num2;
        }
      }
      return num1;
    }

    public int GetUnitMaxAttackHeight(Unit self, SkillData skill)
    {
      return self.GetAttackHeight(skill);
    }

    private void UpdateHelperUnits(Unit self)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(this.CurrentMap != null, "map == null");
      this.mHelperUnits.Clear();
      if (!self.IsEnableAttackCondition(false) || !self.IsEnableHelpCondition() || this.IsMultiVersus)
        return;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (self != mUnit && self.Side == mUnit.Side && (!mUnit.IsDeadCondition() && mUnit.IsEnableHelpCondition()))
        {
          int combinationRange = self.GetCombinationRange();
          if (this.CalcGridDistance(self, mUnit) <= combinationRange && this.CheckCombination(self, mUnit))
            this.mHelperUnits.Add(mUnit);
        }
      }
    }

    private bool CheckCombination(Unit self, Unit other)
    {
      return GameUtility.IsDebugBuild && BattleCore.DEBUG_IS_FORCE_COMBINATION || (self.GetCombination() + other.GetCombination()) * 100 / 128 >= (int) (this.GetRandom() % 100U);
    }

    public bool CheckGridEventTrigger(Unit self, Grid grid, EEventTrigger trigger, bool is_exec_positive_only = false)
    {
      if (self == null || self.IsDead || grid == null)
        return false;
      Unit gimmickAtGrid = this.FindGimmickAtGrid(grid, false);
      if (gimmickAtGrid == null || self == gimmickAtGrid || !gimmickAtGrid.CheckEventTrigger(trigger))
        return false;
      switch (gimmickAtGrid.EventTrigger.EventType)
      {
        case EEventType.Treasure:
        case EEventType.Gem:
          if (self.Side != EUnitSide.Player)
            return false;
          break;
      }
      return !is_exec_positive_only || gimmickAtGrid.EventTrigger.IsAdvantage;
    }

    public bool CheckGridEventTrigger(Unit self, EEventTrigger trigger, bool is_exec_positive_only = false)
    {
      if (self == null || self.IsDead)
        return false;
      Grid unitGridPosition = this.GetUnitGridPosition(self);
      return this.CheckGridEventTrigger(self, unitGridPosition, trigger, is_exec_positive_only);
    }

    public bool ExecuteEventTriggerOnGrid(Unit self, EEventTrigger trigger, bool is_exec_positive_only = false)
    {
      if (!this.CheckGridEventTrigger(self, trigger, is_exec_positive_only))
        return false;
      Unit gimmickAtGrid = this.FindGimmickAtGrid(this.GetUnitGridPosition(self), false);
      return this.GridEventStart(self, gimmickAtGrid, trigger);
    }

    private bool GridEventStart(Unit self, Unit target, EEventTrigger type)
    {
      if (self == null || target == null || (!target.CheckEventTrigger(type) || this.IsBattleFlag(EBattleFlag.PredictResult)))
        return false;
      EventTrigger eventTrigger = target.EventTrigger;
      if (eventTrigger == null)
        return false;
      bool isDead = self.IsDead;
      switch (eventTrigger.Trigger)
      {
        case EEventTrigger.Dead:
          if (eventTrigger.EventType == EEventType.Win || eventTrigger.EventType == EEventType.Lose || !target.IsDead)
            return false;
          break;
        case EEventTrigger.HpDownBorder:
          if (eventTrigger.EventType == EEventType.Win || eventTrigger.EventType == EEventType.Lose || (int) target.MaximumStatus.param.hp * eventTrigger.IntValue / 100 < (int) target.CurrentStatus.param.hp)
            return false;
          break;
      }
      LogMapEvent log = this.Log<LogMapEvent>();
      log.self = self;
      log.target = target;
      log.type = eventTrigger.EventType;
      log.gimmick = eventTrigger.GimmickType;
      switch (eventTrigger.EventType)
      {
        case EEventType.Win:
          ++this.mWinTriggerCount;
          break;
        case EEventType.Lose:
          ++this.mLoseTriggerCount;
          break;
        case EEventType.Gem:
          this.AddGems(self, (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue);
          eventTrigger.ExecuteGimmickEffect(target, self, log);
          break;
      }
      target.DecrementTriggerCount();
      if (eventTrigger.Trigger == EEventTrigger.ExecuteOnGrid)
      {
        self.SetUnitFlag(EUnitFlag.Action, true);
        self.SetCommandFlag(EUnitCommandFlag.Action, true);
      }
      if (!isDead && self.IsDead)
        this.Dead((Unit) null, self, DeadTypes.Damage);
      return true;
    }

    private bool CheckBackAttack(Unit self, Unit target, SkillData skill)
    {
      return this.CheckBackAttack(self.x, self.y, target, skill);
    }

    private bool CheckBackAttack(int sx, int sy, Unit target, SkillData skill)
    {
      int index = 0;
      int direction = (int) target.Direction;
      int num1 = target.x - sx;
      int num2 = target.y - sy;
      if (num1 > 0)
        index = 0;
      if (num1 < 0)
        index = 2;
      if (num2 > 0)
        index = 1;
      if (num2 < 0)
        index = 3;
      return Math.Abs(Unit.DIRECTION_OFFSETS[index, 0] + Unit.DIRECTION_OFFSETS[direction, 0]) == 2 || Math.Abs(Unit.DIRECTION_OFFSETS[index, 1] + Unit.DIRECTION_OFFSETS[direction, 1]) == 2;
    }

    private bool CheckSideAttack(Unit self, Unit target, SkillData skill)
    {
      return this.CheckSideAttack(self.x, self.y, target, skill);
    }

    private bool CheckSideAttack(int sx, int sy, Unit target, SkillData skill)
    {
      int index = 0;
      int direction = (int) target.Direction;
      int num1 = target.x - sx;
      int num2 = target.y - sy;
      if (num1 > 0)
        index = 0;
      if (num1 < 0)
        index = 2;
      if (num2 > 0)
        index = 1;
      if (num2 < 0)
        index = 3;
      return Math.Abs(Unit.DIRECTION_OFFSETS[index, 0] + Unit.DIRECTION_OFFSETS[direction, 0]) == 1 && Math.Abs(Unit.DIRECTION_OFFSETS[index, 1] + Unit.DIRECTION_OFFSETS[direction, 1]) == 1;
    }

    public bool CheckDisableAbilities(Unit self)
    {
      if (self.Side != EUnitSide.Player)
        return false;
      return this.mQuestParam.CheckDisableAbilities();
    }

    public bool CheckDisableItems(Unit self)
    {
      if (self.Side != EUnitSide.Player)
        return false;
      return this.mQuestParam.CheckDisableItems();
    }

    public Grid GetCorrectDuplicatePosition(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      Grid start = currentMap[self.x, self.y];
      for (int index1 = 0; index1 < this.Units.Count; ++index1)
      {
        Unit unit = this.Units[index1];
        if (unit != self && unit.CheckCollision(start.x, start.y))
        {
          int num1 = Math.Max(currentMap.Width, currentMap.Height);
          int num2 = 999;
          Grid grid1 = (Grid) null;
          for (int index2 = -num1; index2 <= num1; ++index2)
          {
            for (int index3 = -num1; index3 <= num1; ++index3)
            {
              if (Math.Abs(index3) + Math.Abs(index2) <= num1)
              {
                int index4 = self.x + index3;
                int index5 = self.y + index2;
                Grid grid2 = currentMap[index4, index5];
                if (currentMap.CheckEnableMove(self, grid2, false))
                {
                  bool flag = true;
                  for (int index6 = 0; index6 < this.mUnits.Count; ++index6)
                  {
                    if (this.mUnits[index6].UnitType == EUnitType.Unit && this.mUnits[index6] != self && (!this.mUnits[index6].IsSub && !this.mUnits[index6].IsDead) && (this.mUnits[index6].IsEntry && this.mUnits[index6].CheckCollision(grid2)))
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag)
                  {
                    int num3 = this.CalcGridDistance(start, grid2);
                    if (num3 < num2)
                    {
                      num2 = num3;
                      grid1 = grid2;
                    }
                  }
                }
              }
            }
          }
          DebugUtility.Assert(grid1 != null, "空きグリッドが見つからなかった");
          return grid1;
        }
      }
      return start;
    }

    public bool EntryBattleMultiPlayTimeUp { get; set; }

    public bool MultiPlayDisconnectAutoBattle { get; set; }

    public bool EntryBattleMultiPlay(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
    {
      return this.ExecMultiPlayerCommandCore(type, current, enemy, skill, item, gx, gy, unitLockTarget);
    }

    public bool CheckSkillScopeMultiPlay(Unit self, Unit target, int gx, int gy, SkillData skill)
    {
      return true;
    }

    private bool ExecMultiPlayerCommandCore(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
    {
      switch (type)
      {
        case EBattleCommand.Attack:
          if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, current.GetAttackSkill()) && this.UseSkill(current, gx, gy, current.GetAttackSkill(), unitLockTarget, 0, 0))
            return true;
          break;
        case EBattleCommand.Skill:
          if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, skill))
          {
            if (skill.EffectType != SkillEffectTypes.Throw ? this.UseSkill(current, gx, gy, skill, unitLockTarget, 0, 0) : this.UseSkill(current, gx, gy, skill, unitLockTarget, enemy.x, enemy.y))
              return true;
            break;
          }
          break;
      }
      return false;
    }

    public static string SuspendFileName
    {
      get
      {
        return Application.get_persistentDataPath() + "/" + BattleCore.SUSPENDDATA_FILENAME;
      }
    }

    public static void RemoveSuspendData()
    {
      string suspendFileName = BattleCore.SuspendFileName;
      if (!System.IO.File.Exists(suspendFileName))
        return;
      System.IO.File.Delete(suspendFileName);
    }

    public bool CheckEnableSuspendSave()
    {
      return !this.IsMultiPlay && this.mQuestParam.CheckEnableSuspendStart() && !this.IsBattleFlag(EBattleFlag.SuspendStart);
    }

    public bool SaveSuspendData()
    {
      bool flag = false;
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) System.IO.File.Open(BattleCore.SuspendFileName, this.mSuspendIndex != 0 ? FileMode.Append : FileMode.Create)))
      {
        if (binaryWriter.BaseStream.CanWrite)
        {
          if (this.mSuspendIndex == 0)
          {
            binaryWriter.Write(Application.get_version());
            binaryWriter.Write(AssetManager.AssetRevision);
            binaryWriter.Write(this.mQuestParam.iname);
            binaryWriter.Write(this.mBtlID);
            binaryWriter.Write(GameUtility.Config_AutoMode_Treasure.Value);
            binaryWriter.Write(GameUtility.Config_AutoMode_DisableSkill.Value);
          }
          for (int mSuspendIndex = this.mSuspendIndex; mSuspendIndex < this.mSuspendData.Count; ++mSuspendIndex)
          {
            BattleCore.SuspendData suspendData = this.mSuspendData[mSuspendIndex];
            binaryWriter.Write((int) suspendData.timing);
            binaryWriter.Write(suspendData.seed);
            binaryWriter.Write(suspendData.rnd);
            binaryWriter.Write(suspendData.uid);
            binaryWriter.Write(suspendData.x);
            binaryWriter.Write(suspendData.y);
            binaryWriter.Write(suspendData.dir);
            binaryWriter.Write(suspendData.skill);
            binaryWriter.Write(suspendData.item);
            binaryWriter.Write(suspendData.tx);
            binaryWriter.Write(suspendData.ty);
            binaryWriter.Write(suspendData.locked);
            binaryWriter.Write(suspendData.ux);
            binaryWriter.Write(suspendData.uy);
          }
          flag = true;
        }
        binaryWriter.Close();
        if (!flag)
          BattleCore.RemoveSuspendData();
      }
      this.mSuspendIndex = this.mSuspendData.Count;
      this.IsSuspendSaveRequest = false;
      return flag;
    }

    public bool LoadSuspendData()
    {
      bool flag = false;
      string suspendFileName = BattleCore.SuspendFileName;
      this.mSuspendData.Clear();
      if (System.IO.File.Exists(suspendFileName))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) System.IO.File.Open(suspendFileName, FileMode.Open)))
        {
          if (binaryReader.BaseStream.CanRead && binaryReader.BaseStream.Length > 0L)
          {
            if (binaryReader.ReadString() != Application.get_version())
              DebugUtility.LogWarning("Restoration failure. Version is different.");
            else if (binaryReader.ReadInt32() != AssetManager.AssetRevision)
              DebugUtility.LogWarning("Restoration failure. Revision is different.");
            else if (binaryReader.ReadString() != this.mQuestParam.iname)
              DebugUtility.LogWarning("Restoration failure. QuestID is different.");
            else if (binaryReader.ReadInt64() != this.mBtlID)
            {
              DebugUtility.LogWarning("Restoration failure. BattleID is different.");
            }
            else
            {
              GameUtility.Config_AutoMode_Treasure.Value = binaryReader.ReadBoolean();
              GameUtility.Config_AutoMode_DisableSkill.Value = binaryReader.ReadBoolean();
              while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                this.mSuspendData.Add(new BattleCore.SuspendData()
                {
                  timing = (BattleCore.SuspendTiming) binaryReader.ReadInt32(),
                  seed = binaryReader.ReadUInt32(),
                  rnd = binaryReader.ReadUInt32(),
                  uid = binaryReader.ReadInt64(),
                  x = binaryReader.ReadInt32(),
                  y = binaryReader.ReadInt32(),
                  dir = binaryReader.ReadInt32(),
                  skill = binaryReader.ReadString(),
                  item = binaryReader.ReadString(),
                  tx = binaryReader.ReadInt32(),
                  ty = binaryReader.ReadInt32(),
                  locked = binaryReader.ReadInt32(),
                  ux = binaryReader.ReadInt32(),
                  uy = binaryReader.ReadInt32()
                });
              if (this.mSuspendData[this.mSuspendData.Count - 1].timing == BattleCore.SuspendTiming.UnitStart)
                flag = true;
            }
          }
          binaryReader.Close();
          if (!flag)
            BattleCore.RemoveSuspendData();
        }
      }
      return flag;
    }

    public bool CheckEnableSuspendStart()
    {
      if ((bool) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsDisableSuspend || this.IsMultiPlay || !this.mQuestParam.CheckEnableSuspendStart())
        return false;
      return System.IO.File.Exists(BattleCore.SuspendFileName);
    }

    public bool SuspendStart()
    {
      if (this.mSuspendErrorCtr >= 3)
        return false;
      this.mSuspendLogLists.Clear();
      this.mSuspendMsgLists.Clear();
      this.SetBattleFlag(EBattleFlag.SuspendStart, true);
      this.IsAutoBattle = false;
      this.IsSuspendStart = true;
      this.IsFirstUnitStart = false;
      this.IsSuspendSaveRequest = false;
      for (int index = 0; index < this.mSuspendData.Count; ++index)
      {
        try
        {
          BattleCore.SuspendData suspendData = this.mSuspendData[index];
          switch (suspendData.timing)
          {
            case BattleCore.SuspendTiming.MapStart:
              this.mSeed = suspendData.seed;
              this.mRand.Seed(this.mSeed);
              this.MapStart();
              continue;
            case BattleCore.SuspendTiming.UnitStart:
              this.UnitStart();
              if ((int) suspendData.rnd != (int) this.mSeedDamage)
              {
                DebugUtility.LogError("SuspendStart random value failed.");
                this.mSuspendMsgLists.Add(string.Format("SuspendStart random value failed. c={0} seed={1},{2}", (object) index, (object) suspendData.rnd, (object) this.mSeedDamage));
              }
              if (suspendData.uid == this.CurrentUnit.UnitData.UniqueID && suspendData.x == this.CurrentUnit.x && suspendData.y == this.CurrentUnit.y)
              {
                if ((EUnitDirection) suspendData.dir == this.CurrentUnit.Direction)
                  continue;
              }
              DebugUtility.LogError("SuspendStart unit different.");
              UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(suspendData.uid);
              string iname = suspendData.uid.ToString();
              if (unitDataByUniqueId != null)
                iname = unitDataByUniqueId.UnitParam.iname;
              this.mSuspendMsgLists.Add(string.Format("SuspendStart unit different. c={0} unit={1}/{2}", (object) index, (object) iname, (object) this.CurrentUnit.UnitParam.iname));
              continue;
            case BattleCore.SuspendTiming.Wait:
              this.CommandWait((EUnitDirection) suspendData.dir);
              continue;
            case BattleCore.SuspendTiming.Move:
              this.Move(this.CurrentUnit, this.CurrentMap[suspendData.x, suspendData.y], (EUnitDirection) suspendData.dir, true, false);
              continue;
            case BattleCore.SuspendTiming.UseSkill:
              this.mSuspendLogLists.Add(new BattleCore.SuspendLog(index, suspendData.skill, this.CurrentUnit.UnitParam.iname));
              SkillData skillData = this.CurrentUnit.GetSkillData(suspendData.skill);
              this.UseSkill(this.CurrentUnit, suspendData.tx, suspendData.ty, skillData, suspendData.locked != 0, suspendData.ux, suspendData.uy);
              continue;
            case BattleCore.SuspendTiming.UseItem:
              ItemData inventoryByItemId = this.FindInventoryByItemID(suspendData.item);
              this.UseItem(this.CurrentUnit, suspendData.tx, suspendData.ty, inventoryByItemId);
              continue;
            case BattleCore.SuspendTiming.UnitEnd:
              this.UnitEnd();
              continue;
            case BattleCore.SuspendTiming.CastSkillStart:
              this.mSuspendUseSkillID = (string) null;
              this.mSuspendUseSkillUnitID = (string) null;
              this.CastSkillStart();
              if (!string.IsNullOrEmpty(this.mSuspendUseSkillID))
              {
                this.mSuspendLogLists.Add(new BattleCore.SuspendLog(index, this.mSuspendUseSkillID, this.mSuspendUseSkillUnitID));
                this.mSuspendUseSkillID = (string) null;
                continue;
              }
              continue;
            case BattleCore.SuspendTiming.CastSkillEnd:
              this.CastSkillEnd();
              continue;
            case BattleCore.SuspendTiming.Continued:
              this.ContinueStart(this.mBtlID, (int) suspendData.seed);
              continue;
            case BattleCore.SuspendTiming.AI:
              this.mSuspendUseSkillID = (string) null;
              this.mSuspendUseSkillUnitID = (string) null;
              this.IsAutoBattle = true;
              this.UpdateMapAI(false);
              this.IsAutoBattle = false;
              if (!string.IsNullOrEmpty(this.mSuspendUseSkillID))
              {
                this.mSuspendLogLists.Add(new BattleCore.SuspendLog(index, this.mSuspendUseSkillID, this.mSuspendUseSkillUnitID));
                this.mSuspendUseSkillID = (string) null;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        catch (Exception ex)
        {
          if (this.mSuspendErrorCtr == 0)
          {
            Debug.Log((object) this.makeSuspendErrorLog(index));
            using (List<string>.Enumerator enumerator = this.mSuspendMsgLists.GetEnumerator())
            {
              while (enumerator.MoveNext())
                Debug.Log((object) enumerator.Current);
            }
          }
          ++this.mSuspendErrorCtr;
          throw new Exception("[Rethrow]", ex);
        }
      }
      this.SetBattleFlag(EBattleFlag.SuspendStart, false);
      this.mSuspendIndex = this.mSuspendData.Count;
      this.Log<LogUnitStart>();
      return true;
    }

    private string makeSuspendErrorLog(int index)
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      stringBuilder.AppendFormat("[SuspendLog3] e={0} ", (object) index);
      int num1 = index;
      int num2 = num1 - 32;
      if (num2 < 0)
        num2 = 0;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CmakeSuspendErrorLog\u003Ec__AnonStorey1B5 logCAnonStorey1B5 = new BattleCore.\u003CmakeSuspendErrorLog\u003Ec__AnonStorey1B5();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (logCAnonStorey1B5.idx = num2; logCAnonStorey1B5.idx <= num1; ++logCAnonStorey1B5.idx)
      {
        // ISSUE: reference to a compiler-generated field
        BattleCore.SuspendData suspendData = this.mSuspendData[logCAnonStorey1B5.idx];
        // ISSUE: reference to a compiler-generated field
        stringBuilder.AppendFormat("c={0},t={1}", (object) logCAnonStorey1B5.idx, (object) suspendData.timing);
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(suspendData.uid);
        string iname = suspendData.uid.ToString();
        if (unitDataByUniqueId != null)
          iname = unitDataByUniqueId.UnitParam.iname;
        switch (suspendData.timing)
        {
          case BattleCore.SuspendTiming.UseSkill:
            // ISSUE: reference to a compiler-generated method
            BattleCore.SuspendLog suspendLog1 = this.mSuspendLogLists.Find(new Predicate<BattleCore.SuspendLog>(logCAnonStorey1B5.\u003C\u003Em__154));
            if (suspendLog1 != null && !string.IsNullOrEmpty(suspendLog1.mUnitID))
            {
              stringBuilder.AppendFormat(",u={0}/{1},s={2}", (object) iname, (object) suspendLog1.mUnitID, (object) suspendData.skill);
              break;
            }
            stringBuilder.AppendFormat(",u={0},s={1}", (object) iname, (object) suspendData.skill);
            break;
          case BattleCore.SuspendTiming.UseItem:
            stringBuilder.AppendFormat(",u={0},i={1}", (object) iname, (object) suspendData.item);
            break;
          case BattleCore.SuspendTiming.CastSkillStart:
          case BattleCore.SuspendTiming.AI:
            // ISSUE: reference to a compiler-generated method
            BattleCore.SuspendLog suspendLog2 = this.mSuspendLogLists.Find(new Predicate<BattleCore.SuspendLog>(logCAnonStorey1B5.\u003C\u003Em__155));
            if (suspendLog2 != null)
            {
              stringBuilder.AppendFormat(",u={0},s={1}", !string.IsNullOrEmpty(suspendLog2.mUnitID) ? (object) suspendLog2.mUnitID : (object) iname, (object) suspendLog2.mSkillID);
              break;
            }
            break;
          default:
            if (iname != "0")
            {
              stringBuilder.AppendFormat(",u={0}", (object) iname);
              break;
            }
            break;
        }
        stringBuilder.AppendFormat(" ");
      }
      return stringBuilder.ToString();
    }

    public void SetBattleID(long btlid)
    {
      this.mBtlID = btlid;
    }

    public bool IsArenaSkip
    {
      get
      {
        return this.mIsArenaSkip;
      }
      set
      {
        this.mIsArenaSkip = value;
      }
    }

    public uint ArenaActionCount
    {
      get
      {
        return this.mArenaActionCount;
      }
    }

    private uint ArenaSubActionCount(uint count = 1)
    {
      if (this.mArenaActionCount >= count)
        this.mArenaActionCount -= count;
      else
        this.mArenaActionCount = 0U;
      return this.mArenaActionCount;
    }

    public void ArenaKeepQuestData(string quest_id, BattleCore.Json_Battle json_btl)
    {
      this.mArenaQuestID = quest_id;
      this.mArenaQuestJsonBtl = json_btl;
      if (json_btl.maxActionNum <= 0)
        return;
      this.mArenaActionCountMax = (uint) json_btl.maxActionNum;
    }

    public bool ArenaResetQuestData()
    {
      this.mArenaActionCount = this.mArenaActionCountMax;
      if (string.IsNullOrEmpty(this.mArenaQuestID) || this.mArenaQuestJsonBtl == null)
        return false;
      this.mMap.Clear();
      this.mPlayer.Clear();
      this.mAllUnits.Clear();
      this.mStartingMembers.Clear();
      this.Deserialize(this.mArenaQuestID, this.mArenaQuestJsonBtl, 0, (UnitData[]) null, (int[]) null, true, (int[]) null);
      return true;
    }

    public bool IsArenaCalc
    {
      get
      {
        return this.mIsArenaCalc;
      }
    }

    public BattleCore.QuestResult ArenaCalcResult
    {
      get
      {
        return this.mArenaCalcResult;
      }
    }

    public void ArenaCalcStart()
    {
      this.mArenaActionCount = this.mArenaActionCountMax;
      this.mIsArenaCalc = true;
      this.mArenaCalcResult = BattleCore.QuestResult.Pending;
      this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.MAP_START;
    }

    public void ArenaCalcFinish()
    {
      this.mIsArenaCalc = false;
    }

    private void judgeStartTypeArenaCalc()
    {
      if (this.CurrentOrderData.IsCastSkill)
        this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.CAST_SKILL_START;
      else
        this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_START;
    }

    public bool ArenaCalcStep()
    {
      int num = 0;
      do
      {
        switch (this.mArenaCalcTypeNext)
        {
          case BattleCore.eArenaCalcType.MAP_START:
            this.MapStart();
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_START;
            break;
          case BattleCore.eArenaCalcType.UNIT_START:
            this.UnitStart();
            Unit currentUnit = this.CurrentUnit;
            if (currentUnit != null && currentUnit.CastSkill != null && currentUnit.CastSkill.CastType == ECastTypes.Jump)
            {
              this.CommandWait();
              this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_END;
              break;
            }
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.AI;
            break;
          case BattleCore.eArenaCalcType.AI:
            this.IsAutoBattle = true;
            bool flag = this.UpdateMapAI(false);
            this.IsAutoBattle = false;
            if (!flag)
            {
              this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_END;
              break;
            }
            break;
          case BattleCore.eArenaCalcType.UNIT_END:
            this.UnitEnd();
            this.judgeStartTypeArenaCalc();
            break;
          case BattleCore.eArenaCalcType.CAST_SKILL_START:
            this.CastSkillStart();
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.CAST_SKILL_END;
            break;
          case BattleCore.eArenaCalcType.CAST_SKILL_END:
            this.CastSkillEnd();
            this.judgeStartTypeArenaCalc();
            break;
          case BattleCore.eArenaCalcType.MAP_END:
            if (this.IsBattleFlag(EBattleFlag.UnitStart))
              this.UnitEnd();
            if (this.IsBattleFlag(EBattleFlag.MapStart))
              this.MapEnd();
            this.mArenaCalcResult = this.GetQuestResult();
            return true;
          default:
            DebugUtility.Log(string.Format("BattleCore/ArenaCalcStep Type unknown! type=", (object) this.mArenaCalcTypeNext.ToString()));
            this.mArenaCalcTypeNext = !this.IsBattleFlag(EBattleFlag.MapStart) ? BattleCore.eArenaCalcType.MAP_START : BattleCore.eArenaCalcType.MAP_END;
            break;
        }
        if (!this.IsBattleFlag(EBattleFlag.MapStart) || this.GetQuestResult() != BattleCore.QuestResult.Pending)
          this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.MAP_END;
      }
      while (++num < 64);
      return false;
    }

    public uint VersusTurnMax { get; set; }

    public uint VersusTurnCount { get; set; }

    public uint RemainVersusTurnCount
    {
      get
      {
        return this.VersusTurnMax - this.VersusTurnCount;
      }
      set
      {
        this.VersusTurnCount = value;
      }
    }

    public Unit GetUnitUseCollaboSkill(Unit unit, SkillData skill)
    {
      if (unit == null)
        return (Unit) null;
      return unit.GetUnitUseCollaboSkill(skill, false);
    }

    public bool IsUseSkillCollabo(Unit unit, SkillData skill)
    {
      if (unit == null)
        return false;
      return unit.IsUseSkillCollabo(skill, false);
    }

    public int GetDeadCountEnemy()
    {
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (this.Units[index].Side == EUnitSide.Enemy && this.Units[index].IsDead)
          ++num;
      }
      return num;
    }

    private bool isKnockBack(SkillData skill)
    {
      if (skill == null || (int) skill.SkillParam.KnockBackVal == 0)
        return false;
      return (int) skill.SkillParam.KnockBackRate != 0;
    }

    private bool checkKnockBack(Unit self, Unit target, SkillData skill)
    {
      if (!this.isKnockBack(skill) || target.IsDisableUnitCondition(EUnitCondition.DisableKnockback))
        return false;
      EnchantParam enchantAssist = self.CurrentStatus.enchant_assist;
      EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
      int num = (int) skill.SkillParam.KnockBackRate + (int) enchantAssist[EnchantTypes.Knockback] - (int) enchantResist[EnchantTypes.Knockback];
      return num > 0 && (num >= 100 || (int) (this.GetRandom() % 100U) < num);
    }

    private Grid getGridKnockBack(Unit unit_att, Unit unit_def, SkillData skill)
    {
      if (!this.isKnockBack(skill))
        return (Grid) null;
      int knockBackHeight = (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.KnockBackHeight;
      int index1 = (int) BattleCore.UnitDirectionFromVector((Unit) null, unit_def.x - unit_att.x, unit_def.y - unit_att.y);
      int x = unit_def.x;
      int y = unit_def.y;
      Grid grid = (Grid) null;
      Grid current1 = this.CurrentMap[x, y];
      if (current1 != null)
      {
        for (int index2 = 0; index2 < (int) skill.SkillParam.KnockBackVal; ++index2)
        {
          x += Unit.DIRECTION_OFFSETS[index1, 0];
          y += Unit.DIRECTION_OFFSETS[index1, 1];
          Grid current2 = this.CurrentMap[x, y];
          if (current2 != null && Math.Abs(current1.height - current2.height) <= knockBackHeight && this.CurrentMap.CheckEnableMove(unit_def, current2, false))
            grid = current2;
          else
            break;
        }
      }
      return grid;
    }

    public GridMap<int> MoveMap
    {
      get
      {
        return this.mMoveMap;
      }
    }

    public GridMap<bool> RangeMap
    {
      get
      {
        return this.mRangeMap;
      }
    }

    public GridMap<bool> ScopeMap
    {
      get
      {
        return this.mScopeMap;
      }
    }

    public GridMap<bool> SearchMap
    {
      get
      {
        return this.mSearchMap;
      }
    }

    private void ClearAI()
    {
      BattleMap currentMap = this.CurrentMap;
      this.mUseSkillLists.Clear();
      this.mForceSkillList.Clear();
      if (this.mEnemyPriorities == null)
        this.mEnemyPriorities = new List<Unit>(this.mUnits.Count);
      this.mEnemyPriorities.Clear();
      int width = currentMap.Width;
      int height = currentMap.Height;
      if (this.mMoveMap == null)
        this.mMoveMap = new GridMap<int>(width, height);
      if (this.mMoveMap.w != width || this.mMoveMap.h != height)
        this.mMoveMap.resize(width, height);
      this.mMoveMap.fill(-1);
      if (this.mRangeMap == null)
        this.mRangeMap = new GridMap<bool>(width, height);
      if (this.mRangeMap.w != width || this.mRangeMap.h != height)
        this.mRangeMap.resize(width, height);
      this.mRangeMap.fill(false);
      if (this.mScopeMap == null)
        this.mScopeMap = new GridMap<bool>(width, height);
      if (this.mScopeMap.w != width || this.mScopeMap.h != height)
        this.mScopeMap.resize(width, height);
      this.mScopeMap.fill(false);
      if (this.mSearchMap == null)
        this.mSearchMap = new GridMap<bool>(width, height);
      if (this.mSearchMap.w != width || this.mSearchMap.h != height)
        this.mSearchMap.resize(width, height);
      this.mSearchMap.fill(false);
      if (this.mSafeMap == null)
        this.mSafeMap = new GridMap<int>(width, height);
      if (this.mSafeMap.w != width || this.mSafeMap.h != height)
        this.mSafeMap.resize(width, height);
      this.mSafeMap.fill(-1);
      this.UpdateTreasureTargetAI();
    }

    public bool UpdateMapAI(bool forceAI)
    {
      Unit currentUnit = this.CurrentUnit;
      DebugUtility.Assert(currentUnit != null, "self == null");
      if (this.CheckEnableSuspendSave())
        this.mSuspendData.Add(new BattleCore.SuspendData()
        {
          timing = BattleCore.SuspendTiming.AI
        });
      if (currentUnit.AI != null)
      {
        if (currentUnit.AI.CheckFlag(AIFlags.DisableMove))
          currentUnit.SetUnitFlag(EUnitFlag.Moved, true);
        if (currentUnit.AI.CheckFlag(AIFlags.DisableAction))
          currentUnit.SetUnitFlag(EUnitFlag.Action, true);
      }
      if (currentUnit.IsUnitFlag(EUnitFlag.DamagedActionStart))
      {
        this.CommandWait();
        return false;
      }
      if (currentUnit.IsAIPatrolTable() && this.CalcMoveTargetAI(currentUnit, forceAI))
        return true;
      AIAction currentAiAction = currentUnit.GetCurrentAIAction();
      if (currentAiAction != null && currentUnit.IsEnableActionCondition() && (string.IsNullOrEmpty((string) currentAiAction.skill) && (int) currentAiAction.type == 0))
      {
        this.CommandWait();
        currentUnit.NextAIAction();
        return false;
      }
      if (!this.CalcSearchingAI(currentUnit))
        return false;
      this.GetEnemyPriorities(currentUnit, this.mEnemyPriorities);
      if (this.CheckEscapeAI(currentUnit))
        currentUnit.SetUnitFlag(EUnitFlag.Escaped, true);
      if (this.CalcUseSkillAI(currentUnit, forceAI) || this.CalcMoveTargetAI(currentUnit, forceAI))
        return true;
      this.CommandWait();
      return false;
    }

    public GridMap<int> CreateMoveMap(Unit self, int movcount = 0)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<int> movmap = new GridMap<int>(currentMap.Width, currentMap.Height);
      if (0 < movcount)
        this.UpdateMoveMap(self, movmap, movcount);
      else
        this.UpdateMoveMap(self, movmap);
      return movmap;
    }

    private void UpdateMoveMap(Unit self)
    {
      this.UpdateMoveMap(self, this.mMoveMap);
    }

    private void UpdateMoveMap(Unit self, GridMap<int> movmap)
    {
      int movcount = self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false);
      this.UpdateMoveMap(self, movmap, movcount);
    }

    private void UpdateMoveMap(Unit self, GridMap<int> movmap, int movcount)
    {
      movmap.fill(-1);
      BattleMap currentMap = this.CurrentMap;
      Grid target = currentMap[self.x, self.y];
      currentMap.CalcMoveSteps(self, target);
      int num = movcount;
      for (int index1 = -num; index1 <= num; ++index1)
      {
        for (int index2 = -num; index2 <= num; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= num)
          {
            Grid grid = currentMap[self.x + index2, self.y + index1];
            if (grid != null && (int) grid.step != (int) byte.MaxValue)
            {
              int src = Math.Max((int) grid.step - (int) target.step, 0);
              if (src <= num)
                movmap.set(grid.x, grid.y, src);
            }
          }
        }
      }
    }

    private void UpdateSafeMap(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      int num = self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false);
      this.mSafeMap.fill(-1);
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsGimmick && !mUnit.IsDeadCondition() && (!mUnit.IsSub && mUnit.IsEntry) && this.CheckEnemySide(self, mUnit))
        {
          Grid self_grid;
          Grid target_grid;
          this.FindNearGridAndDistance(self, mUnit, out self_grid, out target_grid);
          if (currentMap.CalcMoveSteps(self, target_grid))
          {
            for (int index2 = -num; index2 <= num; ++index2)
            {
              for (int index3 = -num; index3 <= num; ++index3)
              {
                int x = self.x + index3;
                int y = self.y + index2;
                Grid grid = currentMap[x, y];
                if (grid != null && (int) grid.step != (int) byte.MaxValue)
                {
                  int src = this.mSafeMap.get(x, y) + (int) grid.step;
                  this.mSafeMap.set(x, y, src);
                }
              }
            }
          }
        }
      }
    }

    private int GetCurrentEnemyNum(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && mUnit.IsEntry && (!mUnit.IsGimmick && !mUnit.IsSub) && (mUnit != self && this.CheckEnemySide(self, mUnit)))
          ++num;
      }
      return num;
    }

    private List<Grid> GetEnableMoveGridList(Unit self, bool is_move = true, bool is_friendlyfire = true, bool is_sneaked = false, bool is_treasure = false)
    {
      BattleMap currentMap = this.CurrentMap;
      Grid grid1 = currentMap[self.x, self.y];
      List<Grid> gridList = new List<Grid>(this.mMoveMap.w * this.mMoveMap.h);
      int num1 = self.GetMoveCount(false);
      if (self.IsUnitFlag(EUnitFlag.Moved) || !self.IsEnableMoveCondition(false))
        num1 = 0;
      if (is_move && num1 > 0)
      {
        int num2 = (int) byte.MaxValue;
        if (self.TreasureGainTarget != null && is_treasure)
        {
          Grid grid2 = currentMap[self.x, self.y];
          currentMap.CalcMoveSteps(self, self.TreasureGainTarget);
          num2 = (int) grid2.step / num1 + ((int) grid2.step % num1 == 0 ? 0 : 1);
        }
        for (int x = 0; x < this.mMoveMap.w; ++x)
        {
          for (int y = 0; y < this.mMoveMap.h; ++y)
          {
            if (this.mMoveMap.get(x, y) >= 0)
            {
              Grid grid2 = currentMap[x, y];
              if (!is_friendlyfire || !this.CheckFriendlyFireOnGridMap(self, grid2))
              {
                if (self.TreasureGainTarget != null && is_treasure)
                {
                  if ((int) grid2.step / num1 + ((int) grid2.step % num1 == 0 ? 0 : 1) > num2)
                    continue;
                }
                else if (is_sneaked && this.mSearchMap.get(x, y))
                  continue;
                if (this.CheckMove(self, grid2) && !gridList.Contains(grid2))
                  gridList.Add(grid2);
              }
            }
          }
        }
        if (self.TreasureGainTarget != null && is_treasure)
        {
          if (gridList.Contains(self.TreasureGainTarget))
          {
            gridList.Clear();
            gridList.Add(self.TreasureGainTarget);
          }
          else
          {
            int num3 = (int) byte.MaxValue;
            for (int index = 0; index < gridList.Count; ++index)
            {
              if (num3 > (int) gridList[index].step)
                num3 = (int) gridList[index].step;
            }
            for (int index = 0; index < gridList.Count; ++index)
            {
              if (num3 < (int) gridList[index].step)
                gridList.RemoveAt(index--);
            }
          }
          if (self.IsEnableActionCondition())
          {
            Grid grid2 = currentMap[self.x, self.y];
            if (!gridList.Contains(grid2))
              gridList.Add(grid2);
          }
        }
      }
      else
        gridList.Add(grid1);
      return gridList;
    }

    private void UpdateTreasureTargetAI()
    {
      if (!this.mQuestParam.CheckAllowedAutoBattle() || !GameUtility.Config_AutoMode_Treasure.Value || this.mTreasures.Count == 0)
        return;
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].TreasureGainTarget = (Grid) null;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = 0; index1 < this.mTreasures.Count; ++index1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        BattleCore.\u003CUpdateTreasureTargetAI\u003Ec__AnonStorey1B6 aiCAnonStorey1B6 = new BattleCore.\u003CUpdateTreasureTargetAI\u003Ec__AnonStorey1B6();
        if (this.mTreasures[index1].EventTrigger != null && this.mTreasures[index1].EventTrigger.EventType == EEventType.Treasure && this.mTreasures[index1].EventTrigger.Count != 0)
        {
          // ISSUE: reference to a compiler-generated field
          aiCAnonStorey1B6.suited = (Unit) null;
          Grid grid = currentMap[this.mTreasures[index1].x, this.mTreasures[index1].y];
          int num1 = (int) byte.MaxValue;
          for (int index2 = 0; index2 < this.mPlayer.Count; ++index2)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleCore.\u003CUpdateTreasureTargetAI\u003Ec__AnonStorey1B7 aiCAnonStorey1B7 = new BattleCore.\u003CUpdateTreasureTargetAI\u003Ec__AnonStorey1B7();
            // ISSUE: reference to a compiler-generated field
            aiCAnonStorey1B7.\u003C\u003Ef__ref\u0024438 = aiCAnonStorey1B6;
            // ISSUE: reference to a compiler-generated field
            aiCAnonStorey1B7.unit = this.mPlayer[index2];
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (aiCAnonStorey1B7.unit.UnitType == EUnitType.Unit && aiCAnonStorey1B7.unit.TreasureGainTarget == null && (aiCAnonStorey1B7.unit.IsEntry && !aiCAnonStorey1B7.unit.IsSub) && (aiCAnonStorey1B7.unit.IsEnableAutoMode() && aiCAnonStorey1B7.unit.IsEnableMoveCondition(true)))
            {
              // ISSUE: reference to a compiler-generated field
              int moveCount = aiCAnonStorey1B7.unit.GetMoveCount(true);
              if (moveCount != 0)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                currentMap.CalcMoveSteps(aiCAnonStorey1B7.unit, currentMap[aiCAnonStorey1B7.unit.x, aiCAnonStorey1B7.unit.y]);
                int num2 = (int) grid.step / moveCount + ((int) grid.step % moveCount <= 0 ? 0 : 1);
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                if (num2 <= num1 && (num2 != num1 || aiCAnonStorey1B6.suited == null || this.mOrder.FindIndex(new Predicate<BattleCore.OrderData>(aiCAnonStorey1B7.\u003C\u003Em__156)) >= this.mOrder.FindIndex(new Predicate<BattleCore.OrderData>(aiCAnonStorey1B7.\u003C\u003Em__157))))
                {
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  aiCAnonStorey1B6.suited = aiCAnonStorey1B7.unit;
                  num1 = num2;
                }
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          if (aiCAnonStorey1B6.suited != null)
          {
            // ISSUE: reference to a compiler-generated field
            aiCAnonStorey1B6.suited.TreasureGainTarget = grid;
          }
        }
      }
    }

    private bool CalcMoveTargetAI(Unit self, bool forceAI)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CCalcMoveTargetAI\u003Ec__AnonStorey1B8 aiCAnonStorey1B8 = new BattleCore.\u003CCalcMoveTargetAI\u003Ec__AnonStorey1B8();
      if (!self.IsEnableMoveCondition(false) || self.GetMoveCount(false) == 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      aiCAnonStorey1B8.map = this.CurrentMap;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      aiCAnonStorey1B8.start = aiCAnonStorey1B8.map[self.x, self.y];
      if (self.IsUnitFlag(EUnitFlag.Escaped))
      {
        Grid escapePositionAi = this.GetEscapePositionAI(self);
        if (escapePositionAi != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (aiCAnonStorey1B8.start == escapePositionAi)
            return false;
          if (this.Move(self, escapePositionAi, forceAI))
            return true;
        }
      }
      AIPatrolPoint currentPatrolPoint = self.GetCurrentPatrolPoint();
      if (currentPatrolPoint != null)
      {
        // ISSUE: reference to a compiler-generated field
        Grid goal = aiCAnonStorey1B8.map[currentPatrolPoint.x, currentPatrolPoint.y];
        if (goal != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (aiCAnonStorey1B8.start == goal)
            return false;
          if (this.Move(self, goal, forceAI))
          {
            // ISSUE: reference to a compiler-generated field
            if ((int) aiCAnonStorey1B8.map[self.x, self.y].step <= currentPatrolPoint.length)
              self.NextPatrolPoint();
            return true;
          }
        }
      }
      List<Unit> mEnemyPriorities = this.mEnemyPriorities;
      bool flag1 = false;
      if (self.IsUnitFlag(EUnitFlag.Action) && (self.AI == null || !self.AI.CheckFlag(AIFlags.DisableAction)))
        flag1 = true;
      if (flag1)
      {
        Grid safePositionAi = this.GetSafePositionAI(self);
        if (safePositionAi != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (aiCAnonStorey1B8.start == safePositionAi)
            return false;
          if (this.Move(self, safePositionAi, forceAI))
            return true;
        }
      }
      bool is_friendlyfire = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      bool is_sneaked = self.IsUnitFlag(EUnitFlag.Sneaking);
      if (self.TreasureGainTarget != null)
      {
        List<Grid> enableMoveGridList = this.GetEnableMoveGridList(self, true, is_friendlyfire, is_sneaked, true);
        Grid grid = !enableMoveGridList.Contains(self.TreasureGainTarget) ? (enableMoveGridList.Count <= 0 ? (Grid) null : enableMoveGridList[0]) : self.TreasureGainTarget;
        // ISSUE: reference to a compiler-generated field
        if (aiCAnonStorey1B8.map.CalcMoveSteps(self, grid) && this.Move(self, grid, forceAI))
          return true;
      }
      for (int index1 = 0; index1 < mEnemyPriorities.Count; ++index1)
      {
        Grid unitGridPosition = this.GetUnitGridPosition(mEnemyPriorities[index1]);
        // ISSUE: reference to a compiler-generated field
        if (aiCAnonStorey1B8.map.CalcMoveSteps(self, unitGridPosition))
        {
          List<Grid> enableMoveGridList = this.GetEnableMoveGridList(self, true, is_friendlyfire, is_sneaked, false);
          if (is_sneaked)
          {
            bool flag2 = false;
            for (int index2 = 0; index2 < enableMoveGridList.Count; ++index2)
            {
              // ISSUE: reference to a compiler-generated field
              if ((int) enableMoveGridList[index2].step >= 0 && (int) enableMoveGridList[index2].step < (int) aiCAnonStorey1B8.start.step)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              enableMoveGridList = this.GetEnableMoveGridList(self, true, is_friendlyfire, false, false);
          }
          // ISSUE: reference to a compiler-generated method
          MySort<Grid>.Sort(enableMoveGridList, new Comparison<Grid>(aiCAnonStorey1B8.\u003C\u003Em__158));
          // ISSUE: reference to a compiler-generated field
          for (int index2 = 0; index2 < enableMoveGridList.Count && aiCAnonStorey1B8.start != enableMoveGridList[index2]; ++index2)
          {
            if (this.Move(self, enableMoveGridList[index2], forceAI))
              return true;
          }
          // ISSUE: reference to a compiler-generated field
          if (aiCAnonStorey1B8.start == unitGridPosition)
            return false;
          if (this.Move(self, unitGridPosition, forceAI))
            return true;
        }
      }
      return false;
    }

    private bool CalcSearchingAI(Unit self)
    {
      if (self.IsUnitFlag(EUnitFlag.Searched))
        return true;
      List<Unit> unitList = new List<Unit>(1);
      unitList.Add(self);
      if (!string.IsNullOrEmpty(self.UniqueName))
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && mUnit.Side == self.Side && mUnit.ParentUniqueName == self.UniqueName)
            unitList.Add(this.mUnits[index]);
        }
      }
      if (!string.IsNullOrEmpty(self.ParentUniqueName))
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && mUnit.Side == self.Side && (mUnit.UniqueName == self.ParentUniqueName || mUnit.ParentUniqueName == self.ParentUniqueName))
            unitList.Add(this.mUnits[index]);
        }
      }
      bool flag = false;
      for (int index = 0; index < unitList.Count; ++index)
      {
        if (unitList[index].IsUnitFlag(EUnitFlag.Searched) || this.Searching(unitList[index]))
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index = 0; index < unitList.Count; ++index)
          unitList[index].SetUnitFlag(EUnitFlag.Searched, true);
        return true;
      }
      this.CommandWait();
      return false;
    }

    private bool CheckEscapeAI(Unit self)
    {
      if (this.QuestType == QuestTypes.Arena || !self.IsEnableMoveCondition(false) || (self.GetMoveCount(false) == 0 || !self.CheckNeedEscaped()))
        return false;
      return this.GetHealer(self).Count > 0;
    }

    private List<Unit> GetHealer(Unit self)
    {
      List<Unit> l = new List<Unit>();
      int hp1 = (int) self.CurrentStatus.param.hp;
      int hp2 = (int) self.MaximumStatus.param.hp;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsDead && mUnit.IsEntry && (!mUnit.IsGimmick && !mUnit.IsSub) && (mUnit != self && mUnit.IsEnableSkillCondition(true) && !this.CheckEnemySide(self, mUnit)))
        {
          for (int index2 = 0; index2 < mUnit.BattleSkills.Count; ++index2)
          {
            SkillData battleSkill = mUnit.BattleSkills[index2];
            if (battleSkill != null && battleSkill.IsHealSkill() && (this.CheckEnableUseSkill(mUnit, battleSkill, true) && this.IsUseSkillCollabo(mUnit, battleSkill)) && (this.CheckSkillTargetAI(mUnit, self, battleSkill) && hp2 * (int) mUnit.AI.heal_border >= hp1 * 100))
            {
              l.Add(mUnit);
              break;
            }
          }
        }
      }
      if (l.Count > 0)
        MySort<Unit>.Sort(l, (Comparison<Unit>) ((src, dsc) =>
        {
          if (src == dsc)
            return 0;
          int chargeTime1 = (int) src.ChargeTime;
          int chargeTimeMax1 = (int) src.ChargeTimeMax;
          int chargeTime2 = (int) dsc.ChargeTime;
          int chargeTimeMax2 = (int) dsc.ChargeTimeMax;
          if (chargeTime1 != chargeTime2)
          {
            if (chargeTime1 >= chargeTimeMax1 && chargeTime2 >= chargeTimeMax2)
              return chargeTime2 - chargeTimeMax2 - (chargeTime1 - chargeTimeMax1);
            if (chargeTime1 >= chargeTimeMax1)
              return -1;
            if (chargeTime2 >= chargeTimeMax2)
              return 1;
            int chargeSpeed1 = (int) src.GetChargeSpeed();
            int chargeSpeed2 = (int) dsc.GetChargeSpeed();
            int num1 = chargeTimeMax1 - chargeTime1 == 0 ? 0 : (chargeTimeMax1 - chargeTime1) * 100 / chargeSpeed1;
            int num2 = chargeTimeMax2 - chargeTime2 == 0 ? 0 : (chargeTimeMax2 - chargeTime2) * 100 / chargeSpeed2;
            if (num1 != num2)
              return num1 - num2;
          }
          return this.CalcNearGridDistance(self, src) - this.CalcNearGridDistance(self, dsc);
        }));
      return l;
    }

    public List<Unit> CreateAttackTargetsAI(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> skillScopeMapAll = this.CreateSkillScopeMapAll(self, skill, is_move);
      List<Unit> targets = new List<Unit>(this.mUnits.Count);
      this.SearchTargetsInGridMap(self, skill, skillScopeMapAll, targets);
      for (int index = 0; index < targets.Count; ++index)
      {
        if (!this.CheckSkillTargetAI(self, targets[index], skill))
          targets.Remove(targets[index--]);
      }
      return targets;
    }

    private void GetEnemyPriorities(Unit self, List<Unit> targets)
    {
      if (self == null || targets == null)
        return;
      targets.Clear();
      Unit rageTarget = self.GetRageTarget();
      if (rageTarget != null)
      {
        targets.Add(rageTarget);
      }
      else
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && !mUnit.IsDead && (!mUnit.IsGimmick && !mUnit.IsSub) && (mUnit.IsEntry && this.CheckEnemySide(self, mUnit)))
            targets.Add(mUnit);
        }
        this.SortAttackTargets(self, targets);
      }
    }

    private int GetSkillTargetsHighestPriority(Unit self, SkillData skill, LogSkill log)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CGetSkillTargetsHighestPriority\u003Ec__AnonStorey1BA priorityCAnonStorey1Ba = new BattleCore.\u003CGetSkillTargetsHighestPriority\u003Ec__AnonStorey1BA();
      // ISSUE: reference to a compiler-generated field
      priorityCAnonStorey1Ba.log = log;
      int val1 = (int) byte.MaxValue;
      // ISSUE: reference to a compiler-generated field
      if (self == null || skill == null || priorityCAnonStorey1Ba.log == null)
        return val1;
      AIParam ai = self.AI;
      if (skill.IsDamagedSkill() && ai != null && !ai.CheckFlag(AIFlags.DisableTargetPriority))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        BattleCore.\u003CGetSkillTargetsHighestPriority\u003Ec__AnonStorey1BB priorityCAnonStorey1Bb = new BattleCore.\u003CGetSkillTargetsHighestPriority\u003Ec__AnonStorey1BB();
        // ISSUE: reference to a compiler-generated field
        priorityCAnonStorey1Bb.\u003C\u003Ef__ref\u0024442 = priorityCAnonStorey1Ba;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (priorityCAnonStorey1Bb.k = 0; priorityCAnonStorey1Bb.k < priorityCAnonStorey1Ba.log.targets.Count; ++priorityCAnonStorey1Bb.k)
        {
          // ISSUE: reference to a compiler-generated method
          int index = this.mEnemyPriorities.FindIndex(new Predicate<Unit>(priorityCAnonStorey1Bb.\u003C\u003Em__15A));
          if (index != -1)
            val1 = Math.Max(Math.Min(val1, index), 0);
        }
      }
      return val1;
    }

    private void SortAttackTargets(Unit unit, List<Unit> targets)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CSortAttackTargets\u003Ec__AnonStorey1BD targetsCAnonStorey1Bd = new BattleCore.\u003CSortAttackTargets\u003Ec__AnonStorey1BD();
      // ISSUE: reference to a compiler-generated field
      targetsCAnonStorey1Bd.unit = unit;
      // ISSUE: reference to a compiler-generated field
      targetsCAnonStorey1Bd.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      DebugUtility.Assert(targetsCAnonStorey1Bd.unit != null, "unit == null");
      if (targets.Count <= 0)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      MySort<Unit>.Sort(targets, new Comparison<Unit>(new BattleCore.\u003CSortAttackTargets\u003Ec__AnonStorey1BC()
      {
        \u003C\u003Ef__ref\u0024445 = targetsCAnonStorey1Bd,
        \u003C\u003Ef__this = this,
        rage = targetsCAnonStorey1Bd.unit.GetRageTarget()
      }.\u003C\u003Em__15B));
    }

    private Grid GetEscapePositionAI(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      if ((self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false)) == 0)
        return (Grid) null;
      bool flag1 = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      Grid grid1 = currentMap[self.x, self.y];
      List<Unit> healer = this.GetHealer(self);
      for (int index = 0; index < healer.Count; ++index)
      {
        Grid target = currentMap[healer[index].x, healer[index].y];
        if (currentMap.CalcMoveSteps(self, target))
        {
          bool flag2 = false;
          for (int x = 0; x < this.mMoveMap.w; ++x)
          {
            for (int y = 0; y < this.mMoveMap.h; ++y)
            {
              if (this.mMoveMap.get(x, y) >= 0)
              {
                Grid grid2 = currentMap[x, y];
                if (this.CheckMove(self, grid2) && (!flag1 || !this.CheckFriendlyFireOnGridMap(self, grid2)) && (int) grid1.step > (int) grid2.step)
                {
                  grid1 = grid2;
                  flag2 = true;
                }
              }
            }
          }
          if (flag2)
            break;
        }
      }
      return grid1;
    }

    private Grid GetSafePositionAI(Unit self)
    {
      int currentEnemyNum = this.GetCurrentEnemyNum(self);
      if (currentEnemyNum == 0)
        return (Grid) null;
      BattleMap currentMap = this.CurrentMap;
      if ((self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false)) == 0)
        return (Grid) null;
      bool flag = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      Grid start = currentMap[self.x, self.y];
      Grid goal = currentMap[self.x, self.y];
      int num1 = Math.Max(this.mSafeMap.get(start.x, start.y) + (self.AI == null ? 0 : (int) self.AI.safe_border * currentEnemyNum), 0);
      for (int x = 0; x < this.mSafeMap.w; ++x)
      {
        for (int y = 0; y < this.mSafeMap.h; ++y)
        {
          if (this.mMoveMap.get(x, y) >= 0 && this.mSafeMap.get(x, y) >= num1)
          {
            Grid grid = currentMap[x, y];
            if ((!flag || !this.CheckFriendlyFireOnGridMap(self, grid)) && this.CheckMove(self, grid))
            {
              if (goal != null)
              {
                int num2 = this.CalcGridDistance(start, goal);
                if (this.CalcGridDistance(start, grid) < num2)
                  goal = grid;
              }
              else
                goal = grid;
            }
          }
        }
      }
      return goal;
    }

    private bool CheckSkillTargetAI(Unit self, Unit target, SkillData skill)
    {
      if (!this.CheckSkillTarget(self, target, skill))
        return false;
      if (skill.Target == ESkillTarget.UnitAll || skill.Target == ESkillTarget.NotSelf)
      {
        switch (skill.EffectType)
        {
          case SkillEffectTypes.Heal:
          case SkillEffectTypes.Buff:
          case SkillEffectTypes.Revive:
          case SkillEffectTypes.Shield:
          case SkillEffectTypes.CureCondition:
          case SkillEffectTypes.GemsGift:
          case SkillEffectTypes.Guard:
          case SkillEffectTypes.RateHeal:
            return !this.CheckEnemySide(self, target);
          case SkillEffectTypes.GemsIncDec:
            if ((int) skill.EffectValue > 0)
              return !this.CheckEnemySide(self, target);
            return this.CheckEnemySide(self, target);
          case SkillEffectTypes.Teleport:
          case SkillEffectTypes.Changing:
          case SkillEffectTypes.Throw:
            break;
          default:
            return this.CheckEnemySide(self, target);
        }
      }
      return true;
    }

    private bool CalcUseSkillAI(Unit self, bool forceAI)
    {
      this.UpdateUsedSkillDicision(self);
      if (self.CastSkill != null)
        return false;
      if (self.IsAIActionTable())
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        BattleCore.\u003CCalcUseSkillAI\u003Ec__AnonStorey1BE aiCAnonStorey1Be = new BattleCore.\u003CCalcUseSkillAI\u003Ec__AnonStorey1BE();
        // ISSUE: reference to a compiler-generated field
        aiCAnonStorey1Be.action = self.GetCurrentAIAction();
        // ISSUE: reference to a compiler-generated field
        if (aiCAnonStorey1Be.action != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (string.IsNullOrEmpty((string) aiCAnonStorey1Be.action.skill))
          {
            // ISSUE: reference to a compiler-generated field
            switch ((int) aiCAnonStorey1Be.action.type)
            {
              case 1:
                break;
              default:
                return false;
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            SkillData skill = self.BattleSkills.Find(new Predicate<SkillData>(aiCAnonStorey1Be.\u003C\u003Em__15C));
            if (this.CheckEnableUseSkill(self, skill, false))
            {
              BattleCore.mSkillResults.Clear();
              this.GetUsedSkillResult(self, skill, BattleCore.mSkillResults);
              if (BattleCore.mSkillResults.Count > 0 && this.UseSkillAI(self, BattleCore.mSkillResults, forceAI))
                return true;
            }
          }
        }
      }
      for (int index1 = 0; index1 < this.mUseSkillLists.Count; ++index1)
      {
        if (this.mUseSkillLists[index1] != null && this.mUseSkillLists[index1].Count != 0)
        {
          List<SkillData> mUseSkillList = this.mUseSkillLists[index1];
          BattleCore.mSkillResults.Clear();
          for (int index2 = 0; index2 < mUseSkillList.Count; ++index2)
          {
            SkillData skill = mUseSkillList[index2];
            this.GetUsedSkillResult(self, skill, BattleCore.mSkillResults);
          }
          if (BattleCore.mSkillResults.Count != 0 && this.UseSkillAI(self, BattleCore.mSkillResults, forceAI))
            return true;
        }
      }
      for (int index = 0; index < this.mForceSkillList.Count; ++index)
      {
        SkillData mForceSkill = this.mForceSkillList[index];
        if (this.CheckEnableUseSkill(self, mForceSkill, false))
        {
          BattleCore.mSkillResults.Clear();
          this.GetUsedSkillResult(self, mForceSkill, BattleCore.mSkillResults);
          if (BattleCore.mSkillResults.Count != 0 && this.UseSkillAI(self, BattleCore.mSkillResults, forceAI))
            return true;
        }
      }
      return false;
    }

    private void SortSkillResult(Unit self, List<BattleCore.SkillResult> results)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CSortSkillResult\u003Ec__AnonStorey1BF resultCAnonStorey1Bf = new BattleCore.\u003CSortSkillResult\u003Ec__AnonStorey1BF();
      // ISSUE: reference to a compiler-generated field
      resultCAnonStorey1Bf.self = self;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      resultCAnonStorey1Bf.bPositioning = resultCAnonStorey1Bf.self.AI != null && resultCAnonStorey1Bf.self.AI.CheckFlag(AIFlags.Positioning);
      // ISSUE: reference to a compiler-generated method
      MySort<BattleCore.SkillResult>.Sort(results, new Comparison<BattleCore.SkillResult>(resultCAnonStorey1Bf.\u003C\u003Em__15D));
    }

    private bool UseSkillAI(Unit self, List<BattleCore.SkillResult> results, bool forceAI)
    {
      if (self == null || results == null || results.Count == 0)
        return false;
      this.SortSkillResult(self, results);
      BattleCore.SkillResult result = results[0];
      if ((self.x != result.movpos.x || self.y != result.movpos.y) && this.Move(self, result.movpos, forceAI))
        return true;
      bool bUnitLockTarget = false;
      if (result.skill.IsCastSkill() && result.skill.IsEnableUnitLockTarget())
      {
        if (result.skill.IsAreaSkill())
        {
          if (result.skill.Target != ESkillTarget.UnitAll && result.skill.Target != ESkillTarget.NotSelf)
            bUnitLockTarget = result.locked;
        }
        else
          bUnitLockTarget = result.locked;
      }
      AIAction currentAiAction = self.GetCurrentAIAction();
      if (currentAiAction != null)
      {
        string str = string.Empty;
        if (!string.IsNullOrEmpty((string) currentAiAction.skill))
          str = (string) currentAiAction.skill;
        else if ((int) currentAiAction.type == 1)
        {
          SkillData attackSkill = self.GetAttackSkill();
          str = attackSkill == null ? string.Empty : attackSkill.SkillID;
        }
        if (str == result.skill.SkillID)
          self.NextAIAction();
      }
      return this.UseSkill(self, result.usepos.x, result.usepos.y, result.skill, bUnitLockTarget, 0, 0);
    }

    private void UpdateUsedSkillDicision(Unit self)
    {
      AIParam ai = self.AI;
      this.mUseSkillLists.Clear();
      this.mForceSkillList.Clear();
      this.mHealSkills.Clear();
      this.mDamageSkills.Clear();
      this.mSupportSkills.Clear();
      this.mCureConditionSkills.Clear();
      this.mFailConditionSkills.Clear();
      this.mDisableConditionSkills.Clear();
      bool flag1 = true;
      if (!self.IsEnableSkillCondition(false) || this.CheckDisableAbilities(self))
        flag1 = false;
      if (ai != null && ai.CheckFlag(AIFlags.DisableSkill))
        flag1 = false;
      if (this.mQuestParam.CheckAllowedAutoBattle() && self.IsEnableAutoMode() && GameUtility.Config_AutoMode_DisableSkill.Value)
        flag1 = false;
      if (flag1)
      {
        int gems = this.GetGems(self);
        if (!self.IsAIActionTable())
        {
          if (gems >= (int) ai.gems_border)
          {
            for (int index = 0; index < self.BattleSkills.Count; ++index)
              this.EntrySkillListCategory(self, self.BattleSkills[index], false);
          }
          this.EntrySkillListCategory(self, self.AIForceSkill, true);
        }
      }
      this.EntrySkillListCategory(self, self.GetAttackSkill(), this.mForceSkillList.Count > 0);
      if (ai != null && ai.SkillCategoryPriorities != null)
      {
        for (int index1 = 0; index1 < ai.SkillCategoryPriorities.Length; ++index1)
        {
          List<SkillData> skillDataList = (List<SkillData>) null;
          switch (ai.SkillCategoryPriorities[index1])
          {
            case SkillCategory.Damage:
              skillDataList = this.mDamageSkills;
              goto default;
            case SkillCategory.Heal:
              if (this.GetHealUnitCount(self) != 0)
              {
                skillDataList = this.mHealSkills;
                goto default;
              }
              else
                goto default;
            case SkillCategory.Support:
              if (ai.CheckFlag(AIFlags.SelfBuffOnly))
              {
                bool flag2 = false;
                for (int index2 = 0; index2 < self.BuffAttachments.Count; ++index2)
                {
                  if (!(bool) self.BuffAttachments[index2].IsPassive && self.BuffAttachments[index2].BuffType == BuffTypes.Buff && self.BuffAttachments[index2].user == self)
                  {
                    flag2 = true;
                    break;
                  }
                }
                if (flag2)
                  break;
              }
              skillDataList = this.mSupportSkills;
              goto default;
            case SkillCategory.CureCondition:
              skillDataList = this.mCureConditionSkills;
              goto default;
            case SkillCategory.FailCondition:
              skillDataList = this.mFailConditionSkills;
              goto default;
            case SkillCategory.DisableCondition:
              skillDataList = this.mDisableConditionSkills;
              goto default;
            default:
              if (skillDataList != null && !this.mUseSkillLists.Contains(skillDataList))
              {
                this.mUseSkillLists.Add(skillDataList);
                break;
              }
              break;
          }
        }
      }
      else
        this.mUseSkillLists.Add(this.mDamageSkills);
    }

    private bool EntrySkillListCategory(Unit self, SkillData skill, bool forced = false)
    {
      if (skill == null || skill.SkillType != ESkillType.Attack && skill.SkillType != ESkillType.Skill && skill.SkillType != ESkillType.Item || (skill.EffectType == SkillEffectTypes.Throw || !this.CheckEnableUseSkill(self, skill, false)))
        return false;
      if (forced)
      {
        this.mForceSkillList.Add(skill);
        return true;
      }
      if (!this.CheckSkillDicisionAI(self, skill))
        return false;
      List<SkillData> skillDataList = (List<SkillData>) null;
      if (skill.IsDamagedSkill())
        skillDataList = this.mDamageSkills;
      else if (skill.IsHealSkill())
        skillDataList = this.mHealSkills;
      else if (skill.IsSupportSkill())
        skillDataList = this.mSupportSkills;
      else if (skill.EffectType == SkillEffectTypes.CureCondition)
        skillDataList = this.mCureConditionSkills;
      else if (skill.EffectType == SkillEffectTypes.FailCondition)
        skillDataList = this.mFailConditionSkills;
      else if (skill.EffectType == SkillEffectTypes.DisableCondition)
        skillDataList = this.mDisableConditionSkills;
      if (skillDataList == null)
        return false;
      skillDataList.Add(skill);
      return true;
    }

    private bool CheckSkillDicisionAI(Unit self, SkillData skill)
    {
      if (this.QuestType == QuestTypes.Arena || skill.IsNormalAttack())
        return true;
      if (skill.IsDamagedSkill())
      {
        if (skill.IsJewelAttack() && self.AI != null && self.AI.CheckFlag(AIFlags.DisableJewelAttack))
          return false;
      }
      else
      {
        if (self.GetRageTarget() != null)
          return false;
        bool flag1 = false;
        if (!skill.IsHealSkill())
        {
          if (skill.IsSupportSkill())
            flag1 = true;
          else if (skill.EffectType == SkillEffectTypes.CureCondition)
          {
            CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.CureCondition))
              return false;
            bool flag2 = false;
            for (int index = 0; index < condEffect.param.conditions.Length; ++index)
            {
              if (this.GetFailCondSelfSideUnitCount(self, condEffect.param.conditions[index]) != 0)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              return false;
          }
          else if (skill.EffectType != SkillEffectTypes.FailCondition && skill.EffectType == SkillEffectTypes.DisableCondition)
          {
            CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.DisableCondition))
              return false;
            bool flag2 = false;
            for (int index = 0; index < condEffect.param.conditions.Length; ++index)
            {
              EUnitCondition condition = condEffect.param.conditions[index];
              if (this.CheckFailCondSkillUseEnemies(self, condition))
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              return false;
          }
        }
        if (flag1)
        {
          bool flag2 = false;
          bool flag3 = true;
          if ((int) self.AI.DisableSupportActionHpBorder != 0)
          {
            int num = (int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp;
            flag2 = true;
            flag3 &= (int) self.AI.DisableSupportActionHpBorder >= num;
          }
          if ((int) self.AI.DisableSupportActionMemberBorder != 0)
          {
            int aliveUnitCount = this.GetAliveUnitCount(self);
            flag2 = true;
            flag3 &= (int) self.AI.DisableSupportActionMemberBorder >= aliveUnitCount;
          }
          if (flag2 && flag3)
            return false;
        }
      }
      return self.IsPartyMember || (skill.UseCondition == null || skill.UseCondition.type == 0 || skill.UseCondition.unlock) && (int) (this.GetRandom() % 100U) < (int) skill.UseRate;
    }

    private int GetHealUnitCount(Unit self)
    {
      int num = 0;
      AIParam ai = self.AI;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && mUnit.IsEntry && (!mUnit.IsGimmick && !mUnit.IsSub) && !this.CheckEnemySide(self, mUnit))
        {
          int hp1 = (int) mUnit.CurrentStatus.param.hp;
          int hp2 = (int) mUnit.MaximumStatus.param.hp;
          if (ai != null)
          {
            if (hp2 * (int) ai.heal_border < hp1 * 100)
              continue;
          }
          else if (hp1 == hp2)
            continue;
          ++num;
        }
      }
      return num;
    }

    private int GetFailCondSelfSideUnitCount(Unit self, EUnitCondition condition)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!this.mUnits[index].IsSub && this.mUnits[index].IsEntry && (!this.mUnits[index].IsDead && !this.mUnits[index].IsGimmick) && (!this.CheckEnemySide(self, mUnit) && mUnit.IsUnitCondition(condition)))
          ++num;
      }
      return num;
    }

    private int GetAliveUnitCount(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && !mUnit.IsGimmick && mUnit.Side == self.Side)
          ++num;
      }
      return num;
    }

    private int GetDeadUnitCount(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.IsDead && !mUnit.IsGimmick && mUnit.Side == self.Side)
          ++num;
      }
      return num;
    }

    private bool CheckFailCondSkillUseEnemies(Unit self, EUnitCondition condition)
    {
      if (condition == EUnitCondition.AutoHeal || condition == EUnitCondition.GoodSleep || (condition == EUnitCondition.AutoJewel || condition == EUnitCondition.DisableBuff) || (condition == EUnitCondition.DisableDebuff || condition == EUnitCondition.DisableKnockback))
        return false;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsSub && mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsGimmick) && (mUnit.IsEnableSkillCondition(true) && this.CheckEnemySide(self, mUnit) && mUnit.BattleSkills != null))
        {
          for (int index2 = 0; index2 < mUnit.BattleSkills.Count; ++index2)
          {
            SkillData battleSkill = mUnit.BattleSkills[index2];
            if (mUnit.IsPartyMember || (int) battleSkill.UseRate != 0 && (battleSkill.UseCondition == null || battleSkill.UseCondition.type == 0 || battleSkill.UseCondition.unlock))
            {
              CondEffect condEffect = battleSkill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect != null && condEffect.param != null && condEffect.param.conditions != null && (condEffect.param.type == ConditionEffectTypes.FailCondition || condEffect.param.type == ConditionEffectTypes.ForcedFailCondition || condEffect.param.type == ConditionEffectTypes.RandomFailCondition))
                return Array.IndexOf<EUnitCondition>(condEffect.param.conditions, condition) != -1;
            }
          }
        }
      }
      return false;
    }

    private bool CheckBuffDebuffEffectiveEnemies(Unit self, BuffTypes type)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsSub && mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsGimmick) && (this.CheckEnemySide(self, mUnit) && mUnit.CheckActionSkillBuffAttachments(type)))
          return true;
      }
      return false;
    }

    private bool CheckElementDamageSkillUseEnemies(Unit self)
    {
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsSub && mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsGimmick) && this.CheckEnemySide(self, mUnit))
        {
          SkillData attackSkill = mUnit.GetAttackSkill();
          if (attackSkill != null && attackSkill.ElementType != EElement.None)
            return true;
          if (mUnit.BattleSkills != null)
          {
            for (int index2 = 0; index2 < mUnit.BattleSkills.Count; ++index2)
            {
              SkillData battleSkill = mUnit.BattleSkills[index2];
              if ((mUnit.IsPartyMember || (int) battleSkill.UseRate != 0 && (battleSkill.UseCondition == null || battleSkill.UseCondition.type == 0 || battleSkill.UseCondition.unlock)) && battleSkill.ElementType != EElement.None)
                return true;
            }
          }
        }
      }
      return false;
    }

    private void GetUsedSkillResult(Unit self, SkillData skill, List<BattleCore.SkillResult> results)
    {
      if (skill == null || results == null)
        return;
      bool is_friendlyfire = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      bool is_move = true;
      if (self.IsUnitFlag(EUnitFlag.Escaped) && !skill.IsHealSkill())
        is_move = false;
      List<Grid> enableMoveGridList = this.GetEnableMoveGridList(self, is_move, is_friendlyfire, false, true);
      int skillUsedCost = self.GetSkillUsedCost(skill);
      int conditionPriority = self.GetConditionPriority(skill, SkillEffectTargets.Target);
      BattleMap currentMap = this.CurrentMap;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      for (int index1 = 0; index1 < enableMoveGridList.Count; ++index1)
      {
        Grid grid = enableMoveGridList[index1];
        if (currentMap.CheckEnableMove(self, grid, false))
        {
          int x = self.x;
          int y = self.y;
          self.x = grid.x;
          self.y = grid.y;
          if (!this.IsUseSkillCollabo(self, skill))
          {
            self.x = x;
            self.y = y;
          }
          else
          {
            GridMap<bool> selectGridMap = this.CreateSelectGridMap(self, grid.x, grid.y, skill);
            for (int index2 = 0; index2 < selectGridMap.w; ++index2)
            {
              for (int index3 = 0; index3 < selectGridMap.h; ++index3)
              {
                if (selectGridMap.get(index2, index3))
                {
                  BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
                  List<Unit> targets = new List<Unit>(this.mOrder.Count);
                  this.GetExecuteSkillLineTarget(self, index2, index3, skill, ref targets, ref shot);
                  if (targets.Count > 0)
                  {
                    this.SetBattleFlag(EBattleFlag.PredictResult, true);
                    this.mRandDamage.Seed(this.mSeedDamage);
                    this.CurrentRand = this.mRandDamage;
                    LogSkill log = new LogSkill();
                    log.self = self;
                    log.skill = skill;
                    log.pos.x = index2;
                    log.pos.y = index3;
                    log.reflect = (LogSkill.Reflection) null;
                    for (int index4 = 0; index4 < targets.Count; ++index4)
                      log.SetSkillTarget(self, targets[index4]);
                    if (shot != null)
                    {
                      log.pos.x = shot.end.x;
                      log.pos.y = shot.end.y;
                      log.rad = (int) (shot.rad * 100.0);
                      log.height = (int) (shot.height * 100.0);
                    }
                    this.ExecuteSkill(ESkillTiming.Used, log, skill);
                    if (this.CheckEnableUseSkillEffect(self, skill, log))
                    {
                      BattleCore.SkillResult skillResult = new BattleCore.SkillResult();
                      skillResult.skill = skill;
                      skillResult.movpos = grid;
                      skillResult.usepos = currentMap[index2, index3];
                      skillResult.locked = this.FindUnitAtGrid(skillResult.usepos) != null;
                      skillResult.log = log;
                      skillResult.heal = log.GetTruthTotalHpHeal();
                      skillResult.heal_num = log.GetTruthTotalHpHealCount();
                      skillResult.cond_prio = conditionPriority;
                      skillResult.cure_num = log.GetTotalCureConditionCount();
                      skillResult.fail_num = log.GetTotalFailConditionCount();
                      skillResult.disable_num = log.GetTotalDisableConditionCount();
                      skillResult.damage = log.GetTruthTotalHpDamage();
                      skillResult.dead_num = log.GetTotalDeathCount();
                      skillResult.gain_jewel = log.GetGainJewel();
                      skillResult.cost_jewel = skillUsedCost;
                      skillResult.distance = this.CalcGridDistance(skillResult.usepos, skillResult.movpos);
                      skillResult.unit_prio = this.GetSkillTargetsHighestPriority(self, skill, log);
                      skillResult.ct = 0;
                      if (grid.x != x || grid.y != y)
                        skillResult.ct -= (int) fixParam.ChargeTimeDecMove;
                      skillResult.ct -= (int) fixParam.ChargeTimeDecWait;
                      skillResult.ct -= (int) fixParam.ChargeTimeDecAction;
                      log.GetTotalBuffEffect(out skillResult.buff_num, out skillResult.buff);
                      skillResult.buff_prio = (int) byte.MaxValue;
                      for (int index4 = 0; index4 < log.targets.Count; ++index4)
                      {
                        int buffPriority = log.targets[index4].target.GetBuffPriority(skill, SkillEffectTargets.Target);
                        skillResult.buff_prio = Math.Max(Math.Min(buffPriority, skillResult.buff_prio), 0);
                      }
                      if (skillResult.buff_prio == (int) byte.MaxValue)
                        skillResult.buff_prio = self.GetBuffPriority(skill, SkillEffectTargets.Self);
                      results.Add(skillResult);
                    }
                    this.CurrentRand = this.mRand;
                    self.SetUnitFlag(EUnitFlag.SideAttack, false);
                    self.SetUnitFlag(EUnitFlag.BackAttack, false);
                    this.SetBattleFlag(EBattleFlag.PredictResult, false);
                  }
                }
              }
            }
            self.x = x;
            self.y = y;
          }
        }
      }
    }

    private bool CheckEnableUseSkillEffect(Unit self, SkillData skill, LogSkill log)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleCore.\u003CCheckEnableUseSkillEffect\u003Ec__AnonStorey1C0 effectCAnonStorey1C0 = new BattleCore.\u003CCheckEnableUseSkillEffect\u003Ec__AnonStorey1C0();
      // ISSUE: reference to a compiler-generated field
      effectCAnonStorey1C0.self = self;
      // ISSUE: reference to a compiler-generated field
      effectCAnonStorey1C0.skill = skill;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (effectCAnonStorey1C0.self == null || effectCAnonStorey1C0.skill == null || log == null)
        return false;
      int num1 = 0;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      effectCAnonStorey1C0.rage = effectCAnonStorey1C0.self.GetRageTarget();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      if (effectCAnonStorey1C0.rage != null && (!effectCAnonStorey1C0.skill.IsDamagedSkill() || log.targets.Find(new Predicate<LogSkill.Target>(effectCAnonStorey1C0.\u003C\u003Em__15E)) == null))
        return false;
      for (int index1 = 0; index1 < log.targets.Count; ++index1)
      {
        Unit target = log.targets[index1].target;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!this.CheckSkillTargetAI(effectCAnonStorey1C0.self, target, effectCAnonStorey1C0.skill))
          return false;
        // ISSUE: reference to a compiler-generated field
        if (!effectCAnonStorey1C0.skill.IsDamagedSkill())
        {
          // ISSUE: reference to a compiler-generated field
          if (effectCAnonStorey1C0.skill.IsHealSkill())
          {
            if (Math.Max(Math.Min(log.targets[index1].GetTotalHpHeal(), (int) target.MaximumStatus.param.hp - (int) target.CurrentStatus.param.hp), 0) == 0)
              continue;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (effectCAnonStorey1C0.skill.IsSupportSkill())
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              if (effectCAnonStorey1C0.self.AI == null || !effectCAnonStorey1C0.self.AI.CheckFlag(AIFlags.SelfBuffOnly) || log.targets.Find(new Predicate<LogSkill.Target>(effectCAnonStorey1C0.\u003C\u003Em__15F)) != null)
              {
                // ISSUE: reference to a compiler-generated field
                if ((int) effectCAnonStorey1C0.skill.ControlChargeTimeValue == 0)
                {
                  // ISSUE: reference to a compiler-generated method
                  if (target.BuffAttachments.Count <= 0 || target.BuffAttachments.Find(new Predicate<BuffAttachment>(effectCAnonStorey1C0.\u003C\u003Em__160)) == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    BuffEffect buffEffect = effectCAnonStorey1C0.skill.GetBuffEffect(SkillEffectTargets.Target);
                    if (buffEffect != null && buffEffect.CheckEnableBuffTarget(target))
                    {
                      bool flag = false;
                      for (int index2 = 0; index2 < buffEffect.targets.Count; ++index2)
                      {
                        switch (buffEffect.targets[index2].buffType)
                        {
                          case BuffTypes.Buff:
                            if (target.IsEnableBuffEffect(BuffTypes.Buff))
                            {
                              // ISSUE: reference to a compiler-generated field
                              // ISSUE: reference to a compiler-generated field
                              int num2 = effectCAnonStorey1C0.self.AI == null ? 0 : (int) effectCAnonStorey1C0.self.AI.buff_border;
                              if (Math.Max(100 - (int) target.CurrentStatus.enchant_resist.resist_buff, 0) > num2)
                                goto default;
                              else
                                break;
                            }
                            else
                              break;
                          case BuffTypes.Debuff:
                            if (target.IsEnableBuffEffect(BuffTypes.Debuff))
                            {
                              // ISSUE: reference to a compiler-generated field
                              // ISSUE: reference to a compiler-generated field
                              int num2 = effectCAnonStorey1C0.self.AI == null ? 0 : (int) effectCAnonStorey1C0.self.AI.buff_border;
                              if (Math.Max(100 - (int) target.CurrentStatus.enchant_resist.resist_debuff, 0) > num2)
                                goto default;
                              else
                                break;
                            }
                            else
                              break;
                          default:
                            if (target.GetActionSkillBuffValue(buffEffect.targets[index2].buffType, buffEffect.targets[index2].calcType, buffEffect.targets[index2].paramType) < Math.Abs((int) buffEffect.targets[index2].value))
                            {
                              flag = true;
                              goto label_25;
                            }
                            else
                              break;
                        }
                      }
label_25:
                      if (!flag)
                        continue;
                    }
                    else
                      continue;
                  }
                  else
                    continue;
                }
              }
              else
                continue;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (effectCAnonStorey1C0.skill.IsConditionSkill())
              {
                // ISSUE: reference to a compiler-generated field
                CondEffect condEffect = effectCAnonStorey1C0.skill.GetCondEffect(SkillEffectTargets.Target);
                if (condEffect != null && condEffect.param.conditions != null && condEffect.CheckEnableCondTarget(target))
                {
                  bool flag = false;
                  // ISSUE: reference to a compiler-generated field
                  if (effectCAnonStorey1C0.skill.EffectType == SkillEffectTypes.CureCondition)
                  {
                    for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                    {
                      if (target.CheckEnableCureCondition(condEffect.param.conditions[index2]))
                      {
                        flag = true;
                        break;
                      }
                    }
                  }
                  else
                  {
                    // ISSUE: reference to a compiler-generated field
                    if (effectCAnonStorey1C0.skill.EffectType == SkillEffectTypes.FailCondition)
                    {
                      // ISSUE: reference to a compiler-generated field
                      // ISSUE: reference to a compiler-generated field
                      int num2 = effectCAnonStorey1C0.self.AI == null ? 0 : (int) effectCAnonStorey1C0.self.AI.cond_border;
                      if (num2 <= 0 || (int) condEffect.rate <= 0 || (int) condEffect.rate >= num2)
                      {
                        for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                        {
                          EUnitCondition condition = condEffect.param.conditions[index2];
                          switch (condition)
                          {
                            case EUnitCondition.DisableBuff:
                              // ISSUE: reference to a compiler-generated field
                              if (this.CheckBuffDebuffEffectiveEnemies(effectCAnonStorey1C0.self, BuffTypes.Buff))
                                goto default;
                              else
                                break;
                            case EUnitCondition.DisableDebuff:
                              // ISSUE: reference to a compiler-generated field
                              if (this.CheckBuffDebuffEffectiveEnemies(effectCAnonStorey1C0.self, BuffTypes.Debuff))
                                goto default;
                              else
                                break;
                            default:
                              if (target.CheckEnableFailCondition(condition) && (num2 <= 0 || Math.Max((int) condEffect.value - (int) target.CurrentStatus.enchant_resist[condition], 0) >= num2))
                              {
                                flag = true;
                                goto label_50;
                              }
                              else
                                break;
                          }
                        }
                      }
                      else
                        continue;
                    }
                    else
                    {
                      // ISSUE: reference to a compiler-generated field
                      if (effectCAnonStorey1C0.skill.EffectType == SkillEffectTypes.DisableCondition)
                      {
                        for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                        {
                          if (!target.IsDisableUnitCondition(condEffect.param.conditions[index2]))
                          {
                            flag = true;
                            break;
                          }
                        }
                      }
                    }
                  }
label_50:
                  if (!flag)
                    continue;
                }
                else
                  continue;
              }
            }
          }
        }
        ++num1;
      }
      return num1 != 0;
    }

    public bool CheckFriendlyFireOnGridMap(Unit self, Grid grid)
    {
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.CastSkill != null && !unit.CastSkill.IsAllEffect() && (unit.UnitTarget != self && unit.CastSkill.IsDamagedSkill()) && (this.CheckSkillTarget(unit, self, unit.CastSkill) && unit.CastSkillGridMap != null && unit.CastSkillGridMap.get(grid.x, grid.y)))
          return true;
      }
      return false;
    }

    private bool CheckSkillChargeInTime(Unit self, Unit target, SkillData skill)
    {
      if (!skill.IsCastSkill())
        return true;
      int castTimeMax = (int) self.CastTimeMax;
      int castSpeed = (int) self.GetCastSpeed(skill);
      int num1 = 0;
      int num2 = 0;
      if (castSpeed > 0)
      {
        while (num1 < castTimeMax)
        {
          num1 += castSpeed;
          ++num2;
        }
      }
      int chargeTimeMax = (int) target.ChargeTimeMax;
      int chargeSpeed = (int) target.GetChargeSpeed();
      int chargeTime = (int) target.ChargeTime;
      int num3 = 0;
      if (chargeSpeed > 0)
      {
        while (chargeTime < chargeTimeMax)
        {
          chargeTime += chargeSpeed;
          ++num3;
        }
      }
      if (num2 != num3)
        return num2 < num3;
      if (num1 != chargeTime)
        return num1 > chargeTime;
      return (int) self.UnitIndex < (int) target.UnitIndex;
    }

    private bool Searching(Unit self)
    {
      DebugUtility.Assert(self != null, "self == null");
      if (self.IsUnitFlag(EUnitFlag.Searched))
        return true;
      BattleMap currentMap = this.CurrentMap;
      DebugUtility.Assert(currentMap != null, "map == null");
      int searchRange = self.GetSearchRange();
      if (searchRange <= 0)
        return false;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateSelectGridMap(self, self.x, self.y, 0, searchRange, ESelectType.Diamond, ELineType.None, 0, false, false, 99, false, ref result, false);
      for (int x = 0; x < result.w; ++x)
      {
        for (int y = 0; y < result.h; ++y)
        {
          if (result.get(x, y))
          {
            Unit unitAtGrid = this.FindUnitAtGrid(currentMap[x, y]);
            if (unitAtGrid != null && unitAtGrid.Side != self.Side)
              return true;
          }
        }
      }
      return false;
    }

    private void UpdateSearchMap(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      this.mSearchMap.fill(false);
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      result.fill(false);
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit != self && mUnit.Side != self.Side && (!mUnit.IsDead && !mUnit.IsGimmick) && (!mUnit.IsSub && mUnit.IsEntry))
        {
          int range_max = mUnit.GetSearchRange() + 1;
          if (range_max != 0)
          {
            int x1 = mUnit.x;
            int y1 = mUnit.y;
            this.CreateSelectGridMap(mUnit, x1, y1, 0, range_max, ESelectType.Diamond, ELineType.None, 0, false, false, 99, false, ref result, false);
            for (int x2 = 0; x2 < result.w; ++x2)
            {
              for (int y2 = 0; y2 < result.h; ++y2)
              {
                if (result.get(x2, y2))
                  this.mSearchMap.set(x2, y2, true);
              }
            }
          }
        }
      }
    }

    private bool CheckSearchMap(Unit self)
    {
      DebugUtility.Assert(this.mSearchMap != null, "mSearchMap == null");
      BattleMap currentMap = this.CurrentMap;
      DebugUtility.Assert(currentMap != null, "map == null");
      DebugUtility.Assert(currentMap[self.x, self.y] != null, "grid == null");
      for (int x = 0; x < this.mSearchMap.w; ++x)
      {
        for (int y = 0; y < this.mSearchMap.h; ++y)
        {
          if (this.mSearchMap.get(x, y))
          {
            Grid grid = currentMap[x, y];
            if (self.CheckCollision(grid))
              return true;
          }
        }
      }
      return false;
    }

    private bool CheckEnemyIntercept(Unit self)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit != self && mUnit.Side != self.Side && (!mUnit.IsDeadCondition() && !mUnit.IsSub) && (mUnit.IsEntry && !mUnit.IsGimmick && (mUnit.IsUnitFlag(EUnitFlag.Searched) || this.CheckSearchMap(self))))
          return true;
      }
      return false;
    }

    public class OrderData
    {
      public Unit Unit;
      public bool IsCastSkill;
      public bool IsCharged;

      public OInt GetChargeTime()
      {
        if (this.IsCastSkill)
          return this.Unit.CastTime;
        return this.Unit.ChargeTime;
      }

      public OInt GetChargeTimeMax()
      {
        if (this.IsCastSkill)
          return this.Unit.CastTimeMax;
        return this.Unit.ChargeTimeMax;
      }

      public OInt GetChargeSpeed()
      {
        if (this.IsCastSkill)
          return this.Unit.GetCastSpeed();
        return this.Unit.GetChargeSpeed();
      }

      public bool CheckChargeTimeFullOver()
      {
        if (this.IsCastSkill)
          return this.Unit.CheckCastTimeFullOver();
        return this.Unit.CheckChargeTimeFullOver();
      }

      public void UpdateChargeTime()
      {
        if (this.IsCastSkill)
          this.Unit.UpdateCastTime();
        else
          this.Unit.UpdateChargeTime();
      }
    }

    public class Record
    {
      public OInt playerexp = (OInt) 0;
      public OInt unitexp = (OInt) 0;
      public OInt gold = (OInt) 0;
      public OInt chain = (OInt) 0;
      public OInt multicoin = (OInt) 0;
      public List<UnitParam> units = new List<UnitParam>(4);
      public List<ItemParam> items = new List<ItemParam>(4);
      public Dictionary<OString, OInt> used_items = new Dictionary<OString, OInt>();
      public BattleCore.QuestResult result;
      public int bonusFlags;
      public OInt[] drops;
      public OInt[] item_steals;
      public OInt[] gold_steals;

      public bool IsZero
      {
        get
        {
          return (int) this.gold == 0 && (int) this.playerexp == 0 && ((int) this.unitexp == 0 && this.items.Count <= 0) && (int) this.multicoin == 0;
        }
      }
    }

    public enum RESUME_STATE
    {
      NONE,
      REQUEST,
      WAIT,
    }

    public class Json_BattleCont
    {
      public long btlid;
      public BattleCore.Json_BtlInfo btlinfo;
      public Json_PlayerData player;
    }

    public class Json_Battle
    {
      public long btlid;
      public BattleCore.Json_BtlInfo btlinfo;
      public Json_Unit[] coloenemyunits;
      public int maxActionNum;
    }

    public class Json_BtlReward
    {
      public int gold;
    }

    public class Json_BtlInfo
    {
      public string qid;
      public BattleCore.Json_BtlUnit[] units;
      public Json_Support help;
      public BattleCore.Json_BtlDrop[] drops;
      public BattleCore.Json_BtlSteal[] steals;
      public BattleCore.Json_BtlReward[] rewards;
      public string key;
      public int seed;
      public int[] atkmags;
      public string[] campaigns;
      public long start_at;

      public virtual RandDeckResult[] GetDeck()
      {
        return (RandDeckResult[]) null;
      }
    }

    public class Json_BtlUnit
    {
      public int iid;
    }

    public class Json_BtlDrop
    {
      public string iname;
      public int gold;
      public int num;
    }

    public class Json_BtlSteal
    {
      public string iname;
      public int gold;
      public int num;
    }

    public enum QuestResult
    {
      Pending,
      Win,
      Lose,
      Retreat,
      Draw,
    }

    public struct SVector2
    {
      public int x;
      public int y;

      public SVector2(int _x_, int _y_)
      {
        this.x = _x_;
        this.y = _y_;
      }

      public SVector2(BattleCore.SVector2 v)
      {
        this.x = v.x;
        this.y = v.y;
      }

      public int Length()
      {
        return BattleCore.Sqrt(this.x * this.x + this.y * this.y);
      }

      public static int DotProduct(ref BattleCore.SVector2 s, ref BattleCore.SVector2 t)
      {
        return s.x * t.x + s.y * t.y;
      }

      public override bool Equals(object obj)
      {
        if (obj is BattleCore.SVector2)
          return this == (BattleCore.SVector2) obj;
        return false;
      }

      public override int GetHashCode()
      {
        return 0;
      }

      public static BattleCore.SVector2 operator +(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x + b.x, a.y + b.y);
      }

      public static BattleCore.SVector2 operator -(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x - b.x, a.y - b.y);
      }

      public static BattleCore.SVector2 operator *(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x * b.x, a.y * b.y);
      }

      public static BattleCore.SVector2 operator *(BattleCore.SVector2 a, int mul)
      {
        return new BattleCore.SVector2(a.x * mul, a.y * mul);
      }

      public static bool operator ==(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        if (a.x == b.x)
          return a.y == b.y;
        return false;
      }

      public static bool operator !=(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        if (a.x == b.x)
          return a.y != b.y;
        return true;
      }
    }

    public class HitData
    {
      public int hp_damage;
      public int mp_damage;
      public int ch_damage;
      public int ca_damage;
      public int hp_heal;
      public int mp_heal;
      public int ch_heal;
      public int ca_heal;
      public bool is_critical;
      public bool is_avoid;

      public HitData(int _hp_damage_, int _mp_damage_, int _ch_damage_, int _ca_damage_, int _hp_heal_, int _mp_heal_, int _ch_heal_, int _ca_heal_, bool _critical_, bool _avoid_)
      {
        this.hp_damage = _hp_damage_;
        this.mp_damage = _mp_damage_;
        this.ch_damage = _ch_damage_;
        this.ca_damage = _ca_damage_;
        this.hp_heal = _hp_heal_;
        this.mp_heal = _mp_heal_;
        this.ch_heal = _ch_heal_;
        this.ca_heal = _ca_heal_;
        this.is_critical = _critical_;
        this.is_avoid = _avoid_;
      }
    }

    public class ChainUnit
    {
      public List<BattleCore.HitData> hits = new List<BattleCore.HitData>(4);
      public Unit self;
    }

    public class UnitResult
    {
      public Unit react_unit;
      public Unit unit;
      public int hp_damage;
      public int mp_damage;
      public int hp_heal;
      public int mp_heal;
      public int avoid;
      public int critical;
      public int reaction;
    }

    public class CommandResult
    {
      public Unit self;
      public SkillData skill;
      public BattleCore.UnitResult self_effect;
      public List<BattleCore.UnitResult> targets;
      public List<BattleCore.UnitResult> reactions;
    }

    private class ShotTarget
    {
      public List<Unit> piercers = new List<Unit>();
      public Grid end;
      public double rad;
      public double height;
    }

    public enum SuspendTiming
    {
      MapStart,
      UnitStart,
      Wait,
      Move,
      UseSkill,
      UseItem,
      UnitEnd,
      CastSkillStart,
      CastSkillEnd,
      Continued,
      AI,
    }

    public class SuspendData
    {
      public string skill = string.Empty;
      public string item = string.Empty;
      public BattleCore.SuspendTiming timing;
      public uint seed;
      public uint rnd;
      public long uid;
      public int x;
      public int y;
      public int dir;
      public int tx;
      public int ty;
      public int locked;
      public int ux;
      public int uy;
    }

    private class SuspendLog
    {
      public int mIndex;
      public string mSkillID;
      public string mUnitID;

      public SuspendLog(int idx, string skill_id, string unit_id)
      {
        this.mIndex = idx;
        this.mSkillID = skill_id;
        this.mUnitID = unit_id;
      }
    }

    public enum eArenaCalcType
    {
      UNKNOWN,
      MAP_START,
      UNIT_START,
      AI,
      UNIT_END,
      CAST_SKILL_START,
      CAST_SKILL_END,
      MAP_END,
    }

    public class SkillResult
    {
      public SkillData skill;
      public Grid movpos;
      public Grid usepos;
      public LogSkill log;
      public bool locked;
      public int unit_prio;
      public int cost_jewel;
      public int gain_jewel;
      public int heal;
      public int heal_num;
      public int cure_num;
      public int fail_num;
      public int disable_num;
      public int cond_prio;
      public int buff;
      public int buff_num;
      public int buff_prio;
      public int damage;
      public int dead_num;
      public int distance;
      public int ct;
    }

    public delegate void LogCallback(string s);
  }
}
