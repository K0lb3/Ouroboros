// Decompiled with JetBrains decompiler
// Type: SRPG.CondDescTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class CondDescTooltip : MonoBehaviour
  {
    public CondDescTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
    public ImageArray ImageCond;
    public float PosLeftOffset;
    public float PosRightOffset;
    public float PosUpOffset;
    private static Tooltip mTooltip;
    private CanvasGroup[] mParentCgs;

    public CondDescTooltip()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      UIEventListener uiEventListener = ((Component) this).RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowTooltip);
      if (Object.op_Implicit((Object) this.ImageCond))
        return;
      this.ImageCond = (ImageArray) ((Component) this).GetComponent<ImageArray>();
    }

    private CanvasGroup[] ParentCgs
    {
      get
      {
        if (this.mParentCgs == null)
          this.mParentCgs = (CanvasGroup[]) ((Component) this).GetComponentsInParent<CanvasGroup>();
        return this.mParentCgs;
      }
    }

    private void ShowTooltip(PointerEventData event_data)
    {
      if (!Object.op_Implicit((Object) this.PrefabTooltip) || Object.op_Implicit((Object) CondDescTooltip.mTooltip))
        return;
      RaycastResult pointerCurrentRaycast = event_data.get_pointerCurrentRaycast();
      // ISSUE: explicit reference operation
      if (Object.op_Equality((Object) ((RaycastResult) @pointerCurrentRaycast).get_gameObject(), (Object) null))
        return;
      CanvasGroup[] parentCgs = this.ParentCgs;
      if (parentCgs != null)
      {
        foreach (CanvasGroup canvasGroup in parentCgs)
        {
          if ((double) canvasGroup.get_alpha() <= 0.0)
            return;
        }
      }
      if (Object.op_Equality((Object) CondDescTooltip.mTooltip, (Object) null))
        CondDescTooltip.mTooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.PrefabTooltip);
      else
        CondDescTooltip.mTooltip.ResetPosition();
      if (!Object.op_Inequality((Object) CondDescTooltip.mTooltip.TooltipText, (Object) null) || !Object.op_Implicit((Object) this.ImageCond))
        return;
      int imageIndex = this.ImageCond.ImageIndex;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (imageIndex >= 0 && imageIndex < Unit.StrNameUnitConds.Length)
        empty1 = LocalizedText.Get(Unit.StrNameUnitConds[imageIndex]);
      if (imageIndex >= 0 && imageIndex < Unit.StrDescUnitConds.Length)
        empty2 = LocalizedText.Get(Unit.StrDescUnitConds[imageIndex]);
      CondDescTooltip.mTooltip.TooltipText.set_text(string.Format(LocalizedText.Get("quest.BUD_COND_DESC"), (object) empty1, (object) empty2));
      CondDescTooltip.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new CondDescTooltip.\u003CResetPosiotion\u003Ec__Iterator5F()
      {
        \u003C\u003Ef__this = this
      };
    }

    public enum eDispType
    {
      LEFT,
      RIGHT,
      UP,
    }
  }
}
