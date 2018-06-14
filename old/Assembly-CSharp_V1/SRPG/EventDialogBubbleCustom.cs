// Decompiled with JetBrains decompiler
// Type: SRPG.EventDialogBubbleCustom
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventDialogBubbleCustom : MonoBehaviour
  {
    public static List<EventDialogBubbleCustom> Instances = new List<EventDialogBubbleCustom>();
    private static Regex regEndTag = new Regex("^\\s*/\\s*([a-zA-Z0-9]+)\\s*");
    private static Regex regColor = new Regex("color=(#?[a-z0-9]+)");
    public const float TopMargin = 30f;
    public const float BottomMargin = 20f;
    public const float LeftMargin = 20f;
    public const float RightMargin = 20f;
    public UnityEngine.UI.Text NameText;
    public UnityEngine.UI.Text BodyText;
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
    private EventDialogBubbleCustom.Character[] mCharacters;
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
    private bool mFadingOut;
    private bool mShouldOpen;
    private EventDialogBubbleCustom.Anchors mAnchor;

    public EventDialogBubbleCustom()
    {
      base.\u002Ector();
    }

    public string VoiceSheetName { get; set; }

    public string VoiceCueName { get; set; }

    public static EventDialogBubbleCustom Find(string id)
    {
      for (int index = EventDialogBubbleCustom.Instances.Count - 1; index >= 0; --index)
      {
        if (EventDialogBubbleCustom.Instances[index].BubbleID == id)
          return EventDialogBubbleCustom.Instances[index];
      }
      return (EventDialogBubbleCustom) null;
    }

    public static EventDialogBubbleCustom FindHead()
    {
      return EventDialogBubbleCustom.Instances[0];
    }

    public static void DiscardAll()
    {
      for (int index = EventDialogBubbleCustom.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventDialogBubbleCustom.Instances[index]).get_gameObject().get_activeInHierarchy())
          Object.Destroy((Object) ((Component) EventDialogBubbleCustom.Instances[index]).get_gameObject());
        else
          EventDialogBubbleCustom.Instances[index].mCloseAndDestroy = true;
      }
      EventDialogBubbleCustom.Instances.Clear();
    }

    private void Awake()
    {
      EventDialogBubbleCustom.Instances.Add(this);
      if (!Object.op_Inequality((Object) this.BodyText, (Object) null))
        return;
      RectTransform transform1 = ((Component) this).get_transform() as RectTransform;
      RectTransform transform2 = ((Component) this.BodyText).get_transform() as RectTransform;
      Rect rect1 = transform1.get_rect();
      // ISSUE: explicit reference operation
      double width1 = (double) ((Rect) @rect1).get_width();
      Rect rect2 = transform2.get_rect();
      // ISSUE: explicit reference operation
      double width2 = (double) ((Rect) @rect2).get_width();
      this.mBaseWidth = (float) (width1 - width2);
    }

    private void OnDestroy()
    {
      EventDialogBubbleCustom.Instances.Remove(this);
    }

    private void FadeOutVoice()
    {
      if (this.mVoice == null)
        return;
      this.mVoice.StopAll(this.VoiceFadeOutSec);
      this.mVoice.Cleanup();
      this.mVoice = (MySound.Voice) null;
    }

    public bool IsPrinting
    {
      get
      {
        if (!this.mFadingOut && this.mTextNeedsUpdate)
          return this.mNumCharacters > 0;
        return false;
      }
    }

    public void Skip()
    {
      float time = Time.get_time();
      if (!this.IsPrinting || (double) time - (double) this.mStartTime <= 0.100000001490116)
        return;
      for (int index = 0; index < this.mNumCharacters; ++index)
        this.mCharacters[index].TimeOffset = 0.0f;
      this.mStartTime = time - this.FadeInTime;
      this.mSkipFadeOut = true;
    }

    public void AdjustWidth(string bodyText)
    {
      if (Object.op_Equality((Object) this.BodyText, (Object) null) || !this.AutoExpandWidth)
        return;
      EventDialogBubbleCustom.Element[] elementArray = EventDialogBubbleCustom.SplitTags(bodyText);
      StringBuilder stringBuilder = new StringBuilder(elementArray.Length);
      for (int index = 0; index < elementArray.Length; ++index)
      {
        if (!string.IsNullOrEmpty(elementArray[index].Value))
          stringBuilder.Append(elementArray[index].Value);
      }
      float num = Mathf.Min(this.BodyText.get_cachedTextGeneratorForLayout().GetPreferredWidth(stringBuilder.ToString(), this.BodyText.GetGenerationSettings(Vector2.get_zero())) / this.BodyText.get_pixelsPerUnit(), this.MaxBodyTextWidth) + this.mBaseWidth;
      RectTransform transform = ((Component) this).get_transform() as RectTransform;
      Vector2 sizeDelta = transform.get_sizeDelta();
      sizeDelta.x = (__Null) (double) Mathf.Max((float) sizeDelta.x, num);
      transform.set_sizeDelta(sizeDelta);
    }

    public void SetName(string name)
    {
      if (!Object.op_Inequality((Object) this.NameText, (Object) null))
        return;
      this.NameText.set_text(name);
    }

    private static EventDialogBubbleCustom.Element[] SplitTags(string s)
    {
      int index1 = 0;
      List<EventDialogBubbleCustom.Element> elementList = new List<EventDialogBubbleCustom.Element>();
      while (index1 < s.Length)
      {
        bool flag = false;
        EventDialogBubbleCustom.Element element = new EventDialogBubbleCustom.Element();
        elementList.Add(element);
        string empty = string.Empty;
        if ((int) s[index1] == 60)
        {
          flag = true;
          int index2 = index1 + 1;
          while (index2 < s.Length && (int) s[index2] != 62)
            empty += (string) (object) s[index2++];
          index1 = index2 + 1;
        }
        else
        {
          while (index1 < s.Length && (int) s[index1] != 60)
            empty += (string) (object) s[index1++];
        }
        if (flag)
          element.Tag = empty;
        else
          element.Value = empty;
      }
      return elementList.ToArray();
    }

    private void Parse(EventDialogBubbleCustom.Element[] c, ref int n, string end, EventDialogBubbleCustom.Ctx ctx)
    {
      while (n < c.Length)
      {
        if (!string.IsNullOrEmpty(c[n].Tag))
        {
          Match match1;
          if ((match1 = EventDialogBubbleCustom.regEndTag.Match(c[n].Tag)).Success)
          {
            if (match1.Groups[1].Value == end)
            {
              ++n;
              break;
            }
            ++n;
          }
          else
          {
            Match match2;
            if ((match2 = EventDialogBubbleCustom.regColor.Match(c[n].Tag)).Success)
            {
              ++n;
              Color32 color = ctx.Color;
              ctx.Color = ColorUtility.ParseColor(match2.Groups[1].Value);
              this.Parse(c, ref n, "color", ctx);
              ctx.Color = color;
            }
            else
              ++n;
          }
        }
        else
        {
          this.PushCharacters(c[n].Value, ctx);
          ++n;
        }
      }
    }

    private void PushCharacters(string s, EventDialogBubbleCustom.Ctx ctx)
    {
      float num = this.mNumCharacters <= 0 ? 0.0f : this.mCharacters[this.mNumCharacters - 1].TimeOffset;
      for (int index = 0; index < s.Length; ++index)
      {
        float interval = ctx.Interval;
        if ((int) s[index] == 10)
          interval = this.NewLineInterval;
        this.mCharacters[this.mNumCharacters] = new EventDialogBubbleCustom.Character(s[index], ctx.Color, interval, num + interval);
        num = this.mCharacters[this.mNumCharacters].TimeOffset;
        ++this.mNumCharacters;
      }
    }

    private void FlushText()
    {
      string mTextQueue = this.mTextQueue;
      this.mTextQueue = (string) null;
      if (this.mCharacters == null || this.mCharacters.Length < mTextQueue.Length)
        this.mCharacters = new EventDialogBubbleCustom.Character[mTextQueue.Length * 2];
      string str = "REPLACE_PLAYER_NAME";
      string newValue = string.Empty;
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        newValue = MonoSingleton<GameManager>.GetInstanceDirect().Player.Name;
      string s = mTextQueue.Replace("<p_name>", str).Replace("<br>", "\n");
      EventAction_Dialog.TextSpeedTypes speed = EventAction_Dialog.TextSpeedTypes.Normal;
      int n = 0;
      EventDialogBubbleCustom.Ctx ctx = new EventDialogBubbleCustom.Ctx();
      ctx.Interval = speed.ToFloat();
      ctx.Color = Color32.op_Implicit(!Object.op_Inequality((Object) this.BodyText, (Object) null) ? Color.get_black() : ((Graphic) this.BodyText).get_color());
      this.mNumCharacters = 0;
      EventDialogBubbleCustom.Element[] c = EventDialogBubbleCustom.SplitTags(s);
      for (int index = 0; index < c.Length; ++index)
      {
        if (c[index] != null)
          c[index].Value = c[index].Value.Replace(str, newValue);
      }
      this.Parse(c, ref n, (string) null, ctx);
      if (Object.op_Inequality((Object) this.BodyText, (Object) null))
        this.BodyText.set_text(string.Empty);
      this.mStartTime = Time.get_time() + this.FadeInTime;
      this.mTextNeedsUpdate = this.mNumCharacters > 0;
      this.mFadingOut = false;
      if (string.IsNullOrEmpty(this.VoiceSheetName) || string.IsNullOrEmpty(this.VoiceCueName))
      {
        this.FadeOutVoice();
      }
      else
      {
        this.mVoice = new MySound.Voice(this.VoiceSheetName, (string) null, (string) null);
        this.mVoice.Play(this.VoiceCueName, 0.0f);
        this.VoiceCueName = (string) null;
      }
    }

    public void SetBody(string text)
    {
      if (this.mTextQueue == null && this.mNumCharacters <= 0)
      {
        this.mTextQueue = text;
        this.FlushText();
      }
      else
      {
        this.BeginFadeOut();
        this.mTextQueue = text;
      }
    }

    private void OnEnable()
    {
      this.mStartTime = Time.get_time();
    }

    private void Start()
    {
      this.mShouldOpen = true;
    }

    private void BeginFadeOut()
    {
      if (this.mSkipFadeOut)
      {
        for (int index = 0; index < this.mNumCharacters; ++index)
          this.mCharacters[index].TimeOffset = this.FadeOutTime;
      }
      else
      {
        for (int index = 0; index < this.mNumCharacters; ++index)
          this.mCharacters[index].TimeOffset = (float) index * this.FadeOutInterval + this.FadeOutTime;
      }
      this.mSkipFadeOut = false;
      this.mStartTime = Time.get_time();
      this.mFadingOut = true;
    }

    public bool Finished
    {
      get
      {
        if (!this.mFadingOut)
          return !this.mTextNeedsUpdate;
        return false;
      }
    }

    public void Open()
    {
      this.SetVisibility(true);
    }

    public void Close()
    {
      this.SetVisibility(false);
    }

    private void SetVisibility(bool open)
    {
      this.mShouldOpen = open;
      if (((Behaviour) this).get_enabled())
        return;
      ((Behaviour) this).set_enabled(true);
      this.UpdateStateBool();
    }

    public void Forward()
    {
      if (this.Finished)
        ;
    }

    private void UpdateText()
    {
      if (!this.mFadingOut)
      {
        if (!this.mTextNeedsUpdate)
          return;
        float time = Time.get_time();
        StringBuilder stringBuilder = new StringBuilder(this.mNumCharacters);
        for (int index = 0; index < this.mNumCharacters; ++index)
        {
          float num = Mathf.Clamp01((float) (1.0 - ((double) this.mStartTime + (double) this.mCharacters[index].TimeOffset - (double) time) / (double) this.FadeInTime));
          if ((double) num > 0.0)
          {
            Color32 color = this.mCharacters[index].Color;
            color.a = (__Null) (int) (byte) ((double) (float) color.a * (double) num);
            stringBuilder.Append("<color=");
            stringBuilder.Append(color.ToColorValue());
            stringBuilder.Append(">");
            stringBuilder.Append(this.mCharacters[index].Code);
            stringBuilder.Append("</color>");
          }
          else
            break;
        }
        if (Object.op_Inequality((Object) this.BodyText, (Object) null))
          this.BodyText.set_text(stringBuilder.ToString());
        if ((double) this.mStartTime + (double) this.mCharacters[this.mNumCharacters - 1].TimeOffset > (double) time)
          return;
        this.mTextNeedsUpdate = false;
      }
      else
      {
        float time = Time.get_time();
        StringBuilder stringBuilder = new StringBuilder(this.mNumCharacters);
        for (int index = 0; index < this.mNumCharacters; ++index)
        {
          float num = Mathf.Clamp01((this.mStartTime + this.mCharacters[index].TimeOffset - time) / this.FadeOutTime);
          Color32 color = this.mCharacters[index].Color;
          color.a = (__Null) (int) (byte) ((double) (float) color.a * (double) num);
          stringBuilder.Append("<color=");
          stringBuilder.Append(color.ToColorValue());
          stringBuilder.Append(">");
          stringBuilder.Append(this.mCharacters[index].Code);
          stringBuilder.Append("</color>");
        }
        if (Object.op_Inequality((Object) this.BodyText, (Object) null))
          this.BodyText.set_text(stringBuilder.ToString());
        if ((double) this.mStartTime + (double) this.mCharacters[this.mNumCharacters - 1].TimeOffset > (double) time)
          return;
        this.mNumCharacters = 0;
      }
    }

    private void UpdateStateBool()
    {
      if (!Object.op_Inequality((Object) this.BubbleAnimator, (Object) null))
        return;
      this.BubbleAnimator.SetBool(this.VisibilityBoolName, this.mShouldOpen);
    }

    private void Update()
    {
      if (this.mCloseAndDestroy)
      {
        this.mShouldOpen = false;
        if (Object.op_Inequality((Object) this.BubbleAnimator, (Object) null) && !string.IsNullOrEmpty(this.ClosedStateName))
        {
          this.UpdateStateBool();
          AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
          // ISSUE: explicit reference operation
          if (!((AnimatorStateInfo) @animatorStateInfo).IsName(this.ClosedStateName))
            return;
          Object.Destroy((Object) ((Component) this).get_gameObject());
        }
        else
          Object.Destroy((Object) ((Component) this).get_gameObject());
      }
      else
      {
        if (Object.op_Inequality((Object) this.BubbleAnimator, (Object) null))
        {
          this.UpdateStateBool();
          if (!this.mShouldOpen && !string.IsNullOrEmpty(this.ClosedStateName))
          {
            AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
            // ISSUE: explicit reference operation
            if (((AnimatorStateInfo) @animatorStateInfo).IsName(this.ClosedStateName))
              this.mNumCharacters = 0;
          }
          if (!string.IsNullOrEmpty(this.OpenedStateName))
          {
            AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
            // ISSUE: explicit reference operation
            if (!((AnimatorStateInfo) @animatorStateInfo).IsName(this.OpenedStateName))
            {
              this.mStartTime = Time.get_time();
              return;
            }
          }
        }
        if (this.mNumCharacters == 0 && !string.IsNullOrEmpty(this.mTextQueue))
          this.FlushText();
        if (this.mNumCharacters <= 0)
          return;
        this.UpdateText();
      }
    }

    public EventDialogBubbleCustom.Anchors Anchor
    {
      set
      {
        if (this.mAnchor == value)
          return;
        RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        this.mAnchor = value;
        switch (this.mAnchor)
        {
          case EventDialogBubbleCustom.Anchors.TopLeft:
            RectTransform rectTransform1 = component;
            Vector2 vector2_1 = new Vector2(0.0f, 1f);
            component.set_anchorMax(vector2_1);
            Vector2 vector2_2 = vector2_1;
            rectTransform1.set_anchorMin(vector2_2);
            component.set_pivot(new Vector2(0.0f, 1f));
            component.set_anchoredPosition(new Vector2(20f, -30f));
            break;
          case EventDialogBubbleCustom.Anchors.TopCenter:
            RectTransform rectTransform2 = component;
            Vector2 vector2_3 = new Vector2(0.5f, 1f);
            component.set_anchorMax(vector2_3);
            Vector2 vector2_4 = vector2_3;
            rectTransform2.set_anchorMin(vector2_4);
            component.set_pivot(new Vector2(0.5f, 1f));
            component.set_anchoredPosition(new Vector2(0.0f, -30f));
            break;
          case EventDialogBubbleCustom.Anchors.TopRight:
            RectTransform rectTransform3 = component;
            Vector2 vector2_5 = new Vector2(1f, 1f);
            component.set_anchorMax(vector2_5);
            Vector2 vector2_6 = vector2_5;
            rectTransform3.set_anchorMin(vector2_6);
            component.set_pivot(new Vector2(1f, 1f));
            component.set_anchoredPosition(new Vector2(-20f, -30f));
            break;
          case EventDialogBubbleCustom.Anchors.MiddleLeft:
            RectTransform rectTransform4 = component;
            Vector2 vector2_7 = new Vector2(0.0f, 0.5f);
            component.set_anchorMax(vector2_7);
            Vector2 vector2_8 = vector2_7;
            rectTransform4.set_anchorMin(vector2_8);
            component.set_pivot(new Vector2(0.0f, 0.5f));
            component.set_anchoredPosition(new Vector2(20f, 0.0f));
            break;
          case EventDialogBubbleCustom.Anchors.MiddleRight:
            RectTransform rectTransform5 = component;
            Vector2 vector2_9 = new Vector2(1f, 0.5f);
            component.set_anchorMax(vector2_9);
            Vector2 vector2_10 = vector2_9;
            rectTransform5.set_anchorMin(vector2_10);
            component.set_pivot(new Vector2(1f, 0.5f));
            component.set_anchoredPosition(new Vector2(-20f, 0.0f));
            break;
          case EventDialogBubbleCustom.Anchors.BottomLeft:
            RectTransform rectTransform6 = component;
            Vector2 vector2_11 = new Vector2(0.0f, 0.0f);
            component.set_anchorMax(vector2_11);
            Vector2 vector2_12 = vector2_11;
            rectTransform6.set_anchorMin(vector2_12);
            component.set_pivot(new Vector2(0.0f, 0.0f));
            component.set_anchoredPosition(new Vector2(20f, 20f));
            break;
          case EventDialogBubbleCustom.Anchors.BottomCenter:
            RectTransform rectTransform7 = component;
            Vector2 vector2_13 = new Vector2(0.5f, 0.0f);
            component.set_anchorMax(vector2_13);
            Vector2 vector2_14 = vector2_13;
            rectTransform7.set_anchorMin(vector2_14);
            component.set_pivot(new Vector2(0.5f, 0.0f));
            component.set_anchoredPosition(new Vector2(0.0f, 20f));
            break;
          case EventDialogBubbleCustom.Anchors.BottomRight:
            RectTransform rectTransform8 = component;
            Vector2 vector2_15 = new Vector2(1f, 0.0f);
            component.set_anchorMax(vector2_15);
            Vector2 vector2_16 = vector2_15;
            rectTransform8.set_anchorMin(vector2_16);
            component.set_pivot(new Vector2(1f, 0.0f));
            component.set_anchoredPosition(new Vector2(-20f, 20f));
            break;
        }
      }
      get
      {
        return this.mAnchor;
      }
    }

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
      }
    }

    private class Element
    {
      public string Tag;
      public string Value;
    }

    private struct Ctx
    {
      public Color32 Color;
      public float Interval;
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
      BottomRight,
    }
  }
}
