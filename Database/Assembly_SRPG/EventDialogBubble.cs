namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventDialogBubble : MonoBehaviour
    {
        public const float TopMargin = 30f;
        public const float BottomMargin = 20f;
        public const float LeftMargin = 20f;
        public const float RightMargin = 20f;
        public static List<EventDialogBubble> Instances;
        public RawImage PortraitFront;
        public Text NameText;
        public Text BodyText;
        [NonSerialized]
        public Texture2D CustomEmotion;
        private SRPG.PortraitSet mPortraitSet;
        private SRPG.PortraitSet.EmotionTypes mDesiredEmotion;
        private SRPG.PortraitSet.EmotionTypes mCurrentEmotion;
        public string VisibilityBoolName;
        public Animator BubbleAnimator;
        public string OpenedStateName;
        public string ClosedStateName;
        [NonSerialized]
        public string BubbleID;
        private bool mCloseAndDestroy;
        private MySound.Voice mVoice;
        private readonly float VoiceFadeOutSec;
        private bool mSkipFadeOut;
        public float FadeInTime;
        public float FadeOutTime;
        public float FadeOutInterval;
        private Character[] mCharacters;
        private int mNumCharacters;
        public float NewLineInterval;
        [NonSerialized]
        public EventAction_Dialog.TextSpeedTypes TextSpeed;
        public bool AutoExpandWidth;
        public float MaxBodyTextWidth;
        private float mBaseWidth;
        private float mStartTime;
        private bool mTextNeedsUpdate;
        private string mTextQueue;
        private static Regex regEndTag;
        private static Regex regColor;
        private bool mFadingOut;
        private bool mShouldOpen;
        private Anchors mAnchor;
        [CompilerGenerated]
        private string <VoiceSheetName>k__BackingField;
        [CompilerGenerated]
        private string <VoiceCueName>k__BackingField;

        static EventDialogBubble()
        {
            Instances = new List<EventDialogBubble>();
            regEndTag = new Regex(@"^\s*/\s*([a-zA-Z0-9]+)\s*");
            regColor = new Regex("color=(#?[a-z0-9]+)");
            return;
        }

        public EventDialogBubble()
        {
            this.VisibilityBoolName = "open";
            this.OpenedStateName = "opened";
            this.ClosedStateName = "closed";
            this.VoiceFadeOutSec = 0.1f;
            this.FadeInTime = 1f;
            this.FadeOutTime = 0.5f;
            this.FadeOutInterval = 0.05f;
            this.NewLineInterval = 0.5f;
            this.AutoExpandWidth = 1;
            this.MaxBodyTextWidth = 800f;
            base..ctor();
            return;
        }

        public unsafe void AdjustWidth(string bodyText)
        {
            Element[] elementArray;
            StringBuilder builder;
            int num;
            float num2;
            float num3;
            RectTransform transform;
            Vector2 vector;
            if ((this.BodyText == null) != null)
            {
                goto Label_001C;
            }
            if (this.AutoExpandWidth != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            elementArray = SplitTags(bodyText);
            builder = new StringBuilder((int) elementArray.Length);
            num = 0;
            goto Label_0059;
        Label_0034:
            if (string.IsNullOrEmpty(elementArray[num].Value) != null)
            {
                goto Label_0055;
            }
            builder.Append(elementArray[num].Value);
        Label_0055:
            num += 1;
        Label_0059:
            if (num < ((int) elementArray.Length))
            {
                goto Label_0034;
            }
            num2 = this.BodyText.get_cachedTextGeneratorForLayout().GetPreferredWidth(builder.ToString(), this.BodyText.GetGenerationSettings(Vector2.get_zero())) / this.BodyText.get_pixelsPerUnit();
            num3 = Mathf.Min(num2, this.MaxBodyTextWidth) + this.mBaseWidth;
            transform = base.get_transform() as RectTransform;
            vector = transform.get_sizeDelta();
            &vector.x = Mathf.Max(&vector.x, num3);
            transform.set_sizeDelta(vector);
            return;
        }

        private unsafe void Awake()
        {
            RectTransform transform;
            RectTransform transform2;
            Rect rect;
            Rect rect2;
            Instances.Add(this);
            if ((this.BodyText != null) == null)
            {
                goto Label_005C;
            }
            transform = base.get_transform() as RectTransform;
            transform2 = this.BodyText.get_transform() as RectTransform;
            this.mBaseWidth = &transform.get_rect().get_width() - &transform2.get_rect().get_width();
        Label_005C:
            return;
        }

        private unsafe void BeginFadeOut()
        {
            int num;
            int num2;
            if (this.mSkipFadeOut == null)
            {
                goto Label_003E;
            }
            num = 0;
            goto Label_002D;
        Label_0012:
            &(this.mCharacters[num]).TimeOffset = this.FadeOutTime;
            num += 1;
        Label_002D:
            if (num < this.mNumCharacters)
            {
                goto Label_0012;
            }
            goto Label_0076;
        Label_003E:
            num2 = 0;
            goto Label_006A;
        Label_0045:
            &(this.mCharacters[num2]).TimeOffset = (((float) num2) * this.FadeOutInterval) + this.FadeOutTime;
            num2 += 1;
        Label_006A:
            if (num2 < this.mNumCharacters)
            {
                goto Label_0045;
            }
        Label_0076:
            this.mSkipFadeOut = 0;
            this.mStartTime = Time.get_time();
            this.mFadingOut = 1;
            return;
        }

        public void Close()
        {
            this.FadeOutVoice();
            this.SetVisibility(0);
            return;
        }

        public static void DiscardAll()
        {
            int num;
            num = Instances.Count - 1;
            goto Label_005B;
        Label_0012:
            if (Instances[num].get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0046;
            }
            Object.Destroy(Instances[num].get_gameObject());
            goto Label_0057;
        Label_0046:
            Instances[num].mCloseAndDestroy = 1;
        Label_0057:
            num -= 1;
        Label_005B:
            if (num >= 0)
            {
                goto Label_0012;
            }
            Instances.Clear();
            return;
        }

        private void FadeOutVoice()
        {
            if (this.mVoice != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mVoice.StopAll(this.VoiceFadeOutSec);
            this.mVoice.Cleanup();
            this.mVoice = null;
            return;
        }

        public static EventDialogBubble Find(string id)
        {
            int num;
            num = Instances.Count - 1;
            goto Label_003D;
        Label_0012:
            if ((Instances[num].BubbleID == id) == null)
            {
                goto Label_0039;
            }
            return Instances[num];
        Label_0039:
            num -= 1;
        Label_003D:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return null;
        }

        private unsafe void FlushText()
        {
            string str;
            EventAction_Dialog.TextSpeedTypes types;
            int num;
            Ctx ctx;
            str = this.mTextQueue;
            this.mTextQueue = null;
            if ((this.mCharacters != null) && (((int) this.mCharacters.Length) >= str.Length))
            {
                goto Label_003F;
            }
            this.mCharacters = new Character[str.Length * 2];
        Label_003F:
            str = str.Replace("<br>", "\n");
            types = 0;
            num = 0;
            ctx = new Ctx();
            &ctx.Interval = SRPG_Extensions.ToFloat(types);
            &ctx.Color = ((this.BodyText != null) == null) ? Color.get_black() : this.BodyText.get_color();
            this.mNumCharacters = 0;
            this.Parse(SplitTags(str), &num, null, ctx);
            if ((this.BodyText != null) == null)
            {
                goto Label_00D3;
            }
            this.BodyText.set_text(string.Empty);
        Label_00D3:
            this.mStartTime = Time.get_time() + this.FadeInTime;
            this.mTextNeedsUpdate = this.mNumCharacters > 0;
            this.mFadingOut = 0;
            this.mCurrentEmotion = this.Emotion;
            if (string.IsNullOrEmpty(this.VoiceSheetName) != null)
            {
                goto Label_0127;
            }
            if (string.IsNullOrEmpty(this.VoiceCueName) == null)
            {
                goto Label_0132;
            }
        Label_0127:
            this.FadeOutVoice();
            goto Label_016F;
        Label_0132:
            this.mVoice = new MySound.Voice(this.VoiceSheetName, null, null, EventAction.IsUnManagedAssets(this.VoiceSheetName, 0));
            this.mVoice.Play(this.VoiceCueName, 0f, 0);
            this.VoiceCueName = null;
        Label_016F:
            return;
        }

        public void Forward()
        {
            this.FadeOutVoice();
            if (this.Finished == null)
            {
                goto Label_0011;
            }
        Label_0011:
            return;
        }

        private void OnDestroy()
        {
            this.FadeOutVoice();
            Instances.Remove(this);
            return;
        }

        private void OnEnable()
        {
            this.mStartTime = Time.get_time();
            return;
        }

        public void Open()
        {
            this.SetVisibility(1);
            return;
        }

        private unsafe void Parse(Element[] c, ref int n, string end, Ctx ctx)
        {
            Match match;
            Color32 color;
            goto Label_00ED;
        Label_0005:
            if (string.IsNullOrEmpty(c[*((int*) n)].Tag) != null)
            {
                goto Label_00D6;
            }
            if ((match = regEndTag.Match(c[*((int*) n)].Tag)).Success == null)
            {
                goto Label_0065;
            }
            if ((match.Groups[1].Value == end) == null)
            {
                goto Label_005A;
            }
            *((int*) n) += 1;
            return;
        Label_005A:
            *((int*) n) += 1;
            goto Label_00ED;
        Label_0065:
            if ((match = regColor.Match(c[*((int*) n)].Tag)).Success == null)
            {
                goto Label_00CB;
            }
            *((int*) n) += 1;
            color = &ctx.Color;
            &ctx.Color = ColorUtility.ParseColor(match.Groups[1].Value);
            this.Parse(c, n, "color", ctx);
            &ctx.Color = color;
            goto Label_00ED;
        Label_00CB:
            *((int*) n) += 1;
            goto Label_00ED;
        Label_00D6:
            this.PushCharacters(c[*((int*) n)].Value, ctx);
            *((int*) n) += 1;
        Label_00ED:
            if (*(((int*) n)) < ((int) c.Length))
            {
                goto Label_0005;
            }
            return;
        }

        private unsafe void PushCharacters(string s, Ctx ctx)
        {
            float num;
            int num2;
            float num3;
            num = (this.mNumCharacters <= 0) ? 0f : &(this.mCharacters[this.mNumCharacters - 1]).TimeOffset;
            num2 = 0;
            goto Label_00A9;
        Label_0036:
            num3 = &ctx.Interval;
            if (s[num2] != 10)
            {
                goto Label_0053;
            }
            num3 = this.NewLineInterval;
        Label_0053:
            *(&(this.mCharacters[this.mNumCharacters])) = new Character(s[num2], &ctx.Color, num3, num + num3);
            num = &(this.mCharacters[this.mNumCharacters]).TimeOffset;
            this.mNumCharacters += 1;
            num2 += 1;
        Label_00A9:
            if (num2 < s.Length)
            {
                goto Label_0036;
            }
            return;
        }

        public void SetBody(string text)
        {
            if (this.mTextQueue != null)
            {
                goto Label_0029;
            }
            if (this.mNumCharacters > 0)
            {
                goto Label_0029;
            }
            this.mTextQueue = text;
            this.FlushText();
            goto Label_0036;
        Label_0029:
            this.BeginFadeOut();
            this.mTextQueue = text;
        Label_0036:
            return;
        }

        public void SetName(string name)
        {
            if ((this.NameText != null) == null)
            {
                goto Label_001D;
            }
            this.NameText.set_text(name);
        Label_001D:
            return;
        }

        private void SetVisibility(bool open)
        {
            this.mShouldOpen = open;
            if (base.get_enabled() != null)
            {
                goto Label_001F;
            }
            base.set_enabled(1);
            this.UpdateStateBool();
        Label_001F:
            return;
        }

        public unsafe void Skip()
        {
            float num;
            int num2;
            num = Time.get_time();
            if (this.IsPrinting == null)
            {
                goto Label_0065;
            }
            if ((num - this.mStartTime) <= 0.1f)
            {
                goto Label_0065;
            }
            num2 = 0;
            goto Label_0044;
        Label_002A:
            &(this.mCharacters[num2]).TimeOffset = 0f;
            num2 += 1;
        Label_0044:
            if (num2 < this.mNumCharacters)
            {
                goto Label_002A;
            }
            this.mStartTime = num - this.FadeInTime;
            this.mSkipFadeOut = 1;
        Label_0065:
            return;
        }

        private static Element[] SplitTags(string s)
        {
            int num;
            List<Element> list;
            bool flag;
            Element element;
            string str;
            num = 0;
            list = new List<Element>();
            goto Label_00CB;
        Label_000D:
            flag = 0;
            element = new Element();
            list.Add(element);
            str = string.Empty;
            if (s[num] != 60)
            {
                goto Label_0096;
            }
            flag = 1;
            num += 1;
            goto Label_0055;
        Label_003C:
            str = str + ((char) s[num++]);
        Label_0055:
            if (num >= s.Length)
            {
                goto Label_006F;
            }
            if (s[num] != 0x3e)
            {
                goto Label_003C;
            }
        Label_006F:
            num += 1;
            goto Label_00B0;
            goto Label_0096;
        Label_007D:
            str = str + ((char) s[num++]);
        Label_0096:
            if (num >= s.Length)
            {
                goto Label_00B0;
            }
            if (s[num] != 60)
            {
                goto Label_007D;
            }
        Label_00B0:
            if (flag == null)
            {
                goto Label_00C3;
            }
            element.Tag = str;
            goto Label_00CB;
        Label_00C3:
            element.Value = str;
        Label_00CB:
            if (num < s.Length)
            {
                goto Label_000D;
            }
            return list.ToArray();
        }

        private void Start()
        {
            this.mShouldOpen = 1;
            return;
        }

        public void StopVoice()
        {
            if (this.mVoice != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mVoice.StopAll(0f);
            this.mVoice = null;
            return;
        }

        private unsafe void Update()
        {
            AnimatorStateInfo info;
            AnimatorStateInfo info2;
            AnimatorStateInfo info3;
            this.UpdatePortrait();
            if (this.mCloseAndDestroy == null)
            {
                goto Label_007B;
            }
            this.mShouldOpen = 0;
            if ((this.BubbleAnimator != null) == null)
            {
                goto Label_006F;
            }
            if (string.IsNullOrEmpty(this.ClosedStateName) != null)
            {
                goto Label_006F;
            }
            this.UpdateStateBool();
            if (&this.BubbleAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.ClosedStateName) == null)
            {
                goto Label_007A;
            }
            Object.Destroy(base.get_gameObject());
            return;
            goto Label_007A;
        Label_006F:
            Object.Destroy(base.get_gameObject());
        Label_007A:
            return;
        Label_007B:
            if ((this.BubbleAnimator != null) == null)
            {
                goto Label_010E;
            }
            this.UpdateStateBool();
            if (this.mShouldOpen != null)
            {
                goto Label_00D3;
            }
            if (string.IsNullOrEmpty(this.ClosedStateName) != null)
            {
                goto Label_00D3;
            }
            if (&this.BubbleAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.ClosedStateName) == null)
            {
                goto Label_00D3;
            }
            this.mNumCharacters = 0;
        Label_00D3:
            if (string.IsNullOrEmpty(this.OpenedStateName) != null)
            {
                goto Label_010E;
            }
            if (&this.BubbleAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.OpenedStateName) != null)
            {
                goto Label_010E;
            }
            this.mStartTime = Time.get_time();
            return;
        Label_010E:
            if (this.mNumCharacters != null)
            {
                goto Label_012F;
            }
            if (string.IsNullOrEmpty(this.mTextQueue) != null)
            {
                goto Label_012F;
            }
            this.FlushText();
        Label_012F:
            if (this.mNumCharacters <= 0)
            {
                goto Label_0141;
            }
            this.UpdateText();
        Label_0141:
            return;
        }

        private void UpdatePortrait()
        {
            if ((this.PortraitFront == null) != null)
            {
                goto Label_0033;
            }
            if ((this.PortraitSet == null) == null)
            {
                goto Label_0034;
            }
            if ((this.CustomEmotion == null) == null)
            {
                goto Label_0034;
            }
        Label_0033:
            return;
        Label_0034:
            if ((this.CustomEmotion == null) == null)
            {
                goto Label_0066;
            }
            this.PortraitFront.set_texture(this.PortraitSet.GetEmotionImage(this.mCurrentEmotion));
            goto Label_0077;
        Label_0066:
            this.PortraitFront.set_texture(this.CustomEmotion);
        Label_0077:
            return;
        }

        private void UpdateStateBool()
        {
            if ((this.BubbleAnimator != null) == null)
            {
                goto Label_0028;
            }
            this.BubbleAnimator.SetBool(this.VisibilityBoolName, this.mShouldOpen);
        Label_0028:
            return;
        }

        private unsafe void UpdateText()
        {
            float num;
            StringBuilder builder;
            int num2;
            float num3;
            Color32 color;
            float num4;
            StringBuilder builder2;
            int num5;
            float num6;
            Color32 color2;
            if (this.mFadingOut != null)
            {
                goto Label_013E;
            }
            if (this.mTextNeedsUpdate == null)
            {
                goto Label_025E;
            }
            num = Time.get_time();
            builder = new StringBuilder(this.mNumCharacters);
            num2 = 0;
            goto Label_00DF;
        Label_002F:
            num3 = Mathf.Clamp01(1f - (((this.mStartTime + &(this.mCharacters[num2]).TimeOffset) - num) / this.FadeInTime));
            if (num3 > 0f)
            {
                goto Label_006C;
            }
            goto Label_00EB;
        Label_006C:
            color = &(this.mCharacters[num2]).Color;
            &color.a = (byte) (((float) &color.a) * num3);
            builder.Append("<color=");
            builder.Append(SRPG_Extensions.ToColorValue(color));
            builder.Append(">");
            builder.Append(&(this.mCharacters[num2]).Code);
            builder.Append("</color>");
            num2 += 1;
        Label_00DF:
            if (num2 < this.mNumCharacters)
            {
                goto Label_002F;
            }
        Label_00EB:
            if ((this.BodyText != null) == null)
            {
                goto Label_010D;
            }
            this.BodyText.set_text(builder.ToString());
        Label_010D:
            if ((this.mStartTime + &(this.mCharacters[this.mNumCharacters - 1]).TimeOffset) > num)
            {
                goto Label_025E;
            }
            this.mTextNeedsUpdate = 0;
            goto Label_025E;
        Label_013E:
            num4 = Time.get_time();
            builder2 = new StringBuilder(this.mNumCharacters);
            num5 = 0;
            goto Label_0201;
        Label_015A:
            num6 = Mathf.Clamp01(((this.mStartTime + &(this.mCharacters[num5]).TimeOffset) - num4) / this.FadeOutTime);
            color2 = &(this.mCharacters[num5]).Color;
            &color2.a = (byte) (((float) &color2.a) * num6);
            builder2.Append("<color=");
            builder2.Append(SRPG_Extensions.ToColorValue(color2));
            builder2.Append(">");
            builder2.Append(&(this.mCharacters[num5]).Code);
            builder2.Append("</color>");
            num5 += 1;
        Label_0201:
            if (num5 < this.mNumCharacters)
            {
                goto Label_015A;
            }
            if ((this.BodyText != null) == null)
            {
                goto Label_0231;
            }
            this.BodyText.set_text(builder2.ToString());
        Label_0231:
            if ((this.mStartTime + &(this.mCharacters[this.mNumCharacters - 1]).TimeOffset) > num4)
            {
                goto Label_025E;
            }
            this.mNumCharacters = 0;
        Label_025E:
            return;
        }

        public SRPG.PortraitSet PortraitSet
        {
            get
            {
                return this.mPortraitSet;
            }
            set
            {
                this.mPortraitSet = value;
                return;
            }
        }

        public SRPG.PortraitSet.EmotionTypes Emotion
        {
            get
            {
                return this.mDesiredEmotion;
            }
            set
            {
                this.mDesiredEmotion = value;
                if (this.mTextQueue != null)
                {
                    goto Label_001E;
                }
                this.mCurrentEmotion = this.mDesiredEmotion;
            Label_001E:
                return;
            }
        }

        public string VoiceSheetName
        {
            [CompilerGenerated]
            get
            {
                return this.<VoiceSheetName>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VoiceSheetName>k__BackingField = value;
                return;
            }
        }

        public string VoiceCueName
        {
            [CompilerGenerated]
            get
            {
                return this.<VoiceCueName>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VoiceCueName>k__BackingField = value;
                return;
            }
        }

        public bool IsPrinting
        {
            get
            {
                return (((this.mFadingOut != null) || (this.mTextNeedsUpdate == null)) ? 0 : (this.mNumCharacters > 0));
            }
        }

        public bool Finished
        {
            get
            {
                return ((this.mFadingOut != null) ? 0 : (this.mTextNeedsUpdate == 0));
            }
        }

        public Anchors Anchor
        {
            get
            {
                return this.mAnchor;
            }
            set
            {
                RectTransform transform;
                Anchors anchors;
                Vector2 vector;
                if (this.mAnchor != value)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                transform = base.GetComponent<RectTransform>();
                this.mAnchor = value;
                switch ((this.mAnchor - 1))
                {
                    case 0:
                        goto Label_0053;

                    case 1:
                        goto Label_00A0;

                    case 2:
                        goto Label_00ED;

                    case 3:
                        goto Label_013A;

                    case 4:
                        goto Label_02BB;

                    case 5:
                        goto Label_0187;

                    case 6:
                        goto Label_01D4;

                    case 7:
                        goto Label_0221;

                    case 8:
                        goto Label_026E;
                }
                goto Label_02BB;
            Label_0053:
                vector = new Vector2(0f, 1f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(0f, 1f));
                transform.set_anchoredPosition(new Vector2(20f, -30f));
                goto Label_02BB;
            Label_00A0:
                vector = new Vector2(0.5f, 1f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(0.5f, 1f));
                transform.set_anchoredPosition(new Vector2(0f, -30f));
                goto Label_02BB;
            Label_00ED:
                vector = new Vector2(1f, 1f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(1f, 1f));
                transform.set_anchoredPosition(new Vector2(-20f, -30f));
                goto Label_02BB;
            Label_013A:
                vector = new Vector2(0f, 0.5f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(0f, 0.5f));
                transform.set_anchoredPosition(new Vector2(20f, 0f));
                goto Label_02BB;
            Label_0187:
                vector = new Vector2(1f, 0.5f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(1f, 0.5f));
                transform.set_anchoredPosition(new Vector2(-20f, 0f));
                goto Label_02BB;
            Label_01D4:
                vector = new Vector2(0f, 0f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(0f, 0f));
                transform.set_anchoredPosition(new Vector2(20f, 20f));
                goto Label_02BB;
            Label_0221:
                vector = new Vector2(0.5f, 0f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(0.5f, 0f));
                transform.set_anchoredPosition(new Vector2(0f, 20f));
                goto Label_02BB;
            Label_026E:
                vector = new Vector2(1f, 0f);
                transform.set_anchorMax(vector);
                transform.set_anchorMin(vector);
                transform.set_pivot(new Vector2(1f, 0f));
                transform.set_anchoredPosition(new Vector2(-20f, 20f));
            Label_02BB:
                return;
            }
        }

        public enum Anchors
        {
            None,
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            Center,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Character
        {
            public char Code;
            public Color32 Color;
            public float Interval;
            public float TimeOffset;
            public Character(char code, Color32 color, float interval, float timeOffset)
            {
                interval = Mathf.Max(interval, 0.01f);
                this.Code = code;
                this.Color = color;
                this.Interval = interval;
                this.TimeOffset = timeOffset;
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Ctx
        {
            public Color32 Color;
            public float Interval;
        }

        private class Element
        {
            public string Tag;
            public string Value;

            public Element()
            {
                base..ctor();
                return;
            }
        }
    }
}

