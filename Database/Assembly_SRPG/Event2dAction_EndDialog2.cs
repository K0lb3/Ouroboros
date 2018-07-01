// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndDialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/会話/閉じる(2D)", "表示されている吹き出しを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndDialog2 : EventAction
  {
    public float FadeTime = 0.2f;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();
    [StringIsActorID]
    public string ActorID;
    private EventDialogBubbleCustom mBubble;
    public bool Async;
    private float fadingTime;
    private bool IsFading;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.ActorID))
      {
        for (int index = EventDialogBubbleCustom.Instances.Count - 1; index >= 0; --index)
          EventDialogBubbleCustom.Instances[index].Close();
      }
      else
      {
        this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
        if (Object.op_Inequality((Object) this.mBubble, (Object) null))
          this.mBubble.Close();
      }
      this.fadeInList.Clear();
      this.fadeInParticleList.Clear();
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
              foreach (Component componentsInChild in (GameObjectID[]) ((Component) current).get_gameObject().GetComponentsInChildren<GameObjectID>())
              {
                CanvasGroup component = (CanvasGroup) componentsInChild.GetComponent<CanvasGroup>();
                if (Object.op_Inequality((Object) component, (Object) null) && (double) component.get_alpha() != 1.0)
                  this.fadeInParticleList.Add(component);
              }
            }
          }
        }
      }
      if (!this.IsFading)
      {
        this.ActivateNext();
      }
      else
      {
        this.fadingTime = this.FadeTime;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if (!this.IsFading)
        return;
      this.fadingTime -= Time.get_deltaTime();
      if ((double) this.fadingTime <= 0.0)
      {
        this.fadingTime = 0.0f;
        this.IsFading = false;
        if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      this.FadeIn(this.fadingTime);
    }

    private void FadeIn(float time)
    {
      float num1 = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.get_white(), Color.get_grey(), num1);
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
          Color color2 = Color.op_Multiply(white, color1);
          Color color3 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).get_color();
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) ((Color) @color3).get_maxColorComponent() <= (double) ((Color) @color2).get_maxColorComponent())
          {
            ((Graphic) component.FaceObject.GetComponent<RawImage>()).set_color(color2);
            ((Graphic) component.BodyObject.GetComponent<RawImage>()).set_color(color2);
          }
        }
      }
      float num2 = Mathf.Lerp(1f, 0.0f, num1);
      using (List<CanvasGroup>.Enumerator enumerator = this.fadeInParticleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CanvasGroup current = enumerator.Current;
          if ((double) current.get_alpha() <= (double) num2)
            current.set_alpha(num2);
        }
      }
    }
  }
}
