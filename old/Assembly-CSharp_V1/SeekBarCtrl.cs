// Decompiled with JetBrains decompiler
// Type: SeekBarCtrl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeekBarCtrl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
{
  public MediaPlayerCtrl m_srcVideo;
  public Slider m_srcSlider;
  public float m_fDragTime;
  private bool m_bActiveDrag;
  private bool m_bUpdate;
  private float m_fDeltaTime;

  public SeekBarCtrl()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.m_bActiveDrag)
    {
      this.m_fDeltaTime += Time.get_deltaTime();
      if ((double) this.m_fDeltaTime > (double) this.m_fDragTime)
      {
        this.m_bActiveDrag = true;
        this.m_fDeltaTime = 0.0f;
      }
    }
    if (!this.m_bUpdate || !Object.op_Inequality((Object) this.m_srcVideo, (Object) null) || !Object.op_Inequality((Object) this.m_srcSlider, (Object) null))
      return;
    this.m_srcSlider.set_value(this.m_srcVideo.GetSeekBarValue());
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    Debug.Log((object) "OnPointerEnter:");
    this.m_bUpdate = false;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    Debug.Log((object) "OnPointerExit:");
    this.m_bUpdate = true;
  }

  public void OnPointerDown(PointerEventData eventData)
  {
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    this.m_srcVideo.SetSeekBarValue(this.m_srcSlider.get_value());
  }

  public void OnDrag(PointerEventData eventData)
  {
    Debug.Log((object) ("OnDrag:" + (object) eventData));
    if (!this.m_bActiveDrag)
      return;
    this.m_srcVideo.SetSeekBarValue(this.m_srcSlider.get_value());
    this.m_bActiveDrag = false;
  }
}
