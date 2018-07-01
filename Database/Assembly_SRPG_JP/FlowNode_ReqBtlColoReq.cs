// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlColoReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class FlowNode_ReqBtlColoReq : FlowNode_Network
  {
    private FlowNode_ReqBtlColoReq.OnSuccesDelegate mOnSuccessDelegate;

    public FlowNode_ReqBtlColoReq.OnSuccesDelegate OnSuccessListeners
    {
      set
      {
        this.mOnSuccessDelegate = value;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ColoCantSelect:
            this.OnBack();
            break;
          case Network.EErrCode.ColoIsBusy:
            this.OnBack();
            break;
          case Network.EErrCode.ColoCostShort:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoIntervalShort:
            this.OnBack();
            break;
          case Network.EErrCode.ColoBattleNotEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoPlayerLvShort:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.mOnSuccessDelegate(jsonObject.body);
        }
      }
    }

    public delegate void OnSuccesDelegate(BattleCore.Json_Battle response);
  }
}
