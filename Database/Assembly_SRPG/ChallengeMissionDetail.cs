namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x3eb, "装備強化画面へ移動", 1, 0x3ec), Pin(0x3ed, "マルチプレイへ移動", 1, 0x3ed), Pin(0x3ee, "エリートクエスト選択へ移動", 1, 0x3ee), Pin(0x3ef, "イベントクエスト選択へ移動", 1, 0x3ef), Pin(0x3f0, "アリーナへ移動", 1, 0x3f0), Pin(0x3f1, "ユニット強化画面へ移動", 1, 0x3f1), Pin(0x3f2, "ユニットアビリティセット画面へ移動", 1, 0x3f2), Pin(0x3f3, "ユニットジョブ画面へ移動", 1, 0x3f3), Pin(0x3f4, "ユニット選択へ移動", 1, 0x3f4), Pin(0x3f5, "アンナの店へ移動", 1, 0x3f5), Pin(0x3f6, "マリアの店へ移動", 1, 0x3f6), Pin(0x3f7, "ルイザの店へ移動", 1, 0x3f7), Pin(0x3f8, "ソウルショップへ移動", 1, 0x3f8), Pin(0x3f9, "ツアーの店へ移動", 1, 0x3f9), Pin(0x3fa, "闘技場交換所へ移動", 1, 0x3fa), Pin(0x3fb, "マルチ交換所へ移動", 1, 0x3fb), Pin(0x3fc, "魂の交換所へ移動", 1, 0x3fc), Pin(0x3fd, "武具の店へ移動", 1, 0x3fd), Pin(0x3fe, "武具錬成画面へ移動", 1, 0x3fe), Pin(0x3ff, "武具強化画面へ移動", 1, 0x3ff), Pin(0x400, "武具進化画面へ移動", 1, 0x400), Pin(0x401, "FgGID画面へ移動", 1, 0x401), Pin(0x402, "塔へ移動", 1, 0x402), Pin(0x403, "対戦へ移動", 1, 0x403), Pin(0x404, "初心者イベントクエスト選択へ移動", 1, 0x404), Pin(0x405, "初心者の館へ移動", 1, 0x405), Pin(0x406, "真理念装へ移動", 1, 0x406), Pin(0x407, "カルマへ移動", 1, 0x407), Pin(0x408, "ホームへ移動", 1, 0x408), Pin(0x409, "お知らせへ移動", 1, 0x409), Pin(0x40a, "フレンド画面へ移動", 1, 0x40a), Pin(0x3ea, "アビリティ強化画面へ移動", 1, 0x3eb), Pin(0x3ec, "ゴールド購入画面へ移動", 1, 0x3ea), Pin(0x3e9, "クエスト選択へ移動", 1, 0x3e9), Pin(0x3e8, "ガチャへ移動", 1, 0x3e8), Pin(100, "キャンセル", 1, 100), Pin(0, "表示", 0, 0)]
    public class ChallengeMissionDetail : MonoBehaviour, IFlowInterface
    {
        public const int PIN_OPEN = 0;
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
        public const int PIN_GOTO_FGGID = 0x401;
        public const int PIN_GOTO_TOWER = 0x402;
        public const int PIN_GOTO_VERSUS = 0x403;
        public const int PIN_GOTO_EVENT_BEGINEER = 0x404;
        public const int PIN_GOTO_BEGINNERTOP = 0x405;
        public const int PIN_GOTO_CONCEPTCARD = 0x406;
        public const int PIN_GOTO_ORDEAL = 0x407;
        public const int PIN_GOTO_HOME = 0x408;
        public const int PIN_GOTO_NEWS = 0x409;
        public const int PIN_GOTO_FRIEND = 0x40a;
        public RawImage ImageItem;
        public RawImage ImageExp;
        public RawImage ImageGold;
        public RawImage ImageStamina;
        public Text TextReward;
        public Button ButtonCancel;
        public Button ButtonTry;
        public ConceptCardIcon ConceptCardObject;

        public ChallengeMissionDetail()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0012;
            }
            base.get_gameObject().SetActive(1);
        Label_0012:
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

        private void MsgBoxJumpToQuest(GameObject go)
        {
        }

        private void OnCancel()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnEnable()
        {
            TrophyParam param;
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            this.UpdateReward(param);
            return;
        }

        private unsafe void OnTry()
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
                    goto Label_0565;

                case 2:
                    goto Label_0565;

                case 3:
                    goto Label_0565;

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
                    goto Label_0565;

                case 12:
                    goto Label_0565;

                case 13:
                    goto Label_0565;

                case 14:
                    goto Label_0389;

                case 15:
                    goto Label_0389;

                case 0x10:
                    goto Label_0565;

                case 0x11:
                    goto Label_0565;

                case 0x12:
                    goto Label_03BF;

                case 0x13:
                    goto Label_0565;

                case 20:
                    goto Label_0565;

                case 0x15:
                    goto Label_0565;

                case 0x16:
                    goto Label_0565;

                case 0x17:
                    goto Label_03CF;

                case 0x18:
                    goto Label_03CF;

                case 0x19:
                    goto Label_03EF;

                case 0x1a:
                    goto Label_03EF;

                case 0x1b:
                    goto Label_03EF;

                case 0x1c:
                    goto Label_03FF;

                case 0x1d:
                    goto Label_03FF;

                case 30:
                    goto Label_03FF;

                case 0x1f:
                    goto Label_03DF;

                case 0x20:
                    goto Label_032D;

                case 0x21:
                    goto Label_01F4;

                case 0x22:
                    goto Label_031D;

                case 0x23:
                    goto Label_040F;

                case 0x24:
                    goto Label_041B;

                case 0x25:
                    goto Label_042B;

                case 0x26:
                    goto Label_043B;

                case 0x27:
                    goto Label_031D;

                case 40:
                    goto Label_031D;

                case 0x29:
                    goto Label_0572;

                case 0x2a:
                    goto Label_0572;

                case 0x2b:
                    goto Label_0572;

                case 0x2c:
                    goto Label_0572;

                case 0x2d:
                    goto Label_044B;

                case 0x2e:
                    goto Label_01F4;

                case 0x2f:
                    goto Label_02B5;

                case 0x30:
                    goto Label_02F5;

                case 0x31:
                    goto Label_044B;

                case 50:
                    goto Label_0389;

                case 0x33:
                    goto Label_0572;

                case 0x34:
                    goto Label_04C9;

                case 0x35:
                    goto Label_04C9;

                case 0x36:
                    goto Label_0572;

                case 0x37:
                    goto Label_0572;

                case 0x38:
                    goto Label_0572;

                case 0x39:
                    goto Label_0572;

                case 0x3a:
                    goto Label_0572;

                case 0x3b:
                    goto Label_0572;

                case 60:
                    goto Label_0572;

                case 0x3d:
                    goto Label_0572;

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
                    goto Label_0572;

                case 0x4b:
                    goto Label_0572;

                case 0x4c:
                    goto Label_0572;

                case 0x4d:
                    goto Label_01F4;

                case 0x4e:
                    goto Label_01F4;

                case 0x4f:
                    goto Label_0572;

                case 80:
                    goto Label_0505;

                case 0x51:
                    goto Label_0505;

                case 0x52:
                    goto Label_0515;

                case 0x53:
                    goto Label_0515;

                case 0x54:
                    goto Label_0515;

                case 0x55:
                    goto Label_0515;

                case 0x56:
                    goto Label_0515;

                case 0x57:
                    goto Label_0515;

                case 0x58:
                    goto Label_0515;

                case 0x59:
                    goto Label_0525;

                case 90:
                    goto Label_0525;

                case 0x5b:
                    goto Label_0525;

                case 0x5c:
                    goto Label_0525;

                case 0x5d:
                    goto Label_0525;

                case 0x5e:
                    goto Label_0525;

                case 0x5f:
                    goto Label_0525;

                case 0x60:
                    goto Label_0525;

                case 0x61:
                    goto Label_0525;

                case 0x62:
                    goto Label_0545;

                case 0x63:
                    goto Label_01F4;

                case 100:
                    goto Label_01F4;

                case 0x65:
                    goto Label_01F4;

                case 0x66:
                    goto Label_02DD;

                case 0x67:
                    goto Label_0535;

                case 0x68:
                    goto Label_0535;

                case 0x69:
                    goto Label_0555;
            }
            goto Label_0572;
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
            FlowNode_GameObject.ActivateOutputLinks(this, 0x404);
            goto Label_02B0;
        Label_0280:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x402);
            goto Label_02B0;
        Label_0290:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ed);
            goto Label_02B0;
        Label_02A0:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e9);
        Label_02B0:
            goto Label_05A0;
        Label_02B5:
            if (param2.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_02CD;
            }
            return;
        Label_02CD:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e9);
            goto Label_05A0;
        Label_02DD:
            param2.GotoEventListQuest(null, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_05A0;
        Label_02F5:
            param2.GotoEventListQuest(null, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_05A0;
        Label_030D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            goto Label_05A0;
        Label_031D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ed);
            goto Label_05A0;
        Label_032D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ea);
            goto Label_05A0;
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
            goto Label_05A0;
        Label_0379:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ec);
            goto Label_05A0;
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
            goto Label_05A0;
        Label_03BF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x401);
            goto Label_05A0;
        Label_03CF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f1);
            goto Label_05A0;
        Label_03DF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f2);
            goto Label_05A0;
        Label_03EF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f3);
            goto Label_05A0;
        Label_03FF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f4);
            goto Label_05A0;
        Label_040F:
            this.GotoShop(param);
            goto Label_05A0;
        Label_041B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fe);
            goto Label_05A0;
        Label_042B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ff);
            goto Label_05A0;
        Label_043B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x400);
            goto Label_05A0;
        Label_044B:
            types2 = 0;
            if (param2.TransSectionGotoTower(param.Objectives[0].sval_base, &types2) != null)
            {
                goto Label_0469;
            }
            return;
        Label_0469:
            types4 = types2;
            switch ((types4 - 10))
            {
                case 0:
                    goto Label_0494;

                case 1:
                    goto Label_0487;

                case 2:
                    goto Label_0487;

                case 3:
                    goto Label_04A4;
            }
        Label_0487:
            if (types4 == 5)
            {
                goto Label_0494;
            }
            goto Label_04B4;
        Label_0494:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3ef);
            goto Label_04C4;
        Label_04A4:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x404);
            goto Label_04C4;
        Label_04B4:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x402);
        Label_04C4:
            goto Label_05A0;
        Label_04C9:
            if (data.CheckUnlock(0x10000) == null)
            {
                goto Label_04E9;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x403);
            goto Label_0500;
        Label_04E9:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10000);
        Label_0500:
            goto Label_05A0;
        Label_0505:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x405);
            goto Label_05A0;
        Label_0515:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x406);
            goto Label_05A0;
        Label_0525:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f4);
            goto Label_05A0;
        Label_0535:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x407);
            goto Label_05A0;
        Label_0545:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x40a);
            goto Label_05A0;
        Label_0555:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x409);
            goto Label_05A0;
        Label_0565:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_05A0;
        Label_0572:
            DebugUtility.Log(string.Format("未知の Trophy 条件 / {0}", (TrophyConditionTypes) param.Objectives[0].type));
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_05A0:
            return;
        }

        private void Start()
        {
            if ((this.ButtonCancel == null) != null)
            {
                goto Label_0022;
            }
            if ((this.ButtonTry == null) == null)
            {
                goto Label_002A;
            }
        Label_0022:
            base.set_enabled(0);
            return;
        Label_002A:
            this.ButtonCancel.get_onClick().AddListener(new UnityAction(this, this.OnCancel));
            this.ButtonTry.get_onClick().AddListener(new UnityAction(this, this.OnTry));
            return;
        }

        private unsafe void UpdateReward(TrophyParam trophy)
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            string str;
            ItemParam param;
            string str2;
            UnitParam param2;
            ConceptCardParam param3;
            string str3;
            ConceptCardData data;
            if (trophy != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            flag4 = 0;
            flag5 = 0;
            str = string.Empty;
            param = null;
            if (trophy.Gold == null)
            {
                goto Label_004A;
            }
            flag3 = 1;
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (int) trophy.Gold);
            goto Label_0255;
        Label_004A:
            if (trophy.Exp == null)
            {
                goto Label_0078;
            }
            flag2 = 1;
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (int) trophy.Exp);
            goto Label_0255;
        Label_0078:
            if (trophy.Coin == null)
            {
                goto Label_00B7;
            }
            flag = 1;
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) trophy.Coin);
            param = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            goto Label_0255;
        Label_00B7:
            if (trophy.Stamina == null)
            {
                goto Label_00E5;
            }
            flag4 = 1;
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (int) trophy.Stamina);
            goto Label_0255;
        Label_00E5:
            if (trophy.Items == null)
            {
                goto Label_01B9;
            }
            if (((int) trophy.Items.Length) <= 0)
            {
                goto Label_01B9;
            }
            flag = 1;
            param = MonoSingleton<GameManager>.Instance.GetItemParam(&(trophy.Items[0]).iname);
            if (param == null)
            {
                goto Label_0255;
            }
            str2 = string.Empty;
            if (param.type != 0x10)
            {
                goto Label_0182;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetUnitParam(param.iname);
            if (param2 == null)
            {
                goto Label_0255;
            }
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT"), (int) (param2.rare + 1), param2.name);
            goto Label_01B4;
        Label_0182:
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD"), param.name, (int) &(trophy.Items[0]).Num);
        Label_01B4:
            goto Label_0255;
        Label_01B9:
            if (trophy.ConceptCards == null)
            {
                goto Label_0255;
            }
            if (((int) trophy.ConceptCards.Length) <= 0)
            {
                goto Label_0255;
            }
            flag5 = 1;
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(&(trophy.ConceptCards[0]).iname);
            str = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_CONCEPT_CARD"), param3.name, (int) &(trophy.ConceptCards[0]).Num);
            if ((this.ConceptCardObject != null) == null)
            {
                goto Label_0255;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(param3.iname);
            this.ConceptCardObject.Setup(data);
        Label_0255:
            if ((this.ImageItem != null) == null)
            {
                goto Label_0277;
            }
            this.ImageItem.get_gameObject().SetActive(flag);
        Label_0277:
            if ((this.ImageExp != null) == null)
            {
                goto Label_0299;
            }
            this.ImageExp.get_gameObject().SetActive(flag2);
        Label_0299:
            if ((this.ImageGold != null) == null)
            {
                goto Label_02BB;
            }
            this.ImageGold.get_gameObject().SetActive(flag3);
        Label_02BB:
            if ((this.ImageStamina != null) == null)
            {
                goto Label_02DD;
            }
            this.ImageStamina.get_gameObject().SetActive(flag4);
        Label_02DD:
            if (param == null)
            {
                goto Label_02F1;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
        Label_02F1:
            if ((this.TextReward != null) == null)
            {
                goto Label_030F;
            }
            this.TextReward.set_text(str);
        Label_030F:
            if ((this.ConceptCardObject != null) == null)
            {
                goto Label_0332;
            }
            this.ConceptCardObject.get_gameObject().SetActive(flag5);
        Label_0332:
            return;
        }
    }
}

