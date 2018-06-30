namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleStamp : MonoBehaviour
    {
        public SelectEvent OnSelectItem;
        public RectTransform ListParent;
        public ListItemEvents ItemTemplate;
        public Sprite[] Sprites;
        public GameObject[] Prefabs;
        public string SpriteGameObjectID;
        public string SelectCursorGameObjectID;
        private List<ListItemEvents> mItems;
        private int mSelectID;

        public BattleStamp()
        {
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Refresh>m__296(GameObject go)
        {
            int num;
            GameObjectID[] tidArray;
            int num2;
            Sprite sprite;
            <Refresh>c__AnonStorey302 storey;
            storey = new <Refresh>c__AnonStorey302();
            storey.go = go;
            storey.<>f__this = this;
            this.mSelectID = this.mItems.FindIndex(new Predicate<ListItemEvents>(storey.<>m__297));
            num = 0;
            goto Label_009B;
        Label_003C:
            tidArray = this.mItems[num].GetComponentsInChildren<GameObjectID>(1);
            if (tidArray == null)
            {
                goto Label_0097;
            }
            num2 = 0;
            goto Label_008E;
        Label_005C:
            if (tidArray[num2].ID.Equals(this.SelectCursorGameObjectID) == null)
            {
                goto Label_008A;
            }
            tidArray[num2].get_gameObject().SetActive(num == this.mSelectID);
        Label_008A:
            num2 += 1;
        Label_008E:
            if (num2 < ((int) tidArray.Length))
            {
                goto Label_005C;
            }
        Label_0097:
            num += 1;
        Label_009B:
            if (num < this.mItems.Count)
            {
                goto Label_003C;
            }
            if (this.mSelectID >= 0)
            {
                goto Label_00B9;
            }
            return;
        Label_00B9:
            sprite = this.Sprites[this.mSelectID];
            if (sprite == null)
            {
                goto Label_00E9;
            }
            if (this.OnSelectItem == null)
            {
                goto Label_00E9;
            }
            this.OnSelectItem(sprite);
        Label_00E9:
            return;
        }

        public unsafe void Refresh()
        {
            int num;
            ListItemEvents events;
            ListItemEvents events2;
            GameObjectID[] tidArray;
            int num2;
            Image image;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            if ((((this.ItemTemplate == null) == null) && ((this.ListParent == null) == null)) && (this.Sprites != null))
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            num = 0;
            goto Label_0169;
        Label_0040:
            events2 = Object.Instantiate<ListItemEvents>(this.ItemTemplate);
            events2.get_gameObject().SetActive(1);
            events2.get_gameObject().set_name(events2.get_gameObject().get_name() + ":" + &num.ToString());
            events2.get_transform().SetParent(this.ListParent, 0);
            tidArray = events2.GetComponentsInChildren<GameObjectID>();
            if (tidArray == null)
            {
                goto Label_0147;
            }
            num2 = 0;
            goto Label_013D;
        Label_00A8:
            if (tidArray[num2].ID.Equals(this.SpriteGameObjectID) == null)
            {
                goto Label_0107;
            }
            image = ((tidArray[num2] == null) == null) ? tidArray[num2].get_gameObject().GetComponent<Image>() : null;
            if ((image != null) == null)
            {
                goto Label_0137;
            }
            image.set_sprite(this.Sprites[num]);
            goto Label_0137;
        Label_0107:
            if (tidArray[num2].ID.Equals(this.SelectCursorGameObjectID) == null)
            {
                goto Label_0137;
            }
            tidArray[num2].get_gameObject().SetActive(num == this.mSelectID);
        Label_0137:
            num2 += 1;
        Label_013D:
            if (num2 < ((int) tidArray.Length))
            {
                goto Label_00A8;
            }
        Label_0147:
            this.mItems.Add(events2);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.<Refresh>m__296);
            num += 1;
        Label_0169:
            if (num < ((int) this.Sprites.Length))
            {
                goto Label_0040;
            }
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_004E;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
            if ((this.ListParent == null) == null)
            {
                goto Label_004E;
            }
            this.ListParent = this.ItemTemplate.get_transform().get_parent() as RectTransform;
        Label_004E:
            this.Refresh();
            return;
        }

        public int SelectStampID
        {
            get
            {
                return this.mSelectID;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey302
        {
            internal GameObject go;
            internal BattleStamp <>f__this;

            public <Refresh>c__AnonStorey302()
            {
                base..ctor();
                return;
            }

            internal bool <>m__297(ListItemEvents it)
            {
                return (it.get_gameObject() == this.go.get_gameObject());
            }
        }

        public delegate void SelectEvent(Sprite sprite);
    }
}

