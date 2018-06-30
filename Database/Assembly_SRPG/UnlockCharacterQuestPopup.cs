namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnlockCharacterQuestPopup : MonoBehaviour
    {
        private UnitData mCurrentUnit;
        public Text EpisodeTitle;
        public Text EpisodeNumber;

        public UnlockCharacterQuestPopup()
        {
            base..ctor();
            return;
        }

        public void Setup(UnitData unit, int episodeNumber, string episodeTitle)
        {
            this.mCurrentUnit = unit;
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mCurrentUnit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
        }
    }
}

