namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(Selectable))]
    public class TowerLevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
    {
        public Text ConditionText;
        public GameObject ShowLocked;
        public GameObject ShowUnlocked;
        public bool ToggleInteractable;
        private int mUnlockLevel;
        private TowerParam Param;

        public TowerLevelLock()
        {
            this.ToggleInteractable = 1;
            base..ctor();
            return;
        }

        public bool GetIsUnlock()
        {
            if (this.Param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.Param.is_unlock;
        }

        public TowerFloorParam GetTowerFloorParam(string iname)
        {
            return MonoSingleton<GameManager>.Instance.FindTowerFloor(iname);
        }

        public TowerParam GetTowerParam(string tower_id)
        {
            return MonoSingleton<GameManager>.Instance.FindTower(tower_id);
        }

        private bool IsIgnorePlayerLevel()
        {
            if (this.mUnlockLevel != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return 0;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerData data;
            StringBuilder builder;
            string str;
            string str2;
            string str3;
            data = MonoSingleton<GameManager>.Instance.Player;
            builder = new StringBuilder();
            str = this.ShowLockMessage(data.Lv, this.mUnlockLevel);
            str2 = this.ShowProgLockMessage();
            if (this.Param == null)
            {
                goto Label_00B5;
            }
            if (this.Param.is_unlock != null)
            {
                goto Label_00B5;
            }
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_005C;
            }
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00B5;
            }
        Label_005C:
            builder.Append(LocalizedText.Get("sys.UNLOCK_COND"));
            if (this.IsIgnorePlayerLevel() != null)
            {
                goto Label_008C;
            }
            builder.Append(str);
            builder.Append("\n");
        Label_008C:
            builder.Append(str2);
            str3 = builder.ToString();
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_00B5;
            }
            UIUtility.SystemMessage(null, str3, null, null, 0, -1);
        Label_00B5:
            eventData.Use();
            return;
        }

        public string ShowLockMessage(int playerLv, int reqLv)
        {
            string str;
            return string.Format(LocalizedText.Get("sys.UNLOCK_COND_REQLV"), (int) reqLv);
        }

        public string ShowProgLockMessage()
        {
            object[] objArray1;
            TowerFloorParam param;
            TowerParam param2;
            if (this.Param != null)
            {
                goto Label_0011;
            }
            return string.Empty;
        Label_0011:
            if (this.Param.is_unlock == null)
            {
                goto Label_0027;
            }
            return string.Empty;
        Label_0027:
            param = this.GetTowerFloorParam(this.Param.unlock_quest);
            if (param != null)
            {
                goto Label_0045;
            }
            return string.Empty;
        Label_0045:
            param2 = this.GetTowerParam(param.tower_id);
            if (param2 != null)
            {
                goto Label_005E;
            }
            return string.Empty;
        Label_005E:
            objArray1 = new object[] { param2.name, param.name };
            return LocalizedText.Get("sys.UNLOCK_COND_TOWER_PROG", objArray1);
        }

        private void Start()
        {
            this.Param = DataSource.FindDataOfClass<TowerParam>(base.get_gameObject(), null);
            this.mUnlockLevel = this.Param.unlock_level;
            this.UpdateLockState();
            return;
        }

        private void UpdateLockState()
        {
            GameManager manager;
            PlayerData data;
            bool flag;
            Selectable selectable;
            data = MonoSingleton<GameManager>.Instance.Player;
            flag = this.GetIsUnlock();
            if (this.ToggleInteractable == null)
            {
                goto Label_0039;
            }
            selectable = base.GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0039;
            }
            selectable.set_interactable(flag);
        Label_0039:
            if ((this.ShowUnlocked != null) == null)
            {
                goto Label_0056;
            }
            this.ShowUnlocked.SetActive(flag);
        Label_0056:
            if ((this.ShowLocked != null) == null)
            {
                goto Label_0076;
            }
            this.ShowLocked.SetActive(flag == 0);
        Label_0076:
            if ((this.ConditionText != null) == null)
            {
                goto Label_00C9;
            }
            if (this.mUnlockLevel <= 0)
            {
                goto Label_00C9;
            }
            if (data.Lv >= this.mUnlockLevel)
            {
                goto Label_00C9;
            }
            this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (int) this.mUnlockLevel));
        Label_00C9:
            return;
        }
    }
}

