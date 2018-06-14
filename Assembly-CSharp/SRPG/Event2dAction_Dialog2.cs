// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/会話/表示2(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog2 : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble2";
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string Emotion = string.Empty;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<GameObject> fadeOutList = new List<GameObject>();
    [HideInInspector]
    public float FadeTime = 0.2f;
    [HideInInspector]
    [SerializeField]
    public string[] IgnoreFadeOut = new string[1];
    private const float DialogPadding = 20f;
    private const float normalScale = 1f;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    public string CharaID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsTextID(false)]
    public string TextID;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    public bool Fade;
    private bool IsFading;
    private UnitParam mUnit;
    private string mPlayerName;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;

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
      return (IEnumerator) new Event2dAction_Dialog2.\u003CPreloadAssets\u003Ec__IteratorAF() { \u003C\u003Ef__this = this };
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
          string[] idPair = Event2dAction_Dialog2.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        ((Component) this.mBubble).get_transform().SetAsLastSibling();
        RectTransform transform1 = ((Component) this.mBubble).get_transform() as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform2 = ((Component) EventDialogBubbleCustom.Instances[index]).get_transform() as RectTransform;
          if (Object.op_Inequality((Object) transform1, (Object) transform2))
          {
            Rect rect = transform1.get_rect();
            // ISSUE: explicit reference operation
            if (((Rect) @rect).Overlaps(transform2.get_rect()))
              EventDialogBubbleCustom.Instances[index].Close();
          }
        }
        if (string.IsNullOrEmpty(this.mPlayerName))
          this.mBubble.SetName(this.mUnit == null ? "???" : this.mUnit.name);
        else
          this.mBubble.SetName(this.mPlayerName);
        this.mBubble.SetBody(this.mTextData);
        this.mBubble.Open();
      }
      this.fadeInList.Clear();
      this.fadeOutList.Clear();
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
                foreach (GameObject standChara in current.StandCharaList)
                {
                  if (Color.op_Inequality(((Graphic) ((EventStandChara2) standChara.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color(), Color.get_white()))
                  {
                    this.fadeInList.Add(standChara);
                    this.IsFading = true;
                  }
                }
                int num = ((Component) this.mBubble).get_transform().GetSiblingIndex() - 1;
                Debug.Log((object) ("set index:" + (object) num));
                ((Component) current).get_transform().SetSiblingIndex(num);
                ((Component) current).get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), 1f));
                if (!string.IsNullOrEmpty(this.Emotion))
                  current.UpdateEmotion(this.Emotion);
              }
              else if (((Behaviour) current).get_isActiveAndEnabled())
              {
                foreach (GameObject standChara in current.StandCharaList)
                {
                  if (Color.op_Inequality(((Graphic) ((EventStandChara2) standChara.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color(), Color.get_gray()))
                  {
                    this.fadeOutList.Add(standChara);
                    this.IsFading = true;
                  }
                }
              }
            }
          }
        }
      }
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    public override void Update()
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
        this.FadeIn(this.FadeTime);
    }

    private void FadeIn(float time)
    {
      Color color1;
      // ISSUE: explicit reference operation
      ((Color) @color1).\u002Ector(Mathf.Lerp((float) Color.get_white().r, (float) Color.get_gray().r, time), Mathf.Lerp((float) Color.get_white().g, (float) Color.get_gray().g, time), Mathf.Lerp((float) Color.get_white().b, (float) Color.get_gray().b, time), 1f);
      Color color2;
      // ISSUE: explicit reference operation
      ((Color) @color2).\u002Ector(Mathf.Lerp((float) Color.get_gray().r, (float) Color.get_white().r, time), Mathf.Lerp((float) Color.get_gray().g, (float) Color.get_white().g, time), Mathf.Lerp((float) Color.get_gray().b, (float) Color.get_white().b, time), 1f);
      using (List<GameObject>.Enumerator enumerator = this.fadeInList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color().r <= color1.r)
          {
            ((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).set_color(color1);
            ((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).BodyObject.GetComponent<RawImage>()).set_color(color1);
          }
        }
      }
      using (List<GameObject>.Enumerator enumerator = this.fadeOutList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color().r >= color2.r)
          {
            ((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).set_color(color2);
            ((Graphic) ((EventStandChara2) current.GetComponent<EventStandChara2>()).BodyObject.GetComponent<RawImage>()).set_color(color2);
          }
        }
      }
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
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
      {
        if (this.mBubble.Finished)
        {
          this.mBubble.Forward();
          this.ActivateNext();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog2.GetIDPair(this.mVoiceID), false);
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
