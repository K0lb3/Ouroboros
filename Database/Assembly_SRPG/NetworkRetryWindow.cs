namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class NetworkRetryWindow : MonoBehaviour
    {
        public RetryWindowEvent Delegate;
        public Text Title;
        public Text Message;
        public Button ButtonOk;
        public Button ButtonCancel;

        public NetworkRetryWindow()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.ButtonOk != null) == null)
            {
                goto Label_002D;
            }
            this.ButtonOk.get_onClick().AddListener(new UnityAction(this, this.OnOk));
        Label_002D:
            if ((this.ButtonCancel != null) == null)
            {
                goto Label_005A;
            }
            this.ButtonCancel.get_onClick().AddListener(new UnityAction(this, this.OnCancel));
        Label_005A:
            return;
        }

        private void OnCancel()
        {
            this.Delegate(0);
            return;
        }

        private void OnOk()
        {
            this.Delegate(1);
            return;
        }

        private void Start()
        {
            if ((this.Title != null) == null)
            {
                goto Label_0026;
            }
            this.Title.set_text(LocalizedText.Get("embed.CONN_RETRY"));
        Label_0026:
            return;
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

        public delegate void RetryWindowEvent(bool retry);
    }
}

