namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GenericSlot : MonoBehaviour
    {
        public SelectEvent OnSelect;
        [Space(10f)]
        public Graphic MainGraphic;
        public Image BGImage;
        public Sprite EmptyBG;
        public Sprite NonEmptyBG;
        [Space(10f)]
        public SRPG_Button SelectButton;

        public GenericSlot()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.SelectButton != null) == null)
            {
                goto Label_0028;
            }
            this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
        Label_0028:
            return;
        }

        private void OnButtonClick(SRPG_Button button)
        {
            if (this.OnSelect == null)
            {
                goto Label_0028;
            }
            if (button.get_interactable() == null)
            {
                goto Label_0028;
            }
            this.OnSelect(this, button.IsInteractable());
        Label_0028:
            return;
        }

        public void SetLocked(bool locked)
        {
            GenericSlotFlags[] flagsArray;
            int num;
            flagsArray = base.GetComponentsInChildren<GenericSlotFlags>(1);
            num = 0;
            goto Label_0030;
        Label_000F:
            if ((flagsArray[num].Flags & 4) == null)
            {
                goto Label_002C;
            }
            flagsArray[num].get_gameObject().SetActive(locked);
        Label_002C:
            num += 1;
        Label_0030:
            if (num < ((int) flagsArray.Length))
            {
                goto Label_000F;
            }
            if ((this.SelectButton != null) == null)
            {
                goto Label_0059;
            }
            this.SelectButton.set_interactable(locked == 0);
        Label_0059:
            return;
        }

        public void SetMainColor(Color color)
        {
            if ((this.MainGraphic != null) == null)
            {
                goto Label_001D;
            }
            this.MainGraphic.set_color(color);
        Label_001D:
            return;
        }

        public void SetSlotData<T>(T data)
        {
            bool flag;
            GenericSlotFlags[] flagsArray;
            int num;
            DataSource.Bind<T>(base.get_gameObject(), data);
            flag = ((T) data) == null;
            if ((this.BGImage != null) == null)
            {
                goto Label_0049;
            }
            this.BGImage.set_sprite((flag == null) ? this.NonEmptyBG : this.EmptyBG);
        Label_0049:
            flagsArray = base.GetComponentsInChildren<GenericSlotFlags>(1);
            num = 0;
            goto Label_0099;
        Label_0058:
            if ((flagsArray[num].Flags & 1) == null)
            {
                goto Label_0075;
            }
            flagsArray[num].get_gameObject().SetActive(flag);
        Label_0075:
            if ((flagsArray[num].Flags & 2) == null)
            {
                goto Label_0095;
            }
            flagsArray[num].get_gameObject().SetActive(flag == 0);
        Label_0095:
            num += 1;
        Label_0099:
            if (num < ((int) flagsArray.Length))
            {
                goto Label_0058;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public delegate void SelectEvent(GenericSlot slot, bool interactable);
    }
}

