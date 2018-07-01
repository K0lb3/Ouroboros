// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchRankingTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class RankMatchRankingTab : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private GameObject PlayerUnit;
    [SerializeField]
    private ListItemEvents ListItem;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.PlayerUnit, (Object) null) && Object.op_Inequality((Object) this.PlayerGO, (Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        DataSource.Bind<PlayerData>(this.PlayerGO, player);
        DataSource.Bind<UnitData>(this.PlayerUnit, player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID));
        GameParameter.UpdateAll(this.PlayerUnit);
      }
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
        return;
      this.ClearItems();
      ((Component) this.ListItem).get_gameObject().SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchRanking(new Network.ResponseCallback(this.ResponseCallback)), false);
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
        WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response> jsonBodyResponse = (WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          if (jsonBodyResponse.body.rankings == null)
            return;
          for (int index = 0; index < jsonBodyResponse.body.rankings.Length; ++index)
          {
            ReqRankMatchRanking.ResponceRanking ranking = jsonBodyResponse.body.rankings[index];
            ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ListItem);
            DataSource.Bind<ReqRankMatchRanking.ResponceRanking>(((Component) listItemEvents).get_gameObject(), ranking);
            FriendData data = new FriendData();
            data.Deserialize(ranking.enemy);
            DataSource.Bind<FriendData>(((Component) listItemEvents).get_gameObject(), data);
            DataSource.Bind<UnitData>(((Component) listItemEvents).get_gameObject(), data.Unit);
            this.AddItem(listItemEvents);
            ((Component) listItemEvents).get_transform().SetParent(((Component) this).get_transform(), false);
            ((Component) listItemEvents).get_gameObject().SetActive(true);
          }
          Network.RemoveAPI();
        }
      }
    }
  }
}
