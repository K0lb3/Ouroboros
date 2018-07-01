// Decompiled with JetBrains decompiler
// Type: SRPG.EventScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EventScript : ScriptableObject
  {
    public static string[] StrCompTypeRestHP = new string[7]
    {
      "==",
      "!=",
      ">",
      ">=",
      "<",
      "<=",
      string.Empty
    };
    public static string[] StrCalcTypeRestHP = new string[3]
    {
      "％",
      "　",
      string.Empty
    };
    public static string[] StrSkillTiming = new string[3]
    {
      "スキル使用前",
      "スキル使用後",
      string.Empty
    };
    public static string[] StrShortSkillTiming = new string[3]
    {
      "前",
      "後",
      string.Empty
    };
    public const string ScenePreviewName = "@EventScenePreview";
    public const string EventScriptDir = "Events/";
    public const int MAX_UNMANAGED_FILE = 10;
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

    public static IntVector2 ConvToIntVector2Grid(string str_grid)
    {
      IntVector2 intVector2 = new IntVector2(0, 0);
      if (!string.IsNullOrEmpty(str_grid))
      {
        string[] strArray = str_grid.Split(',');
        if (strArray != null)
        {
          if (strArray.Length > 0)
            int.TryParse(strArray[0], out intVector2.x);
          if (strArray.Length > 1)
            int.TryParse(strArray[1], out intVector2.y);
        }
      }
      return intVector2;
    }

    public static string ConvToStringGrid(IntVector2 iv_grid)
    {
      return string.Format("{0},{1}", (object) iv_grid.x, (object) iv_grid.y);
    }

    public static EventScript.cRestHP ConvToObjectRestHP(string str_rest_hp)
    {
      EventScript.cRestHP cRestHp = new EventScript.cRestHP();
      if (!string.IsNullOrEmpty(str_rest_hp))
      {
        string[] strArray1 = str_rest_hp.Split(',');
        if (strArray1 != null)
        {
          foreach (string str in strArray1)
          {
            char[] chArray = new char[1]{ '-' };
            string[] strArray2 = str.Split(chArray);
            if (strArray2 != null && strArray2.Length >= 3)
            {
              int result1 = 0;
              int result2 = 0;
              int result3 = 0;
              int.TryParse(strArray2[0], out result1);
              int.TryParse(strArray2[1], out result2);
              int.TryParse(strArray2[2], out result3);
              EventScript.cRestHP.Cond cond = new EventScript.cRestHP.Cond(result1, result2, result3);
              cRestHp.mCondLists.Add(cond);
            }
          }
        }
      }
      return cRestHp;
    }

    public static string ConvToStringRestHP(EventScript.cRestHP rest_hp)
    {
      string str = string.Empty;
      if (rest_hp != null)
      {
        for (int index = 0; index < rest_hp.mCondLists.Count; ++index)
        {
          EventScript.cRestHP.Cond mCondList = rest_hp.mCondLists[index];
          if (index != 0)
            str += ",";
          str = str + (object) mCondList.mComp + "-" + (object) mCondList.mVal + "-" + (object) mCondList.mCalc;
        }
      }
      return str;
    }

    public void ResetTriggers()
    {
      for (int index = 0; index < this.mSequences.Length; ++index)
        this.mSequences[index].Triggered = false;
    }

    public static void DestroyCanvas()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) EventScript.mCanvas).get_gameObject(), 1f);
      EventScript.mCanvas = (Canvas) null;
    }

    private void CreateCanvas()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mCanvas, (UnityEngine.Object) null))
        return;
      GameObject gameObject = new GameObject("EventCanvas", new System.Type[6]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (SRPG_CanvasScaler),
        typeof (CanvasStack),
        typeof (Button),
        typeof (NullGraphic)
      });
      EventScript.mCanvas = (Canvas) gameObject.GetComponent<Canvas>();
      EventScript.mCanvas.set_renderMode((RenderMode) 0);
      CanvasStack component = (CanvasStack) gameObject.GetComponent<CanvasStack>();
      component.Priority = -1;
      component.Modal = true;
    }

    private EventScript.Sequence StartSequence(EventScript.TestCondition test, bool is_auto_forward = true, int startOffset = 0)
    {
      GameObject gameObject1 = new GameObject("EventCameraStream");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        gameObject1.AddComponent<Animation>();
      for (int index1 = 0; index1 < this.mSequences.Length; ++index1)
      {
        if (!this.mSequences[index1].Triggered && test(this.mSequences[index1]))
        {
          this.CreateCanvas();
          GameObject gameObject2 = new GameObject(((UnityEngine.Object) this).get_name());
          this.mSequences[index1].Triggered = true;
          EventScript.Sequence sequence = (EventScript.Sequence) gameObject2.AddComponent<EventScript.Sequence>();
          sequence.Actions = new EventAction[this.mSequences[index1].Actions.Count - startOffset];
          sequence.IsAutoForward = is_auto_forward;
          sequence.ParentSequence = this.mSequences[index1];
          for (int index2 = startOffset; index2 < this.mSequences[index1].Actions.Count; ++index2)
          {
            int index3 = index2 - startOffset;
            sequence.Actions[index3] = (EventAction) UnityEngine.Object.Instantiate<EventAction>((M0) this.mSequences[index1].Actions[index2]);
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
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.PostMapLoad), true, 0);
    }

    public EventScript.Sequence OnStart(int startOffset = 0, bool is_auto_forward = false)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Auto), is_auto_forward, startOffset);
    }

    public EventScript.Sequence OnQuestWin()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Win), true, 0);
    }

    public EventScript.Sequence OnTurnStart(int turnCount)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.TurnStart)
          return trigger.Turn == turnCount;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitStart(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitStart && controller.IsA(trigger.UnitName))
          return trigger.Turn == controller.Unit.TurnCount;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitHPChange(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.HPBelowPercent && controller.IsA(trigger.UnitName))
          return controller.HPPercentage <= trigger.Percentage;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitTurnStart(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitTurnStart && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName))
          return trigger.Turn == controller.Unit.TurnCount;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitAppear(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitAppear && (!trigger.IsFirstOnly || isFirstPlay))
          return controller.IsA(trigger.UnitName);
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitDead(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitDead && (!trigger.IsFirstOnly || isFirstPlay))
          return controller.IsA(trigger.UnitName);
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnStandbyGrid(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        IntVector2 intVector2Grid = EventScript.ConvToIntVector2Grid(trigger.GridXY);
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.StandbyGrid && (!trigger.IsFirstOnly || isFirstPlay) && (this.IsContainsUnit(trigger.UnitName, controller, (TacticsUnitController) null) && controller.Unit.x == intVector2Grid.x))
          return controller.Unit.y == intVector2Grid.y;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitRestHP(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        EventScript.cRestHP objectRestHp = EventScript.ConvToObjectRestHP(trigger.RestHP);
        bool flag = true;
        using (List<EventScript.cRestHP.Cond>.Enumerator enumerator = objectRestHp.mCondLists.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EventScript.cRestHP.Cond current = enumerator.Current;
            int num = 0;
            switch (current.mCalc)
            {
              case EventScript.cRestHP.Cond.CalcType.SCALE:
                num = controller.HPPercentage;
                break;
              case EventScript.cRestHP.Cond.CalcType.FIXED:
                num = (int) controller.Unit.CurrentStatus.param.hp;
                break;
            }
            switch (current.mComp)
            {
              case EventScript.cRestHP.Cond.CompType.EQUAL:
                if (num != current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
              case EventScript.cRestHP.Cond.CompType.NOT_EQUAL:
                if (num == current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
              case EventScript.cRestHP.Cond.CompType.GREATER:
                if (num <= current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
              case EventScript.cRestHP.Cond.CompType.GREATER_EQUAL:
                if (num < current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
              case EventScript.cRestHP.Cond.CompType.LESS:
                if (num >= current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
              case EventScript.cRestHP.Cond.CompType.LESS_EQUAL:
                if (num > current.mVal)
                {
                  flag = false;
                  break;
                }
                break;
            }
            if (!flag)
              break;
          }
        }
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.RestHP && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName))
          return flag;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUseSkill(EventScript.SkillTiming timing, TacticsUnitController controller, SkillData skill, List<TacticsUnitController> TargetLists, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        bool flag = false;
        if (TargetLists != null)
        {
          using (List<TacticsUnitController>.Enumerator enumerator = TargetLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              if (this.IsContainsUnit(trigger.TargetUnit, current, controller))
              {
                flag = true;
                break;
              }
            }
          }
        }
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UseSkill && (EventScript.SkillTiming) trigger.SkillTiming == timing && (!trigger.IsFirstOnly || isFirstPlay) && (this.IsContainsUnit(trigger.UnitName, controller, (TacticsUnitController) null) && flag))
          return this.IsContainsSkill(trigger.TargetSkill, skill);
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnUnitWithdraw(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitWithdraw && (!trigger.IsFirstOnly || isFirstPlay))
          return controller.IsA(trigger.UnitName);
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnQuestLose()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Lose), true, 0);
    }

    public EventScript.Sequence OnRecvSkillElem(TacticsUnitController controller, EElement elem, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.RecvSkillElem && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller, (TacticsUnitController) null))
          return elem == (EElement) trigger.SkillElem;
        return false;
      }), true, 0);
    }

    public EventScript.Sequence OnRecvSkillCond(TacticsUnitController controller, EUnitCondition cond, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        if (trigger.Trigger == EventScript.ScriptSequence.StartConditions.RecvSkillCond && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller, (TacticsUnitController) null))
          return (cond & (EUnitCondition) trigger.SkillCond) != (EUnitCondition) 0;
        return false;
      }), true, 0);
    }

    private bool IsContainsUnit(string unit_name, TacticsUnitController self, TacticsUnitController opp = null)
    {
      if (string.IsNullOrEmpty(unit_name) || UnityEngine.Object.op_Equality((UnityEngine.Object) self, (UnityEngine.Object) null))
        return false;
      bool flag = false;
      string[] strArray = unit_name.Split(',');
      if (strArray != null)
      {
        foreach (string id in strArray)
        {
          if (id == "pall")
          {
            if (self.Unit.Side == EUnitSide.Player)
              flag = true;
          }
          else if (id == "eall")
          {
            if (self.Unit.Side == EUnitSide.Enemy)
              flag = true;
          }
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) opp, (UnityEngine.Object) null) && id == "other")
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self, (UnityEngine.Object) opp))
              flag = true;
          }
          else if (self.IsA(id))
            flag = true;
          if (flag)
            break;
        }
      }
      return flag;
    }

    private bool IsContainsSkill(string skill_name, SkillData skill)
    {
      if (string.IsNullOrEmpty(skill_name) || skill == null)
        return false;
      if (skill_name.IndexOf("sall") >= 0)
        return true;
      bool flag = false;
      string[] strArray = skill_name.Split(',');
      if (strArray != null)
      {
        foreach (string str in strArray)
        {
          if (str == skill.SkillParam.iname)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    [Serializable]
    public class ScriptSequence
    {
      [IntIsSkillCond]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RecvSkillCond})]
      public int SkillCond = 1;
      [StringIsActorID]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.HPBelowPercent, EventScript.ScriptSequence.StartConditions.UnitTurnStart, EventScript.ScriptSequence.StartConditions.UnitAppear, EventScript.ScriptSequence.StartConditions.UnitDead, EventScript.ScriptSequence.StartConditions.StandbyGrid, EventScript.ScriptSequence.StartConditions.RestHP, EventScript.ScriptSequence.StartConditions.UseSkill, EventScript.ScriptSequence.StartConditions.UnitWithdraw, EventScript.ScriptSequence.StartConditions.RecvSkillElem, EventScript.ScriptSequence.StartConditions.RecvSkillCond})]
      public string UnitName;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.HPBelowPercent})]
      [Range(0.0f, 99f)]
      public int Percentage;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.TurnStart, EventScript.ScriptSequence.StartConditions.UnitTurnStart})]
      public int Turn;
      [StringIsGrid]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.StandbyGrid})]
      public string GridXY;
      [StringIsRestHP]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RestHP})]
      public string RestHP;
      [IntIsSkillTiming]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      public int SkillTiming;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      [StringIsTargetSkill]
      public string TargetSkill;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      [StringIsTargetUnit]
      public string TargetUnit;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitTurnStart, EventScript.ScriptSequence.StartConditions.UnitAppear, EventScript.ScriptSequence.StartConditions.UnitDead, EventScript.ScriptSequence.StartConditions.StandbyGrid, EventScript.ScriptSequence.StartConditions.RestHP, EventScript.ScriptSequence.StartConditions.UseSkill, EventScript.ScriptSequence.StartConditions.UnitWithdraw})]
      public bool IsFirstOnly;
      [IntIsSkillElem]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RecvSkillElem})]
      public int SkillElem;
      public EventScript.ScriptSequence.StartConditions Trigger;
      public List<EventAction> Actions;
      [NonSerialized]
      public bool Triggered;

      public bool IsSavePlayBgmID
      {
        get
        {
          EventScript.ScriptSequence.StartConditions trigger = this.Trigger;
          switch (trigger)
          {
            case EventScript.ScriptSequence.StartConditions.Auto:
            case EventScript.ScriptSequence.StartConditions.Win:
            case EventScript.ScriptSequence.StartConditions.PostMapLoad:
              return false;
            default:
              if (trigger != EventScript.ScriptSequence.StartConditions.Lose)
                return true;
              goto case EventScript.ScriptSequence.StartConditions.Auto;
          }
        }
      }

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
        UnitTurnStart,
        UnitAppear,
        UnitDead,
        StandbyGrid,
        RestHP,
        UseSkill,
        UnitWithdraw,
        Lose,
        RecvSkillElem,
        RecvSkillCond,
      }
    }

    public class cRestHP
    {
      public List<EventScript.cRestHP.Cond> mCondLists = new List<EventScript.cRestHP.Cond>();

      public class Cond
      {
        public EventScript.cRestHP.Cond.CompType mComp;
        public int mVal;
        public EventScript.cRestHP.Cond.CalcType mCalc;

        public Cond()
        {
        }

        public Cond(int comp, int val, int calc)
        {
          if (comp < 0 || comp >= 6)
            comp = 0;
          if (calc < 0 || calc >= 2)
            calc = 0;
          this.mComp = (EventScript.cRestHP.Cond.CompType) comp;
          this.mVal = val;
          this.mCalc = (EventScript.cRestHP.Cond.CalcType) calc;
        }

        public enum CompType
        {
          EQUAL,
          NOT_EQUAL,
          GREATER,
          GREATER_EQUAL,
          LESS,
          LESS_EQUAL,
          MAX,
        }

        public enum CalcType
        {
          SCALE,
          FIXED,
          MAX,
        }
      }
    }

    public enum SkillTiming
    {
      BEFORE,
      AFTER,
      MAX,
    }

    public class Sequence : MonoBehaviour
    {
      public EventScript Script;
      public EventAction[] Actions;
      private bool mReady;
      private bool mForceSkip;
      private int mLastActionIndex;
      private List<int> mForceSkipIndex;
      private UnityAction mClickAction;
      public bool IsAutoForward;
      private float mTimerAutoForward;
      private int mCurIdxAutoForward;
      public EventScript.ScriptSequence ParentSequence;
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
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScene, (UnityEngine.Object) null))
            UnityEngine.Object.Destroy((UnityEngine.Object) this.mScene);
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
        this.mTimerAutoForward = 0.0f;
        this.mCurIdxAutoForward = -1;
        this.StartCoroutine(this.PreloadAssetsAsync());
      }

      private void OnDestroy()
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mScene);
        EventDialogBubble.DiscardAll();
        EventDialogBubbleCustom.DiscardAll();
        EventStandCharaController2.DiscardAll();
        for (int index = 0; index < this.Actions.Length; ++index)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.Actions[index]);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.Canvas, (UnityEngine.Object) null))
          return;
        ((UnityEvent) ((Button) ((Component) EventScript.Canvas).GetComponent<Button>()).get_onClick()).RemoveListener(this.mClickAction);
        EventScript.DestroyCanvas();
      }

      [DebuggerHidden]
      private IEnumerator PreloadAssetsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new EventScript.Sequence.\u003CPreloadAssetsAsync\u003Ec__Iterator9D()
        {
          \u003C\u003Ef__this = this
        };
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
        if (this.mForceSkip)
        {
          for (int index1 = 0; index1 < this.mForceSkipIndex.Count; ++index1)
          {
            int index2 = this.mForceSkipIndex[index1];
            if (index2 < this.Actions.Length && this.Actions[index2].enabled)
            {
              EventAction action = this.Actions[index2];
              if (!(action is EventAction_WaitTap) && !(action is EventAction_Dialog) && (!(action is EventAction_Dialog2) && !(action is EventAction_FadeCanvas)) && (!(action is EventAction_FadeScreen) && !(action is EventAction_WaitSeconds)))
              {
                action.Update();
                if (!action.enabled)
                {
                  this.mForceSkipIndex.RemoveAt(index1);
                  --index1;
                }
              }
              else
              {
                action.GoToEndState();
                action.enabled = false;
                this.mForceSkipIndex.RemoveAt(index1);
                --index1;
              }
            }
            else
            {
              this.mForceSkipIndex.RemoveAt(index1);
              --index1;
            }
          }
          if (this.mForceSkipIndex.Any<int>())
            return;
          if (this.mLastActionIndex >= 0)
            this.StartForceSkip();
          this.mForceSkip = false;
          this.mForceSkipIndex.Clear();
          this.mLastActionIndex = -1;
        }
        else
        {
          int num = -1;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled)
            {
              this.Actions[index].Update();
              num = index;
            }
          }
          if (!this.IsAutoForward)
            return;
          SceneBattle instance = SceneBattle.Instance;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || !instance.Battle.RequestAutoBattle && !instance.Battle.IsMultiPlay)
            return;
          if (this.mCurIdxAutoForward != num)
          {
            this.mCurIdxAutoForward = num;
            this.mTimerAutoForward = GameSettings.Instance.Quest.WaitTimeScriptEventForward;
          }
          if ((double) this.mTimerAutoForward <= 0.0)
            return;
          this.mTimerAutoForward -= Time.get_deltaTime();
          if ((double) this.mTimerAutoForward > 0.0)
            return;
          bool flag = false;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled && this.Actions[index].Forward())
              flag = true;
          }
          if (flag)
            return;
          this.mTimerAutoForward = 1f;
        }
      }

      private void StartForceSkip()
      {
        int index1 = this.mLastActionIndex + 1;
        for (int index2 = index1; index2 < this.Actions.Length; ++index2)
        {
          this.Actions[index2].GoToEndState();
          this.Actions[index2].Skip = true;
        }
        if (index1 >= this.Actions.Length)
          return;
        this.Actions[index1].Forward();
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

      public void GoToEndState()
      {
        this.mForceSkip = true;
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled)
            this.mForceSkipIndex.Add(index);
        }
        if (this.mForceSkipIndex.Any<int>())
          this.mLastActionIndex = this.mForceSkipIndex[this.mForceSkipIndex.Count - 1];
        else
          this.mLastActionIndex = -1;
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
        if (index1 == -1)
          return;
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
