namespace SRPG
{
    using System;
    using UnityEngine;

    public class RankMatchClassListItem : ListItemEvents
    {
        public GameObject RewardUnit;
        public GameObject RewardItem;
        public GameObject RewardCard;
        public GameObject RewardArtifact;
        public GameObject RewardAward;
        public GameObject RewardGold;
        public GameObject RewardCoin;
        public Transform RewardList;

        public RankMatchClassListItem()
        {
            base..ctor();
            return;
        }
    }
}

