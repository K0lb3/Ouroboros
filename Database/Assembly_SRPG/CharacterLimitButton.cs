namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [AddComponentMenu("CharacterLimitButton")]
    public class CharacterLimitButton : MonoBehaviour
    {
        [SerializeField]
        public InputfieldSet[] target_list;
        [SerializeField]
        public Button target_button;

        public CharacterLimitButton()
        {
            base..ctor();
            return;
        }

        public void OnInputFieldChange(string value)
        {
            bool flag;
            InputfieldSet set;
            InputfieldSet[] setArray;
            int num;
            int num2;
            if ((this.target_button == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            flag = 1;
            setArray = this.target_list;
            num = 0;
            goto Label_005D;
        Label_0022:
            set = setArray[num];
            num2 = set.input.get_text().Length;
            if (set.min_length > num2)
            {
                goto Label_0052;
            }
            if (num2 <= set.max_length)
            {
                goto Label_0059;
            }
        Label_0052:
            flag = 0;
            goto Label_0066;
        Label_0059:
            num += 1;
        Label_005D:
            if (num < ((int) setArray.Length))
            {
                goto Label_0022;
            }
        Label_0066:
            this.target_button.set_interactable(flag);
            return;
        }

        private void Start()
        {
            InputfieldSet set;
            InputfieldSet[] setArray;
            int num;
            if ((this.target_button == null) != null)
            {
                goto Label_001C;
            }
            if (this.target_list != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            setArray = this.target_list;
            num = 0;
            goto Label_006B;
        Label_002B:
            set = setArray[num];
            if ((set.input != null) == null)
            {
                goto Label_0067;
            }
            set.input.get_onValueChanged().AddListener(new UnityAction<string>(this, this.OnInputFieldChange));
            this.OnInputFieldChange(string.Empty);
        Label_0067:
            num += 1;
        Label_006B:
            if (num < ((int) setArray.Length))
            {
                goto Label_002B;
            }
            return;
        }

        [Serializable]
        public class InputfieldSet
        {
            public InputField input;
            public int min_length;
            public int max_length;

            public InputfieldSet()
            {
                base..ctor();
                return;
            }
        }
    }
}

