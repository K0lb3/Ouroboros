namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "換金", 1, 10), Pin(2, "換金(1度だけ表示)", 0, 2), Pin(12, "ゲリラショップへ遷移", 1, 12), Pin(1, "開始", 0, 1), Pin(11, "退店", 1, 11)]
    public class ShopWindow : MonoBehaviour, IFlowInterface
    {
        public RawImage ImgBackGround;
        public RawImage ImgNPC;
        public EShopType[] NpcRandArray;
        public ChangeButton[] ChangeButtons;
        [Space(16f)]
        public ImageArray NamePlateImages;
        public GameObject GuerrillaShopBanner;
        private static readonly string ImgPathPrefix;
        private bool alreadyShowExchange;
        public LevelLock[] ShopLevelLock;

        static ShopWindow()
        {
            ImgPathPrefix = "MenuChar/MenuChar_Shop_";
            return;
        }

        public ShopWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            bool flag;
            if (pinID != 1)
            {
                goto Label_0052;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
            if ((this.GuerrillaShopBanner != null) == null)
            {
                goto Label_0034;
            }
            this.GuerrillaShopBanner.SetActive(flag);
        Label_0034:
            if (flag == null)
            {
                goto Label_0047;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            goto Label_004D;
        Label_0047:
            this.ShowExchangeDialog();
        Label_004D:
            goto Label_005F;
        Label_0052:
            if (pinID != 2)
            {
                goto Label_005F;
            }
            this.ShowExchangeDialog();
        Label_005F:
            return;
        }

        private void Awake()
        {
            GameManager local1;
            List<EShopType> list;
            int num;
            EShopType type;
            UnlockTargets targets;
            list = new List<EShopType>();
            num = 0;
            goto Label_0080;
        Label_000D:
            if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.ShopLevelLock[num].Condition) == null)
            {
                goto Label_007C;
            }
            targets = this.ShopLevelLock[num].Condition;
            if (targets == 1)
            {
                goto Label_0058;
            }
            if (targets == 0x20)
            {
                goto Label_0064;
            }
            if (targets == 0x40)
            {
                goto Label_0070;
            }
            goto Label_007C;
        Label_0058:
            list.Add(0);
            goto Label_007C;
        Label_0064:
            list.Add(1);
            goto Label_007C;
        Label_0070:
            list.Add(2);
        Label_007C:
            num += 1;
        Label_0080:
            if (num < ((int) this.ShopLevelLock.Length))
            {
                goto Label_000D;
            }
            this.NpcRandArray = list.ToArray();
            if ((this.ImgNPC != null) == null)
            {
                goto Label_00E1;
            }
            type = this.NpcRandArray[Random.Range(0, (int) this.NpcRandArray.Length)];
            this.ImgNPC.set_texture(AssetManager.Load<Texture2D>(ImgPathPrefix + ((EShopType) type)));
        Label_00E1:
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.OnGoOutShop));
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

        private void ShowExchangeDialog()
        {
            if (this.alreadyShowExchange != null)
            {
                goto Label_002E;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckEnableConvertGold() == null)
            {
                goto Label_002E;
            }
            this.alreadyShowExchange = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_002E:
            return;
        }

        [Serializable]
        public class ChangeButton
        {
            public EShopType shopType;
            public Button button;

            public ChangeButton()
            {
                base..ctor();
                return;
            }
        }
    }
}

