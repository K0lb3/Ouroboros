// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqReadTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqReadTips", 32741)]
  [FlowNode.Pin(0, "TIPS既読", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "成功", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqReadTips : FlowNode_Network
  {
    private const int PIN_ID_REQUEST = 0;
    private const int PIN_ID_SUCCESS = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string lastReadTips = GlobalVars.LastReadTips;
      if (MonoSingleton<GameManager>.Instance.Tips.Contains(lastReadTips))
      {
        this.ActivateOutputLinks(10);
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.OnReadTips(lastReadTips);
        string trophy_progs;
        string bingo_progs;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
        this.ExecRequest((WebAPI) new ReqReadTips(lastReadTips, trophy_progs, bingo_progs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ReturnTips> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReturnTips>>(www.text);
      if (jsonObject.body.tips != null)
      {
        List<string> tips = MonoSingleton<GameManager>.Instance.Tips;
        if (!tips.Contains(jsonObject.body.tips))
          tips.Add(jsonObject.body.tips);
      }
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      this.ActivateOutputLinks(10);
      Network.RemoveAPI();
    }
  }
}
