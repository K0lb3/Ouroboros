namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0, "所持称号取得", 0, 0), NodeType("System/ReqAwardList", 0x7fe5), Pin(10, "Success", 1, 10), Pin(11, "Failure", 1, 11)]
    public class FlowNode_ReqAwardList : FlowNode_Network
    {
        [DropTarget(typeof(GameObject), true), ShowInInfo]
        public GameObject Target;
        public MODE mMode;

        public FlowNode_ReqAwardList()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0024;
            }
            base.ExecRequest(new ReqAwardList(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ResAwardList> response;
            AwardList list;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResAwardList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_004C;
            }
            this.Failure();
            return;
        Label_004C:
            if (this.mMode != null)
            {
                goto Label_00A7;
            }
            if ((this.Target == null) != null)
            {
                goto Label_007E;
            }
            if ((this.Target.GetComponent<AwardList>() == null) == null)
            {
                goto Label_0085;
            }
        Label_007E:
            this.Failure();
            return;
        Label_0085:
            this.Target.GetComponent<AwardList>().SetOpenAwards(response.body.awards);
            goto Label_00CF;
        Label_00A7:
            if (this.mMode != 1)
            {
                goto Label_00CF;
            }
            MonoSingleton<GameManager>.Instance.Player.SetHaveAward(response.body.awards);
        Label_00CF:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        public enum MODE
        {
            SetAwardList,
            SetPlayerAward
        }
    }
}

