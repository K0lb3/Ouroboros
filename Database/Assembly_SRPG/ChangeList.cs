namespace SRPG
{
    using System;
    using UnityEngine;

    public class ChangeList : MonoBehaviour
    {
        public GameObject List;
        [FlexibleArray]
        public ChangeListItem[] ListItems;

        public ChangeList()
        {
            this.ListItems = new ChangeListItem[0];
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.List == null) == null)
            {
                goto Label_001D;
            }
            this.List = base.get_gameObject();
        Label_001D:
            return;
        }

        public unsafe void SetData(ChangeListData[] src)
        {
            Transform transform;
            int num;
            ChangeListData data;
            int num2;
            int num3;
            ChangeListItem item;
            long num4;
            long num5;
            long num6;
            transform = this.List.get_transform();
            num = 0;
            goto Label_01E9;
        Label_0013:
            data = src[num];
            num2 = -1;
            num3 = 0;
            goto Label_005C;
        Label_0021:
            if ((this.ListItems[num3] != null) == null)
            {
                goto Label_0056;
            }
            if (this.ListItems[num3].ID != data.ItemID)
            {
                goto Label_0056;
            }
            num2 = num3;
            goto Label_006B;
        Label_0056:
            num3 += 1;
        Label_005C:
            if (num3 < ((int) this.ListItems.Length))
            {
                goto Label_0021;
            }
        Label_006B:
            if (num2 != -1)
            {
                goto Label_0077;
            }
            goto Label_01E5;
        Label_0077:
            item = Object.Instantiate<ChangeListItem>(this.ListItems[num2]);
            if (data.MetaData == null)
            {
                goto Label_00B4;
            }
            if (data.MetaDataType == null)
            {
                goto Label_00B4;
            }
            DataSource.Bind(item.get_gameObject(), data.MetaDataType, data.MetaData);
        Label_00B4:
            if ((item.ValNew != null) == null)
            {
                goto Label_00ED;
            }
            if (string.IsNullOrEmpty(data.ValNew) != null)
            {
                goto Label_00ED;
            }
            item.ValNew.set_text(data.ValNew.ToString());
        Label_00ED:
            if ((item.ValOld != null) == null)
            {
                goto Label_0126;
            }
            if (string.IsNullOrEmpty(data.ValOld) != null)
            {
                goto Label_0126;
            }
            item.ValOld.set_text(data.ValOld.ToString());
        Label_0126:
            if ((item.Diff != null) == null)
            {
                goto Label_0196;
            }
            if (string.IsNullOrEmpty(data.ValNew) != null)
            {
                goto Label_0196;
            }
            if (string.IsNullOrEmpty(data.ValOld) != null)
            {
                goto Label_0196;
            }
            if (long.TryParse(data.ValOld, &num4) == null)
            {
                goto Label_0196;
            }
            if (long.TryParse(data.ValNew, &num5) == null)
            {
                goto Label_0196;
            }
            num6 = num5 - num4;
            item.Diff.set_text(&num6.ToString());
        Label_0196:
            if ((item.Label != null) == null)
            {
                goto Label_01CA;
            }
            if (string.IsNullOrEmpty(data.Label) != null)
            {
                goto Label_01CA;
            }
            item.Label.set_text(data.Label);
        Label_01CA:
            item.get_gameObject().SetActive(1);
            item.get_transform().SetParent(transform, 0);
        Label_01E5:
            num += 1;
        Label_01E9:
            if (num < ((int) src.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void Start()
        {
            int num;
            num = 0;
            goto Label_0048;
        Label_0007:
            if ((this.ListItems[num] != null) == null)
            {
                goto Label_0044;
            }
            if (this.ListItems[num].get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0044;
            }
            this.ListItems[num].get_gameObject().SetActive(0);
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) this.ListItems.Length))
            {
                goto Label_0007;
            }
            return;
        }
    }
}

