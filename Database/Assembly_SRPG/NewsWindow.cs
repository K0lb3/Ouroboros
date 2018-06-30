namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class NewsWindow : MonoBehaviour
    {
        public RectTransform WebViewContainer;
        public bool usegAuth;
        public SerializeValueBehaviour ValueList;
        private string[] allow_change_scenes;
        public Button CloseButton;
        public int testCounter;

        public NewsWindow()
        {
            string[] textArray1;
            this.usegAuth = 1;
            textArray1 = new string[] { "MENU_ARENA", "MENU_MULTI", "MENU_INN", "MENU_PARTY", "MENU_SHOP", "MENU_SHOP_TABI", "MENU_SHOP_KIMAGURE", "MENU_SHOP_MONOZUKI", "MENU_UNITLIST", "MENU_QUEST", "MENU_DAILY", "MENU_GACHA", "MENU_REPLAY" };
            this.allow_change_scenes = textArray1;
            base..ctor();
            return;
        }

        private void Start()
        {
            Debug.Log("[NewsWindow]Start");
            if (MonoSingleton<DebugManager>.Instance.IsWebViewEnable() != null)
            {
                goto Label_0041;
            }
            if ((this.CloseButton != null) == null)
            {
                goto Label_0036;
            }
            this.CloseButton.set_interactable(1);
        Label_0036:
            Debug.Log("[NewsWindow]Not WebView Enable");
            return;
        Label_0041:
            Debug.Log("[NewsWindow]WebView Enable");
            if ((this.CloseButton != null) == null)
            {
                goto Label_0068;
            }
            this.CloseButton.set_interactable(1);
        Label_0068:
            return;
        }

        private void StartSceneChange(string new_scene)
        {
            string str;
            string[] strArray;
            int num;
            GameObject obj2;
            strArray = this.allow_change_scenes;
            num = 0;
            goto Label_0056;
        Label_000E:
            str = strArray[num];
            if ((str == new_scene) == null)
            {
                goto Label_0052;
            }
            obj2 = GameObject.Find("Config_Home(Clone)");
            if ((obj2 != null) == null)
            {
                goto Label_003B;
            }
            Object.Destroy(obj2);
        Label_003B:
            Object.Destroy(base.get_gameObject());
            GlobalEvent.Invoke(new_scene, this);
            goto Label_005F;
        Label_0052:
            num += 1;
        Label_0056:
            if (num < ((int) strArray.Length))
            {
                goto Label_000E;
            }
        Label_005F:
            return;
        }
    }
}

