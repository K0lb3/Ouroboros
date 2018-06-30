namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusTowerRewardItem : MonoBehaviour
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
        private REWARD_TYPE mType;
        private int mSeasonIdx;

        public VersusTowerRewardItem()
        {
            this.mSeasonIdx = -1;
            base..ctor();
            return;
        }

        public unsafe void OnDetailClick()
        {
            GameManager manager;
            VersusTowerParam param;
            VERSUS_ITEM_TYPE versus_item_type;
            string str;
            int num;
            string str2;
            string str3;
            ItemParam param2;
            ArtifactParam param3;
            UnitParam param4;
            AwardParam param5;
            VERSUS_ITEM_TYPE versus_item_type2;
            manager = MonoSingleton<GameManager>.Instance;
            param = DataSource.FindDataOfClass<VersusTowerParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            versus_item_type = 0;
            str = string.Empty;
            num = 0;
            if (this.mType != null)
            {
                goto Label_0048;
            }
            versus_item_type = param.ArrivalItemType;
            str = param.ArrivalIteminame;
            goto Label_00AE;
        Label_0048:
            if (this.mSeasonIdx < 0)
            {
                goto Label_00AE;
            }
            if (this.mSeasonIdx >= ((int) param.SeasonIteminame.Length))
            {
                goto Label_00AE;
            }
            versus_item_type = param.SeasonItemType[this.mSeasonIdx];
            str = *(&(param.SeasonIteminame[this.mSeasonIdx]));
            num = *(&(param.SeasonItemnum[this.mSeasonIdx]));
        Label_00AE:
            str2 = string.Empty;
            str3 = string.Empty;
            versus_item_type2 = versus_item_type;
            switch (versus_item_type2)
            {
                case 0:
                    goto Label_00E3;

                case 1:
                    goto Label_0133;

                case 2:
                    goto Label_010A;

                case 3:
                    goto Label_0197;

                case 4:
                    goto Label_015C;

                case 5:
                    goto Label_01BD;
            }
            goto Label_01E4;
        Label_00E3:
            param2 = manager.GetItemParam(str);
            if (param2 == null)
            {
                goto Label_01E4;
            }
            str2 = param2.name;
            str3 = param2.Expr;
            goto Label_01E4;
        Label_010A:
            str2 = LocalizedText.Get("sys.COIN");
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num);
            goto Label_01E4;
        Label_0133:
            str2 = LocalizedText.Get("sys.GOLD");
            str3 = &num.ToString() + LocalizedText.Get("sys.GOLD");
            goto Label_01E4;
        Label_015C:
            param3 = manager.MasterParam.GetArtifactParam(str);
            if (param3 == null)
            {
                goto Label_01E4;
            }
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param3.name);
            str3 = param3.Expr;
            goto Label_01E4;
        Label_0197:
            param4 = manager.GetUnitParam(str);
            str2 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param4.name);
            goto Label_01E4;
        Label_01BD:
            param5 = manager.GetAwardParam(str);
            if (param5 == null)
            {
                goto Label_01E4;
            }
            str2 = param5.name;
            str3 = param5.expr;
        Label_01E4:
            if ((this.rewardDetailName != null) == null)
            {
                goto Label_0202;
            }
            this.rewardDetailName.set_text(str2);
        Label_0202:
            if ((this.rewardDetailInfo != null) == null)
            {
                goto Label_0220;
            }
            this.rewardDetailInfo.set_text(str3);
        Label_0220:
            if ((this.pos != null) == null)
            {
                goto Label_024C;
            }
            this.pos.set_position(base.get_gameObject().get_transform().get_position());
        Label_024C:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "OPEN_DETAIL");
            return;
        }

        public unsafe void Refresh(REWARD_TYPE type, int idx)
        {
            VersusTowerParam param;
            param = DataSource.FindDataOfClass<VersusTowerParam>(base.get_gameObject(), null);
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
            this.rewardFloor.set_text(GameUtility.HalfNum2FullNum(&param.Floor.ToString()) + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
        Label_004F:
            this.SetData(type, idx);
            return;
        }

        public unsafe void SetData(REWARD_TYPE type, int idx)
        {
            GameManager manager;
            VersusTowerParam param;
            int num;
            VERSUS_ITEM_TYPE versus_item_type;
            string str;
            int num2;
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
            VERSUS_ITEM_TYPE versus_item_type2;
            manager = MonoSingleton<GameManager>.Instance;
            param = DataSource.FindDataOfClass<VersusTowerParam>(base.get_gameObject(), null);
            num = manager.Player.VersusTowerFloor;
            if (param != null)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            versus_item_type = 0;
            str = string.Empty;
            num2 = 0;
            if (type != null)
            {
                goto Label_005E;
            }
            versus_item_type = param.ArrivalItemType;
            str = param.ArrivalIteminame;
            num2 = param.ArrivalItemNum;
            goto Label_00AC;
        Label_005E:
            if (idx < 0)
            {
                goto Label_00AC;
            }
            if (idx >= ((int) param.SeasonIteminame.Length))
            {
                goto Label_00AC;
            }
            versus_item_type = param.SeasonItemType[idx];
            str = *(&(param.SeasonIteminame[idx]));
            num2 = *(&(param.SeasonItemnum[idx]));
        Label_00AC:
            if ((this.itemObj != null) == null)
            {
                goto Label_00C9;
            }
            this.itemObj.SetActive(1);
        Label_00C9:
            if ((this.amountObj != null) == null)
            {
                goto Label_00E6;
            }
            this.amountObj.SetActive(1);
        Label_00E6:
            if ((this.unitObj != null) == null)
            {
                goto Label_0103;
            }
            this.unitObj.SetActive(0);
        Label_0103:
            versus_item_type2 = versus_item_type;
            switch (versus_item_type2)
            {
                case 0:
                    goto Label_012A;

                case 1:
                    goto Label_0388;

                case 2:
                    goto Label_02BD;

                case 3:
                    goto Label_051E;

                case 4:
                    goto Label_0453;

                case 5:
                    goto Label_05E2;
            }
            goto Label_0705;
        Label_012A:
            if ((this.itemObj != null) == null)
            {
                goto Label_0705;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            icon = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon != null) == null)
            {
                goto Label_016E;
            }
            icon.set_enabled(0);
        Label_016E:
            this.itemObj.SetActive(1);
            source = this.itemObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_019B;
            }
            source.Clear();
        Label_019B:
            source = this.amountObj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_01BC;
            }
            source.Clear();
        Label_01BC:
            param2 = manager.GetItemParam(str);
            DataSource.Bind<ItemParam>(this.itemObj, param2);
            data = new ItemData();
            data.Setup(0L, param2, num2);
            DataSource.Bind<ItemData>(this.amountObj, data);
            transform = this.itemObj.get_transform().FindChild("icon");
            if ((transform != null) == null)
            {
                goto Label_0237;
            }
            parameter = transform.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_0237;
            }
            parameter.set_enabled(1);
        Label_0237:
            GameParameter.UpdateAll(this.itemObj);
            if ((this.iconParam != null) == null)
            {
                goto Label_025E;
            }
            this.iconParam.UpdateValue();
        Label_025E:
            if ((this.frameParam != null) == null)
            {
                goto Label_027A;
            }
            this.frameParam.UpdateValue();
        Label_027A:
            if ((this.rewardName != null) == null)
            {
                goto Label_0705;
            }
            this.rewardName.set_text(param2.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num2));
            goto Label_0705;
        Label_02BD:
            if ((this.itemTex != null) == null)
            {
                goto Label_0301;
            }
            parameter2 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter2 != null) == null)
            {
                goto Label_02F0;
            }
            parameter2.set_enabled(0);
        Label_02F0:
            this.itemTex.set_texture(this.coinTex);
        Label_0301:
            if ((this.frameTex != null) == null)
            {
                goto Label_0334;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_0334;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_0334:
            if ((this.rewardName != null) == null)
            {
                goto Label_0366;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num2));
        Label_0366:
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            this.amountObj.SetActive(0);
            goto Label_0705;
        Label_0388:
            if ((this.itemTex != null) == null)
            {
                goto Label_03CC;
            }
            parameter3 = this.itemTex.GetComponent<GameParameter>();
            if ((parameter3 != null) == null)
            {
                goto Label_03BB;
            }
            parameter3.set_enabled(0);
        Label_03BB:
            this.itemTex.set_texture(this.goldTex);
        Label_03CC:
            if ((this.frameTex != null) == null)
            {
                goto Label_03FF;
            }
            if ((this.goldBase != null) == null)
            {
                goto Label_03FF;
            }
            this.frameTex.set_sprite(this.goldBase);
        Label_03FF:
            if ((this.rewardName != null) == null)
            {
                goto Label_0431;
            }
            this.rewardName.set_text(&num2.ToString() + LocalizedText.Get("sys.GOLD"));
        Label_0431:
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            this.amountObj.SetActive(0);
            goto Label_0705;
        Label_0453:
            if ((this.itemObj != null) == null)
            {
                goto Label_0705;
            }
            source2 = this.itemObj.GetComponent<DataSource>();
            if ((source2 != null) == null)
            {
                goto Label_0485;
            }
            source2.Clear();
        Label_0485:
            param3 = manager.MasterParam.GetArtifactParam(str);
            DataSource.Bind<ArtifactParam>(this.itemObj, param3);
            icon2 = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon2 != null) == null)
            {
                goto Label_0705;
            }
            icon2.set_enabled(1);
            icon2.UpdateValue();
            if ((this.rewardName != null) == null)
            {
                goto Label_04FC;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), param3.name));
        Label_04FC:
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            this.amountObj.SetActive(0);
            goto Label_0705;
        Label_051E:
            if ((this.unitObj != null) == null)
            {
                goto Label_0705;
            }
            if ((this.itemObj != null) == null)
            {
                goto Label_054C;
            }
            this.itemObj.SetActive(0);
        Label_054C:
            this.unitObj.SetActive(1);
            param4 = manager.GetUnitParam(str);
            DebugUtility.Assert((param4 == null) == 0, "Invalid unit:" + str);
            data2 = new UnitData();
            data2.Setup(str, 0, 1, 0, null, 1, 0, 0);
            DataSource.Bind<UnitData>(this.unitObj, data2);
            GameParameter.UpdateAll(this.unitObj);
            if ((this.rewardName != null) == null)
            {
                goto Label_0705;
            }
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), param4.name));
            goto Label_0705;
        Label_05E2:
            if ((this.itemObj != null) == null)
            {
                goto Label_0705;
            }
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            icon3 = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((icon3 != null) == null)
            {
                goto Label_0626;
            }
            icon3.set_enabled(0);
        Label_0626:
            this.itemObj.SetActive(1);
            param5 = manager.GetAwardParam(str);
            transform2 = this.itemObj.get_transform().FindChild("icon");
            if ((transform2 != null) == null)
            {
                goto Label_06B0;
            }
            loader = GameUtility.RequireComponent<IconLoader>(transform2.get_gameObject());
            if (string.IsNullOrEmpty(param5.icon) != null)
            {
                goto Label_0692;
            }
            loader.ResourcePath = AssetPath.ItemIcon(param5.icon);
        Label_0692:
            parameter4 = transform2.GetComponent<GameParameter>();
            if ((parameter4 != null) == null)
            {
                goto Label_06B0;
            }
            parameter4.set_enabled(0);
        Label_06B0:
            if ((this.frameTex != null) == null)
            {
                goto Label_06E3;
            }
            if ((this.coinBase != null) == null)
            {
                goto Label_06E3;
            }
            this.frameTex.set_sprite(this.coinBase);
        Label_06E3:
            if ((this.amountObj != null) == null)
            {
                goto Label_0705;
            }
            this.amountObj.SetActive(0);
        Label_0705:
            this.mType = type;
            this.mSeasonIdx = idx;
            if (type != null)
            {
                goto Label_0776;
            }
            if ((this.currentMark != null) == null)
            {
                goto Label_0745;
            }
            this.currentMark.SetActive((param.Floor - 1) == num);
        Label_0745:
            if ((this.current_fil != null) == null)
            {
                goto Label_07CA;
            }
            this.current_fil.SetActive((param.Floor - 1) == num);
            goto Label_07CA;
        Label_0776:
            if ((this.currentMark != null) == null)
            {
                goto Label_07A0;
            }
            this.currentMark.SetActive(param.Floor == num);
        Label_07A0:
            if ((this.current_fil != null) == null)
            {
                goto Label_07CA;
            }
            this.current_fil.SetActive(param.Floor == num);
        Label_07CA:
            if ((this.clearMark != null) == null)
            {
                goto Label_07F6;
            }
            this.clearMark.SetActive((param.Floor - 1) < num);
        Label_07F6:
            if ((this.cleared_fil != null) == null)
            {
                goto Label_0822;
            }
            this.cleared_fil.SetActive((param.Floor - 1) < num);
        Label_0822:
            return;
        }

        private void Start()
        {
        }

        public enum REWARD_TYPE
        {
            Arrival,
            Season
        }
    }
}

