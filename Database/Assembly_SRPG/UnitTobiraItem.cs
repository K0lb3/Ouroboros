namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitTobiraItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject DisableGO;
        [SerializeField]
        private GameObject EnableGO;
        [SerializeField]
        private GameObject[] LevelIconGOList;
        [SerializeField]
        private GameObject SelectedGO;
        [SerializeField]
        private GameObject LockGO;
        [SerializeField]
        private ImageArray TobiraEnableImages;
        [SerializeField]
        private ImageArray TobiraDisableImages;
        [SerializeField]
        private GameObject HilightGo;
        private UnitData mUnit;
        private SRPG.TobiraParam.Category mCategory;
        private TobiraParam mParam;

        public UnitTobiraItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <Refresh>m__487(TobiraData tobira)
        {
            return (tobira.Param.TobiraCategory == this.mCategory);
        }

        public void Initialize(UnitData unit, SRPG.TobiraParam.Category category)
        {
            this.mUnit = unit;
            this.mCategory = category;
            this.SelectedGO.SetActive(0);
            this.Refresh();
            return;
        }

        public void Refresh()
        {
            Button button;
            TobiraData data;
            bool flag;
            bool flag2;
            int num;
            button = base.GetComponent<Button>();
            if ((((button == null) == null) && (this.mCategory > 0)) && (8 > this.mCategory))
            {
                goto Label_0037;
            }
            base.get_gameObject().SetActive(0);
        Label_0037:
            this.TobiraEnableImages.ImageIndex = this.mCategory - 1;
            this.TobiraDisableImages.ImageIndex = this.mCategory - 1;
            this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraParam(this.mUnit.UnitID, this.mCategory);
            if ((this.mParam != null) && (this.mParam.Enable != null))
            {
                goto Label_00B2;
            }
            button.set_interactable(0);
            this.LockGO.SetActive(1);
            return;
        Label_00B2:
            data = this.mUnit.TobiraData.Find(new Predicate<TobiraData>(this.<Refresh>m__487));
            flag = (data == null) ? 0 : data.IsUnlocked;
            if (flag != null)
            {
                goto Label_011E;
            }
            flag2 = TobiraUtility.IsClearAllToboraConditions(this.mUnit, this.mCategory);
            this.DisableGO.SetActive(1);
            this.SetHilightAnimationEnable(flag2);
            this.EnableGO.SetActive(0);
            goto Label_013D;
        Label_011E:
            this.DisableGO.SetActive(0);
            this.SetHilightAnimationEnable(0);
            this.EnableGO.SetActive(1);
        Label_013D:
            if (this.LevelIconGOList == null)
            {
                goto Label_015B;
            }
            if (((int) this.LevelIconGOList.Length) == null)
            {
                goto Label_015B;
            }
            if (flag != null)
            {
                goto Label_015C;
            }
        Label_015B:
            return;
        Label_015C:
            num = 0;
            goto Label_019B;
        Label_0164:
            if ((this.LevelIconGOList[num] == null) == null)
            {
                goto Label_017D;
            }
            goto Label_0195;
        Label_017D:
            this.LevelIconGOList[num].SetActive(num < data.ViewLv);
        Label_0195:
            num += 1;
        Label_019B:
            if (num < ((int) this.LevelIconGOList.Length))
            {
                goto Label_0164;
            }
            return;
        }

        public void Select(bool select)
        {
            Animator animator;
            this.SelectedGO.SetActive(select);
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_002B;
            }
            animator.SetBool("select", select);
        Label_002B:
            return;
        }

        private void SetHilightAnimationEnable(bool isEnable)
        {
            Animator animator;
            if ((this.HilightGo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.HilightGo.SetActive(isEnable);
            animator = this.HilightGo.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0049;
            }
            animator.set_enabled(isEnable);
            animator.SetBool("hilit", isEnable);
        Label_0049:
            return;
        }

        public SRPG.TobiraParam.Category Category
        {
            get
            {
                return this.mCategory;
            }
        }

        public TobiraParam Param
        {
            get
            {
                return this.mParam;
            }
        }
    }
}

