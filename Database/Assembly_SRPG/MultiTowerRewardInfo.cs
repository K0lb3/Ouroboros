namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiTowerRewardInfo : MonoBehaviour
    {
        public GameObject unitObj;
        public GameObject itemObj;
        public GameObject amountObj;
        public GameObject artifactObj;
        public GameParameter iconParam;
        public GameParameter frameParam;
        public RawImage itemTex;
        public Image frameTex;
        public Texture coinTex;
        public Texture goldTex;
        public Sprite coinBase;
        public Sprite goldBase;
        public Text rewardName;
        public Text rewardFloor;
        public RectTransform pos;
        public Text rewardDetailName;
        public Text rewardDetailInfo;
        public GameObject amountOther;
        public Text amountCount;
        public bool amountDisp;
        public Text Artifactamount;

        public MultiTowerRewardInfo()
        {
            base..ctor();
            return;
        }

        public unsafe void OnDetailClick()
        {
            GameManager manager;
            MultiTowerRewardItem item;
            MultiTowerRewardItem.RewardType type;
            string str;
            int num;
            string str2;
            string str3;
            ItemParam param;
            ArtifactParam param2;
            UnitParam param3;
            AwardParam param4;
            MultiTowerRewardItem.RewardType type2;
            manager = MonoSingleton<GameManager>.Instance;
            item = DataSource.FindDataOfClass<MultiTowerRewardItem>(base.get_gameObject(), null);
            if (item != null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            type = item.type;
            str = item.itemname;
            num = 0;
            str2 = string.Empty;
            str3 = string.Empty;
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_0062;

                case 1:
                    goto Label_0089;

                case 2:
                    goto Label_00DB;

                case 3:
                    goto Label_013C;

                case 4:
                    goto Label_0116;

                case 5:
                    goto Label_00B2;
            }
            goto Label_0163;
        Label_0062:
            param = manager.GetItemParam(str);
            if (param == null)
            {
                goto Label_0163;
            }
            str2 = param.name;
            str3 = param.Expr;
            goto Label_0163;
        Label_0089:
            str2 = LocalizedText.Get("sys.COIN");
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num);
            goto Label_0163;
        Label_00B2:
            str2 = LocalizedText.Get("sys.GOLD");
            str3 = &num.ToString() + LocalizedText.Get("sys.GOLD");
            goto Label_0163;
        Label_00DB:
            param2 = manager.MasterParam.GetArtifactParam(str);
            if (param2 == null)
            {
                goto Label_0163;
            }
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param2.name);
            str3 = param2.Expr;
            goto Label_0163;
        Label_0116:
            param3 = manager.GetUnitParam(str);
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param3.name);
            goto Label_0163;
        Label_013C:
            param4 = manager.GetAwardParam(str);
            if (param4 == null)
            {
                goto Label_0163;
            }
            str2 = param4.name;
            str3 = param4.expr;
        Label_0163:
            if ((this.rewardDetailName != null) == null)
            {
                goto Label_0181;
            }
            this.rewardDetailName.set_text(str2);
        Label_0181:
            if ((this.rewardDetailInfo != null) == null)
            {
                goto Label_019F;
            }
            this.rewardDetailInfo.set_text(str3);
        Label_019F:
            if ((this.pos != null) == null)
            {
                goto Label_01CB;
            }
            this.pos.set_position(base.get_gameObject().get_transform().get_position());
        Label_01CB:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "OPEN_DETAIL");
            return;
        }

        public void Refresh()
        {
            this.SetData();
            return;
        }

        private unsafe void SetData()
        {
            GameManager manager;
            MultiTowerRewardItem item;
            MultiTowerRewardItem.RewardType type;
            string str;
            int num;
            DataSource source;
            ItemParam param;
            ItemData data;
            Transform transform;
            GameParameter parameter;
            GameParameter parameter2;
            GameParameter parameter3;
            DataSource source2;
            ArtifactParam param2;
            ArtifactIcon icon;
            UnitParam param3;
            UnitData data2;
            AwardParam param4;
            Transform transform2;
            IconLoader loader;
            GameParameter parameter4;
            MultiTowerRewardItem.RewardType type2;
            manager = MonoSingleton<GameManager>.Instance;
            item = DataSource.FindDataOfClass<MultiTowerRewardItem>(base.get_gameObject(), null);
            if (item != null)
            {
                goto Label_0093;
            }
            if ((this.itemObj != null) == null)
            {
                goto Label_0036;
            }
            this.itemObj.SetActive(0);
        Label_0036:
            if ((this.amountObj != null) == null)
            {
                goto Label_0053;
            }
            this.amountObj.SetActive(0);
        Label_0053:
            if ((this.unitObj != null) == null)
            {
                goto Label_0070;
            }
            this.unitObj.SetActive(0);
        Label_0070:
            if ((this.rewardName != null) == null)
            {
                goto Label_0092;
            }
            this.rewardName.get_gameObject().SetActive(0);
        Label_0092:
            return;
        Label_0093:
            type = item.type;
            str = item.itemname;
            num = item.num;
            if ((this.itemObj != null) == null)
            {
                goto Label_00C6;
            }
            this.itemObj.SetActive(1);
        Label_00C6:
            if ((this.amountObj != null) == null)
            {
                goto Label_00E3;
            }
            this.amountObj.SetActive(1);
        Label_00E3:
            if ((this.unitObj != null) == null)
            {
                goto Label_0100;
            }
            this.unitObj.SetActive(0);
        Label_0100:
            if ((this.rewardName != null) == null)
            {
                goto Label_0122;
            }
            this.rewardName.get_gameObject().SetActive(1);
        Label_0122:
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_014B;

                case 1:
                    goto Label_02F5;

                case 2:
                    goto Label_0585;

                case 3:
                    goto Label_079A;

                case 4:
                    goto Label_069F;

                case 5:
                    goto Label_043D;
            }
            goto Label_08F7;
        Label_014B:
            if ((this.artifactObj != null) == null)
            {
                goto Label_0168;
            }
            this.artifactObj.SetActive(0);
        Label_0168:
            if ((this.itemObj != null) == null)
            {
                goto Label_08F7;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_08F7;
            }
            this.itemObj.SetActive(1);
            source = this.itemObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_01B7;
            }
            source.Clear();
        Label_01B7:
            source = this.amountObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_01D8;
            }
            source.Clear();
        Label_01D8:
            param = manager.GetItemParam(str);
            DataSource.Bind<ItemParam>(this.itemObj, param);
            data = new ItemData();
            data.Setup(0L, param, num);
            DataSource.Bind<ItemData>(this.amountObj, data);
            transform = this.itemObj.get_transform().FindChild("icon");
            if ((transform != null) == null)
            {
                goto Label_0252;
            }
            parameter = transform.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_0252;
            }
            parameter.set_enabled(1);
        Label_0252:
            GameParameter.UpdateAll(this.itemObj);
            if ((this.iconParam != null) == null)
            {
                goto Label_0279;
            }
            this.iconParam.UpdateValue();
        Label_0279:
            if ((this.frameParam != null) == null)
            {
                goto Label_0295;
            }
            this.frameParam.UpdateValue();
        Label_0295:
            if ((this.rewardName != null) == null)
            {
                goto Label_02D3;
            }
            this.rewardName.set_text(param.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num));
        Label_02D3:
            if ((this.amountOther != null) == null)
            {
                goto Label_08F7;
            }
            this.amountOther.SetActive(0);
            goto Label_08F7;
        Label_02F5:
            if ((this.artifactObj != null) == null)
            {
                goto Label_0312;
            }
            this.artifactObj.SetActive(0);
        Label_0312:
            if ((this.itemTex != null) == null)
            {
                goto Label_0356;
            }
            parameter2 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter2 != null) == null)
            {
                goto Label_0345;
            }
            parameter2.set_enabled(0);
        Label_0345:
            this.itemTex.set_texture(this.coinTex);
        Label_0356:
            if ((this.frameTex != null) == null)
            {
                goto Label_0389;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_0389;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_0389:
            if ((this.rewardName != null) == null)
            {
                goto Label_03BB;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num));
        Label_03BB:
            if ((this.amountObj != null) == null)
            {
                goto Label_03D8;
            }
            this.amountObj.SetActive(0);
        Label_03D8:
            if ((this.amountOther != null) == null)
            {
                goto Label_08F7;
            }
            if (this.amountDisp == null)
            {
                goto Label_042C;
            }
            this.amountOther.SetActive(1);
            if ((this.amountCount != null) == null)
            {
                goto Label_08F7;
            }
            this.amountCount.set_text(&item.num.ToString());
            goto Label_0438;
        Label_042C:
            this.amountOther.SetActive(0);
        Label_0438:
            goto Label_08F7;
        Label_043D:
            if ((this.artifactObj != null) == null)
            {
                goto Label_045A;
            }
            this.artifactObj.SetActive(0);
        Label_045A:
            if ((this.itemTex != null) == null)
            {
                goto Label_049E;
            }
            parameter3 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter3 != null) == null)
            {
                goto Label_048D;
            }
            parameter3.set_enabled(0);
        Label_048D:
            this.itemTex.set_texture(this.goldTex);
        Label_049E:
            if ((this.frameTex != null) == null)
            {
                goto Label_04D1;
            }
            if ((this.goldBase != null) == null)
            {
                goto Label_04D1;
            }
            this.frameTex.set_sprite(this.goldBase);
        Label_04D1:
            if ((this.rewardName != null) == null)
            {
                goto Label_0503;
            }
            this.rewardName.set_text(&num.ToString() + LocalizedText.Get("sys.GOLD"));
        Label_0503:
            if ((this.amountObj != null) == null)
            {
                goto Label_0520;
            }
            this.amountObj.SetActive(0);
        Label_0520:
            if ((this.amountOther != null) == null)
            {
                goto Label_08F7;
            }
            if (this.amountDisp == null)
            {
                goto Label_0574;
            }
            this.amountOther.SetActive(1);
            if ((this.amountCount != null) == null)
            {
                goto Label_08F7;
            }
            this.amountCount.set_text(&item.num.ToString());
            goto Label_0580;
        Label_0574:
            this.amountOther.SetActive(0);
        Label_0580:
            goto Label_08F7;
        Label_0585:
            if ((this.itemObj != null) == null)
            {
                goto Label_05A2;
            }
            this.itemObj.SetActive(0);
        Label_05A2:
            if ((this.artifactObj != null) == null)
            {
                goto Label_08F7;
            }
            this.artifactObj.SetActive(1);
            source2 = this.artifactObj.GetComponent<DataSource>();
            if ((source2 != null) == null)
            {
                goto Label_05E0;
            }
            source2.Clear();
        Label_05E0:
            param2 = manager.MasterParam.GetArtifactParam(str);
            DataSource.Bind<ArtifactParam>(this.artifactObj, param2);
            icon = this.artifactObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon != null) == null)
            {
                goto Label_08F7;
            }
            icon.set_enabled(1);
            icon.UpdateValue();
            if ((this.rewardName != null) == null)
            {
                goto Label_0656;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param2.name));
        Label_0656:
            if ((this.amountObj != null) == null)
            {
                goto Label_0673;
            }
            this.amountObj.SetActive(0);
        Label_0673:
            if ((this.Artifactamount != null) == null)
            {
                goto Label_08F7;
            }
            this.Artifactamount.set_text(&item.num.ToString());
            goto Label_08F7;
        Label_069F:
            if ((this.unitObj != null) == null)
            {
                goto Label_08F7;
            }
            if ((this.itemObj != null) == null)
            {
                goto Label_06CD;
            }
            this.itemObj.SetActive(0);
        Label_06CD:
            if ((this.artifactObj != null) == null)
            {
                goto Label_06EA;
            }
            this.artifactObj.SetActive(0);
        Label_06EA:
            this.unitObj.SetActive(1);
            param3 = manager.GetUnitParam(str);
            DebugUtility.Assert((param3 == null) == 0, "Invalid unit:" + str);
            data2 = new UnitData();
            data2.Setup(str, 0, 1, 0, null, 1, 0, 0);
            DataSource.Bind<UnitData>(this.unitObj, data2);
            GameParameter.UpdateAll(this.unitObj);
            if ((this.rewardName != null) == null)
            {
                goto Label_0778;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param3.name));
        Label_0778:
            if ((this.amountOther != null) == null)
            {
                goto Label_08F7;
            }
            this.amountOther.SetActive(0);
            goto Label_08F7;
        Label_079A:
            if ((this.artifactObj != null) == null)
            {
                goto Label_07B7;
            }
            this.artifactObj.SetActive(0);
        Label_07B7:
            if ((this.itemObj != null) == null)
            {
                goto Label_08F7;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_08F7;
            }
            this.itemObj.SetActive(1);
            param4 = manager.GetAwardParam(str);
            transform2 = this.itemObj.get_transform().FindChild("icon");
            if ((transform2 != null) == null)
            {
                goto Label_0885;
            }
            loader = GameUtility.RequireComponent<IconLoader>(transform2.get_gameObject());
            if (string.IsNullOrEmpty(param4.icon) != null)
            {
                goto Label_0844;
            }
            loader.ResourcePath = AssetPath.ItemIcon(param4.icon);
        Label_0844:
            parameter4 = transform2.GetComponent<GameParameter>();
            if ((parameter4 != null) == null)
            {
                goto Label_0862;
            }
            parameter4.set_enabled(0);
        Label_0862:
            if ((this.rewardName != null) == null)
            {
                goto Label_0885;
            }
            this.rewardName.set_text(param4.name);
        Label_0885:
            if ((this.frameTex != null) == null)
            {
                goto Label_08B8;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_08B8;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_08B8:
            if ((this.amountObj != null) == null)
            {
                goto Label_08D5;
            }
            this.amountObj.SetActive(0);
        Label_08D5:
            if ((this.amountOther != null) == null)
            {
                goto Label_08F7;
            }
            this.amountOther.SetActive(0);
        Label_08F7:
            return;
        }

        private void Start()
        {
        }
    }
}

