// Decompiled with JetBrains decompiler
// Type: ImageArray
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ImageArray")]
public class ImageArray : Image
{
  public Sprite[] Images;

  public ImageArray()
  {
    base.\u002Ector();
  }

  public int ImageIndex
  {
    set
    {
      if (0 <= value && value < this.Images.Length)
        this.set_sprite(this.Images[value]);
      else
        Debug.LogError((object) "範囲外のインデックスが指定されました。");
    }
  }

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    if (Object.op_Equality((Object) this.get_sprite(), (Object) null))
      toFill.Clear();
    else
      base.OnPopulateMesh(toFill);
  }
}
