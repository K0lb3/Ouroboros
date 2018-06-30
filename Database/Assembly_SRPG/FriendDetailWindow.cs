namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("")]
    public class FriendDetailWindow : MonoBehaviour
    {
        public string ToolTipPrefab;
        private SerializeValueList m_ValueList;
        private Mode m_Mode;
        private FriendData m_FriendData;
        private ChatPlayerData m_ChatPlayerData;
        private SupportElementListRootWindow m_ElementWindow;
        private GameObject m_ToolTip;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map12;

        public FriendDetailWindow()
        {
            this.ToolTipPrefab = string.Empty;
            this.m_Mode = 1;
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        public void Bind()
        {
            Mode mode;
            switch ((this.m_Mode - 1))
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0028;

                case 2:
                    goto Label_0042;

                case 3:
                    goto Label_005C;

                case 4:
                    goto Label_005C;
            }
            goto Label_0097;
        Label_0028:
            if (GlobalVars.SelectedFriend == null)
            {
                goto Label_0097;
            }
            this.SetFriendData(GlobalVars.SelectedFriend);
            goto Label_0097;
        Label_0042:
            if (GlobalVars.FoundFriend == null)
            {
                goto Label_0097;
            }
            this.SetFriendData(GlobalVars.FoundFriend);
            goto Label_0097;
        Label_005C:
            if (GlobalVars.SelectedFriend == null)
            {
                goto Label_0076;
            }
            this.SetFriendData(GlobalVars.SelectedFriend);
            goto Label_0092;
        Label_0076:
            if (this.m_ChatPlayerData == null)
            {
                goto Label_0097;
            }
            this.SetFriendData(this.m_ChatPlayerData.ToFriendData());
        Label_0092:;
        Label_0097:
            if (this.m_FriendData == null)
            {
                goto Label_00E9;
            }
            this.m_ValueList.SetField("fuid", this.m_FriendData.FUID);
            GlobalVars.SelectedFriend = this.m_FriendData;
            GlobalVars.SelectedFriendID = this.m_FriendData.FUID;
            DataSource.Bind<FriendData>(base.get_gameObject(), this.m_FriendData);
        Label_00E9:
            return;
        }

        public unsafe void OnEvent(string key, string value)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            str = key;
            if (str == null)
            {
                goto Label_00CD;
            }
            if (<>f__switch$map12 != null)
            {
                goto Label_005B;
            }
            dictionary = new Dictionary<string, int>(5);
            dictionary.Add("START", 0);
            dictionary.Add("OPEN", 1);
            dictionary.Add("REFRESH", 2);
            dictionary.Add("SELECT", 3);
            dictionary.Add("HOLD", 4);
            <>f__switch$map12 = dictionary;
        Label_005B:
            if (<>f__switch$map12.TryGetValue(str, &num) == null)
            {
                goto Label_00CD;
            }
            switch (num)
            {
                case 0:
                    goto Label_008C;

                case 1:
                    goto Label_00A1;

                case 2:
                    goto Label_00AC;

                case 3:
                    goto Label_00B7;

                case 4:
                    goto Label_00C2;
            }
            goto Label_00CD;
        Label_008C:
            this.Setup(EventCall.currentValue as SerializeValueList);
            goto Label_00CD;
        Label_00A1:
            this.OnEvent_Open();
            goto Label_00CD;
        Label_00AC:
            this.Refresh();
            goto Label_00CD;
        Label_00B7:
            this.OnEvent_ToolTip();
            goto Label_00CD;
        Label_00C2:
            this.OnEvent_ToolTip();
        Label_00CD:
            return;
        }

        private void OnEvent_Open()
        {
            this.Refresh();
            return;
        }

        private void OnEvent_ToolTip()
        {
            SerializeValueList list;
            UnitData data;
            GameObject obj2;
            GameObject obj3;
            UnitJobDropdown dropdown;
            Selectable selectable;
            Image image;
            ArtifactSlots slots;
            AbilitySlots slots2;
            ConceptCardSlots slots3;
            if ((this.m_ToolTip != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_013E;
            }
            data = list.GetDataSource<UnitData>("_self");
            if (data == null)
            {
                goto Label_013E;
            }
            if (string.IsNullOrEmpty(this.ToolTipPrefab) != null)
            {
                goto Label_013E;
            }
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.ToolTipPrefab));
            DataSource.Bind<UnitData>(obj3, data);
            dropdown = obj3.GetComponentInChildren<UnitJobDropdown>();
            if ((dropdown != null) == null)
            {
                goto Label_00DA;
            }
            dropdown.get_gameObject().SetActive(1);
            selectable = dropdown.get_gameObject().GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_00A4;
            }
            selectable.set_interactable(0);
        Label_00A4:
            image = dropdown.get_gameObject().GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00DA;
            }
            image.set_color(new Color(0.5f, 0.5f, 0.5f));
        Label_00DA:
            slots = obj3.GetComponentInChildren<ArtifactSlots>();
            slots2 = obj3.GetComponentInChildren<AbilitySlots>();
            if ((slots != null) == null)
            {
                goto Label_0114;
            }
            if ((slots2 != null) == null)
            {
                goto Label_0114;
            }
            slots.Refresh(0);
            slots2.Refresh(0);
        Label_0114:
            slots3 = obj3.GetComponentInChildren<ConceptCardSlots>();
            if ((slots3 != null) == null)
            {
                goto Label_0131;
            }
            slots3.Refresh(0);
        Label_0131:
            GameParameter.UpdateAll(obj3);
            this.m_ToolTip = obj3;
        Label_013E:
            return;
        }

        public void Refresh()
        {
            bool flag;
            bool flag2;
            Mode mode;
            flag = 0;
            if ((this.m_ElementWindow != null) == null)
            {
                goto Label_002F;
            }
            this.m_ElementWindow.SetSupportUnitData(this.m_ValueList.GetObject<UnitData[]>("data_units", null));
        Label_002F:
            this.Bind();
            this.m_ValueList.SetActive(this.m_Mode, 1);
            switch ((this.m_Mode - 1))
            {
                case 0:
                    goto Label_006F;

                case 1:
                    goto Label_0076;

                case 2:
                    goto Label_007B;

                case 3:
                    goto Label_0080;

                case 4:
                    goto Label_0080;
            }
            goto Label_01D6;
        Label_006F:
            flag = 1;
            goto Label_01D6;
        Label_0076:
            goto Label_01D6;
        Label_007B:
            goto Label_01D6;
        Label_0080:
            flag2 = (this.m_ChatPlayerData == null) ? 0 : (this.m_ChatPlayerData.fuid == MonoSingleton<GameManager>.Instance.Player.FUID);
            if (this.m_Mode != 5)
            {
                goto Label_0102;
            }
            this.m_ValueList.SetActive(4, 1);
            this.m_ValueList.SetActive("btn_block", 1);
            this.m_ValueList.SetActive("btn_block_on", 1);
            this.m_ValueList.SetActive("btn_block_off", 0);
            goto Label_0138;
        Label_0102:
            this.m_ValueList.SetActive("btn_block", 1);
            this.m_ValueList.SetActive("btn_block_on", 0);
            this.m_ValueList.SetActive("btn_block_off", 1);
        Label_0138:
            if (flag2 != null)
            {
                goto Label_01AD;
            }
            if (this.m_ChatPlayerData == null)
            {
                goto Label_01D6;
            }
            if (this.m_ChatPlayerData.IsFriend == null)
            {
                goto Label_0184;
            }
            flag = 1;
            this.m_ValueList.SetActive("btn_block_friend_add", 0);
            this.m_ValueList.SetActive("btn_block_friend_remove", 1);
            goto Label_01A8;
        Label_0184:
            this.m_ValueList.SetActive("btn_block_friend_add", 1);
            this.m_ValueList.SetActive("btn_block_friend_remove", 0);
        Label_01A8:
            goto Label_01D1;
        Label_01AD:
            this.m_ValueList.SetActive("btn_block", 0);
            this.m_ValueList.SetActive("btn_block_friend", 0);
        Label_01D1:;
        Label_01D6:
            if (flag == null)
            {
                goto Label_0244;
            }
            if (this.m_FriendData == null)
            {
                goto Label_0220;
            }
            if (this.m_FriendData.IsFavorite == null)
            {
                goto Label_0220;
            }
            this.m_ValueList.SetActive("btn_favorite_on", 1);
            this.m_ValueList.SetActive("btn_favorite_off", 0);
            goto Label_0244;
        Label_0220:
            this.m_ValueList.SetActive("btn_favorite_on", 0);
            this.m_ValueList.SetActive("btn_favorite_off", 1);
        Label_0244:
            this.m_ValueList.SetActive("btn_favorite", flag);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void SetChatPlayerData(ChatPlayerData data)
        {
            this.m_ChatPlayerData = data;
            this.m_ValueList.SetField("fuid", data.fuid);
            return;
        }

        private void SetFriendData(FriendData data)
        {
            this.m_FriendData = data;
            return;
        }

        private void Setup(SerializeValueList valueList)
        {
            if (valueList == null)
            {
                goto Label_0012;
            }
            this.m_ValueList = valueList;
            goto Label_001D;
        Label_0012:
            this.m_ValueList = new SerializeValueList();
        Label_001D:
            this.m_ValueList.SetActive(1, 0);
            this.m_ValueList.SetActive(2, 0);
            this.m_ValueList.SetActive(3, 0);
            this.m_ValueList.SetActive(4, 0);
            this.m_ValueList.SetActive(5, 0);
            if (this.m_ValueList.GetBool("notification") == null)
            {
                goto Label_007F;
            }
            this.m_Mode = 2;
            goto Label_00F5;
        Label_007F:
            if (this.m_ValueList.GetBool("search") == null)
            {
                goto Label_00A0;
            }
            this.m_Mode = 3;
            goto Label_00F5;
        Label_00A0:
            if (this.m_ValueList.GetBool("block") == null)
            {
                goto Label_00C7;
            }
            this.m_Mode = 4;
            GlobalVars.SelectedFriend = null;
            goto Label_00F5;
        Label_00C7:
            if (this.m_ValueList.GetBool("chat") == null)
            {
                goto Label_00EE;
            }
            this.m_Mode = 5;
            GlobalVars.SelectedFriend = null;
            goto Label_00F5;
        Label_00EE:
            this.m_Mode = 1;
        Label_00F5:
            this.m_ValueList.SetField("mode", ((Mode) this.m_Mode).ToString());
            this.m_ElementWindow = this.m_ValueList.GetComponent<SupportElementListRootWindow>("element_window");
            this.Bind();
            return;
        }

        public enum Mode
        {
            NONE,
            DEFAULT,
            NOTIFICATION,
            SEARCH,
            BLOCK,
            CHAT
        }
    }
}

