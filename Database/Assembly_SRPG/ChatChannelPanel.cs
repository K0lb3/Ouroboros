// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Channel Select", FlowNode.PinTypes.Output, 0)]
  public class ChatChannelPanel : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Transform lists;
    [SerializeField]
    private Text Title;
    private Transform[] items;
    [SerializeField]
    private SRPG_ToggleButton[] ChannelButtons;
    private int mActiveChanneID;

    public ChatChannelPanel()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.lists, (Object) null))
        return;
      int childCount = ((Component) this.lists).get_transform().get_childCount();
      this.items = new Transform[childCount];
      for (int index = 0; index < childCount; ++index)
      {
        Transform child = ((Component) this.lists).get_transform().GetChild(index);
        this.items[index] = child;
        ((Component) child).get_gameObject().SetActive(false);
      }
    }

    public void Activated(int piniD)
    {
    }

    public void Refresh(ChatChannelParam[] ch_params)
    {
      if (ch_params == null || ch_params.Length < 0 || (this.items == null || this.items.Length < 0))
        return;
      if (Object.op_Inequality((Object) this.Title, (Object) null))
        this.Title.set_text(LocalizedText.Get("sys.TEXT_SELECT_CHANNEL", (object) ch_params[0].id.ToString(), (object) ch_params[ch_params.Length - 1].id.ToString()));
      for (int index = 0; index < this.items.Length; ++index)
      {
        Transform transform = this.items[index];
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          ChatChannelItem component1 = (ChatChannelItem) ((Component) transform).GetComponent<ChatChannelItem>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            ((Component) transform).get_gameObject().SetActive(true);
            component1.Refresh(ch_params[index]);
          }
          SRPG_ToggleButton component2 = (SRPG_ToggleButton) ((Component) transform).GetComponent<SRPG_ToggleButton>();
          if (Object.op_Inequality((Object) component2, (Object) null))
          {
            component2.IsOn = ch_params[index].id == (int) GlobalVars.CurrentChatChannel;
            component2.AddListener(new SRPG_Button.ButtonClickEvent(this.OnChannelChange));
          }
        }
      }
    }

    public void OnChannelChange(SRPG_Button button)
    {
      if (!this.ChannelChange(button))
        return;
      GlobalVars.CurrentChatChannel.Set(this.mActiveChanneID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    private bool ChannelChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      ChatChannelItem component = (ChatChannelItem) ((Component) button).get_gameObject().GetComponent<ChatChannelItem>();
      if (Object.op_Inequality((Object) component, (Object) null))
        this.mActiveChanneID = component.ChannelID;
      return true;
    }
  }
}
