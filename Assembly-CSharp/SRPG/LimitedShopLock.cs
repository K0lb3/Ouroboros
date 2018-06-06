// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopLock
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class LimitedShopLock : MonoBehaviour
  {
    [SerializeField]
    private GameObject LockObject;
    private Button mButton;

    public LimitedShopLock()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      Button component = (Button) ((Component) this).GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mButton = component;
    }

    private void Start()
    {
      this.UpdateLockState();
    }

    private void UpdateLockState()
    {
      if (Object.op_Equality((Object) this.mButton, (Object) null))
        return;
      this.LockObject.SetActive(!((Selectable) this.mButton).get_interactable());
    }
  }
}
