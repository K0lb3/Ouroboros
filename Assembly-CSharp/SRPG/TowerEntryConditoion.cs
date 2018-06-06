// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEntryConditoion
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TowerEntryConditoion : MonoBehaviour, IGameParameter
  {
    public TowerEntryConditoion()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void UpdateValue()
    {
      ((Component) this).get_gameObject().SetActive(DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null).GetEntryQuestConditions(true).Count > 2);
    }
  }
}
