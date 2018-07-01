// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockCharacterQuestPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnlockCharacterQuestPopup : MonoBehaviour
  {
    private UnitData mCurrentUnit;
    public Text EpisodeTitle;
    public Text EpisodeNumber;

    public UnlockCharacterQuestPopup()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Setup(UnitData unit, int episodeNumber, string episodeTitle)
    {
      this.mCurrentUnit = unit;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mCurrentUnit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
