// Decompiled with JetBrains decompiler
// Type: GiziScroll
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GiziScroll : MonoBehaviour
{
  public Vector2 MinAspectSize;
  public float MinScroll;
  public float MinOffset;
  [Space(10f)]
  public Vector2 MaxAspectSize;
  public float MaxScroll;
  public float MaxOffset;
  [Space(10f)]
  public float MaxZ;
  public Camera ProjectionCamera;
  private float mScrollPos;
  private List<KeyValuePair<Transform, Vector3>> mChildren;

  public GiziScroll()
  {
    base.\u002Ector();
  }

  public float ScrollPos
  {
    set
    {
      value = Mathf.Clamp01(value);
      if ((double) this.mScrollPos == (double) value)
        return;
      this.mScrollPos = value;
      this.Layout();
    }
    get
    {
      return this.mScrollPos;
    }
  }

  private void Start()
  {
    Transform transform = ((Component) this).get_transform();
    for (int index = 0; index < transform.get_childCount(); ++index)
    {
      Transform child = transform.GetChild(index);
      this.mChildren.Add(new KeyValuePair<Transform, Vector3>(child, child.get_position()));
    }
    this.Layout();
  }

  private void Layout()
  {
    float num1 = (float) ((double) this.ScrollPos * 2.0 - 1.0);
    float num2 = (float) (this.MinAspectSize.x / this.MinAspectSize.y);
    float num3 = (float) (this.MaxAspectSize.x / this.MaxAspectSize.y);
    float num4 = Mathf.Clamp01((float) (((double) ((float) Screen.get_width() / (float) Screen.get_height()) - (double) num2) / ((double) num3 - (double) num2)));
    float num5 = Mathf.Lerp(this.MinOffset, this.MaxOffset, num4);
    float num6 = Mathf.Lerp(this.MinScroll, this.MaxScroll, num4);
    for (int index = 0; index < this.mChildren.Count; ++index)
    {
      Transform key = this.mChildren[index].Key;
      float num7 = Mathf.Abs((float) key.get_position().z / this.MaxZ);
      key.set_position(Vector3.op_Subtraction(this.mChildren[index].Value, Vector3.op_Multiply(Vector3.get_right(), (float) ((double) num1 * (double) num5 * (double) num7 + (double) num1 * (double) num6))));
      if (Object.op_Inequality((Object) this.ProjectionCamera, (Object) null))
      {
        UIProjector component = (UIProjector) ((Component) key).GetComponent<UIProjector>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.PreCull(this.ProjectionCamera);
      }
    }
  }

  private void OnValidate()
  {
    this.ScrollPos = Mathf.Clamp(this.ScrollPos, -1f, 1f);
  }
}
