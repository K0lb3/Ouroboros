namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitListItemEvents : ListItemEvents
    {
        private const float CHANGE_INTERBAL = 2f;
        public Image[] EqIcons;
        public Image[] AttrIcons;
        public GameObject Badge;
        public GameObject CharacterQuestBadge;
        public GameObject FavoriteBadge;
        public bool DispAlternate;
        private int m_Index;
        private float m_Time;

        public UnitListItemEvents()
        {
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            this.Refresh();
            return;
        }

        public void Refresh()
        {
            UnitData data;
            EquipData[] dataArray;
            PlayerData data2;
            SpriteSheet sheet;
            int num;
            EquipData data3;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0234;
            }
            dataArray = data.CurrentEquips;
            if ((((dataArray == null) || (this.EqIcons == null)) || ((((int) this.EqIcons.Length) < ((int) dataArray.Length)) || (this.AttrIcons == null))) || (((int) this.AttrIcons.Length) < ((int) dataArray.Length)))
            {
                goto Label_0218;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player;
            sheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
            num = 0;
            goto Label_020E;
        Label_0074:
            data3 = dataArray[num];
            if ((this.EqIcons[num] == null) != null)
            {
                goto Label_0208;
            }
            if ((this.AttrIcons[num] == null) == null)
            {
                goto Label_00A7;
            }
            goto Label_0208;
        Label_00A7:
            this.EqIcons[num].get_gameObject().SetActive(0);
            this.AttrIcons[num].get_gameObject().SetActive(0);
            if ((data3 != null) && (data3.IsValid() != null))
            {
                goto Label_00F6;
            }
            this.EqIcons[num].set_sprite(null);
            goto Label_0208;
        Label_00F6:
            this.EqIcons[num].get_gameObject().SetActive(1);
            this.EqIcons[num].set_sprite(sheet.GetSprite(data3.ItemID));
            if (data3.IsEquiped() == null)
            {
                goto Label_0136;
            }
            goto Label_0208;
        Label_0136:
            this.AttrIcons[num].get_gameObject().SetActive(1);
            if (data2.HasItem(data3.ItemID) == null)
            {
                goto Label_019B;
            }
            this.AttrIcons[num].set_sprite(sheet.GetSprite((data3.ItemParam.equipLv <= data.Lv) ? "plus0" : "plus1"));
            goto Label_0208;
        Label_019B:
            if (data2.CheckEnableCreateItem(data3.ItemParam, 1, 1, null) == null)
            {
                goto Label_01EF;
            }
            this.AttrIcons[num].set_sprite(sheet.GetSprite((data3.ItemParam.equipLv <= data.Lv) ? "plus0" : "plus1"));
            goto Label_0208;
        Label_01EF:
            this.AttrIcons[num].set_sprite(sheet.GetSprite("plus2"));
        Label_0208:
            num += 1;
        Label_020E:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0074;
            }
        Label_0218:
            this.m_Time = 2f;
            this.Update();
            GameParameter.UpdateAll(base.get_gameObject());
        Label_0234:
            return;
        }

        private void Start()
        {
            if ((this.Badge != null) == null)
            {
                goto Label_001D;
            }
            this.Badge.SetActive(0);
        Label_001D:
            return;
        }

        private void Update()
        {
            if (this.DispAlternate != null)
            {
                goto Label_0016;
            }
            this.UpdateBadgeDefault();
            goto Label_001C;
        Label_0016:
            this.UpdateBadgeAlternate();
        Label_001C:
            return;
        }

        private void UpdateBadgeAlternate()
        {
            List<GameObject> list;
            List<GameObject> list2;
            UnitData data;
            UnitParam param;
            int num;
            int num2;
            list = new List<GameObject>();
            list2 = new List<GameObject>();
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_00DA;
            }
            if ((this.Badge != null) == null)
            {
                goto Label_0058;
            }
            if (data.BadgeState == null)
            {
                goto Label_004C;
            }
            list.Add(this.Badge);
            goto Label_0058;
        Label_004C:
            list2.Add(this.Badge);
        Label_0058:
            if ((this.CharacterQuestBadge != null) == null)
            {
                goto Label_009C;
            }
            if (data.IsOpenCharacterQuest() == null)
            {
                goto Label_0090;
            }
            if (data.GetCurrentCharaEpisodeData() == null)
            {
                goto Label_0090;
            }
            list.Add(this.CharacterQuestBadge);
            goto Label_009C;
        Label_0090:
            list2.Add(this.CharacterQuestBadge);
        Label_009C:
            if ((this.FavoriteBadge != null) == null)
            {
                goto Label_0165;
            }
            if (data.IsFavorite == null)
            {
                goto Label_00C9;
            }
            list.Add(this.FavoriteBadge);
            goto Label_00D5;
        Label_00C9:
            list2.Add(this.FavoriteBadge);
        Label_00D5:
            goto Label_0165;
        Label_00DA:
            param = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0165;
            }
            if ((this.Badge != null) == null)
            {
                goto Label_012B;
            }
            if (MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(param) == null)
            {
                goto Label_011F;
            }
            list.Add(this.Badge);
            goto Label_012B;
        Label_011F:
            list2.Add(this.Badge);
        Label_012B:
            if ((this.CharacterQuestBadge != null) == null)
            {
                goto Label_0148;
            }
            list2.Add(this.CharacterQuestBadge);
        Label_0148:
            if ((this.FavoriteBadge != null) == null)
            {
                goto Label_0165;
            }
            list2.Add(this.FavoriteBadge);
        Label_0165:
            num = 0;
            goto Label_0194;
        Label_016D:
            if ((list2[num] != null) == null)
            {
                goto Label_018E;
            }
            list2[num].SetActive(0);
        Label_018E:
            num += 1;
        Label_0194:
            if (num < list2.Count)
            {
                goto Label_016D;
            }
            this.m_Time += Time.get_deltaTime();
            if (this.m_Time <= 2f)
            {
                goto Label_01FB;
            }
            this.m_Time -= 2f;
            this.m_Index += 1;
            if (this.m_Index < list.Count)
            {
                goto Label_01FB;
            }
            this.m_Index = 0;
        Label_01FB:
            num2 = 0;
            goto Label_0233;
        Label_0203:
            if ((list[num2] != null) == null)
            {
                goto Label_022D;
            }
            list[num2].SetActive(this.m_Index == num2);
        Label_022D:
            num2 += 1;
        Label_0233:
            if (num2 < list.Count)
            {
                goto Label_0203;
            }
            return;
        }

        private void UpdateBadgeDefault()
        {
            UnitData data;
            UnitParam param;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0098;
            }
            if ((this.Badge != null) == null)
            {
                goto Label_003B;
            }
            this.Badge.SetActive((data.BadgeState == 0) == 0);
        Label_003B:
            if ((this.CharacterQuestBadge != null) == null)
            {
                goto Label_0071;
            }
            this.CharacterQuestBadge.SetActive((data.IsOpenCharacterQuest() == null) ? 0 : ((data.GetCurrentCharaEpisodeData() == null) == 0));
        Label_0071:
            if ((this.FavoriteBadge != null) == null)
            {
                goto Label_00D2;
            }
            this.FavoriteBadge.SetActive(data.IsFavorite);
            goto Label_00D2;
        Label_0098:
            param = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_00D2;
            }
            if ((this.Badge != null) == null)
            {
                goto Label_00D2;
            }
            this.Badge.SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(param));
        Label_00D2:
            return;
        }
    }
}

