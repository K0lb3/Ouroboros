namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1), Pin(100, "Unlock", 1, 100), Pin(0x65, "Selected Quest", 1, 0x65)]
    public class ItemGetUnlockWindow : MonoBehaviour, IFlowInterface
    {
        private ItemParam UnlockItem;

        public ItemGetUnlockWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void Refresh()
        {
            string str;
            ItemData data;
            str = GlobalVars.ItemSelectListItemData.iiname;
            this.UnlockItem = MonoSingleton<GameManager>.Instance.GetItemParam(str);
            DataSource.Bind<ItemParam>(base.get_gameObject(), this.UnlockItem);
            DataSource.Bind<ItemSelectListItemData>(base.get_gameObject(), GlobalVars.ItemSelectListItemData);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.UnlockItem);
            if (data == null)
            {
                goto Label_0065;
            }
            DataSource.Bind<ItemData>(base.get_gameObject(), data);
        Label_0065:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

