// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/表示(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble2";
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string Emotion = string.Empty;
    private const float DialogPadding = 20f;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    public string CharaID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsTextID(false)]
    public string TextID;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
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
      return (IEnumerator) new Event2dAction_Dialog.\u003CPreloadAssets\u003Ec__IteratorAE() { \u003C\u003Ef__this = this };
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
          string[] idPair = Event2dAction_Dialog.GetIDPair(this.mVoiceID);
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
        if (!GlobalVars.IsTutorialEnd)
          AnalyticsManager.TrackTutorialAnalyticsEvent(this.TextID);
        this.mBubble.Open();
      }
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        using (List<EventStandCharaController2>.Enumerator enumerator = EventStandCharaController2.Instances.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EventStandCharaController2 current = enumerator.Current;
            if (current.CharaID == this.CharaID)
            {
              if (!current.IsClose)
              {
                ((Component) current).get_transform().SetSiblingIndex(((Component) this.mBubble).get_transform().GetSiblingIndex() - 1);
                if (!string.IsNullOrEmpty(this.Emotion))
                  current.UpdateEmotion(this.Emotion);
              }
            }
            else if (!current.IsClose)
              ;
          }
        }
      }
      if (!this.Async)
        return;
      this.ActivateNext();
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
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog.GetIDPair(this.mVoiceID), false);
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
