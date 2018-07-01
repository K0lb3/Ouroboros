// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartReplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Started", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/Replay/StartReplay", 32741)]
  public class FlowNode_StartReplay : FlowNode
  {
    private const int PIN_ID_LOAD = 0;
    private const int PIN_ID_STARTED = 10;
    private const int PIN_ID_FAILED = 11;
    [HideInInspector]
    public RestorePoints RestorePoint;
    public bool SetRestorePoint;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      ((Behaviour) this).set_enabled(true);
      CriticalSection.Enter(CriticalSections.SceneChange);
      this.StartCoroutine(this.StartScene(MonoSingleton<GameManager>.Instance.FindQuest((string) GlobalVars.ReplaySelectedQuestID)));
    }

    private void OnSceneLoad(GameObject sceneRoot)
    {
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
      CriticalSection.Leave(CriticalSections.SceneChange);
    }

    [DebuggerHidden]
    private IEnumerator StartScene(QuestParam questParam)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartReplay.\u003CStartScene\u003Ec__IteratorD3() { questParam = questParam, \u003C\u0024\u003EquestParam = questParam, \u003C\u003Ef__this = this };
    }

    private class QuestLauncher
    {
      private QuestParam mQuestParam;

      public QuestLauncher(QuestParam questParam)
      {
        this.mQuestParam = questParam;
      }

      public void OnSceneAwake(GameObject scene)
      {
        SceneReplay component = (SceneReplay) scene.GetComponent<SceneReplay>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        CriticalSection.Leave(CriticalSections.SceneChange);
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
        component.StartQuest(this.mQuestParam.iname);
      }
    }
  }
}
