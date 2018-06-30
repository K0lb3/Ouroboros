namespace SRPG
{
    using GR;
    using System;

    public class NewsUtility
    {
        public NewsUtility()
        {
            base..ctor();
            return;
        }

        public static void clearNewsType()
        {
            GlobalVars.UrgencyPubHash = string.Empty;
            return;
        }

        public static NewsTypes getNewsTypes()
        {
            if (string.IsNullOrEmpty(GlobalVars.UrgencyPubHash) != null)
            {
                goto Label_0011;
            }
            return 2;
        Label_0011:
            if (string.IsNullOrEmpty(GlobalVars.PubHash) != null)
            {
                goto Label_0022;
            }
            return 1;
        Label_0022:
            return 0;
        }

        public static bool isNewsDisplay()
        {
            return ((getNewsTypes() == 0) == 0);
        }

        public static void setNewsState(string pub_hash, string urgency_pub_hash, bool force_display)
        {
            string str;
            string str2;
            str = (string) MonoSingleton<UserInfoManager>.Instance.GetValue("PubHash");
            if (string.IsNullOrEmpty(pub_hash) != null)
            {
                goto Label_0049;
            }
            if ((str != pub_hash) != null)
            {
                goto Label_0032;
            }
            if (force_display == null)
            {
                goto Label_0049;
            }
        Label_0032:
            MonoSingleton<UserInfoManager>.Instance.SetValue("PubHash", pub_hash, 1);
            GlobalVars.PubHash = pub_hash;
        Label_0049:
            str2 = (string) MonoSingleton<UserInfoManager>.Instance.GetValue("UrgencyPubHash");
            if (string.IsNullOrEmpty(urgency_pub_hash) != null)
            {
                goto Label_008C;
            }
            if ((str2 != urgency_pub_hash) == null)
            {
                goto Label_008C;
            }
            MonoSingleton<UserInfoManager>.Instance.SetValue("UrgencyPubHash", urgency_pub_hash, 1);
            GlobalVars.UrgencyPubHash = urgency_pub_hash;
        Label_008C:
            return;
        }

        public enum NewsTypes
        {
            None,
            Normal,
            Urgency
        }
    }
}

