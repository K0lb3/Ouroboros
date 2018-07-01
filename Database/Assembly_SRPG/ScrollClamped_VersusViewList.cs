// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusViewList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusViewList : MonoBehaviour, ScrollListSetUp
  {
    private readonly string VS_LOBBY_NAME;
    private readonly string VS_FRIEND_SUFFIX;
    public float Space;
    private int m_Max;
    private List<MyPhoton.MyRoom> m_Rooms;

    public ScrollClamped_VersusViewList()
    {
      base.\u002Ector();
    }

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.LOBBY)
        return;
      if (this.m_Rooms == null)
        this.m_Rooms = new List<MyPhoton.MyRoom>();
      this.m_Rooms.Clear();
      List<MyPhoton.MyRoom> roomList = instance.GetRoomList();
      for (int index = 0; index < roomList.Count; ++index)
      {
        if (roomList[index].lobby == this.VS_LOBBY_NAME && roomList[index].name.IndexOf(this.VS_FRIEND_SUFFIX) == -1 && roomList[index].start)
          this.m_Rooms.Add(roomList[index]);
      }
      this.m_Max = this.m_Rooms.Count;
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = component1.OnItemUpdate;
      ScrollClamped_VersusViewList clampedVersusViewList = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) clampedVersusViewList, __vmethodptr(clampedVersusViewList, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      Vector2 anchoredPosition = component2.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      sizeDelta.y = (__Null) ((double) component1.ItemScale * (double) this.Space * (double) this.m_Max);
      component2.set_sizeDelta(sizeDelta);
      component2.set_anchoredPosition(anchoredPosition);
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<MyPhoton.MyRoom>(obj, this.m_Rooms[idx]);
        VersusViewRoomInfo component = (VersusViewRoomInfo) obj.GetComponent<VersusViewRoomInfo>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Refresh();
      }
    }
  }
}
