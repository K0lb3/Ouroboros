namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "UpdatePlayerInfo", 0, 0)]
    public class ConfigWindow : MonoBehaviour, IFlowInterface
    {
        public Slider SoundVolume;
        public Slider MusicVolume;
        public Slider VoiceVolume;
        public Toggle UseAssetBundle;
        public Toggle UseDevServer;
        public Toggle UseAwsServer;
        public Toggle NewGame;
        public Toggle[] InputMethods;
        public Toggle UseAutoPlay;
        public Toggle UsePushStamina;
        public Toggle UsePushNews;
        public Toggle MultiInvitationFlag;
        public InputField MultiInvitationComment;
        public GameObject LoginBonus;
        public GameObject LoginBonus28days;
        public InputField AssetVersion;
        public Toggle ToggleChatState;
        public Toggle ToggleMultiState;
        public Toggle MultiUserSetting;
        public InputField MultiUserName;
        public Toggle UseLocalMasterData;
        public Button MasterCheckButton;
        public Button VoiceCopyButton;
        public InputField ClientExPath;
        public GameObject AwardState;
        public GameObject SupportIcon;
        public GameObject Prefab_NewItemBadge;
        public GameObject TreasureList;
        public GameObject TreasureListNode;
        private List<GameObject> mTreasureListNodes;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache1E;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache1F;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache20;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache21;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache22;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache23;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache24;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache25;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache26;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache27;
        [CompilerGenerated]
        private static UnityAction<string> <>f__am$cache28;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache29;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache2A;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache2B;
        [CompilerGenerated]
        private static UnityAction<bool> <>f__am$cache2C;

        public ConfigWindow()
        {
            this.InputMethods = new Toggle[0];
            this.mTreasureListNodes = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E0(float value)
        {
            GameUtility.Config_SoundVolume = value;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E1(float value)
        {
            GameUtility.Config_MusicVolume = value;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E2(float value)
        {
            GameUtility.Config_VoiceVolume = value;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E3(bool yes)
        {
            GameUtility.Config_UseAssetBundles.Value = yes;
            if (yes != null)
            {
                goto Label_001B;
            }
            AssetDownloader.ClearCache();
            AssetManager.UnloadAll();
        Label_001B:
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E4(bool yes)
        {
            GameUtility.Config_UseDevServer.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E5(bool yes)
        {
            GameUtility.Config_UseAwsServer.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E6(bool yes)
        {
            GameUtility.Config_UseAutoPlay.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E7(bool yes)
        {
            GameUtility.Config_UsePushStamina.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E8(bool yes)
        {
            GameUtility.Config_UsePushNews.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2E9(bool yes)
        {
            GlobalVars.MultiInvitaionFlag = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2EA(string text)
        {
            GlobalVars.MultiInvitaionComment = text;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2EB(bool yes)
        {
            GameUtility.Config_ChatState.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2EC(bool yes)
        {
            GameUtility.Config_MultiState.Value = yes;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2ED(bool yes)
        {
            GameUtility.Config_NewGame.Value = yes;
            return;
        }

        [CompilerGenerated]
        private void <Start>m__2EE(bool flag)
        {
            if ((this.MultiUserName != null) == null)
            {
                goto Label_0020;
            }
            this.MultiUserName.set_readOnly(flag == 0);
        Label_0020:
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__2EF(bool yes)
        {
            GameUtility.Config_UseLocalData.Value = yes;
            return;
        }

        [CompilerGenerated]
        private void <Start>m__2F0()
        {
            string str;
            string str2;
            string[] strArray;
            int num;
            string str3;
            str = this.ClientExPath.get_text() + "/Assets/sound";
            str2 = "Assets/StreamingAssets/";
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_007D;
            }
            if (Directory.Exists(str) == null)
            {
                goto Label_006D;
            }
            strArray = Directory.GetFiles(str);
            num = 0;
            goto Label_005F;
        Label_0040:
            str3 = str2 + Path.GetFileName(strArray[num]);
            File.Copy(strArray[num], str3, 1);
            num += 1;
        Label_005F:
            if (num < ((int) strArray.Length))
            {
                goto Label_0040;
            }
            goto Label_007D;
        Label_006D:
            Debug.Log("not exist directory" + str);
        Label_007D:
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_000E;
            }
            this.UpdatePlayerInfo();
        Label_000E:
            return;
        }

        public static GameObject CreateItemObject(GameObject node, GameObject newIcon, QuestResult.DropItemData item)
        {
            GameObject obj2;
            DropItemIcon icon;
            GameObject obj3;
            RectTransform transform;
            obj2 = Object.Instantiate<GameObject>(node);
            if ((obj2 != null) == null)
            {
                goto Label_00A6;
            }
            DataSource.Bind<QuestResult.DropItemData>(obj2, item);
            if (item.mIsSecret == null)
            {
                goto Label_003F;
            }
            icon = obj2.GetComponent<DropItemIcon>();
            if ((icon != null) == null)
            {
                goto Label_003F;
            }
            icon.IsSecret = 1;
        Label_003F:
            obj2.SetActive(1);
            GameParameter.UpdateAll(obj2);
            if ((newIcon != null) == null)
            {
                goto Label_00A6;
            }
            if (item.IsNew == null)
            {
                goto Label_00A6;
            }
            obj3 = Object.Instantiate<GameObject>(newIcon);
            if ((obj3 != null) == null)
            {
                goto Label_00A6;
            }
            transform = obj3.get_transform() as RectTransform;
            transform.get_gameObject().SetActive(1);
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.SetParent(obj2.get_transform(), 0);
        Label_00A6:
            return obj2;
        }

        private void OnInputMethodChange(bool y)
        {
            int num;
            if (y == null)
            {
                goto Label_004F;
            }
            num = 0;
            goto Label_0041;
        Label_000D:
            if ((this.InputMethods[num] != null) == null)
            {
                goto Label_003D;
            }
            if (this.InputMethods[num].get_isOn() == null)
            {
                goto Label_003D;
            }
            GameUtility.Config_InputMethod = num;
            goto Label_004F;
        Label_003D:
            num += 1;
        Label_0041:
            if (num < ((int) this.InputMethods.Length))
            {
                goto Label_000D;
            }
        Label_004F:
            return;
        }

        public static void SetupTreasureList(GameObject list, GameObject node, GameObject newIcon, GameObject owner, List<GameObject> itemNodes)
        {
            PlayerData data;
            SceneBattle battle;
            BattleCore core;
            BattleCore.Record record;
            Transform transform;
            List<QuestResult.DropItemData> list2;
            int num;
            bool flag;
            int num2;
            QuestResult.DropItemData data2;
            ItemData data3;
            List<UnitData> list3;
            int num3;
            GameObject obj2;
            <SetupTreasureList>c__AnonStorey32A storeya;
            if ((node != null) == null)
            {
                goto Label_0013;
            }
            node.SetActive(0);
        Label_0013:
            if (((newIcon != null) == null) || (newIcon.get_gameObject().get_activeInHierarchy() == null))
            {
                goto Label_0036;
            }
            newIcon.SetActive(0);
        Label_0036:
            data = MonoSingleton<GameManager>.Instance.Player;
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_038E;
            }
            core = battle.Battle;
            record = new BattleCore.Record();
            core.GainUnitSteal(record);
            core.GainUnitDrop(record, 1);
            DataSource.Bind<BattleCore.Record>(owner, record);
            if (record == null)
            {
                goto Label_038E;
            }
            transform = ((list != null) == null) ? node.get_transform().get_parent() : list.get_transform();
            list2 = new List<QuestResult.DropItemData>();
            num = 0;
            goto Label_0329;
        Label_00AF:
            flag = 0;
            num2 = 0;
            goto Label_018D;
        Label_00BA:
            if (list2[num2].mIsSecret == record.items[num].mIsSecret)
            {
                goto Label_00E4;
            }
            goto Label_0187;
        Label_00E4:
            if (list2[num2].IsItem == null)
            {
                goto Label_0138;
            }
            if (list2[num2].itemParam != record.items[num].itemParam)
            {
                goto Label_0187;
            }
            list2[num2].Gain(1);
            flag = 1;
            goto Label_019B;
            goto Label_0187;
        Label_0138:
            if ((list2[num2].IsConceptCard == null) || (list2[num2].conceptCardParam != record.items[num].conceptCardParam))
            {
                goto Label_0187;
            }
            list2[num2].Gain(1);
            flag = 1;
            goto Label_019B;
        Label_0187:
            num2 += 1;
        Label_018D:
            if (num2 < list2.Count)
            {
                goto Label_00BA;
            }
        Label_019B:
            if (flag == null)
            {
                goto Label_01A7;
            }
            goto Label_0323;
        Label_01A7:
            data2 = new QuestResult.DropItemData();
            if (record.items[num].IsItem == null)
            {
                goto Label_02B4;
            }
            data2.SetupDropItemData(2, 0L, record.items[num].itemParam.iname, 1);
            if (record.items[num].itemParam.type == 0x10)
            {
                goto Label_0261;
            }
            data3 = data.FindItemDataByItemParam(record.items[num].itemParam);
            data2.IsNew = (data.ItemEntryExists(record.items[num].itemParam.iname) == null) ? 1 : ((data3 == null) ? 1 : data3.IsNew);
            goto Label_02AF;
        Label_0261:
            storeya = new <SetupTreasureList>c__AnonStorey32A();
            storeya.iid = record.items[num].itemParam.iname;
            if (data.Units.Find(new Predicate<UnitData>(storeya.<>m__2F1)) != null)
            {
                goto Label_02ED;
            }
            data2.IsNew = 1;
        Label_02AF:
            goto Label_02ED;
        Label_02B4:
            if (record.items[num].IsConceptCard == null)
            {
                goto Label_02ED;
            }
            data2.SetupDropItemData(3, 0L, record.items[num].conceptCardParam.iname, 1);
        Label_02ED:
            data2.mIsSecret = record.items[num].mIsSecret;
            if (data2.mIsSecret == null)
            {
                goto Label_031A;
            }
            data2.IsNew = 0;
        Label_031A:
            list2.Add(data2);
        Label_0323:
            num += 1;
        Label_0329:
            if (num < record.items.Count)
            {
                goto Label_00AF;
            }
            num3 = 0;
            goto Label_0380;
        Label_0343:
            obj2 = CreateItemObject(node, newIcon, list2[num3]);
            if ((obj2 != null) == null)
            {
                goto Label_037A;
            }
            obj2.get_transform().SetParent(transform, 0);
            itemNodes.Add(obj2);
        Label_037A:
            num3 += 1;
        Label_0380:
            if (num3 < list2.Count)
            {
                goto Label_0343;
            }
        Label_038E:
            return;
        }

        private void Start()
        {
            bool flag;
            string str;
            int num;
            MoveInputMethods methods;
            PlayerData data;
            UnitData data2;
            if ((this.SoundVolume != null) == null)
            {
                goto Label_004E;
            }
            this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
            if (<>f__am$cache1E != null)
            {
                goto Label_0044;
            }
            <>f__am$cache1E = new UnityAction<float>(null, <Start>m__2E0);
        Label_0044:
            this.SoundVolume.get_onValueChanged().AddListener(<>f__am$cache1E);
        Label_004E:
            if ((this.MusicVolume != null) == null)
            {
                goto Label_009C;
            }
            this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
            if (<>f__am$cache1F != null)
            {
                goto Label_0092;
            }
            <>f__am$cache1F = new UnityAction<float>(null, <Start>m__2E1);
        Label_0092:
            this.MusicVolume.get_onValueChanged().AddListener(<>f__am$cache1F);
        Label_009C:
            if ((this.VoiceVolume != null) == null)
            {
                goto Label_00EA;
            }
            this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
            if (<>f__am$cache20 != null)
            {
                goto Label_00E0;
            }
            <>f__am$cache20 = new UnityAction<float>(null, <Start>m__2E2);
        Label_00E0:
            this.VoiceVolume.get_onValueChanged().AddListener(<>f__am$cache20);
        Label_00EA:
            if ((this.UseAssetBundle != null) == null)
            {
                goto Label_013D;
            }
            this.UseAssetBundle.set_isOn(GameUtility.Config_UseAssetBundles.Value);
            if (<>f__am$cache21 != null)
            {
                goto Label_0133;
            }
            <>f__am$cache21 = new UnityAction<bool>(null, <Start>m__2E3);
        Label_0133:
            this.UseAssetBundle.onValueChanged.AddListener(<>f__am$cache21);
        Label_013D:
            if ((this.UseDevServer != null) == null)
            {
                goto Label_0190;
            }
            this.UseDevServer.set_isOn(GameUtility.Config_UseDevServer.Value);
            if (<>f__am$cache22 != null)
            {
                goto Label_0186;
            }
            <>f__am$cache22 = new UnityAction<bool>(null, <Start>m__2E4);
        Label_0186:
            this.UseDevServer.onValueChanged.AddListener(<>f__am$cache22);
        Label_0190:
            if ((this.UseAwsServer != null) == null)
            {
                goto Label_01E3;
            }
            this.UseAwsServer.set_isOn(GameUtility.Config_UseAwsServer.Value);
            if (<>f__am$cache23 != null)
            {
                goto Label_01D9;
            }
            <>f__am$cache23 = new UnityAction<bool>(null, <Start>m__2E5);
        Label_01D9:
            this.UseAwsServer.onValueChanged.AddListener(<>f__am$cache23);
        Label_01E3:
            if ((this.UseAutoPlay != null) == null)
            {
                goto Label_0236;
            }
            this.UseAutoPlay.set_isOn(GameUtility.Config_UseAutoPlay.Value);
            if (<>f__am$cache24 != null)
            {
                goto Label_022C;
            }
            <>f__am$cache24 = new UnityAction<bool>(null, <Start>m__2E6);
        Label_022C:
            this.UseAutoPlay.onValueChanged.AddListener(<>f__am$cache24);
        Label_0236:
            if ((this.UsePushStamina != null) == null)
            {
                goto Label_0289;
            }
            this.UsePushStamina.set_isOn(GameUtility.Config_UsePushStamina.Value);
            if (<>f__am$cache25 != null)
            {
                goto Label_027F;
            }
            <>f__am$cache25 = new UnityAction<bool>(null, <Start>m__2E7);
        Label_027F:
            this.UsePushStamina.onValueChanged.AddListener(<>f__am$cache25);
        Label_0289:
            if ((this.UsePushNews != null) == null)
            {
                goto Label_02DC;
            }
            this.UsePushNews.set_isOn(GameUtility.Config_UsePushNews.Value);
            if (<>f__am$cache26 != null)
            {
                goto Label_02D2;
            }
            <>f__am$cache26 = new UnityAction<bool>(null, <Start>m__2E8);
        Label_02D2:
            this.UsePushNews.onValueChanged.AddListener(<>f__am$cache26);
        Label_02DC:
            if ((this.MultiInvitationFlag != null) == null)
            {
                goto Label_033C;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
            GlobalVars.MultiInvitaionFlag = flag;
            this.MultiInvitationFlag.set_isOn(flag);
            if (<>f__am$cache27 != null)
            {
                goto Label_0332;
            }
            <>f__am$cache27 = new UnityAction<bool>(null, <Start>m__2E9);
        Label_0332:
            this.MultiInvitationFlag.onValueChanged.AddListener(<>f__am$cache27);
        Label_033C:
            if ((this.MultiInvitationComment != null) == null)
            {
                goto Label_03A7;
            }
            str = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionComment;
            GlobalVars.MultiInvitaionComment = str;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_037A;
            }
            SRPG_Extensions.SetText(this.MultiInvitationComment, str);
        Label_037A:
            if (<>f__am$cache28 != null)
            {
                goto Label_039D;
            }
            <>f__am$cache28 = new UnityAction<string>(null, <Start>m__2EA);
        Label_039D:
            this.MultiInvitationComment.get_onValueChanged().AddListener(<>f__am$cache28);
        Label_03A7:
            if ((this.ToggleChatState != null) == null)
            {
                goto Label_03FA;
            }
            this.ToggleChatState.set_isOn(GameUtility.Config_ChatState.Value);
            if (<>f__am$cache29 != null)
            {
                goto Label_03F0;
            }
            <>f__am$cache29 = new UnityAction<bool>(null, <Start>m__2EB);
        Label_03F0:
            this.ToggleChatState.onValueChanged.AddListener(<>f__am$cache29);
        Label_03FA:
            if ((this.ToggleMultiState != null) == null)
            {
                goto Label_044D;
            }
            this.ToggleMultiState.set_isOn(GameUtility.Config_MultiState.Value);
            if (<>f__am$cache2A != null)
            {
                goto Label_0443;
            }
            <>f__am$cache2A = new UnityAction<bool>(null, <Start>m__2EC);
        Label_0443:
            this.ToggleMultiState.onValueChanged.AddListener(<>f__am$cache2A);
        Label_044D:
            if ((this.NewGame != null) == null)
            {
                goto Label_04A0;
            }
            this.NewGame.set_isOn(GameUtility.Config_NewGame.Value);
            if (<>f__am$cache2B != null)
            {
                goto Label_0496;
            }
            <>f__am$cache2B = new UnityAction<bool>(null, <Start>m__2ED);
        Label_0496:
            this.NewGame.onValueChanged.AddListener(<>f__am$cache2B);
        Label_04A0:
            if ((this.MultiUserSetting != null) == null)
            {
                goto Label_04EF;
            }
            this.MultiUserSetting.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__2EE));
            this.MultiUserSetting.get_gameObject().SetActive(0);
            this.MultiUserName.get_gameObject().SetActive(0);
        Label_04EF:
            if ((this.UseLocalMasterData != null) == null)
            {
                goto Label_0553;
            }
            this.UseLocalMasterData.set_isOn(GameUtility.Config_UseLocalData.Value);
            if (<>f__am$cache2C != null)
            {
                goto Label_0538;
            }
            <>f__am$cache2C = new UnityAction<bool>(null, <Start>m__2EF);
        Label_0538:
            this.UseLocalMasterData.onValueChanged.AddListener(<>f__am$cache2C);
            this.UseLocalMasterData.get_gameObject().SetActive(0);
        Label_0553:
            if ((this.VoiceCopyButton != null) == null)
            {
                goto Label_05A0;
            }
            this.VoiceCopyButton.get_onClick().AddListener(new UnityAction(this, this.<Start>m__2F0));
            this.VoiceCopyButton.get_gameObject().get_transform().get_parent().get_gameObject().SetActive(0);
        Label_05A0:
            num = 0;
            goto Label_05DC;
        Label_05A7:
            if ((this.InputMethods[num] != null) == null)
            {
                goto Label_05D8;
            }
            this.InputMethods[num].onValueChanged.AddListener(new UnityAction<bool>(this, this.OnInputMethodChange));
        Label_05D8:
            num += 1;
        Label_05DC:
            if (num < ((int) this.InputMethods.Length))
            {
                goto Label_05A7;
            }
            methods = GameUtility.Config_InputMethod;
            if (methods >= ((int) this.InputMethods.Length))
            {
                goto Label_061F;
            }
            if ((this.InputMethods[methods] != null) == null)
            {
                goto Label_061F;
            }
            this.InputMethods[methods].set_isOn(1);
        Label_061F:
            if ((this.LoginBonus != null) == null)
            {
                goto Label_0650;
            }
            this.LoginBonus.SetActive((MonoSingleton<GameManager>.Instance.Player.LoginBonus == null) == 0);
        Label_0650:
            if ((this.LoginBonus28days != null) == null)
            {
                goto Label_0681;
            }
            this.LoginBonus28days.SetActive((MonoSingleton<GameManager>.Instance.Player.LoginBonus28days == null) == 0);
        Label_0681:
            if ((this.MasterCheckButton != null) == null)
            {
                goto Label_06A3;
            }
            this.MasterCheckButton.get_gameObject().SetActive(0);
        Label_06A3:
            if ((this.AwardState != null) == null)
            {
                goto Label_06D4;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data == null)
            {
                goto Label_06D4;
            }
            DataSource.Bind<PlayerData>(this.AwardState, data);
        Label_06D4:
            if ((this.SupportIcon != null) == null)
            {
                goto Label_0714;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            if (data2 == null)
            {
                goto Label_0714;
            }
            DataSource.Bind<UnitData>(this.SupportIcon, data2);
        Label_0714:
            SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, base.get_gameObject(), this.mTreasureListNodes);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void UpdatePlayerInfo()
        {
            PlayerData data;
            UnitData data2;
            if ((this.AwardState != null) == null)
            {
                goto Label_002E;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data == null)
            {
                goto Label_002E;
            }
            DataSource.Bind<PlayerData>(this.AwardState, data);
        Label_002E:
            if ((this.SupportIcon != null) == null)
            {
                goto Label_006B;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            if (data2 == null)
            {
                goto Label_006B;
            }
            DataSource.Bind<UnitData>(this.SupportIcon, data2);
        Label_006B:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        [CompilerGenerated]
        private sealed class <SetupTreasureList>c__AnonStorey32A
        {
            internal string iid;

            public <SetupTreasureList>c__AnonStorey32A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2F1(UnitData p)
            {
                return (p.UnitParam.iname == this.iid);
            }
        }
    }
}

