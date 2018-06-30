namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TrophyRecordPullView : SRPG_ListBase
    {
        private float FRAME_MERGIN;
        [SerializeField]
        private int CREATE_CHILD_COUNT;
        [SerializeField]
        private GameObject[] original_objects;
        [SerializeField]
        private GameObject badge;
        [SerializeField]
        private VerticalLayoutGroup vertical_layout_group;
        [SerializeField]
        private Transform contents_parent;
        [SerializeField]
        private LayoutElement root_layout_element;
        [SerializeField]
        private RectTransform view_port_rect;
        [SerializeField]
        private RectTransform grid_rect;
        [SerializeField]
        private RectTransform contents_transform;
        [SerializeField]
        private RectTransform button_open_rect;
        [SerializeField]
        private RectTransform button_close_rect;
        [SerializeField]
        private BitmapText comp_trophy_count_text;
        [SerializeField]
        private BitmapText total_trophy_count_text;
        private eState state;
        private TrophyCategoryData category_data;
        private TrophyList trophy_list;
        private int comp_trophy_count;
        private int index;
        private float item_distance;
        private float view_mergin;
        private float start_button_open_size;
        [SerializeField]
        private float CLOSE_SECOND;
        private Vector2 start_pos;
        private Vector2 target_pos;
        private float default_min_height;
        private float target_view_port_size;
        private float anim_speed;
        private float move_value;
        private float DEFAULT_OPEN_SPEED_AREA;
        private float OPEN_SPEED;
        private float HI_OPEN_SPEED;
        private float HI_CLOSE_SPEED;

        public TrophyRecordPullView()
        {
            this.FRAME_MERGIN = 25f;
            this.CREATE_CHILD_COUNT = 20;
            this.state = 4;
            this.CLOSE_SECOND = 0.15f;
            this.DEFAULT_OPEN_SPEED_AREA = 500f;
            this.OPEN_SPEED = 2000f;
            this.HI_OPEN_SPEED = 20000f;
            this.HI_CLOSE_SPEED = 9000f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.badge != null) == null)
            {
                goto Label_003F;
            }
            this.button_open_rect.get_gameObject().SetActive(0);
            this.button_close_rect.get_gameObject().SetActive(1);
            this.badge.SetActive(0);
        Label_003F:
            return;
        }

        public void ChangeState(eState _new_state)
        {
            eState state;
            if (this.state != _new_state)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            switch (this.state)
            {
                case 0:
                    goto Label_002F;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_003A;

                case 3:
                    goto Label_0045;
            }
            goto Label_0050;
        Label_002F:
            this.EndOpen();
            goto Label_0050;
        Label_003A:
            this.EndClose();
            goto Label_0050;
        Label_0045:
            this.EndCloseImmediate();
        Label_0050:
            this.state = _new_state;
            state = this.state;
            if (state == 3)
            {
                goto Label_007C;
            }
            if (state == 4)
            {
                goto Label_0071;
            }
            goto Label_0087;
        Label_0071:
            this.StartClosed();
            goto Label_0087;
        Label_007C:
            this.StartCloseImmediate();
        Label_0087:
            return;
        }

        public unsafe void CreateContents()
        {
            float num;
            RectTransform transform;
            List<GameObject> list;
            int num2;
            Rect rect;
            Vector3 vector;
            Vector3 vector2;
            if (((int) this.original_objects.Length) > 0)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            if ((this.contents_parent == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            num = 0f;
            transform = null;
            list = this.CreateInstances();
            if (list != null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            num2 = 0;
            goto Label_009A;
        Label_003E:
            list[num2].get_transform().SetParent(this.contents_parent, 0);
            transform = list[num2].GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0096;
            }
            num += &transform.get_rect().get_height() * &this.grid_rect.get_localScale().y;
        Label_0096:
            num2 += 1;
        Label_009A:
            if (num2 < list.Count)
            {
                goto Label_003E;
            }
            if ((this.vertical_layout_group != null) == null)
            {
                goto Label_00CF;
            }
            num += this.vertical_layout_group.get_spacing() * ((float) (list.Count - 1));
        Label_00CF:
            this.target_view_port_size = num - &this.grid_rect.get_transform().get_localPosition().y;
            this.target_view_port_size += this.FRAME_MERGIN;
            return;
        }

        private List<GameObject> CreateInstances()
        {
            List<GameObject> list;
            ListItemEvents events;
            int num;
            int num2;
            if (this.category_data == null)
            {
                goto Label_0021;
            }
            if (this.category_data.Trophies.Count > 0)
            {
                goto Label_0023;
            }
        Label_0021:
            return null;
        Label_0023:
            list = new List<GameObject>();
            events = null;
            num = this.CREATE_CHILD_COUNT;
            num2 = 0;
            goto Label_00CA;
        Label_0039:
            if (num != null)
            {
                goto Label_0044;
            }
            goto Label_00E0;
        Label_0044:
            events = this.trophy_list.MakeTrophyPlate(this.category_data.Trophies[num2], this.category_data.Trophies[num2].IsCompleted);
            if ((events != null) == null)
            {
                goto Label_00C6;
            }
            num -= 1;
            base.AddItem(events);
            list.Add(events.get_gameObject());
            events.DisplayRectMergin = new Vector2(0f, this.view_mergin);
            events.ParentScale = this.grid_rect.get_localScale();
        Label_00C6:
            num2 += 1;
        Label_00CA:
            if (num2 < this.category_data.Trophies.Count)
            {
                goto Label_0039;
            }
        Label_00E0:
            return list;
        }

        private unsafe void EndClose()
        {
            Vector2 vector;
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
            this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
            this.button_open_rect.set_sizeDelta(new Vector2(0f, this.start_button_open_size));
            return;
        }

        protected unsafe void EndCloseImmediate()
        {
            Vector2 vector;
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
            this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
            this.button_open_rect.set_sizeDelta(new Vector2(0f, this.start_button_open_size));
            return;
        }

        private unsafe void EndOpen()
        {
            float num;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
            this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
            num = &this.view_port_rect.get_sizeDelta().y - &this.contents_transform.get_anchoredPosition().y;
            this.button_open_rect.set_sizeDelta(new Vector2(0f, num));
            return;
        }

        protected override RectTransform GetRectTransform()
        {
            return (this.trophy_list.get_transform() as RectTransform);
        }

        public void Init(string _title_str)
        {
            Text text;
            int num;
            base.get_gameObject().SetActive(1);
            text = base.GetComponentInChildren<Text>();
            if ((text != null) == null)
            {
                goto Label_0026;
            }
            text.set_text(_title_str);
        Label_0026:
            if (((int) this.original_objects.Length) <= 0)
            {
                goto Label_005B;
            }
            num = 0;
            goto Label_004D;
        Label_003B:
            this.original_objects[num].SetActive(0);
            num += 1;
        Label_004D:
            if (num < ((int) this.original_objects.Length))
            {
                goto Label_003B;
            }
        Label_005B:
            if ((this.root_layout_element != null) == null)
            {
                goto Label_007D;
            }
            this.default_min_height = this.root_layout_element.get_minHeight();
        Label_007D:
            return;
        }

        public void OnClickEvent()
        {
            this.trophy_list.SetClickTarget(this);
            return;
        }

        public unsafe void Refresh(RectTransform _scroll_trans_rect)
        {
            float num;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            if (this.State == 1)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if ((_scroll_trans_rect == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            base.ClearItems();
            this.Setup(this.index, this.trophy_list);
            this.RefreshDisplayParam();
            this.CreateContents();
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
            this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
            num = &this.view_port_rect.get_sizeDelta().y - &this.contents_transform.get_anchoredPosition().y;
            this.button_open_rect.set_sizeDelta(new Vector2(0f, num));
            this.target_pos = new Vector2(&_scroll_trans_rect.get_anchoredPosition().x, ((float) this.index) * this.item_distance);
            _scroll_trans_rect.set_anchoredPosition(new Vector2(0f, &this.target_pos.y));
            return;
        }

        public unsafe void RefreshDisplayParam()
        {
            int num;
            int num2;
            int num3;
            this.comp_trophy_count = 0;
            num = 0;
            goto Label_0040;
        Label_000E:
            if (this.category_data.Trophies[num].IsCompleted != null)
            {
                goto Label_002E;
            }
            goto Label_003C;
        Label_002E:
            this.comp_trophy_count += 1;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < this.category_data.Trophies.Count)
            {
                goto Label_000E;
            }
            if ((this.comp_trophy_count_text != null) == null)
            {
                goto Label_00CA;
            }
            if ((this.total_trophy_count_text != null) == null)
            {
                goto Label_00CA;
            }
            num2 = Mathf.Min(this.CREATE_CHILD_COUNT, this.comp_trophy_count);
            num3 = Mathf.Min(this.CREATE_CHILD_COUNT, this.category_data.Trophies.Count);
            this.comp_trophy_count_text.text = &num2.ToString();
            this.total_trophy_count_text.text = &num3.ToString();
        Label_00CA:
            if ((this.badge != null) == null)
            {
                goto Label_00EF;
            }
            this.badge.SetActive(this.comp_trophy_count > 0);
        Label_00EF:
            return;
        }

        public void SetCategoryData(TrophyCategoryData _category_data)
        {
            this.category_data = _category_data;
            return;
        }

        public unsafe void Setup(int _index, TrophyList _trophy_list)
        {
            Vector2 vector;
            Vector2 vector2;
            this.index = _index;
            this.item_distance = Mathf.Abs(&this.contents_transform.get_anchoredPosition().y + &this.grid_rect.get_anchoredPosition().y);
            this.trophy_list = _trophy_list;
            this.view_mergin = this.item_distance * ((float) (this.index + 1));
            return;
        }

        public void StartClose()
        {
            float num;
            if ((this.contents_parent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.target_view_port_size / this.CLOSE_SECOND;
            this.anim_speed = Mathf.Min(num, this.HI_CLOSE_SPEED);
            this.target_view_port_size = 0f;
            this.ChangeState(2);
            return;
        }

        private void StartClosed()
        {
            base.ClearItems();
            this.button_open_rect.get_gameObject().SetActive(0);
            this.button_close_rect.get_gameObject().SetActive(1);
            return;
        }

        protected void StartCloseImmediate()
        {
            if ((this.contents_parent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.target_view_port_size = 0f;
            return;
        }

        public unsafe void StartOpen()
        {
            Vector2 vector;
            this.move_value = 0f;
            this.anim_speed = this.OPEN_SPEED;
            this.button_open_rect.get_gameObject().SetActive(1);
            this.button_close_rect.get_gameObject().SetActive(0);
            this.start_button_open_size = &this.button_open_rect.get_sizeDelta().y;
            this.ChangeState(0);
            return;
        }

        private void Update()
        {
            eState state;
            switch (this.state)
            {
                case 0:
                    goto Label_0026;

                case 1:
                    goto Label_0047;

                case 2:
                    goto Label_0031;

                case 3:
                    goto Label_003C;

                case 4:
                    goto Label_0047;
            }
            goto Label_004C;
        Label_0026:
            this.UpdateOpen();
            goto Label_004C;
        Label_0031:
            this.UpdateClose();
            goto Label_004C;
        Label_003C:
            this.UpdateCloseImmediate();
            goto Label_004C;
        Label_0047:;
        Label_004C:
            return;
        }

        private unsafe void UpdateClose()
        {
            float num;
            float num2;
            float num3;
            float num4;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            num = this.anim_speed * Time.get_deltaTime();
            num2 = &this.view_port_rect.get_sizeDelta().y - num;
            num2 = Mathf.Max(num2, this.target_view_port_size);
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, num2));
            num3 = this.root_layout_element.get_minHeight() - num;
            num3 = Mathf.Max(num3, this.default_min_height + this.target_view_port_size);
            this.root_layout_element.set_minHeight(num3);
            num4 = &this.view_port_rect.get_sizeDelta().y - &this.contents_transform.get_anchoredPosition().y;
            num4 = Mathf.Max(num4, this.start_button_open_size);
            this.button_open_rect.set_sizeDelta(new Vector2(0f, num4));
            if (num2 > this.target_view_port_size)
            {
                goto Label_00E4;
            }
            this.ChangeState(4);
        Label_00E4:
            return;
        }

        protected void UpdateCloseImmediate()
        {
            this.ChangeState(4);
            return;
        }

        private unsafe void UpdateOpen()
        {
            float num;
            float num2;
            float num3;
            float num4;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            num = this.anim_speed * Time.get_deltaTime();
            this.move_value += num;
            if (this.move_value < this.DEFAULT_OPEN_SPEED_AREA)
            {
                goto Label_0038;
            }
            this.anim_speed = this.HI_OPEN_SPEED;
        Label_0038:
            num2 = &this.view_port_rect.get_sizeDelta().y + num;
            num2 = Mathf.Min(num2, this.target_view_port_size);
            this.view_port_rect.set_sizeDelta(new Vector2(&this.view_port_rect.get_sizeDelta().x, num2));
            num3 = this.root_layout_element.get_minHeight() + num;
            num3 = Mathf.Min(num3, this.default_min_height + this.target_view_port_size);
            this.root_layout_element.set_minHeight(num3);
            num4 = &this.view_port_rect.get_sizeDelta().y - &this.contents_transform.get_anchoredPosition().y;
            num4 = Mathf.Max(num4, this.start_button_open_size);
            this.button_open_rect.set_sizeDelta(new Vector2(0f, num4));
            if (num2 < this.target_view_port_size)
            {
                goto Label_010F;
            }
            this.ChangeState(1);
        Label_010F:
            return;
        }

        private eState State
        {
            get
            {
                return this.state;
            }
        }

        public int HashCode
        {
            get
            {
                return this.category_data.Param.hash_code;
            }
        }

        public bool IsStateOpen
        {
            get
            {
                return ((this.state == null) ? 1 : (this.state == 1));
            }
        }

        public bool IsStateOpened
        {
            get
            {
                return (this.state == 1);
            }
        }

        public bool IsStateClose
        {
            get
            {
                return (((this.state == 2) || (this.state == 4)) ? 1 : (this.state == 3));
            }
        }

        public bool IsStateClosed
        {
            get
            {
                return (this.state == 4);
            }
        }

        public float RootLayoutElementMinHeightDef
        {
            get
            {
                return (this.root_layout_element.get_minHeight() - this.default_min_height);
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
        }

        public float ItemDistance
        {
            get
            {
                return this.item_distance;
            }
        }

        public float TargetViewPortSize
        {
            get
            {
                return this.target_view_port_size;
            }
        }

        public float VerticalLayoutSpacing
        {
            get
            {
                return this.vertical_layout_group.get_spacing();
            }
        }

        public enum eState
        {
            OPEN,
            OPENED,
            CLOSE,
            CLOSE_IMMEDIATE,
            CLOSED
        }
    }
}

