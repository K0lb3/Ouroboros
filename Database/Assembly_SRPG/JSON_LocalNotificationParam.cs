namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_LocalNotificationParam
    {
        public string msg_stamina;
        public string iOSAct_stamina;
        public int limitSec_stamina;

        public JSON_LocalNotificationParam()
        {
            base..ctor();
            return;
        }
    }
}

