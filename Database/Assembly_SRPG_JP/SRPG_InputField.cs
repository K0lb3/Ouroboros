// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_InputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/InputField (SRPG)")]
  public class SRPG_InputField : InputField
  {
    private static readonly char[] Separators = new char[6]
    {
      ' ',
      '.',
      ',',
      '\t',
      '\r',
      '\n'
    };
    private bool m_IsPointerOutofRange;
    private Event m_EventWork;
    private static bool NowInput;
    private CanvasRenderer m_Renderer;
    private RectTransform m_RectTrans;

    public SRPG_InputField()
    {
      base.\u002Ector();
    }

    public static bool IsFocus
    {
      get
      {
        return SRPG_InputField.NowInput;
      }
    }

    public static void ResetInput()
    {
      SRPG_InputField.NowInput = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerEnter(eventData);
      this.m_IsPointerOutofRange = false;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerExit(eventData);
      this.m_IsPointerOutofRange = true;
    }

    public virtual void OnUpdateSelected(BaseEventData eventData)
    {
      if (!this.get_isFocused())
        return;
      if (this.m_IsPointerOutofRange && this.GetMouseButtonDown())
      {
        Event @event = new Event();
        do
          ;
        while (Event.PopEvent(@event));
        this.UpdateLabel();
        ((AbstractEventData) eventData).Use();
      }
      else
      {
        bool flag = false;
        while (Event.PopEvent(this.m_EventWork))
        {
          if (this.m_EventWork.get_rawType() == 4)
          {
            flag = true;
            if (this.KeyPressedForWin(this.m_EventWork) == 1)
            {
              this.DeactivateInputField();
              break;
            }
          }
          else if (this.m_EventWork.get_rawType() == 5 && this.m_EventWork.get_keyCode() == 8)
            flag = true;
          EventType type = this.m_EventWork.get_type();
          if (type == 13 || type == 14)
          {
            string commandName = this.m_EventWork.get_commandName();
            if (commandName != null)
            {
              // ISSUE: reference to a compiler-generated field
              if (SRPG_InputField.\u003C\u003Ef__switch\u0024mapA == null)
              {
                // ISSUE: reference to a compiler-generated field
                SRPG_InputField.\u003C\u003Ef__switch\u0024mapA = new Dictionary<string, int>(1)
                {
                  {
                    "SelectAll",
                    0
                  }
                };
              }
              int num;
              // ISSUE: reference to a compiler-generated field
              if (SRPG_InputField.\u003C\u003Ef__switch\u0024mapA.TryGetValue(commandName, out num) && num == 0)
              {
                this.SelectAll();
                flag = true;
              }
            }
          }
        }
        if (flag)
          this.UpdateLabel();
        ((AbstractEventData) eventData).Use();
      }
    }

    private bool GetMouseButtonDown()
    {
      if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        return Input.GetMouseButtonDown(2);
      return true;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
      base.OnSelect(eventData);
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
      base.OnDeselect(eventData);
    }

    public virtual void ForceSetText(string text)
    {
      if (this.get_characterLimit() > 0 && text.Length > this.get_characterLimit())
        text = text.Substring(0, this.get_characterLimit());
      this.m_Text = (__Null) text;
      if (this.m_Keyboard != null)
        ((TouchScreenKeyboard) this.m_Keyboard).set_text((string) this.m_Text);
      if (this.m_CaretPosition > ((string) this.m_Text).Length)
        this.m_CaretPosition = (__Null) (int) (this.m_CaretSelectPosition = (__Null) ((string) this.m_Text).Length);
      if (this.get_onValueChanged() != null)
        ((UnityEvent<string>) this.get_onValueChanged()).Invoke(text);
      this.UpdateLabel();
    }

    protected virtual void OnDestroy()
    {
      if (this.m_Keyboard != null)
        ((TouchScreenKeyboard) this.m_Keyboard).set_active(false);
      ((UIBehaviour) this).OnDestroy();
    }

    private static string clipboard
    {
      get
      {
        return GUIUtility.get_systemCopyBuffer();
      }
      set
      {
        GUIUtility.set_systemCopyBuffer(value);
      }
    }

    private void DeleteForWin()
    {
      if (this.get_readOnly() || this.get_caretPositionInternal() == this.get_caretSelectPositionInternal())
        return;
      int num1 = this.get_caretPositionInternal() - Input.get_compositionString().Length;
      int num2 = this.get_caretSelectPositionInternal() - Input.get_compositionString().Length;
      if (num1 < num2)
      {
        this.m_Text = (__Null) (this.get_text().Substring(0, num1) + this.get_text().Substring(num2, this.get_text().Length - num2));
        this.set_caretSelectPositionInternal(this.get_caretPositionInternal());
      }
      else
      {
        this.m_Text = (__Null) (this.get_text().Substring(0, num2) + this.get_text().Substring(num1, this.get_text().Length - num1));
        this.set_caretPositionInternal(this.get_caretSelectPositionInternal());
      }
    }

    private void SendOnValueChangedAndUpdateLabelForWin()
    {
      if (this.get_onValueChanged() != null)
        ((UnityEvent<string>) this.get_onValueChanged()).Invoke(this.get_text());
      this.UpdateLabel();
    }

    private string GetSelectedStringForWin()
    {
      if (this.get_caretPositionInternal() == this.get_caretSelectPositionInternal())
        return string.Empty;
      int startIndex = this.get_caretPositionInternal();
      int num1 = this.get_caretSelectPositionInternal();
      if (startIndex > num1)
      {
        int num2 = startIndex;
        startIndex = num1;
        num1 = num2;
      }
      return this.get_text().Substring(startIndex, num1 - startIndex);
    }

    private int FindtPrevWordBegin()
    {
      if (this.get_caretSelectPositionInternal() - 2 < 0)
        return 0;
      int num = this.get_text().LastIndexOfAny(SRPG_InputField.Separators, this.get_caretSelectPositionInternal() - 2);
      return num != -1 ? num + 1 : 0;
    }

    private int FindtNextWordBeginForWin()
    {
      if (this.get_caretSelectPositionInternal() + 1 >= this.get_text().Length)
        return this.get_text().Length;
      int num = this.get_text().IndexOfAny(SRPG_InputField.Separators, this.get_caretSelectPositionInternal() + 1);
      return num != -1 ? num + 1 : this.get_text().Length;
    }

    private void MoveLeft(bool shift, bool ctrl)
    {
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal() && !shift)
      {
        int num = Mathf.Min(this.get_caretPositionInternal(), this.get_caretSelectPositionInternal());
        this.set_caretSelectPositionInternal(num);
        this.set_caretPositionInternal(num);
      }
      else
      {
        int num1 = !ctrl ? this.get_caretSelectPositionInternal() - 1 : this.FindtPrevWordBegin();
        if (shift)
        {
          this.set_caretSelectPositionInternal(num1);
        }
        else
        {
          int num2 = num1;
          this.set_caretPositionInternal(num2);
          this.set_caretSelectPositionInternal(num2);
        }
      }
    }

    private void MoveRight(bool shift, bool ctrl)
    {
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal() && !shift)
      {
        int num = Mathf.Max(this.get_caretPositionInternal(), this.get_caretSelectPositionInternal());
        this.set_caretSelectPositionInternal(num);
        this.set_caretPositionInternal(num);
      }
      else
      {
        int num1 = !ctrl ? this.get_caretSelectPositionInternal() + 1 : this.FindtNextWordBeginForWin();
        if (shift)
        {
          this.set_caretSelectPositionInternal(num1);
        }
        else
        {
          int num2 = num1;
          this.set_caretPositionInternal(num2);
          this.set_caretSelectPositionInternal(num2);
        }
      }
    }

    private static int GetLineEndPositionForWin(TextGenerator gen, int line)
    {
      line = Mathf.Max(line, 0);
      if (line + 1 < ((ICollection<UILineInfo>) gen.get_lines()).Count)
        return gen.get_lines()[line + 1].startCharIdx - 1;
      return gen.get_characterCountVisible();
    }

    private int DetermineCharacterLineForWin(int charPos, TextGenerator generator)
    {
      for (int index = 0; index < generator.get_lineCount() - 1; ++index)
      {
        if (generator.get_lines()[index + 1].startCharIdx > charPos)
          return index;
      }
      return generator.get_lineCount() - 1;
    }

    private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
    {
      if (originalPos > this.get_cachedInputTextGenerator().get_characterCountVisible())
        return 0;
      UICharInfo character = this.get_cachedInputTextGenerator().get_characters()[originalPos];
      int characterLineForWin = this.DetermineCharacterLineForWin(originalPos, this.get_cachedInputTextGenerator());
      if (characterLineForWin <= 0)
      {
        if (goToFirstChar)
          return 0;
        return originalPos;
      }
      int num = this.get_cachedInputTextGenerator().get_lines()[characterLineForWin].startCharIdx - 1;
      for (int startCharIdx = (int) this.get_cachedInputTextGenerator().get_lines()[characterLineForWin - 1].startCharIdx; startCharIdx < num; ++startCharIdx)
      {
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((^(Vector2&) @this.get_cachedInputTextGenerator().get_characters()[startCharIdx].cursorPos).x >= (^(Vector2&) @character.cursorPos).x)
          return startCharIdx;
      }
      return num;
    }

    private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
    {
      if (originalPos >= this.get_cachedInputTextGenerator().get_characterCountVisible())
        return this.get_text().Length;
      UICharInfo character = this.get_cachedInputTextGenerator().get_characters()[originalPos];
      int characterLineForWin = this.DetermineCharacterLineForWin(originalPos, this.get_cachedInputTextGenerator());
      if (characterLineForWin + 1 >= this.get_cachedInputTextGenerator().get_lineCount())
      {
        if (goToLastChar)
          return this.get_text().Length;
        return originalPos;
      }
      int endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(this.get_cachedInputTextGenerator(), characterLineForWin + 1);
      for (int startCharIdx = (int) this.get_cachedInputTextGenerator().get_lines()[characterLineForWin + 1].startCharIdx; startCharIdx < endPositionForWin; ++startCharIdx)
      {
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((^(Vector2&) @this.get_cachedInputTextGenerator().get_characters()[startCharIdx].cursorPos).x >= (^(Vector2&) @character.cursorPos).x)
          return startCharIdx;
      }
      return endPositionForWin;
    }

    private void MoveDown(bool shift, bool goToLastChar)
    {
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal() && !shift)
      {
        int num = Mathf.Max(this.get_caretPositionInternal(), this.get_caretSelectPositionInternal());
        this.set_caretSelectPositionInternal(num);
        this.set_caretPositionInternal(num);
      }
      int num1 = !this.get_multiLine() ? this.get_text().Length : this.LineDownCharacterPosition(this.get_caretSelectPositionInternal(), goToLastChar);
      if (shift)
      {
        this.set_caretSelectPositionInternal(num1);
      }
      else
      {
        int num2 = num1;
        this.set_caretSelectPositionInternal(num2);
        this.set_caretPositionInternal(num2);
      }
    }

    private void MoveUp(bool shift, bool goToFirstChar)
    {
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal() && !shift)
      {
        int num = Mathf.Min(this.get_caretPositionInternal(), this.get_caretSelectPositionInternal());
        this.set_caretSelectPositionInternal(num);
        this.set_caretPositionInternal(num);
      }
      int num1 = !this.get_multiLine() ? 0 : this.LineUpCharacterPosition(this.get_caretSelectPositionInternal(), goToFirstChar);
      if (shift)
      {
        this.set_caretSelectPositionInternal(num1);
      }
      else
      {
        int num2 = num1;
        this.set_caretPositionInternal(num2);
        this.set_caretSelectPositionInternal(num2);
      }
    }

    private void ForwardSpaceForWin()
    {
      if (this.get_readOnly())
        return;
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal())
      {
        this.DeleteForWin();
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
      else
      {
        if (this.get_caretPositionInternal() >= this.get_text().Length)
          return;
        this.m_Text = (__Null) this.get_text().Remove(this.get_caretPositionInternal(), 1);
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
    }

    private void BackspaceForWin()
    {
      if (this.get_readOnly())
        return;
      if (this.get_caretPositionInternal() != this.get_caretSelectPositionInternal())
      {
        this.DeleteForWin();
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
      else
      {
        if (this.get_caretPositionInternal() <= 0)
          return;
        this.m_Text = (__Null) this.get_text().Remove(this.get_caretPositionInternal() - 1, 1);
        int num = this.get_caretPositionInternal() - 1;
        this.set_caretPositionInternal(num);
        this.set_caretSelectPositionInternal(num);
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
    }

    private bool IsValidCharForWin(char c)
    {
      switch (c)
      {
        case '\t':
        case '\n':
          return true;
        case '\x007F':
          return false;
        default:
          return ((Text) this.m_TextComponent).get_font().HasCharacter(c);
      }
    }

    private bool InPlaceEditingForWin()
    {
      return !TouchScreenKeyboard.get_isSupported();
    }

    private void InsertForWin(char c)
    {
      if (this.get_readOnly())
        return;
      string str = c.ToString();
      this.DeleteForWin();
      if (this.get_characterLimit() > 0 && this.get_text().Length >= this.get_characterLimit())
        return;
      this.m_Text = (__Null) this.get_text().Insert((int) this.m_CaretPosition, str);
      SRPG_InputField srpgInputField = this;
      int num1;
      int num2 = num1 = srpgInputField.get_caretPositionInternal() + str.Length;
      srpgInputField.set_caretPositionInternal(num1);
      this.set_caretSelectPositionInternal(num2);
      if (this.get_onValueChanged() == null)
        return;
      ((UnityEvent<string>) this.get_onValueChanged()).Invoke(this.get_text());
    }

    protected virtual void Append(char input)
    {
      if (this.get_readOnly() || !this.InPlaceEditingForWin())
        return;
      if (this.get_onValidateInput() != null)
        input = this.get_onValidateInput().Invoke(this.get_text(), this.get_caretPositionInternal(), input);
      else if (this.get_characterValidation() != null)
        input = this.Validate(this.get_text(), this.get_caretPositionInternal(), input);
      if (input == char.MinValue)
        return;
      this.InsertForWin(input);
    }

    protected InputField.EditState KeyPressedForWin(Event evt)
    {
      EventModifiers modifiers = evt.get_modifiers();
      RuntimePlatform platform = Application.get_platform();
      bool ctrl = platform != null && platform != 1 && platform != 3 ? (modifiers & 2) != 0 : (modifiers & 8) != 0;
      bool shift = (modifiers & 1) != 0;
      bool flag1 = (modifiers & 4) != 0;
      bool flag2 = ctrl && !flag1 && !shift;
      KeyCode keyCode = evt.get_keyCode();
      switch (keyCode - 271)
      {
        case 0:
label_23:
          if (this.get_lineType() != 2)
            return (InputField.EditState) 1;
          break;
        case 2:
          this.MoveUp(shift, true);
          return (InputField.EditState) 0;
        case 3:
          this.MoveDown(shift, true);
          return (InputField.EditState) 0;
        case 4:
          this.MoveRight(shift, ctrl);
          return (InputField.EditState) 0;
        case 5:
          this.MoveLeft(shift, ctrl);
          return (InputField.EditState) 0;
        case 7:
          this.MoveTextStart(shift);
          return (InputField.EditState) 0;
        case 8:
          this.MoveTextEnd(shift);
          return (InputField.EditState) 0;
        default:
          switch (keyCode - 97)
          {
            case 0:
              if (flag2)
              {
                this.SelectAll();
                return (InputField.EditState) 0;
              }
              break;
            case 2:
              if (flag2)
              {
                SRPG_InputField.clipboard = this.get_inputType() == 2 ? string.Empty : this.GetSelectedStringForWin();
                return (InputField.EditState) 0;
              }
              break;
            default:
              switch (keyCode - 118)
              {
                case 0:
                  if (flag2)
                  {
                    this.Append(SRPG_InputField.clipboard);
                    return (InputField.EditState) 0;
                  }
                  break;
                case 2:
                  if (flag2)
                  {
                    SRPG_InputField.clipboard = this.get_inputType() == 2 ? string.Empty : this.GetSelectedStringForWin();
                    this.DeleteForWin();
                    this.SendOnValueChangedAndUpdateLabelForWin();
                    return (InputField.EditState) 0;
                  }
                  break;
                default:
                  if (keyCode != 8)
                  {
                    if (keyCode != 13)
                    {
                      if (keyCode == 27)
                        return this.KeyPressed(evt);
                      if (keyCode == (int) sbyte.MaxValue)
                      {
                        this.ForwardSpaceForWin();
                        return (InputField.EditState) 0;
                      }
                      break;
                    }
                    goto label_23;
                  }
                  else
                  {
                    this.BackspaceForWin();
                    return (InputField.EditState) 0;
                  }
              }
          }
      }
      char ch = evt.get_character();
      if (!this.get_multiLine() && (ch == '\t' || ch == '\r' || ch == '\n'))
        return (InputField.EditState) 0;
      if (ch == '\r' || ch == '\x0003')
        ch = '\n';
      if (this.IsValidCharForWin(ch))
        this.Append(ch);
      if (ch == char.MinValue && Input.get_compositionString().Length > 0)
        this.UpdateLabel();
      return (InputField.EditState) 0;
    }

    public virtual void Rebuild(CanvasUpdate update)
    {
      if (update != 4)
        return;
      this.UpdateGeometryForWin();
    }

    private void UpdateGeometryForWin()
    {
      if (!this.get_shouldHideMobileInput())
        return;
      if (Object.op_Equality((Object) this.m_Renderer, (Object) null) && Object.op_Inequality((Object) this.m_TextComponent, (Object) null))
      {
        GameObject gameObject = new GameObject(((Object) ((Component) this).get_transform()).get_name() + " Input Caret");
        ((Object) gameObject).set_hideFlags((HideFlags) 52);
        gameObject.get_transform().SetParent(((Component) this.m_TextComponent).get_transform().get_parent());
        gameObject.get_transform().SetAsFirstSibling();
        gameObject.set_layer(((Component) this).get_gameObject().get_layer());
        this.m_RectTrans = (RectTransform) gameObject.AddComponent<RectTransform>();
        this.m_Renderer = (CanvasRenderer) gameObject.AddComponent<CanvasRenderer>();
        this.m_Renderer.SetMaterial(Graphic.get_defaultGraphicMaterial(), (Texture) Texture2D.get_whiteTexture());
        ((LayoutElement) gameObject.AddComponent<LayoutElement>()).set_ignoreLayout(true);
        this.AssignPositioningIfNeeded();
      }
      if (Object.op_Equality((Object) this.m_Renderer, (Object) null))
        return;
      this.OnFillVBO(this.get_mesh());
      this.m_Renderer.SetMesh(this.get_mesh());
    }

    private void AssignPositioningIfNeeded()
    {
      if (!Object.op_Inequality((Object) this.m_TextComponent, (Object) null) || !Object.op_Inequality((Object) this.m_RectTrans, (Object) null) || !Vector3.op_Inequality(((Transform) this.m_RectTrans).get_localPosition(), ((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localPosition()) && !Quaternion.op_Inequality(((Transform) this.m_RectTrans).get_localRotation(), ((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localRotation()) && (!Vector3.op_Inequality(((Transform) this.m_RectTrans).get_localScale(), ((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localScale()) && !Vector2.op_Inequality(this.m_RectTrans.get_anchorMin(), ((Graphic) this.m_TextComponent).get_rectTransform().get_anchorMin())) && (!Vector2.op_Inequality(this.m_RectTrans.get_anchorMax(), ((Graphic) this.m_TextComponent).get_rectTransform().get_anchorMax()) && !Vector2.op_Inequality(this.m_RectTrans.get_anchoredPosition(), ((Graphic) this.m_TextComponent).get_rectTransform().get_anchoredPosition()) && (!Vector2.op_Inequality(this.m_RectTrans.get_sizeDelta(), ((Graphic) this.m_TextComponent).get_rectTransform().get_sizeDelta()) && !Vector2.op_Inequality(this.m_RectTrans.get_pivot(), ((Graphic) this.m_TextComponent).get_rectTransform().get_pivot()))))
        return;
      ((Transform) this.m_RectTrans).set_localPosition(((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localPosition());
      ((Transform) this.m_RectTrans).set_localRotation(((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localRotation());
      ((Transform) this.m_RectTrans).set_localScale(((Transform) ((Graphic) this.m_TextComponent).get_rectTransform()).get_localScale());
      this.m_RectTrans.set_anchorMin(((Graphic) this.m_TextComponent).get_rectTransform().get_anchorMin());
      this.m_RectTrans.set_anchorMax(((Graphic) this.m_TextComponent).get_rectTransform().get_anchorMax());
      this.m_RectTrans.set_anchoredPosition(((Graphic) this.m_TextComponent).get_rectTransform().get_anchoredPosition());
      this.m_RectTrans.set_sizeDelta(((Graphic) this.m_TextComponent).get_rectTransform().get_sizeDelta());
      this.m_RectTrans.set_pivot(((Graphic) this.m_TextComponent).get_rectTransform().get_pivot());
    }

    private void OnFillVBO(Mesh vbo)
    {
      using (VertexHelper vbo1 = new VertexHelper())
      {
        if (!this.get_isFocused())
        {
          vbo1.FillMesh(vbo);
        }
        else
        {
          Rect rect = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
          // ISSUE: explicit reference operation
          Vector2 size = ((Rect) @rect).get_size();
          Vector2 textAnchorPivot = Text.GetTextAnchorPivot(((Text) this.m_TextComponent).get_alignment());
          Vector2 zero = Vector2.get_zero();
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          zero.x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) textAnchorPivot.x);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          zero.y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) textAnchorPivot.y);
          Vector2 roundingOffset = Vector2.op_Addition(Vector2.op_Subtraction(((Graphic) this.m_TextComponent).PixelAdjustPoint(zero), zero), Vector2.Scale(size, textAnchorPivot));
          roundingOffset.x = (__Null) (roundingOffset.x - (double) Mathf.Floor((float) (0.5 + roundingOffset.x)));
          roundingOffset.y = (__Null) (roundingOffset.y - (double) Mathf.Floor((float) (0.5 + roundingOffset.y)));
          if (this.get_caretPositionInternal() == this.get_caretSelectPositionInternal())
            this.GenerateCaret(vbo1, roundingOffset);
          else
            this.GenerateHightlight(vbo1, roundingOffset);
          vbo1.FillMesh(vbo);
        }
      }
    }

    private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
    {
      if (this.m_CaretVisible == null)
        return;
      if (this.m_CursorVerts == null)
        this.CreateCursorVerts();
      float caretWidth = (float) this.get_caretWidth();
      int charPos = Mathf.Max(0, this.get_caretPositionInternal() - this.m_DrawStart);
      TextGenerator cachedTextGenerator = ((Text) this.m_TextComponent).get_cachedTextGenerator();
      if (cachedTextGenerator == null || cachedTextGenerator.get_lineCount() == 0)
        return;
      Vector2 zero = Vector2.get_zero();
      if (charPos < ((ICollection<UICharInfo>) cachedTextGenerator.get_characters()).Count)
      {
        UICharInfo character = cachedTextGenerator.get_characters()[charPos];
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        zero.x = (^(Vector2&) @character.cursorPos).x;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @zero;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).x = (__Null) ((^local1).x / (double) ((Text) this.m_TextComponent).get_pixelsPerUnit());
      // ISSUE: variable of the null type
      __Null x = zero.x;
      Rect rect1 = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
      // ISSUE: explicit reference operation
      double xMax1 = (double) ((Rect) @rect1).get_xMax();
      if (x > xMax1)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @zero;
        Rect rect2 = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
        // ISSUE: explicit reference operation
        double xMax2 = (double) ((Rect) @rect2).get_xMax();
        // ISSUE: explicit reference operation
        (^local2).x = (__Null) xMax2;
      }
      int characterLineForWin = this.DetermineCharacterLineForWin(charPos, cachedTextGenerator);
      zero.y = (__Null) (cachedTextGenerator.get_lines()[characterLineForWin].topY / (double) ((Text) this.m_TextComponent).get_pixelsPerUnit());
      float num = (float) cachedTextGenerator.get_lines()[characterLineForWin].height / ((Text) this.m_TextComponent).get_pixelsPerUnit();
      for (int index = 0; index < this.m_CursorVerts.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(UIVertex&) @this.m_CursorVerts[index]).color = (__Null) Color32.op_Implicit(this.get_caretColor());
      }
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(UIVertex&) @this.m_CursorVerts[0]).position = (__Null) new Vector3((float) zero.x, (float) zero.y - num, 0.0f);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(UIVertex&) @this.m_CursorVerts[1]).position = (__Null) new Vector3((float) zero.x + caretWidth, (float) zero.y - num, 0.0f);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(UIVertex&) @this.m_CursorVerts[2]).position = (__Null) new Vector3((float) zero.x + caretWidth, (float) zero.y, 0.0f);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(UIVertex&) @this.m_CursorVerts[3]).position = (__Null) new Vector3((float) zero.x, (float) zero.y, 0.0f);
      if (Vector2.op_Inequality(roundingOffset, Vector2.get_zero()))
      {
        for (int index = 0; index < this.m_CursorVerts.Length; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          UIVertex uiVertex = ^(UIVertex&) @this.m_CursorVerts[index];
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local2 = @uiVertex.position;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Vector3&) local2).x = (^(Vector3&) local2).x + roundingOffset.x;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local3 = @uiVertex.position;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Vector3&) local3).y = (^(Vector3&) local3).y + roundingOffset.y;
        }
      }
      vbo.AddUIVertexQuad((UIVertex[]) this.m_CursorVerts);
      int height = Screen.get_height();
      zero.y = (__Null) ((double) height - zero.y);
      Input.set_compositionCursorPos(zero);
    }

    private void CreateCursorVerts()
    {
      this.m_CursorVerts = (__Null) new UIVertex[4];
      for (int index = 0; index < this.m_CursorVerts.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(UIVertex&) @this.m_CursorVerts[index] = (UIVertex) UIVertex.simpleVert;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(UIVertex&) @this.m_CursorVerts[index]).uv0 = (__Null) Vector2.get_zero();
      }
    }

    private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
    {
      int num1 = this.get_caretPositionInternal();
      int num2 = this.get_caretSelectPositionInternal();
      if (((string) this.m_Text).Length == Mathf.Abs((int) (this.m_CaretPosition - this.m_CaretSelectPosition)))
      {
        int length = Input.get_compositionString().Length;
        num1 -= length;
        num2 -= length;
      }
      else if (this.m_CaretPosition > this.m_CaretSelectPosition)
      {
        num1 = (int) this.m_CaretSelectPosition;
        num2 = (int) this.m_CaretPosition;
      }
      int charPos = Mathf.Max(0, num1 - this.m_DrawStart);
      int num3 = Mathf.Max(0, num2 - this.m_DrawStart);
      if (charPos > num3)
      {
        int num4 = charPos;
        charPos = num3;
        num3 = num4;
      }
      int num5 = num3 - 1;
      TextGenerator cachedTextGenerator = ((Text) this.m_TextComponent).get_cachedTextGenerator();
      if (cachedTextGenerator.get_lineCount() <= 0)
        return;
      int characterLineForWin = this.DetermineCharacterLineForWin(charPos, cachedTextGenerator);
      int endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
      UIVertex simpleVert = (UIVertex) UIVertex.simpleVert;
      simpleVert.uv0 = (__Null) Vector2.get_zero();
      simpleVert.color = (__Null) Color32.op_Implicit(this.get_selectionColor());
      for (int index = charPos; index <= num5 && index < cachedTextGenerator.get_characterCount(); ++index)
      {
        if (index == endPositionForWin || index == num5)
        {
          UICharInfo character1 = cachedTextGenerator.get_characters()[charPos];
          UICharInfo character2 = cachedTextGenerator.get_characters()[index];
          Vector2 vector2_1;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_1).\u002Ector((float) (^(Vector2&) @character1.cursorPos).x / ((Text) this.m_TextComponent).get_pixelsPerUnit(), (float) cachedTextGenerator.get_lines()[characterLineForWin].topY / ((Text) this.m_TextComponent).get_pixelsPerUnit());
          Vector2 vector2_2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).\u002Ector((float) ((^(Vector2&) @character2.cursorPos).x + character2.charWidth) / ((Text) this.m_TextComponent).get_pixelsPerUnit(), (float) (vector2_1.y - (double) (float) cachedTextGenerator.get_lines()[characterLineForWin].height / (double) ((Text) this.m_TextComponent).get_pixelsPerUnit()));
          // ISSUE: variable of the null type
          __Null x1 = vector2_2.x;
          Rect rect1 = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
          // ISSUE: explicit reference operation
          double xMax1 = (double) ((Rect) @rect1).get_xMax();
          if (x1 <= xMax1)
          {
            // ISSUE: variable of the null type
            __Null x2 = vector2_2.x;
            Rect rect2 = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
            // ISSUE: explicit reference operation
            double xMin = (double) ((Rect) @rect2).get_xMin();
            if (x2 >= xMin)
              goto label_13;
          }
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @vector2_2;
          Rect rect3 = ((Graphic) this.m_TextComponent).get_rectTransform().get_rect();
          // ISSUE: explicit reference operation
          double xMax2 = (double) ((Rect) @rect3).get_xMax();
          // ISSUE: explicit reference operation
          (^local).x = (__Null) xMax2;
label_13:
          int currentVertCount = vbo.get_currentVertCount();
          simpleVert.position = (__Null) Vector3.op_Addition(new Vector3((float) vector2_1.x, (float) vector2_2.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = (__Null) Vector3.op_Addition(new Vector3((float) vector2_2.x, (float) vector2_2.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = (__Null) Vector3.op_Addition(new Vector3((float) vector2_2.x, (float) vector2_1.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = (__Null) Vector3.op_Addition(new Vector3((float) vector2_1.x, (float) vector2_1.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
          vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
          charPos = index + 1;
          ++characterLineForWin;
          endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
        }
      }
    }
  }
}
