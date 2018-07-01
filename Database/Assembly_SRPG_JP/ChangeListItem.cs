// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChangeListItem : MonoBehaviour
  {
    [FourCC]
    public int ID;
    public Text Label;
    public Text ValOld;
    public Text ValNew;
    public Text Diff;

    public ChangeListItem()
    {
      base.\u002Ector();
    }
  }
}
