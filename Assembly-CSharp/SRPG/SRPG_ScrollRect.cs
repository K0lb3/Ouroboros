// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/ScrollRect (SRPG)")]
  public class SRPG_ScrollRect : ScrollRect
  {
    public SRPG_ScrollRect()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      if (!Object.op_Inequality((Object) this.get_verticalScrollbar(), (Object) null))
        return;
      this.get_verticalScrollbar().set_value(1f);
    }
  }
}
