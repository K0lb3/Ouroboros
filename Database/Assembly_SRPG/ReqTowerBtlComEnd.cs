namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class ReqTowerBtlComEnd : WebAPI
    {
        [CompilerGenerated]
        private static Predicate<Unit> <>f__am$cache0;

        public unsafe ReqTowerBtlComEnd(long btlid, Unit[] Player, Unit[] Enemy, int actCount, int round, byte floor, BtlResultTypes result, RandDeckResult[] deck, Network.ResponseCallback response, string trophyprog, string bingoprog, int[] missions, int[] missions_value)
        {
            StringBuilder builder;
            int num;
            Unit unit;
            int num2;
            TowerFloorParam param;
            List<Unit> list;
            List<TowerResuponse.EnemyUnit> list2;
            int num3;
            int num4;
            Unit unit2;
            int num5;
            Unit unit3;
            SupportData data;
            int num6;
            int num7;
            BtlResultTypes types;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/btl/end";
            builder.Length = 0;
            this.SetValue(&builder, "\"btlid\":", btlid);
            builder.Append("\"btlendparam\":{");
            if (Player == null)
            {
                goto Label_0181;
            }
            builder.Append("\"pdeck\":[");
            num = 0;
            goto Label_015E;
        Label_0051:
            unit = Player[num];
            if (unit.Side != null)
            {
                goto Label_015A;
            }
            if (unit.UnitData.UniqueID != null)
            {
                goto Label_0075;
            }
            goto Label_015A;
        Label_0075:
            builder.Append("{");
            this.SetValue(&builder, "\"iid\":", unit.UnitData.UniqueID);
            this.SetValue(&builder, "\"iname\":\"", unit.UnitData.UnitParam.iname, "\",");
            num2 = unit.CalcTowerDamege();
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(SceneBattle.Instance.Battle.QuestID);
            num2 -= param.CalcHelaNum(unit.MaximumStatus.param.hp);
            num2 = Mathf.Max(num2, 0);
            this.SetValue(&builder, "\"damage\":", (long) num2);
            this.SetValue(&builder, "\"is_died\":", (long) ((unit.IsDeadCondition() == null) ? 0 : ((unit.IsUnitFlag(0x80000) == null) ? 1 : 0)), string.Empty);
            builder.Append("},");
        Label_015A:
            num += 1;
        Label_015E:
            if (num < ((int) Player.Length))
            {
                goto Label_0051;
            }
            builder.Length -= 1;
            builder.Append("],");
        Label_0181:
            builder.Append("\"status\":\"");
            types = result;
            switch (types)
            {
                case 0:
                    goto Label_01AD;

                case 1:
                    goto Label_01BE;

                case 2:
                    goto Label_01CF;

                case 3:
                    goto Label_01E0;
            }
            goto Label_01F1;
        Label_01AD:
            builder.Append("win");
            goto Label_01F1;
        Label_01BE:
            builder.Append("lose");
            goto Label_01F1;
        Label_01CF:
            builder.Append("retire");
            goto Label_01F1;
        Label_01E0:
            builder.Append("cancel");
        Label_01F1:
            builder.Append("\"");
            builder.Append(",\"turn\":");
            builder.Append(actCount);
            builder.Append(",\"round\":");
            builder.Append(round);
            builder.Append(",\"floor\":");
            builder.Append(floor);
            if (result != 1)
            {
                goto Label_05B8;
            }
            builder.Append(",\"edeck\":[");
            list = new List<Unit>(Enemy);
            if (<>f__am$cache0 != null)
            {
                goto Label_0272;
            }
            <>f__am$cache0 = new Predicate<Unit>(ReqTowerBtlComEnd.<ReqTowerBtlComEnd>m__4D6);
        Label_0272:
            list.RemoveAll(<>f__am$cache0);
            if (MonoSingleton<GameManager>.Instance.TowerResuponse.edeck == null)
            {
                goto Label_046B;
            }
            list2 = MonoSingleton<GameManager>.Instance.TowerResuponse.edeck;
            num3 = 0;
            num4 = 0;
            goto Label_0458;
        Label_02AD:
            if (list2[num4].hp != null)
            {
                goto Label_0335;
            }
            builder.Append("{");
            this.SetValue(&builder, "\"eid\":\"", (long) num4, "\",");
            this.SetValue(&builder, "\"iname\":\"", list2[num4].iname, "\",");
            this.SetValue(&builder, "\"hp\":", 0L);
            this.SetValue(&builder, "\"jewel\":", 0L, string.Empty);
            builder.Append("},");
            goto Label_0452;
        Label_0335:
            unit2 = list[num3];
            num3 += 1;
            builder.Append("{");
            this.SetValue(&builder, "\"eid\":\"", (long) num4, "\",");
            this.SetValue(&builder, "\"iname\":\"", unit2.UnitParam.iname, "\",");
            if ((unit2.IsGimmick == null) || (unit2.IsDisableGimmick() != null))
            {
                goto Label_03E6;
            }
            if (unit2.IsBreakObj == null)
            {
                goto Label_03D2;
            }
            this.SetValue(&builder, "\"hp\":", (long) unit2.CurrentStatus.param.hp);
            goto Label_03E1;
        Label_03D2:
            this.SetValue(&builder, "\"hp\":", 1L);
        Label_03E1:
            goto Label_041D;
        Label_03E6:
            this.SetValue(&builder, "\"hp\":", (unit2.IsDead == null) ? ((long) unit2.CurrentStatus.param.hp) : 0L);
        Label_041D:
            this.SetValue(&builder, "\"jewel\":", (long) unit2.CurrentStatus.param.mp, string.Empty);
            builder.Append("},");
        Label_0452:
            num4 += 1;
        Label_0458:
            if (num4 < list2.Count)
            {
                goto Label_02AD;
            }
            goto Label_059E;
        Label_046B:
            num5 = 0;
            goto Label_0590;
        Label_0473:
            unit3 = list[num5];
            builder.Append("{");
            this.SetValue(&builder, "\"eid\":\"", (long) num5, "\",");
            this.SetValue(&builder, "\"iname\":\"", unit3.UnitParam.iname, "\",");
            if ((unit3.IsGimmick == null) || (unit3.IsDisableGimmick() != null))
            {
                goto Label_051E;
            }
            if (unit3.IsBreakObj == null)
            {
                goto Label_050A;
            }
            this.SetValue(&builder, "\"hp\":", (long) unit3.CurrentStatus.param.hp);
            goto Label_0519;
        Label_050A:
            this.SetValue(&builder, "\"hp\":", 1L);
        Label_0519:
            goto Label_0555;
        Label_051E:
            this.SetValue(&builder, "\"hp\":", (unit3.IsDead == null) ? ((long) unit3.CurrentStatus.param.hp) : 0L);
        Label_0555:
            this.SetValue(&builder, "\"jewel\":", (long) unit3.CurrentStatus.param.mp, string.Empty);
            builder.Append("},");
            num5 += 1;
        Label_0590:
            if (num5 < list.Count)
            {
                goto Label_0473;
            }
        Label_059E:
            builder.Length -= 1;
            builder.Append("]");
        Label_05B8:
            data = GlobalVars.SelectedSupport.Get();
            if (GlobalVars.SelectedFriendID == null)
            {
                goto Label_0613;
            }
            if (data == null)
            {
                goto Label_0613;
            }
            builder.Append(",\"help\":{\"fuid\":\"");
            builder.Append(GlobalVars.SelectedFriendID);
            builder.Append("\",\"cost\":");
            builder.Append(data.Cost);
            builder.Append("}");
        Label_0613:
            if (missions == null)
            {
                goto Label_06EC;
            }
            if (missions_value == null)
            {
                goto Label_06EC;
            }
            builder.Append(",");
            builder.Append("\"missions\":[");
            num6 = 0;
            goto Label_066D;
        Label_0641:
            if (num6 <= 0)
            {
                goto Label_0652;
            }
            builder.Append(0x2c);
        Label_0652:
            builder.Append(&(missions[num6]).ToString());
            num6 += 1;
        Label_066D:
            if (num6 < ((int) missions.Length))
            {
                goto Label_0641;
            }
            builder.Append("]");
            builder.Append(",");
            builder.Append("\"missions_val\":[");
            num7 = 0;
            goto Label_06D0;
        Label_06A4:
            if (num7 <= 0)
            {
                goto Label_06B5;
            }
            builder.Append(0x2c);
        Label_06B5:
            builder.Append(&(missions_value[num7]).ToString());
            num7 += 1;
        Label_06D0:
            if (num7 < ((int) missions_value.Length))
            {
                goto Label_06A4;
            }
            builder.Append("]");
            goto Label_071C;
        Label_06EC:
            builder.Append(",");
            builder.Append("\"missions\":[]");
            builder.Append(",");
            builder.Append("\"missions_val\":[]");
        Label_071C:
            builder.Append("}");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0749;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0749:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_076A;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_076A:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        [CompilerGenerated]
        private static bool <ReqTowerBtlComEnd>m__4D6(Unit x)
        {
            return ((x.IsBreakObj == null) ? 0 : (string.IsNullOrEmpty(x.CreateBreakObjId) == 0));
        }

        public void SetValue(ref StringBuilder sb, string name, long value)
        {
            *(sb).Append(name);
            *(sb).Append(value);
            *(sb).Append(",");
            return;
        }

        public void SetValue(ref StringBuilder sb, string name, string value)
        {
            *(sb).Append(name);
            *(sb).Append(value);
            *(sb).Append(",");
            return;
        }

        public void SetValue(ref StringBuilder sb, string name, long value, string end)
        {
            *(sb).Append(name);
            *(sb).Append(value);
            *(sb).Append(end);
            return;
        }

        public void SetValue(ref StringBuilder sb, string name, string value, string end)
        {
            *(sb).Append(name);
            *(sb).Append(value);
            *(sb).Append(end);
            return;
        }
    }
}

