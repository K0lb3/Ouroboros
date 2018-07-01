// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLimitButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("CharacterLimitButton")]
  public class CharacterLimitButton : MonoBehaviour
  {
    [SerializeField]
    public CharacterLimitButton.InputfieldSet[] target_list;
    [SerializeField]
    public Button target_button;

    public CharacterLimitButton()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.target_button, (UnityEngine.Object) null) || this.target_list == null)
        return;
      foreach (CharacterLimitButton.InputfieldSet target in this.target_list)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) target.input, (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent<string>) target.input.get_onValueChanged()).AddListener(new UnityAction<string>((object) this, __methodptr(OnInputFieldChange)));
          this.OnInputFieldChange(string.Empty);
        }
      }
    }

    public void OnInputFieldChange(string value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.target_button, (UnityEngine.Object) null))
        return;
      bool flag = true;
      foreach (CharacterLimitButton.InputfieldSet target in this.target_list)
      {
        int length = target.input.get_text().Length;
        if (target.min_length > length || length > target.max_length)
        {
          flag = false;
          break;
        }
      }
      ((Selectable) this.target_button).set_interactable(flag);
    }

    [Serializable]
    public class InputfieldSet
    {
      public InputField input;
      public int min_length;
      public int max_length;
    }
  }
}
