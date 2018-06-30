namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Request(PageIndex)", 1, 2), Pin(0, "Refresh", 0, 0), Pin(1, "Request", 1, 1), Pin(0x63, "Close", 1, 0x63)]
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
            this.ONE_VIEW = 20;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001B;
            }
            FlowNode_Variable.Set("SelectChannelPage", string.Empty);
            this.Refresh();
        Label_001B:
            return;
        }

        private void Awake()
        {
            if ((this.PanelTemplate1 != null) == null)
            {
                goto Label_001D;
            }
            this.PanelTemplate1.SetActive(0);
        Label_001D:
            if ((this.PanelTemplate2 != null) == null)
            {
                goto Label_003A;
            }
            this.PanelTemplate2.SetActive(0);
        Label_003A:
            if ((this.ChatChannelPagePanel != null) == null)
            {
                goto Label_005C;
            }
            this.ChatChannelPagePanel.get_gameObject().SetActive(0);
        Label_005C:
            if ((this.ChannelPageItem != null) == null)
            {
                goto Label_0079;
            }
            this.ChannelPageItem.SetActive(0);
        Label_0079:
            this.gm = MonoSingleton<GameManager>.Instance;
            return;
        }

        private unsafe void OnSelectPage(int page)
        {
            FlowNode_Variable.Set("SelectChannelPage", &page.ToString());
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
            return;
        }

        private unsafe void Refresh()
        {
            object[] objArray1;
            ChatChannelParam[] paramArray;
            ChatChannelParam param;
            ChatChannelParam param2;
            ChatChannelParam[] paramArray2;
            int num;
            ChatChannelParam[] paramArray3;
            int num2;
            ChatChannelPanel panel;
            ChatChannelPanel panel2;
            LayoutElement[] elementArray;
            LayoutElement element;
            LayoutElement[] elementArray2;
            int num3;
            ChatChannelMasterParam[] paramArray4;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            GameObject obj2;
            Transform transform;
            Text text;
            StringBuilder builder;
            string str;
            SRPG_Button button;
            <Refresh>c__AnonStorey312 storey;
            paramArray = this.mChannel.channels;
            param = paramArray[0];
            param2 = paramArray[((int) paramArray.Length) - 1];
            this.RefreshPageButton(param.id, param2.id);
            paramArray2 = new ChatChannelParam[10];
            num = 0;
            goto Label_0048;
        Label_003A:
            paramArray2[num] = paramArray[num];
            num += 1;
        Label_0048:
            if (num < 10)
            {
                goto Label_003A;
            }
            paramArray3 = new ChatChannelParam[10];
            num2 = 0;
            goto Label_0074;
        Label_0062:
            paramArray3[num2] = paramArray[10 + num2];
            num2 += 1;
        Label_0074:
            if (num2 < 10)
            {
                goto Label_0062;
            }
            if ((this.PanelTemplate1 != null) == null)
            {
                goto Label_00BC;
            }
            panel = this.PanelTemplate1.GetComponent<ChatChannelPanel>();
            if ((panel != null) == null)
            {
                goto Label_00BC;
            }
            this.PanelTemplate1.SetActive(1);
            panel.Refresh(paramArray2);
        Label_00BC:
            if ((this.PanelTemplate2 != null) == null)
            {
                goto Label_00FC;
            }
            panel2 = this.PanelTemplate2.GetComponent<ChatChannelPanel>();
            if ((panel2 != null) == null)
            {
                goto Label_00FC;
            }
            this.PanelTemplate2.SetActive(1);
            panel2.Refresh(paramArray3);
        Label_00FC:
            if ((this.ChatChannelPagePanel != null) == null)
            {
                goto Label_02DF;
            }
            elementArray = this.PageItemRoot.get_transform().GetComponentsInChildren<LayoutElement>(0);
            if (elementArray == null)
            {
                goto Label_0168;
            }
            elementArray2 = elementArray;
            num3 = 0;
            goto Label_015D;
        Label_0133:
            element = elementArray2[num3];
            if (element != null)
            {
                goto Label_014B;
            }
            goto Label_0157;
        Label_014B:
            Object.Destroy(element.get_gameObject());
        Label_0157:
            num3 += 1;
        Label_015D:
            if (num3 < ((int) elementArray2.Length))
            {
                goto Label_0133;
            }
        Label_0168:
            paramArray4 = this.gm.GetChatChannelMaster();
            if ((paramArray4 != null) && (((int) paramArray4.Length) >= 0))
            {
                goto Label_0187;
            }
            return;
        Label_0187:
            num4 = ((int) paramArray4.Length) / this.ONE_VIEW;
            num5 = 0;
            goto Label_02D6;
        Label_019C:
            num6 = 20 * num5;
            num7 = (((int) paramArray4.Length) < num6) ? 0 : paramArray4[num6].id;
            num8 = (num6 - 1) + 20;
            num9 = (((int) paramArray4.Length) < num8) ? 0 : paramArray4[num8].id;
            obj2 = Object.Instantiate<GameObject>(this.ChannelPageItem);
            obj2.get_transform().SetParent(this.PageItemRoot, 0);
            transform = obj2.get_transform().FindChild("text");
            if ((transform != null) == null)
            {
                goto Label_0281;
            }
            text = transform.GetComponent<Text>();
            builder = GameUtility.GetStringBuilder();
            builder.Append("CH ");
            objArray1 = new object[] { &num7.ToString(), &num9.ToString() };
            builder.Append(LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", objArray1));
            str = builder.ToString();
            text.set_text(str);
        Label_0281:
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_02C8;
            }
            storey = new <Refresh>c__AnonStorey312();
            storey.<>f__this = this;
            storey.index = num5;
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__2B6));
        Label_02C8:
            obj2.SetActive(1);
            num5 += 1;
        Label_02D6:
            if (num5 < num4)
            {
                goto Label_019C;
            }
        Label_02DF:
            return;
        }

        private unsafe void RefreshPageButton(int begin, int end)
        {
            object[] objArray1;
            Transform transform;
            Text text;
            transform = this.ChatChannelBtn.FindChild("text");
            if ((transform != null) == null)
            {
                goto Label_004E;
            }
            objArray1 = new object[] { &begin.ToString(), &end.ToString() };
            transform.GetComponent<Text>().set_text(LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", objArray1));
        Label_004E:
            return;
        }

        private void Start()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
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
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey312
        {
            internal int index;
            internal ChatChannelWindow <>f__this;

            public <Refresh>c__AnonStorey312()
            {
                base..ctor();
                return;
            }

            internal void <>m__2B6()
            {
                this.<>f__this.OnSelectPage(this.index);
                return;
            }
        }
    }
}

