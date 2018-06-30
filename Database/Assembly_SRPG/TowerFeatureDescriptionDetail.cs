namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "次のページボタン", 0, 1), Pin(2, "前のページボタン", 0, 2)]
    public class TowerFeatureDescriptionDetail : MonoBehaviour, IFlowInterface
    {
        private const int PIN_NEXT_BUTTON = 1;
        private const int PIN_PREV_BUTTON = 2;
        [SerializeField]
        private ImageArray ImageData;
        [SerializeField]
        private Button PrevButton;
        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private GameObject ParentPageIcon;
        [SerializeField]
        private GameObject TemplatePageIcon;
        private List<Toggle> mToggleIconList;

        public TowerFeatureDescriptionDetail()
        {
            this.mToggleIconList = new List<Toggle>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_0058;
            }
            goto Label_008D;
        Label_0015:
            if (this.ImageData.ImageIndex >= (((int) this.ImageData.Images.Length) - 1))
            {
                goto Label_008D;
            }
            this.ImageData.ImageIndex += 1;
            this.SetButtonInteractable();
            this.SetTobbleIcon();
            goto Label_008D;
        Label_0058:
            if (this.ImageData.ImageIndex <= 0)
            {
                goto Label_008D;
            }
            this.ImageData.ImageIndex -= 1;
            this.SetButtonInteractable();
            this.SetTobbleIcon();
        Label_008D:
            return;
        }

        private void SetButtonInteractable()
        {
            if (((int) this.ImageData.Images.Length) != 1)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (this.ImageData.ImageIndex != (((int) this.ImageData.Images.Length) - 1))
            {
                goto Label_0050;
            }
            this.NextButton.set_interactable(0);
            this.PrevButton.set_interactable(1);
            goto Label_0078;
        Label_0050:
            if (this.ImageData.ImageIndex != null)
            {
                goto Label_0078;
            }
            this.NextButton.set_interactable(1);
            this.PrevButton.set_interactable(0);
        Label_0078:
            return;
        }

        private void SetTobbleIcon()
        {
            int num;
            num = 0;
            goto Label_0045;
        Label_0007:
            if (num != this.ImageData.ImageIndex)
            {
                goto Label_002F;
            }
            this.mToggleIconList[num].set_isOn(1);
            goto Label_0041;
        Label_002F:
            this.mToggleIconList[num].set_isOn(0);
        Label_0041:
            num += 1;
        Label_0045:
            if (num < this.mToggleIconList.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private unsafe void Start()
        {
            int num;
            GameObject obj2;
            Vector2 vector;
            Toggle toggle;
            int num2;
            if ((this.ImageData == null) == null)
            {
                goto Label_002E;
            }
            if (((int) this.ImageData.Images.Length) != null)
            {
                goto Label_002E;
            }
            Debug.LogError("ImageData not data.");
            return;
        Label_002E:
            this.ImageData.ImageIndex = 0;
            if (((int) this.ImageData.Images.Length) != 1)
            {
                goto Label_0074;
            }
            this.NextButton.get_gameObject().SetActive(0);
            this.PrevButton.get_gameObject().SetActive(0);
            goto Label_0080;
        Label_0074:
            this.PrevButton.set_interactable(0);
        Label_0080:
            this.TemplatePageIcon.SetActive(0);
            num = 0;
            goto Label_011C;
        Label_0093:
            obj2 = Object.Instantiate<GameObject>(this.TemplatePageIcon);
            vector = obj2.get_transform().get_localScale();
            obj2.get_transform().SetParent(this.ParentPageIcon.get_transform());
            obj2.get_transform().set_localScale(vector);
            obj2.get_gameObject().SetActive(1);
            num2 = num + 1;
            obj2.set_name(this.TemplatePageIcon.get_name() + &num2.ToString());
            toggle = obj2.GetComponent<Toggle>();
            this.mToggleIconList.Add(toggle);
            num += 1;
        Label_011C:
            if (num < ((int) this.ImageData.Images.Length))
            {
                goto Label_0093;
            }
            this.mToggleIconList[0].set_isOn(1);
            return;
        }

        private void Update()
        {
        }
    }
}

