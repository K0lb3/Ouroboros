namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class KeyQuestBanner : BaseIcon
    {
        public GameObject IconRoot;
        public RawImage Icon;
        public Image Frame;
        public Text UseNum;
        public Text Amount;
        public GameObject Locked;
        public QuestTimeLimit QuestTimer;

        public KeyQuestBanner()
        {
            base..ctor();
            return;
        }

        public override unsafe void UpdateValue()
        {
            KeyItem item;
            ItemParam param;
            ItemData data;
            int num;
            Sprite sprite;
            ChapterParam param2;
            bool flag;
            item = DataSource.FindDataOfClass<KeyItem>(base.get_gameObject(), null);
            if (item == null)
            {
                goto Label_013D;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            if (param == null)
            {
                goto Label_013D;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
            num = (data == null) ? 0 : data.Num;
            if ((this.Icon != null) == null)
            {
                goto Label_0075;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(param));
        Label_0075:
            if ((this.Frame != null) == null)
            {
                goto Label_00A0;
            }
            sprite = GameSettings.Instance.GetItemFrame(param);
            this.Frame.set_sprite(sprite);
        Label_00A0:
            if ((this.UseNum != null) == null)
            {
                goto Label_00C7;
            }
            this.UseNum.set_text(&item.num.ToString());
        Label_00C7:
            if ((this.Amount != null) == null)
            {
                goto Label_00EA;
            }
            this.Amount.set_text(&num.ToString());
        Label_00EA:
            if ((this.Locked != null) == null)
            {
                goto Label_013D;
            }
            param2 = DataSource.FindDataOfClass<ChapterParam>(base.get_gameObject(), null);
            flag = ((param2 == null) || (param2.IsKeyQuest() == null)) ? 0 : param2.IsKeyUnlock(Network.GetServerTime());
            this.Locked.SetActive(flag == 0);
        Label_013D:
            if ((this.QuestTimer != null) == null)
            {
                goto Label_0159;
            }
            this.QuestTimer.UpdateValue();
        Label_0159:
            if ((this.IconRoot != null) == null)
            {
                goto Label_0191;
            }
            if ((this.Locked != null) == null)
            {
                goto Label_0191;
            }
            this.IconRoot.SetActive(this.Locked.get_activeSelf());
        Label_0191:
            return;
        }
    }
}

