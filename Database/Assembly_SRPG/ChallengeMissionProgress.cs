namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [Pin(10, "パネル表示", 1, 10), Pin(0x3f1, "ユニット強化画面へ移動", 1, 0x3f1), Pin(0x3f0, "アリーナへ移動", 1, 0x3f0), Pin(0x3ef, "イベントクエスト選択へ移動", 1, 0x3ef), Pin(0x3ee, "エリートクエスト選択へ移動", 1, 0x3ee), Pin(0x3ed, "マルチプレイへ移動", 1, 0x3ed), Pin(0x3eb, "装備強化画面へ移動", 1, 0x3ec), Pin(0x3ea, "アビリティ強化画面へ移動", 1, 0x3eb), Pin(0x3ec, "ゴールド購入画面へ移動", 1, 0x3ea), Pin(0x3e9, "クエスト選択へ移動", 1, 0x3e9), Pin(0x3e8, "ガチャへ移動", 1, 0x3e8), Pin(100, "キャンセル", 1, 100), Pin(11, "ヘルプ表示", 1, 11), Pin(2, "Refresh", 0, 2), Pin(1, "Close", 0, 1), Pin(0, "Open", 0, 0), Pin(0x409, "ホームへ移動", 1, 0x409), Pin(0x408, "フレンド画面へ移動", 1, 0x408), Pin(0x407, "お知らせへ移動", 1, 0x407), Pin(0x406, "カルマへ移動", 1, 0x406), Pin(0x405, "真理念装へ移動", 1, 0x405), Pin(0x404, "初心者の館へ移動", 1, 0x404), Pin(0x403, "初心者イベントクエスト選択へ移動", 1, 0x403), Pin(0x402, "対戦へ移動", 1, 0x402), Pin(0x401, "塔へ移動", 1, 0x401), Pin(0x400, "武具進化画面へ移動", 1, 0x400), Pin(0x3ff, "武具強化画面へ移動", 1, 0x3ff), Pin(0x3fe, "武具錬成画面へ移動", 1, 0x3fe), Pin(0x3fd, "武具の店へ移動", 1, 0x3fd), Pin(0x3fc, "魂の交換所へ移動", 1, 0x3fc), Pin(0x3fb, "マルチ交換所へ移動", 1, 0x3fb), Pin(0x3fa, "闘技場交換所へ移動", 1, 0x3fa), Pin(0x3f9, "ツアーの店へ移動", 1, 0x3f9), Pin(0x3f8, "ソウルショップへ移動", 1, 0x3f8), Pin(0x3f7, "ルイザの店へ移動", 1, 0x3f7), Pin(0x3f6, "マリアの店へ移動", 1, 0x3f6), Pin(0x3f5, "アンナの店へ移動", 1, 0x3f5), Pin(0x3f4, "ユニット選択へ移動", 1, 0x3f4), Pin(0x3f3, "ユニットジョブ画面へ移動", 1, 0x3f3), Pin(0x3f2, "ユニットアビリティセット画面へ移動", 1, 0x3f2)]
    public class ChallengeMissionProgress : MonoBehaviour, IFlowInterface
    {
        public const int PIN_OPEN = 0;
        public const int PIN_CLOSE = 1;
        public const int PIN_REFRESH = 2;
        public const int PIN_SHOW_PANEL = 10;
        public const int PIN_SHOW_HELP = 11;
        public const int PIN_CANCEL = 100;
        public const int PIN_GOTO_GACHA = 0x3e8;
        public const int PIN_GOTO_QUEST = 0x3e9;
        public const int PIN_GOTO_ABILITY = 0x3ea;
        public const int PIN_GOTO_SOUBI = 0x3eb;
        public const int PIN_GOTO_BUYGOLD = 0x3ec;
        public const int PIN_GOTO_MULTI = 0x3ed;
        public const int PIN_GOTO_ELITE = 0x3ee;
        public const int PIN_GOTO_EVENT = 0x3ef;
        public const int PIN_GOTO_ARENA = 0x3f0;
        public const int PIN_GOTO_UNIT_STR = 0x3f1;
        public const int PIN_GOTO_ABILITYSET = 0x3f2;
        public const int PIN_GOTO_UNIT_JOB = 0x3f3;
        public const int PIN_GOTO_UNIT = 0x3f4;
        public const int PIN_GOTO_SHOP_NORMAL = 0x3f5;
        public const int PIN_GOTO_SHOP_TABI = 0x3f6;
        public const int PIN_GOTO_SHOP_KIMAGRE = 0x3f7;
        public const int PIN_GOTO_SHOP_MONOZUKI = 0x3f8;
        public const int PIN_GOTO_SHOP_TOUR = 0x3f9;
        public const int PIN_GOTO_SHOP_ARENA = 0x3fa;
        public const int PIN_GOTO_SHOP_MULTI = 0x3fb;
        public const int PIN_GOTO_SHOP_KAKERA = 0x3fc;
        public const int PIN_GOTO_SHOP_ARTIFACT = 0x3fd;
        public const int PIN_GOTO_ATF_TRANS = 0x3fe;
        public const int PIN_GOTO_ATF_STRTH = 0x3ff;
        public const int PIN_GOTO_ATF_EVOLT = 0x400;
        public const int PIN_GOTO_TOWER = 0x401;
        public const int PIN_GOTO_VERSUS = 0x402;
        public const int PIN_GOTO_EVENT_BEGINEER = 0x403;
        public const int PIN_GOTO_BEGINNERTOP = 0x404;
        public const int PIN_GOTO_CONCEPTCARD = 0x405;
        public const int PIN_GOTO_ORDEAL = 0x406;
        public const int PIN_GOTO_NEWS = 0x407;
        public const int PIN_GOTO_FRIEND = 0x408;
        public const int PIN_GOTO_HOME = 0x409;
        public Button ButtonHelp;
        public Button ButtonDetail;
        public Button ButtonTry;
        public Button ButtonReward;
        public RawImage ImageItem;
        public RawImage ImageExp;
        public RawImage ImageGold;
        public RawImage ImageAp;
        public Text TextReward;
        public Image HelpIcon;
        [SerializeField]
        private GameObject ConceptCard;
        private readonly string EVENT_CHALLENGE_ICON_SHOW;
        private readonly string EVENT_CHALLENGE_ICON_HIDE;
        private bool mShowingOverlay;

        public ChallengeMissionProgress()
        {
            this.EVENT_CHALLENGE_ICON_SHOW = "CHALLENGE_ICON_SHOW";
            this.EVENT_CHALLENGE_ICON_HIDE = "CHALLENGE_ICON_HIDE";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0025;

                case 2:
                    goto Label_0046;
            }
            goto Label_0052;
        Label_0019:
            this.Refresh(0);
            goto Label_0057;
        Label_0025:
            if (base.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0057;
            }
            base.get_gameObject().SetActive(0);
            goto Label_0057;
        Label_0046:
            this.Refresh(1);
            goto Label_0057;
        Label_0052:;
        Label_0057:
            return;
        }

        private void Awake()
        {
            if ((this.ButtonTry != null) == null)
            {
                goto Label_002D;
            }
            this.ButtonTry.get_onClick().AddListener(new UnityAction(this, this.OnClickTry));
        Label_002D:
            if ((this.ButtonDetail != null) == null)
            {
                goto Label_005A;
            }
            this.ButtonDetail.get_onClick().AddListener(new UnityAction(this, this.OnClickDetail));
        Label_005A:
            if ((this.ButtonHelp != null) == null)
            {
                goto Label_0087;
            }
            this.ButtonHelp.get_onClick().AddListener(new UnityAction(this, this.OnClickHelp));
        Label_0087:
            if ((this.ButtonReward != null) == null)
            {
                goto Label_00B4;
            }
            this.ButtonReward.get_onClick().AddListener(new UnityAction(this, this.OnClickDetail));
        Label_00B4:
            return;
        }

        private void GotoShop(TrophyParam trophy)
        {
            char[] chArray1;
            GameManager manager;
            PlayerData data;
            EShopType type;
            char[] chArray;
            string[] strArray;
            UnlockTargets targets;
            EShopType type2;
            data = MonoSingleton<GameManager>.Instance.Player;
            type = 0;
            if (string.IsNullOrEmpty(trophy.Objectives[0].sval_base) != null)
            {
                goto Label_0070;
            }
            chArray1 = new char[] { 0x2c };
            chArray = chArray1;
            strArray = trophy.Objectives[0].sval_base.Split(chArray);
            if (string.IsNullOrEmpty(strArray[0]) == null)
            {
                goto Label_005C;
            }
            type = 0;
            goto Label_0070;
        Label_005C:
            type = MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]);
        Label_0070:
            if (type < 0)
            {
                goto Label_014B;
            }
            if (data.CheckUnlockShopType(type) == null)
            {
                goto Label_014B;
            }
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_00B6;

                case 1:
                    goto Label_00C6;

                case 2:
                    goto Label_00D6;

                case 3:
                    goto Label_00E6;

                case 4:
                    goto Label_00F6;

                case 5:
                    goto Label_0106;

                case 6:
                    goto Label_0116;

                case 7:
                    goto Label_0126;

                case 8:
                    goto Label_0136;
            }
            goto Label_0146;
        Label_00B6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f5);
            goto Label_014B;
        Label_00C6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f6);
            goto Label_014B;
        Label_00D6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f7);
            goto Label_014B;
        Label_00E6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f8);
            goto Label_014B;
        Label_00F6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f9);
            goto Label_014B;
        Label_0106:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fa);
            goto Label_014B;
        Label_0116:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fb);
            goto Label_014B;
        Label_0126:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fc);
            goto Label_014B;
        Label_0136:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fd);
            goto Label_014B;
        Label_0146:;
        Label_014B:
            if (type < 0)
            {
                goto Label_018E;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type) != null)
            {
                goto Label_018E;
            }
        Label_0167:
            try
            {
                targets = SRPG_Extensions.ToUnlockTargets(type);
                LevelLock.ShowLockMessage(data.Lv, data.VipRank, targets);
                goto Label_018E;
            }
            catch (Exception)
            {
            Label_0188:
                goto Label_018E;
            }
        Label_018E:
            return;
        }

        private bool IsIncludeObject(GameObject value)
        {
            Transform transform;
            transform = value.get_transform();
            goto Label_002B;
        Label_000C:
            if ((base.get_gameObject() == transform.get_gameObject()) == null)
            {
                goto Label_0024;
            }
            return 1;
        Label_0024:
            transform = transform.get_parent();
        Label_002B:
            if ((transform != null) != null)
            {
                goto Label_000C;
            }
            return 0;
        }

        private void MsgBoxJumpToQuest(GameObject go)
        {
        }

        private void OnClickDetail()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            this.mShowingOverlay = 1;
            return;
        }

        private unsafe void OnClickHelp()
        {
            TrophyParam param;
            string str;
            string str2;
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (param.help != null)
            {
                goto Label_005D;
            }
            str = ((TrophyConditionTypes) param.Objectives[0].type).ToString().ToUpper();
            str2 = LocalizedText.Get("sys.CHALLENGE_HELP_" + str);
            FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, str2);
            goto Label_0072;
        Label_005D:
            FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, &param.help.ToString());
        Label_0072:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            this.mShowingOverlay = 1;
            return;
        }

        private unsafe void OnClickTry()
        {
            TrophyParam param;
            QuestParam param2;
            GameManager manager;
            PlayerData data;
            QuestTypes types;
            QuestTypes types2;
            TrophyConditionTypes types3;
            QuestTypes types4;
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_001C;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_001C:
            param2 = new QuestParam();
            data = MonoSingleton<GameManager>.Instance.Player;
            switch ((param.Objectives[0].type - 1))
            {
                case 0:
                    goto Label_01F4;

                case 1:
                    goto Label_0555;

                case 2:
                    goto Label_0555;

                case 3:
                    goto Label_0555;

                case 4:
                    goto Label_02B5;

                case 5:
                    goto Label_02F5;

                case 6:
                    goto Label_030D;

                case 7:
                    goto Label_031D;

                case 8:
                    goto Label_032D;

                case 9:
                    goto Label_033D;

                case 10:
                    goto Label_0379;

                case 11:
                    goto Label_0555;

                case 12:
                    goto Label_0555;

                case 13:
                    goto Label_0555;

                case 14:
                    goto Label_0389;

                case 15:
                    goto Label_0389;

                case 0x10:
                    goto Label_0555;

                case 0x11:
                    goto Label_0555;

                case 0x12:
                    goto Label_0555;

                case 0x13:
                    goto Label_0555;

                case 20:
                    goto Label_0555;

                case 0x15:
                    goto Label_0555;

                case 0x16:
                    goto Label_0555;

                case 0x17:
                    goto Label_03BF;

                case 0x18:
                    goto Label_03BF;

                case 0x19:
                    goto Label_03DF;

                case 0x1a:
                    goto Label_03DF;

                case 0x1b:
                    goto Label_03DF;

                case 0x1c:
                    goto Label_03EF;

                case 0x1d:
                    goto Label_03EF;

                case 30:
                    goto Label_03EF;

                case 0x1f:
                    goto Label_03CF;

                case 0x20:
                    goto Label_032D;

                case 0x21:
                    goto Label_01F4;

                case 0x22:
                    goto Label_031D;

                case 0x23:
                    goto Label_03FF;

                case 0x24:
                    goto Label_040B;

                case 0x25:
                    goto Label_041B;

                case 0x26:
                    goto Label_042B;

                case 0x27:
                    goto Label_031D;

                case 40:
                    goto Label_031D;

                case 0x29:
                    goto Label_0562;

                case 0x2a:
                    goto Label_0562;

                case 0x2b:
                    goto Label_0562;

                case 0x2c:
                    goto Label_0562;

                case 0x2d:
                    goto Label_043B;

                case 0x2e:
                    goto Label_01F4;

                case 0x2f:
                    goto Label_02B5;

                case 0x30:
                    goto Label_02F5;

                case 0x31:
                    goto Label_043B;

                case 50:
                    goto Label_0389;

                case 0x33:
                    goto Label_0562;

                case 0x34:
                    goto Label_04B9;

                case 0x35:
                    goto Label_04B9;

                case 0x36:
                    goto Label_0562;

                case 0x37:
                    goto Label_0562;

                case 0x38:
                    goto Label_0562;

                case 0x39:
                    goto Label_0562;

                case 0x3a:
                    goto Label_0562;

                case 0x3b:
                    goto Label_0562;

                case 60:
                    goto Label_0562;

                case 0x3d:
                    goto Label_0562;

                case 0x3e:
                    goto Label_02F5;

                case 0x3f:
                    goto Label_02F5;

                case 0x40:
                    goto Label_02F5;

                case 0x41:
                    goto Label_02F5;

                case 0x42:
                    goto Label_02F5;

                case 0x43:
                    goto Label_02F5;

                case 0x44:
                    goto Label_02F5;

                case 0x45:
                    goto Label_02F5;

                case 70:
                    goto Label_02F5;

                case 0x47:
                    goto Label_02F5;

                case 0x48:
                    goto Label_02F5;

                case 0x49:
                    goto Label_02F5;

                case 0x4a:
                    goto Label_0562;

                case 0x4b:
                    goto Label_0562;

                case 0x4c:
                    goto Label_0562;

                case 0x4d:
                    goto Label_01F4;

                case 0x4e:
                    goto Label_01F4;

                case 0x4f:
                    goto Label_0562;

                case 80:
                    goto Label_04F5;

                case 0x51:
                    goto Label_04F5;

                case 0x52:
                    goto Label_0505;

                case 0x53:
                    goto Label_0505;

                case 0x54:
                    goto Label_0505;

                case 0x55:
                    goto Label_0505;

                case 0x56:
                    goto Label_0505;

                case 0x57:
                    goto Label_0505;

                case 0x58:
                    goto Label_0505;

                case 0x59:
                    goto Label_0515;

                case 90:
                    goto Label_0515;

                case 0x5b:
                    goto Label_0515;

                case 0x5c:
                    goto Label_0515;

                case 0x5d:
                    goto Label_0515;

                case 0x5e:
                    goto Label_0515;

                case 0x5f:
                    goto Label_0515;

                case 0x60:
                    goto Label_0515;

                case 0x61:
                    goto Label_0515;

                case 0x62:
                    goto Label_0535;

                case 0x63:
                    goto Label_01F4;

                case 100:
                    goto Label_01F4;

                case 0x65:
                    goto Label_01F4;

                case 0x66:
                    goto Label_02DD;

                case 0x67:
                    goto Label_0525;

                case 0x68:
                    goto Label_0525;

                case 0x69:
                    goto Label_0545;
            }
            goto Label_0562;
        Label_01F4:
            types = 0;
            if (param2.TransSectionGotoQuest(param.Objectives[0].sval_base, &types, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_021E;
            }
            return;
        Label_021E:
            types4 = types;
            switch ((types4 - 5))
            {
                case 0:
                    goto Label_0260;

                case 1:
                    goto Label_0253;

                case 2:
                    goto Label_0280;

                case 3:
                    goto Label_0253;

                case 4:
                    goto Label_0253;

                case 5:
                    goto Label_0260;

                case 6:
                    goto Label_0253;

                case 7:
                    goto Label_0253;

                case 8:
                    goto Label_0270;

                case 9:
                    goto Label_0290;
            }
        Label_0253:
            if (types4 == 1)
            {
                goto Label_0290;
            }
            goto Label_02A0;
        Label_0260:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_02B0;
        Label_0270:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x403);
            goto Label_02B0;
        Label_0280:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x401);
            goto Label_02B0;
        Label_0290:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ed);
            goto Label_02B0;
        Label_02A0:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e9);
        Label_02B0:
            goto Label_0590;
        Label_02B5:
            if (param2.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_02CD;
            }
            return;
        Label_02CD:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e9);
            goto Label_0590;
        Label_02DD:
            param2.GotoEventListQuest(null, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_0590;
        Label_02F5:
            param2.GotoEventListQuest(null, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_0590;
        Label_030D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            goto Label_0590;
        Label_031D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ed);
            goto Label_0590;
        Label_032D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ea);
            goto Label_0590;
        Label_033D:
            if (data.CheckUnlock(0x800) == null)
            {
                goto Label_035D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3eb);
            goto Label_0374;
        Label_035D:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x800);
        Label_0374:
            goto Label_0590;
        Label_0379:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ec);
            goto Label_0590;
        Label_0389:
            if (data.CheckUnlock(0x10) == null)
            {
                goto Label_03A6;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f0);
            goto Label_03BA;
        Label_03A6:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10);
        Label_03BA:
            goto Label_0590;
        Label_03BF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f1);
            goto Label_0590;
        Label_03CF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f2);
            goto Label_0590;
        Label_03DF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f3);
            goto Label_0590;
        Label_03EF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f4);
            goto Label_0590;
        Label_03FF:
            this.GotoShop(param);
            goto Label_0590;
        Label_040B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fe);
            goto Label_0590;
        Label_041B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ff);
            goto Label_0590;
        Label_042B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x400);
            goto Label_0590;
        Label_043B:
            types2 = 0;
            if (param2.TransSectionGotoTower(param.Objectives[0].sval_base, &types2) != null)
            {
                goto Label_0459;
            }
            return;
        Label_0459:
            types4 = types2;
            switch ((types4 - 10))
            {
                case 0:
                    goto Label_0484;

                case 1:
                    goto Label_0477;

                case 2:
                    goto Label_0477;

                case 3:
                    goto Label_0494;
            }
        Label_0477:
            if (types4 == 5)
            {
                goto Label_0484;
            }
            goto Label_04A4;
        Label_0484:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_04B4;
        Label_0494:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x403);
            goto Label_04B4;
        Label_04A4:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x401);
        Label_04B4:
            goto Label_0590;
        Label_04B9:
            if (data.CheckUnlock(0x10000) == null)
            {
                goto Label_04D9;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x402);
            goto Label_04F0;
        Label_04D9:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10000);
        Label_04F0:
            goto Label_0590;
        Label_04F5:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x404);
            goto Label_0590;
        Label_0505:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x405);
            goto Label_0590;
        Label_0515:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f4);
            goto Label_0590;
        Label_0525:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x406);
            goto Label_0590;
        Label_0535:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x408);
            goto Label_0590;
        Label_0545:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x407);
            goto Label_0590;
        Label_0555:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0590;
        Label_0562:
            DebugUtility.Log(string.Format("未知の Trophy 条件 / {0}", (TrophyConditionTypes) param.Objectives[0].type));
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0590:
            return;
        }

        private void OnDestroy()
        {
            if ((this.ButtonReward != null) == null)
            {
                goto Label_002D;
            }
            this.ButtonReward.get_onClick().RemoveListener(new UnityAction(this, this.OnClickDetail));
        Label_002D:
            if ((this.ButtonHelp != null) == null)
            {
                goto Label_005A;
            }
            this.ButtonHelp.get_onClick().RemoveListener(new UnityAction(this, this.OnClickHelp));
        Label_005A:
            if ((this.ButtonDetail != null) == null)
            {
                goto Label_0087;
            }
            this.ButtonDetail.get_onClick().RemoveListener(new UnityAction(this, this.OnClickDetail));
        Label_0087:
            if ((this.ButtonTry != null) == null)
            {
                goto Label_00B4;
            }
            this.ButtonTry.get_onClick().RemoveListener(new UnityAction(this, this.OnClickTry));
        Label_00B4:
            return;
        }

        private void OnDisable()
        {
            GlobalEvent.Invoke(this.EVENT_CHALLENGE_ICON_SHOW, this);
            return;
        }

        private void OnEnable()
        {
            GlobalEvent.Invoke(this.EVENT_CHALLENGE_ICON_HIDE, this);
            return;
        }

        private void Refresh(bool fromRefresh)
        {
            TrophyParam param;
            TrophyState state;
            ChallengeCategoryParam[] paramArray;
            ChallengeCategoryParam param2;
            bool flag;
            this.mShowingOverlay = 0;
            param = ChallengeMission.GetTopMostPriorityTrophy();
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            state = ChallengeMission.GetTrophyCounter(param);
            if (state.IsEnded != null)
            {
                goto Label_0045;
            }
            DataSource.Bind<TrophyParam>(base.get_gameObject(), param);
            DataSource.Bind<TrophyState>(base.get_gameObject(), state);
            this.RefreshRewardIcon(param);
        Label_0045:
            if (base.get_gameObject().get_activeSelf() != null)
            {
                goto Label_0061;
            }
            base.get_gameObject().SetActive(1);
        Label_0061:
            GameParameter.UpdateAll(base.get_gameObject());
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
            if (paramArray == null)
            {
                goto Label_00A3;
            }
            if (((int) paramArray.Length) <= 0)
            {
                goto Label_00A3;
            }
            param2 = Enumerable.FirstOrDefault<ChallengeCategoryParam>(paramArray);
            DataSource.Bind<ChallengeCategoryParam>(this.HelpIcon.get_gameObject(), param2);
        Label_00A3:
            if (param.IsChallengeMissionRoot == null)
            {
                goto Label_00B6;
            }
            flag = 1;
            goto Label_00BE;
        Label_00B6:
            flag = state.IsCompleted;
        Label_00BE:
            if ((this.ButtonTry != null) == null)
            {
                goto Label_0153;
            }
            if ((this.ButtonDetail != null) == null)
            {
                goto Label_0153;
            }
            if ((this.ButtonReward != null) == null)
            {
                goto Label_0153;
            }
            if ((this.ButtonHelp != null) == null)
            {
                goto Label_0153;
            }
            this.ButtonTry.get_gameObject().SetActive(flag == 0);
            this.ButtonDetail.get_gameObject().SetActive(flag == 0);
            this.ButtonReward.get_gameObject().SetActive(flag);
            this.ButtonHelp.get_gameObject().SetActive(flag == 0);
        Label_0153:
            return;
        }

        private unsafe void RefreshRewardIcon(TrophyParam trophy)
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            string str;
            ItemParam param;
            bool flag5;
            string str2;
            ConceptCardIcon icon;
            ConceptCardData data;
            JSON_ConceptCard card;
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            flag4 = 0;
            str = string.Empty;
            param = null;
            flag5 = 0;
            str2 = "@{0}";
            if (trophy.Gold == null)
            {
                goto Label_0042;
            }
            flag3 = 1;
            str = string.Format(str2, (int) trophy.Gold);
            goto Label_0163;
        Label_0042:
            if (trophy.Exp == null)
            {
                goto Label_0068;
            }
            flag2 = 1;
            str = string.Format(str2, (int) trophy.Exp);
            goto Label_0163;
        Label_0068:
            if (trophy.Coin == null)
            {
                goto Label_009F;
            }
            flag = 1;
            param = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            str = string.Format(str2, (int) trophy.Coin);
            goto Label_0163;
        Label_009F:
            if (trophy.Stamina == null)
            {
                goto Label_00C5;
            }
            flag4 = 1;
            str = string.Format(str2, (int) trophy.Stamina);
            goto Label_0163;
        Label_00C5:
            if (trophy.Items == null)
            {
                goto Label_0128;
            }
            if (((int) trophy.Items.Length) <= 0)
            {
                goto Label_0128;
            }
            flag = 1;
            param = MonoSingleton<GameManager>.Instance.GetItemParam(&(trophy.Items[0]).iname);
            if (param == null)
            {
                goto Label_0163;
            }
            str = string.Format(str2, (int) &(trophy.Items[0]).Num);
            goto Label_0163;
        Label_0128:
            if (trophy.ConceptCards == null)
            {
                goto Label_0163;
            }
            if (((int) trophy.ConceptCards.Length) <= 0)
            {
                goto Label_0163;
            }
            flag5 = 1;
            str = string.Format(str2, (int) &(trophy.ConceptCards[0]).Num);
        Label_0163:
            if ((this.ImageItem != null) == null)
            {
                goto Label_0185;
            }
            this.ImageItem.get_gameObject().SetActive(flag);
        Label_0185:
            if ((this.ImageExp != null) == null)
            {
                goto Label_01A7;
            }
            this.ImageExp.get_gameObject().SetActive(flag2);
        Label_01A7:
            if ((this.ImageGold != null) == null)
            {
                goto Label_01C9;
            }
            this.ImageGold.get_gameObject().SetActive(flag3);
        Label_01C9:
            if ((this.ImageAp != null) == null)
            {
                goto Label_01EB;
            }
            this.ImageAp.get_gameObject().SetActive(flag4);
        Label_01EB:
            if ((this.TextReward != null) == null)
            {
                goto Label_0209;
            }
            this.TextReward.set_text(str);
        Label_0209:
            if ((this.ConceptCard != null) == null)
            {
                goto Label_0227;
            }
            this.ConceptCard.SetActive(flag5);
        Label_0227:
            if (flag5 == null)
            {
                goto Label_02A7;
            }
            icon = this.ConceptCard.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_02B4;
            }
            data = new ConceptCardData();
            card = new JSON_ConceptCard();
            card.iid = 1L;
            card.iname = &(trophy.ConceptCards[0]).iname;
            card.exp = 0;
            card.trust = 0;
            card.fav = 0;
            data.Deserialize(card);
            icon.Setup(data);
            goto Label_02B4;
        Label_02A7:
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
        Label_02B4:
            return;
        }

        private unsafe void Update()
        {
            EventSystem system;
            List<RaycastResult> list;
            PointerEventData data;
            bool flag;
            RaycastResult result;
            RaycastResult result2;
            if (this.mShowingOverlay != null)
            {
                goto Label_00C7;
            }
            if (Input.GetMouseButtonDown(0) == null)
            {
                goto Label_00C7;
            }
            system = EventSystem.get_current();
            list = new List<RaycastResult>();
            data = new PointerEventData(system);
            flag = 1;
            data.set_position(new Vector2(285f, 376f));
            system.RaycastAll(data, list);
            if (list.Count == null)
            {
                goto Label_0070;
            }
            result = list[0];
            if (this.IsIncludeObject(&result.get_gameObject()) == null)
            {
                goto Label_0070;
            }
            flag = 0;
        Label_0070:
            if (flag == null)
            {
                goto Label_0077;
            }
            return;
        Label_0077:
            list.Clear();
            data.set_position(Input.get_mousePosition());
            system.RaycastAll(data, list);
            if (list.Count == null)
            {
                goto Label_00C7;
            }
            result2 = list[0];
            if (this.IsIncludeObject(&result2.get_gameObject()) != null)
            {
                goto Label_00C7;
            }
            base.get_gameObject().SetActive(0);
        Label_00C7:
            return;
        }
    }
}

