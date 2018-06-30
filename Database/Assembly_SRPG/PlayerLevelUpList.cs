namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerLevelUpList : MonoBehaviour
    {
        [Description("プレイヤーレベルの行")]
        public GameObject Level;
        [Description("現在の出撃ポイントの行")]
        public GameObject StaminaCurrent;
        [Description("最大出撃ポイントの行")]
        public GameObject StaminaMax;
        [Description("最大フレンド枠の行")]
        public GameObject FriendSlotMax;
        [Description("アンロック情報")]
        public GameObject[] UnlockInfo;

        public PlayerLevelUpList()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            GameManager manager;
            PlayerLevelUpInfo info;
            PlayerData data;
            PlayerParam param;
            PlayerParam param2;
            List<UnlockParam> list;
            int num;
            int num2;
            UnlockParam param3;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            info = new PlayerLevelUpInfo();
            data = manager.Player;
            info.LevelNext = PlayerData.CalcLevelFromExp(GlobalVars.PlayerExpNew);
            info.LevelPrev = PlayerData.CalcLevelFromExp(GlobalVars.PlayerExpOld);
            param = manager.MasterParam.GetPlayerParam(info.LevelPrev);
            param2 = manager.MasterParam.GetPlayerParam(info.LevelNext);
            info.StaminaNext = data.Stamina;
            info.StaminaMaxPrev = param.pt;
            info.StaminaMaxNext = param2.pt;
            info.MaxFriendNumPrev = param.fcap;
            info.MaxFriendNumNext = param2.fcap;
            list = new List<UnlockParam>();
            num = info.LevelPrev + 1;
            goto Label_015A;
        Label_00CA:
            num2 = 0;
            goto Label_0140;
        Label_00D2:
            param3 = manager.MasterParam.Unlocks[num2];
            if (param3.UnlockTarget != 8)
            {
                goto Label_00F4;
            }
            goto Label_013A;
        Label_00F4:
            if (param3.VipRank <= data.VipRank)
            {
                goto Label_010B;
            }
            goto Label_013A;
        Label_010B:
            if (param3.PlayerLevel == num)
            {
                goto Label_011E;
            }
            goto Label_013A;
        Label_011E:
            if (list.Contains(param3) == null)
            {
                goto Label_0131;
            }
            goto Label_013A;
        Label_0131:
            list.Add(param3);
        Label_013A:
            num2 += 1;
        Label_0140:
            if (num2 < ((int) manager.MasterParam.Unlocks.Length))
            {
                goto Label_00D2;
            }
            num += 1;
        Label_015A:
            if (num <= info.LevelNext)
            {
                goto Label_00CA;
            }
            if (list == null)
            {
                goto Label_01CC;
            }
            info.UnlockInfo = new string[list.Count];
            num3 = 0;
            goto Label_01B9;
        Label_0188:
            info.UnlockInfo[num3] = LocalizedText.Get("sys.UNLOCK_" + list[num3].iname.ToUpper());
            num3 += 1;
        Label_01B9:
            if (num3 < list.Count)
            {
                goto Label_0188;
            }
            goto Label_01D8;
        Label_01CC:
            info.UnlockInfo = new string[0];
        Label_01D8:
            DataSource.Bind<PlayerLevelUpInfo>(base.get_gameObject(), info);
            if ((this.StaminaMax != null) == null)
            {
                goto Label_0211;
            }
            this.StaminaMax.SetActive((info.StaminaMaxPrev == info.StaminaMaxNext) == 0);
        Label_0211:
            if ((this.FriendSlotMax != null) == null)
            {
                goto Label_023E;
            }
            this.FriendSlotMax.SetActive((info.MaxFriendNumPrev == info.MaxFriendNumNext) == 0);
        Label_023E:
            num4 = 0;
            goto Label_0266;
        Label_0246:
            this.UnlockInfo[num4].SetActive(num4 < ((int) info.UnlockInfo.Length));
            num4 += 1;
        Label_0266:
            if (num4 < ((int) this.UnlockInfo.Length))
            {
                goto Label_0246;
            }
            return;
        }
    }
}

