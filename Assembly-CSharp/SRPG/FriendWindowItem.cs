// Decompiled with JetBrains decompiler
// Type: SRPG.FriendWindowItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class FriendWindowItem : MonoBehaviour
  {
    [SerializeField]
    private Toggle toggle;
    public FlowNode_MultiPlayFriendRequest FriendRequest;
    public JSON_MyPhotonPlayerParam PlayerParam;

    public FriendWindowItem()
    {
      base.\u002Ector();
    }

    public bool IsOn
    {
      get
      {
        return this.toggle.get_isOn();
      }
    }

    public bool Interactable
    {
      set
      {
        ((Selectable) this.toggle).set_interactable(value);
      }
    }

    private void Start()
    {
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(Set)));
    }

    public void Set(bool on)
    {
      this.FriendRequest.SetInteractable();
    }
  }
}
