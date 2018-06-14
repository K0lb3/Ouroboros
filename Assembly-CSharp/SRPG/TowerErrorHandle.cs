// Decompiled with JetBrains decompiler
// Type: SRPG.TowerErrorHandle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TowerErrorHandle
  {
    public static bool Error(FlowNode_Network check = null)
    {
      if (!Network.IsError)
        return false;
      switch (Network.ErrCode)
      {
        case Network.EErrCode.TowerLocked:
        case Network.EErrCode.NotExist_tower:
        case Network.EErrCode.NotExist_reward:
        case Network.EErrCode.NoMatch_party:
        case Network.EErrCode.IncorrectBtlparam:
        case Network.EErrCode.AlreadyBtlend:
        case Network.EErrCode.FaildReset:
          if (Object.op_Equality((Object) check, (Object) null))
          {
            FlowNode_Network.Failed();
            break;
          }
          check.OnFailed();
          break;
        case Network.EErrCode.ConditionsErr:
        case Network.EErrCode.NotRecovery_permit:
        case Network.EErrCode.NotExist_floor:
        case Network.EErrCode.NoMatch_mid:
        case Network.EErrCode.IncorrectCoin:
        case Network.EErrCode.AlreadyClear:
        case Network.EErrCode.ArtifactBoxLimit:
          if (Object.op_Equality((Object) check, (Object) null))
          {
            FlowNode_Network.Back();
            break;
          }
          check.OnBack();
          break;
        case Network.EErrCode.FaildRegistration:
          if (Object.op_Equality((Object) check, (Object) null))
          {
            FlowNode_Network.Retry();
            break;
          }
          check.OnRetry();
          break;
        default:
          if (Object.op_Equality((Object) check, (Object) null))
          {
            FlowNode_Network.Failed();
            break;
          }
          check.OnFailed();
          break;
      }
      return true;
    }
  }
}
