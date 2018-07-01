// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Telop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/テロップ(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Telop : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/TelopBubble";
    [HideInInspector]
    public string ActorID = "2DPlus";
    private const float DialogPadding = 20f;
    [StringIsTextIDPopup(false)]
    public string TextID;
    public bool TextColor;
    public Event2dAction_Telop.TextPositionTypes TextPosition;
    private string mTextData;
    private EventTelopBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Telop.\u003CPreloadAssets\u003Ec__IteratorAB()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble = EventTelopBubble.Find(this.ActorID);
      if (Object.op_Equality((Object) this.mBubble, (Object) null) && this.mBubbleResource != null)
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventTelopBubble;
        ((Component) this.mBubble).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).get_transform().SetAsLastSibling();
        ((Component) this.mBubble).get_gameObject().SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
    }

    private Vector2 CalcBubblePosition(Vector3 position)
    {
      Vector2 vector2 = Vector2.op_Implicit(Camera.get_main().WorldToScreenPoint(position));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @vector2;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).x = (__Null) ((^local1).x / (double) Screen.get_width());
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @vector2;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local2).y = (__Null) ((^local2).y / (double) Screen.get_height());
      return vector2;
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && !((Component) this.mBubble).get_gameObject().get_activeInHierarchy())
      {
        ((Component) this.mBubble).get_gameObject().SetActive(true);
        RectTransform component = (RectTransform) ((Component) this.mBubble).GetComponent<RectTransform>();
        RectTransform rectTransform = component;
        Vector2 vector2_1 = new Vector2(0.5f, 0.5f);
        component.set_anchorMax(vector2_1);
        Vector2 vector2_2 = vector2_1;
        rectTransform.set_anchorMin(vector2_2);
        component.set_pivot(new Vector2(0.5f, 0.5f));
        component.set_anchoredPosition(new Vector2(0.0f, 0.0f));
      }
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      RectTransform transform1 = ((Component) this.mBubble).get_transform() as RectTransform;
      for (int index = 0; index < EventTelopBubble.Instances.Count; ++index)
      {
        RectTransform transform2 = ((Component) EventTelopBubble.Instances[index]).get_transform() as RectTransform;
        if (Object.op_Inequality((Object) transform1, (Object) transform2))
        {
          Rect rect = transform1.get_rect();
          // ISSUE: explicit reference operation
          if (((Rect) @rect).Overlaps(transform2.get_rect()))
            EventTelopBubble.Instances[index].Close();
        }
      }
      this.mBubble.TextColor = this.TextColor;
      this.mBubble.TextPosition = this.TextPosition;
      this.mBubble.SetBody(this.mTextData);
      this.mBubble.Open();
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = (TacticsUnitController) actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          Unit unit = component.Unit;
          if (unit != null)
            return unit.UnitName;
        }
      }
      return actorID;
    }

    public override bool Forward()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
      {
        if (this.mBubble.Finished)
        {
          this.mBubble.Forward();
          this.ActivateNext();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public enum TextPositionTypes
    {
      Left,
      Center,
      Right,
    }

    public enum TextSpeedTypes
    {
      Normal,
      Slow,
      Fast,
    }
  }
}
