// Decompiled with JetBrains decompiler
// Type: SGWorldBannerItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGWorldBannerItem : MonoBehaviour
{
  [SerializeField]
  private Text SectionText;
  [SerializeField]
  private Text ChapterText;

  public SGWorldBannerItem()
  {
    base.\u002Ector();
  }

  public void SetChapterText(string text)
  {
    this.ChapterText.set_text(text);
  }
}
