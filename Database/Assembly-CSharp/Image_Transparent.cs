// Decompiled with JetBrains decompiler
// Type: Image_Transparent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Image (透明)")]
public class Image_Transparent : Image
{
  public Image_Transparent()
  {
    base.\u002Ector();
  }

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    if (Object.op_Inequality((Object) this.get_sprite(), (Object) null))
      base.OnPopulateMesh(toFill);
    else
      toFill.Clear();
  }
}
