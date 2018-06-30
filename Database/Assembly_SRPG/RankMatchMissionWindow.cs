namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Receive", 1, 10), Pin(0, "Refresh", 0, 0)]
    public class RankMatchMissionWindow : SRPG_ListBase, IFlowInterface
    {
        [SerializeField]
        private RankMatchMissionItem ListItem;
        private APIType mAPIType;

        public RankMatchMissionWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        public void ReceiveReward(VersusRankMissionParam mission)
        {
            List<VersusRankReward> list;
            <ReceiveReward>c__AnonStorey393 storey;
            storey = new <ReceiveReward>c__AnonStorey393();
            storey.gm = MonoSingleton<GameManager>.Instance;
            storey.gm.Player.RewardedRankMatchMission(mission.IName);
            storey.param = new RewardData();
            storey.gm.GetVersusRankClassRewardList(mission.RewardId).ForEach(new Action<VersusRankReward>(storey.<>m__3E5));
            GlobalVars.LastReward.Set(storey.param);
            this.mAPIType = 1;
            Network.RequestAPI(new ReqRankMatchMissionExec(mission.IName, new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }

        private void Refresh()
        {
            if ((this.ListItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.ClearItems();
            this.ListItem.get_gameObject().SetActive(0);
            this.mAPIType = 0;
            Network.RequestAPI(new ReqRankMatchMission(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response> response;
            PlayerData data;
            ReqRankMatchMission.MissionProgress progress;
            ReqRankMatchMission.MissionProgress[] progressArray;
            int num;
            RankMatchMissionState state;
            GameManager manager;
            List<VersusRankMissionParam> list;
            int num2;
            VersusRankMissionParam param;
            RankMatchMissionItem item;
            Network.EErrCode code;
            <ResponseCallback>c__AnonStorey394 storey;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_009E;
            }
            code = Network.ErrCode;
            switch ((code - 0xca))
            {
                case 0:
                    goto Label_008B;

                case 1:
                    goto Label_008B;

                case 2:
                    goto Label_003E;

                case 3:
                    goto Label_008B;

                case 4:
                    goto Label_008B;
            }
        Label_003E:
            if (code == 0xce5)
            {
                goto Label_0067;
            }
            if (code == 0xe78)
            {
                goto Label_0079;
            }
            if (code == 0x2718)
            {
                goto Label_0079;
            }
            goto Label_0098;
        Label_0067:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            return;
        Label_0079:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            return;
        Label_008B:
            Network.RemoveAPI();
            base.set_enabled(0);
            return;
        Label_0098:
            FlowNode_Network.Retry();
            return;
        Label_009E:
            if (this.mAPIType != null)
            {
                goto Label_028C;
            }
            storey = new <ResponseCallback>c__AnonStorey394();
            storey.mission_progs = new Dictionary<string, ReqRankMatchMission.MissionProgress>();
            response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body == null)
            {
                goto Label_0174;
            }
            if (response.body.missionprogs == null)
            {
                goto Label_0174;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data.RankMatchMissionState.Clear();
            progressArray = response.body.missionprogs;
            num = 0;
            goto Label_016A;
        Label_011F:
            progress = progressArray[num];
            state = new RankMatchMissionState();
            state.Deserialize(progress.iname, progress.prog, progress.rewarded_at);
            data.RankMatchMissionState.Add(state);
            storey.mission_progs.Add(progress.iname, progress);
            num += 1;
        Label_016A:
            if (num < ((int) progressArray.Length))
            {
                goto Label_011F;
            }
        Label_0174:
            manager = MonoSingleton<GameManager>.Instance;
            list = manager.GetVersusRankMissionList(manager.RankMatchScheduleId);
            list.Sort(new Comparison<VersusRankMissionParam>(storey.<>m__3E6));
            num2 = 0;
            goto Label_0279;
        Label_01A7:
            param = list[num2];
            if (storey.mission_progs.ContainsKey(param.IName) == null)
            {
                goto Label_01F1;
            }
            if (string.IsNullOrEmpty(storey.mission_progs[param.IName].rewarded_at) != null)
            {
                goto Label_01F1;
            }
            goto Label_0273;
        Label_01F1:
            item = Object.Instantiate<RankMatchMissionItem>(this.ListItem);
            DataSource.Bind<VersusRankMissionParam>(item.get_gameObject(), param);
            if (storey.mission_progs.ContainsKey(param.IName) == null)
            {
                goto Label_0243;
            }
            DataSource.Bind<ReqRankMatchMission.MissionProgress>(item.get_gameObject(), storey.mission_progs[param.IName]);
        Label_0243:
            base.AddItem(item);
            item.get_transform().SetParent(base.get_transform(), 0);
            item.get_gameObject().SetActive(1);
            item.Initialize(this);
        Label_0273:
            num2 += 1;
        Label_0279:
            if (num2 < list.Count)
            {
                goto Label_01A7;
            }
            goto Label_02A0;
        Label_028C:
            if (this.mAPIType != 1)
            {
                goto Label_02A0;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_02A0:
            Network.RemoveAPI();
            return;
        }

        protected override void Start()
        {
            base.Start();
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <ReceiveReward>c__AnonStorey393
        {
            internal GameManager gm;
            internal RewardData param;

            public <ReceiveReward>c__AnonStorey393()
            {
                base..ctor();
                return;
            }

            internal void <>m__3E5(VersusRankReward reward)
            {
                ItemParam param;
                ItemData data;
                UnitParam param2;
                GiftRecieveItemData data2;
                ArtifactParam param3;
                ArtifactRewardData data3;
                RewardType type;
                switch (reward.Type)
                {
                    case 0:
                        goto Label_0086;

                    case 1:
                        goto Label_00D4;

                    case 2:
                        goto Label_00EA;

                    case 3:
                        goto Label_0105;

                    case 4:
                        goto Label_002C;

                    case 5:
                        goto Label_0100;
                }
                goto Label_0154;
            Label_002C:
                if (this.gm.GetUnitParam(reward.IName) != null)
                {
                    goto Label_0045;
                }
                return;
            Label_0045:
                data2 = new GiftRecieveItemData();
                data2.iname = reward.IName;
                data2.num = 1;
                data2.type = 0x80L;
                this.param.GiftRecieveItemDataDic.Add(reward.IName, data2);
                goto Label_0155;
            Label_0086:
                param = this.gm.GetItemParam(reward.IName);
                if (param != null)
                {
                    goto Label_009F;
                }
                return;
            Label_009F:
                data = new ItemData();
                if (data.Setup(0L, param.iname, reward.Num) == null)
                {
                    goto Label_0155;
                }
                this.param.Items.Add(data);
                goto Label_0155;
            Label_00D4:
                this.param.Gold = reward.Num;
                goto Label_0155;
            Label_00EA:
                this.param.Coin = reward.Num;
                goto Label_0155;
            Label_0100:
                goto Label_0155;
            Label_0105:
                param3 = this.gm.MasterParam.GetArtifactParam(reward.IName);
                if (param3 != null)
                {
                    goto Label_0125;
                }
                return;
            Label_0125:
                data3 = new ArtifactRewardData();
                data3.ArtifactParam = param3;
                data3.Num = 1;
                this.param.Artifacts.Add(data3);
                goto Label_0155;
            Label_0154:
                return;
            Label_0155:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <ResponseCallback>c__AnonStorey394
        {
            internal Dictionary<string, ReqRankMatchMission.MissionProgress> mission_progs;

            public <ResponseCallback>c__AnonStorey394()
            {
                base..ctor();
                return;
            }

            internal int <>m__3E6(VersusRankMissionParam m1, VersusRankMissionParam m2)
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                if (this.mission_progs.ContainsKey(m1.IName) == null)
                {
                    goto Label_0064;
                }
                if (string.IsNullOrEmpty(this.mission_progs[m1.IName].rewarded_at) == null)
                {
                    goto Label_0062;
                }
                if (this.mission_progs[m1.IName].prog < m1.IVal)
                {
                    goto Label_0064;
                }
                num = 1;
                goto Label_0064;
            Label_0062:
                num = -1;
            Label_0064:
                if (this.mission_progs.ContainsKey(m2.IName) == null)
                {
                    goto Label_00C4;
                }
                if (string.IsNullOrEmpty(this.mission_progs[m2.IName].rewarded_at) == null)
                {
                    goto Label_00C2;
                }
                if (this.mission_progs[m2.IName].prog < m2.IVal)
                {
                    goto Label_00C4;
                }
                num2 = 1;
                goto Label_00C4;
            Label_00C2:
                num2 = -1;
            Label_00C4:
                if (num == num2)
                {
                    goto Label_00CF;
                }
                return (num2 - num);
            Label_00CF:
                return m1.IName.CompareTo(m2.IName);
            }
        }

        private enum APIType
        {
            MISSION,
            MISSION_EXEC
        }
    }
}

