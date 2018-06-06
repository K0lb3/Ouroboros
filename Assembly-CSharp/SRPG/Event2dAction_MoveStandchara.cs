// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveStandchara
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵/移動(2D)", "立ち絵を指定した位置に移動させます。", 5592405, 4473992)]
  public class Event2dAction_MoveStandchara : EventAction
  {
    private const float MOVE_TIME = 0.5f;
    public string CharaID;
    public float MoveTime;
    public EventStandChara.PositionTypes MoveTo;
    private EventStandChara mStandChara;
    private Vector3 FromPosition;
    private Vector3 ToPosition;
    private float offset;

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mStandChara, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        if ((double) this.MoveTime <= 0.0)
          this.MoveTime = 0.5f;
        this.FromPosition = ((Component) this.mStandChara).get_transform().get_localPosition();
        RectTransform component = (RectTransform) ((Component) this.mStandChara).GetComponent<RectTransform>();
        RectTransform transform = ((Component) this.ActiveCanvas).get_transform() as RectTransform;
        Rect rect1 = transform.get_rect();
        // ISSUE: explicit reference operation
        double num1 = (double) ((Rect) @rect1).get_width() / 2.0;
        Rect rect2 = component.get_rect();
        // ISSUE: explicit reference operation
        double num2 = (double) ((Rect) @rect2).get_width() / 2.0;
        float num3 = (float) (num1 - num2);
        Rect rect3 = transform.get_rect();
        // ISSUE: explicit reference operation
        double num4 = (double) ((Rect) @rect3).get_width() / 2.0;
        Rect rect4 = component.get_rect();
        // ISSUE: explicit reference operation
        double num5 = (double) ((Rect) @rect4).get_width() / 2.0;
        float num6 = (float) (num4 + num5);
        if (this.MoveTo == EventStandChara.PositionTypes.Left)
          this.ToPosition = new Vector3(-num3, (float) this.FromPosition.y, (float) this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Center)
          this.ToPosition = new Vector3(0.0f, (float) this.FromPosition.y, (float) this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Right)
          this.ToPosition = new Vector3(num3, (float) this.FromPosition.y, (float) this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.OverLeft)
          this.ToPosition = new Vector3(-num6, (float) this.FromPosition.y, (float) this.FromPosition.z);
        if (this.MoveTo != EventStandChara.PositionTypes.OverRight)
          return;
        this.ToPosition = new Vector3(num6, (float) this.FromPosition.y, (float) this.FromPosition.z);
      }
    }

    public override void Update()
    {
      ((Component) this.mStandChara).get_transform().set_localPosition(Vector3.op_Addition(this.FromPosition, Vector3.Scale(Vector3.op_Subtraction(this.ToPosition, this.FromPosition), new Vector3(this.offset, this.offset, this.offset))));
      this.offset += Time.get_deltaTime() / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      ((Component) this.mStandChara).get_transform().set_localPosition(this.ToPosition);
      this.ActivateNext();
    }
  }
}
