namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0x65, "Failure", 1, 0x65), Pin(0, "Get", 0, 0), Pin(100, "Success", 1, 100), NodeType("Multi/GetRankMatchMissionProgress", 0x7fe5), Pin(0x1388, "NoMatchVersion", 1, 0x1388), Pin(0x1770, "MultiMaintenance", 1, 0x1770)]
    public class FlowNode_MultiPlayGetRankMatchMissionProgress : FlowNode_Network
    {
        private const int PIN_IN_RANKMATCH_START = 0x48;
        private const int PIN_IN_RANKMATCH_STATUS = 70;
        private const int PIN_IN_RANKMATCH_CREATE_ROOM = 0x47;

        public FlowNode_MultiPlayGetRankMatchMissionProgress()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            DebugUtility.Log("MultiPlayAPI failure");
            base.set_enabled(0);
            base.ActivateOutputLinks(0x65);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001D;
            }
            base.ExecRequest(new ReqRankMatchMission(new Network.ResponseCallback(this.ResponseCallback)));
        Label_001D:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response> response;
            PlayerData data;
            ReqRankMatchMission.MissionProgress progress;
            ReqRankMatchMission.MissionProgress[] progressArray;
            int num;
            RankMatchMissionState state;
            DebugUtility.Log("OnSuccess");
            if (Network.IsError == null)
            {
                goto Label_00D9;
            }
            if (Network.ErrCode != 0xce5)
            {
                goto Label_0041;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12c2);
            return;
        Label_0041:
            if (Network.ErrCode == 0xe78)
            {
                goto Label_005F;
            }
            if (Network.ErrCode != 0x2718)
            {
                goto Label_007D;
            }
        Label_005F:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1388);
            return;
        Label_007D:
            if (Network.ErrCode == 0xca)
            {
                goto Label_00B9;
            }
            if (Network.ErrCode == 0xcb)
            {
                goto Label_00B9;
            }
            if (Network.ErrCode == 0xce)
            {
                goto Label_00B9;
            }
            if (Network.ErrCode != 0xcd)
            {
                goto Label_00D2;
            }
        Label_00B9:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1770);
            return;
        Label_00D2:
            this.OnFailed();
            return;
        Label_00D9:
            response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0109;
            }
            this.OnFailed();
            return;
        Label_0109:
            if (response.body.missionprogs == null)
            {
                goto Label_0185;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data.RankMatchMissionState.Clear();
            progressArray = response.body.missionprogs;
            num = 0;
            goto Label_017B;
        Label_0143:
            progress = progressArray[num];
            state = new RankMatchMissionState();
            state.Deserialize(progress.iname, progress.prog, progress.rewarded_at);
            data.RankMatchMissionState.Add(state);
            num += 1;
        Label_017B:
            if (num < ((int) progressArray.Length))
            {
                goto Label_0143;
            }
        Label_0185:
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            DebugUtility.Log("MultiPlayAPI success");
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

