// Decompiled with JetBrains decompiler
// Type: SGWorldBannerItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
