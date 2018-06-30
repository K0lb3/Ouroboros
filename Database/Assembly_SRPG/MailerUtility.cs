namespace SRPG
{
    using DeviceKit;
    using System;

    public class MailerUtility
    {
        public MailerUtility()
        {
            base..ctor();
            return;
        }

        public static void Launch(string mailto, string subject, string body)
        {
            App.LaunchMailer(mailto, subject, body.Replace("\n", "%0A"));
            return;
        }
    }
}

