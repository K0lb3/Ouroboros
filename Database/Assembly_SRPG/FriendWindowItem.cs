namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class FriendWindowItem : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;
        [SerializeField]
        private Toggle block;
        [SerializeField]
        private GameObject FriendMark;
        [SerializeField]
        private GameObject BlockMark;
        [NonSerialized]
        public FlowNode_MultiPlayFriendRequest FriendRequest;
        [NonSerialized]
        public JSON_MyPhotonPlayerParam PlayerParam;
        [NonSerialized]
        public SupportData Support;
        private MultiFuid m_Friend;

        public FriendWindowItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <Refresh>m__323(MultiFuid f)
        {
            return ((f.fuid == null) ? 0 : f.fuid.Equals(this.PlayerParam.FUID));
        }

        public void Refresh(bool on)
        {
            bool flag;
            List<MultiFuid> list;
            flag = 0;
            if (this.Support == null)
            {
                goto Label_0021;
            }
            flag = this.Support.mIsFriend == 1;
            goto Label_008D;
        Label_0021:
            if (this.m_Friend != null)
            {
                goto Label_0060;
            }
            list = MonoSingleton<GameManager>.Instance.Player.MultiFuids;
            this.m_Friend = (list != null) ? list.Find(new Predicate<MultiFuid>(this.<Refresh>m__323)) : null;
        Label_0060:
            flag = ((this.m_Friend == null) || (this.m_Friend.status.Equals("friend") == null)) ? 0 : 1;
        Label_008D:
            if ((this.FriendMark != null) == null)
            {
                goto Label_00AA;
            }
            this.FriendMark.SetActive(flag);
        Label_00AA:
            if ((this.toggle != null) == null)
            {
                goto Label_00D8;
            }
            this.toggle.set_interactable((flag != null) ? 0 : (this.IsBlockOn == 0));
        Label_00D8:
            if ((this.block != null) == null)
            {
                goto Label_011B;
            }
            this.block.set_interactable(this.toggle.get_isOn() == 0);
            this.block.get_gameObject().SetActive(this.Support == null);
        Label_011B:
            this.RefreshBlockMark((flag != null) ? 0 : this.IsBlockOn);
            return;
        }

        private void RefreshBlockMark(bool _active)
        {
            if ((this.block != null) == null)
            {
                goto Label_002E;
            }
            if ((this.BlockMark != null) == null)
            {
                goto Label_002E;
            }
            this.BlockMark.SetActive(_active);
        Label_002E:
            return;
        }

        private void Start()
        {
            if ((this.toggle != null) == null)
            {
                goto Label_002D;
            }
            this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.Refresh));
        Label_002D:
            if ((this.block != null) == null)
            {
                goto Label_005A;
            }
            this.block.onValueChanged.AddListener(new UnityAction<bool>(this, this.Refresh));
        Label_005A:
            return;
        }

        public bool IsOn
        {
            get
            {
                return this.toggle.get_isOn();
            }
        }

        public bool Interactable
        {
            set
            {
                this.toggle.set_interactable(value);
                return;
            }
        }

        public bool IsBlockOn
        {
            get
            {
                return this.block.get_isOn();
            }
        }
    }
}

