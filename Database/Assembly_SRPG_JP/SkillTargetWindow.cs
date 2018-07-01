// Decompiled with JetBrains decompiler
// Type: SRPG.SkillTargetWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SkillTargetWindow : MonoBehaviour
  {
    public SkillTargetWindow.TargetSelectEvent OnTargetSelect;
    public SkillTargetWindow.CancelEvent OnCancel;
    private WindowController mWC;

    public SkillTargetWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mWC = (WindowController) ((Component) this).GetComponent<WindowController>();
    }

    public void Show()
    {
      if (!Object.op_Inequality((Object) this.mWC, (Object) null))
        return;
      this.mWC.Open();
    }

    public void Hide()
    {
      if (!Object.op_Inequality((Object) this.mWC, (Object) null))
        return;
      this.mWC.Close();
    }

    public void ForceHide()
    {
      if (!Object.op_Inequality((Object) this.mWC, (Object) null))
        return;
      this.mWC.ForceClose();
    }

    public void UnitSelected()
    {
      if (this.OnTargetSelect == null)
        return;
      this.OnTargetSelect(false);
      this.Hide();
    }

    public void GridSelected()
    {
      if (this.OnTargetSelect == null)
        return;
      this.OnTargetSelect(true);
      this.Hide();
    }

    public void Cancel()
    {
      if (this.OnCancel == null)
        return;
      this.OnCancel();
      this.Hide();
    }

    public delegate void TargetSelectEvent(bool grid);

    public delegate void CancelEvent();
  }
}
