namespace SRPG
{
    using System;
    using UnityEngine;

    [AddComponentMenu("UI/リスト/ボーナス勝利条件")]
    public class QuestBonusObjectiveList : MonoBehaviour
    {
        public GameObject ItemTemplate;

        public QuestBonusObjectiveList()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            return;
        }

        private void Start()
        {
            QuestParam param;
            if (DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null) != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            return;
        }
    }
}

