// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitEnvironment
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Init", 65535)]
  public class FlowNode_InitEnvironment : FlowNode
  {
    private void Init()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      GameManager gameManager;
      if (Object.op_Inequality((Object) instanceDirect, (Object) null))
      {
        Object.DestroyImmediate((Object) instanceDirect);
        gameManager = (GameManager) null;
      }
      CriticalSection.ForceReset();
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
