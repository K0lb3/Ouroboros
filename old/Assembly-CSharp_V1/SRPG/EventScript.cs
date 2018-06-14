// Decompiled with JetBrains decompiler
// Type: SRPG.EventScript
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EventScript : ScriptableObject
  {
    public const string ScenePreviewName = "@EventScenePreview";
    public const string EventScriptDir = "Events/";
    private static Canvas mCanvas;
    public string QuestID;
    public EventScript.ScriptSequence[] mSequences;

    public EventScript()
    {
      base.\u002Ector();
    }

    public static Canvas Canvas
    {
      get
      {
        return EventScript.mCanvas;
      }
    }

    public void ResetTriggers()
    {
      for (int index = 0; index < this.mSequences.Length; ++index)
        this.mSequences[index].Triggered = false;
    }

    public static void DestroyCanvas()
    {
      Object.Destroy((Object) ((Component) EventScript.mCanvas).get_gameObject(), 1f);
      EventScript.mCanvas = (Canvas) null;
    }

    private void CreateCanvas()
    {
      if (Object.op_Inequality((Object) EventScript.mCanvas, (Object) null))
        return;
      GameObject gameObject = new GameObject("EventCanvas", new System.Type[7]{ typeof (Canvas), typeof (GraphicRaycaster), typeof (SRPG_CanvasScaler), typeof (CanvasStack), typeof (Button), typeof (HoldGesture), typeof (NullGraphic) });
      EventScript.mCanvas = (Canvas) gameObject.GetComponent<Canvas>();
      EventScript.mCanvas.set_renderMode((RenderMode) 0);
      CanvasStack component = (CanvasStack) gameObject.GetComponent<CanvasStack>();
      component.Priority = -1;
      component.Modal = true;
    }

    private EventScript.Sequence StartSequence(EventScript.TestCondition test, int startOffset = 0)
    {
      GameObject gameObject1 = new GameObject("EventCameraStream");
      if (Object.op_Inequality((Object) gameObject1, (Object) null))
        gameObject1.AddComponent<Animation>();
      for (int index1 = 0; index1 < this.mSequences.Length; ++index1)
      {
        if (!this.mSequences[index1].Triggered && test(this.mSequences[index1]))
        {
          this.CreateCanvas();
          GameObject gameObject2 = new GameObject(((Object) this).get_name());
          this.mSequences[index1].Triggered = true;
          EventScript.Sequence sequence = (EventScript.Sequence) gameObject2.AddComponent<EventScript.Sequence>();
          sequence.Actions = new EventAction[this.mSequences[index1].Actions.Count - startOffset];
          for (int index2 = startOffset; index2 < this.mSequences[index1].Actions.Count; ++index2)
          {
            int index3 = index2 - startOffset;
            sequence.Actions[index3] = (EventAction) Object.Instantiate<EventAction>((M0) this.mSequences[index1].Actions[index2]);
            sequence.Actions[index3].Sequence = sequence;
            if (index2 > startOffset)
              sequence.Actions[index3 - 1].NextAction = sequence.Actions[index3];
          }
          return sequence;
        }
      }
      return (EventScript.Sequence) null;
    }

    public EventScript.Sequence OnPostMapLoad()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.PostMapLoad), 0);
    }

    public EventScript.Sequence OnStart(int startOffset = 0)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Auto), startOffset);
    }

    public EventScript.Sequence OnQuestWin()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Win), 0);
    }

    public EventScript.Sequence OnTurnStart(int turnCount)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.TurnStart)
          return trigger.Turn == turnCount;
        return false;
      }), 0);
    }

    public EventScript.Sequence OnUnitStart(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitStart && controller.IsA(trigger.UnitName))
          return trigger.Turn == controller.TurnCount;
        return false;
      }), 0);
    }

    public EventScript.Sequence OnUnitHPChange(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.HPBelowPercent && controller.IsA(trigger.UnitName))
          return controller.HPPercentage <= trigger.Percentage;
        return false;
      }), 0);
    }

    [Serializable]
    public class ScriptSequence
    {
      [StringIsActorID]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.HPBelowPercent})]
      public string UnitName;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.HPBelowPercent})]
      [Range(0.0f, 99f)]
      public int Percentage;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.TurnStart})]
      public int Turn;
      public EventScript.ScriptSequence.StartConditions Trigger;
      public List<EventAction> Actions;
      [NonSerialized]
      public bool Triggered;

      public class ConditionAttr : Attribute
      {
        public EventScript.ScriptSequence.StartConditions[] Conditions;

        public ConditionAttr(params EventScript.ScriptSequence.StartConditions[] conditions)
        {
          this.Conditions = conditions;
        }

        public bool Contains(EventScript.ScriptSequence.StartConditions condition)
        {
          return Array.IndexOf<EventScript.ScriptSequence.StartConditions>(this.Conditions, condition) >= 0;
        }
      }

      public enum StartConditions
      {
        Auto,
        UnitStart,
        HPBelowPercent,
        Win,
        TurnStart,
        PostMapLoad,
      }
    }

    public class Sequence : MonoBehaviour
    {
      public EventScript Script;
      public EventAction[] Actions;
      private bool mReady;
      private UnityAction mClickAction;
      private UnityAction mDownAction;
      private UnityAction mUpAction;
      private bool buttonDown;
      private bool fastForward;
      private float holdDownTime;
      private GameObject mScene;
      public List<GameObject> SpawnedObjects;

      public Sequence()
      {
        base.\u002Ector();
      }

      public GameObject Scene
      {
        set
        {
          if (Object.op_Inequality((Object) this.mScene, (Object) null))
            Object.Destroy((Object) this.mScene);
          this.mScene = value;
        }
        get
        {
          return this.mScene;
        }
      }

      private void Start()
      {
        Debug.LogWarning((object) this);
        // ISSUE: method pointer
        this.mClickAction = new UnityAction((object) this, __methodptr(OnClick));
        ((UnityEvent) ((Button) ((Component) EventScript.Canvas).GetComponent<Button>()).get_onClick()).AddListener(this.mClickAction);
        HoldGesture component = (HoldGesture) ((Component) EventScript.Canvas).GetComponent<HoldGesture>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          // ISSUE: method pointer
          this.mDownAction = new UnityAction((object) this, __methodptr(OnDown));
          component.OnHoldStart = new UnityEvent();
          component.OnHoldStart.AddListener(this.mDownAction);
          // ISSUE: method pointer
          this.mUpAction = new UnityAction((object) this, __methodptr(OnUp));
          component.OnHoldEnd = new UnityEvent();
          component.OnHoldEnd.AddListener(this.mUpAction);
        }
        this.StartCoroutine(this.PreloadAssetsAsync());
      }

      private void OnDestroy()
      {
        Object.Destroy((Object) this.mScene);
        EventDialogBubble.DiscardAll();
        EventDialogBubbleCustom.DiscardAll();
        EventStandCharaController2.DiscardAll();
        for (int index = 0; index < this.Actions.Length; ++index)
          Object.Destroy((Object) this.Actions[index]);
        if (!Object.op_Inequality((Object) EventScript.Canvas, (Object) null))
          return;
        ((UnityEvent) ((Button) ((Component) EventScript.Canvas).GetComponent<Button>()).get_onClick()).RemoveListener(this.mClickAction);
        HoldGesture component = (HoldGesture) ((Component) EventScript.Canvas).GetComponent<HoldGesture>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          // ISSUE: method pointer
          component.OnHoldStart.RemoveListener(new UnityAction((object) this, __methodptr(OnDown)));
          // ISSUE: method pointer
          component.OnHoldEnd.RemoveListener(new UnityAction((object) this, __methodptr(OnUp)));
        }
        EventScript.DestroyCanvas();
      }

      [DebuggerHidden]
      private IEnumerator PreloadAssetsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new EventScript.Sequence.\u003CPreloadAssetsAsync\u003Ec__Iterator72() { \u003C\u003Ef__this = this };
      }

      private void StartActions()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (!this.Actions[index].Skip)
            this.Actions[index].PreStart();
        }
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (!this.Actions[index].Skip)
          {
            this.Actions[index].enabled = true;
            break;
          }
        }
      }

      private void Update()
      {
        if (!this.mReady)
          return;
        this.fastForward = this.buttonDown && (double) Time.get_time() - (double) this.holdDownTime > 0.200000002980232;
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled)
          {
            this.Actions[index].Update();
            if (this.fastForward)
              this.Actions[index].Forward();
          }
        }
      }

      public bool IsPlaying
      {
        get
        {
          if (!this.mReady)
            return true;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled)
              return true;
          }
          return false;
        }
      }

      private void OnClick()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled)
            this.Actions[index].Forward();
        }
      }

      private void OnDown()
      {
        this.buttonDown = true;
        this.holdDownTime = Time.get_time();
      }

      private void OnUp()
      {
        this.buttonDown = false;
      }

      public void OnQuit()
      {
        int index1 = -1;
        for (int index2 = 0; index2 < this.Actions.Length; ++index2)
        {
          if (this.Actions[index2].enabled)
            index1 = index2;
          else if (index1 != -1)
          {
            if (!(this.Actions[index2] is Event2dAction_QuitDisable) && !(this.Actions[index2] is Event2dAction_Scene))
              this.Actions[index2].Skip = true;
            else
              break;
          }
        }
        this.Actions[index1].Forward();
      }

      public void OnQuitImmediate()
      {
        int index1 = -1;
        for (int index2 = 0; index2 < this.Actions.Length; ++index2)
        {
          if (this.Actions[index2].enabled)
          {
            this.Actions[index2].SkipImmediate();
            this.Actions[index2].Skip = true;
            index1 = index2;
          }
          else
            this.Actions[index2].Skip = true;
        }
        if (index1 == -1)
          return;
        this.Actions[index1].Forward();
      }

      public bool ReplaySkipButtonEnable()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled && !this.Actions[index].ReplaySkipButtonEnable())
            return false;
        }
        return true;
      }
    }

    private delegate bool TestCondition(EventScript.ScriptSequence trigger);
  }
}
