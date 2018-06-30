namespace SRPG
{
    using GR;
    using System;

    [Pin(10, "成功", 1, 10), Pin(0, "メール取得", 0, 0), NodeType("System/ReqMail", 0x7fe5)]
    public class FlowNode_ReqMail : FlowNode_Network
    {
        private const int PIN_ID_REQUEST = 0;
        private const int PIN_ID_SUCCESS = 10;

        public FlowNode_ReqMail()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MailWindow.MailPageRequestData data;
            int num;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_0048;
        Label_000D:
            data = DataSource.FindDataOfClass<MailWindow.MailPageRequestData>(base.get_gameObject(), null);
            base.ExecRequest(new ReqMail(data.page, data.isPeriod, data.isRead, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0048:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_MailPage> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailPage>>(&www.text);
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.mails);
            if (response.body.mails == null)
            {
                goto Label_0092;
            }
            GlobalVars.ConceptCardNum.Set(response.body.mails.concept_count);
        Label_0092:
            base.ActivateOutputLinks(10);
            Network.RemoveAPI();
            return;
        }
    }
}

