// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
