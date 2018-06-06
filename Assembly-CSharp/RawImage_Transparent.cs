// Decompiled with JetBrains decompiler
// Type: RawImage_Transparent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/RawImage (透明)")]
public class RawImage_Transparent : RawImage
{
  public string Preview;

  public RawImage_Transparent()
  {
    base.\u002Ector();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    if (Object.op_Inequality((Object) this.get_texture(), (Object) null))
      base.OnPopulateMesh(vh);
    else
      vh.Clear();
  }
}
