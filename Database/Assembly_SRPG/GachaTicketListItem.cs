namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "OnDetailClick", 1, 10)]
    public class GachaTicketListItem : MonoBehaviour, IFlowInterface
    {
        public Text TicketTitle;
        public GameObject Icon;
        public SRPG_Button DetailBtn;
        public SRPG_Button ExecBtn;
        private readonly string GACHA_URL_PREFIX;
        public Text Amount;
        private GachaTopParamNew mGachaParam;
        private string mDetailURL;
        [CompilerGenerated]
        private int <SelectIndex>k__BackingField;

        public GachaTicketListItem()
        {
            this.GACHA_URL_PREFIX = "notice/detail/gacha/";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void OnDetail(SRPG_Button button)
        {
            FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDetailURL);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return;
        }

        public void Refresh(GachaTopParamNew param, int index)
        {
            ItemData data;
            StringBuilder builder;
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (string.IsNullOrEmpty(param.ticket_iname) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.ticket_iname);
            if (data != null)
            {
                goto Label_0035;
            }
            return;
        Label_0035:
            DataSource.Bind<ItemData>(base.get_gameObject(), data);
            this.SelectIndex = index;
            if ((this.DetailBtn != null) == null)
            {
                goto Label_0070;
            }
            this.DetailBtn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnDetail));
        Label_0070:
            builder = new StringBuilder();
            builder.Append(Network.SiteHost);
            builder.Append(this.GACHA_URL_PREFIX);
            builder.Append(param.detail_url);
            this.mDetailURL = builder.ToString();
            return;
        }

        public bool SetGachaButtonEvent(UnityAction action)
        {
            if (action == null)
            {
                goto Label_003A;
            }
            if ((this.ExecBtn != null) == null)
            {
                goto Label_003A;
            }
            this.ExecBtn.get_onClick().RemoveAllListeners();
            this.ExecBtn.get_onClick().AddListener(action);
            return 1;
        Label_003A:
            return 0;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        public int SelectIndex
        {
            [CompilerGenerated]
            get
            {
                return this.<SelectIndex>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<SelectIndex>k__BackingField = value;
                return;
            }
        }
    }
}

