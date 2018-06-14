// Decompiled with JetBrains decompiler
// Type: MediaPlayerFullScreenCtrl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class MediaPlayerFullScreenCtrl : MonoBehaviour
{
  public GameObject m_objVideo;
  private int m_iOrgWidth;
  private int m_iOrgHeight;

  public MediaPlayerFullScreenCtrl()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.Resize();
  }

  private void Update()
  {
    if (this.m_iOrgWidth != Screen.get_width())
      this.Resize();
    if (this.m_iOrgHeight == Screen.get_height())
      return;
    this.Resize();
  }

  private void Resize()
  {
    this.m_iOrgWidth = Screen.get_width();
    this.m_iOrgHeight = Screen.get_height();
    float num = (float) this.m_iOrgHeight / (float) this.m_iOrgWidth;
    this.m_objVideo.get_transform().set_localScale(new Vector3(20f / num, 20f / num, 1f));
    ((MediaPlayerCtrl) ((Component) this.m_objVideo.get_transform()).GetComponent<MediaPlayerCtrl>()).Resize();
  }
}
