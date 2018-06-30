namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class NetworkErrorWindowEx : MonoBehaviour
    {
        [SerializeField]
        private Text Message;
        [SerializeField]
        private Button ButtonOk;

        public NetworkErrorWindowEx()
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

        private void OnOk()
        {
        }

        private void Start()
        {
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
    }
}

