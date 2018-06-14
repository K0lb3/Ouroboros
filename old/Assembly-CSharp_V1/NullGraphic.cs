// Decompiled with JetBrains decompiler
// Type: NullGraphic
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
