// Decompiled with JetBrains decompiler
// Type: SRPG.EquipTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
