namespace SRPG
{
    using System;
    using UnityEngine;

    public class GalleryItemDetailWindow : MonoBehaviour
    {
        public GalleryItemDetailWindow()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            SerializeValueList list;
            GameObject obj2;
            ItemParam param;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            param = DataSource.FindDataOfClass<ItemParam>(list.GetGameObject("item"), null);
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

