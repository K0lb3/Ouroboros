namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(10, "Success", 1, 10), Pin(11, "Failue", 1, 11), NodeType("Network/ReqFirstChargeBonus", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqFirstChargeBonus : FlowNode_Network
    {
        public const int PIN_IN_REQUEST = 0;
        public const int PIN_OT_SUCCESS = 10;
        public const int PIN_OT_FAILURE = 11;

        public FlowNode_ReqFirstChargeBonus()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0030;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.ExecRequest(new ReqFirstChargeBonus(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0030:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            ChargeInfoResultWindow window;
            List<FirstChargeReward> list;
            int num;
            Reward reward;
            FirstChargeReward reward2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0056;
            }
            switch ((Network.ErrCode - 0x1fa7))
            {
                case 0:
                    goto Label_0033;

                case 1:
                    goto Label_003A;

                case 2:
                    goto Label_0041;

                case 3:
                    goto Label_0048;
            }
            goto Label_004F;
        Label_0033:
            this.OnBack();
            return;
        Label_003A:
            this.OnBack();
            return;
        Label_0041:
            this.OnBack();
            return;
        Label_0048:
            this.OnBack();
            return;
        Label_004F:
            this.OnRetry();
            return;
        Label_0056:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            window = base.get_gameObject().GetComponent<ChargeInfoResultWindow>();
            if ((window != null) == null)
            {
                goto Label_0106;
            }
            list = new List<FirstChargeReward>();
            num = 0;
            goto Label_00D5;
        Label_00AA:
            reward = response.body.rewards[num];
            reward2 = new FirstChargeReward(reward);
            if (reward2 == null)
            {
                goto Label_00D1;
            }
            list.Add(reward2);
        Label_00D1:
            num += 1;
        Label_00D5:
            if (num < ((int) response.body.rewards.Length))
            {
                goto Label_00AA;
            }
            if (list == null)
            {
                goto Label_0106;
            }
            if (list.Count <= 0)
            {
                goto Label_0106;
            }
            window.SetUp(list.ToArray());
        Label_0106:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        [Serializable]
        public class Json
        {
            public FlowNode_ReqFirstChargeBonus.Reward[] rewards;

            public Json()
            {
                base..ctor();
                return;
            }
        }

        public class Reward
        {
            public string iname;
            public string type;
            public int num;

            public Reward()
            {
                this.iname = string.Empty;
                this.type = string.Empty;
                base..ctor();
                return;
            }

            public GiftTypes GetGiftType()
            {
                long num;
                if (string.IsNullOrEmpty(this.type) == null)
                {
                    goto Label_0013;
                }
                return 0L;
            Label_0013:
                num = 0L;
                if ((this.type == "item") == null)
                {
                    goto Label_0035;
                }
                num |= 1L;
                goto Label_00D4;
            Label_0035:
                if ((this.type == "unit") == null)
                {
                    goto Label_0058;
                }
                num |= 0x80L;
                goto Label_00D4;
            Label_0058:
                if ((this.type == "artifact") == null)
                {
                    goto Label_0078;
                }
                num |= 0x40L;
                goto Label_00D4;
            Label_0078:
                if ((this.type == "concept_card") == null)
                {
                    goto Label_009B;
                }
                num |= 0x1000L;
                goto Label_00D4;
            Label_009B:
                if ((this.type == "coin") == null)
                {
                    goto Label_00BA;
                }
                num |= 4L;
                goto Label_00D4;
            Label_00BA:
                if ((this.type == "gold") == null)
                {
                    goto Label_00D4;
                }
                num |= 2L;
            Label_00D4:
                return num;
            }
        }
    }
}

