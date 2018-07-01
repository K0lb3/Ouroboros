// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewRoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "JoinFriendRoom", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "Select Reset", FlowNode.PinTypes.Input, 2)]
  public class VersusViewRoomInfo : MonoBehaviour, IFlowInterface
  {
    private readonly string FREE_SUFFIX;
    public GameObject Player1P;
    public GameObject Player2P;
    public Image RoomType;
    public Image RoomIcon;
    public Image MapThumnail;
    public Sprite FreeMatch;
    public Sprite TowerMatch;
    public Sprite FreeIcon;
    public Sprite TowerIcon;
    public Text MapName;
    public Text MapDetail;

    public VersusViewRoomInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Refresh()
    {
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(((Component) this).get_gameObject(), (MyPhoton.MyRoom) null);
      if (dataOfClass == null)
      {
        ((Component) this).get_gameObject().SetActive(false);
      }
      else
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(dataOfClass.json);
        if (myPhotonRoomParam.players == null)
        {
          ((Component) this).get_gameObject().SetActive(false);
        }
        else
        {
          ((Component) this).get_gameObject().SetActive(true);
          if (Object.op_Inequality((Object) this.Player1P, (Object) null))
          {
            if (myPhotonRoomParam.players.Length > 0)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player1P, myPhotonRoomParam.players[0]);
            VersusViewPlayerInfo component = (VersusViewPlayerInfo) this.Player1P.GetComponent<VersusViewPlayerInfo>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh();
          }
          if (Object.op_Inequality((Object) this.Player2P, (Object) null))
          {
            if (myPhotonRoomParam.players.Length > 1)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, myPhotonRoomParam.players[1]);
            else
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, (JSON_MyPhotonPlayerParam) null);
            VersusViewPlayerInfo component = (VersusViewPlayerInfo) this.Player2P.GetComponent<VersusViewPlayerInfo>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh();
          }
          if (Object.op_Inequality((Object) this.RoomType, (Object) null))
          {
            if (dataOfClass.name.IndexOf(this.FREE_SUFFIX) != -1)
              this.RoomType.set_sprite(this.FreeMatch);
            else
              this.RoomType.set_sprite(this.TowerMatch);
          }
          if (Object.op_Inequality((Object) this.RoomIcon, (Object) null))
          {
            if (dataOfClass.name.IndexOf(this.FREE_SUFFIX) != -1)
              this.RoomIcon.set_sprite(this.FreeIcon);
            else
              this.RoomIcon.set_sprite(this.TowerIcon);
          }
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
          if (quest == null)
            return;
          if (Object.op_Inequality((Object) this.MapThumnail, (Object) null))
          {
            SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if (Object.op_Inequality((Object) spriteSheet, (Object) null))
              this.MapThumnail.set_sprite(spriteSheet.GetSprite(quest.VersusThumnail));
          }
          if (Object.op_Inequality((Object) this.MapName, (Object) null))
            this.MapName.set_text(quest.name);
          if (!Object.op_Inequality((Object) this.MapDetail, (Object) null))
            return;
          this.MapDetail.set_text(quest.expr);
        }
      }
    }

    public void OnClickRoomInfo()
    {
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(((Component) this).get_gameObject(), (MyPhoton.MyRoom) null);
      if (dataOfClass == null)
        return;
      MonoSingleton<GameManager>.Instance.AudienceRoom = dataOfClass;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "COMFIRM_AUDIENCE");
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        this.Refresh();
      }
      else
      {
        if (pinID != 2)
          return;
        MonoSingleton<GameManager>.Instance.AudienceRoom = (MyPhoton.MyRoom) null;
      }
    }
  }
}
