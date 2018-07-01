// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankMatchHistory : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private ListItemEvents ListItem;
    [SerializeField]
    [Space(10f)]
    private Text LastBattleDate;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.PlayerGO, (Object) null))
        DataSource.Bind<PlayerData>(this.PlayerGO, MonoSingleton<GameManager>.Instance.Player);
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
        return;
      this.ClearItems();
      ((Component) this.ListItem).get_gameObject().SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchHistory(new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            break;
          default:
            if (errCode != Network.EErrCode.OutOfDateQuest)
            {
              if (errCode == Network.EErrCode.MultiVersionMismatch || errCode == Network.EErrCode.VS_Version)
              {
                Network.RemoveAPI();
                Network.ResetError();
                ((Behaviour) this).set_enabled(false);
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response> jsonBodyResponse = (WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          if (jsonBodyResponse.body.histories == null || jsonBodyResponse.body.histories.list == null)
            return;
          long unixtime = 0;
          for (int index = 0; index < jsonBodyResponse.body.histories.list.Length; ++index)
          {
            ReqRankMatchHistory.ResponceHistoryList data1 = jsonBodyResponse.body.histories.list[index];
            ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ListItem);
            DataSource.Bind<ReqRankMatchHistory.ResponceHistoryList>(((Component) listItemEvents).get_gameObject(), data1);
            FriendData data2 = new FriendData();
            data2.Deserialize(data1.enemy);
            DataSource.Bind<FriendData>(((Component) listItemEvents).get_gameObject(), data2);
            DataSource.Bind<UnitData>(((Component) listItemEvents).get_gameObject(), data2.Unit);
            this.AddItem(listItemEvents);
            ((Component) listItemEvents).get_transform().SetParent(((Component) this).get_transform(), false);
            ((Component) listItemEvents).get_gameObject().SetActive(true);
            if (unixtime < data1.time_end)
              unixtime = data1.time_end;
          }
          if (Object.op_Inequality((Object) this.LastBattleDate, (Object) null) && unixtime > 0L)
            this.LastBattleDate.set_text(TimeManager.FromUnixTime(unixtime).ToString("MM/dd HH:mm"));
          Network.RemoveAPI();
        }
      }
    }
  }
}
