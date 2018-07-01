// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPreview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class UnitPreview : UnitController
  {
    public int DefaultLayer = GameUtility.LayerDefault;
    private const string ID_IDLE = "idle";
    private const string ID_ACTION = "action";
    public bool PlayAction;
    private bool mPlayingAction;

    protected override void Start()
    {
      this.KeepUnitHidden = true;
      this.LoadEquipments = true;
      base.Start();
    }

    protected override void PostSetup()
    {
      base.PostSetup();
      this.LoadUnitAnimationAsync("idle", "unit_info_idle0", true, false);
      this.LoadUnitAnimationAsync("action", "unit_info_act0", true, false);
      this.StartCoroutine(this.LoadThread());
    }

    protected override void Update()
    {
      base.Update();
      if (this.IsLoading)
        return;
      if (this.PlayAction)
      {
        this.PlayAction = false;
        this.PlayAnimation("action", false, 0.1f, 0.0f);
        this.mPlayingAction = true;
      }
      else
      {
        if (!this.mPlayingAction || (double) this.GetRemainingTime("action") > 0.0)
          return;
        this.PlayAnimation("idle", true, 0.1f, 0.0f);
        this.mPlayingAction = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitPreview.\u003CLoadThread\u003Ec__Iterator86()
      {
        \u003C\u003Ef__this = this
      };
    }

    public Transform GetHeadPosition()
    {
      if (Object.op_Inequality((Object) this.Rig, (Object) null))
        return GameUtility.findChildRecursively(((Component) this).get_transform(), this.Rig.Head);
      return (Transform) null;
    }
  }
}
