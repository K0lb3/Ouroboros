// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
