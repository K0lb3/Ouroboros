// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Event2dFade : MonoBehaviour
  {
    public RawImage mImage;
    private Color mCurrentColor;
    private Color mStartColor;
    private Color mEndColor;
    private float mCurrentTime;
    private float mDuration;
    private bool mInitialized;

    public Event2dFade()
    {
      base.\u002Ector();
    }

    private static Event2dFade Instance { get; set; }

    public static Event2dFade Find()
    {
      return Event2dFade.Instance;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) null, (Object) Event2dFade.Instance))
        Object.Destroy((Object) this);
      Event2dFade.Instance = this;
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) this, (Object) Event2dFade.Instance))
        return;
      Event2dFade.Instance = (Event2dFade) null;
    }

    public bool IsFading
    {
      get
      {
        return (double) this.mCurrentTime < (double) this.mDuration;
      }
    }

    public void FadeTo(Color dest, float time)
    {
      if (!this.mInitialized)
      {
        this.mCurrentColor = dest;
        this.mCurrentColor.a = (__Null) (1.0 - this.mCurrentColor.a);
        this.mInitialized = true;
        ((Graphic) this.mImage).set_color(this.mCurrentColor);
      }
      if ((double) time > 0.0)
      {
        this.mStartColor = this.mCurrentColor;
        this.mEndColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = time;
        ((Component) this).get_gameObject().SetActive(true);
      }
      else
      {
        this.mCurrentColor = dest;
        this.mCurrentTime = 0.0f;
        this.mDuration = 0.0f;
        ((Graphic) this.mImage).set_color(this.mCurrentColor);
        ((Component) this).get_gameObject().SetActive(this.mCurrentColor.a > 0.0);
      }
    }

    private void Update()
    {
      if ((double) this.mCurrentTime >= (double) this.mDuration)
      {
        if (this.mCurrentColor.a > 0.0 || !((Component) this).get_gameObject().GetActive())
          return;
        ((Component) this).get_gameObject().SetActive(false);
      }
      else
      {
        this.mCurrentTime += Time.get_unscaledDeltaTime();
        this.mCurrentColor = Color.Lerp(this.mStartColor, this.mEndColor, Mathf.Clamp01(this.mCurrentTime / this.mDuration));
        ((Graphic) this.mImage).set_color(this.mCurrentColor);
      }
    }
  }
}
