// Decompiled with JetBrains decompiler
// Type: SRPG.ChatPlayerWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "Output Remove Block", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Output Add Block", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Remove Block", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Add Block", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class ChatPlayerWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text UserName;
    [SerializeField]
    private BitmapText UserLv;
    [SerializeField]
    private Text LastLogin;
    [SerializeField]
    private GameObject Add;
    [SerializeField]
    private GameObject Remove;
    [SerializeField]
    private GameObject FriendAdd;
    [SerializeField]
    private GameObject FriendRemove;
    [SerializeField]
    private GameObject Award;
    private ChatPlayerData mPlayer;

    public ChatPlayerWindow()
    {
      base.\u002Ector();
    }

    public ChatPlayerData Player
    {
      get
      {
        return this.mPlayer;
      }
      set
      {
        if (value == null)
          this.DummyUserData();
        else
          this.mPlayer = value;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 2:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
      }
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.Award, (Object) null))
        return;
      this.Award.SetActive(false);
    }

    private void Refresh()
    {
      if (this.mPlayer == null)
        return;
      GlobalVars.SelectedFriendID = this.mPlayer.fuid;
      if (Object.op_Inequality((Object) this.UserName, (Object) null))
        this.UserName.set_text(this.mPlayer.name);
      if (Object.op_Inequality((Object) this.LastLogin, (Object) null))
      {
        TimeSpan timeSpan = TimeManager.ServerTime - GameUtility.UnixtimeToLocalTime(this.mPlayer.lastlogin);
        int days = timeSpan.Days;
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        if (days > 0)
          this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_DAY", new object[1]
          {
            (object) days.ToString()
          }));
        else if (hours > 0)
          this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_HOUR", new object[1]
          {
            (object) hours.ToString()
          }));
        else if (minutes > 0)
          this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_MINUTE", new object[1]
          {
            (object) minutes.ToString()
          }));
        else
          this.LastLogin.set_text(LocalizedText.Get("sys.CHAT_POSTAT_NOW"));
      }
      if (Object.op_Inequality((Object) this.UserLv, (Object) null))
        this.UserLv.text = this.mPlayer.lv.ToString();
      if (Object.op_Inequality((Object) this.Add, (Object) null) && Object.op_Inequality((Object) this.Remove, (Object) null))
      {
        if (FlowNode_Variable.Get("IsBlackList").Contains("1"))
        {
          this.Remove.SetActive(true);
          this.Add.SetActive(false);
        }
        else
        {
          this.Remove.SetActive(false);
          this.Add.SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.FriendAdd, (Object) null) && Object.op_Inequality((Object) this.FriendRemove, (Object) null))
      {
        this.FriendRemove.SetActive((int) this.mPlayer.is_friend != 0);
        this.FriendAdd.SetActive((int) this.mPlayer.is_friend == 0);
        Button component = (Button) this.FriendRemove.GetComponent<Button>();
        if (Object.op_Inequality((Object) component, (Object) null))
          ((Selectable) component).set_interactable(!this.mPlayer.IsFavorite);
      }
      UnitData unit = this.mPlayer.unit;
      if (unit != null)
        DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      if (this.mPlayer != null)
      {
        DataSource.Bind<ChatPlayerData>(((Component) this).get_gameObject(), this.mPlayer);
        if (Object.op_Inequality((Object) this.Award, (Object) null))
          this.Award.SetActive(true);
      }
      this.FriendAdd.SetActive(!this.mPlayer.IsFriend);
      this.FriendRemove.SetActive(this.mPlayer.IsFriend);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void DummyUserData()
    {
      this.mPlayer = new ChatPlayerData();
      this.mPlayer.exp = 10000;
      this.mPlayer.name = "TestMan";
      this.mPlayer.lv = 10;
      this.mPlayer.lastlogin = 0L;
      this.mPlayer.unit = MonoSingleton<GameManager>.Instance.Player.Units[0];
    }
  }
}
