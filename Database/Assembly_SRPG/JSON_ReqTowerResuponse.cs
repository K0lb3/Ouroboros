namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JSON_ReqTowerResuponse
    {
        public long rtime;
        public Json_TowerStatus stats;
        public Json_TowerPlayerUnit[] pdeck;
        public Json_TowerEnemyUnit[] edeck;
        public RandDeckResult[] lot_enemies;
        public short reset_cost;
        public byte round;
        public byte is_reset;
        public int turn_num;
        public int died_num;
        public int retire_num;
        public int recover_num;
        public Json_RankStatus rank;

        public JSON_ReqTowerResuponse()
        {
            base..ctor();
            return;
        }

        public class Json_RankStatus
        {
            public int turn_num;
            public int died_num;
            public int retire_num;
            public int recovery_num;
            public int spd_rank;
            public int tec_rank;
            public int spd_score;
            public int tec_score;
            public int ret_score;
            public int rcv_score;
            public int challenge_num;
            public int lose_num;
            public int reset_num;
            public int challenge_score;
            public int lose_score;
            public int reset_score;

            public Json_RankStatus()
            {
                base..ctor();
                return;
            }
        }

        public class Json_TowerEnemyUnit
        {
            public int eid;
            public int hp;
            public int jewel;

            public Json_TowerEnemyUnit()
            {
                base..ctor();
                return;
            }
        }

        public class Json_TowerPlayerUnit
        {
            public string uname;
            public int damage;
            public int is_died;

            public Json_TowerPlayerUnit()
            {
                base..ctor();
                return;
            }
        }

        public class Json_TowerProg
        {
            public string iname;
            public int is_open;

            public Json_TowerProg()
            {
                base..ctor();
                return;
            }
        }

        public class Json_TowerStatus
        {
            public string fname;
            public string state;
            [CompilerGenerated]
            private static Dictionary<string, int> <>f__switch$mapE;

            public Json_TowerStatus()
            {
                base..ctor();
                return;
            }

            public QuestStates questStates
            {
                get
                {
                    string str;
                    Dictionary<string, int> dictionary;
                    int num;
                    str = this.state;
                    if (str == null)
                    {
                        goto Label_007C;
                    }
                    if (<>f__switch$mapE != null)
                    {
                        goto Label_0054;
                    }
                    dictionary = new Dictionary<string, int>(4);
                    dictionary.Add("win", 0);
                    dictionary.Add("lose", 1);
                    dictionary.Add("ritire", 1);
                    dictionary.Add("cancel", 1);
                    <>f__switch$mapE = dictionary;
                Label_0054:
                    if (<>f__switch$mapE.TryGetValue(str, &num) == null)
                    {
                        goto Label_007C;
                    }
                    if (num == null)
                    {
                        goto Label_0078;
                    }
                    if (num == 1)
                    {
                        goto Label_007A;
                    }
                    goto Label_007C;
                Label_0078:
                    return 2;
                Label_007A:
                    return 1;
                Label_007C:
                    return 0;
                }
                set
                {
                    QuestStates states;
                    states = value;
                    switch (states)
                    {
                        case 0:
                            goto Label_0019;

                        case 1:
                            goto Label_0019;

                        case 2:
                            goto Label_0029;
                    }
                    goto Label_0039;
                Label_0019:
                    this.state = "ritire";
                    goto Label_0039;
                Label_0029:
                    this.state = "win";
                Label_0039:
                    return;
                }
            }
        }

        public class Json_UserCoin
        {
            public int free;
            public int paid;
            public int com;

            public Json_UserCoin()
            {
                base..ctor();
                return;
            }
        }
    }
}

