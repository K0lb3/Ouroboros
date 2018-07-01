// Decompiled with JetBrains decompiler
// Type: SRPG.ResultMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ResultMask : MonoBehaviour
  {
    public RawImage ImgBg;

    public ResultMask()
    {
      base.\u002Ector();
    }

    public void SetBg(Texture2D tex)
    {
      if (!Object.op_Implicit((Object) this.ImgBg) || Object.op_Equality((Object) tex, (Object) null))
        return;
      this.ImgBg.set_texture((Texture) tex);
      ((Component) this.ImgBg).get_gameObject().SetActive(true);
    }
  }
}
