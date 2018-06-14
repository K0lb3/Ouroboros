// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjectiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
