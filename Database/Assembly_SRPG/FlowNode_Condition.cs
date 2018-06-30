namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    [Pin(100, "Test", 0, 0), Pin(2, "No", 1, 2), NodeType("SRPG/条件判定", 0x7fe5), Pin(1, "Yes", 1, 1)]
    public class FlowNode_Condition : FlowNode
    {
        public Conditions Condition;

        public FlowNode_Condition()
        {
            base..ctor();
            return;
        }

        public override string[] GetInfoLines()
        {
            string[] textArray1;
            textArray1 = new string[] { "Condition is " + ((Conditions) this.Condition).ToString() };
            return textArray1;
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
                goto Label_0032;
            }
            return data.LeaderSkill.SkillParam;
        Label_0032:
            return null;
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

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0014;
            }
            this.SetResult(this.Test());
        Label_0014:
            return;
        }

        private void SetResult(bool result)
        {
            if (result == null)
            {
                goto Label_0013;
            }
            base.ActivateOutputLinks(1);
            goto Label_001B;
        Label_0013:
            base.ActivateOutputLinks(2);
        Label_001B:
            return;
        }

        private bool Test()
        {
            PlayerData data;
            SupportData data2;
            PartyData data3;
            QuestParam param;
            QuestParam param2;
            int num;
            int num2;
            ItemParam param3;
            FriendData data4;
            BattleCore.Record record;
            SupportData data5;
            int num3;
            int num4;
            QuestParam param4;
            UnitParam param5;
            QuestParam param6;
            ChapterParam param7;
            ChapterParam param8;
            ChapterParam param9;
            string str;
            string str2;
            GameManager manager;
            QuestParam param10;
            GameManager manager2;
            QuestParam param11;
            QuestParam param12;
            Conditions conditions;
            <Test>c__AnonStorey267 storey;
            data = MonoSingleton<GameManager>.Instance.Player;
            switch (this.Condition)
            {
                case 0:
                    goto Label_0109;

                case 1:
                    goto Label_00F3;

                case 2:
                    goto Label_016C;

                case 3:
                    goto Label_014D;

                case 4:
                    goto Label_0192;

                case 5:
                    goto Label_01DF;

                case 6:
                    goto Label_01FF;

                case 7:
                    goto Label_0249;

                case 8:
                    goto Label_026F;

                case 9:
                    goto Label_02EE;

                case 10:
                    goto Label_02F9;

                case 11:
                    goto Label_0304;

                case 12:
                    goto Label_0316;

                case 13:
                    goto Label_0336;

                case 14:
                    goto Label_0356;

                case 15:
                    goto Label_0376;

                case 0x10:
                    goto Label_03A7;

                case 0x11:
                    goto Label_03B2;

                case 0x12:
                    goto Label_03BF;

                case 0x13:
                    goto Label_03E4;

                case 20:
                    goto Label_0414;

                case 0x15:
                    goto Label_0429;

                case 0x16:
                    goto Label_0464;

                case 0x17:
                    goto Label_0499;

                case 0x18:
                    goto Label_0489;

                case 0x19:
                    goto Label_049F;

                case 0x1a:
                    goto Label_04B9;

                case 0x1b:
                    goto Label_04E4;

                case 0x1c:
                    goto Label_04F9;

                case 0x1d:
                    goto Label_052D;

                case 30:
                    goto Label_054D;

                case 0x1f:
                    goto Label_0589;

                case 0x20:
                    goto Label_05DB;

                case 0x21:
                    goto Label_0619;

                case 0x22:
                    goto Label_0657;

                case 0x23:
                    goto Label_0667;

                case 0x24:
                    goto Label_0684;

                case 0x25:
                    goto Label_06A4;

                case 0x26:
                    goto Label_06C9;

                case 0x27:
                    goto Label_06E9;

                case 40:
                    goto Label_0709;

                case 0x29:
                    goto Label_0729;

                case 0x2a:
                    goto Label_075F;

                case 0x2b:
                    goto Label_0797;

                case 0x2c:
                    goto Label_07D9;

                case 0x2d:
                    goto Label_07E4;

                case 0x2e:
                    goto Label_07EF;

                case 0x2f:
                    goto Label_080E;

                case 0x30:
                    goto Label_082D;

                case 0x31:
                    goto Label_085A;

                case 50:
                    goto Label_086A;

                case 0x33:
                    goto Label_088A;

                case 0x34:
                    goto Label_08B5;
            }
            goto Label_08D7;
        Label_00F3:
            if ((data2 = this.GetSupportData()) == null)
            {
                goto Label_0107;
            }
            return data2.IsFriend();
        Label_0107:
            return 0;
        Label_0109:
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param2 == null)
            {
                goto Label_08D7;
            }
            num = param2.RequiredApWithPlayerLv(data.Lv, 1);
            return ((MonoSingleton<GameManager>.Instance.Player.Stamina < num) == 0);
            goto Label_08D7;
        Label_014D:
            if ((data2 = this.GetSupportData()) == null)
            {
                goto Label_08D7;
            }
            return ((data2.LeaderSkill == null) == 0);
            goto Label_08D7;
        Label_016C:
            if ((data3 = DataSource.FindDataOfClass<PartyData>(base.get_gameObject(), null)) == null)
            {
                goto Label_08D7;
            }
            return ((this.GetLeaderSkill(data3) == null) == 0);
            goto Label_08D7;
        Label_0192:
            num2 = 0;
            goto Label_01CB;
        Label_019A:
            if (data.Partys[num2].GetUnitUniqueID(data.Partys[num2].LeaderIndex) != null)
            {
                goto Label_01C5;
            }
            return 0;
        Label_01C5:
            num2 += 1;
        Label_01CB:
            if (num2 < data.Partys.Count)
            {
                goto Label_019A;
            }
            return 1;
        Label_01DF:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.UIParam_TargetValid;
            goto Label_08D7;
        Label_01FF:
            if ((param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null)) == null)
            {
                goto Label_08D7;
            }
            if ((QuestDropParam.Instance == null) == null)
            {
                goto Label_0224;
            }
            return 0;
        Label_0224:
            return ((QuestDropParam.Instance.GetHardDropPiece(param.iname, GlobalVars.GetDropTableGeneratedDateTime()) == null) == 0);
            goto Label_08D7;
        Label_0249:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return ((SceneBattle.Instance.UnitStartCount > 1) == 0);
            goto Label_08D7;
        Label_026F:
            storey = new <Test>c__AnonStorey267();
            storey.support = GlobalVars.SelectedSupport;
            if (storey.support != null)
            {
                goto Label_0295;
            }
            return 0;
        Label_0295:
            if (storey.support.IsFriend() == null)
            {
                goto Label_02A8;
            }
            return 0;
        Label_02A8:
            data4 = data.Friends.Find(new Predicate<FriendData>(storey.<>m__193));
            if (data4 != null)
            {
                goto Label_02CB;
            }
            return 1;
        Label_02CB:
            if (data4.State != 1)
            {
                goto Label_02DA;
            }
            return 0;
        Label_02DA:
            if (data4.State != 2)
            {
                goto Label_08D7;
            }
            return 0;
            goto Label_08D7;
        Label_02EE:
            return GlobalVars.PlayerLevelChanged;
        Label_02F9:
            return GameUtility.Config_NewGame.Value;
        Label_0304:
            return ((GlobalVars.BtlID == 0L) == 0);
        Label_0316:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsPlayingMultiQuest;
            goto Label_08D7;
        Label_0336:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsPlayingArenaQuest;
            goto Label_08D7;
        Label_0356:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsArenaRankupInfo();
            goto Label_08D7;
        Label_0376:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return (SceneBattle.Instance.Battle.GetQuestRecord().IsZero == 0);
            goto Label_08D7;
        Label_03A7:
            return MonoSingleton<GameManager>.Instance.IsAgreeTermsOfUse();
        Label_03B2:
            return ((this.GetSupportData() == null) == 0);
        Label_03BF:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.CurrentQuest.Silent;
            goto Label_08D7;
        Label_03E4:
            data5 = this.GetSupportData();
            if (data5 == null)
            {
                goto Label_08D7;
            }
            num3 = data.Gold;
            if (data5.GetCost() <= num3)
            {
                goto Label_08D7;
            }
            return 1;
            goto Label_08D7;
        Label_0414:
            return MonoSingleton<GameManager>.Instance.Player.CheckUnlock(0x100);
        Label_0429:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            param4 = SceneBattle.Instance.Battle.GetQuest();
            return ((param4 == null) ? 0 : param4.CheckAllowedAutoBattle());
            goto Label_08D7;
        Label_0464:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.Battle.IsAutoBattle;
            goto Label_08D7;
        Label_0489:
            return MonoSingleton<GameManager>.Instance.Player.IsBeginner();
        Label_0499:
            return GameUtility.IsDebugBuild;
        Label_049F:
            return (((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L) == 0);
        Label_04B9:
            param5 = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            return ((MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(param5.iname) == null) == 0);
        Label_04E4:
            return MonoSingleton<GameManager>.Instance.Player.CheckUnlock(0x10000);
        Label_04F9:
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_08D7;
            }
            param6 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param6 == null)
            {
                goto Label_08D7;
            }
            return param6.ShowReviewPopup;
            goto Label_08D7;
        Label_052D:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsFirstWin;
            goto Label_08D7;
        Label_054D:
            if ((((SceneBattle.Instance != null) == null) || (SceneBattle.Instance.CurrentQuest == null)) || (SceneBattle.Instance.CurrentQuest.type != 10))
            {
                goto Label_08D7;
            }
            return 1;
            goto Label_08D7;
        Label_0589:
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_08D7;
            }
            param7 = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if ((param7 == null) || (param7.IsGpsQuest() == null))
            {
                goto Label_08D7;
            }
            return (param7.children.Count > 0);
            goto Label_08D7;
        Label_05DB:
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_08D7;
            }
            param8 = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if (param8 == null)
            {
                goto Label_08D7;
            }
            return param8.IsGpsQuest();
            goto Label_08D7;
        Label_0619:
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_08D7;
            }
            param9 = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if (param9 == null)
            {
                goto Label_08D7;
            }
            return param9.HasGpsQuest();
            goto Label_08D7;
        Label_0657:
            return MonoSingleton<GameManager>.Instance.Player.ValidGpsGift;
        Label_0667:
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_0682;
            }
            return HomeWindow.Current.IsSceneChanging;
        Label_0682:
            return 0;
        Label_0684:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsPlayLastDemo;
            goto Label_08D7;
        Label_06A4:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.Battle.IsRankingQuest;
            goto Label_08D7;
        Label_06C9:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsRankingQuestNewScore;
            goto Label_08D7;
        Label_06E9:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsRankingQuestJoinReward;
            goto Label_08D7;
        Label_0709:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.ValidateRankingQuestRank;
            goto Label_08D7;
        Label_0729:
            str = MyApplicationPlugin.get_version();
            str2 = PlayerPrefsUtility.GetString(PlayerPrefsUtility.AWAKE_VERSION, string.Empty);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.AWAKE_VERSION, str, 1);
            return ((string.Compare(str, str2) == 0) == 0);
        Label_075F:
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_08D7;
            }
            param10 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param10 == null)
            {
                goto Label_08D7;
            }
            return param10.IsMultiAreaQuest;
            goto Label_08D7;
        Label_0797:
            if (string.IsNullOrEmpty(FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.iname) != null)
            {
                goto Label_08D7;
            }
            param11 = MonoSingleton<GameManager>.Instance.FindQuest(FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.iname);
            if (param11 == null)
            {
                goto Label_08D7;
            }
            return param11.IsMultiAreaQuest;
            goto Label_08D7;
        Label_07D9:
            return MonoSingleton<GameManager>.Instance.IsValidAreaQuest();
        Label_07E4:
            return MonoSingleton<GameManager>.Instance.IsValidMultiAreaQuest();
        Label_07EF:
            if (SceneBattle.Instance == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsOrdealQuest;
            goto Label_08D7;
        Label_080E:
            if (SceneBattle.Instance == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsGetFirstClearItem;
            goto Label_08D7;
        Label_082D:
            return ((((byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus) == 1) ? 1 : (((byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus) == 2));
        Label_085A:
            return MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopStarted;
        Label_086A:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_08D7;
            }
            return SceneBattle.Instance.IsCardSendMail;
            goto Label_08D7;
        Label_088A:
            param12 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param12 == null)
            {
                goto Label_08D7;
            }
            if (param12.HasMission() == null)
            {
                goto Label_08D7;
            }
            return 1;
            goto Label_08D7;
        Label_08B5:
            if (data.IsBeginner() == null)
            {
                goto Label_08D7;
            }
            if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED, 0) != null)
            {
                goto Label_08D7;
            }
            return 1;
        Label_08D7:
            return 0;
        }

        [CompilerGenerated]
        private sealed class <Test>c__AnonStorey267
        {
            internal SupportData support;

            public <Test>c__AnonStorey267()
            {
                base..ctor();
                return;
            }

            internal bool <>m__193(FriendData f)
            {
                return (f.FUID == this.support.FUID);
            }
        }

        public enum Conditions
        {
            [Description("スタミナが足りているか", "クエストを開始するのに必要なスタミナがあるかどうか判定します。")]
            QUEST_HASENOUGHSTAMINA = 0,
            [Description("フレンドであるか")]
            FRIEND_ISFRIEND = 1,
            [Description("パーティリーダーはリーダースキルを所持しているか?")]
            PARTY_LEADERSKILLAVAIL = 2,
            [Description("選択されたフレンドはフレンドスキルを所持しているか?")]
            FRIEND_LEADERSKILLAVAIL = 3,
            [Description("パーティのリーダーが設定されているか")]
            PARTY_LEADEREXIST = 4,
            [Description("バトルで現在ターゲットしているユニットに対してコマンドが有効であるか判定します。")]
            TARGET_COMMANDVALID = 5,
            [Description("クエストから欠片を入手できるか?")]
            QUEST_DROPSKAKERA = 6,
            [Description("クエスト最初のターンか?")]
            QUEST_FIRSTTURN = 7,
            [Description("サポートしてくれた人にフレンドリクエスト送るべき?")]
            QUEST_NEEDFRIENDREQUEST = 8,
            [Description("プレイヤーのレベルが変化した")]
            PLAYER_LEVELCHANGED = 9,
            [Description("ニューゲームが選択された")]
            NEWGAME = 10,
            [Description("再開用BTLIDが設定されている")]
            BTLIDSET = 11,
            [Description("マルチプレイ中?")]
            QUEST_ISMULTIPLAY = 12,
            [Description("アリーナプレイ中?")]
            QUEST_ISARENA = 13,
            [Description("勝てばアリーナでランクアップする。")]
            ARENA_RANKUP = 14,
            QUEST_HASREWARD = 15,
            TERMSOFUSE_AGREED = 0x10,
            [Description("フレンドは設定されているか？")]
            FRIEND_VALID = 0x11,
            QUEST_ENDSILENT = 0x12,
            [Description("サポートユニットの契約金額が不足しているか？")]
            IS_NOT_ENOUGH_SUPPORT_COST = 0x13,
            [Description("マルチはアンロックされているか？")]
            MULTI_PLAY_IS_UNLOCKED = 20,
            [Description("オートバトル可能か？")]
            QUEST_IS_ENABLE_AUTOBATTLE = 0x15,
            [Description("オートバトル中か？")]
            QUEST_IS_AUTOBATTLE = 0x16,
            DEBUGBUILD = 0x17,
            [Description("Begginer?")]
            IS_BEGINNER = 0x18,
            [Description("チュートリアルを最後まで行ったか？")]
            IS_END_TUTORIAL = 0x19,
            [Description("ユニットを所持しているか？")]
            IS_GET_UNIT = 0x1a,
            [Description("対戦はアンロック済みか？")]
            VERSUS_UNLOCK = 0x1b,
            [Description("レビューポップアップを表示するか？")]
            QUEST_IS_SHOW_REVIEW = 0x1c,
            [Description("クエストは初回クリアか？")]
            QUEST_IS_FIRST_CLEAR = 0x1d,
            [Description("クエストタイプがGPSクエストか？")]
            QUEST_IS_GPS = 30,
            [Description("選択中のチャプターがGPSヘッダか？")]
            QUEST_IS_GPSCHAPTER_HEAD = 0x1f,
            [Description("選択中のチャプターがGPSクエストか？")]
            QUEST_IS_GPSCHAPTER_QUEST = 0x20,
            [Description("選択中のチャプターに有効なGPSクエストがあるか？")]
            QUEST_IS_GPSCHAPTER_QUEST_VALID = 0x21,
            [Description("GPSギフトが有効かどうか")]
            VALID_GPSGIFT = 0x22,
            [Description("シーン遷移中かどうか")]
            SCENE_CHANGING = 0x23,
            [Description("終了デモ再生条件を満たしているか？")]
            QUEST_IS_PLAY_LAST_DEMO = 0x24,
            [Description("ランキングクエストか？")]
            QUEST_IS_RANKINGQUEST = 0x25,
            [Description("ランキングクエストでハイスコア更新したか？")]
            QUEST_IS_RANKINGQUEST_NEWSCORE = 0x26,
            [Description("ランキングクエストで参加報酬をうけとれたか？")]
            QUEST_IS_RANKINGQUEST_JOIN_REWARD = 0x27,
            [Description("ランキングクエストのリザルトで有効なランクだったか？（集計中は無効なランクになる）")]
            QUEST_IS_RANKINGQUEST_RESULT_VALIDRANK = 40,
            [Description("前回起動したバージョンと違うか？")]
            DIFFERENT_VERSION = 0x29,
            [Description("選択しているクエストはマルチエリアクエストか？")]
            SELECTQUEST_IS_MULTIGPS = 0x2a,
            [Description("LINEから招待されたクエストはマルチエリアクエストか？")]
            LINEQUEST_IS_MULTIGPS = 0x2b,
            [Description("エリアクエストは有効なスケジュールか？")]
            GPSQUEST_IS_VALID = 0x2c,
            [Description("マルチエリアクエストは有効なスケジュールか？")]
            MULTI_GPSQUEST_IS_VALID = 0x2d,
            [Description("複数チームクエストか？")]
            QUEST_IS_ORDEAL = 0x2e,
            [Description("クエストでユニットを獲得したか？")]
            QUEST_IS_GET_UNIT = 0x2f,
            [Description("初回購入キャンペーンが有効かどうか")]
            VALID_FIRST_CHARGE_CAMPAIGN = 0x30,
            [Description("ゲリラショップが始まったか？")]
            GUERRILLASHOP_IS_STARTED = 0x31,
            [Description("バトルで獲得した真理念装はギフトへ送られた？")]
            QUEST_END_CARD_IS_SENDMAIL = 50,
            [Description("ミッションがあるタワークエストか？")]
            TOWER_QUEST_HAVE_MISSION = 0x33,
            [Description("初心者の館への遷移ポップアップを出すべきか？")]
            SHOW_BEGINNER_TOP_NOTIFY = 0x34
        }

        private class Description : Attribute
        {
            public Description(string text)
            {
                base..ctor();
                return;
            }

            public Description(string text, string detail)
            {
                base..ctor();
                return;
            }
        }
    }
}

