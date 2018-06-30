namespace SRPG
{
    using System;

    [Serializable]
    public class AudienceStartParam
    {
        public JSON_MyPhotonPlayerParam[] players;
        public BattleCore.Json_BtlInfo btlinfo;

        public AudienceStartParam()
        {
            base..ctor();
            return;
        }
    }
}

