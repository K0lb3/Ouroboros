// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqPresentList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(300, "一括送付", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(220, "プレゼント一括受け取り失敗", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(210, "プレゼント一括受け取り完了", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(100, "一覧取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(410, "プレゼント贈ってくれた人完了", FlowNode.PinTypes.Output, 410)]
  [FlowNode.Pin(310, "プレゼント一括送付完了", FlowNode.PinTypes.Output, 310)]
  [FlowNode.Pin(420, "プレゼント贈ってくれた人失敗", FlowNode.PinTypes.Output, 420)]
  [FlowNode.Pin(200, "一括受け取り", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(320, "プレゼント一括送付失敗", FlowNode.PinTypes.Output, 320)]
  [FlowNode.Pin(400, "贈ってくれた人", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(120, "プレゼント一覧取得失敗", FlowNode.PinTypes.Output, 120)]
  [FlowNode.NodeType("System/WebApi/PresentList", 32741)]
  [FlowNode.Pin(110, "プレゼント一覧取得完了", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqPresentList : FlowNode_Network
  {
    private FlowNode_ReqPresentList.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        switch (pinID)
        {
          case 100:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentList(this);
            break;
          case 200:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentListExec(this);
            break;
          case 300:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentListSend(this);
            break;
          case 400:
            SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (currentValue != null)
            {
              new FlowNode_ReqPresentList.Api_PresentListGave(this, currentValue.GetGameObject("item")).Start();
              break;
            }
            break;
        }
        if (this.m_Api == null)
          return;
        this.m_Api.Start();
        ((Behaviour) this).set_enabled(true);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqPresentList.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqPresentList m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqPresentList node)
      {
        this.m_Node = node;
      }

      public virtual string url
      {
        get
        {
          return string.Empty;
        }
      }

      public virtual string req
      {
        get
        {
          return (string) null;
        }
      }

      public virtual void Success()
      {
      }

      public virtual void Failed()
      {
      }

      public virtual void Complete(WWWResult www)
      {
      }

      public virtual void Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
          this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
        else
          this.Failed();
      }
    }

    public class Api_PresentList : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentList(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "presentlist";
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(120);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }

    public class Api_PresentListExec : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentListExec(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "presentlist/exec";
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(210);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(220);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      private RewardData ReceiveDataToRewardData(FlowNode_ReqPresentList.Api_PresentListExec.JsonItem receiveData)
      {
        FriendPresentItemParam presentItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(receiveData.iname);
        if (presentItemParam == null)
          return (RewardData) null;
        RewardData rewardData = new RewardData();
        rewardData.Exp = 0;
        rewardData.Coin = 0;
        rewardData.Gold = 0;
        rewardData.Stamina = 0;
        rewardData.MultiCoin = 0;
        rewardData.KakeraCoin = 0;
        if (presentItemParam.IsItem())
          rewardData.AddReward(presentItemParam.item, presentItemParam.num * receiveData.num);
        else
          rewardData.Gold = presentItemParam.zeny * receiveData.num;
        return rewardData;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListExec.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListExec.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          bool flag = false;
          if (jsonObject.body != null)
          {
            if (jsonObject.body.player != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            if (jsonObject.body.items != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.presents != null)
            {
              RewardData rewardData = new RewardData();
              for (int index = 0; index < jsonObject.body.presents.Length; ++index)
              {
                FlowNode_ReqPresentList.Api_PresentListExec.JsonItem present = jsonObject.body.presents[index];
                if (present != null)
                {
                  RewardData dataToRewardData = this.ReceiveDataToRewardData(present);
                  if (dataToRewardData != null)
                  {
                    rewardData.Exp += dataToRewardData.Exp;
                    rewardData.Stamina += dataToRewardData.Stamina;
                    rewardData.Coin += dataToRewardData.Coin;
                    rewardData.Gold += dataToRewardData.Gold;
                    rewardData.ArenaMedal += dataToRewardData.ArenaMedal;
                    rewardData.MultiCoin += dataToRewardData.MultiCoin;
                    rewardData.KakeraCoin += dataToRewardData.KakeraCoin;
                    using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = dataToRewardData.GiftRecieveItemDataDic.Values.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        GiftRecieveItemData current = enumerator.Current;
                        rewardData.AddReward(current);
                      }
                    }
                    flag = true;
                  }
                }
              }
              GlobalVars.LastReward.Set(rewardData);
              if (rewardData != null)
                MonoSingleton<GameManager>.Instance.Player.OnGoldChange(rewardData.Gold);
              MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = false;
            }
          }
          Network.RemoveAPI();
          if (flag)
            this.Success();
          else
            this.Failed();
        }
      }

      [Serializable]
      public class JsonItem
      {
        public string iname;
        public int num;
      }

      [Serializable]
      public class Json
      {
        public Json_PlayerData player;
        public Json_Item[] items;
        public FlowNode_ReqPresentList.Api_PresentListExec.JsonItem[] presents;
      }
    }

    public class Api_PresentListSend : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentListSend(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "present/send";
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(310);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(320);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListSend.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListSend.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListSend.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.result && FriendPresentRootWindow.instance != null)
            FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.SENDING);
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public bool result;
      }
    }

    public class Api_PresentListGave : FlowNode_ReqPresentList.ApiBase
    {
      private FriendPresentItemParam m_Param;

      public Api_PresentListGave(FlowNode_ReqPresentList node, GameObject gobj)
        : base(node)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gobj, (UnityEngine.Object) null))
          return;
        ContentNode component = (ContentNode) gobj.GetComponent<ContentNode>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam itemParam = component.GetParam<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>();
        if (itemParam == null)
          return;
        this.m_Param = itemParam.present;
      }

      public override string url
      {
        get
        {
          return "presentlist/exec";
        }
      }

      public override void Start()
      {
        this.Complete();
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(410);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(320);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public void Complete()
      {
        FriendPresentGaveWindow instance = FriendPresentGaveWindow.instance;
        if (instance != null && this.m_Param != null)
        {
          instance.ClearFuids();
          FriendPresentReceiveList.Param obj = MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.GetParam(this.m_Param.iname);
          if (obj != null)
          {
            for (int index = 0; index < obj.uids.Count; ++index)
              instance.AddUid(obj.uids[index]);
            this.Success();
          }
          else
            this.Failed();
        }
        else
          this.Failed();
      }
    }
  }
}
