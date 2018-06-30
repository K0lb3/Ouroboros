namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class BattleSuspend
    {
        private const string SUSPEND_FILENAME = "new_suspend.bin";

        public BattleSuspend()
        {
            base..ctor();
            return;
        }

        public static int GetIdxFromAllUnits(BattleCore bc, Unit unit)
        {
            int num;
            if (bc != null)
            {
                goto Label_0027;
            }
            bc = (SceneBattle.Instance == null) ? null : SceneBattle.Instance.Battle;
        Label_0027:
            if (bc == null)
            {
                goto Label_0033;
            }
            if (unit != null)
            {
                goto Label_0035;
            }
        Label_0033:
            return -1;
        Label_0035:
            num = 0;
            goto Label_0059;
        Label_003C:
            if (bc.AllUnits[num].Equals(unit) == null)
            {
                goto Label_0055;
            }
            return num;
        Label_0055:
            num += 1;
        Label_0059:
            if (num < bc.AllUnits.Count)
            {
                goto Label_003C;
            }
            return -1;
        }

        public static Unit GetUnitFromAllUnits(BattleCore bc, int idx)
        {
            if (bc != null)
            {
                goto Label_0027;
            }
            bc = (SceneBattle.Instance == null) ? null : SceneBattle.Instance.Battle;
        Label_0027:
            if (bc == null)
            {
                goto Label_0045;
            }
            if (idx < 0)
            {
                goto Label_0045;
            }
            if (idx < bc.AllUnits.Count)
            {
                goto Label_0047;
            }
        Label_0045:
            return null;
        Label_0047:
            return bc.AllUnits[idx];
        }

        public static bool IsExistData()
        {
            return File.Exists(mSuspendFileName);
        }

        public static bool LoadData()
        {
            Data data;
            data = loadSaveData(mSuspendFileName);
            if (data != null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return restoreSaveData(data);
        }

        private static Data loadSaveData(string file_name)
        {
            byte[] buffer;
            string str;
            Data data;
            if (string.IsNullOrEmpty(file_name) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            try
            {
                buffer = File.ReadAllBytes(file_name);
                if (buffer == null)
                {
                    goto Label_003A;
                }
                str = MyEncrypt.Decrypt(0, buffer, 0);
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_003A;
                }
                data = JsonUtility.FromJson<Data>(str);
                goto Label_0047;
            Label_003A:
                goto Label_0045;
            }
            catch
            {
            Label_003F:
                goto Label_0045;
            }
        Label_0045:
            return null;
        Label_0047:
            return data;
        }

        private static unsafe Data makeSaveData()
        {
            SceneBattle battle;
            BattleCore core;
            Data data;
            Data.Header header;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            Data.UnitInfo info;
            int num;
            int num2;
            int num3;
            Unit.AbilityChange change;
            Data.UnitInfo.AbilChg chg;
            int num4;
            Unit.AbilityChange.Data data2;
            Data.UnitInfo.AbilChg.Data data3;
            int num5;
            AbilityData data4;
            Data.UnitInfo.AddedAbil abil;
            KeyValuePair<SkillData, OInt> pair;
            Dictionary<SkillData, OInt>.Enumerator enumerator2;
            Data.UnitInfo.SkillUse use;
            BuffAttachment attachment;
            List<BuffAttachment>.Enumerator enumerator3;
            Data.UnitInfo.Buff buff;
            int num6;
            int num7;
            CondAttachment attachment2;
            List<CondAttachment>.Enumerator enumerator4;
            Data.UnitInfo.Cond cond;
            Unit.UnitShield shield;
            List<Unit.UnitShield>.Enumerator enumerator5;
            Data.UnitInfo.Shield shield2;
            SkillData data5;
            List<SkillData>.Enumerator enumerator6;
            Unit.UnitMhmDamage damage;
            List<Unit.UnitMhmDamage>.Enumerator enumerator7;
            Data.UnitInfo.MhmDmg dmg;
            BattleCore.Record record;
            KeyValuePair<OString, OInt> pair2;
            Dictionary<OString, OInt>.Enumerator enumerator8;
            Data.UsedItem item;
            List<TrickData> list;
            TrickData data6;
            List<TrickData>.Enumerator enumerator9;
            Data.TrickInfo info2;
            string str;
            Dictionary<string, BattleCore.SkillExecLog>.KeyCollection.Enumerator enumerator10;
            Data.SkillExecLogInfo info3;
            Data.Variables variables;
            uint[] numArray;
            GimmickEvent event2;
            List<GimmickEvent>.Enumerator enumerator11;
            Data.Variables.GimmickEvent event3;
            EventScript.ScriptSequence sequence;
            EventScript.ScriptSequence[] sequenceArray;
            int num8;
            Data.Variables.ScriptEvent event4;
            WeatherData data7;
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0022;
            }
            return null;
        Label_0022:
            data = new Data();
            header = data.hdr;
            header.apv = Application.get_version();
            header.arv = AssetManager.AssetRevision;
            header.qid = core.QuestID;
            header.bid = core.BtlID;
            header.cat = GameUtility.Config_AutoMode_Treasure.Value;
            header.cad = GameUtility.Config_AutoMode_DisableSkill.Value;
            data.uil.Clear();
            enumerator = core.AllUnits.GetEnumerator();
        Label_0095:
            try
            {
                goto Label_0BC7;
            Label_009A:
                unit = &enumerator.Current;
                if (unit != null)
                {
                    goto Label_00AF;
                }
                goto Label_0BC7;
            Label_00AF:
                info = new Data.UnitInfo();
                info.nam = unit.UnitName;
                info.nhp = unit.CurrentStatus.param.hp;
                info.chp = unit.UnitChangedHp;
                info.gem = unit.Gems;
                info.ugx = unit.x;
                info.ugy = unit.y;
                info.dir = unit.Direction;
                info.ufg = unit.UnitFlag;
                info.isb = unit.IsSub;
                info.crt = unit.ChargeTime;
                info.tgi = GetIdxFromAllUnits(core, unit.Target);
                info.rti = GetIdxFromAllUnits(core, unit.RageTarget);
                info.cti = -1;
                if (unit.CastSkill == null)
                {
                    goto Label_02D3;
                }
                info.csi = unit.CastSkill.SkillParam.iname;
                info.ctm = unit.CastTime;
                info.cid = unit.CastIndex;
                if (unit.CastSkillGridMap == null)
                {
                    goto Label_0275;
                }
                info.cgw = unit.CastSkillGridMap.w;
                info.cgh = unit.CastSkillGridMap.h;
                if (unit.CastSkillGridMap.data == null)
                {
                    goto Label_0275;
                }
                info.cgm = new int[(int) unit.CastSkillGridMap.data.Length];
                num = 0;
                goto Label_0260;
            Label_0235:
                info.cgm[num] = (unit.CastSkillGridMap.data[num] == null) ? 0 : 1;
                num += 1;
            Label_0260:
                if (num < ((int) unit.CastSkillGridMap.data.Length))
                {
                    goto Label_0235;
                }
            Label_0275:
                info.ctx = (unit.GridTarget == null) ? -1 : unit.GridTarget.x;
                info.cty = (unit.GridTarget == null) ? -1 : unit.GridTarget.y;
                info.cti = GetIdxFromAllUnits(core, unit.UnitTarget);
            Label_02D3:
                info.dct = unit.DeathCount;
                info.ajw = unit.AutoJewel;
                info.wtt = unit.WaitClock;
                info.mvt = unit.WaitMoveTurn;
                info.acc = unit.ActionCount;
                info.tuc = unit.TurnCount;
                info.trc = (unit.EventTrigger == null) ? 0 : unit.EventTrigger.Count;
                info.klc = unit.KillCount;
                if (unit.EntryTriggers == null)
                {
                    goto Label_03C8;
                }
                info.etr = new int[unit.EntryTriggers.Count];
                num2 = 0;
                goto Label_03B5;
            Label_0386:
                info.etr[num2] = (unit.EntryTriggers[num2].on == null) ? 0 : 1;
                num2 += 1;
            Label_03B5:
                if (num2 < unit.EntryTriggers.Count)
                {
                    goto Label_0386;
                }
            Label_03C8:
                info.aid = unit.AIActionIndex;
                info.atu = unit.AIActionTurnCount;
                info.apt = unit.AIPatrolIndex;
                info.boi = unit.CreateBreakObjId;
                info.boc = unit.CreateBreakObjClock;
                info.tid = unit.TeamId;
                info.fst = unit.FriendStates;
                info.acl.Clear();
                num3 = 0;
                goto Label_0551;
            Label_044D:
                change = unit.AbilityChangeLists[num3];
                if (change == null)
                {
                    goto Label_054B;
                }
                if (change.mDataLists.Count != null)
                {
                    goto Label_047A;
                }
                goto Label_054B;
            Label_047A:
                chg = new Data.UnitInfo.AbilChg();
                num4 = 0;
                goto Label_052A;
            Label_0489:
                data2 = change.mDataLists[num4];
                data3 = new Data.UnitInfo.AbilChg.Data();
                data3.fid = data2.mFromAp.iname;
                data3.tid = data2.mToAp.iname;
                data3.tur = data2.mTurn;
                data3.irs = (data2.mIsReset == null) ? 0 : 1;
                data3.exp = data2.mExp;
                data3.iif = (data2.mIsInfinite == null) ? 0 : 1;
                chg.acd.Add(data3);
                num4 += 1;
            Label_052A:
                if (num4 < change.mDataLists.Count)
                {
                    goto Label_0489;
                }
                info.acl.Add(chg);
            Label_054B:
                num3 += 1;
            Label_0551:
                if (num3 < unit.AbilityChangeLists.Count)
                {
                    goto Label_044D;
                }
                info.aal.Clear();
                num5 = 0;
                goto Label_05CB;
            Label_0578:
                data4 = unit.AddedAbilitys[num5];
                if (data4 != null)
                {
                    goto Label_0594;
                }
                goto Label_05C5;
            Label_0594:
                abil = new Data.UnitInfo.AddedAbil();
                abil.aid = data4.AbilityID;
                abil.exp = data4.Exp;
                info.aal.Add(abil);
            Label_05C5:
                num5 += 1;
            Label_05CB:
                if (num5 < unit.AddedAbilitys.Count)
                {
                    goto Label_0578;
                }
                info.sul.Clear();
                enumerator2 = unit.GetSkillUseCount().GetEnumerator();
            Label_05F8:
                try
                {
                    goto Label_0646;
                Label_05FD:
                    pair = &enumerator2.Current;
                    use = new Data.UnitInfo.SkillUse();
                    use.sid = &pair.Key.SkillParam.iname;
                    use.ctr = &pair.Value;
                    info.sul.Add(use);
                Label_0646:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_05FD;
                    }
                    goto Label_0664;
                }
                finally
                {
                Label_0657:
                    ((Dictionary<SkillData, OInt>.Enumerator) enumerator2).Dispose();
                }
            Label_0664:
                info.bfl.Clear();
                enumerator3 = unit.BuffAttachments.GetEnumerator();
            Label_067E:
                try
                {
                    goto Label_086B;
                Label_0683:
                    attachment = &enumerator3.Current;
                    if ((attachment.IsPassive == null) || ((attachment.Param != null) && (attachment.Param.mIsUpBuff != null)))
                    {
                        goto Label_06E6;
                    }
                    if (attachment.skill != null)
                    {
                        goto Label_06D0;
                    }
                    goto Label_086B;
                Label_06D0:
                    if (attachment.skill.IsSubActuate() == null)
                    {
                        goto Label_06E6;
                    }
                    goto Label_086B;
                Label_06E6:
                    if (attachment.CheckTiming != 7)
                    {
                        goto Label_06F8;
                    }
                    goto Label_086B;
                Label_06F8:
                    buff = new Data.UnitInfo.Buff();
                    buff.sid = (attachment.skill == null) ? null : attachment.skill.SkillParam.iname;
                    buff.stg = attachment.skilltarget;
                    buff.tur = attachment.turn;
                    buff.uni = GetIdxFromAllUnits(core, attachment.user);
                    buff.cui = GetIdxFromAllUnits(core, attachment.CheckTarget);
                    buff.tim = attachment.CheckTiming;
                    buff.ipa = attachment.IsPassive;
                    buff.ucd = attachment.UseCondition;
                    buff.btp = attachment.BuffType;
                    buff.vtp = (attachment.IsNegativeValueIsBuff == null) ? 0 : 1;
                    buff.ctp = attachment.CalcType;
                    buff.lid = attachment.LinkageID;
                    buff.ubc = attachment.UpBuffCount;
                    buff.atl.Clear();
                    if (attachment.AagTargetLists == null)
                    {
                        goto Label_085D;
                    }
                    num6 = 0;
                    goto Label_084A;
                Label_0818:
                    num7 = GetIdxFromAllUnits(core, attachment.AagTargetLists[num6]);
                    if (num7 < 0)
                    {
                        goto Label_0844;
                    }
                    buff.atl.Add(num7);
                Label_0844:
                    num6 += 1;
                Label_084A:
                    if (num6 < attachment.AagTargetLists.Count)
                    {
                        goto Label_0818;
                    }
                Label_085D:
                    info.bfl.Add(buff);
                Label_086B:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_0683;
                    }
                    goto Label_0889;
                }
                finally
                {
                Label_087C:
                    ((List<BuffAttachment>.Enumerator) enumerator3).Dispose();
                }
            Label_0889:
                info.cdl.Clear();
                enumerator4 = unit.CondAttachments.GetEnumerator();
            Label_08A3:
                try
                {
                    goto Label_09E7;
                Label_08A8:
                    attachment2 = &enumerator4.Current;
                    if (attachment2.IsPassive == null)
                    {
                        goto Label_08E9;
                    }
                    if (attachment2.skill != null)
                    {
                        goto Label_08D3;
                    }
                    goto Label_09E7;
                Label_08D3:
                    if (attachment2.skill.IsSubActuate() == null)
                    {
                        goto Label_08E9;
                    }
                    goto Label_09E7;
                Label_08E9:
                    cond = new Data.UnitInfo.Cond();
                    cond.sid = (attachment2.skill == null) ? null : attachment2.skill.SkillParam.iname;
                    cond.stg = attachment2.skilltarget;
                    cond.cid = attachment2.CondId;
                    cond.tur = attachment2.turn;
                    cond.uni = GetIdxFromAllUnits(core, attachment2.user);
                    cond.cui = GetIdxFromAllUnits(core, attachment2.CheckTarget);
                    cond.tim = attachment2.CheckTiming;
                    cond.ipa = attachment2.IsPassive;
                    cond.ucd = attachment2.UseCondition;
                    cond.cdt = attachment2.CondType;
                    cond.cnd = (int) attachment2.Condition;
                    cond.icu = attachment2.IsCurse;
                    cond.lid = attachment2.LinkageID;
                    info.cdl.Add(cond);
                Label_09E7:
                    if (&enumerator4.MoveNext() != null)
                    {
                        goto Label_08A8;
                    }
                    goto Label_0A05;
                }
                finally
                {
                Label_09F8:
                    ((List<CondAttachment>.Enumerator) enumerator4).Dispose();
                }
            Label_0A05:
                info.shl.Clear();
                enumerator5 = unit.Shields.GetEnumerator();
            Label_0A1F:
                try
                {
                    goto Label_0AC7;
                Label_0A24:
                    shield = &enumerator5.Current;
                    shield2 = new Data.UnitInfo.Shield();
                    shield2.inm = shield.skill_param.iname;
                    shield2.nhp = shield.hp;
                    shield2.mhp = shield.hpMax;
                    shield2.ntu = shield.turn;
                    shield2.mtu = shield.turnMax;
                    shield2.drt = shield.damage_rate;
                    shield2.dvl = shield.damage_value;
                    info.shl.Add(shield2);
                Label_0AC7:
                    if (&enumerator5.MoveNext() != null)
                    {
                        goto Label_0A24;
                    }
                    goto Label_0AE5;
                }
                finally
                {
                Label_0AD8:
                    ((List<Unit.UnitShield>.Enumerator) enumerator5).Dispose();
                }
            Label_0AE5:
                info.hpi.Clear();
                enumerator6 = unit.JudgeHpLists.GetEnumerator();
            Label_0AFF:
                try
                {
                    goto Label_0B20;
                Label_0B04:
                    data5 = &enumerator6.Current;
                    info.hpi.Add(data5.SkillID);
                Label_0B20:
                    if (&enumerator6.MoveNext() != null)
                    {
                        goto Label_0B04;
                    }
                    goto Label_0B3E;
                }
                finally
                {
                Label_0B31:
                    ((List<SkillData>.Enumerator) enumerator6).Dispose();
                }
            Label_0B3E:
                info.mhl.Clear();
                enumerator7 = unit.MhmDamageLists.GetEnumerator();
            Label_0B58:
                try
                {
                    goto Label_0B9C;
                Label_0B5D:
                    damage = &enumerator7.Current;
                    dmg = new Data.UnitInfo.MhmDmg();
                    dmg.typ = damage.mType;
                    dmg.dmg = damage.mDamage;
                    info.mhl.Add(dmg);
                Label_0B9C:
                    if (&enumerator7.MoveNext() != null)
                    {
                        goto Label_0B5D;
                    }
                    goto Label_0BBA;
                }
                finally
                {
                Label_0BAD:
                    ((List<Unit.UnitMhmDamage>.Enumerator) enumerator7).Dispose();
                }
            Label_0BBA:
                data.uil.Add(info);
            Label_0BC7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_009A;
                }
                goto Label_0BE5;
            }
            finally
            {
            Label_0BD8:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0BE5:
            data.itl.Clear();
            enumerator8 = core.GetQuestRecord().used_items.GetEnumerator();
        Label_0C06:
            try
            {
                goto Label_0C4E;
            Label_0C0B:
                pair2 = &enumerator8.Current;
                item = new Data.UsedItem();
                item.iti = &pair2.Key;
                item.num = &pair2.Value;
                data.itl.Add(item);
            Label_0C4E:
                if (&enumerator8.MoveNext() != null)
                {
                    goto Label_0C0B;
                }
                goto Label_0C6C;
            }
            finally
            {
            Label_0C5F:
                ((Dictionary<OString, OInt>.Enumerator) enumerator8).Dispose();
            }
        Label_0C6C:
            data.trl.Clear();
            enumerator9 = TrickData.GetEffectAll().GetEnumerator();
        Label_0C87:
            try
            {
                goto Label_0D63;
            Label_0C8C:
                data6 = &enumerator9.Current;
                info2 = new Data.TrickInfo();
                info2.tid = data6.TrickParam.Iname;
                info2.val = data6.Valid;
                info2.cun = GetIdxFromAllUnits(core, data6.CreateUnit);
                info2.rnk = data6.Rank;
                info2.rcp = data6.RankCap;
                info2.grx = data6.GridX;
                info2.gry = data6.GridY;
                info2.rac = data6.RestActionCount;
                info2.ccl = data6.CreateClock;
                info2.tag = data6.Tag;
                data.trl.Add(info2);
            Label_0D63:
                if (&enumerator9.MoveNext() != null)
                {
                    goto Label_0C8C;
                }
                goto Label_0D81;
            }
            finally
            {
            Label_0D74:
                ((List<TrickData>.Enumerator) enumerator9).Dispose();
            }
        Label_0D81:
            data.sel.Clear();
            enumerator10 = core.SkillExecLogs.Keys.GetEnumerator();
        Label_0D9E:
            try
            {
                goto Label_0DFB;
            Label_0DA3:
                str = &enumerator10.Current;
                info3 = new Data.SkillExecLogInfo();
                info3.inm = str;
                info3.ucnt = core.SkillExecLogs[str].use_count;
                info3.kcnt = core.SkillExecLogs[str].kill_count;
                data.sel.Add(info3);
            Label_0DFB:
                if (&enumerator10.MoveNext() != null)
                {
                    goto Label_0DA3;
                }
                goto Label_0E19;
            }
            finally
            {
            Label_0E0C:
                ((Dictionary<string, BattleCore.SkillExecLog>.KeyCollection.Enumerator) enumerator10).Dispose();
            }
        Label_0E19:
            variables = data.var;
            variables.wtc = core.WinTriggerCount;
            variables.ltc = core.LoseTriggerCount;
            variables.act = core.ActionCount;
            variables.kls = core.Killstreak;
            variables.mks = core.MaxKillstreak;
            variables.thl = core.TotalHeal;
            variables.tdt = core.TotalDamagesTaken;
            variables.tdm = core.TotalDamages;
            variables.nui = core.NumUsedItems;
            variables.nus = core.NumUsedSkills;
            variables.ctm = core.ClockTime;
            variables.ctt = core.ClockTimeTotal;
            variables.coc = core.ContinueCount;
            variables.fns = core.FinisherIname;
            variables.glc = battle.GoldCount;
            variables.trc = battle.TreasureCount;
            variables.rsd = core.Seed;
            numArray = core.Rand.GetSeed();
            if (numArray == null)
            {
                goto Label_0F31;
            }
            variables.ris = new uint[(int) numArray.Length];
            numArray.CopyTo(variables.ris, 0);
        Label_0F31:
            variables.gsl.Clear();
            enumerator11 = core.GimmickEventList.GetEnumerator();
        Label_0F4A:
            try
            {
                goto Label_0F95;
            Label_0F4F:
                event2 = &enumerator11.Current;
                event3 = new Data.Variables.GimmickEvent();
                event3.ctr = event2.count;
                event3.cmp = (event2.IsCompleted == null) ? 0 : 1;
                variables.gsl.Add(event3);
            Label_0F95:
                if (&enumerator11.MoveNext() != null)
                {
                    goto Label_0F4F;
                }
                goto Label_0FB3;
            }
            finally
            {
            Label_0FA6:
                ((List<GimmickEvent>.Enumerator) enumerator11).Dispose();
            }
        Label_0FB3:
            variables.ssl.Clear();
            if ((battle.EventScript != null) == null)
            {
                goto Label_1030;
            }
            if (battle.EventScript.mSequences == null)
            {
                goto Label_1030;
            }
            sequenceArray = battle.EventScript.mSequences;
            num8 = 0;
            goto Label_1025;
        Label_0FF5:
            sequence = sequenceArray[num8];
            event4 = new Data.Variables.ScriptEvent();
            event4.trg = sequence.Triggered;
            variables.ssl.Add(event4);
            num8 += 1;
        Label_1025:
            if (num8 < ((int) sequenceArray.Length))
            {
                goto Label_0FF5;
            }
        Label_1030:
            variables.tkk = Enumerable.ToArray<string>(core.TargetKillstreak.Keys);
            variables.tkv = Enumerable.ToArray<int>(core.TargetKillstreak.Values);
            variables.mtk = Enumerable.ToArray<string>(core.MaxTargetKillstreak.Keys);
            variables.mtv = Enumerable.ToArray<int>(core.MaxTargetKillstreak.Values);
            variables.pbm = core.PlayByManually;
            variables.uam = core.IsUseAutoPlayMode;
            variables.wti.wid = null;
            data7 = WeatherData.CurrentWeatherData;
            if (data7 == null)
            {
                goto Label_113A;
            }
            variables.wti.wid = data7.WeatherParam.Iname;
            variables.wti.mun = GetIdxFromAllUnits(core, data7.ModifyUnit);
            variables.wti.rnk = data7.Rank;
            variables.wti.rcp = data7.RankCap;
            variables.wti.ccl = data7.ChangeClock;
        Label_113A:
            variables.ctd = core.CurrentTeamId;
            variables.mtd = core.MaxTeamId;
            variables.pbd = battle.EventPlayBgmID;
            data.ivl = 1;
            return data;
        }

        public static void RemoveData()
        {
            string str;
            str = mSuspendFileName;
            if (File.Exists(str) == null)
            {
                goto Label_001F;
            }
            writeSaveData(str, null);
            File.Delete(str);
        Label_001F:
            return;
        }

        private static unsafe bool restoreSaveData(Data data)
        {
            SceneBattle battle;
            BattleCore core;
            Data.Header header;
            int num;
            Data.UnitInfo info;
            int num2;
            Data.UnitInfo info2;
            Unit unit;
            BattleCore.Record record;
            Data.UsedItem item;
            List<Data.UsedItem>.Enumerator enumerator;
            ItemData data2;
            Data.TrickInfo info3;
            List<Data.TrickInfo>.Enumerator enumerator2;
            Unit unit2;
            Data.SkillExecLogInfo info4;
            List<Data.SkillExecLogInfo>.Enumerator enumerator3;
            BattleCore.SkillExecLog log;
            Data.Variables variables;
            int num3;
            int num4;
            Data.Variables.GimmickEvent event2;
            int num5;
            Data.Variables.ScriptEvent event3;
            Dictionary<string, int> dictionary;
            int num6;
            int num7;
            Dictionary<string, int> dictionary2;
            int num8;
            int num9;
            Data.Variables.WeatherInfo info5;
            Unit unit3;
            if (data != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (data.ivl != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0037;
            }
            return 0;
        Label_0037:
            header = data.hdr;
            if ((header.apv != Application.get_version()) == null)
            {
                goto Label_005F;
            }
            DebugUtility.LogWarning("BattleSuspend/Restoration failure! Version is different.");
            return 0;
        Label_005F:
            if (header.arv == AssetManager.AssetRevision)
            {
                goto Label_007B;
            }
            DebugUtility.LogWarning("BattleSuspend/Restoration failure! Revision is different.");
            return 0;
        Label_007B:
            if ((header.qid != core.QuestID) == null)
            {
                goto Label_009D;
            }
            DebugUtility.LogWarning("BattleSuspend/Restoration failure! QuestID is different.");
            return 0;
        Label_009D:
            if (header.bid == core.BtlID)
            {
                goto Label_00BA;
            }
            DebugUtility.LogWarning("BattleSuspend/Restoration failure! BattleID is different.");
            return 0;
        Label_00BA:
            GameUtility.Config_AutoMode_Treasure.Value = header.cat;
            GameUtility.Config_AutoMode_DisableSkill.Value = header.cad;
            num = core.AllUnits.Count;
            goto Label_0132;
        Label_00EB:
            info = data.uil[num];
            if (string.IsNullOrEmpty(info.boi) == null)
            {
                goto Label_010F;
            }
            goto Label_012E;
        Label_010F:
            core.BreakObjCreate(info.boi, info.ugx, info.ugy, null, null, 0);
        Label_012E:
            num += 1;
        Label_0132:
            if (num < data.uil.Count)
            {
                goto Label_00EB;
            }
            if (core.IsOrdeal == null)
            {
                goto Label_015F;
            }
            core.OrdealRestore(data.var.ctd);
        Label_015F:
            num2 = 0;
            goto Label_01CA;
        Label_0167:
            if (num2 < core.AllUnits.Count)
            {
                goto Label_017E;
            }
            goto Label_01DC;
        Label_017E:
            info2 = data.uil[num2];
            unit = core.AllUnits[num2];
            if ((unit.UnitName != info2.nam) == null)
            {
                goto Label_01B9;
            }
            goto Label_01C4;
        Label_01B9:
            unit.SetupSuspend(core, info2);
        Label_01C4:
            num2 += 1;
        Label_01CA:
            if (num2 < data.uil.Count)
            {
                goto Label_0167;
            }
        Label_01DC:
            record = core.GetQuestRecord();
            enumerator = data.itl.GetEnumerator();
        Label_01F1:
            try
            {
                goto Label_024C;
            Label_01F6:
                item = &enumerator.Current;
                record.used_items[item.iti] = item.num;
                data2 = core.FindInventoryByItemID(item.iti);
                if (data2 != null)
                {
                    goto Label_023E;
                }
                goto Label_024C;
            Label_023E:
                data2.Used(item.num);
            Label_024C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01F6;
                }
                goto Label_026A;
            }
            finally
            {
            Label_025D:
                ((List<Data.UsedItem>.Enumerator) enumerator).Dispose();
            }
        Label_026A:
            TrickData.ClearEffect();
            enumerator2 = data.trl.GetEnumerator();
        Label_027C:
            try
            {
                goto Label_02D9;
            Label_0281:
                info3 = &enumerator2.Current;
                unit2 = GetUnitFromAllUnits(core, info3.cun);
                TrickData.SuspendEffect(info3.tid, info3.grx, info3.gry, info3.tag, unit2, info3.ccl, info3.rnk, info3.rcp, info3.rac);
            Label_02D9:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0281;
                }
                goto Label_02F7;
            }
            finally
            {
            Label_02EA:
                ((List<Data.TrickInfo>.Enumerator) enumerator2).Dispose();
            }
        Label_02F7:
            core.SkillExecLogs.Clear();
            enumerator3 = data.sel.GetEnumerator();
        Label_030F:
            try
            {
                goto Label_0341;
            Label_0314:
                info4 = &enumerator3.Current;
                log = new BattleCore.SkillExecLog();
                log.Restore(info4);
                core.SkillExecLogs.Add(info4.inm, log);
            Label_0341:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0314;
                }
                goto Label_035F;
            }
            finally
            {
            Label_0352:
                ((List<Data.SkillExecLogInfo>.Enumerator) enumerator3).Dispose();
            }
        Label_035F:
            variables = data.var;
            core.WinTriggerCount = variables.wtc;
            core.LoseTriggerCount = variables.ltc;
            core.ActionCount = variables.act;
            core.Killstreak = variables.kls;
            core.MaxKillstreak = variables.mks;
            core.TotalHeal = variables.thl;
            core.TotalDamagesTaken = variables.tdt;
            core.TotalDamages = variables.tdm;
            core.NumUsedItems = variables.nui;
            core.NumUsedSkills = variables.nus;
            core.ClockTime = variables.ctm;
            core.ClockTimeTotal = variables.ctt;
            core.ContinueCount = variables.coc;
            core.FinisherIname = variables.fns;
            battle.GoldCount = variables.glc;
            battle.RestoreTreasureCount(variables.trc);
            core.Seed = variables.rsd;
            core.PlayByManually = variables.pbm;
            core.IsUseAutoPlayMode = variables.uam;
            if (variables.ris == null)
            {
                goto Label_049A;
            }
            num3 = 0;
            goto Label_048A;
        Label_0472:
            core.SetRandSeed(num3, variables.ris[num3]);
            num3 += 1;
        Label_048A:
            if (num3 < ((int) variables.ris.Length))
            {
                goto Label_0472;
            }
        Label_049A:
            if (variables.gsl.Count != core.GimmickEventList.Count)
            {
                goto Label_051F;
            }
            num4 = 0;
            goto Label_050C;
        Label_04BE:
            event2 = variables.gsl[num4];
            core.GimmickEventList[num4].count = event2.ctr;
            core.GimmickEventList[num4].IsCompleted = (event2.cmp == 0) == 0;
            num4 += 1;
        Label_050C:
            if (num4 < variables.gsl.Count)
            {
                goto Label_04BE;
            }
        Label_051F:
            if (variables.ssl.Count == null)
            {
                goto Label_0588;
            }
            core.EventTriggers = new bool[variables.ssl.Count];
            num5 = 0;
            goto Label_0575;
        Label_054F:
            event3 = variables.ssl[num5];
            core.EventTriggers[num5] = event3.trg;
            num5 += 1;
        Label_0575:
            if (num5 < variables.ssl.Count)
            {
                goto Label_054F;
            }
        Label_0588:
            dictionary = new Dictionary<string, int>();
            num6 = Math.Min((int) variables.tkk.Length, (int) variables.tkv.Length);
            num7 = 0;
            goto Label_05D1;
        Label_05B0:
            dictionary.Add(variables.tkk[num7], variables.tkv[num7]);
            num7 += 1;
        Label_05D1:
            if (num7 < num6)
            {
                goto Label_05B0;
            }
            core.TargetKillstreak = dictionary;
            dictionary2 = new Dictionary<string, int>();
            num8 = Math.Min((int) variables.mtk.Length, (int) variables.mtv.Length);
            num9 = 0;
            goto Label_062B;
        Label_060A:
            dictionary2.Add(variables.mtk[num9], variables.mtv[num9]);
            num9 += 1;
        Label_062B:
            if (num9 < num8)
            {
                goto Label_060A;
            }
            core.MaxTargetKillstreak = dictionary2;
            info5 = variables.wti;
            if (string.IsNullOrEmpty(info5.wid) != null)
            {
                goto Label_068E;
            }
            unit3 = GetUnitFromAllUnits(core, info5.mun);
            WeatherData.SuspendWeather(info5.wid, core.Units, unit3, info5.rnk, info5.rcp, info5.ccl);
        Label_068E:
            core.CurrentTeamId = variables.ctd;
            core.MaxTeamId = variables.mtd;
            battle.EventPlayBgmID = variables.pbd;
            return 1;
        }

        public static bool SaveData()
        {
            Data data;
            data = makeSaveData();
            if (data != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return writeSaveData(mSuspendFileName, data);
        }

        private static bool writeSaveData(string file_name, Data data)
        {
            string str;
            byte[] buffer;
            bool flag;
            if (string.IsNullOrEmpty(file_name) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (data != null)
            {
                goto Label_001A;
            }
            data = new Data();
        Label_001A:
            try
            {
                str = JsonUtility.ToJson(data);
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_0049;
                }
                buffer = MyEncrypt.Encrypt(0, str, 0);
                if (buffer == null)
                {
                    goto Label_0049;
                }
                File.WriteAllBytes(file_name, buffer);
                flag = 1;
                goto Label_0056;
            Label_0049:
                goto Label_0054;
            }
            catch
            {
            Label_004E:
                goto Label_0054;
            }
        Label_0054:
            return 0;
        Label_0056:
            return flag;
        }

        private static string mSuspendFileName
        {
            get
            {
                return (AppPath.persistentDataPath + "/new_suspend.bin");
            }
        }

        [Serializable]
        public class Data
        {
            public bool ivl;
            public Header hdr;
            public List<UnitInfo> uil;
            public List<UsedItem> itl;
            public List<TrickInfo> trl;
            public List<SkillExecLogInfo> sel;
            public Variables var;

            public Data()
            {
                this.hdr = new Header();
                this.uil = new List<UnitInfo>();
                this.itl = new List<UsedItem>();
                this.trl = new List<TrickInfo>();
                this.sel = new List<SkillExecLogInfo>();
                this.var = new Variables();
                base..ctor();
                return;
            }

            [Serializable]
            public class Header
            {
                public string apv;
                public int arv;
                public string qid;
                public long bid;
                public bool cat;
                public bool cad;

                public Header()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class SkillExecLogInfo
            {
                public string inm;
                public int ucnt;
                public int kcnt;

                public SkillExecLogInfo()
                {
                    base..ctor();
                    return;
                }
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

                public TrickInfo()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class UnitInfo
            {
                public string nam;
                public int nhp;
                public int chp;
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
                public int tid;
                public int fst;
                public List<AbilChg> acl;
                public List<AddedAbil> aal;
                public List<SkillUse> sul;
                public List<Buff> bfl;
                public List<Cond> cdl;
                public List<Shield> shl;
                public List<string> hpi;
                public List<MhmDmg> mhl;

                public UnitInfo()
                {
                    this.acl = new List<AbilChg>();
                    this.aal = new List<AddedAbil>();
                    this.sul = new List<SkillUse>();
                    this.bfl = new List<Buff>();
                    this.cdl = new List<Cond>();
                    this.shl = new List<Shield>();
                    this.hpi = new List<string>();
                    this.mhl = new List<MhmDmg>();
                    base..ctor();
                    return;
                }

                [Serializable]
                public class AbilChg
                {
                    public List<Data> acd;

                    public AbilChg()
                    {
                        this.acd = new List<Data>();
                        base..ctor();
                        return;
                    }

                    [Serializable]
                    public class Data
                    {
                        public string fid;
                        public string tid;
                        public int tur;
                        public int irs;
                        public int exp;
                        public int iif;

                        public Data()
                        {
                            base..ctor();
                            return;
                        }
                    }
                }

                [Serializable]
                public class AddedAbil
                {
                    public string aid;
                    public int exp;

                    public AddedAbil()
                    {
                        base..ctor();
                        return;
                    }
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
                    public int vtp;
                    public int ctp;
                    public uint lid;
                    public int ubc;
                    public List<int> atl;

                    public Buff()
                    {
                        this.atl = new List<int>();
                        base..ctor();
                        return;
                    }
                }

                [Serializable]
                public class Cond
                {
                    public string sid;
                    public int stg;
                    public string cid;
                    public int tur;
                    public int uni;
                    public int cui;
                    public int tim;
                    public bool ipa;
                    public int ucd;
                    public int cdt;
                    public int cnd;
                    public bool icu;
                    public uint lid;

                    public Cond()
                    {
                        base..ctor();
                        return;
                    }
                }

                [Serializable]
                public class MhmDmg
                {
                    public int typ;
                    public int dmg;

                    public MhmDmg()
                    {
                        base..ctor();
                        return;
                    }
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

                    public Shield()
                    {
                        base..ctor();
                        return;
                    }
                }

                [Serializable]
                public class SkillUse
                {
                    public string sid;
                    public int ctr;

                    public SkillUse()
                    {
                        base..ctor();
                        return;
                    }
                }
            }

            [Serializable]
            public class UsedItem
            {
                public string iti;
                public int num;

                public UsedItem()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class Variables
            {
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
                public List<GimmickEvent> gsl;
                public List<ScriptEvent> ssl;
                public WeatherInfo wti;
                public int ctd;
                public int mtd;
                public string pbd;

                public Variables()
                {
                    this.gsl = new List<GimmickEvent>();
                    this.ssl = new List<ScriptEvent>();
                    this.wti = new WeatherInfo();
                    base..ctor();
                    return;
                }

                [Serializable]
                public class GimmickEvent
                {
                    public int ctr;
                    public int cmp;

                    public GimmickEvent()
                    {
                        base..ctor();
                        return;
                    }
                }

                [Serializable]
                public class ScriptEvent
                {
                    public bool trg;

                    public ScriptEvent()
                    {
                        base..ctor();
                        return;
                    }
                }

                [Serializable]
                public class WeatherInfo
                {
                    public string wid;
                    public int mun;
                    public int rnk;
                    public int rcp;
                    public int ccl;

                    public WeatherInfo()
                    {
                        base..ctor();
                        return;
                    }
                }
            }
        }
    }
}

