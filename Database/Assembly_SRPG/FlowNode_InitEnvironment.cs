// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitEnvironment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Init", 65535)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
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
      AssetDownloader.ResetTextSetting();
      Network.Reset();
      gameManager = MonoSingleton<GameManager>.Instance;
      GameUtility.ForceSetDefaultSleepSetting();
      if (GameUtility.IsStripBuild)
        GameUtility.Config_UseAssetBundles.Value = true;
      LocalizedText.UnloadAllTables();
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
