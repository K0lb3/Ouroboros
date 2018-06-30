namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class NetworkErrorWindow : MonoBehaviour
    {
        public Text Title;
        public Text StatusCode;
        public Text Message;

        public NetworkErrorWindow()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        private void OnClick()
        {
            if (Network.ErrCode != 0x1389)
            {
                goto Label_0019;
            }
            MonoSingleton<GameManager>.Instance.ResetAuth();
        Label_0019:
            return;
        }

        public void OpenMaintenanceSite()
        {
            Application.OpenURL(Network.SiteHost);
            return;
        }

        public void OpenStore()
        {
        }

        public void OpenVersionUpSite()
        {
            Application.OpenURL(Network.SiteHost);
            return;
        }

        private void Start()
        {
            object[] objArray2;
            object[] objArray1;
            Transform transform;
            Button button;
            if ((this.Title != null) == null)
            {
                goto Label_0026;
            }
            this.Title.set_text(LocalizedText.Get("embed.CONN_ERR"));
        Label_0026:
            if ((this.StatusCode != null) == null)
            {
                goto Label_0095;
            }
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_0084;
            }
            objArray1 = new object[] { ((Network.EErrCode) Network.ErrCode).ToString() };
            this.StatusCode.set_text(LocalizedText.Get("embed.CONN_ERRCODE", objArray1));
            this.StatusCode.get_gameObject().SetActive(1);
            goto Label_0095;
        Label_0084:
            this.StatusCode.get_gameObject().SetActive(0);
        Label_0095:
            if ((this.Message != null) == null)
            {
                goto Label_00F7;
            }
            if (string.IsNullOrEmpty(Network.ErrMsg) == null)
            {
                goto Label_00E7;
            }
            objArray2 = new object[] { ((Network.EErrCode) Network.ErrCode).ToString() };
            this.Message.set_text(LocalizedText.Get("embed.APP_REBOOT", objArray2));
            goto Label_00F7;
        Label_00E7:
            this.Message.set_text(Network.ErrMsg);
        Label_00F7:
            transform = base.get_transform().FindChild("window/Button");
            if ((transform != null) == null)
            {
                goto Label_013E;
            }
            button = transform.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_013E;
            }
            button.get_onClick().AddListener(new UnityAction(this, this.OnClick));
        Label_013E:
            return;
        }
    }
}

