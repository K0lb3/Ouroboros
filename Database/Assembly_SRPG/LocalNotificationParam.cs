namespace SRPG
{
    using System;

    [Serializable]
    public class LocalNotificationParam
    {
        public string msg_stamina;
        public string iOSAct_stamina;
        public int limitSec_stamina;

        public LocalNotificationParam()
        {
            this.msg_stamina = "null";
            this.iOSAct_stamina = "null";
            this.limitSec_stamina = 10;
            base..ctor();
            return;
        }
    }
}

