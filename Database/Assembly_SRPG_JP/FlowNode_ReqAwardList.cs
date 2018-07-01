// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAwardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "所持称号取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqAwardList", 32741)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failure", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqAwardList : FlowNode_Network
  {
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    public GameObject Target;
    public FlowNode_ReqAwardList.MODE mMode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqAwardList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(11);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ResAwardList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResAwardList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          if (this.mMode == FlowNode_ReqAwardList.MODE.SetAwardList)
          {
            if (Object.op_Equality((Object) this.Target, (Object) null) || Object.op_Equality((Object) this.Target.GetComponent<AwardList>(), (Object) null))
            {
              this.Failure();
              return;
            }
            ((AwardList) this.Target.GetComponent<AwardList>()).SetOpenAwards(jsonObject.body.awards);
          }
          else if (this.mMode == FlowNode_ReqAwardList.MODE.SetPlayerAward)
            MonoSingleton<GameManager>.Instance.Player.SetHaveAward(jsonObject.body.awards);
          this.Success();
        }
      }
    }

    public enum MODE
    {
      SetAwardList,
      SetPlayerAward,
    }
  }
}
