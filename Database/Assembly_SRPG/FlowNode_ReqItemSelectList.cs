namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("System/ReqItemSelectList", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqItemSelectList : FlowNode_Network
    {
        public GetItemWindow mGetItemWindow;

        public FlowNode_ReqItemSelectList()
        {
            base..ctor();
            return;
        }

        private void Deserialize(Json_ItemSelectResponse json)
        {
            ItemSelectListData data;
            data = new ItemSelectListData();
            data.Deserialize(json);
            this.mGetItemWindow.Refresh(data.items.ToArray());
            return;
        }

        private Json_ItemSelectResponse DummyResponse()
        {
            string[] textArray1;
            string[] strArray;
            int num;
            Json_ItemSelectResponse response;
            GameManager manager;
            int num2;
            Json_ItemSelectItem item;
            textArray1 = new string[] { "IT_SET_EQUP_MIT_03", "IT_SET_EQUP_MIT_03" };
            strArray = textArray1;
            num = (int) strArray.Length;
            response = new Json_ItemSelectResponse();
            response.select = new Json_ItemSelectItem[num];
            if ((MonoSingleton<GameManager>.GetInstanceDirect() == null) == null)
            {
                goto Label_0045;
            }
            manager = MonoSingleton<GameManager>.Instance;
        Label_0045:
            num2 = 0;
            goto Label_0070;
        Label_004D:
            item = new Json_ItemSelectItem();
            item.iname = strArray[num2];
            response.select[num2] = item;
            num2 += 1;
        Label_0070:
            if (num2 < num)
            {
                goto Label_004D;
            }
            return response;
        }

        public override void OnActivate(int pinID)
        {
            MailData data;
            GiftData data2;
            if (pinID != null)
            {
                goto Label_0089;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode == 1)
            {
                goto Label_0077;
            }
            data = MonoSingleton<GameManager>.Instance.FindMail(GlobalVars.SelectedMailUniqueID);
            if (data != null)
            {
                goto Label_0040;
            }
            base.set_enabled(0);
            return;
        Label_0040:
            base.set_enabled(1);
            data2 = data.Find(0x200L);
            base.ExecRequest(new ReqMailSelect(data2.iname, 0, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0089;
        Label_0077:
            this.Deserialize(this.DummyResponse());
            this.Success();
        Label_0089:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ItemSelectResponse> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ItemSelectResponse>>(&www.text);
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

