// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MigrateAccount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/MigrateAccount", 32741)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MigrateAccount : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Facebook_Migrate.old_device_id == string.Empty || Facebook_Migrate.old_secret_key == string.Empty)
      {
        DebugUtility.LogError("Access Token not found!");
        this.ActivateOutputLinks(2);
      }
      else
      {
        MonoSingleton<GameManager>.Instance.SaveAuth(Facebook_Migrate.old_device_id, Facebook_Migrate.old_secret_key);
        MonoSingleton<GameManager>.Instance.InitAuth();
        PlayerPrefs.SetString("PlayerName", GlobalVars.NewPlayerName);
        PlayerPrefs.SetInt("AccountLinked", 1);
        Facebook_Migrate.old_device_id = string.Empty;
        Facebook_Migrate.old_secret_key = string.Empty;
        this.ActivateOutputLinks(1);
      }
    }
  }
}
