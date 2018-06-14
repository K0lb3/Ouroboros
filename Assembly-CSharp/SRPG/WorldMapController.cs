// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class WorldMapController : MonoBehaviour
  {
    public AreaMapController[] AreaMaps;
    public RawImage[] Images;
    public RadialBlurEffect RadialBlurEffect;
    public bool AutoSelectArea;
    private RectTransform mTransform;
    private Vector2 mDefaultPosition;
    private Vector2 mDefaultScale;
    private AreaMapController mCurrentArea;
    private AreaMapController mPrevArea;
    private AreaMapController mNextArea;
    public float TransitionTime;
    public AnimationCurve RadialBlurCurve;
    private StateMachine<WorldMapController> mStateMachine;

    public WorldMapController()
    {
      base.\u002Ector();
    }

    public static WorldMapController FindInstance(string gameobjectID)
    {
      GameObject gameObject = GameObjectID.FindGameObject(gameobjectID);
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        return (WorldMapController) gameObject.GetComponent<WorldMapController>();
      return (WorldMapController) null;
    }

    public void GotoArea(string areaID)
    {
      for (int index = 0; index < this.AreaMaps.Length; ++index)
      {
        if (this.AreaMaps[index].MapID == areaID)
        {
          this.mCurrentArea = this.AreaMaps[index];
          return;
        }
      }
      this.mCurrentArea = (AreaMapController) null;
    }

    private void Start()
    {
      this.mTransform = ((Component) this).get_transform() as RectTransform;
      this.mDefaultPosition = this.mTransform.get_anchoredPosition();
      this.mDefaultScale = Vector2.op_Implicit(((Transform) this.mTransform).get_localScale());
      this.mStateMachine = new StateMachine<WorldMapController>(this);
      if (this.AutoSelectArea)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (string.IsNullOrEmpty((string) GlobalVars.SelectedSection) && string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
        {
          GlobalVars.SelectedSection.Set(instance.Sections[0].iname);
          for (int index = 0; index < instance.Chapters.Length; ++index)
          {
            if (instance.Chapters[index].section == (string) GlobalVars.SelectedSection)
            {
              GlobalVars.SelectedChapter.Set(instance.Chapters[index].iname);
              break;
            }
          }
        }
        for (int index1 = 0; index1 < instance.Chapters.Length; ++index1)
        {
          if (instance.Chapters[index1].section == (string) GlobalVars.SelectedSection && (instance.Chapters[index1].iname == (string) GlobalVars.SelectedChapter || string.IsNullOrEmpty((string) GlobalVars.SelectedChapter)))
          {
            for (int index2 = 0; index2 < this.AreaMaps.Length; ++index2)
            {
              if (this.AreaMaps[index2].MapID == instance.Chapters[index1].world)
              {
                this.mCurrentArea = this.AreaMaps[index2];
                break;
              }
            }
            break;
          }
        }
        if (Object.op_Inequality((Object) this.mCurrentArea, (Object) null))
        {
          this.mStateMachine.GotoState<WorldMapController.State_World2Area>();
          return;
        }
      }
      this.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
    }

    private void SetRadialBlurStrength(float t)
    {
      if (this.RadialBlurCurve != null && this.RadialBlurCurve.get_keys().Length > 0)
        this.RadialBlurEffect.Strength = this.RadialBlurCurve.Evaluate(t);
      else
        this.RadialBlurEffect.Strength = Mathf.Sin(t * 3.141593f);
    }

    private void Update()
    {
      this.mStateMachine.Update();
    }

    private class State_WorldSelect : State<WorldMapController>
    {
      public override void Begin(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mNextArea, (Object) null))
          return;
        self.mCurrentArea = self.mNextArea;
        self.mNextArea = (AreaMapController) null;
        self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
      }

      public override void Update(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
          return;
        self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
      }
    }

    private class State_World2Area : State<WorldMapController>
    {
      private float mTransition;
      private AreaMapController mTarget;
      private Vector2 mDesiredScale;
      private Vector2 mDesiredPosition;
      private Vector2 mTargetPosition;

      public override void Begin(WorldMapController self)
      {
        if (Object.op_Equality((Object) self.mCurrentArea, (Object) null))
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        this.mTarget = self.mCurrentArea;
        RectTransform transform = ((Component) this.mTarget).get_transform() as RectTransform;
        float num1 = (float) (1.0 / ((Transform) transform).get_localScale().x * self.mDefaultScale.x);
        float num2 = (float) (1.0 / ((Transform) transform).get_localScale().y * self.mDefaultScale.y);
        this.mDesiredScale = Vector2.op_Implicit(new Vector3(num1, num2));
        this.mTargetPosition = transform.get_anchoredPosition();
        this.mDesiredPosition = Vector2.op_UnaryNegation(transform.get_anchoredPosition());
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @this.mDesiredPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) ((^local1).x * (double) num1);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @this.mDesiredPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) ((^local2).y * (double) num2);
        if (!self.AutoSelectArea)
          return;
        self.AutoSelectArea = false;
        this.mTarget.SetOpacity(1f);
        self.mTransform.set_anchoredPosition(this.mDesiredPosition);
        ((Transform) self.mTransform).set_localScale(Vector2.op_Implicit(this.mDesiredScale));
        self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
      }

      public override void Update(WorldMapController self)
      {
        if (Object.op_Equality((Object) self.mCurrentArea, (Object) null))
        {
          self.mPrevArea = this.mTarget;
          self.mNextArea = (AreaMapController) null;
          self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.get_deltaTime());
          float opacity = Mathf.Sin((float) ((double) this.mTransition * 3.14159274101257 * 0.5));
          this.mTarget.SetOpacity(opacity);
          self.mTransform.set_anchoredPosition(Vector2.Lerp(self.mDefaultPosition, this.mDesiredPosition, opacity));
          ((Transform) self.mTransform).set_localScale(Vector2.op_Implicit(Vector2.Lerp(self.mDefaultScale, this.mDesiredScale, opacity)));
          self.SetRadialBlurStrength(this.mTransition);
          if (Object.op_Inequality((Object) self.RadialBlurEffect, (Object) null))
          {
            // ISSUE: variable of the null type
            __Null x = this.mTargetPosition.x;
            Rect rect1 = self.mTransform.get_rect();
            // ISSUE: explicit reference operation
            double width = (double) ((Rect) @rect1).get_width();
            float num1 = (float) (x / width + 0.5);
            // ISSUE: variable of the null type
            __Null y = this.mTargetPosition.y;
            Rect rect2 = self.mTransform.get_rect();
            // ISSUE: explicit reference operation
            double height = (double) ((Rect) @rect2).get_height();
            float num2 = (float) (y / height + 0.5);
            self.RadialBlurEffect.Focus = new Vector2(num1, num2);
          }
          if ((double) this.mTransition < 1.0)
            return;
          self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
        }
      }
    }

    private class State_AreaSelect : State<WorldMapController>
    {
      private AreaMapController mArea;

      public override void Begin(WorldMapController self)
      {
        this.mArea = self.mCurrentArea;
      }

      public override void Update(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mCurrentArea, (Object) this.mArea))
          return;
        self.mPrevArea = this.mArea;
        self.mNextArea = self.mCurrentArea;
        self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
      }
    }

    private class State_Area2World : State<WorldMapController>
    {
      private float mTransition;
      private Vector2 mStartScale;
      private Vector2 mStartPosition;

      public override void Begin(WorldMapController self)
      {
        if (Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
        {
          self.mPrevArea.SetOpacity(0.0f);
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        }
        else
        {
          this.mStartScale = Vector2.op_Implicit(((Transform) self.mTransform).get_localScale());
          this.mStartPosition = Vector2.op_Implicit(((Transform) self.mTransform).get_localPosition());
        }
      }

      public override void Update(WorldMapController self)
      {
        if (Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
        {
          self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.get_deltaTime());
          float num = Mathf.Sin((float) ((double) this.mTransition * 3.14159274101257 * 0.5));
          self.mPrevArea.SetOpacity(1f - num);
          self.mTransform.set_anchoredPosition(Vector2.Lerp(this.mStartPosition, self.mDefaultPosition, num));
          ((Transform) self.mTransform).set_localScale(Vector2.op_Implicit(Vector2.Lerp(this.mStartScale, self.mDefaultScale, num)));
          self.SetRadialBlurStrength(this.mTransition);
          if ((double) this.mTransition < 1.0)
            return;
          self.mPrevArea.SetOpacity(0.0f);
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        }
      }
    }
  }
}
