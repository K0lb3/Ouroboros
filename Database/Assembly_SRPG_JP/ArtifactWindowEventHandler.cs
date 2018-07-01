// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindowEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactWindowEventHandler : MonoBehaviour, IGameParameter
  {
    public ArtifactList mArtifactList;
    public Button mBackButton;
    public Button mForwardButton;

    public ArtifactWindowEventHandler()
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
