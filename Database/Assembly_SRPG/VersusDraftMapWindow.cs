namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class VersusDraftMapWindow : MonoBehaviour
    {
        public VersusDraftMapWindow()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            GameManager manager;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.FindQuest(manager.VSDraftQuestId);
            if (param != null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            DataSource.Bind<QuestParam>(base.get_gameObject(), param);
            return;
        }
    }
}

