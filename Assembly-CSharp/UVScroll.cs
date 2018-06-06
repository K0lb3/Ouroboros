// Decompiled with JetBrains decompiler
// Type: UVScroll
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/UVScroll")]
[RequireComponent(typeof (Renderer))]
[DisallowMultipleComponent]
public class UVScroll : MonoBehaviour
{
  public Vector2 Speed;
  private Vector2 mOffset;
  public Vector2 Limit;

  public UVScroll()
  {
    base.\u002Ector();
  }

  private void Update()
  {
    UVScroll uvScroll = this;
    uvScroll.mOffset = Vector2.op_Addition(uvScroll.mOffset, Vector2.op_Multiply(Time.get_deltaTime(), this.Speed));
    if (this.Limit.x != 0.0 && (double) Mathf.Abs((float) this.mOffset.x) >= this.Limit.x)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local = @this.mOffset;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local).x = (^local).x % this.Limit.x;
    }
    if (this.Limit.y != 0.0 && (double) Mathf.Abs((float) this.mOffset.y) >= this.Limit.y)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local = @this.mOffset;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local).y = (^local).y % this.Limit.y;
    }
    ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material().set_mainTextureOffset(this.mOffset);
  }
}
