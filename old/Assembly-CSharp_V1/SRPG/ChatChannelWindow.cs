// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(99, "Close", FlowNode.PinTypes.Output, 99)]
  [FlowNode.Pin(2, "Request(PageIndex)", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class ChatChannelWindow : MonoBehaviour, IFlowInterface
  {
    private const int page_item_max = 20;
    [SerializeField]
    private GameObject PanelTemplate1;
    [SerializeField]
    private GameObject PanelTemplate2;
    [SerializeField]
    private Transform ChatChannelBtn;
    [SerializeField]
    private Transform ChatChannelPagePanel;
    [SerializeField]
    private Transform PageItemRoot;
    [SerializeField]
    private GameObject ChannelPageItem;
    private GameObject[] mChannelPages;
    private ChatChannel mChannel;
    public int ONE_VIEW;
    private GameManager gm;

    public ChatChannelWindow()
    {
      base.\u002Ector();
    }

    public ChatChannel Channel
    {
      get
      {
        return this.mChannel;
      }
      set
      {
        this.mChannel = value;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      FlowNode_Variable.Set("SelectChannelPage", string.Empty);
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.PanelTemplate1, (Object) null))
        this.PanelTemplate1.SetActive(false);
      if (Object.op_Inequality((Object) this.PanelTemplate2, (Object) null))
        this.PanelTemplate2.SetActive(false);
      if (Object.op_Inequality((Object) this.ChatChannelPagePanel, (Object) null))
        ((Component) this.ChatChannelPagePanel).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.ChannelPageItem, (Object) null))
        this.ChannelPageItem.SetActive(false);
      this.gm = MonoSingleton<GameManager>.Instance;
    }

    private void Start()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void Refresh()
    {
      ChatChannelParam[] channels = this.mChannel.channels;
      this.RefreshPageButton(channels[0].id, channels[channels.Length - 1].id);
      ChatChannelParam[] ch_params1 = new ChatChannelParam[10];
      for (int index = 0; index < 10; ++index)
        ch_params1[index] = channels[index];
      ChatChannelParam[] ch_params2 = new ChatChannelParam[10];
      for (int index = 0; index < 10; ++index)
        ch_params2[index] = channels[10 + index];
      if (Object.op_Inequality((Object) this.PanelTemplate1, (Object) null))
      {
        ChatChannelPanel component = (ChatChannelPanel) this.PanelTemplate1.GetComponent<ChatChannelPanel>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          this.PanelTemplate1.SetActive(true);
          component.Refresh(ch_params1);
        }
      }
      if (Object.op_Inequality((Object) this.PanelTemplate2, (Object) null))
      {
        ChatChannelPanel component = (ChatChannelPanel) this.PanelTemplate2.GetComponent<ChatChannelPanel>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          this.PanelTemplate2.SetActive(true);
          component.Refresh(ch_params2);
        }
      }
      if (!Object.op_Inequality((Object) this.ChatChannelPagePanel, (Object) null))
        return;
      LayoutElement[] componentsInChildren = (LayoutElement[]) ((Component) ((Component) this.PageItemRoot).get_transform()).GetComponentsInChildren<LayoutElement>(false);
      if (componentsInChildren != null)
      {
        foreach (LayoutElement layoutElement in componentsInChildren)
        {
          if (Object.op_Implicit((Object) layoutElement))
            Object.Destroy((Object) ((Component) layoutElement).get_gameObject());
        }
      }
      ChatChannelMasterParam[] chatChannelMaster = this.gm.GetChatChannelMaster();
      if (chatChannelMaster == null || chatChannelMaster.Length < 0)
        return;
      int num1 = chatChannelMaster.Length / this.ONE_VIEW;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = 20 * index1;
        int num2 = chatChannelMaster.Length < index2 ? 0 : chatChannelMaster[index2].id;
        int index3 = index2 - 1 + 20;
        int num3 = chatChannelMaster.Length < index3 ? 0 : chatChannelMaster[index3].id;
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ChannelPageItem);
        gameObject.get_transform().SetParent(this.PageItemRoot, false);
        Transform child = gameObject.get_transform().FindChild("text");
        if (Object.op_Inequality((Object) child, (Object) null))
        {
          UnityEngine.UI.Text component = (UnityEngine.UI.Text) ((Component) child).GetComponent<UnityEngine.UI.Text>();
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("CH ");
          stringBuilder.Append(LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", (object) num2.ToString(), (object) num3.ToString()));
          string str = stringBuilder.ToString();
          component.set_text(str);
        }
        SRPG_Button component1 = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
        if (Object.op_Inequality((Object) component1, (Object) null))
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          ((UnityEvent) component1.get_onClick()).AddListener(new UnityAction((object) new ChatChannelWindow.\u003CRefresh\u003Ec__AnonStorey23A()
          {
            \u003C\u003Ef__this = this,
            index = index1
          }, __methodptr(\u003C\u003Em__251)));
        }
        gameObject.SetActive(true);
      }
    }

    private void RefreshPageButton(int begin, int end)
    {
      Transform child = this.ChatChannelBtn.FindChild("text");
      if (!Object.op_Inequality((Object) child, (Object) null))
        return;
      ((UnityEngine.UI.Text) ((Component) child).GetComponent<UnityEngine.UI.Text>()).set_text(LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", (object) begin.ToString(), (object) end.ToString()));
    }

    private void OnSelectPage(int page)
    {
      FlowNode_Variable.Set("SelectChannelPage", page.ToString());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
