namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemSelectAmount : MonoBehaviour, IGameParameter
    {
        public ItemSelectAmount()
        {
            base..ctor();
            return;
        }

        public unsafe void UpdateValue()
        {
            ItemSelectListItemData data;
            Text text;
            data = DataSource.FindDataOfClass<ItemSelectListItemData>(base.get_gameObject(), null);
            text = base.get_gameObject().GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_003C;
            }
            if (data == null)
            {
                goto Label_003C;
            }
            text.set_text(&data.num.ToString());
        Label_003C:
            return;
        }
    }
}

