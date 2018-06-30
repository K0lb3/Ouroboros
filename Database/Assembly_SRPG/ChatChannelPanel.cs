namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "Channel Select", 1, 0)]
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
            base..ctor();
            return;
        }

        public void Activated(int piniD)
        {
        }

        private void Awake()
        {
            int num;
            int num2;
            Transform transform;
            if ((this.lists != null) == null)
            {
                goto Label_0067;
            }
            num = this.lists.get_transform().get_childCount();
            this.items = new Transform[num];
            num2 = 0;
            goto Label_0060;
        Label_0035:
            transform = this.lists.get_transform().GetChild(num2);
            this.items[num2] = transform;
            transform.get_gameObject().SetActive(0);
            num2 += 1;
        Label_0060:
            if (num2 < num)
            {
                goto Label_0035;
            }
        Label_0067:
            return;
        }

        private bool ChannelChange(SRPG_Button button)
        {
            ChatChannelItem item;
            if (button.IsInteractable() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            item = button.get_gameObject().GetComponent<ChatChannelItem>();
            if ((item != null) == null)
            {
                goto Label_0031;
            }
            this.mActiveChanneID = item.ChannelID;
        Label_0031:
            return 1;
        }

        public void OnChannelChange(SRPG_Button button)
        {
            if (this.ChannelChange(button) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            GlobalVars.CurrentChatChannel.Set(this.mActiveChanneID);
            FlowNode_GameObject.ActivateOutputLinks(this, 0);
            return;
        }

        public unsafe void Refresh(ChatChannelParam[] ch_params)
        {
            object[] objArray1;
            int num;
            int num2;
            int num3;
            Transform transform;
            ChatChannelItem item;
            SRPG_ToggleButton button;
            if (ch_params == null)
            {
                goto Label_000F;
            }
            if (((int) ch_params.Length) >= 0)
            {
                goto Label_0010;
            }
        Label_000F:
            return;
        Label_0010:
            if (this.items == null)
            {
                goto Label_0029;
            }
            if (((int) this.items.Length) >= 0)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            if ((this.Title != null) == null)
            {
                goto Label_0080;
            }
            num = ch_params[0].id;
            num2 = ch_params[((int) ch_params.Length) - 1].id;
            objArray1 = new object[] { &num.ToString(), &num2.ToString() };
            this.Title.set_text(LocalizedText.Get("sys.TEXT_SELECT_CHANNEL", objArray1));
        Label_0080:
            num3 = 0;
            goto Label_011F;
        Label_0087:
            transform = this.items[num3];
            if ((transform != null) == null)
            {
                goto Label_011B;
            }
            item = transform.GetComponent<ChatChannelItem>();
            if ((item != null) == null)
            {
                goto Label_00C7;
            }
            transform.get_gameObject().SetActive(1);
            item.Refresh(ch_params[num3]);
        Label_00C7:
            button = transform.GetComponent<SRPG_ToggleButton>();
            if ((button != null) == null)
            {
                goto Label_011B;
            }
            if (ch_params[num3].id != GlobalVars.CurrentChatChannel)
            {
                goto Label_0100;
            }
            button.IsOn = 1;
            goto Label_0108;
        Label_0100:
            button.IsOn = 0;
        Label_0108:
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnChannelChange));
        Label_011B:
            num3 += 1;
        Label_011F:
            if (num3 < ((int) this.items.Length))
            {
                goto Label_0087;
            }
            return;
        }
    }
}

