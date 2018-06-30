namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Action", 0, 1)]
    public class ReviewAction : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        public string url;

        public ReviewAction()
        {
            this.url = string.Empty;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.OnAction();
        Label_000D:
            return;
        }

        public void OnAction()
        {
            if (string.IsNullOrEmpty(this.url) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.Success();
            return;
        }

        private void Start()
        {
        }

        private void Success()
        {
            Application.OpenURL(this.url);
            return;
        }
    }
}

