// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnStandchara
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("立ち絵/配置(2D)", "立ち絵を配置します", 5592405, 4473992)]
  public class Event2dAction_SpawnStandchara : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/EventStandChara";
    public string CharaID;
    public bool Flip;
    public EventStandChara.PositionTypes Position;
    [HideInInspector]
    public Texture2D StandcharaImage;
    [HideInInspector]
    public EventStandChara mStandChara;
    [HideInInspector]
    public IEnumerator mEnumerator;
    private LoadRequest mStandCharaResource;

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
      return (IEnumerator) new Event2dAction_SpawnStandchara.\u003CPreloadAssets\u003Ec__Iterator7A() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = Object.Instantiate(this.mStandCharaResource.asset) as EventStandChara;
      RectTransform component = (RectTransform) ((Component) this.mStandChara).GetComponent<RectTransform>();
      this.mStandChara.InitPositionX(((Component) this.ActiveCanvas).get_transform() as RectTransform);
      Vector3 vector3;
      // ISSUE: explicit reference operation
      ((Vector3) @vector3).\u002Ector(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
      ((Transform) component).set_position(vector3);
      ((Component) this.mStandChara).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) this.mStandChara).get_transform().SetAsFirstSibling();
      this.mStandChara.CharaID = this.CharaID;
      ((Component) this.mStandChara).get_gameObject().SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mStandChara, (Object) null) && !((Component) this.mStandChara).get_gameObject().get_activeInHierarchy())
        ((Component) this.mStandChara).get_gameObject().SetActive(true);
      if (!Object.op_Inequality((Object) this.mStandChara, (Object) null))
        return;
      ((Graphic) ((Component) this.mStandChara).get_gameObject().GetComponent<RawImage>()).set_color(new Color((float) Color.get_gray().r, (float) Color.get_gray().g, (float) Color.get_gray().b, 0.0f));
      ((RawImage) ((Component) this.mStandChara).get_gameObject().GetComponent<RawImage>()).set_texture((Texture) this.StandcharaImage);
      RectTransform component = (RectTransform) ((Component) this.mStandChara).GetComponent<RectTransform>();
      if (Object.op_Equality((Object) ((Component) this.mStandChara).get_transform().get_parent(), (Object) null))
      {
        Vector3 vector3_1;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3_1).\u002Ector(this.mStandChara.GetPositionX((int) this.Position), 0.0f, 0.0f);
        ((Transform) component).set_position(vector3_1);
        Vector3 vector3_2;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3_2).\u002Ector(1f, 1f, 1f);
        ((Transform) component).set_localScale(vector3_2);
        ((Component) this.mStandChara).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
        ((Component) this.mStandChara).get_transform().SetAsFirstSibling();
        ((Component) this.mStandChara).get_transform().SetSiblingIndex(((Component) EventDialogBubbleCustom.FindHead()).get_transform().GetSiblingIndex() - 1);
      }
      if (this.Flip)
        ((Transform) component).Rotate(new Vector3(0.0f, 180f, 0.0f));
      this.mStandChara.Open(0.3f);
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mStandChara, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mStandChara).get_gameObject());
    }

    public override void Update()
    {
      if (!this.enabled)
        return;
      if (!this.mStandChara.Fading && this.mStandChara.State == EventStandChara.StateTypes.FadeIn)
      {
        this.mStandChara.State = EventStandChara.StateTypes.Active;
        this.ActivateNext(true);
      }
      else
      {
        if (this.mStandChara.Fading || this.mStandChara.State != EventStandChara.StateTypes.FadeOut)
          return;
        this.mStandChara.State = EventStandChara.StateTypes.Inactive;
        this.enabled = false;
        ((Component) this.mStandChara).get_gameObject().SetActive(false);
      }
    }

    public enum StateTypes
    {
      Initialized,
      StartFadeIn,
      FadeIn,
      EndFadeIn,
      StartFadeOut,
      FadeOut,
      EndFadeOut,
      Active,
      Inactive,
    }
  }
}
