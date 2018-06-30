namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Add Block", 0, 1), Pin(11, "Output Remove Block", 1, 11), Pin(10, "Output Add Block", 1, 10), Pin(2, "Remove Block", 0, 2), Pin(0, "Refresh", 0, 0)]
    public class ChatPlayerWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Text UserName;
        [SerializeField]
        private BitmapText UserLv;
        [SerializeField]
        private Text LastLogin;
        [SerializeField]
        private GameObject Add;
        [SerializeField]
        private GameObject Remove;
        [SerializeField]
        private GameObject FriendAdd;
        [SerializeField]
        private GameObject FriendRemove;
        [SerializeField]
        private GameObject Award;
        private ChatPlayerData mPlayer;

        public ChatPlayerWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0024;

                case 2:
                    goto Label_0031;
            }
            goto Label_003E;
        Label_0019:
            this.Refresh();
            goto Label_0043;
        Label_0024:
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0043;
        Label_0031:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_0043;
        Label_003E:;
        Label_0043:
            return;
        }

        private void Awake()
        {
            if ((this.Award != null) == null)
            {
                goto Label_001D;
            }
            this.Award.SetActive(0);
        Label_001D:
            return;
        }

        private void DummyUserData()
        {
            this.mPlayer = new ChatPlayerData();
            this.mPlayer.exp = 0x2710;
            this.mPlayer.name = "TestMan";
            this.mPlayer.lv = 10;
            this.mPlayer.lastlogin = 0L;
            this.mPlayer.unit = MonoSingleton<GameManager>.Instance.Player.Units[0];
            return;
        }

        private unsafe void Refresh()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            TimeSpan span;
            int num;
            int num2;
            int num3;
            bool flag;
            Button button;
            UnitData data;
            if (this.mPlayer != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            GlobalVars.SelectedFriendID = this.mPlayer.fuid;
            if ((this.UserName != null) == null)
            {
                goto Label_0043;
            }
            this.UserName.set_text(this.mPlayer.name);
        Label_0043:
            if ((this.LastLogin != null) == null)
            {
                goto Label_0111;
            }
            time = GameUtility.UnixtimeToLocalTime(this.mPlayer.lastlogin);
            span = DateTime.Now - time;
            num = &span.Days;
            num2 = &span.Hours;
            num3 = &span.Minutes;
            if (num <= 0)
            {
                goto Label_00BB;
            }
            objArray1 = new object[] { &num.ToString() };
            this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_DAY", objArray1));
            goto Label_0111;
        Label_00BB:
            if (num2 <= 0)
            {
                goto Label_00EC;
            }
            objArray2 = new object[] { &num2.ToString() };
            this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_HOUR", objArray2));
            goto Label_0111;
        Label_00EC:
            objArray3 = new object[] { &num3.ToString() };
            this.LastLogin.set_text(LocalizedText.Get("sys.LASTLOGIN_MINUTE", objArray3));
        Label_0111:
            if ((this.UserLv != null) == null)
            {
                goto Label_013D;
            }
            this.UserLv.text = &this.mPlayer.lv.ToString();
        Label_013D:
            if ((this.Add != null) == null)
            {
                goto Label_01B1;
            }
            if ((this.Remove != null) == null)
            {
                goto Label_01B1;
            }
            if (FlowNode_Variable.Get("IsBlackList").Contains("1") == null)
            {
                goto Label_0199;
            }
            this.Remove.SetActive(1);
            this.Add.SetActive(0);
            goto Label_01B1;
        Label_0199:
            this.Remove.SetActive(0);
            this.Add.SetActive(1);
        Label_01B1:
            if ((this.FriendAdd != null) == null)
            {
                goto Label_023D;
            }
            if ((this.FriendRemove != null) == null)
            {
                goto Label_023D;
            }
            this.FriendRemove.SetActive((this.mPlayer.is_friend == 0) == 0);
            this.FriendAdd.SetActive(((this.mPlayer.is_friend == 0) == 0) == 0);
            button = this.FriendRemove.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_023D;
            }
            button.set_interactable(this.mPlayer.IsFavorite == 0);
        Label_023D:
            data = this.mPlayer.unit;
            if (data == null)
            {
                goto Label_025E;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
        Label_025E:
            if (this.mPlayer == null)
            {
                goto Label_0297;
            }
            DataSource.Bind<ChatPlayerData>(base.get_gameObject(), this.mPlayer);
            if ((this.Award != null) == null)
            {
                goto Label_0297;
            }
            this.Award.SetActive(1);
        Label_0297:
            this.FriendAdd.SetActive(this.mPlayer.IsFriend == 0);
            this.FriendRemove.SetActive(this.mPlayer.IsFriend);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public ChatPlayerData Player
        {
            get
            {
                return this.mPlayer;
            }
            set
            {
                if (value != null)
                {
                    goto Label_0011;
                }
                this.DummyUserData();
                goto Label_0018;
            Label_0011:
                this.mPlayer = value;
            Label_0018:
                return;
            }
        }
    }
}

