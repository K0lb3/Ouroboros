// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/会話/表示3(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog3 : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble2";
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string Emotion = string.Empty;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<GameObject> fadeOutList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();
    private List<CanvasGroup> fadeOutParticleList = new List<CanvasGroup>();
    [HideInInspector]
    public float FadeTime = 0.2f;
    [HideInInspector]
    [SerializeField]
    public string[] IgnoreFadeOut = new string[1];
    [HideInInspector]
    public float FrequencyX = 12.51327f;
    [HideInInspector]
    public float FrequencyY = 20.4651f;
    [HideInInspector]
    public float AmplitudeX = 0.1f;
    [HideInInspector]
    public float AmplitudeY = 0.1f;
    private const float DialogPadding = 20f;
    private const float normalScale = 1f;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    public string CharaID;
    [StringIsLocalUnitIDPopup]
    public string UnitID;
    [StringIsTextIDPopup(false)]
    public string TextID;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    private float fadingTime;
    private bool IsFading;
    private bool FoldOut;
    [HideInInspector]
    public bool ActorParticle;
    private UnitParam mUnit;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;
    private RectTransform bubbleTransform;
    private bool FoldOutShake;
    [HideInInspector]
    public float Duration;
    private float mSeedX;
    private float mSeedY;
    private float ShakingTime;
    private bool IsShaking;
    private Vector2 originalPvt;

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      if (strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0)
        return strArray;
      return (string[]) null;
    }

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Dialog3.\u003CPreloadAssets\u003Ec__IteratorA0()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      if (!string.IsNullOrEmpty(this.UnitID))
        this.ActorID = this.UnitID;
      this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
      if (this.mBubbleResource != null && Object.op_Equality((Object) this.mBubble, (Object) null))
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubbleCustom;
        ((Component) this.mBubble).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).get_transform().SetAsLastSibling();
        ((Component) this.mBubble).get_gameObject().SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
      this.mBubble.Anchor = EventDialogBubbleCustom.Anchors.BottomCenter;
    }

    private void LoadTextData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        string[] strArray = LocalizedText.Get(this.TextID).Split('\t');
        this.mTextData = strArray[0];
        this.mVoiceID = strArray.Length <= 1 ? (string) null : strArray[1];
      }
      else
        this.mTextData = this.mVoiceID = (string) null;
    }

    private Vector2 CalcBubblePosition(Vector3 position)
    {
      Vector2 vector2 = Vector2.op_Implicit(Camera.get_main().WorldToScreenPoint(position));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @vector2;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).x = (__Null) ((^local1).x / (double) Screen.get_width());
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @vector2;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local2).y = (__Null) ((^local2).y / (double) Screen.get_height());
      return vector2;
    }

    private bool ContainIgnoreFO(string charID)
    {
      for (int index = 0; index < this.IgnoreFadeOut.Length; ++index)
      {
        if (this.IgnoreFadeOut[index].Equals(charID))
          return true;
      }
      return false;
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && !((Component) this.mBubble).get_gameObject().get_activeInHierarchy())
      {
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count && Object.op_Inequality((Object) EventDialogBubbleCustom.Instances[index], (Object) this.mBubble); ++index)
        {
          if (EventDialogBubbleCustom.Instances[index].BubbleID == this.ActorID)
            EventDialogBubbleCustom.Instances[index].Close();
        }
        ((Component) this.mBubble).get_gameObject().SetActive(true);
      }
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.mVoiceID))
        {
          string[] idPair = Event2dAction_Dialog3.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        ((Component) this.mBubble).get_transform().SetAsLastSibling();
        this.bubbleTransform = ((Component) this.mBubble).get_transform() as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform = ((Component) EventDialogBubbleCustom.Instances[index]).get_transform() as RectTransform;
          if (Object.op_Inequality((Object) this.bubbleTransform, (Object) transform))
          {
            Rect rect = this.bubbleTransform.get_rect();
            // ISSUE: explicit reference operation
            if (((Rect) @rect).Overlaps(transform.get_rect()))
              EventDialogBubbleCustom.Instances[index].Close();
          }
        }
        this.mBubble.SetName(this.mUnit == null ? "???" : this.mUnit.name);
        this.mBubble.SetBody(this.mTextData);
        this.mBubble.Open();
      }
      this.fadeInList.Clear();
      this.fadeOutList.Clear();
      this.fadeInParticleList.Clear();
      this.fadeOutParticleList.Clear();
      this.IsFading = false;
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        using (List<EventStandCharaController2>.Enumerator enumerator = EventStandCharaController2.Instances.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EventStandCharaController2 current = enumerator.Current;
            if (!current.IsClose)
            {
              if (current.CharaID == this.CharaID || this.ContainIgnoreFO(current.CharaID))
              {
                Color white = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(current.CharaID))
                  white = Event2dAction_OperateStandChara.CharaColorDic[current.CharaID];
                foreach (GameObject standChara in current.StandCharaList)
                {
                  if (Color.op_Inequality(((Graphic) ((EventStandChara2) standChara.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color(), white))
                  {
                    this.fadeInList.Add(standChara);
                    this.IsFading = true;
                  }
                }
                if (this.ActorParticle)
                {
                  foreach (Component componentsInChild in (GameObjectID[]) ((Component) current).get_gameObject().GetComponentsInChildren<GameObjectID>())
                  {
                    CanvasGroup canvasGroup = componentsInChild.RequireComponent<CanvasGroup>();
                    if ((double) canvasGroup.get_alpha() != 1.0)
                      this.fadeInParticleList.Add(canvasGroup);
                  }
                }
                int num = ((Component) this.mBubble).get_transform().GetSiblingIndex() - 1;
                ((Component) current).get_transform().SetSiblingIndex(num);
                ((Component) current).get_transform().set_localScale(Vector3.op_Multiply(((Component) current).get_transform().get_localScale(), 1f));
                if (!string.IsNullOrEmpty(this.Emotion))
                  current.UpdateEmotion(this.Emotion);
              }
              else if (((Behaviour) current).get_isActiveAndEnabled())
              {
                Color color = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(current.CharaID))
                  color = Color.op_Multiply(Event2dAction_OperateStandChara.CharaColorDic[current.CharaID], Color.get_gray());
                foreach (GameObject standChara in current.StandCharaList)
                {
                  if (Color.op_Inequality(((Graphic) ((EventStandChara2) standChara.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color(), color))
                  {
                    this.fadeOutList.Add(standChara);
                    this.IsFading = true;
                  }
                }
                if (this.ActorParticle)
                {
                  foreach (Component componentsInChild in (GameObjectID[]) ((Component) current).get_gameObject().GetComponentsInChildren<GameObjectID>())
                  {
                    CanvasGroup canvasGroup = componentsInChild.RequireComponent<CanvasGroup>();
                    if ((double) canvasGroup.get_alpha() != 0.0)
                      this.fadeOutParticleList.Add(canvasGroup);
                  }
                }
              }
            }
          }
        }
      }
      if (this.IsFading)
        this.fadingTime = this.FadeTime;
      this.IsShaking = false;
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && (double) this.Duration > 0.0)
      {
        this.IsShaking = true;
        this.originalPvt = new Vector2(0.5f, 0.0f);
        this.ShakingTime = this.Duration;
        this.mSeedX = Random.get_value();
        this.mSeedY = Random.get_value();
      }
      if (!this.Async)
        return;
      this.ActivateNext(true);
    }

    public override void Update()
    {
      if (this.IsFading)
      {
        this.fadingTime -= Time.get_deltaTime();
        if ((double) this.fadingTime <= 0.0)
        {
          this.fadingTime = 0.0f;
          this.IsFading = false;
        }
        this.FadeIn(this.fadingTime);
      }
      if (this.IsShaking)
      {
        this.ShakingTime -= Time.get_deltaTime();
        if ((double) this.ShakingTime <= 0.0)
        {
          this.ShakingTime = 0.0f;
          this.IsShaking = false;
        }
        this.Shake(this.ShakingTime);
      }
      if (!this.Async || this.IsFading || this.IsShaking)
        return;
      this.enabled = false;
    }

    private void FadeIn(float time)
    {
      float num1 = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.get_white(), Color.get_grey(), num1);
      Color color2 = Color.Lerp(Color.get_grey(), Color.get_white(), num1);
      using (List<GameObject>.Enumerator enumerator = this.fadeInList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          EventStandChara2 component = (EventStandChara2) current.GetComponent<EventStandChara2>();
          string charaId = ((EventStandCharaController2) current.GetComponentInParent<EventStandCharaController2>()).CharaID;
          Color white = Color.get_white();
          if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
            white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
          Color color3 = Color.op_Multiply(white, color1);
          Color color4 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).get_color();
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) ((Color) @color4).get_maxColorComponent() <= (double) ((Color) @color3).get_maxColorComponent())
          {
            ((Graphic) component.FaceObject.GetComponent<RawImage>()).set_color(color3);
            ((Graphic) component.BodyObject.GetComponent<RawImage>()).set_color(color3);
          }
        }
      }
      using (List<GameObject>.Enumerator enumerator = this.fadeOutList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          EventStandChara2 component = (EventStandChara2) current.GetComponent<EventStandChara2>();
          string charaId = ((EventStandCharaController2) current.GetComponentInParent<EventStandCharaController2>()).CharaID;
          Color white = Color.get_white();
          if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
            white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
          Color color3 = Color.op_Multiply(white, color2);
          Color color4 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).get_color();
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) ((Color) @color4).get_maxColorComponent() >= (double) ((Color) @color3).get_maxColorComponent())
          {
            ((Graphic) component.FaceObject.GetComponent<RawImage>()).set_color(color3);
            ((Graphic) component.BodyObject.GetComponent<RawImage>()).set_color(color3);
          }
        }
      }
      float num2 = Mathf.Lerp(1f, 0.0f, num1);
      float num3 = Mathf.Lerp(0.0f, 1f, num1);
      using (List<CanvasGroup>.Enumerator enumerator = this.fadeInParticleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CanvasGroup current = enumerator.Current;
          if ((double) current.get_alpha() <= (double) num2)
            current.set_alpha(num2);
        }
      }
      using (List<CanvasGroup>.Enumerator enumerator = this.fadeOutParticleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CanvasGroup current = enumerator.Current;
          if ((double) current.get_alpha() >= (double) num3)
            current.set_alpha(num3);
        }
      }
    }

    private void Shake(float time)
    {
      if ((double) time > 0.0)
      {
        float num1 = Mathf.Clamp01(time / this.Duration);
        float num2 = Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num1;
        float num3 = Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num1;
        Vector2 vector2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2).\u002Ector((float) this.originalPvt.x + num2, (float) this.originalPvt.y + num3);
        this.bubbleTransform.set_pivot(vector2);
      }
      else
        this.bubbleTransform.set_pivot(this.originalPvt);
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = (TacticsUnitController) actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          Unit unit = component.Unit;
          if (unit != null)
            return unit.UnitName;
        }
      }
      return actorID;
    }

    public override bool Forward()
    {
      if (this.Async || !Object.op_Inequality((Object) this.mBubble, (Object) null))
        return false;
      if (this.mBubble.Finished)
      {
        if (this.IsFading)
          this.FadeIn(0.0f);
        if (this.IsShaking)
          this.Shake(0.0f);
        this.mBubble.StopVoice();
        this.mBubble.Forward();
        this.ActivateNext();
        return true;
      }
      if (this.mBubble.IsPrinting)
        this.mBubble.Skip();
      return false;
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog3.GetIDPair(this.mVoiceID), false);
      }
      return (string[]) null;
    }

    public enum TextSpeedTypes
    {
      Normal,
      Slow,
      Fast,
    }
  }
}
