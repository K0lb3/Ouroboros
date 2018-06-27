// Decompiled with JetBrains decompiler
// Type: ImageArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ImageArray")]
public class ImageArray : Image
{
  public Sprite[] Images;
  private int mImageIndex;

  public ImageArray()
  {
    base.\u002Ector();
  }

  public int ImageIndex
  {
    get
    {
      return this.mImageIndex;
    }
    set
    {
      if (0 <= value && value < this.Images.Length)
      {
        this.set_sprite(this.Images[value]);
        this.mImageIndex = value;
      }
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
