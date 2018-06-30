namespace SRPG
{
    using GR;
    using Gsc.App;
    using System;

    [Pin(12, "Reset to Title", 1, 12), Pin(11, "Success Offline", 1, 11), Pin(10, "Success Online", 1, 10), Pin(100, "Start Online", 0, 0), NodeType("System/CheckVersion2", 0x7fe5), Pin(0x3e9, "Different Assets", 1, 0x3e9), Pin(0x3e8, "No Version", 1, 0x3e8)]
    public class FlowNode_CheckVersion2 : FlowNode_Network
    {
        public FlowNode_CheckVersion2()
        {
            base..ctor();
            return;
        }

        public void CheckVersionResponseCallback(WWWResult www)
        {
            if (Network.IsNoVersion == null)
            {
                goto Label_0025;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(0x3e8);
            goto Label_0037;
        Label_0025:
            if (FlowNode_Network.HasCommonError(www) != null)
            {
                goto Label_0037;
            }
            this.OnSuccess(www);
        Label_0037:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_004B;
            }
            if (BootLoader.BootStates != 1)
            {
                goto Label_003B;
            }
            base.ExecRequest(new ReqCheckVersion2(Network.Version, new Network.ResponseCallback(this.CheckVersionResponseCallback)));
            base.set_enabled(1);
            goto Label_004B;
        Label_003B:
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
        Label_004B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_VersionInfo> response;
            string str;
            string str2;
            bool flag;
            Network.EErrCode code;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersionInfo>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (Network.IsError == null)
            {
                goto Label_0036;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0036:
            if (response.body != null)
            {
                goto Label_0048;
            }
            this.OnRetry();
            return;
        Label_0048:
            Network.RemoveAPI();
            if (response.body.environments != null)
            {
                goto Label_005E;
            }
            return;
        Label_005E:
            if (response.body.environments.alchemist != null)
            {
                goto Label_0074;
            }
            return;
        Label_0074:
            str = response.body.environments.alchemist.assets;
            str2 = response.body.environments.alchemist.assets_ex;
            if (FlowNode_GsccInit.SettingAssets(str, str2) == null)
            {
                goto Label_00D9;
            }
            if (MonoSingleton<GameManager>.Instance.IsRelogin == null)
            {
                goto Label_00D9;
            }
            MonoSingleton<GameManager>.Instance.IsRelogin = 0;
            base.ActivateOutputLinks(0x3e9);
            goto Label_00E2;
        Label_00D9:
            base.ActivateOutputLinks(10);
        Label_00E2:
            base.set_enabled(0);
            return;
        }

        private class Json_Alchemist
        {
            public string assets;
            public string assets_ex;

            public Json_Alchemist()
            {
                base..ctor();
                return;
            }
        }

        private class Json_Environment
        {
            public FlowNode_CheckVersion2.Json_Alchemist alchemist;

            public Json_Environment()
            {
                base..ctor();
                return;
            }
        }

        private class Json_VersionInfo
        {
            public FlowNode_CheckVersion2.Json_Environment environments;

            public Json_VersionInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

