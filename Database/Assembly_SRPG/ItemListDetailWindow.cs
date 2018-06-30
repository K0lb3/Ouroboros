namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Reload", 0, 1)]
    public class ItemListDetailWindow : MonoBehaviour, IFlowInterface
    {
        public ItemListDetailWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Refresh()
        {
            ItemData data;
            ItemParam param;
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
            if (data == null)
            {
                goto Label_002C;
            }
            DataSource.Bind<ItemData>(base.get_gameObject(), data);
            goto Label_0054;
        Label_002C:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
            if (param != null)
            {
                goto Label_0048;
            }
            return;
        Label_0048:
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
        Label_0054:
            GameParameter.UpdateAll(base.get_gameObject());
            base.set_enabled(1);
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

