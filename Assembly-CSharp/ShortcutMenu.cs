// Decompiled with JetBrains decompiler
// Type: ShortcutMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;

[FlowNode.Pin(110, "InputOFF_Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(101, "InputON", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "InputOFF", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(111, "InputON_Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 0)]
public class ShortcutMenu : MonoBehaviour, IFlowInterface
{
  private RectTransform mRectTransform;
  public GameObject Badge_MenuOpen;
  public GameObject Badge_MenuClose;
  public GameObject Badge_Unit;
  public GameObject Badge_Gift;
  public GameObject Badge_DailyMission;
  private bool IsInput;

  public ShortcutMenu()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mRectTransform = ((Component) this).get_transform() as RectTransform;
    if (Object.op_Inequality((Object) this.Badge_MenuOpen, (Object) null))
      this.Badge_MenuOpen.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_MenuClose, (Object) null))
      this.Badge_MenuClose.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_Unit, (Object) null))
      this.Badge_Unit.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
      this.Badge_Gift.SetActive(false);
    if (!Object.op_Inequality((Object) this.Badge_DailyMission, (Object) null))
      return;
    this.Badge_DailyMission.SetActive(false);
  }

  private void Update()
  {
    if (this.IsInput && Input.GetMouseButtonDown(0))
    {
      Vector2 vector2;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mRectTransform, Vector2.op_Implicit(Input.get_mousePosition()), (Camera) null, ref vector2);
      Rect rect = this.mRectTransform.get_rect();
      // ISSUE: explicit reference operation
      if (!((Rect) @rect).Contains(vector2) && (!MonoSingleton<GameManager>.Instance.IsTutorial() || Object.op_Equality((Object) SGHighlightObject.Instance(), (Object) null)))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!Object.op_Inequality((Object) instanceDirect, (Object) null))
      return;
    bool flag = false;
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock) && Object.op_Inequality((Object) this.Badge_Unit, (Object) null))
    {
      this.Badge_Unit.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock));
      if (this.Badge_Unit.GetActive())
        flag = true;
    }
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.GiftBox) && Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
    {
      this.Badge_Gift.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.GiftBox));
      if (this.Badge_Gift.GetActive())
        flag = true;
    }
    if (Object.op_Inequality((Object) this.Badge_DailyMission, (Object) null))
    {
      this.Badge_DailyMission.SetActive(false);
      TrophyState[] trophyStates = instanceDirect.Player.TrophyStates;
      for (int index = 0; index < trophyStates.Length; ++index)
      {
        if (trophyStates[index].Param.IsShowBadge(trophyStates[index]))
        {
          this.Badge_DailyMission.SetActive(true);
          break;
        }
      }
      if (this.Badge_DailyMission.GetActive())
        flag = true;
    }
    if (Object.op_Inequality((Object) this.Badge_MenuOpen, (Object) null))
      this.Badge_MenuOpen.SetActive(flag);
    if (!Object.op_Inequality((Object) this.Badge_MenuClose, (Object) null))
      return;
    this.Badge_MenuClose.SetActive(flag);
  }

  public void Activated(int pinID)
  {
    if (pinID == 100)
    {
      this.IsInput = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }
    else
    {
      if (pinID != 101)
        return;
      this.IsInput = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }
  }
}
