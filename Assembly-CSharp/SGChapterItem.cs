// Decompiled with JetBrains decompiler
// Type: SGChapterItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGChapterItem : MonoBehaviour
{
  [SerializeField]
  private ImageArray[] stars;
  [SerializeField]
  private Text progressText;

  public SGChapterItem()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void Update()
  {
  }

  public void SetProgress(int total, int completed)
  {
    this.progressText.set_text(completed.ToString() + "/" + (object) total);
    int num = (int) ((double) ((float) completed / (float) total) / 0.330000013113022);
    for (int index = 0; index < num; ++index)
      this.stars[index].ImageIndex = 1;
  }
}
