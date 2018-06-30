namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameCenterManager
    {
        private static List<AchievementParam> mAchievementList;

        static GameCenterManager()
        {
        }

        public GameCenterManager()
        {
            base..ctor();
            return;
        }

        public static void Auth()
        {
            if (isValidEnvironment() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            return;
        }

        public static List<AchievementParam> GetAchievementData()
        {
            string str;
            JSON_AchievementParam[] paramArray;
            JSON_AchievementParam param;
            JSON_AchievementParam[] paramArray2;
            int num;
            AchievementParam param2;
            if (mAchievementList == null)
            {
                goto Label_0010;
            }
            return mAchievementList;
        Label_0010:
            mAchievementList = new List<AchievementParam>();
            str = string.Empty;
            paramArray = JSONParser.parseJSONArray<JSON_AchievementParam>(AssetManager.LoadTextData("GameCenter/acheivement"));
            if (paramArray != null)
            {
                goto Label_003A;
            }
            return null;
        Label_003A:
            paramArray2 = paramArray;
            num = 0;
            goto Label_00AA;
        Label_0044:
            param = paramArray2[num];
            param2 = new AchievementParam();
            param2.id = param.fields.id;
            param2.iname = param.fields.iname;
            param2.ios = param.fields.ios;
            param2.googleplay = param.fields.googleplay;
            mAchievementList.Add(param2);
            num += 1;
        Label_00AA:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0044;
            }
            return mAchievementList;
        }

        private static unsafe AchievementParam GetAchievementParam(string iname)
        {
            List<AchievementParam> list;
            AchievementParam param;
            List<AchievementParam>.Enumerator enumerator;
            AchievementParam param2;
            list = GetAchievementData();
            if (list == null)
            {
                goto Label_0018;
            }
            if (list.Count >= 1)
            {
                goto Label_001A;
            }
        Label_0018:
            return null;
        Label_001A:
            enumerator = list.GetEnumerator();
        Label_0021:
            try
            {
                goto Label_0046;
            Label_0026:
                param = &enumerator.Current;
                if ((param.iname == iname) == null)
                {
                    goto Label_0046;
                }
                param2 = param;
                goto Label_0065;
            Label_0046:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0026;
                }
                goto Label_0063;
            }
            finally
            {
            Label_0057:
                ((List<AchievementParam>.Enumerator) enumerator).Dispose();
            }
        Label_0063:
            return null;
        Label_0065:
            return param2;
        }

        public static void GetLeaderboardData()
        {
        }

        public static bool IsAuth()
        {
            return 1;
        }

        public static bool isValidEnvironment()
        {
            return 1;
        }

        private static void ProcessAuthGameCenter(bool success)
        {
            if (success == null)
            {
                goto Label_0015;
            }
            Debug.Log("[GameCenter]UserLogin Success!!");
            goto Label_001F;
        Label_0015:
            Debug.Log("[GameCenter]UserLogin Failed!!");
        Label_001F:
            return;
        }

        public static void ReAuth()
        {
            if (isValidEnvironment() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            return;
        }

        public static void SendAchievementProgress(AchievementParam param)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            SendAchievementProgressInternal(param.AchievementID);
            return;
        }

        public static void SendAchievementProgress(string iname)
        {
            AchievementParam param;
            param = GetAchievementParam(iname);
            if (param != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            SendAchievementProgressInternal(param.AchievementID);
            return;
        }

        public static void SendAchievementProgress(string achievement_id, long progress)
        {
        }

        public static void SendAchievementProgressInternal(string achievementID)
        {
            long num;
            if (string.IsNullOrEmpty(achievementID) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 100L;
            SendAchievementProgress(achievementID, num);
            return;
        }

        public static void SendLeaderBoardScore(string leader_board_id, long score)
        {
        }

        public static void ShowAchievement()
        {
        }

        public static void ShowLeaderBoard()
        {
        }

        public static bool IsLogin
        {
            get
            {
                if (Social.get_localUser() != null)
                {
                    goto Label_0016;
                }
                DebugUtility.Log("[GameCenterManager]Login Error!");
                return 0;
            Label_0016:
                return Social.get_localUser().get_authenticated();
            }
        }
    }
}

