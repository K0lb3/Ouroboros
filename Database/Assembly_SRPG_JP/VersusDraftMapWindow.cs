// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftMapWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class VersusDraftMapWindow : MonoBehaviour
  {
    public VersusDraftMapWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam quest = instance.FindQuest(instance.VSDraftQuestId);
      if (quest == null)
        return;
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), quest);
    }
  }
}
