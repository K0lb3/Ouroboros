namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GachaTopParamNew
    {
        public string iname;
        public string category;
        public long startat;
        public long endat;
        public int coin;
        public int coin_p;
        public int gold;
        public List<UnitParam> units;
        public int num;
        public string ticket_iname;
        public int ticket_num;
        public bool step;
        public int step_num;
        public int step_index;
        public bool limit;
        public int limit_num;
        public int limit_stock;
        public bool limit_cnt;
        public int limit_cnt_rest;
        public int limit_cnt_num;
        public string type;
        public string asset_title;
        public string asset_bg;
        public string group;
        public string btext;
        public string confirm;
        public List<GachaBonusParam> bonus_items;
        public List<ArtifactParam> artifacts;
        public string detail_url;
        public long reset_at;
        public bool disabled;
        public string bonus_msg;
        public int appeal_type;
        public string appeal_message;
        public bool is_stepupui_hide;
        public bool is_stepup_loop;
        public bool is_free_pause;
        public bool redraw;
        public int redraw_rest;
        public int redraw_num;

        public GachaTopParamNew()
        {
            this.iname = string.Empty;
            this.category = string.Empty;
            this.coin = -1;
            this.coin_p = -1;
            this.gold = -1;
            this.ticket_iname = string.Empty;
            this.type = string.Empty;
            this.asset_title = string.Empty;
            this.asset_bg = string.Empty;
            this.group = string.Empty;
            this.btext = string.Empty;
            this.confirm = string.Empty;
            this.bonus_items = new List<GachaBonusParam>();
            this.detail_url = string.Empty;
            this.bonus_msg = string.Empty;
            this.appeal_message = string.Empty;
            base..ctor();
            return;
        }

        public void Deserialize(GachaParam param)
        {
            if (param != null)
            {
                goto Label_000C;
            }
            throw new InvalidCastException();
        Label_000C:
            this.iname = param.iname;
            this.category = param.category;
            this.startat = param.startat;
            this.endat = param.endat;
            this.coin = param.coin;
            this.gold = param.gold;
            this.coin_p = param.coin_p;
            this.units = param.units;
            this.num = param.num;
            this.ticket_iname = param.ticket_iname;
            this.ticket_num = param.ticket_num;
            this.step = param.step;
            this.step_num = param.step_num;
            this.step_index = param.step_index;
            this.limit = param.limit;
            this.limit_num = param.limit_num;
            this.limit_stock = param.limit_stock;
            this.limit_cnt = param.limit_cnt;
            this.limit_cnt_rest = param.limit_cnt_rest;
            this.limit_cnt_num = param.limit_cnt_num;
            this.type = string.Empty;
            this.asset_title = param.asset_title;
            this.asset_bg = param.asset_bg;
            this.group = param.group;
            this.btext = param.btext;
            this.confirm = param.confirm;
            this.bonus_items = param.bonus_items;
            this.artifacts = param.artifacts;
            this.detail_url = param.detail_url;
            this.reset_at = param.reset_at;
            this.disabled = param.disabled;
            this.bonus_msg = param.bonus_msg;
            this.appeal_type = param.appeal_type;
            this.appeal_message = param.appeal_message;
            this.is_stepupui_hide = param.is_hide;
            this.is_stepup_loop = param.is_loop;
            this.is_free_pause = param.is_free_pause;
            this.redraw = param.redraw;
            this.redraw_rest = param.redraw_rest;
            this.redraw_num = param.redraw_num;
            return;
        }

        public long GetTimerAt()
        {
            return ((this.reset_at <= 0L) ? this.endat : this.reset_at);
        }

        public GachaCostType CostType
        {
            get
            {
                GachaCostType type;
                type = 0;
                if (this.coin < 0)
                {
                    goto Label_0015;
                }
                type = 1;
                goto Label_0059;
            Label_0015:
                if (this.coin_p < 0)
                {
                    goto Label_0028;
                }
                type = 2;
                goto Label_0059;
            Label_0028:
                if (this.gold < 0)
                {
                    goto Label_003B;
                }
                type = 3;
                goto Label_0059;
            Label_003B:
                if (string.IsNullOrEmpty(this.ticket_iname) != null)
                {
                    goto Label_0059;
                }
                if (this.ticket_num <= 0)
                {
                    goto Label_0059;
                }
                type = 4;
            Label_0059:
                return type;
            }
        }

        public GachaCategory Category
        {
            get
            {
                GachaCategory category;
                category = 0;
                if (this.category.Contains("gold") == null)
                {
                    goto Label_001E;
                }
                category = 2;
                goto Label_0035;
            Label_001E:
                if (this.category.Contains("coin") == null)
                {
                    goto Label_0035;
                }
                category = 1;
            Label_0035:
                return category;
            }
        }

        public bool IsStepUpUIHide
        {
            get
            {
                return this.is_stepupui_hide;
            }
        }

        public bool IsStepUpLoop
        {
            get
            {
                return this.is_stepup_loop;
            }
        }

        public bool IsFreePause
        {
            get
            {
                bool flag;
                flag = 0;
                if (this.Category == null)
                {
                    goto Label_0014;
                }
                flag = this.is_free_pause;
            Label_0014:
                return flag;
            }
        }

        public int Cost
        {
            get
            {
                if (this.CostType != 1)
                {
                    goto Label_0013;
                }
                return this.coin;
            Label_0013:
                if (this.CostType != 2)
                {
                    goto Label_0026;
                }
                return this.coin_p;
            Label_0026:
                if (this.CostType != 3)
                {
                    goto Label_0039;
                }
                return this.gold;
            Label_0039:
                return 0;
            }
        }
    }
}

