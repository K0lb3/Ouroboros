namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1)]
    public class SelectItemInfo : MonoBehaviour, IFlowInterface
    {
        public SelectItemInfo()
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

        private void Awake()
        {
        }

        private void Refresh()
        {
            string str;
            ItemParam param;
            ItemData data;
            str = GlobalVars.ItemSelectListItemData.iiname;
            param = MonoSingleton<GameManager>.Instance.GetItemParam(str);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.iname);
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            DataSource.Bind<ItemSelectListItemData>(base.get_gameObject(), GlobalVars.ItemSelectListItemData);
            DataSource.Bind<ItemData>(base.get_gameObject(), data);
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

