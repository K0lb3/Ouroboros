// Decompiled with JetBrains decompiler
// Type: SRPG.BookmarkToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class BookmarkToggleButton : MonoBehaviour
  {
    [SerializeField]
    private GameObject OnImage;
    [SerializeField]
    private GameObject OffImage;
    [SerializeField]
    private GameObject Shadow;

    public BookmarkToggleButton()
    {
      base.\u002Ector();
    }

    public void Activate(bool doActivate)
    {
      this.OnImage.SetActive(doActivate);
      this.OffImage.SetActive(!doActivate);
    }

    public void EnableShadow(bool enabled)
    {
      this.Shadow.SetActive(enabled);
    }
  }
}
