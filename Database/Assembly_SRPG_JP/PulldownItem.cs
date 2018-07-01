// Decompiled with JetBrains decompiler
// Type: SRPG.PulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
