// Decompiled with JetBrains decompiler
// Type: SRPG.NPCSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class NPCSetting : UnitSetting
  {
    public AIActionTable acttbl = new AIActionTable();
    public AIPatrolTable patrol = new AIPatrolTable();
    public List<OString> notice_members = new List<OString>();
    public OString iname;
    public OInt lv;
    public OInt rare;
    public OInt awake;
    public OInt elem;
    public OInt exp;
    public OInt gems;
    public OInt gold;
    public OInt search;
    public OBool control;
    public EquipAbilitySetting[] abilities;
    public OString fskl;
    public OInt notice_damage;
    public MapBreakObj break_obj;

    public NPCSetting()
    {
    }

    public NPCSetting(JSON_MapEnemyUnit json)
    {
      this.uniqname = (OString) json.name;
      this.iname = (OString) json.iname;
      this.side = (OInt) json.side;
      this.lv = (OInt) Math.Max(json.lv, 1);
      this.rare = (OInt) json.rare;
      this.awake = (OInt) json.awake;
      this.elem = (OInt) json.elem;
      this.exp = (OInt) json.exp;
      this.gems = (OInt) json.gems;
      this.gold = (OInt) json.gold;
      this.ai = (OString) json.ai;
      this.pos.x = (OInt) json.x;
      this.pos.y = (OInt) json.y;
      this.dir = (OInt) json.dir;
      this.search = (OInt) json.search;
      this.control = (OBool) (json.ctrl != 0);
      this.trigger = (EventTrigger) null;
      this.abilities = (EquipAbilitySetting[]) null;
      this.waitEntryClock = (OInt) json.wait_e;
      this.waitMoveTurn = (OInt) json.wait_m;
      this.waitExitTurn = (OInt) json.wait_exit;
      this.startCtCalc = (eMapUnitCtCalcType) json.ct_calc;
      this.startCtVal = (OInt) json.ct_val;
      this.DisableFirceVoice = json.fvoff != 0;
      this.ai_type = (AIActionType) json.ai_type;
      this.ai_pos.x = (OInt) json.ai_x;
      this.ai_pos.y = (OInt) json.ai_y;
      this.ai_len = (OInt) json.ai_len;
      this.parent = (OString) json.parent;
      this.fskl = (OString) json.fskl;
      this.notice_damage = (OInt) json.notice_damage;
      if (json.notice_members != null)
      {
        this.notice_members = new List<OString>(json.notice_members.Length);
        for (int index = 0; index < json.notice_members.Length; ++index)
        {
          if (!string.IsNullOrEmpty(json.notice_members[index]))
            this.notice_members.Add((OString) json.notice_members[index]);
        }
      }
      if (json.trg != null)
      {
        this.trigger = new EventTrigger();
        this.trigger.Deserialize(json.trg);
      }
      if (json.entries != null && json.entries.Length > 0)
      {
        this.entries = new List<UnitEntryTrigger>((IEnumerable<UnitEntryTrigger>) json.entries);
        this.entries_and = (OInt) json.entries_and;
      }
      if (json.abils != null && json.abils.Length > 0)
      {
        this.abilities = new EquipAbilitySetting[json.abils.Length];
        for (int index1 = 0; index1 < json.abils.Length; ++index1)
        {
          this.abilities[index1] = new EquipAbilitySetting();
          this.abilities[index1].iname = (OString) json.abils[index1].iname;
          this.abilities[index1].rank = (OInt) json.abils[index1].rank;
          if (json.abils[index1].skills != null)
          {
            this.abilities[index1].skills = new EquipSkillSetting[json.abils[index1].skills.Length];
            for (int index2 = 0; index2 < json.abils[index1].skills.Length; ++index2)
            {
              this.abilities[index1].skills[index2] = new EquipSkillSetting();
              this.abilities[index1].skills[index2].iname = (OString) json.abils[index1].skills[index2].iname;
              this.abilities[index1].skills[index2].rate = (OInt) json.abils[index1].skills[index2].rate;
              if (json.abils[index1].skills[index2].cond != null && json.abils[index1].skills[index2].cond.type != 0)
              {
                SkillLockCondition dsc = new SkillLockCondition();
                json.abils[index1].skills[index2].cond.CopyTo(dsc);
                this.abilities[index1].skills[index2].cond = dsc;
              }
            }
          }
        }
      }
      if (json.acttbl != null && json.acttbl.actions != null && json.acttbl.actions.Length > 0)
      {
        this.acttbl.actions.Clear();
        for (int index = 0; index < json.acttbl.actions.Length; ++index)
        {
          AIAction aiAction = new AIAction();
          aiAction.skill = (OString) json.acttbl.actions[index].skill;
          aiAction.type = (OInt) json.acttbl.actions[index].type;
          aiAction.turn = (OInt) json.acttbl.actions[index].turn;
          aiAction.notBlock = (OBool) (json.acttbl.actions[index].notBlock != 0);
          if (json.acttbl.actions[index].cond != null && json.acttbl.actions[index].cond.type != 0)
          {
            SkillLockCondition dsc = new SkillLockCondition();
            json.acttbl.actions[index].cond.CopyTo(dsc);
            aiAction.cond = dsc;
          }
          this.acttbl.actions.Add(aiAction);
        }
        this.acttbl.looped = json.acttbl.looped;
      }
      if (json.patrol != null && json.patrol.routes != null && json.patrol.routes.Length > 0)
        json.patrol.CopyTo(this.patrol);
      if (json.break_obj == null)
        return;
      this.break_obj = new MapBreakObj();
      json.break_obj.CopyTo(this.break_obj);
    }
  }
}
