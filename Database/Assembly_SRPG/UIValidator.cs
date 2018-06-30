namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [RequireComponent(typeof(Selectable)), DisallowMultipleComponent]
    public class UIValidator : MonoBehaviour
    {
        public static List<UIValidator> Validators;
        [BitMask]
        public CriticalSections Mask;
        public InputField Input;
        [BitMask]
        public ToggleMasks ToggleMask;

        static UIValidator()
        {
            Validators = new List<UIValidator>();
            return;
        }

        public UIValidator()
        {
            this.ToggleMask = 1;
            base..ctor();
            return;
        }

        private unsafe bool IsEmoji(char a)
        {
            string str;
            if (char.IsSurrogate(a) == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            str = "[-]|[-]|[-]|[-]|\x00ee[\x0098-\x009d][\x0080-\x00bf]|(?:\x00ee[\x00b1-\x00b3\x00b5\x00b6\x00bd-\x00bf]|\x00ef[\x0081-\x0083])[\x0080-\x00bf]|\x00ee[\x0080\x0081\x0084\x0085\x0088\x0089\x008c\x008d\x0090\x0091\x0094][\x0080-\x00bf]|(?:(?:#|[0-9])⃣)";
            if (Regex.IsMatch(&a.ToString(), str) == null)
            {
                goto Label_0027;
            }
            return 1;
        Label_0027:
            return 0;
        }

        private bool IsEmojiWork(char a)
        {
            string str;
            int[] numArray;
            int num;
            int[] numArray2;
            int num2;
            str = "\x00a9\x00ae   ‼⁉™ℹ↩↪⌚⌛⏩⏪⏫⏬⏰⏳Ⓜ▪▫▶◀◻◼◽◾☀☁☎☑☔☕☝☺♈♉♊♋♌♍♎♏♐♑♒♓♠♣♥♦♨♻♿⚓⚠⚡⚪⚫⚽⚾⛄⛅⛎⛔⛪⛲⛳⛵⛺⛽✂✅✈✉✊✋✌✏✒✔✖✨✳✴❄❇❌❎❓❔❕❗❤➕➖➗➡➰⤴⤵⬅⬆⬇⬛⬜⭐⭕〰〽㊗㊙";
            if (str.IndexOf(a) < 0)
            {
                goto Label_0015;
            }
            return 1;
        Label_0015:
            numArray = new int[] { 
                0x1f004, 0x1f0cf, 0x1f170, 0x1f171, 0x1f17e, 0x1f17f, 0x1f18e, 0x1f201, 0x1f202, 0x1f21a, 0x1f22f, 0x1f250, 0x1f251, 0x1f30f, 0x1f311, 0x1f313,
                0x1f314, 0x1f315, 0x1f319, 0x1f31b, 0x1f31f, 0x1f320, 0x1f330, 0x1f331, 0x1f334, 0x1f335, 0x1f3c6, 0x1f3c8, 0x1f3ca, 0x1f40c, 0x1f40d, 0x1f40e,
                0x1f411, 0x1f412, 0x1f414, 0x1f417, 0x1f418, 0x1f419, 0x1f440, 0x1f4ee, 0x1f503, 0x1f60f, 0x1f612, 0x1f613, 0x1f614, 0x1f616, 0x1f618, 0x1f61a,
                0x1f61c, 0x1f61d, 0x1f61e, 0x1f62d, 0x1f635, 0x1f683, 0x1f684, 0x1f685, 0x1f687, 0x1f689, 0x1f68c, 0x1f68f, 0x1f691, 0x1f692, 0x1f693, 0x1f695,
                0x1f697, 0x1f699, 0x1f69a, 0x1f6a2, 0x1f6a4, 0x1f6a5, 0x1f6b2, 0x1f6b6, 0x1f6b9, 0x1f6c0, 0x1f1e8, 0x1f1f3, 0x1f1e9, 0x1f1ea, 0x1f1ea, 0x1f1f8,
                0x1f1eb, 0x1f1f7, 0x1f1ec, 0x1f1e7, 0x1f1ee, 0x1f1f9, 0x1f1ef, 0x1f1f5, 0x1f1f0, 0x1f1f7, 0x1f1f7, 0x1f1fa, 0x1f1fa, 0x1f1f8
            };
            num = 0;
            goto Label_003E;
        Label_002F:
            if (numArray[num] != a)
            {
                goto Label_003A;
            }
            return 1;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) numArray.Length))
            {
                goto Label_002F;
            }
            numArray2 = new int[] { 
                0x2194, 0x2199, 0x1f191, 0x1f19a, 0x1f232, 0x1f23a, 0x1f300, 0x1f30c, 0x1f337, 0x1f34f, 0x1f351, 0x1f37b, 0x1f380, 0x1f393, 0x1f3a0, 0x1f3c4,
                0x1f3e0, 0x1f3e3, 0x1f3e5, 0x1f3f0, 0x1f41a, 0x1f429, 0x1f42b, 0x1f43e, 0x1f442, 0x1f464, 0x1f466, 0x1f46b, 0x1f46e, 0x1f4ac, 0x1f4ae, 0x1f4b5,
                0x1f4b8, 0x1f4eb, 0x1f4f0, 0x1f4f7, 0x1f4f9, 0x1f4fc, 0x1f50a, 0x1f514, 0x1f516, 0x1f52b, 0x1f52e, 0x1f53d, 0x1f550, 0x1f55b, 0x1f5fb, 0x1f5ff,
                0x1f601, 0x1f606, 0x1f609, 0x1f60d, 0x1f620, 0x1f625, 0x1f628, 0x1f62b, 0x1f630, 0x1f633, 0x1f637, 0x1f640, 0x1f645, 0x1f680, 0x1f6a7, 0x1f6ad,
                0x1f6ba, 0x1f6be
            };
            num2 = 0;
            goto Label_0080;
        Label_0062:
            if (numArray2[num2] > a)
            {
                goto Label_007A;
            }
            if (a > numArray2[num2 + 1])
            {
                goto Label_007A;
            }
            return 1;
        Label_007A:
            num2 += 2;
        Label_0080:
            if (num2 < ((int) numArray2.Length))
            {
                goto Label_0062;
            }
            return 0;
        }

        private void OnDisable()
        {
            Validators.Remove(this);
            return;
        }

        private void OnEnable()
        {
            Validators.Add(this);
            return;
        }

        private void OnInputFieldChange(string value)
        {
            int num;
            num = 0;
            goto Label_0034;
        Label_0007:
            if (this.IsEmoji(value[num]) == null)
            {
                goto Label_0030;
            }
            this.Input.set_text(value.Remove(num));
            goto Label_0040;
        Label_0030:
            num += 1;
        Label_0034:
            if (num < value.Length)
            {
                goto Label_0007;
            }
        Label_0040:
            this.UpdateInteractable(CriticalSection.GetActive());
            return;
        }

        private void Start()
        {
            if ((this.Input != null) == null)
            {
                goto Label_002D;
            }
            this.Input.get_onValueChanged().AddListener(new UnityAction<string>(this, this.OnInputFieldChange));
        Label_002D:
            this.UpdateInteractable(CriticalSection.GetActive());
            return;
        }

        private void UpdateInteractable(CriticalSections csMask)
        {
            bool flag;
            Selectable selectable;
            Selectable selectable2;
            CanvasGroup group;
            flag = 1;
            if ((csMask & this.Mask) == null)
            {
                goto Label_0011;
            }
            flag = 0;
        Label_0011:
            if ((this.Input != null) == null)
            {
                goto Label_0039;
            }
            if (string.IsNullOrEmpty(this.Input.get_text()) == null)
            {
                goto Label_0039;
            }
            flag = 0;
        Label_0039:
            if ((this.ToggleMask & 2) == null)
            {
                goto Label_0060;
            }
            selectable = base.GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0060;
            }
            selectable.set_enabled(flag);
        Label_0060:
            if ((this.ToggleMask & 1) == null)
            {
                goto Label_0087;
            }
            selectable2 = base.GetComponent<Selectable>();
            if ((selectable2 != null) == null)
            {
                goto Label_0087;
            }
            selectable2.set_interactable(flag);
        Label_0087:
            if ((this.ToggleMask & 4) == null)
            {
                goto Label_00AE;
            }
            group = base.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_00AE;
            }
            group.set_blocksRaycasts(flag);
        Label_00AE:
            return;
        }

        public static void UpdateValidators(CriticalSections updateMask, CriticalSections activeMask)
        {
            int num;
            num = Validators.Count - 1;
            goto Label_003E;
        Label_0012:
            if ((Validators[num].Mask & updateMask) == null)
            {
                goto Label_003A;
            }
            Validators[num].UpdateInteractable(activeMask);
        Label_003A:
            num -= 1;
        Label_003E:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return;
        }

        [Flags]
        public enum ToggleMasks
        {
            Interactable = 1,
            Enable = 2,
            BlockRaycast = 4
        }
    }
}

