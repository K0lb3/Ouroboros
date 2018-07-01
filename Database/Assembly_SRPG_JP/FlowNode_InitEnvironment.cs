// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitEnvironment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/Init", 65535)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_InitEnvironment : FlowNode
  {
    private void Init()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      GameManager gameManager;
      if (Object.op_Inequality((Object) instanceDirect, (Object) null))
      {
        if (instanceDirect.IsRelogin)
        {
          instanceDirect.ResetData();
        }
        else
        {
          Object.DestroyImmediate((Object) instanceDirect);
          gameManager = (GameManager) null;
        }
      }
      CriticalSection.ForceReset();
      ButtonEvent.Reset();
      SRPG_TouchInputModule.UnlockInput(true);
      PunMonoSingleton<MyPhoton>.Instance.Disconnect();
      UIUtility.PopCanvasAll();
      AssetManager.UnloadAll();
      AssetDownloader.Reset();
      Network.Reset();
      gameManager = MonoSingleton<GameManager>.Instance;
      GameUtility.ForceSetDefaultSleepSetting();
      if (GameUtility.IsStripBuild)
        GameUtility.Config_UseAssetBundles.Value = true;
      LocalizedText.UnloadAllTables();
      SRPG_InputField.ResetInput();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.Init();
      this.ActivateOutputLinks(1);
    }
  }
}
