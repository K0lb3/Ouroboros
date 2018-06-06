// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayFriendRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.NodeType("Multi/MultiPlayFriendRequest", 32741)]
[FlowNode.Pin(0, "開始", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "次の人", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "実行", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlayFriendRequest : FlowNode
{
  private int mCurrentIndex;

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
      FlowNode_MultiPlayFriendRequest.\u003CSearchTarget\u003Ec__AnonStorey20D targetCAnonStorey20D = new FlowNode_MultiPlayFriendRequest.\u003CSearchTarget\u003Ec__AnonStorey20D();
      // ISSUE: reference to a compiler-generated field
      targetCAnonStorey20D.startedPlayer = myPlayersStarted[index];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (targetCAnonStorey20D.startedPlayer != null && targetCAnonStorey20D.startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(targetCAnonStorey20D.startedPlayer.FUID))
      {
        // ISSUE: reference to a compiler-generated method
        MultiFuid multiFuid = multiFuids != null ? multiFuids.Find(new Predicate<MultiFuid>(targetCAnonStorey20D.\u003C\u003Em__1F3)) : (MultiFuid) null;
        if (multiFuid != null && multiFuid.status.Equals("none"))
          return index;
      }
    }
    return -1;
  }

  private void Output()
  {
    if (this.mCurrentIndex < 0)
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

  public override void OnActivate(int pinID)
  {
    if (pinID == 0)
    {
      this.mCurrentIndex = this.SearchTarget(0);
      this.Output();
    }
    else
    {
      if (pinID != 1)
        return;
      this.mCurrentIndex = this.SearchTarget(this.mCurrentIndex + 1);
      this.Output();
    }
  }
}
