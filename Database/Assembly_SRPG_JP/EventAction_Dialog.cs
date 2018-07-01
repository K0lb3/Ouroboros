// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Dialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/表示", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592456, 5592490)]
  public class EventAction_Dialog : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble1";
    private const float DialogPadding = 20f;
    private const string ExtraEmotionDir = "ExtraEmotions/";
    [StringIsActorID]
    public string ActorID;
    [StringIsUnitID]
    public string UnitID;
    [StringIsTextID(true)]
    public string TextID;
    private string mTextData;
    private string mVoiceID;
    private UnitParam mUnit;
    private EventDialogBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;
    [HideInInspector]
    public PortraitSet.EmotionTypes Emotion;
    [HideInInspector]
    [StringIsResourcePath(typeof (Texture2D), "ExtraEmotions/")]
    public string CustomEmotion;
    public EventDialogBubble.Anchors Position;

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      if (strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0)
        return strArray;
      Debug.LogError((object) ("Invalid Voice ID: " + src));
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
      return (IEnumerator) new EventAction_Dialog.\u003CPreloadAssets\u003Ec__Iterator88()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble = EventDialogBubble.Find(this.ActorID);
      if (this.mBubbleResource != null && (Object.op_Equality((Object) this.mBubble, (Object) null) || this.mBubble.Anchor != this.Position))
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubble;
        ((Component) this.mBubble).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).get_gameObject().SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
      this.mBubble.Anchor = this.Position;
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
        for (int index = 0; index < EventDialogBubble.Instances.Count && Object.op_Inequality((Object) EventDialogBubble.Instances[index], (Object) this.mBubble); ++index)
        {
          if (EventDialogBubble.Instances[index].BubbleID == this.ActorID)
            EventDialogBubble.Instances[index].Close();
        }
        ((Component) this.mBubble).get_gameObject().SetActive(true);
      }
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      if (!string.IsNullOrEmpty(this.mVoiceID))
      {
        string[] idPair = EventAction_Dialog.GetIDPair(this.mVoiceID);
        if (idPair != null)
        {
          this.mBubble.VoiceSheetName = idPair[0];
          this.mBubble.VoiceCueName = idPair[1];
        }
      }
      RectTransform transform1 = ((Component) this.mBubble).get_transform() as RectTransform;
      for (int index = 0; index < EventDialogBubble.Instances.Count; ++index)
      {
        RectTransform transform2 = ((Component) EventDialogBubble.Instances[index]).get_transform() as RectTransform;
        if (Object.op_Inequality((Object) transform1, (Object) transform2))
        {
          Rect rect = transform1.get_rect();
          // ISSUE: explicit reference operation
          if (((Rect) @rect).Overlaps(transform2.get_rect()))
            EventDialogBubble.Instances[index].Close();
        }
      }
      this.mBubble.SetName(this.mUnit == null ? "???" : this.mUnit.name);
      this.mBubble.SetBody(this.mTextData);
      if (this.mPortraitResource != null && this.mPortraitResource.isDone)
      {
        if (this.mPortraitResource.asset is PortraitSet)
        {
          this.mBubble.PortraitSet = (PortraitSet) this.mPortraitResource.asset;
          this.mBubble.CustomEmotion = (Texture2D) null;
        }
        else
          this.mBubble.CustomEmotion = (Texture2D) this.mPortraitResource.asset;
      }
      this.mBubble.Emotion = this.Emotion;
      this.mBubble.Open();
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
          this.OnFinish();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public override void GoToEndState()
    {
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble.Close();
      this.enabled = false;
    }

    public override void SkipImmediate()
    {
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble.Close();
      this.OnFinish();
    }

    protected virtual void OnFinish()
    {
      this.ActivateNext();
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(EventAction_Dialog.GetIDPair(this.mVoiceID), false);
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
