// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FindFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(200, "みつからなかった", FlowNode.PinTypes.Output, 200)]
  [FlowNode.NodeType("System/FindFriend", 32741)]
  [FlowNode.Pin(0, "FindByUsername", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "FindByID", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_FindFriend : FlowNode_Network
  {
    public InputField InputFieldFriendID;

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputFieldFriendID, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<string>) this.InputFieldFriendID.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__2A4)));
      ((Behaviour) this).set_enabled(true);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GUtility.SetImmersiveMove();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputFieldFriendID, (UnityEngine.Object) null) || this.InputFieldFriendID.get_onEndEdit() == null)
        return;
      ((UnityEventBase) this.InputFieldFriendID.get_onEndEdit()).RemoveAllListeners();
    }

    private void OnEndEdit(InputField field)
    {
      GUtility.SetImmersiveMove();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 1 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
        this.Failure();
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.InputFieldFriendID, (UnityEngine.Object) null))
      {
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        string text = this.InputFieldFriendID.get_text();
        if (string.IsNullOrEmpty(text))
        {
          ((Behaviour) this).set_enabled(false);
        }
        else
        {
          GlobalVars.SelectedFriendID = string.Empty;
          switch (pinID)
          {
            case 0:
              this.ExecRequest((WebAPI) new ReqFriendFind(text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
            case 1:
              this.ExecRequest((WebAPI) new ReqFriendFindByUsername(text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
          }
          ((Behaviour) this).set_enabled(true);
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(200);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.FindNoFriend:
            Network.RemoveAPI();
            Network.ResetError();
            this.Failure();
            break;
          case Network.EErrCode.FindIsMine:
            ((Behaviour) this).set_enabled(false);
            this.OnBack();
            break;
          case Network.EErrCode.StringTooShort:
            ((Behaviour) this).set_enabled(false);
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        DebugMenu.Log("API", "find/friend:" + www.text);
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          try
          {
            if (jsonObject.body.friends == null || jsonObject.body.friends.Length < 1)
              throw new InvalidJSONException();
            FriendData friendData = new FriendData();
            friendData.Deserialize(jsonObject.body.friends[0]);
            GlobalVars.FoundFriend = friendData;
            this.Success();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.Failure();
          }
        }
      }
    }
  }
}
