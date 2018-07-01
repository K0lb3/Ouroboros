// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestRankingParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "指定ユーザのパーティ取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "指定ユーザのパーティ取得(成功)", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "指定ユーザのパーティ取得(失敗)", FlowNode.PinTypes.Output, 102)]
  [FlowNode.NodeType("System/ReqQuestRankingParty")]
  public class FlowNode_ReqQuestRankingParty : FlowNode_Network
  {
    public const int INPUT_RANKING_PARTY = 100;
    public const int OUTPUT_RANKING_PARTY_SUCCESS = 101;
    public const int OUTPUT_RANKING_PARTY_FAILED = 102;

    public override void OnActivate(int pinID)
    {
      ReqQuestRankingPartyData dataOfClass = DataSource.FindDataOfClass<ReqQuestRankingPartyData>(((Component) this).get_gameObject(), (ReqQuestRankingPartyData) null);
      if (dataOfClass == null || pinID != 100)
        return;
      this.ExecRequest((WebAPI) new FlowNode_ReqQuestRankingParty.API_ReqQuestRankingParty(dataOfClass.m_ScheduleID, dataOfClass.m_RankingType, dataOfClass.m_QuestID, dataOfClass.m_TargetUID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.RankingQuestMaintenance:
          case Network.EErrCode.RankingQuest_OutOfPeriod:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(102);
            break;
          case Network.EErrCode.RankingQuest_NotNewScore:
          case Network.EErrCode.RankingQuest_AlreadyEntry:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRankingParty.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqQuestRankingParty.Json>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          GlobalVars.UserSelectionPartyData selectionPartyData = new GlobalVars.UserSelectionPartyData();
          FlowNode_ReqQuestRankingParty.Json body = jsonObject.body;
          if (body.party != null)
          {
            UnitData[] unitDataArray = new UnitData[((IEnumerable<Json_Unit>) body.party).Count<Json_Unit>((Func<Json_Unit, bool>) (j => j != null))];
            for (int index = 0; index < unitDataArray.Length; ++index)
            {
              if (body.party[index] == null || string.IsNullOrEmpty(body.party[index].iname))
              {
                unitDataArray[index] = (UnitData) null;
              }
              else
              {
                unitDataArray[index] = new UnitData();
                unitDataArray[index].Deserialize(body.party[index]);
              }
            }
            selectionPartyData.unitData = unitDataArray;
          }
          if (body.items != null)
          {
            ItemData[] itemDataArray = new ItemData[body.items.Length];
            for (int index = 0; index < itemDataArray.Length; ++index)
            {
              itemDataArray[index] = new ItemData();
              itemDataArray[index].Setup(0L, body.items[index].iname, body.items[index].num);
            }
            selectionPartyData.usedItems = itemDataArray;
          }
          if (body.help != null)
          {
            SupportData supportData = new SupportData();
            supportData.Deserialize(body.help);
            selectionPartyData.supportData = supportData;
          }
          selectionPartyData.achievements = new int[0];
          GlobalVars.UserSelectionPartyDataInfo = selectionPartyData;
          this.Success();
        }
      }
    }

    public class API_ReqQuestRankingParty : WebAPI
    {
      public API_ReqQuestRankingParty(int schedule_id, RankingQuestType type, string quest_id, string uid, Network.ResponseCallback response)
      {
        this.name = "quest/ranking/party";
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        FlowNode_ReqQuestRankingParty.API_ReqQuestRankingParty.AppendKeyValue(stringBuilder, nameof (schedule_id), schedule_id);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRankingParty.API_ReqQuestRankingParty.AppendKeyValue(stringBuilder, nameof (type), (int) type);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRankingParty.API_ReqQuestRankingParty.AppendKeyValue(stringBuilder, "iname", quest_id);
        stringBuilder.Append(",");
        FlowNode_ReqQuestRankingParty.API_ReqQuestRankingParty.AppendKeyValue(stringBuilder, "target_uid", uid);
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
    public class Json_LightItem
    {
      public string iname;
      public int num;
    }

    [Serializable]
    public class Json_Party
    {
      public string uid;
      public string fuid;
      public string name;
      public string award;
      public int lv;
      public long lastlogin;
      public bool is_multi_push;
      public string multi_comment;
      public string created_at;
      public Json_Unit[] units;
      public Json_Support help;
      public FlowNode_ReqQuestRankingParty.Json_LightItem[] items;
    }

    [Serializable]
    public class Json
    {
      public Json_Unit[] party;
      public Json_Support help;
      public FlowNode_ReqQuestRankingParty.Json_LightItem[] items;
    }
  }
}
