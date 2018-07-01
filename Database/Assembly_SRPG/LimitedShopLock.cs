// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
