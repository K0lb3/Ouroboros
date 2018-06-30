namespace SRPG
{
    using System;

    public class RarityParam
    {
        public static readonly int MAX_RARITY;
        public OInt UnitLvCap;
        public OInt UnitJobLvCap;
        public OInt UnitAwakeLvCap;
        public OInt UnitUnlockPieceNum;
        public OInt UnitChangePieceNum;
        public OInt UnitSelectChangePieceNum;
        public OInt UnitRarityUpCost;
        public OInt PieceToPoint;
        public string DropSE;
        public RarityEquipEnhanceParam EquipEnhanceParam;
        public OInt ArtifactLvCap;
        public OInt ArtifactCostRate;
        public OInt ArtifactCreatePieceNum;
        public OInt ArtifactGouseiPieceNum;
        public OInt ArtifactChangePieceNum;
        public OInt ArtifactCreateCost;
        public OInt ArtifactRarityUpCost;
        public OInt ArtifactChangeCost;
        public OInt ConceptCardLvCap;
        public OInt ConceptCardAwakeCountMax;
        public StatusParam GrowStatus;

        static RarityParam()
        {
            MAX_RARITY = 6;
            return;
        }

        public RarityParam()
        {
            this.GrowStatus = new StatusParam();
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_RarityParam json)
        {
            int[][] numArrayArray1;
            string[] textArray1;
            int num;
            int num2;
            string[] strArray;
            int[][] numArray;
            int num3;
            int num4;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.UnitLvCap = json.unitcap;
            this.UnitJobLvCap = json.jobcap;
            this.UnitAwakeLvCap = json.awakecap;
            this.UnitUnlockPieceNum = json.piece;
            this.UnitChangePieceNum = json.ch_piece;
            this.UnitSelectChangePieceNum = json.ch_piece_select;
            this.UnitRarityUpCost = json.rareup_cost;
            this.PieceToPoint = json.gain_pp;
            if (this.EquipEnhanceParam != null)
            {
                goto Label_00A6;
            }
            this.EquipEnhanceParam = new RarityEquipEnhanceParam();
        Label_00A6:
            num = json.eq_enhcap + 1;
            this.EquipEnhanceParam.rankcap = num;
            this.EquipEnhanceParam.cost_scale = json.eq_costscale;
            this.EquipEnhanceParam.ranks = null;
            if (num <= 0)
            {
                goto Label_023E;
            }
            if (json.eq_points == null)
            {
                goto Label_0115;
            }
            if (json.eq_num1 == null)
            {
                goto Label_0115;
            }
            if (json.eq_num2 == null)
            {
                goto Label_0115;
            }
            if (json.eq_num3 != null)
            {
                goto Label_0117;
            }
        Label_0115:
            return 0;
        Label_0117:
            this.EquipEnhanceParam.ranks = new RarityEquipEnhanceParam.RankParam[num];
            num2 = 0;
            goto Label_0164;
        Label_012F:
            this.EquipEnhanceParam.ranks[num2] = new RarityEquipEnhanceParam.RankParam();
            this.EquipEnhanceParam.ranks[num2].need_point = json.eq_points[num2];
            num2 += 1;
        Label_0164:
            if (num2 < num)
            {
                goto Label_012F;
            }
            textArray1 = new string[] { json.eq_item1, json.eq_item2, json.eq_item3 };
            strArray = textArray1;
            numArrayArray1 = new int[][] { json.eq_num1, json.eq_num2, json.eq_num3 };
            numArray = numArrayArray1;
            num3 = 0;
            goto Label_0234;
        Label_01B7:
            num4 = 0;
            goto Label_0226;
        Label_01BF:
            this.EquipEnhanceParam.ranks[num4].return_item[num3] = new ReturnItem();
            this.EquipEnhanceParam.ranks[num4].return_item[num3].iname = strArray[num3];
            this.EquipEnhanceParam.ranks[num4].return_item[num3].num = numArray[num3][num4];
            num4 += 1;
        Label_0226:
            if (num4 < num)
            {
                goto Label_01BF;
            }
            num3 += 1;
        Label_0234:
            if (num3 < ((int) strArray.Length))
            {
                goto Label_01B7;
            }
        Label_023E:
            this.ArtifactLvCap = json.af_lvcap;
            this.ArtifactCostRate = json.af_upcost;
            this.ArtifactCreatePieceNum = json.af_unlock;
            this.ArtifactGouseiPieceNum = json.af_gousei;
            this.ArtifactChangePieceNum = json.af_change;
            this.ArtifactCreateCost = json.af_unlock_cost;
            this.ArtifactRarityUpCost = json.af_gousei_cost;
            this.ArtifactChangeCost = json.af_change_cost;
            this.GrowStatus.hp = json.hp;
            this.GrowStatus.mp = json.mp;
            this.GrowStatus.atk = json.atk;
            this.GrowStatus.def = json.def;
            this.GrowStatus.mag = json.mag;
            this.GrowStatus.mnd = json.mnd;
            this.GrowStatus.dex = json.dex;
            this.GrowStatus.spd = json.spd;
            this.GrowStatus.cri = json.cri;
            this.GrowStatus.luk = json.luk;
            this.DropSE = json.drop;
            this.ConceptCardLvCap = json.card_lvcap;
            this.ConceptCardAwakeCountMax = json.card_awake_count;
            return 1;
        }
    }
}

