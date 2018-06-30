namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class UnitSetting
    {
        public OString uniqname;
        public OString ai;
        public OIntVector2 pos;
        public OInt dir;
        public OInt side;
        public OInt waitEntryClock;
        public OInt waitMoveTurn;
        public OInt waitExitTurn;
        public eMapUnitCtCalcType startCtCalc;
        public OInt startCtVal;
        public bool DisableFirceVoice;
        public AIActionType ai_type;
        public OIntVector2 ai_pos;
        public OInt ai_len;
        public EventTrigger trigger;
        public List<UnitEntryTrigger> entries;
        public OInt entries_and;
        public OString parent;

        public UnitSetting()
        {
            base..ctor();
            return;
        }

        public unsafe UnitSetting(JSON_MapPartyUnit json)
        {
            base..ctor();
            this.uniqname = json.name;
            this.ai = json.ai;
            &this.pos.x = json.x;
            &this.pos.y = json.y;
            this.dir = json.dir;
            this.waitEntryClock = json.wait_e;
            this.waitMoveTurn = json.wait_m;
            this.waitExitTurn = json.wait_exit;
            this.startCtCalc = json.ct_calc;
            this.startCtVal = json.ct_val;
            this.DisableFirceVoice = (json.fvoff == 0) == 0;
            this.side = 0;
            this.ai_type = json.ai_type;
            &this.ai_pos.x = json.ai_x;
            &this.ai_pos.y = json.ai_y;
            this.ai_len = json.ai_len;
            this.parent = json.parent;
            if (json.trg == null)
            {
                goto Label_0155;
            }
            this.trigger = new EventTrigger();
            this.trigger.Deserialize(json.trg);
        Label_0155:
            if (json.entries == null)
            {
                goto Label_0190;
            }
            if (((int) json.entries.Length) <= 0)
            {
                goto Label_0190;
            }
            this.entries = new List<UnitEntryTrigger>(json.entries);
            this.entries_and = json.entries_and;
        Label_0190:
            return;
        }
    }
}

