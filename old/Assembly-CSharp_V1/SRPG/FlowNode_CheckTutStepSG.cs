// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutStepSG
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "On Complete", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(12, "Complete Tutorial", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Complete Step", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Enter", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/CheckTutStepSG", 32741)]
  [FlowNode.Pin(1, "On Enter", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_CheckTutStepSG : FlowNodePersistent
  {
    [SerializeField]
    private string tutorialStep;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool poll;
    private string lastTutorialStep;
    private bool isTracking;

    public override void OnActivate(int pinID)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.IsTutorial())
        return;
      string nextTutorialStep = instance.GetNextTutorialStep();
      DebugUtility.LogWarning("nextTutorialStep " + nextTutorialStep);
      switch (pinID)
      {
        case 10:
          if (!(nextTutorialStep == this.tutorialStep))
            break;
          this.ActivateOutputLinks(1);
          DebugUtility.LogWarning("Starting step " + this.tutorialStep);
          if (this.poll)
            this.poll = false;
          if (this.tutorialStep.Equals("ShowStartTutorialDialog"))
          {
            AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip1", new Dictionary<string, object>()
            {
              {
                "step_number",
                (object) "16"
              }
            });
            break;
          }
          if (this.tutorialStep.Equals("ShowStartTutorialUnitsDialog"))
          {
            AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip2", new Dictionary<string, object>()
            {
              {
                "step_number",
                (object) "22"
              }
            });
            break;
          }
          if (!this.tutorialStep.Equals("ShowGridMovement"))
            break;
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.guide_battle_move", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "7"
            }
          });
          break;
        case 11:
          if (!(nextTutorialStep == this.tutorialStep))
            break;
          instance.CompleteTutorialStep();
          this.ActivateOutputLinks(2);
          DebugUtility.LogWarning("Finished step " + this.tutorialStep);
          break;
        case 12:
          this.CompleteTutorial();
          break;
      }
    }

    private new void Awake()
    {
      if (MonoSingleton<GameManager>.Instance.IsTutorial())
        return;
      Object.Destroy((Object) this);
    }

    private void Update()
    {
      if (!this.poll)
        return;
      string nextTutorialStep = MonoSingleton<GameManager>.Instance.GetNextTutorialStep();
      if (nextTutorialStep != this.lastTutorialStep)
        this.OnActivate(10);
      this.lastTutorialStep = nextTutorialStep;
    }

    private void CompleteTutorial()
    {
      MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(1L);
      this.StartCoroutine(this.WaitCompleteTutorialAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitCompleteTutorialAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_CheckTutStepSG.\u003CWaitCompleteTutorialAsync\u003Ec__Iterator20() { \u003C\u003Ef__this = this };
    }
  }
}
