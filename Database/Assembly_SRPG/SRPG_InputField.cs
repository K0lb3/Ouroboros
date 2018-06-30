namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [AddComponentMenu("UI/InputField (SRPG)")]
    public class SRPG_InputField : InputField
    {
        private bool m_IsPointerOutofRange;
        private Event m_EventWork;
        private static bool NowInput;
        private static readonly char[] Separators;
        private CanvasRenderer m_Renderer;
        private RectTransform m_RectTrans;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$mapA;

        static SRPG_InputField()
        {
            Separators = new char[] { 0x20, 0x2e, 0x2c, 9, 13, 10 };
            return;
        }

        public SRPG_InputField()
        {
            this.m_EventWork = new Event();
            base..ctor();
            return;
        }

        protected override void Append(char input)
        {
            if (base.get_readOnly() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.InPlaceEditingForWin() != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (base.get_onValidateInput() == null)
            {
                goto Label_0042;
            }
            input = base.get_onValidateInput().Invoke(base.get_text(), base.get_caretPositionInternal(), input);
            goto Label_0062;
        Label_0042:
            if (base.get_characterValidation() == null)
            {
                goto Label_0062;
            }
            input = base.Validate(base.get_text(), base.get_caretPositionInternal(), input);
        Label_0062:
            if (input != null)
            {
                goto Label_0069;
            }
            return;
        Label_0069:
            this.InsertForWin(input);
            return;
        }

        private void AssignPositioningIfNeeded()
        {
            if ((base.m_TextComponent != null) == null)
            {
                goto Label_0222;
            }
            if ((this.m_RectTrans != null) == null)
            {
                goto Label_0222;
            }
            if ((this.m_RectTrans.get_localPosition() != base.m_TextComponent.get_rectTransform().get_localPosition()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_localRotation() != base.m_TextComponent.get_rectTransform().get_localRotation()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_localScale() != base.m_TextComponent.get_rectTransform().get_localScale()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_anchorMin() != base.m_TextComponent.get_rectTransform().get_anchorMin()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_anchorMax() != base.m_TextComponent.get_rectTransform().get_anchorMax()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_anchoredPosition() != base.m_TextComponent.get_rectTransform().get_anchoredPosition()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_sizeDelta() != base.m_TextComponent.get_rectTransform().get_sizeDelta()) != null)
            {
                goto Label_014A;
            }
            if ((this.m_RectTrans.get_pivot() != base.m_TextComponent.get_rectTransform().get_pivot()) == null)
            {
                goto Label_0222;
            }
        Label_014A:
            this.m_RectTrans.set_localPosition(base.m_TextComponent.get_rectTransform().get_localPosition());
            this.m_RectTrans.set_localRotation(base.m_TextComponent.get_rectTransform().get_localRotation());
            this.m_RectTrans.set_localScale(base.m_TextComponent.get_rectTransform().get_localScale());
            this.m_RectTrans.set_anchorMin(base.m_TextComponent.get_rectTransform().get_anchorMin());
            this.m_RectTrans.set_anchorMax(base.m_TextComponent.get_rectTransform().get_anchorMax());
            this.m_RectTrans.set_anchoredPosition(base.m_TextComponent.get_rectTransform().get_anchoredPosition());
            this.m_RectTrans.set_sizeDelta(base.m_TextComponent.get_rectTransform().get_sizeDelta());
            this.m_RectTrans.set_pivot(base.m_TextComponent.get_rectTransform().get_pivot());
        Label_0222:
            return;
        }

        private void BackspaceForWin()
        {
            int num;
            if (base.get_readOnly() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (base.get_caretPositionInternal() == base.get_caretSelectPositionInternal())
            {
                goto Label_002E;
            }
            this.DeleteForWin();
            this.SendOnValueChangedAndUpdateLabelForWin();
            goto Label_0071;
        Label_002E:
            if (base.get_caretPositionInternal() <= 0)
            {
                goto Label_0071;
            }
            base.m_Text = base.get_text().Remove(base.get_caretPositionInternal() - 1, 1);
            num = base.get_caretPositionInternal() - 1;
            base.set_caretPositionInternal(num);
            base.set_caretSelectPositionInternal(num);
            this.SendOnValueChangedAndUpdateLabelForWin();
        Label_0071:
            return;
        }

        private unsafe void CreateCursorVerts()
        {
            int num;
            base.m_CursorVerts = new UIVertex[4];
            num = 0;
            goto Label_0043;
        Label_0013:
            *(&(base.m_CursorVerts[num])) = UIVertex.simpleVert;
            &(base.m_CursorVerts[num]).uv0 = Vector2.get_zero();
            num += 1;
        Label_0043:
            if (num < ((int) base.m_CursorVerts.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void DeleteForWin()
        {
            int num;
            int num2;
            if (base.get_readOnly() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (base.get_caretPositionInternal() != base.get_caretSelectPositionInternal())
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            num = base.get_caretPositionInternal() - Input.get_compositionString().Length;
            num2 = base.get_caretSelectPositionInternal() - Input.get_compositionString().Length;
            if (num >= num2)
            {
                goto Label_008B;
            }
            base.m_Text = base.get_text().Substring(0, num) + base.get_text().Substring(num2, base.get_text().Length - num2);
            base.set_caretSelectPositionInternal(base.get_caretPositionInternal());
            goto Label_00C8;
        Label_008B:
            base.m_Text = base.get_text().Substring(0, num2) + base.get_text().Substring(num, base.get_text().Length - num);
            base.set_caretPositionInternal(base.get_caretSelectPositionInternal());
        Label_00C8:
            return;
        }

        private unsafe int DetermineCharacterLineForWin(int charPos, TextGenerator generator)
        {
            int num;
            UILineInfo info;
            num = 0;
            goto Label_0029;
        Label_0007:
            info = generator.get_lines()[num + 1];
            if (&info.startCharIdx <= charPos)
            {
                goto Label_0025;
            }
            return num;
        Label_0025:
            num += 1;
        Label_0029:
            if (num < (generator.get_lineCount() - 1))
            {
                goto Label_0007;
            }
            return (generator.get_lineCount() - 1);
        }

        private int FindtNextWordBeginForWin()
        {
            int num;
            if ((base.get_caretSelectPositionInternal() + 1) < base.get_text().Length)
            {
                goto Label_0024;
            }
            return base.get_text().Length;
        Label_0024:
            num = base.get_text().IndexOfAny(Separators, base.get_caretSelectPositionInternal() + 1);
            if (num != -1)
            {
                goto Label_0055;
            }
            num = base.get_text().Length;
            goto Label_0059;
        Label_0055:
            num += 1;
        Label_0059:
            return num;
        }

        private int FindtPrevWordBegin()
        {
            int num;
            if ((base.get_caretSelectPositionInternal() - 2) >= 0)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            num = base.get_text().LastIndexOfAny(Separators, base.get_caretSelectPositionInternal() - 2);
            if (num != -1)
            {
                goto Label_0037;
            }
            num = 0;
            goto Label_003B;
        Label_0037:
            num += 1;
        Label_003B:
            return num;
        }

        public virtual void ForceSetText(string text)
        {
            int num;
            if (base.get_characterLimit() <= 0)
            {
                goto Label_002C;
            }
            if (text.Length <= base.get_characterLimit())
            {
                goto Label_002C;
            }
            text = text.Substring(0, base.get_characterLimit());
        Label_002C:
            base.m_Text = text;
            if (base.m_Keyboard == null)
            {
                goto Label_004F;
            }
            base.m_Keyboard.set_text(base.m_Text);
        Label_004F:
            if (base.m_CaretPosition <= base.m_Text.Length)
            {
                goto Label_007F;
            }
            base.m_CaretPosition = base.m_CaretSelectPosition = base.m_Text.Length;
        Label_007F:
            if (base.get_onValueChanged() == null)
            {
                goto Label_0096;
            }
            base.get_onValueChanged().Invoke(text);
        Label_0096:
            base.UpdateLabel();
            return;
        }

        private void ForwardSpaceForWin()
        {
            if (base.get_readOnly() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (base.get_caretPositionInternal() == base.get_caretSelectPositionInternal())
            {
                goto Label_002E;
            }
            this.DeleteForWin();
            this.SendOnValueChangedAndUpdateLabelForWin();
            goto Label_0062;
        Label_002E:
            if (base.get_caretPositionInternal() >= base.get_text().Length)
            {
                goto Label_0062;
            }
            base.m_Text = base.get_text().Remove(base.get_caretPositionInternal(), 1);
            this.SendOnValueChangedAndUpdateLabelForWin();
        Label_0062:
            return;
        }

        private unsafe void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
        {
            float num;
            int num2;
            TextGenerator generator;
            Vector2 vector;
            UICharInfo info;
            int num3;
            float num4;
            int num5;
            int num6;
            UIVertex vertex;
            int num7;
            Rect rect;
            Rect rect2;
            UILineInfo info2;
            UILineInfo info3;
            if (base.m_CaretVisible != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (base.m_CursorVerts != null)
            {
                goto Label_001D;
            }
            this.CreateCursorVerts();
        Label_001D:
            num = (float) base.get_caretWidth();
            num2 = Mathf.Max(0, base.get_caretPositionInternal() - base.m_DrawStart);
            generator = base.m_TextComponent.get_cachedTextGenerator();
            if (generator != null)
            {
                goto Label_004C;
            }
            return;
        Label_004C:
            if (generator.get_lineCount() != null)
            {
                goto Label_0058;
            }
            return;
        Label_0058:
            vector = Vector2.get_zero();
            if (num2 >= generator.get_characters().Count)
            {
                goto Label_0090;
            }
            info = generator.get_characters()[num2];
            &vector.x = &&info.cursorPos.x;
        Label_0090:
            &vector.x /= base.m_TextComponent.get_pixelsPerUnit();
            if (&vector.x <= &base.m_TextComponent.get_rectTransform().get_rect().get_xMax())
            {
                goto Label_00EE;
            }
            &vector.x = &base.m_TextComponent.get_rectTransform().get_rect().get_xMax();
        Label_00EE:
            num3 = this.DetermineCharacterLineForWin(num2, generator);
            info2 = generator.get_lines()[num3];
            &vector.y = &info2.topY / base.m_TextComponent.get_pixelsPerUnit();
            info3 = generator.get_lines()[num3];
            num4 = ((float) &info3.height) / base.m_TextComponent.get_pixelsPerUnit();
            num5 = 0;
            goto Label_0171;
        Label_014E:
            &(base.m_CursorVerts[num5]).color = base.get_caretColor();
            num5 += 1;
        Label_0171:
            if (num5 < ((int) base.m_CursorVerts.Length))
            {
                goto Label_014E;
            }
            &(base.m_CursorVerts[0]).position = new Vector3(&vector.x, &vector.y - num4, 0f);
            &(base.m_CursorVerts[1]).position = new Vector3(&vector.x + num, &vector.y - num4, 0f);
            &(base.m_CursorVerts[2]).position = new Vector3(&vector.x + num, &vector.y, 0f);
            &(base.m_CursorVerts[3]).position = new Vector3(&vector.x, &vector.y, 0f);
            if ((roundingOffset != Vector2.get_zero()) == null)
            {
                goto Label_02A3;
            }
            num6 = 0;
            goto Label_0294;
        Label_0246:
            vertex = *(&(base.m_CursorVerts[num6]));
            &&vertex.position.x += &roundingOffset.x;
            &&vertex.position.y += &roundingOffset.y;
            num6 += 1;
        Label_0294:
            if (num6 < ((int) base.m_CursorVerts.Length))
            {
                goto Label_0246;
            }
        Label_02A3:
            vbo.AddUIVertexQuad(base.m_CursorVerts);
            num7 = Screen.get_height();
            &vector.y = ((float) num7) - &vector.y;
            Input.set_compositionCursorPos(vector);
            return;
        }

        private unsafe void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            TextGenerator generator;
            int num7;
            int num8;
            UIVertex vertex;
            int num9;
            UICharInfo info;
            UICharInfo info2;
            Vector2 vector;
            Vector2 vector2;
            int num10;
            UILineInfo info3;
            UILineInfo info4;
            Rect rect;
            Rect rect2;
            Rect rect3;
            num = base.get_caretPositionInternal();
            num2 = base.get_caretSelectPositionInternal();
            if (base.m_Text.Length != Mathf.Abs(base.m_CaretPosition - base.m_CaretSelectPosition))
            {
                goto Label_0048;
            }
            num3 = Input.get_compositionString().Length;
            num -= num3;
            num2 -= num3;
            goto Label_0067;
        Label_0048:
            if (base.m_CaretPosition <= base.m_CaretSelectPosition)
            {
                goto Label_0067;
            }
            num = base.m_CaretSelectPosition;
            num2 = base.m_CaretPosition;
        Label_0067:
            num4 = Mathf.Max(0, num - base.m_DrawStart);
            num5 = Mathf.Max(0, num2 - base.m_DrawStart);
            if (num4 <= num5)
            {
                goto Label_0098;
            }
            num6 = num4;
            num4 = num5;
            num5 = num6;
        Label_0098:
            num5 -= 1;
            generator = base.m_TextComponent.get_cachedTextGenerator();
            if (generator.get_lineCount() > 0)
            {
                goto Label_00B9;
            }
            return;
        Label_00B9:
            num7 = this.DetermineCharacterLineForWin(num4, generator);
            num8 = GetLineEndPositionForWin(generator, num7);
            vertex = UIVertex.simpleVert;
            &vertex.uv0 = Vector2.get_zero();
            &vertex.color = base.get_selectionColor();
            num9 = num4;
            goto Label_0338;
        Label_00FC:
            if (num9 == num8)
            {
                goto Label_010E;
            }
            if (num9 != num5)
            {
                goto Label_0332;
            }
        Label_010E:
            info = generator.get_characters()[num4];
            info2 = generator.get_characters()[num9];
            info3 = generator.get_lines()[num7];
            &vector..ctor(&&info.cursorPos.x / base.m_TextComponent.get_pixelsPerUnit(), &info3.topY / base.m_TextComponent.get_pixelsPerUnit());
            info4 = generator.get_lines()[num7];
            &vector2..ctor((&&info2.cursorPos.x + &info2.charWidth) / base.m_TextComponent.get_pixelsPerUnit(), &vector.y - (((float) &info4.height) / base.m_TextComponent.get_pixelsPerUnit()));
            if (&vector2.x > &base.m_TextComponent.get_rectTransform().get_rect().get_xMax())
            {
                goto Label_020C;
            }
            if (&vector2.x >= &base.m_TextComponent.get_rectTransform().get_rect().get_xMin())
            {
                goto Label_022C;
            }
        Label_020C:
            &vector2.x = &base.m_TextComponent.get_rectTransform().get_rect().get_xMax();
        Label_022C:
            num10 = vbo.get_currentVertCount();
            &vertex.position = new Vector3(&vector.x, &vector2.y, 0f) + roundingOffset;
            vbo.AddVert(vertex);
            &vertex.position = new Vector3(&vector2.x, &vector2.y, 0f) + roundingOffset;
            vbo.AddVert(vertex);
            &vertex.position = new Vector3(&vector2.x, &vector.y, 0f) + roundingOffset;
            vbo.AddVert(vertex);
            &vertex.position = new Vector3(&vector.x, &vector.y, 0f) + roundingOffset;
            vbo.AddVert(vertex);
            vbo.AddTriangle(num10, num10 + 1, num10 + 2);
            vbo.AddTriangle(num10 + 2, num10 + 3, num10);
            num4 = num9 + 1;
            num7 += 1;
            num8 = GetLineEndPositionForWin(generator, num7);
        Label_0332:
            num9 += 1;
        Label_0338:
            if (num9 > num5)
            {
                goto Label_034F;
            }
            if (num9 < generator.get_characterCount())
            {
                goto Label_00FC;
            }
        Label_034F:
            return;
        }

        private static unsafe int GetLineEndPositionForWin(TextGenerator gen, int line)
        {
            UILineInfo info;
            line = Mathf.Max(line, 0);
            if ((line + 1) >= gen.get_lines().Count)
            {
                goto Label_0035;
            }
            info = gen.get_lines()[line + 1];
            return (&info.startCharIdx - 1);
        Label_0035:
            return gen.get_characterCountVisible();
        }

        private bool GetMouseButtonDown()
        {
            return (((Input.GetMouseButtonDown(0) != null) || (Input.GetMouseButtonDown(1) != null)) ? 1 : Input.GetMouseButtonDown(2));
        }

        private string GetSelectedStringForWin()
        {
            int num;
            int num2;
            int num3;
            if (base.get_caretPositionInternal() != base.get_caretSelectPositionInternal())
            {
                goto Label_0017;
            }
            return string.Empty;
        Label_0017:
            num = base.get_caretPositionInternal();
            num2 = base.get_caretSelectPositionInternal();
            if (num <= num2)
            {
                goto Label_0032;
            }
            num3 = num;
            num = num2;
            num2 = num3;
        Label_0032:
            return base.get_text().Substring(num, num2 - num);
        }

        private bool InPlaceEditingForWin()
        {
            return (TouchScreenKeyboard.get_isSupported() == 0);
        }

        private unsafe void InsertForWin(char c)
        {
            string str;
            int num;
            if (base.get_readOnly() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            str = &c.ToString();
            this.DeleteForWin();
            if (base.get_characterLimit() <= 0)
            {
                goto Label_003D;
            }
            if (base.get_text().Length < base.get_characterLimit())
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            base.m_Text = base.get_text().Insert(base.m_CaretPosition, str);
            base.set_caretPositionInternal(num = base.get_caretPositionInternal() + str.Length);
            base.set_caretSelectPositionInternal(num);
            if (base.get_onValueChanged() == null)
            {
                goto Label_008D;
            }
            base.get_onValueChanged().Invoke(base.get_text());
        Label_008D:
            return;
        }

        private bool IsValidCharForWin(char c)
        {
            if (c != 0x7f)
            {
                goto Label_000A;
            }
            return 0;
        Label_000A:
            if (c == 9)
            {
                goto Label_001A;
            }
            if (c != 10)
            {
                goto Label_001C;
            }
        Label_001A:
            return 1;
        Label_001C:
            return base.m_TextComponent.get_font().HasCharacter(c);
        }

        protected InputField.EditState KeyPressedForWin(Event evt)
        {
            EventModifiers modifiers;
            RuntimePlatform platform;
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            char ch;
            KeyCode code;
            modifiers = evt.get_modifiers();
            platform = Application.get_platform();
            flag2 = ((((platform == null) || (platform == 1)) ? 1 : (platform == 3)) == null) ? (((modifiers & 2) == 0) == 0) : (((modifiers & 8) == 0) == 0);
            flag3 = ((modifiers & 1) == 0) == 0;
            flag4 = ((modifiers & 4) == 0) == 0;
            flag5 = ((flag2 == null) || (flag4 != null)) ? 0 : (flag3 == 0);
            code = evt.get_keyCode();
            switch ((code - 0x10f))
            {
                case 0:
                    goto Label_01EB;

                case 1:
                    goto Label_00A6;

                case 2:
                    goto Label_01D5;

                case 3:
                    goto Label_01E0;

                case 4:
                    goto Label_01CA;

                case 5:
                    goto Label_01BF;

                case 6:
                    goto Label_00A6;

                case 7:
                    goto Label_010A;

                case 8:
                    goto Label_0114;
            }
        Label_00A6:
            switch ((code - 0x61))
            {
                case 0:
                    goto Label_011E;

                case 1:
                    goto Label_00BC;

                case 2:
                    goto Label_0132;
            }
        Label_00BC:
            switch ((code - 0x76))
            {
                case 0:
                    goto Label_0166;

                case 1:
                    goto Label_00D2;

                case 2:
                    goto Label_017F;
            }
        Label_00D2:
            if (code == 8)
            {
                goto Label_00FA;
            }
            if (code == 13)
            {
                goto Label_01EB;
            }
            if (code == 0x1b)
            {
                goto Label_01FE;
            }
            if (code == 0x7f)
            {
                goto Label_0102;
            }
            goto Label_0206;
        Label_00FA:
            this.BackspaceForWin();
            return 0;
        Label_0102:
            this.ForwardSpaceForWin();
            return 0;
        Label_010A:
            base.MoveTextStart(flag3);
            return 0;
        Label_0114:
            base.MoveTextEnd(flag3);
            return 0;
        Label_011E:
            if (flag5 == null)
            {
                goto Label_0206;
            }
            base.SelectAll();
            return 0;
            goto Label_0206;
        Label_0132:
            if (flag5 == null)
            {
                goto Label_0206;
            }
            if (base.get_inputType() == 2)
            {
                goto Label_0155;
            }
            clipboard = this.GetSelectedStringForWin();
            goto Label_015F;
        Label_0155:
            clipboard = string.Empty;
        Label_015F:
            return 0;
            goto Label_0206;
        Label_0166:
            if (flag5 == null)
            {
                goto Label_0206;
            }
            this.Append(clipboard);
            return 0;
            goto Label_0206;
        Label_017F:
            if (flag5 == null)
            {
                goto Label_0206;
            }
            if (base.get_inputType() == 2)
            {
                goto Label_01A2;
            }
            clipboard = this.GetSelectedStringForWin();
            goto Label_01AC;
        Label_01A2:
            clipboard = string.Empty;
        Label_01AC:
            this.DeleteForWin();
            this.SendOnValueChangedAndUpdateLabelForWin();
            return 0;
            goto Label_0206;
        Label_01BF:
            this.MoveLeft(flag3, flag2);
            return 0;
        Label_01CA:
            this.MoveRight(flag3, flag2);
            return 0;
        Label_01D5:
            this.MoveUp(flag3, 1);
            return 0;
        Label_01E0:
            this.MoveDown(flag3, 1);
            return 0;
        Label_01EB:
            if (base.get_lineType() == 2)
            {
                goto Label_0206;
            }
            return 1;
            goto Label_0206;
        Label_01FE:
            return base.KeyPressed(evt);
        Label_0206:
            ch = evt.get_character();
            if (base.get_multiLine() != null)
            {
                goto Label_0236;
            }
            if (ch == 9)
            {
                goto Label_0234;
            }
            if (ch == 13)
            {
                goto Label_0234;
            }
            if (ch != 10)
            {
                goto Label_0236;
            }
        Label_0234:
            return 0;
        Label_0236:
            if (ch == 13)
            {
                goto Label_0247;
            }
            if (ch != 3)
            {
                goto Label_024B;
            }
        Label_0247:
            ch = 10;
        Label_024B:
            if (this.IsValidCharForWin(ch) == null)
            {
                goto Label_0260;
            }
            this.Append(ch);
        Label_0260:
            if (ch != null)
            {
                goto Label_027D;
            }
            if (Input.get_compositionString().Length <= 0)
            {
                goto Label_027D;
            }
            base.UpdateLabel();
        Label_027D:
            return 0;
        }

        private unsafe int LineDownCharacterPosition(int originalPos, bool goToLastChar)
        {
            UICharInfo info;
            int num;
            int num2;
            int num3;
            UILineInfo info2;
            UICharInfo info3;
            if (originalPos < base.get_cachedInputTextGenerator().get_characterCountVisible())
            {
                goto Label_001D;
            }
            return base.get_text().Length;
        Label_001D:
            info = base.get_cachedInputTextGenerator().get_characters()[originalPos];
            num = this.DetermineCharacterLineForWin(originalPos, base.get_cachedInputTextGenerator());
            if ((num + 1) < base.get_cachedInputTextGenerator().get_lineCount())
            {
                goto Label_0068;
            }
            return ((goToLastChar == null) ? originalPos : base.get_text().Length);
        Label_0068:
            num2 = GetLineEndPositionForWin(base.get_cachedInputTextGenerator(), num + 1);
            info2 = base.get_cachedInputTextGenerator().get_lines()[num + 1];
            num3 = &info2.startCharIdx;
            goto Label_00CF;
        Label_0099:
            info3 = base.get_cachedInputTextGenerator().get_characters()[num3];
            if (&&info3.cursorPos.x < &&info.cursorPos.x)
            {
                goto Label_00CB;
            }
            return num3;
        Label_00CB:
            num3 += 1;
        Label_00CF:
            if (num3 < num2)
            {
                goto Label_0099;
            }
            return num2;
        }

        private unsafe int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
        {
            UICharInfo info;
            int num;
            int num2;
            int num3;
            UILineInfo info2;
            UILineInfo info3;
            UICharInfo info4;
            if (originalPos <= base.get_cachedInputTextGenerator().get_characterCountVisible())
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            info = base.get_cachedInputTextGenerator().get_characters()[originalPos];
            num = this.DetermineCharacterLineForWin(originalPos, base.get_cachedInputTextGenerator());
            if (num > 0)
            {
                goto Label_0048;
            }
            return ((goToFirstChar == null) ? originalPos : 0);
        Label_0048:
            info2 = base.get_cachedInputTextGenerator().get_lines()[num];
            num2 = &info2.startCharIdx - 1;
            info3 = base.get_cachedInputTextGenerator().get_lines()[num - 1];
            num3 = &info3.startCharIdx;
            goto Label_00BD;
        Label_0087:
            info4 = base.get_cachedInputTextGenerator().get_characters()[num3];
            if (&&info4.cursorPos.x < &&info.cursorPos.x)
            {
                goto Label_00B9;
            }
            return num3;
        Label_00B9:
            num3 += 1;
        Label_00BD:
            if (num3 < num2)
            {
                goto Label_0087;
            }
            return num2;
        }

        private void MoveDown(bool shift, bool goToLastChar)
        {
            int num;
            int num2;
            if ((base.get_caretPositionInternal() == base.get_caretSelectPositionInternal()) || (shift != null))
            {
                goto Label_0037;
            }
            num2 = Mathf.Max(base.get_caretPositionInternal(), base.get_caretSelectPositionInternal());
            base.set_caretSelectPositionInternal(num2);
            base.set_caretPositionInternal(num2);
        Label_0037:
            num = (base.get_multiLine() == null) ? base.get_text().Length : this.LineDownCharacterPosition(base.get_caretSelectPositionInternal(), goToLastChar);
            if (shift == null)
            {
                goto Label_0072;
            }
            base.set_caretSelectPositionInternal(num);
            goto Label_0082;
        Label_0072:
            num2 = num;
            base.set_caretSelectPositionInternal(num2);
            base.set_caretPositionInternal(num2);
        Label_0082:
            return;
        }

        private void MoveLeft(bool shift, bool ctrl)
        {
            int num;
            int num2;
            if (base.get_caretPositionInternal() == base.get_caretSelectPositionInternal())
            {
                goto Label_0038;
            }
            if (shift != null)
            {
                goto Label_0038;
            }
            num2 = Mathf.Min(base.get_caretPositionInternal(), base.get_caretSelectPositionInternal());
            base.set_caretSelectPositionInternal(num2);
            base.set_caretPositionInternal(num2);
            return;
        Label_0038:
            if (ctrl == null)
            {
                goto Label_004A;
            }
            num = this.FindtPrevWordBegin();
            goto Label_0053;
        Label_004A:
            num = base.get_caretSelectPositionInternal() - 1;
        Label_0053:
            if (shift == null)
            {
                goto Label_0065;
            }
            base.set_caretSelectPositionInternal(num);
            goto Label_0075;
        Label_0065:
            num2 = num;
            base.set_caretPositionInternal(num2);
            base.set_caretSelectPositionInternal(num2);
        Label_0075:
            return;
        }

        private void MoveRight(bool shift, bool ctrl)
        {
            int num;
            int num2;
            if (base.get_caretPositionInternal() == base.get_caretSelectPositionInternal())
            {
                goto Label_0038;
            }
            if (shift != null)
            {
                goto Label_0038;
            }
            num2 = Mathf.Max(base.get_caretPositionInternal(), base.get_caretSelectPositionInternal());
            base.set_caretSelectPositionInternal(num2);
            base.set_caretPositionInternal(num2);
            return;
        Label_0038:
            if (ctrl == null)
            {
                goto Label_004A;
            }
            num = this.FindtNextWordBeginForWin();
            goto Label_0053;
        Label_004A:
            num = base.get_caretSelectPositionInternal() + 1;
        Label_0053:
            if (shift == null)
            {
                goto Label_0065;
            }
            base.set_caretSelectPositionInternal(num);
            goto Label_0075;
        Label_0065:
            num2 = num;
            base.set_caretPositionInternal(num2);
            base.set_caretSelectPositionInternal(num2);
        Label_0075:
            return;
        }

        private void MoveUp(bool shift, bool goToFirstChar)
        {
            int num;
            int num2;
            if ((base.get_caretPositionInternal() == base.get_caretSelectPositionInternal()) || (shift != null))
            {
                goto Label_0037;
            }
            num2 = Mathf.Min(base.get_caretPositionInternal(), base.get_caretSelectPositionInternal());
            base.set_caretSelectPositionInternal(num2);
            base.set_caretPositionInternal(num2);
        Label_0037:
            num = (base.get_multiLine() == null) ? 0 : this.LineUpCharacterPosition(base.get_caretSelectPositionInternal(), goToFirstChar);
            if (shift == null)
            {
                goto Label_0068;
            }
            base.set_caretSelectPositionInternal(num);
            goto Label_0078;
        Label_0068:
            num2 = num;
            base.set_caretPositionInternal(num2);
            base.set_caretSelectPositionInternal(num2);
        Label_0078:
            return;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            return;
        }

        protected override void OnDestroy()
        {
            if (base.m_Keyboard == null)
            {
                goto Label_0017;
            }
            base.m_Keyboard.set_active(0);
        Label_0017:
            base.OnDestroy();
            return;
        }

        private unsafe void OnFillVBO(Mesh vbo)
        {
            VertexHelper helper;
            Rect rect;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            helper = new VertexHelper();
        Label_0006:
            try
            {
                if (base.get_isFocused() != null)
                {
                    goto Label_001D;
                }
                helper.FillMesh(vbo);
                goto Label_0139;
            Label_001D:
                rect = base.m_TextComponent.get_rectTransform().get_rect();
                vector = &rect.get_size();
                vector2 = Text.GetTextAnchorPivot(base.m_TextComponent.get_alignment());
                vector3 = Vector2.get_zero();
                float introduced7 = &rect.get_xMin();
                &vector3.x = Mathf.Lerp(introduced7, &rect.get_xMax(), &vector2.x);
                float introduced8 = &rect.get_yMin();
                &vector3.y = Mathf.Lerp(introduced8, &rect.get_yMax(), &vector2.y);
                vector5 = (base.m_TextComponent.PixelAdjustPoint(vector3) - vector3) + Vector2.Scale(vector, vector2);
                &vector5.x -= Mathf.Floor(0.5f + &vector5.x);
                &vector5.y -= Mathf.Floor(0.5f + &vector5.y);
                if (base.get_caretPositionInternal() != base.get_caretSelectPositionInternal())
                {
                    goto Label_0117;
                }
                this.GenerateCaret(helper, vector5);
                goto Label_0120;
            Label_0117:
                this.GenerateHightlight(helper, vector5);
            Label_0120:
                helper.FillMesh(vbo);
                goto Label_0139;
            }
            finally
            {
            Label_012C:
                if (helper == null)
                {
                    goto Label_0138;
                }
                helper.Dispose();
            Label_0138:;
            }
        Label_0139:
            return;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            this.m_IsPointerOutofRange = 0;
            return;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            this.m_IsPointerOutofRange = 1;
            return;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            return;
        }

        public override unsafe void OnUpdateSelected(BaseEventData eventData)
        {
            Event event2;
            bool flag;
            InputField.EditState state;
            EventType type;
            string str;
            Dictionary<string, int> dictionary;
            int num;
            if (base.get_isFocused() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.m_IsPointerOutofRange == null)
            {
                goto Label_0045;
            }
            if (this.GetMouseButtonDown() == null)
            {
                goto Label_0045;
            }
            event2 = new Event();
        Label_002D:
            if (Event.PopEvent(event2) != null)
            {
                goto Label_002D;
            }
            base.UpdateLabel();
            eventData.Use();
            return;
        Label_0045:
            flag = 0;
            goto Label_0133;
        Label_004C:
            if (this.m_EventWork.get_rawType() != 4)
            {
                goto Label_0083;
            }
            flag = 1;
            if (this.KeyPressedForWin(this.m_EventWork) != 1)
            {
                goto Label_00A7;
            }
            base.DeactivateInputField();
            goto Label_0143;
            goto Label_00A7;
        Label_0083:
            if (this.m_EventWork.get_rawType() != 5)
            {
                goto Label_00A7;
            }
            if (this.m_EventWork.get_keyCode() != 8)
            {
                goto Label_00A7;
            }
            flag = 1;
        Label_00A7:
            type = this.m_EventWork.get_type();
            if (type == 13)
            {
                goto Label_00C8;
            }
            if (type == 14)
            {
                goto Label_00C8;
            }
            goto Label_0133;
        Label_00C8:
            str = this.m_EventWork.get_commandName();
            if (str == null)
            {
                goto Label_0133;
            }
            if (<>f__switch$mapA != null)
            {
                goto Label_0102;
            }
            dictionary = new Dictionary<string, int>(1);
            dictionary.Add("SelectAll", 0);
            <>f__switch$mapA = dictionary;
        Label_0102:
            if (<>f__switch$mapA.TryGetValue(str, &num) == null)
            {
                goto Label_0133;
            }
            if (num == null)
            {
                goto Label_0121;
            }
            goto Label_012E;
        Label_0121:
            base.SelectAll();
            flag = 1;
        Label_012E:;
        Label_0133:
            if (Event.PopEvent(this.m_EventWork) != null)
            {
                goto Label_004C;
            }
        Label_0143:
            if (flag == null)
            {
                goto Label_014F;
            }
            base.UpdateLabel();
        Label_014F:
            eventData.Use();
            return;
        }

        public override void Rebuild(CanvasUpdate update)
        {
            CanvasUpdate update2;
            update2 = update;
            if (update2 == 4)
            {
                goto Label_000E;
            }
            goto Label_0019;
        Label_000E:
            this.UpdateGeometryForWin();
        Label_0019:
            return;
        }

        public static void ResetInput()
        {
            NowInput = 0;
            return;
        }

        private void SendOnValueChangedAndUpdateLabelForWin()
        {
            if (base.get_onValueChanged() == null)
            {
                goto Label_001C;
            }
            base.get_onValueChanged().Invoke(base.get_text());
        Label_001C:
            base.UpdateLabel();
            return;
        }

        private void UpdateGeometryForWin()
        {
            GameObject obj2;
            if (base.get_shouldHideMobileInput() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.m_Renderer == null) == null)
            {
                goto Label_00C7;
            }
            if ((base.m_TextComponent != null) == null)
            {
                goto Label_00C7;
            }
            obj2 = new GameObject(base.get_transform().get_name() + " Input Caret");
            obj2.set_hideFlags(0x34);
            obj2.get_transform().SetParent(base.m_TextComponent.get_transform().get_parent());
            obj2.get_transform().SetAsFirstSibling();
            obj2.set_layer(base.get_gameObject().get_layer());
            this.m_RectTrans = obj2.AddComponent<RectTransform>();
            this.m_Renderer = obj2.AddComponent<CanvasRenderer>();
            this.m_Renderer.SetMaterial(Graphic.get_defaultGraphicMaterial(), Texture2D.get_whiteTexture());
            obj2.AddComponent<LayoutElement>().set_ignoreLayout(1);
            this.AssignPositioningIfNeeded();
        Label_00C7:
            if ((this.m_Renderer == null) == null)
            {
                goto Label_00D9;
            }
            return;
        Label_00D9:
            this.OnFillVBO(base.get_mesh());
            this.m_Renderer.SetMesh(base.get_mesh());
            return;
        }

        public static bool IsFocus
        {
            get
            {
                return NowInput;
            }
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
                return;
            }
        }
    }
}

