namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class UnitGaugeMark : MonoBehaviour
    {
        private EGemIcon mGemIconType;
        public string EndAnimationName;
        public string EndTriggerName;
        public int EndTriggerValue;
        public SRPG.UnitGauge UnitGauge;
        public EMarkType MarkType;
        public GameObject MapChest;
        public UnitGaugeGemIcon MapGem;
        private List<ObjectAnim> mActiveMarkLists;
        private bool mIsGaugeUpdate;
        private bool mIsUnitDead;
        private bool mIsUseSkill;

        public UnitGaugeMark()
        {
            this.EndAnimationName = "chest_kari_end";
            this.EndTriggerName = "mode";
            this.EndTriggerValue = 2;
            this.mActiveMarkLists = new List<ObjectAnim>(((((int) Enum.GetNames(typeof(EMarkType)).Length) - 1) + ((int) Enum.GetNames(typeof(EGemIcon)).Length)) - 1);
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <Update>m__460(ObjectAnim am)
        {
            return ((am.MarkType != this.MarkType) ? 0 : (am.GemIconType == this.mGemIconType));
        }

        public void ChangeAnimationByUnitType(EUnitType Type)
        {
            EUnitType type;
            type = Type;
            switch (type)
            {
                case 0:
                    goto Label_0035;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_0029;

                case 3:
                    goto Label_0035;
            }
            goto Label_0035;
        Label_001D:
            this.MarkType = 1;
            goto Label_003A;
        Label_0029:
            this.MarkType = 2;
            goto Label_003A;
        Label_0035:;
        Label_003A:
            this.mGemIconType = 0;
            return;
        }

        private GameObject CreateMarkObject()
        {
            GameObject obj2;
            UnitGaugeGemIcon icon;
            EMarkType type;
            obj2 = null;
            type = this.MarkType;
            if (type == 1)
            {
                goto Label_001C;
            }
            if (type == 2)
            {
                goto Label_0058;
            }
            goto Label_00AC;
        Label_001C:
            if (this.MapChest == null)
            {
                goto Label_00AC;
            }
            obj2 = Object.Instantiate(this.MapChest, base.get_transform().get_position(), base.get_transform().get_rotation()) as GameObject;
            goto Label_00AC;
        Label_0058:
            if (this.MapGem == null)
            {
                goto Label_00AC;
            }
            icon = Object.Instantiate(this.MapGem, base.get_transform().get_position(), base.get_transform().get_rotation()) as UnitGaugeGemIcon;
            icon.IconImages.ImageIndex = this.mGemIconType;
            obj2 = icon.get_gameObject();
        Label_00AC:
            return obj2;
        }

        public void DeleteIconAll()
        {
            int num;
            ObjectAnim anim;
            num = 0;
            goto Label_0034;
        Label_0007:
            anim = this.mActiveMarkLists[num];
            if (anim == null)
            {
                goto Label_0020;
            }
            anim.Release();
        Label_0020:
            this.mActiveMarkLists.RemoveAt(num--);
            num += 1;
        Label_0034:
            if (num < this.mActiveMarkLists.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private int GetUnitGaugeMode()
        {
            int num;
            return this.UnitGauge.Mode;
        }

        public bool IsUpdatable(EMarkType MarkType)
        {
            bool flag;
            SRPG.UnitGauge.GaugeMode mode;
            EMarkType type;
            flag = 0;
            mode = this.GetUnitGaugeMode();
            type = MarkType;
            if (mode != null)
            {
                goto Label_002E;
            }
            if (this.mIsGaugeUpdate != null)
            {
                goto Label_002E;
            }
            if (this.mIsUnitDead != null)
            {
                goto Label_002E;
            }
            flag = 1;
        Label_002E:
            return flag;
        }

        public unsafe void SetEndAnimation(EMarkType Type)
        {
            ObjectAnim anim;
            List<ObjectAnim>.Enumerator enumerator;
            enumerator = this.mActiveMarkLists.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0031;
            Label_0011:
                anim = &enumerator.Current;
                if (anim.MarkType == Type)
                {
                    goto Label_002A;
                }
                goto Label_0031;
            Label_002A:
                this.SetEndAnimation(anim);
            Label_0031:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_004E;
            }
            finally
            {
            Label_0042:
                ((List<ObjectAnim>.Enumerator) enumerator).Dispose();
            }
        Label_004E:
            return;
        }

        private void SetEndAnimation(ObjectAnim mark)
        {
            if (mark == null)
            {
                goto Label_0017;
            }
            if ((mark.Animator == null) == null)
            {
                goto Label_0018;
            }
        Label_0017:
            return;
        Label_0018:
            if (mark.IsEnd == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            mark.IsEnd = 1;
            mark.Animator.SetInteger(this.EndTriggerName, this.EndTriggerValue);
            return;
        }

        public unsafe void SetEndAnimationAll()
        {
            ObjectAnim anim;
            List<ObjectAnim>.Enumerator enumerator;
            enumerator = this.mActiveMarkLists.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0020;
            Label_0011:
                anim = &enumerator.Current;
                this.SetEndAnimation(anim);
            Label_0020:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003D;
            }
            finally
            {
            Label_0031:
                ((List<ObjectAnim>.Enumerator) enumerator).Dispose();
            }
        Label_003D:
            return;
        }

        public void SetGemIcon(EEventGimmick EventType)
        {
            EEventGimmick gimmick;
            this.MarkType = 2;
            gimmick = EventType;
            switch ((gimmick - 8))
            {
                case 0:
                    goto Label_0041;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_0035;
            }
        Label_001D:
            if (gimmick == 1)
            {
                goto Label_0029;
            }
            goto Label_004D;
        Label_0029:
            this.mGemIconType = 1;
            goto Label_0059;
        Label_0035:
            this.mGemIconType = 3;
            goto Label_0059;
        Label_0041:
            this.mGemIconType = 2;
            goto Label_0059;
        Label_004D:
            this.mGemIconType = 0;
        Label_0059:
            return;
        }

        private unsafe void Update()
        {
            int num;
            ObjectAnim anim;
            AnimatorStateInfo info;
            ObjectAnim anim2;
            GameObject obj2;
            Animator animator;
            num = 0;
            goto Label_00E2;
        Label_0007:
            anim = this.mActiveMarkLists[num];
            if (this.IsUpdatable(anim.MarkType) != null)
            {
                goto Label_0036;
            }
            anim.Object.SetActive(0);
            goto Label_00DE;
        Label_0036:
            if (anim.Object.get_activeInHierarchy() != null)
            {
                goto Label_0061;
            }
            anim.Release();
            this.mActiveMarkLists.RemoveAt(num--);
            goto Label_00DE;
        Label_0061:
            if (anim.Object.get_activeInHierarchy() == null)
            {
                goto Label_00DE;
            }
            if ((anim.Animator != null) == null)
            {
                goto Label_00DE;
            }
            info = anim.Animator.GetCurrentAnimatorStateInfo(0);
            if (&info.IsName(this.EndAnimationName) == null)
            {
                goto Label_00DE;
            }
            if (anim.Animator.IsInTransition(0) != null)
            {
                goto Label_00DE;
            }
            if (&info.get_normalizedTime() < 1f)
            {
                goto Label_00DE;
            }
            anim.Release();
            this.mActiveMarkLists.RemoveAt(num--);
        Label_00DE:
            num += 1;
        Label_00E2:
            if (num < this.mActiveMarkLists.Count)
            {
                goto Label_0007;
            }
            if (this.MarkType == null)
            {
                goto Label_0197;
            }
            if (this.IsUpdatable(this.MarkType) == null)
            {
                goto Label_0197;
            }
            if (this.mActiveMarkLists.Find(new Predicate<ObjectAnim>(this.<Update>m__460)) != null)
            {
                goto Label_0197;
            }
            obj2 = this.CreateMarkObject();
            if ((obj2 != null) == null)
            {
                goto Label_0197;
            }
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().SetAsLastSibling();
            animator = obj2.GetComponent<Animator>();
            animator.SetInteger(this.EndTriggerName, 0);
            this.mActiveMarkLists.Add(new ObjectAnim(obj2, animator, this.MarkType, this.mGemIconType));
        Label_0197:
            this.MarkType = 0;
            return;
        }

        public EGemIcon GemIconType
        {
            get
            {
                return this.mGemIconType;
            }
        }

        public bool IsGaugeUpdate
        {
            get
            {
                return this.mIsGaugeUpdate;
            }
            set
            {
                this.mIsGaugeUpdate = value;
                return;
            }
        }

        public bool IsUnitDead
        {
            get
            {
                return this.mIsUnitDead;
            }
            set
            {
                this.mIsUnitDead = value;
                return;
            }
        }

        public bool IsUseSkill
        {
            get
            {
                return this.mIsUseSkill;
            }
            set
            {
                this.mIsUseSkill = value;
                return;
            }
        }

        [Serializable]
        public enum EGemIcon
        {
            Normal,
            Heal,
            CriUp,
            MovUp
        }

        [Serializable]
        public enum EMarkType
        {
            None,
            MapChest,
            MapGem
        }

        private class ObjectAnim
        {
            public GameObject Object;
            public UnityEngine.Animator Animator;
            public bool IsEnd;
            public UnitGaugeMark.EMarkType MarkType;
            public UnitGaugeMark.EGemIcon GemIconType;

            public ObjectAnim(GameObject Object, UnityEngine.Animator Animator, UnitGaugeMark.EMarkType mark_type, UnitGaugeMark.EGemIcon gem_icon)
            {
                base..ctor();
                this.Object = Object;
                this.Animator = Animator;
                this.MarkType = mark_type;
                this.GemIconType = gem_icon;
                this.IsEnd = 0;
                return;
            }

            public void Release()
            {
                this.Animator = null;
                if ((this.Object != null) == null)
                {
                    goto Label_0023;
                }
                UnityEngine.Object.Destroy(this.Object);
            Label_0023:
                return;
            }
        }
    }
}

