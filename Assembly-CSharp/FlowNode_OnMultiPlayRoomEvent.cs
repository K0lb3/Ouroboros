// Decompiled with JetBrains decompiler
// Type: FlowNode_OnMultiPlayRoomEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;

[FlowNode.Pin(2, "OnPlayerChanged", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(1, "OnDisconnected", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(300, "Reset", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(201, "Ignore Full Off", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(200, "Ignore Full On", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "Ignore Off", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Ignore On", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/OnMultiPlayRoomEvent", 58751)]
[AddComponentMenu("")]
[FlowNode.Pin(9, "OnRoomOnlyMember", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(10, "OnRoomParam", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(7, "OnRoomCreatorOut", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(8, "OnRoomFullMember", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(6, "OnRoomCommentChanged", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(5, "OnRoomClosed", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(4, "OnAllPlayerNotReady", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(3, "OnAllPlayerReady", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(50, "OnRoomPassChanged", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnMultiPlayRoomEvent : FlowNodePersistent
{
  private string mRoomPass = string.Empty;
  private string mRoomComment = string.Empty;
  private string mQuestName = string.Empty;
  private bool mIgnoreFullMember = true;
  private List<MyPhoton.MyPlayer> mPlayers;
  private bool mIgnore;
  private int mMemberCnt;

  private void Start()
  {
    this.mIgnore = false;
    this.mIgnoreFullMember = true;
    this.mQuestName = GlobalVars.SelectedQuestID;
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        this.mIgnore = true;
        break;
      case 101:
        this.mIgnore = false;
        break;
      case 200:
        this.mIgnoreFullMember = true;
        break;
      case 201:
        this.mIgnoreFullMember = false;
        break;
      case 300:
        this.Reset();
        break;
    }
  }

  private void Update()
  {
    MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
    if (this.mIgnore)
      return;
    if (instance.CurrentState != MyPhoton.MyState.ROOM)
    {
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(1);
    }
    else
    {
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
      bool flag1 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID;
      bool flag2 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS && GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend;
      bool flag3 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER;
      if (roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == 1)) == null && (flag1 || flag2 || flag3))
      {
        this.ActivateOutputLinks(7);
      }
      else
      {
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        if (instance.IsUpdateRoomProperty)
        {
          if (currentRoom.start)
          {
            this.ActivateOutputLinks(5);
            return;
          }
          instance.IsUpdateRoomProperty = false;
        }
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
        string str1 = myPhotonRoomParam != null ? myPhotonRoomParam.comment : string.Empty;
        if (!this.mRoomComment.Equals(str1))
        {
          DebugUtility.Log("change room comment");
          this.ActivateOutputLinks(6);
        }
        this.mRoomComment = str1;
        string str2 = myPhotonRoomParam != null ? myPhotonRoomParam.passCode : string.Empty;
        if (!this.mRoomPass.Equals(str2))
        {
          DebugUtility.Log("change room pass");
          this.ActivateOutputLinks(50);
        }
        this.mRoomPass = str2;
        bool flag4 = false;
        if (roomPlayerList == null)
          instance.Disconnect();
        else if (this.mPlayers == null)
          flag4 = true;
        else if (this.mPlayers.Count != roomPlayerList.Count)
        {
          flag4 = true;
        }
        else
        {
          for (int index = 0; index < this.mPlayers.Count; ++index)
          {
            if (this.mPlayers[index].playerID != roomPlayerList[index].playerID)
            {
              flag4 = true;
              break;
            }
            if (!this.mPlayers[index].json.Equals(roomPlayerList[index].json))
            {
              flag4 = true;
              break;
            }
          }
        }
        if (!string.IsNullOrEmpty(this.mQuestName) && !this.mQuestName.Equals(myPhotonRoomParam.iname))
        {
          DebugUtility.Log("change quest iname" + myPhotonRoomParam.iname);
          this.ActivateOutputLinks(10);
        }
        this.mQuestName = myPhotonRoomParam.iname;
        if (flag4)
        {
          this.mPlayers = new List<MyPhoton.MyPlayer>((IEnumerable<MyPhoton.MyPlayer>) roomPlayerList);
          this.ActivateOutputLinks(2);
          if (instance.IsOldestPlayer())
          {
            JSON_MyPhotonPlayerParam[] photonPlayerParamArray = new JSON_MyPhotonPlayerParam[roomPlayerList.Count];
            for (int index = 0; index < photonPlayerParamArray.Length; ++index)
              photonPlayerParamArray[index] = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
            myPhotonRoomParam.players = photonPlayerParamArray;
            instance.SetRoomParam(myPhotonRoomParam.Serialize());
          }
          bool flag5 = true;
          using (List<MyPhoton.MyPlayer>.Enumerator enumerator = this.mPlayers.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(enumerator.Current.json);
              if (photonPlayerParam.state == 0 || photonPlayerParam.state == 4 || photonPlayerParam.state == 5)
              {
                flag5 = false;
                break;
              }
            }
          }
          if (flag5)
            this.ActivateOutputLinks(3);
          else
            this.ActivateOutputLinks(4);
        }
        else
        {
          int count = instance.GetRoomPlayerList().Count;
          if (count == 1 && this.mMemberCnt != count)
            this.ActivateOutputLinks(9);
          this.mMemberCnt = instance.GetRoomPlayerList().Count;
          if (this.mIgnoreFullMember || instance.GetCurrentRoom().maxPlayers - 1 != count)
            return;
          this.ActivateOutputLinks(8);
        }
      }
    }
  }

  private void Reset()
  {
    this.mPlayers = (List<MyPhoton.MyPlayer>) null;
    this.mRoomPass = string.Empty;
    this.mRoomComment = string.Empty;
    this.mQuestName = string.Empty;
    this.mMemberCnt = 0;
  }
}
