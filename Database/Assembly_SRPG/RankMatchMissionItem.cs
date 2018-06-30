namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class RankMatchMissionItem : ListItemEvents
    {
        [SerializeField]
        private GameObject Completed;
        [SerializeField]
        private Button GetRewardButton;
        [SerializeField]
        private GameObject Count;
        [SerializeField]
        private GameObject RewardUnit;
        [SerializeField]
        private GameObject RewardItem;
        [SerializeField]
        private GameObject RewardCard;
        [SerializeField]
        private GameObject RewardArtifact;
        [SerializeField]
        private GameObject RewardAward;
        [SerializeField]
        private GameObject RewardGold;
        [SerializeField]
        private GameObject RewardCoin;
        [SerializeField]
        private Transform RewardList;
        private RankMatchMissionWindow mWindow;

        public RankMatchMissionItem()
        {
            base..ctor();
            return;
        }

        public void Initialize(RankMatchMissionWindow window)
        {
            VersusRankMissionParam param;
            int num;
            ReqRankMatchMission.MissionProgress progress;
            List<VersusRankReward> list;
            <Initialize>c__AnonStorey392 storey;
            storey = new <Initialize>c__AnonStorey392();
            storey.<>f__this = this;
            if ((this.RewardUnit != null) == null)
            {
                goto Label_002C;
            }
            this.RewardUnit.SetActive(0);
        Label_002C:
            if ((this.RewardItem != null) == null)
            {
                goto Label_0049;
            }
            this.RewardItem.SetActive(0);
        Label_0049:
            if ((this.RewardCard != null) == null)
            {
                goto Label_0066;
            }
            this.RewardCard.SetActive(0);
        Label_0066:
            if ((this.RewardArtifact != null) == null)
            {
                goto Label_0083;
            }
            this.RewardArtifact.SetActive(0);
        Label_0083:
            if ((this.RewardAward != null) == null)
            {
                goto Label_00A0;
            }
            this.RewardAward.SetActive(0);
        Label_00A0:
            if ((this.RewardGold != null) == null)
            {
                goto Label_00BD;
            }
            this.RewardGold.SetActive(0);
        Label_00BD:
            if ((this.RewardCoin != null) == null)
            {
                goto Label_00DA;
            }
            this.RewardCoin.SetActive(0);
        Label_00DA:
            this.mWindow = window;
            param = DataSource.FindDataOfClass<VersusRankMissionParam>(base.get_gameObject(), null);
            num = 0;
            progress = DataSource.FindDataOfClass<ReqRankMatchMission.MissionProgress>(base.get_gameObject(), null);
            if (progress == null)
            {
                goto Label_010A;
            }
            num = progress.prog;
        Label_010A:
            if ((this.Completed != null) == null)
            {
                goto Label_0132;
            }
            this.Completed.SetActive((param.IVal > num) == 0);
        Label_0132:
            if ((this.GetRewardButton != null) == null)
            {
                goto Label_0168;
            }
            this.GetRewardButton.set_interactable((param.IVal > num) ? 0 : string.IsNullOrEmpty(progress.rewarded_at));
        Label_0168:
            if ((this.Count != null) == null)
            {
                goto Label_018D;
            }
            this.Count.SetActive(param.IVal > num);
        Label_018D:
            storey.gm = MonoSingleton<GameManager>.Instance;
            storey.gm.GetVersusRankClassRewardList(param.RewardId).ForEach(new Action<VersusRankReward>(storey.<>m__3E4));
            return;
        }

        public void MissionComplate()
        {
            VersusRankMissionParam param;
            param = DataSource.FindDataOfClass<VersusRankMissionParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.mWindow.ReceiveReward(param);
            return;
        }

        [CompilerGenerated]
        private sealed class <Initialize>c__AnonStorey392
        {
            internal GameManager gm;
            internal RankMatchMissionItem <>f__this;

            public <Initialize>c__AnonStorey392()
            {
                base..ctor();
                return;
            }

            internal unsafe void <>m__3E4(VersusRankReward reward)
            {
                GameObject obj2;
                bool flag;
                UnitParam param;
                ItemParam param2;
                AwardParam param3;
                ArtifactParam param4;
                Transform transform;
                Text text;
                RewardType type;
                int num;
                obj2 = null;
                flag = 0;
                switch (reward.Type)
                {
                    case 0:
                        goto Label_0066;

                    case 1:
                        goto Label_009E;

                    case 2:
                        goto Label_00B6;

                    case 3:
                        goto Label_0107;

                    case 4:
                        goto Label_0030;

                    case 5:
                        goto Label_00CE;
                }
                goto Label_0145;
            Label_0030:
                param = this.gm.GetUnitParam(reward.IName);
                if (param != null)
                {
                    goto Label_0049;
                }
                return;
            Label_0049:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardUnit);
                DataSource.Bind<UnitParam>(obj2, param);
                goto Label_0146;
            Label_0066:
                param2 = this.gm.GetItemParam(reward.IName);
                if (param2 != null)
                {
                    goto Label_007F;
                }
                return;
            Label_007F:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardItem);
                DataSource.Bind<ItemParam>(obj2, param2);
                flag = 1;
                goto Label_0146;
            Label_009E:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardGold);
                flag = 1;
                goto Label_0146;
            Label_00B6:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardCoin);
                flag = 1;
                goto Label_0146;
            Label_00CE:
                param3 = this.gm.GetAwardParam(reward.IName);
                if (param3 != null)
                {
                    goto Label_00E9;
                }
                return;
            Label_00E9:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardAward);
                DataSource.Bind<AwardParam>(obj2, param3);
                goto Label_0146;
            Label_0107:
                param4 = this.gm.MasterParam.GetArtifactParam(reward.IName);
                if (param4 != null)
                {
                    goto Label_0127;
                }
                return;
            Label_0127:
                obj2 = Object.Instantiate<GameObject>(this.<>f__this.RewardArtifact);
                DataSource.Bind<ArtifactParam>(obj2, param4);
                goto Label_0146;
            Label_0145:
                return;
            Label_0146:
                if (flag == null)
                {
                    goto Label_0197;
                }
                transform = obj2.get_transform().FindChild("amount/Text_amount");
                if ((transform != null) == null)
                {
                    goto Label_0197;
                }
                text = transform.GetComponent<Text>();
                if ((text != null) == null)
                {
                    goto Label_0197;
                }
                text.set_text(&reward.Num.ToString());
            Label_0197:
                obj2.get_transform().SetParent(this.<>f__this.RewardList, 0);
                obj2.SetActive(1);
                return;
            }
        }
    }
}

