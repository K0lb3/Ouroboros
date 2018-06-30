namespace SRPG
{
    using System;

    public class QuestCampaignChildParam
    {
        public string iname;
        public QuestCampaignScopes scope;
        public QuestTypes questType;
        public QuestDifficulties questMode;
        public string questId;
        public string unit;
        public int dropRate;
        public int dropNum;
        public int expPlayer;
        public int expUnit;
        public int apRate;
        public QuestCampaignParentParam[] parents;
        public QuestCampaignTrust campaignTrust;

        public QuestCampaignChildParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_QuestCampaignChildParam json)
        {
            this.iname = json.iname;
            this.scope = json.scope;
            this.questType = (byte) json.quest_type;
            this.questMode = (byte) json.quest_mode;
            this.questId = json.quest_id;
            this.unit = json.unit;
            this.dropRate = json.drop_rate;
            this.dropNum = json.drop_num;
            this.expPlayer = json.exp_player;
            this.expUnit = json.exp_unit;
            this.apRate = json.ap_rate;
            this.parents = new QuestCampaignParentParam[0];
            this.campaignTrust = null;
            return 1;
        }

        public unsafe bool IsAvailablePeriod(DateTime now)
        {
            QuestCampaignParentParam param;
            QuestCampaignParentParam[] paramArray;
            int num;
            paramArray = this.parents;
            num = 0;
            goto Label_003A;
        Label_000E:
            param = paramArray[num];
            if (param.IsAvailablePeriod(now) == null)
            {
                goto Label_0036;
            }
            if (&param.endAt.CompareTo(DateTime.MaxValue) >= 0)
            {
                goto Label_0036;
            }
            return 1;
        Label_0036:
            num += 1;
        Label_003A:
            if (num < ((int) paramArray.Length))
            {
                goto Label_000E;
            }
            return 0;
        }

        public bool IsValidQuest(QuestParam questParam)
        {
            bool flag;
            QuestCampaignScopes scopes;
            flag = 0;
            switch ((this.scope - 1))
            {
                case 0:
                    goto Label_0026;

                case 1:
                    goto Label_003D;

                case 2:
                    goto Label_0065;

                case 3:
                    goto Label_006C;
            }
            goto Label_0083;
        Label_0026:
            flag = this.questId == questParam.iname;
            goto Label_0083;
        Label_003D:
            flag = (this.questType != questParam.type) ? 0 : (this.questMode == questParam.difficulty);
            goto Label_0083;
        Label_0065:
            flag = 1;
            goto Label_0083;
        Label_006C:
            flag = this.questId == questParam.iname;
        Label_0083:
            return flag;
        }

        public QuestCampaignData[] MakeData()
        {
            QuestCampaignData[] dataArray1;
            QuestCampaignData data;
            int num;
            QuestCampaignData[] dataArray;
            QuestCampaignData data2;
            QuestCampaignData data3;
            QuestCampaignData data4;
            QuestCampaignData data5;
            QuestCampaignData data6;
            QuestCampaignData data7;
            QuestCampaignData data8;
            QuestCampaignData data9;
            if (this.scope == 3)
            {
                goto Label_0018;
            }
            if (this.scope != 4)
            {
                goto Label_0048;
            }
        Label_0018:
            data = new QuestCampaignData();
            data.type = 1;
            data.unit = this.unit;
            data.value = this.expUnit;
            dataArray1 = new QuestCampaignData[] { data };
            return dataArray1;
        Label_0048:
            num = 0;
            if (this.apRate == 100)
            {
                goto Label_005B;
            }
            num += 1;
        Label_005B:
            if (this.expUnit == 100)
            {
                goto Label_006C;
            }
            num += 1;
        Label_006C:
            if (this.expPlayer == 100)
            {
                goto Label_007D;
            }
            num += 1;
        Label_007D:
            if (this.dropNum == 100)
            {
                goto Label_008E;
            }
            num += 1;
        Label_008E:
            if (this.dropRate == 100)
            {
                goto Label_009F;
            }
            num += 1;
        Label_009F:
            if (this.campaignTrust == null)
            {
                goto Label_00E8;
            }
            if (this.campaignTrust.concept_card == null)
            {
                goto Label_00BE;
            }
            num += 1;
        Label_00BE:
            if (this.campaignTrust.card_trust_lottery_rate <= 0)
            {
                goto Label_00D3;
            }
            num += 1;
        Label_00D3:
            if (this.campaignTrust.card_trust_qe_bonus <= 0)
            {
                goto Label_00E8;
            }
            num += 1;
        Label_00E8:
            dataArray = new QuestCampaignData[num];
            num -= 1;
            if (this.apRate == 100)
            {
                goto Label_0121;
            }
            data2 = new QuestCampaignData();
            data2.type = 4;
            data2.value = this.apRate;
            dataArray[num] = data2;
            num -= 1;
        Label_0121:
            if (this.expUnit == 100)
            {
                goto Label_0153;
            }
            data3 = new QuestCampaignData();
            data3.type = 1;
            data3.value = this.expUnit;
            dataArray[num] = data3;
            num -= 1;
        Label_0153:
            if (this.expPlayer == 100)
            {
                goto Label_0185;
            }
            data4 = new QuestCampaignData();
            data4.type = 0;
            data4.value = this.expPlayer;
            dataArray[num] = data4;
            num -= 1;
        Label_0185:
            if (this.dropNum == 100)
            {
                goto Label_01B7;
            }
            data5 = new QuestCampaignData();
            data5.type = 3;
            data5.value = this.dropNum;
            dataArray[num] = data5;
            num -= 1;
        Label_01B7:
            if (this.dropRate == 100)
            {
                goto Label_01E9;
            }
            data6 = new QuestCampaignData();
            data6.type = 2;
            data6.value = this.dropRate;
            dataArray[num] = data6;
            num -= 1;
        Label_01E9:
            if (this.campaignTrust == null)
            {
                goto Label_029A;
            }
            if (this.campaignTrust.concept_card == null)
            {
                goto Label_0224;
            }
            data7 = new QuestCampaignData();
            data7.type = 7;
            data7.value = 0;
            dataArray[num] = data7;
            num -= 1;
        Label_0224:
            if (this.campaignTrust.card_trust_lottery_rate <= 0)
            {
                goto Label_025F;
            }
            data8 = new QuestCampaignData();
            data8.type = 6;
            data8.value = this.campaignTrust.card_trust_lottery_rate;
            dataArray[num] = data8;
            num -= 1;
        Label_025F:
            if (this.campaignTrust.card_trust_qe_bonus <= 0)
            {
                goto Label_029A;
            }
            data9 = new QuestCampaignData();
            data9.type = 5;
            data9.value = this.campaignTrust.card_trust_qe_bonus;
            dataArray[num] = data9;
            num -= 1;
        Label_029A:
            return dataArray;
        }
    }
}

