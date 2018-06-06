// Decompiled with JetBrains decompiler
// Type: SRPG.EventTelopBubble
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventTelopBubble : MonoBehaviour
  {
    public static List<EventTelopBubble> Instances = new List<EventTelopBubble>();
    private static Regex regEndTag = new Regex("^\\s*/\\s*([a-zA-Z0-9]+)\\s*");
    private static Regex regColor = new Regex("color=(#?[a-z0-9]+)");
    public RawImage PortraitFront;
    public RawImage PortraitBack;
    public UnityEngine.UI.Text NameText;
    public UnityEngine.UI.Text BodyText;
    public float PortraitTransitionTime;
    public bool TextColor;
    public Event2dAction_Telop.TextPositionTypes TextPosition;
    private bool mPortraitInitialized;
    public Texture2D CustomEmotion;
    private PortraitSet mPortraitSet;
    [NonSerialized]
    public PortraitSet.EmotionTypes Emotion;
    private PortraitSet.EmotionTypes mCurrentEmotion;
    private float mPortraitTransition;
    public string VisibilityBoolName;
    public Animator BubbleAnimator;
    public string OpenedStateName;
    public string ClosedStateName;
    [NonSerialized]
    public string BubbleID;
    private bool mCloseAndDestroy;
    private bool mSkipFadeOut;
    public float FadeInTime;
    public float FadeOutTime;
    public float FadeOutInterval;
    private EventTelopBubble.Character[] mCharacters;
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

    public EventTelopBubble()
    {
      base.\u002Ector();
    }

    public PortraitSet PortraitSet
    {
      set
      {
        this.mPortraitSet = value;
      }
      get
      {
        return this.mPortraitSet;
      }
    }

    public static EventTelopBubble Find(string id)
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (EventTelopBubble.Instances[index].BubbleID == id)
          return EventTelopBubble.Instances[index];
      }
      return (EventTelopBubble) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventTelopBubble.Instances[index]).get_gameObject().get_activeInHierarchy())
          Object.Destroy((Object) ((Component) EventTelopBubble.Instances[index]).get_gameObject());
        else
          EventTelopBubble.Instances[index].mCloseAndDestroy = true;
      }
      EventTelopBubble.Instances.Clear();
    }

    private void UpdatePortrait()
    {
      if (Object.op_Equality((Object) this.PortraitFront, (Object) null) || Object.op_Equality((Object) this.PortraitSet, (Object) null) && Object.op_Equality((Object) this.CustomEmotion, (Object) null))
        return;
      if (this.mPortraitInitialized && this.Emotion == this.mCurrentEmotion && (Object.op_Equality((Object) this.CustomEmotion, (Object) null) || Object.op_Equality((Object) this.PortraitFront.get_texture(), (Object) this.CustomEmotion)))
        this.mPortraitTransition = 0.0f;
      else if (Object.op_Inequality((Object) this.PortraitBack, (Object) null) && this.mPortraitInitialized)
      {
        this.mPortraitTransition += Time.get_deltaTime();
        if ((double) this.mPortraitTransition < (double) this.PortraitTransitionTime)
        {
          float num = Mathf.Clamp01(this.mPortraitTransition / this.PortraitTransitionTime);
          ((Graphic) this.PortraitFront).set_color(new Color(1f, 1f, 1f, num));
          this.PortraitBack.set_texture(this.PortraitFront.get_texture());
          if (Object.op_Equality((Object) this.CustomEmotion, (Object) null))
            this.PortraitFront.set_texture((Texture) this.PortraitSet.GetEmotionImage(this.Emotion));
          else
            this.PortraitFront.set_texture((Texture) this.CustomEmotion);
          ((Graphic) this.PortraitBack).set_color(new Color(1f, 1f, 1f, 1f - num));
          ((Component) this.PortraitBack).get_gameObject().SetActive(true);
        }
        else
        {
          this.mCurrentEmotion = this.Emotion;
          ((Graphic) this.PortraitFront).set_color(new Color(1f, 1f, 1f, 1f));
          ((Component) this.PortraitBack).get_gameObject().SetActive(false);
        }
      }
      else
      {
        if (Object.op_Equality((Object) this.CustomEmotion, (Object) null))
          this.PortraitFront.set_texture((Texture) this.PortraitSet.GetEmotionImage(this.mCurrentEmotion));
        else
          this.PortraitFront.set_texture((Texture) this.CustomEmotion);
        this.mPortraitInitialized = true;
      }
    }

    private void Awake()
    {
      EventTelopBubble.Instances.Add(this);
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
      EventTelopBubble.Instances.Remove(this);
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
      EventTelopBubble.Element[] elementArray = EventTelopBubble.SplitTags(bodyText);
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

    private static EventTelopBubble.Element[] SplitTags(string s)
    {
      int index1 = 0;
      List<EventTelopBubble.Element> elementList = new List<EventTelopBubble.Element>();
      while (index1 < s.Length)
      {
        bool flag = false;
        EventTelopBubble.Element element = new EventTelopBubble.Element();
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

    private void Parse(EventTelopBubble.Element[] c, ref int n, string end, EventTelopBubble.Ctx ctx)
    {
      while (n < c.Length)
      {
        if (!string.IsNullOrEmpty(c[n].Tag))
        {
          Match match1;
          if ((match1 = EventTelopBubble.regEndTag.Match(c[n].Tag)).Success)
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
            if ((match2 = EventTelopBubble.regColor.Match(c[n].Tag)).Success)
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

    private void PushCharacters(string s, EventTelopBubble.Ctx ctx)
    {
      float num = this.mNumCharacters <= 0 ? 0.0f : this.mCharacters[this.mNumCharacters - 1].TimeOffset;
      for (int index = 0; index < s.Length; ++index)
      {
        float interval = ctx.Interval;
        if ((int) s[index] == 10)
          interval = this.NewLineInterval;
        this.mCharacters[this.mNumCharacters] = new EventTelopBubble.Character(s[index], ctx.Color, interval, num + interval);
        num = this.mCharacters[this.mNumCharacters].TimeOffset;
        ++this.mNumCharacters;
      }
    }

    private void FlushText()
    {
      string mTextQueue = this.mTextQueue;
      this.mTextQueue = (string) null;
      if (this.mCharacters == null || this.mCharacters.Length < mTextQueue.Length)
        this.mCharacters = new EventTelopBubble.Character[mTextQueue.Length * 2];
      string s = mTextQueue.Replace("<br>", "\n");
      EventAction_Dialog.TextSpeedTypes speed = EventAction_Dialog.TextSpeedTypes.Normal;
      int n = 0;
      EventTelopBubble.Ctx ctx = new EventTelopBubble.Ctx();
      ctx.Interval = speed.ToFloat();
      ctx.Color = Color32.op_Implicit(!Object.op_Inequality((Object) this.BodyText, (Object) null) ? Color.get_black() : ((Graphic) this.BodyText).get_color());
      if (this.TextColor)
        ctx.Color = Color32.op_Implicit(Color.get_white());
      if (this.TextPosition == Event2dAction_Telop.TextPositionTypes.Center)
        this.BodyText.set_alignment((TextAnchor) 4);
      else if (this.TextPosition == Event2dAction_Telop.TextPositionTypes.Right)
        this.BodyText.set_alignment((TextAnchor) 5);
      else
        this.BodyText.set_alignment((TextAnchor) 3);
      this.mNumCharacters = 0;
      this.Parse(EventTelopBubble.SplitTags(s), ref n, (string) null, ctx);
      if (Object.op_Inequality((Object) this.BodyText, (Object) null))
        this.BodyText.set_text(string.Empty);
      this.mStartTime = Time.get_time() + this.FadeInTime;
      this.mTextNeedsUpdate = this.mNumCharacters > 0;
      this.mFadingOut = false;
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
  }
}
