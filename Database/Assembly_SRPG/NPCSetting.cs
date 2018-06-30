namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class NPCSetting : UnitSetting
    {
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
        public AIActionTable acttbl;
        public AIPatrolTable patrol;
        public OString fskl;
        public OInt notice_damage;
        public List<OString> notice_members;
        public MapBreakObj break_obj;

        public NPCSetting()
        {
            this.acttbl = new AIActionTable();
            this.patrol = new AIPatrolTable();
            this.notice_members = new List<OString>();
            base..ctor();
            return;
        }

        public unsafe NPCSetting(JSON_MapEnemyUnit json)
        {
            int num;
            int num2;
            int num3;
            SkillLockCondition condition;
            int num4;
            AIAction action;
            SkillLockCondition condition2;
            this.acttbl = new AIActionTable();
            this.patrol = new AIPatrolTable();
            this.notice_members = new List<OString>();
            base..ctor();
            base.uniqname = json.name;
            this.iname = json.iname;
            base.side = json.side;
            this.lv = Math.Max(json.lv, 1);
            this.rare = json.rare;
            this.awake = json.awake;
            this.elem = json.elem;
            this.exp = json.exp;
            this.gems = json.gems;
            this.gold = json.gold;
            base.ai = json.ai;
            &this.pos.x = json.x;
            &this.pos.y = json.y;
            base.dir = json.dir;
            this.search = json.search;
            this.control = (json.ctrl == 0) == 0;
            base.trigger = null;
            this.abilities = null;
            base.waitEntryClock = json.wait_e;
            base.waitMoveTurn = json.wait_m;
            base.waitExitTurn = json.wait_exit;
            base.startCtCalc = json.ct_calc;
            base.startCtVal = json.ct_val;
            base.DisableFirceVoice = (json.fvoff == 0) == 0;
            base.ai_type = json.ai_type;
            &this.ai_pos.x = json.ai_x;
            &this.ai_pos.y = json.ai_y;
            base.ai_len = json.ai_len;
            base.parent = json.parent;
            this.fskl = json.fskl;
            this.notice_damage = json.notice_damage;
            if (json.notice_members == null)
            {
                goto Label_029F;
            }
            this.notice_members = new List<OString>((int) json.notice_members.Length);
            num = 0;
            goto Label_0291;
        Label_025E:
            if (string.IsNullOrEmpty(json.notice_members[num]) == null)
            {
                goto Label_0275;
            }
            goto Label_028D;
        Label_0275:
            this.notice_members.Add(json.notice_members[num]);
        Label_028D:
            num += 1;
        Label_0291:
            if (num < ((int) json.notice_members.Length))
            {
                goto Label_025E;
            }
        Label_029F:
            if (json.trg == null)
            {
                goto Label_02C7;
            }
            base.trigger = new EventTrigger();
            base.trigger.Deserialize(json.trg);
        Label_02C7:
            if ((json.entries == null) || (((int) json.entries.Length) <= 0))
            {
                goto Label_0302;
            }
            base.entries = new List<UnitEntryTrigger>(json.entries);
            base.entries_and = json.entries_and;
        Label_0302:
            if ((json.abils == null) || (((int) json.abils.Length) <= 0))
            {
                goto Label_04EC;
            }
            this.abilities = new EquipAbilitySetting[(int) json.abils.Length];
            num2 = 0;
            goto Label_04DE;
        Label_0335:
            this.abilities[num2] = new EquipAbilitySetting();
            this.abilities[num2].iname = json.abils[num2].iname;
            this.abilities[num2].rank = json.abils[num2].rank;
            if (json.abils[num2].skills == null)
            {
                goto Label_04DA;
            }
            this.abilities[num2].skills = new EquipSkillSetting[(int) json.abils[num2].skills.Length];
            num3 = 0;
            goto Label_04C5;
        Label_03BA:
            this.abilities[num2].skills[num3] = new EquipSkillSetting();
            this.abilities[num2].skills[num3].iname = json.abils[num2].skills[num3].iname;
            this.abilities[num2].skills[num3].rate = json.abils[num2].skills[num3].rate;
            this.abilities[num2].skills[num3].check_cnt = json.abils[num2].skills[num3].check_cnt;
            if ((json.abils[num2].skills[num3].cond == null) || (json.abils[num2].skills[num3].cond.type == null))
            {
                goto Label_04C1;
            }
            condition = new SkillLockCondition();
            json.abils[num2].skills[num3].cond.CopyTo(condition);
            this.abilities[num2].skills[num3].cond = condition;
        Label_04C1:
            num3 += 1;
        Label_04C5:
            if (num3 < ((int) json.abils[num2].skills.Length))
            {
                goto Label_03BA;
            }
        Label_04DA:
            num2 += 1;
        Label_04DE:
            if (num2 < ((int) json.abils.Length))
            {
                goto Label_0335;
            }
        Label_04EC:
            if (((json.acttbl == null) || (json.acttbl.actions == null)) || (((int) json.acttbl.actions.Length) <= 0))
            {
                goto Label_06CA;
            }
            this.acttbl.actions.Clear();
            num4 = 0;
            goto Label_06A0;
        Label_0532:
            action = new AIAction();
            action.skill = json.acttbl.actions[num4].skill;
            action.type = json.acttbl.actions[num4].type;
            action.turn = json.acttbl.actions[num4].turn;
            action.notBlock = (json.acttbl.actions[num4].notBlock != null) ? 1 : 0;
            action.noExecAct = json.acttbl.actions[num4].noExecAct;
            action.nextActIdx = json.acttbl.actions[num4].nextActIdx;
            action.nextTurnAct = json.acttbl.actions[num4].nextTurnAct;
            action.turnActIdx = json.acttbl.actions[num4].turnActIdx;
            if (json.acttbl.actions[num4].cond == null)
            {
                goto Label_0688;
            }
            if (json.acttbl.actions[num4].cond.type == null)
            {
                goto Label_0688;
            }
            condition2 = new SkillLockCondition();
            json.acttbl.actions[num4].cond.CopyTo(condition2);
            action.cond = condition2;
        Label_0688:
            this.acttbl.actions.Add(action);
            num4 += 1;
        Label_06A0:
            if (num4 < ((int) json.acttbl.actions.Length))
            {
                goto Label_0532;
            }
            this.acttbl.looped = json.acttbl.looped;
        Label_06CA:
            if (json.patrol == null)
            {
                goto Label_0709;
            }
            if (json.patrol.routes == null)
            {
                goto Label_0709;
            }
            if (((int) json.patrol.routes.Length) <= 0)
            {
                goto Label_0709;
            }
            json.patrol.CopyTo(this.patrol);
        Label_0709:
            if (json.break_obj == null)
            {
                goto Label_0730;
            }
            this.break_obj = new MapBreakObj();
            json.break_obj.CopyTo(this.break_obj);
        Label_0730:
            return;
        }
    }
}

