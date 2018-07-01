// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(300, "ランキング取得(成功)", FlowNode.PinTypes.Output, 300)]
  [FlowNode.NodeType("System/ReqQuestRanking")]
  [FlowNode.Pin(50, "ランキング取得(TOP + 自身)", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(100, "ランキング取得(TOP)", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "ランキング取得(自身)", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(301, "ランキング取得(失敗)", FlowNode.PinTypes.Output, 301)]
  public class FlowNode_ReqQuestRanking : FlowNode_Network
  {
    public const int INPUT_REQUEST_RANKING_TOP_OWN = 50;
    public const int INPUT_REQUEST_RANKING_TOP = 100;
    public const int INPUT_REQUEST_RANKING_OWN = 200;
    public const int OUTPUT_REQUEST_RANKING_SUCCESS = 300;
    public const int OUTPUT_REQUEST_RANKING_FAILED = 301;
    [SerializeField]
    private RankingQuestRankWindow m_TargetWindow;

    public override void OnActivate(int pinID)
    {
      RankingQuestParam rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      if (GlobalVars.SelectedRankingQuestParam == null)
        return;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(rankingQuestParam.iname);
      switch (pinID)
      {
        case 50:
          this.ExecRequest((WebAPI) new FlowNode_ReqQuestRanking.API_ReqQuestRanking(rankingQuestParam.schedule_id, rankingQuestParam.type, quest.iname, 0, false, new Network.ResponseCallback(this.ResponseCallback_RequestRankingTopOwn)));
          break;
        case 100:
          this.ExecRequest((WebAPI) new FlowNode_ReqQuestRanking.API_ReqQuestRanking(rankingQuestParam.schedule_id, rankingQuestParam.type, quest.iname, 0, false, new Network.ResponseCallback(this.ResponseCallback_RequestRankingTop)));
          break;
        case 200:
          this.ExecRequest((WebAPI) new FlowNode_ReqQuestRanking.API_ReqQuestRanking(rankingQuestParam.schedule_id, rankingQuestParam.type, quest.iname, 0, true, new Network.ResponseCallback(this.ResponseCallback_RequestRankingOwn)));
          break;
      }
    }

    private void Success()
    {
      this.ActivateOutputLinks(300);
    }

    private void ResponseCallback_RequestRankingTopOwn(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.RankingQuestMaintenance:
            break;
          case Network.EErrCode.RankingQuest_NotNewScore:
          case Network.EErrCode.RankingQuest_AlreadyEntry:
            this.OnFailed();
            return;
          case Network.EErrCode.RankingQuest_OutOfPeriod:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(301);
            return;
          default:
            this.OnRetry();
            return;
        }
      }
      DebugUtility.Log(www.text);
      WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.m_TargetWindow.SetData(RankingQuestUserData.CreateRankingUserDataFromJson(jsonObject.body.ranking, GlobalVars.SelectedRankingQuestParam.type), RankingQuestUserData.CreateRankingUserDataFromJson(jsonObject.body.my_info, GlobalVars.SelectedRankingQuestParam.type));
        this.Success();
      }
    }

    private void ResponseCallback_RequestRankingTop(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.RankingQuestMaintenance:
            break;
          case Network.EErrCode.RankingQuest_NotNewScore:
          case Network.EErrCode.RankingQuest_AlreadyEntry:
            this.OnFailed();
            return;
          case Network.EErrCode.RankingQuest_OutOfPeriod:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(301);
            return;
          default:
            this.OnRetry();
            return;
        }
      }
      DebugUtility.Log(www.text);
      WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.m_TargetWindow.SetData(RankingQuestUserData.CreateRankingUserDataFromJson(jsonObject.body.ranking, GlobalVars.SelectedRankingQuestParam.type));
        this.Success();
      }
    }

    private void ResponseCallback_RequestRankingOwn(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.RankingQuestMaintenance:
            break;
          case Network.EErrCode.RankingQuest_NotNewScore:
          case Network.EErrCode.RankingQuest_AlreadyEntry:
            this.OnFailed();
            return;
          case Network.EErrCode.RankingQuest_OutOfPeriod:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(301);
            return;
          default:
            this.OnRetry();
            return;
        }
      }
      WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRanking.Json>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.m_TargetWindow.SetData(RankingQuestUserData.CreateRankingUserDataFromJson(jsonObject.body.ranking, GlobalVars.SelectedRankingQuestParam.type));
        this.Success();
      }
    }

    public override void OnSuccess(WWWResult www)
    {
    }

    public class API_ReqQuestRanking : WebAPI
    {
      public API_ReqQuestRanking(int schedule_id, RankingQuestType type, string quest_id, int rank, bool isOwn, Network.ResponseCallback response)
      {
        this.name = "quest/ranking";
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        FlowNode_ReqQuestRanking.API_ReqQuestRanking.AppendKeyValue(stringBuilder, nameof (schedule_id), schedule_id);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRanking.API_ReqQuestRanking.AppendKeyValue(stringBuilder, nameof (type), (int) type);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRanking.API_ReqQuestRanking.AppendKeyValue(stringBuilder, "iname", quest_id);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRanking.API_ReqQuestRanking.AppendKeyValue(stringBuilder, nameof (rank), rank);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRanking.API_ReqQuestRanking.AppendKeyValue(stringBuilder, "is_near", !isOwn ? 0 : 1);
        this.body = WebAPI.GetRequestString(stringBuilder.ToString());
        this.callback = response;
      }

      private static void AppendKeyValue(StringBuilder sb, string key, int value)
      {
        sb.Append("\"");
        sb.Append(key);
        sb.Append("\":");
        sb.Append(value);
      }

      private static void AppendKeyValue(StringBuilder sb, string key, string value)
      {
        sb.Append("\"");
        sb.Append(key);
        sb.Append("\":\"");
        sb.Append(value);
        sb.Append("\"");
      }
    }

    [Serializable]
    public class Json_UnitDataLight
    {
      public string unit_iname;
      public int unit_lv;
      public int job_lv;
    }

    [Serializable]
    public class Json_OwnRankingData
    {
      public FlowNode_ReqQuestRanking.Json_UnitDataLight unit;
      public int rank;
      public int main_score;
      public int sub_score;
    }

    [Serializable]
    public class Json_OthersRankingData
    {
      public string uid;
      public string name;
      public int rank;
      public string unit_iname;
      public int unit_lv;
      public int job_lv;
      public int main_score;
      public int sub_score;
    }

    [Serializable]
    public class Json
    {
      public FlowNode_ReqQuestRanking.Json_OwnRankingData my_info;
      public FlowNode_ReqQuestRanking.Json_OthersRankingData[] ranking;
    }
  }
}
