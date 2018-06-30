namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class EmbedSystemMessageEx : MonoBehaviour
    {
        public const string PrefabPath = "e/UI/EmbedSystemMessageEx";
        public Text Message;
        public GameObject ButtonTemplate;
        public GameObject ButtonBase;

        public EmbedSystemMessageEx()
        {
            base..ctor();
            return;
        }

        public void AddButton(string btn_text, bool is_close, SystemMessageEvent callback)
        {
            GameObject obj2;
            LText text;
            Button button;
            ButtonEvent event2;
            <AddButton>c__AnonStorey32B storeyb;
            storeyb = new <AddButton>c__AnonStorey32B();
            storeyb.callback = callback;
            if ((this.ButtonTemplate == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if ((this.ButtonBase == null) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            obj2 = Object.Instantiate<GameObject>(this.ButtonTemplate);
            obj2.SetActive(1);
            text = obj2.GetComponentInChildren<LText>();
            if ((text != null) == null)
            {
                goto Label_0060;
            }
            text.set_text(btn_text);
        Label_0060:
            button = obj2.GetComponentInChildren<Button>();
            if ((button != null) == null)
            {
                goto Label_008B;
            }
            button.get_onClick().AddListener(new UnityAction(storeyb, this.<>m__2F2));
        Label_008B:
            event2 = obj2.GetComponentInChildren<ButtonEvent>();
            if ((event2 != null) == null)
            {
                goto Label_00A5;
            }
            event2.set_enabled(is_close);
        Label_00A5:
            obj2.get_transform().SetParent(this.ButtonBase.get_transform(), 0);
            return;
        }

        private void Awake()
        {
            if ((this.ButtonTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ButtonTemplate.SetActive(0);
        Label_001D:
            return;
        }

        public static EmbedSystemMessageEx Create(string msg)
        {
            EmbedSystemMessageEx ex;
            ex = Object.Instantiate<EmbedSystemMessageEx>(Resources.Load<EmbedSystemMessageEx>("e/UI/EmbedSystemMessageEx"));
            ex.Body = msg;
            return ex;
        }

        public string Body
        {
            get
            {
                return this.Message.get_text();
            }
            set
            {
                this.Message.set_text(value);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <AddButton>c__AnonStorey32B
        {
            internal EmbedSystemMessageEx.SystemMessageEvent callback;

            public <AddButton>c__AnonStorey32B()
            {
                base..ctor();
                return;
            }

            internal void <>m__2F2()
            {
                this.callback(1);
                return;
            }
        }

        public delegate void SystemMessageEvent(bool yes);
    }
}

