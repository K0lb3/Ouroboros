// Decompiled with JetBrains decompiler
// Type: SRPG.BookmarkToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
