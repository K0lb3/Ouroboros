// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopLock : MonoBehaviour
  {
    [SerializeField]
    private GameObject LockObject;

    public EventShopLock()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.LockObject, (Object) null))
        this.LockObject.SetActive(!(bool) GlobalVars.IsEventShopOpen);
      Button component = (Button) ((Component) this).get_gameObject().GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Selectable) component).set_interactable((bool) GlobalVars.IsEventShopOpen);
    }
  }
}
