// Decompiled with JetBrains decompiler
// Type: SRPG.PulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PulldownItem : MonoBehaviour
  {
    public Text Text;
    public Graphic Graphic;
    public int Value;
    public Image Overray;

    public PulldownItem()
    {
      base.\u002Ector();
    }

    public virtual void OnStatusChanged(bool enabled)
    {
    }
  }
}
