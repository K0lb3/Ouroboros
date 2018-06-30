namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqTower", 0x7fe5), Pin(1, "Success", 1, 10)]
    public class FlowNode_ReqTower : FlowNode_Network
    {
        public FlowNode_ReqTower()
        {
            base..ctor();
            return;
        }

        private void Deserialize(JSON_ReqTowerResuponse json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            MonoSingleton<GameManager>.Instance.Deserialize(json);
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0048;
            }
            if (Network.Mode != null)
            {
                goto Label_0038;
            }
            base.ExecRequest(new ReqTower(GlobalVars.SelectedTowerID, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0048;
        Label_0038:
            GlobalVars.SelectedTowerID = "QE_TW_BABEL";
            this.Success();
        Label_0048:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqTowerResuponse> response;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqTowerResuponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
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

