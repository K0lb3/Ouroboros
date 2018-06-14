// Decompiled with JetBrains decompiler
// Type: SRPG.EquipTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EquipTree : MonoBehaviour
  {
    public Image CursorImage;

    public EquipTree()
    {
      base.\u002Ector();
    }

    public void SetIsLast(bool is_last)
    {
      ((Behaviour) this.CursorImage).set_enabled(is_last);
    }
  }
}
