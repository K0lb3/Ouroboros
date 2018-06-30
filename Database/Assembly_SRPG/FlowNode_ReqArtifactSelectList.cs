namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/ReqArtifactSelectList", 0x7fe5)]
    public class FlowNode_ReqArtifactSelectList : FlowNode_Network
    {
        public ArtifactSelectListData mArtifactSelectListData;
        public GetArtifactWindow mGetArtifactWindow;

        public FlowNode_ReqArtifactSelectList()
        {
            base..ctor();
            return;
        }

        private void Deserialize(Json_ArtifactSelectResponse json)
        {
            this.mArtifactSelectListData = new ArtifactSelectListData();
            this.mArtifactSelectListData.Deserialize(json);
            this.mGetArtifactWindow.Refresh(this.mArtifactSelectListData.items.ToArray());
            return;
        }

        private Json_ArtifactSelectResponse DummyResponse()
        {
            string[] textArray1;
            string[] strArray;
            int num;
            Json_ArtifactSelectResponse response;
            GameManager manager;
            int num2;
            Json_ArtifactSelectItem item;
            textArray1 = new string[] { "AF_ARMS_SWO_MITHRIL_GREEN" };
            strArray = textArray1;
            num = (int) strArray.Length;
            response = new Json_ArtifactSelectResponse();
            response.select = new Json_ArtifactSelectItem[num];
            if ((MonoSingleton<GameManager>.GetInstanceDirect() == null) == null)
            {
                goto Label_003D;
            }
            manager = MonoSingleton<GameManager>.Instance;
        Label_003D:
            num2 = 0;
            goto Label_0068;
        Label_0045:
            item = new Json_ArtifactSelectItem();
            item.iname = strArray[num2];
            response.select[num2] = item;
            num2 += 1;
        Label_0068:
            if (num2 < num)
            {
                goto Label_0045;
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
            data2 = data.Find(0x400L);
            base.ExecRequest(new ReqMailSelect(data2.iname, 2, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0088;
        Label_0076:
            this.Deserialize(this.DummyResponse());
            this.Success();
        Label_0088:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse> response;
            int num;
            Json_ArtifactSelectItem item;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            Network.RemoveAPI();
            num = 0;
            goto Label_0086;
        Label_0053:
            item = response.body.select[num];
            if (item.num <= 1)
            {
                goto Label_0082;
            }
            DebugUtility.LogError("武具は一つしか付与できません " + item.iname);
        Label_0082:
            num += 1;
        Label_0086:
            if (num < ((int) response.body.select.Length))
            {
                goto Label_0053;
            }
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

