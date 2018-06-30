namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class GachaParam
    {
        public string iname;
        public string category;
        public long startat;
        public long endat;
        public int gold;
        public int coin;
        public int coin_p;
        public string ticket_iname;
        public int ticket_num;
        public int num;
        public string msg;
        public string name;
        public string movie;
        public string bg;
        public string asset_bg;
        public string asset_title;
        public string detail_url;
        public string[] ext_type;
        public List<UnitData> units2;
        public List<UnitParam> units;
        public bool step;
        public int step_num;
        public int step_index;
        public bool limit;
        public int limit_num;
        public int limit_stock;
        public bool limit_cnt;
        public int limit_cnt_rest;
        public int limit_cnt_num;
        public string group;
        public string btext;
        public string confirm;
        public List<GachaBonusParam> bonus_items;
        public List<ArtifactParam> artifacts;
        public long reset_at;
        public bool disabled;
        public string bonus_msg;
        public int appeal_type;
        public string appeal_message;
        public bool is_hide;
        public bool is_loop;
        public bool is_free_pause;
        public bool redraw;
        public int redraw_rest;
        public int redraw_num;

        public GachaParam()
        {
            this.iname = string.Empty;
            this.category = string.Empty;
            this.gold = -1;
            this.coin = -1;
            this.coin_p = -1;
            this.ticket_iname = string.Empty;
            this.msg = string.Empty;
            this.name = string.Empty;
            this.movie = string.Empty;
            this.bg = string.Empty;
            this.asset_bg = string.Empty;
            this.asset_title = string.Empty;
            this.detail_url = string.Empty;
            this.group = string.Empty;
            this.btext = string.Empty;
            this.confirm = string.Empty;
            this.bonus_items = new List<GachaBonusParam>();
            this.bonus_msg = string.Empty;
            this.appeal_message = string.Empty;
            base..ctor();
            return;
        }

        public void Deserialize(Json_GachaParam json)
        {
            int num;
            UnitParam param;
            int num2;
            ArtifactParam param2;
            int num3;
            int num4;
            GachaBonusParam param3;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidCastException();
        Label_000C:
            if ((this.units != null) || (json.units == null))
            {
                goto Label_007D;
            }
            this.units = new List<UnitParam>((int) json.units.Length);
            num = 0;
            goto Label_006F;
        Label_003C:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.units[num]);
            if (param != null)
            {
                goto Label_005F;
            }
            goto Label_006B;
        Label_005F:
            this.units.Add(param);
        Label_006B:
            num += 1;
        Label_006F:
            if (num < ((int) json.units.Length))
            {
                goto Label_003C;
            }
        Label_007D:
            if ((this.artifacts != null) || (json.pickup_art == null))
            {
                goto Label_00EE;
            }
            this.artifacts = new List<ArtifactParam>((int) json.pickup_art.Length);
            num2 = 0;
            goto Label_00E0;
        Label_00AD:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.pickup_art[num2]);
            if (param2 != null)
            {
                goto Label_00D0;
            }
            goto Label_00DC;
        Label_00D0:
            this.artifacts.Add(param2);
        Label_00DC:
            num2 += 1;
        Label_00E0:
            if (num2 < ((int) json.pickup_art.Length))
            {
                goto Label_00AD;
            }
        Label_00EE:
            this.iname = json.iname;
            this.category = json.cat;
            this.startat = json.startat;
            this.endat = json.endat;
            this.num = json.num;
            this.msg = json.msg;
            if (json.cost == null)
            {
                goto Label_01B0;
            }
            this.gold = json.cost.gold;
            this.coin = json.cost.coin;
            this.coin_p = json.cost.coin_p;
            if (json.cost.ticket == null)
            {
                goto Label_01B0;
            }
            this.ticket_iname = json.cost.ticket.iname;
            this.ticket_num = json.cost.ticket.num;
        Label_01B0:
            this.name = json.name;
            this.movie = json.movie;
            this.bg = json.bg;
            this.asset_bg = json.asset_bg;
            this.asset_title = json.asset_title;
            this.detail_url = json.detail_url;
            if (json.ext_type == null)
            {
                goto Label_0245;
            }
            this.ext_type = new string[(int) json.ext_type.Length];
            num3 = 0;
            goto Label_0236;
        Label_021E:
            this.ext_type[num3] = json.ext_type[num3];
            num3 += 1;
        Label_0236:
            if (num3 < ((int) json.ext_type.Length))
            {
                goto Label_021E;
            }
        Label_0245:
            this.step = 0;
            this.step_num = 0;
            this.step_index = 0;
            this.limit = 0;
            this.limit_num = 0;
            this.limit_stock = 0;
            this.limit_cnt = 0;
            this.limit_cnt_rest = 0;
            this.limit_cnt_num = 0;
            this.reset_at = 0L;
            this.disabled = 0;
            this.redraw = 0;
            this.redraw_rest = 0;
            this.redraw_num = 0;
            if (json.ext_param == null)
            {
                goto Label_03DF;
            }
            if (json.ext_param.step == null)
            {
                goto Label_02F6;
            }
            this.step = 1;
            this.step_num = json.ext_param.step.num;
            this.step_index = json.ext_param.step.index;
        Label_02F6:
            if (json.ext_param.limit == null)
            {
                goto Label_0339;
            }
            this.limit = 1;
            this.limit_num = json.ext_param.limit.num;
            this.limit_stock = json.ext_param.limit.stock;
        Label_0339:
            if (json.ext_param.limit_cnt == null)
            {
                goto Label_037C;
            }
            this.limit_cnt = 1;
            this.limit_cnt_rest = json.ext_param.limit_cnt.rest;
            this.limit_cnt_num = json.ext_param.limit_cnt.num;
        Label_037C:
            if (json.ext_param.redraw == null)
            {
                goto Label_03BF;
            }
            this.redraw = 1;
            this.redraw_rest = json.ext_param.redraw.rest;
            this.redraw_num = json.ext_param.redraw.num;
        Label_03BF:
            this.reset_at = json.ext_param.next_reset_time;
            this.disabled = json.disabled == 1;
        Label_03DF:
            this.group = json.group;
            this.btext = json.btext;
            this.confirm = json.confirm;
            if ((json.bonus_items == null) || (((int) json.bonus_items.Length) <= 0))
            {
                goto Label_0477;
            }
            num4 = 0;
            goto Label_0468;
        Label_0424:
            param3 = new GachaBonusParam();
            param3.iname = json.bonus_items[num4].iname;
            param3.num = json.bonus_items[num4].num;
            this.bonus_items.Add(param3);
            num4 += 1;
        Label_0468:
            if (num4 < ((int) json.bonus_items.Length))
            {
                goto Label_0424;
            }
        Label_0477:
            this.detail_url = json.detail_url;
            this.bonus_msg = json.bonus_msg;
            if (json.appeal == null)
            {
                goto Label_04BC;
            }
            this.appeal_type = json.appeal.type;
            this.appeal_message = json.appeal.message;
        Label_04BC:
            this.is_hide = (json.isHide != 1) ? 0 : 1;
            this.is_loop = (json.isLoop != 1) ? 0 : 1;
            this.is_free_pause = (json.isFreePause != 1) ? 0 : 1;
            return;
        }
    }
}

