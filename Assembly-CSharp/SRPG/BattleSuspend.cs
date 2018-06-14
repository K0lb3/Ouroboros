// Decompiled with JetBrains decompiler
// Type: SRPG.BattleSuspend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class BattleSuspend
  {
    private const string SUSPEND_FILENAME = "new_suspend.bin";

    private static string mSuspendFileName
    {
      get
      {
        return AppPath.persistentDataPath + "/new_suspend.bin";
      }
    }

    private static bool writeSaveData(string file_name, BattleSuspend.Data data = null)
    {
      if (string.IsNullOrEmpty(file_name))
        return false;
      if (data == null)
        data = new BattleSuspend.Data();
      try
      {
        string json = JsonUtility.ToJson((object) data);
        if (!string.IsNullOrEmpty(json))
        {
          byte[] bytes = MyEncrypt.Encrypt(0, json, false);
          if (bytes != null)
          {
            File.WriteAllBytes(file_name, bytes);
            return true;
          }
        }
      }
      catch
      {
      }
      return false;
    }

    private static BattleSuspend.Data loadSaveData(string file_name)
    {
      if (string.IsNullOrEmpty(file_name))
        return (BattleSuspend.Data) null;
      try
      {
        byte[] data = File.ReadAllBytes(file_name);
        if (data != null)
        {
          string str = MyEncrypt.Decrypt(0, data, false);
          if (!string.IsNullOrEmpty(str))
            return (BattleSuspend.Data) JsonUtility.FromJson<BattleSuspend.Data>(str);
        }
      }
      catch
      {
      }
      return (BattleSuspend.Data) null;
    }

    private static BattleSuspend.Data makeSaveData()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return (BattleSuspend.Data) null;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return (BattleSuspend.Data) null;
      BattleSuspend.Data data = new BattleSuspend.Data();
      BattleSuspend.Data.Header hdr = data.hdr;
      hdr.apv = Application.get_version();
      hdr.arv = AssetManager.AssetRevision;
      hdr.qid = battle.QuestID;
      hdr.bid = battle.BtlID;
      hdr.cat = GameUtility.Config_AutoMode_Treasure.Value;
      hdr.cad = GameUtility.Config_AutoMode_DisableSkill.Value;
      data.uil.Clear();
      using (List<Unit>.Enumerator enumerator1 = battle.AllUnits.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          Unit current1 = enumerator1.Current;
          if (current1 != null)
          {
            BattleSuspend.Data.UnitInfo unitInfo = new BattleSuspend.Data.UnitInfo();
            unitInfo.nam = current1.UnitName;
            unitInfo.nhp = (int) current1.CurrentStatus.param.hp;
            unitInfo.gem = current1.Gems;
            unitInfo.ugx = current1.x;
            unitInfo.ugy = current1.y;
            unitInfo.dir = (int) current1.Direction;
            unitInfo.ufg = current1.UnitFlag;
            unitInfo.isb = current1.IsSub;
            unitInfo.crt = (int) current1.ChargeTime;
            unitInfo.tgi = BattleSuspend.GetIdxFromAllUnits(battle, current1.Target);
            unitInfo.rti = BattleSuspend.GetIdxFromAllUnits(battle, current1.RageTarget);
            unitInfo.cti = -1;
            if (current1.CastSkill != null)
            {
              unitInfo.csi = current1.CastSkill.SkillParam.iname;
              unitInfo.ctm = (int) current1.CastTime;
              unitInfo.cid = (int) current1.CastIndex;
              if (current1.CastSkillGridMap != null)
              {
                unitInfo.cgw = current1.CastSkillGridMap.w;
                unitInfo.cgh = current1.CastSkillGridMap.h;
                if (current1.CastSkillGridMap.data != null)
                {
                  unitInfo.cgm = new int[current1.CastSkillGridMap.data.Length];
                  for (int index = 0; index < current1.CastSkillGridMap.data.Length; ++index)
                    unitInfo.cgm[index] = !current1.CastSkillGridMap.data[index] ? 0 : 1;
                }
              }
              unitInfo.ctx = current1.GridTarget == null ? -1 : current1.GridTarget.x;
              unitInfo.cty = current1.GridTarget == null ? -1 : current1.GridTarget.y;
              unitInfo.cti = BattleSuspend.GetIdxFromAllUnits(battle, current1.UnitTarget);
            }
            unitInfo.dct = current1.DeathCount;
            unitInfo.ajw = current1.AutoJewel;
            unitInfo.wtt = current1.WaitClock;
            unitInfo.mvt = current1.WaitMoveTurn;
            unitInfo.acc = current1.ActionCount;
            unitInfo.tuc = current1.TurnCount;
            unitInfo.trc = current1.EventTrigger == null ? 0 : current1.EventTrigger.Count;
            unitInfo.klc = current1.KillCount;
            if (current1.EntryTriggers != null)
            {
              unitInfo.etr = new int[current1.EntryTriggers.Count];
              for (int index = 0; index < current1.EntryTriggers.Count; ++index)
                unitInfo.etr[index] = !current1.EntryTriggers[index].on ? 0 : 1;
            }
            unitInfo.aid = (int) current1.AIActionIndex;
            unitInfo.atu = (int) current1.AIActionTurnCount;
            unitInfo.apt = (int) current1.AIPatrolIndex;
            unitInfo.boi = current1.CreateBreakObjId;
            unitInfo.boc = current1.CreateBreakObjClock;
            unitInfo.sul.Clear();
            using (Dictionary<SkillData, OInt>.Enumerator enumerator2 = current1.GetSkillUseCount().GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                KeyValuePair<SkillData, OInt> current2 = enumerator2.Current;
                unitInfo.sul.Add(new BattleSuspend.Data.UnitInfo.SkillUse()
                {
                  sid = current2.Key.SkillParam.iname,
                  ctr = (int) current2.Value
                });
              }
            }
            unitInfo.bfl.Clear();
            using (List<BuffAttachment>.Enumerator enumerator2 = current1.BuffAttachments.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                BuffAttachment current2 = enumerator2.Current;
                if ((!(bool) current2.IsPassive || current2.skill != null && !current2.skill.IsSubActuate()) && current2.CheckTiming != EffectCheckTimings.Moment)
                  unitInfo.bfl.Add(new BattleSuspend.Data.UnitInfo.Buff()
                  {
                    sid = current2.skill == null ? (string) null : current2.skill.SkillParam.iname,
                    stg = (int) current2.skilltarget,
                    tur = (int) current2.turn,
                    uni = BattleSuspend.GetIdxFromAllUnits(battle, current2.user),
                    cui = BattleSuspend.GetIdxFromAllUnits(battle, current2.CheckTarget),
                    tim = (int) current2.CheckTiming,
                    ipa = (bool) current2.IsPassive,
                    ucd = (int) current2.UseCondition,
                    btp = (int) current2.BuffType,
                    ctp = (int) current2.CalcType
                  });
              }
            }
            unitInfo.cdl.Clear();
            using (List<CondAttachment>.Enumerator enumerator2 = current1.CondAttachments.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                CondAttachment current2 = enumerator2.Current;
                if (!(bool) current2.IsPassive || current2.skill != null && !current2.skill.IsSubActuate())
                  unitInfo.cdl.Add(new BattleSuspend.Data.UnitInfo.Cond()
                  {
                    sid = current2.skill == null ? (string) null : current2.skill.SkillParam.iname,
                    tur = (int) current2.turn,
                    uni = BattleSuspend.GetIdxFromAllUnits(battle, current2.user),
                    cui = BattleSuspend.GetIdxFromAllUnits(battle, current2.CheckTarget),
                    tim = (int) current2.CheckTiming,
                    ipa = (bool) current2.IsPassive,
                    ucd = (int) current2.UseCondition,
                    cdt = (int) current2.CondType,
                    cnd = (int) current2.Condition,
                    icu = current2.IsCurse
                  });
              }
            }
            unitInfo.shl.Clear();
            using (List<Unit.UnitShield>.Enumerator enumerator2 = current1.Shields.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                Unit.UnitShield current2 = enumerator2.Current;
                unitInfo.shl.Add(new BattleSuspend.Data.UnitInfo.Shield()
                {
                  inm = current2.skill_param.iname,
                  nhp = (int) current2.hp,
                  mhp = (int) current2.hpMax,
                  ntu = (int) current2.turn,
                  mtu = (int) current2.turnMax,
                  drt = (int) current2.damage_rate,
                  dvl = (int) current2.damage_value
                });
              }
            }
            data.uil.Add(unitInfo);
          }
        }
      }
      data.itl.Clear();
      using (Dictionary<OString, OInt>.Enumerator enumerator = battle.GetQuestRecord().used_items.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<OString, OInt> current = enumerator.Current;
          data.itl.Add(new BattleSuspend.Data.UsedItem()
          {
            iti = (string) current.Key,
            num = (int) current.Value
          });
        }
      }
      data.trl.Clear();
      using (List<TrickData>.Enumerator enumerator = TrickData.GetEffectAll().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TrickData current = enumerator.Current;
          data.trl.Add(new BattleSuspend.Data.TrickInfo()
          {
            tid = current.TrickParam.Iname,
            val = (bool) current.Valid,
            cun = BattleSuspend.GetIdxFromAllUnits(battle, current.CreateUnit),
            rnk = (int) current.Rank,
            rcp = (int) current.RankCap,
            grx = (int) current.GridX,
            gry = (int) current.GridY,
            rac = (int) current.RestActionCount,
            ccl = (int) current.CreateClock,
            tag = current.Tag
          });
        }
      }
      data.sel.Clear();
      using (Dictionary<string, BattleCore.SkillExecLog>.KeyCollection.Enumerator enumerator = battle.SkillExecLogs.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          data.sel.Add(new BattleSuspend.Data.SkillExecLogInfo()
          {
            inm = current,
            ucnt = battle.SkillExecLogs[current].use_count,
            kcnt = battle.SkillExecLogs[current].kill_count
          });
        }
      }
      BattleSuspend.Data.Variables var = data.var;
      var.wtc = battle.WinTriggerCount;
      var.ltc = battle.LoseTriggerCount;
      var.act = battle.ActionCount;
      var.kls = battle.Killstreak;
      var.mks = battle.MaxKillstreak;
      var.thl = battle.TotalHeal;
      var.tdt = battle.TotalDamagesTaken;
      var.tdm = battle.TotalDamages;
      var.nui = battle.NumUsedItems;
      var.nus = battle.NumUsedSkills;
      var.ctm = battle.ClockTime;
      var.ctt = battle.ClockTimeTotal;
      var.coc = battle.ContinueCount;
      var.fns = battle.FinisherIname;
      var.glc = instance.GoldCount;
      var.trc = instance.TreasureCount;
      var.rsd = battle.Seed;
      uint[] seed = battle.Rand.GetSeed();
      if (seed != null)
      {
        var.ris = new uint[seed.Length];
        seed.CopyTo((Array) var.ris, 0);
      }
      var.gsl.Clear();
      using (List<GimmickEvent>.Enumerator enumerator = battle.GimmickEventList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GimmickEvent current = enumerator.Current;
          var.gsl.Add(new BattleSuspend.Data.Variables.GimmickEvent()
          {
            ctr = current.count,
            cmp = !current.IsCompleted ? 0 : 1
          });
        }
      }
      var.ssl.Clear();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance.EventScript, (UnityEngine.Object) null) && instance.EventScript.mSequences != null)
      {
        foreach (EventScript.ScriptSequence mSequence in instance.EventScript.mSequences)
          var.ssl.Add(new BattleSuspend.Data.Variables.ScriptEvent()
          {
            trg = mSequence.Triggered
          });
      }
      var.tkk = battle.TargetKillstreak.Keys.ToArray<string>();
      var.tkv = battle.TargetKillstreak.Values.ToArray<int>();
      var.mtk = battle.MaxTargetKillstreak.Keys.ToArray<string>();
      var.mtv = battle.MaxTargetKillstreak.Values.ToArray<int>();
      var.pbm = battle.PlayByManually;
      var.uam = battle.IsUseAutoPlayMode;
      var.wti.wid = (string) null;
      WeatherData currentWeatherData = WeatherData.CurrentWeatherData;
      if (currentWeatherData != null)
      {
        var.wti.wid = currentWeatherData.WeatherParam.Iname;
        var.wti.mun = BattleSuspend.GetIdxFromAllUnits(battle, currentWeatherData.ModifyUnit);
        var.wti.rnk = (int) currentWeatherData.Rank;
        var.wti.rcp = (int) currentWeatherData.RankCap;
        var.wti.ccl = (int) currentWeatherData.ChangeClock;
      }
      data.ivl = true;
      return data;
    }

    private static bool restoreSaveData(BattleSuspend.Data data)
    {
      if (data == null || !data.ivl)
        return false;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return false;
      BattleSuspend.Data.Header hdr = data.hdr;
      if (hdr.apv != Application.get_version())
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! Version is different.");
        return false;
      }
      if (hdr.arv != AssetManager.AssetRevision)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! Revision is different.");
        return false;
      }
      if (hdr.qid != battle.QuestID)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! QuestID is different.");
        return false;
      }
      if (hdr.bid != battle.BtlID)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! BattleID is different.");
        return false;
      }
      GameUtility.Config_AutoMode_Treasure.Value = hdr.cat;
      GameUtility.Config_AutoMode_DisableSkill.Value = hdr.cad;
      for (int count = battle.AllUnits.Count; count < data.uil.Count; ++count)
      {
        BattleSuspend.Data.UnitInfo unitInfo = data.uil[count];
        if (!string.IsNullOrEmpty(unitInfo.boi))
          battle.BreakObjCreate(unitInfo.boi, unitInfo.ugx, unitInfo.ugy, (Unit) null, (LogSkill) null, 0);
      }
      for (int index = 0; index < data.uil.Count && index < battle.AllUnits.Count; ++index)
      {
        BattleSuspend.Data.UnitInfo unit_info = data.uil[index];
        Unit allUnit = battle.AllUnits[index];
        if (!(allUnit.UnitName != unit_info.nam))
          allUnit.SetupSuspend(battle, unit_info);
      }
      BattleCore.Record questRecord = battle.GetQuestRecord();
      using (List<BattleSuspend.Data.UsedItem>.Enumerator enumerator = data.itl.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.UsedItem current = enumerator.Current;
          questRecord.used_items[(OString) current.iti] = (OInt) current.num;
          ItemData inventoryByItemId = battle.FindInventoryByItemID(current.iti);
          if (inventoryByItemId != null)
            inventoryByItemId.Used(current.num);
        }
      }
      TrickData.ClearEffect();
      using (List<BattleSuspend.Data.TrickInfo>.Enumerator enumerator = data.trl.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.TrickInfo current = enumerator.Current;
          Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battle, current.cun);
          TrickData.SuspendEffect(current.tid, current.grx, current.gry, current.tag, unitFromAllUnits, current.ccl, current.rnk, current.rcp, current.rac);
        }
      }
      using (List<BattleSuspend.Data.SkillExecLogInfo>.Enumerator enumerator = data.sel.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.SkillExecLogInfo current = enumerator.Current;
          BattleCore.SkillExecLog skillExecLog = new BattleCore.SkillExecLog();
          skillExecLog.Restore(current);
          battle.SkillExecLogs.Add(current.inm, skillExecLog);
        }
      }
      BattleSuspend.Data.Variables var = data.var;
      battle.WinTriggerCount = var.wtc;
      battle.LoseTriggerCount = var.ltc;
      battle.ActionCount = var.act;
      battle.Killstreak = var.kls;
      battle.MaxKillstreak = var.mks;
      battle.TotalHeal = var.thl;
      battle.TotalDamagesTaken = var.tdt;
      battle.TotalDamages = var.tdm;
      battle.NumUsedItems = var.nui;
      battle.NumUsedSkills = var.nus;
      battle.ClockTime = var.ctm;
      battle.ClockTimeTotal = var.ctt;
      battle.ContinueCount = var.coc;
      battle.FinisherIname = var.fns;
      instance.GoldCount = var.glc;
      instance.RestoreTreasureCount(var.trc);
      battle.Seed = var.rsd;
      battle.PlayByManually = var.pbm;
      battle.IsUseAutoPlayMode = var.uam;
      if (var.ris != null)
      {
        for (int index = 0; index < var.ris.Length; ++index)
          battle.SetRandSeed(index, var.ris[index]);
      }
      if (var.gsl.Count == battle.GimmickEventList.Count)
      {
        for (int index = 0; index < var.gsl.Count; ++index)
        {
          BattleSuspend.Data.Variables.GimmickEvent gimmickEvent = var.gsl[index];
          battle.GimmickEventList[index].count = gimmickEvent.ctr;
          battle.GimmickEventList[index].IsCompleted = gimmickEvent.cmp != 0;
        }
      }
      if (var.ssl.Count != 0)
      {
        battle.EventTriggers = new bool[var.ssl.Count];
        for (int index = 0; index < var.ssl.Count; ++index)
        {
          BattleSuspend.Data.Variables.ScriptEvent scriptEvent = var.ssl[index];
          battle.EventTriggers[index] = scriptEvent.trg;
        }
      }
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      int num1 = Math.Min(var.tkk.Length, var.tkv.Length);
      for (int index = 0; index < num1; ++index)
        dictionary1.Add(var.tkk[index], var.tkv[index]);
      battle.TargetKillstreak = dictionary1;
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      int num2 = Math.Min(var.mtk.Length, var.mtv.Length);
      for (int index = 0; index < num2; ++index)
        dictionary2.Add(var.mtk[index], var.mtv[index]);
      battle.MaxTargetKillstreak = dictionary2;
      BattleSuspend.Data.Variables.WeatherInfo wti = var.wti;
      if (!string.IsNullOrEmpty(wti.wid))
      {
        Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battle, wti.mun);
        WeatherData.SuspendWeather(wti.wid, battle.Units, unitFromAllUnits, wti.rnk, wti.rcp, wti.ccl);
      }
      return true;
    }

    public static int GetIdxFromAllUnits(BattleCore bc, Unit unit)
    {
      if (bc == null || unit == null)
        return -1;
      for (int index = 0; index < bc.AllUnits.Count; ++index)
      {
        if (bc.AllUnits[index].Equals((object) unit))
          return index;
      }
      return -1;
    }

    public static Unit GetUnitFromAllUnits(BattleCore bc, int idx)
    {
      if (bc == null || idx < 0 || idx >= bc.AllUnits.Count)
        return (Unit) null;
      return bc.AllUnits[idx];
    }

    public static bool IsExistData()
    {
      return File.Exists(BattleSuspend.mSuspendFileName);
    }

    public static void RemoveData()
    {
      string mSuspendFileName = BattleSuspend.mSuspendFileName;
      if (!File.Exists(mSuspendFileName))
        return;
      BattleSuspend.writeSaveData(mSuspendFileName, (BattleSuspend.Data) null);
      File.Delete(mSuspendFileName);
    }

    public static bool SaveData()
    {
      BattleSuspend.Data data = BattleSuspend.makeSaveData();
      if (data == null)
        return false;
      return BattleSuspend.writeSaveData(BattleSuspend.mSuspendFileName, data);
    }

    public static bool LoadData()
    {
      BattleSuspend.Data data = BattleSuspend.loadSaveData(BattleSuspend.mSuspendFileName);
      if (data == null)
        return false;
      return BattleSuspend.restoreSaveData(data);
    }

    [Serializable]
    public class Data
    {
      public BattleSuspend.Data.Header hdr = new BattleSuspend.Data.Header();
      public List<BattleSuspend.Data.UnitInfo> uil = new List<BattleSuspend.Data.UnitInfo>();
      public List<BattleSuspend.Data.UsedItem> itl = new List<BattleSuspend.Data.UsedItem>();
      public List<BattleSuspend.Data.TrickInfo> trl = new List<BattleSuspend.Data.TrickInfo>();
      public List<BattleSuspend.Data.SkillExecLogInfo> sel = new List<BattleSuspend.Data.SkillExecLogInfo>();
      public BattleSuspend.Data.Variables var = new BattleSuspend.Data.Variables();
      public bool ivl;

      [Serializable]
      public class Header
      {
        public string apv;
        public int arv;
        public string qid;
        public long bid;
        public bool cat;
        public bool cad;
      }

      [Serializable]
      public class UnitInfo
      {
        public List<BattleSuspend.Data.UnitInfo.SkillUse> sul = new List<BattleSuspend.Data.UnitInfo.SkillUse>();
        public List<BattleSuspend.Data.UnitInfo.Buff> bfl = new List<BattleSuspend.Data.UnitInfo.Buff>();
        public List<BattleSuspend.Data.UnitInfo.Cond> cdl = new List<BattleSuspend.Data.UnitInfo.Cond>();
        public List<BattleSuspend.Data.UnitInfo.Shield> shl = new List<BattleSuspend.Data.UnitInfo.Shield>();
        public string nam;
        public int nhp;
        public int gem;
        public int ugx;
        public int ugy;
        public int dir;
        public int ufg;
        public bool isb;
        public int crt;
        public int tgi;
        public int rti;
        public string csi;
        public int ctm;
        public int cid;
        public int cti;
        public int ctx;
        public int cty;
        public int cgw;
        public int cgh;
        public int[] cgm;
        public int dct;
        public int ajw;
        public int wtt;
        public int mvt;
        public int acc;
        public int tuc;
        public int trc;
        public int klc;
        public int[] etr;
        public int aid;
        public int atu;
        public int apt;
        public string boi;
        public int boc;

        [Serializable]
        public class SkillUse
        {
          public string sid;
          public int ctr;
        }

        [Serializable]
        public class Buff
        {
          public string sid;
          public int stg;
          public int tur;
          public int uni;
          public int cui;
          public int tim;
          public bool ipa;
          public int ucd;
          public int btp;
          public int ctp;
        }

        [Serializable]
        public class Cond
        {
          public string sid;
          public int tur;
          public int uni;
          public int cui;
          public int tim;
          public bool ipa;
          public int ucd;
          public int cdt;
          public int cnd;
          public bool icu;
        }

        [Serializable]
        public class Shield
        {
          public string inm;
          public int nhp;
          public int mhp;
          public int ntu;
          public int mtu;
          public int drt;
          public int dvl;
        }
      }

      [Serializable]
      public class UsedItem
      {
        public string iti;
        public int num;
      }

      [Serializable]
      public class TrickInfo
      {
        public string tid;
        public bool val;
        public int cun;
        public int rnk;
        public int rcp;
        public int grx;
        public int gry;
        public int rac;
        public int ccl;
        public string tag;
      }

      [Serializable]
      public class SkillExecLogInfo
      {
        public string inm;
        public int ucnt;
        public int kcnt;
      }

      [Serializable]
      public class Variables
      {
        public List<BattleSuspend.Data.Variables.GimmickEvent> gsl = new List<BattleSuspend.Data.Variables.GimmickEvent>();
        public List<BattleSuspend.Data.Variables.ScriptEvent> ssl = new List<BattleSuspend.Data.Variables.ScriptEvent>();
        public BattleSuspend.Data.Variables.WeatherInfo wti = new BattleSuspend.Data.Variables.WeatherInfo();
        public int wtc;
        public int ltc;
        public int act;
        public int kls;
        public int mks;
        public string[] tkk;
        public int[] tkv;
        public string[] mtk;
        public int[] mtv;
        public bool pbm;
        public bool uam;
        public int thl;
        public int tdt;
        public int tdm;
        public int nui;
        public int nus;
        public int ctm;
        public int ctt;
        public int coc;
        public string fns;
        public int glc;
        public int trc;
        public uint rsd;
        public uint[] ris;

        [Serializable]
        public class GimmickEvent
        {
          public int ctr;
          public int cmp;
        }

        [Serializable]
        public class ScriptEvent
        {
          public bool trg;
        }

        [Serializable]
        public class WeatherInfo
        {
          public string wid;
          public int mun;
          public int rnk;
          public int rcp;
          public int ccl;
        }
      }
    }
  }
}
