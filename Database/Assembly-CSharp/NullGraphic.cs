// Decompiled with JetBrains decompiler
// Type: NullGraphic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NullGraphic : Graphic
{
  public NullGraphic()
  {
    base.\u002Ector();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
  }
}
