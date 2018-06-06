// Decompiled with JetBrains decompiler
// Type: Image_Transparent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
