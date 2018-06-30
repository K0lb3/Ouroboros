namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("System/ReqMailSelectConceptCard", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqMailSelectConceptCard : FlowNode_Network
    {
        public GetConceptCardListWindow m_GetConceptCardListWindow;

        public FlowNode_ReqMailSelectConceptCard()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MailData data;
            GiftData data2;
            if (pinID != null)
            {
                goto Label_005B;
            }
            data = MonoSingleton<GameManager>.Instance.FindMail(GlobalVars.SelectedMailUniqueID);
            if (data != null)
            {
                goto Label_0029;
            }
            base.set_enabled(0);
            return;
        Label_0029:
            base.set_enabled(1);
            data2 = data.Find(0x2000L);
            base.ExecRequest(new ReqMailSelect(data2.iname, 3, new Network.ResponseCallback(this.ResponseCallback)));
        Label_005B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            ConceptCardData[] dataArray;
            int num;
            Json_SelectConceptCard card;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0018:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0048;
            }
            this.OnRetry();
            return;
        Label_0048:
            Network.RemoveAPI();
            if (response.body.select == null)
            {
                goto Label_00E4;
            }
            if (((int) response.body.select.Length) <= 0)
            {
                goto Label_00E4;
            }
            dataArray = new ConceptCardData[(int) response.body.select.Length];
            num = 0;
            goto Label_00C5;
        Label_008A:
            card = response.body.select[num];
            dataArray[num] = ConceptCardData.CreateConceptCardDataForDisplay(card.iname);
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(card.iname, card.has_count);
            num += 1;
        Label_00C5:
            if (num < ((int) response.body.select.Length))
            {
                goto Label_008A;
            }
            this.m_GetConceptCardListWindow.Setup(dataArray);
        Label_00E4:
            return;
        }

        public class Json
        {
            public FlowNode_ReqMailSelectConceptCard.Json_SelectConceptCard[] select;

            public Json()
            {
                base..ctor();
                return;
            }
        }

        public class Json_SelectConceptCard
        {
            public long id;
            public string iname;
            public int has_count;

            public Json_SelectConceptCard()
            {
                base..ctor();
                return;
            }
        }
    }
}

