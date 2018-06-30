namespace SRPG
{
    using System;

    public class JSON_MapEnemyUnit : JSON_MapPartyUnit
    {
        public string iname;
        public int side;
        public int lv;
        public int rare;
        public int awake;
        public int elem;
        public int exp;
        public int gems;
        public int gold;
        public int search;
        public int ctrl;
        public int no_st_drop;
        public int no_disp_drop;
        public string drop;
        public int notice_damage;
        public string[] notice_members;
        public JSON_MapEquipAbility[] abils;
        public JSON_AIActionTable acttbl;
        public AIPatrolTable patrol;
        public string fskl;
        public short weight;
        public byte tag;
        public int spawn_max;
        public MapBreakObj break_obj;

        public JSON_MapEnemyUnit()
        {
            base..ctor();
            return;
        }

        public bool IsRandSymbol
        {
            get
            {
                return this.iname.StartsWith("enemy_");
            }
        }

        public int RandTagIndex
        {
            get
            {
                char[] chArray1;
                string[] strArray;
                if (this.IsRandSymbol != null)
                {
                    goto Label_000D;
                }
                return -1;
            Label_000D:
                chArray1 = new char[] { 0x5f };
                strArray = this.iname.Split(chArray1);
                return int.Parse(strArray[((int) strArray.Length) - 1]);
            }
        }
    }
}

