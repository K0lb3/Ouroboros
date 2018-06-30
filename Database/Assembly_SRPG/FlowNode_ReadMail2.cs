namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [NodeType("System/ReadMail2", 0x7fe5), Pin(0, "開封", 0, 1), Pin(5, "ユニット引き換え券", 0, 5), Pin(6, "アイテム引き換え券", 0, 6), Pin(7, "武具引き換え券", 0, 7), Pin(8, "念装引き換え券", 0, 8), Pin(20, "一件受け取り完了", 1, 20), Pin(0x15, "一括受け取り完了", 1, 0x15), Pin(0x16, "いくつか受け取れなかった", 1, 0x16), Pin(0x17, "何も受け取れなかった", 1, 0x17)]
    public class FlowNode_ReadMail2 : FlowNode_Network
    {
        private const int INPUT_OPEN_MAIL = 0;
        private const int INPUT_OPEN_SELECT_UNIT_SUMMON_TICKET = 5;
        private const int INPUT_OPEN_SELECT_ITEM_SUMMON_TICKET = 6;
        private const int INPUT_OPEN_SELECT_ARTIFACT_SUMMON_TICKET = 7;
        private const int INPUT_OPEN_SELECT_CONCEPT_CARD_SUMMON_TICKET = 8;
        private const int OUTPUT_RECEIVED_MAIL = 20;
        private const int OUTPUT_RECEIVED_MAILS = 0x15;
        private const int OUTPUT_RECEIVED_SOME = 0x16;
        private const int OUTPUT_NOT_RECEIVED_ANYTHING = 0x17;
        private ReceiveStatus mReceiveStatus;

        public FlowNode_ReadMail2()
        {
            base..ctor();
            return;
        }

        private RewardData GiftDataToRewardData(GiftData[] giftDatas)
        {
            RewardData data;
            int num;
            GiftData data2;
            ConceptCardParam param;
            ItemParam param2;
            ArtifactParam param3;
            AwardParam param4;
            ItemParam param5;
            data = new RewardData();
            data.Exp = 0;
            data.Stamina = 0;
            data.MultiCoin = 0;
            data.KakeraCoin = 0;
            num = 0;
            goto Label_018D;
        Label_0029:
            data2 = giftDatas[num];
            data.Coin += data2.coin;
            data.Gold += data2.gold;
            data.ArenaMedal += data2.arenacoin;
            data.MultiCoin += data2.multicoin;
            data.KakeraCoin += data2.kakeracoin;
            if (data2.CheckGiftTypeIncluded(0x1000L) == null)
            {
                goto Label_00E6;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(data2.ConceptCardIname);
            data.AddReward(param, data2.ConceptCardNum);
            if (data2.IsGetConceptCardUnit == null)
            {
                goto Label_00E6;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(data2.ConceptCardGetUnitIname);
            data.AddReward(param2, 1);
        Label_00E6:
            if (data2.iname != null)
            {
                goto Label_00F6;
            }
            goto Label_0189;
        Label_00F6:
            if (data2.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_012E;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data2.iname);
            data.AddReward(param3, data2.num);
            goto Label_0189;
        Label_012E:
            if (data2.CheckGiftTypeIncluded(0x800L) == null)
            {
                goto Label_0169;
            }
            param4 = MonoSingleton<GameManager>.Instance.GetAwardParam(data2.iname);
            data.AddReward(param4.ToItemParam(), data2.num);
            goto Label_0189;
        Label_0169:
            param5 = MonoSingleton<GameManager>.Instance.GetItemParam(data2.iname);
            data.AddReward(param5, data2.num);
        Label_0189:
            num += 1;
        Label_018D:
            if (num < ((int) giftDatas.Length))
            {
                goto Label_0029;
            }
            return data;
        }

        public override unsafe void OnActivate(int pinID)
        {
            MailWindow.MailReadRequestData data;
            List<MailData> list;
            List<MailData> list2;
            List<MailData> list3;
            List<GiftData> list4;
            HaveCheckScope scope;
            MailData data2;
            List<MailData>.Enumerator enumerator;
            int num;
            long[] numArray;
            int num2;
            string str;
            ItemParam param;
            <OnActivate>c__AnonStorey275 storey;
            if (pinID != null)
            {
                goto Label_0216;
            }
            storey = new <OnActivate>c__AnonStorey275();
            data = DataSource.FindDataOfClass<MailWindow.MailReadRequestData>(base.get_gameObject(), null);
            if (Network.Mode != 1)
            {
                goto Label_0036;
            }
            base.ActivateOutputLinks(0x15);
            base.set_enabled(0);
            return;
        Label_0036:
            list = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
            list2 = new List<MailData>();
            storey.ids = new List<long>(data.mailIDs);
            list3 = list.FindAll(new Predicate<MailData>(storey.<>m__1BA));
            if (list3.Count >= 1)
            {
                goto Label_008F;
            }
            base.ActivateOutputLinks(0x15);
            base.set_enabled(0);
            return;
        Label_008F:
            list4 = new List<GiftData>();
            scope = new HaveCheckScope();
        Label_009D:
            try
            {
                enumerator = list3.GetEnumerator();
            Label_00A5:
                try
                {
                    goto Label_0121;
                Label_00AA:
                    data2 = &enumerator.Current;
                    if (scope.CheckReceivable(data2) != null)
                    {
                        goto Label_00C6;
                    }
                    goto Label_0121;
                Label_00C6:
                    list2.Add(data2);
                    if (data2.gifts == null)
                    {
                        goto Label_0118;
                    }
                    num = 0;
                    goto Label_0108;
                Label_00E2:
                    if (data2.gifts[num] == null)
                    {
                        goto Label_0102;
                    }
                    list4.Add(data2.gifts[num]);
                Label_0102:
                    num += 1;
                Label_0108:
                    if (num < ((int) data2.gifts.Length))
                    {
                        goto Label_00E2;
                    }
                Label_0118:
                    scope.AddCurrentNum(data2);
                Label_0121:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_00AA;
                    }
                    goto Label_013F;
                }
                finally
                {
                Label_0132:
                    ((List<MailData>.Enumerator) enumerator).Dispose();
                }
            Label_013F:
                goto Label_0153;
            }
            finally
            {
            Label_0144:
                if (scope == null)
                {
                    goto Label_0152;
                }
                scope.Dispose();
            Label_0152:;
            }
        Label_0153:
            if (list4.Count < 1)
            {
                goto Label_0171;
            }
            GlobalVars.UnitGetReward = new UnitGetParam(list4.ToArray());
        Label_0171:
            if (list2.Count >= 1)
            {
                goto Label_018E;
            }
            base.ActivateOutputLinks(0x17);
            base.set_enabled(0);
            return;
        Label_018E:
            if (list2.Count >= list3.Count)
            {
                goto Label_01AB;
            }
            this.mReceiveStatus = 2;
            goto Label_01B2;
        Label_01AB:
            this.mReceiveStatus = 0;
        Label_01B2:
            numArray = new long[list2.Count];
            num2 = 0;
            goto Label_01DF;
        Label_01C7:
            numArray[num2] = list2[num2].mid;
            num2 += 1;
        Label_01DF:
            if (num2 < list2.Count)
            {
                goto Label_01C7;
            }
            base.ExecRequest(new ReqMailRead(numArray, data.isPeriod, data.page, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0319;
        Label_0216:
            if (((pinID != 5) && (pinID != 6)) && ((pinID != 7) && (pinID != 8)))
            {
                goto Label_0319;
            }
            str = GlobalVars.UnlockUnitID;
            if (pinID != 5)
            {
                goto Label_0272;
            }
            str = GlobalVars.UnlockUnitID;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_02B5;
            }
            GlobalVars.UnitGetReward = new UnitGetParam(MonoSingleton<GameManager>.Instance.GetItemParam(str));
            goto Label_02B5;
        Label_0272:
            if (pinID != 6)
            {
                goto Label_028A;
            }
            str = GlobalVars.ItemSelectListItemData.iiname;
            goto Label_02B5;
        Label_028A:
            if (pinID != 7)
            {
                goto Label_02A2;
            }
            str = GlobalVars.ArtifactListItem.iname;
            goto Label_02B5;
        Label_02A2:
            if (pinID != 8)
            {
                goto Label_02B5;
            }
            str = GetConceptCardListWindow.GetSelectedConceptCard();
            GetConceptCardListWindow.ClearSelectedConceptCard();
        Label_02B5:
            this.mReceiveStatus = 0;
            if (Network.Mode != 1)
            {
                goto Label_02D5;
            }
            base.set_enabled(0);
            this.Success();
            return;
        Label_02D5:
            base.ExecRequest(new ReqMailRead(GlobalVars.SelectedMailUniqueID, (GlobalVars.SelectedMailPeriod != 1) ? 0 : 1, GlobalVars.SelectedMailPage, str, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0319:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_MailRead> response;
            MailData[] dataArray;
            int num;
            int num2;
            string str;
            ConceptCardData data;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_005C;
            }
            GlobalVars.UnitGetReward = null;
            code = Network.ErrCode;
            if (code == 0x5dc)
            {
                goto Label_0040;
            }
            if (code == 0x5dd)
            {
                goto Label_0047;
            }
            if (code == 0x233c)
            {
                goto Label_004E;
            }
            goto Label_0055;
        Label_0040:
            this.OnBack();
            return;
        Label_0047:
            this.OnBack();
            return;
        Label_004E:
            this.OnBack();
            return;
        Label_0055:
            this.OnRetry();
            return;
        Label_005C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailRead>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_007F:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.mails);
                if (response.body.artifacts == null)
                {
                    goto Label_00FF;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 1);
            Label_00FF:
                if (response.body.mails == null)
                {
                    goto Label_0129;
                }
                GlobalVars.ConceptCardNum.Set(response.body.mails.concept_count);
            Label_0129:
                if (response.body.processed == null)
                {
                    goto Label_0206;
                }
                if (((int) response.body.processed.Length) <= 0)
                {
                    goto Label_0206;
                }
                dataArray = new MailData[(int) response.body.processed.Length];
                num = 0;
                goto Label_01F6;
            Label_0166:
                dataArray[num] = new MailData();
                dataArray[num].Deserialize(response.body.processed[num]);
                if (dataArray[num].Contains(0x1000L) == null)
                {
                    goto Label_01F2;
                }
                GlobalVars.IsDirtyConceptCardData.Set(1);
                num2 = 0;
                goto Label_01E2;
            Label_01A9:
                str = dataArray[num].gifts[num2].ConceptCardIname;
                if (dataArray[num].gifts[num2].IsGetConceptCardUnit == null)
                {
                    goto Label_01DE;
                }
                FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(str));
            Label_01DE:
                num2 += 1;
            Label_01E2:
                if (num2 < ((int) dataArray[num].gifts.Length))
                {
                    goto Label_01A9;
                }
            Label_01F2:
                num += 1;
            Label_01F6:
                if (num < ((int) dataArray.Length))
                {
                    goto Label_0166;
                }
                this.SetRewordData(dataArray);
            Label_0206:
                goto Label_0224;
            }
            catch (Exception exception1)
            {
            Label_020B:
                exception = exception1;
                GlobalVars.UnitGetReward = null;
                DebugUtility.LogException(exception);
                goto Label_0268;
            }
        Label_0224:
            if (GlobalVars.LastReward == null)
            {
                goto Label_025B;
            }
            if (GlobalVars.LastReward.Get() == null)
            {
                goto Label_025B;
            }
            MonoSingleton<GameManager>.Instance.Player.OnGoldChange(GlobalVars.LastReward.Get().Gold);
        Label_025B:
            base.set_enabled(0);
            this.Success();
        Label_0268:
            return;
        }

        private unsafe void SetRewordData(MailData[] mail_datas)
        {
            RewardData data;
            int num;
            MailData data2;
            RewardData data3;
            GiftRecieveItemData data4;
            Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator;
            if (mail_datas != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (((int) mail_datas.Length) > 0)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            data = new RewardData();
            num = 0;
            goto Label_0104;
        Label_001E:
            data2 = mail_datas[num];
            if (data2 == null)
            {
                goto Label_0100;
            }
            data3 = this.GiftDataToRewardData(data2.gifts);
            enumerator = data3.GiftRecieveItemDataDic.Values.GetEnumerator();
        Label_0047:
            try
            {
                goto Label_005D;
            Label_004C:
                data4 = &enumerator.Current;
                data.AddReward(data4);
            Label_005D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_004C;
                }
                goto Label_007B;
            }
            finally
            {
            Label_006E:
                ((Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_007B:
            data.Exp += data3.Exp;
            data.Stamina += data3.Stamina;
            data.Coin += data3.Coin;
            data.Gold += data3.Gold;
            data.ArenaMedal += data3.ArenaMedal;
            data.MultiCoin += data3.MultiCoin;
            data.KakeraCoin += data3.KakeraCoin;
        Label_0100:
            num += 1;
        Label_0104:
            if (num < ((int) mail_datas.Length))
            {
                goto Label_001E;
            }
            GlobalVars.LastReward.Set(data);
            return;
        }

        private void Success()
        {
            if (this.mReceiveStatus != null)
            {
                goto Label_0019;
            }
            base.ActivateOutputLinks(20);
            goto Label_0048;
        Label_0019:
            if (this.mReceiveStatus != 1)
            {
                goto Label_0033;
            }
            base.ActivateOutputLinks(0x17);
            goto Label_0048;
        Label_0033:
            if (this.mReceiveStatus != 2)
            {
                goto Label_0048;
            }
            base.ActivateOutputLinks(0x16);
        Label_0048:
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey275
        {
            internal List<long> ids;

            public <OnActivate>c__AnonStorey275()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1BA(MailData md)
            {
                if (this.ids.Contains(md.mid) == null)
                {
                    goto Label_002A;
                }
                this.ids.Remove(md.mid);
                return 1;
            Label_002A:
                return 0;
            }
        }

        private class HaveCheckScope : IDisposable
        {
            private Dictionary<GiftTypes, int> currentNums;

            public HaveCheckScope()
            {
                base..ctor();
                this.currentNums = new Dictionary<GiftTypes, int>();
                this.currentNums.Add(0x40L, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
                this.currentNums.Add(0x1000L, GlobalVars.ConceptCardNum.Get());
                return;
            }

            public void AddCurrentNum(MailData mailData)
            {
                GiftData data;
                GiftData[] dataArray;
                int num;
                Dictionary<GiftTypes, int> dictionary;
                GiftTypes types;
                int num2;
                Dictionary<GiftTypes, int> dictionary2;
                if (mailData.gifts == null)
                {
                    goto Label_009A;
                }
                dataArray = mailData.gifts;
                num = 0;
                goto Label_0091;
            Label_0019:
                data = dataArray[num];
                if (data.CheckGiftTypeIncluded(0x40L) == null)
                {
                    goto Label_0051;
                }
                num2 = dictionary[types];
                (dictionary = this.currentNums)[types = 0x40L] = num2 + data.num;
            Label_0051:
                if (data.CheckGiftTypeIncluded(0x1000L) == null)
                {
                    goto Label_008D;
                }
                num2 = dictionary2[types];
                (dictionary2 = this.currentNums)[types = 0x1000L] = num2 + data.ConceptCardNum;
            Label_008D:
                num += 1;
            Label_0091:
                if (num < ((int) dataArray.Length))
                {
                    goto Label_0019;
                }
            Label_009A:
                return;
            }

            public bool CheckReceivable(MailData mailData)
            {
                int num;
                int num2;
                bool flag;
                GiftData data;
                GiftData[] dataArray;
                int num3;
                ConceptCardParam param;
                num = 0;
                num2 = 0;
                flag = 1;
                if (mailData.gifts == null)
                {
                    goto Label_010F;
                }
                dataArray = mailData.gifts;
                num3 = 0;
                goto Label_00BC;
            Label_0021:
                data = dataArray[num3];
                if (data.CheckGiftTypeIncluded(0x40L) == null)
                {
                    goto Label_004D;
                }
                num += this.currentNums[0x40L] + data.num;
            Label_004D:
                if (data.CheckGiftTypeIncluded(0x1000L) == null)
                {
                    goto Label_00B6;
                }
                num2 += this.currentNums[0x1000L] + data.ConceptCardNum;
                if (data.conceptCard == null)
                {
                    goto Label_00B6;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(data.conceptCard.iname);
                if (param == null)
                {
                    goto Label_00B6;
                }
                if (param.type == 1)
                {
                    goto Label_00B6;
                }
                flag = 0;
            Label_00B6:
                num3 += 1;
            Label_00BC:
                if (num3 < ((int) dataArray.Length))
                {
                    goto Label_0021;
                }
                if (num <= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
                {
                    goto Label_00E8;
                }
                return 0;
            Label_00E8:
                if (num2 <= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax)
                {
                    goto Label_010F;
                }
                if (flag == null)
                {
                    goto Label_010F;
                }
                return 0;
            Label_010F:
                return 1;
            }

            public void Dispose()
            {
                this.currentNums.Clear();
                return;
            }
        }

        public class Json_MailRead
        {
            public Json_PlayerData player;
            public Json_Unit[] units;
            public Json_Item[] items;
            public Json_Friend[] friends;
            public Json_Artifact[] artifacts;
            public Json_Mails mails;
            public Json_Mail[] processed;

            public Json_MailRead()
            {
                base..ctor();
                return;
            }
        }

        private enum ReceiveStatus
        {
            Recieve,
            NotReceiveAll,
            NotReceiveSome
        }
    }
}

