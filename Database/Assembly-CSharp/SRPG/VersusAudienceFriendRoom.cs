// Decompiled with JetBrains decompiler
// Type: SRPG.VersusAudienceFriendRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusAudienceFriendRoom : MonoBehaviour
  {
    private readonly float UPDATE_INTERVAL;
    public GameObject RoomObj;
    public Text AudienceTxt;
    private float mUpdateTime;

    public VersusAudienceFriendRoom()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Refresh((MyPhoton.MyRoom) null);
    }

    private void Refresh(MyPhoton.MyRoom room = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Inequality((Object) this.RoomObj, (Object) null) || instance.AudienceRoom == null)
        return;
      DataSource.Bind<MyPhoton.MyRoom>(this.RoomObj, room == null ? instance.AudienceRoom : room);
      VersusViewRoomInfo component = (VersusViewRoomInfo) this.RoomObj.GetComponent<VersusViewRoomInfo>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.Refresh();
      if (!Object.op_Inequality((Object) this.AudienceTxt, (Object) null))
        return;
      this.AudienceTxt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_NUM"), (object) GameUtility.HalfNum2FullNum(instance.AudienceRoom.audience.ToString()), (object) GameUtility.HalfNum2FullNum(instance.AudienceRoom.audienceMax.ToString())));
    }

    private void Update()
    {
      this.mUpdateTime -= Time.get_deltaTime();
      if ((double) this.mUpdateTime > 0.0)
        return;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (!Object.op_Inequality((Object) instance2, (Object) null) || instance1.AudienceRoom == null || instance1.AudienceRoom.battle)
        return;
      if (!instance2.IsConnected() && !instance1.AudienceRoom.battle)
      {
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FORCE_LEAVE");
        instance1.AudienceRoom = (MyPhoton.MyRoom) null;
      }
      else
      {
        if (!instance2.IsRoomListUpdated)
          return;
        JSON_MyPhotonRoomParam myPhotonRoomParam1 = JSON_MyPhotonRoomParam.Parse(instance1.AudienceRoom.json);
        if (myPhotonRoomParam1 != null)
        {
          MyPhoton.MyRoom room = instance2.SearchRoom(myPhotonRoomParam1.roomid);
          if (room != null)
          {
            if (!room.json.Equals(instance1.AudienceRoom.json))
            {
              this.Refresh(room);
              JSON_MyPhotonRoomParam myPhotonRoomParam2 = JSON_MyPhotonRoomParam.Parse(room.json);
              if (myPhotonRoomParam2 != null && myPhotonRoomParam2.audience == 0)
              {
                FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "AUDIENCE_DISABLE");
                instance1.AudienceRoom = (MyPhoton.MyRoom) null;
                return;
              }
            }
            instance1.AudienceRoom = room;
            if (room.battle)
              FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "START_AUDIENCE");
          }
          else
            FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "DISBANDED");
        }
        instance2.IsRoomListUpdated = false;
        this.mUpdateTime = this.UPDATE_INTERVAL;
      }
    }

    public void OnClickBack()
    {
      if (!Network.IsConnecting)
        return;
      Network.Abort();
    }
  }
}
