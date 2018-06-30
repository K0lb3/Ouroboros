namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class WebView : MonoBehaviour
    {
        public Text Text_Title;
        public Button Btn_Close;
        public RawImage ClientArea;
        public UIUtility.DialogResultEvent OnClose;

        public WebView()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.Btn_Close != null) == null)
            {
                goto Label_0038;
            }
            UIUtility.AddEventListener(this.Btn_Close.get_gameObject(), this.Btn_Close.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
        Label_0038:
            return;
        }

        public void BeginClose()
        {
            Object.DestroyImmediate(base.get_gameObject());
            return;
        }

        private void OnClickButton(GameObject obj)
        {
            bool flag;
            if ((obj == this.Btn_Close.get_gameObject()) == null)
            {
                goto Label_003A;
            }
            if ((this.Btn_Close != null) == null)
            {
                goto Label_003A;
            }
            this.OnClose(base.get_gameObject());
        Label_003A:
            this.BeginClose();
            return;
        }

        public void OpenURL(string url)
        {
        }

        public void SetHeaderField(string key, string value)
        {
        }

        public void SetTitleText(string text)
        {
            if ((this.Text_Title != null) == null)
            {
                goto Label_001D;
            }
            this.Text_Title.set_text(text);
        Label_001D:
            return;
        }
    }
}

