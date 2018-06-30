namespace SRPG
{
    using System;
    using UnityEngine;

    public class UpdateTrophyInterval
    {
        private const float NOW_TROPHY_INTERVAL_TIME = 0f;
        private const float UPDATE_TROPHY_INTERVAL_TIME = 5f;
        private const float SYNC_TROPHY_INTERVAL_TIME = 0.1f;
        private float updat_torphy_interval;

        public UpdateTrophyInterval()
        {
            base..ctor();
            return;
        }

        public bool PlayCheckUpdate()
        {
            if (0f < this.updat_torphy_interval)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            this.updat_torphy_interval -= Time.get_unscaledDeltaTime();
            return 0;
        }

        public void SetSyncInterval()
        {
            this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 0.1f);
            return;
        }

        public void SetSyncNow()
        {
            this.updat_torphy_interval = 0f;
            return;
        }

        public void SetUpdateInterval()
        {
            this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 5f);
            return;
        }
    }
}

