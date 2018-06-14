// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MigrateAccount
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/MigrateAccount", 32741)]
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
