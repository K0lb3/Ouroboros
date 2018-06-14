// Decompiled with JetBrains decompiler
// Type: SRPG.SceneReplay
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Skip", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Skip生成完了", FlowNode.PinTypes.Input, 0)]
  public class SceneReplay : Scene, IFlowInterface
  {
    public static readonly string QUEST_TEXTTABLE = "quest";
    private const string CREATE_SKIP_CANVAS = "CREATE_SKIP_CANVAS";
    private const string DESTROY_SKIP_CANVAS = "DESTROY_SKIP_CANVAS";
    private const string ENABLE_SKIP_BUTTON = "ENABLE_SKIP";
    private const string DISABLE_SKIP_BUTTON = "DISABLE_SKIP";
    private const string SCENE_EXIT = "EXIT";
    private const int PIN_ID_SKIP = 0;
    private const int PIN_ID_SKIP_CANVAS_CREATED = 1;
    private StateMachine<SceneReplay> mState;
    private bool mStartCalled;
    private QuestParam mCurrentQuest;
    private BattleCore mBattle;
    private IEnumerator<string> mEventNames;
    private bool skip;
    private bool canvasCreating;
    private bool mSceneExiting;

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.skip = true;
      }
      else
      {
        if (pinID != 1)
          return;
        this.canvasCreating = false;
      }
    }

    private void Start()
    {
      LocalizedText.LoadTable(SceneReplay.QUEST_TEXTTABLE, false);
      FadeController.Instance.ResetSceneFade(0.0f);
      this.mState = new StateMachine<SceneReplay>(this);
      this.mStartCalled = true;
      this.mBattle = new BattleCore();
    }

    private void OnDestroy()
    {
      LocalizedText.UnloadTable(SceneReplay.QUEST_TEXTTABLE);
      if (this.mBattle == null)
        return;
      this.mBattle.Release();
      this.mBattle = (BattleCore) null;
    }

    public void RemoveLog()
    {
      DebugUtility.Log("RemoveLog(): " + (object) this.mBattle.Logs.Peek.GetType());
      this.mBattle.Logs.RemoveLog();
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update();
    }

    public void GotoState<StateType>() where StateType : State<SceneReplay>, new()
    {
      this.mState.GotoState<StateType>();
    }

    public bool IsInState<StateType>() where StateType : State<SceneReplay>
    {
      return this.mState.IsInState<StateType>();
    }

    public void StartQuest(string questID)
    {
      this.StartCoroutine(this.StartQuestAsync(questID));
    }

    [DebuggerHidden]
    private IEnumerator StartQuestAsync(string questID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneReplay.\u003CStartQuestAsync\u003Ec__Iterator5A() { questID = questID, \u003C\u0024\u003EquestID = questID, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator DownloadQuestAsync(QuestParam quest)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneReplay.\u003CDownloadQuestAsync\u003Ec__Iterator5B() { quest = quest, \u003C\u0024\u003Equest = quest, \u003C\u003Ef__this = this };
    }

    private void CreateSkipCanvas()
    {
      this.canvasCreating = true;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CREATE_SKIP_CANVAS");
    }

    private void DestroySkipCanvas()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "DESTROY_SKIP_CANVAS");
    }

    private void EnableSkipButton()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "ENABLE_SKIP");
    }

    private void DisableSkipButton()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "DISABLE_SKIP");
    }

    private void ExitScene()
    {
      if (this.mSceneExiting)
        return;
      this.mSceneExiting = true;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "EXIT");
    }

    private class State_PreReplay : State<SceneReplay>
    {
      public override void Begin(SceneReplay self)
      {
      }

      public override void Update(SceneReplay self)
      {
        if (self.mEventNames.MoveNext())
          self.GotoState<SceneReplay.State_Replay>();
        else
          self.GotoState<SceneReplay.State_Exit>();
      }
    }

    private class State_Replay : State<SceneReplay>
    {
      private string eventName;
      private string path;
      private bool is3DEvent;

      public override void Begin(SceneReplay self)
      {
        this.eventName = self.mEventNames.Current;
        this.path = "Events/" + this.eventName;
        if (!string.IsNullOrEmpty(this.eventName) && this.eventName.EndsWith("_3d"))
          this.is3DEvent = true;
        self.StartCoroutine(this.Exec());
      }

      [DebuggerHidden]
      private IEnumerator Exec()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneReplay.State_Replay.\u003CExec\u003Ec__Iterator5C() { \u003C\u003Ef__this = this };
      }
    }

    private class State_PostReplay : State<SceneReplay>
    {
      private AsyncOperation mAsyncOp;

      public override void Begin(SceneReplay self)
      {
        this.mAsyncOp = AssetManager.UnloadUnusedAssets();
      }

      public override void Update(SceneReplay self)
      {
        if (!this.mAsyncOp.get_isDone())
          return;
        if (self.mEventNames.MoveNext())
          self.GotoState<SceneReplay.State_Replay>();
        else
          self.GotoState<SceneReplay.State_Exit>();
      }
    }

    private class State_Exit : State<SceneReplay>
    {
      public override void Begin(SceneReplay self)
      {
        self.ExitScene();
      }
    }
  }
}
