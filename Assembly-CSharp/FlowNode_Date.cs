// Decompiled with JetBrains decompiler
// Type: FlowNode_Date
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;

[FlowNode.Pin(99, "Reset", FlowNode.PinTypes.Input, 99)]
[FlowNode.Pin(1004, "> CompareValue", FlowNode.PinTypes.Output, 1004)]
[FlowNode.Pin(105, "CompareYear", FlowNode.PinTypes.Input, 105)]
[FlowNode.Pin(1005, ">= CompareValue", FlowNode.PinTypes.Output, 1005)]
[FlowNode.Pin(502, "日が変わった", FlowNode.PinTypes.Output, 502)]
[FlowNode.Pin(503, "日も時刻も変わらず", FlowNode.PinTypes.Output, 503)]
[FlowNode.Pin(1000, "== CompareValue", FlowNode.PinTypes.Output, 1000)]
[FlowNode.Pin(1001, "!= CompareValue", FlowNode.PinTypes.Output, 1001)]
[FlowNode.Pin(1002, "< CompareValue", FlowNode.PinTypes.Output, 1002)]
[FlowNode.Pin(1003, "<= CompareValue", FlowNode.PinTypes.Output, 1003)]
[FlowNode.Pin(103, "CompareMonth", FlowNode.PinTypes.Input, 103)]
[FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(102, "CompareHour", FlowNode.PinTypes.Input, 102)]
[FlowNode.NodeType("System/Date", 32741)]
[FlowNode.Pin(501, "時刻が変わった(日は変わらず)", FlowNode.PinTypes.Output, 501)]
[FlowNode.Pin(500, "Updated", FlowNode.PinTypes.Output, 500)]
[FlowNode.Pin(101, "CompareDate", FlowNode.PinTypes.Input, 101)]
[FlowNode.Pin(104, "CompareDay", FlowNode.PinTypes.Input, 104)]
public class FlowNode_Date : FlowNode
{
  public int CompareMonth = 10;
  public int CompareDay = 16;
  public int CompareYear;
  public int CompareHour;
  private static DateTime sDate;

  protected override void Awake()
  {
    base.Awake();
    FlowNode_Date.sDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 99:
        FlowNode_Date.sDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        this.ActivateOutputLinks(500);
        break;
      case 100:
        DateTime sDate = FlowNode_Date.sDate;
        FlowNode_Date.sDate = TimeManager.ServerTime;
        this.CheckUpdate(FlowNode_Date.sDate, sDate);
        this.ActivateOutputLinks(500);
        break;
      case 101:
        DateTime compare = new DateTime(this.CompareYear > 0 ? this.CompareYear : FlowNode_Date.sDate.Year, this.CompareMonth, this.CompareDay);
        this.CheckResult(FlowNode_Date.sDate, compare);
        break;
      case 102:
        this.CheckResult(FlowNode_Date.sDate.Hour, this.CompareHour);
        break;
      case 103:
        this.CheckResult(FlowNode_Date.sDate.Month, this.CompareMonth);
        break;
      case 104:
        this.CheckResult(FlowNode_Date.sDate.Day, this.CompareDay);
        break;
      case 105:
        this.CheckResult(FlowNode_Date.sDate.Year, this.CompareYear);
        break;
    }
  }

  private void CheckResult(DateTime target, DateTime compare)
  {
    if (target == compare)
      this.ActivateOutputLinks(1000);
    else
      this.ActivateOutputLinks(1001);
    if (target < compare)
      this.ActivateOutputLinks(1002);
    if (target <= compare)
      this.ActivateOutputLinks(1003);
    if (target > compare)
      this.ActivateOutputLinks(1004);
    if (!(target >= compare))
      return;
    this.ActivateOutputLinks(1005);
  }

  private void CheckResult(int target, int compare)
  {
    if (target == compare)
      this.ActivateOutputLinks(1000);
    else
      this.ActivateOutputLinks(1001);
    if (target < compare)
      this.ActivateOutputLinks(1002);
    if (target <= compare)
      this.ActivateOutputLinks(1003);
    if (target > compare)
      this.ActivateOutputLinks(1004);
    if (target < compare)
      return;
    this.ActivateOutputLinks(1005);
  }

  private DateTime CheckUpdate(DateTime dt, DateTime dtOld)
  {
    bool flag1 = dt.Year != dtOld.Year || dt.Month != dtOld.Month || dt.Day != dtOld.Day;
    bool flag2 = flag1 || dt.Hour != dtOld.Hour;
    if (flag1)
      this.ActivateOutputLinks(502);
    else if (flag2)
      this.ActivateOutputLinks(501);
    else
      this.ActivateOutputLinks(503);
    return dt;
  }
}
