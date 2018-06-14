// Decompiled with JetBrains decompiler
// Type: NullGraphic2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NullGraphic2 : Graphic
{
  public NullGraphic2()
  {
    base.\u002Ector();
  }

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    this.set_color(new Color(0.0f, 0.0f, 0.0f, 0.0f));
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    base.OnPopulateMesh(vh);
  }
}
