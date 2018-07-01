// Decompiled with JetBrains decompiler
// Type: SRPG.FriendWindowItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class FriendWindowItem : MonoBehaviour
  {
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Toggle block;
    [SerializeField]
    private GameObject FriendMark;
    [SerializeField]
    private GameObject BlockMark;
    [NonSerialized]
    public FlowNode_MultiPlayFriendRequest FriendRequest;
    [NonSerialized]
    public JSON_MyPhotonPlayerParam PlayerParam;
    [NonSerialized]
    public SupportData Support;
    private MultiFuid m_Friend;

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

    public bool IsBlockOn
    {
      get
      {
        return this.block.get_isOn();
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.toggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(Refresh)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.block.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(Refresh)));
    }

    public void Refresh(bool on = false)
    {
      bool flag;
      if (this.Support != null)
      {
        flag = this.Support.mIsFriend == 1;
      }
      else
      {
        if (this.m_Friend == null)
          this.m_Friend = MonoSingleton<GameManager>.Instance.Player.MultiFuids?.Find((Predicate<MultiFuid>) (f =>
          {
            if (f.fuid != null)
              return f.fuid.Equals(this.PlayerParam.FUID);
            return false;
          }));
        flag = this.m_Friend != null && this.m_Friend.status.Equals("friend");
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendMark, (UnityEngine.Object) null))
        this.FriendMark.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.toggle, (UnityEngine.Object) null))
        ((Selectable) this.toggle).set_interactable(!flag && !this.IsBlockOn);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null))
      {
        ((Selectable) this.block).set_interactable(!this.toggle.get_isOn());
        ((Component) this.block).get_gameObject().SetActive(this.Support == null);
      }
      this.RefreshBlockMark(!flag && this.IsBlockOn);
    }

    private void RefreshBlockMark(bool _active)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BlockMark, (UnityEngine.Object) null))
        return;
      this.BlockMark.SetActive(_active);
    }
  }
}
