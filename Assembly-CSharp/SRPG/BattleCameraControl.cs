// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraControl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      if (!Object.op_Inequality((Object) this.RotationSlider, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<float>) this.RotationSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnRotationValueChange)));
    }

    private void Update()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      float cameraYawRatio = instance.CameraYawRatio;
      if (Object.op_Inequality((Object) this.RotationSlider, (Object) null))
        this.RotationSlider.set_value(cameraYawRatio);
      if (Object.op_Inequality((Object) this.RotationScroll, (Object) null))
        this.RotationScroll.set_value(cameraYawRatio);
      if (Object.op_Inequality((Object) this.RotateLeft, (Object) null))
        ((Selectable) this.RotateLeft).set_interactable((double) cameraYawRatio > 1.0 / 1000.0);
      if (!Object.op_Inequality((Object) this.RotateRight, (Object) null))
        return;
      ((Selectable) this.RotateRight).set_interactable((double) cameraYawRatio < 0.999000012874603);
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
  }
}
