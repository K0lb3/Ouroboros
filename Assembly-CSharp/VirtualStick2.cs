// Decompiled with JetBrains decompiler
// Type: VirtualStick2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class VirtualStick2 : MonoBehaviour
{
  private RectTransform mTransform;
  public RectTransform Knob;
  public float Radius;
  private Animator mAnimator;
  private Vector2 mInputDelta;
  private bool mVisible;
  public string VisibleBool;

  public VirtualStick2()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    this.mTransform = ((Component) this).get_transform() as RectTransform;
  }

  public void SetPosition(Vector2 position)
  {
    this.mTransform.set_anchoredPosition(position);
  }

  public bool Visible
  {
    set
    {
      if (this.Visible != value)
        this.mInputDelta = Vector2.get_zero();
      this.mVisible = value;
    }
    get
    {
      return this.mVisible;
    }
  }

  public Vector2 Delta
  {
    set
    {
      this.mInputDelta = value;
      if ((double) ((Vector2) @this.mInputDelta).get_magnitude() >= (double) this.Radius)
      {
        ((Vector2) @this.mInputDelta).Normalize();
        VirtualStick2 virtualStick2 = this;
        virtualStick2.mInputDelta = Vector2.op_Multiply(virtualStick2.mInputDelta, this.Radius);
      }
      if (!Object.op_Inequality((Object) this.Knob, (Object) null))
        return;
      this.Knob.set_anchoredPosition(this.mInputDelta);
    }
  }

  public Vector2 Velocity
  {
    get
    {
      return Vector2.op_Multiply(this.mInputDelta, 1f / this.Radius);
    }
  }

  private void Update()
  {
    if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
      return;
    this.mAnimator.SetBool(this.VisibleBool, this.mVisible);
  }
}
