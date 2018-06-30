namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class WebHelpButton : SRPG_Button
    {
        public GameObject Target;
        [StringIsResourcePath(typeof(GameObject))]
        public string PrefabPath;
        public bool usegAuth;
        private IWebHelp mInterface;

        public WebHelpButton()
        {
            this.usegAuth = 1;
            base..ctor();
            return;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Update();
            return;
        }

        private unsafe void ShowWebHelp()
        {
            string str;
            string str2;
            if (this.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mInterface.GetHelpURL(&str, &str2) != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            Application.OpenURL(SRPG_Extensions.ComposeURL(GameSettings.Instance.WebHelp_URLMode, str));
            return;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.Target != null) == null)
            {
                goto Label_0029;
            }
            this.mInterface = this.Target.GetComponentInChildren<IWebHelp>(1);
        Label_0029:
            base.get_onClick().AddListener(new UnityAction(this, this.ShowWebHelp));
            return;
        }

        private unsafe void Update()
        {
            string str;
            string str2;
            if (this.mInterface == null)
            {
                goto Label_0020;
            }
            base.set_interactable(this.mInterface.GetHelpURL(&str, &str2));
        Label_0020:
            return;
        }
    }
}

