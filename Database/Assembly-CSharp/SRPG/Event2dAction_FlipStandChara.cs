// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FlipStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/反転(2D)", "立ち絵2を反転します", 5592405, 4473992)]
  public class Event2dAction_FlipStandChara : EventAction
  {
    public static List<EventStandCharaController2> InstancesForFlip = new List<EventStandCharaController2>();
    public float Time = 1f;
    private List<RawImage> fadeInList = new List<RawImage>();
    private List<RawImage> fadeOutList = new List<RawImage>();
    public string CharaID;
    public bool async;
    private GameObject mStandObjectFlip;
    private EventStandCharaController2 mEVCharaController;
    private EventStandCharaController2 mEVCharaFlipController;
    private float offset;
    private Color InColor;
    private Color OutColor;

    public override void PreStart()
    {
      if (string.IsNullOrEmpty(this.CharaID))
        return;
      this.mEVCharaController = EventStandCharaController2.FindInstances(this.CharaID);
      string str = this.CharaID + "_Flip";
      for (int index = 0; index < Event2dAction_FlipStandChara.InstancesForFlip.Count; ++index)
      {
        if (Event2dAction_FlipStandChara.InstancesForFlip[index].CharaID == str)
        {
          this.mEVCharaFlipController = Event2dAction_FlipStandChara.InstancesForFlip[index];
          this.mStandObjectFlip = ((Component) this.mEVCharaFlipController).get_gameObject();
          break;
        }
      }
      if (!Object.op_Equality((Object) this.mEVCharaFlipController, (Object) null) || !Object.op_Inequality((Object) this.mEVCharaController, (Object) null))
        return;
      this.mStandObjectFlip = (GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.mEVCharaController).get_gameObject());
      this.mEVCharaFlipController = (EventStandCharaController2) this.mStandObjectFlip.GetComponent<EventStandCharaController2>();
      this.mEVCharaFlipController.CharaID = str;
      Event2dAction_FlipStandChara.InstancesForFlip.Add(this.mEVCharaFlipController);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mStandObjectFlip, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        if (!this.mStandObjectFlip.get_gameObject().get_activeInHierarchy())
          this.mStandObjectFlip.get_gameObject().SetActive(true);
        this.mEVCharaFlipController.Emotion = this.mEVCharaController.Emotion;
        RectTransform transform1 = ((Component) this.mEVCharaController).get_gameObject().get_transform() as RectTransform;
        RectTransform transform2 = this.mStandObjectFlip.get_transform() as RectTransform;
        ((Transform) transform2).SetParent(((Transform) transform1).get_parent());
        ((Transform) transform2).set_localScale(((Transform) transform1).get_localScale());
        transform2.set_anchorMax(transform1.get_anchorMax());
        transform2.set_anchorMin(transform1.get_anchorMin());
        transform2.set_anchoredPosition(transform1.get_anchoredPosition());
        ((Transform) transform2).set_localRotation(Quaternion.op_Multiply(((Transform) transform1).get_localRotation(), Quaternion.Euler(0.0f, 180f, 0.0f)));
        this.mEVCharaFlipController.Open(0.0f);
        for (int index = 0; index < this.mEVCharaController.StandCharaList.Length; ++index)
        {
          GameObject bodyObject = ((EventStandChara2) this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>()).BodyObject;
          if (Object.op_Inequality((Object) bodyObject, (Object) null))
            this.fadeOutList.Add((RawImage) bodyObject.GetComponent<RawImage>());
          GameObject faceObject = ((EventStandChara2) this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>()).FaceObject;
          if (Object.op_Inequality((Object) faceObject, (Object) null))
            this.fadeOutList.Add((RawImage) faceObject.GetComponent<RawImage>());
        }
        for (int index = 0; index < this.mEVCharaFlipController.StandCharaList.Length; ++index)
        {
          GameObject bodyObject = ((EventStandChara2) this.mEVCharaFlipController.StandCharaList[index].GetComponent<EventStandChara2>()).BodyObject;
          if (Object.op_Inequality((Object) bodyObject, (Object) null))
            this.fadeInList.Add((RawImage) bodyObject.GetComponent<RawImage>());
          GameObject faceObject = ((EventStandChara2) this.mEVCharaFlipController.StandCharaList[index].GetComponent<EventStandChara2>()).FaceObject;
          if (Object.op_Inequality((Object) faceObject, (Object) null))
            this.fadeInList.Add((RawImage) faceObject.GetComponent<RawImage>());
        }
        for (int index = 0; index < this.fadeOutList.Count; ++index)
        {
          if (((Behaviour) this.fadeOutList[index]).get_isActiveAndEnabled())
          {
            this.InColor = ((Graphic) this.fadeOutList[index]).get_color();
            break;
          }
        }
        this.OutColor = this.InColor;
        this.OutColor.a = (__Null) 0.0;
        for (int index = 0; index < this.fadeInList.Count; ++index)
          ((Graphic) this.fadeInList[index]).set_color(this.InColor);
        this.offset = (double) this.Time > 0.0 ? 0.0f : 1f;
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if ((double) this.offset >= 1.0)
      {
        this.mEVCharaFlipController.Close(0.0f);
        ((Component) this.mEVCharaFlipController).get_gameObject().SetActive(false);
        ((Transform) ((Component) this.mEVCharaController).get_gameObject().GetComponent<RectTransform>()).Rotate(new Vector3(0.0f, 180f, 0.0f));
        for (int index = 0; index < this.fadeOutList.Count; ++index)
          ((Graphic) this.fadeOutList[index]).set_color(this.InColor);
        if (this.async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        Color color1 = Color.Lerp(this.OutColor, this.InColor, this.offset);
        for (int index = 0; index < this.fadeInList.Count; ++index)
          ((Graphic) this.fadeInList[index]).set_color(color1);
        Color color2 = Color.Lerp(this.InColor, this.OutColor, this.offset);
        for (int index = 0; index < this.fadeOutList.Count; ++index)
          ((Graphic) this.fadeOutList[index]).set_color(color2);
        this.offset += UnityEngine.Time.get_deltaTime() / this.Time;
        this.offset = Mathf.Clamp01(this.offset);
      }
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mStandObjectFlip, (Object) null))
        return;
      Object.Destroy((Object) this.mStandObjectFlip.get_gameObject());
    }
  }
}
