namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class JSON_MapUnit
    {
        public JSON_MapPartyUnit[] party;
        public JSON_MapEnemyUnit[] enemy;
        public JSON_MapPartyUnit[] arena;
        public JSON_QuestMonitorCondition w_cond;
        public JSON_QuestMonitorCondition l_cond;
        public JSON_GimmickEvent[] gs;
        public JSON_MapEnemyUnit[] deck;
        public JSON_RandUnitTag[] rand_tag;
        public JSON_MapPartySubCT[] party_subs;
        public JSON_MapTrick[] tricks;

        public JSON_MapUnit()
        {
            base..ctor();
            return;
        }

        private void DeleteRandSymbolInEnemies()
        {
            List<JSON_MapEnemyUnit> list;
            int num;
            list = new List<JSON_MapEnemyUnit>();
            num = 0;
            goto Label_0036;
        Label_000D:
            if (this.enemy[num].IsRandSymbol == null)
            {
                goto Label_0024;
            }
            goto Label_0032;
        Label_0024:
            list.Add(this.enemy[num]);
        Label_0032:
            num += 1;
        Label_0036:
            if (num < ((int) this.enemy.Length))
            {
                goto Label_000D;
            }
            this.enemy = list.ToArray();
            return;
        }

        public List<JSON_MapEnemyUnit> GetRandFixedUnit()
        {
            List<JSON_MapEnemyUnit> list;
            int num;
            UnitParam param;
            list = new List<JSON_MapEnemyUnit>();
            num = 0;
            goto Label_0081;
        Label_000D:
            if (MonoSingleton<GameManager>.Instance.MasterParam.ContainsUnitID(this.enemy[num].iname) != null)
            {
                goto Label_0033;
            }
            goto Label_007D;
        Label_0033:
            param = MonoSingleton<GameManager>.Instance.GetUnitParam(this.enemy[num].iname);
            if (param.type == 2)
            {
                goto Label_006F;
            }
            if (param.type == 4)
            {
                goto Label_006F;
            }
            if (param.type != 1)
            {
                goto Label_007D;
            }
        Label_006F:
            list.Add(this.enemy[num]);
        Label_007D:
            num += 1;
        Label_0081:
            if (num < ((int) this.enemy.Length))
            {
                goto Label_000D;
            }
            return list;
        }

        public JSON_MapEnemyUnit[] ReplacedRandEnemy(RandDeckResult[] rand_lot_result, bool delete_rand_symbol)
        {
            int num;
            JSON_MapEnemyUnit unit;
            string str;
            JSON_MapEnemyUnit unit2;
            if (rand_lot_result == null)
            {
                goto Label_000F;
            }
            if (((int) rand_lot_result.Length) > 0)
            {
                goto Label_0022;
            }
        Label_000F:
            if (delete_rand_symbol == null)
            {
                goto Label_001B;
            }
            this.DeleteRandSymbolInEnemies();
        Label_001B:
            return this.enemy;
        Label_0022:
            num = 0;
            goto Label_00E9;
        Label_0029:
            if (((int) this.enemy.Length) > rand_lot_result[num].set_id)
            {
                goto Label_004A;
            }
            DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
            return null;
        Label_004A:
            unit = this.enemy[rand_lot_result[num].set_id];
            if (unit.IsRandSymbol != null)
            {
                goto Label_0071;
            }
            DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
            return null;
        Label_0071:
            unit2 = JsonUtility.FromJson<JSON_MapEnemyUnit>(JsonUtility.ToJson(this.deck[rand_lot_result[num].id]));
            unit2.x = unit.x;
            unit2.y = unit.y;
            unit2.dir = unit.dir;
            unit2.name = unit.name;
            unit2.entries = unit.entries;
            unit2.entries_and = unit.entries_and;
            this.enemy[rand_lot_result[num].set_id] = unit2;
            num += 1;
        Label_00E9:
            if (num < ((int) rand_lot_result.Length))
            {
                goto Label_0029;
            }
            if (delete_rand_symbol == null)
            {
                goto Label_00FE;
            }
            this.DeleteRandSymbolInEnemies();
        Label_00FE:
            return this.enemy;
        }
    }
}

