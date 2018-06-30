namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [NodeType("Network/gauth_migrate", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "RequestFgG", 0, 1), Pin(2, "Success", 1, 2)]
    public class FlowNode_ReqHikkoshi : FlowNode_Network
    {
        public string HikkoshiCodeInputFieldID;
        public string HikkoshiFgGMailAddress;
        public string HikkoshiFgGPassword;
        [CompilerGenerated]
        private API <m_Api>k__BackingField;

        public FlowNode_ReqHikkoshi()
        {
            base..ctor();
            return;
        }

        private void Failure(int pinID)
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(pinID);
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameObject obj2;
            InputField field;
            GameObject obj3;
            GameObject obj4;
            InputField field2;
            InputField field3;
            if (pinID != null)
            {
                goto Label_0092;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            obj2 = GameObjectID.FindGameObject(this.HikkoshiCodeInputFieldID);
            if ((obj2 == null) != null)
            {
                goto Label_0043;
            }
            if (((field = obj2.GetComponent<InputField>()) == null) == null)
            {
                goto Label_004E;
            }
        Label_0043:
            DebugUtility.LogError("InputField doesn't exist.");
            return;
        Label_004E:
            base.ExecRequest(new ReqGAuthMigrate(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, field.get_text(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            this.m_Api = 0;
            goto Label_0160;
        Label_0092:
            if (pinID != 1)
            {
                goto Label_0160;
            }
            if (Network.Mode != 1)
            {
                goto Label_00AB;
            }
            this.Success();
            return;
        Label_00AB:
            obj3 = GameObjectID.FindGameObject(this.HikkoshiFgGMailAddress);
            obj4 = GameObjectID.FindGameObject(this.HikkoshiFgGPassword);
            if ((obj3 == null) != null)
            {
                goto Label_00E3;
            }
            if (((field2 = obj3.GetComponent<InputField>()) == null) == null)
            {
                goto Label_00EE;
            }
        Label_00E3:
            DebugUtility.LogError("InputField doesn't exist.");
            return;
        Label_00EE:
            if ((obj4 == null) != null)
            {
                goto Label_010E;
            }
            if (((field3 = obj4.GetComponent<InputField>()) == null) == null)
            {
                goto Label_0119;
            }
        Label_010E:
            DebugUtility.LogError("InputField doesn't exist.");
            return;
        Label_0119:
            base.ExecRequest(new ReqGAuthMigrateFgG(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, field2.get_text(), field3.get_text(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            this.m_Api = 1;
        Label_0160:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_Migrate> response;
            WebAPI.JSON_BodyResponse<JSON_Migrate> response2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0069;
            }
            code = Network.ErrCode;
            switch ((code - 0x2260))
            {
                case 0:
                    goto Label_005B;

                case 1:
                    goto Label_005B;

                case 2:
                    goto Label_005B;

                case 3:
                    goto Label_005B;
            }
            switch ((code - 0x13ec))
            {
                case 0:
                    goto Label_0054;

                case 1:
                    goto Label_0054;

                case 2:
                    goto Label_0054;
            }
            if (code == 0x125c)
            {
                goto Label_0054;
            }
            goto Label_0062;
        Label_0054:
            this.OnBack();
            return;
        Label_005B:
            this.OnBack();
            return;
        Label_0062:
            this.OnRetry();
            return;
        Label_0069:
            if (this.m_Api != null)
            {
                goto Label_00BE;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Migrate>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00A4;
            }
            this.OnRetry();
            return;
        Label_00A4:
            MonoSingleton<GameManager>.Instance.SaveAuth(response.body.old_device_id);
            goto Label_010F;
        Label_00BE:
            if (this.m_Api != 1)
            {
                goto Label_010F;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Migrate>>(&www.text);
            DebugUtility.Assert((response2 == null) == 0, "res == null");
            if (response2.body != null)
            {
                goto Label_00FA;
            }
            this.OnRetry();
            return;
        Label_00FA:
            MonoSingleton<GameManager>.Instance.SaveAuth(response2.body.old_device_id);
        Label_010F:
            MonoSingleton<GameManager>.Instance.InitAuth();
            GameUtility.ClearPreferences();
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        private API m_Api
        {
            [CompilerGenerated]
            get
            {
                return this.<m_Api>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<m_Api>k__BackingField = value;
                return;
            }
        }

        private enum API
        {
            Normal,
            FgG
        }
    }
}

