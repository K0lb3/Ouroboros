namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusTowerFloor : MonoBehaviour
    {
        public Text FriendName;
        public Text FloorText;
        public GameObject UnitObj;
        public GameObject FloorInfoObj;
        public GameObject RingObj;
        public Sprite CurrentSprite;
        public Sprite DefaultSprite;
        public Image FloorImage;
        private int mCurrentFloor;

        public VersusTowerFloor()
        {
            this.mCurrentFloor = -1;
            base..ctor();
            return;
        }

        public unsafe void Refresh(int idx, int max)
        {
            GameManager manager;
            int num;
            int num2;
            VersusFriendScore[] scoreArray;
            int num3;
            string str;
            manager = MonoSingleton<GameManager>.Instance;
            num = manager.Player.VersusTowerFloor;
            num2 = idx + 1;
            if (idx < 0)
            {
                goto Label_022C;
            }
            if (idx >= max)
            {
                goto Label_022C;
            }
            this.mCurrentFloor = num2;
            if ((this.FloorInfoObj != null) == null)
            {
                goto Label_0048;
            }
            this.FloorInfoObj.SetActive(1);
        Label_0048:
            if ((this.FloorText != null) == null)
            {
                goto Label_00C4;
            }
            this.FloorText.set_text(&num2.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
            if (num2 != num)
            {
                goto Label_00A5;
            }
            this.FloorText.set_color(new Color(1f, 1f, 0f));
            goto Label_00C4;
        Label_00A5:
            this.FloorText.set_color(new Color(1f, 1f, 1f));
        Label_00C4:
            if ((this.FloorImage != null) == null)
            {
                goto Label_0125;
            }
            if (num2 != num)
            {
                goto Label_0103;
            }
            if ((this.CurrentSprite != null) == null)
            {
                goto Label_0125;
            }
            this.FloorImage.set_sprite(this.CurrentSprite);
            goto Label_0125;
        Label_0103:
            if ((this.DefaultSprite != null) == null)
            {
                goto Label_0125;
            }
            this.FloorImage.set_sprite(this.DefaultSprite);
        Label_0125:
            if ((this.RingObj != null) == null)
            {
                goto Label_0145;
            }
            this.RingObj.SetActive(num == num2);
        Label_0145:
            scoreArray = manager.GetVersusFriendScore(num2);
            if (scoreArray == null)
            {
                goto Label_020A;
            }
            if (((int) scoreArray.Length) <= 0)
            {
                goto Label_020A;
            }
            if (num2 == num)
            {
                goto Label_020A;
            }
            num3 = (int) scoreArray.Length;
            str = string.Empty;
            if ((this.UnitObj != null) == null)
            {
                goto Label_01AA;
            }
            this.UnitObj.SetActive(1);
            DataSource.Bind<UnitData>(this.UnitObj, scoreArray[0].unit);
            GameParameter.UpdateAll(this.UnitObj);
        Label_01AA:
            num3 -= 1;
            if (num3 <= 0)
            {
                goto Label_01DD;
            }
            str = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND_NAME"), scoreArray[0].name, (int) num3);
            goto Label_01E7;
        Label_01DD:
            str = scoreArray[0].name;
        Label_01E7:
            if ((this.FriendName != null) == null)
            {
                goto Label_02A0;
            }
            this.FriendName.set_text(str);
            goto Label_0227;
        Label_020A:
            if ((this.UnitObj != null) == null)
            {
                goto Label_02A0;
            }
            this.UnitObj.SetActive(0);
        Label_0227:
            goto Label_02A0;
        Label_022C:
            this.mCurrentFloor = -1;
            if ((this.FloorInfoObj != null) == null)
            {
                goto Label_0250;
            }
            this.FloorInfoObj.SetActive(0);
        Label_0250:
            if ((this.RingObj != null) == null)
            {
                goto Label_026D;
            }
            this.RingObj.SetActive(0);
        Label_026D:
            if ((this.FloorImage != null) == null)
            {
                goto Label_02A0;
            }
            if ((this.DefaultSprite != null) == null)
            {
                goto Label_02A0;
            }
            this.FloorImage.set_sprite(this.DefaultSprite);
        Label_02A0:
            return;
        }

        public void SetPlayerObject(GameObject playerObj)
        {
            if ((playerObj != null) == null)
            {
                goto Label_003F;
            }
            playerObj.SetActive(1);
            if ((this.RingObj != null) == null)
            {
                goto Label_003F;
            }
            playerObj.get_transform().set_position(this.RingObj.get_transform().get_position());
        Label_003F:
            return;
        }

        public int Floor
        {
            get
            {
                return this.mCurrentFloor;
            }
        }
    }
}

