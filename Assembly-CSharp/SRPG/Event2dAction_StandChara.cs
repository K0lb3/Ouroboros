// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵2/配置(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    private string DummyID = "dummyID";
    public string CharaID;
    public bool Flip;
    public EventStandCharaController2.PositionTypes Position;
    public GameObject StandTemplate;
    public string Emotion;
    private GameObject mStandObject;
    private EventStandChara2 mEventStandChara;
    private EventStandCharaController2 mEVCharaController;
    private LoadRequest mStandCharaResource;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__Iterator7B assetsCIterator7B = new Event2dAction_StandChara.\u003CPreloadAssets\u003Ec__Iterator7B();
      return (IEnumerator) assetsCIterator7B;
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandObject, (Object) null))
        return;
      string id = this.DummyID;
      if (!string.IsNullOrEmpty(this.CharaID))
        id = this.CharaID;
      if (Object.op_Inequality((Object) EventStandCharaController2.FindInstances(id), (Object) null))
      {
        this.mEVCharaController = EventStandCharaController2.FindInstances(id);
        this.mStandObject = ((Component) this.mEVCharaController).get_gameObject();
      }
      if (!Object.op_Equality((Object) this.mStandObject, (Object) null) || !Object.op_Inequality((Object) this.StandTemplate, (Object) null))
        return;
      this.mStandObject = (GameObject) Object.Instantiate<GameObject>((M0) this.StandTemplate);
      RectTransform component = (RectTransform) this.mStandObject.GetComponent<RectTransform>();
      this.mEVCharaController = (EventStandCharaController2) this.mStandObject.GetComponent<EventStandCharaController2>();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
      RectTransform rectTransform = component;
      Vector2 vector2_2 = vector2_1;
      component.set_anchorMax(vector2_2);
      Vector2 vector2_3 = vector2_2;
      rectTransform.set_anchorMin(vector2_3);
      this.mStandObject.get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      this.mStandObject.get_transform().SetAsLastSibling();
      this.mEVCharaController.CharaID = this.CharaID;
      this.mStandObject.get_gameObject().SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mStandObject, (Object) null) && !this.mStandObject.get_gameObject().get_activeInHierarchy())
        this.mStandObject.get_gameObject().SetActive(true);
      if (!Object.op_Inequality((Object) this.mStandObject, (Object) null))
        return;
      this.mEVCharaController.Emotion = this.Emotion;
      RectTransform component = (RectTransform) this.mStandObject.GetComponent<RectTransform>();
      if (Object.op_Equality((Object) ((Component) component).get_transform().get_parent(), (Object) null))
      {
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector(this.mEVCharaController.GetAnchorPostionX((int) this.Position), 0.0f);
        RectTransform rectTransform = component;
        Vector2 vector2_2 = vector2_1;
        component.set_anchorMax(vector2_2);
        Vector2 vector2_3 = vector2_2;
        rectTransform.set_anchorMin(vector2_3);
        Vector3 vector3;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(1f, 1f, 1f);
        ((Transform) component).set_localScale(vector3);
        this.mStandObject.get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
        this.mStandObject.get_transform().SetAsLastSibling();
      }
      if (this.Flip)
        ((Transform) component).Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mEVCharaController.Open(0.5f);
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mStandObject, (Object) null))
        return;
      Object.Destroy((Object) this.mStandObject.get_gameObject());
    }

    public override void Update()
    {
      if (!this.enabled)
        return;
      if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeIn)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Active;
        this.ActivateNext();
      }
      else if (!this.mEVCharaController.Fading && this.mEVCharaController.State == EventStandCharaController2.StateTypes.FadeOut)
      {
        this.mEVCharaController.State = EventStandCharaController2.StateTypes.Inactive;
        this.enabled = false;
        ((Component) this.mEVCharaController).get_gameObject().SetActive(false);
        ((Component) this.mEVCharaController).get_transform().set_parent((Transform) null);
      }
      else
      {
        if (this.mEVCharaController.Fading)
          return;
        this.ActivateNext();
      }
    }
  }
}
