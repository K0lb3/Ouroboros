namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/ReqUnitSelectList", 0x7fe5)]
    public class FlowNode_ReqUnitSelectList : FlowNode_Network
    {
        public UnitSelectListData mUnitSelectListData;

        public FlowNode_ReqUnitSelectList()
        {
            base..ctor();
            return;
        }

        private void Deserialize(Json_UnitSelectResponse json)
        {
            this.mUnitSelectListData = new UnitSelectListData();
            this.mUnitSelectListData.Deserialize(json);
            base.get_gameObject().GetComponent<GetUnitWindow>().RefreshPieceUnit(0, this.mUnitSelectListData);
            return;
        }

        private Json_UnitSelectResponse DummyResponse()
        {
            string[] textArray1;
            string[] strArray;
            int num;
            Json_UnitSelectResponse response;
            GameManager manager;
            int num2;
            Json_UnitSelectItem item;
            textArray1 = new string[] { 
                "UN_V2_VANEKIS", "UN_V2_AMIS", "UN_V2_ISHUNA", "UN_V2_MIZUCHI", "UN_V2_KAZAHAYA", "UN_V2_CIEL", "UN_V2_YUAN", "UN_V2_DECEL", "UN_V2_ENNIS", "UN_V2_ANNEROSE", "UN_V2_GAYN", "UN_V2_AYLLU", "UN_V2_SARAUZU", "UN_V2_RION", "UN_V2_PATTI", "UN_V2_ALMILA",
                "UN_V2_MICHAEL", "UN_V2_ARKILL", "UN_V2_KUON", "UN_V2_MIANNU"
            };
            strArray = textArray1;
            num = (int) strArray.Length;
            response = new Json_UnitSelectResponse();
            response.select = new Json_UnitSelectItem[num];
            if ((MonoSingleton<GameManager>.GetInstanceDirect() == null) == null)
            {
                goto Label_00E1;
            }
            manager = MonoSingleton<GameManager>.Instance;
        Label_00E1:
            num2 = 0;
            goto Label_010C;
        Label_00E9:
            item = new Json_UnitSelectItem();
            item.iname = strArray[num2];
            response.select[num2] = item;
            num2 += 1;
        Label_010C:
            if (num2 < num)
            {
                goto Label_00E9;
            }
            return response;
        }

        public override void OnActivate(int pinID)
        {
            MailData data;
            GiftData data2;
            if (pinID != null)
            {
                goto Label_0088;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != null)
            {
                goto Label_0076;
            }
            data = MonoSingleton<GameManager>.Instance.FindMail(GlobalVars.SelectedMailUniqueID);
            if (data != null)
            {
                goto Label_003F;
            }
            base.set_enabled(0);
            return;
        Label_003F:
            base.set_enabled(1);
            data2 = data.Find(0x100L);
            base.ExecRequest(new ReqMailSelect(data2.iname, 1, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0088;
        Label_0076:
            this.Deserialize(this.DummyResponse());
            this.Success();
        Label_0088:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_UnitSelectResponse> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_UnitSelectResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            Network.RemoveAPI();
            this.Deserialize(response.body);
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

