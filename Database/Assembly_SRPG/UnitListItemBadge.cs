namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnitListItemBadge : MonoBehaviour
    {
        private const float CHANGE_INTERBAL = 2f;
        [CustomField("強化", 1), CustomGroup("バッジ")]
        public GameObject m_Badge;
        [CustomGroup("バッジ"), CustomField("ストーリー", 1)]
        public GameObject m_CharacterQuest;
        [CustomField("お気に入り", 1), CustomGroup("バッジ")]
        public GameObject m_Favorite;
        private UnitParam m_UnitParam;
        private UnitData m_UnitData;
        private List<GameObject> m_DispOn;
        private List<GameObject> m_DispOff;
        private int m_Index;
        private float m_Time;

        public UnitListItemBadge()
        {
            this.m_DispOn = new List<GameObject>();
            this.m_DispOff = new List<GameObject>();
            base..ctor();
            return;
        }

        private void OnDisable()
        {
        }

        private void OnEnable()
        {
            this.Refresh();
            return;
        }

        public void Refresh()
        {
            this.m_DispOn.Clear();
            this.m_DispOff.Clear();
            this.m_UnitParam = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            this.m_UnitData = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (this.m_UnitData == null)
            {
                goto Label_0132;
            }
            if ((this.m_Badge != null) == null)
            {
                goto Label_008D;
            }
            if (this.m_UnitData.BadgeState == null)
            {
                goto Label_007C;
            }
            this.m_DispOn.Add(this.m_Badge);
            goto Label_008D;
        Label_007C:
            this.m_DispOff.Add(this.m_Badge);
        Label_008D:
            if ((this.m_CharacterQuest != null) == null)
            {
                goto Label_00E5;
            }
            if (this.m_UnitData.IsOpenCharacterQuest() == null)
            {
                goto Label_00D4;
            }
            if (this.m_UnitData.GetCurrentCharaEpisodeData() == null)
            {
                goto Label_00D4;
            }
            this.m_DispOn.Add(this.m_CharacterQuest);
            goto Label_00E5;
        Label_00D4:
            this.m_DispOff.Add(this.m_CharacterQuest);
        Label_00E5:
            if ((this.m_Favorite != null) == null)
            {
                goto Label_0239;
            }
            if (this.m_UnitData.IsFavorite == null)
            {
                goto Label_011C;
            }
            this.m_DispOn.Add(this.m_Favorite);
            goto Label_012D;
        Label_011C:
            this.m_DispOff.Add(this.m_Favorite);
        Label_012D:
            goto Label_0239;
        Label_0132:
            if (this.m_UnitParam == null)
            {
                goto Label_01D3;
            }
            if ((this.m_Badge != null) == null)
            {
                goto Label_018A;
            }
            if (MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(this.m_UnitParam) == null)
            {
                goto Label_0179;
            }
            this.m_DispOn.Add(this.m_Badge);
            goto Label_018A;
        Label_0179:
            this.m_DispOff.Add(this.m_Badge);
        Label_018A:
            if ((this.m_CharacterQuest != null) == null)
            {
                goto Label_01AC;
            }
            this.m_DispOff.Add(this.m_CharacterQuest);
        Label_01AC:
            if ((this.m_Favorite != null) == null)
            {
                goto Label_0239;
            }
            this.m_DispOff.Add(this.m_Favorite);
            goto Label_0239;
        Label_01D3:
            if ((this.m_Badge != null) == null)
            {
                goto Label_01F5;
            }
            this.m_DispOff.Add(this.m_Badge);
        Label_01F5:
            if ((this.m_CharacterQuest != null) == null)
            {
                goto Label_0217;
            }
            this.m_DispOff.Add(this.m_CharacterQuest);
        Label_0217:
            if ((this.m_Favorite != null) == null)
            {
                goto Label_0239;
            }
            this.m_DispOff.Add(this.m_Favorite);
        Label_0239:
            this.m_Time = 0f;
            this.m_Index = 0;
            this.UpdateBadgeAlternate(0);
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.m_Time += Time.get_deltaTime();
            if (this.m_Time <= 2f)
            {
                goto Label_005F;
            }
            this.m_Time -= 2f;
            this.m_Index += 1;
            if (this.m_Index < this.m_DispOn.Count)
            {
                goto Label_005F;
            }
            this.m_Index = 0;
        Label_005F:
            this.UpdateBadgeAlternate(this.m_Index);
            return;
        }

        private void UpdateBadgeAlternate(int index)
        {
            int num;
            int num2;
            bool flag;
            num = 0;
            goto Label_004A;
        Label_0007:
            if ((this.m_DispOff[num] != null) == null)
            {
                goto Label_0046;
            }
            if (this.m_DispOff[num].get_activeSelf() == null)
            {
                goto Label_0046;
            }
            this.m_DispOff[num].SetActive(0);
        Label_0046:
            num += 1;
        Label_004A:
            if (num < this.m_DispOff.Count)
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_00AB;
        Label_0062:
            if ((this.m_DispOn[num2] != null) == null)
            {
                goto Label_00A7;
            }
            flag = index == num2;
            if (this.m_DispOn[num2].get_activeSelf() == flag)
            {
                goto Label_00A7;
            }
            this.m_DispOn[num2].SetActive(flag);
        Label_00A7:
            num2 += 1;
        Label_00AB:
            if (num2 < this.m_DispOn.Count)
            {
                goto Label_0062;
            }
            return;
        }
    }
}

