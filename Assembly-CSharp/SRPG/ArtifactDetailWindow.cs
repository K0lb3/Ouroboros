// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactDetailWindow : MonoBehaviour, IGameParameter
  {
    public ArtifactList mArtifactList;
    public Button mBackButton;
    public Button mForwardButton;

    public ArtifactDetailWindow()
    {
      base.\u002Ector();
    }

    public void OnBackButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || Object.op_Equality((Object) this.mArtifactList, (Object) null))
        return;
      this.mArtifactList.SelectBack(artifactData);
    }

    public void OnForwardButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || Object.op_Equality((Object) this.mArtifactList, (Object) null))
        return;
      this.mArtifactList.SelectFoward(artifactData);
    }

    public ArtifactData GetArtifactData()
    {
      return DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
    }

    public void UpdateValue()
    {
      this.UpdateBackButtonIntaractable();
      this.UpdateForwardButtonIntaractable();
    }

    private void UpdateBackButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || !Object.op_Inequality((Object) this.mArtifactList, (Object) null) || !Object.op_Inequality((Object) this.mBackButton, (Object) null))
        return;
      ((Selectable) this.mBackButton).set_interactable(!this.mArtifactList.CheckStartOfIndex(artifactData));
    }

    private void UpdateForwardButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || !Object.op_Inequality((Object) this.mArtifactList, (Object) null) || !Object.op_Inequality((Object) this.mForwardButton, (Object) null))
        return;
      ((Selectable) this.mForwardButton).set_interactable(!this.mArtifactList.CheckEndOfIndex(artifactData));
    }
  }
}
