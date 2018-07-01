// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GalleryItem : MonoBehaviour
  {
    [SerializeField]
    private Button Button;
    [SerializeField]
    private GameObject UnknownImage;

    public GalleryItem()
    {
      base.\u002Ector();
    }

    public void SetAvailable(bool isAvailable)
    {
      if (Object.op_Inequality((Object) this.Button, (Object) null))
        ((Selectable) this.Button).set_interactable(isAvailable);
      if (!Object.op_Inequality((Object) this.UnknownImage, (Object) null))
        return;
      this.UnknownImage.SetActive(!isAvailable);
    }
  }
}
