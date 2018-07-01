// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Condition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("SRPG/条件判定", 32741)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_Condition : FlowNode
  {
    public FlowNode_Condition.Conditions Condition;

    private SupportData GetSupportData()
    {
      return DataSource.FindDataOfClass<SupportData>(((Component) this).get_gameObject(), (SupportData) null) ?? (SupportData) GlobalVars.SelectedSupport;
    }

    private SkillParam GetLeaderSkill(PartyData party)
    {
      return MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(party.GetUnitUniqueID(party.LeaderIndex))?.LeaderSkill.SkillParam;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      this.SetResult(this.Test());
    }

    private void SetResult(bool result)
    {
      if (result)
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }

    public override string[] GetInfoLines()
    {
      return new string[1]
      {
        "Condition is " + this.Condition.ToString()
      };
    }

    private bool Test()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (this.Condition)
      {
        case FlowNode_Condition.Conditions.QUEST_HASENOUGHSTAMINA:
          QuestParam quest1 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest1 != null)
            return MonoSingleton<GameManager>.Instance.Player.Stamina >= quest1.RequiredApWithPlayerLv(player.Lv, true);
          break;
        case FlowNode_Condition.Conditions.FRIEND_ISFRIEND:
          SupportData supportData1;
          if ((supportData1 = this.GetSupportData()) != null)
            return supportData1.IsFriend();
          return false;
        case FlowNode_Condition.Conditions.PARTY_LEADERSKILLAVAIL:
          PartyData dataOfClass1;
          if ((dataOfClass1 = DataSource.FindDataOfClass<PartyData>(((Component) this).get_gameObject(), (PartyData) null)) != null)
            return this.GetLeaderSkill(dataOfClass1) != null;
          break;
        case FlowNode_Condition.Conditions.FRIEND_LEADERSKILLAVAIL:
          SupportData supportData2;
          if ((supportData2 = this.GetSupportData()) != null)
            return supportData2.LeaderSkill != null;
          break;
        case FlowNode_Condition.Conditions.PARTY_LEADEREXIST:
          for (int index = 0; index < player.Partys.Count; ++index)
          {
            if (player.Partys[index].GetUnitUniqueID(player.Partys[index].LeaderIndex) == 0L)
              return false;
          }
          return true;
        case FlowNode_Condition.Conditions.TARGET_COMMANDVALID:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.UIParam_TargetValid;
          break;
        case FlowNode_Condition.Conditions.QUEST_DROPSKAKERA:
          QuestParam dataOfClass2;
          if ((dataOfClass2 = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null)) != null && !UnityEngine.Object.op_Equality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
            return QuestDropParam.Instance.GetHardDropPiece(dataOfClass2.iname, GlobalVars.GetDropTableGeneratedDateTime()) != null;
          break;
        case FlowNode_Condition.Conditions.QUEST_FIRSTTURN:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.UnitStartCount <= 1;
          break;
        case FlowNode_Condition.Conditions.QUEST_NEEDFRIENDREQUEST:
          SupportData support = (SupportData) GlobalVars.SelectedSupport;
          if (support == null || support.IsFriend())
            return false;
          FriendData friendData = player.Friends.Find((Predicate<FriendData>) (f => f.FUID == support.FUID));
          if (friendData == null)
            return true;
          return friendData.State == FriendStates.Friend || friendData.State != FriendStates.Follow ? false : false;
        case FlowNode_Condition.Conditions.PLAYER_LEVELCHANGED:
          return (bool) GlobalVars.PlayerLevelChanged;
        case FlowNode_Condition.Conditions.NEWGAME:
          return GameUtility.Config_NewGame.Value;
        case FlowNode_Condition.Conditions.BTLIDSET:
          return (long) GlobalVars.BtlID != 0L;
        case FlowNode_Condition.Conditions.QUEST_ISMULTIPLAY:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsPlayingMultiQuest;
          break;
        case FlowNode_Condition.Conditions.QUEST_ISARENA:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsPlayingArenaQuest;
          break;
        case FlowNode_Condition.Conditions.ARENA_RANKUP:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsArenaRankupInfo();
          break;
        case FlowNode_Condition.Conditions.QUEST_HASREWARD:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return !SceneBattle.Instance.Battle.GetQuestRecord().IsZero;
          break;
        case FlowNode_Condition.Conditions.TERMSOFUSE_AGREED:
          return MonoSingleton<GameManager>.Instance.IsAgreeTermsOfUse();
        case FlowNode_Condition.Conditions.FRIEND_VALID:
          return this.GetSupportData() != null;
        case FlowNode_Condition.Conditions.QUEST_ENDSILENT:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.CurrentQuest.Silent;
          break;
        case FlowNode_Condition.Conditions.IS_NOT_ENOUGH_SUPPORT_COST:
          SupportData supportData3 = this.GetSupportData();
          if (supportData3 != null)
          {
            int gold = player.Gold;
            if (supportData3.GetCost() > gold)
              return true;
            break;
          }
          break;
        case FlowNode_Condition.Conditions.MULTI_PLAY_IS_UNLOCKED:
          return MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.MultiPlay);
        case FlowNode_Condition.Conditions.QUEST_IS_ENABLE_AUTOBATTLE:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
          {
            QuestParam quest2 = SceneBattle.Instance.Battle.GetQuest();
            if (quest2 != null)
              return quest2.CheckAllowedAutoBattle();
            return false;
          }
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_AUTOBATTLE:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.Battle.IsAutoBattle;
          break;
        case FlowNode_Condition.Conditions.DEBUGBUILD:
          return GameUtility.IsDebugBuild;
        case FlowNode_Condition.Conditions.IS_BEGINNER:
          return MonoSingleton<GameManager>.Instance.Player.IsBeginner();
        case FlowNode_Condition.Conditions.IS_END_TUTORIAL:
          return (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != 0L;
        case FlowNode_Condition.Conditions.IS_GET_UNIT:
          return MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null).iname) != null;
        case FlowNode_Condition.Conditions.VERSUS_UNLOCK:
          return MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.MultiVS);
        case FlowNode_Condition.Conditions.QUEST_IS_SHOW_REVIEW:
          if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (quest2 != null)
              return quest2.ShowReviewPopup;
            break;
          }
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_FIRST_CLEAR:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsFirstWin;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_GPS:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.CurrentQuest != null && SceneBattle.Instance.CurrentQuest.type == QuestTypes.Gps)
            return true;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_GPSCHAPTER_HEAD:
          if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
            if (area != null && area.IsGpsQuest())
              return area.children.Count > 0;
            break;
          }
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_GPSCHAPTER_QUEST:
          if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
            if (area != null)
              return area.IsGpsQuest();
            break;
          }
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_GPSCHAPTER_QUEST_VALID:
          if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
            if (area != null)
              return area.HasGpsQuest();
            break;
          }
          break;
        case FlowNode_Condition.Conditions.VALID_GPSGIFT:
          return MonoSingleton<GameManager>.Instance.Player.ValidGpsGift;
        case FlowNode_Condition.Conditions.SCENE_CHANGING:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
            return HomeWindow.Current.IsSceneChanging;
          return false;
        case FlowNode_Condition.Conditions.QUEST_IS_PLAY_LAST_DEMO:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsPlayLastDemo;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_RANKINGQUEST:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.Battle.IsRankingQuest;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_RANKINGQUEST_NEWSCORE:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsRankingQuestNewScore;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_RANKINGQUEST_JOIN_REWARD:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsRankingQuestJoinReward;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_RANKINGQUEST_RESULT_VALIDRANK:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.ValidateRankingQuestRank;
          break;
        case FlowNode_Condition.Conditions.DIFFERENT_VERSION:
          string version = MyApplicationPlugin.get_version();
          string strB = PlayerPrefsUtility.GetString(PlayerPrefsUtility.AWAKE_VERSION, string.Empty);
          PlayerPrefsUtility.SetString(PlayerPrefsUtility.AWAKE_VERSION, version, true);
          return string.Compare(version, strB) != 0;
        case FlowNode_Condition.Conditions.SELECTQUEST_IS_MULTIGPS:
          if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (quest2 != null)
              return quest2.IsMultiAreaQuest;
            break;
          }
          break;
        case FlowNode_Condition.Conditions.LINEQUEST_IS_MULTIGPS:
          if (!string.IsNullOrEmpty(FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.iname))
          {
            QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.iname);
            if (quest2 != null)
              return quest2.IsMultiAreaQuest;
            break;
          }
          break;
        case FlowNode_Condition.Conditions.GPSQUEST_IS_VALID:
          return MonoSingleton<GameManager>.Instance.IsValidAreaQuest();
        case FlowNode_Condition.Conditions.MULTI_GPSQUEST_IS_VALID:
          return MonoSingleton<GameManager>.Instance.IsValidMultiAreaQuest();
        case FlowNode_Condition.Conditions.QUEST_IS_ORDEAL:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance))
            return SceneBattle.Instance.IsOrdealQuest;
          break;
        case FlowNode_Condition.Conditions.QUEST_IS_GET_UNIT:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance))
            return SceneBattle.Instance.IsGetFirstClearItem;
          break;
        case FlowNode_Condition.Conditions.VALID_FIRST_CHARGE_CAMPAIGN:
          if ((byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus != (byte) 1)
            return (byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus == (byte) 2;
          return true;
        case FlowNode_Condition.Conditions.GUERRILLASHOP_IS_STARTED:
          return MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopStarted;
        case FlowNode_Condition.Conditions.QUEST_END_CARD_IS_SENDMAIL:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
            return SceneBattle.Instance.IsCardSendMail;
          break;
        case FlowNode_Condition.Conditions.TOWER_QUEST_HAVE_MISSION:
          QuestParam quest3 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest3 != null && quest3.HasMission())
            return true;
          break;
        case FlowNode_Condition.Conditions.SHOW_BEGINNER_TOP_NOTIFY:
          if (player.IsBeginner() && PlayerPrefsUtility.GetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED, 0) == 0)
            return true;
          break;
      }
      return false;
    }

    private class Description : Attribute
    {
      public Description(string text)
      {
      }

      public Description(string text, string detail)
      {
      }
    }

    public enum Conditions
    {
      [FlowNode_Condition.Description("スタミナが足りているか", "クエストを開始するのに必要なスタミナがあるかどうか判定します。")] QUEST_HASENOUGHSTAMINA,
      [FlowNode_Condition.Description("フレンドであるか")] FRIEND_ISFRIEND,
      [FlowNode_Condition.Description("パーティリーダーはリーダースキルを所持しているか?")] PARTY_LEADERSKILLAVAIL,
      [FlowNode_Condition.Description("選択されたフレンドはフレンドスキルを所持しているか?")] FRIEND_LEADERSKILLAVAIL,
      [FlowNode_Condition.Description("パーティのリーダーが設定されているか")] PARTY_LEADEREXIST,
      [FlowNode_Condition.Description("バトルで現在ターゲットしているユニットに対してコマンドが有効であるか判定します。")] TARGET_COMMANDVALID,
      [FlowNode_Condition.Description("クエストから欠片を入手できるか?")] QUEST_DROPSKAKERA,
      [FlowNode_Condition.Description("クエスト最初のターンか?")] QUEST_FIRSTTURN,
      [FlowNode_Condition.Description("サポートしてくれた人にフレンドリクエスト送るべき?")] QUEST_NEEDFRIENDREQUEST,
      [FlowNode_Condition.Description("プレイヤーのレベルが変化した")] PLAYER_LEVELCHANGED,
      [FlowNode_Condition.Description("ニューゲームが選択された")] NEWGAME,
      [FlowNode_Condition.Description("再開用BTLIDが設定されている")] BTLIDSET,
      [FlowNode_Condition.Description("マルチプレイ中?")] QUEST_ISMULTIPLAY,
      [FlowNode_Condition.Description("アリーナプレイ中?")] QUEST_ISARENA,
      [FlowNode_Condition.Description("勝てばアリーナでランクアップする。")] ARENA_RANKUP,
      QUEST_HASREWARD,
      TERMSOFUSE_AGREED,
      [FlowNode_Condition.Description("フレンドは設定されているか？")] FRIEND_VALID,
      QUEST_ENDSILENT,
      [FlowNode_Condition.Description("サポートユニットの契約金額が不足しているか？")] IS_NOT_ENOUGH_SUPPORT_COST,
      [FlowNode_Condition.Description("マルチはアンロックされているか？")] MULTI_PLAY_IS_UNLOCKED,
      [FlowNode_Condition.Description("オートバトル可能か？")] QUEST_IS_ENABLE_AUTOBATTLE,
      [FlowNode_Condition.Description("オートバトル中か？")] QUEST_IS_AUTOBATTLE,
      DEBUGBUILD,
      [FlowNode_Condition.Description("Begginer?")] IS_BEGINNER,
      [FlowNode_Condition.Description("チュートリアルを最後まで行ったか？")] IS_END_TUTORIAL,
      [FlowNode_Condition.Description("ユニットを所持しているか？")] IS_GET_UNIT,
      [FlowNode_Condition.Description("対戦はアンロック済みか？")] VERSUS_UNLOCK,
      [FlowNode_Condition.Description("レビューポップアップを表示するか？")] QUEST_IS_SHOW_REVIEW,
      [FlowNode_Condition.Description("クエストは初回クリアか？")] QUEST_IS_FIRST_CLEAR,
      [FlowNode_Condition.Description("クエストタイプがGPSクエストか？")] QUEST_IS_GPS,
      [FlowNode_Condition.Description("選択中のチャプターがGPSヘッダか？")] QUEST_IS_GPSCHAPTER_HEAD,
      [FlowNode_Condition.Description("選択中のチャプターがGPSクエストか？")] QUEST_IS_GPSCHAPTER_QUEST,
      [FlowNode_Condition.Description("選択中のチャプターに有効なGPSクエストがあるか？")] QUEST_IS_GPSCHAPTER_QUEST_VALID,
      [FlowNode_Condition.Description("GPSギフトが有効かどうか")] VALID_GPSGIFT,
      [FlowNode_Condition.Description("シーン遷移中かどうか")] SCENE_CHANGING,
      [FlowNode_Condition.Description("終了デモ再生条件を満たしているか？")] QUEST_IS_PLAY_LAST_DEMO,
      [FlowNode_Condition.Description("ランキングクエストか？")] QUEST_IS_RANKINGQUEST,
      [FlowNode_Condition.Description("ランキングクエストでハイスコア更新したか？")] QUEST_IS_RANKINGQUEST_NEWSCORE,
      [FlowNode_Condition.Description("ランキングクエストで参加報酬をうけとれたか？")] QUEST_IS_RANKINGQUEST_JOIN_REWARD,
      [FlowNode_Condition.Description("ランキングクエストのリザルトで有効なランクだったか？（集計中は無効なランクになる）")] QUEST_IS_RANKINGQUEST_RESULT_VALIDRANK,
      [FlowNode_Condition.Description("前回起動したバージョンと違うか？")] DIFFERENT_VERSION,
      [FlowNode_Condition.Description("選択しているクエストはマルチエリアクエストか？")] SELECTQUEST_IS_MULTIGPS,
      [FlowNode_Condition.Description("LINEから招待されたクエストはマルチエリアクエストか？")] LINEQUEST_IS_MULTIGPS,
      [FlowNode_Condition.Description("エリアクエストは有効なスケジュールか？")] GPSQUEST_IS_VALID,
      [FlowNode_Condition.Description("マルチエリアクエストは有効なスケジュールか？")] MULTI_GPSQUEST_IS_VALID,
      [FlowNode_Condition.Description("複数チームクエストか？")] QUEST_IS_ORDEAL,
      [FlowNode_Condition.Description("クエストでユニットを獲得したか？")] QUEST_IS_GET_UNIT,
      [FlowNode_Condition.Description("初回購入キャンペーンが有効かどうか")] VALID_FIRST_CHARGE_CAMPAIGN,
      [FlowNode_Condition.Description("ゲリラショップが始まったか？")] GUERRILLASHOP_IS_STARTED,
      [FlowNode_Condition.Description("バトルで獲得した真理念装はギフトへ送られた？")] QUEST_END_CARD_IS_SENDMAIL,
      [FlowNode_Condition.Description("ミッションがあるタワークエストか？")] TOWER_QUEST_HAVE_MISSION,
      [FlowNode_Condition.Description("初心者の館への遷移ポップアップを出すべきか？")] SHOW_BEGINNER_TOP_NOTIFY,
    }
  }
}
