// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutrial
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/CheckTutrial")]
  [FlowNode.Pin(0, "CheckTutrial", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Skip", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_CheckTutrial : FlowNode
  {
    private void Start()
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != 0L || (long) GlobalVars.BtlID != 0L)
        {
          this.ActivateOutputLinks(3);
          return;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.UpdateTutorialStep();
        if (instance.TutorialStep == 0 && GameUtility.IsDebugBuild)
        {
          UIUtility.ConfirmBox(LocalizedText.Get("sys.PLAYTUTORIAL"), new UIUtility.DialogResultEvent(this.OnPlayTutorial), new UIUtility.DialogResultEvent(this.OnSkipTutorial), (GameObject) null, false, -1);
          return;
        }
      }
      this.ActivateOutputLinks(3);
    }

    private void OnPlayTutorial(GameObject go)
    {
      GlobalVars.DebugIsPlayTutorial = true;
      this.ActivateOutputLinks(1);
    }

    private void OnSkipTutorial(GameObject go)
    {
      GlobalVars.DebugIsPlayTutorial = false;
      this.ActivateOutputLinks(2);
    }
  }
}
