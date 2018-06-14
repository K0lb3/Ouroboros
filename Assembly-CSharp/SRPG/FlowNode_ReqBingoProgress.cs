// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBingoProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqBingoProgress", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqBingoProgress : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        ((Behaviour) this).set_enabled(false);
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqBingoProgress(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqBingoProgress.JSON_BingoResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBingoProgress.JSON_BingoResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
          this.OnRetry();
        else if (jsonObject.body.bingoprogs == null)
        {
          Network.RemoveAPI();
          this.Success();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          for (int index1 = 0; index1 < jsonObject.body.bingoprogs.Length; ++index1)
          {
            JSON_TrophyProgress bingoprog = jsonObject.body.bingoprogs[index1];
            if (bingoprog != null)
            {
              if (instance.MasterParam.GetTrophy(bingoprog.iname) == null)
              {
                DebugUtility.LogError("存在しないミッション:" + bingoprog.iname);
              }
              else
              {
                TrophyState trophyCounter = instance.Player.GetTrophyCounter(instance.MasterParam.GetTrophy(bingoprog.iname), false);
                for (int index2 = 0; index2 < bingoprog.pts.Length && index2 < trophyCounter.Count.Length; ++index2)
                  trophyCounter.Count[index2] = bingoprog.pts[index2];
                trophyCounter.StartYMD = bingoprog.ymd;
                trophyCounter.IsEnded = bingoprog.rewarded_at != 0;
              }
            }
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }

    public class JSON_BingoResponse
    {
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
