// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayFriendRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[FlowNode.Pin(100, "実行", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(102, "一括申請用プレハブ作成完了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(103, "申請可能", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(104, "申請不可能", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(0, "開始", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(3, "フレンド申請が可能か", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/MultiPlayFriendRequest", 32741)]
[FlowNode.Pin(2, "一括申請用プレハブ作成", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "次の人", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlayFriendRequest : FlowNode
{
  private List<FriendWindowItem> FriendItemList = new List<FriendWindowItem>();
  private int mCurrentIndex;
  [SerializeField]
  private GameObject Template;
  [SerializeField]
  private Button OkButton;

  private int SearchTarget(int startIndex)
  {
    MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
    List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
    if (myPlayersStarted == null || MonoSingleton<GameManager>.Instance.Player == null)
      return -1;
    List<MultiFuid> multiFuids = MonoSingleton<GameManager>.Instance.Player.MultiFuids;
    for (int index = startIndex; index < myPlayersStarted.Count; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_MultiPlayFriendRequest.\u003CSearchTarget\u003Ec__AnonStorey2CB targetCAnonStorey2Cb = new FlowNode_MultiPlayFriendRequest.\u003CSearchTarget\u003Ec__AnonStorey2CB();
      // ISSUE: reference to a compiler-generated field
      targetCAnonStorey2Cb.startedPlayer = myPlayersStarted[index];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (targetCAnonStorey2Cb.startedPlayer != null && targetCAnonStorey2Cb.startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(targetCAnonStorey2Cb.startedPlayer.FUID))
      {
        // ISSUE: reference to a compiler-generated method
        MultiFuid multiFuid = multiFuids != null ? multiFuids.Find(new Predicate<MultiFuid>(targetCAnonStorey2Cb.\u003C\u003Em__2A9)) : (MultiFuid) null;
        if (multiFuid != null && multiFuid.status.Equals("none"))
          return index;
      }
    }
    return -1;
  }

  private void Output()
  {
    if (!MonoSingleton<GameManager>.Instance.Player.IsRequestFriend())
    {
      this.mCurrentIndex = -1;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(101);
    }
    else if (this.mCurrentIndex < 0)
    {
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(101);
    }
    else
    {
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      GlobalVars.SelectedSupport.Set(myPlayersStarted != null ? myPlayersStarted[this.mCurrentIndex].CreateSupportData() : (SupportData) null);
      GlobalVars.SelectedFriendID = GlobalVars.SelectedSupport.Get() != null ? myPlayersStarted[this.mCurrentIndex].FUID : (string) null;
      this.ActivateOutputLinks(100);
    }
  }

  private void CheckFriend()
  {
    if (!MonoSingleton<GameManager>.Instance.Player.IsRequestFriend() || this.SearchTarget(0) < 0)
    {
      this.mCurrentIndex = -1;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(104);
    }
    else
      this.ActivateOutputLinks(103);
  }

  public void CreateFriendItem()
  {
    List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
    this.mCurrentIndex = -1;
    for (int index = 0; index < myPlayersStarted.Count; ++index)
    {
      this.mCurrentIndex = this.SearchTarget(this.mCurrentIndex + 1);
      if (this.mCurrentIndex >= 0)
      {
        SupportData data = myPlayersStarted != null ? myPlayersStarted[this.mCurrentIndex].CreateSupportData() : (SupportData) null;
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Template);
        gameObject.get_transform().SetParent(this.Template.get_transform().get_parent(), false);
        DataSource.Bind<SupportData>(gameObject, data);
        FriendWindowItem component = (FriendWindowItem) gameObject.GetComponent<FriendWindowItem>();
        component.FriendRequest = this;
        component.PlayerParam = myPlayersStarted[this.mCurrentIndex];
        this.FriendItemList.Add(component);
      }
      else
        break;
    }
    GameParameter.UpdateAll(((Component) this.Template.get_transform().get_parent()).get_gameObject());
    this.Template.SetActive(false);
  }

  public void SetInteractable()
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OkButton, (UnityEngine.Object) null))
      return;
    ((Selectable) this.OkButton).set_interactable(false);
    for (int index = 0; index < this.FriendItemList.Count; ++index)
    {
      if (this.FriendItemList[index].IsOn)
      {
        ((Selectable) this.OkButton).set_interactable(true);
        break;
      }
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 0:
        this.mCurrentIndex = this.SearchTarget(0);
        this.Output();
        break;
      case 1:
        this.mCurrentIndex = this.SearchTarget(this.mCurrentIndex + 1);
        this.Output();
        break;
      case 2:
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OkButton, (UnityEngine.Object) null))
          ((Selectable) this.OkButton).set_interactable(false);
        this.CreateFriendItem();
        break;
      case 3:
        this.CheckFriend();
        break;
    }
  }
}
