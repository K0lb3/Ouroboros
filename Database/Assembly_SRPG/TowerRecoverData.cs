namespace SRPG
{
    using System;

    public class TowerRecoverData
    {
        public string towerID;
        public int useCoin;

        public TowerRecoverData(string towerID, int useCoin)
        {
            base..ctor();
            this.towerID = towerID;
            this.useCoin = useCoin;
            return;
        }
    }
}

