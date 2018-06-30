namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class RankMatchSeasonRewardList : SRPG_ListBase
    {
        [SerializeField]
        private RankMatchClassListItem ListItem;

        public RankMatchSeasonRewardList()
        {
            base..ctor();
            return;
        }

        protected override void Start()
        {
            <Start>c__AnonStorey398 storey;
            storey = new <Start>c__AnonStorey398();
            storey.<>f__this = this;
            base.Start();
            if ((this.ListItem == null) == null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            this.ListItem.get_gameObject().SetActive(0);
            if ((this.ListItem.RewardUnit == null) == null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            this.ListItem.RewardUnit.SetActive(0);
            if ((this.ListItem.RewardItem == null) == null)
            {
                goto Label_0075;
            }
            return;
        Label_0075:
            this.ListItem.RewardItem.SetActive(0);
            if ((this.ListItem.RewardCard == null) == null)
            {
                goto Label_009D;
            }
            return;
        Label_009D:
            this.ListItem.RewardCard.SetActive(0);
            if ((this.ListItem.RewardArtifact == null) == null)
            {
                goto Label_00C5;
            }
            return;
        Label_00C5:
            this.ListItem.RewardArtifact.SetActive(0);
            if ((this.ListItem.RewardAward == null) == null)
            {
                goto Label_00ED;
            }
            return;
        Label_00ED:
            this.ListItem.RewardAward.SetActive(0);
            if ((this.ListItem.RewardGold == null) == null)
            {
                goto Label_0115;
            }
            return;
        Label_0115:
            this.ListItem.RewardGold.SetActive(0);
            if ((this.ListItem.RewardCoin == null) == null)
            {
                goto Label_013D;
            }
            return;
        Label_013D:
            this.ListItem.RewardCoin.SetActive(0);
            storey.gm = MonoSingleton<GameManager>.Instance;
            GlobalVars.RankMatchSeasonReward.ForEach(new Action<List<VersusRankReward>>(storey.<>m__3EA));
            return;
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey398
        {
            internal GameManager gm;
            internal RankMatchSeasonRewardList <>f__this;

            public <Start>c__AnonStorey398()
            {
                base..ctor();
                return;
            }

            internal void <>m__3EA(List<VersusRankReward> vrc)
            {
                <Start>c__AnonStorey399 storey;
                storey = new <Start>c__AnonStorey399();
                storey.<>f__ref$920 = this;
                storey.item = Object.Instantiate<RankMatchClassListItem>(this.<>f__this.ListItem);
                this.<>f__this.AddItem(storey.item);
                storey.item.get_transform().SetParent(this.<>f__this.get_transform(), 0);
                storey.item.get_gameObject().SetActive(1);
                vrc.ForEach(new Action<VersusRankReward>(storey.<>m__3EB));
                return;
            }

            private sealed class <Start>c__AnonStorey399
            {
                internal RankMatchClassListItem item;
                internal RankMatchSeasonRewardList.<Start>c__AnonStorey398 <>f__ref$920;

                public <Start>c__AnonStorey399()
                {
                    base..ctor();
                    return;
                }

                internal unsafe void <>m__3EB(VersusRankReward reward)
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
                            goto Label_006B;

                        case 1:
                            goto Label_00A8;

                        case 2:
                            goto Label_00C0;

                        case 3:
                            goto Label_0116;

                        case 4:
                            goto Label_0030;

                        case 5:
                            goto Label_00D8;
                    }
                    goto Label_0159;
                Label_0030:
                    param = this.<>f__ref$920.gm.GetUnitParam(reward.IName);
                    if (param != null)
                    {
                        goto Label_004E;
                    }
                    return;
                Label_004E:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardUnit);
                    DataSource.Bind<UnitParam>(obj2, param);
                    goto Label_015A;
                Label_006B:
                    param2 = this.<>f__ref$920.gm.GetItemParam(reward.IName);
                    if (param2 != null)
                    {
                        goto Label_0089;
                    }
                    return;
                Label_0089:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardItem);
                    DataSource.Bind<ItemParam>(obj2, param2);
                    flag = 1;
                    goto Label_015A;
                Label_00A8:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardGold);
                    flag = 1;
                    goto Label_015A;
                Label_00C0:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardCoin);
                    flag = 1;
                    goto Label_015A;
                Label_00D8:
                    param3 = this.<>f__ref$920.gm.GetAwardParam(reward.IName);
                    if (param3 != null)
                    {
                        goto Label_00F8;
                    }
                    return;
                Label_00F8:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardAward);
                    DataSource.Bind<AwardParam>(obj2, param3);
                    goto Label_015A;
                Label_0116:
                    param4 = this.<>f__ref$920.gm.MasterParam.GetArtifactParam(reward.IName);
                    if (param4 != null)
                    {
                        goto Label_013B;
                    }
                    return;
                Label_013B:
                    obj2 = Object.Instantiate<GameObject>(this.item.RewardArtifact);
                    DataSource.Bind<ArtifactParam>(obj2, param4);
                    goto Label_015A;
                Label_0159:
                    return;
                Label_015A:
                    if (flag == null)
                    {
                        goto Label_01AB;
                    }
                    transform = obj2.get_transform().FindChild("amount/Text_amount");
                    if ((transform != null) == null)
                    {
                        goto Label_01AB;
                    }
                    text = transform.GetComponent<Text>();
                    if ((text != null) == null)
                    {
                        goto Label_01AB;
                    }
                    text.set_text(&reward.Num.ToString());
                Label_01AB:
                    obj2.get_transform().SetParent(this.item.RewardList, 0);
                    obj2.SetActive(1);
                    return;
                }
            }
        }
    }
}

