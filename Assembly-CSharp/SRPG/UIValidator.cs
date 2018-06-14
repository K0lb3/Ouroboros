// Decompiled with JetBrains decompiler
// Type: SRPG.UIValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [DisallowMultipleComponent]
  [RequireComponent(typeof (Selectable))]
  public class UIValidator : MonoBehaviour
  {
    public static List<UIValidator> Validators = new List<UIValidator>();
    [BitMask]
    public CriticalSections Mask;
    public InputField Input;
    public InputField[] ExtraInput;
    [BitMask]
    public UIValidator.ToggleMasks ToggleMask;

    public UIValidator()
    {
      base.\u002Ector();
    }

    public static void UpdateValidators(CriticalSections updateMask, CriticalSections activeMask)
    {
      for (int index = UIValidator.Validators.Count - 1; index >= 0; --index)
      {
        if ((UIValidator.Validators[index].Mask & updateMask) != (CriticalSections) 0)
          UIValidator.Validators[index].UpdateInteractable(activeMask);
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Input, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<string>) this.Input.get_onValueChanged()).AddListener(new UnityAction<string>((object) this, __methodptr(OnInputFieldChange)));
      }
      if (this.ExtraInput != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UIValidator.\u003CStart\u003Ec__AnonStorey2B9 startCAnonStorey2B9 = new UIValidator.\u003CStart\u003Ec__AnonStorey2B9();
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey2B9.\u003C\u003Ef__this = this;
        foreach (InputField inputField in this.ExtraInput)
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey2B9.i = inputField;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent<string>) startCAnonStorey2B9.i.get_onValueChanged()).AddListener(new UnityAction<string>((object) startCAnonStorey2B9, __methodptr(\u003C\u003Em__287)));
        }
      }
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    private void UpdateInteractable(CriticalSections csMask)
    {
      bool flag = true;
      if ((csMask & this.Mask) != (CriticalSections) 0)
        flag = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Input, (UnityEngine.Object) null) && string.IsNullOrEmpty(this.Input.get_text()))
        flag = false;
      if (this.ExtraInput != null)
      {
        foreach (InputField inputField in this.ExtraInput)
        {
          if (string.IsNullOrEmpty(inputField.get_text()))
          {
            flag = false;
            break;
          }
        }
      }
      if ((this.ToggleMask & UIValidator.ToggleMasks.Enable) != (UIValidator.ToggleMasks) 0)
      {
        Selectable component = (Selectable) ((Component) this).GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).set_enabled(flag);
      }
      if ((this.ToggleMask & UIValidator.ToggleMasks.Interactable) != (UIValidator.ToggleMasks) 0)
      {
        Selectable component = (Selectable) ((Component) this).GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.set_interactable(flag);
      }
      if ((this.ToggleMask & UIValidator.ToggleMasks.BlockRaycast) == (UIValidator.ToggleMasks) 0)
        return;
      CanvasGroup component1 = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      component1.set_blocksRaycasts(flag);
    }

    private void OnEnable()
    {
      UIValidator.Validators.Add(this);
    }

    private void OnDisable()
    {
      UIValidator.Validators.Remove(this);
    }

    private bool IsEmoji(char a)
    {
      if (char.IsSurrogate(a))
        return true;
      string pattern = "[\xE63E-\xE6A5]|[\xE6AC-\xE6AE]|[\xE6B1-\xE6BA]|[\xE6CE-\xE757]|î[\x0098-\x009D][\x0080-¿]|(?:î[±-\x00B3µ¶\x00BD-¿]|ï[\x0081-\x0083])[\x0080-¿]|î[\x0080\x0081\x0084\x0085\x0088\x0089\x008C\x008D\x0090\x0091\x0094][\x0080-¿]|(?:(?:#|[0-9])⃣)";
      return Regex.IsMatch(a.ToString(), pattern);
    }

    private bool IsEmojiWork(char a)
    {
      if ("©®   ‼⁉™ℹ↩↪⌚⌛⏩⏪⏫⏬⏰⏳Ⓜ▪▫▶◀◻◼◽◾☀☁☎☑☔☕☝☺♈♉♊♋♌♍♎♏♐♑♒♓♠♣♥♦♨♻♿⚓⚠⚡⚪⚫⚽⚾⛄⛅⛎⛔⛪⛲⛳⛵⛺⛽✂✅✈✉✊✋✌✏✒✔✖✨✳✴❄❇❌❎❓❔❕❗❤➕➖➗➡➰⤴⤵⬅⬆⬇⬛⬜⭐⭕〰〽㊗㊙".IndexOf(a) >= 0)
        return true;
      int[] numArray1 = new int[94]{ 126980, 127183, 127344, 127345, 127358, 127359, 127374, 127489, 127490, 127514, 127535, 127568, 127569, 127759, 127761, 127763, 127764, 127765, 127769, 127771, 127775, 127776, 127792, 127793, 127796, 127797, 127942, 127944, 127946, 128012, 128013, 128014, 128017, 128018, 128020, 128023, 128024, 128025, 128064, 128238, 128259, 128527, 128530, 128531, 128532, 128534, 128536, 128538, 128540, 128541, 128542, 128557, 128565, 128643, 128644, 128645, 128647, 128649, 128652, 128655, 128657, 128658, 128659, 128661, 128663, 128665, 128666, 128674, 128676, 128677, 128690, 128694, 128697, 128704, 127464, 127475, 127465, 127466, 127466, 127480, 127467, 127479, 127468, 127463, 127470, 127481, 127471, 127477, 127472, 127479, 127479, 127482, 127482, 127480 };
      foreach (int num in numArray1)
      {
        if (num == (int) a)
          return true;
      }
      int[] numArray2 = new int[66]{ 8596, 8601, 127377, 127386, 127538, 127546, 127744, 127756, 127799, 127823, 127825, 127867, 127872, 127891, 127904, 127940, 127968, 127971, 127973, 127984, 128026, 128041, 128043, 128062, 128066, 128100, 128102, 128107, 128110, 128172, 128174, 128181, 128184, 128235, 128240, 128247, 128249, 128252, 128266, 128276, 128278, 128299, 128302, 128317, 128336, 128347, 128507, 128511, 128513, 128518, 128521, 128525, 128544, 128549, 128552, 128555, 128560, 128563, 128567, 128576, 128581, 128640, 128679, 128685, 128698, 128702 };
      int index = 0;
      while (index < numArray2.Length)
      {
        if (numArray2[index] <= (int) a && (int) a <= numArray2[index + 1])
          return true;
        index += 2;
      }
      return false;
    }

    private void OnInputFieldChange(string value)
    {
      for (int startIndex = 0; startIndex < value.Length; ++startIndex)
      {
        if (this.IsEmoji(value[startIndex]))
        {
          this.Input.set_text(value.Remove(startIndex));
          break;
        }
      }
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    private void OnInputFieldChange(string value, InputField input)
    {
      for (int startIndex = 0; startIndex < value.Length; ++startIndex)
      {
        if (this.IsEmoji(value[startIndex]))
        {
          input.set_text(value.Remove(startIndex));
          break;
        }
      }
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    [Flags]
    public enum ToggleMasks
    {
      Interactable = 1,
      Enable = 2,
      BlockRaycast = 4,
    }
  }
}
