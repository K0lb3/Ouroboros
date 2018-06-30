namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiTowerRewardItemUI : MonoBehaviour
    {
        public GameObject unitObj;
        public GameObject itemObj;
        public GameObject amountObj;
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
        public GameObject currentMark;
        public GameObject clearMark;
        public GameObject current_fil;
        public GameObject cleared_fil;
        private int mIdx;

        public MultiTowerRewardItemUI()
        {
            this.mIdx = -1;
            base..ctor();
            return;
        }

        public unsafe void OnDetailClick()
        {
            GameManager manager;
            MultiTowerFloorParam param;
            int num;
            List<MultiTowerRewardItem> list;
            MultiTowerRewardItem item;
            MultiTowerRewardItem.RewardType type;
            string str;
            int num2;
            string str2;
            string str3;
            ItemParam param2;
            ArtifactParam param3;
            UnitParam param4;
            AwardParam param5;
            MultiTowerRewardItem.RewardType type2;
            manager = MonoSingleton<GameManager>.Instance;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            num = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
            list = manager.GetMTFloorReward(param.reward_id, num);
            item = list[this.mIdx];
            type = 1;
            str = string.Empty;
            num2 = 0;
            if (this.mIdx < 0)
            {
                goto Label_008B;
            }
            if (this.mIdx >= list.Count)
            {
                goto Label_008B;
            }
            type = item.type;
            str = item.itemname;
            num2 = item.num;
        Label_008B:
            str2 = string.Empty;
            str3 = string.Empty;
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_00C3;

                case 1:
                    goto Label_00EB;

                case 2:
                    goto Label_013D;

                case 3:
                    goto Label_01A0;

                case 4:
                    goto Label_0179;

                case 5:
                    goto Label_0114;
            }
            goto Label_01C8;
        Label_00C3:
            param2 = manager.GetItemParam(str);
            if (param2 == null)
            {
                goto Label_01C8;
            }
            str2 = param2.name;
            str3 = param2.Expr;
            goto Label_01C8;
        Label_00EB:
            str2 = LocalizedText.Get("sys.COIN");
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num2);
            goto Label_01C8;
        Label_0114:
            str2 = LocalizedText.Get("sys.GOLD");
            str3 = &num2.ToString() + LocalizedText.Get("sys.GOLD");
            goto Label_01C8;
        Label_013D:
            param3 = manager.MasterParam.GetArtifactParam(str);
            if (param3 == null)
            {
                goto Label_01C8;
            }
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param3.name);
            str3 = param3.Expr;
            goto Label_01C8;
        Label_0179:
            param4 = manager.GetUnitParam(str);
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param4.name);
            goto Label_01C8;
        Label_01A0:
            param5 = manager.GetAwardParam(str);
            if (param5 == null)
            {
                goto Label_01C8;
            }
            str2 = param5.name;
            str3 = param5.expr;
        Label_01C8:
            if ((this.rewardDetailName != null) == null)
            {
                goto Label_01E6;
            }
            this.rewardDetailName.set_text(str2);
        Label_01E6:
            if ((this.rewardDetailInfo != null) == null)
            {
                goto Label_0204;
            }
            this.rewardDetailInfo.set_text(str3);
        Label_0204:
            if ((this.pos != null) == null)
            {
                goto Label_0230;
            }
            this.pos.set_position(base.get_gameObject().get_transform().get_position());
        Label_0230:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "OPEN_DETAIL");
            return;
        }

        public unsafe void Refresh(int idx)
        {
            MultiTowerFloorParam param;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if ((this.rewardFloor != null) == null)
            {
                goto Label_004F;
            }
            this.rewardFloor.set_text(GameUtility.HalfNum2FullNum(&param.floor.ToString()) + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
        Label_004F:
            this.SetData(idx);
            return;
        }

        public unsafe void SetData(int idx)
        {
            GameManager manager;
            MultiTowerFloorParam param;
            int num;
            int num2;
            List<MultiTowerRewardItem> list;
            MultiTowerRewardItem item;
            MultiTowerRewardItem.RewardType type;
            string str;
            int num3;
            ArtifactIcon icon;
            DataSource source;
            ItemParam param2;
            ItemData data;
            Transform transform;
            GameParameter parameter;
            GameParameter parameter2;
            GameParameter parameter3;
            DataSource source2;
            ArtifactParam param3;
            ArtifactIcon icon2;
            UnitParam param4;
            UnitData data2;
            ArtifactIcon icon3;
            AwardParam param5;
            Transform transform2;
            IconLoader loader;
            GameParameter parameter4;
            MultiTowerRewardItem.RewardType type2;
            manager = MonoSingleton<GameManager>.Instance;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            num = param.floor;
            if (param != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            num2 = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
            list = manager.GetMTFloorReward(param.reward_id, num2);
            item = list[idx];
            type = 1;
            str = string.Empty;
            num3 = 0;
            if (idx < 0)
            {
                goto Label_0086;
            }
            if (idx >= list.Count)
            {
                goto Label_0086;
            }
            type = item.type;
            str = item.itemname;
            num3 = item.num;
        Label_0086:
            if ((this.itemObj != null) == null)
            {
                goto Label_00A3;
            }
            this.itemObj.SetActive(1);
        Label_00A3:
            if ((this.amountObj != null) == null)
            {
                goto Label_00C0;
            }
            this.amountObj.SetActive(1);
        Label_00C0:
            if ((this.unitObj != null) == null)
            {
                goto Label_00DD;
            }
            this.unitObj.SetActive(0);
        Label_00DD:
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_0107;

                case 1:
                    goto Label_029A;

                case 2:
                    goto Label_0430;

                case 3:
                    goto Label_05BF;

                case 4:
                    goto Label_04FB;

                case 5:
                    goto Label_0365;
            }
            goto Label_06E2;
        Label_0107:
            if ((this.itemObj != null) == null)
            {
                goto Label_06E2;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            icon = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon != null) == null)
            {
                goto Label_014B;
            }
            icon.set_enabled(0);
        Label_014B:
            this.itemObj.SetActive(1);
            source = this.itemObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_0178;
            }
            source.Clear();
        Label_0178:
            source = this.amountObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_0199;
            }
            source.Clear();
        Label_0199:
            param2 = manager.GetItemParam(str);
            DataSource.Bind<ItemParam>(this.itemObj, param2);
            data = new ItemData();
            data.Setup(0L, param2, num3);
            DataSource.Bind<ItemData>(this.amountObj, data);
            transform = this.itemObj.get_transform().FindChild("icon");
            if ((transform != null) == null)
            {
                goto Label_0214;
            }
            parameter = transform.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_0214;
            }
            parameter.set_enabled(1);
        Label_0214:
            GameParameter.UpdateAll(this.itemObj);
            if ((this.iconParam != null) == null)
            {
                goto Label_023B;
            }
            this.iconParam.UpdateValue();
        Label_023B:
            if ((this.frameParam != null) == null)
            {
                goto Label_0257;
            }
            this.frameParam.UpdateValue();
        Label_0257:
            if ((this.rewardName != null) == null)
            {
                goto Label_06E2;
            }
            this.rewardName.set_text(param2.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num3));
            goto Label_06E2;
        Label_029A:
            if ((this.itemTex != null) == null)
            {
                goto Label_02DE;
            }
            parameter2 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter2 != null) == null)
            {
                goto Label_02CD;
            }
            parameter2.set_enabled(0);
        Label_02CD:
            this.itemTex.set_texture(this.coinTex);
        Label_02DE:
            if ((this.frameTex != null) == null)
            {
                goto Label_0311;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_0311;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_0311:
            if ((this.rewardName != null) == null)
            {
                goto Label_0343;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num3));
        Label_0343:
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            this.amountObj.SetActive(0);
            goto Label_06E2;
        Label_0365:
            if ((this.itemTex != null) == null)
            {
                goto Label_03A9;
            }
            parameter3 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter3 != null) == null)
            {
                goto Label_0398;
            }
            parameter3.set_enabled(0);
        Label_0398:
            this.itemTex.set_texture(this.goldTex);
        Label_03A9:
            if ((this.frameTex != null) == null)
            {
                goto Label_03DC;
            }
            if ((this.goldBase != null) == null)
            {
                goto Label_03DC;
            }
            this.frameTex.set_sprite(this.goldBase);
        Label_03DC:
            if ((this.rewardName != null) == null)
            {
                goto Label_040E;
            }
            this.rewardName.set_text(&num3.ToString() + LocalizedText.Get("sys.GOLD"));
        Label_040E:
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            this.amountObj.SetActive(0);
            goto Label_06E2;
        Label_0430:
            if ((this.itemObj != null) == null)
            {
                goto Label_06E2;
            }
            source2 = this.itemObj.GetComponent<DataSource>();
            if ((source2 != null) == null)
            {
                goto Label_0462;
            }
            source2.Clear();
        Label_0462:
            param3 = manager.MasterParam.GetArtifactParam(str);
            DataSource.Bind<ArtifactParam>(this.itemObj, param3);
            icon2 = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon2 != null) == null)
            {
                goto Label_06E2;
            }
            icon2.set_enabled(1);
            icon2.UpdateValue();
            if ((this.rewardName != null) == null)
            {
                goto Label_04D9;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param3.name));
        Label_04D9:
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            this.amountObj.SetActive(0);
            goto Label_06E2;
        Label_04FB:
            if ((this.unitObj != null) == null)
            {
                goto Label_06E2;
            }
            if ((this.itemObj != null) == null)
            {
                goto Label_0529;
            }
            this.itemObj.SetActive(0);
        Label_0529:
            this.unitObj.SetActive(1);
            param4 = manager.GetUnitParam(str);
            DebugUtility.Assert((param4 == null) == 0, "Invalid unit:" + str);
            data2 = new UnitData();
            data2.Setup(str, 0, 1, 0, null, 1, 0, 0);
            DataSource.Bind<UnitData>(this.unitObj, data2);
            GameParameter.UpdateAll(this.unitObj);
            if ((this.rewardName != null) == null)
            {
                goto Label_06E2;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param4.name));
            goto Label_06E2;
        Label_05BF:
            if ((this.itemObj != null) == null)
            {
                goto Label_06E2;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            icon3 = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon3 != null) == null)
            {
                goto Label_0603;
            }
            icon3.set_enabled(0);
        Label_0603:
            this.itemObj.SetActive(1);
            param5 = manager.GetAwardParam(str);
            transform2 = this.itemObj.get_transform().FindChild("icon");
            if ((transform2 != null) == null)
            {
                goto Label_068D;
            }
            loader = GameUtility.RequireComponent<IconLoader>(transform2.get_gameObject());
            if (string.IsNullOrEmpty(param5.icon) != null)
            {
                goto Label_066F;
            }
            loader.ResourcePath = AssetPath.ItemIcon(param5.icon);
        Label_066F:
            parameter4 = transform2.GetComponent<GameParameter>();
            if ((parameter4 != null) == null)
            {
                goto Label_068D;
            }
            parameter4.set_enabled(0);
        Label_068D:
            if ((this.frameTex != null) == null)
            {
                goto Label_06C0;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_06C0;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_06C0:
            if ((this.amountObj != null) == null)
            {
                goto Label_06E2;
            }
            this.amountObj.SetActive(0);
        Label_06E2:
            this.mIdx = idx;
            if ((this.currentMark != null) == null)
            {
                goto Label_070E;
            }
            this.currentMark.SetActive(param.floor == num);
        Label_070E:
            if ((this.current_fil != null) == null)
            {
                goto Label_0733;
            }
            this.current_fil.SetActive(param.floor == num);
        Label_0733:
            if ((this.clearMark != null) == null)
            {
                goto Label_075A;
            }
            this.clearMark.SetActive((param.floor - 1) < num);
        Label_075A:
            if ((this.cleared_fil != null) == null)
            {
                goto Label_0781;
            }
            this.cleared_fil.SetActive((param.floor - 1) < num);
        Label_0781:
            return;
        }

        private void Start()
        {
        }
    }
}

