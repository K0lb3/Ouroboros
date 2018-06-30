namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("UI/Game Parameter")]
    public class GameParameter : MonoBehaviour, IGameParameter
    {
        private const int PARAMETER_CATEGORY_SIZE = 100;
        public static List<GameParameter> Instances;
        private static bool[] mAlwaysUpdate;
        public ParameterTypes ParameterType;
        public int InstanceType;
        public int Index;
        private Slider mSlider;
        private Text mText;
        private InputField mInputField;
        private Animator mAnimator;
        private RawImage mImage;
        private ImageArray mImageArray;
        private Coroutine mUpdateCoroutine;
        private float mNextUpdateTime;
        private string mDefaultValue;
        private Vector2 mDefaultRangeValue;
        private Texture mDefaultImage;
        private Sprite mDefaultSprite;
        private bool mUpdate;
        private bool mIsEmptyGO;
        private bool mStarted;
        [CompilerGenerated]
        private static MatchEvaluator <>f__am$cache14;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache15;
        [CompilerGenerated]
        private static Predicate<MyPhoton.MyPlayer> <>f__am$cache16;

        static GameParameter()
        {
            string[] strArray;
            int num;
            FieldInfo info;
            Instances = new List<GameParameter>();
            strArray = Enum.GetNames(typeof(ParameterTypes));
            mAlwaysUpdate = new bool[(int) strArray.Length];
            num = 0;
            goto Label_0068;
        Label_002E:
            info = typeof(ParameterTypes).GetField(strArray[num]);
            if (info == null)
            {
                goto Label_0064;
            }
            mAlwaysUpdate[num] = ((int) info.GetCustomAttributes(typeof(AlwaysUpdate), 1).Length) > 0;
        Label_0064:
            num += 1;
        Label_0068:
            if (num < ((int) strArray.Length))
            {
                goto Label_002E;
            }
            return;
        }

        public GameParameter()
        {
            this.ParameterType = 0x146;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe string <InternalUpdateValue>m__108(Match m)
        {
            char ch;
            ch = (ushort) (0xff10 + (m.Value[0] - 0x30));
            return &ch.ToString();
        }

        [CompilerGenerated]
        private static bool <InternalUpdateValue>m__10F(PartySlotTypeUnitPair s)
        {
            return (s.Type == 0);
        }

        [CompilerGenerated]
        private static bool <InternalUpdateValue>m__111(MyPhoton.MyPlayer p)
        {
            return (p.playerID == 1);
        }

        private static int AbilityTypeDetailToImageIndex(EAbilitySlot type, EAbilityTypeDetail typeDetail)
        {
            if (typeDetail == 1)
            {
                goto Label_000E;
            }
            if (typeDetail != 2)
            {
                goto Label_0010;
            }
        Label_000E:
            return 0;
        Label_0010:
            if (typeDetail != 4)
            {
                goto Label_0019;
            }
            return 2;
        Label_0019:
            if (typeDetail != 3)
            {
                goto Label_0022;
            }
            return 1;
        Label_0022:
            return 3;
        }

        private unsafe void Awake()
        {
            Instances.Add(this);
            this.mText = base.GetComponent<Text>();
            this.mSlider = base.GetComponent<Slider>();
            this.mInputField = base.GetComponent<InputField>();
            this.mAnimator = base.GetComponent<Animator>();
            this.mImage = base.GetComponent<RawImage>();
            this.mImageArray = base.GetComponent<ImageArray>();
            if ((this.mText != null) == null)
            {
                goto Label_007A;
            }
            this.mDefaultValue = this.mText.get_text();
            goto Label_0138;
        Label_007A:
            if ((this.mSlider != null) == null)
            {
                goto Label_00BC;
            }
            &this.mDefaultRangeValue.x = this.mSlider.get_value();
            &this.mDefaultRangeValue.y = this.mSlider.get_maxValue();
            goto Label_0138;
        Label_00BC:
            if ((this.mInputField != null) == null)
            {
                goto Label_00E3;
            }
            this.mDefaultValue = this.mInputField.get_text();
            goto Label_0138;
        Label_00E3:
            if ((this.mImage != null) == null)
            {
                goto Label_010A;
            }
            this.mDefaultImage = this.mImage.get_texture();
            goto Label_0138;
        Label_010A:
            if ((this.mImageArray != null) == null)
            {
                goto Label_0131;
            }
            this.mDefaultSprite = this.mImageArray.get_sprite();
            goto Label_0138;
        Label_0131:
            this.mIsEmptyGO = 1;
        Label_0138:
            return;
        }

        protected virtual void BatchUpdate()
        {
        }

        private bool CheckUnlockInstanceType()
        {
            return MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.InstanceType);
        }

        private AbilityData GetAbilityData()
        {
            AbilityData data;
            data = null;
            return DataSource.FindDataOfClass<AbilityData>(base.get_gameObject(), null);
        }

        private AbilityParam GetAbilityParam()
        {
            AbilityParam param;
            AbilityData data;
            param = DataSource.FindDataOfClass<AbilityParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0027;
            }
            data = this.GetAbilityData();
            if (data == null)
            {
                goto Label_0027;
            }
            param = data.Param;
        Label_0027:
            return param;
        }

        private ArenaPlayer GetArenaPlayer()
        {
            ArenaPlayerInstanceTypes types;
            types = this.InstanceType;
            if (types == null)
            {
                goto Label_0019;
            }
            if (types == 1)
            {
                goto Label_0026;
            }
        Label_0019:
            return DataSource.FindDataOfClass<ArenaPlayer>(base.get_gameObject(), null);
        Label_0026:
            return GlobalVars.SelectedArenaPlayer;
        }

        private ArtifactData GetArtifactData()
        {
            return DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
        }

        private ArtifactParam GetArtifactParam()
        {
            ArtifactData data;
            QuestParam param;
            ArtifactRewardData data2;
            ArtifactParam param2;
            ArtifactInstanceTypes types;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_0020;

                case 1:
                    goto Label_0047;

                case 2:
                    goto Label_00A5;
            }
            goto Label_00D2;
        Label_0020:
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_003A;
            }
            return data.ArtifactParam;
        Label_003A:
            return DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
        Label_0047:
            param = this.GetQuestParamAuto();
            if (param == null)
            {
                goto Label_00D2;
            }
            if (0 > this.Index)
            {
                goto Label_00D2;
            }
            if (param.bonusObjective == null)
            {
                goto Label_00D2;
            }
            if (this.Index >= ((int) param.bonusObjective.Length))
            {
                goto Label_00D2;
            }
            return MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(param.bonusObjective[this.Index].item);
            goto Label_00D2;
        Label_00A5:
            data2 = DataSource.FindDataOfClass<ArtifactRewardData>(base.get_gameObject(), null);
            if (data2 == null)
            {
                goto Label_00C7;
            }
            param2 = data2.ArtifactParam;
            if (param2 == null)
            {
                goto Label_00C7;
            }
            return param2;
        Label_00C7:
            this.ResetToDefault();
        Label_00D2:
            return null;
        }

        private EnhanceEquipData GetEnhanceEquipData()
        {
            EnhanceEquipData data;
            return DataSource.FindDataOfClass<EnhanceEquipData>(base.get_gameObject(), null);
        }

        private EnhanceMaterial GetEnhanceMaterial()
        {
            return DataSource.FindDataOfClass<EnhanceMaterial>(base.get_gameObject(), null);
        }

        private EquipData GetEquipData()
        {
            EquipData data;
            return DataSource.FindDataOfClass<EquipData>(base.get_gameObject(), null);
        }

        private EquipItemParameter GetEquipItemParameter()
        {
            return DataSource.FindDataOfClass<EquipItemParameter>(base.get_gameObject(), null);
        }

        private EventShopItem GetEventShopItem()
        {
            return DataSource.FindDataOfClass<EventShopItem>(base.get_gameObject(), null);
        }

        private FriendData GetFriendData()
        {
            return DataSource.FindDataOfClass<FriendData>(base.get_gameObject(), null);
        }

        private GachaParam GetGachaParam()
        {
            return DataSource.FindDataOfClass<GachaParam>(base.get_gameObject(), null);
        }

        private int GetImageLength()
        {
            if ((this.mImageArray != null) == null)
            {
                goto Label_001F;
            }
            return (int) this.mImageArray.Images.Length;
        Label_001F:
            return 0;
        }

        private ItemData GetInventoryItemData()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (0 > this.Index)
            {
                goto Label_0038;
            }
            if (this.Index >= ((int) data.Inventory.Length))
            {
                goto Label_0038;
            }
            return data.Inventory[this.Index];
        Label_0038:
            return null;
        }

        private ItemParam GetInventoryItemParam()
        {
            ItemData data;
            data = this.GetInventoryItemData();
            return ((data == null) ? null : data.Param);
        }

        private ItemParam GetItemParam()
        {
            ItemData data;
            PlayerData data2;
            QuestParam param;
            TowerRewardItem item;
            ItemParam param2;
            GameManager manager;
            PlayerData data3;
            VersusTowerParam param3;
            string str;
            ItemParam param4;
            EquipData data4;
            EnhanceMaterial material;
            EnhanceEquipData data5;
            SellItem item2;
            ConsumeItemData data6;
            ItemInstanceTypes types;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_0034;

                case 1:
                    goto Label_005B;

                case 2:
                    goto Label_009D;

                case 3:
                    goto Label_01EA;

                case 4:
                    goto Label_020C;

                case 5:
                    goto Label_023F;

                case 6:
                    goto Label_0272;

                case 7:
                    goto Label_02A5;
            }
            goto Label_02C7;
        Label_0034:
            data = DataSource.FindDataOfClass<ItemData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_004E;
            }
            return data.Param;
        Label_004E:
            return DataSource.FindDataOfClass<ItemParam>(base.get_gameObject(), null);
        Label_005B:
            data2 = MonoSingleton<GameManager>.Instance.Player;
            if (0 > this.Index)
            {
                goto Label_02C7;
            }
            if (this.Index >= ((int) data2.Inventory.Length))
            {
                goto Label_02C7;
            }
            return data2.Inventory[this.Index].Param;
            goto Label_02C7;
        Label_009D:
            param = null;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_00EF;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_00EF;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
            if (param != null)
            {
                goto Label_00EF;
            }
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
        Label_00EF:
            if (param == null)
            {
                goto Label_0132;
            }
            if (param.type != 7)
            {
                goto Label_0132;
            }
            item = this.GetTowerRewardItem();
            if (item != null)
            {
                goto Label_0110;
            }
            return null;
        Label_0110:
            if (item.type == null)
            {
                goto Label_011D;
            }
            return null;
        Label_011D:
            return MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
        Label_0132:
            if (param == null)
            {
                goto Label_0198;
            }
            if (param.IsVersus == null)
            {
                goto Label_0198;
            }
            manager = MonoSingleton<GameManager>.Instance;
            data3 = manager.Player;
            param3 = manager.GetCurrentVersusTowerParam(data3.VersusTowerFloor + 1);
            if (param3 == null)
            {
                goto Label_0196;
            }
            if (param3.ArrivalItemType == null)
            {
                goto Label_017A;
            }
            return null;
        Label_017A:
            str = param3.ArrivalIteminame;
            return manager.GetItemParam(str);
        Label_0196:
            return null;
        Label_0198:
            if (param == null)
            {
                goto Label_02C7;
            }
            if (0 > this.Index)
            {
                goto Label_02C7;
            }
            if (param.bonusObjective == null)
            {
                goto Label_02C7;
            }
            if (this.Index >= ((int) param.bonusObjective.Length))
            {
                goto Label_02C7;
            }
            return MonoSingleton<GameManager>.Instance.GetItemParam(param.bonusObjective[this.Index].item);
            goto Label_02C7;
        Label_01EA:
            data4 = DataSource.FindDataOfClass<EquipData>(base.get_gameObject(), null);
            if (data4 == null)
            {
                goto Label_02C7;
            }
            return data4.ItemParam;
            goto Label_02C7;
        Label_020C:
            material = DataSource.FindDataOfClass<EnhanceMaterial>(base.get_gameObject(), null);
            if (material == null)
            {
                goto Label_02C7;
            }
            if (material.item == null)
            {
                goto Label_02C7;
            }
            return material.item.Param;
            goto Label_02C7;
        Label_023F:
            data5 = DataSource.FindDataOfClass<EnhanceEquipData>(base.get_gameObject(), null);
            if (data5 == null)
            {
                goto Label_02C7;
            }
            if (data5.equip == null)
            {
                goto Label_02C7;
            }
            return data5.equip.ItemParam;
            goto Label_02C7;
        Label_0272:
            item2 = DataSource.FindDataOfClass<SellItem>(base.get_gameObject(), null);
            if (item2 == null)
            {
                goto Label_02C7;
            }
            if (item2.item == null)
            {
                goto Label_02C7;
            }
            return item2.item.Param;
            goto Label_02C7;
        Label_02A5:
            data6 = DataSource.FindDataOfClass<ConsumeItemData>(base.get_gameObject(), null);
            if (data6 == null)
            {
                goto Label_02C7;
            }
            return data6.param;
        Label_02C7:
            return null;
        }

        private JobParam GetJobParam()
        {
            JobParam param;
            JobData data;
            param = DataSource.FindDataOfClass<JobParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0033;
            }
            data = DataSource.FindDataOfClass<JobData>(base.get_gameObject(), null);
            return ((data == null) ? null : data.Param);
        Label_0033:
            return param;
        }

        private SkillParam GetLeaderSkill(PartyData party)
        {
            long num;
            GameManager manager;
            UnitData data;
            num = party.GetUnitUniqueID(party.LeaderIndex);
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num);
            if (data == null)
            {
                goto Label_003D;
            }
            if (data.LeaderSkill == null)
            {
                goto Label_003D;
            }
            return data.LeaderSkill.SkillParam;
        Label_003D:
            return null;
        }

        private PlayerLevelUpInfo GetLevelUpInfo()
        {
            return DataSource.FindDataOfClass<PlayerLevelUpInfo>(base.get_gameObject(), null);
        }

        private LimitedShopItem GetLimitedShopItem()
        {
            return DataSource.FindDataOfClass<LimitedShopItem>(base.get_gameObject(), null);
        }

        private MailData GetMailData()
        {
            return DataSource.FindDataOfClass<MailData>(base.get_gameObject(), null);
        }

        private MapEffectParam GetMapEffectParam()
        {
            return DataSource.FindDataOfClass<MapEffectParam>(base.get_gameObject(), null);
        }

        private JSON_MyPhotonPlayerParam.UnitDataElem GetMultiPlayerUnitData(int index)
        {
            JSON_MyPhotonPlayerParam param;
            <GetMultiPlayerUnitData>c__AnonStorey213 storey;
            storey = new <GetMultiPlayerUnitData>c__AnonStorey213();
            storey.index = index;
            param = this.GetRoomPlayerParam();
            if (param == null)
            {
                goto Label_0025;
            }
            if (param.units != null)
            {
                goto Label_0027;
            }
        Label_0025:
            return null;
        Label_0027:
            return Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(param.units, new Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>(storey.<>m__103));
        }

        private BattleCore.OrderData GetOrderData()
        {
            return DataSource.FindDataOfClass<BattleCore.OrderData>(base.get_gameObject(), null);
        }

        private string GetParamTypeName(ParamTypes type)
        {
            ParamTypes types;
            types = type;
            if (types == 0)
            {
                goto Label_000E;
            }
            goto Label_0010;
        Label_000E:
            return null;
        Label_0010:;
        Label_0015:
            return LocalizedText.Get("sys." + ((ParamTypes) type).ToString());
        }

        private PartyData GetPartyData()
        {
            PartyData data;
            return DataSource.FindDataOfClass<PartyData>(base.get_gameObject(), null);
        }

        private unsafe void GetQuestObjectiveCount(QuestParam questParam, out int compCount, out int maxCount)
        {
            int num;
            *((int*) maxCount) = (int) questParam.bonusObjective.Length;
            *((int*) compCount) = 0;
            num = 0;
            goto Label_0030;
        Label_0014:
            if ((questParam.clear_missions & (1 << (num & 0x1f))) == null)
            {
                goto Label_002C;
            }
            *((int*) compCount) += 1;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < *(((int*) maxCount)))
            {
                goto Label_0014;
            }
            return;
        }

        private QuestParam GetQuestParam()
        {
            QuestParam param;
            string str;
            QuestInstanceTypes types;
            param = null;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_0020;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_0063;
            }
            goto Label_0078;
        Label_0020:
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            goto Label_0078;
        Label_0032:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0078;
            }
            str = SceneBattle.Instance.Battle.QuestID;
            param = MonoSingleton<GameManager>.Instance.FindQuest(str);
            goto Label_0078;
        Label_0063:
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        Label_0078:
            return param;
        }

        private QuestParam GetQuestParamAuto()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_002A;
            }
            return MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
        Label_002A:
            return DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
        }

        private MultiPlayAPIRoom GetRoom()
        {
            return DataSource.FindDataOfClass<MultiPlayAPIRoom>(base.get_gameObject(), null);
        }

        private JSON_MyPhotonRoomParam GetRoomParam()
        {
            JSON_MyPhotonRoomParam param;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            param = DataSource.FindDataOfClass<JSON_MyPhotonRoomParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0015;
            }
            return param;
        Label_0015:
            room = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            return ((room != null) ? JSON_MyPhotonRoomParam.Parse(room.json) : null);
        }

        private JSON_MyPhotonPlayerParam GetRoomPlayerParam()
        {
            JSON_MyPhotonPlayerParam param;
            param = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0015;
            }
            return null;
        Label_0015:
            if (param.playerIndex > 0)
            {
                goto Label_0023;
            }
            return null;
        Label_0023:
            return param;
        }

        private SellItem GetSellItem()
        {
            return DataSource.FindDataOfClass<SellItem>(base.get_gameObject(), null);
        }

        private List<SellItem> GetSellItemList()
        {
            return DataSource.FindDataOfClass<List<SellItem>>(base.get_gameObject(), null);
        }

        private ShopItem GetShopItem()
        {
            ShopItem item;
            item = DataSource.FindDataOfClass<ShopItem>(base.get_gameObject(), null);
            if (item != null)
            {
                goto Label_0027;
            }
            item = this.GetLimitedShopItem();
            if (item != null)
            {
                goto Label_0027;
            }
            item = this.GetEventShopItem();
        Label_0027:
            return item;
        }

        private SkillData GetSkillData()
        {
            SkillData data;
            return DataSource.FindDataOfClass<SkillData>(base.get_gameObject(), null);
        }

        private SkillParam GetSkillParam()
        {
            return DataSource.FindDataOfClass<SkillParam>(base.get_gameObject(), null);
        }

        private SupportData GetSupportData()
        {
            SupportData data;
            data = DataSource.FindDataOfClass<SupportData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_001E;
            }
            data = GlobalVars.SelectedSupport;
        Label_001E:
            return data;
        }

        private TowerRewardItem GetTowerRewardItem()
        {
            QuestParam param;
            TowerFloorParam param2;
            TowerRewardParam param3;
            List<TowerRewardItem> list;
            param = this.GetQuestParamAuto();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            param2 = MonoSingleton<GameManager>.Instance.FindTowerFloor(param.iname);
            param3 = MonoSingleton<GameManager>.Instance.FindTowerReward(param2.reward_id);
            if (param3 != null)
            {
                goto Label_0039;
            }
            return null;
        Label_0039:
            list = param3.GetTowerRewardItem();
            if (list == null)
            {
                goto Label_0057;
            }
            if (list.Count >= this.Index)
            {
                goto Label_0059;
            }
        Label_0057:
            return null;
        Label_0059:
            return list[this.Index];
        }

        private TrickParam GetTrickParam()
        {
            return DataSource.FindDataOfClass<TrickParam>(base.get_gameObject(), null);
        }

        private TrophyObjective GetTrophyObjective()
        {
            return DataSource.FindDataOfClass<TrophyObjective>(base.get_gameObject(), null);
        }

        private TrophyParam GetTrophyParam()
        {
            return DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
        }

        private Unit GetUnit()
        {
            UnitInstanceTypes types;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_0022;

                case 1:
                    goto Label_0022;

                case 2:
                    goto Label_0022;

                case 3:
                    goto Label_002F;
            }
        Label_0022:
            return DataSource.FindDataOfClass<Unit>(base.get_gameObject(), null);
        Label_002F:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0068;
            }
            if (SceneBattle.Instance.Battle.CurrentUnit == null)
            {
                goto Label_0068;
            }
            return SceneBattle.Instance.Battle.CurrentUnit;
        Label_0068:
            return null;
        }

        private UnitData GetUnitData()
        {
            UnitData data;
            Unit unit;
            ArenaPlayer player;
            int num;
            ArenaPlayer player2;
            int num2;
            PlayerData data2;
            int num3;
            PartyData data3;
            long num4;
            PlayerData data4;
            PartyData data5;
            long num5;
            PlayerData data6;
            PartyData data7;
            long num6;
            VersusCpuData data8;
            int num7;
            List<UnitData> list;
            int num8;
            UnitInstanceTypes types;
            data = null;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_009A;

                case 1:
                    goto Label_02D0;

                case 2:
                    goto Label_02D0;

                case 3:
                    goto Label_00C6;

                case 4:
                    goto Label_0104;

                case 5:
                    goto Label_0104;

                case 6:
                    goto Label_0104;

                case 7:
                    goto Label_012E;

                case 8:
                    goto Label_012E;

                case 9:
                    goto Label_012E;

                case 10:
                    goto Label_015B;

                case 11:
                    goto Label_015B;

                case 12:
                    goto Label_015B;

                case 13:
                    goto Label_01A4;

                case 14:
                    goto Label_02D0;

                case 15:
                    goto Label_02D0;

                case 0x10:
                    goto Label_020D;

                case 0x11:
                    goto Label_020D;

                case 0x12:
                    goto Label_020D;

                case 0x13:
                    goto Label_01D8;

                case 20:
                    goto Label_024D;

                case 0x15:
                    goto Label_026C;

                case 0x16:
                    goto Label_026C;

                case 0x17:
                    goto Label_026C;

                case 0x18:
                    goto Label_026C;

                case 0x19:
                    goto Label_026C;

                case 0x1a:
                    goto Label_026C;

                case 0x1b:
                    goto Label_026C;

                case 0x1c:
                    goto Label_026C;

                case 0x1d:
                    goto Label_026C;

                case 30:
                    goto Label_026C;

                case 0x1f:
                    goto Label_026C;

                case 0x20:
                    goto Label_026C;
            }
            goto Label_02D0;
        Label_009A:
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_02D0;
            }
            unit = this.GetUnit();
            if (unit == null)
            {
                goto Label_02D0;
            }
            data = unit.UnitData;
            goto Label_02D0;
        Label_00C6:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_02D0;
            }
            if (SceneBattle.Instance.Battle.CurrentUnit == null)
            {
                goto Label_02D0;
            }
            return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
            goto Label_02D0;
        Label_0104:
            player = DataSource.FindDataOfClass<ArenaPlayer>(base.get_gameObject(), null);
            if (player == null)
            {
                goto Label_02D0;
            }
            num = this.InstanceType - 4;
            data = player.Unit[num];
            goto Label_02D0;
        Label_012E:
            player2 = GlobalVars.SelectedArenaPlayer;
            if (player2 == null)
            {
                goto Label_02D0;
            }
            num2 = this.InstanceType - 7;
            data = player2.Unit[num2];
            goto Label_02D0;
        Label_015B:
            data2 = MonoSingleton<GameManager>.Instance.Player;
            num3 = this.InstanceType - 10;
            data3 = data2.Partys[GlobalVars.SelectedPartyIndex];
            num4 = data3.GetUnitUniqueID(num3);
            data = data2.FindUnitDataByUniqueID(num4);
            goto Label_02D0;
        Label_01A4:
            data4 = MonoSingleton<GameManager>.Instance.Player;
            data5 = data4.Partys[7];
            num5 = data5.GetUnitUniqueID(0);
            data = data4.FindUnitDataByUniqueID(num5);
            goto Label_02D0;
        Label_01D8:
            data6 = MonoSingleton<GameManager>.Instance.Player;
            data7 = data6.Partys[10];
            num6 = data7.GetUnitUniqueID(0);
            data = data6.FindUnitDataByUniqueID(num6);
            goto Label_02D0;
        Label_020D:
            data8 = DataSource.FindDataOfClass<VersusCpuData>(base.get_gameObject(), null);
            if (data8 == null)
            {
                goto Label_02D0;
            }
            num7 = this.InstanceType - 0x10;
            if (((int) data8.Units.Length) <= num7)
            {
                goto Label_02D0;
            }
            data = data8.Units[num7];
            goto Label_02D0;
        Label_024D:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            goto Label_02D0;
        Label_026C:
            if (this.InstanceType >= 0x1b)
            {
                goto Label_0290;
            }
            list = VersusDraftList.VersusDraftUnitDataListPlayer;
            num8 = this.InstanceType - 0x15;
            goto Label_02A2;
        Label_0290:
            list = VersusDraftList.VersusDraftUnitDataListEnemy;
            num8 = this.InstanceType - 0x1b;
        Label_02A2:
            if (list != null)
            {
                goto Label_02AE;
            }
            goto Label_02D0;
        Label_02AE:
            if (list.Count > num8)
            {
                goto Label_02C1;
            }
            goto Label_02D0;
        Label_02C1:
            data = list[num8];
        Label_02D0:
            return data;
        }

        private EquipData GetUnitEquipData()
        {
            UnitData data;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0040;
            }
            if (0 > this.Index)
            {
                goto Label_0040;
            }
            if (this.Index >= ((int) data.CurrentEquips.Length))
            {
                goto Label_0040;
            }
            return data.CurrentEquips[this.Index];
        Label_0040:
            return null;
        }

        private UnitParam GetUnitParam()
        {
            UnitParam param;
            UnitData data;
            param = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0015;
            }
            return param;
        Label_0015:
            data = this.GetUnitData();
            return ((data == null) ? null : data.UnitParam);
        }

        private JSON_MyPhotonPlayerParam GetVersusPlayerParam()
        {
            MyPhoton photon;
            JSON_MyPhotonPlayerParam param;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param2;
            JSON_MyPhotonPlayerParam[] paramArray;
            string str;
            FlowNode_StartMultiPlay.PlayerList list;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            param = null;
            if ((photon != null) == null)
            {
                goto Label_00A7;
            }
            if (this.InstanceType != null)
            {
                goto Label_002C;
            }
            param = JSON_MyPhotonPlayerParam.Create(0, 0);
            goto Label_00A7;
        Label_002C:
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_00A7;
            }
            param2 = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param2 == null)
            {
                goto Label_006E;
            }
            paramArray = param2.players;
            if (paramArray == null)
            {
                goto Label_006E;
            }
            if (((int) paramArray.Length) <= 1)
            {
                goto Label_006E;
            }
            param = this.GetVersusPlayerParam(paramArray, 1);
        Label_006E:
            if (param != null)
            {
                goto Label_00A7;
            }
            str = photon.GetRoomParam("started");
            if (str == null)
            {
                goto Label_00A7;
            }
            list = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(str);
            if (list == null)
            {
                goto Label_00A7;
            }
            param = this.GetVersusPlayerParam(list.players, 1);
        Label_00A7:
            return param;
        }

        private JSON_MyPhotonPlayerParam GetVersusPlayerParam(JSON_MyPhotonPlayerParam[] players, int cnt)
        {
            MyPhoton photon;
            JSON_MyPhotonPlayerParam param;
            int num;
            JSON_MyPhotonPlayerParam param2;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            param = null;
            if (((int) players.Length) <= cnt)
            {
                goto Label_0051;
            }
            num = 0;
            goto Label_0048;
        Label_0018:
            param2 = players[num];
            if (param2 != null)
            {
                goto Label_0027;
            }
            goto Label_0044;
        Label_0027:
            if (param2.playerID == photon.GetMyPlayer().playerID)
            {
                goto Label_0044;
            }
            param = param2;
            goto Label_0051;
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) players.Length))
            {
                goto Label_0018;
            }
        Label_0051:
            return param;
        }

        private WeatherParam GetWeatherParam()
        {
            return DataSource.FindDataOfClass<WeatherParam>(base.get_gameObject(), null);
        }

        private unsafe void InternalUpdateValue()
        {
            object[] objArray8;
            object[] objArray7;
            object[] objArray6;
            object[] objArray5;
            string[] textArray2;
            string[] textArray1;
            object[] objArray4;
            char[] chArray1;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            GameManager manager;
            QuestParam param;
            FriendData data;
            UnitData data2;
            Unit unit;
            PartyData data3;
            ItemData data4;
            ItemParam param2;
            AbilityData data5;
            AbilityParam param3;
            SkillData data6;
            EquipData data7;
            JobEvolutionRecipe recipe;
            RecipeItemParameter parameter;
            PlayerLevelUpInfo info;
            MailData data8;
            JSON_MyPhotonPlayerParam param4;
            MultiPlayAPIRoom room;
            SellItem item;
            List<SellItem> list;
            ShopItem item2;
            EnhanceEquipData data9;
            EnhanceMaterial material;
            EquipItemParameter parameter2;
            UnitParam param5;
            GachaParam param6;
            JobParam param7;
            long num;
            FixParam param8;
            string str;
            IconLoader loader;
            string str2;
            IconLoader loader2;
            StarGauge gauge;
            OInt num2;
            int num3;
            JobData data10;
            int num4;
            int num5;
            int num6;
            int num7;
            long num8;
            JobData data11;
            int num9;
            string str3;
            QuestParam param9;
            int num10;
            UnitData data12;
            JobData data13;
            int num11;
            SupportData data14;
            UnitParam param10;
            TowerRewardItem item3;
            PlayerData data15;
            VersusTowerParam param11;
            string str4;
            EnhanceMaterial material2;
            EnhanceEquipData data16;
            SellItem item4;
            ConsumeItemData data17;
            IconLoader loader3;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            int num24;
            int num25;
            string str5;
            IconLoader loader4;
            string str6;
            IconLoader loader5;
            JobData data18;
            int num26;
            int num27;
            JobData data19;
            int num28;
            int num29;
            JobData data20;
            int num30;
            int num31;
            JobData data21;
            JobData data22;
            int num32;
            JobParam param12;
            int num33;
            int num34;
            int num35;
            int num36;
            int num37;
            int num38;
            int num39;
            int num40;
            int num41;
            int num42;
            int num43;
            int num44;
            int num45;
            int num46;
            int num47;
            int num48;
            int num49;
            int num50;
            OInt num51;
            RecipeParam param13;
            int num52;
            int num53;
            JobData data23;
            AbilityUnlockInfo info2;
            RecipeParam param14;
            IconLoader loader6;
            BattleCore.Record record;
            BattleCore.Record record2;
            BattleCore.Record record3;
            BattleCore.Record record4;
            int num54;
            int num55;
            long num56;
            int num57;
            int num58;
            ChapterParam param15;
            ChapterParam param16;
            ChapterParam param17;
            MyPhoton photon;
            bool flag;
            List<MyPhoton.MyPlayer> list2;
            MyPhoton.MyPlayer player;
            JSON_MyPhotonPlayerParam.UnitDataElem elem;
            string str7;
            IconLoader loader7;
            JSON_MyPhotonPlayerParam.UnitDataElem elem2;
            JSON_MyPhotonPlayerParam.UnitDataElem elem3;
            JobData data24;
            int num59;
            int num60;
            JSON_MyPhotonPlayerParam.UnitDataElem elem4;
            JobParam param18;
            string str8;
            IconLoader loader8;
            JSON_MyPhotonPlayerParam.UnitDataElem elem5;
            StarGauge gauge2;
            JSON_MyPhotonPlayerParam.UnitDataElem elem6;
            int num61;
            JSON_MyPhotonPlayerParam.UnitDataElem elem7;
            int num62;
            JSON_MyPhotonRoomParam param19;
            int num63;
            MyPhoton photon2;
            List<MyPhoton.MyPlayer> list3;
            MyPhoton.MyPlayer player2;
            bool flag2;
            bool flag3;
            SRPG_Button button;
            MyPhoton photon3;
            List<MyPhoton.MyPlayer> list4;
            MyPhoton.MyPlayer player3;
            JSON_MyPhotonRoomParam param20;
            JSON_MyPhotonPlayerParam param21;
            int num64;
            int num65;
            int num66;
            string str9;
            GiftData[] dataArray;
            int num67;
            ConceptCardParam param22;
            string str10;
            ArtifactParam param23;
            string str11;
            ItemParam param24;
            string str12;
            UnitParam param25;
            AwardParam param26;
            DateTime time;
            string str13;
            DateTime time2;
            string str14;
            DateTime time3;
            DateTime time4;
            TimeSpan span;
            string str15;
            string str16;
            DateTime time5;
            TimeSpan span2;
            int num68;
            int num69;
            int num70;
            bool flag4;
            int num71;
            int num72;
            int num73;
            LimitedShopItem item5;
            LimitedShopItem item6;
            EventShopItem item7;
            int num74;
            List<UnitData> list5;
            int num75;
            int num76;
            JobData data25;
            int num77;
            FixParam param27;
            DateTime time6;
            int num78;
            int num79;
            string str17;
            PlayerData data26;
            PlayerData data27;
            PlayerData data28;
            List<FriendData> list6;
            string str18;
            IconLoader loader9;
            string str19;
            IconLoader loader10;
            string str20;
            int num80;
            int num81;
            RarityParam param28;
            int num82;
            int num83;
            int num84;
            int num85;
            int num86;
            MyPhoton photon4;
            VersusAudienceManager manager2;
            AudienceStartParam param29;
            JSON_MyPhotonPlayerParam param30;
            JSON_MyPhotonPlayerParam param31;
            JSON_MyPhotonPlayerParam param32;
            IconLoader loader11;
            string str21;
            VersusAudienceManager manager3;
            AudienceStartParam param33;
            int num87;
            int num88;
            JSON_MyPhotonPlayerParam param34;
            MyPhoton photon5;
            JSON_MyPhotonPlayerParam.UnitDataElem[] elemArray;
            int num89;
            JSON_MyPhotonPlayerParam param35;
            JSON_MyPhotonPlayerParam param36;
            UnitIcon icon;
            IconLoader loader12;
            string str22;
            int num90;
            VersusAudienceManager manager4;
            AudienceStartParam param37;
            int num91;
            JSON_MyPhotonPlayerParam param38;
            int num92;
            MyPhoton photon6;
            JSON_MyPhotonPlayerParam.UnitDataElem[] elemArray2;
            JSON_MyPhotonPlayerParam param39;
            JSON_MyPhotonPlayerParam param40;
            int num93;
            TrophyParam param41;
            SceneBattle battle;
            SceneBattle battle2;
            SceneBattle battle3;
            JSON_MyPhotonRoomParam param42;
            bool flag5;
            bool flag6;
            MyPhoton photon7;
            MyPhoton.MyRoom room2;
            JSON_MyPhotonRoomParam param43;
            JSON_MyPhotonRoomParam param44;
            QuestParam param45;
            MyPhoton photon8;
            MyPhoton.MyRoom room3;
            JSON_MyPhotonRoomParam param46;
            string str23;
            MyPhoton photon9;
            List<JSON_MyPhotonPlayerParam> list7;
            JSON_MyPhotonPlayerParam param47;
            bool flag7;
            JSON_MyPhotonRoomParam param48;
            int num94;
            int num95;
            bool flag8;
            JSON_MyPhotonRoomParam param49;
            int num96;
            int num97;
            bool flag9;
            JSON_MyPhotonPlayerParam.UnitDataElem elem8;
            bool flag10;
            JSON_MyPhotonRoomParam param50;
            int num98;
            Button button2;
            GameUtility.EScene scene;
            bool flag11;
            Button button3;
            string str24;
            PlayerData data29;
            int num99;
            GameUtility.EScene scene2;
            bool flag12;
            PlayerData data30;
            int num100;
            SceneBattle battle4;
            int num101;
            TrophyObjectiveData data31;
            TrophyParam param51;
            TrophyObjectiveData data32;
            float num102;
            string str25;
            int num103;
            TrophyParam param52;
            TrophyState state;
            float num104;
            string str26;
            int num105;
            TrophyObjectiveData data33;
            float num106;
            string str27;
            TrophyParam param53;
            float num107;
            string str28;
            int num108;
            BuffEffect effect;
            int num109;
            string str29;
            int num110;
            SkillData data34;
            BuffEffect effect2;
            int num111;
            int num112;
            SkillData data35;
            BuffEffect effect3;
            int num113;
            int num114;
            SkillData data36;
            BuffEffect effect4;
            EquipData data37;
            int num115;
            int num116;
            int num117;
            int num118;
            int num119;
            EquipData data38;
            int num120;
            int num121;
            int num122;
            int num123;
            EquipData data39;
            int num124;
            int num125;
            int num126;
            int num127;
            EquipData data40;
            int num128;
            int num129;
            int num130;
            SceneBattle battle5;
            SceneBattle battle6;
            MyPhoton photon10;
            List<JSON_MyPhotonPlayerParam> list8;
            JSON_MyPhotonPlayerParam param54;
            List<MyPhoton.MyPlayer> list9;
            MyPhoton.MyPlayer player4;
            MyPhoton photon11;
            List<JSON_MyPhotonPlayerParam> list10;
            JSON_MyPhotonPlayerParam param55;
            MyPhoton photon12;
            MyPhoton photon13;
            GachaResultData_old _old;
            PlayerData data41;
            TrophyState[] stateArray;
            bool flag13;
            int num131;
            int num132;
            TrophyParam param56;
            TrophyParam param57;
            TrophyParam param58;
            TrophyParam param59;
            RewardData data42;
            RewardData data43;
            RewardData data44;
            RewardData data45;
            TextAsset asset;
            TextAsset asset2;
            int num133;
            PaymentManager.Product product;
            PaymentManager.Product product2;
            string str30;
            string[] strArray;
            int num134;
            PaymentManager.Product product3;
            PaymentManager.Product product4;
            string str31;
            FixParam param60;
            DateTime time7;
            TimeSpan span3;
            int num135;
            ArenaPlayer player5;
            ArenaPlayer player6;
            ArenaPlayer player7;
            ArenaPlayer player8;
            ArenaPlayer player9;
            ItemData data46;
            ItemData data47;
            long num136;
            long num137;
            long num138;
            string str32;
            FixParam param61;
            DateTime time8;
            int num139;
            ArenaPlayer player10;
            UnitEquipmentSlotEvents events;
            int num140;
            EquipData[] dataArray2;
            ItemParam param62;
            string str33;
            IconLoader loader13;
            string str34;
            IconLoader loader14;
            int num141;
            int num142;
            int num143;
            int num144;
            int num145;
            int num146;
            string str35;
            RewardData data48;
            JobData data49;
            ShopData data50;
            int num147;
            ShopData data51;
            int num148;
            ShopData data52;
            int num149;
            int num150;
            UnitUnlockTimeParam param63;
            string str36;
            int num151;
            int num152;
            int num153;
            List<PartyData> list11;
            int num154;
            JobData data53;
            int num155;
            SkillParam param64;
            LearningSkill skill;
            int num156;
            string str37;
            long num157;
            string str38;
            string str39;
            string str40;
            string str41;
            string str42;
            long num158;
            string str43;
            string str44;
            string str45;
            string str46;
            string str47;
            SpriteSheet sheet;
            Image image;
            JobData data54;
            Sprite sprite;
            JobSetParam param65;
            StringBuilder builder;
            int num159;
            JobParam param66;
            OInt num160;
            ArtifactParam param67;
            ArtifactParam param68;
            ArtifactParam param69;
            ArtifactData data55;
            ArtifactData data56;
            ArtifactParam param70;
            int num161;
            List<ArtifactData> list12;
            int num162;
            ArtifactData data57;
            int num163;
            int num164;
            int num165;
            int num166;
            RarityParam param71;
            int num167;
            RewardData data58;
            RewardData data59;
            List<string> list13;
            string str48;
            int num168;
            PartyCondType type;
            List<string> list14;
            bool flag14;
            string str49;
            bool flag15;
            UnitData.CharacterQuestParam param72;
            UnitData.CharacterQuestParam param73;
            JobData data60;
            SceneBattle battle7;
            string str50;
            int num169;
            int num170;
            int num171;
            int num172;
            int num173;
            int num174;
            int num175;
            int num176;
            int num177;
            bool flag16;
            SceneBattle battle8;
            MyPhoton photon14;
            SceneBattle battle9;
            SceneBattle battle10;
            List<string> list15;
            string str51;
            int num178;
            List<string> list16;
            int num179;
            List<string> list17;
            bool flag17;
            List<string> list18;
            int num180;
            bool flag18;
            EventCoinData data61;
            EventCoinData data62;
            EventCoinData data63;
            EventCoinData data64;
            EventShopItem item8;
            ItemParam param74;
            EventShopItem item9;
            TrophyParam param75;
            DateTime time9;
            DateTime time10;
            DateTime time11;
            TrophyState state2;
            TimeSpan span4;
            string str52;
            VersusAudienceManager manager5;
            AudienceStartParam param76;
            JSON_MyPhotonPlayerParam param77;
            UnitData data65;
            IconLoader loader15;
            JSON_MyPhotonPlayerParam param78;
            UnitData data66;
            IconLoader loader16;
            VersusCpuData data67;
            UnitData data68;
            IconLoader loader17;
            VersusAudienceManager manager6;
            AudienceStartParam param79;
            JSON_MyPhotonPlayerParam param80;
            JSON_MyPhotonPlayerParam param81;
            VersusCpuData data69;
            VersusAudienceManager manager7;
            AudienceStartParam param82;
            JSON_MyPhotonPlayerParam param83;
            JSON_MyPhotonPlayerParam param84;
            VersusCpuData data70;
            VersusAudienceManager manager8;
            AudienceStartParam param85;
            JSON_MyPhotonPlayerParam param86;
            JSON_MyPhotonPlayerParam param87;
            VersusCpuData data71;
            JSON_MyPhotonPlayerParam param88;
            SceneBattle battle11;
            BattleCore core;
            BattleCore.Record record5;
            Image image2;
            QuestParam param89;
            SpriteSheet sheet2;
            Image image3;
            VersusMapParam param90;
            SpriteSheet sheet3;
            VersusMapParam param91;
            VersusMapParam param92;
            SceneBattle battle12;
            PlayerData data72;
            SceneBattle battle13;
            QuestParam param93;
            BattleCore core2;
            BattleCore.Record record6;
            string str53;
            PlayerData data73;
            SceneBattle battle14;
            BattleCore core3;
            BattleCore.Record record7;
            MyPhoton photon15;
            List<JSON_MyPhotonPlayerParam> list19;
            List<MyPhoton.MyPlayer> list20;
            MyPhoton.MyPlayer player11;
            SceneBattle battle15;
            BattleCore.QuestResult result;
            MyPhoton.MyRoom room4;
            string str54;
            int num181;
            JSON_MyPhotonRoomParam param94;
            string str55;
            ImageArray array;
            VersusCpuData data74;
            VersusCpuData data75;
            VersusCpuData data76;
            ArtifactData data77;
            ArtifactData.RarityUpResults results;
            string str56;
            bool flag19;
            ArtifactData data78;
            ArtifactData data79;
            bool flag20;
            bool flag21;
            bool flag22;
            string str57;
            List<string> list21;
            string str58;
            int num182;
            QuestCondParam param95;
            bool flag23;
            QuestCondParam param96;
            PartySlotTypeUnitPair[] pairArray;
            bool flag24;
            QuestCondParam param97;
            Selectable selectable;
            bool flag25;
            bool flag26;
            TrickParam param98;
            TrickParam param99;
            TrickParam param100;
            IconLoader loader18;
            BattleCore.OrderData data80;
            Image image4;
            int num183;
            int num184;
            int num185;
            WeatherData data81;
            WeatherParam param101;
            WeatherParam param102;
            IconLoader loader19;
            ArtifactParam param103;
            ArtifactRewardData data82;
            ArtifactParam param104;
            ArtifactRewardData data83;
            ArtifactParam param105;
            QuestParam param106;
            ArtifactRewardData data84;
            ArtifactParam param107;
            MultiPlayAPIRoom room5;
            PlayerData data85;
            PartyData data86;
            QuestParam param108;
            bool flag27;
            int num186;
            long num187;
            UnitData data87;
            MultiPlayAPIRoom room6;
            VersusAudienceManager manager9;
            AudienceStartParam param109;
            JSON_MyPhotonPlayerParam param110;
            string str59;
            string str60;
            bool flag28;
            VersusCpuData data88;
            MyPhoton photon16;
            List<JSON_MyPhotonPlayerParam> list22;
            JSON_MyPhotonPlayerParam param111;
            List<MyPhoton.MyPlayer> list23;
            MyPhoton.MyPlayer player12;
            string str61;
            JSON_MyPhotonRoomParam param112;
            JSON_MyPhotonPlayerParam param113;
            int num188;
            int num189;
            int num190;
            MyPhoton photon17;
            List<MyPhoton.MyPlayer> list24;
            MyPhoton.MyPlayer player13;
            bool flag29;
            JSON_MyPhotonPlayerParam param114;
            Button button4;
            Text text;
            MyPhoton photon18;
            MyPhoton.MyRoom room7;
            JSON_MyPhotonRoomParam param115;
            QuestParam param116;
            Transform transform;
            Transform transform2;
            bool flag30;
            int num191;
            int num192;
            int num193;
            int num194;
            int num195;
            int num196;
            QuestParam param117;
            QuestParam param118;
            QuestParam param119;
            int num197;
            bool flag31;
            string str62;
            QuestParam param120;
            int num198;
            string str63;
            string str64;
            string str65;
            int num199;
            int num200;
            int num201;
            int num202;
            int num203;
            QuestParam param121;
            int num204;
            bool flag32;
            Color color;
            string str66;
            MapEffectParam param122;
            JobParam param123;
            JobParam param124;
            JobParam param125;
            JobParam param126;
            FixParam param127;
            PlayerData data89;
            Image image5;
            ChallengeCategoryParam param128;
            SpriteSheet sheet4;
            Image image6;
            ChallengeCategoryParam param129;
            SpriteSheet sheet5;
            Image image7;
            TrophyParam param130;
            SpriteSheet sheet6;
            TobiraRecipeParam param131;
            UnlockTobiraRecipe recipe2;
            UnlockTobiraRecipe recipe3;
            TobiraEnhanceRecipe recipe4;
            TobiraEnhanceRecipe recipe5;
            TobiraData data90;
            bool flag33;
            int num205;
            ItemParam param132;
            Image image8;
            GameSettings settings;
            int num206;
            int num207;
            int num208;
            Image image9;
            VersusRankClassParam param133;
            SpriteSheet sheet7;
            Image image10;
            VersusRankClassParam param134;
            SpriteSheet sheet8;
            Image image11;
            VersusRankClassParam param135;
            SpriteSheet sheet9;
            VersusRankClassParam param136;
            Image image12;
            SpriteSheet sheet10;
            Image image13;
            SpriteSheet sheet11;
            int num209;
            Image image14;
            SpriteSheet sheet12;
            int num210;
            PlayerData data91;
            int num211;
            int num212;
            long num213;
            ReqRankMatchRanking.ResponceRanking ranking;
            ReqRankMatchRanking.ResponceRanking ranking2;
            Image image15;
            SpriteSheet sheet13;
            ReqRankMatchRanking.ResponceRanking ranking3;
            Image image16;
            SpriteSheet sheet14;
            ReqRankMatchRanking.ResponceRanking ranking4;
            ReqRankMatchRanking.ResponceRanking ranking5;
            ReqRankMatchRanking.ResponceRanking ranking6;
            VersusRankRankingRewardParam param137;
            int num214;
            VersusRankRankingRewardParam param138;
            VersusRankRankingRewardParam param139;
            Image image17;
            SpriteSheet sheet15;
            ReqRankMatchHistory.ResponceHistoryList list25;
            Transform transform3;
            ReqRankMatchHistory.ResponceHistoryList list26;
            string str67;
            Color color2;
            ReqRankMatchHistory.ResponceHistoryList list27;
            Image image18;
            SpriteSheet sheet16;
            ReqRankMatchHistory.ResponceHistoryList list28;
            ReqRankMatchHistory.ResponceHistoryList list29;
            ReqRankMatchHistory.ResponceHistoryList list30;
            VersusRankMissionParam param140;
            VersusRankMissionParam param141;
            ReqRankMatchMission.MissionProgress progress;
            int num215;
            int num216;
            VersusRankClassParam param142;
            List<VersusRankMissionParam> list31;
            bool flag34;
            List<RankMatchMissionState>.Enumerator enumerator;
            VersusRankMissionParam param143;
            Image image19;
            SpriteSheet sheet17;
            Image image20;
            SpriteSheet sheet18;
            JSON_MyPhotonPlayerParam param144;
            Image image21;
            SpriteSheet sheet19;
            int num217;
            VersusRankParam param145;
            VersusRankParam param146;
            bool flag35;
            AbilityDeriveParam param147;
            Color color3;
            Color color4;
            AbilityDeriveParam param148;
            string str68;
            ImageSpriteSheet sheet20;
            Color color5;
            Color color6;
            SkillDeriveParam param149;
            SkillAbilityDeriveParam param150;
            string str69;
            ArtifactParam param151;
            ConceptCardData data92;
            int num218;
            bool flag36;
            bool flag37;
            <InternalUpdateValue>c__AnonStorey214 storey;
            ParameterTypes types;
            SupportData data93;
            PartyAttackTypes types2;
            ItemInstanceTypes types3;
            SkillParam param152;
            QuestDifficulties difficulties;
            int num219;
            ESaleType type2;
            int num220;
            <InternalUpdateValue>c__AnonStorey215 storey2;
            <InternalUpdateValue>c__AnonStorey216 storey3;
            <InternalUpdateValue>c__AnonStorey217 storey4;
            <InternalUpdateValue>c__AnonStorey218 storey5;
            <InternalUpdateValue>c__AnonStorey219 storey6;
            <InternalUpdateValue>c__AnonStorey21A storeya;
            TrophyBadgeInstanceTypes types4;
            long num221;
            long num222;
            long num223;
            int num224;
            long num225;
            long num226;
            long num227;
            int num228;
            float num229;
            float num230;
            int num231;
            <InternalUpdateValue>c__AnonStorey21B storeyb;
            int num232;
            ArtifactInstanceTypes types5;
            <InternalUpdateValue>c__AnonStorey21C storeyc;
            int num233;
            <InternalUpdateValue>c__AnonStorey21D storeyd;
            DateTime time12;
            DateTime time13;
            DateTime time14;
            DateTime time15;
            DateTime time16;
            DateTime time17;
            DateTime time18;
            DateTime time19;
            storey = new <InternalUpdateValue>c__AnonStorey214();
            types = this.ParameterType;
            switch (types)
            {
                case 0:
                    goto Label_0AF5;

                case 1:
                    goto Label_0BF7;

                case 2:
                    goto Label_0B95;

                case 3:
                    goto Label_0BDF;

                case 4:
                    goto Label_0B3D;

                case 5:
                    goto Label_0B7D;

                case 6:
                    goto Label_0B0D;

                case 7:
                    goto Label_0B25;

                case 8:
                    goto Label_0C22;

                case 9:
                    goto Label_0C4E;

                case 10:
                    goto Label_0C73;

                case 11:
                    goto Label_0CA8;

                case 12:
                    goto Label_0D71;

                case 13:
                    goto Label_0D96;

                case 14:
                    goto Label_1937;

                case 15:
                    goto Label_0DED;

                case 0x10:
                    goto Label_0EA8;

                case 0x11:
                    goto Label_0F73;

                case 0x12:
                    goto Label_0F36;

                case 0x13:
                    goto Label_10EA;

                case 20:
                    goto Label_0FB0;

                case 0x15:
                    goto Label_0EE5;

                case 0x16:
                    goto Label_1001;

                case 0x17:
                    goto Label_1094;

                case 0x18:
                    goto Label_1052;

                case 0x19:
                    goto Label_0E12;

                case 0x1a:
                    goto Label_114C;

                case 0x1b:
                    goto Label_122B;

                case 0x1c:
                    goto Label_1472;

                case 0x1d:
                    goto Label_133B;

                case 30:
                    goto Label_13A3;

                case 0x1f:
                    goto Label_140A;

                case 0x20:
                    goto Label_143E;

                case 0x21:
                    goto Label_1250;

                case 0x22:
                    goto Label_1314;

                case 0x23:
                    goto Label_12A6;

                case 0x24:
                    goto Label_14A5;

                case 0x25:
                    goto Label_14EE;

                case 0x26:
                    goto Label_1537;

                case 0x27:
                    goto Label_156B;

                case 40:
                    goto Label_15D3;

                case 0x29:
                    goto Label_159F;

                case 0x2a:
                    goto Label_1607;

                case 0x2b:
                    goto Label_25E4;

                case 0x2c:
                    goto Label_163C;

                case 0x2d:
                    goto Label_16A3;

                case 0x2e:
                    goto Label_1937;

                case 0x2f:
                    goto Label_1973;

                case 0x30:
                    goto Label_199A;

                case 0x31:
                    goto Label_1A00;

                case 50:
                    goto Label_1A27;

                case 0x33:
                    goto Label_1A4E;

                case 0x34:
                    goto Label_1A75;

                case 0x35:
                    goto Label_1EB1;

                case 0x36:
                    goto Label_1EE4;

                case 0x37:
                    goto Label_1F18;

                case 0x38:
                    goto Label_0770;

                case 0x39:
                    goto Label_1F84;

                case 0x3a:
                    goto Label_1FDE;

                case 0x3b:
                    goto Label_1FE3;

                case 60:
                    goto Label_203D;

                case 0x3d:
                    goto Label_3678;

                case 0x3e:
                    goto Label_3699;

                case 0x3f:
                    goto Label_2D60;

                case 0x40:
                    goto Label_2DA9;

                case 0x41:
                    goto Label_0770;

                case 0x42:
                    goto Label_0770;

                case 0x43:
                    goto Label_0770;

                case 0x44:
                    goto Label_1F30;

                case 0x45:
                    goto Label_1F61;

                case 70:
                    goto Label_20F4;

                case 0x47:
                    goto Label_214A;

                case 0x48:
                    goto Label_21A8;

                case 0x49:
                    goto Label_21F4;

                case 0x4a:
                    goto Label_2219;

                case 0x4b:
                    goto Label_2274;

                case 0x4c:
                    goto Label_229D;

                case 0x4d:
                    goto Label_22E7;

                case 0x4e:
                    goto Label_231E;

                case 0x4f:
                    goto Label_2370;

                case 80:
                    goto Label_239B;

                case 0x51:
                    goto Label_239C;

                case 0x52:
                    goto Label_23C3;

                case 0x53:
                    goto Label_24B9;

                case 0x54:
                    goto Label_2664;

                case 0x55:
                    goto Label_26BC;

                case 0x56:
                    goto Label_2721;

                case 0x57:
                    goto Label_27B0;

                case 0x58:
                    goto Label_2802;

                case 0x59:
                    goto Label_2854;

                case 90:
                    goto Label_28A6;

                case 0x5b:
                    goto Label_28F8;

                case 0x5c:
                    goto Label_294A;

                case 0x5d:
                    goto Label_299C;

                case 0x5e:
                    goto Label_29EE;

                case 0x5f:
                    goto Label_2A41;

                case 0x60:
                    goto Label_2A94;

                case 0x61:
                    goto Label_2AE7;

                case 0x62:
                    goto Label_2B3A;

                case 0x63:
                    goto Label_2B8D;

                case 100:
                    goto Label_2BE0;

                case 0x65:
                    goto Label_2C33;

                case 0x66:
                    goto Label_2C86;

                case 0x67:
                    goto Label_2CD9;

                case 0x68:
                    goto Label_2D11;

                case 0x69:
                    goto Label_2E16;

                case 0x6a:
                    goto Label_2E17;

                case 0x6b:
                    goto Label_2E18;

                case 0x6c:
                    goto Label_2E19;

                case 0x6d:
                    goto Label_2E1A;

                case 110:
                    goto Label_2E5D;

                case 0x6f:
                    goto Label_2EA1;

                case 0x70:
                    goto Label_2EE2;

                case 0x71:
                    goto Label_2F0F;

                case 0x72:
                    goto Label_2F42;

                case 0x73:
                    goto Label_2F74;

                case 0x74:
                    goto Label_2FBA;

                case 0x75:
                    goto Label_2FEA;

                case 0x76:
                    goto Label_301A;

                case 0x77:
                    goto Label_304B;

                case 120:
                    goto Label_3081;

                case 0x79:
                    goto Label_30A4;

                case 0x7a:
                    goto Label_3106;

                case 0x7b:
                    goto Label_2624;

                case 0x7c:
                    goto Label_315B;

                case 0x7d:
                    goto Label_31A0;

                case 0x7e:
                    goto Label_31C3;

                case 0x7f:
                    goto Label_31D0;

                case 0x80:
                    goto Label_31DD;

                case 0x81:
                    goto Label_322C;

                case 130:
                    goto Label_3259;

                case 0x83:
                    goto Label_328C;

                case 0x84:
                    goto Label_32BE;

                case 0x85:
                    goto Label_3306;

                case 0x86:
                    goto Label_3334;

                case 0x87:
                    goto Label_3357;

                case 0x88:
                    goto Label_33C7;

                case 0x89:
                    goto Label_3398;

                case 0x8a:
                    goto Label_33F6;

                case 0x8b:
                    goto Label_3454;

                case 140:
                    goto Label_345B;

                case 0x8d:
                    goto Label_347E;

                case 0x8e:
                    goto Label_34A1;

                case 0x8f:
                    goto Label_34C4;

                case 0x90:
                    goto Label_34E7;

                case 0x91:
                    goto Label_350A;

                case 0x92:
                    goto Label_352D;

                case 0x93:
                    goto Label_3550;

                case 0x94:
                    goto Label_359A;

                case 0x95:
                    goto Label_0770;

                case 150:
                    goto Label_0770;

                case 0x97:
                    goto Label_360E;

                case 0x98:
                    goto Label_24B9;

                case 0x99:
                    goto Label_24B9;

                case 0x9a:
                    goto Label_24B9;

                case 0x9b:
                    goto Label_3643;

                case 0x9c:
                    goto Label_36BA;

                case 0x9d:
                    goto Label_3704;

                case 0x9e:
                    goto Label_3727;

                case 0x9f:
                    goto Label_3753;

                case 160:
                    goto Label_377A;

                case 0xa1:
                    goto Label_3792;

                case 0xa2:
                    goto Label_37EE;

                case 0xa3:
                    goto Label_3850;

                case 0xa4:
                    goto Label_3883;

                case 0xa5:
                    goto Label_38AD;

                case 0xa6:
                    goto Label_391C;

                case 0xa7:
                    goto Label_3943;

                case 0xa8:
                    goto Label_396F;

                case 0xa9:
                    goto Label_399C;

                case 170:
                    goto Label_39C4;

                case 0xab:
                    goto Label_3AA0;

                case 0xac:
                    goto Label_41BB;

                case 0xad:
                    goto Label_4633;

                case 0xae:
                    goto Label_467F;

                case 0xaf:
                    goto Label_478E;

                case 0xb0:
                    goto Label_47D6;

                case 0xb1:
                    goto Label_4834;

                case 0xb2:
                    goto Label_4872;

                case 0xb3:
                    goto Label_49A0;

                case 180:
                    goto Label_48FD;

                case 0xb5:
                    goto Label_4A02;

                case 0xb6:
                    goto Label_4A31;

                case 0xb7:
                    goto Label_4A52;

                case 0xb8:
                    goto Label_4A73;

                case 0xb9:
                    goto Label_4A99;

                case 0xba:
                    goto Label_4B8F;

                case 0xbb:
                    goto Label_4BA7;

                case 0xbc:
                    goto Label_4BDC;

                case 0xbd:
                    goto Label_4C00;

                case 190:
                    goto Label_4C26;

                case 0xbf:
                    goto Label_4C4A;

                case 0xc0:
                    goto Label_4CAF;

                case 0xc1:
                    goto Label_4CF7;

                case 0xc2:
                    goto Label_4D39;

                case 0xc3:
                    goto Label_4EA2;

                case 0xc4:
                    goto Label_4EC4;

                case 0xc5:
                    goto Label_4EE8;

                case 0xc6:
                    goto Label_4F17;

                case 0xc7:
                    goto Label_4F3C;

                case 200:
                    goto Label_500D;

                case 0xc9:
                    goto Label_50D6;

                case 0xca:
                    goto Label_24B9;

                case 0xcb:
                    goto Label_2768;

                case 0xcc:
                    goto Label_24B9;

                case 0xcd:
                    goto Label_50F0;

                case 0xce:
                    goto Label_511F;

                case 0xcf:
                    goto Label_5146;

                case 0xd0:
                    goto Label_5176;

                case 0xd1:
                    goto Label_51B4;

                case 210:
                    goto Label_492A;

                case 0xd3:
                    goto Label_3D77;

                case 0xd4:
                    goto Label_3DBA;

                case 0xd5:
                    goto Label_3E4D;

                case 0xd6:
                    goto Label_3DE2;

                case 0xd7:
                    goto Label_3E25;

                case 0xd8:
                    goto Label_59C4;

                case 0xd9:
                    goto Label_4094;

                case 0xda:
                    goto Label_40A8;

                case 0xdb:
                    goto Label_40D2;

                case 220:
                    goto Label_59F4;

                case 0xdd:
                    goto Label_5A4E;

                case 0xde:
                    goto Label_5A7D;

                case 0xdf:
                    goto Label_5CCD;

                case 0xe0:
                    goto Label_40BC;

                case 0xe1:
                    goto Label_5B79;

                case 0xe2:
                    goto Label_5C11;

                case 0xe3:
                    goto Label_5D8F;

                case 0xe4:
                    goto Label_5BD0;

                case 0xe5:
                    goto Label_5F80;

                case 230:
                    goto Label_6243;

                case 0xe7:
                    goto Label_62DE;

                case 0xe8:
                    goto Label_6561;

                case 0xe9:
                    goto Label_66DE;

                case 0xea:
                    goto Label_6702;

                case 0xeb:
                    goto Label_6726;

                case 0xec:
                    goto Label_6786;

                case 0xed:
                    goto Label_67E9;

                case 0xee:
                    goto Label_687E;

                case 0xef:
                    goto Label_696A;

                case 240:
                    goto Label_6A50;

                case 0xf1:
                    goto Label_6A82;

                case 0xf2:
                    goto Label_6AB1;

                case 0xf3:
                    goto Label_6B82;

                case 0xf4:
                    goto Label_6C10;

                case 0xf5:
                    goto Label_6C97;

                case 0xf6:
                    goto Label_6CBF;

                case 0xf7:
                    goto Label_6D08;

                case 0xf8:
                    goto Label_6D71;

                case 0xf9:
                    goto Label_6D83;

                case 250:
                    goto Label_6DC5;

                case 0xfb:
                    goto Label_6E0F;

                case 0xfc:
                    goto Label_6F68;

                case 0xfd:
                    goto Label_7030;

                case 0xfe:
                    goto Label_7075;

                case 0xff:
                    goto Label_708E;

                case 0x100:
                    goto Label_70B7;

                case 0x101:
                    goto Label_722E;

                case 0x102:
                    goto Label_71F4;

                case 0x103:
                    goto Label_7268;

                case 260:
                    goto Label_733C;

                case 0x105:
                    goto Label_72DC;

                case 0x106:
                    goto Label_730C;

                case 0x107:
                    goto Label_739C;

                case 0x108:
                    goto Label_73C2;

                case 0x109:
                    goto Label_73F3;

                case 0x10a:
                    goto Label_30C6;

                case 0x10b:
                    goto Label_7419;

                case 0x10c:
                    goto Label_744D;

                case 0x10d:
                    goto Label_7481;

                case 270:
                    goto Label_74AB;

                case 0x10f:
                    goto Label_75F5;

                case 0x110:
                    goto Label_77AC;

                case 0x111:
                    goto Label_77E5;

                case 0x112:
                    goto Label_780F;

                case 0x113:
                    goto Label_786A;

                case 0x114:
                    goto Label_78CA;

                case 0x115:
                    goto Label_78F4;

                case 0x116:
                    goto Label_7928;

                case 0x117:
                    goto Label_797E;

                case 280:
                    goto Label_79EE;

                case 0x119:
                    goto Label_7A22;

                case 0x11a:
                    goto Label_604B;

                case 0x11b:
                    goto Label_5EFB;

                case 0x11c:
                    goto Label_7F7B;

                case 0x11d:
                    goto Label_0770;

                case 0x11e:
                    goto Label_0770;

                case 0x11f:
                    goto Label_7FE0;

                case 0x120:
                    goto Label_7A95;

                case 0x121:
                    goto Label_7B3A;

                case 290:
                    goto Label_7B64;

                case 0x123:
                    goto Label_7B7C;

                case 0x124:
                    goto Label_7D3C;

                case 0x125:
                    goto Label_7D8F;

                case 0x126:
                    goto Label_7DC4;

                case 0x127:
                    goto Label_7E0F;

                case 0x128:
                    goto Label_7E4B;

                case 0x129:
                    goto Label_7E76;

                case 0x12a:
                    goto Label_7ED4;

                case 0x12b:
                    goto Label_7F2F;

                case 300:
                    goto Label_7FBB;

                case 0x12d:
                    goto Label_8010;

                case 0x12e:
                    goto Label_8055;

                case 0x12f:
                    goto Label_80F9;

                case 0x130:
                    goto Label_81A8;

                case 0x131:
                    goto Label_826E;

                case 0x132:
                    goto Label_82A2;

                case 0x133:
                    goto Label_8329;

                case 0x134:
                    goto Label_8367;

                case 0x135:
                    goto Label_83A3;

                case 310:
                    goto Label_83C1;

                case 0x137:
                    goto Label_83DF;

                case 0x138:
                    goto Label_841B;

                case 0x139:
                    goto Label_8439;

                case 0x13a:
                    goto Label_8457;

                case 0x13b:
                    goto Label_8476;

                case 0x13c:
                    goto Label_8495;

                case 0x13d:
                    goto Label_84B4;

                case 0x13e:
                    goto Label_84D6;

                case 0x13f:
                    goto Label_84F8;

                case 320:
                    goto Label_855B;

                case 0x141:
                    goto Label_85D3;

                case 0x142:
                    goto Label_85FE;

                case 0x143:
                    goto Label_8616;

                case 0x144:
                    goto Label_866A;

                case 0x145:
                    goto Label_86E9;

                case 0x146:
                    goto Label_0AEE;

                case 0x147:
                    goto Label_8723;

                case 0x148:
                    goto Label_8750;

                case 0x149:
                    goto Label_878F;

                case 330:
                    goto Label_8A32;

                case 0x14b:
                    goto Label_8A99;

                case 0x14c:
                    goto Label_8BA1;

                case 0x14d:
                    goto Label_8C34;

                case 0x14e:
                    goto Label_8C7D;

                case 0x14f:
                    goto Label_8CA0;

                case 0x150:
                    goto Label_8CA1;

                case 0x151:
                    goto Label_8CA2;

                case 0x152:
                    goto Label_8D6E;

                case 0x153:
                    goto Label_8DB6;

                case 340:
                    goto Label_8DB7;

                case 0x155:
                    goto Label_8E83;

                case 0x156:
                    goto Label_8EB7;

                case 0x157:
                    goto Label_521F;

                case 0x158:
                    goto Label_8EB8;

                case 0x159:
                    goto Label_528A;

                case 0x15a:
                    goto Label_11AE;

                case 0x15b:
                    goto Label_11F2;

                case 0x15c:
                    goto Label_8EDF;

                case 0x15d:
                    goto Label_3B21;

                case 350:
                    goto Label_3B67;

                case 0x15f:
                    goto Label_3BC7;

                case 0x160:
                    goto Label_3C4C;

                case 0x161:
                    goto Label_3CD6;

                case 0x162:
                    goto Label_3D28;

                case 0x163:
                    goto Label_72A2;

                case 0x164:
                    goto Label_245A;

                case 0x165:
                    goto Label_26FE;

                case 0x166:
                    goto Label_3425;

                case 0x167:
                    goto Label_60C6;

                case 360:
                    goto Label_5E2D;

                case 0x169:
                    goto Label_5EA7;

                case 0x16a:
                    goto Label_736C;

                case 0x16b:
                    goto Label_0CDE;

                case 0x16c:
                    goto Label_0D03;

                case 0x16d:
                    goto Label_0D28;

                case 0x16e:
                    goto Label_0D4D;

                case 0x16f:
                    goto Label_245A;

                case 0x170:
                    goto Label_0770;

                case 0x171:
                    goto Label_0770;

                case 370:
                    goto Label_0770;

                case 0x173:
                    goto Label_0770;

                case 0x174:
                    goto Label_0770;

                case 0x175:
                    goto Label_0770;

                case 0x176:
                    goto Label_0770;

                case 0x177:
                    goto Label_0770;

                case 0x178:
                    goto Label_8F64;

                case 0x179:
                    goto Label_8F87;

                case 0x17a:
                    goto Label_8FAA;

                case 0x17b:
                    goto Label_8FCD;

                case 380:
                    goto Label_8FF0;

                case 0x17d:
                    goto Label_9014;

                case 0x17e:
                    goto Label_9038;

                case 0x17f:
                    goto Label_905C;

                case 0x180:
                    goto Label_9083;

                case 0x181:
                    goto Label_90AA;

                case 0x182:
                    goto Label_90D1;

                case 0x183:
                    goto Label_90F8;

                case 0x184:
                    goto Label_911F;

                case 0x185:
                    goto Label_9146;

                case 390:
                    goto Label_916D;

                case 0x187:
                    goto Label_9194;

                case 0x188:
                    goto Label_91BB;

                case 0x189:
                    goto Label_91F4;

                case 0x18a:
                    goto Label_61DF;

                case 0x18b:
                    goto Label_3EAF;

                case 0x18c:
                    goto Label_38F2;

                case 0x18d:
                    goto Label_5AAC;

                case 0x18e:
                    goto Label_46B9;

                case 0x18f:
                    goto Label_93C4;

                case 400:
                    goto Label_93D2;

                case 0x191:
                    goto Label_9451;

                case 0x192:
                    goto Label_9492;

                case 0x193:
                    goto Label_74EE;

                case 0x194:
                    goto Label_7638;

                case 0x195:
                    goto Label_94C9;

                case 0x196:
                    goto Label_9501;

                case 0x197:
                    goto Label_9533;

                case 0x198:
                    goto Label_9565;

                case 0x199:
                    goto Label_9597;

                case 410:
                    goto Label_9608;

                case 0x19b:
                    goto Label_9657;

                case 0x19c:
                    goto Label_96E7;

                case 0x19d:
                    goto Label_971F;

                case 0x19e:
                    goto Label_972F;

                case 0x19f:
                    goto Label_9797;

                case 0x1a0:
                    goto Label_97AF;

                case 0x1a1:
                    goto Label_9832;

                case 0x1a2:
                    goto Label_987A;

                case 0x1a3:
                    goto Label_98BD;

                case 420:
                    goto Label_98ED;

                case 0x1a5:
                    goto Label_991D;

                case 0x1a6:
                    goto Label_9AC7;

                case 0x1a7:
                    goto Label_9B3C;

                case 0x1a8:
                    goto Label_9BAD;

                case 0x1a9:
                    goto Label_9C26;

                case 0x1aa:
                    goto Label_9BE0;

                case 0x1ab:
                    goto Label_4D1B;

                case 0x1ac:
                    goto Label_9C84;

                case 0x1ad:
                    goto Label_9D7B;

                case 430:
                    goto Label_9D2C;

                case 0x1af:
                    goto Label_9E6B;

                case 0x1b0:
                    goto Label_9ED7;

                case 0x1b1:
                    goto Label_4B56;

                case 0x1b2:
                    goto Label_9FE7;

                case 0x1b3:
                    goto Label_A172;

                case 0x1b4:
                    goto Label_A26D;

                case 0x1b5:
                    goto Label_9F0C;

                case 0x1b6:
                    goto Label_9F4E;

                case 0x1b7:
                    goto Label_9FAD;

                case 440:
                    goto Label_A2A6;

                case 0x1b9:
                    goto Label_A2DD;

                case 0x1ba:
                    goto Label_A325;

                case 0x1bb:
                    goto Label_A36D;

                case 0x1bc:
                    goto Label_A3B5;

                case 0x1bd:
                    goto Label_A408;

                case 0x1be:
                    goto Label_A437;

                case 0x1bf:
                    goto Label_A60D;

                case 0x1c0:
                    goto Label_A66D;

                case 0x1c1:
                    goto Label_A81E;

                case 450:
                    goto Label_A8DA;

                case 0x1c3:
                    goto Label_A9A0;

                case 0x1c4:
                    goto Label_AA95;

                case 0x1c5:
                    goto Label_AAFD;

                case 0x1c6:
                    goto Label_AAFE;

                case 0x1c7:
                    goto Label_AAFF;

                case 0x1c8:
                    goto Label_AB00;

                case 0x1c9:
                    goto Label_AB01;

                case 0x1ca:
                    goto Label_AB02;

                case 0x1cb:
                    goto Label_AB03;

                case 460:
                    goto Label_AB04;

                case 0x1cd:
                    goto Label_ABAE;

                case 0x1ce:
                    goto Label_AC6B;

                case 0x1cf:
                    goto Label_ACA4;

                case 0x1d0:
                    goto Label_ACE3;

                case 0x1d1:
                    goto Label_ACF9;

                case 0x1d2:
                    goto Label_862E;

                case 0x1d3:
                    goto Label_8646;

                case 0x1d4:
                    goto Label_528A;
            }
        Label_0770:
            switch ((types - 0x960))
            {
                case 0:
                    goto Label_D6F9;

                case 1:
                    goto Label_D72D;

                case 2:
                    goto Label_D745;

                case 3:
                    goto Label_D791;

                case 4:
                    goto Label_D7D1;

                case 5:
                    goto Label_D87D;

                case 6:
                    goto Label_D923;

                case 7:
                    goto Label_D9C9;

                case 8:
                    goto Label_DA0B;

                case 9:
                    goto Label_DA59;

                case 10:
                    goto Label_DC89;

                case 11:
                    goto Label_DD37;

                case 12:
                    goto Label_DDA1;

                case 13:
                    goto Label_DE5F;

                case 14:
                    goto Label_DEF0;

                case 15:
                    goto Label_DF1F;

                case 0x10:
                    goto Label_DF4E;

                case 0x11:
                    goto Label_DAFB;

                case 0x12:
                    goto Label_DF78;

                case 0x13:
                    goto Label_DF78;

                case 20:
                    goto Label_E088;

                case 0x15:
                    goto Label_E146;

                case 0x16:
                    goto Label_E257;

                case 0x17:
                    goto Label_E316;

                case 0x18:
                    goto Label_E3A7;

                case 0x19:
                    goto Label_E3D6;

                case 0x1a:
                    goto Label_E405;

                case 0x1b:
                    goto Label_E42F;

                case 0x1c:
                    goto Label_E459;

                case 0x1d:
                    goto Label_E483;

                case 30:
                    goto Label_DB9E;

                case 0x1f:
                    goto Label_DC41;

                case 0x20:
                    goto Label_DC59;

                case 0x21:
                    goto Label_DC71;

                case 0x22:
                    goto Label_E4BC;

                case 0x23:
                    goto Label_E034;

                case 0x24:
                    goto Label_E54E;

                case 0x25:
                    goto Label_E62D;

                case 0x26:
                    goto Label_E667;

                case 0x27:
                    goto Label_E730;

                case 40:
                    goto Label_E7C5;

                case 0x29:
                    goto Label_E7F3;

                case 0x2a:
                    goto Label_E829;

                case 0x2b:
                    goto Label_E8C7;

                case 0x2c:
                    goto Label_EA19;
            }
            switch ((types - 0x3e8))
            {
                case 0:
                    goto Label_AD0F;

                case 1:
                    goto Label_AD76;

                case 2:
                    goto Label_AD99;

                case 3:
                    goto Label_AE15;

                case 4:
                    goto Label_AE44;

                case 5:
                    goto Label_AE8C;

                case 6:
                    goto Label_AF2B;

                case 7:
                    goto Label_AFB9;

                case 8:
                    goto Label_B111;

                case 9:
                    goto Label_B1AF;

                case 10:
                    goto Label_B1C5;

                case 11:
                    goto Label_B269;

                case 12:
                    goto Label_B2A8;

                case 13:
                    goto Label_B2BC;

                case 14:
                    goto Label_B33A;

                case 15:
                    goto Label_B384;

                case 0x10:
                    goto Label_B3B4;

                case 0x11:
                    goto Label_B3E4;
            }
            switch ((types - 0x6a4))
            {
                case 0:
                    goto Label_CA73;

                case 1:
                    goto Label_CAD7;

                case 2:
                    goto Label_CB3C;

                case 3:
                    goto Label_CB75;

                case 4:
                    goto Label_CBDB;

                case 5:
                    goto Label_CC5D;

                case 6:
                    goto Label_CD70;

                case 7:
                    goto Label_CE80;

                case 8:
                    goto Label_CEE4;

                case 9:
                    goto Label_CF74;

                case 10:
                    goto Label_CFB7;
            }
            switch ((types - 0x640))
            {
                case 0:
                    goto Label_C061;

                case 1:
                    goto Label_C27D;

                case 2:
                    goto Label_C2EF;

                case 3:
                    goto Label_C69E;

                case 4:
                    goto Label_C7C0;

                case 5:
                    goto Label_C8E2;

                case 6:
                    goto Label_C96D;

                case 7:
                    goto Label_C9A8;

                case 8:
                    goto Label_C9EF;
            }
            switch ((types - 0x4b0))
            {
                case 0:
                    goto Label_B5B2;

                case 1:
                    goto Label_B60C;

                case 2:
                    goto Label_B67A;

                case 3:
                    goto Label_B79D;

                case 4:
                    goto Label_B845;

                case 5:
                    goto Label_B8B8;

                case 6:
                    goto Label_B98D;

                case 7:
                    goto Label_B9F3;
            }
            switch ((types - 0x578))
            {
                case 0:
                    goto Label_BAEE;

                case 1:
                    goto Label_BB31;

                case 2:
                    goto Label_BBA0;

                case 3:
                    goto Label_BC38;

                case 4:
                    goto Label_BCD0;

                case 5:
                    goto Label_BD68;

                case 6:
                    goto Label_BD87;

                case 7:
                    goto Label_BDB5;
            }
            switch ((types - 0x708))
            {
                case 0:
                    goto Label_D10D;

                case 1:
                    goto Label_D177;

                case 2:
                    goto Label_D1A5;

                case 3:
                    goto Label_D1D3;

                case 4:
                    goto Label_D210;

                case 5:
                    goto Label_D23E;

                case 6:
                    goto Label_24B9;
            }
            switch ((types - 0x834))
            {
                case 0:
                    goto Label_82D5;

                case 1:
                    goto Label_AA66;

                case 2:
                    goto Label_5383;

                case 3:
                    goto Label_552E;

                case 4:
                    goto Label_598C;

                case 5:
                    goto Label_552E;

                case 6:
                    goto Label_5778;
            }
            switch ((types - 0x898))
            {
                case 0:
                    goto Label_D4B3;

                case 1:
                    goto Label_D4DC;

                case 2:
                    goto Label_D505;

                case 3:
                    goto Label_D52E;

                case 4:
                    goto Label_D557;

                case 5:
                    goto Label_D580;

                case 6:
                    goto Label_D580;
            }
            switch ((types - 0x44c))
            {
                case 0:
                    goto Label_B414;

                case 1:
                    goto Label_B4CF;

                case 2:
                    goto Label_B553;

                case 3:
                    goto Label_BE75;

                case 4:
                    goto Label_BF13;

                case 5:
                    goto Label_BF88;
            }
            switch ((types - 0x514))
            {
                case 0:
                    goto Label_BA4D;

                case 1:
                    goto Label_BA7B;

                case 2:
                    goto Label_BAA9;
            }
            switch ((types - 0x7d0))
            {
                case 0:
                    goto Label_D2D3;

                case 1:
                    goto Label_D373;

                case 2:
                    goto Label_D413;
            }
            switch ((types - 0x9c4))
            {
                case 0:
                    goto Label_EA49;

                case 1:
                    goto Label_EAB8;

                case 2:
                    goto Label_EB59;
            }
            if (types == 0x5dc)
            {
                goto Label_BE0D;
            }
            if (types == 0x5dd)
            {
                goto Label_BE41;
            }
            if (types == 0x76c)
            {
                goto Label_D27B;
            }
            if (types == 0x76d)
            {
                goto Label_D2AC;
            }
            if (types == 0x8fc)
            {
                goto Label_D621;
            }
            if (types == 0x8fd)
            {
                goto Label_D6AF;
            }
            if (types == 0xb54)
            {
                goto Label_ED96;
            }
            if (types == 0xb55)
            {
                goto Label_EDB6;
            }
            if (types == 0xa28)
            {
                goto Label_EBD9;
            }
            if (types == 0xa8c)
            {
                goto Label_EC90;
            }
            if (types == 0xaf0)
            {
                goto Label_ECED;
            }
            goto Label_EDD9;
        Label_0AEE:
            this.BatchUpdate();
            return;
        Label_0AF5:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.Name);
            return;
        Label_0B0D:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.Gold);
            return;
        Label_0B25:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.Coin);
            return;
        Label_0B3D:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.Exp);
            this.SetSliderValue(manager.Player.GetExp(), manager.Player.GetExp() + manager.Player.GetNextExp());
            return;
        Label_0B7D:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.GetNextExp());
            return;
        Label_0B95:
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.UpdateStamina();
            this.SetTextValue(manager.Player.Stamina);
            this.SetSliderValue(manager.Player.Stamina, manager.Player.StaminaMax);
            this.SetUpdateInterval(1f);
            return;
        Label_0BDF:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.StaminaMax);
            return;
        Label_0BF7:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.CalcLevel());
            this.SetSliderValue(manager.Player.CalcLevel(), 0x63);
            return;
        Label_0C22:
            num = MonoSingleton<GameManager>.Instance.Player.GetNextStaminaRecoverySec();
            this.SetTextValue(TimeManager.ToMinSecString(num));
            this.SetUpdateInterval(1f);
            return;
        Label_0C4E:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0C6C;
            }
            this.SetTextValue(param.name);
            goto Label_0C72;
        Label_0C6C:
            this.ResetToDefault();
        Label_0C72:
            return;
        Label_0C73:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0CA1;
            }
            this.SetTextValue(param.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv, 1));
            goto Label_0CA7;
        Label_0CA1:
            this.ResetToDefault();
        Label_0CA7:
            return;
        Label_0CA8:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0CD7;
            }
            this.SetAnimatorInt("state", param.state);
            this.SetImageIndex(param.state);
            goto Label_0CDD;
        Label_0CD7:
            this.ResetToDefault();
        Label_0CDD:
            return;
        Label_0CDE:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0CFC;
            }
            this.SetTextValue(param.GetChallangeCount());
            goto Label_0D02;
        Label_0CFC:
            this.ResetToDefault();
        Label_0D02:
            return;
        Label_0D03:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0D21;
            }
            this.SetTextValue(param.GetChallangeLimit());
            goto Label_0D27;
        Label_0D21:
            this.ResetToDefault();
        Label_0D27:
            return;
        Label_0D28:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0D46;
            }
            this.SetTextValue(param.dailyReset);
            goto Label_0D4C;
        Label_0D46:
            this.ResetToDefault();
        Label_0D4C:
            return;
        Label_0D4D:
            param8 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            this.SetTextValue(param8.EliteResetMax);
            return;
        Label_0D71:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0D8F;
            }
            this.SetTextValue(param.cond);
            goto Label_0D95;
        Label_0D8F:
            this.ResetToDefault();
        Label_0D95:
            return;
        Label_0D96:
            if ((((param = this.GetQuestParam()) == null) || (param.bonusObjective == null)) || ((0 > this.Index) || (this.Index >= ((int) param.bonusObjective.Length))))
            {
                goto Label_0DE6;
            }
            this.SetTextValue(GameUtility.ComposeQuestBonusObjectiveText(param.bonusObjective[this.Index]));
            return;
        Label_0DE6:
            this.ResetToDefault();
            return;
        Label_0DED:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_0E0B;
            }
            this.SetTextValue(param.expr);
            goto Label_0E11;
        Label_0E0B:
            this.ResetToDefault();
        Label_0E11:
            return;
        Label_0E12:
            if (((storey.supportData = this.GetSupportData()) == null) || (storey.supportData.Unit == null))
            {
                goto Label_0EA1;
            }
            str = AssetPath.UnitSkinIconSmall(storey.supportData.Unit.UnitParam, storey.supportData.Unit.GetSelectedSkin(-1), storey.supportData.JobID);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0EA1;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str;
            return;
        Label_0EA1:
            this.ResetToDefault();
            return;
        Label_0EA8:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_0EDE;
            }
            this.SetTextValue(storey.supportData.PlayerName);
            goto Label_0EE4;
        Label_0EDE:
            this.ResetToDefault();
        Label_0EE4:
            return;
        Label_0EE5:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_0F2F;
            }
            this.SetTextValue(storey.supportData.UnitParam.ini_status.param.hp);
            goto Label_0F35;
        Label_0F2F:
            this.ResetToDefault();
        Label_0F35:
            return;
        Label_0F36:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_0F6C;
            }
            this.SetTextValue(storey.supportData.UnitLevel);
            goto Label_0F72;
        Label_0F6C:
            this.ResetToDefault();
        Label_0F72:
            return;
        Label_0F73:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_0FA9;
            }
            this.SetTextValue(storey.supportData.PlayerLevel);
            goto Label_0FAF;
        Label_0FA9:
            this.ResetToDefault();
        Label_0FAF:
            return;
        Label_0FB0:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_0FFA;
            }
            this.SetTextValue(storey.supportData.UnitParam.ini_status.param.atk);
            goto Label_1000;
        Label_0FFA:
            this.ResetToDefault();
        Label_1000:
            return;
        Label_1001:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_104B;
            }
            this.SetTextValue(storey.supportData.UnitParam.ini_status.param.mag);
            goto Label_1051;
        Label_104B:
            this.ResetToDefault();
        Label_1051:
            return;
        Label_1052:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_108D;
            }
            this.SetAnimatorInt("element", storey.supportData.UnitElement);
            goto Label_1093;
        Label_108D:
            this.ResetToDefault();
        Label_1093:
            return;
        Label_1094:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_10E3;
            }
            this.SetAnimatorInt("rare", storey.supportData.UnitRarity);
            this.SetImageIndex(storey.supportData.UnitRarity);
            goto Label_10E9;
        Label_10E3:
            this.ResetToDefault();
        Label_10E9:
            return;
        Label_10EA:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_1145;
            }
            storey.skillParam = storey.supportData.LeaderSkill;
            if (storey.skillParam == null)
            {
                goto Label_114B;
            }
            this.SetTextValue(storey.skillParam.name);
            goto Label_114B;
        Label_1145:
            this.ResetToDefault();
        Label_114B:
            return;
        Label_114C:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_11A7;
            }
            storey.skillParam = storey.supportData.LeaderSkill;
            if (storey.skillParam == null)
            {
                goto Label_11AD;
            }
            this.SetTextValue(storey.skillParam.expr);
            goto Label_11AD;
        Label_11A7:
            this.ResetToDefault();
        Label_11AD:
            return;
        Label_11AE:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_11E5;
            }
            base.get_gameObject().SetActive(storey.supportData.IsFriend());
            return;
        Label_11E5:
            base.get_gameObject().SetActive(0);
            return;
        Label_11F2:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_1224;
            }
            this.SetTextValue(storey.supportData.GetCost());
            return;
        Label_1224:
            this.ResetToDefault();
            return;
        Label_122B:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_1249;
            }
            this.SetTextValue(param.title);
            goto Label_124F;
        Label_1249:
            this.ResetToDefault();
        Label_124F:
            return;
        Label_1250:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_129F;
            }
            str2 = AssetPath.UnitSkinIconSmall(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_129F;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str2;
            return;
        Label_129F:
            this.ResetToDefault();
            return;
        Label_12A6:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_130D;
            }
            gauge = base.GetComponent<StarGauge>();
            if ((gauge != null) == null)
            {
                goto Label_12EB;
            }
            gauge.Max = data2.GetRarityCap() + 1;
            gauge.Value = data2.Rarity + 1;
            goto Label_1308;
        Label_12EB:
            this.SetAnimatorInt("rare", data2.Rarity);
            this.SetImageIndex(data2.Rarity);
        Label_1308:
            goto Label_1313;
        Label_130D:
            this.ResetToDefault();
        Label_1313:
            return;
        Label_1314:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_1334;
            }
            this.SetTextValue(param5.name);
            goto Label_133A;
        Label_1334:
            this.ResetToDefault();
        Label_133A:
            return;
        Label_133B:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_139C;
            }
            this.SetTextValue(unit.CurrentStatus.param.hp);
            this.SetSliderValue(unit.CurrentStatus.param.hp, unit.MaximumStatus.param.hp);
            goto Label_13A2;
        Label_139C:
            this.ResetToDefault();
        Label_13A2:
            return;
        Label_13A3:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_1403;
            }
            num2 = 0;
            unit = this.GetUnit();
            if (unit == null)
            {
                goto Label_13DF;
            }
            num2 = unit.MaximumStatus.param.hp;
            goto Label_13F1;
        Label_13DF:
            num2 = data2.Status.param.hp;
        Label_13F1:
            this.SetTextValue(num2);
            goto Label_1409;
        Label_1403:
            this.ResetToDefault();
        Label_1409:
            return;
        Label_140A:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_1437;
            }
            this.SetTextValue(data2.Status.param.atk);
            goto Label_143D;
        Label_1437:
            this.ResetToDefault();
        Label_143D:
            return;
        Label_143E:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_146B;
            }
            this.SetTextValue(data2.Status.param.mag);
            goto Label_1471;
        Label_146B:
            this.ResetToDefault();
        Label_1471:
            return;
        Label_1472:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_149E;
            }
            num3 = data2.CalcLevel();
            this.SetTextValue(num3);
            this.SetSliderValue(num3, 0x63);
            goto Label_14A4;
        Label_149E:
            this.ResetToDefault();
        Label_14A4:
            return;
        Label_14A5:
            if ((data3 = this.GetPartyData()) == null)
            {
                goto Label_14E7;
            }
            storey.skillParam = this.GetLeaderSkill(data3);
            if (storey.skillParam == null)
            {
                goto Label_14E7;
            }
            this.SetTextValue(storey.skillParam.name);
            return;
        Label_14E7:
            this.ResetToDefault();
            return;
        Label_14EE:
            if ((data3 = this.GetPartyData()) == null)
            {
                goto Label_1530;
            }
            storey.skillParam = this.GetLeaderSkill(data3);
            if (storey.skillParam == null)
            {
                goto Label_1530;
            }
            this.SetTextValue(storey.skillParam.expr);
            return;
        Label_1530:
            this.ResetToDefault();
            return;
        Label_1537:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_1564;
            }
            this.SetTextValue(data2.Status.param.def);
            goto Label_156A;
        Label_1564:
            this.ResetToDefault();
        Label_156A:
            return;
        Label_156B:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_1598;
            }
            this.SetTextValue(data2.Status.param.mnd);
            goto Label_159E;
        Label_1598:
            this.ResetToDefault();
        Label_159E:
            return;
        Label_159F:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_15CC;
            }
            this.SetTextValue(data2.Status.param.luk);
            goto Label_15D2;
        Label_15CC:
            this.ResetToDefault();
        Label_15D2:
            return;
        Label_15D3:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_1600;
            }
            this.SetTextValue(data2.Status.param.spd);
            goto Label_1606;
        Label_1600:
            this.ResetToDefault();
        Label_1606:
            return;
        Label_1607:
            if (((data2 = this.GetUnitData()) == null) || (data2.CurrentJob == null))
            {
                goto Label_1635;
            }
            this.SetTextValue(data2.CurrentJob.Name);
            return;
        Label_1635:
            this.ResetToDefault();
            return;
        Label_163C:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_166B;
            }
            num4 = data2.Element;
            this.SetAnimatorInt("element", num4);
            this.SetImageIndex(num4);
            goto Label_16A2;
        Label_166B:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_169C;
            }
            num5 = param5.element;
            this.SetAnimatorInt("element", num5);
            this.SetImageIndex(num5);
            goto Label_16A2;
        Label_169C:
            this.ResetToDefault();
        Label_16A2:
            return;
        Label_16A3:
            if ((data3 = this.GetPartyData()) == null)
            {
                goto Label_1930;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num6 = 0;
            num7 = 0;
            goto Label_175C;
        Label_16C2:
            num8 = data3.GetUnitUniqueID(num7);
            data2 = manager.Player.FindUnitDataByUniqueID(num8);
            if (data2 == null)
            {
                goto Label_1756;
            }
            data11 = data2.GetJobFor(data3.PartyType);
            num9 = data2.JobIndex;
            if (data11 == data2.CurrentJob)
            {
                goto Label_170D;
            }
            data2.SetJob(data11);
        Label_170D:
            num6 += data2.Status.param.atk;
            num6 += data2.Status.param.mag;
            if (data2.JobIndex == num9)
            {
                goto Label_1756;
            }
            data2.SetJobIndex(num9);
        Label_1756:
            num7 += 1;
        Label_175C:
            if (num7 < data3.MAX_UNIT)
            {
                goto Label_16C2;
            }
            if (this.InstanceType == 1)
            {
                goto Label_1783;
            }
            goto Label_191E;
        Label_1783:
            str3 = GlobalVars.SelectedQuestID;
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_1880;
            }
            param9 = MonoSingleton<GameManager>.Instance.FindQuest(str3);
            if ((param9 == null) || (param9.units.IsNotNull() == null))
            {
                goto Label_1880;
            }
            num10 = 0;
            goto Label_186D;
        Label_17C4:
            data12 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(param9.units.Get(num10));
            if (data12 == null)
            {
                goto Label_1867;
            }
            data13 = data12.GetJobFor(data3.PartyType);
            num11 = data12.JobIndex;
            if (data13 == data12.CurrentJob)
            {
                goto Label_181A;
            }
            data12.SetJob(data13);
        Label_181A:
            num6 += data12.Status.param.atk;
            num6 += data12.Status.param.mag;
            if (num11 == data12.JobIndex)
            {
                goto Label_1867;
            }
            data12.SetJobIndex(num11);
        Label_1867:
            num10 += 1;
        Label_186D:
            if (num10 < param9.units.Length)
            {
                goto Label_17C4;
            }
        Label_1880:
            storey.supportData = GlobalVars.SelectedSupport;
            if (storey.supportData == null)
            {
                goto Label_1923;
            }
            data14 = MonoSingleton<GameManager>.Instance.Player.Supports.Find(new Predicate<SupportData>(storey.<>m__104));
            if ((data14 == null) || (data14.Unit == null))
            {
                goto Label_1923;
            }
            num6 += data14.Unit.Status.param.atk;
            num6 += data14.Unit.Status.param.mag;
            goto Label_1923;
        Label_191E:;
        Label_1923:
            this.SetTextValue(num6);
            goto Label_1936;
        Label_1930:
            this.ResetToDefault();
        Label_1936:
            return;
        Label_1937:
            param2 = (this.ParameterType != 14) ? this.GetInventoryItemParam() : this.GetItemParam();
            if ((param2 == null) || (this.LoadItemIcon(param2) == null))
            {
                goto Label_196C;
            }
            return;
        Label_196C:
            this.ResetToDefault();
            return;
        Label_1973:
            if ((param2 = this.GetInventoryItemParam()) == null)
            {
                goto Label_1993;
            }
            this.SetTextValue(param2.name);
            goto Label_1999;
        Label_1993:
            this.ResetToDefault();
        Label_1999:
            return;
        Label_199A:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_19F9;
            }
            if (param2.type != 0x10)
            {
                goto Label_19E7;
            }
            param10 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param2.iname);
            if (param10 == null)
            {
                goto Label_19FF;
            }
            this.SetTextValue(param10.name);
            goto Label_19F4;
        Label_19E7:
            this.SetTextValue(param2.name);
        Label_19F4:
            goto Label_19FF;
        Label_19F9:
            this.ResetToDefault();
        Label_19FF:
            return;
        Label_1A00:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_1A20;
            }
            this.SetTextValue(param2.Expr);
            goto Label_1A26;
        Label_1A20:
            this.ResetToDefault();
        Label_1A26:
            return;
        Label_1A27:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_1A47;
            }
            this.SetTextValue(param2.sell);
            goto Label_1A4D;
        Label_1A47:
            this.ResetToDefault();
        Label_1A4D:
            return;
        Label_1A4E:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_1A6E;
            }
            this.SetTextValue(param2.buy);
            goto Label_1A74;
        Label_1A6E:
            this.ResetToDefault();
        Label_1A74:
            return;
        Label_1A75:
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_1AAD;

                case 1:
                    goto Label_1AE8;

                case 2:
                    goto Label_1B4D;

                case 3:
                    goto Label_1D13;

                case 4:
                    goto Label_1D68;

                case 5:
                    goto Label_1DBF;

                case 6:
                    goto Label_1E2B;

                case 7:
                    goto Label_1E82;
            }
            goto Label_1EAA;
        Label_1AAD:
            if ((data4 = DataSource.FindDataOfClass<ItemData>(base.get_gameObject(), null)) == null)
            {
                goto Label_1EAA;
            }
            this.SetTextValue(data4.Num);
            this.SetSliderValue(data4.Num, data4.HaveCap);
            return;
            goto Label_1EAA;
        Label_1AE8:
            manager = MonoSingleton<GameManager>.Instance;
            if ((0 > this.Index) || (this.Index >= ((int) manager.Player.Inventory.Length)))
            {
                goto Label_1EAA;
            }
            data4 = manager.Player.Inventory[this.Index];
            this.SetTextValue(data4.Num);
            this.SetSliderValue(data4.Num, data4.HaveCap);
            return;
            goto Label_1EAA;
        Label_1B4D:
            if (((param = this.GetQuestParamAuto()) == null) || (param.type != 7))
            {
                goto Label_1BCF;
            }
            item3 = this.GetTowerRewardItem();
            if (item3 != null)
            {
                goto Label_1B76;
            }
            return;
        Label_1B76:
            this.SetTextValue(item3.num);
            if ((string.IsNullOrEmpty(item3.iname) != null) || (item3.type != null))
            {
                goto Label_1BCE;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(item3.iname);
            if (param2 == null)
            {
                goto Label_1BCE;
            }
            this.SetSliderValue(item3.num, param2.cap);
        Label_1BCE:
            return;
        Label_1BCF:
            if (((param = this.GetQuestParamAuto()) == null) || (param.IsVersus == null))
            {
                goto Label_1C7B;
            }
            manager = MonoSingleton<GameManager>.Instance;
            data15 = manager.Player;
            param11 = manager.GetCurrentVersusTowerParam(data15.VersusTowerFloor + 1);
            if (param11 == null)
            {
                goto Label_1C74;
            }
            this.SetTextValue(param11.ArrivalItemNum);
            str4 = param11.ArrivalIteminame;
            if ((string.IsNullOrEmpty(str4) != null) || (param11.ArrivalItemType != null))
            {
                goto Label_1C73;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(str4);
            if (param2 == null)
            {
                goto Label_1C73;
            }
            this.SetSliderValue(param11.ArrivalItemNum, param2.cap);
        Label_1C73:
            return;
        Label_1C74:
            this.ResetToDefault();
            return;
        Label_1C7B:
            if (((((param = this.GetQuestParamAuto()) == null) || (param.bonusObjective == null)) || ((0 > this.Index) || (this.Index >= ((int) param.bonusObjective.Length)))) || ((param2 = MonoSingleton<GameManager>.Instance.GetItemParam(param.bonusObjective[this.Index].item)) == null))
            {
                goto Label_1EAA;
            }
            this.SetTextValue(param.bonusObjective[this.Index].itemNum);
            this.SetSliderValue(param.bonusObjective[this.Index].itemNum, param2.cap);
            return;
            goto Label_1EAA;
        Label_1D13:
            param2 = this.GetItemParam();
            if (param2 == null)
            {
                goto Label_1EAA;
            }
            data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param2.iname);
            if (data4 == null)
            {
                goto Label_1EAA;
            }
            this.SetTextValue(data4.Num);
            this.SetSliderValue(data4.Num, data4.HaveCap);
            return;
            goto Label_1EAA;
        Label_1D68:
            material2 = DataSource.FindDataOfClass<EnhanceMaterial>(base.get_gameObject(), null);
            if ((material2 == null) || (material2.item == null))
            {
                goto Label_1EAA;
            }
            this.SetTextValue(material2.item.Num);
            this.SetSliderValue(material2.item.Num, material2.item.HaveCap);
            return;
            goto Label_1EAA;
        Label_1DBF:
            data16 = DataSource.FindDataOfClass<EnhanceEquipData>(base.get_gameObject(), null);
            if ((data16 == null) || (data16.equip == null))
            {
                goto Label_1EAA;
            }
            data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data16.equip.ItemID);
            if (data4 == null)
            {
                goto Label_1EAA;
            }
            this.SetTextValue(data4.Num);
            this.SetSliderValue(data4.Num, data4.HaveCap);
            return;
            goto Label_1EAA;
        Label_1E2B:
            item4 = DataSource.FindDataOfClass<SellItem>(base.get_gameObject(), null);
            if ((item4 == null) || (item4.item == null))
            {
                goto Label_1EAA;
            }
            this.SetTextValue(item4.item.Num);
            this.SetSliderValue(item4.item.Num, item4.item.HaveCap);
            return;
            goto Label_1EAA;
        Label_1E82:
            data17 = DataSource.FindDataOfClass<ConsumeItemData>(base.get_gameObject(), null);
            if (data17 == null)
            {
                goto Label_1EAA;
            }
            this.SetTextValue(data17.num);
            return;
        Label_1EAA:
            this.ResetToDefault();
            return;
        Label_1EB1:
            if (((data4 = this.GetInventoryItemData()) == null) || (data4.Param == null))
            {
                goto Label_1EDD;
            }
            this.SetTextValue(data4.Num);
            goto Label_1EE3;
        Label_1EDD:
            this.ResetToDefault();
        Label_1EE3:
            return;
        Label_1EE4:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.UnitNum);
            this.SetSliderValue(manager.Player.UnitNum, manager.Player.UnitCap);
            return;
        Label_1F18:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.UnitCap);
            return;
        Label_1F30:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_1F5A;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.AbilityIcon(param3);
            return;
        Label_1F5A:
            this.ResetToDefault();
            return;
        Label_1F61:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_1F7D;
            }
            this.SetTextValue(param3.name);
            return;
        Label_1F7D:
            this.ResetToDefault();
            return;
        Label_1F84:
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_1FA5;
            }
            this.SetTextValue(data6.SkillParam.name);
            return;
        Label_1FA5:
            if ((storey.skillParam = this.GetSkillParam()) == null)
            {
                goto Label_1FD7;
            }
            this.SetTextValue(storey.skillParam.name);
            return;
        Label_1FD7:
            this.ResetToDefault();
            return;
        Label_1FDE:
            goto Label_EDD9;
        Label_1FE3:
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_2004;
            }
            this.SetTextValue(data6.SkillParam.expr);
            return;
        Label_2004:
            if ((storey.skillParam = this.GetSkillParam()) == null)
            {
                goto Label_2036;
            }
            this.SetTextValue(storey.skillParam.expr);
            return;
        Label_2036:
            this.ResetToDefault();
            return;
        Label_203D:
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_2093;
            }
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_2069;
            }
            this.SetTextValue(unit.GetSkillUsedCost(data6));
            return;
        Label_2069:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_2085;
            }
            this.SetTextValue(data2.GetSkillUsedCost(data6));
            return;
        Label_2085:
            this.SetTextValue(data6.Cost);
            return;
        Label_2093:
            if ((storey.skillParam = this.GetSkillParam()) == null)
            {
                goto Label_20ED;
            }
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_20D3;
            }
            this.SetTextValue(data2.GetSkillUsedCost(storey.skillParam));
            return;
        Label_20D3:
            this.SetTextValue(storey.skillParam.cost);
            return;
        Label_20ED:
            this.ResetToDefault();
            return;
        Label_20F4:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_2143;
            }
            if ((QuestDropParam.Instance == null) == null)
            {
                goto Label_2112;
            }
            return;
        Label_2112:
            param2 = QuestDropParam.Instance.GetHardDropPiece(param.iname, GlobalVars.GetDropTableGeneratedDateTime());
            if ((param2 == null) || (this.LoadItemIcon(param2.icon) == null))
            {
                goto Label_2143;
            }
            return;
        Label_2143:
            this.ResetToDefault();
            return;
        Label_214A:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_21A1;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num12 = data2.GetExp();
            num13 = manager.MasterParam.GetUnitLevelExp(data2.GetNextLevel()) - manager.MasterParam.GetUnitLevelExp(data2.Lv);
            this.SetTextValue(num12);
            this.SetSliderValue(num12, num13);
            goto Label_21A7;
        Label_21A1:
            this.ResetToDefault();
        Label_21A7:
            return;
        Label_21A8:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_21ED;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num14 = manager.MasterParam.GetUnitLevelExp(data2.GetNextLevel()) - manager.MasterParam.GetUnitLevelExp(data2.Lv);
            this.SetTextValue(num14);
            goto Label_21F3;
        Label_21ED:
            this.ResetToDefault();
        Label_21F3:
            return;
        Label_21F4:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_2212;
            }
            num15 = data2.GetNextExp();
            this.SetTextValue(num15);
            return;
        Label_2212:
            this.ResetToDefault();
            return;
        Label_2219:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_226D;
            }
            num16 = data2.GetPieces();
            num17 = data2.AwakeLv;
            num18 = data2.GetAwakeLevelCap();
            num19 = (num17 >= num18) ? num16 : data2.GetAwakeNeedPieces();
            this.SetTextValue(num16);
            this.SetSliderValue(num16, num19);
            goto Label_2273;
        Label_226D:
            this.ResetToDefault();
        Label_2273:
            return;
        Label_2274:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_2296;
            }
            num20 = data2.GetAwakeNeedPieces();
            this.SetTextValue(num20);
            goto Label_229C;
        Label_2296:
            this.ResetToDefault();
        Label_229C:
            return;
        Label_229D:
            if (((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null))
            {
                goto Label_22E0;
            }
            num21 = data7.GetExp();
            num22 = data7.GetNextExp();
            this.SetTextValue(num21);
            this.SetSliderValue(num21, num22);
            goto Label_22E6;
        Label_22E0:
            this.ResetToDefault();
        Label_22E6:
            return;
        Label_22E7:
            if (((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null))
            {
                goto Label_2317;
            }
            num23 = data7.GetNextExp();
            this.SetTextValue(num23);
            goto Label_231D;
        Label_2317:
            this.ResetToDefault();
        Label_231D:
            return;
        Label_231E:
            if (((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null))
            {
                goto Label_2369;
            }
            this.SetTextValue(data7.Rank);
            this.SetAnimatorInt("rank", data7.Rank);
            this.SetImageIndex(data7.Rank);
            goto Label_236F;
        Label_2369:
            this.ResetToDefault();
        Label_236F:
            return;
        Label_2370:
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_2394;
            }
            num24 = data5.Rank;
            this.SetTextValue(num24);
            goto Label_239A;
        Label_2394:
            this.ResetToDefault();
        Label_239A:
            return;
        Label_239B:
            return;
        Label_239C:
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_23BC;
            }
            this.SetTextValue(data5.GetNextGold());
            goto Label_23C2;
        Label_23BC:
            this.ResetToDefault();
        Label_23C2:
            return;
        Label_23C3:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_2453;
            }
            if (param3.type_detail == null)
            {
                goto Label_242F;
            }
            this.SetAnimatorInt("type", param3.slot);
            if (this.GetImageLength() > 3)
            {
                goto Label_240D;
            }
            this.SetImageIndex(param3.slot);
            goto Label_242A;
        Label_240D:
            num25 = AbilityTypeDetailToImageIndex(param3.slot, param3.type_detail);
            this.SetImageIndex(num25);
        Label_242A:
            goto Label_244E;
        Label_242F:
            this.SetAnimatorInt("type", param3.slot);
            this.SetImageIndex(param3.slot);
        Label_244E:
            goto Label_2459;
        Label_2453:
            this.ResetToDefault();
        Label_2459:
            return;
        Label_245A:
            if ((param7 = this.GetJobParam()) == null)
            {
                goto Label_24B2;
            }
            if (this.ParameterType != 0x16f)
            {
                goto Label_2486;
            }
            str5 = AssetPath.JobIconMedium(param7);
            goto Label_248F;
        Label_2486:
            str5 = AssetPath.JobIconSmall(param7);
        Label_248F:
            if (string.IsNullOrEmpty(str5) != null)
            {
                goto Label_24B2;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str5;
            return;
        Label_24B2:
            this.ResetToDefault();
            return;
        Label_24B9:
            if (((data2 = this.GetUnitData()) == null) || (data2.IsJobAvailable(this.Index) == null))
            {
                goto Label_2563;
            }
            if ((this.ParameterType != 0xca) && (this.ParameterType != 0xcc))
            {
                goto Label_250A;
            }
            param7 = data2.GetClassChangeJobParam(this.Index);
            goto Label_255E;
        Label_250A:;
            param7 = (((this.ParameterType != 0x98) && (this.ParameterType != 0x99)) && (this.ParameterType != 0x70e)) ? data2.Jobs[this.Index].Param : data2.CurrentJob.Param;
        Label_255E:
            goto Label_256B;
        Label_2563:
            param7 = this.GetJobParam();
        Label_256B:
            if (param7 == null)
            {
                goto Label_25DD;
            }
            if (((this.ParameterType != 0x9a) && (this.ParameterType != 0xcc)) && (this.ParameterType != 0x70e))
            {
                goto Label_25B0;
            }
            str6 = AssetPath.JobIconMedium(param7);
            goto Label_25B9;
        Label_25B0:
            str6 = AssetPath.JobIconSmall(param7);
        Label_25B9:
            if (string.IsNullOrEmpty(str6) == null)
            {
                goto Label_25C6;
            }
            return;
        Label_25C6:
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str6;
            return;
        Label_25DD:
            this.ResetToDefault();
            return;
        Label_25E4:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_261D;
            }
            num26 = data2.CurrentJob.Rank;
            num27 = data2.GetJobRankCap();
            this.SetTextValue(num26);
            this.SetSliderValue(num26, num27);
            return;
        Label_261D:
            this.ResetToDefault();
            return;
        Label_2624:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_265D;
            }
            num28 = data2.CurrentJob.Rank;
            num29 = data2.GetJobRankCap();
            this.SetTextValue(num29);
            this.SetSliderValue(num28, num29);
            return;
        Label_265D:
            this.ResetToDefault();
            return;
        Label_2664:
            if (((data2 = this.GetUnitData()) == null) || (data2.IsJobAvailable(this.Index) == null))
            {
                goto Label_26B5;
            }
            data20 = data2.Jobs[this.Index];
            num30 = data20.Rank;
            num31 = data2.GetJobRankCap();
            this.SetTextValue(num30);
            this.SetSliderValue(num30, num31);
            return;
        Label_26B5:
            this.ResetToDefault();
            return;
        Label_26BC:
            if (((data2 = this.GetUnitData()) == null) || (data2.IsJobAvailable(this.Index) == null))
            {
                goto Label_26F7;
            }
            data21 = data2.Jobs[this.Index];
            this.SetTextValue(data21.Name);
            return;
        Label_26F7:
            this.ResetToDefault();
            return;
        Label_26FE:
            if ((param7 = this.GetJobParam()) == null)
            {
                goto Label_271A;
            }
            this.SetTextValue(param7.name);
            return;
        Label_271A:
            this.ResetToDefault();
            return;
        Label_2721:
            if (((data2 = this.GetUnitData()) == null) || (data2.IsJobAvailable(this.Index) == null))
            {
                goto Label_2761;
            }
            data22 = data2.Jobs[this.Index];
            num32 = data22.GetJobRankCap(data2);
            this.SetTextValue(num32);
            return;
        Label_2761:
            this.ResetToDefault();
            return;
        Label_2768:
            if (((data2 = this.GetUnitData()) == null) || (data2.IsJobAvailable(this.Index) == null))
            {
                goto Label_27A9;
            }
            param12 = data2.GetClassChangeJobParam(this.Index);
            if (param12 == null)
            {
                goto Label_27A9;
            }
            this.SetTextValue(param12.name);
            return;
        Label_27A9:
            this.ResetToDefault();
            return;
        Label_27B0:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_27FB;
            }
            num33 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(1, 0);
            this.SetTextValue(num33);
            this.ToggleEmpty((num33 == 0) == 0);
            goto Label_2801;
        Label_27FB:
            this.ResetToDefault();
        Label_2801:
            return;
        Label_2802:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_284D;
            }
            num34 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(3, 0);
            this.SetTextValue(num34);
            this.ToggleEmpty((num34 == 0) == 0);
            goto Label_2853;
        Label_284D:
            this.ResetToDefault();
        Label_2853:
            return;
        Label_2854:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_289F;
            }
            num35 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(4, 0);
            this.SetTextValue(num35);
            this.ToggleEmpty((num35 == 0) == 0);
            goto Label_28A5;
        Label_289F:
            this.ResetToDefault();
        Label_28A5:
            return;
        Label_28A6:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_28F1;
            }
            num36 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(5, 0);
            this.SetTextValue(num36);
            this.ToggleEmpty((num36 == 0) == 0);
            goto Label_28F7;
        Label_28F1:
            this.ResetToDefault();
        Label_28F7:
            return;
        Label_28F8:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2943;
            }
            num37 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(6, 0);
            this.SetTextValue(num37);
            this.ToggleEmpty((num37 == 0) == 0);
            goto Label_2949;
        Label_2943:
            this.ResetToDefault();
        Label_2949:
            return;
        Label_294A:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2995;
            }
            num38 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(7, 0);
            this.SetTextValue(num38);
            this.ToggleEmpty((num38 == 0) == 0);
            goto Label_299B;
        Label_2995:
            this.ResetToDefault();
        Label_299B:
            return;
        Label_299C:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_29E7;
            }
            num39 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(8, 0);
            this.SetTextValue(num39);
            this.ToggleEmpty((num39 == 0) == 0);
            goto Label_29ED;
        Label_29E7:
            this.ResetToDefault();
        Label_29ED:
            return;
        Label_29EE:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2A3A;
            }
            num40 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(9, 0);
            this.SetTextValue(num40);
            this.ToggleEmpty((num40 == 0) == 0);
            goto Label_2A40;
        Label_2A3A:
            this.ResetToDefault();
        Label_2A40:
            return;
        Label_2A41:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2A8D;
            }
            num41 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(11, 0);
            this.SetTextValue(num41);
            this.ToggleEmpty((num41 == 0) == 0);
            goto Label_2A93;
        Label_2A8D:
            this.ResetToDefault();
        Label_2A93:
            return;
        Label_2A94:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2AE0;
            }
            num42 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(12, 0);
            this.SetTextValue(num42);
            this.ToggleEmpty((num42 == 0) == 0);
            goto Label_2AE6;
        Label_2AE0:
            this.ResetToDefault();
        Label_2AE6:
            return;
        Label_2AE7:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2B33;
            }
            num43 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(13, 0);
            this.SetTextValue(num43);
            this.ToggleEmpty((num43 == 0) == 0);
            goto Label_2B39;
        Label_2B33:
            this.ResetToDefault();
        Label_2B39:
            return;
        Label_2B3A:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2B86;
            }
            num44 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(14, 0);
            this.SetTextValue(num44);
            this.ToggleEmpty((num44 == 0) == 0);
            goto Label_2B8C;
        Label_2B86:
            this.ResetToDefault();
        Label_2B8C:
            return;
        Label_2B8D:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2BD9;
            }
            num45 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(15, 0);
            this.SetTextValue(num45);
            this.ToggleEmpty((num45 == 0) == 0);
            goto Label_2BDF;
        Label_2BD9:
            this.ResetToDefault();
        Label_2BDF:
            return;
        Label_2BE0:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2C2C;
            }
            num46 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(0x10, 0);
            this.SetTextValue(num46);
            this.ToggleEmpty((num46 == 0) == 0);
            goto Label_2C32;
        Label_2C2C:
            this.ResetToDefault();
        Label_2C32:
            return;
        Label_2C33:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2C7F;
            }
            num47 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(0x11, 0);
            this.SetTextValue(num47);
            this.ToggleEmpty((num47 == 0) == 0);
            goto Label_2C85;
        Label_2C7F:
            this.ResetToDefault();
        Label_2C85:
            return;
        Label_2C86:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2CD2;
            }
            num48 = (data7.Skill == null) ? 0 : data7.Skill.GetBuffEffectValue(0x12, 0);
            this.SetTextValue(num48);
            this.ToggleEmpty((num48 == 0) == 0);
            goto Label_2CD8;
        Label_2CD2:
            this.ResetToDefault();
        Label_2CD8:
            return;
        Label_2CD9:
            if (((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null))
            {
                goto Label_2D0A;
            }
            this.SetTextValue(data7.ItemParam.name);
            goto Label_2D10;
        Label_2D0A:
            this.ResetToDefault();
        Label_2D10:
            return;
        Label_2D11:
            if ((((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null)) || ((string.IsNullOrEmpty(data7.ItemParam.icon) != null) || (this.LoadItemIcon(data7.ItemParam.icon) == null)))
            {
                goto Label_2D59;
            }
            return;
        Label_2D59:
            this.ResetToDefault();
            return;
        Label_2D60:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_2DA2;
            }
            num49 = unit.Gems;
            num50 = unit.MaximumStatus.param.mp;
            this.SetTextValue(num49);
            this.SetSliderValue(num49, num50);
            return;
        Label_2DA2:
            this.ResetToDefault();
            return;
        Label_2DA9:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_2E0F;
            }
            num51 = 0;
            unit = this.GetUnit();
            if (unit == null)
            {
                goto Label_2DEA;
            }
            num51 = unit.MaximumStatus.param.mp;
            goto Label_2E01;
        Label_2DEA:
            num51 = data2.Status.param.mp;
        Label_2E01:
            this.SetTextValue(num51);
            return;
        Label_2E0F:
            this.ResetToDefault();
            return;
        Label_2E16:
            return;
        Label_2E17:
            return;
        Label_2E18:
            return;
        Label_2E19:
            return;
        Label_2E1A:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2E56;
            }
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.GetItemAmount(data7.ItemID));
            this.SetUpdateInterval(1f);
            goto Label_2E5C;
        Label_2E56:
            this.ResetToDefault();
        Label_2E5C:
            return;
        Label_2E5D:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_2E79;
            }
            this.SetTextValue(param2.equipLv);
            return;
        Label_2E79:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_2E9A;
            }
            this.SetTextValue(data7.ItemParam.equipLv);
            return;
        Label_2E9A:
            this.ResetToDefault();
            return;
        Label_2EA1:
            if ((recipe = DataSource.FindDataOfClass<JobEvolutionRecipe>(base.get_gameObject(), null)) == null)
            {
                goto Label_2EDB;
            }
            this.SetTextValue(recipe.Amount);
            this.SetSliderValue(recipe.Amount, recipe.RequiredAmount);
            goto Label_2EE1;
        Label_2EDB:
            this.ResetToDefault();
        Label_2EE1:
            return;
        Label_2EE2:
            if ((recipe = DataSource.FindDataOfClass<JobEvolutionRecipe>(base.get_gameObject(), null)) == null)
            {
                goto Label_2F08;
            }
            this.SetTextValue(recipe.RequiredAmount);
            goto Label_2F0E;
        Label_2F08:
            this.ResetToDefault();
        Label_2F0E:
            return;
        Label_2F0F:
            if (((recipe = DataSource.FindDataOfClass<JobEvolutionRecipe>(base.get_gameObject(), null)) == null) || (this.LoadItemIcon(recipe.Item.icon) == null))
            {
                goto Label_2F3B;
            }
            return;
        Label_2F3B:
            this.ResetToDefault();
            return;
        Label_2F42:
            if ((recipe = DataSource.FindDataOfClass<JobEvolutionRecipe>(base.get_gameObject(), null)) == null)
            {
                goto Label_2F6D;
            }
            this.SetTextValue(recipe.Item.name);
            goto Label_2F73;
        Label_2F6D:
            this.ResetToDefault();
        Label_2F73:
            return;
        Label_2F74:
            param13 = DataSource.FindDataOfClass<RecipeParam>(base.get_gameObject(), null);
            if (param13 == null)
            {
                goto Label_2FB3;
            }
            this.SetTextValue(param13.cost);
            this.SetSliderValue(MonoSingleton<GameManager>.Instance.Player.Gold, param13.cost);
            return;
        Label_2FB3:
            this.ResetToDefault();
            return;
        Label_2FBA:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_2FE3;
            }
            this.SetTextValue(data2.Status.param.cri);
            return;
        Label_2FE3:
            this.ResetToDefault();
            return;
        Label_2FEA:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_3013;
            }
            this.SetTextValue(data2.Status.param.rec);
            return;
        Label_3013:
            this.ResetToDefault();
            return;
        Label_301A:
            if (((data2 = this.GetUnitData()) == null) || (data2.LeaderSkill == null))
            {
                goto Label_3044;
            }
            this.SetTextValue(data2.LeaderSkill.Name);
            return;
        Label_3044:
            this.ResetToDefault();
            return;
        Label_304B:
            if (((data2 = this.GetUnitData()) == null) || (data2.LeaderSkill == null))
            {
                goto Label_307A;
            }
            this.SetTextValue(data2.LeaderSkill.SkillParam.expr);
            return;
        Label_307A:
            this.ResetToDefault();
            return;
        Label_3081:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_309D;
            }
            this.SetTextValue(param2.value);
            return;
        Label_309D:
            this.ResetToDefault();
            return;
        Label_30A4:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_30BF;
            }
            this.SetTextValue(data2.GetLevelCap(0));
            return;
        Label_30BF:
            this.ResetToDefault();
            return;
        Label_30C6:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_30FF;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num52 = data2.GetLevelCap(0);
            num53 = manager.Player.Lv;
            this.SetTextValue(Mathf.Min(num52, num53));
            return;
        Label_30FF:
            this.ResetToDefault();
            return;
        Label_3106:
            if ((((data2 = this.GetUnitData()) == null) || (0 > this.Index)) || (this.Index >= ((int) data2.Jobs.Length)))
            {
                goto Label_3154;
            }
            data23 = data2.Jobs[this.Index];
            this.SetAnimatorBool("unlocked", data23.IsActivated);
            return;
        Label_3154:
            this.ResetToDefault();
            return;
        Label_315B:
            info2 = DataSource.FindDataOfClass<AbilityUnlockInfo>(base.get_gameObject(), null);
            if (info2 == null)
            {
                goto Label_3199;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.ABILITY_UNLOCK_RANK"), info2.JobName, (int) info2.Rank));
            return;
        Label_3199:
            this.ResetToDefault();
            return;
        Label_31A0:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_31BC;
            }
            this.SetTextValue(param3.expr);
            return;
        Label_31BC:
            this.ResetToDefault();
            return;
        Label_31C3:
            this.SetItemFrame(this.GetItemParam());
            return;
        Label_31D0:
            this.SetItemFrame(this.GetInventoryItemParam());
            return;
        Label_31DD:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_3225;
            }
            parameter.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(parameter.Item.iname);
            this.SetTextValue(parameter.Amount);
            goto Label_322B;
        Label_3225:
            this.ResetToDefault();
        Label_322B:
            return;
        Label_322C:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_3252;
            }
            this.SetTextValue(parameter.RequiredAmount);
            goto Label_3258;
        Label_3252:
            this.ResetToDefault();
        Label_3258:
            return;
        Label_3259:
            if (((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null) || (this.LoadItemIcon(parameter.Item.icon) == null))
            {
                goto Label_3285;
            }
            return;
        Label_3285:
            this.ResetToDefault();
            return;
        Label_328C:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_32B7;
            }
            this.SetTextValue(parameter.Item.name);
            goto Label_32BD;
        Label_32B7:
            this.ResetToDefault();
        Label_32BD:
            return;
        Label_32BE:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_32FF;
            }
            param14 = MonoSingleton<GameManager>.Instance.GetRecipeParam(parameter.Item.recipe);
            if (param14 == null)
            {
                goto Label_32FF;
            }
            this.SetTextValue(param14.cost);
            return;
        Label_32FF:
            this.ResetToDefault();
            return;
        Label_3306:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_332D;
            }
            this.SetTextValue(parameter.Item.name);
            return;
        Label_332D:
            this.ResetToDefault();
            return;
        Label_3334:
            if ((parameter = DataSource.FindDataOfClass<RecipeItemParameter>(base.get_gameObject(), null)) == null)
            {
                goto Label_3356;
            }
            this.SetItemFrame(parameter.Item);
            return;
        Label_3356:
            return;
        Label_3357:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_3391;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.UnitSkinIconMedium(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJobId);
            return;
        Label_3391:
            this.ResetToDefault();
            return;
        Label_3398:
            record = DataSource.FindDataOfClass<BattleCore.Record>(base.get_gameObject(), null);
            if (record == null)
            {
                goto Label_33C0;
            }
            this.SetTextValue(record.playerexp);
            return;
        Label_33C0:
            this.ResetToDefault();
            return;
        Label_33C7:
            record2 = DataSource.FindDataOfClass<BattleCore.Record>(base.get_gameObject(), null);
            if (record2 == null)
            {
                goto Label_33EF;
            }
            this.SetTextValue(record2.gold);
            return;
        Label_33EF:
            this.ResetToDefault();
            return;
        Label_33F6:
            record3 = DataSource.FindDataOfClass<BattleCore.Record>(base.get_gameObject(), null);
            if (record3 == null)
            {
                goto Label_341E;
            }
            this.SetTextValue(record3.unitexp);
            return;
        Label_341E:
            this.ResetToDefault();
            return;
        Label_3425:
            record4 = DataSource.FindDataOfClass<BattleCore.Record>(base.get_gameObject(), null);
            if (record4 == null)
            {
                goto Label_344D;
            }
            this.SetTextValue(record4.multicoin);
            return;
        Label_344D:
            this.ResetToDefault();
            return;
        Label_3454:
            this.ResetToDefault();
            return;
        Label_345B:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_3477;
            }
            this.SetTextValue(info.LevelPrev);
            return;
        Label_3477:
            this.ResetToDefault();
            return;
        Label_347E:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_349A;
            }
            this.SetTextValue(info.LevelNext);
            return;
        Label_349A:
            this.ResetToDefault();
            return;
        Label_34A1:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_34BD;
            }
            this.SetTextValue(info.StaminaNext);
            return;
        Label_34BD:
            this.ResetToDefault();
            return;
        Label_34C4:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_34E0;
            }
            this.SetTextValue(info.StaminaMaxPrev);
            return;
        Label_34E0:
            this.ResetToDefault();
            return;
        Label_34E7:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_3503;
            }
            this.SetTextValue(info.StaminaMaxNext);
            return;
        Label_3503:
            this.ResetToDefault();
            return;
        Label_350A:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_3526;
            }
            this.SetTextValue(info.MaxFriendNumPrev);
            return;
        Label_3526:
            this.ResetToDefault();
            return;
        Label_352D:
            if ((info = this.GetLevelUpInfo()) == null)
            {
                goto Label_3549;
            }
            this.SetTextValue(info.MaxFriendNumNext);
            return;
        Label_3549:
            this.ResetToDefault();
            return;
        Label_3550:
            if ((((info = this.GetLevelUpInfo()) == null) || (0 > this.Index)) || (this.Index >= ((int) info.UnlockInfo.Length)))
            {
                goto Label_3593;
            }
            this.SetTextValue(info.UnlockInfo[this.Index]);
            return;
        Label_3593:
            this.ResetToDefault();
            return;
        Label_359A:
            if ((((param = this.GetQuestParam()) == null) || (param.bonusObjective == null)) || ((0 > this.Index) || (this.Index >= ((int) param.bonusObjective.Length))))
            {
                goto Label_3607;
            }
            num54 = ((param.clear_missions & (1 << (this.Index & 0x1f))) == null) ? 0 : 1;
            this.SetAnimatorInt("state", num54);
            this.SetImageIndex(num54);
            return;
        Label_3607:
            this.ResetToDefault();
            return;
        Label_360E:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_363C;
            }
            this.SetImageIndex(unit.Side);
            this.SetAnimatorInt("index", unit.Side);
            return;
        Label_363C:
            this.ResetToDefault();
            return;
        Label_3643:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_3671;
            }
            num55 = MonoSingleton<GameManager>.Instance.Player.GetCreateItemCost(param2);
            this.SetTextValue(num55);
            goto Label_3677;
        Label_3671:
            this.ResetToDefault();
        Label_3677:
            return;
        Label_3678:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_3698;
            }
            this.SetTextValue(SceneBattle.Instance.GoldCount);
        Label_3698:
            return;
        Label_3699:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_36B9;
            }
            this.SetTextValue(SceneBattle.Instance.DispTreasureCount);
        Label_36B9:
            return;
        Label_36BA:
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.UpdateCaveStamina();
            this.SetTextValue(manager.Player.CaveStamina);
            this.SetSliderValue(manager.Player.CaveStamina, manager.Player.CaveStaminaMax);
            this.SetUpdateInterval(1f);
            return;
        Label_3704:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.CaveStaminaMax);
            this.SetUpdateInterval(1f);
            return;
        Label_3727:
            num56 = MonoSingleton<GameManager>.Instance.Player.GetNextCaveStaminaRecoverySec();
            this.SetTextValue(TimeManager.ToMinSecString(num56));
            this.SetUpdateInterval(1f);
            return;
        Label_3753:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_3773;
            }
            this.SetTextValue(param2.cap);
            goto Label_3779;
        Label_3773:
            this.ResetToDefault();
        Label_3779:
            return;
        Label_377A:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.GetItemSlotAmount());
            return;
        Label_3792:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_37E7;
            }
            num57 = 0;
            switch (param.difficulty)
            {
                case 0:
                    goto Label_37C6;

                case 1:
                    goto Label_37CE;

                case 2:
                    goto Label_37D6;
            }
            goto Label_37DE;
        Label_37C6:
            num57 = 0;
            goto Label_37DE;
        Label_37CE:
            num57 = 1;
            goto Label_37DE;
        Label_37D6:
            num57 = 2;
        Label_37DE:
            this.SetImageIndex(num57);
            return;
        Label_37E7:
            this.ResetToDefault();
            return;
        Label_37EE:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_3849;
            }
            num58 = 0;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_3822;
            }
            num58 = SceneBattle.Instance.GetDisplayHeight(unit);
            goto Label_3840;
        Label_3822:
            if ((MultiPlayVersusReady.Instance != null) == null)
            {
                goto Label_3840;
            }
            num58 = MultiPlayVersusReady.Instance.GetDisplayHeight(unit);
        Label_3840:
            this.SetTextValue(num58);
            return;
        Label_3849:
            this.ResetToDefault();
            return;
        Label_3850:
            if (((data7 = this.GetEquipData()) == null) || (data7.IsValid() == null))
            {
                goto Label_387C;
            }
            this.SetItemFrame(data7.ItemParam);
            goto Label_3882;
        Label_387C:
            this.ResetToDefault();
        Label_3882:
            return;
        Label_3883:
            param15 = DataSource.FindDataOfClass<ChapterParam>(base.get_gameObject(), null);
            if (param15 == null)
            {
                goto Label_38A6;
            }
            this.SetTextValue(param15.name);
            return;
        Label_38A6:
            this.ResetToDefault();
            return;
        Label_38AD:
            param16 = DataSource.FindDataOfClass<ChapterParam>(base.get_gameObject(), null);
            if (param16 == null)
            {
                goto Label_38EB;
            }
            this.SetTextValue((param16.sectionParam != null) ? param16.sectionParam.name : string.Empty);
            return;
        Label_38EB:
            this.ResetToDefault();
            return;
        Label_38F2:
            param17 = DataSource.FindDataOfClass<ChapterParam>(base.get_gameObject(), null);
            if (param17 == null)
            {
                goto Label_3915;
            }
            this.SetTextValue(param17.expr);
            return;
        Label_3915:
            this.ResetToDefault();
            return;
        Label_391C:
            if ((data8 = this.GetMailData()) == null)
            {
                goto Label_393C;
            }
            this.SetTextValue(data8.msg);
            goto Label_3942;
        Label_393C:
            this.ResetToDefault();
        Label_3942:
            return;
        Label_3943:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_3968;
            }
            this.SetImageIndex((GlobalVars.SelectedMultiPlayQuestIsEvent == null) ? 0 : 1);
            return;
        Label_3968:
            this.ResetToDefault();
            return;
        Label_396F:
            param4 = this.GetRoomPlayerParam();
            if (param4 != null)
            {
                goto Label_398E;
            }
            this.SetTextValue(string.Empty);
            goto Label_399B;
        Label_398E:
            this.SetTextValue(param4.playerName);
        Label_399B:
            return;
        Label_399C:
            param4 = this.GetRoomPlayerParam();
            if (param4 != null)
            {
                goto Label_39B6;
            }
            this.ResetToDefault();
            goto Label_39C3;
        Label_39B6:
            this.SetTextValue(param4.playerLevel);
        Label_39C3:
            return;
        Label_39C4:
            param4 = this.GetRoomPlayerParam();
            if (param4 == null)
            {
                goto Label_3A93;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            flag = 0;
            if ((photon != null) == null)
            {
                goto Label_3A23;
            }
            list2 = photon.GetRoomPlayerList();
            if (list2 == null)
            {
                goto Label_3A23;
            }
            player = photon.FindPlayer(list2, param4.playerID, param4.playerIndex);
            if (player == null)
            {
                goto Label_3A23;
            }
            flag = player.start;
        Label_3A23:
            if (this.Index != null)
            {
                goto Label_3A6C;
            }
            base.get_gameObject().SetActive((((param4.state == null) || (param4.state == 4)) || (param4.state == 5)) ? 0 : (flag == 0));
            goto Label_3A92;
        Label_3A6C:
            if (this.Index != 1)
            {
                goto Label_3A92;
            }
            base.get_gameObject().SetActive(param4.state == this.InstanceType);
        Label_3A92:
            return;
        Label_3A93:
            base.get_gameObject().SetActive(0);
            return;
        Label_3AA0:
            elem = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem != null) ? elem.unit : null;
            if (data2 != null)
            {
                goto Label_3AD4;
            }
            this.ResetToDefault();
            goto Label_3B20;
        Label_3AD4:
            str7 = AssetPath.UnitSkinIconSmall(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str7) == null)
            {
                goto Label_3B0A;
            }
            this.ResetToDefault();
            goto Label_3B20;
        Label_3B0A:
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str7;
        Label_3B20:
            return;
        Label_3B21:
            elem2 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem2 != null) ? elem2.unit : null;
            if (data2 != null)
            {
                goto Label_3B55;
            }
            this.ResetToDefault();
            goto Label_3B66;
        Label_3B55:
            this.SetImageIndex(data2.CurrentJob.Rank);
        Label_3B66:
            return;
        Label_3B67:
            elem3 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem3 != null) ? elem3.unit : null;
            if (data2 != null)
            {
                goto Label_3B9B;
            }
            this.ResetToDefault();
            goto Label_3BC6;
        Label_3B9B:
            num59 = data2.CurrentJob.Rank;
            num60 = data2.GetJobRankCap();
            this.SetTextValue(num59);
            this.SetSliderValue(num59, num60);
        Label_3BC6:
            return;
        Label_3BC7:
            elem4 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem4 != null) ? elem4.unit : null;
            if ((data2 != null) && (data2.CurrentJob != null))
            {
                goto Label_3C06;
            }
            this.ResetToDefault();
            goto Label_3C4B;
        Label_3C06:
            param18 = data2.CurrentJob.Param;
            str8 = (param18 != null) ? AssetPath.JobIconSmall(param18) : null;
            if (string.IsNullOrEmpty(str8) != null)
            {
                goto Label_3C4B;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str8;
        Label_3C4B:
            return;
        Label_3C4C:
            elem5 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem5 != null) ? elem5.unit : null;
            if (data2 != null)
            {
                goto Label_3C80;
            }
            this.ResetToDefault();
            goto Label_3CD5;
        Label_3C80:
            gauge2 = base.GetComponent<StarGauge>();
            if ((gauge2 != null) == null)
            {
                goto Label_3CB8;
            }
            gauge2.Max = data2.GetRarityCap() + 1;
            gauge2.Value = data2.Rarity + 1;
            goto Label_3CD5;
        Label_3CB8:
            this.SetAnimatorInt("rare", data2.Rarity);
            this.SetImageIndex(data2.Rarity);
        Label_3CD5:
            return;
        Label_3CD6:
            elem6 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem6 != null) ? elem6.unit : null;
            if (data2 != null)
            {
                goto Label_3D0A;
            }
            this.ResetToDefault();
            goto Label_3D27;
        Label_3D0A:
            num61 = data2.Element;
            this.SetAnimatorInt("element", num61);
            this.SetImageIndex(num61);
        Label_3D27:
            return;
        Label_3D28:
            elem7 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem7 != null) ? elem7.unit : null;
            if (data2 != null)
            {
                goto Label_3D5C;
            }
            this.ResetToDefault();
            goto Label_3D76;
        Label_3D5C:
            num62 = data2.CalcLevel();
            this.SetTextValue(num62);
            this.SetSliderValue(num62, 0x63);
        Label_3D76:
            return;
        Label_3D77:
            param19 = this.GetRoomParam();
            num63 = (param19 != null) ? param19.GetUnitSlotNum() : 0;
            base.get_gameObject().SetActive((0 > this.Index) ? 0 : (this.Index < num63));
            return;
        Label_3DBA:
            param4 = this.GetRoomPlayerParam();
            if (param4 != null)
            {
                goto Label_3DD4;
            }
            this.ResetToDefault();
            goto Label_3DE1;
        Label_3DD4:
            this.SetTextValue(param4.playerIndex);
        Label_3DE1:
            return;
        Label_3DE2:
            param4 = this.GetRoomPlayerParam();
            if (param4 == null)
            {
                goto Label_3E18;
            }
            base.get_gameObject().SetActive((param4 == null) ? 0 : ((param4.playerID > 0) == 0));
            goto Label_3E24;
        Label_3E18:
            base.get_gameObject().SetActive(1);
        Label_3E24:
            return;
        Label_3E25:
            param4 = this.GetRoomPlayerParam();
            base.get_gameObject().SetActive((param4 == null) ? 0 : (param4.playerID > 0));
            return;
        Label_3E4D:
            photon2 = PunMonoSingleton<MyPhoton>.Instance;
            param4 = this.GetRoomPlayerParam();
            list3 = photon2.GetRoomPlayerList();
            player2 = (param4 == null) ? null : photon2.FindPlayer(list3, param4.playerID, param4.playerIndex);
            base.get_gameObject().SetActive((player2 == null) ? 0 : photon2.IsHost(player2.playerID));
            return;
        Label_3EAF:
            param4 = this.GetRoomPlayerParam();
            if ((param4 != null) && (param4.playerID > 0))
            {
                goto Label_3EDC;
            }
            base.get_gameObject().SetActive(0);
            goto Label_4093;
        Label_3EDC:
            flag2 = param4.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex;
            flag3 = 1;
            flag3 &= (param4.state == 0) == 0;
            flag3 &= (param4.state == 4) == 0;
            flag3 &= (param4.state == 5) == 0;
            if (this.Index != null)
            {
                goto Label_3F47;
            }
            base.get_gameObject().SetActive(flag2);
            goto Label_4093;
        Label_3F47:
            if (this.Index != 1)
            {
                goto Label_3F68;
            }
            base.get_gameObject().SetActive(flag2 == 0);
            goto Label_4093;
        Label_3F68:
            if (this.Index != 2)
            {
                goto Label_3F99;
            }
            base.get_gameObject().SetActive(1);
            this.SetImageIndex((flag2 == null) ? 1 : 0);
            goto Label_4093;
        Label_3F99:
            if (this.Index != 3)
            {
                goto Label_3FC4;
            }
            base.get_gameObject().SetActive((flag2 == null) ? 0 : (flag3 == 0));
            goto Label_4093;
        Label_3FC4:
            if (this.Index != 4)
            {
                goto Label_3FEC;
            }
            base.get_gameObject().SetActive((flag2 == null) ? 1 : flag3);
            goto Label_4093;
        Label_3FEC:
            if (this.Index != 5)
            {
                goto Label_4020;
            }
            button = base.get_gameObject().GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_4093;
            }
            button.set_interactable(flag2);
            goto Label_4093;
        Label_4020:
            if (this.Index != 6)
            {
                goto Label_4093;
            }
            photon3 = PunMonoSingleton<MyPhoton>.Instance;
            list4 = photon3.GetRoomPlayerList();
            if (list4 == null)
            {
                goto Label_4087;
            }
            player3 = photon3.FindPlayer(list4, param4.playerID, param4.playerIndex);
            if (player3 != null)
            {
                goto Label_4074;
            }
            base.get_gameObject().SetActive(0);
            goto Label_4086;
        Label_4074:
            base.get_gameObject().SetActive(player3.start);
        Label_4086:
            return;
        Label_4087:
            base.get_gameObject().SetActive(0);
        Label_4093:
            return;
        Label_4094:
            base.get_gameObject().SetActive(GlobalVars.SelectedMultiPlayRoomType == 0);
            return;
        Label_40A8:
            base.get_gameObject().SetActive(GlobalVars.SelectedMultiPlayRoomType == 1);
            return;
        Label_40BC:
            base.get_gameObject().SetActive(MultiPlayAPIRoom.IsLocked(GlobalVars.EditMultiPlayRoomPassCode));
            return;
        Label_40D2:
            param20 = this.GetRoomParam();
            param21 = GlobalVars.SelectedMultiPlayerParam;
            if (((param21 != null) && (param21.units != null)) && (param20 != null))
            {
                goto Label_4102;
            }
            this.ResetToDefault();
            return;
        Label_4102:
            num64 = 0;
            num65 = param20.GetUnitSlotNum(param21.playerIndex);
            num66 = 0;
            goto Label_41A2;
        Label_411D:
            if (param21.units[num66].slotID >= num65)
            {
                goto Label_419C;
            }
            if (param21.units[num66].unit != null)
            {
                goto Label_414C;
            }
            goto Label_419C;
        Label_414C:
            num64 += param21.units[num66].unit.Status.param.atk;
            num64 += param21.units[num66].unit.Status.param.mag;
        Label_419C:
            num66 += 1;
        Label_41A2:
            if (num66 < ((int) param21.units.Length))
            {
                goto Label_411D;
            }
            this.SetTextValue(num64);
            return;
        Label_41BB:
            if ((data8 = this.GetMailData()) != null)
            {
                goto Label_41D0;
            }
            this.ResetToDefault();
            return;
        Label_41D0:
            str9 = string.Empty;
            dataArray = data8.gifts;
            num67 = 0;
            goto Label_45FB;
        Label_41E8:
            if (dataArray[num67].coin <= 0)
            {
                goto Label_423B;
            }
            str9 = ((str9 + LocalizedText.Get("sys.COIN")) + "\x00d7" + &dataArray[num67].coin.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
        Label_423B:
            if (dataArray[num67].gold <= 0)
            {
                goto Label_4272;
            }
            str9 = str9 + string.Format(LocalizedText.Get("sys.CONVERT_TO_GOLD"), (int) dataArray[num67].gold);
        Label_4272:
            if (dataArray[num67].arenacoin <= 0)
            {
                goto Label_42C5;
            }
            str9 = ((str9 + LocalizedText.Get("sys.ARENA_COIN")) + "\x00d7" + &dataArray[num67].arenacoin.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
        Label_42C5:
            if (dataArray[num67].multicoin <= 0)
            {
                goto Label_4318;
            }
            str9 = ((str9 + LocalizedText.Get("sys.MULTI_COIN")) + "\x00d7" + &dataArray[num67].multicoin.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
        Label_4318:
            if (dataArray[num67].kakeracoin <= 0)
            {
                goto Label_436B;
            }
            str9 = ((str9 + LocalizedText.Get("sys.PIECE_POINT")) + "\x00d7" + &dataArray[num67].kakeracoin.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM_MAI");
        Label_436B:
            if ((string.IsNullOrEmpty(dataArray[num67].ConceptCardIname) != null) || (dataArray[num67].CheckGiftTypeIncluded(0x1000L) == null))
            {
                goto Label_4402;
            }
            param22 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(dataArray[num67].ConceptCardIname);
            if (param22 == null)
            {
                goto Label_4402;
            }
            str10 = "\x00d7";
            str9 = ((str9 + param22.name) + str10 + &dataArray[num67].ConceptCardNum.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
        Label_4402:
            if ((dataArray[num67].iname == null) || (dataArray[num67].num <= 0))
            {
                goto Label_45BC;
            }
            if (dataArray[num67].CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_4499;
            }
            param23 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataArray[num67].iname);
            if (param23 == null)
            {
                goto Label_4499;
            }
            str11 = "\x00d7";
            str9 = ((str9 + param23.name) + str11 + &dataArray[num67].num.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
        Label_4499:
            if ((dataArray[num67].CheckGiftTypeIncluded(1L) == null) && (dataArray[num67].CheckGiftTypeIncluded(0x2700L) == null))
            {
                goto Label_4520;
            }
            param24 = MonoSingleton<GameManager>.Instance.GetItemParam(dataArray[num67].iname);
            if (param24 == null)
            {
                goto Label_4520;
            }
            str12 = "\x00d7";
            str9 = ((str9 + param24.name) + str12 + &dataArray[num67].num.ToString()) + LocalizedText.Get("sys.MAILBOX_ITEM_NUM");
        Label_4520:
            if (dataArray[num67].CheckGiftTypeIncluded(0x80L) == null)
            {
                goto Label_4567;
            }
            param25 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(dataArray[num67].iname);
            if (param25 == null)
            {
                goto Label_4567;
            }
            str9 = str9 + param25.name;
        Label_4567:
            if (dataArray[num67].CheckGiftTypeIncluded(0x800L) == null)
            {
                goto Label_45BC;
            }
            param26 = MonoSingleton<GameManager>.Instance.GetAwardParam(dataArray[num67].iname);
            if (param26 == null)
            {
                goto Label_45BC;
            }
            str9 = (str9 + LocalizedText.Get("sys.MAILBOX_ITEM_AWARD")) + param26.name;
        Label_45BC:
            if (((str9 != string.Empty) == null) || (str9[str9.Length - 1] == 0x3001))
            {
                goto Label_45F5;
            }
            str9 = str9 + "、";
        Label_45F5:
            num67 += 1;
        Label_45FB:
            if (num67 < ((int) dataArray.Length))
            {
                goto Label_41E8;
            }
            if ((str9 != string.Empty) == null)
            {
                goto Label_462A;
            }
            str9 = str9.Substring(0, str9.Length - 1);
        Label_462A:
            this.SetTextValue(str9);
            return;
        Label_4633:
            if ((data8 = this.GetMailData()) != null)
            {
                goto Label_4648;
            }
            this.ResetToDefault();
            return;
        Label_4648:
            time = &GameUtility.UnixtimeToLocalTime(data8.post_at).AddDays(14.0);
            str13 = "yyyy/MM/dd HH:mm";
            this.SetTextValue(&time.ToString(str13));
            return;
        Label_467F:
            if ((data8 = this.GetMailData()) != null)
            {
                goto Label_4694;
            }
            this.ResetToDefault();
            return;
        Label_4694:
            time2 = GameUtility.UnixtimeToLocalTime(data8.read);
            str14 = "yyyy/MM/dd HH:mm:ss";
            this.SetTextValue(&time2.ToString(str14));
            return;
        Label_46B9:
            if ((data8 = this.GetMailData()) != null)
            {
                goto Label_46CE;
            }
            this.ResetToDefault();
            return;
        Label_46CE:
            time3 = TimeManager.ServerTime;
            time4 = GameUtility.UnixtimeToLocalTime(data8.post_at);
            span = time3 - time4;
            str15 = string.Empty;
            if (&span.Days < 1)
            {
                goto Label_4719;
            }
            str16 = "yyyy/MM/dd";
            str15 = &time4.ToString(str16);
            goto Label_4785;
        Label_4719:
            if (&span.Hours < 1)
            {
                goto Label_4743;
            }
            str15 = ((int) &span.Hours) + "時間前";
            goto Label_4785;
        Label_4743:
            if (&span.Minutes < 1)
            {
                goto Label_476D;
            }
            str15 = ((int) &span.Minutes) + "分前";
            goto Label_4785;
        Label_476D:
            str15 = ((int) &span.Seconds) + "秒前";
        Label_4785:
            this.SetTextValue(str15);
            return;
        Label_478E:
            room = this.GetRoom();
            if (room != null)
            {
                goto Label_47AD;
            }
            this.SetTextValue(string.Empty);
            goto Label_47D5;
        Label_47AD:
            this.SetTextValue((string.IsNullOrEmpty(room.comment) == null) ? room.comment : string.Empty);
        Label_47D5:
            return;
        Label_47D6:
            room = this.GetRoom();
            if (room != null)
            {
                goto Label_47F5;
            }
            this.SetTextValue(string.Empty);
            goto Label_4833;
        Label_47F5:;
            this.SetTextValue(((room.owner != null) && (string.IsNullOrEmpty(room.owner.name) == null)) ? room.owner.name : "???");
        Label_4833:
            return;
        Label_4834:
            room = this.GetRoom();
            if ((room != null) && (room.owner != null))
            {
                goto Label_485F;
            }
            this.SetTextValue(string.Empty);
            goto Label_4871;
        Label_485F:
            this.SetTextValue(room.owner.level);
        Label_4871:
            return;
        Label_4872:
            room = this.GetRoom();
            param = (((room != null) && (room.quest != null)) && (string.IsNullOrEmpty(room.quest.iname) == null)) ? MonoSingleton<GameManager>.Instance.FindQuest(room.quest.iname) : null;
            if (param != null)
            {
                goto Label_48D6;
            }
            this.SetTextValue(string.Empty);
            goto Label_48FC;
        Label_48D6:
            this.SetTextValue((string.IsNullOrEmpty(param.name) == null) ? param.name : "ERROR");
        Label_48FC:
            return;
        Label_48FD:
            room = this.GetRoom();
            if (room != null)
            {
                goto Label_491C;
            }
            this.SetTextValue(string.Empty);
            goto Label_4929;
        Label_491C:
            this.SetTextValue(room.num);
        Label_4929:
            return;
        Label_492A:
            room = this.GetRoom();
            param = (((room != null) && (room.quest != null)) && (string.IsNullOrEmpty(room.quest.iname) == null)) ? MonoSingleton<GameManager>.Instance.FindQuest(room.quest.iname) : null;
            if (param != null)
            {
                goto Label_498E;
            }
            this.SetTextValue(string.Empty);
            goto Label_499F;
        Label_498E:
            this.SetTextValue(param.playerNum);
        Label_499F:
            return;
        Label_49A0:
            room = this.GetRoom();
            if (room != null)
            {
                goto Label_49C0;
            }
            base.get_gameObject().SetActive(0);
            goto Label_4A01;
        Label_49C0:
            if (this.Index != null)
            {
                goto Label_49E7;
            }
            base.get_gameObject().SetActive(MultiPlayAPIRoom.IsLocked(room.pwd_hash));
            goto Label_4A01;
        Label_49E7:
            base.get_gameObject().SetActive(MultiPlayAPIRoom.IsLocked(room.pwd_hash) == 0);
        Label_4A01:
            return;
        Label_4A02:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.FUID != null)
            {
                goto Label_4A1F;
            }
            this.ResetToDefault();
            return;
        Label_4A1F:
            this.SetTextValue(manager.Player.FUID);
            return;
        Label_4A31:
            if ((data = this.GetFriendData()) != null)
            {
                goto Label_4A45;
            }
            this.ResetToDefault();
            return;
        Label_4A45:
            this.SetTextValue(data.FUID);
            return;
        Label_4A52:
            if ((data = this.GetFriendData()) != null)
            {
                goto Label_4A66;
            }
            this.ResetToDefault();
            return;
        Label_4A66:
            this.SetTextValue(data.PlayerName);
            return;
        Label_4A73:
            if ((data = this.GetFriendData()) != null)
            {
                goto Label_4A87;
            }
            this.ResetToDefault();
            return;
        Label_4A87:
            this.SetTextValue(&data.PlayerLevel.ToString());
            return;
        Label_4A99:
            if ((data = this.GetFriendData()) != null)
            {
                goto Label_4AAD;
            }
            this.ResetToDefault();
            return;
        Label_4AAD:
            time5 = GameUtility.UnixtimeToLocalTime(data.LastLogin);
            span2 = DateTime.Now - time5;
            num68 = &span2.Days;
            if (num68 <= 0)
            {
                goto Label_4AFA;
            }
            objArray1 = new object[] { &num68.ToString() };
            this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_DAY", objArray1));
            return;
        Label_4AFA:
            num69 = &span2.Hours;
            if (num69 <= 0)
            {
                goto Label_4B2C;
            }
            objArray2 = new object[] { &num69.ToString() };
            this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_HOUR", objArray2));
            return;
        Label_4B2C:
            num70 = &span2.Minutes;
            objArray3 = new object[] { &num70.ToString() };
            this.SetTextValue(LocalizedText.Get("sys.LASTLOGIN_MINUTE", objArray3));
            return;
        Label_4B56:
            flag4 = 0;
            data = this.GetFriendData();
            if (data == null)
            {
                goto Label_4B6E;
            }
            flag4 = data.IsFavorite;
        Label_4B6E:
            if (this.Index != 1)
            {
                goto Label_4B81;
            }
            flag4 = flag4 == 0;
        Label_4B81:
            base.get_gameObject().SetActive(flag4);
            return;
        Label_4B8F:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.GetItemSlotAmount());
            return;
        Label_4BA7:
            item = this.GetSellItem();
            if (item != null)
            {
                goto Label_4BBD;
            }
            this.ResetToDefault();
            return;
        Label_4BBD:
            num71 = item.item.Sell * item.num;
            this.SetTextValue(num71);
            return;
        Label_4BDC:
            item = this.GetSellItem();
            if (item != null)
            {
                goto Label_4BF2;
            }
            this.ResetToDefault();
            return;
        Label_4BF2:
            this.SetTextValue(item.num);
            return;
        Label_4C00:
            item = this.GetSellItem();
            if (item != null)
            {
                goto Label_4C16;
            }
            this.ResetToDefault();
            return;
        Label_4C16:
            this.SetTextValue(item.index + 1);
            return;
        Label_4C26:
            list = this.GetSellItemList();
            if (list != null)
            {
                goto Label_4C3C;
            }
            this.ResetToDefault();
            return;
        Label_4C3C:
            this.SetTextValue(list.Count);
            return;
        Label_4C4A:
            list = this.GetSellItemList();
            if (list != null)
            {
                goto Label_4C60;
            }
            this.ResetToDefault();
            return;
        Label_4C60:
            num72 = 0;
            num73 = 0;
            goto Label_4C98;
        Label_4C6B:
            num72 += list[num73].item.Sell * list[num73].num;
            num73 += 1;
        Label_4C98:
            if (num73 < list.Count)
            {
                goto Label_4C6B;
            }
            this.SetTextValue(num72);
            return;
        Label_4CAF:
            item = this.GetSellItem();
            if (item == null)
            {
                goto Label_4CF0;
            }
            base.get_gameObject().SetActive((MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(item.item.Param.iname) == null) == 0);
            return;
        Label_4CF0:
            this.ResetToDefault();
            return;
        Label_4CF7:
            item2 = this.GetShopItem();
            if (item2 == null)
            {
                goto Label_4D14;
            }
            this.SetTextValue(item2.num);
            return;
        Label_4D14:
            this.ResetToDefault();
            return;
        Label_4D1B:
            item5 = this.GetLimitedShopItem();
            if (item5 == null)
            {
                goto Label_4D38;
            }
            this.SetTextValue(item5.remaining_num);
            return;
        Label_4D38:
            return;
        Label_4D39:
            item6 = this.GetLimitedShopItem();
            item7 = this.GetEventShopItem();
            if ((item6 == null) || (item6.isSetSaleValue == null))
            {
                goto Label_4D6A;
            }
            this.SetTextValue(item6.saleValue);
            return;
        Label_4D6A:
            if ((item7 == null) || (item7.isSetSaleValue == null))
            {
                goto Label_4D8B;
            }
            this.SetTextValue(item7.saleValue);
            return;
        Label_4D8B:
            num74 = 0;
            item2 = this.GetShopItem();
            if (item2 == null)
            {
                goto Label_4E9B;
            }
            if (item2.isSetSaleValue == null)
            {
                goto Label_4DB7;
            }
            this.SetTextValue(item2.saleValue);
            return;
        Label_4DB7:
            param2 = this.GetItemParam();
            if (param2 == null)
            {
                goto Label_4E9B;
            }
            switch (item2.saleType)
            {
                case 0:
                    goto Label_4DFF;

                case 1:
                    goto Label_4E15;

                case 2:
                    goto Label_4E2B;

                case 3:
                    goto Label_4E41;

                case 4:
                    goto Label_4E57;

                case 5:
                    goto Label_4E6D;

                case 6:
                    goto Label_4E83;

                case 7:
                    goto Label_4E15;
            }
            goto Label_4E92;
        Label_4DFF:
            num74 = item2.num * param2.buy;
            goto Label_4E92;
        Label_4E15:
            num74 = item2.num * param2.coin;
            goto Label_4E92;
        Label_4E2B:
            num74 = item2.num * param2.tour_coin;
            goto Label_4E92;
        Label_4E41:
            num74 = item2.num * param2.arena_coin;
            goto Label_4E92;
        Label_4E57:
            num74 = item2.num * param2.piece_point;
            goto Label_4E92;
        Label_4E6D:
            num74 = item2.num * param2.multi_coin;
            goto Label_4E92;
        Label_4E83:
            DebugUtility.Assert("There is no common price in the event coin.");
        Label_4E92:
            this.SetTextValue(num74);
            return;
        Label_4E9B:
            this.ResetToDefault();
            return;
        Label_4EA2:
            item2 = this.GetShopItem();
            if (item2 == null)
            {
                goto Label_4EC3;
            }
            base.get_gameObject().SetActive(item2.is_soldout);
        Label_4EC3:
            return;
        Label_4EC4:
            item2 = this.GetShopItem();
            if (item2 == null)
            {
                goto Label_4EE1;
            }
            this.SetBuyPriceTypeIcon(item2.saleType);
            return;
        Label_4EE1:
            this.ResetToDefault();
            return;
        Label_4EE8:
            item = this.GetSellItem();
            if (item == null)
            {
                goto Label_4F10;
            }
            base.get_gameObject().SetActive((item.index == -1) == 0);
            return;
        Label_4F10:
            this.ResetToDefault();
            return;
        Label_4F17:
            item = this.GetSellItem();
            if (item == null)
            {
                goto Label_4F35;
            }
            this.SetTextValue(-item.num);
            return;
        Label_4F35:
            this.ResetToDefault();
            return;
        Label_4F3C:
            param2 = this.GetItemParam();
            if (param2 == null)
            {
                goto Label_5006;
            }
            list5 = MonoSingleton<GameManager>.Instance.Player.Units;
            num75 = 0;
            goto Label_4FEB;
        Label_4F64:
            num76 = 0;
            goto Label_4FCE;
        Label_4F6C:
            data25 = list5[num75].Jobs[num76];
            if (data25.IsActivated != null)
            {
                goto Label_4F90;
            }
            goto Label_4FC8;
        Label_4F90:
            num77 = data25.FindEquipSlotByItemID(param2.iname);
            if (num77 != -1)
            {
                goto Label_4FAD;
            }
            goto Label_4FC8;
        Label_4FAD:
            if (data25.CheckEnableEquipSlot(num77) == null)
            {
                goto Label_4FC8;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_4FC8:
            num76 += 1;
        Label_4FCE:
            if (num76 < ((int) list5[num75].Jobs.Length))
            {
                goto Label_4F6C;
            }
            num75 += 1;
        Label_4FEB:
            if (num75 < list5.Count)
            {
                goto Label_4F64;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_5006:
            this.ResetToDefault();
            return;
        Label_500D:
            param27 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param27.ShopUpdateTime == null)
            {
                goto Label_50CF;
            }
            time6 = TimeManager.ServerTime;
            num78 = *(&(param27.ShopUpdateTime[0]));
            num79 = 0;
            goto Label_509B;
        Label_5052:
            if (&time6.Hour >= *(&(param27.ShopUpdateTime[num79])))
            {
                goto Label_5095;
            }
            num78 = *(&(param27.ShopUpdateTime[num79]));
            goto Label_50AB;
        Label_5095:
            num79 += 1;
        Label_509B:
            if (num79 < ((int) param27.ShopUpdateTime.Length))
            {
                goto Label_5052;
            }
        Label_50AB:
            str17 = &num78.ToString().PadLeft(2, 0x30);
            this.SetTextValue(str17 + ":00");
            return;
        Label_50CF:
            this.ResetToDefault();
            return;
        Label_50D6:
            data26 = MonoSingleton<GameManager>.Instance.Player;
            this.SetTextValue(data26.FollowerNum);
            return;
        Label_50F0:
            item = this.GetSellItem();
            if (item == null)
            {
                goto Label_5118;
            }
            base.get_gameObject().SetActive((item.num == 0) == 0);
            return;
        Label_5118:
            this.ResetToDefault();
            return;
        Label_511F:
            data27 = MonoSingleton<GameManager>.Instance.Player;
            this.SetTextValue(&data27.FriendCap.ToString());
            return;
        Label_5146:
            data28 = MonoSingleton<GameManager>.Instance.Player;
            if (data28.Friends != null)
            {
                goto Label_5168;
            }
            this.ResetToDefault();
        Label_5168:
            this.SetTextValue(data28.mFriendNum);
            return;
        Label_5176:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_51AD;
            }
            str18 = LocalizedText.Get("unit." + data2.UnitParam.iname + "_PROFILE");
            this.SetTextValue(str18);
            return;
        Label_51AD:
            this.ResetToDefault();
            return;
        Label_51B4:
            if (((data2 = this.GetUnitData()) == null) || (string.IsNullOrEmpty(data2.UnitParam.image) != null))
            {
                goto Label_5218;
            }
            loader9 = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            str19 = AssetPath.UnitSkinImage(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str19) != null)
            {
                goto Label_5218;
            }
            loader9.ResourcePath = str19;
            return;
        Label_5218:
            this.ResetToDefault();
            return;
        Label_521F:
            if (((data2 = this.GetUnitData()) == null) || (string.IsNullOrEmpty(data2.UnitParam.image) != null))
            {
                goto Label_5283;
            }
            loader10 = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            str20 = AssetPath.UnitSkinImage2(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str20) != null)
            {
                goto Label_5283;
            }
            loader10.ResourcePath = str20;
            return;
        Label_5283:
            this.ResetToDefault();
            return;
        Label_528A:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_537C;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_536B;
            }
            num80 = data2.AwakeLv;
            num81 = data2.GetAwakeLevelCap();
            if (this.ParameterType != 0x1d4)
            {
                goto Label_52ED;
            }
            num81 = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(data2.GetRarityCap()).UnitAwakeLvCap;
        Label_52ED:
            num82 = 5;
            num83 = num81 / num82;
            if (num83 <= this.Index)
            {
                goto Label_535A;
            }
            num84 = num82;
            num85 = this.Index * num82;
            num86 = (this.Index + 1) * num82;
            if (num86 <= num80)
            {
                goto Label_5341;
            }
            num84 = ((num80 - num85) >= 0) ? (num80 % num82) : 0;
        Label_5341:
            this.SetImageIndex(num84);
            base.get_gameObject().SetActive(1);
            goto Label_5366;
        Label_535A:
            base.get_gameObject().SetActive(0);
        Label_5366:
            goto Label_5377;
        Label_536B:
            this.SetTextValue(data2.AwakeLv);
        Label_5377:
            goto Label_5382;
        Label_537C:
            this.ResetToDefault();
        Label_5382:
            return;
        Label_5383:
            photon4 = PunMonoSingleton<MyPhoton>.Instance;
            data2 = null;
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_53F1;
            }
            param30 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
            if (param30 == null)
            {
                goto Label_54C6;
            }
            param30.SetupUnits();
            if (((int) param30.units.Length) <= 0)
            {
                goto Label_54C6;
            }
            data2 = param30.units[0].unit;
            goto Label_54C6;
        Label_53F1:
            storey2 = new <InternalUpdateValue>c__AnonStorey215();
            storey2.player = photon4.GetMyPlayer();
            if (this.InstanceType != null)
            {
                goto Label_546C;
            }
            param31 = JSON_MyPhotonPlayerParam.Parse(storey2.player.json);
            if (((param31.units == null) || (((int) param31.units.Length) <= 0)) || (param31.units[0].unit == null))
            {
                goto Label_54C6;
            }
            data2 = param31.units[0].unit;
            goto Label_54C6;
        Label_546C:
            param32 = photon4.GetMyPlayersStarted().Find(new Predicate<JSON_MyPhotonPlayerParam>(storey2.<>m__105));
            if (((param32.units == null) || (((int) param32.units.Length) <= 0)) || (param32.units[0].unit == null))
            {
                goto Label_54C6;
            }
            data2 = param32.units[0].unit;
        Label_54C6:
            if ((data2 == null) || (string.IsNullOrEmpty(data2.UnitParam.image) != null))
            {
                goto Label_5523;
            }
            loader11 = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            str21 = AssetPath.UnitSkinImage(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str21) != null)
            {
                goto Label_5523;
            }
            loader11.ResourcePath = str21;
            return;
        Label_5523:
            this.ResetToDefault();
            goto Label_EDD9;
        Label_552E:
            data2 = null;
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_55BC;
            }
            param33 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
            if (this.InstanceType >= 5)
            {
                goto Label_5570;
            }
            num87 = 0;
            num88 = this.InstanceType;
            goto Label_557D;
        Label_5570:
            num87 = 1;
            num88 = this.InstanceType - 5;
        Label_557D:
            param34 = param33.players[num87];
            if (param34 == null)
            {
                goto Label_56AB;
            }
            param34.SetupUnits();
            if (((int) param34.units.Length) <= num88)
            {
                goto Label_56AB;
            }
            data2 = param34.units[num88].unit;
            goto Label_56AB;
        Label_55BC:
            storey3 = new <InternalUpdateValue>c__AnonStorey216();
            photon5 = PunMonoSingleton<MyPhoton>.Instance;
            storey3.player = photon5.GetMyPlayer();
            elemArray = null;
            num89 = -1;
            if (this.InstanceType >= 5)
            {
                goto Label_5634;
            }
            num89 = this.InstanceType;
            param35 = JSON_MyPhotonPlayerParam.Parse(storey3.player.json);
            if ((param35.units == null) || (((int) param35.units.Length) <= 0))
            {
                goto Label_567F;
            }
            elemArray = param35.units;
            goto Label_567F;
        Label_5634:
            num89 = this.InstanceType - 5;
            param36 = photon5.GetMyPlayersStarted().Find(new Predicate<JSON_MyPhotonPlayerParam>(storey3.<>m__106));
            if ((param36.units == null) || (((int) param36.units.Length) <= 0))
            {
                goto Label_567F;
            }
            elemArray = param36.units;
        Label_567F:
            if (((elemArray == null) || (num89 >= ((int) elemArray.Length))) || (elemArray[num89].unit == null))
            {
                goto Label_56AB;
            }
            data2 = elemArray[num89].unit;
        Label_56AB:
            if (this.ParameterType != 0x837)
            {
                goto Label_5700;
            }
            if (data2 == null)
            {
                goto Label_56EF;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data2);
            icon = base.get_gameObject().GetComponent<UnitIcon>();
            if ((icon != null) == null)
            {
                goto Label_56EE;
            }
            icon.UpdateValue();
        Label_56EE:
            return;
        Label_56EF:
            base.get_gameObject().SetActive(0);
            goto Label_5773;
        Label_5700:
            if (this.ParameterType != 0x839)
            {
                goto Label_EDD9;
            }
            if ((data2 == null) || (string.IsNullOrEmpty(data2.UnitParam.image) != null))
            {
                goto Label_576D;
            }
            loader12 = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            str22 = AssetPath.UnitSkinImage(data2.UnitParam, data2.GetSelectedSkin(-1), data2.CurrentJob.JobID);
            if (string.IsNullOrEmpty(str22) != null)
            {
                goto Label_576D;
            }
            loader12.ResourcePath = str22;
            return;
        Label_576D:
            this.ResetToDefault();
        Label_5773:
            goto Label_EDD9;
        Label_5778:
            num90 = 0;
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_5842;
            }
            param37 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
            if (this.InstanceType != null)
            {
                goto Label_57B2;
            }
            num91 = 0;
            goto Label_57B5;
        Label_57B2:
            num91 = 1;
        Label_57B5:
            param38 = param37.players[num91];
            if (param38 == null)
            {
                goto Label_597F;
            }
            param38.SetupUnits();
            num92 = 0;
            goto Label_582D;
        Label_57D7:
            num90 += param38.units[num92].unit.Status.param.atk;
            num90 += param38.units[num92].unit.Status.param.mag;
            num92 += 1;
        Label_582D:
            if (num92 < ((int) param38.units.Length))
            {
                goto Label_57D7;
            }
            goto Label_597F;
        Label_5842:
            storey4 = new <InternalUpdateValue>c__AnonStorey217();
            photon6 = PunMonoSingleton<MyPhoton>.Instance;
            storey4.player = photon6.GetMyPlayer();
            elemArray2 = null;
            if (this.InstanceType != null)
            {
                goto Label_58BA;
            }
            param39 = JSON_MyPhotonPlayerParam.Parse(storey4.player.json);
            if ((param39.units == null) || (((int) param39.units.Length) <= 0))
            {
                goto Label_5905;
            }
            elemArray2 = param39.units;
            goto Label_5905;
        Label_58BA:
            param40 = photon6.GetMyPlayersStarted().Find(new Predicate<JSON_MyPhotonPlayerParam>(storey4.<>m__107));
            if ((param40.units == null) || (((int) param40.units.Length) <= 0))
            {
                goto Label_5905;
            }
            elemArray2 = param40.units;
        Label_5905:
            if (elemArray2 == null)
            {
                goto Label_597F;
            }
            num93 = 0;
            goto Label_5970;
        Label_5918:
            num90 += elemArray2[num93].unit.Status.param.atk;
            num90 += elemArray2[num93].unit.Status.param.mag;
            num93 += 1;
        Label_5970:
            if (num93 < ((int) elemArray2.Length))
            {
                goto Label_5918;
            }
        Label_597F:
            this.SetTextValue(num90);
            goto Label_EDD9;
        Label_598C:
            if (this.InstanceType != null)
            {
                goto Label_59AC;
            }
            base.get_gameObject().SetActive(VersusDraftList.VersusDraftTurnOwn);
            goto Label_59BF;
        Label_59AC:
            base.get_gameObject().SetActive(VersusDraftList.VersusDraftTurnOwn == 0);
        Label_59BF:
            goto Label_EDD9;
        Label_59C4:
            param41 = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param41 == null)
            {
                goto Label_59ED;
            }
            this.SetTextValue(param41.Name);
            return;
        Label_59ED:
            this.ResetToDefault();
            return;
        Label_59F4:
            battle = SceneBattle.Instance;
            if ((((battle == null) == null) && (battle.Battle != null)) && (battle.Battle.CurrentUnit != null))
            {
                goto Label_5A34;
            }
            this.ResetToDefault();
            return;
        Label_5A34:
            this.SetTextValue(battle.Battle.CurrentUnit.OwnerPlayerIndex);
            return;
        Label_5A4E:
            battle2 = SceneBattle.Instance;
            if ((battle2 == null) == null)
            {
                goto Label_5A6D;
            }
            this.ResetToDefault();
            return;
        Label_5A6D:
            this.SetTextValue(battle2.GetNextMyTurn());
            return;
        Label_5A7D:
            battle3 = SceneBattle.Instance;
            if ((battle3 == null) == null)
            {
                goto Label_5A9C;
            }
            this.ResetToDefault();
            return;
        Label_5A9C:
            this.SetTextValue(battle3.DisplayMultiPlayInputTimeLimit);
            return;
        Label_5AAC:
            param42 = this.GetRoomParam();
            if (param42 != null)
            {
                goto Label_5ACC;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_5ACC:
            flag5 = MultiPlayAPIRoom.IsLocked(param42.passCode);
            flag6 = PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer();
            if (this.Index != null)
            {
                goto Label_5B0B;
            }
            base.get_gameObject().SetActive(flag5);
            goto Label_5B78;
        Label_5B0B:
            if (this.Index != 1)
            {
                goto Label_5B2E;
            }
            base.get_gameObject().SetActive(flag5 == 0);
            goto Label_5B78;
        Label_5B2E:
            if (this.Index != 2)
            {
                goto Label_5B5A;
            }
            base.get_gameObject().SetActive((flag6 == null) ? 0 : flag5);
            goto Label_5B78;
        Label_5B5A:
            base.get_gameObject().SetActive((flag6 == null) ? 0 : (flag5 == 0));
        Label_5B78:
            return;
        Label_5B79:
            room2 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            param43 = (room2 != null) ? JSON_MyPhotonRoomParam.Parse(room2.json) : null;
            if (param43 != null)
            {
                goto Label_5BC0;
            }
            this.ResetToDefault();
            return;
        Label_5BC0:
            this.SetTextValue(param43.comment);
            return;
        Label_5BD0:
            param44 = this.GetRoomParam();
            if (param44 != null)
            {
                goto Label_5BEA;
            }
            this.ResetToDefault();
            return;
        Label_5BEA:
            param45 = MonoSingleton<GameManager>.Instance.FindQuest(param44.iname);
            this.SetTextValue(param45.name);
            return;
        Label_5C11:
            room3 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            param46 = (room3 != null) ? JSON_MyPhotonRoomParam.Parse(room3.json) : null;
            if ((param46 != null) && (MultiPlayAPIRoom.IsLocked(param46.passCode) != null))
            {
                goto Label_5C6B;
            }
            this.ResetToDefault();
            return;
        Label_5C6B:
            str23 = string.Format("{0:D5}", (int) param46.roomid);
            if (this.Index != 1)
            {
                goto Label_5CC2;
            }
            if (<>f__am$cache14 != null)
            {
                goto Label_5CB4;
            }
            <>f__am$cache14 = new MatchEvaluator(GameParameter.<InternalUpdateValue>m__108);
        Label_5CB4:
            str23 = Regex.Replace(str23, "[0-9]", <>f__am$cache14);
        Label_5CC2:
            this.SetTextValue(str23);
            return;
        Label_5CCD:
            storey5 = new <InternalUpdateValue>c__AnonStorey218();
            storey5.bs = SceneBattle.Instance;
            if ((((storey5.bs == null) == null) && (storey5.bs.Battle != null)) && (storey5.bs.Battle.CurrentUnit != null))
            {
                goto Label_5D2A;
            }
            this.ResetToDefault();
            return;
        Label_5D2A:
            list7 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            param47 = (list7 != null) ? list7.Find(new Predicate<JSON_MyPhotonPlayerParam>(storey5.<>m__109)) : null;
            if (param47 != null)
            {
                goto Label_5D7F;
            }
            this.ResetToDefault();
            goto Label_5D8E;
        Label_5D7F:
            this.SetTextValue(param47.playerName);
        Label_5D8E:
            return;
        Label_5D8F:
            flag7 = 0;
            if (GameUtility.GetCurrentScene() != 4)
            {
                goto Label_5E1D;
            }
            param48 = this.GetRoomParam();
            if ((param48 == null) || (string.IsNullOrEmpty(param48.iname) == null))
            {
                goto Label_5DCA;
            }
            goto Label_5E1D;
        Label_5DCA:
            param4 = this.GetRoomPlayerParam();
            num94 = (param4 != null) ? param4.playerIndex : 0;
            num95 = (param48 != null) ? param48.GetUnitSlotNum(num94) : 0;
            flag7 = (this.Index < num95) == 0;
        Label_5E1D:
            base.get_gameObject().SetActive(flag7);
            return;
        Label_5E2D:
            flag8 = 0;
            if (GameUtility.GetCurrentScene() != 4)
            {
                goto Label_5E97;
            }
            param49 = this.GetRoomParam();
            param4 = this.GetRoomPlayerParam();
            num96 = (param4 != null) ? param4.playerIndex : 0;
            num97 = (param49 != null) ? param49.GetUnitSlotNum(num96) : 0;
            flag8 = this.Index < num97;
        Label_5E97:
            base.get_gameObject().SetActive(flag8);
            return;
        Label_5EA7:
            flag9 = 0;
            if (GameUtility.GetCurrentScene() != 4)
            {
                goto Label_5EEB;
            }
            elem8 = this.GetMultiPlayerUnitData(this.Index);
            data2 = (elem8 != null) ? elem8.unit : null;
            flag9 = (data2 == null) == 0;
        Label_5EEB:
            base.get_gameObject().SetActive(flag9);
            return;
        Label_5EFB:
            flag10 = 0;
            if (GameUtility.GetCurrentScene() != 4)
            {
                goto Label_5F51;
            }
            param50 = this.GetRoomParam();
            param4 = this.GetRoomPlayerParam();
            num98 = (param4 != null) ? param4.playerIndex : 0;
            flag10 = (this.Index < param50.GetUnitSlotNum(num98)) == 0;
        Label_5F51:
            button2 = base.get_gameObject().GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_5F7F;
            }
            button2.set_interactable(flag10 == 0);
        Label_5F7F:
            return;
        Label_5F80:
            scene = GameUtility.GetCurrentScene();
            flag11 = (scene == 4) ? 1 : (scene == 2);
            if (this.Index != null)
            {
                goto Label_5FC3;
            }
            base.get_gameObject().SetActive(flag11 == 0);
            goto Label_6046;
        Label_5FC3:
            if (this.Index != 1)
            {
                goto Label_5FE3;
            }
            base.get_gameObject().SetActive(flag11);
            goto Label_6046;
        Label_5FE3:
            button3 = base.get_gameObject().GetComponent<Button>();
            if ((button3 == null) == null)
            {
                goto Label_600C;
            }
            this.ResetToDefault();
            goto Label_6046;
        Label_600C:
            if (this.Index != 2)
            {
                goto Label_602D;
            }
            button3.set_interactable(flag11 == 0);
            goto Label_6046;
        Label_602D:
            if (this.Index != 3)
            {
                goto Label_EDD9;
            }
            button3.set_interactable(flag11);
        Label_6046:
            goto Label_EDD9;
        Label_604B:
            str24 = LocalizedText.Get("sys.MP_REST_REWARD");
            if (string.IsNullOrEmpty(str24) == null)
            {
                goto Label_6072;
            }
            this.ResetToDefault();
            goto Label_60C5;
        Label_6072:
            data29 = MonoSingleton<GameManager>.Instance.Player;
            num99 = data29.ChallengeMultiMax - data29.ChallengeMultiNum;
            num99 = Math.Max(num99, 0);
            str24 = string.Format(str24, (int) num99);
            this.SetTextValue(str24);
        Label_60C5:
            return;
        Label_60C6:
            scene2 = GameUtility.GetCurrentScene();
            if (((scene2 == 4) ? 1 : (scene2 == 2)) == null)
            {
                goto Label_61DE;
            }
            data30 = MonoSingleton<GameManager>.Instance.Player;
            num100 = data30.ChallengeMultiMax - data30.ChallengeMultiNum;
            if (this.Index != null)
            {
                goto Label_613A;
            }
            base.get_gameObject().SetActive((num100 > 0) == 0);
            goto Label_61DE;
        Label_613A:
            if (this.Index != 1)
            {
                goto Label_615D;
            }
            base.get_gameObject().SetActive(num100 > 0);
            goto Label_61DE;
        Label_615D:
            if (this.Index != 2)
            {
                goto Label_6183;
            }
            base.get_gameObject().SetActive((num100 < 0) == 0);
            goto Label_61DE;
        Label_6183:
            if (this.Index != 3)
            {
                goto Label_61A6;
            }
            base.get_gameObject().SetActive(num100 < 0);
            goto Label_61DE;
        Label_61A6:
            if (this.Index != 4)
            {
                goto Label_61C9;
            }
            base.get_gameObject().SetActive(num100 == 0);
            goto Label_61DE;
        Label_61C9:
            base.get_gameObject().SetActive((num100 == 0) == 0);
        Label_61DE:
            return;
        Label_61DF:
            battle4 = SceneBattle.Instance;
            if ((battle4 == null) == null)
            {
                goto Label_61FE;
            }
            this.ResetToDefault();
            return;
        Label_61FE:
            num101 = battle4.GetNextMyTurn();
            if (this.Index != null)
            {
                goto Label_622D;
            }
            base.get_gameObject().SetActive(num101 < 0);
            goto Label_6242;
        Label_622D:
            base.get_gameObject().SetActive((num101 < 0) == 0);
        Label_6242:
            return;
        Label_6243:
            if (this.InstanceType != null)
            {
                goto Label_627C;
            }
            data31 = DataSource.FindDataOfClass<TrophyObjectiveData>(base.get_gameObject(), null);
            if (data31 == null)
            {
                goto Label_62D3;
            }
            this.SetTextValue(data31.Description);
            return;
            goto Label_62D3;
        Label_627C:
            param51 = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (((param51 == null) || (0 > this.Index)) || (this.Index >= ((int) param51.Objectives.Length)))
            {
                goto Label_62D3;
            }
            this.SetTextValue(SRPG_Extensions.GetDescription(param51.Objectives[this.Index]));
            return;
        Label_62D3:
            this.ResetToDefault();
            goto Label_EDD9;
        Label_62DE:
            if (this.InstanceType != null)
            {
                goto Label_63CD;
            }
            data32 = DataSource.FindDataOfClass<TrophyObjectiveData>(base.get_gameObject(), null);
            if (data32 == null)
            {
                goto Label_6556;
            }
            if ((data32.Objective.type != 0x57) && (data32.Objective.type != 0x58))
            {
                goto Label_638F;
            }
            num102 = Mathf.Min(((float) data32.Count) / 100f, ((float) data32.CountMax) / 100f);
            str25 = string.Format("{0:0.0}", (float) num102);
            this.SetTextValue(str25);
            this.SetSliderValue((int) num102, data32.CountMax);
            goto Label_63C7;
        Label_638F:
            num103 = Mathf.Min(data32.Count, data32.CountMax);
            this.SetTextValue(num103);
            this.SetSliderValue(num103, data32.CountMax);
        Label_63C7:
            return;
            goto Label_6556;
        Label_63CD:
            param52 = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (((param52 == null) || (0 > this.Index)) || (this.Index >= ((int) param52.Objectives.Length)))
            {
                goto Label_6556;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(param52, 0);
            if ((state == null) || (this.Index >= ((int) state.Count.Length)))
            {
                goto Label_6555;
            }
            if ((param52.Objectives[this.Index].type != 0x57) && (param52.Objectives[this.Index].type != 0x58))
            {
                goto Label_64FE;
            }
            num104 = Mathf.Min(((float) state.Count[this.Index]) / 100f, ((float) param52.Objectives[this.Index].RequiredCount) / 100f);
            str26 = string.Format("{0:0.0}", (float) num104);
            this.SetTextValue(str26);
            this.SetSliderValue((int) num104, param52.Objectives[this.Index].RequiredCount / 100);
            goto Label_6555;
        Label_64FE:
            num105 = Mathf.Min(state.Count[this.Index], param52.Objectives[this.Index].RequiredCount);
            this.SetTextValue(num105);
            this.SetSliderValue(num105, param52.Objectives[this.Index].RequiredCount);
        Label_6555:
            return;
        Label_6556:
            this.ResetToDefault();
            goto Label_EDD9;
        Label_6561:
            if (this.InstanceType != null)
            {
                goto Label_65FE;
            }
            data33 = DataSource.FindDataOfClass<TrophyObjectiveData>(base.get_gameObject(), null);
            if (data33 == null)
            {
                goto Label_66D3;
            }
            if ((data33.Objective.type != 0x57) && (data33.Objective.type != 0x58))
            {
                goto Label_65E9;
            }
            num106 = ((float) data33.CountMax) / 100f;
            str27 = string.Format("{0:0.0}", (float) num106);
            this.SetTextValue(str27);
            goto Label_65F8;
        Label_65E9:
            this.SetTextValue(data33.CountMax);
        Label_65F8:
            return;
            goto Label_66D3;
        Label_65FE:
            param53 = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (((param53 == null) || (0 > this.Index)) || (this.Index >= ((int) param53.Objectives.Length)))
            {
                goto Label_66D3;
            }
            if ((param53.Objectives[this.Index].type != 0x57) && (param53.Objectives[this.Index].type != 0x58))
            {
                goto Label_66B7;
            }
            num107 = ((float) param53.Objectives[this.Index].ival) / 100f;
            str28 = string.Format("{0:0.0}", (float) num107);
            this.SetTextValue(str28);
            goto Label_66D2;
        Label_66B7:
            this.SetTextValue(param53.Objectives[this.Index].RequiredCount);
        Label_66D2:
            return;
        Label_66D3:
            this.ResetToDefault();
            goto Label_EDD9;
        Label_66DE:
            param2 = this.GetItemParam();
            if (param2 == null)
            {
                goto Label_66FB;
            }
            this.SetTextValue(param2.enhace_point);
            return;
        Label_66FB:
            this.ResetToDefault();
            return;
        Label_6702:
            material = this.GetEnhanceMaterial();
            if (material == null)
            {
                goto Label_671F;
            }
            this.SetTextValue(material.num);
            return;
        Label_671F:
            this.ResetToDefault();
            return;
        Label_6726:
            param2 = (this.InstanceType == 1) ? this.GetInventoryItemParam() : this.GetItemParam();
            if (param2 == null)
            {
                goto Label_6779;
            }
            num108 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(param2.iname);
            base.get_gameObject().SetActive(num108 > 0);
            return;
        Label_6779:
            base.get_gameObject().SetActive(0);
            return;
        Label_6786:
            parameter2 = this.GetEquipItemParameter();
            if (parameter2 == null)
            {
                goto Label_67E2;
            }
            effect = parameter2.equip.Skill.GetBuffEffect(0);
            num109 = parameter2.param_index;
            str29 = this.GetParamTypeName(effect.targets[num109].paramType);
            this.SetTextValue(str29);
            return;
        Label_67E2:
            this.ResetToDefault();
            return;
        Label_67E9:
            parameter2 = this.GetEquipItemParameter();
            if (parameter2 == null)
            {
                goto Label_6877;
            }
            num110 = parameter2.param_index;
            data34 = parameter2.equip.Skill;
            if (data34 == null)
            {
                goto Label_6877;
            }
            effect2 = data34.GetBuffEffect(0);
            if (((effect2 == null) || (effect2.param == null)) || (effect2.param.buffs == null))
            {
                goto Label_6877;
            }
            this.SetTextValue(effect2.targets[num110].value);
            return;
        Label_6877:
            this.ResetToDefault();
            return;
        Label_687E:
            parameter2 = this.GetEquipItemParameter();
            if (parameter2 == null)
            {
                goto Label_6963;
            }
            num111 = parameter2.param_index;
            num112 = 0;
            data35 = parameter2.equip.Skill;
            if (data35 == null)
            {
                goto Label_693B;
            }
            effect3 = data35.GetBuffEffect(0);
            if (((effect3 == null) || (effect3.param == null)) || (effect3.param.buffs == null))
            {
                goto Label_693B;
            }
            num112 = (effect3 == null) ? 0 : (effect3.targets[num111].value - effect3.param.buffs[num111].value_ini);
        Label_693B:
            if (num112 == null)
            {
                goto Label_6963;
            }
            this.SetTextValue("(+" + &num112.ToString() + ")");
            return;
        Label_6963:
            this.ResetToDefault();
            return;
        Label_696A:
            parameter2 = this.GetEquipItemParameter();
            if (parameter2 == null)
            {
                goto Label_6A43;
            }
            num113 = parameter2.param_index;
            num114 = 0;
            data36 = parameter2.equip.Skill;
            if (data36 == null)
            {
                goto Label_6A27;
            }
            effect4 = data36.GetBuffEffect(0);
            if (((effect4 == null) || (effect4.param == null)) || (effect4.param.buffs == null))
            {
                goto Label_6A27;
            }
            num114 = (effect4 == null) ? 0 : (effect4.targets[num113].value - effect4.param.buffs[num113].value_ini);
        Label_6A27:
            if (num114 == null)
            {
                goto Label_6A43;
            }
            base.get_gameObject().SetActive(num114 > 0);
            return;
        Label_6A43:
            base.get_gameObject().SetActive(0);
            return;
        Label_6A50:
            material = this.GetEnhanceMaterial();
            if (material == null)
            {
                goto Label_6A75;
            }
            base.get_gameObject().SetActive(material.num > 0);
            return;
        Label_6A75:
            base.get_gameObject().SetActive(0);
            return;
        Label_6A82:
            material = this.GetEnhanceMaterial();
            if (material == null)
            {
                goto Label_6AA4;
            }
            base.get_gameObject().SetActive(material.selected);
            return;
        Label_6AA4:
            base.get_gameObject().SetActive(0);
            return;
        Label_6AB1:
            if (((data9 = this.GetEnhanceEquipData()) == null) || (data9.is_enhanced == null))
            {
                goto Label_6B7B;
            }
            data37 = data9.equip;
            num115 = data37.Exp + data9.gainexp;
            num116 = data37.CalcRankFromExp(num115);
            num117 = data37.GetRankCap();
            num118 = (num116 >= num117) ? data37.GetNextExp(num117) : data37.GetExpFromExp(num115);
            num119 = data37.GetNextExp((num116 >= num117) ? num117 : (num116 + 1));
            this.SetTextValue(num118);
            this.SetSliderValue(num118, num119);
            return;
        Label_6B7B:
            this.ResetToDefault();
            return;
        Label_6B82:
            if ((data9 = this.GetEnhanceEquipData()) == null)
            {
                goto Label_6C09;
            }
            data38 = data9.equip;
            num120 = data38.Exp + data9.gainexp;
            num121 = data38.CalcRankFromExp(num120);
            num122 = data38.GetRankCap();
            num123 = (num121 >= num122) ? data38.GetNextExp(num122) : data38.GetExpFromExp(num120);
            this.SetTextValue(num123);
            return;
        Label_6C09:
            this.ResetToDefault();
            return;
        Label_6C10:
            if ((data9 = this.GetEnhanceEquipData()) == null)
            {
                goto Label_6C90;
            }
            data39 = data9.equip;
            num124 = data39.Exp + data9.gainexp;
            num125 = data39.CalcRankFromExp(num124);
            num126 = data39.GetRankCap();
            num127 = data39.GetNextExp((num125 >= num126) ? num126 : (num125 + 1));
            this.SetTextValue(num127);
            return;
        Label_6C90:
            this.ResetToDefault();
            return;
        Label_6C97:
            if ((data9 = this.GetEnhanceEquipData()) == null)
            {
                goto Label_6CB8;
            }
            this.SetTextValue(data9.equip.Rank);
            return;
        Label_6CB8:
            this.ResetToDefault();
            return;
        Label_6CBF:
            if ((data9 = this.GetEnhanceEquipData()) == null)
            {
                goto Label_6D01;
            }
            data40 = data9.equip;
            num128 = data40.CalcRankFromExp(data40.Exp + data9.gainexp);
            this.SetTextValue(num128);
            return;
        Label_6D01:
            this.ResetToDefault();
            return;
        Label_6D08:
            if ((data7 = this.GetEquipData()) == null)
            {
                goto Label_6D6A;
            }
            num129 = data7.Rank - 1;
            if (num129 <= 0)
            {
                goto Label_6D5D;
            }
            num130 = num129 - 1;
            base.get_gameObject().SetActive(1);
            this.SetAnimatorInt("rare", num130);
            this.SetImageIndex(num130);
            return;
        Label_6D5D:
            base.get_gameObject().SetActive(0);
            return;
        Label_6D6A:
            this.ResetToDefault();
            return;
        Label_6D71:
            base.get_gameObject().SetActive(this.CheckUnlockInstanceType());
            return;
        Label_6D83:
            battle5 = SceneBattle.Instance;
            if (((battle5 == null) == null) && (battle5.CurrentNotifyDisconnectedPlayer != null))
            {
                goto Label_6DB0;
            }
            this.ResetToDefault();
            return;
        Label_6DB0:
            this.SetTextValue(battle5.CurrentNotifyDisconnectedPlayer.playerIndex);
            return;
        Label_6DC5:
            battle6 = SceneBattle.Instance;
            if (((battle6 == null) == null) && (battle6.CurrentNotifyDisconnectedPlayer != null))
            {
                goto Label_6DF2;
            }
            this.ResetToDefault();
            return;
        Label_6DF2:
            base.get_gameObject().SetActive(battle6.CurrentNotifyDisconnectedPlayerType == this.Index);
            return;
        Label_6E0F:
            storey6 = new <InternalUpdateValue>c__AnonStorey219();
            storey6.bs = SceneBattle.Instance;
            if ((((storey6.bs == null) == null) && (storey6.bs.Battle != null)) && (storey6.bs.Battle.CurrentUnit != null))
            {
                goto Label_6E6C;
            }
            this.ResetToDefault();
            return;
        Label_6E6C:
            manager = MonoSingleton<GameManager>.Instance;
            photon10 = PunMonoSingleton<MyPhoton>.Instance;
            list8 = photon10.GetMyPlayersStarted();
            param54 = (list8 != null) ? list8.Find(new Predicate<JSON_MyPhotonPlayerParam>(storey6.<>m__10A)) : null;
            list9 = photon10.GetRoomPlayerList();
            player4 = (param54 != null) ? photon10.FindPlayer(list9, param54.playerID, param54.playerIndex) : null;
            if ((manager.AudienceMode == null) && (manager.IsVSCpuBattle == null))
            {
                goto Label_6F19;
            }
            base.get_gameObject().SetActive(0);
            goto Label_6F67;
        Label_6F19:
            if (this.Index != null)
            {
                goto Label_6F3B;
            }
            base.get_gameObject().SetActive(player4 == null);
            goto Label_6F67;
        Label_6F3B:
            if (this.Index != 1)
            {
                goto Label_6F61;
            }
            base.get_gameObject().SetActive((player4 == null) == 0);
            goto Label_6F67;
        Label_6F61:
            this.ResetToDefault();
        Label_6F67:
            return;
        Label_6F68:
            storeya = new <InternalUpdateValue>c__AnonStorey21A();
            storeya.bs = SceneBattle.Instance;
            if ((((storeya.bs == null) == null) && (storeya.bs.Battle != null)) && (storeya.bs.Battle.CurrentUnit != null))
            {
                goto Label_6FC5;
            }
            this.ResetToDefault();
            return;
        Label_6FC5:
            photon11 = PunMonoSingleton<MyPhoton>.Instance;
            list10 = photon11.GetMyPlayersStarted();
            param55 = (list10 != null) ? list10.Find(new Predicate<JSON_MyPhotonPlayerParam>(storeya.<>m__10B)) : null;
            base.get_gameObject().SetActive((param55 == null) ? 0 : photon11.IsOldestPlayer(param55.playerID));
            return;
        Label_7030:
            photon12 = PunMonoSingleton<MyPhoton>.Instance;
            if (this.Index != null)
            {
                goto Label_705D;
            }
            base.get_gameObject().SetActive(photon12.IsOldestPlayer());
            goto Label_7074;
        Label_705D:
            base.get_gameObject().SetActive(photon12.IsOldestPlayer() == 0);
        Label_7074:
            return;
        Label_7075:
            photon13 = PunMonoSingleton<MyPhoton>.Instance;
            this.SetTextValue(photon13.GetOldestPlayer());
            return;
        Label_708E:
            _old = DataSource.FindDataOfClass<GachaResultData_old>(base.get_gameObject(), null);
            if (_old == null)
            {
                goto Label_70B6;
            }
            this.SetTextValue(_old.Name);
        Label_70B6:
            return;
        Label_70B7:
            stateArray = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
            flag13 = 0;
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_710D;

                case 1:
                    goto Label_7165;

                case 2:
                    goto Label_7160;

                case 3:
                    goto Label_71D0;

                case 4:
                    goto Label_71D5;

                case 5:
                    goto Label_71DA;

                case 6:
                    goto Label_71DF;
            }
            goto Label_71E4;
        Label_710D:
            num131 = 0;
            goto Label_714C;
        Label_7117:
            if (stateArray[num131].Param.IsShowBadge(stateArray[num131]) == null)
            {
                goto Label_7142;
            }
            flag13 = 1;
            goto Label_715B;
        Label_7142:
            num131 += 1;
        Label_714C:
            if (num131 < ((int) stateArray.Length))
            {
                goto Label_7117;
            }
        Label_715B:
            goto Label_71E4;
        Label_7160:
            goto Label_71E4;
        Label_7165:
            num132 = 0;
            goto Label_71BC;
        Label_716F:
            if ((stateArray[num132].Param.IsDaily != null) || (stateArray[num132].Param.IsShowBadge(stateArray[num132]) == null))
            {
                goto Label_71B2;
            }
            flag13 = 1;
            goto Label_71CB;
        Label_71B2:
            num132 += 1;
        Label_71BC:
            if (num132 < ((int) stateArray.Length))
            {
                goto Label_716F;
            }
        Label_71CB:
            goto Label_71E4;
        Label_71D0:
            goto Label_71E4;
        Label_71D5:
            goto Label_71E4;
        Label_71DA:
            goto Label_71E4;
        Label_71DF:;
        Label_71E4:
            base.get_gameObject().SetActive(flag13);
            return;
        Label_71F4:
            param56 = this.GetTrophyParam();
            if (param56 == null)
            {
                goto Label_722D;
            }
            this.SetTextValue(param56.Coin);
            base.get_gameObject().SetActive(param56.Coin > 0);
        Label_722D:
            return;
        Label_722E:
            param57 = this.GetTrophyParam();
            if (param57 == null)
            {
                goto Label_7267;
            }
            this.SetTextValue(param57.Gold);
            base.get_gameObject().SetActive(param57.Gold > 0);
        Label_7267:
            return;
        Label_7268:
            param58 = this.GetTrophyParam();
            if (param58 == null)
            {
                goto Label_72A1;
            }
            this.SetTextValue(param58.Exp);
            base.get_gameObject().SetActive(param58.Exp > 0);
        Label_72A1:
            return;
        Label_72A2:
            param59 = this.GetTrophyParam();
            if (param59 == null)
            {
                goto Label_72DB;
            }
            this.SetTextValue(param59.Stamina);
            base.get_gameObject().SetActive(param59.Stamina > 0);
        Label_72DB:
            return;
        Label_72DC:
            data42 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data42 == null)
            {
                goto Label_7305;
            }
            this.SetTextValue(data42.Coin);
            return;
        Label_7305:
            this.ResetToDefault();
            return;
        Label_730C:
            data43 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data43 == null)
            {
                goto Label_7335;
            }
            this.SetTextValue(data43.Gold);
            return;
        Label_7335:
            this.ResetToDefault();
            return;
        Label_733C:
            data44 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data44 == null)
            {
                goto Label_7365;
            }
            this.SetTextValue(data44.Exp);
            return;
        Label_7365:
            this.ResetToDefault();
            return;
        Label_736C:
            data45 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data45 == null)
            {
                goto Label_7395;
            }
            this.SetTextValue(data45.Stamina);
            return;
        Label_7395:
            this.ResetToDefault();
            return;
        Label_739C:
            data2 = this.GetUnitData();
            if (data2 == null)
            {
                goto Label_73BB;
            }
            base.get_gameObject().SetActive(data2.IsFavorite);
            return;
        Label_73BB:
            this.ResetToDefault();
            return;
        Label_73C2:
            data7 = this.GetEquipData();
            if ((data7 == null) || (data7.IsEquiped() == null))
            {
                goto Label_73EB;
            }
            this.SetEquipItemFrame(data7.ItemParam);
            return;
        Label_73EB:
            this.SetEquipItemFrame(null);
            return;
        Label_73F3:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_7412;
            }
            this.SetImageIndex(data2.CurrentJob.Rank);
            return;
        Label_7412:
            this.ResetToDefault();
            return;
        Label_7419:
            asset = Resources.Load<TextAsset>("revision");
            if ((asset != null) == null)
            {
                goto Label_7446;
            }
            this.SetTextValue(asset.get_text());
            return;
        Label_7446:
            this.ResetToDefault();
            return;
        Label_744D:
            asset2 = Resources.Load<TextAsset>("build");
            if ((asset2 != null) == null)
            {
                goto Label_747A;
            }
            this.SetTextValue(asset2.get_text());
            return;
        Label_747A:
            this.ResetToDefault();
            return;
        Label_7481:
            num133 = AssetManager.AssetRevision;
            if (num133 <= 0)
            {
                goto Label_74A4;
            }
            this.SetTextValue(&num133.ToString());
            return;
        Label_74A4:
            this.ResetToDefault();
            return;
        Label_74AB:
            product = DataSource.FindDataOfClass<PaymentManager.Product>(base.get_gameObject(), null);
            if ((product != null) && (string.IsNullOrEmpty(product.name) == null))
            {
                goto Label_74DE;
            }
            this.ResetToDefault();
            return;
        Label_74DE:
            this.SetTextValue(product.name);
            return;
        Label_74EE:
            product2 = DataSource.FindDataOfClass<PaymentManager.Product>(base.get_gameObject(), null);
            if ((product2 != null) && (string.IsNullOrEmpty(product2.desc) == null))
            {
                goto Label_7521;
            }
            this.ResetToDefault();
            return;
        Label_7521:
            str30 = null;
            if (this.Index != null)
            {
                goto Label_7543;
            }
            str30 = product2.desc;
            goto Label_75B1;
        Label_7543:
            chArray1 = new char[] { 0x7c };
            strArray = product2.desc.Split(chArray1);
            num134 = (this.Index >= 0) ? this.Index : -this.Index;
            str30 = ((strArray != null) && ((num134 - 1) < ((int) strArray.Length))) ? strArray[num134 - 1] : null;
        Label_75B1:
            if (this.Index < 0)
            {
                goto Label_75DF;
            }
            this.SetTextValue((str30 != null) ? str30 : string.Empty);
            goto Label_75F4;
        Label_75DF:
            base.get_gameObject().SetActive((str30 == null) == 0);
        Label_75F4:
            return;
        Label_75F5:
            product3 = DataSource.FindDataOfClass<PaymentManager.Product>(base.get_gameObject(), null);
            if ((product3 != null) && (string.IsNullOrEmpty(product3.price) == null))
            {
                goto Label_7628;
            }
            this.ResetToDefault();
            return;
        Label_7628:
            this.SetTextValue(product3.price);
            return;
        Label_7638:
            product4 = DataSource.FindDataOfClass<PaymentManager.Product>(base.get_gameObject(), null);
            if (product4 != null)
            {
                goto Label_7658;
            }
            this.ResetToDefault();
            return;
        Label_7658:
            str31 = string.Empty;
            param60 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (product4.productID.Contains(param60.VipCardProduct) == null)
            {
                goto Label_773C;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckEnableVipCard() == null)
            {
                goto Label_7779;
            }
            time7 = MonoSingleton<GameManager>.Instance.Player.VipExpiredAt;
            span3 = time7 - TimeManager.FromUnixTime(Network.GetServerTime());
            if (0 >= &span3.Days)
            {
                goto Label_7708;
            }
            str31 = string.Format(LocalizedText.Get("sys.VIP_REMAIN_D"), (int) &span3.Days);
            goto Label_7737;
        Label_7708:
            str31 = string.Format(LocalizedText.Get("sys.VIP_EXPIRE_HHMM"), (int) &time7.Hour, (int) &time7.Minute);
        Label_7737:
            goto Label_7779;
        Label_773C:
            num135 = product4.numPaid + product4.numFree;
            if (0 >= num135)
            {
                goto Label_7779;
            }
            str31 = string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num135);
        Label_7779:
            if (this.Index != -1)
            {
                goto Label_77A1;
            }
            base.get_gameObject().SetActive(string.IsNullOrEmpty(str31) == 0);
            goto Label_77AB;
        Label_77A1:
            this.SetTextValue(str31);
        Label_77AB:
            return;
        Label_77AC:
            player5 = this.GetArenaPlayer();
            if ((player5 == null) || (player5.ArenaRank <= 0))
            {
                goto Label_77DE;
            }
            this.SetTextValue(player5.ArenaRank);
            return;
        Label_77DE:
            this.ResetToDefault();
            return;
        Label_77E5:
            player6 = this.GetArenaPlayer();
            if (player6 == null)
            {
                goto Label_7808;
            }
            this.SetTextValue(player6.TotalAtk);
            return;
        Label_7808:
            this.ResetToDefault();
            return;
        Label_780F:
            player7 = this.GetArenaPlayer();
            if (((player7 == null) || (player7.Unit[0] == null)) || (player7.Unit[0].LeaderSkill == null))
            {
                goto Label_7863;
            }
            this.SetTextValue(player7.Unit[0].LeaderSkill.Name);
            return;
        Label_7863:
            this.ResetToDefault();
            return;
        Label_786A:
            player8 = this.GetArenaPlayer();
            if (((player8 == null) || (player8.Unit[0] == null)) || (player8.Unit[0].LeaderSkill == null))
            {
                goto Label_78C3;
            }
            this.SetTextValue(player8.Unit[0].LeaderSkill.SkillParam.expr);
            return;
        Label_78C3:
            this.ResetToDefault();
            return;
        Label_78CA:
            player9 = this.GetArenaPlayer();
            if (player9 == null)
            {
                goto Label_78ED;
            }
            this.SetTextValue(player9.PlayerName);
            return;
        Label_78ED:
            this.ResetToDefault();
            return;
        Label_78F4:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.ArenaRank <= 0)
            {
                goto Label_7921;
            }
            this.SetTextValue(manager.Player.ArenaRank);
            goto Label_7927;
        Label_7921:
            this.ResetToDefault();
        Label_7927:
            return;
        Label_7928:
            if (((param = this.GetQuestParam()) == null) || (string.IsNullOrEmpty(param.ticket) != null))
            {
                goto Label_7977;
            }
            data46 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.ticket);
            if (data46 == null)
            {
                goto Label_7977;
            }
            this.SetTextValue(data46.Num);
            return;
        Label_7977:
            this.ResetToDefault();
            return;
        Label_797E:
            if ((((param = this.GetQuestParam()) == null) || (param.state != 2)) || (string.IsNullOrEmpty(param.ticket) != null))
            {
                goto Label_79E1;
            }
            data47 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.ticket);
            if (data47 == null)
            {
                goto Label_79E1;
            }
            base.get_gameObject().SetActive(data47.Num > 0);
            return;
        Label_79E1:
            base.get_gameObject().SetActive(0);
            return;
        Label_79EE:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.ChallengeArenaNum);
            this.SetSliderValue(manager.Player.ChallengeArenaNum, manager.Player.ChallengeArenaMax);
            return;
        Label_7A22:
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.UpdateChallengeArenaTimer();
            num136 = manager.Player.GetNextChallengeArenaCoolDownSec();
            num137 = num136 / 60L;
            num138 = num136 % 60L;
            str32 = LocalizedText.Get("sys.ARENA_COOLDOWN");
            this.SetTextValue(string.Format(str32, (long) num137, (long) num138));
            this.SetUpdateInterval(0.25f);
            return;
        Label_7A95:
            param61 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param61.ShopUpdateTime == null)
            {
                goto Label_7B33;
            }
            time8 = TimeManager.ServerTime;
            num139 = 0;
            goto Label_7B0E;
        Label_7AC9:
            if (&time8.Hour >= *(&(param61.ShopUpdateTime[num139])))
            {
                goto Label_7B04;
            }
            this.SetTextValue(LocalizedText.Get("sys.TODAY"));
            return;
        Label_7B04:
            num139 += 1;
        Label_7B0E:
            if (num139 < ((int) param61.ShopUpdateTime.Length))
            {
                goto Label_7AC9;
            }
            this.SetTextValue(LocalizedText.Get("sys.TOMORROW"));
            return;
        Label_7B33:
            this.ResetToDefault();
            return;
        Label_7B3A:
            player10 = this.GetArenaPlayer();
            if (player10 == null)
            {
                goto Label_7B5D;
            }
            this.SetTextValue(player10.PlayerLevel);
            return;
        Label_7B5D:
            this.ResetToDefault();
            return;
        Label_7B64:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.VipRank);
            return;
        Label_7B7C:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_7D3B;
            }
            events = base.get_gameObject().GetComponent<UnitEquipmentSlotEvents>();
            if ((events != null) == null)
            {
                goto Label_7D3B;
            }
            num140 = this.Index;
            dataArray2 = data2.GetRankupEquips(data2.JobIndex);
            if (dataArray2[num140].IsValid() != null)
            {
                goto Label_7BE4;
            }
            events.get_gameObject().SetActive(0);
            return;
        Label_7BE4:
            events.get_gameObject().SetActive(1);
            param62 = dataArray2[num140].ItemParam;
            DataSource.Bind<ItemParam>(events.get_gameObject(), param62);
            DataSource.Bind<EquipData>(events.get_gameObject(), dataArray2[num140]);
            if (dataArray2[num140].IsEquiped() == null)
            {
                goto Label_7C50;
            }
            events.StateType = 3;
            goto Label_7D3B;
        Label_7C50:
            if (MonoSingleton<GameManager>.Instance.Player.HasItem(param62.iname) == null)
            {
                goto Label_7C9F;
            }
            if (param62.equipLv <= data2.Lv)
            {
                goto Label_7C90;
            }
            events.StateType = 2;
            goto Label_7C9A;
        Label_7C90:
            events.StateType = 1;
        Label_7C9A:
            goto Label_7D3B;
        Label_7C9F:
            if (MonoSingleton<GameManager>.Instance.Player.CheckCreateItem(param62) != 1)
            {
                goto Label_7CEA;
            }
            if (param62.equipLv <= data2.Lv)
            {
                goto Label_7CDB;
            }
            events.StateType = 5;
            goto Label_7CE5;
        Label_7CDB:
            events.StateType = 4;
        Label_7CE5:
            goto Label_7D3B;
        Label_7CEA:
            if (data2.CheckCommon(data2.JobIndex, num140) == null)
            {
                goto Label_7D31;
            }
            if (param62.equipLv <= data2.Lv)
            {
                goto Label_7D22;
            }
            events.StateType = 8;
            goto Label_7D2C;
        Label_7D22:
            events.StateType = 7;
        Label_7D2C:
            goto Label_7D3B;
        Label_7D31:
            events.StateType = 0;
        Label_7D3B:
            return;
        Label_7D3C:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7D88;
            }
            str33 = AssetPath.UnitIconSmall(param5, param5.GetJobId(0));
            if (string.IsNullOrEmpty(str33) != null)
            {
                goto Label_7D88;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str33;
            return;
        Label_7D88:
            this.ResetToDefault();
            return;
        Label_7D8F:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7DBD;
            }
            this.SetAnimatorInt("rare", param5.rare);
            this.SetImageIndex(param5.rare);
            return;
        Label_7DBD:
            this.ResetToDefault();
            return;
        Label_7DC4:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7E08;
            }
            str34 = GameUtility.ComposeJobIconPath(param5);
            if (string.IsNullOrEmpty(str34) != null)
            {
                goto Label_7E08;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = str34;
            return;
        Label_7E08:
            this.ResetToDefault();
            return;
        Label_7E0F:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7E44;
            }
            num141 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(param5.piece);
            this.SetTextValue(num141);
            return;
        Label_7E44:
            this.ResetToDefault();
            return;
        Label_7E4B:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7E6F;
            }
            num142 = param5.GetUnlockNeedPieces();
            this.SetTextValue(num142);
            return;
        Label_7E6F:
            this.ResetToDefault();
            return;
        Label_7E76:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7ECD;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num143 = param5.GetUnlockNeedPieces();
            num144 = Math.Min(manager.Player.GetItemAmount(param5.piece), num143);
            this.SetTextValue(num144);
            this.SetSliderValue(num144, num143);
            return;
        Label_7ECD:
            this.ResetToDefault();
            return;
        Label_7ED4:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_7F22;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num145 = param5.GetUnlockNeedPieces();
            num146 = manager.Player.GetItemAmount(param5.piece);
            base.get_gameObject().SetActive((num146 < num145) == 0);
            return;
        Label_7F22:
            base.get_gameObject().SetActive(0);
            return;
        Label_7F2F:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_7F74;
            }
            if ((QuestDropParam.Instance == null) == null)
            {
                goto Label_7F4D;
            }
            return;
        Label_7F4D:
            param2 = QuestDropParam.Instance.GetHardDropPiece(param.iname, GlobalVars.GetDropTableGeneratedDateTime());
            if (param2 == null)
            {
                goto Label_7F74;
            }
            this.SetItemFrame(param2);
            return;
        Label_7F74:
            this.ResetToDefault();
            return;
        Label_7F7B:
            str35 = GameUtility.Config_OkyakusamaCode;
            if (string.IsNullOrEmpty(str35) == null)
            {
                goto Label_7F9D;
            }
            this.ResetToDefault();
            goto Label_7FBA;
        Label_7F9D:
            objArray4 = new object[] { str35 };
            this.SetTextValue(LocalizedText.Get("sys.OKYAKUSAMACODE", objArray4));
        Label_7FBA:
            return;
        Label_7FBB:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_7FD9;
            }
            this.SetTextValue(data2.GetCombination());
            goto Label_7FDF;
        Label_7FD9:
            this.ResetToDefault();
        Label_7FDF:
            return;
        Label_7FE0:
            data48 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data48 == null)
            {
                goto Label_8009;
            }
            this.SetTextValue(data48.ArenaMedal);
            return;
        Label_8009:
            this.ResetToDefault();
            return;
        Label_8010:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8048;
            }
            data49 = data2.CurrentJob;
            base.get_gameObject().SetActive((data49 == null) ? 0 : data49.IsActivated);
            return;
        Label_8048:
            base.get_gameObject().SetActive(0);
            return;
        Label_8055:
            data50 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (data50 == null)
            {
                goto Label_80EC;
            }
            num147 = 0;
            goto Label_80D5;
        Label_8080:
            if (data50.items[num147].saleType == null)
            {
                goto Label_80CB;
            }
            if (data50.items[num147].saleType != 1)
            {
                goto Label_80BE;
            }
            goto Label_80CB;
        Label_80BE:
            base.get_gameObject().SetActive(1);
            return;
        Label_80CB:
            num147 += 1;
        Label_80D5:
            if (num147 < data50.items.Count)
            {
                goto Label_8080;
            }
        Label_80EC:
            base.get_gameObject().SetActive(0);
            return;
        Label_80F9:
            data51 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (data51 == null)
            {
                goto Label_81A1;
            }
            num148 = 0;
            goto Label_818A;
        Label_8124:
            if (data51.items[num148].saleType == null)
            {
                goto Label_8180;
            }
            if (data51.items[num148].saleType != 1)
            {
                goto Label_8162;
            }
            goto Label_8180;
        Label_8162:
            this.SetBuyPriceTypeIcon(data51.items[num148].saleType);
            return;
        Label_8180:
            num148 += 1;
        Label_818A:
            if (num148 < data51.items.Count)
            {
                goto Label_8124;
            }
        Label_81A1:
            this.ResetToDefault();
            return;
        Label_81A8:
            data52 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (data52 == null)
            {
                goto Label_8267;
            }
            num149 = 0;
            goto Label_8250;
        Label_81D3:
            if (data52.items[num149].saleType == null)
            {
                goto Label_8246;
            }
            if (data52.items[num149].saleType != 1)
            {
                goto Label_8211;
            }
            goto Label_8246;
        Label_8211:
            num150 = MonoSingleton<GameManager>.Instance.Player.GetShopTypeCostAmount(data52.items[num149].saleType);
            this.SetTextValue(num150);
            return;
        Label_8246:
            num149 += 1;
        Label_8250:
            if (num149 < data52.items.Count)
            {
                goto Label_81D3;
            }
        Label_8267:
            this.ResetToDefault();
            return;
        Label_826E:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8295;
            }
            base.get_gameObject().SetActive(((data2.BadgeState & 1) == 0) == 0);
            return;
        Label_8295:
            base.get_gameObject().SetActive(0);
            return;
        Label_82A2:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_82C8;
            }
            base.get_gameObject().SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(param5));
            return;
        Label_82C8:
            base.get_gameObject().SetActive(0);
            return;
        Label_82D5:
            if ((param5 = this.GetUnitParam()) == null)
            {
                goto Label_8328;
            }
            param63 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitUnlockTimeParam(param5.unlock_time);
            if (param63 != null)
            {
                goto Label_8307;
            }
            return;
        Label_8307:
            str36 = "yyyy/MM/dd HH:mm";
            this.SetTextValue(&param63.end_at.ToString(str36));
        Label_8328:
            return;
        Label_8329:
            if (((param2 = this.GetItemParam()) == null) || (MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableEquipUnit(param2) == null))
            {
                goto Label_835A;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_835A:
            base.get_gameObject().SetActive(0);
            return;
        Label_8367:
            if (((MonoSingleton<GameManager>.Instance.BadgeFlags & 1) == null) && ((MonoSingleton<GameManager>.Instance.BadgeFlags & 2) == null))
            {
                goto Label_8396;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_8396:
            base.get_gameObject().SetActive(0);
            return;
        Label_83A3:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 1) == 0) == 0);
            return;
        Label_83C1:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 2) == 0) == 0);
            return;
        Label_83DF:
            if (((MonoSingleton<GameManager>.Instance.BadgeFlags & 4) == null) && ((MonoSingleton<GameManager>.Instance.BadgeFlags & 8) == null))
            {
                goto Label_840E;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_840E:
            base.get_gameObject().SetActive(0);
            return;
        Label_841B:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 4) == 0) == 0);
            return;
        Label_8439:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 8) == 0) == 0);
            return;
        Label_8457:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x20) == 0) == 0);
            return;
        Label_8476:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x40) == 0) == 0);
            return;
        Label_8495:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x10) == 0) == 0);
            return;
        Label_84B4:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x80) == 0) == 0);
            return;
        Label_84D6:
            base.get_gameObject().SetActive(((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x100) == 0) == 0);
            return;
        Label_84F8:
            if ((((MonoSingleton<GameManager>.Instance.BadgeFlags & 1) == null) && ((MonoSingleton<GameManager>.Instance.BadgeFlags & 2) == null)) && (((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x10) == null) && ((MonoSingleton<GameManager>.Instance.BadgeFlags & 0x100) == null)))
            {
                goto Label_854E;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_854E:
            base.get_gameObject().SetActive(0);
            return;
        Label_855B:
            manager = MonoSingleton<GameManager>.Instance;
            num151 = manager.Player.VipPoint - ((manager.Player.VipRank <= 0) ? 0 : manager.MasterParam.GetVipRankNextPoint(manager.Player.VipRank - 1));
            num152 = manager.MasterParam.GetVipRankNextPoint(manager.Player.VipRank);
            this.SetTextValue(num151);
            this.SetSliderValue(num151, num152);
            return;
        Label_85D3:
            manager = MonoSingleton<GameManager>.Instance;
            num153 = manager.MasterParam.GetVipRankNextPoint(manager.Player.VipRank);
            this.SetTextValue(num153);
            return;
        Label_85FE:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.FreeCoin);
            return;
        Label_8616:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.PaidCoin);
            return;
        Label_862E:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.ComCoin);
            return;
        Label_8646:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.FreeCoin + manager.Player.ComCoin);
            return;
        Label_866A:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_86DC;
            }
            list11 = MonoSingleton<GameManager>.Instance.Player.Partys;
            num154 = 0;
            goto Label_86CA;
        Label_8696:
            if (list11[num154].IsPartyUnit(data2.UniqueID) == null)
            {
                goto Label_86C0;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_86C0:
            num154 += 1;
        Label_86CA:
            if (num154 < list11.Count)
            {
                goto Label_8696;
            }
        Label_86DC:
            base.get_gameObject().SetActive(0);
            return;
        Label_86E9:
            if (((data4 = DataSource.FindDataOfClass<ItemData>(base.get_gameObject(), null)) == null) || ((data4 as LoginBonusData) == null))
            {
                goto Label_871C;
            }
            this.SetTextValue(((LoginBonusData) data4).DayNum);
            return;
        Label_871C:
            this.ResetToDefault();
            return;
        Label_8723:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8743;
            }
            base.get_gameObject().SetActive(GameUtility.GetUnitShowSetting(0x11));
            return;
        Label_8743:
            base.get_gameObject().SetActive(0);
            return;
        Label_8750:
            if ((((data2 = this.GetUnitData()) == null) || (GameUtility.GetUnitShowSetting(0x12) != null)) || (GameUtility.GetUnitShowSetting(0x11) != null))
            {
                goto Label_8782;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_8782:
            base.get_gameObject().SetActive(0);
            return;
        Label_878F:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8A2B;
            }
            if (GameUtility.GetUnitShowSetting(0x11) == null)
            {
                goto Label_87B5;
            }
            this.SetTextValue(data2.Lv);
            return;
        Label_87B5:
            if (GameUtility.GetUnitShowSetting(0x12) == null)
            {
                goto Label_87C8;
            }
            this.ResetToDefault();
            return;
        Label_87C8:
            if (GameUtility.GetUnitShowSetting(0x13) == null)
            {
                goto Label_87FD;
            }
            data53 = data2.CurrentJob;
            this.SetTextValue((data53 == null) ? 1 : data53.Rank);
            return;
        Label_87FD:
            if (GameUtility.GetUnitShowSetting(20) == null)
            {
                goto Label_8825;
            }
            this.SetTextValue(data2.Status.param.hp);
            return;
        Label_8825:
            if (GameUtility.GetUnitShowSetting(0x15) == null)
            {
                goto Label_884D;
            }
            this.SetTextValue(data2.Status.param.atk);
            return;
        Label_884D:
            if (GameUtility.GetUnitShowSetting(0x16) == null)
            {
                goto Label_8875;
            }
            this.SetTextValue(data2.Status.param.def);
            return;
        Label_8875:
            if (GameUtility.GetUnitShowSetting(0x17) == null)
            {
                goto Label_889D;
            }
            this.SetTextValue(data2.Status.param.mag);
            return;
        Label_889D:
            if (GameUtility.GetUnitShowSetting(0x18) == null)
            {
                goto Label_88C5;
            }
            this.SetTextValue(data2.Status.param.mnd);
            return;
        Label_88C5:
            if (GameUtility.GetUnitShowSetting(0x19) == null)
            {
                goto Label_88ED;
            }
            this.SetTextValue(data2.Status.param.spd);
            return;
        Label_88ED:
            if (GameUtility.GetUnitShowSetting(0x1a) == null)
            {
                goto Label_89F9;
            }
            num155 = 0;
            num155 += data2.Status.param.atk;
            num155 += data2.Status.param.def;
            num155 += data2.Status.param.mag;
            num155 += data2.Status.param.mnd;
            num155 += data2.Status.param.spd;
            num155 += data2.Status.param.dex;
            num155 += data2.Status.param.cri;
            num155 += data2.Status.param.luk;
            this.SetTextValue(num155);
            return;
        Label_89F9:
            if (GameUtility.GetUnitShowSetting(0x1b) == null)
            {
                goto Label_8A12;
            }
            this.SetTextValue(data2.AwakeLv);
            return;
        Label_8A12:
            if (GameUtility.GetUnitShowSetting(0x1c) == null)
            {
                goto Label_8A2B;
            }
            this.SetTextValue(data2.GetCombination());
            return;
        Label_8A2B:
            this.ResetToDefault();
            return;
        Label_8A32:
            if ((storey.skillParam = this.GetSkillParam()) == null)
            {
                goto Label_8A8C;
            }
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8A7F;
            }
            base.get_gameObject().SetActive(data2.GetSkillData(storey.skillParam.iname) == null);
            return;
        Label_8A7F:
            base.get_gameObject().SetActive(1);
            return;
        Label_8A8C:
            base.get_gameObject().SetActive(0);
            return;
        Label_8A99:
            if ((((storey.skillParam = this.GetSkillParam()) == null) || ((param3 = this.GetAbilityParam()) == null)) || ((param3.skills == null) || (((int) param3.skills.Length) <= 0)))
            {
                goto Label_8B85;
            }
            if (string.IsNullOrEmpty(storey.skillParam.ReplacedTargetId) != null)
            {
                goto Label_8B39;
            }
            param64 = MonoSingleton<GameManager>.Instance.GetSkillParam(storey.skillParam.ReplacedTargetId);
            storey.skillParam = (param64 == null) ? storey.skillParam : param64;
        Label_8B39:
            skill = Array.Find<LearningSkill>(param3.skills, new Predicate<LearningSkill>(storey.<>m__10C));
            if (skill == null)
            {
                goto Label_8B85;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.SKILL_LEANING_CONDITION1"), (int) skill.locklv));
            return;
        Label_8B85:
            this.SetTextValue(string.Format(LocalizedText.Get("sys.SKILL_LEANING_CONDITION1"), (int) 1));
            return;
        Label_8BA1:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8C2C;
            }
            num156 = 1;
            str37 = null;
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_8BD1;
            }
            str37 = data5.AbilityID;
        Label_8BD1:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_8BEA;
            }
            str37 = param3.iname;
        Label_8BEA:
            MonoSingleton<GameManager>.Instance.GetLearningAbilitySource(data2, str37, &param7, &num156);
            if (param7 == null)
            {
                goto Label_8C2C;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.ABILITY_LEANING_COND1"), param7.name, (int) num156));
            return;
        Label_8C2C:
            this.SetTextValue(null);
            return;
        Label_8C34:
            if ((param6 = this.GetGachaParam()) == null)
            {
                goto Label_8C76;
            }
            if (param6.gold == null)
            {
                goto Label_8C5C;
            }
            this.SetTextValue(param6.gold);
            return;
        Label_8C5C:
            if (param6.coin == null)
            {
                goto Label_8C76;
            }
            this.SetTextValue(param6.coin);
            return;
        Label_8C76:
            this.ResetToDefault();
            return;
        Label_8C7D:
            if ((param6 = this.GetGachaParam()) == null)
            {
                goto Label_8C99;
            }
            this.SetTextValue(param6.num);
            return;
        Label_8C99:
            this.ResetToDefault();
            return;
        Label_8CA0:
            return;
        Label_8CA1:
            return;
        Label_8CA2:
            num157 = MonoSingleton<GameManager>.Instance.Player.GetNextFreeGachaGoldCoolDownSec();
            num221 = num157 / 0xe10L;
            str38 = &num221.ToString();
            num222 = (num157 % 0xe10L) / 60L;
            str39 = &num222.ToString();
            num223 = num157 % 60L;
            str40 = &num223.ToString();
            textArray1 = new string[] { str38.PadLeft(2, 0x30), ":", str39.PadLeft(2, 0x30), ":", str40.PadLeft(2, 0x30) };
            str41 = string.Concat(textArray1);
            this.SetTextValue(str41);
            this.SetUpdateInterval(1f);
            return;
        Label_8D6E:
            manager = MonoSingleton<GameManager>.Instance;
            num224 = manager.MasterParam.FixParam.FreeGachaGoldMax - manager.Player.FreeGachaGold.num;
            str42 = &num224.ToString();
            this.SetTextValue(str42);
            return;
        Label_8DB6:
            return;
        Label_8DB7:
            num158 = MonoSingleton<GameManager>.Instance.Player.GetNextFreeGachaCoinCoolDownSec();
            num225 = num158 / 0xe10L;
            str43 = &num225.ToString();
            num226 = (num158 % 0xe10L) / 60L;
            str44 = &num226.ToString();
            num227 = num158 % 60L;
            str45 = &num227.ToString();
            textArray2 = new string[] { str43.PadLeft(2, 0x30), ":", str44.PadLeft(2, 0x30), ":", str45.PadLeft(2, 0x30) };
            str46 = string.Concat(textArray2);
            this.SetTextValue(str46);
            this.SetUpdateInterval(1f);
            return;
        Label_8E83:
            manager = MonoSingleton<GameManager>.Instance;
            num228 = 1 - manager.Player.FreeGachaCoin.num;
            str47 = &num228.ToString();
            this.SetTextValue(str47);
            return;
        Label_8EB7:
            return;
        Label_8EB8:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_8ED8;
            }
            this.SetTextValue(param2.Flavor);
            goto Label_8EDE;
        Label_8ED8:
            this.ResetToDefault();
        Label_8EDE:
            return;
        Label_8EDF:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_8F63;
            }
            sheet = AssetManager.Load<SpriteSheet>(AssetPath.JobIconThumbnail());
            image = base.GetComponent<Image>();
            if (((sheet != null) == null) || ((image != null) == null))
            {
                goto Label_8F63;
            }
            data54 = data2.CurrentJob;
            if (data54 == null)
            {
                goto Label_8F59;
            }
            sprite = sheet.GetSprite(data54.JobResourceID);
            image.set_sprite(sprite);
            return;
        Label_8F59:
            image.set_sprite(null);
        Label_8F63:
            return;
        Label_8F64:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_8F86;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(1L));
        Label_8F86:
            return;
        Label_8F87:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_8FA9;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(2L));
        Label_8FA9:
            return;
        Label_8FAA:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_8FCC;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(4L));
        Label_8FCC:
            return;
        Label_8FCD:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_8FEF;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(8L));
        Label_8FEF:
            return;
        Label_8FF0:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_9013;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x10L));
        Label_9013:
            return;
        Label_9014:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_9037;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x20L));
        Label_9037:
            return;
        Label_9038:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_905B;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x40L));
        Label_905B:
            return;
        Label_905C:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_9082;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x80L));
        Label_9082:
            return;
        Label_9083:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_90A9;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x100L));
        Label_90A9:
            return;
        Label_90AA:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_90D0;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x200L));
        Label_90D0:
            return;
        Label_90D1:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_90F7;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x400L));
        Label_90F7:
            return;
        Label_90F8:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_911E;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x800L));
        Label_911E:
            return;
        Label_911F:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_9145;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x1000L));
        Label_9145:
            return;
        Label_9146:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_916C;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x2000L));
        Label_916C:
            return;
        Label_916D:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_9193;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x4000L));
        Label_9193:
            return;
        Label_9194:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_91BA;
            }
            base.get_gameObject().SetActive(unit.IsUnitCondition(0x8000L));
        Label_91BA:
            return;
        Label_91BB:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_91ED;
            }
            if (unit.Side != null)
            {
                goto Label_91E1;
            }
            this.SetImageIndex(0);
            goto Label_91E8;
        Label_91E1:
            this.SetImageIndex(1);
        Label_91E8:
            goto Label_91F3;
        Label_91ED:
            this.ResetToDefault();
        Label_91F3:
            return;
        Label_91F4:
            param65 = DataSource.FindDataOfClass<JobSetParam>(base.get_gameObject(), null);
            if (param65 == null)
            {
                goto Label_93BD;
            }
            builder = GameUtility.GetStringBuilder();
            if (param65.lock_rarity <= 0)
            {
                goto Label_925A;
            }
            builder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_RARITY"), (int) (param65.lock_rarity + 1)));
            builder.Append(10);
        Label_925A:
            if (param65.lock_awakelv <= 0)
            {
                goto Label_929C;
            }
            builder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_AWAKELV"), (int) param65.lock_awakelv));
            builder.Append(10);
        Label_929C:
            if (param65.lock_jobs == null)
            {
                goto Label_936F;
            }
            num159 = 0;
            goto Label_935B;
        Label_92B4:
            if ((string.IsNullOrEmpty(param65.lock_jobs[num159].iname) != null) || (param65.lock_jobs[num159].lv <= 0))
            {
                goto Label_9351;
            }
            param66 = MonoSingleton<GameManager>.Instance.GetJobParam(param65.lock_jobs[num159].iname);
            builder.Append(string.Format(LocalizedText.Get("sys.UNLOCK_CONDITION"), param66.name, (int) param65.lock_jobs[num159].lv));
            builder.Append(10);
        Label_9351:
            num159 += 1;
        Label_935B:
            if (num159 < ((int) param65.lock_jobs.Length))
            {
                goto Label_92B4;
            }
        Label_936F:
            if ((builder.Length <= 0) || (builder[builder.Length - 1] != 10))
            {
                goto Label_93AD;
            }
            builder.Length -= 1;
        Label_93AD:
            this.SetTextValue(builder.ToString());
            return;
        Label_93BD:
            this.ResetToDefault();
            return;
        Label_93C4:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_93D1;
            }
        Label_93D1:
            return;
        Label_93D2:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_944A;
            }
            num160 = Mathf.Min(unit.ChargeTime, unit.ChargeTimeMax);
            this.SetTextValue(&Mathf.Floor(((float) num160) / 10f).ToString());
            this.SetSliderValue(num160, unit.ChargeTimeMax);
            return;
        Label_944A:
            this.ResetToDefault();
            return;
        Label_9451:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_948B;
            }
            this.SetTextValue(&Mathf.Floor(((float) unit.ChargeTimeMax) / 10f).ToString());
            return;
        Label_948B:
            this.ResetToDefault();
            return;
        Label_9492:
            if ((unit = this.GetUnit()) == null)
            {
                goto Label_94C2;
            }
            this.SetTextValue(LocalizedText.Get("quest." + unit.UnitParam.iname));
            return;
        Label_94C2:
            this.ResetToDefault();
            return;
        Label_94C9:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_94F6;
            }
            this.SetTextValue(data2.Status.param.dex);
            goto Label_94FC;
        Label_94F6:
            this.ResetToDefault();
        Label_94FC:
            goto Label_EDD9;
        Label_9501:
            param67 = this.GetArtifactParam();
            if (param67 == null)
            {
                goto Label_9528;
            }
            this.SetTextValue(param67.name);
            goto Label_952E;
        Label_9528:
            this.ResetToDefault();
        Label_952E:
            goto Label_EDD9;
        Label_9533:
            param68 = this.GetArtifactParam();
            if (param68 == null)
            {
                goto Label_955A;
            }
            this.SetTextValue(param68.Expr);
            goto Label_9560;
        Label_955A:
            this.ResetToDefault();
        Label_9560:
            goto Label_EDD9;
        Label_9565:
            param69 = this.GetArtifactParam();
            if (param69 == null)
            {
                goto Label_958C;
            }
            this.SetTextValue(param69.spec);
            goto Label_9592;
        Label_958C:
            this.ResetToDefault();
        Label_9592:
            goto Label_EDD9;
        Label_9597:
            data55 = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data55 == null)
            {
                goto Label_9601;
            }
            this.SetTextValue(data55.Rarity + 1);
            this.SetImageIndex(data55.Rarity);
            this.SetSliderValue(data55.Rarity, data55.RarityCap);
            goto Label_9607;
        Label_9601:
            this.ResetToDefault();
        Label_9607:
            return;
        Label_9608:
            data56 = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data56 == null)
            {
                goto Label_9650;
            }
            this.SetTextValue(data56.RarityCap + 1);
            this.SetImageIndex(data56.RarityCap);
            goto Label_9656;
        Label_9650:
            this.ResetToDefault();
        Label_9656:
            return;
        Label_9657:
            param70 = this.GetArtifactParam();
            if (param70 == null)
            {
                goto Label_96DC;
            }
            num161 = 0;
            list12 = MonoSingleton<GameManager>.Instance.Player.Artifacts;
            num162 = 0;
            goto Label_96BB;
        Label_968C:
            if (list12[num162].ArtifactParam != param70)
            {
                goto Label_96B1;
            }
            num161 += 1;
        Label_96B1:
            num162 += 1;
        Label_96BB:
            if (num162 < list12.Count)
            {
                goto Label_968C;
            }
            this.SetTextValue(num161);
            goto Label_96E2;
        Label_96DC:
            this.ResetToDefault();
        Label_96E2:
            goto Label_EDD9;
        Label_96E7:
            data57 = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data57 == null)
            {
                goto Label_9714;
            }
            this.SetTextValue(data57.GetSellPrice());
            goto Label_971A;
        Label_9714:
            this.ResetToDefault();
        Label_971A:
            goto Label_EDD9;
        Label_971F:
            this.SetTextValue(MyApplicationPlugin.get_version());
            goto Label_EDD9;
        Label_972F:
            if ((storey.supportData = this.GetSupportData()) == null)
            {
                goto Label_9790;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num163 = storey.supportData.Unit.GetLevelCap(0);
            num164 = storey.supportData.PlayerLevel;
            this.SetTextValue(Mathf.Min(num163, num164));
            return;
        Label_9790:
            this.ResetToDefault();
            return;
        Label_9797:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.PiecePoint);
            return;
        Label_97AF:
            list = this.GetSellItemList();
            if (list != null)
            {
                goto Label_97C5;
            }
            this.ResetToDefault();
            return;
        Label_97C5:
            num165 = 0;
            num166 = 0;
            goto Label_9817;
        Label_97D4:
            num165 += list[num166].item.RarityParam.PieceToPoint * list[num166].num;
            num166 += 1;
        Label_9817:
            if (num166 < list.Count)
            {
                goto Label_97D4;
            }
            this.SetTextValue(num165);
            return;
        Label_9832:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_9873;
            }
            param71 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(param2.rare);
            this.SetTextValue(param71.PieceToPoint);
            goto Label_9879;
        Label_9873:
            this.ResetToDefault();
        Label_9879:
            return;
        Label_987A:
            item = this.GetSellItem();
            if (item != null)
            {
                goto Label_9890;
            }
            this.ResetToDefault();
            return;
        Label_9890:
            num167 = item.item.RarityParam.PieceToPoint * item.num;
            this.SetTextValue(num167);
            return;
        Label_98BD:
            data58 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data58 == null)
            {
                goto Label_98E6;
            }
            this.SetTextValue(data58.MultiCoin);
            return;
        Label_98E6:
            this.ResetToDefault();
            return;
        Label_98ED:
            data59 = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            if (data59 == null)
            {
                goto Label_9916;
            }
            this.SetTextValue(data59.KakeraCoin);
            return;
        Label_9916:
            this.ResetToDefault();
            return;
        Label_991D:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_9AC0;
            }
            if (param.type != 6)
            {
                goto Label_9948;
            }
            list13 = param.GetEntryQuestConditionsCh(1, 0, 1);
            goto Label_9955;
        Label_9948:
            list13 = param.GetEntryQuestConditions(1, 1, 1);
        Label_9955:
            if ((list13 == null) || (list13.Count <= 0))
            {
                goto Label_9AAF;
            }
            str48 = string.Empty;
            num168 = 0;
            goto Label_9A00;
        Label_9980:
            if (num168 <= 0)
            {
                goto Label_99DC;
            }
            switch (this.Index)
            {
                case 0:
                    goto Label_99AE;

                case 1:
                    goto Label_99C5;

                case 2:
                    goto Label_99AE;
            }
            goto Label_99C5;
        Label_99AE:
            str48 = str48 + "\n";
            goto Label_99DC;
        Label_99C5:
            str48 = str48 + "、";
        Label_99DC:
            str48 = str48 + list13[num168];
            num168 += 1;
        Label_9A00:
            if (num168 < list13.Count)
            {
                goto Label_9980;
            }
            if (string.IsNullOrEmpty(str48) != null)
            {
                goto Label_9AC0;
            }
            if (this.Index == null)
            {
                goto Label_9A7C;
            }
            type = param.GetPartyCondType();
            if (type != 1)
            {
                goto Label_9A5B;
            }
            str48 = LocalizedText.Get("sys.PARTYEDITOR_COND_LIMIT") + str48;
            goto Label_9A7C;
        Label_9A5B:
            if (type != 2)
            {
                goto Label_9A7C;
            }
            str48 = LocalizedText.Get("sys.PARTYEDITOR_COND_FIXED") + str48;
        Label_9A7C:
            if (this.Index != 4)
            {
                goto Label_9A9F;
            }
            str48 = str48.Replace("\n", string.Empty);
        Label_9A9F:
            this.SetTextValue(str48);
            return;
            goto Label_9AC0;
        Label_9AAF:
            this.SetTextValue(LocalizedText.Get("sys.PARTYEDITOR_COND_NO_LIMIT"));
            return;
        Label_9AC0:
            this.ResetToDefault();
            return;
        Label_9AC7:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_9B2F;
            }
            list14 = param.GetEntryQuestConditions(1, 1, 1);
            flag14 = (list14 == null) ? 0 : (list14.Count > 0);
            if (this.Index != null)
            {
                goto Label_9B1C;
            }
            base.get_gameObject().SetActive(flag14);
            goto Label_9B2E;
        Label_9B1C:
            base.get_gameObject().SetActive(flag14 == 0);
        Label_9B2E:
            return;
        Label_9B2F:
            base.get_gameObject().SetActive(0);
            return;
        Label_9B3C:
            if (((data2 = this.GetUnitData()) == null) || ((param = this.GetQuestParam()) == null))
            {
                goto Label_9BA0;
            }
            str49 = string.Empty;
            flag15 = param.IsEntryQuestCondition(data2, &str49);
            if (this.Index != null)
            {
                goto Label_9B8D;
            }
            base.get_gameObject().SetActive(flag15);
            goto Label_9B9F;
        Label_9B8D:
            base.get_gameObject().SetActive(flag15 == 0);
        Label_9B9F:
            return;
        Label_9BA0:
            base.get_gameObject().SetActive(0);
            return;
        Label_9BAD:
            if (((data2 = this.GetUnitData()) == null) || ((param = this.GetQuestParam()) == null))
            {
                goto Label_9BD9;
            }
            this.SetTextValue(GameUtility.ComposeCharacterQuestMainUnitConditionText(data2, param));
            goto Label_9BDF;
        Label_9BD9:
            this.ResetToDefault();
        Label_9BDF:
            return;
        Label_9BE0:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_9C1F;
            }
            param72 = data2.GetCurrentCharaEpisodeData();
            if (param72 == null)
            {
                goto Label_9C14;
            }
            this.SetTextValue(param72.EpisodeTitle);
            goto Label_9C1A;
        Label_9C14:
            this.ResetToDefault();
        Label_9C1A:
            goto Label_9C25;
        Label_9C1F:
            this.ResetToDefault();
        Label_9C25:
            return;
        Label_9C26:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_9C7D;
            }
            param73 = data2.GetCurrentCharaEpisodeData();
            if (param73 == null)
            {
                goto Label_9C72;
            }
            objArray5 = new object[] { (int) param73.EpisodeNum };
            this.SetTextValue(LocalizedText.Get("sys.CHARQUEST_EP_NUM", objArray5));
            goto Label_9C78;
        Label_9C72:
            this.ResetToDefault();
        Label_9C78:
            goto Label_9C83;
        Label_9C7D:
            this.ResetToDefault();
        Label_9C83:
            return;
        Label_9C84:
            data60 = DataSource.FindDataOfClass<JobData>(base.get_gameObject(), null);
            if (data60 == null)
            {
                goto Label_9CB2;
            }
            base.get_gameObject().SetActive(data60.CheckJobMaster());
            return;
        Label_9CB2:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_9D1F;
            }
            if (this.Index != -1)
            {
                goto Label_9CDA;
            }
            data60 = data2.CurrentJob;
            goto Label_9CFB;
        Label_9CDA:
            if (data2.IsJobAvailable(this.Index) == null)
            {
                goto Label_9CFB;
            }
            data60 = data2.GetJobData(this.Index);
        Label_9CFB:
            if ((data60 == null) || (data60.CheckJobMaster() == null))
            {
                goto Label_9D1F;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_9D1F:
            base.get_gameObject().SetActive(0);
            return;
        Label_9D2C:
            battle7 = SceneBattle.Instance;
            if ((battle7 == null) == null)
            {
                goto Label_9D4B;
            }
            this.ResetToDefault();
            return;
        Label_9D4B:
            num231 = (int) battle7.MultiPlayAddInputTime;
            str50 = "+" + &num231.ToString();
            this.SetTextValue(str50);
            return;
        Label_9D7B:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_9E64;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_9E53;
            }
            num169 = data2.AwakeLv + 1;
            num170 = data2.GetAwakeLevelCap();
            num171 = 5;
            num172 = num170 / num171;
            if (num172 <= this.Index)
            {
                goto Label_9E42;
            }
            num173 = num171;
            num174 = this.Index * num171;
            num175 = (this.Index + 1) * num171;
            if (num175 <= num169)
            {
                goto Label_9E27;
            }
            num173 = ((num169 - num174) >= 0) ? (num169 % num171) : 0;
        Label_9E27:
            this.SetImageIndex(num173);
            base.get_gameObject().SetActive(1);
            goto Label_9E4E;
        Label_9E42:
            base.get_gameObject().SetActive(0);
        Label_9E4E:
            goto Label_9E5F;
        Label_9E53:
            this.SetTextValue(data2.AwakeLv);
        Label_9E5F:
            goto Label_9E6A;
        Label_9E64:
            this.ResetToDefault();
        Label_9E6A:
            return;
        Label_9E6B:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_9ECA;
            }
            num176 = data2.AwakeLv;
            num177 = data2.GetAwakeLevelCap();
            if (this.Index != null)
            {
                goto Label_9EB1;
            }
            base.get_gameObject().SetActive(num177 > num176);
            goto Label_9EC9;
        Label_9EB1:
            base.get_gameObject().SetActive((num177 > num176) == 0);
        Label_9EC9:
            return;
        Label_9ECA:
            base.get_gameObject().SetActive(0);
            return;
        Label_9ED7:
            flag16 = GameUtility.Config_UseAutoPlay.Value;
            base.get_gameObject().SetActive((this.Index != null) ? (flag16 == 0) : flag16);
            return;
        Label_9F0C:
            battle8 = SceneBattle.Instance;
            if (((battle8 == null) == null) && (battle8.CurrentResumePlayer != null))
            {
                goto Label_9F39;
            }
            this.ResetToDefault();
            return;
        Label_9F39:
            this.SetTextValue(battle8.CurrentResumePlayer.playerIndex);
            return;
        Label_9F4E:
            photon14 = PunMonoSingleton<MyPhoton>.Instance;
            battle9 = SceneBattle.Instance;
            if (((battle9 == null) == null) && (battle9.CurrentResumePlayer != null))
            {
                goto Label_9F8A;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_9F8A:
            base.get_gameObject().SetActive(photon14.IsHost(battle9.CurrentResumePlayer.playerID));
            return;
        Label_9FAD:
            battle10 = SceneBattle.Instance;
            if ((battle10 == null) == null)
            {
                goto Label_9FD2;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_9FD2:
            base.get_gameObject().SetActive(battle10.ResumeOnly);
            return;
        Label_9FE7:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_A16B;
            }
            list15 = param.GetEntryQuestConditionsCh(1, 1, 0);
            str51 = string.Empty;
            if ((list15 == null) || (list15.Count <= 0))
            {
                goto Label_A0A0;
            }
            num178 = 0;
            goto Label_A08E;
        Label_A02C:
            if (num178 <= 0)
            {
                goto Label_A06A;
            }
            if (this.Index != null)
            {
                goto Label_A058;
            }
            str51 = str51 + "\n";
            goto Label_A06A;
        Label_A058:
            str51 = str51 + "、";
        Label_A06A:
            str51 = str51 + list15[num178];
            num178 += 1;
        Label_A08E:
            if (num178 < list15.Count)
            {
                goto Label_A02C;
            }
        Label_A0A0:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_A152;
            }
            list16 = data2.GetQuestUnlockConditions(param);
            if ((list16 == null) || (list16.Count <= 0))
            {
                goto Label_A152;
            }
            num179 = 0;
            goto Label_A140;
        Label_A0DA:
            if (string.IsNullOrEmpty(str51) != null)
            {
                goto Label_A11C;
            }
            if (this.Index != null)
            {
                goto Label_A10A;
            }
            str51 = str51 + "\n";
            goto Label_A11C;
        Label_A10A:
            str51 = str51 + "、";
        Label_A11C:
            str51 = str51 + list16[num179];
            num179 += 1;
        Label_A140:
            if (num179 < list16.Count)
            {
                goto Label_A0DA;
            }
        Label_A152:
            if (string.IsNullOrEmpty(str51) != null)
            {
                goto Label_A16B;
            }
            this.SetTextValue(str51);
            return;
        Label_A16B:
            this.ResetToDefault();
            return;
        Label_A172:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_A260;
            }
            list17 = param.GetEntryQuestConditionsCh(1, 1, 1);
            flag17 = 1;
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_A208;
            }
            list18 = data2.GetQuestUnlockConditions(param);
            if ((list18 == null) || (list18.Count <= 0))
            {
                goto Label_A208;
            }
            num180 = 0;
            goto Label_A1F6;
        Label_A1CB:
            if (string.IsNullOrEmpty(list18[num180]) != null)
            {
                goto Label_A1EC;
            }
            flag17 = 0;
            goto Label_A208;
        Label_A1EC:
            num180 += 1;
        Label_A1F6:
            if (num180 < list18.Count)
            {
                goto Label_A1CB;
            }
        Label_A208:;
            flag18 = ((list17 != null) && (list17.Count > 0)) ? 1 : (flag17 == 0);
            if (this.Index != null)
            {
                goto Label_A24D;
            }
            base.get_gameObject().SetActive(flag18);
            goto Label_A25F;
        Label_A24D:
            base.get_gameObject().SetActive(flag18 == 0);
        Label_A25F:
            return;
        Label_A260:
            base.get_gameObject().SetActive(0);
            return;
        Label_A26D:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_A29F;
            }
            objArray6 = new object[] { data2.UnitParam.name };
            this.SetTextValue(LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_TITLE", objArray6));
            return;
        Label_A29F:
            this.ResetToDefault();
            return;
        Label_A2A6:
            data61 = DataSource.FindDataOfClass<EventCoinData>(base.get_gameObject(), null);
            if ((data61 == null) || (data61.param == null))
            {
                goto Label_A2DC;
            }
            this.SetBuyPriceEventCoinTypeIcon(data61.iname);
        Label_A2DC:
            return;
        Label_A2DD:
            data62 = DataSource.FindDataOfClass<EventCoinData>(base.get_gameObject(), null);
            if ((data62 == null) || (data62.param == null))
            {
                goto Label_A31D;
            }
            this.SetTextValue(data62.param.name);
            goto Label_A324;
        Label_A31D:
            this.SetTextValue(0);
        Label_A324:
            return;
        Label_A325:
            data63 = DataSource.FindDataOfClass<EventCoinData>(base.get_gameObject(), null);
            if ((data63 == null) || (data63.param == null))
            {
                goto Label_A365;
            }
            this.SetTextValue(data63.param.Flavor);
            goto Label_A36C;
        Label_A365:
            this.SetTextValue(0);
        Label_A36C:
            return;
        Label_A36D:
            data64 = DataSource.FindDataOfClass<EventCoinData>(base.get_gameObject(), null);
            if ((data64 == null) || (data64.have == null))
            {
                goto Label_A3AD;
            }
            this.SetTextValue(data64.have.Num);
            goto Label_A3B4;
        Label_A3AD:
            this.SetTextValue(0);
        Label_A3B4:
            return;
        Label_A3B5:
            item8 = this.GetEventShopItem();
            if (item8 == null)
            {
                goto Label_A3D8;
            }
            this.SetBuyPriceEventCoinTypeIcon(item8.cost_iname);
            return;
        Label_A3D8:
            param74 = DataSource.FindDataOfClass<ItemParam>(base.get_gameObject(), null);
            if (param74 == null)
            {
                goto Label_A401;
            }
            this.SetBuyPriceEventCoinTypeIcon(param74.iname);
            return;
        Label_A401:
            this.ResetToDefault();
            return;
        Label_A408:
            item9 = this.GetEventShopItem();
            if (item9 == null)
            {
                goto Label_A42F;
            }
            this.SetTextValue(item9.remaining_num);
            goto Label_A436;
        Label_A42F:
            this.SetTextValue(0);
        Label_A436:
            return;
        Label_A437:
            param75 = this.GetTrophyParam();
            if (param75 == null)
            {
                goto Label_A60C;
            }
            time9 = TimeManager.ServerTime;
            time10 = param75.CategoryParam.end_at.DateTimes;
            if (param75.CategoryParam.IsBeginner == null)
            {
                goto Label_A4B3;
            }
            time11 = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
            time10 = ((time10 <= time11) == null) ? time11 : time10;
        Label_A4B3:
            if (param75.CategoryParam.IsBeginner != null)
            {
                goto Label_A4DD;
            }
            time10 = param75.CategoryParam.GetQuestTime(time10, 1);
        Label_A4DD:
            if (MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(param75, 0).IsCompleted == null)
            {
                goto Label_A510;
            }
            time10 = param75.GetGraceRewardTime();
        Label_A510:
            if (((string.IsNullOrEmpty(param75.CategoryParam.end_at.StrTime) != null) || (param75.IsDaily != null)) || ((time10 >= time9) == null))
            {
                goto Label_A600;
            }
            span4 = time10 - time9;
            if (&span4.Days <= 0)
            {
                goto Label_A595;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_DAY"), (int) &span4.Days));
            goto Label_A5EF;
        Label_A595:
            if (&span4.Hours <= 0)
            {
                goto Label_A5CC;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_HOUR"), (int) &span4.Hours));
            goto Label_A5EF;
        Label_A5CC:
            this.SetTextValue(string.Format(LocalizedText.Get("sys.TROPHY_REMAINING_MINUTE"), (int) &span4.Minutes));
        Label_A5EF:
            base.get_gameObject().SetActive(1);
            goto Label_A60C;
        Label_A600:
            base.get_gameObject().SetActive(0);
        Label_A60C:
            return;
        Label_A60D:
            str52 = GameUtility.Config_OkyakusamaCode;
            if (this.Index != null)
            {
                goto Label_A63D;
            }
            base.get_gameObject().SetActive(string.IsNullOrEmpty(str52) == 0);
            goto Label_A66C;
        Label_A63D:
            if (this.Index != 1)
            {
                goto Label_A66C;
            }
            if (string.IsNullOrEmpty(str52) == null)
            {
                goto Label_A662;
            }
            this.ResetToDefault();
            goto Label_A66C;
        Label_A662:
            this.SetTextValue(str52);
        Label_A66C:
            return;
        Label_A66D:
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_A715;
            }
            param77 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
            if (param77 == null)
            {
                goto Label_A817;
            }
            param77.SetupUnits();
            data65 = param77.units[0].unit;
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.UnitSkinImage(data65.UnitParam, data65.GetSelectedSkin(-1), data65.CurrentJob.JobID);
            return;
            goto Label_A817;
        Label_A715:
            param78 = this.GetVersusPlayerParam();
            if (param78 == null)
            {
                goto Label_A784;
            }
            param78.SetupUnits();
            data66 = param78.units[0].unit;
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.UnitSkinImage(data66.UnitParam, data66.GetSelectedSkin(-1), data66.CurrentJob.JobID);
            return;
        Label_A784:
            if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle == null)
            {
                goto Label_A817;
            }
            data67 = GlobalVars.VersusCpu;
            if (((data67 == null) || (data67.Units == null)) || (((int) data67.Units.Length) <= 0))
            {
                goto Label_A817;
            }
            data68 = data67.Units[0];
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.UnitSkinImage(data68.UnitParam, data68.GetSelectedSkin(-1), data68.CurrentJob.JobID);
            return;
        Label_A817:
            this.ResetToDefault();
            return;
        Label_A81E:
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_A87A;
            }
            param80 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
            if (param80 == null)
            {
                goto Label_A8D3;
            }
            this.SetTextValue(param80.playerName);
            return;
            goto Label_A8D3;
        Label_A87A:
            param81 = this.GetVersusPlayerParam();
            if (param81 == null)
            {
                goto Label_A89D;
            }
            this.SetTextValue(param81.playerName);
            return;
        Label_A89D:
            if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle == null)
            {
                goto Label_A8D3;
            }
            data69 = GlobalVars.VersusCpu;
            if (data69 == null)
            {
                goto Label_A8D3;
            }
            this.SetTextValue(data69.Name);
            return;
        Label_A8D3:
            this.ResetToDefault();
            return;
        Label_A8DA:
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_A93B;
            }
            param83 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
            if (param83 == null)
            {
                goto Label_A999;
            }
            this.SetTextValue(&param83.playerLevel.ToString());
            return;
            goto Label_A999;
        Label_A93B:
            param84 = this.GetVersusPlayerParam();
            if (param84 == null)
            {
                goto Label_A963;
            }
            this.SetTextValue(&param84.playerLevel.ToString());
            return;
        Label_A963:
            if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle == null)
            {
                goto Label_A999;
            }
            data70 = GlobalVars.VersusCpu;
            if (data70 == null)
            {
                goto Label_A999;
            }
            this.SetTextValue(data70.Lv);
            return;
        Label_A999:
            this.ResetToDefault();
            return;
        Label_A9A0:
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_AA01;
            }
            param86 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam().players[this.InstanceType];
            if (param86 == null)
            {
                goto Label_AA5F;
            }
            this.SetTextValue(&param86.totalAtk.ToString());
            return;
            goto Label_AA5F;
        Label_AA01:
            param87 = this.GetVersusPlayerParam();
            if (param87 == null)
            {
                goto Label_AA29;
            }
            this.SetTextValue(&param87.totalAtk.ToString());
            return;
        Label_AA29:
            if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle == null)
            {
                goto Label_AA5F;
            }
            data71 = GlobalVars.VersusCpu;
            if (data71 == null)
            {
                goto Label_AA5F;
            }
            this.SetTextValue(data71.Score);
            return;
        Label_AA5F:
            this.ResetToDefault();
            return;
        Label_AA66:
            param88 = this.GetVersusPlayerParam();
            if (param88 == null)
            {
                goto Label_AA8E;
            }
            this.SetTextValue(&param88.totalStatus.ToString());
            return;
        Label_AA8E:
            this.ResetToDefault();
            return;
        Label_AA95:
            battle11 = SceneBattle.Instance;
            if ((battle11 != null) == null)
            {
                goto Label_AAF6;
            }
            core = battle11.Battle;
            if (core == null)
            {
                goto Label_AAF6;
            }
            record5 = core.GetQuestRecord();
            if (record5 == null)
            {
                goto Label_AAF6;
            }
            base.get_gameObject().SetActive(record5.result == this.InstanceType);
            return;
        Label_AAF6:
            this.ResetToDefault();
            return;
        Label_AAFD:
            return;
        Label_AAFE:
            return;
        Label_AAFF:
            return;
        Label_AB00:
            return;
        Label_AB01:
            return;
        Label_AB02:
            return;
        Label_AB03:
            return;
        Label_AB04:
            image2 = base.GetComponent<Image>();
            if ((image2 == null) == null)
            {
                goto Label_AB2A;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_AB2A:
            param89 = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if ((param89 == null) || (string.IsNullOrEmpty(param89.VersusThumnail) != null))
            {
                goto Label_AB99;
            }
            sheet2 = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if ((sheet2 != null) == null)
            {
                goto Label_AB99;
            }
            image2.set_sprite(sheet2.GetSprite(param89.VersusThumnail));
            image2.set_enabled(1);
            return;
        Label_AB99:
            image2.set_sprite(null);
            image2.set_enabled(0);
            return;
        Label_ABAE:
            image3 = base.GetComponent<Image>();
            if ((image3 == null) == null)
            {
                goto Label_ABD4;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_ABD4:
            param90 = DataSource.FindDataOfClass<VersusMapParam>(base.get_gameObject(), null);
            if (((param90 == null) || (param90.quest == null)) || (string.IsNullOrEmpty(param90.quest.VersusThumnail) != null))
            {
                goto Label_AC51;
            }
            sheet3 = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if ((sheet3 != null) == null)
            {
                goto Label_AC51;
            }
            image3.set_sprite(sheet3.GetSprite(param90.quest.VersusThumnail));
            return;
        Label_AC51:
            image3.set_sprite(null);
            image3.get_gameObject().SetActive(0);
            return;
        Label_AC6B:
            param91 = DataSource.FindDataOfClass<VersusMapParam>(base.get_gameObject(), null);
            if (param91 == null)
            {
                goto Label_AC9D;
            }
            this.SetTextValue(param91.quest.name);
            goto Label_ACA3;
        Label_AC9D:
            this.ResetToDefault();
        Label_ACA3:
            return;
        Label_ACA4:
            param92 = DataSource.FindDataOfClass<VersusMapParam>(base.get_gameObject(), null);
            if (param92 == null)
            {
                goto Label_ACD6;
            }
            base.get_gameObject().SetActive(param92.selected);
            goto Label_ACE2;
        Label_ACD6:
            base.get_gameObject().SetActive(0);
        Label_ACE2:
            return;
        Label_ACE3:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.ArenaCoin);
            return;
        Label_ACF9:
            this.SetTextValue(MonoSingleton<GameManager>.Instance.Player.MultiCoin);
            return;
        Label_AD0F:
            battle12 = SceneBattle.Instance;
            if (((battle12 != null) == null) || (battle12.Battle == null))
            {
                goto Label_AD6F;
            }
            if (battle12.Battle.GetQuestResult() != 1)
            {
                goto Label_AD5E;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_WIN"));
            goto Label_AD6E;
        Label_AD5E:
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_JOIN"));
        Label_AD6E:
            return;
        Label_AD6F:
            this.ResetToDefault();
            return;
        Label_AD76:
            data72 = MonoSingleton<GameManager>.Instance.Player;
            base.get_gameObject().SetActive(data72.VersusSeazonGiftReceipt);
            return;
        Label_AD99:
            manager = MonoSingleton<GameManager>.Instance;
            battle13 = SceneBattle.Instance;
            if (((battle13 != null) == null) || (battle13.CurrentQuest == null))
            {
                goto Label_AE0E;
            }
            core2 = battle13.Battle;
            if (core2 == null)
            {
                goto Label_AE0E;
            }
            record6 = core2.GetQuestRecord();
            if (record6 == null)
            {
                goto Label_AE0E;
            }
            this.SetTextValue(record6.pvpcoin);
            return;
        Label_AE0E:
            this.ResetToDefault();
            return;
        Label_AE15:
            manager = MonoSingleton<GameManager>.Instance;
            str53 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_COIN"), (int) manager.VersusCoinRemainCnt);
            this.SetTextValue(str53);
            return;
        Label_AE44:
            manager = MonoSingleton<GameManager>.Instance;
            if (this.Index != null)
            {
                goto Label_AE6B;
            }
            base.get_gameObject().SetActive(manager.VersusTowerMatchBegin);
            goto Label_AE8B;
        Label_AE6B:
            if (this.Index != 1)
            {
                goto Label_AE8B;
            }
            base.get_gameObject().SetActive(manager.VersusTowerMatchBegin == 0);
        Label_AE8B:
            return;
        Label_AE8C:
            if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle == null)
            {
                goto Label_AEB0;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_CPU"));
            goto Label_AF2A;
        Label_AEB0:
            if (GlobalVars.SelectedMultiPlayVersusType != null)
            {
                goto Label_AECF;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_FREE"));
            goto Label_AF2A;
        Label_AECF:
            if (GlobalVars.SelectedMultiPlayVersusType != 1)
            {
                goto Label_AEEF;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_TOWER"));
            goto Label_AF2A;
        Label_AEEF:
            if (GlobalVars.SelectedMultiPlayVersusType != 2)
            {
                goto Label_AF0F;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND"));
            goto Label_AF2A;
        Label_AF0F:
            if (GlobalVars.SelectedMultiPlayVersusType != 3)
            {
                goto Label_AF2A;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_RANKMATCH"));
        Label_AF2A:
            return;
        Label_AF2B:
            data73 = MonoSingleton<GameManager>.Instance.Player;
            battle14 = SceneBattle.Instance;
            if (((battle14 != null) == null) || (data73 == null))
            {
                goto Label_AFB2;
            }
            core3 = battle14.Battle;
            if (core3 == null)
            {
                goto Label_AFB2;
            }
            record7 = core3.GetQuestRecord();
            if (record7 == null)
            {
                goto Label_AFB2;
            }
            base.get_gameObject().SetActive((record7.result != 1) ? 0 : (data73.VersusTowerWinBonus > 0));
            return;
        Label_AFB2:
            this.ResetToDefault();
            return;
        Label_AFB9:
            storeyb = new <InternalUpdateValue>c__AnonStorey21B();
            storeyb.bs = SceneBattle.Instance;
            if ((((storeyb.bs == null) == null) && (storeyb.bs.Battle != null)) && (storeyb.bs.Battle.CurrentUnit != null))
            {
                goto Label_B016;
            }
            this.ResetToDefault();
            return;
        Label_B016:
            photon15 = PunMonoSingleton<MyPhoton>.Instance;
            list19 = photon15.GetMyPlayersStarted();
            storeyb.param = (list19 != null) ? list19.Find(new Predicate<JSON_MyPhotonPlayerParam>(storeyb.<>m__10D)) : null;
            list20 = photon15.GetRoomPlayerList();
            player11 = ((list20 != null) && (storeyb.param != null)) ? list20.Find(new Predicate<MyPhoton.MyPlayer>(storeyb.<>m__10E)) : null;
            if (MonoSingleton<GameManager>.Instance.AudienceMode == null)
            {
                goto Label_B0C2;
            }
            base.get_gameObject().SetActive(1);
            goto Label_B110;
        Label_B0C2:
            if (this.Index != null)
            {
                goto Label_B0E4;
            }
            base.get_gameObject().SetActive(player11 == null);
            goto Label_B110;
        Label_B0E4:
            if (this.Index != 1)
            {
                goto Label_B10A;
            }
            base.get_gameObject().SetActive((player11 == null) == 0);
            goto Label_B110;
        Label_B10A:
            this.ResetToDefault();
        Label_B110:
            return;
        Label_B111:
            battle15 = SceneBattle.Instance;
            if ((((battle15 == null) == null) && (battle15.Battle != null)) && (battle15.Battle.CurrentUnit != null))
            {
                goto Label_B151;
            }
            this.ResetToDefault();
            return;
        Label_B151:
            result = battle15.CheckAudienceResult();
            if (result != null)
            {
                goto Label_B16E;
            }
            this.ResetToDefault();
            return;
        Label_B16E:
            if (this.Index != null)
            {
                goto Label_B190;
            }
            base.get_gameObject().SetActive(result == 1);
            goto Label_B1AE;
        Label_B190:
            if (this.Index != 1)
            {
                goto Label_B1AE;
            }
            base.get_gameObject().SetActive(result == 2);
        Label_B1AE:
            return;
        Label_B1AF:
            base.get_gameObject().SetActive(MonoSingleton<GameManager>.Instance.AudienceMode);
            return;
        Label_B1C5:
            room4 = MonoSingleton<GameManager>.Instance.AudienceRoom;
            if (room4 == null)
            {
                goto Label_B262;
            }
            if (room4.name.IndexOf("_free") == -1)
            {
                goto Label_B20A;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_FREE"));
            goto Label_B261;
        Label_B20A:
            if (room4.name.IndexOf("_tower") == -1)
            {
                goto Label_B238;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_TOWER"));
            goto Label_B261;
        Label_B238:
            if (room4.name.IndexOf("_friend") == -1)
            {
                goto Label_B261;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_VERSUS_ADUIENCE_FRIEND"));
        Label_B261:
            return;
        Label_B262:
            this.ResetToDefault();
            return;
        Label_B269:
            str54 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_TOWER_NOW_FLOOR"), GameUtility.HalfNum2FullNum(&MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor.ToString()));
            this.SetTextValue(str54);
            return;
        Label_B2A8:
            base.get_gameObject().SetActive(GlobalVars.SelectedMultiPlayVersusType == 1);
            return;
        Label_B2BC:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.AudienceRoom == null)
            {
                goto Label_B333;
            }
            num181 = manager.AudienceRoom.audienceMax;
            param94 = manager.AudienceManager.GetRoomParam();
            if (param94 == null)
            {
                goto Label_B333;
            }
            str55 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_NUM"), GameUtility.HalfNum2FullNum(&param94.audienceNum.ToString()), GameUtility.HalfNum2FullNum(&num181.ToString()));
            this.SetTextValue(str55);
            return;
        Label_B333:
            this.ResetToDefault();
            return;
        Label_B33A:
            if (GlobalVars.VersusCpu == null)
            {
                goto Label_B37D;
            }
            array = base.get_gameObject().GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_B37D;
            }
            array.ImageIndex = GlobalVars.VersusCpu.Get().Deck - 1;
            return;
        Label_B37D:
            this.ResetToDefault();
            return;
        Label_B384:
            data74 = DataSource.FindDataOfClass<VersusCpuData>(base.get_gameObject(), null);
            if (data74 == null)
            {
                goto Label_B3AD;
            }
            this.SetTextValue(data74.Name);
            return;
        Label_B3AD:
            this.ResetToDefault();
            return;
        Label_B3B4:
            data75 = DataSource.FindDataOfClass<VersusCpuData>(base.get_gameObject(), null);
            if (data75 == null)
            {
                goto Label_B3DD;
            }
            this.SetTextValue(data75.Lv);
            return;
        Label_B3DD:
            this.ResetToDefault();
            return;
        Label_B3E4:
            data76 = DataSource.FindDataOfClass<VersusCpuData>(base.get_gameObject(), null);
            if (data76 == null)
            {
                goto Label_B40D;
            }
            this.SetTextValue(data76.Score);
            return;
        Label_B40D:
            this.ResetToDefault();
            return;
        Label_B414:
            data77 = this.GetArtifactData();
            if (data77 == null)
            {
                goto Label_B4C2;
            }
            results = data77.CheckEnableRarityUp();
            str56 = null;
            if ((results & 8) == null)
            {
                goto Label_B452;
            }
            str56 = "sys.ARTI_RARITYUP_MAX";
            goto Label_B498;
        Label_B452:
            if ((results & 1) == null)
            {
                goto Label_B46B;
            }
            str56 = "sys.ARTI_RARITYUP_NOLV";
            goto Label_B498;
        Label_B46B:
            if ((results & 2) == null)
            {
                goto Label_B484;
            }
            str56 = "sys.ARTI_RARITYUP_NOGOLD";
            goto Label_B498;
        Label_B484:
            if ((results & 4) == null)
            {
                goto Label_B498;
            }
            str56 = "sys.ARTI_RARITYUP_NOMTRL";
        Label_B498:
            if (string.IsNullOrEmpty(str56) != null)
            {
                goto Label_B4C2;
            }
            base.get_gameObject().SetActive(1);
            this.SetTextValue(LocalizedText.Get(str56));
            return;
        Label_B4C2:
            base.get_gameObject().SetActive(0);
            return;
        Label_B4CF:
            flag19 = 0;
            data78 = this.GetArtifactData();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (((manager != null) == null) || (data78 == null))
            {
                goto Label_B52C;
            }
            data78 = manager.Player.FindArtifactByUniqueID(data78.UniqueID);
            if (data78 == null)
            {
                goto Label_B52C;
            }
            flag19 = data78.IsFavorite;
        Label_B52C:
            if (this.Index != 1)
            {
                goto Label_B543;
            }
            flag19 = flag19 == 0;
        Label_B543:
            base.get_gameObject().SetActive(flag19);
            return;
        Label_B553:
            data79 = this.GetArtifactData();
            if (data79 == null)
            {
                goto Label_B5A5;
            }
            flag20 = data79.CheckEnableRarityUp() == 0;
            if (flag20 == null)
            {
                goto Label_B595;
            }
            this.SetImageIndex(data79.Rarity + 1);
        Label_B595:
            base.get_gameObject().SetActive(flag20);
            return;
        Label_B5A5:
            base.get_gameObject().SetActive(0);
            return;
        Label_B5B2:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B5FF;
            }
            flag21 = param.CheckDisableContinue();
            if (this.Index != null)
            {
                goto Label_B5E8;
            }
            base.get_gameObject().SetActive(flag21);
            goto Label_B5FA;
        Label_B5E8:
            base.get_gameObject().SetActive(flag21 == 0);
        Label_B5FA:
            goto Label_B60B;
        Label_B5FF:
            base.get_gameObject().SetActive(0);
        Label_B60B:
            return;
        Label_B60C:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B66D;
            }
            flag22 = (param.DamageUpprPl != null) ? 1 : ((param.DamageUpprEn == 0) == 0);
            if (this.Index != null)
            {
                goto Label_B656;
            }
            base.get_gameObject().SetActive(flag22);
            goto Label_B668;
        Label_B656:
            base.get_gameObject().SetActive(flag22 == 0);
        Label_B668:
            goto Label_B679;
        Label_B66D:
            base.get_gameObject().SetActive(0);
        Label_B679:
            return;
        Label_B67A:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B79C;
            }
            str57 = LocalizedText.Get("quest_info." + param.iname);
            if ((str57 == param.iname) == null)
            {
                goto Label_B6C2;
            }
            str57 = string.Empty;
            goto Label_B6D9;
        Label_B6C2:
            str57 = LocalizedText.Get("sys.PARTYEDITOR_COND_DETAIL") + str57;
        Label_B6D9:
            LocalizedText.UnloadTable("quest_info");
            if (string.IsNullOrEmpty(str57) != null)
            {
                goto Label_B703;
            }
            str57 = str57 + "\n";
        Label_B703:
            list21 = param.GetAddQuestInfo(1);
            if (list21.Count == null)
            {
                goto Label_B792;
            }
            str58 = string.Empty;
            num182 = 0;
            goto Label_B76F;
        Label_B72F:
            if (num182 <= 0)
            {
                goto Label_B74B;
            }
            str58 = str58 + "\n";
        Label_B74B:
            str58 = str58 + list21[num182];
            num182 += 1;
        Label_B76F:
            if (num182 < list21.Count)
            {
                goto Label_B72F;
            }
            str57 = str57 + str58;
        Label_B792:
            this.SetTextValue(str57);
        Label_B79C:
            return;
        Label_B79D:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B838;
            }
            param95 = null;
            if (param.EntryCondition == null)
            {
                goto Label_B7C9;
            }
            param95 = param.EntryCondition;
            goto Label_B7DE;
        Label_B7C9:
            if (param.EntryConditionCh == null)
            {
                goto Label_B7DE;
            }
            param95 = param.EntryConditionCh;
        Label_B7DE:
            if ((param95 == null) || (param95.party_type != 2))
            {
                goto Label_B838;
            }
            flag23 = (this.GetUnitData() == null) == 0;
            if (this.Index != null)
            {
                goto Label_B825;
            }
            base.get_gameObject().SetActive(flag23);
            goto Label_B837;
        Label_B825:
            base.get_gameObject().SetActive(flag23 == 0);
        Label_B837:
            return;
        Label_B838:
            base.get_gameObject().SetActive(0);
            return;
        Label_B845:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B8AB;
            }
            param96 = null;
            if (param.EntryCondition == null)
            {
                goto Label_B871;
            }
            param96 = param.EntryCondition;
            goto Label_B886;
        Label_B871:
            if (param.EntryConditionCh == null)
            {
                goto Label_B886;
            }
            param96 = param.EntryConditionCh;
        Label_B886:
            if ((param96 == null) || (param96.party_type != 1))
            {
                goto Label_B8AB;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_B8AB:
            base.get_gameObject().SetActive(0);
            return;
        Label_B8B8:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B980;
            }
            if (param.questParty == null)
            {
                goto Label_B927;
            }
            if (<>f__am$cache15 != null)
            {
                goto Label_B8FB;
            }
            <>f__am$cache15 = new Func<PartySlotTypeUnitPair, bool>(GameParameter.<InternalUpdateValue>m__10F);
        Label_B8FB:
            if ((Enumerable.Any<PartySlotTypeUnitPair>(param.questParty.GetMainSubSlots(), <>f__am$cache15) == 0) == null)
            {
                goto Label_B980;
            }
            base.get_gameObject().SetActive(1);
            return;
            goto Label_B980;
        Label_B927:
            param97 = null;
            if (param.EntryCondition == null)
            {
                goto Label_B946;
            }
            param97 = param.EntryCondition;
            goto Label_B95B;
        Label_B946:
            if (param.EntryConditionCh == null)
            {
                goto Label_B95B;
            }
            param97 = param.EntryConditionCh;
        Label_B95B:
            if ((param97 == null) || (param97.party_type != 2))
            {
                goto Label_B980;
            }
            base.get_gameObject().SetActive(1);
            return;
        Label_B980:
            base.get_gameObject().SetActive(0);
            return;
        Label_B98D:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_B9F2;
            }
            selectable = base.get_gameObject().GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_B9F2;
            }
            flag25 = param.state == 2;
            if (this.Index != null)
            {
                goto Label_B9E2;
            }
            selectable.set_interactable(flag25);
            goto Label_B9F2;
        Label_B9E2:
            selectable.set_interactable(flag25 == 0);
        Label_B9F2:
            return;
        Label_B9F3:
            if ((param = this.GetQuestParam()) == null)
            {
                goto Label_BA40;
            }
            flag26 = param.IsUnitChange;
            if (this.Index != null)
            {
                goto Label_BA29;
            }
            base.get_gameObject().SetActive(flag26);
            goto Label_BA3B;
        Label_BA29:
            base.get_gameObject().SetActive(flag26 == 0);
        Label_BA3B:
            goto Label_BA4C;
        Label_BA40:
            base.get_gameObject().SetActive(0);
        Label_BA4C:
            return;
        Label_BA4D:
            param98 = this.GetTrickParam();
            if (param98 == null)
            {
                goto Label_BA74;
            }
            this.SetTextValue(param98.Name);
            goto Label_BA7A;
        Label_BA74:
            this.ResetToDefault();
        Label_BA7A:
            return;
        Label_BA7B:
            param99 = this.GetTrickParam();
            if (param99 == null)
            {
                goto Label_BAA2;
            }
            this.SetTextValue(param99.Expr);
            goto Label_BAA8;
        Label_BAA2:
            this.ResetToDefault();
        Label_BAA8:
            return;
        Label_BAA9:
            param100 = this.GetTrickParam();
            if (param100 == null)
            {
                goto Label_BAE7;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.TrickIconUI(param100.MarkerName);
            goto Label_BAED;
        Label_BAE7:
            this.ResetToDefault();
        Label_BAED:
            return;
        Label_BAEE:
            data80 = this.GetOrderData();
            if (data80 == null)
            {
                goto Label_BB30;
            }
            image4 = base.get_gameObject().GetComponent<Image>();
            if (image4 == null)
            {
                goto Label_BB30;
            }
            image4.set_enabled(data80.IsCastSkill);
        Label_BB30:
            return;
        Label_BB31:
            storey.skillParam = null;
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_BB5E;
            }
            storey.skillParam = data6.SkillParam;
            goto Label_BB6D;
        Label_BB5E:
            storey.skillParam = this.GetSkillParam();
        Label_BB6D:
            if (storey.skillParam == null)
            {
                goto Label_BB99;
            }
            this.SetTextValue(storey.skillParam.count);
            goto Label_BB9F;
        Label_BB99:
            this.ResetToDefault();
        Label_BB9F:
            return;
        Label_BBA0:
            storey.skillParam = null;
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_BBCD;
            }
            storey.skillParam = data6.SkillParam;
            goto Label_BBDC;
        Label_BBCD:
            storey.skillParam = this.GetSkillParam();
        Label_BBDC:
            num183 = 0;
            if (storey.skillParam == null)
            {
                goto Label_BC01;
            }
            num183 = storey.skillParam.element_type;
        Label_BC01:
            if (num183 == null)
            {
                goto Label_BC25;
            }
            this.SetImageIndex(num183);
            base.get_gameObject().SetActive(1);
            goto Label_BC37;
        Label_BC25:
            this.ResetToDefault();
            base.get_gameObject().SetActive(0);
        Label_BC37:
            return;
        Label_BC38:
            storey.skillParam = null;
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_BC65;
            }
            storey.skillParam = data6.SkillParam;
            goto Label_BC74;
        Label_BC65:
            storey.skillParam = this.GetSkillParam();
        Label_BC74:
            num184 = 0;
            if (storey.skillParam == null)
            {
                goto Label_BC99;
            }
            num184 = storey.skillParam.attack_detail;
        Label_BC99:
            if (num184 == null)
            {
                goto Label_BCBD;
            }
            this.SetImageIndex(num184);
            base.get_gameObject().SetActive(1);
            goto Label_BCCF;
        Label_BCBD:
            this.ResetToDefault();
            base.get_gameObject().SetActive(0);
        Label_BCCF:
            return;
        Label_BCD0:
            storey.skillParam = null;
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_BCFD;
            }
            storey.skillParam = data6.SkillParam;
            goto Label_BD0C;
        Label_BCFD:
            storey.skillParam = this.GetSkillParam();
        Label_BD0C:
            num185 = 0;
            if (storey.skillParam == null)
            {
                goto Label_BD31;
            }
            num185 = storey.skillParam.attack_type;
        Label_BD31:
            if (num185 == null)
            {
                goto Label_BD55;
            }
            this.SetImageIndex(num185);
            base.get_gameObject().SetActive(1);
            goto Label_BD67;
        Label_BD55:
            this.ResetToDefault();
            base.get_gameObject().SetActive(0);
        Label_BD67:
            return;
        Label_BD68:
            data81 = WeatherData.CurrentWeatherData;
            base.get_gameObject().SetActive((data81 == null) == 0);
            return;
        Label_BD87:
            param101 = this.GetWeatherParam();
            if (param101 == null)
            {
                goto Label_BDAE;
            }
            this.SetTextValue(param101.Name);
            goto Label_BDB4;
        Label_BDAE:
            this.ResetToDefault();
        Label_BDB4:
            return;
        Label_BDB5:
            param102 = this.GetWeatherParam();
            if ((param102 == null) || (string.IsNullOrEmpty(param102.Icon) != null))
            {
                goto Label_BE06;
            }
            GameUtility.RequireComponent<IconLoader>(base.get_gameObject()).ResourcePath = AssetPath.WeatherIcon(param102.Icon);
            goto Label_BE0C;
        Label_BE06:
            this.ResetToDefault();
        Label_BE0C:
            return;
        Label_BE0D:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_BE3A;
            }
            this.SetTextValue(data2.Status.param.mov);
            goto Label_BE40;
        Label_BE3A:
            this.ResetToDefault();
        Label_BE40:
            return;
        Label_BE41:
            if ((data2 = this.GetUnitData()) == null)
            {
                goto Label_BE6E;
            }
            this.SetTextValue(data2.Status.param.jmp);
            goto Label_BE74;
        Label_BE6E:
            this.ResetToDefault();
        Label_BE74:
            return;
        Label_BE75:
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_BE99;

                case 1:
                    goto Label_BE99;

                case 2:
                    goto Label_BEC7;
            }
            goto Label_BF12;
        Label_BE99:
            param103 = this.GetArtifactParam();
            if ((param103 == null) || (this.LoadArtifactIcon(param103) == null))
            {
                goto Label_BEBC;
            }
            return;
        Label_BEBC:
            this.ResetToDefault();
            goto Label_BF12;
        Label_BEC7:
            data82 = DataSource.FindDataOfClass<ArtifactRewardData>(base.get_gameObject(), null);
            if (data82 != null)
            {
                goto Label_BEE1;
            }
            return;
        Label_BEE1:
            param104 = data82.ArtifactParam;
            if ((param104 == null) || (this.LoadArtifactIcon(param104) == null))
            {
                goto Label_BF07;
            }
            return;
        Label_BF07:
            this.ResetToDefault();
        Label_BF12:
            return;
        Label_BF13:
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_BF37;

                case 1:
                    goto Label_BF37;

                case 2:
                    goto Label_BF48;
            }
            goto Label_BF87;
        Label_BF37:
            this.SetArtifactFrame(this.GetArtifactParam());
            goto Label_BF87;
        Label_BF48:
            data83 = DataSource.FindDataOfClass<ArtifactRewardData>(base.get_gameObject(), null);
            if (data83 != null)
            {
                goto Label_BF62;
            }
            return;
        Label_BF62:
            param105 = data83.ArtifactParam;
            if (param105 == null)
            {
                goto Label_BF87;
            }
            this.SetArtifactFrame(param105);
        Label_BF87:
            return;
        Label_BF88:
            switch (this.InstanceType)
            {
                case 0:
                    goto Label_BFAC;

                case 1:
                    goto Label_BFB1;

                case 2:
                    goto Label_C015;
            }
            goto Label_C05A;
        Label_BFAC:
            goto Label_C05A;
        Label_BFB1:
            param106 = this.GetQuestParamAuto();
            if (((param106 == null) || (0 > this.Index)) || ((param106.bonusObjective == null) || (this.Index >= ((int) param106.bonusObjective.Length))))
            {
                goto Label_C05A;
            }
            this.SetTextValue(param106.bonusObjective[this.Index].itemNum);
            return;
            goto Label_C05A;
        Label_C015:
            data84 = DataSource.FindDataOfClass<ArtifactRewardData>(base.get_gameObject(), null);
            if (data84 != null)
            {
                goto Label_C02F;
            }
            return;
        Label_C02F:
            if (data84.ArtifactParam == null)
            {
                goto Label_C05A;
            }
            this.SetTextValue(data84.Num);
            return;
        Label_C05A:
            this.ResetToDefault();
            return;
        Label_C061:
            room5 = DataSource.FindDataOfClass<MultiPlayAPIRoom>(base.get_gameObject(), null);
            if (room5 == null)
            {
                goto Label_C276;
            }
            if (room5.limit != null)
            {
                goto Label_C0C8;
            }
            this.SetTextValue("-");
            if ((this.mText != null) == null)
            {
                goto Label_C275;
            }
            this.mText.set_color(new Color(0f, 0f, 0f));
            goto Label_C275;
        Label_C0C8:
            if (room5.unitlv != null)
            {
                goto Label_C11B;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"));
            if ((this.mText != null) == null)
            {
                goto Label_C275;
            }
            this.mText.set_color(new Color(0f, 0f, 0f));
            goto Label_C275;
        Label_C11B:
            this.SetTextValue(((int) room5.unitlv) + LocalizedText.Get("sys.MULTI_JOINLIMIT_OVER"));
            if ((this.mText != null) == null)
            {
                goto Label_C275;
            }
            manager = MonoSingleton<GameManager>.Instance;
            data85 = manager.Player;
            data86 = data85.Partys[2];
            param108 = manager.FindQuest(room5.quest.iname);
            flag27 = 1;
            if ((data86 == null) || (param108 == null))
            {
                goto Label_C229;
            }
            num186 = 0;
            goto Label_C212;
        Label_C1AB:
            num187 = data86.GetUnitUniqueID(0);
            if (num187 > 0L)
            {
                goto Label_C1CE;
            }
            flag27 = 0;
            goto Label_C229;
        Label_C1CE:
            data87 = data85.FindUnitDataByUniqueID(num187);
            if (data87 == null)
            {
                goto Label_C208;
            }
            flag27 &= (data87.CalcLevel() < room5.unitlv) == 0;
        Label_C208:
            num186 += 1;
        Label_C212:
            if (num186 < param108.unitNum)
            {
                goto Label_C1AB;
            }
        Label_C229:
            if (flag27 == null)
            {
                goto Label_C256;
            }
            this.mText.set_color(new Color(0f, 0f, 0f));
            goto Label_C275;
        Label_C256:
            this.mText.set_color(new Color(1f, 0f, 0f));
        Label_C275:
            return;
        Label_C276:
            this.ResetToDefault();
            return;
        Label_C27D:
            room6 = DataSource.FindDataOfClass<MultiPlayAPIRoom>(base.get_gameObject(), null);
            if (room6 == null)
            {
                goto Label_C2E8;
            }
            if (room6.limit != null)
            {
                goto Label_C2B4;
            }
            this.SetTextValue("-");
            goto Label_C2E7;
        Label_C2B4:
            if (room6.clear != null)
            {
                goto Label_C2D7;
            }
            this.SetTextValue(LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"));
            goto Label_C2E7;
        Label_C2D7:
            this.SetTextValue(LocalizedText.Get("sys.MULTI_JOIN_LIMIT_ONLY_CLEAR"));
        Label_C2E7:
            return;
        Label_C2E8:
            this.ResetToDefault();
            return;
        Label_C2EF:
            storeyc = new <InternalUpdateValue>c__AnonStorey21C();
            manager = MonoSingleton<GameManager>.Instance;
            storeyc.bs = SceneBattle.Instance;
            if ((((storeyc.bs == null) == null) && (storeyc.bs.Battle != null)) && (storeyc.bs.Battle.CurrentUnit != null))
            {
                goto Label_C352;
            }
            this.ResetToDefault();
            return;
        Label_C352:
            if (manager.AudienceMode == null)
            {
                goto Label_C45E;
            }
            param109 = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
            if ((storeyc.bs.Battle.CurrentUnit.OwnerPlayerIndex > 0) && (storeyc.bs.Battle.CurrentUnit.OwnerPlayerIndex <= 2))
            {
                goto Label_C3BB;
            }
            this.ResetToDefault();
            return;
        Label_C3BB:
            param110 = param109.players[storeyc.bs.Battle.CurrentUnit.OwnerPlayerIndex - 1];
            if (param110 == null)
            {
                goto Label_C69D;
            }
            str59 = param110.playerName;
            if (this.Index != 1)
            {
                goto Label_C426;
            }
            str59 = param110.playerName + LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
            goto Label_C44E;
        Label_C426:
            if (this.Index != 2)
            {
                goto Label_C44E;
            }
            str59 = param110.playerName + LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
        Label_C44E:
            this.SetTextValue(str59);
            return;
            goto Label_C69D;
        Label_C45E:
            if (manager.IsVSCpuBattle == null)
            {
                goto Label_C541;
            }
            str60 = manager.Player.Name;
            flag28 = (storeyc.bs.Battle.CurrentUnit.OwnerPlayerIndex == 1) == 0;
            if (flag28 == null)
            {
                goto Label_C4C7;
            }
            data88 = GlobalVars.VersusCpu;
            if (data88 == null)
            {
                goto Label_C4C7;
            }
            str60 = data88.Name;
        Label_C4C7:
            if (this.Index != 1)
            {
                goto Label_C4EF;
            }
            str60 = str60 + LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
            goto Label_C532;
        Label_C4EF:
            if (this.Index != 2)
            {
                goto Label_C532;
            }
            if (flag28 == null)
            {
                goto Label_C51B;
            }
            str60 = str60 + LocalizedText.Get("sys.MULTI_VS_CPU");
        Label_C51B:
            str60 = str60 + LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
        Label_C532:
            this.SetTextValue(str60);
            goto Label_C69D;
        Label_C541:
            photon16 = PunMonoSingleton<MyPhoton>.Instance;
            list22 = photon16.GetMyPlayersStarted();
            param111 = (list22 != null) ? list22.Find(new Predicate<JSON_MyPhotonPlayerParam>(storeyc.<>m__110)) : null;
            list23 = photon16.GetRoomPlayerList();
            player12 = (param111 != null) ? photon16.FindPlayer(list23, param111.playerID, param111.playerIndex) : null;
            if (param111 != null)
            {
                goto Label_C5D5;
            }
            this.ResetToDefault();
            goto Label_C69D;
        Label_C5D5:
            str61 = param111.playerName;
            if (this.Index != null)
            {
                goto Label_C63E;
            }
            if (player12 != null)
            {
                goto Label_C61A;
            }
            this.mText.set_color(new Color(0.5f, 0.5f, 0.5f));
            goto Label_C639;
        Label_C61A:
            this.mText.set_color(new Color(1f, 1f, 1f));
        Label_C639:
            goto Label_C693;
        Label_C63E:
            if (this.Index != 1)
            {
                goto Label_C66B;
            }
            str61 = param111.playerName + LocalizedText.Get("sys.MULTI_BATTLE_THINKING");
            goto Label_C693;
        Label_C66B:
            if (this.Index != 2)
            {
                goto Label_C693;
            }
            str61 = param111.playerName + LocalizedText.Get("sys.MULTI_BATTLE_NOWTURN");
        Label_C693:
            this.SetTextValue(str61);
        Label_C69D:
            return;
        Label_C69E:
            param112 = this.GetRoomParam();
            param113 = this.GetRoomPlayerParam();
            if (((param113 != null) && (param113.units != null)) && (param112 != null))
            {
                goto Label_C6D9;
            }
            this.ResetToDefault();
            return;
        Label_C6D9:
            num188 = 0;
            num189 = param112.GetUnitSlotNum(param113.playerIndex);
            num190 = 0;
            goto Label_C7A1;
        Label_C6FE:
            if (param113.units[num190].slotID >= num189)
            {
                goto Label_C797;
            }
            if (param113.units[num190].unit != null)
            {
                goto Label_C737;
            }
            goto Label_C797;
        Label_C737:
            num188 += param113.units[num190].unit.Status.param.atk;
            num188 += param113.units[num190].unit.Status.param.mag;
        Label_C797:
            num190 += 1;
        Label_C7A1:
            if (num190 < ((int) param113.units.Length))
            {
                goto Label_C6FE;
            }
            this.SetTextValue(num188);
            return;
        Label_C7C0:
            list24 = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
            if (list24 != null)
            {
                goto Label_C7EC;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_C7EC:
            player13 = null;
            if (<>f__am$cache16 != null)
            {
                goto Label_C80D;
            }
            <>f__am$cache16 = new Predicate<MyPhoton.MyPlayer>(GameParameter.<InternalUpdateValue>m__111);
        Label_C80D:
            player13 = list24.Find(<>f__am$cache16);
            flag29 = 0;
            if (player13 == null)
            {
                goto Label_C855;
            }
            flag29 = JSON_MyPhotonPlayerParam.Parse(player13.json).state == this.InstanceType;
            goto Label_C862;
        Label_C855:
            base.get_gameObject().SetActive(0);
            return;
        Label_C862:
            num233 = this.Index;
            if (num233 == null)
            {
                goto Label_C884;
            }
            if (num233 == 1)
            {
                goto Label_C894;
            }
            goto Label_C8D5;
        Label_C884:
            base.get_gameObject().SetActive(flag29);
            return;
        Label_C894:
            button4 = base.get_gameObject().GetComponent<Button>();
            if ((button4 != null) == null)
            {
                goto Label_C8C4;
            }
            button4.set_interactable(flag29);
            goto Label_C8D4;
        Label_C8C4:
            base.get_gameObject().SetActive(flag29);
            return;
        Label_C8D4:
            return;
        Label_C8D5:
            base.get_gameObject().SetActive(0);
            return;
        Label_C8E2:
            text = base.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_C96C;
            }
            photon18 = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon18 != null) == null)
            {
                goto Label_C966;
            }
            room7 = photon18.GetCurrentRoom();
            if (room7 == null)
            {
                goto Label_C966;
            }
            param115 = JSON_MyPhotonRoomParam.Parse(room7.json);
            if (param115 == null)
            {
                goto Label_C966;
            }
            text.set_text(((int) param115.challegedMTFloor) + "!");
            return;
        Label_C966:
            this.ResetToDefault();
        Label_C96C:
            return;
        Label_C96D:
            param116 = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param116 == null)
            {
                goto Label_C99B;
            }
            base.get_gameObject().SetActive(param116.IsMultiLeaderSkill);
            return;
        Label_C99B:
            base.get_gameObject().SetActive(0);
            return;
        Label_C9A8:
            room = this.GetRoom();
            if (room.floor > 0)
            {
                goto Label_C9CD;
            }
            this.SetTextValue(string.Empty);
            goto Label_C9EE;
        Label_C9CD:
            this.SetTextValue(string.Format(LocalizedText.Get("sys.MULTI_TOWER_FLOOR_NAME"), (int) room.floor));
        Label_C9EE:
            return;
        Label_C9EF:
            room = this.GetRoom();
            transform = base.get_transform().Find("Clear");
            transform2 = base.get_transform().Find("NotClear");
            flag30 = room.is_clear == 1;
            if ((transform != null) == null)
            {
                goto Label_CA4E;
            }
            transform.get_gameObject().SetActive(flag30);
        Label_CA4E:
            if ((transform2 != null) == null)
            {
                goto Label_CA72;
            }
            transform2.get_gameObject().SetActive(flag30 == 0);
        Label_CA72:
            return;
        Label_CA73:
            if (((param = this.GetQuestParam()) == null) || (param.bonusObjective == null))
            {
                goto Label_CAD0;
            }
            this.GetQuestObjectiveCount(param, &num191, &num192);
            if (num191 > 0)
            {
                goto Label_CAA9;
            }
            goto Label_CAD0;
        Label_CAA9:
            if ((num191 <= 0) || (num191 >= num192))
            {
                goto Label_CAC8;
            }
            this.SetImageIndex(0);
            return;
        Label_CAC8:
            this.SetImageIndex(1);
            return;
        Label_CAD0:
            this.ResetToDefault();
            return;
        Label_CAD7:
            if (((param = this.GetQuestParam()) == null) || (param.bonusObjective == null))
            {
                goto Label_CB35;
            }
            this.GetQuestObjectiveCount(param, &num193, &num194);
            this.SetTextValue(num193);
            if (num193 < num194)
            {
                goto Label_CB34;
            }
            this.mText.set_color(new Color(1f, 1f, 0f));
        Label_CB34:
            return;
        Label_CB35:
            this.ResetToDefault();
            return;
        Label_CB3C:
            if (((param = this.GetQuestParam()) == null) || (param.bonusObjective == null))
            {
                goto Label_CB6E;
            }
            this.GetQuestObjectiveCount(param, &num195, &num196);
            this.SetTextValue(num196);
            return;
        Label_CB6E:
            this.ResetToDefault();
            return;
        Label_CB75:
            param117 = this.GetQuestParamAuto();
            if (((param117 == null) || (0 > this.Index)) || ((param117.bonusObjective == null) || (this.Index >= ((int) param117.bonusObjective.Length))))
            {
                goto Label_CBD4;
            }
            this.SetTextValue(param117.bonusObjective[this.Index].itemNum);
            return;
        Label_CBD4:
            this.ResetToDefault();
            return;
        Label_CBDB:
            param118 = this.GetQuestParamAuto();
            if (((param118 == null) || (0 > this.Index)) || ((param118.bonusObjective == null) || (this.Index >= ((int) param118.bonusObjective.Length))))
            {
                goto Label_CC56;
            }
            if (param118.bonusObjective[this.Index].IsProgressMission() == null)
            {
                goto Label_CC49;
            }
            base.get_gameObject().SetActive(1);
            goto Label_CC55;
        Label_CC49:
            base.get_gameObject().SetActive(0);
        Label_CC55:
            return;
        Label_CC56:
            this.ResetToDefault();
            return;
        Label_CC5D:
            param119 = this.GetQuestParamAuto();
            if (((param119 == null) || (0 > this.Index)) || ((param119.bonusObjective == null) || (this.Index >= ((int) param119.bonusObjective.Length))))
            {
                goto Label_CD69;
            }
            if (param119.bonusObjective[this.Index].IsProgressMission() == null)
            {
                goto Label_CD5C;
            }
            if ((param119.mission_values == null) || (this.Index >= ((int) param119.mission_values.Length)))
            {
                goto Label_CD4C;
            }
            num197 = Mathf.Max(param119.GetMissionValue(this.Index), 0);
            flag31 = param119.bonusObjective[this.Index].CheckHomeMissionValueAchievable(num197);
            str62 = GameUtility.ComposeQuestMissionProgressText(param119.bonusObjective[this.Index], num197, flag31);
            this.SetTextValue(str62);
            goto Label_CD57;
        Label_CD4C:
            this.SetTextValue("-");
        Label_CD57:
            goto Label_CD68;
        Label_CD5C:
            base.get_gameObject().SetActive(0);
        Label_CD68:
            return;
        Label_CD69:
            this.ResetToDefault();
            return;
        Label_CD70:
            param120 = this.GetQuestParamAuto();
            if (((param120 == null) || (0 > this.Index)) || ((param120.bonusObjective == null) || (this.Index >= ((int) param120.bonusObjective.Length))))
            {
                goto Label_CE79;
            }
            num198 = 0;
            if (param120.bonusObjective[this.Index].IsProgressMission() == null)
            {
                goto Label_CE6C;
            }
            str63 = GameUtility.GetQuestMissionTextID(param120.bonusObjective[this.Index]);
            if (int.TryParse(param120.bonusObjective[this.Index].TypeParam, &num198) == null)
            {
                goto Label_CE46;
            }
            objArray7 = new object[] { (int) num198 };
            str64 = LocalizedText.Get(str63 + "_PROGRESS_TARGET", objArray7);
            this.SetTextValue(str64);
            goto Label_CE67;
        Label_CE46:
            str65 = LocalizedText.Get(str63 + "_PROGRESS_TARGET");
            this.SetTextValue(str65);
        Label_CE67:
            goto Label_CE78;
        Label_CE6C:
            base.get_gameObject().SetActive(0);
        Label_CE78:
            return;
        Label_CE79:
            this.ResetToDefault();
            return;
        Label_CE80:
            if (((param = this.GetQuestParam()) == null) || (param.HasMission() == null))
            {
                goto Label_CEDD;
            }
            num199 = 0;
            if ((param.IsMissionCompleteALL() == null) || (param.state != 2))
            {
                goto Label_CEBE;
            }
            num199 = 1;
            goto Label_CEC3;
        Label_CEBE:
            num199 = 0;
        Label_CEC3:
            this.SetAnimatorInt("state", num199);
            this.SetImageIndex(num199);
            return;
        Label_CEDD:
            this.ResetToDefault();
            return;
        Label_CEE4:
            if (((param = this.GetQuestParam()) == null) || (param.bonusObjective == null))
            {
                goto Label_CF6D;
            }
            this.GetQuestObjectiveCount(param, &num200, &num201);
            if ((param.IsMissionCompleteALL() == null) || (param.state != 2))
            {
                goto Label_CF2C;
            }
            num200 += 1;
        Label_CF2C:
            num201 += 1;
            this.SetTextValue(num200);
            if (num200 < num201)
            {
                goto Label_CF6C;
            }
            this.mText.set_color(new Color(1f, 1f, 0f));
        Label_CF6C:
            return;
        Label_CF6D:
            this.ResetToDefault();
            return;
        Label_CF74:
            if (((param = this.GetQuestParam()) == null) || (param.bonusObjective == null))
            {
                goto Label_CFB0;
            }
            this.GetQuestObjectiveCount(param, &num202, &num203);
            num203 += 1;
            this.SetTextValue(num203);
            return;
        Label_CFB0:
            this.ResetToDefault();
            return;
        Label_CFB7:
            param121 = this.GetQuestParamAuto();
            if (((param121 == null) || (0 > this.Index)) || ((param121.bonusObjective == null) || (this.Index >= ((int) param121.bonusObjective.Length))))
            {
                goto Label_D106;
            }
            if (param121.bonusObjective[this.Index].IsProgressMission() == null)
            {
                goto Label_D0F9;
            }
            if ((param121.mission_values == null) || (this.Index >= ((int) param121.mission_values.Length)))
            {
                goto Label_D0E9;
            }
            num204 = Mathf.Max(param121.GetMissionValue(this.Index), 0);
            flag32 = param121.bonusObjective[this.Index].CheckHomeMissionValueAchievable(num204);
            color = Color.get_white();
            if (param121.IsMissionClear(this.Index) != null)
            {
                goto Label_D0AF;
            }
            color = (flag32 == null) ? Color.get_red() : Color.get_green();
        Label_D0AF:
            str66 = GameUtility.ComposeQuestMissionProgressText(param121.bonusObjective[this.Index], num204, flag32);
            this.SetTextValue(str66);
            this.SetTextColor(color);
            goto Label_D0F4;
        Label_D0E9:
            this.SetTextValue("-");
        Label_D0F4:
            goto Label_D105;
        Label_D0F9:
            base.get_gameObject().SetActive(0);
        Label_D105:
            return;
        Label_D106:
            this.ResetToDefault();
            return;
        Label_D10D:
            storey.skillParam = null;
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_D13A;
            }
            storey.skillParam = data6.SkillParam;
            goto Label_D149;
        Label_D13A:
            storey.skillParam = this.GetSkillParam();
        Label_D149:
            if (storey.skillParam == null)
            {
                goto Label_D170;
            }
            this.SetTextValue(storey.skillParam.MapEffectDesc);
            goto Label_D176;
        Label_D170:
            this.ResetToDefault();
        Label_D176:
            return;
        Label_D177:
            param122 = this.GetMapEffectParam();
            if (param122 == null)
            {
                goto Label_D19E;
            }
            this.SetTextValue(param122.Name);
            goto Label_D1A4;
        Label_D19E:
            this.ResetToDefault();
        Label_D1A4:
            return;
        Label_D1A5:
            param123 = this.GetJobParam();
            if (param123 == null)
            {
                goto Label_D1CC;
            }
            this.SetTextValue(param123.DescCharacteristic);
            goto Label_D1D2;
        Label_D1CC:
            this.ResetToDefault();
        Label_D1D2:
            return;
        Label_D1D3:
            param124 = this.GetJobParam();
            if (param124 == null)
            {
                goto Label_D203;
            }
            base.get_gameObject().SetActive(string.IsNullOrEmpty(param124.DescCharacteristic) == 0);
            return;
        Label_D203:
            base.get_gameObject().SetActive(0);
            return;
        Label_D210:
            param125 = this.GetJobParam();
            if (param125 == null)
            {
                goto Label_D237;
            }
            this.SetTextValue(param125.DescOther);
            goto Label_D23D;
        Label_D237:
            this.ResetToDefault();
        Label_D23D:
            return;
        Label_D23E:
            param126 = this.GetJobParam();
            if (param126 == null)
            {
                goto Label_D26E;
            }
            base.get_gameObject().SetActive(string.IsNullOrEmpty(param126.DescOther) == 0);
            return;
        Label_D26E:
            base.get_gameObject().SetActive(0);
            return;
        Label_D27B:
            param127 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param127 == null)
            {
                goto Label_D2AB;
            }
            this.SetTextValue(param127.FirstFriendMax);
        Label_D2AB:
            return;
        Label_D2AC:
            data89 = MonoSingleton<GameManager>.Instance.Player;
            if (data89 == null)
            {
                goto Label_D2D2;
            }
            this.SetTextValue(data89.FirstFriendCount);
        Label_D2D2:
            return;
        Label_D2D3:
            image5 = base.GetComponent<Image>();
            if ((image5 == null) == null)
            {
                goto Label_D2F9;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D2F9:
            param128 = DataSource.FindDataOfClass<ChallengeCategoryParam>(base.get_gameObject(), null);
            if ((param128 == null) || (string.IsNullOrEmpty(param128.iname) != null))
            {
                goto Label_D368;
            }
            sheet4 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
            if ((sheet4 != null) == null)
            {
                goto Label_D368;
            }
            image5.set_sprite(sheet4.GetSprite("help/" + param128.iname));
            return;
        Label_D368:
            image5.set_sprite(null);
            return;
        Label_D373:
            image6 = base.GetComponent<Image>();
            if ((image6 == null) == null)
            {
                goto Label_D399;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D399:
            param129 = DataSource.FindDataOfClass<ChallengeCategoryParam>(base.get_gameObject(), null);
            if ((param129 == null) || (string.IsNullOrEmpty(param129.iname) != null))
            {
                goto Label_D408;
            }
            sheet5 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
            if ((sheet5 != null) == null)
            {
                goto Label_D408;
            }
            image6.set_sprite(sheet5.GetSprite("button/" + param129.iname));
            return;
        Label_D408:
            image6.set_sprite(null);
            return;
        Label_D413:
            image7 = base.GetComponent<Image>();
            if ((image7 == null) == null)
            {
                goto Label_D439;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D439:
            param130 = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if ((param130 == null) || (string.IsNullOrEmpty(param130.iname) != null))
            {
                goto Label_D4A8;
            }
            sheet6 = AssetManager.Load<SpriteSheet>("ChallengeMission/ChallengeMission_Images");
            if ((sheet6 != null) == null)
            {
                goto Label_D4A8;
            }
            image7.set_sprite(sheet6.GetSprite("reward/" + param130.iname));
            return;
        Label_D4A8:
            image7.set_sprite(null);
            return;
        Label_D4B3:
            param131 = DataSource.FindDataOfClass<TobiraRecipeParam>(base.get_gameObject(), null);
            if (param131 == null)
            {
                goto Label_D4DB;
            }
            this.SetTextValue(param131.Cost);
        Label_D4DB:
            return;
        Label_D4DC:
            recipe2 = DataSource.FindDataOfClass<UnlockTobiraRecipe>(base.get_gameObject(), null);
            if (recipe2 == null)
            {
                goto Label_D504;
            }
            this.SetTextValue(recipe2.RequiredAmount);
        Label_D504:
            return;
        Label_D505:
            recipe3 = DataSource.FindDataOfClass<UnlockTobiraRecipe>(base.get_gameObject(), null);
            if (recipe3 == null)
            {
                goto Label_D52D;
            }
            this.SetTextValue(recipe3.Amount);
        Label_D52D:
            return;
        Label_D52E:
            recipe4 = DataSource.FindDataOfClass<TobiraEnhanceRecipe>(base.get_gameObject(), null);
            if (recipe4 == null)
            {
                goto Label_D556;
            }
            this.SetTextValue(recipe4.RequiredAmount);
        Label_D556:
            return;
        Label_D557:
            recipe5 = DataSource.FindDataOfClass<TobiraEnhanceRecipe>(base.get_gameObject(), null);
            if (recipe5 == null)
            {
                goto Label_D57F;
            }
            this.SetTextValue(recipe5.Amount);
        Label_D57F:
            return;
        Label_D580:
            data90 = DataSource.FindDataOfClass<TobiraData>(base.get_gameObject(), null);
            if (data90 == null)
            {
                goto Label_D61A;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_D606;
            }
            flag33 = (data90.ViewLv < (this.Index + 1)) == 0;
            num205 = (flag33 == null) ? 0 : 1;
            this.SetImageIndex(num205);
            if (this.ParameterType != 0x89d)
            {
                goto Label_D620;
            }
            base.get_gameObject().SetActive(flag33);
            goto Label_D615;
        Label_D606:
            this.SetTextValue(data90.ViewLv);
        Label_D615:
            goto Label_D620;
        Label_D61A:
            this.ResetToDefault();
        Label_D620:
            return;
        Label_D621:
            param132 = this.GetItemParam();
            image8 = base.GetComponent<Image>();
            if (param132 == null)
            {
                goto Label_D6A8;
            }
            if ((image8 != null) == null)
            {
                goto Label_D6A8;
            }
            settings = GameSettings.Instance;
            num206 = param132.rare;
            if ((settings != null) == null)
            {
                goto Label_D6A8;
            }
            if (num206 < 0)
            {
                goto Label_D6A8;
            }
            if (num206 >= ((int) settings.ArtifactIcon_Rarity.Length))
            {
                goto Label_D6A8;
            }
            image8.set_sprite(settings.ArtifactIcon_Rarity[num206]);
            return;
        Label_D6A8:
            this.ResetToDefault();
            return;
        Label_D6AF:
            if ((param2 = this.GetItemParam()) == null)
            {
                goto Label_D6F2;
            }
            if (string.IsNullOrEmpty(param2.Flavor) != null)
            {
                goto Label_D6E0;
            }
            this.SetTextValue(param2.Flavor);
            goto Label_D6ED;
        Label_D6E0:
            this.SetTextValue(param2.Expr);
        Label_D6ED:
            goto Label_D6F8;
        Label_D6F2:
            this.ResetToDefault();
        Label_D6F8:
            return;
        Label_D6F9:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.RankMatchRank <= 0)
            {
                goto Label_D726;
            }
            this.SetTextValue(manager.Player.RankMatchRank);
            goto Label_D72C;
        Label_D726:
            this.ResetToDefault();
        Label_D72C:
            return;
        Label_D72D:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.RankMatchScore);
            return;
        Label_D745:
            manager = MonoSingleton<GameManager>.Instance;
            num207 = manager.GetNextVersusRankClass(manager.RankMatchScheduleId, manager.Player.RankMatchClass, manager.Player.RankMatchScore);
            if (num207 <= 0)
            {
                goto Label_D78A;
            }
            this.SetTextValue(num207);
            goto Label_D790;
        Label_D78A:
            this.ResetToDefault();
        Label_D790:
            return;
        Label_D791:
            manager = MonoSingleton<GameManager>.Instance;
            num208 = manager.GetMaxBattlePoint(manager.RankMatchScheduleId);
            this.SetTextValue(((int) manager.Player.RankMatchBattlePoint) + "/" + ((int) num208));
            return;
        Label_D7D1:
            manager = MonoSingleton<GameManager>.Instance;
            image9 = base.GetComponent<Image>();
            if ((image9 == null) == null)
            {
                goto Label_D7FD;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D7FD:
            param133 = DataSource.FindDataOfClass<VersusRankClassParam>(base.get_gameObject(), null);
            if (param133 != null)
            {
                goto Label_D82B;
            }
            image9.set_sprite(null);
            image9.set_enabled(0);
            return;
        Label_D82B:
            sheet7 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet7 != null) == null)
            {
                goto Label_D87C;
            }
            image9.set_sprite(sheet7.GetSprite("class_" + ((int) param133.Class)));
            image9.set_enabled(1);
        Label_D87C:
            return;
        Label_D87D:
            image10 = base.GetComponent<Image>();
            if ((image10 == null) == null)
            {
                goto Label_D8A3;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D8A3:
            param134 = DataSource.FindDataOfClass<VersusRankClassParam>(base.get_gameObject(), null);
            if (param134 != null)
            {
                goto Label_D8D1;
            }
            image10.set_sprite(null);
            image10.set_enabled(0);
            return;
        Label_D8D1:
            sheet8 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet8 != null) == null)
            {
                goto Label_D922;
            }
            image10.set_sprite(sheet8.GetSprite("classname_" + ((int) param134.Class)));
            image10.set_enabled(1);
        Label_D922:
            return;
        Label_D923:
            image11 = base.GetComponent<Image>();
            if ((image11 == null) == null)
            {
                goto Label_D949;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_D949:
            param135 = DataSource.FindDataOfClass<VersusRankClassParam>(base.get_gameObject(), null);
            if (param135 != null)
            {
                goto Label_D977;
            }
            image11.set_sprite(null);
            image11.set_enabled(0);
            return;
        Label_D977:
            sheet9 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet9 != null) == null)
            {
                goto Label_D9C8;
            }
            image11.set_sprite(sheet9.GetSprite("classframe_" + ((int) param135.Class)));
            image11.set_enabled(1);
        Label_D9C8:
            return;
        Label_D9C9:
            manager = MonoSingleton<GameManager>.Instance;
            param136 = DataSource.FindDataOfClass<VersusRankClassParam>(base.get_gameObject(), null);
            if (param136 != null)
            {
                goto Label_D9E9;
            }
            return;
        Label_D9E9:
            base.get_gameObject().SetActive(param136.Class == manager.Player.RankMatchClass);
            return;
        Label_DA0B:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.RankMatchRank <= 3)
            {
                goto Label_DA4C;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_RANKING_RANK"), (int) manager.Player.RankMatchRank));
            goto Label_DA58;
        Label_DA4C:
            base.get_gameObject().SetActive(0);
        Label_DA58:
            return;
        Label_DA59:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.RankMatchRank <= 3)
            {
                goto Label_DA81;
            }
            base.get_gameObject().SetActive(0);
            goto Label_DAFA;
        Label_DA81:
            image12 = base.GetComponent<Image>();
            if ((image12 == null) == null)
            {
                goto Label_DAA7;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DAA7:
            sheet10 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet10 != null) == null)
            {
                goto Label_DAFA;
            }
            image12.set_sprite(sheet10.GetSprite("ranking_" + ((int) manager.Player.RankMatchRank)));
            image12.set_enabled(1);
        Label_DAFA:
            return;
        Label_DAFB:
            manager = MonoSingleton<GameManager>.Instance;
            image13 = base.GetComponent<Image>();
            if ((image13 == null) == null)
            {
                goto Label_DB27;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DB27:
            sheet11 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet11 != null) == null)
            {
                goto Label_DB9D;
            }
            num209 = manager.Player.RankMatchClass;
            if (this.InstanceType != 1)
            {
                goto Label_DB6E;
            }
            num209 = manager.Player.RankMatchOldClass;
        Label_DB6E:
            image13.set_sprite(sheet11.GetSprite("class_" + ((int) num209)));
            image13.set_enabled(1);
        Label_DB9D:
            return;
        Label_DB9E:
            manager = MonoSingleton<GameManager>.Instance;
            image14 = base.GetComponent<Image>();
            if ((image14 == null) == null)
            {
                goto Label_DBCA;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DBCA:
            sheet12 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet12 != null) == null)
            {
                goto Label_DC40;
            }
            num210 = manager.Player.RankMatchClass;
            if (this.InstanceType != 1)
            {
                goto Label_DC11;
            }
            num210 = manager.Player.RankMatchOldClass;
        Label_DC11:
            image14.set_sprite(sheet12.GetSprite("classname_result_" + ((int) num210)));
            image14.set_enabled(1);
        Label_DC40:
            return;
        Label_DC41:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.RankMatchTotalCount);
            return;
        Label_DC59:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.RankMatchWinCount);
            return;
        Label_DC71:
            manager = MonoSingleton<GameManager>.Instance;
            this.SetTextValue(manager.Player.RankMatchLoseCount);
            return;
        Label_DC89:
            data91 = MonoSingleton<GameManager>.Instance.Player;
            data3 = data91.FindPartyOfType(10);
            num211 = 0;
            num212 = 0;
            goto Label_DD1C;
        Label_DCB3:
            num213 = data3.GetUnitUniqueID(num212);
            data2 = data91.FindUnitDataByUniqueID(num213);
            if (data2 == null)
            {
                goto Label_DD12;
            }
            num211 += data2.Status.param.atk;
            num211 += data2.Status.param.mag;
        Label_DD12:
            num212 += 1;
        Label_DD1C:
            if (num212 < data3.MAX_UNIT)
            {
                goto Label_DCB3;
            }
            this.SetTextValue(num211);
            return;
        Label_DD37:
            ranking = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            if (ranking != null)
            {
                goto Label_DD5D;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DD5D:
            if (ranking.rank <= 3)
            {
                goto Label_DD94;
            }
            this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_RANKING_RANK"), (int) ranking.rank));
            goto Label_DDA0;
        Label_DD94:
            base.get_gameObject().SetActive(0);
        Label_DDA0:
            return;
        Label_DDA1:
            ranking2 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            if (ranking2 != null)
            {
                goto Label_DDC7;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DDC7:
            if (ranking2.rank <= 3)
            {
                goto Label_DDE7;
            }
            base.get_gameObject().SetActive(0);
            goto Label_DE5E;
        Label_DDE7:
            image15 = base.GetComponent<Image>();
            if ((image15 == null) == null)
            {
                goto Label_DE0D;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_DE0D:
            sheet13 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet13 != null) == null)
            {
                goto Label_DE5E;
            }
            image15.set_sprite(sheet13.GetSprite("ranking_" + ((int) ranking2.rank)));
            image15.set_enabled(1);
        Label_DE5E:
            return;
        Label_DE5F:
            ranking3 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            image16 = base.GetComponent<Image>();
            if (ranking3 == null)
            {
                goto Label_DE91;
            }
            if ((image16 == null) == null)
            {
                goto Label_DE9E;
            }
        Label_DE91:
            base.get_gameObject().SetActive(0);
            return;
        Label_DE9E:
            sheet14 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet14 != null) == null)
            {
                goto Label_DEEF;
            }
            image16.set_sprite(sheet14.GetSprite("class_" + ((int) ranking3.type)));
            image16.set_enabled(1);
        Label_DEEF:
            return;
        Label_DEF0:
            ranking4 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            if (ranking4 != null)
            {
                goto Label_DF0A;
            }
            return;
        Label_DF0A:
            this.SetTextValue(ranking4.enemy.name);
            return;
        Label_DF1F:
            ranking5 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            if (ranking5 != null)
            {
                goto Label_DF39;
            }
            return;
        Label_DF39:
            this.SetTextValue(ranking5.enemy.lv);
            return;
        Label_DF4E:
            ranking6 = DataSource.FindDataOfClass<ReqRankMatchRanking.ResponceRanking>(base.get_gameObject(), null);
            if (ranking6 != null)
            {
                goto Label_DF68;
            }
            return;
        Label_DF68:
            this.SetTextValue(ranking6.score);
            return;
        Label_DF78:
            param137 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(base.get_gameObject(), null);
            if (param137 != null)
            {
                goto Label_DFA8;
            }
            base.get_transform().get_parent().get_gameObject().SetActive(0);
            return;
        Label_DFA8:
            if (this.ParameterType != 0x972)
            {
                goto Label_DFF2;
            }
            num214 = param137.RankBegin;
            if (num214 != param137.RankEnd)
            {
                goto Label_DFFF;
            }
            base.get_transform().get_parent().get_gameObject().SetActive(0);
            goto Label_DFFF;
        Label_DFF2:
            num214 = param137.RankEnd;
        Label_DFFF:
            if (param137.RankBegin <= 3)
            {
                goto Label_E01D;
            }
            this.SetTextValue(num214);
            goto Label_E033;
        Label_E01D:
            base.get_transform().get_parent().get_gameObject().SetActive(0);
        Label_E033:
            return;
        Label_E034:
            param138 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(base.get_gameObject(), null);
            if (param138 != null)
            {
                goto Label_E05F;
            }
            base.get_transform().get_gameObject().SetActive(0);
            return;
        Label_E05F:
            if (param138.RankBegin != param138.RankEnd)
            {
                goto Label_E087;
            }
            base.get_transform().get_gameObject().SetActive(0);
        Label_E087:
            return;
        Label_E088:
            param139 = DataSource.FindDataOfClass<VersusRankRankingRewardParam>(base.get_gameObject(), null);
            if (param139 != null)
            {
                goto Label_E0AE;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E0AE:
            if (param139.RankBegin <= 3)
            {
                goto Label_E0CE;
            }
            base.get_gameObject().SetActive(0);
            goto Label_E145;
        Label_E0CE:
            image17 = base.GetComponent<Image>();
            if ((image17 == null) == null)
            {
                goto Label_E0F4;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E0F4:
            sheet15 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet15 != null) == null)
            {
                goto Label_E145;
            }
            image17.set_sprite(sheet15.GetSprite("ranking_" + ((int) param139.RankBegin)));
            image17.set_enabled(1);
        Label_E145:
            return;
        Label_E146:
            list25 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            if (list25 != null)
            {
                goto Label_E160;
            }
            return;
        Label_E160:
            if ((list25.result.result == "win") == null)
            {
                goto Label_E196;
            }
            transform3 = base.get_transform().FindChild("Win");
            goto Label_E237;
        Label_E196:
            if ((list25.result.result == "lose") != null)
            {
                goto Label_E20A;
            }
            if ((list25.result.result == "retire") != null)
            {
                goto Label_E20A;
            }
            if ((list25.result.result == "cancel") != null)
            {
                goto Label_E20A;
            }
            if ((list25.result.result == "crashed") == null)
            {
                goto Label_E223;
            }
        Label_E20A:
            transform3 = base.get_transform().FindChild("Lose");
            goto Label_E237;
        Label_E223:
            transform3 = base.get_transform().FindChild("Draw");
        Label_E237:
            if ((transform3 == null) == null)
            {
                goto Label_E247;
            }
            return;
        Label_E247:
            transform3.get_gameObject().SetActive(1);
            return;
        Label_E257:
            list26 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            str67 = "0";
            color2 = Color.get_white();
            if (list26 == null)
            {
                goto Label_E2F7;
            }
            str67 = &list26.value.ToString();
            if ((list26.result.result == "win") == null)
            {
                goto Label_E2D1;
            }
            str67 = "+" + str67;
            color2 = Color.get_cyan();
            goto Label_E2F7;
        Label_E2D1:
            if ((list26.result.result == "lose") == null)
            {
                goto Label_E2F7;
            }
            color2 = Color.get_red();
        Label_E2F7:
            this.mText.set_text(str67);
            this.mText.set_color(color2);
            return;
        Label_E316:
            list27 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            image18 = base.GetComponent<Image>();
            if (list27 == null)
            {
                goto Label_E348;
            }
            if ((image18 == null) == null)
            {
                goto Label_E355;
            }
        Label_E348:
            base.get_gameObject().SetActive(0);
            return;
        Label_E355:
            sheet16 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet16 != null) == null)
            {
                goto Label_E3A6;
            }
            image18.set_sprite(sheet16.GetSprite("class_" + ((int) list27.type)));
            image18.set_enabled(1);
        Label_E3A6:
            return;
        Label_E3A7:
            list28 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            if (list28 != null)
            {
                goto Label_E3C1;
            }
            return;
        Label_E3C1:
            this.SetTextValue(list28.enemy.name);
            return;
        Label_E3D6:
            list29 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            if (list29 != null)
            {
                goto Label_E3F0;
            }
            return;
        Label_E3F0:
            this.SetTextValue(list29.enemy.lv);
            return;
        Label_E405:
            list30 = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(base.get_gameObject(), null);
            if (list30 != null)
            {
                goto Label_E41F;
            }
            return;
        Label_E41F:
            this.SetTextValue(list30.enemyscore);
            return;
        Label_E42F:
            param140 = DataSource.FindDataOfClass<VersusRankMissionParam>(base.get_gameObject(), null);
            if (param140 != null)
            {
                goto Label_E449;
            }
            return;
        Label_E449:
            this.SetTextValue(param140.Name);
            return;
        Label_E459:
            param141 = DataSource.FindDataOfClass<VersusRankMissionParam>(base.get_gameObject(), null);
            if (param141 != null)
            {
                goto Label_E473;
            }
            return;
        Label_E473:
            this.SetTextValue(param141.IVal);
            return;
        Label_E483:
            progress = DataSource.FindDataOfClass<ReqRankMatchMission.MissionProgress>(base.get_gameObject(), null);
            if (progress != null)
            {
                goto Label_E4AC;
            }
            this.SetTextValue("0");
            goto Label_E4BB;
        Label_E4AC:
            this.SetTextValue(progress.prog);
        Label_E4BB:
            return;
        Label_E4BC:
            manager = MonoSingleton<GameManager>.Instance;
            num215 = 0;
            num216 = 1;
            param142 = manager.GetVersusRankClass(manager.RankMatchScheduleId, manager.Player.RankMatchClass);
            if (param142 == null)
            {
                goto Label_E520;
            }
            num215 = param142.UpPoint - manager.Player.RankMatchScore;
            num216 = param142.UpPoint - param142.DownPoint;
        Label_E520:
            if ((this.mSlider != null) == null)
            {
                goto Label_E54D;
            }
            this.mSlider.set_value(1f - (((float) num215) / ((float) num216)));
        Label_E54D:
            return;
        Label_E54E:
            manager = MonoSingleton<GameManager>.Instance;
            list31 = manager.GetVersusRankMissionList(manager.RankMatchScheduleId);
            flag34 = 0;
            storeyd = new <InternalUpdateValue>c__AnonStorey21D();
            enumerator = manager.Player.RankMatchMissionState.GetEnumerator();
        Label_E586:
            try
            {
                goto Label_E5FB;
            Label_E58B:
                storeyd.rmms = &enumerator.Current;
                param143 = list31.Find(new Predicate<VersusRankMissionParam>(storeyd.<>m__112));
                if (storeyd.rmms.IsRewarded != null)
                {
                    goto Label_E5FB;
                }
                if (param143 == null)
                {
                    goto Label_E5FB;
                }
                if (param143.IVal > storeyd.rmms.Progress)
                {
                    goto Label_E5FB;
                }
                flag34 = 1;
                goto Label_E609;
            Label_E5FB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_E58B;
                }
            Label_E609:
                goto Label_E61D;
            }
            finally
            {
            Label_E60E:
                ((List<RankMatchMissionState>.Enumerator) enumerator).Dispose();
            }
        Label_E61D:
            base.get_gameObject().SetActive(flag34);
            return;
        Label_E62D:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.mRankMatchSeasonResult != null)
            {
                goto Label_E650;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E650:
            this.SetTextValue(manager.Player.mRankMatchSeasonResult.Rank);
            return;
        Label_E667:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.mRankMatchSeasonResult != null)
            {
                goto Label_E68A;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E68A:
            if (manager.Player.mRankMatchSeasonResult.Rank <= 3)
            {
                goto Label_E6B1;
            }
            base.get_gameObject().SetActive(0);
            goto Label_E72F;
        Label_E6B1:
            image19 = base.GetComponent<Image>();
            if ((image19 == null) == null)
            {
                goto Label_E6D7;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E6D7:
            sheet17 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet17 != null) == null)
            {
                goto Label_E72F;
            }
            image19.set_sprite(sheet17.GetSprite("ranking_" + ((int) manager.Player.mRankMatchSeasonResult.Rank)));
            image19.set_enabled(1);
        Label_E72F:
            return;
        Label_E730:
            manager = MonoSingleton<GameManager>.Instance;
            image20 = base.GetComponent<Image>();
            if (manager.Player.mRankMatchSeasonResult == null)
            {
                goto Label_E75F;
            }
            if ((image20 == null) == null)
            {
                goto Label_E76C;
            }
        Label_E75F:
            base.get_gameObject().SetActive(0);
            return;
        Label_E76C:
            sheet18 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet18 != null) == null)
            {
                goto Label_E7C4;
            }
            image20.set_sprite(sheet18.GetSprite("class_" + ((int) manager.Player.mRankMatchSeasonResult.Class)));
            image20.set_enabled(1);
        Label_E7C4:
            return;
        Label_E7C5:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.mRankMatchSeasonResult != null)
            {
                goto Label_E7DC;
            }
            return;
        Label_E7DC:
            this.SetTextValue(manager.Player.mRankMatchSeasonResult.Score);
            return;
        Label_E7F3:
            this.InstanceType = 1;
            param144 = this.GetVersusPlayerParam();
            if (param144 == null)
            {
                goto Label_E822;
            }
            this.SetTextValue(&param144.rankmatch_score.ToString());
            return;
        Label_E822:
            this.ResetToDefault();
            return;
        Label_E829:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.mRankMatchSeasonResult != null)
            {
                goto Label_E840;
            }
            return;
        Label_E840:
            image21 = base.GetComponent<Image>();
            if ((image21 == null) == null)
            {
                goto Label_E866;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_E866:
            sheet19 = AssetManager.Load<SpriteSheet>("pvp/rankmatch_class");
            if ((sheet19 != null) == null)
            {
                goto Label_E8C6;
            }
            num217 = manager.Player.mRankMatchSeasonResult.Class;
            image21.set_sprite(sheet19.GetSprite("classname_result_" + ((int) num217)));
            image21.set_enabled(1);
        Label_E8C6:
            return;
        Label_E8C7:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.mRankMatchSeasonResult != null)
            {
                goto Label_E8DE;
            }
            return;
        Label_E8DE:
            param145 = manager.GetVersusRankParam(manager.Player.mRankMatchSeasonResult.ScheduleId);
            if (param145 != null)
            {
                goto Label_E902;
            }
            return;
        Label_E902:
            LocalizedText.Get("sys.RANK_MATCH_MATCHING_SEASONTIME");
            objArray8 = new object[] { (int) &param145.BeginAt.Month, (int) &param145.BeginAt.Day, (int) &param145.BeginAt.Hour, (int) &param145.BeginAt.Minute, (int) &param145.EndAt.Month, (int) &param145.EndAt.Day, (int) &param145.EndAt.Hour, (int) &param145.EndAt.Minute };
            this.SetTextValue(string.Format(LocalizedText.Get("sys.RANK_MATCH_MATCHING_SEASONTIME"), objArray8));
            return;
        Label_EA19:
            manager = MonoSingleton<GameManager>.Instance;
            param146 = manager.GetVersusRankParam(manager.RankMatchScheduleId);
            if (param146 != null)
            {
                goto Label_EA39;
            }
            return;
        Label_EA39:
            this.SetTextValue(param146.Name);
            return;
        Label_EA49:
            flag35 = 0;
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_EA6C;
            }
            flag35 = data5.IsDerivedAbility;
            goto Label_EAA8;
        Label_EA6C:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_EAA8;
            }
            param147 = DataSource.FindDataOfClass<AbilityDeriveParam>(base.get_gameObject(), null);
            if (param147 == null)
            {
                goto Label_EAA8;
            }
            if (param147.m_DeriveParam != param3)
            {
                goto Label_EAA8;
            }
            flag35 = 1;
        Label_EAA8:
            base.get_gameObject().SetActive(flag35);
            return;
        Label_EAB8:
            &color3..ctor(1f, 1f, 0f, 1f);
            color4 = Color.get_white();
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_EB05;
            }
            if (data5.IsDerivedAbility == null)
            {
                goto Label_EB44;
            }
            color4 = color3;
            goto Label_EB44;
        Label_EB05:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_EB44;
            }
            param148 = DataSource.FindDataOfClass<AbilityDeriveParam>(base.get_gameObject(), null);
            if (param148 == null)
            {
                goto Label_EB44;
            }
            if (param148.m_DeriveParam != param3)
            {
                goto Label_EB44;
            }
            color4 = color3;
        Label_EB44:
            this.SetTextColor(color4);
            this.SetSyncColorOriginColor(color4);
            return;
        Label_EB59:
            str68 = string.Empty;
            sheet20 = base.GetComponent<ImageSpriteSheet>();
            if ((sheet20 != null) == null)
            {
                goto Label_EB88;
            }
            str68 = sheet20.DefaultKey;
        Label_EB88:
            if ((data5 = this.GetAbilityData()) == null)
            {
                goto Label_EBB0;
            }
            str68 = AbilityParam.TypeDetailToSpriteSheetKey(data5.Param.type_detail);
            goto Label_EBCE;
        Label_EBB0:
            if ((param3 = this.GetAbilityParam()) == null)
            {
                goto Label_EBCE;
            }
            str68 = AbilityParam.TypeDetailToSpriteSheetKey(param3.type_detail);
        Label_EBCE:
            this.SetImageBySpriteSheet(str68);
            return;
        Label_EBD9:
            &color5..ctor(1f, 1f, 0f, 1f);
            color6 = Color.get_white();
            if ((data6 = this.GetSkillData()) == null)
            {
                goto Label_EC26;
            }
            if (data6.IsDerivedSkill == null)
            {
                goto Label_EC7B;
            }
            color6 = color5;
            goto Label_EC7B;
        Label_EC26:
            if ((storey.skillParam = this.GetSkillParam()) == null)
            {
                goto Label_EC7B;
            }
            param149 = DataSource.FindDataOfClass<SkillDeriveParam>(base.get_gameObject(), null);
            if (param149 == null)
            {
                goto Label_EC7B;
            }
            if (param149.m_DeriveParam != storey.skillParam)
            {
                goto Label_EC7B;
            }
            color6 = color5;
        Label_EC7B:
            this.SetTextColor(color6);
            this.SetSyncColorOriginColor(color6);
            return;
        Label_EC90:
            param150 = DataSource.FindDataOfClass<SkillAbilityDeriveParam>(base.get_gameObject(), null);
            if (param150 == null)
            {
                goto Label_ECEC;
            }
            str69 = param150.GetTriggerArtifactIname(this.Index);
            if (string.IsNullOrEmpty(str69) != null)
            {
                goto Label_ECEC;
            }
            param151 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(str69);
            this.LoadArtifactIcon(param151);
        Label_ECEC:
            return;
        Label_ECED:
            this.mImageArray.set_sprite(null);
            data92 = DataSource.FindDataOfClass<ConceptCardData>(base.get_gameObject(), null);
            if (data92 == null)
            {
                goto Label_ED95;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_ED95;
            }
            if (data92.AwakeCount <= 0)
            {
                goto Label_ED95;
            }
            num218 = data92.AwakeCount - 1;
            if (data92.Lv < data92.LvCap)
            {
                goto Label_ED7F;
            }
            num218 = ((int) this.mImageArray.Images.Length) - 1;
        Label_ED7F:
            this.SetImageIndex(num218);
            base.get_gameObject().SetActive(1);
        Label_ED95:
            return;
        Label_ED96:
            flag36 = DataSource.FindDataOfClass<bool>(base.get_gameObject(), 1);
            base.get_gameObject().SetActive(flag36);
            return;
        Label_EDB6:
            flag37 = DataSource.FindDataOfClass<bool>(base.get_gameObject(), 0);
            base.get_gameObject().SetActive(flag37 == 0);
            return;
        Label_EDD9:
            return;
        }

        private bool LoadArtifactIcon(ArtifactParam param)
        {
            IconLoader loader;
            loader = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            if (param == null)
            {
                goto Label_0020;
            }
            loader.ResourcePath = AssetPath.ArtifactIcon(param);
            return 1;
        Label_0020:
            return 0;
        }

        private bool LoadItemIcon(ItemParam itemParam)
        {
            IconLoader loader;
            loader = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            if (itemParam == null)
            {
                goto Label_0020;
            }
            loader.ResourcePath = AssetPath.ItemIcon(itemParam);
            return 1;
        Label_0020:
            return 0;
        }

        private bool LoadItemIcon(string iconName)
        {
            IconLoader loader;
            loader = GameUtility.RequireComponent<IconLoader>(base.get_gameObject());
            if (string.IsNullOrEmpty(iconName) != null)
            {
                goto Label_0025;
            }
            loader.ResourcePath = AssetPath.ItemIcon(iconName);
            return 1;
        Label_0025:
            return 0;
        }

        private void OnDestroy()
        {
            Instances.Remove(this);
            return;
        }

        private void OnEnable()
        {
            if (this.mUpdateCoroutine == null)
            {
                goto Label_001E;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
            this.mUpdateCoroutine = null;
        Label_001E:
            if (this.mStarted == null)
            {
                goto Label_002F;
            }
            this.UpdateValue();
        Label_002F:
            return;
        }

        public unsafe void ResetToDefault()
        {
            if ((this.mText != null) == null)
            {
                goto Label_0027;
            }
            this.mText.set_text(this.mDefaultValue);
            goto Label_00D9;
        Label_0027:
            if ((this.mSlider != null) == null)
            {
                goto Label_0069;
            }
            this.mSlider.set_value(&this.mDefaultRangeValue.x);
            this.mSlider.set_maxValue(&this.mDefaultRangeValue.y);
            goto Label_00D9;
        Label_0069:
            if ((this.mInputField != null) == null)
            {
                goto Label_0090;
            }
            this.mInputField.set_text(this.mDefaultValue);
            goto Label_00D9;
        Label_0090:
            if ((this.mImage != null) == null)
            {
                goto Label_00B7;
            }
            this.mImage.set_texture(this.mDefaultImage);
            goto Label_00D9;
        Label_00B7:
            if ((this.mImageArray != null) == null)
            {
                goto Label_00D9;
            }
            this.mImageArray.set_sprite(this.mDefaultSprite);
        Label_00D9:
            return;
        }

        private void SetAnimatorBool(string name, bool value)
        {
            if ((this.mAnimator != null) == null)
            {
                goto Label_001E;
            }
            this.mAnimator.SetBool(name, value);
        Label_001E:
            return;
        }

        private void SetAnimatorInt(string name, int value)
        {
            if ((this.mAnimator != null) == null)
            {
                goto Label_001E;
            }
            this.mAnimator.SetInteger(name, value);
        Label_001E:
            return;
        }

        private void SetArtifactFrame(ArtifactParam param)
        {
            Image image;
            int num;
            GameSettings settings;
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            image = base.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_004F;
            }
            num = param.rareini;
            settings = GameSettings.Instance;
            if ((settings != null) == null)
            {
                goto Label_004F;
            }
            if (num >= ((int) settings.ArtifactIcon_Frames.Length))
            {
                goto Label_004F;
            }
            image.set_sprite(settings.ArtifactIcon_Frames[num]);
        Label_004F:
            return;
        }

        private void SetBuyPriceEventCoinTypeIcon(string cost_iname)
        {
            Image image;
            SpriteSheet sheet;
            image = base.GetComponent<Image>();
            if ((image == null) != null)
            {
                goto Label_0019;
            }
            if (cost_iname != null)
            {
                goto Label_001A;
            }
        Label_0019:
            return;
        Label_001A:
            sheet = AssetManager.Load<SpriteSheet>("EventShopCmn/eventcoin_small");
            if ((sheet != null) == null)
            {
                goto Label_003E;
            }
            image.set_sprite(sheet.GetSprite(cost_iname));
        Label_003E:
            return;
        }

        private void SetBuyPriceTypeIcon(ESaleType type)
        {
            Sprite[] spriteArray;
            Image image;
            int num;
            spriteArray = GameSettings.Instance.ItemPriceIconFrames;
            if (spriteArray != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (type != 6)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            image = base.GetComponent<Image>();
            num = (type == 7) ? 1 : type;
            if ((image != null) == null)
            {
                goto Label_004E;
            }
            if (num >= ((int) spriteArray.Length))
            {
                goto Label_004E;
            }
            image.set_sprite(spriteArray[num]);
        Label_004E:
            return;
        }

        private unsafe void SetEquipItemFrame(ItemParam itemParam)
        {
            Sprite[] spriteArray;
            Image image;
            spriteArray = &GameSettings.Instance.ItemIcons.NormalFrames;
            image = base.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_005C;
            }
            if (((int) spriteArray.Length) <= 0)
            {
                goto Label_005C;
            }
            if (itemParam == null)
            {
                goto Label_0053;
            }
            if (itemParam.rare >= ((int) spriteArray.Length))
            {
                goto Label_0053;
            }
            image.set_sprite(spriteArray[itemParam.rare]);
            goto Label_005C;
        Label_0053:
            image.set_sprite(spriteArray[0]);
        Label_005C:
            return;
        }

        private void SetImageBySpriteSheet(string key)
        {
            ImageSpriteSheet sheet;
            sheet = base.GetComponent<ImageSpriteSheet>();
            if ((sheet != null) == null)
            {
                goto Label_001A;
            }
            sheet.SetSprite(key);
        Label_001A:
            return;
        }

        private void SetImageIndex(int index)
        {
            if ((this.mImageArray != null) == null)
            {
                goto Label_001D;
            }
            this.mImageArray.ImageIndex = index;
        Label_001D:
            return;
        }

        private void SetItemFrame(ItemParam itemParam)
        {
            Image image;
            Sprite sprite;
            if (itemParam != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            image = base.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_002D;
            }
            sprite = GameSettings.Instance.GetItemFrame(itemParam);
            image.set_sprite(sprite);
        Label_002D:
            return;
        }

        private void SetSliderValue(int value, int maxValue)
        {
            if ((this.mSlider != null) == null)
            {
                goto Label_003B;
            }
            this.mSlider.set_maxValue((float) maxValue);
            this.mSlider.set_minValue(0f);
            this.mSlider.set_value((float) value);
        Label_003B:
            return;
        }

        private void SetSyncColorOriginColor(Color color)
        {
            SyncColor color2;
            color2 = base.GetComponent<SyncColor>();
            if ((color2 != null) == null)
            {
                goto Label_001A;
            }
            color2.ForceOriginColorChange(color);
        Label_001A:
            return;
        }

        private void SetTextColor(Color color)
        {
            if ((this.mText != null) == null)
            {
                goto Label_001D;
            }
            this.mText.set_color(color);
        Label_001D:
            return;
        }

        private unsafe void SetTextValue(int value)
        {
            if ((this.mText != null) != null)
            {
                goto Label_0022;
            }
            if ((this.mInputField != null) == null)
            {
                goto Label_002F;
            }
        Label_0022:
            this.SetTextValue(&value.ToString());
        Label_002F:
            return;
        }

        private void SetTextValue(string value)
        {
            if ((this.mText != null) == null)
            {
                goto Label_001D;
            }
            this.mText.set_text(value);
        Label_001D:
            if ((this.mInputField != null) == null)
            {
                goto Label_003A;
            }
            SRPG_Extensions.SetText(this.mInputField, value);
        Label_003A:
            return;
        }

        private void SetUpdateInterval(float interval)
        {
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (interval > 0f)
            {
                goto Label_0034;
            }
            if (this.mUpdateCoroutine == null)
            {
                goto Label_0033;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
        Label_0033:
            return;
        Label_0034:
            this.mNextUpdateTime = Time.get_time() + interval;
            if (this.mUpdateCoroutine == null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            this.mUpdateCoroutine = base.StartCoroutine(this.UpdateTimer());
            return;
        }

        private void Start()
        {
            this.mStarted = 1;
            this.UpdateValue();
            return;
        }

        public void ToggleChildren(bool visible)
        {
            Transform transform;
            int num;
            int num2;
            transform = base.get_transform();
            num = transform.get_childCount();
            num2 = 0;
            goto Label_002B;
        Label_0015:
            transform.GetChild(num2).get_gameObject().SetActive(visible);
            num2 += 1;
        Label_002B:
            if (num2 < num)
            {
                goto Label_0015;
            }
            return;
        }

        public void ToggleEmpty(bool visible)
        {
            if (this.mIsEmptyGO == null)
            {
                goto Label_0017;
            }
            base.get_gameObject().SetActive(visible);
        Label_0017:
            return;
        }

        public static void UpdateAll(GameObject root)
        {
            Component[] componentArray;
            int num;
            GameParameter parameter;
            Transform transform;
            componentArray = root.GetComponentsInChildren(typeof(IGameParameter), 1);
            num = 0;
            goto Label_0093;
        Label_0019:
            if ((componentArray[num] as GameParameter) == null)
            {
                goto Label_0082;
            }
            parameter = componentArray[num] as GameParameter;
            if (parameter.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_004A;
            }
            parameter.UpdateValue();
            goto Label_007D;
        Label_004A:
            transform = parameter.get_transform();
            if ((transform.get_parent() != null) == null)
            {
                goto Label_008F;
            }
            if (transform.get_parent().get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_008F;
            }
            parameter.UpdateValue();
        Label_007D:
            goto Label_008F;
        Label_0082:
            ((IGameParameter) componentArray[num]).UpdateValue();
        Label_008F:
            num += 1;
        Label_0093:
            if (num < ((int) componentArray.Length))
            {
                goto Label_0019;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator UpdateTimer()
        {
            <UpdateTimer>c__Iterator79 iterator;
            iterator = new <UpdateTimer>c__Iterator79();
            iterator.<>f__this = this;
            return iterator;
        }

        public void UpdateValue()
        {
            FieldInfo info;
            bool flag;
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0059;
            }
            if ((((int) typeof(ParameterTypes).GetField(((ParameterTypes) this.ParameterType).ToString()).GetCustomAttributes(typeof(AlwaysUpdate), 1).Length) > 0) == null)
            {
                goto Label_005F;
            }
            this.InternalUpdateValue();
            return;
            goto Label_005F;
        Label_0059:
            this.InternalUpdateValue();
        Label_005F:
            return;
        }

        public static void UpdateValuesOfType(ParameterTypes type)
        {
            int num;
            num = 0;
            goto Label_0031;
        Label_0007:
            if (Instances[num].ParameterType != type)
            {
                goto Label_002D;
            }
            Instances[num].UpdateValue();
        Label_002D:
            num += 1;
        Label_0031:
            if (num < Instances.Count)
            {
                goto Label_0007;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <GetMultiPlayerUnitData>c__AnonStorey213
        {
            internal int index;

            public <GetMultiPlayerUnitData>c__AnonStorey213()
            {
                base..ctor();
                return;
            }

            internal bool <>m__103(JSON_MyPhotonPlayerParam.UnitDataElem e)
            {
                return (e.slotID == this.index);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey214
        {
            internal SupportData supportData;
            internal SkillParam skillParam;

            public <InternalUpdateValue>c__AnonStorey214()
            {
                base..ctor();
                return;
            }

            internal bool <>m__104(SupportData f)
            {
                return (f.FUID == this.supportData.FUID);
            }

            internal bool <>m__10C(LearningSkill p)
            {
                return (p.iname == this.skillParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey215
        {
            internal MyPhoton.MyPlayer player;

            public <InternalUpdateValue>c__AnonStorey215()
            {
                base..ctor();
                return;
            }

            internal bool <>m__105(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerID == this.player.playerID) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey216
        {
            internal MyPhoton.MyPlayer player;

            public <InternalUpdateValue>c__AnonStorey216()
            {
                base..ctor();
                return;
            }

            internal bool <>m__106(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerID == this.player.playerID) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey217
        {
            internal MyPhoton.MyPlayer player;

            public <InternalUpdateValue>c__AnonStorey217()
            {
                base..ctor();
                return;
            }

            internal bool <>m__107(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerID == this.player.playerID) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey218
        {
            internal SceneBattle bs;

            public <InternalUpdateValue>c__AnonStorey218()
            {
                base..ctor();
                return;
            }

            internal bool <>m__109(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerIndex == this.bs.Battle.CurrentUnit.OwnerPlayerIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey219
        {
            internal SceneBattle bs;

            public <InternalUpdateValue>c__AnonStorey219()
            {
                base..ctor();
                return;
            }

            internal bool <>m__10A(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerIndex == this.bs.Battle.CurrentUnit.OwnerPlayerIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey21A
        {
            internal SceneBattle bs;

            public <InternalUpdateValue>c__AnonStorey21A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__10B(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerIndex == this.bs.Battle.CurrentUnit.OwnerPlayerIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey21B
        {
            internal SceneBattle bs;
            internal JSON_MyPhotonPlayerParam param;

            public <InternalUpdateValue>c__AnonStorey21B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__10D(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerIndex == this.bs.Battle.CurrentUnit.OwnerPlayerIndex);
            }

            internal bool <>m__10E(MyPhoton.MyPlayer p)
            {
                return (p.playerID == this.param.playerID);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey21C
        {
            internal SceneBattle bs;

            public <InternalUpdateValue>c__AnonStorey21C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__110(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerIndex == this.bs.Battle.CurrentUnit.OwnerPlayerIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <InternalUpdateValue>c__AnonStorey21D
        {
            internal RankMatchMissionState rmms;

            public <InternalUpdateValue>c__AnonStorey21D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__112(VersusRankMissionParam mission)
            {
                return (mission.IName == this.rmms.IName);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateTimer>c__Iterator79 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GameParameter <>f__this;

            public <UpdateTimer>c__Iterator79()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_003D;

                    case 2:
                        goto Label_0091;
                }
                goto Label_0098;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_009A;
            Label_003D:
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_002A;
                }
                this.<>f__this.InternalUpdateValue();
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_003D;
                }
                this.<>f__this.mUpdateCoroutine = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_009A;
            Label_0091:
                this.$PC = -1;
            Label_0098:
                return 0;
            Label_009A:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public class AlwaysUpdate : Attribute
        {
            public AlwaysUpdate()
            {
                base..ctor();
                return;
            }
        }

        public enum ArenaPlayerInstanceTypes
        {
            Any,
            Enemy
        }

        public enum ArtifactInstanceTypes
        {
            Any,
            QuestReward,
            Trophy
        }

        public class EnumParameterDesc : GameParameter.ParameterDesc
        {
            public EnumParameterDesc(string text, Type type)
            {
                base..ctor(text);
                return;
            }
        }

        public class InstanceTypes : Attribute
        {
            public InstanceTypes(Type instanceType)
            {
                base..ctor();
                return;
            }
        }

        public enum ItemInstanceTypes
        {
            Any,
            Inventory,
            QuestReward,
            Equipment,
            EnhanceMaterial,
            EnhanceEquipData,
            SellItem,
            ConsumeItem
        }

        public class ParameterDesc : Attribute
        {
            public ParameterDesc(string text)
            {
                base..ctor();
                return;
            }
        }

        public enum ParameterTypes
        {
            [ParameterDesc("プレイヤーの名前")]
            GLOBAL_PLAYER_NAME = 0,
            [ParameterDesc("プレイヤーのレベル")]
            GLOBAL_PLAYER_LEVEL = 1,
            [ParameterDesc("プレイヤーの現在のスタミナ")]
            GLOBAL_PLAYER_STAMINA = 2,
            [ParameterDesc("プレイヤーの最大スタミナ")]
            GLOBAL_PLAYER_STAMINAMAX = 3,
            [ParameterDesc("プレイヤーの経験値")]
            GLOBAL_PLAYER_EXP = 4,
            [ParameterDesc("プレイヤーが次のレベルまでに必要な経験値")]
            GLOBAL_PLAYER_EXPNEXT = 5,
            [ParameterDesc("プレイヤーの所持ゴールド")]
            GLOBAL_PLAYER_GOLD = 6,
            [ParameterDesc("プレイヤーの所持コイン")]
            GLOBAL_PLAYER_COIN = 7,
            [ParameterDesc("プレイヤーのスタミナが次に回復するまでの時間")]
            GLOBAL_PLAYER_STAMINATIME = 8,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエスト名")]
            QUEST_NAME = 9,
            [ParameterDesc("クエストに必要なスタミナ"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_STAMINA = 10,
            [EnumParameterDesc("クエストのクリア状態にあわせてAnimatorのstateという名前のInt値、ImageArrayのインデックスを切り替えます。", typeof(QuestStates)), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_STATE = 11,
            [ParameterDesc("クエスト勝利条件"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_OBJECTIVE = 12,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストボーナス条件。インデックスでボーナス条件の番号を指定してください。"), UsesIndex]
            QUEST_BONUSOBJECTIVE = 13,
            [ParameterDesc("アイテムアイコン"), UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes))]
            ITEM_ICON = 14,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストの説明")]
            QUEST_DESCRIPTION = 15,
            [ParameterDesc("フレンドの名前")]
            SUPPORTER_NAME = 0x10,
            [ParameterDesc("フレンドのレベル")]
            SUPPORTER_LEVEL = 0x11,
            [ParameterDesc("フレンドのユニットレベル")]
            SUPPORTER_UNITLEVEL = 0x12,
            [ParameterDesc("フレンドのリーダースキル名")]
            SUPPORTER_LEADERSKILLNAME = 0x13,
            [ParameterDesc("フレンドの攻撃力")]
            SUPPORTER_ATK = 20,
            [ParameterDesc("フレンドのHP")]
            SUPPORTER_HP = 0x15,
            [ParameterDesc("フレンドの魔法攻撃力")]
            SUPPORTER_MAGIC = 0x16,
            [EnumParameterDesc("フレンドのレアリティにあわせてAnimatorのrareという名前のInt値を切り替えます。", typeof(ERarity))]
            SUPPORTER_RARITY = 0x17,
            [EnumParameterDesc("フレンドの属性にあわせてAnimatorのelementという名前のInt値を切り替えます。", typeof(EElement))]
            SUPPORTER_ELEMENT = 0x18,
            [ParameterDesc("フレンドのアイコン")]
            SUPPORTER_ICON = 0x19,
            [ParameterDesc("フレンドのリーダースキルの説明")]
            SUPPORTER_LEADERSKILLDESC = 0x1a,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストの副題")]
            QUEST_SUBTITLE = 0x1b,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのレベル")]
            UNIT_LEVEL = 0x1c,
            [ParameterDesc("ユニットのHP"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_HP = 0x1d,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの最大HP")]
            UNIT_HPMAX = 30,
            [ParameterDesc("ユニットの攻撃力"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_ATK = 0x1f,
            [ParameterDesc("ユニットの魔力"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_MAG = 0x20,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのアイコン")]
            UNIT_ICON = 0x21,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの名前")]
            UNIT_NAME = 0x22,
            [EnumParameterDesc("ユニットのレアリティに応じてAnimatorにrareというint値を設定します。ImageArrayの場合レアリティに応じた番号のイメージを使用します。StarGaugeの場合現在のレアリティと最大のレアリティがそれぞれ現在値と最大値になります。", typeof(ERarity)), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_RARITY = 0x23,
            [ParameterDesc("パーティのリーダースキルの名前")]
            PARTY_LEADERSKILLNAME = 0x24,
            [ParameterDesc("パーティのリーダースキルの説明")]
            PARTY_LEADERSKILLDESC = 0x25,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの防御力")]
            UNIT_DEF = 0x26,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの魔法防御力")]
            UNIT_MND = 0x27,
            [ParameterDesc("ユニットの素早さ"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_SPEED = 40,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの運")]
            UNIT_LUCK = 0x29,
            [ParameterDesc("ユニットジョブ名"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBNAME = 0x2a,
            [ParameterDesc("ユニットジョブランク"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBRANK = 0x2b,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), EnumParameterDesc("ユニット属性にあわせてAnimatorのelementという名前のInt値、もしくはImageArrayを切り替えます。", typeof(EElement))]
            UNIT_ELEMENT = 0x2c,
            [InstanceTypes(typeof(GameParameter.PartyAttackTypes)), ParameterDesc("パーティの総攻撃力")]
            PARTY_TOTALATK = 0x2d,
            [UsesIndex, ParameterDesc("インベントリーにセットされたアイテムのアイコン\n*スロット番号をIndexで指定")]
            INVENTORY_ITEMICON = 0x2e,
            [ParameterDesc("インベントリーにセットされたアイテムの名前*スロット番号をIndexで指定"), UsesIndex]
            INVENTORY_ITEMNAME = 0x2f,
            [ParameterDesc("アイテムの名前"), InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), UsesIndex]
            ITEM_NAME = 0x30,
            [ParameterDesc("アイテムの説明"), InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), UsesIndex]
            ITEM_DESC = 0x31,
            [UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), ParameterDesc("アイテムの売却価格")]
            ITEM_SELLPRICE = 50,
            [ParameterDesc("アイテムの購入価格"), UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes))]
            ITEM_BUYPRICE = 0x33,
            [InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), ParameterDesc("アイテムの所持個数"), UsesIndex]
            ITEM_AMOUNT = 0x34,
            [UsesIndex, ParameterDesc("インベントリーにセットされたアイテムの所持数*スロット番号をIndexで指定")]
            INVENTORY_ITEMAMOUNT = 0x35,
            [ParameterDesc("所持ユニット数")]
            PLAYER_NUMUNITS = 0x36,
            [ParameterDesc("所持可能の最大ユニット数")]
            PLAYER_MAXUNITS = 0x37,
            [ParameterDesc("選択しているグリッドの高さ")]
            GRID_HEIGHT = 0x38,
            [ParameterDesc("スキルの名前")]
            SKILL_NAME = 0x39,
            [ParameterDesc("スキルのアイコン")]
            SKILL_ICON = 0x3a,
            [ParameterDesc("スキルの説明")]
            SKILL_DESCRIPTION = 0x3b,
            [ParameterDesc("スキルの消費ジュエル")]
            SKILL_MP = 60,
            [ParameterDesc("クエストで入手したゴールド")]
            BATTLE_GOLD = 0x3d,
            [ParameterDesc("クエストで入手した宝箱の個数")]
            BATTLE_TREASURE = 0x3e,
            [ParameterDesc("ユニットのMP"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_MP = 0x3f,
            [ParameterDesc("ユニットの最大MP"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_MPMAX = 0x40,
            [ParameterDesc("ターゲットの出力ポイント"), InstanceTypes(typeof(GameParameter.TargetInstanceTypes))]
            TARGET_OUTPUTPOINT = 0x41,
            [ParameterDesc("ターゲットのクリティカル率"), InstanceTypes(typeof(GameParameter.TargetInstanceTypes))]
            TARGET_CRITICALRATE = 0x42,
            [EnumParameterDesc("ターゲットの行動の種類にあわせてAnimatorにstate(Int)を設定する。", typeof(SceneBattle.TargetActionTypes)), InstanceTypes(typeof(GameParameter.TargetInstanceTypes))]
            TARGET_ACTIONTYPE = 0x43,
            [ParameterDesc("アビリティのアイコン")]
            ABILITY_ICON = 0x44,
            [ParameterDesc("アビリティの名前")]
            ABILITY_NAME = 0x45,
            [ParameterDesc("クエストで入手可能な欠片のアイコン"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_KAKERA_ICON = 70,
            [ParameterDesc("ユニットの経験値"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_EXP = 0x47,
            [ParameterDesc("ユニットのレベルアップに必要な経験値の合計"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_EXPMAX = 0x48,
            [ParameterDesc("ユニットのレベルアップに必要な経験値の残り"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_EXPTOGO = 0x49,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの覚醒に必要な欠片の所持数")]
            UNIT_KAKERA_NUM = 0x4a,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの覚醒に必要な欠片の数")]
            UNIT_KAKERA_MAX = 0x4b,
            [ParameterDesc("装備品の経験値 (未実装)")]
            EQUIPMENT_EXP = 0x4c,
            [ParameterDesc("装備品の強化に必要な経験値 (未実装)")]
            EQUIPMENT_EXPMAX = 0x4d,
            [ParameterDesc("装備品のランク。Animatorであればrankというint値にランクを設定する。ImageArrayであればランクに対応したイメージを使用する。")]
            EQUIPMENT_RANK = 0x4e,
            [ParameterDesc("アビリティのレベル")]
            ABILITY_RANK = 0x4f,
            [ParameterDesc("アビリティの経験値")]
            OBSOLETE_ABILITY = 80,
            [ParameterDesc("アビリティの最大経験値")]
            ABILITY_NEXTGOLD = 0x51,
            [EnumParameterDesc("アビリティの種類にあわせて、Animatorのtype、ImageArrayを切り替えます。", typeof(EAbilitySlot))]
            ABILITY_SLOT = 0x52,
            [ParameterDesc("ユニットのIndexで指定したジョブのアイコン"), UsesIndex, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOB_JOBICON = 0x53,
            [ParameterDesc("ユニットのIndexで指定したジョブのランク"), UsesIndex, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOB_RANK = 0x54,
            [ParameterDesc("ユニットのIndexで指定したジョブの名前"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), UsesIndex]
            UNIT_JOB_NAME = 0x55,
            [UsesIndex, InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのIndexで指定したジョブの最大ランク")]
            UNIT_JOB_RANKMAX = 0x56,
            [ParameterDesc("装備アイテムのHP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_HP = 0x57,
            [ParameterDesc("装備アイテムのAP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_AP = 0x58,
            [ParameterDesc("装備アイテムの初期AP。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_IAP = 0x59,
            [AlwaysUpdate, ParameterDesc("装備アイテムの攻撃力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_ATK = 90,
            [ParameterDesc("装備アイテムの防御力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_DEF = 0x5b,
            [AlwaysUpdate, ParameterDesc("装備アイテムの魔法攻撃力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_MAG = 0x5c,
            [ParameterDesc("装備アイテムの魔法防御力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_MND = 0x5d,
            [AlwaysUpdate, ParameterDesc("装備アイテムの回復力。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_REC = 0x5e,
            [ParameterDesc("装備アイテムの速度。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_SPD = 0x5f,
            [ParameterDesc("装備アイテムのクリティカル。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_CRI = 0x60,
            [AlwaysUpdate, ParameterDesc("装備アイテムの運。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_LUK = 0x61,
            [ParameterDesc("装備アイテムの移動量。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_MOV = 0x62,
            [AlwaysUpdate, ParameterDesc("装備アイテムの移動高低差。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_JMP = 0x63,
            [AlwaysUpdate, ParameterDesc("装備アイテムの射程。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。")]
            EQUIPMENT_RANGE = 100,
            [ParameterDesc("装備アイテムの範囲。空のゲームオブジェクトの場合は値が0の時自身を非表示にします。"), AlwaysUpdate]
            EQUIPMENT_SCOPE = 0x65,
            [AlwaysUpdate, ParameterDesc("装備アイテムの影響可能な高低差範囲。値が0の時、子供を非表示にし、LayoutElementを無効にします。")]
            EQUIPMENT_EFFECTHEIGHT = 0x66,
            [ParameterDesc("装備アイテムの名前")]
            EQUIPMENT_NAME = 0x67,
            [ParameterDesc("装備アイテムのアイコン")]
            EQUIPMENT_ICON = 0x68,
            [ParameterDesc("アビリティ強化に使用できるポイントの残り")]
            OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_NUM = 0x69,
            [ParameterDesc("アビリティを強化できる回数")]
            OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNT = 0x6a,
            [ParameterDesc("アビリティを強化できる回数の最大値")]
            OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_RANKUPCOUNTMAX = 0x6b,
            [AlwaysUpdate, ParameterDesc("アビリティを強化できる回数のクールダウン時間。\nクールダウン時間が無い場合は子供を非表示にします。")]
            OBSOLETE_GLOBAL_PLAYER_ABILITYPOINT_COOLDOWNTIME = 0x6c,
            [ParameterDesc("装備アイテムの所持数")]
            EQUIPMENT_AMOUNT = 0x6d,
            [ParameterDesc("装備アイテムを装備するために必要なレベル")]
            EQUIPMENT_REQLV = 110,
            [ParameterDesc("進化素材の所持個数。スライダー対応")]
            JOBEVOITEM_AMOUNT = 0x6f,
            [ParameterDesc("進化素材の必要個数")]
            JOBEVOITEM_REQAMOUNT = 0x70,
            [ParameterDesc("進化素材のアイコン")]
            JOBEVOITEM_ICON = 0x71,
            [ParameterDesc("進化素材の名前")]
            JOBEVOITEM_NAME = 0x72,
            [ParameterDesc("ユニットの現在のジョブを進化させるのに必要なゴールド")]
            UNIT_EVOCOST = 0x73,
            [ParameterDesc("ユニットのクリティカル値"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_CRIT = 0x74,
            [ParameterDesc("ユニットの回復力"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_REGEN = 0x75,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットが持つリーダースキルの名前")]
            UNIT_LEADERSKILLNAME = 0x76,
            [ParameterDesc("ユニットが持つリーダースキルの説明"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_LEADERSKILLDESC = 0x77,
            [ParameterDesc("アイテムの効果値")]
            ITEM_VALUE = 120,
            [ParameterDesc("ユニットのレベルの最大値")]
            UNIT_LEVELMAX = 0x79,
            [ParameterDesc("ユニットのIndexで指定したジョブの解放状態にあわせてAnimatorにBoolパラメーターunlockedを設定します。解放済みであればunlockedがオンになります。"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), UsesIndex]
            UNIT_JOB_UNLOCKSTATE = 0x7a,
            [ParameterDesc("ユニットの現在のジョブのランク"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBRANKMAX = 0x7b,
            [ParameterDesc("アビリティの解放条件")]
            ABILITY_UNLOCKINFO = 0x7c,
            [ParameterDesc("アビリティの説明")]
            ABILITY_DESC = 0x7d,
            [ParameterDesc("アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。"), InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), UsesIndex]
            ITEM_FRAME = 0x7e,
            [ParameterDesc("インベントリーにセットされたアイテムのフレーム。Item/Frameと同じです。"), UsesIndex]
            INVENTORY_FRAME = 0x7f,
            [ParameterDesc("アイテム作成素材の所持個数")]
            RECIPEITEM_AMOUNT = 0x80,
            [ParameterDesc("アイテム作成素材の必要個数")]
            RECIPEITEM_REQAMOUNT = 0x81,
            [ParameterDesc("アイテム作成素材のアイコン")]
            RECIPEITEM_ICON = 130,
            [ParameterDesc("アイテム作成素材の名前")]
            RECIPEITEM_NAME = 0x83,
            [ParameterDesc("アイテム作成コスト")]
            RECIPEITEM_CREATE_COST = 0x84,
            [ParameterDesc("作成アイテム名")]
            RECIPEITEM_CREATE_ITEM_NAME = 0x85,
            [ParameterDesc("アイテム作成素材のフレーム")]
            RECIPEITEM_FRAME = 0x86,
            [ParameterDesc("ユニットのキャライメージ (中サイズ) GameSettings.Unit_PortraitMedium で命名規則を決めれます。"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_PORTRAIT_MEDIUM = 0x87,
            [ParameterDesc("クエストで入手した補正値等も含めたゴールドの合計")]
            QUESTRESULT_GOLD = 0x88,
            [ParameterDesc("クエストでプレイヤーが得た経験値")]
            QUESTRESULT_PLAYEREXP = 0x89,
            [ParameterDesc("クエストでパーティが得た経験値")]
            QUESTRESULT_PARTYEXP = 0x8a,
            [ParameterDesc("クエストの評価結果にあわせてAnimatorのrate(int)、ImageArrayを切り替えます。※使用してない")]
            QUESTRESULT_RATE = 0x8b,
            [ParameterDesc("プレイヤーのレベルアップ前のレベル")]
            PLAYERLEVELUP_LEVEL = 140,
            [ParameterDesc("プレイヤーのレベルアップ後のレベル")]
            PLAYERLEVELUP_LEVELNEXT = 0x8d,
            [ParameterDesc("プレイヤーのレベルアップ後のスタミナ")]
            PLAYERLEVELUP_STAMINA = 0x8e,
            [ParameterDesc("プレイヤーのレベルアップ前の最大スタミナ")]
            PLAYERLEVELUP_STAMINAMAX = 0x8f,
            [ParameterDesc("プレイヤーのレベルアップ後の最大スタミナ")]
            PLAYERLEVELUP_STAMINAMAXNEXT = 0x90,
            [ParameterDesc("プレイヤーのレベルアップ前の最大フレンドスロット数")]
            PLAYERLEVELUP_FRIENDNUM = 0x91,
            [ParameterDesc("プレイヤーのレベルアップ後の最大フレンドスロット数")]
            PLAYERLEVELUP_FRIENDNUMNEXT = 0x92,
            [UsesIndex, ParameterDesc("アンロックされた物の説明。インデックスで行数を指定してください。")]
            PLAYERLEVELUP_UNLOCKINFO = 0x93,
            [EnumParameterDesc("プレイ中クエストのボーナス条件の達成状態にあわせてAnimatorのstate(int)、ImageArrayを切り替えます。インデックスでボーナス条件の番号を指定してください。", typeof(QuestBonusObjectiveState)), UsesIndex, InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_BONUSOBJECTIVE_STATE = 0x94,
            OBSOLETE_QUEST_BONUSOBJECTIVE_ITEMICON = 0x95,
            OBSOLETE_QUEST_BONUSOBJECTIVE_ITEMAMOUNT = 150,
            [EnumParameterDesc("ユニットの陣営にあわせてImageArrayのインデックス、Animatorのindex(int)を切り替えます。", typeof(EUnitSide))]
            UNIT_SIDE = 0x97,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのジョブのアイコン")]
            UNIT_JOBICON = 0x98,
            [ParameterDesc("ユニットの現ジョブのアイコン。GameSettingsのJobIcon2のパスを使用する。"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBICON2 = 0x99,
            [UsesIndex, ParameterDesc("ユニットのIndexで指定したジョブのアイコン。GameSettingsのJobIcon2のパスを使用する。"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOB_JOBICON2 = 0x9a,
            [InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), UsesIndex, ParameterDesc("アイテムの作成コスト")]
            ITEM_CREATECOST = 0x9b,
            [ParameterDesc("プレイヤーの現在の洞窟用スタミナ")]
            GLOBAL_PLAYER_CAVESTAMINA = 0x9c,
            [ParameterDesc("プレイヤーの最大の洞窟用スタミナ")]
            GLOBAL_PLAYER_CAVESTAMINAMAX = 0x9d,
            [ParameterDesc("プレイヤーの洞窟用スタミナが次に回復するまでの時間")]
            GLOBAL_PLAYER_CAVESTAMINATIME = 0x9e,
            [ParameterDesc("アイテムの種類ごとの所持上限"), UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes))]
            ITEM_AMOUNTMAX = 0x9f,
            [ParameterDesc("所持しているアイテムの種類")]
            PLAYER_NUMITEMS = 160,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストが通常クエストかエリートクエストかどうかでImageArrayのインデックスを切り替えます。0=通常、1=エリート、2=エクストラ")]
            QUEST_DIFFICULTY = 0xa1,
            [ParameterDesc("ユニットの現在位置の高さ"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_HEIGHT = 0xa2,
            [ParameterDesc("装備アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。"), UsesIndex]
            EQUIPMENT_FRAME = 0xa3,
            [ParameterDesc("クエストリストで使用するチャプター(章)の名前")]
            QUESTLIST_CHAPTERNAME = 0xa4,
            [ParameterDesc("クエストリストで使用するセクション(部)の名前")]
            QUESTLIST_SECTIONNAME = 0xa5,
            [ParameterDesc("メールの文面")]
            MAIL_MESSAGE = 0xa6,
            [ParameterDesc("マルチクエストが通常マップかイベントマップかどうかでImageArrayのインデックスを切り替えます。0=通常、1=イベント"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_MULTI_TYPE = 0xa7,
            [ParameterDesc("マルチプレイヤーの名前")]
            MULTI_PLAYER_NAME = 0xa8,
            [ParameterDesc("マルチプレイヤーのレベル")]
            MULTI_PLAYER_LEVEL = 0xa9,
            [UsesIndex, InstanceTypes(typeof(JSON_MyPhotonPlayerParam.EState)), AlwaysUpdate, ParameterDesc("マルチプレイヤーの状態( Index: 0 == 否定 / 1 == 完全一致")]
            MULTI_PLAYER_STATE = 170,
            [AlwaysUpdate, UsesIndex, ParameterDesc("マルチプレイヤーのユニットアイコン")]
            MULTI_PLAYER_UNIT_ICON = 0xab,
            [ParameterDesc("メールで付与されるアイテムの文字列表現")]
            MAIL_GIFT_STRING = 0xac,
            [ParameterDesc("メールの有効期限")]
            MAIL_GIFT_LIMIT = 0xad,
            [ParameterDesc("メールを既読にした日時")]
            MAIL_GIFT_GETAT = 0xae,
            [ParameterDesc("マルチ部屋リストのコメント")]
            MULTI_ROOM_LIST_COMMENT = 0xaf,
            [ParameterDesc("マルチ部屋リストの部屋主名")]
            MULTI_ROOM_LIST_OWNER_NAME = 0xb0,
            [ParameterDesc("マルチ部屋リストの部屋主レベル")]
            MULTI_ROOM_LIST_OWNER_LV = 0xb1,
            [ParameterDesc("マルチ部屋リストのクエスト名")]
            MULTI_ROOM_LIST_QUEST_NAME = 0xb2,
            [UsesIndex, ParameterDesc("マルチ部屋リストの鍵アイコン. 0:鍵あり 1:鍵なし"), AlwaysUpdate]
            MULTI_ROOM_LIST_LOCKED_ICON = 0xb3,
            [ParameterDesc("マルチ部屋リストの参加人数")]
            MULTI_ROOM_LIST_PLAYER_NUM = 180,
            [ParameterDesc("プレイヤーのフレンドコード")]
            GLOBAL_PLAYER_FRIENDCODE = 0xb5,
            [ParameterDesc("フレンドのフレンドコード")]
            FRIEND_FRIENDCODE = 0xb6,
            [ParameterDesc("フレンドの名前")]
            FRIEND_NAME = 0xb7,
            [ParameterDesc("フレンドのレベル")]
            FRIEND_LEVEL = 0xb8,
            [ParameterDesc("フレンドの最終ログイン")]
            FRIEND_LASTLOGIN = 0xb9,
            [ParameterDesc("所持可能の最大アイテム数")]
            PLAYER_MAXITEMS = 0xba,
            [ParameterDesc("売却アイテムの選択数分の価格")]
            SHOP_ITEM_SELLPRICE = 0xbb,
            [ParameterDesc("売却アイテムの数")]
            SHOP_ITEM_SELLNUM = 0xbc,
            [ParameterDesc("売却アイテムの選択インデックス")]
            SHOP_ITEM_SELLINDEX = 0xbd,
            [ParameterDesc("売却アイテムの選択数")]
            SHOP_ITEM_SELLSELECTCOUNT = 190,
            [ParameterDesc("ショップ総売却価格")]
            SHOP_SELLPRICETOTAL = 0xbf,
            [ParameterDesc("ショップアイテムのインベントリ設定状態で表示状態を切り替え"), AlwaysUpdate]
            SHOP_ITEM_STATE_INVENTORY = 0xc0,
            [ParameterDesc("ショップアイテムの設置数を取得")]
            SHOP_ITEM_BUYAMOUNT = 0xc1,
            [ParameterDesc("ショップアイテムの購入総額を取得")]
            SHOP_ITEM_BUYPRICE = 0xc2,
            [ParameterDesc("ショップアイテムの売却済み状態で表示状態を切り替え"), AlwaysUpdate]
            SHOP_ITEM_STATE_SOLDOUT = 0xc3,
            [ParameterDesc("ショップアイテムの購入通貨別のアイコン"), UsesIndex]
            SHOP_ITEM_BUYTYPEICON = 0xc4,
            [AlwaysUpdate, ParameterDesc("ショップアイテムの売却選択状態で表示状態を切り替え")]
            SHOP_ITEM_STATE_SELLSELECT = 0xc5,
            [ParameterDesc("ショップアイテムのアイコン上の売却数")]
            SHOP_ITEM_ICONSELLNUM = 0xc6,
            [ParameterDesc("装備可能ユニットが存在する場合のバッジアイコンの表示状態の切り替え"), AlwaysUpdate]
            SHOP_ITEM_STATE_ENABLEEQUIPUNIT = 0xc7,
            [ParameterDesc("ショップアイテムの商品一覧の更新時間")]
            SHOP_ITEM_UPDATETIME = 200,
            [ParameterDesc("プレイヤーに来ているフレンド申請通知の数")]
            PLAYER_FRIENDREQUESTNUM = 0xc9,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのIndexで指定したジョブのクラスチェンジ先のジョブのアイコン"), UsesIndex]
            UNIT_JOB_CLASSCHANGE_JOBICON = 0xca,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのIndexで指定したジョブの名前"), UsesIndex]
            UNIT_JOB_CLASSCHANGE_NAME = 0xcb,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのIndexで指定したジョブのクラスチェンジ先のジョブのアイコン"), UsesIndex]
            UNIT_JOB_CLASSCHANGE_JOBICON2 = 0xcc,
            [ParameterDesc("ショップアイテムのアイコン上の売却数表示切り替え"), AlwaysUpdate]
            SHOP_ITEM_ICONSELLNUMSHOWED = 0xcd,
            [ParameterDesc("プレイヤーのフレンド保持上限")]
            PLAYER_FRIENDMAX = 0xce,
            [ParameterDesc("プレイヤーの保持しているフレンドの数")]
            PLAYER_FRIENDNUM = 0xcf,
            [ParameterDesc("ユニットの長い説明文"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_PROFILETEXT = 0xd0,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのイメージ画像")]
            UNIT_IMAGE = 0xd1,
            [ParameterDesc("マルチ部屋リストの募集人数")]
            MULTI_ROOM_LIST_PLAYER_NUM_MAX = 210,
            [UsesIndex, AlwaysUpdate, ParameterDesc("マルチプレイヤーのユニットアイコンフレーム")]
            MULTI_PLAYER_UNIT_ICON_FRAME = 0xd3,
            [ParameterDesc("マルチプレイヤーのプレイヤーID")]
            MULTI_PLAYER_INDEX = 0xd4,
            [AlwaysUpdate, ParameterDesc("マルチプレイヤーが部屋主のときに表示")]
            MULTI_PLAYER_IS_ROOM_OWNER = 0xd5,
            [ParameterDesc("マルチプレイヤーがいないときに表示"), AlwaysUpdate]
            MULTI_PLAYER_IS_EMPTY = 0xd6,
            [AlwaysUpdate, ParameterDesc("マルチプレイヤーがいるときに表示")]
            MULTI_PLAYER_IS_VALID = 0xd7,
            [ParameterDesc("実績の名前")]
            TROPHY_NAME = 0xd8,
            [AlwaysUpdate, ParameterDesc("共闘マルチのとき表示")]
            MULTI_ROOM_TYPE_IS_RAID = 0xd9,
            [AlwaysUpdate, ParameterDesc("対戦マルチのとき表示")]
            MULTI_ROOM_TYPE_IS_VERSUS = 0xda,
            [ParameterDesc("マルチパーティの総攻撃力")]
            MULTI_PARTY_TOTALATK = 0xdb,
            [ParameterDesc("現在のユニット操作プレイヤーID"), AlwaysUpdate]
            MULTI_CURRENT_PLAYER_INDEX = 220,
            [ParameterDesc("自キャラ行動までの残りターン"), AlwaysUpdate]
            MULTI_MY_NEXT_TURN = 0xdd,
            [AlwaysUpdate, ParameterDesc("残りの入力制限時間")]
            MULTI_INPUT_TIME_LIMIT = 0xde,
            [AlwaysUpdate, ParameterDesc("現在のユニット操作プレイヤー名")]
            MULTI_CURRENT_PLAYER_NAME = 0xdf,
            [ParameterDesc("鍵つき部屋を作るとき表示"), AlwaysUpdate]
            QUEST_MULTI_LOCK = 0xe0,
            [ParameterDesc("現在の部屋コメント"), AlwaysUpdate]
            MULTI_CURRENT_ROOM_COMMENT = 0xe1,
            [UsesIndex, ParameterDesc("現在の部屋パスコード/0 == 半角 / 1 == 全角")]
            MULTI_CURRENT_ROOM_PASSCODE = 0xe2,
            [ParameterDesc("ユニットが不参加スロット枠のとき表示"), UsesIndex, AlwaysUpdate]
            MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE = 0xe3,
            [ParameterDesc("現在の部屋のクエスト名"), AlwaysUpdate]
            MULTI_CURRENT_ROOM_QUEST_NAME = 0xe4,
            [ParameterDesc("マルチプレイのとき非表示(0)/表示(1)/NotInteractive(2)/Interactive(3)"), UsesIndex, AlwaysUpdate]
            QUEST_IS_MULTI = 0xe5,
            [InstanceTypes(typeof(GameParameter.TrophyConditionInstances)), ParameterDesc("実績の条件のテキスト"), UsesIndex]
            TROPHY_CONDITION_TITLE = 230,
            [UsesIndex, ParameterDesc("実績の条件のカウント、スライダーにもできるよ"), InstanceTypes(typeof(GameParameter.TrophyConditionInstances))]
            TROPHY_CONDITION_COUNT = 0xe7,
            [UsesIndex, ParameterDesc("実績の条件の必要カウント"), InstanceTypes(typeof(GameParameter.TrophyConditionInstances))]
            TROPHY_CONDITION_COUNTMAX = 0xe8,
            [ParameterDesc("アイテムの素材経験値"), InstanceTypes(typeof(GameParameter.ItemInstanceTypes))]
            ITEM_ENHANCEPOINT = 0xe9,
            [ParameterDesc("装備アイテムの強化素材の選択数")]
            EQUIPITEM_ENHANCE_MATERIALSELECTCOUNT = 0xea,
            [AlwaysUpdate, ParameterDesc("アイテム所持数によって表示状態を変更（0個の場合非表示）"), InstanceTypes(typeof(GameParameter.ItemInstanceTypes))]
            ITEM_SHOWED_AMOUNT = 0xeb,
            [ParameterDesc("強化パラメータ名")]
            EQUIPITEM_PARAMETER_NAME = 0xec,
            [ParameterDesc("装備アイテムの初期値")]
            EQUIPITEM_PARAMETER_INITVALUE = 0xed,
            [ParameterDesc("装備アイテムの上昇値")]
            EQUIPITEM_PARAMETER_RANKUPVALUE = 0xee,
            [AlwaysUpdate, ParameterDesc("装備アイテムの上昇量に応じて表示状態を変更")]
            EQUIPITEM_PARAMETER_SHOWED_RANKUPVALUE = 0xef,
            [ParameterDesc("装備アイテムの強化素材の選択個数によって表示状態を変更（選択数が0の場合は非表示）"), AlwaysUpdate]
            EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECTCOUNT = 240,
            [ParameterDesc("装備アイテムの強化素材の選択状態によって表示状態を変更（選択していない場合は非表示）"), AlwaysUpdate]
            EQUIPITEM_ENHANCE_SHOWED_MATERIALSELECT = 0xf1,
            [ParameterDesc("装備アイテムの強化ゲージ")]
            EQUIPITEM_ENHANCE_GAUGE = 0xf2,
            [ParameterDesc("装備アイテムの現在の強化ポイント")]
            EQUIPITEM_ENHANCE_CURRENTEXP = 0xf3,
            [ParameterDesc("装備アイテムのランクアップまでの強化ポイント")]
            EQUIPITEM_ENHANCE_NEXTEXP = 0xf4,
            [ParameterDesc("装備アイテムの強化前のランク")]
            EQUIPITEM_ENHANCE_RANKBEFORE = 0xf5,
            [ParameterDesc("装備アイテムの強化後のランク")]
            EQUIPITEM_ENHANCE_RANKAFTER = 0xf6,
            [AlwaysUpdate, ParameterDesc("装備アイテムのランクに応じたイメージを使用します")]
            EQUIPDATA_RANKBADGE = 0xf7,
            [ParameterDesc("機能がアンロックされている場合のみ表示"), InstanceTypes(typeof(UnlockTargets))]
            UNLOCK_SHOWED = 0xf8,
            [ParameterDesc("切断されたプレイヤーIndex")]
            MULTI_NOTIFY_DISCONNECTED_PLAYER_INDEX = 0xf9,
            [ParameterDesc("切断されたプレイヤーが(0:部屋主じゃなかったとき表示 1:他人が部屋主になったとき表示 2:自分が部屋主になったとき表示)"), UsesIndex]
            MULTI_NOTIFY_DISCONNECTED_PLAYER_IS_ROOM_OWNER = 250,
            [UsesIndex, AlwaysUpdate, ParameterDesc("行動順のプレイヤーが切断されているとき表示(0) 非表示(1)")]
            MULTI_CURRENT_PLAYER_IS_DISCONNECTED = 0xfb,
            [AlwaysUpdate, ParameterDesc("行動順のプレイヤーが部屋主かどうか")]
            MULTI_CURRENT_PLAYER_IS_ROOM_OWNER = 0xfc,
            [ParameterDesc("自分が部屋主のとき表示(0) 非表示(1)"), AlwaysUpdate, UsesIndex]
            MULTI_I_AM_ROOM_OWNER = 0xfd,
            [AlwaysUpdate, ParameterDesc("部屋主のプレイヤーIndex")]
            MULTI_ROOM_OWNER_PLAYER_INDEX = 0xfe,
            [ParameterDesc("ガチャでドロップしたものの名称")]
            GACHA_DROPNAME = 0xff,
            [AlwaysUpdate, ParameterDesc("達成済みデイリーミッションの有無で表示状態を切り替える"), InstanceTypes(typeof(GameParameter.TrophyBadgeInstanceTypes))]
            TROPHY_BADGE = 0x100,
            [AlwaysUpdate, ParameterDesc("実績の報酬ゴールド。ゴールドが0なら自身を非表示にする。")]
            TROPHY_REWARDGOLD = 0x101,
            [AlwaysUpdate, ParameterDesc("実績の報酬コイン。コインが0なら自身を非表示にする。")]
            TROPHY_REWARDCOIN = 0x102,
            [ParameterDesc("実績の報酬プレイヤー経験値。経験値が0なら自身を非表示にする。"), AlwaysUpdate]
            TROPHY_REWARDEXP = 0x103,
            [ParameterDesc("報酬に含まれる経験値")]
            REWARD_EXP = 260,
            [ParameterDesc("報酬に含まれるコイン")]
            REWARD_COIN = 0x105,
            [ParameterDesc("報酬に含まれるゴールド")]
            REWARD_GOLD = 0x106,
            [AlwaysUpdate, ParameterDesc("ユニットのお気に入りロック状態")]
            UNIT_FAVORITE = 0x107,
            [UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), ParameterDesc("装備アイテムの種類にあわせたフレームをImageに設定します。フレームの設定はGameSettings.ItemIconsを参照します。")]
            EQUIPDATA_FRAME = 0x108,
            [ParameterDesc("ジョブのランクにあわせてImageArrayを切り替えます"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBRANKFRAME = 0x109,
            [ParameterDesc("ローカルプレイヤーのレベルによってキャップされたユニットの最大レベル"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_CAPPEDLEVELMAX = 0x10a,
            [ParameterDesc("リビジョン番号")]
            APPLICATION_REVISION = 0x10b,
            [ParameterDesc("ビルドID")]
            APPLICATION_BUILD = 0x10c,
            [ParameterDesc("アセットのリビジョン番号")]
            APPLICATION_ASSETREVISION = 0x10d,
            [ParameterDesc("プロダクト名称")]
            PRODUCT_NAME = 270,
            [ParameterDesc("プロダクト値段")]
            PRODUCT_PRICE = 0x10f,
            [InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes)), ParameterDesc("アリーナプレイヤーの順位")]
            ARENAPLAYER_RANK = 0x110,
            [InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes)), ParameterDesc("アリーナプレイヤーの総攻撃力")]
            ARENAPLAYER_TOTALATK = 0x111,
            [InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes)), ParameterDesc("アリーナプレイヤーのリーダースキル")]
            ARENAPLAYER_LEADERSKILLNAME = 0x112,
            [InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes)), ParameterDesc("アリーナプレイヤーのリーダースキルの説明")]
            ARENAPLAYER_LEADERSKILLDESC = 0x113,
            [ParameterDesc("アリーナプレイヤーの名前"), InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes))]
            ARENAPLAYER_NAME = 0x114,
            [ParameterDesc("プレイヤーのアリーナランク")]
            GLOBAL_PLAYER_ARENARANK = 0x115,
            [ParameterDesc("チケット数")]
            QUEST_TICKET = 0x116,
            [ParameterDesc("チケット使用可能な場合のみ表示"), AlwaysUpdate]
            QUEST_IS_TICKET = 0x117,
            [ParameterDesc("アリーナの挑戦権")]
            GLOBAL_PLAYER_ARENATICKETS = 280,
            [ParameterDesc("アリーナのクールダウンタイム")]
            GLOBAL_PLAYER_ARENACOOLDOWNTIME = 0x119,
            [ParameterDesc("本日のマルチプレイ残り報酬獲得回数")]
            MULTI_REST_REWARD = 0x11a,
            [AlwaysUpdate, UsesIndex, ParameterDesc("ユニットが不参加スロット枠のとき押せない")]
            MULTI_CURRENT_ROOM_UNIT_SLOT_DISABLE_NOT_INTERACTIVE = 0x11b,
            [ParameterDesc("お客様コード")]
            GLOBAL_PLAYER_OKYAKUSAMACODE = 0x11c,
            [ParameterDesc("機能がアンロックされている場合のみ有効"), InstanceTypes(typeof(UnlockTargets))]
            UNLOCK_ENABLED = 0x11d,
            [ParameterDesc("機能がアンロックされていると表示されなくなる"), InstanceTypes(typeof(UnlockTargets)), AlwaysUpdate]
            UNLOCK_HIDDEN = 0x11e,
            [ParameterDesc("報酬に含まれるアリーナメダル")]
            REWARD_ARENAMEDAL = 0x11f,
            [ParameterDesc("ショップアイテムの商品一覧の更新日")]
            SHOP_ITEM_UPDATEDAY = 0x120,
            [ParameterDesc("アリーナプレイヤーのレベル"), InstanceTypes(typeof(GameParameter.ArenaPlayerInstanceTypes))]
            ARENAPLAYER_LEVEL = 0x121,
            [ParameterDesc("プレイヤーのVIPランク")]
            GLOBAL_PLAYER_VIPRANK = 290,
            [ParameterDesc("ユニットの装備品を更新"), UsesIndex]
            UNIT_EQUIPSLOT_UPDATE = 0x123,
            [ParameterDesc("ユニットパラメータ指定の初期状態でのアイコン表示")]
            UNITPARAM_ICON = 0x124,
            [ParameterDesc("ユニットパラメータ指定の初期状態でのレアリティ")]
            UNITPARAM_RARITY = 0x125,
            [ParameterDesc("ユニットパラメータ指定の初期状態でのジョブアイコン")]
            UNITPARAM_JOBICON = 0x126,
            [ParameterDesc("ユニットパラメータ指定の初期状態での欠片所持数")]
            UNITPARAM_PIECE_AMOUNT = 0x127,
            [ParameterDesc("ユニットパラメータ指定の初期状態での欠片必要数")]
            UNITPARAM_PIECE_NEED = 0x128,
            [ParameterDesc("ユニットパラメータ指定の初期状態での欠片ゲージ")]
            UNITPARAM_PIECE_GAUGE = 0x129,
            [AlwaysUpdate, ParameterDesc("ユニットパラメータ指定でアンロック可能か確認")]
            UNITPARAM_IS_UNLOCKED = 0x12a,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストで入手可能な欠片のフレーム")]
            QUEST_KAKERA_FRAME = 0x12b,
            [ParameterDesc("ユニットの連携値"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_COMBINATION = 300,
            [AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのジョブ変更可能か確認")]
            UNIT_STATE_JOBCHANGED = 0x12d,
            [AlwaysUpdate, ParameterDesc("ショップの主要通貨の表示状態")]
            SHOP_STATE_MAINCOSTFRAME = 0x12e,
            [ParameterDesc("ショップの主要通貨アイコン")]
            SHOP_MAINCOST_ICON = 0x12f,
            [ParameterDesc("ショップの主要通貨の所持量")]
            SHOP_MAINCOST_AMOUNT = 0x130,
            [ParameterDesc("対象ユニットの成長バッジ"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate]
            UNIT_BADGE_GROWUP = 0x131,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("対象ユニットの解放バッジ")]
            UNITPARAM_BADGE_UNLOCK = 0x132,
            [InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), ParameterDesc("アイテムで装備可能なユニットが存在する場合に表示状態を変更するバッジ"), AlwaysUpdate]
            ITEM_BADGE_EQUIPUNIT = 0x133,
            [AlwaysUpdate, ParameterDesc("ユニットのバッジ表示状態を変更")]
            BADGE_UNIT = 0x134,
            [AlwaysUpdate, ParameterDesc("ユニット強化のバッジ表示状態を変更")]
            BADGE_UNITENHANCED = 0x135,
            [ParameterDesc("ユニット開放のバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_UNITUNLOCKED = 310,
            [ParameterDesc("ガチャのバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_GACHA = 0x137,
            [ParameterDesc("ゴールドガチャのバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_GOLDGACHA = 0x138,
            [ParameterDesc("レアガチャのバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_RAREGACHA = 0x139,
            [AlwaysUpdate, ParameterDesc("アリーナのバッジ表示状態を変更")]
            BADGE_ARENA = 0x13a,
            [AlwaysUpdate, ParameterDesc("マルチプレイのバッジ表示状態を変更")]
            BADGE_MULTIPLAY = 0x13b,
            [ParameterDesc("デイリーミッションのバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_DAILYMISSION = 0x13c,
            [AlwaysUpdate, ParameterDesc("フレンドのバッジ表示状態を変更")]
            BADGE_FRIEND = 0x13d,
            [ParameterDesc("ギフトのバッジ表示状態を変更"), AlwaysUpdate]
            BADGE_GIFTBOX = 0x13e,
            [AlwaysUpdate, ParameterDesc("ショートカットメニューのバッジ表示状態を変更")]
            BADGE_SHORTCUTMENU = 0x13f,
            [ParameterDesc("現VIPランクにおけるVIPポイント。スライダー対応")]
            GLOBAL_PLAYER_VIPPOINT = 320,
            [ParameterDesc("現VIPランクにおける最大VIPポイント")]
            GLOBAL_PLAYER_VIPPOINTMAX = 0x141,
            [ParameterDesc("プレイヤーの所持コイン (固有無償幻晶石)")]
            GLOBAL_PLAYER_COINFREE = 0x142,
            [ParameterDesc("プレイヤーの所持コイン (固有有償幻晶石)")]
            GLOBAL_PLAYER_COINPAID = 0x143,
            [AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットが編成中のパーティメンバーか？")]
            UNIT_STATE_PARTYMEMBER = 0x144,
            [ParameterDesc("ログインボーナスの日付")]
            LOGINBONUS_DAYNUM = 0x145,
            None = 0x146,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate, ParameterDesc("ユニットがレベルソート中か？")]
            UNIT_STATE_LVSORT = 0x147,
            [ParameterDesc("ユニットがパラメータソート中か？"), AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_STATE_PARAMSORT = 0x148,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットのソート対象パラメータの値")]
            UNIT_SORTTYPE_VALUE = 0x149,
            [AlwaysUpdate, ParameterDesc("スキルの修得条件の表示有無")]
            SKILL_STATE_CONDITION = 330,
            [ParameterDesc("スキルの修得条件")]
            SKILL_CONDITION = 0x14b,
            [ParameterDesc("アビリティの修得条件")]
            ABILITY_CONDITION = 0x14c,
            [ParameterDesc("ガチャのコスト")]
            GACHA_COST = 0x14d,
            [ParameterDesc("ガチャの回数")]
            GACHA_NUM = 0x14e,
            [ParameterDesc("無料通常ガチャの残り回数")]
            GACHA_GOLD_RESTNUM = 0x14f,
            [ParameterDesc("無料通常ガチャの残り回数の表示状態変更")]
            GACHA_GOLD_STATE_RESTNUM = 0x150,
            [ParameterDesc("無料通常ガチャのインターバル時間表示"), AlwaysUpdate]
            GACHA_GOLD_TIMER = 0x151,
            [ParameterDesc("無料通常ガチャの状態によって表示状態変更"), AlwaysUpdate]
            GACHA_GOLD_STATE_TIMER = 0x152,
            [ParameterDesc("無料通常ガチャのボタン状態変更"), AlwaysUpdate]
            GACHA_GOLD_STATE_INTERACTIVE = 0x153,
            [ParameterDesc("無料レアガチャのインターバル時間表示"), AlwaysUpdate]
            GACHA_COIN_TIMER = 340,
            [ParameterDesc("無料レアガチャの状態によって表示状態変更"), AlwaysUpdate]
            GACHA_COIN_STATE_TIMER = 0x155,
            [ParameterDesc("無料レアガチャのボタン状態変更"), AlwaysUpdate]
            GACHA_COIN_STATE_INTERACTIVE = 0x156,
            [ParameterDesc("ユニットのイメージ画像2"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_IMAGE2 = 0x157,
            [UsesIndex, InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), ParameterDesc("アイテムのフレーバーテキスト")]
            ITEM_FLAVOR = 0x158,
            [AlwaysUpdate, UsesIndex, ParameterDesc("ユニット覚醒レベル"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_AWAKE = 0x159,
            [ParameterDesc("ユニットがフレンドか？"), AlwaysUpdate]
            SUPPORTER_ISFRIEND = 0x15a,
            [ParameterDesc("ユニット覚醒レベル")]
            SUPPORTER_COST = 0x15b,
            [ParameterDesc("サムネイル化されたジョブのアイコンをImageコンポーネントに設定します"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            Unit_ThumbnailedJobIcon = 0x15c,
            [ParameterDesc("マルチプレイヤーのユニットジョブランクフレーム"), UsesIndex, AlwaysUpdate]
            MULTI_PLAYER_UNIT_JOBRANKFRAME = 0x15d,
            [UsesIndex, AlwaysUpdate, ParameterDesc("マルチプレイヤーのユニットジョブランク")]
            MULTI_PLAYER_UNIT_JOBRANK = 350,
            [ParameterDesc("マルチプレイヤーのユニットジョブアイコン"), UsesIndex, AlwaysUpdate]
            MULTI_PLAYER_UNIT_JOBICON = 0x15f,
            [AlwaysUpdate, UsesIndex, ParameterDesc("マルチプレイヤーのユニットレア度")]
            MULTI_PLAYER_UNIT_RARITY = 0x160,
            [UsesIndex, ParameterDesc("マルチプレイヤーのユニット属性"), AlwaysUpdate]
            MULTI_PLAYER_UNIT_ELEMENT = 0x161,
            [ParameterDesc("マルチプレイヤーのユニットレベル"), UsesIndex, AlwaysUpdate]
            MULTI_PLAYER_UNIT_LEVEL = 0x162,
            [AlwaysUpdate, ParameterDesc("実績の報酬スタミナ。スタミナが0なら自身を非表示にする。")]
            TROPHY_REWARDSTAMINA = 0x163,
            [ParameterDesc("ジョブアイコン")]
            JOB_JOBICON = 0x164,
            [ParameterDesc("ジョブ名")]
            JOB_NAME = 0x165,
            [ParameterDesc("クエストでプレイヤーが得たマルチコイン")]
            QUESTRESULT_MULTICOIN = 0x166,
            [UsesIndex, ParameterDesc("本日のマルチプレイ残り報酬獲得回数が0のとき表示(0)/非表示(1)/受け取れたとき表示(2)/受け取れなかったとき表示(3)/今回が最後のうけとりのとき表示(4)")]
            MULTI_REST_REWARD_IS_ZERO = 0x167,
            [UsesIndex, AlwaysUpdate, ParameterDesc("ユニットが参加スロット枠のとき表示")]
            MULTI_CURRENT_ROOM_UNIT_SLOT_ENABLE = 360,
            [UsesIndex, AlwaysUpdate, ParameterDesc("ユニットが割り当てられているスロット枠のとき表示")]
            MULTI_CURRENT_ROOM_UNIT_SLOT_VALID = 0x169,
            [ParameterDesc("報酬に含まれるスタミナ")]
            REWARD_STAMINA = 0x16a,
            [ParameterDesc("当日クエストに挑戦した回数")]
            QUEST_CHALLENGE_NUM = 0x16b,
            [ParameterDesc("当日クエストに挑戦できる限度")]
            QUEST_CHALLENGE_MAX = 0x16c,
            [ParameterDesc("クエストの挑戦回数をリセットした数")]
            QUEST_RESET_NUM = 0x16d,
            [ParameterDesc("クエストの挑戦回数をリセットできる限度")]
            QUEST_RESET_MAX = 0x16e,
            [ParameterDesc("ジョブアイコン2")]
            JOB_JOBICON2 = 0x16f,
            [ParameterDesc("ユニットの国"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            OBSOLETE_UNIT_PROFILE_COUNTRY = 0x170,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの身長")]
            OBSOLETE_UNIT_PROFILE_HEIGHT = 0x171,
            [ParameterDesc("ユニットの体重"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            OBSOLETE_UNIT_PROFILE_WEIGHT = 370,
            [ParameterDesc("ユニットの誕生日"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            OBSOLETE_UNIT_PROFILE_BIRTHDAY = 0x173,
            [ParameterDesc("ユニットの星座"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            OBSOLETE_UNIT_PROFILE_ZODIAC = 0x174,
            [ParameterDesc("ユニットの血液型"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            OBSOLETE_UNIT_PROFILE_BLOOD = 0x175,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの好きなもの")]
            OBSOLETE_UNIT_PROFILE_FAVORITE = 0x176,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの趣味")]
            OBSOLETE_UNIT_PROFILE_HOBBY = 0x177,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【毒】")]
            UNIT_STATE_CONDITION_POISON = 0x178,
            [ParameterDesc("ユニットの状態異常【麻痺】"), AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_STATE_CONDITION_PARALYSED = 0x179,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【スタン】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_STUN = 0x17a,
            [AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【睡眠】")]
            UNIT_STATE_CONDITION_SLEEP = 0x17b,
            [ParameterDesc("ユニットの状態異常【魅了】"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate]
            UNIT_STATE_CONDITION_CHARM = 380,
            [AlwaysUpdate, ParameterDesc("ユニットの状態異常【石化】"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_STATE_CONDITION_STONE = 0x17d,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【暗闇】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_BLINDNESS = 0x17e,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【沈黙】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_DISABLESKILL = 0x17f,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【移動禁止】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_DISABLEMOVE = 0x180,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【攻撃禁止】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_DISABLEATTACK = 0x181,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの状態異常【ゾンビ化・狂乱】"), AlwaysUpdate]
            UNIT_STATE_CONDITION_ZOMBIE = 0x182,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate, ParameterDesc("ユニットの状態異常【死の宣告】")]
            UNIT_STATE_CONDITION_DEATHSENTENCE = 0x183,
            [ParameterDesc("ユニットの状態異常【狂化】"), AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_STATE_CONDITION_BERSERK = 0x184,
            [ParameterDesc("ユニットの状態異常【ノックバック無効】"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate]
            UNIT_STATE_CONDITION_DISABLEKNOCKBACK = 0x185,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), AlwaysUpdate, ParameterDesc("ユニットの状態異常【バフ無効】")]
            UNIT_STATE_CONDITION_DISABLEBUFF = 390,
            [ParameterDesc("ユニットの状態異常【デバフ無効】"), AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_STATE_CONDITION_DISABLEDEBUFF = 0x187,
            [ParameterDesc("ターン表示のユニット陣営フレーム"), AlwaysUpdate, InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            TURN_UNIT_SIDE_FRAME = 0x188,
            [ParameterDesc("JobSetの開放条件")]
            JOBSET_UNLOCKCONDITION = 0x189,
            [ParameterDesc("マルチで自キャラ生存数が0のとき表示(0)/非表示(1)"), UsesIndex]
            MULTI_REST_MY_UNIT_IS_ZERO = 0x18a,
            [AlwaysUpdate, UsesIndex, ParameterDesc("マルチ部屋画面で対象プレイヤーが自分のとき0:表示/1:非表示/2:ImageArrayのインデックス切り替え(0=自分 1=他人)/3:チーム編成ボタン/4:情報をみるボタン/5:チーム編成ボタン(マルチ塔)/6:プレイヤーがまだリザルト画面に存在するか")]
            MULTI_PLAYER_IS_ME = 0x18b,
            [ParameterDesc("クエストリストで使用するセクション(部)の説明")]
            QUESTLIST_SECTIONEXPR = 0x18c,
            [AlwaysUpdate, UsesIndex, ParameterDesc("マルチプレイの部屋に鍵がかかっているとき表示(0)/非表示(1)/部屋主かつ鍵ありで表示(2)/部屋主かつ鍵なしで表示(3)")]
            MULTI_CURRENT_ROOM_IS_LOCKED = 0x18d,
            [ParameterDesc("メールの受け取り日時")]
            MAIL_GIFT_RECEIVE = 0x18e,
            [ParameterDesc("クエストのタイムリミット")]
            QUEST_TIMELIMIT = 0x18f,
            [ParameterDesc("ユニットの現在のチャージタイム")]
            UNIT_CHARGETIME = 400,
            [ParameterDesc("ユニットのチャージタイム")]
            UNIT_CHARGETIMEMAX = 0x191,
            [ParameterDesc("ギミックオブジェクトの説明文")]
            GIMMICK_DESCRIPTION = 0x192,
            [ParameterDesc("プロダクトDesc 0:そのまま 1:前半 2:後半"), UsesIndex]
            PRODUCT_DESC = 0x193,
            [ParameterDesc("プロダクト個数 (x10)")]
            PRODUCT_NUMX = 0x194,
            [ParameterDesc("ユニットの器用さ (ver1.1以降で表示されます)"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_DEX = 0x195,
            [InstanceTypes(typeof(GameParameter.ArtifactInstanceTypes)), UsesIndex, ParameterDesc("アーティファクトの名前")]
            ARTIFACT_NAME = 0x196,
            [ParameterDesc("アーティファクトのフレーバーテキスト")]
            ARTIFACT_DESC = 0x197,
            [ParameterDesc("アーティファクトのボーナス条件")]
            ARTIFACT_SPEC = 0x198,
            [ParameterDesc("アーティファクトのレアリティ")]
            ARTIFACT_RARITY = 0x199,
            [ParameterDesc("アーティファクトのレアリティ (最大)")]
            ARTIFACT_RARITYCAP = 410,
            [ParameterDesc("アーティファクトの所持数")]
            ARTIFACT_NUM = 0x19b,
            [ParameterDesc("アーティファクトの売却額")]
            ARTIFACT_SELLPRICE = 0x19c,
            [ParameterDesc("アプリのバンドルバージョン")]
            APPLICATION_VERSION = 0x19d,
            [ParameterDesc("フレンドユニットの最大レベル")]
            SUPPORTER_UNITCAPPEDLEVELMAX = 0x19e,
            [ParameterDesc("欠片ポイント")]
            GLOBAL_PLAYER_PIECEPOINT = 0x19f,
            [ParameterDesc("ショップ欠片ポイント総売却価格")]
            SHOP_KAKERA_SELLPRICETOTAL = 0x1a0,
            [InstanceTypes(typeof(GameParameter.ItemInstanceTypes)), UsesIndex, ParameterDesc("魂の欠片の売却価格")]
            ITEM_KAKERA_PRICE = 0x1a1,
            [ParameterDesc("魂の欠片変換の選択数分の価格")]
            SHOP_ITEM_KAKERA_SELLPRICE = 0x1a2,
            [ParameterDesc("報酬に含まれるマルチコイン")]
            REWARD_MULTICOIN = 0x1a3,
            [ParameterDesc("報酬に含まれる欠片ポイント")]
            REWARD_KAKERACOIN = 420,
            [UsesIndex, ParameterDesc("クエスト出撃条件 (0)改行表記、指定文字なし/(1)一行表記、指定文字付/(2)改行表記、指定文字付")]
            QUEST_UNIT_ENTRYCONDITION = 0x1a5,
            [UsesIndex, AlwaysUpdate, ParameterDesc("クエスト出撃条件が設定されている場合に表示(0)/非表示(1)")]
            OBSOLETE_QUEST_IS_UNIT_ENTRYCONDITION = 0x1a6,
            [UsesIndex, ParameterDesc("クエストにユニットが出撃可能な場合に表示(0)/非表示(1)"), AlwaysUpdate]
            QUEST_IS_UNIT_ENABLEENTRYCONDITION = 0x1a7,
            [ParameterDesc("キャラクタークエスト：エピソード解放条件")]
            QUEST_CHARACTER_MAINUNITCONDITION = 0x1a8,
            [ParameterDesc("キャラクタークエスト：話数")]
            QUEST_CHARACTER_EPISODENUM = 0x1a9,
            [ParameterDesc("キャラクタークエスト：エピソード名")]
            QUEST_CHARACTER_EPISODENAME = 0x1aa,
            [ParameterDesc("限定ショップアイテムの残り購入可能数を取得")]
            SHOP_LIMITED_ITEM_BUYAMOUNT = 0x1ab,
            [ParameterDesc("ユニットのIndexで指定したジョブがマスターしている場合に表示。Indexが-1の場合は選択中のジョブで判定。JobDataが直接設定されている場合はバインドされたJobDataで判定"), UsesIndex, AlwaysUpdate]
            UNIT_IS_JOBMASTER = 0x1ac,
            [UsesIndex, AlwaysUpdate, ParameterDesc("ユニット覚醒レベル"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_NEXTAWAKE = 0x1ad,
            [ParameterDesc("操作時間が延長された際に表示する秒数")]
            MULTIPLAY_ADD_INPUTTIME = 430,
            [UsesIndex, ParameterDesc("ユニットの限界突破最大値に達している場合にIndex:0で非表示。Index:1で表示。"), AlwaysUpdate]
            UNIT_IS_AWAKEMAX = 0x1af,
            [ParameterDesc("コンフィグでオートプレイ選択時場合にIndex:0で表示。Index:1で非表示。"), AlwaysUpdate, UsesIndex]
            CONFIG_IS_AUTOPLAY = 0x1b0,
            [AlwaysUpdate, ParameterDesc("フレンドがお気に入りなら表示(0)/非表示(1)"), UsesIndex]
            FRIEND_ISFAVORITE = 0x1b1,
            [ParameterDesc("キャラクタークエスト出撃条件(0)改行表記/(1)一行表記"), UsesIndex]
            QUEST_CHARACTER_ENTRYCONDITION = 0x1b2,
            [AlwaysUpdate, ParameterDesc("キャラクタークエスト出撃条件が設定されている場合に表示(0)/非表示(1)"), UsesIndex]
            QUEST_CHARACTER_IS_ENTRYCONDITION = 0x1b3,
            [AlwaysUpdate, ParameterDesc("キャラクタークエスト出撃条件のタイトル表示")]
            QUEST_CHARACTER_CONDITIONATTENTION = 0x1b4,
            [ParameterDesc("復帰したプレイヤーINDEX")]
            MULTIPLAY_RESUME_PLAYER_INDEX = 0x1b5,
            [ParameterDesc("復帰したプレイヤーがホストか？")]
            MULTIPLAY_RESUME_PLAYER_IS_HOST = 0x1b6,
            [ParameterDesc("復帰したが、他プレイヤーがいない")]
            MULTIPLAY_RESUME_BUT_NOT_PLAYER = 0x1b7,
            [ParameterDesc("ショップごとに保持数を表示するイベントコイン")]
            EVENTCOIN_SHOPTYPEICON = 440,
            [ParameterDesc("イベントコイン一覧のアイテム名")]
            EVENTCOIN_ITEMNAME = 0x1b9,
            [ParameterDesc("イベントコイン一覧の説明")]
            EVENTCOIN_MESSAGE = 0x1ba,
            [ParameterDesc("イベントコイン一覧の所持数")]
            EVENTCOIN_POSSESSION = 0x1bb,
            [ParameterDesc("ショップアイテムのイベントコイン別アイコン")]
            EVENTCOIN_PRICEICON = 0x1bc,
            [ParameterDesc("イベントショップアイテムの残り購入可能数を取得")]
            SHOP_EVENT_ITEM_BUYAMOUNT = 0x1bd,
            [ParameterDesc("イベント終了までの時間")]
            TROPHY_REMAININGTIME = 0x1be,
            [ParameterDesc("お客様コードのみを表示"), UsesIndex]
            GLOBAL_PLAYER_OKYAKUSAMACODE2 = 0x1bf,
            [InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType)), ParameterDesc("対戦相手のユニット")]
            VERSUS_UNIT_IMAGE = 0x1c0,
            [InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType)), ParameterDesc("対戦相手の名前")]
            VERSUS_PLAYER_NAME = 0x1c1,
            [InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType)), ParameterDesc("対戦相手のレベル")]
            VERSUS_PLAYER_LEVEL = 450,
            [ParameterDesc("対戦相手の総合攻撃力"), InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType))]
            VERSUS_PLAYER_TOTALATK = 0x1c3,
            [ParameterDesc("対戦結果"), InstanceTypes(typeof(BattleCore.QuestResult))]
            VERSUS_RESULT = 0x1c4,
            [ParameterDesc("対戦ランク表示")]
            VERSUS_RANK = 0x1c5,
            [ParameterDesc("現在のランクポイントを表示")]
            VERSUS_RANK_POINT = 0x1c6,
            [ParameterDesc("ランクアップまでのポイントを表示")]
            VERSUS_RANK_NEXT_POINT = 0x1c7,
            [ParameterDesc("現在のランクのアイコン")]
            VERSUS_RANK_ICON = 0x1c8,
            [ParameterDesc("現在のランクのインデックス")]
            VERSUS_RANK_ICON_INDEX = 0x1c9,
            [ParameterDesc("部屋内プレイヤーのランクのアイコン")]
            VERSUS_ROOMPLAYER_RANK_ICON = 0x1ca,
            [ParameterDesc("部屋内プレイヤーのランクのインデックス")]
            VERSUS_ROOMPLAYER_RANK_ICON_INDEX = 0x1cb,
            [ParameterDesc("対戦マップのサムネイル")]
            VERSUS_MAP_THUMNAIL = 460,
            [ParameterDesc("マップ選択中のサムネイル")]
            VERSUS_MAP_THUMNAIL2 = 0x1cd,
            [ParameterDesc("マップ選択中のマップ名")]
            VERSUS_MAP_NAME = 0x1ce,
            [AlwaysUpdate, ParameterDesc("マップが選択されていれば表示")]
            VERSUS_MAP_SELECT = 0x1cf,
            [ParameterDesc("所有アリーナコイン")]
            SHOP_ARENA_COIN = 0x1d0,
            [ParameterDesc("所有マルチコイン")]
            SHOP_MULTI_COIN = 0x1d1,
            [ParameterDesc("プレイヤーの所持コイン (共通無償幻晶石)")]
            GLOBAL_PLAYER_COINCOM = 0x1d2,
            [ParameterDesc("プレイヤーの所持コイン (無償幻晶石　共通＆固有)")]
            GLOBAL_PLAYER_FREECOINSET = 0x1d3,
            [AlwaysUpdate, ParameterDesc("ユニット覚醒レベル2"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), UsesIndex]
            UNIT_AWAKE2 = 0x1d4,
            VS_ST = 0x3e7,
            [ParameterDesc("対戦の報酬タイプ")]
            VS_TOWER_REWARD_NAME = 0x3e8,
            [ParameterDesc("シーズン報酬受け取り可能か？")]
            VS_TOWER_SEASON_RECEIPT = 0x3e9,
            [AlwaysUpdate, ParameterDesc("PvPコインの枚数")]
            VS_COIN = 0x3ea,
            [AlwaysUpdate, ParameterDesc("フリーマッチでのpvpコイン受け取り可能枚数")]
            VS_REMAIN_COIN = 0x3eb,
            [ParameterDesc("タワーマッチ開催中か"), UsesIndex, AlwaysUpdate]
            VS_OPEN_TOWERMATCH = 0x3ec,
            [AlwaysUpdate, ParameterDesc("対戦の種類を表示")]
            VS_QUEST_CATEGORY = 0x3ed,
            [AlwaysUpdate, ParameterDesc("タワーマッチで連勝した際のボーナス表示")]
            VS_TOWER_WINBONUS = 0x3ee,
            [AlwaysUpdate, UsesIndex, ParameterDesc("観戦時の表示・非表示対応")]
            VS_AUDIENCE_DISPLAY = 0x3ef,
            [UsesIndex, ParameterDesc("Index == 0:1P勝利 / Index == 1:2P勝利 時に表示する"), AlwaysUpdate]
            VS_AUDIENCE_WIN_PLAYER = 0x3f0,
            [AlwaysUpdate, ParameterDesc("観戦時のときだけ表示する")]
            VS_AUDIENCE_ONLY_DISPLAY = 0x3f1,
            [AlwaysUpdate, ParameterDesc("観戦している部屋の種類")]
            VS_AUDIENCE_TYPE = 0x3f2,
            [AlwaysUpdate, ParameterDesc("現在の階層")]
            VS_TOWER_FLOOR = 0x3f3,
            [AlwaysUpdate, ParameterDesc("タワーマッチのときだけ表示する")]
            VS_TOWER_ONLY_DISPLAY = 0x3f4,
            [ParameterDesc("観戦者数"), AlwaysUpdate]
            VS_AUDIENCE_NUM = 0x3f5,
            [AlwaysUpdate, ParameterDesc("CPU対戦の難易度")]
            VS_CPU_DIFFICULTY = 0x3f6,
            [AlwaysUpdate, ParameterDesc("CPU対戦の名前")]
            VS_CPU_NAME = 0x3f7,
            [AlwaysUpdate, ParameterDesc("CPU対戦のレベル")]
            VS_CPU_LV = 0x3f8,
            [AlwaysUpdate, ParameterDesc("CPU対戦の攻撃力")]
            VS_CPU_TOTALATK = 0x3f9,
            VS_ED = 0x44a,
            [AlwaysUpdate, ParameterDesc("武具コンディション")]
            ARTIFACT_ST = 0x44b,
            [ParameterDesc("武具コンディション"), AlwaysUpdate]
            ARTIFACT_EVOLUTION_COND = 0x44c,
            [ParameterDesc("武具がお気に入りなら表示(0)/非表示(1)"), AlwaysUpdate, UsesIndex]
            ARTIFACT_ISFAVORITE = 0x44d,
            [AlwaysUpdate, ParameterDesc("武具の進化後の★の数")]
            ARTIFACT_EVOLUTION_RARITY = 0x44e,
            [UsesIndex, ParameterDesc("武具アイコン"), InstanceTypes(typeof(GameParameter.ArtifactInstanceTypes))]
            ARTIFACT_ICON = 0x44f,
            [InstanceTypes(typeof(GameParameter.ArtifactInstanceTypes)), UsesIndex, ParameterDesc("武具の種類にあわせたフレームをImageに設定します。")]
            ARTIFACT_FRAME = 0x450,
            [ParameterDesc("武具の個数"), UsesIndex, InstanceTypes(typeof(GameParameter.ArtifactInstanceTypes))]
            ARTIFACT_AMOUNT = 0x451,
            ARTIFACT_ED = 0x4ae,
            QUEST_UI_ST = 0x4af,
            [UsesIndex, ParameterDesc("クエストコンティニュー不可が設定されている場合に表示(0)/非表示(1)"), AlwaysUpdate]
            QUEST_IS_MAP_NO_CONTINUE = 0x4b0,
            [AlwaysUpdate, ParameterDesc("クエストダメージ制限が設定されている場合に表示(0)/非表示(1)"), UsesIndex]
            QUEST_IS_MAP_DAMAGE_LIMIT = 0x4b1,
            [AlwaysUpdate, ParameterDesc("クエスト情報の詳細テキスト Loc/japanese/quest_info.txt参照"), UsesIndex]
            QUEST_UI_DETAIL_INFO = 0x4b2,
            [AlwaysUpdate, ParameterDesc("クエスト出撃条件でチームが固定されていて、かつユニットが設定されている場合に表示(0)/非表示(1)"), UsesIndex]
            QUEST_IS_UNIT_ENTRYCONDITION_FORCE_AVAILABLEUNIT = 0x4b3,
            [AlwaysUpdate, ParameterDesc("クエスト出撃条件で出撃ユニットが制限されていた場合に表示")]
            QUEST_IS_UNIT_ENTRYCONDITION_LIMIT = 0x4b4,
            [ParameterDesc("クエスト出撃条件で出撃ユニットが固定されていた場合に表示"), AlwaysUpdate]
            QUEST_IS_UNIT_ENTRYCONDITION_FORCE = 0x4b5,
            [UsesIndex, AlwaysUpdate, ParameterDesc("クエストをクリアしていた場合にinteractable=true(0)/false(1)")]
            QUEST_IS_CLEARD_INTERACTABLE = 0x4b6,
            [UsesIndex, AlwaysUpdate, ParameterDesc("クエストユニット交代が許可されている場合に表示(0)/非表示(1)")]
            QUEST_IS_UNIT_CHANGE = 0x4b7,
            QUEST_IS_ED = 0x512,
            TRICK_ST = 0x513,
            [ParameterDesc("特殊パネルの名称")]
            TRICK_NAME = 0x514,
            [ParameterDesc("特殊パネルの説明")]
            TRICK_DESC = 0x515,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("特殊パネルUIアイコン")]
            TRICK_UI_ICON = 0x516,
            TRICK_ED = 0x576,
            BATTLE_UI_ST = 0x577,
            [ParameterDesc("オーダーユニットが詠唱中か？")]
            BATTLE_UI_IMG_IS_CAST_ORDER = 0x578,
            [ParameterDesc("スキルの使用回数")]
            SKILL_USE_COUNT = 0x579,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), EnumParameterDesc("スキル属性にあわせてImageArrayを切り替えます。(属性がない場合は非表示)", typeof(EElement))]
            SKILL_ELEMENT = 0x57a,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), EnumParameterDesc("スキル攻撃詳細区分にあわせてImageArrayを切り替えます。(攻撃詳細区分がない場合は非表示)", typeof(AttackDetailTypes))]
            SKILL_ATTACK_DETAIL = 0x57b,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), EnumParameterDesc("スキル攻撃タイプにあわせてImageArrayを切り替えます。(攻撃タイプがない場合は非表示)", typeof(AttackTypes))]
            SKILL_ATTACK_TYPE = 0x57c,
            [ParameterDesc("天候が発動していれば表示(0)/非表示(1)")]
            BATTLE_UI_WTH_IS_ENABLE = 0x57d,
            [ParameterDesc("天候名を表示")]
            BATTLE_UI_WTH_NAME = 0x57e,
            [ParameterDesc("天候アイコンを表示")]
            BATTLE_UI_WTH_ICON = 0x57f,
            BATTLE_UI_ED = 0x5da,
            UNIT_EXTRA_PARAM_ST = 0x5db,
            [InstanceTypes(typeof(GameParameter.UnitInstanceTypes)), ParameterDesc("ユニットの移動力")]
            UNIT_EXTRA_PARAM_MOVE = 0x5dc,
            [ParameterDesc("ユニットのジャンプ力"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_EXTRA_PARAM_JUMP = 0x5dd,
            UNIT_EXTRA_PARAM_ED = 0x63e,
            MULTI_UI_ST = 0x63f,
            [ParameterDesc("マルチのユニットレベル制限")]
            MULTI_UI_ROOM_LIMIT_UNITLV = 0x640,
            [ParameterDesc("クリアの有無")]
            MULTI_UI_ROOM_LIMIT_CLEAR = 0x641,
            [AlwaysUpdate, ParameterDesc("現在の操作プレイヤー名を表示 [0]:切断時グレー表示 [1]:思考中追加 [2]～のターン追加"), UsesIndex]
            MULTI_UI_CURRENT_PLAYER_NAME = 0x642,
            [ParameterDesc("マルチタワーでの総攻撃力")]
            MULTI_TOWER_UI_PARTY_TOTALATK = 0x643,
            [ParameterDesc("オーナーの状態"), AlwaysUpdate, InstanceTypes(typeof(JSON_MyPhotonPlayerParam.EState)), UsesIndex]
            MULTI_OWNER_STATE = 0x644,
            [ParameterDesc("マルチタワー階層")]
            MULTI_TOWER_FLOOR = 0x645,
            [ParameterDesc("リーダースキル有効なクエストか？")]
            MULTI_QUEST_IS_LEADERSKILL = 0x646,
            [ParameterDesc("マルチ部屋リストの階層")]
            MULTI_TOWER_ROOM_LIST_FLOOR = 0x647,
            [ParameterDesc("マルチ部屋リストのクリア済みか")]
            MULTI_TOWER_ROOM_LIST_ISCLEAR = 0x648,
            MULTI_UI_ED = 0x6a2,
            QUEST_BONUSOBJECTIVE_ST = 0x6a3,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), EnumParameterDesc("プレイ中クエストのボーナス条件の達成状態にあわせてImageArrayを切り替えます(未達成/達成/全達成)。インデックスでボーナス条件の番号を指定してください。", typeof(QuestBonusObjectiveState))]
            QUEST_BONUSOBJECTIVE_STAR = 0x6a4,
            [ParameterDesc("クエストミッションの達成個数を表示します。全てのミッションを達成していた場合、テキストの色が変わります。"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_BONUSOBJECTIVE_AMOUNT = 0x6a5,
            [ParameterDesc("クエストミッションの最大個数を表示します。"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_BONUSOBJECTIVE_AMOUNTMAX = 0x6a6,
            [ParameterDesc("クエストミッションの報酬の数を表示します。"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), UsesIndex]
            QUEST_BONUSOBJECTIVE_REWARD_NUM = 0x6a7,
            [UsesIndex, InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストミッションの進捗を表示するタイプのミッションならActive、表示しないタイプなら非Active")]
            QUEST_BONUSOBJECTIVE_PROGRESS_BADGE = 0x6a8,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), UsesIndex, ParameterDesc("クエストミッションの進捗（現在）")]
            QUEST_BONUSOBJECTIVE_PROGRESS_CURRENT = 0x6a9,
            [ParameterDesc("クエストミッションの進捗（目標値）"), UsesIndex, InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_BONUSOBJECTIVE_PROGRESS_TARGET = 0x6aa,
            [UsesIndex, InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("塔クエストの固定ミッションの星")]
            QUEST_TOWER_BONUSOBJECTIVE_FIXED_STATE = 0x6ab,
            [ParameterDesc("塔クエストミッションの達成個数を表示します。全てのミッションを達成していた場合、テキストの色が変わります。"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_TOWER_BONUSOBJECTIVE_AMOUNT = 0x6ac,
            [ParameterDesc("塔クエストミッションの最大個数を表示します。"), InstanceTypes(typeof(GameParameter.QuestInstanceTypes))]
            QUEST_TOWER_BONUSOBJECTIVE_AMOUNTMAX = 0x6ad,
            [InstanceTypes(typeof(GameParameter.QuestInstanceTypes)), ParameterDesc("クエストミッションの進捗（現在）\n▼色変更あり\n　達成可能：緑\n　達成不可：赤\n　達成済み：白"), UsesIndex]
            QUEST_TOWER_BONUSOBJECTIVE_PROGRESS_COLOR = 0x6ae,
            QUEST_BONUSOBJECTIVE_ED = 0x706,
            ME_UI_ST = 0x707,
            [ParameterDesc("スキルのマップ(地形)効果説明")]
            ME_UI_SKILL_DESC = 0x708,
            [ParameterDesc("マップ(地形)効果名")]
            ME_UI_NAME = 0x709,
            [ParameterDesc("ジョブ特効説明")]
            JOB_DESC_CH = 0x70a,
            [ParameterDesc("ジョブ特効説明が設定されていれば表示(0)/非表示(1)")]
            IS_JOB_DESC_CH = 0x70b,
            [ParameterDesc("ジョブその他の効果説明")]
            JOB_DESC_OT = 0x70c,
            [ParameterDesc("ジョブその他の効果説明が設定されていれば表示(0)/非表示(1)")]
            IS_JOB_DESC_OT = 0x70d,
            [ParameterDesc("ユニットの現ジョブのMediumアイコン。UNIT_JOBICON2に参照不具合があるため新設"), InstanceTypes(typeof(GameParameter.UnitInstanceTypes))]
            UNIT_JOBICON2_BUGFIX = 0x70e,
            ME_UI_ED = 0x76a,
            FIRST_FRIEND_ST = 0x76b,
            [ParameterDesc("初フレンド成立人数上限")]
            FIRST_FRIEND_MAX = 0x76c,
            [ParameterDesc("初フレンド成立人数")]
            FIRST_FRIEND_COUNT = 0x76d,
            FIRST_FRIEND_ED = 0x7ce,
            CHALLENGEMISSION_ST = 0x7cf,
            [ParameterDesc("カテゴリ名からヘルプ画像を表示")]
            CHALLENGEMISSION_IMG_HELP = 0x7d0,
            [ParameterDesc("カテゴリ名からタブ画像を表示")]
            CHALLENGEMISSION_IMG_BUTTON = 0x7d1,
            [ParameterDesc("トロフィー名から報酬画像を表示")]
            CHALLENGEMISSION_IMG_REWARD = 0x7d2,
            CHALLENGEMISSION_ED = 0x832,
            UNITPARAM_EXTRA_ST = 0x833,
            [ParameterDesc("ユニットパラメーターの錬成可能終了日時(Y/m/d H:i)")]
            UNITPARAM_EXTRA_UNLOCKLIMIT = 0x834,
            [ParameterDesc("訓練対戦のの総戦闘力"), InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType))]
            VERSUS_COM_TOTALSTATUS = 0x835,
            [InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType)), ParameterDesc("運営指定戦のリーダーユニット画像")]
            VERSUS_DRAFT_IMAGE = 0x836,
            [InstanceTypes(typeof(GameParameter.VersusDraftSlot)), ParameterDesc("運営指定戦のパーティユニットアイコン")]
            VERSUS_DRAFT_PARTY_IMAGE_ICON = 0x837,
            [InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType)), ParameterDesc("運営指定戦で先攻/後攻でGameObjectのOn/Off切り替え")]
            VERSUS_DRAFT_TURN = 0x838,
            [ParameterDesc("運営指定戦のパーティユニット画像"), InstanceTypes(typeof(GameParameter.VersusDraftSlot))]
            VERSUS_DRAFT_PARTY_IMAGE = 0x839,
            [ParameterDesc("運営指定戦のパーティ総攻撃力"), InstanceTypes(typeof(GameParameter.VersusPlayerInstanceType))]
            VERSUS_DRAFT_PARTY_TOTALATK = 0x83a,
            UNITPARAM_EXTRA_ED = 0x896,
            UNIT_TOBIRA_ST = 0x897,
            [ParameterDesc("真理開眼の開放に必要なゼニー")]
            UNIT_TOBIRA_UNLOCK_COST = 0x898,
            [ParameterDesc("真理開眼の開放に必要な素材の数")]
            UNIT_TOBIRA_UNLOCK_REQAMOUNT = 0x899,
            [ParameterDesc("真理開眼の開放に必要な素材の所持数")]
            UNIT_TOBIRA_UNLOCK_AMOUNT = 0x89a,
            [ParameterDesc("真理開眼の扉の強化に必要な素材の数")]
            UNIT_TOBIRA_ENHANCE_REQAMOUNT = 0x89b,
            [ParameterDesc("真理開眼の扉の強化に必要な素材の所持数")]
            UNIT_TOBIRA_ENHANCE_AMOUNT = 0x89c,
            [UsesIndex, ParameterDesc("真理開眼の扉のレベルを示すアイコン表示")]
            UNIT_TOBIRA_LEVEL_ICON = 0x89d,
            [ParameterDesc("真理開眼の扉のレベルを示すアイコン表示"), UsesIndex]
            UNIT_TOBIRA_LEVEL_ICON2 = 0x89e,
            UNIT_TOBIRA_ED = 0x8fa,
            ITEM_ST = 0x8fb,
            [ParameterDesc("アイテムのレアリティ")]
            ITEM_RARITY = 0x8fc,
            [ParameterDesc("アイテムのフレーバーテキスト(フレーバーが無ければ説明)")]
            ITEM_FLAVORORDESC = 0x8fd,
            ITEM_ED = 0x95e,
            GLOBAL_PLAYER_RANKMATCH_ST = 0x95f,
            [ParameterDesc("プレイヤーのランクマッチのランキング順位")]
            GLOBAL_PLAYER_RANKMATCH_RANK = 0x960,
            [ParameterDesc("プレイヤーのランクマッチのポイント")]
            GLOBAL_PLAYER_RANKMATCH_SCORE_CURRENT = 0x961,
            [ParameterDesc("プレイヤーの次のクラスまでに必要なポイント")]
            GLOBAL_PLAYER_RANKMATCH_SCORE_NEXT = 0x962,
            [ParameterDesc("プレイヤーのランクマッチ挑戦可能回数")]
            GLOBAL_PLAYER_RANKMATCH_BP = 0x963,
            [ParameterDesc("ランクマッチのクラスアイコン")]
            RANKMATCH_CLASS_ICON = 0x964,
            [ParameterDesc("ランクマッチのクラス名")]
            RANKMATCH_CLASS_NAME = 0x965,
            [ParameterDesc("ランクマッチのクラスの枠")]
            RANKMATCH_CLASS_FRAME = 0x966,
            [ParameterDesc("プレイヤーのランクマッチのクラスと同じなら表示")]
            RANKMATCH_PLAYER_ISCLASS = 0x967,
            [ParameterDesc("プレイヤーのランクマッチの順位(3位以上なら非表示になる)")]
            GLOBAL_PLAYER_RANKMATCH_RANK_IMAGESET = 0x968,
            [ParameterDesc("プレイヤーのランクマッチの順位アイコン(4位以下なら非表示になる)")]
            GLOBAL_PLAYER_RANKMATCH_RANK_ICON = 0x969,
            [ParameterDesc("プレイヤーのランクマッチパーティの総攻撃力")]
            PARTY_RANKMATCHTOTALATK = 0x96a,
            [ParameterDesc("ランクマッチのランキングの順位(3位以上なら非表示になる)")]
            RANKMATCH_RANKING_RANK_IMAGESET = 0x96b,
            [ParameterDesc("ランクマッチのランキングの順位アイコン(4位以下なら非表示になる)")]
            RANKMATCH_RANKING_RANK_ICON = 0x96c,
            [ParameterDesc("ランクマッチのランキングのクラスアイコン")]
            RANKMATCH_RANKING_CLASS = 0x96d,
            [ParameterDesc("ランクマッチのランキングのユーザー名")]
            RANKMATCH_RANKING_NAME = 0x96e,
            [ParameterDesc("ランクマッチのランキングのユーザーランク")]
            RANKMATCH_RANKING_LEVEL = 0x96f,
            [ParameterDesc("ランクマッチのランキングのスコア")]
            RANKMATCH_RANKING_SCORE = 0x970,
            [InstanceTypes(typeof(GameParameter.RankMatchPlayer)), ParameterDesc("プレイヤーのランクマッチのクラス")]
            GLOBAL_PLAYER_RANKMATCH_CLASS = 0x971,
            [ParameterDesc("ランクマッチのランキングの順位(3位以上なら非表示になる)")]
            RANKMATCH_RANKING_RANKREWARD_IMAGESET_BEGIN = 0x972,
            RANKMATCH_RANKING_RANKREWARD_IMAGESET_END = 0x973,
            [ParameterDesc("ランクマッチのランキングの順位アイコン(4位以下なら非表示になる)")]
            RANKMATCH_RANKING_RANKREWARD_ICON = 0x974,
            [ParameterDesc("ランクマッチの戦績の勝敗")]
            RANKMATCH_HISTORY_RESULT = 0x975,
            [ParameterDesc("ランクマッチの戦績の変動ポイント")]
            RANKMATCH_HISTORY_VALUE = 0x976,
            [ParameterDesc("ランクマッチの戦績のクラスアイコン")]
            RANKMATCH_HISTORY_CLASS = 0x977,
            [ParameterDesc("ランクマッチの戦績のユーザー名")]
            RANKMATCH_HISTORY_NAME = 0x978,
            [ParameterDesc("ランクマッチの戦績のユーザーランク")]
            RANKMATCH_HISTORY_LEVEL = 0x979,
            [ParameterDesc("ランクマッチの戦績のスコア")]
            RANKMATCH_HISTORY_SCORE = 0x97a,
            [ParameterDesc("ランクマッチのミッション名")]
            RANKMATCH_MISSION_NAME = 0x97b,
            [ParameterDesc("ランクマッチのミッションの必要回数")]
            RANKMATCH_MISSION_NEED = 0x97c,
            [ParameterDesc("ランクマッチのミッションの進行度")]
            RANKMATCH_MISSION_PROGRESS = 0x97d,
            [ParameterDesc("プレイヤーのランクマッチのクラス"), InstanceTypes(typeof(GameParameter.RankMatchPlayer))]
            GLOBAL_PLAYER_RANKMATCH_CLASSNAME = 0x97e,
            [ParameterDesc("プレイヤーのランクマッチの対戦回数")]
            GLOBAL_PLAYER_RANKMATCH_COUNT_TOTAL = 0x97f,
            [ParameterDesc("プレイヤーのランクマッチの対戦勝利数")]
            GLOBAL_PLAYER_RANKMATCH_COUNT_WIN = 0x980,
            [ParameterDesc("プレイヤーのランクマッチの対戦敗北数")]
            GLOBAL_PLAYER_RANKMATCH_COUNT_LOSE = 0x981,
            [ParameterDesc("プレイヤーのランクマッチのスコアゲージ")]
            GLOBAL_PLAYER_RANKMATCH_SCORE_GAUGE = 0x982,
            RANKMATCH_RANKING_RANKREWARD_IMAGESET_SPERATE = 0x983,
            [ParameterDesc("ランクマッチのミッションの報酬受取可能ならActiveに"), AlwaysUpdate]
            RANKMATCH_MISSION_COMPLETED_ACTIVE = 0x984,
            [ParameterDesc("ランクマッチのシーズン報酬ランキングの順位(3位以上なら非表示になる)")]
            RANKMATCH_RESULT_RANK_NUM = 0x985,
            [ParameterDesc("ランクマッチのシーズン報酬ランキングの順位アイコン(4位以下なら非表示になる)")]
            RANKMATCH_RESULT_RANK_ICON = 0x986,
            [ParameterDesc("ランクマッチのシーズン報酬ランキングのクラスアイコン")]
            RANKMATCH_RESULT_CLASS = 0x987,
            [ParameterDesc("ランクマッチのシーズン報酬ランキングのスコア")]
            RANKMATCH_RESULT_SCORE = 0x988,
            [ParameterDesc("対戦相手のランクマッチスコア")]
            VERSUS_PLAYER_RANKMATCHSCORE = 0x989,
            [ParameterDesc("ランクマッチのシーズン報酬ランキングのクラス名")]
            RANKMATCH_RESULT_CLASS_NAME = 0x98a,
            [ParameterDesc("ランクマッチのシーズン期間")]
            RANKMATCH_RESULT_PERIOD = 0x98b,
            [ParameterDesc("ランクマッチのタイトル")]
            RANKMATCH_TITLE = 0x98c,
            GLOBAL_PLAYER_RANKMATCH_ED = 0x9c2,
            ABILITY_ST = 0x9c3,
            [ParameterDesc("派生アビリティならActiveになる"), AlwaysUpdate]
            ABILITY_DERIVED_BADGE = 0x9c4,
            [ParameterDesc("派生アビリティならテキストの色が変わる")]
            ABILITY_DERIVED_TEXTCOLOR = 0x9c5,
            [ParameterDesc("アビリティの種類詳細によってタイトルが変わる（SpriteSheet）")]
            ABILITY_TITLE_DETAIL_TYPE = 0x9c6,
            ABILITY_ED = 0xa26,
            SKILL_ST = 0xa27,
            [ParameterDesc("派生スキルならテキストの色が変わる")]
            SKILL_DERIVED_TEXTCOLOR = 0xa28,
            SKILL_ED = 0xa8a,
            ARTIFACT_SETEFFECT_ST = 0xa8b,
            [ParameterDesc("セット効果の発動条件のアイコン"), UsesIndex]
            ARTIFACT_SETEFFECT_TRIGGER_ICON = 0xa8c,
            ARTIFACT_SETEFFECT_ED = 0xaee,
            CONCEPTCARD_ST = 0xaef,
            [ParameterDesc("真理念装の限界突破回数を示すアイコン")]
            CONCEPTCARD_AWAKE = 0xaf0,
            CONCEPTCARD_ED = 0xb52,
            GAMEOBJECT_ST = 0xb53,
            [ParameterDesc("ゲームオブジェクトの表示/非表示")]
            GAMEOBJECT_ACTIVE = 0xb54,
            GAMEOBJECT_INACTIVE = 0xb55,
            GAMEOBJECT_ED = 0xbb6
        }

        public enum PartyAttackTypes
        {
            Normal,
            Quest
        }

        public enum QuestInstanceTypes
        {
            Any,
            Playing,
            Selected
        }

        public enum RankMatchPlayer
        {
            Current,
            Before
        }

        public enum TargetInstanceTypes
        {
            Main,
            Sub
        }

        public enum TrophyBadgeInstanceTypes
        {
            Any,
            Normal,
            Daily,
            NormalStory,
            NormalEvent,
            NormalTraining,
            NormalOther
        }

        public enum TrophyConditionInstances
        {
            List,
            Index
        }

        public enum UnitInstanceTypes
        {
            Any,
            OBSOLETE_MainTarget,
            OBSOLETE_SubTarget,
            CurrentTurn,
            ArenaPlayerUnit0,
            ArenaPlayerUnit1,
            ArenaPlayerUnit2,
            EnemyArenaPlayerUnit0,
            EnemyArenaPlayerUnit1,
            EnemyArenaPlayerUnit2,
            PartyUnit0,
            PartyUnit1,
            PartyUnit2,
            VersusUnit,
            MultiUnit,
            MultiTowerUnit,
            VersusCpuUnit0,
            VersusCpuUnit1,
            VersusCpuUnit2,
            RankMatchUnit,
            FriendPartyUnit,
            VersusDraftPlayerUnit0,
            VersusDraftPlayerUnit1,
            VersusDraftPlayerUnit2,
            VersusDraftPlayerUnit3,
            VersusDraftPlayerUnit4,
            VersusDraftPlayerUnit5,
            VersusDraftEnemyUnit0,
            VersusDraftEnemyUnit1,
            VersusDraftEnemyUnit2,
            VersusDraftEnemyUnit3,
            VersusDraftEnemyUnit4,
            VersusDraftEnemyUnit5
        }

        public class UsesIndex : Attribute
        {
            public UsesIndex()
            {
                base..ctor();
                return;
            }
        }

        public enum VersusDraftSlot
        {
            PlayerLeader,
            PlayerSlot2,
            PlayerSlot3,
            PlayerSlot4,
            PlayerSlot5,
            EnemyLeader,
            EnemySlot2,
            EnemySlot3,
            EnemySlot4,
            EnemySlot5
        }

        public enum VersusPlayerInstanceType
        {
            Player,
            Enemy
        }
    }
}

