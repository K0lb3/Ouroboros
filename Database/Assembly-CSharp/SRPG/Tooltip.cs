// Decompiled with JetBrains decompiler
// Type: SRPG.Tooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Tooltip : MonoBehaviour
  {
    public static Vector2 TooltipPosition;
    public RectTransform Body;
    public RectTransform SizeBody;
    public Text TooltipText;
    public string CloseTrigger;
    public float DestroyDelay;
    private Animator mAnimator;
    private bool mDestroying;
    public bool CloseOnPress;

    public Tooltip()
    {
      base.\u002Ector();
    }

    public static void SetTooltipPosition(RectTransform rect, Vector2 localPos)
    {
      Vector2 vector2 = Vector2.op_Implicit(((Transform) rect).TransformPoint(Vector2.op_Implicit(localPos)));
      CanvasScaler componentInParent = (CanvasScaler) ((Component) rect).GetComponentInParent<CanvasScaler>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        Vector3 localScale = ((Component) componentInParent).get_transform().get_localScale();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @vector2;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (^local1).x / localScale.x;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @vector2;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (^local2).y / localScale.y;
      }
      Tooltip.TooltipPosition = vector2;
    }

    public void ResetPosition()
    {
      if (!Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      this.Body.set_anchorMin(Vector2.get_zero());
      this.Body.set_anchorMax(Vector2.get_zero());
      this.Body.set_anchoredPosition(Tooltip.TooltipPosition);
    }

    public Vector2 BodySize
    {
      get
      {
        if (Object.op_Implicit((Object) this.SizeBody))
          return this.SizeBody.get_sizeDelta();
        return Vector2.get_zero();
      }
    }

    public bool EnableDisp
    {
      set
      {
        if (!Object.op_Implicit((Object) this.SizeBody))
          return;
        CanvasGroup component = (CanvasGroup) ((Component) this.SizeBody).GetComponent<CanvasGroup>();
        if (!Object.op_Implicit((Object) component))
          return;
        component.set_alpha(!value ? 0.0f : 1f);
      }
    }

    private void Start()
    {
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
      this.ResetPosition();
    }

    public void Close()
    {
      this.mDestroying = true;
      if (Object.op_Inequality((Object) this.mAnimator, (Object) null) && !string.IsNullOrEmpty(this.CloseTrigger))
        this.mAnimator.SetTrigger(this.CloseTrigger);
      if ((double) Time.get_timeScale() != 0.0)
        Object.Destroy((Object) ((Component) this).get_gameObject(), this.DestroyDelay);
      else
        Object.Destroy((Object) ((Component) this).get_gameObject());
    }

    private void Update()
    {
      if (this.mDestroying)
        return;
      if (this.CloseOnPress)
      {
        if (!Input.GetMouseButton(0))
          return;
      }
      else if (Input.GetMouseButton(0))
        return;
      this.Close();
    }
  }
}
