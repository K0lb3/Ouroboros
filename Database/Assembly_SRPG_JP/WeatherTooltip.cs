// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class WeatherTooltip : MonoBehaviour
  {
    public WeatherTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
    public float PosLeftOffset;
    public float PosRightOffset;
    public float PosUpOffset;
    private static Tooltip mTooltip;
    private CanvasGroup[] mParentCgs;

    public WeatherTooltip()
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
      if (!Object.op_Implicit((Object) this.PrefabTooltip) || Object.op_Implicit((Object) WeatherTooltip.mTooltip))
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
      if (Object.op_Equality((Object) WeatherTooltip.mTooltip, (Object) null))
        WeatherTooltip.mTooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.PrefabTooltip);
      else
        WeatherTooltip.mTooltip.ResetPosition();
      WeatherParam dataOfClass = DataSource.FindDataOfClass<WeatherParam>(((Component) this).get_gameObject(), (WeatherParam) null);
      if (!Object.op_Inequality((Object) WeatherTooltip.mTooltip.TooltipText, (Object) null) || dataOfClass == null)
        return;
      WeatherTooltip.mTooltip.TooltipText.set_text(string.Format(LocalizedText.Get("quest.WEATHER_DESC"), (object) dataOfClass.Name, (object) dataOfClass.Expr));
      WeatherTooltip.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WeatherTooltip.\u003CResetPosiotion\u003Ec__Iterator2B()
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
