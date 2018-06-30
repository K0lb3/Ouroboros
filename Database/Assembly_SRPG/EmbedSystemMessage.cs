namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class EmbedSystemMessage : MonoBehaviour
    {
        public const string PrefabPath = "e/UI/EmbedSystemMessage";
        public SystemMessageEvent Delegate;
        public Text Message;
        public Button ButtonOk;

        public EmbedSystemMessage()
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
            return;
        }

        public static EmbedSystemMessage Create(string msg, SystemMessageEvent callback)
        {
            EmbedSystemMessage message;
            message = Object.Instantiate<EmbedSystemMessage>(Resources.Load<EmbedSystemMessage>("e/UI/EmbedSystemMessage"));
            message.Body = msg;
            message.Delegate = callback;
            return message;
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

        public delegate void SystemMessageEvent(bool yes);
    }
}

