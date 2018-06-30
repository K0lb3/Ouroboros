namespace SRPG
{
    using Gsc.App.NetworkHelper;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SharedWebWindow : MonoBehaviour
    {
        [SerializeField]
        private WebView Target;
        [SerializeField]
        private GameObject Caution;
        [SerializeField]
        private bool usegAuth;

        public SharedWebWindow()
        {
            this.usegAuth = 1;
            base..ctor();
            return;
        }

        private void Awake()
        {
            Transform transform;
            WebView view;
            Transform transform2;
            if ((this.Target == null) == null)
            {
                goto Label_0048;
            }
            transform = base.get_transform().FindChild("window");
            if ((transform != null) == null)
            {
                goto Label_0048;
            }
            view = transform.GetComponent<WebView>();
            if ((view != null) == null)
            {
                goto Label_0048;
            }
            this.Target = view;
        Label_0048:
            if ((this.Caution != null) == null)
            {
                goto Label_006A;
            }
            this.Caution.SetActive(0);
            goto Label_00B5;
        Label_006A:
            if ((this.Target != null) == null)
            {
                goto Label_00B5;
            }
            transform2 = this.Target.get_transform().FindChild("caution");
            if ((transform2 != null) == null)
            {
                goto Label_00B5;
            }
            this.Caution = transform2.get_gameObject();
            this.Caution.SetActive(0);
        Label_00B5:
            return;
        }

        private unsafe void Start()
        {
            string str;
            string str2;
            Dictionary<string, string> dictionary;
            KeyValuePair<string, string> pair;
            Dictionary<string, string>.Enumerator enumerator;
            if ((this.Target == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            str = FlowNode_Variable.Get("SHARED_WEBWINDOW_TITLE");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0034;
            }
            this.Target.SetTitleText(str);
        Label_0034:
            str2 = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL");
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00D9;
            }
            if (this.usegAuth == null)
            {
                goto Label_00B9;
            }
            dictionary = new Dictionary<string, string>();
            GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
            enumerator = dictionary.GetEnumerator();
        Label_0075:
            try
            {
                goto Label_009B;
            Label_007A:
                pair = &enumerator.Current;
                this.Target.SetHeaderField(&pair.Key, &pair.Value);
            Label_009B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_007A;
                }
                goto Label_00B9;
            }
            finally
            {
            Label_00AC:
                ((Dictionary<string, string>.Enumerator) enumerator).Dispose();
            }
        Label_00B9:
            this.Target.OpenURL(str2);
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", string.Empty);
            goto Label_00E5;
        Label_00D9:
            this.Caution.SetActive(1);
        Label_00E5:
            return;
        }
    }
}

