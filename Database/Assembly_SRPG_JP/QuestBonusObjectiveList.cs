// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/ボーナス勝利条件")]
  public class QuestBonusObjectiveList : MonoBehaviour
  {
    public GameObject ItemTemplate;

    public QuestBonusObjectiveList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if (DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null) == null)
        ;
    }
  }
}
