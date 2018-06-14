// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleCameraControl : MonoBehaviour
  {
    public Button RotateLeft;
    public Button RotateRight;
    public Slider RotationSlider;
    public Scrollbar RotationScroll;
    public float RotateAmount;
    public float RotateTime;
    private Animator m_Animator;
    private Canvas m_Canvas;
    private GraphicRaycaster m_GraphicRaycatser;
    private bool m_Disp;

    public BattleCameraControl()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.RotateLeft, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.RotateLeft.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRotateLeft)));
      }
      if (Object.op_Inequality((Object) this.RotateRight, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.RotateRight.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRotateRight)));
      }
      if (Object.op_Inequality((Object) this.RotationSlider, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<float>) this.RotationSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnRotationValueChange)));
      }
      this.m_Animator = (Animator) ((Component) this).GetComponent<Animator>();
      this.m_Canvas = (Canvas) ((Component) this).GetComponent<Canvas>();
      this.m_GraphicRaycatser = (GraphicRaycaster) ((Component) this).GetComponent<GraphicRaycaster>();
      this.SetDisp(false);
    }

    private void Update()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.RotateLeft, (Object) null))
        ((Selectable) this.RotateLeft).set_interactable(instance.isCameraLeftMove);
      if (Object.op_Inequality((Object) this.RotateRight, (Object) null))
        ((Selectable) this.RotateRight).set_interactable(instance.isCameraRightMove);
      if (!Object.op_Inequality((Object) this.m_Animator, (Object) null))
        return;
      bool flag = this.m_Animator.GetBool("open");
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      if (flag)
      {
        if (Object.op_Inequality((Object) this.m_Canvas, (Object) null))
          ((Behaviour) this.m_Canvas).set_enabled(true);
        // ISSUE: explicit reference operation
        if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0 || !Object.op_Inequality((Object) this.m_GraphicRaycatser, (Object) null))
          return;
        ((Behaviour) this.m_GraphicRaycatser).set_enabled(true);
      }
      else
      {
        // ISSUE: explicit reference operation
        if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() >= 1.0 && Object.op_Inequality((Object) this.m_Canvas, (Object) null))
          ((Behaviour) this.m_Canvas).set_enabled(false);
        if (!Object.op_Inequality((Object) this.m_GraphicRaycatser, (Object) null))
          return;
        ((Behaviour) this.m_GraphicRaycatser).set_enabled(false);
      }
    }

    private void OnRotateLeft()
    {
      SceneBattle.Instance.RotateCamera(-this.RotateAmount, this.RotateTime);
    }

    private void OnRotateRight()
    {
      SceneBattle.Instance.RotateCamera(this.RotateAmount, this.RotateTime);
    }

    private void OnRotationValueChange(float value)
    {
    }

    public void SetDisp(bool value)
    {
      if (value && SceneBattle.Instance.isUpView)
        value = false;
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool("open", value);
    }

    public void OnEventCall(string key, string value)
    {
      string key1 = key;
      if (key1 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (BattleCameraControl.\u003C\u003Ef__switch\u0024mapB == null)
      {
        // ISSUE: reference to a compiler-generated field
        BattleCameraControl.\u003C\u003Ef__switch\u0024mapB = new Dictionary<string, int>(2)
        {
          {
            "DISP",
            0
          },
          {
            "FULLROTATION",
            1
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!BattleCameraControl.\u003C\u003Ef__switch\u0024mapB.TryGetValue(key1, out num))
        return;
      switch (num)
      {
        case 0:
          if (value == "on")
          {
            this.SetDisp(true);
            break;
          }
          this.SetDisp(false);
          break;
        case 1:
          if (value == "on")
          {
            SceneBattle.Instance.SetFullRotationCamera(true);
            break;
          }
          SceneBattle.Instance.SetFullRotationCamera(false);
          break;
      }
    }
  }
}
