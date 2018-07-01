// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventStandChara : MonoBehaviour
  {
    public static List<EventStandChara> Instances = new List<EventStandChara>();
    public const float FADEIN_TIME = 0.3f;
    public const float FADEOUT_TIME = 0.5f;
    public string CharaID;
    public bool mClose;
    private float[] PositionX;
    private float FadeTime;
    private bool IsFading;
    private EventStandChara.StateTypes mState;

    public EventStandChara()
    {
      base.\u002Ector();
    }

    public bool Fading
    {
      get
      {
        return this.IsFading;
      }
    }

    public EventStandChara.StateTypes State
    {
      get
      {
        return this.mState;
      }
      set
      {
        this.mState = value;
      }
    }

    public static EventStandChara Find(string id)
    {
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
      {
        if (EventStandChara.Instances[index].CharaID == id)
          return EventStandChara.Instances[index];
      }
      return (EventStandChara) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventStandChara.Instances[index]).get_gameObject().get_activeInHierarchy())
          Object.Destroy((Object) ((Component) EventStandChara.Instances[index]).get_gameObject());
      }
      EventStandChara.Instances.Clear();
    }

    public static string[] GetCharaIDs()
    {
      List<string> stringList = new List<string>();
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
        stringList.Add(EventStandChara.Instances[index].CharaID);
      return stringList.ToArray();
    }

    private void Awake()
    {
      EventStandChara.Instances.Add(this);
      this.FadeTime = 0.0f;
      this.IsFading = false;
    }

    private void OnDestroy()
    {
      EventStandChara.Instances.Remove(this);
      this.mState = EventStandChara.StateTypes.Inactive;
    }

    public void Open(float fade = 0.3f)
    {
      this.mClose = false;
      this.StartFadeIn(fade);
    }

    public void Close(float fade = 0.5f)
    {
      this.mClose = true;
      this.StartFadeOut(fade);
    }

    public void StartFadeIn(float fade)
    {
      this.IsFading = true;
      this.FadeTime = fade;
      this.mState = EventStandChara.StateTypes.FadeIn;
      if ((double) this.FadeTime > 0.0)
        return;
      Color color = ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).get_color();
      ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).set_color(new Color((float) color.r, (float) color.g, (float) color.b, 1f));
    }

    public void StartFadeOut(float fade)
    {
      this.IsFading = true;
      this.FadeTime = fade;
      this.mState = EventStandChara.StateTypes.FadeOut;
      if ((double) this.FadeTime > 0.0)
        return;
      Color color = ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).get_color();
      ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).set_color(new Color((float) color.r, (float) color.g, (float) color.b, 0.0f));
    }

    private void Update()
    {
      if (!this.IsFading)
        return;
      this.FadeTime -= Time.get_deltaTime();
      if ((double) this.FadeTime <= 0.0)
      {
        this.FadeTime = 0.0f;
        this.IsFading = false;
      }
      else
      {
        if (this.mState == EventStandChara.StateTypes.FadeIn)
          this.FadeIn(this.FadeTime);
        if (this.mState != EventStandChara.StateTypes.FadeOut)
          return;
        this.FadeOut(this.FadeTime);
      }
    }

    private void FadeIn(float time)
    {
      Color color = ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).get_color();
      ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).set_color(new Color((float) color.r, (float) color.g, (float) color.b, Mathf.Lerp(1f, 0.0f, time)));
    }

    private void FadeOut(float time)
    {
      Color color = ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).get_color();
      ((Graphic) ((Component) this).get_gameObject().GetComponent<RawImage>()).set_color(new Color((float) color.r, (float) color.g, (float) color.b, Mathf.Lerp(0.0f, 1f, time)));
    }

    public void InitPositionX(RectTransform canvasRect)
    {
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      float[] numArray = new float[5];
      int index1 = 0;
      Rect rect1 = component.get_rect();
      // ISSUE: explicit reference operation
      double num1 = (double) ((Rect) @rect1).get_width() / 2.0;
      numArray[index1] = (float) num1;
      int index2 = 1;
      Rect rect2 = canvasRect.get_rect();
      // ISSUE: explicit reference operation
      double num2 = (double) ((Rect) @rect2).get_width() / 2.0;
      numArray[index2] = (float) num2;
      int index3 = 2;
      Rect rect3 = canvasRect.get_rect();
      // ISSUE: explicit reference operation
      double width1 = (double) ((Rect) @rect3).get_width();
      Rect rect4 = component.get_rect();
      // ISSUE: explicit reference operation
      double num3 = (double) ((Rect) @rect4).get_width() / 2.0;
      double num4 = width1 - num3;
      numArray[index3] = (float) num4;
      int index4 = 3;
      Rect rect5 = component.get_rect();
      // ISSUE: explicit reference operation
      double num5 = -(double) ((Rect) @rect5).get_width() / 2.0;
      numArray[index4] = (float) num5;
      int index5 = 4;
      Rect rect6 = canvasRect.get_rect();
      // ISSUE: explicit reference operation
      double width2 = (double) ((Rect) @rect6).get_width();
      Rect rect7 = component.get_rect();
      // ISSUE: explicit reference operation
      double num6 = (double) ((Rect) @rect7).get_width() / 2.0;
      double num7 = width2 + num6;
      numArray[index5] = (float) num7;
      this.PositionX = numArray;
    }

    public float GetPositionX(int index)
    {
      if (index >= 0 && index < this.PositionX.Length)
        return this.PositionX[index];
      return 0.0f;
    }

    public enum PositionTypes
    {
      Left,
      Center,
      Right,
      OverLeft,
      OverRight,
    }

    public enum StateTypes
    {
      FadeIn,
      Active,
      FadeOut,
      Inactive,
    }
  }
}
