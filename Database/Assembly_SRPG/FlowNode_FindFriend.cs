namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/FindFriend", 0x7fe5), Pin(200, "みつからなかった", 1, 200)]
    public class FlowNode_FindFriend : FlowNode_Network
    {
        public InputField InputFieldFriendID;

        public FlowNode_FindFriend()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__19C(string)
        {
            this.OnEndEdit(this.InputFieldFriendID);
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(200);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0085;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != 1)
            {
                goto Label_0024;
            }
            this.Failure();
            return;
        Label_0024:
            if ((this.InputFieldFriendID == null) == null)
            {
                goto Label_003D;
            }
            base.set_enabled(0);
            return;
        Label_003D:
            str = this.InputFieldFriendID.get_text();
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_005C;
            }
            base.set_enabled(0);
            return;
        Label_005C:
            GlobalVars.SelectedFriendID = string.Empty;
            base.ExecRequest(new ReqFriendFind(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0085:
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GUtility.SetImmersiveMove();
            if ((this.InputFieldFriendID != null) == null)
            {
                goto Label_003C;
            }
            if (this.InputFieldFriendID.get_onEndEdit() == null)
            {
                goto Label_003C;
            }
            this.InputFieldFriendID.get_onEndEdit().RemoveAllListeners();
        Label_003C:
            return;
        }

        private void OnEndEdit(InputField field)
        {
            GUtility.SetImmersiveMove();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            FriendData data;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0051;
            }
            code = Network.ErrCode;
            if (code == 0x1770)
            {
                goto Label_002B;
            }
            if (code == 0x1771)
            {
                goto Label_003C;
            }
            goto Label_004A;
        Label_002B:
            Network.RemoveAPI();
            Network.ResetError();
            this.Failure();
            return;
        Label_003C:
            base.set_enabled(0);
            this.OnBack();
            return;
        Label_004A:
            this.OnRetry();
            return;
        Label_0051:
            DebugMenu.Log("API", "find/friend:" + &www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_009C;
            }
            this.OnRetry();
            return;
        Label_009C:
            Network.RemoveAPI();
        Label_00A1:
            try
            {
                if (response.body.friends == null)
                {
                    goto Label_00C4;
                }
                if (((int) response.body.friends.Length) >= 1)
                {
                    goto Label_00CA;
                }
            Label_00C4:
                throw new InvalidJSONException();
            Label_00CA:
                data = new FriendData();
                data.Deserialize(response.body.friends[0]);
                GlobalVars.FoundFriend = data;
                this.Success();
                goto Label_0106;
            }
            catch (Exception exception1)
            {
            Label_00F4:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_0106;
            }
        Label_0106:
            return;
        }

        private void Start()
        {
            if ((this.InputFieldFriendID != null) == null)
            {
                goto Label_0034;
            }
            this.InputFieldFriendID.get_onEndEdit().AddListener(new UnityAction<string>(this, this.<Start>m__19C));
            base.set_enabled(1);
        Label_0034:
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

