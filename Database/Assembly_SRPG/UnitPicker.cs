// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class UnitPicker : UIBehaviour
  {
    private Animator mAnimator;
    public string OpenTrigger;
    public string CloseTrigger;
    public float CloseDelay1;
    public float CloseDelay2;
    public ListItemEvents Item_Remove;
    public ListItemEvents Item_Inactive;
    public ListItemEvents Item_Active;

    public UnitPicker()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      base.Awake();
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    }

    protected virtual void Start()
    {
      base.Start();
      this.mAnimator.SetTrigger(this.OpenTrigger);
    }

    public void Refresh(List<UnitData> inactive, List<UnitData> active)
    {
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
    }
  }
}
