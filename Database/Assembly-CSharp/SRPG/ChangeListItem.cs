// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
