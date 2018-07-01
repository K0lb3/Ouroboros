// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandCharaController2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class EventStandCharaController2 : MonoBehaviour
  {
    public static List<EventStandCharaController2> Instances = new List<EventStandCharaController2>();
    private const float FADEIN = 0.5f;
    private const float FADEOUT = 0.3f;
    public GameObject[] StandCharaList;
    public string CharaID;
    private float[] AnchorPostionX;
    private int mCurrentIndex;
    private bool IsFading;
    private float mFadeTime;
    private string mEmotion;
    private bool mClose;
    private EventStandCharaController2.StateTypes mState;

    public EventStandCharaController2()
    {
      base.\u002Ector();
    }

    public static EventStandCharaController2 FindInstances(string id)
    {
      for (int index = EventStandCharaController2.Instances.Count - 1; index >= 0; --index)
      {
        if (EventStandCharaController2.Instances[index].CharaID == id)
          return EventStandCharaController2.Instances[index];
      }
      return (EventStandCharaController2) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventStandCharaController2.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventStandCharaController2.Instances[index]).get_gameObject().get_activeInHierarchy())
          Object.Destroy((Object) ((Component) EventStandCharaController2.Instances[index]).get_gameObject());
      }
      EventStandCharaController2.Instances.Clear();
    }

    public float GetAnchorPostionX(int index)
    {
      if (index >= 0 && index < this.AnchorPostionX.Length)
        return this.AnchorPostionX[index];
      return 0.0f;
    }

    private GameObject Find(string name)
    {
      if (this.StandCharaList != null)
      {
        for (int index = this.StandCharaList.Length - 1; index >= 0; --index)
        {
          if (((Object) this.StandCharaList[index]).get_name() == name)
            return this.StandCharaList[index];
        }
      }
      return (GameObject) null;
    }

    private int FindIndex(string name)
    {
      if (this.StandCharaList != null)
      {
        for (int index = this.StandCharaList.Length - 1; index >= 0; --index)
        {
          if (((Object) this.StandCharaList[index]).get_name() == name)
            return index;
        }
      }
      return -1;
    }

    public bool Fading
    {
      get
      {
        return this.IsFading;
      }
    }

    public string Emotion
    {
      get
      {
        return this.mEmotion;
      }
      set
      {
        this.mEmotion = value;
      }
    }

    private void Awake()
    {
      EventStandCharaController2.Instances.Add(this);
      foreach (GameObject standChara in this.StandCharaList)
        standChara.SetActive(false);
      this.mCurrentIndex = 0;
      this.mEmotion = "normal";
    }

    private void OnDestroy()
    {
      EventStandCharaController2.Instances.Remove(this);
      foreach (GameObject standChara in this.StandCharaList)
        standChara.SetActive(false);
      this.mState = EventStandCharaController2.StateTypes.Inactive;
      this.mCurrentIndex = 0;
      this.mEmotion = "normal";
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (!this.IsFading)
        return;
      this.mFadeTime -= Time.get_deltaTime();
      if ((double) this.mFadeTime <= 0.0)
      {
        this.mFadeTime = 0.0f;
        this.IsFading = false;
      }
      else if (this.mState == EventStandCharaController2.StateTypes.FadeIn)
      {
        this.FadeIn(this.mFadeTime);
      }
      else
      {
        if (this.mState != EventStandCharaController2.StateTypes.FadeOut)
          return;
        this.FadeOut(this.mFadeTime);
      }
    }

    public bool IsClose
    {
      get
      {
        return this.mClose;
      }
    }

    public void Open(float fade = 0.5f)
    {
      if (!this.mClose)
        return;
      this.mClose = false;
      this.StandCharaList[this.mCurrentIndex].SetActive(false);
      int index = this.FindIndex(this.mEmotion);
      this.mCurrentIndex = index == -1 ? 0 : index;
      this.StandCharaList[this.mCurrentIndex].SetActive(true);
      this.StartFadeIn(fade);
    }

    public void Open(string name)
    {
      this.ChangeStandChara(name);
    }

    public void Close(float fade = 0.3f)
    {
      if (this.mClose)
        return;
      this.mClose = true;
      this.StartFadeOut(fade);
    }

    private void StartFadeIn(float fade)
    {
      this.IsFading = true;
      this.mFadeTime = fade;
      this.mState = EventStandCharaController2.StateTypes.FadeIn;
      if ((double) this.mFadeTime > 0.0)
        return;
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_alpha(1f);
    }

    private void StartFadeOut(float fade)
    {
      this.IsFading = true;
      this.mFadeTime = fade;
      this.mState = EventStandCharaController2.StateTypes.FadeOut;
      if ((double) this.mFadeTime > 0.0)
        return;
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_alpha(0.0f);
    }

    public EventStandCharaController2.StateTypes State
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

    public void UpdateEmotion(string name)
    {
      this.ChangeStandChara(name);
    }

    private void ChangeStandChara(string name)
    {
      int index = this.FindIndex(name);
      if (index == -1)
        return;
      this.StandCharaList[this.mCurrentIndex].SetActive(false);
      this.mCurrentIndex = index;
      this.StandCharaList[this.mCurrentIndex].SetActive(true);
    }

    private void FadeIn(float time)
    {
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_alpha(Mathf.Lerp(1f, 0.0f, time));
    }

    private void FadeOut(float time)
    {
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_alpha(Mathf.Lerp(0.0f, 1f, time));
    }

    public enum PositionTypes
    {
      OverLeft,
      Left,
      HLeft,
      Center,
      HRight,
      Right,
      OverRight,
      None,
    }

    public enum StateTypes
    {
      FadeIn,
      Active,
      FadeOut,
      Inactive,
      None,
    }
  }
}
