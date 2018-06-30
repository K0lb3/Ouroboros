namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(11, "退店", 1, 11), Pin(10, "換金", 1, 10)]
    public class EventShopWindow : MonoBehaviour, IFlowInterface
    {
        public RawImage ImgBackGround;
        public RawImage ImgNPC;
        public Text TxtHaveCoin;
        [Space(16f)]
        public ImageArray NamePlateImages;
        private static readonly string ImgPathPrefix;

        static EventShopWindow()
        {
            ImgPathPrefix = "MenuChar/MenuChar_Shop_Monozuki";
            return;
        }

        public EventShopWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.TxtHaveCoin != null) == null)
            {
                goto Label_0026;
            }
            this.TxtHaveCoin.set_text(LocalizedText.Get("sys.CMD_COIN_LIST"));
        Label_0026:
            return;
        }

        private void OnDestroy()
        {
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_0036;
            }
            local1 = MonoSingleton<GameManager>.GetInstanceDirect();
            local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Remove(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.OnGoOutShop));
        Label_0036:
            return;
        }

        private bool OnGoOutShop()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return 1;
        }

        private void Start()
        {
            GameManager local1;
            if ((this.ImgNPC != null) == null)
            {
                goto Label_0026;
            }
            this.ImgNPC.set_texture(AssetManager.Load<Texture2D>(ImgPathPrefix));
        Label_0026:
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.OnGoOutShop));
            return;
        }
    }
}

