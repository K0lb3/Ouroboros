namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class EmbedWindowYesNo : MonoBehaviour
    {
        public const string PrefabPath = "e/UI/EmbedWindowYesNo";
        public YesNoWindowEvent Delegate;
        public Text Message;
        public Button ButtonOk;
        public Button ButtonCancel;

        public EmbedWindowYesNo()
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

        public static EmbedWindowYesNo Create(string msg, YesNoWindowEvent callback)
        {
            EmbedWindowYesNo no;
            no = Object.Instantiate<EmbedWindowYesNo>(Resources.Load<EmbedWindowYesNo>("e/UI/EmbedWindowYesNo"));
            no.Body = msg;
            no.Delegate = callback;
            return no;
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

        public delegate void YesNoWindowEvent(bool yes);
    }
}

