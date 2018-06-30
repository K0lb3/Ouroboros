namespace SRPG
{
    using System;
    using UnityEngine;

    public class VersusTowerParam
    {
        public static readonly int RANK_RANGE;
        public static readonly int RANK_NUM;
        public OString VersusTowerID;
        public OString FloorName;
        public OInt Floor;
        public OInt RankupNum;
        public OInt WinNum;
        public OInt LoseNum;
        public OInt BonusNum;
        public OInt DownFloor;
        public OInt ResetFloor;
        public OString[] WinIteminame;
        public VERSUS_ITEM_TYPE[] WinItemType;
        public OInt[] WinItemNum;
        public OString[] JoinIteminame;
        public VERSUS_ITEM_TYPE[] JoinItemType;
        public OInt[] JoinItemNum;
        public OString[] SpIteminame;
        public VERSUS_ITEM_TYPE[] SpItemType;
        public OInt[] SpItemnum;
        public OString[] SeasonIteminame;
        public VERSUS_ITEM_TYPE[] SeasonItemType;
        public OInt[] SeasonItemnum;
        public OString ArrivalIteminame;
        public VERSUS_ITEM_TYPE ArrivalItemType;
        public OInt ArrivalItemNum;

        static VersusTowerParam()
        {
            RANK_RANGE = 4;
            RANK_NUM = 7;
            return;
        }

        public VersusTowerParam()
        {
            base..ctor();
            return;
        }

        public unsafe void Deserialize(JSON_VersusTowerParam json)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.VersusTowerID = json.vstower_id;
            this.FloorName = json.iname;
            this.Floor = json.floor;
            this.RankupNum = json.rankup_num;
            this.WinNum = json.win_num;
            this.LoseNum = json.lose_num;
            this.BonusNum = json.bonus_num;
            this.DownFloor = json.downfloor;
            this.ResetFloor = json.resetfloor;
            if (json.winitem == null)
            {
                goto Label_0169;
            }
            if (json.win_itemnum == null)
            {
                goto Label_0169;
            }
            if (((int) json.winitem.Length) == ((int) json.win_itemnum.Length))
            {
                goto Label_00D5;
            }
            Debug.LogError("VersusTower Param [ WinItem ] is Invalid");
        Label_00D5:
            this.WinIteminame = new OString[(int) json.winitem.Length];
            this.WinItemNum = new OInt[(int) json.win_itemnum.Length];
            num = 0;
            goto Label_0124;
        Label_0102:
            *(&(this.WinIteminame[num])) = json.winitem[num];
            num += 1;
        Label_0124:
            if (num < ((int) json.winitem.Length))
            {
                goto Label_0102;
            }
            num2 = 0;
            goto Label_015B;
        Label_0139:
            *(&(this.WinItemNum[num2])) = json.win_itemnum[num2];
            num2 += 1;
        Label_015B:
            if (num2 < ((int) json.win_itemnum.Length))
            {
                goto Label_0139;
            }
        Label_0169:
            if (json.joinitem == null)
            {
                goto Label_0232;
            }
            if (json.join_itemnum == null)
            {
                goto Label_0232;
            }
            if (((int) json.joinitem.Length) == ((int) json.join_itemnum.Length))
            {
                goto Label_019E;
            }
            Debug.LogError("VersusTower Param [ LoseItem ] is Invalid");
        Label_019E:
            this.JoinIteminame = new OString[(int) json.joinitem.Length];
            this.JoinItemNum = new OInt[(int) json.join_itemnum.Length];
            num3 = 0;
            goto Label_01ED;
        Label_01CB:
            *(&(this.JoinIteminame[num3])) = json.joinitem[num3];
            num3 += 1;
        Label_01ED:
            if (num3 < ((int) json.joinitem.Length))
            {
                goto Label_01CB;
            }
            num4 = 0;
            goto Label_0224;
        Label_0202:
            *(&(this.JoinItemNum[num4])) = json.join_itemnum[num4];
            num4 += 1;
        Label_0224:
            if (num4 < ((int) json.join_itemnum.Length))
            {
                goto Label_0202;
            }
        Label_0232:
            if (json.spbtl_item == null)
            {
                goto Label_0307;
            }
            if (json.spbtl_itemnum == null)
            {
                goto Label_0307;
            }
            if (((int) json.spbtl_item.Length) == ((int) json.spbtl_itemnum.Length))
            {
                goto Label_0267;
            }
            Debug.LogError("VersusTower Param [ SpecialItem ] is Invalid");
        Label_0267:
            this.SpIteminame = new OString[(int) json.spbtl_item.Length];
            this.SpItemnum = new OInt[(int) json.spbtl_itemnum.Length];
            num5 = 0;
            goto Label_02BB;
        Label_0295:
            *(&(this.SpIteminame[num5])) = json.spbtl_item[num5];
            num5 += 1;
        Label_02BB:
            if (num5 < ((int) json.spbtl_item.Length))
            {
                goto Label_0295;
            }
            num6 = 0;
            goto Label_02F8;
        Label_02D2:
            *(&(this.SpItemnum[num6])) = json.spbtl_itemnum[num6];
            num6 += 1;
        Label_02F8:
            if (num6 < ((int) json.spbtl_itemnum.Length))
            {
                goto Label_02D2;
            }
        Label_0307:
            if (json.season_item == null)
            {
                goto Label_043E;
            }
            if (json.season_itemnum == null)
            {
                goto Label_043E;
            }
            if (json.season_itype == null)
            {
                goto Label_043E;
            }
            if (((int) json.season_item.Length) == ((int) json.season_itemnum.Length))
            {
                goto Label_0347;
            }
            Debug.LogError("VersusTower Param [ SeasonItem ] is Invalid");
        Label_0347:
            this.SeasonIteminame = new OString[(int) json.season_item.Length];
            this.SeasonItemType = new VERSUS_ITEM_TYPE[(int) json.season_itype.Length];
            this.SeasonItemnum = new OInt[(int) json.season_itemnum.Length];
            num7 = 0;
            goto Label_03AE;
        Label_0388:
            *(&(this.SeasonIteminame[num7])) = json.season_item[num7];
            num7 += 1;
        Label_03AE:
            if (num7 < ((int) json.season_item.Length))
            {
                goto Label_0388;
            }
            num8 = 0;
            goto Label_03F2;
        Label_03C5:
            this.SeasonItemType[num8] = (int) Enum.Parse(typeof(VERSUS_ITEM_TYPE), json.season_itype[num8], 1);
            num8 += 1;
        Label_03F2:
            if (num8 < ((int) json.season_itype.Length))
            {
                goto Label_03C5;
            }
            num9 = 0;
            goto Label_042F;
        Label_0409:
            *(&(this.SeasonItemnum[num9])) = json.season_itemnum[num9];
            num9 += 1;
        Label_042F:
            if (num9 < ((int) json.season_itemnum.Length))
            {
                goto Label_0409;
            }
        Label_043E:
            if (string.IsNullOrEmpty(json.arrival_item) != null)
            {
                goto Label_0491;
            }
            this.ArrivalIteminame = json.arrival_item;
            this.ArrivalItemType = (int) Enum.Parse(typeof(VERSUS_ITEM_TYPE), json.arrival_type, 1);
            this.ArrivalItemNum = json.arrival_num;
        Label_0491:
            return;
        }
    }
}

