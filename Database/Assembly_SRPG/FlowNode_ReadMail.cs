namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x16, "いくつか受け取れなかった", 1, 0x16), NodeType("System/ReadMail", 0x7fe5), Pin(20, "一件受け取り完了", 1, 20), Pin(0x15, "全件受け取り完了", 1, 0x15), Pin(0x17, "何も受け取れなかった", 1, 0x17), Pin(11, "全件既読", 0, 11), Pin(10, "一件既読", 0, 10)]
    public class FlowNode_ReadMail : FlowNode_Network
    {
        private RecieveStatus mRecieveStatus;
        private Dictionary<GiftTypes, int> currentNums;

        public FlowNode_ReadMail()
        {
            base..ctor();
            return;
        }

        private void AddCurrentNum(MailData mailData)
        {
            GiftData data;
            GiftData[] dataArray;
            int num;
            Dictionary<GiftTypes, int> dictionary;
            GiftTypes types;
            int num2;
            if (mailData.gifts == null)
            {
                goto Label_005E;
            }
            dataArray = mailData.gifts;
            num = 0;
            goto Label_0055;
        Label_0019:
            data = dataArray[num];
            if (data.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_0051;
            }
            num2 = dictionary[types];
            (dictionary = this.currentNums)[types = 0x40L] = num2 + data.num;
        Label_0051:
            num += 1;
        Label_0055:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0019;
            }
        Label_005E:
            return;
        }

        private int CalcConvertedGold(MailData mail)
        {
            int num;
            GameManager manager;
            int num2;
            ItemParam param;
            ItemData data;
            int num3;
            num = 0;
            if (mail != null)
            {
                goto Label_000A;
            }
            return num;
        Label_000A:
            manager = MonoSingleton<GameManager>.Instance;
            num2 = 0;
            goto Label_00A7;
        Label_0017:
            if (string.IsNullOrEmpty(mail.gifts[num2].iname) == null)
            {
                goto Label_0033;
            }
            goto Label_00A3;
        Label_0033:
            param = manager.GetItemParam(mail.gifts[num2].iname);
            if (param != null)
            {
                goto Label_0052;
            }
            goto Label_00A3;
        Label_0052:
            data = manager.Player.FindItemDataByItemID(param.iname);
            if (data != null)
            {
                goto Label_0071;
            }
            goto Label_00A3;
        Label_0071:
            num3 = (data.Num + mail.gifts[num2].num) - param.cap;
            if (num3 <= 0)
            {
                goto Label_00A3;
            }
            num += num3 * param.sell;
        Label_00A3:
            num2 += 1;
        Label_00A7:
            if (num2 < ((int) mail.gifts.Length))
            {
                goto Label_0017;
            }
            return num;
        }

        private bool CheckRecievable(MailData mailData)
        {
            int num;
            GiftData data;
            GiftData[] dataArray;
            int num2;
            num = 0;
            if (mailData.gifts == null)
            {
                goto Label_0081;
            }
            dataArray = mailData.gifts;
            num2 = 0;
            goto Label_0057;
        Label_001B:
            data = dataArray[num2];
            if (data.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_0053;
            }
            if (data.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_0053;
            }
            num += this.currentNums[0x40L] + data.num;
        Label_0053:
            num2 += 1;
        Label_0057:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_001B;
            }
            if (num < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            return 1;
        }

        public override unsafe void OnActivate(int pinID)
        {
            int[] numArray3;
            long[] numArray1;
            int num;
            MailData data;
            List<MailData> list;
            List<MailData> list2;
            List<MailData> list3;
            int num2;
            MailData data2;
            List<MailData>.Enumerator enumerator;
            long[] numArray;
            int[] numArray2;
            int num3;
            int num4;
            <OnActivate>c__AnonStorey274 storey;
            num4 = pinID;
            if (num4 == 10)
            {
                goto Label_001A;
            }
            if (num4 == 11)
            {
                goto Label_00EF;
            }
            goto Label_02E9;
        Label_001A:
            storey = new <OnActivate>c__AnonStorey274();
            if (base.get_enabled() == null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if (Network.Mode != 1)
            {
                goto Label_0049;
            }
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        Label_0049:
            storey.mailid = GlobalVars.SelectedMailUniqueID;
            num = GlobalVars.SelectedMailPeriod;
            data = MonoSingleton<GameManager>.Instance.Player.Mails.Find(new Predicate<MailData>(storey.<>m__1B9));
            this.RefreshCurrentNums();
            if (this.CheckRecievable(data) != null)
            {
                goto Label_00AA;
            }
            base.ActivateOutputLinks(0x17);
            base.set_enabled(0);
            return;
        Label_00AA:
            this.mRecieveStatus = 0;
            numArray1 = new long[] { storey.mailid };
            numArray3 = new int[] { num };
            base.ExecRequest(new ReqMailRead(numArray1, numArray3, 0, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_02EE;
        Label_00EF:
            if (base.get_enabled() == null)
            {
                goto Label_00FB;
            }
            return;
        Label_00FB:
            if (Network.Mode != 1)
            {
                goto Label_0117;
            }
            base.ActivateOutputLinks(0x15);
            base.set_enabled(0);
            return;
        Label_0117:
            list = MonoSingleton<GameManager>.Instance.Player.Mails;
            list2 = new List<MailData>();
            list3 = new List<MailData>();
            num2 = list.Count - 1;
            goto Label_017D;
        Label_0143:
            if (list[num2] == null)
            {
                goto Label_0177;
            }
            if (list[num2].read <= 0L)
            {
                goto Label_0169;
            }
            goto Label_0177;
        Label_0169:
            list2.Add(list[num2]);
        Label_0177:
            num2 -= 1;
        Label_017D:
            if (num2 >= 0)
            {
                goto Label_0143;
            }
            if (list2.Count >= 1)
            {
                goto Label_01A2;
            }
            base.ActivateOutputLinks(0x15);
            base.set_enabled(0);
            return;
        Label_01A2:
            this.RefreshCurrentNums();
            enumerator = list2.GetEnumerator();
        Label_01B0:
            try
            {
                goto Label_01F4;
            Label_01B5:
                data2 = &enumerator.Current;
                if (this.CheckRecievable(data2) != null)
                {
                    goto Label_01D0;
                }
                goto Label_01F4;
            Label_01D0:
                list3.Add(data2);
                this.AddCurrentNum(data2);
                if (list3.Count < 10)
                {
                    goto Label_01F4;
                }
                goto Label_0200;
            Label_01F4:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01B5;
                }
            Label_0200:
                goto Label_0212;
            }
            finally
            {
            Label_0205:
                ((List<MailData>.Enumerator) enumerator).Dispose();
            }
        Label_0212:
            if (list3.Count >= 1)
            {
                goto Label_0230;
            }
            base.ActivateOutputLinks(0x17);
            base.set_enabled(0);
            return;
        Label_0230:
            if (list3.Count >= 10)
            {
                goto Label_025C;
            }
            if (list3.Count >= list2.Count)
            {
                goto Label_025C;
            }
            this.mRecieveStatus = 2;
            goto Label_0263;
        Label_025C:
            this.mRecieveStatus = 0;
        Label_0263:
            numArray = new long[list3.Count];
            numArray2 = new int[list3.Count];
            num3 = 0;
            goto Label_02B3;
        Label_0287:
            numArray[num3] = list3[num3].mid;
            numArray2[num3] = list3[num3].period;
            num3 += 1;
        Label_02B3:
            if (num3 < list3.Count)
            {
                goto Label_0287;
            }
            base.ExecRequest(new ReqMailRead(numArray, numArray2, 0, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_02EE;
        Label_02E9:;
        Label_02EE:
            return;
        }

        private void OnOverItemAmount(long mailid, int period)
        {
            int[] numArray2;
            long[] numArray1;
            numArray1 = new long[] { mailid };
            numArray2 = new int[] { period };
            this.OnOverItemAmount(numArray1, numArray2);
            return;
        }

        private void OnOverItemAmount(long[] mailids, int[] periods)
        {
            string str;
            <OnOverItemAmount>c__AnonStorey273 storey;
            storey = new <OnOverItemAmount>c__AnonStorey273();
            storey.mailids = mailids;
            storey.periods = periods;
            storey.<>f__this = this;
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MAILBOX_ITEM_OVER_MSG"), null, new UIUtility.DialogResultEvent(storey.<>m__1B7), new UIUtility.DialogResultEvent(storey.<>m__1B8), null, 0, -1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0040;
            }
            code = Network.ErrCode;
            if (code == 0x5dc)
            {
                goto Label_002B;
            }
            if (code == 0x5dd)
            {
                goto Label_0032;
            }
            goto Label_0039;
        Label_002B:
            this.OnBack();
            return;
        Label_0032:
            this.OnBack();
            return;
        Label_0039:
            this.OnRetry();
            return;
        Label_0040:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0070;
            }
            this.OnRetry();
            return;
        Label_0070:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                if (MonoSingleton<GameManager>.Instance.Deserialize(response.body.mails) != null)
                {
                    goto Label_00D4;
                }
                this.OnRetry();
                goto Label_0128;
            Label_00D4:
                if (response.body.artifacts == null)
                {
                    goto Label_00FA;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
            Label_00FA:
                goto Label_0116;
            }
            catch (Exception exception1)
            {
            Label_00FF:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_0128;
            }
        Label_0116:
            Network.RemoveAPI();
            base.set_enabled(0);
            this.Success();
        Label_0128:
            return;
        }

        private void RefreshCurrentNums()
        {
            this.currentNums = new Dictionary<GiftTypes, int>();
            this.currentNums.Add(0x40L, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
            return;
        }

        private void Success()
        {
            if (this.mRecieveStatus != null)
            {
                goto Label_0019;
            }
            base.ActivateOutputLinks(20);
            goto Label_0048;
        Label_0019:
            if (this.mRecieveStatus != 1)
            {
                goto Label_0033;
            }
            base.ActivateOutputLinks(0x17);
            goto Label_0048;
        Label_0033:
            if (this.mRecieveStatus != 2)
            {
                goto Label_0048;
            }
            base.ActivateOutputLinks(0x16);
        Label_0048:
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey274
        {
            internal long mailid;

            public <OnActivate>c__AnonStorey274()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1B9(MailData m)
            {
                return (m.mid == this.mailid);
            }
        }

        [CompilerGenerated]
        private sealed class <OnOverItemAmount>c__AnonStorey273
        {
            internal long[] mailids;
            internal int[] periods;
            internal FlowNode_ReadMail <>f__this;

            public <OnOverItemAmount>c__AnonStorey273()
            {
                base..ctor();
                return;
            }

            internal void <>m__1B7(GameObject go)
            {
                this.<>f__this.ExecRequest(new ReqMailRead(this.mailids, this.periods, 0, new Network.ResponseCallback(this.<>f__this.ResponseCallback)));
                this.<>f__this.set_enabled(1);
                return;
            }

            internal void <>m__1B8(GameObject go)
            {
                this.<>f__this.set_enabled(0);
                return;
            }
        }

        private enum RecieveStatus
        {
            Recieve,
            NotRecieveAll,
            NotRecieveSome
        }
    }
}

