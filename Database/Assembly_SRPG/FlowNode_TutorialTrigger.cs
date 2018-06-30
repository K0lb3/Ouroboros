namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(0, "Activate", 0, 0), Pin(1, "Deactivate", 0, 1), Pin(2, "ForceExec", 0, 2), Pin(10, "Triggered", 1, 10), AddComponentMenu(""), NodeType("Event/Tutorial Trigger", 0xe57f)]
    public class FlowNode_TutorialTrigger : FlowNode
    {
        [BitMask]
        public ActivateFlags Flags;
        public TriggerTypes TriggerType;
        [SerializeField]
        private string sVal0;
        [SerializeField]
        private string sVal1;
        [SerializeField]
        private int iVal0;
        [SerializeField]
        private int iVal1;
        public bool DontSkipFlag;

        public FlowNode_TutorialTrigger()
        {
            this.Flags = 2;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            base.set_enabled(((this.Flags & 1) == 0) == 0);
            return;
        }

        private bool CompareUnit(Unit unit, int turn)
        {
            return (((string.IsNullOrEmpty(this.sVal0) == null) && ((unit.UniqueName == this.sVal0) == null)) ? 0 : ((this.iVal0 < 0) ? 1 : (this.iVal0 == turn)));
        }

        private void ForceExec()
        {
            TriggerTypes types;
            types = this.TriggerType;
            if (types == 20)
            {
                goto Label_001C;
            }
            if (types == 100)
            {
                goto Label_0027;
            }
            goto Label_0032;
        Label_001C:
            this.OnCloseBattleInfo();
            goto Label_0032;
        Label_0027:
            this.OnSkipBattleExplain();
        Label_0032:
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0021;

                case 2:
                    goto Label_0029;
            }
            goto Label_0030;
        Label_0019:
            base.set_enabled(1);
            return;
        Label_0021:
            base.set_enabled(0);
            return;
        Label_0029:
            this.ForceExec();
            return;
        Label_0030:
            return;
        }

        public void OnCloseBattleInfo()
        {
            SceneBattle.Instance.SelectUnitDir(0);
            return;
        }

        public void OnFinishCameraUnitFocus(Unit unit, int turn)
        {
            TacticsUnitController controller;
            FlowNode_TutorialMask[] maskArray;
            int num;
            if (this.TriggerType == 0x13)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            controller = SceneBattle.Instance.FindUnitController(unit);
            maskArray = base.GetComponents<FlowNode_TutorialMask>();
            num = 0;
            goto Label_0059;
        Label_0039:
            if (maskArray[num].ComponentId == null)
            {
                goto Label_004B;
            }
            goto Label_0055;
        Label_004B:
            this.SetupMask_MoveUnit(controller, maskArray[num]);
        Label_0055:
            num += 1;
        Label_0059:
            if (num < ((int) maskArray.Length))
            {
                goto Label_0039;
            }
            this.TriggerIf(this.CompareUnit(unit, turn));
            return;
        }

        public void OnHotTargetsChange(Unit unit, Unit target, int turn)
        {
            TacticsUnitController controller;
            FlowNode_TutorialMask[] maskArray;
            int num;
            if (this.TriggerType == 0x10)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            controller = SceneBattle.Instance.FindUnitController(target);
            maskArray = base.GetComponents<FlowNode_TutorialMask>();
            num = 0;
            goto Label_0087;
        Label_0039:
            this.SetupMask_AttackTargetDesc(controller, maskArray[num]);
            if (maskArray[num].ComponentId != 1)
            {
                goto Label_005F;
            }
            this.TriggerIf(this.CompareUnit(unit, turn));
        Label_005F:
            this.SetupMask_NormalAttackDesc(maskArray[num]);
            this.SetupMask_AbilityDesc(maskArray[num]);
            this.SetupMask_TapNormalAttack(maskArray[num]);
            this.SetupMask_ExecNormalAttack(maskArray[num]);
            num += 1;
        Label_0087:
            if (num < ((int) maskArray.Length))
            {
                goto Label_0039;
            }
            return;
        }

        public void OnMapEnd()
        {
            this.TriggerIf(this.TriggerType == 0x15);
            return;
        }

        public void OnMapStart()
        {
            this.TriggerIf(this.TriggerType == 0x12);
            return;
        }

        public void OnSelectDirectionStart(Unit unit, int turn)
        {
            TacticsUnitController controller;
            FlowNode_TutorialMask[] maskArray;
            int num;
            if (this.TriggerType == 0x11)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            controller = SceneBattle.Instance.FindUnitController(unit);
            maskArray = base.GetComponents<FlowNode_TutorialMask>();
            num = 0;
            goto Label_0036;
        Label_0028:
            this.SetupMask_SelectDir(controller, maskArray[num]);
            num += 1;
        Label_0036:
            if (num < ((int) maskArray.Length))
            {
                goto Label_0028;
            }
            this.TriggerIf(this.CompareUnit(unit, turn));
            return;
        }

        public void OnSkipBattleExplain()
        {
            FlowNode_TutorialTrigger[] triggerArray;
            int num;
            triggerArray = base.GetComponents<FlowNode_TutorialTrigger>();
            num = 0;
            goto Label_002D;
        Label_000E:
            if (triggerArray[num].DontSkipFlag == null)
            {
                goto Label_0020;
            }
            goto Label_0029;
        Label_0020:
            triggerArray[num].TriggerType = 0;
        Label_0029:
            num += 1;
        Label_002D:
            if (num < ((int) triggerArray.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public void OnTargetChange(Unit target, int turn)
        {
            this.TriggerIf((this.TriggerType != 15) ? 0 : this.CompareUnit(target, turn));
            return;
        }

        public void OnUnitEnd(Unit unit, int turn)
        {
            this.TriggerIf((this.TriggerType != 14) ? 0 : this.CompareUnit(unit, turn));
            return;
        }

        public void OnUnitMoveStart(Unit unit, int turn)
        {
            this.TriggerIf((this.TriggerType != 12) ? 0 : this.CompareUnit(unit, turn));
            return;
        }

        public void OnUnitSkillEnd(Unit unit, int turn)
        {
            this.TriggerIf((this.TriggerType != 13) ? 0 : this.CompareUnit(unit, turn));
            return;
        }

        public void OnUnitSkillStart(Unit unit, int turn)
        {
            this.TriggerIf((this.TriggerType != 11) ? 0 : this.CompareUnit(unit, turn));
            return;
        }

        public void OnUnitStart(Unit unit, int turn)
        {
            this.TriggerIf((this.TriggerType != 10) ? 0 : this.CompareUnit(unit, turn));
            return;
        }

        private unsafe void SetupMask_AbilityDesc(FlowNode_TutorialMask flownode_mask)
        {
            List<GameObject> list;
            float num;
            float num2;
            float num3;
            float num4;
            RectTransform transform;
            int num5;
            float num6;
            float num7;
            float num8;
            float num9;
            float num10;
            float num11;
            Vector3 vector;
            string str;
            Vector3 vector2;
            Rect rect;
            Vector3 vector3;
            Vector3 vector4;
            Rect rect2;
            Vector3 vector5;
            Vector3 vector6;
            Rect rect3;
            Vector3 vector7;
            Vector3 vector8;
            Rect rect4;
            Vector3 vector9;
            if (flownode_mask.ComponentId == 3)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0228;
            }
            if ((SceneBattle.Instance.BattleUI != null) == null)
            {
                goto Label_0228;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow != null) == null)
            {
                goto Label_0228;
            }
            if (SceneBattle.Instance.BattleUI.CommandWindow.AbilityButtons == null)
            {
                goto Label_0228;
            }
            list = SceneBattle.Instance.BattleUI.CommandWindow.AbilityButtons;
            num = 99999f;
            num2 = 99999f;
            num3 = -99999f;
            num4 = -99999f;
            transform = null;
            num5 = 0;
            goto Label_01CE;
        Label_009E:
            transform = list[num5].get_transform() as RectTransform;
            num6 = &transform.get_position().x - ((&transform.get_rect().get_width() / 2f) * &transform.get_lossyScale().x);
            num7 = &transform.get_position().y - ((&transform.get_rect().get_height() / 2f) * &transform.get_lossyScale().y);
            num8 = &transform.get_position().x + ((&transform.get_rect().get_width() / 2f) * &transform.get_lossyScale().x);
            num9 = &transform.get_position().y + ((&transform.get_rect().get_height() / 2f) * &transform.get_lossyScale().y);
            if (num < num6)
            {
                goto Label_01A5;
            }
            num = num6;
        Label_01A5:
            if (num2 < num7)
            {
                goto Label_01B0;
            }
            num2 = num7;
        Label_01B0:
            if (num3 > num8)
            {
                goto Label_01BB;
            }
            num3 = num8;
        Label_01BB:
            if (num4 > num9)
            {
                goto Label_01C8;
            }
            num4 = num9;
        Label_01C8:
            num5 += 1;
        Label_01CE:
            if (num5 < list.Count)
            {
                goto Label_009E;
            }
            num10 = num3 - num;
            num11 = num4 - num2;
            &vector..ctor(num + (num10 / 2f), num2 + (num11 / 2f), 0f);
            str = LocalizedText.Get("sys.TUTORIAL_ABILITY");
            flownode_mask.Setup(3, vector, 0, str);
            flownode_mask.SetupMaskSize(num10, num11);
        Label_0228:
            return;
        }

        private unsafe void SetupMask_AttackTargetDesc(TacticsUnitController controller, FlowNode_TutorialMask flownode_mask)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3[] vectorArray;
            float num;
            float num2;
            float num3;
            float num4;
            int num5;
            Vector2 vector3;
            float num6;
            float num7;
            Vector3 vector4;
            Vector3 vector5;
            Vector3 vector6;
            Vector3 vector7;
            Vector3 vector8;
            Vector3 vector9;
            if (flownode_mask.ComponentId == 1)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((controller != null) == null)
            {
                goto Label_0273;
            }
            if ((flownode_mask != null) == null)
            {
                goto Label_0273;
            }
            &vector..ctor(&controller.CenterPosition.x + ((float) this.iVal1), &controller.CenterPosition.y, &controller.CenterPosition.z);
            flownode_mask.Setup(1, vector, 1, null);
            &vector2..ctor(&controller.CenterPosition.x + ((float) this.iVal1), &controller.CenterPosition.y - (controller.Height / 2f), &controller.CenterPosition.z);
            vectorArray = new Vector3[5];
            *(&(vectorArray[0])) = new Vector3(&vector2.x - 0.5f, &vector2.y, &vector2.z - 0.5f);
            *(&(vectorArray[1])) = new Vector3(&vector2.x - 0.5f, &vector2.y, &vector2.z + 0.5f);
            *(&(vectorArray[2])) = new Vector3(&vector2.x + 0.5f, &vector2.y, &vector2.z + 0.5f);
            *(&(vectorArray[3])) = new Vector3(&vector2.x + 0.5f, &vector2.y, &vector2.z - 0.5f);
            *(&(vectorArray[4])) = new Vector3(&vector2.x, &vector2.y + controller.Height, &vector2.z);
            num = 99999f;
            num2 = 99999f;
            num3 = -99999f;
            num4 = -99999f;
            num5 = 0;
            goto Label_024C;
        Label_01D3:
            vector3 = RectTransformUtility.WorldToScreenPoint(Camera.get_main(), *(&(vectorArray[num5])));
            if (num < &vector3.x)
            {
                goto Label_0201;
            }
            num = &vector3.x;
        Label_0201:
            if (num2 < &vector3.y)
            {
                goto Label_0218;
            }
            num2 = &vector3.y;
        Label_0218:
            if (num3 > &vector3.x)
            {
                goto Label_022F;
            }
            num3 = &vector3.x;
        Label_022F:
            if (num4 > &vector3.y)
            {
                goto Label_0246;
            }
            num4 = &vector3.y;
        Label_0246:
            num5 += 1;
        Label_024C:
            if (num5 < ((int) vectorArray.Length))
            {
                goto Label_01D3;
            }
            num6 = num3 - num;
            num7 = num4 - num2;
            flownode_mask.SetupMaskSize(num6, num7 * 1.5f);
        Label_0273:
            return;
        }

        private unsafe void SetupMask_ExecNormalAttack(FlowNode_TutorialMask flownode_mask)
        {
            RectTransform transform;
            Vector2 vector;
            Rect rect;
            Vector3 vector2;
            Rect rect2;
            Vector3 vector3;
            if (flownode_mask.ComponentId == 5)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow.OKButton != null) == null)
            {
                goto Label_00F0;
            }
            transform = SceneBattle.Instance.BattleUI.CommandWindow.OKButton.get_transform() as RectTransform;
            &vector..ctor(&transform.get_rect().get_width() * &transform.get_lossyScale().x, &transform.get_rect().get_height() * &transform.get_lossyScale().y);
            flownode_mask.Setup(5, transform.get_position(), 0, null);
            flownode_mask.SetupMaskSize(&vector.x, &vector.y);
        Label_00F0:
            return;
        }

        private unsafe void SetupMask_MoveUnit(TacticsUnitController controller, FlowNode_TutorialMask flownode_mask)
        {
            FlowNode_TutorialMask[] maskArray;
            int num;
            Vector3 vector;
            Vector3[] vectorArray;
            float num2;
            float num3;
            float num4;
            float num5;
            int num6;
            Vector2 vector2;
            float num7;
            float num8;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            maskArray = base.GetComponents<FlowNode_TutorialMask>();
            num = 0;
            goto Label_0225;
        Label_000E:
            if (maskArray[num].ComponentId == null)
            {
                goto Label_0020;
            }
            goto Label_0221;
        Label_0020:
            if ((controller != null) == null)
            {
                goto Label_0221;
            }
            if ((maskArray[num] != null) == null)
            {
                goto Label_0221;
            }
            &vector..ctor(&controller.CenterPosition.x + ((float) this.iVal1), &controller.CenterPosition.y - (controller.Height / 2f), &controller.CenterPosition.z);
            maskArray[num].Setup(0, vector, 1, null);
            vectorArray = new Vector3[4];
            *(&(vectorArray[0])) = new Vector3(&vector.x - 0.5f, &vector.y, &vector.z - 0.5f);
            *(&(vectorArray[1])) = new Vector3(&vector.x - 0.5f, &vector.y, &vector.z + 0.5f);
            *(&(vectorArray[2])) = new Vector3(&vector.x + 0.5f, &vector.y, &vector.z + 0.5f);
            *(&(vectorArray[3])) = new Vector3(&vector.x + 0.5f, &vector.y, &vector.z - 0.5f);
            num2 = 99999f;
            num3 = 99999f;
            num4 = -99999f;
            num5 = -99999f;
            num6 = 0;
            goto Label_01FD;
        Label_0182:
            vector2 = RectTransformUtility.WorldToScreenPoint(Camera.get_main(), *(&(vectorArray[num6])));
            if (num2 < &vector2.x)
            {
                goto Label_01B2;
            }
            num2 = &vector2.x;
        Label_01B2:
            if (num3 < &vector2.y)
            {
                goto Label_01C9;
            }
            num3 = &vector2.y;
        Label_01C9:
            if (num4 > &vector2.x)
            {
                goto Label_01E0;
            }
            num4 = &vector2.x;
        Label_01E0:
            if (num5 > &vector2.y)
            {
                goto Label_01F7;
            }
            num5 = &vector2.y;
        Label_01F7:
            num6 += 1;
        Label_01FD:
            if (num6 < ((int) vectorArray.Length))
            {
                goto Label_0182;
            }
            num7 = num4 - num2;
            num8 = num5 - num3;
            maskArray[num].SetupMaskSize(num7, num8);
        Label_0221:
            num += 1;
        Label_0225:
            if (num < ((int) maskArray.Length))
            {
                goto Label_000E;
            }
            return;
        }

        private unsafe void SetupMask_NormalAttackDesc(FlowNode_TutorialMask flownode_mask)
        {
            RectTransform transform;
            Vector2 vector;
            string str;
            Rect rect;
            Vector3 vector2;
            Rect rect2;
            Vector3 vector3;
            if (flownode_mask.ComponentId == 2)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_00FC;
            }
            if ((SceneBattle.Instance.BattleUI != null) == null)
            {
                goto Label_00FC;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow != null) == null)
            {
                goto Label_00FC;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow.AttackButton != null) == null)
            {
                goto Label_00FC;
            }
            transform = SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.get_transform() as RectTransform;
            &vector..ctor(&transform.get_rect().get_width() * &transform.get_lossyScale().x, &transform.get_rect().get_height() * &transform.get_lossyScale().y);
            str = LocalizedText.Get("sys.TUTORIAL_NORMAL_ATTACK");
            flownode_mask.Setup(2, transform.get_position(), 0, str);
            flownode_mask.SetupMaskSize(&vector.x, &vector.y);
        Label_00FC:
            return;
        }

        private unsafe void SetupMask_SelectDir(TacticsUnitController controller, FlowNode_TutorialMask flownode_mask)
        {
            Vector3 vector;
            Vector3[] vectorArray;
            float num;
            float num2;
            float num3;
            float num4;
            int num5;
            Vector2 vector2;
            float num6;
            float num7;
            DirectionArrow[] arrowArray;
            int num8;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            Vector3 vector6;
            if (flownode_mask.ComponentId == 6)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            &vector..ctor(&controller.CenterPosition.x + ((float) this.iVal1), &controller.CenterPosition.y - (controller.Height / 2f), &controller.CenterPosition.z);
            vectorArray = new Vector3[4];
            *(&(vectorArray[0])) = new Vector3(&vector.x - 0.5f, &vector.y, &vector.z - 0.5f);
            *(&(vectorArray[1])) = new Vector3(&vector.x - 0.5f, &vector.y, &vector.z + 0.5f);
            *(&(vectorArray[2])) = new Vector3(&vector.x + 0.5f, &vector.y, &vector.z + 0.5f);
            *(&(vectorArray[3])) = new Vector3(&vector.x + 0.5f, &vector.y, &vector.z - 0.5f);
            num = 99999f;
            num2 = 99999f;
            num3 = -99999f;
            num4 = -99999f;
            num5 = 0;
            goto Label_01BE;
        Label_0147:
            vector2 = RectTransformUtility.WorldToScreenPoint(Camera.get_main(), *(&(vectorArray[num5])));
            if (num < &vector2.x)
            {
                goto Label_0175;
            }
            num = &vector2.x;
        Label_0175:
            if (num2 < &vector2.y)
            {
                goto Label_018A;
            }
            num2 = &vector2.y;
        Label_018A:
            if (num3 > &vector2.x)
            {
                goto Label_01A1;
            }
            num3 = &vector2.x;
        Label_01A1:
            if (num4 > &vector2.y)
            {
                goto Label_01B8;
            }
            num4 = &vector2.y;
        Label_01B8:
            num5 += 1;
        Label_01BE:
            if (num5 < ((int) vectorArray.Length))
            {
                goto Label_0147;
            }
            num6 = num3 - num;
            num7 = num4 - num2;
            arrowArray = Object.FindObjectsOfType<DirectionArrow>();
            num8 = 0;
            goto Label_0223;
        Label_01E3:
            if (arrowArray[num8].Direction != null)
            {
                goto Label_021D;
            }
            vector3 = arrowArray[num8].get_transform().get_position();
            flownode_mask.Setup(6, vector3, 1, null);
            flownode_mask.SetupMaskSize(num6, num7);
            goto Label_022E;
        Label_021D:
            num8 += 1;
        Label_0223:
            if (num8 < ((int) arrowArray.Length))
            {
                goto Label_01E3;
            }
        Label_022E:
            return;
        }

        private unsafe void SetupMask_TapNormalAttack(FlowNode_TutorialMask flownode_mask)
        {
            RectTransform transform;
            Vector2 vector;
            Rect rect;
            Vector3 vector2;
            Rect rect2;
            Vector3 vector3;
            if (flownode_mask.ComponentId == 4)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow != null) == null)
            {
                goto Label_00F0;
            }
            if ((SceneBattle.Instance.BattleUI.CommandWindow.AttackButton != null) == null)
            {
                goto Label_00F0;
            }
            transform = SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.get_transform() as RectTransform;
            &vector..ctor(&transform.get_rect().get_width() * &transform.get_lossyScale().x, &transform.get_rect().get_height() * &transform.get_lossyScale().y);
            flownode_mask.Setup(4, transform.get_position(), 0, null);
            flownode_mask.SetupMaskSize(&vector.x, &vector.y);
        Label_00F0:
            return;
        }

        private void Trigger()
        {
            if (base.get_enabled() == null)
            {
                goto Label_0025;
            }
            base.ActivateOutputLinks(10);
            base.set_enabled((this.Flags & 2) == 0);
        Label_0025:
            return;
        }

        private void TriggerIf(bool b)
        {
            if (b == null)
            {
                goto Label_000C;
            }
            this.Trigger();
        Label_000C:
            return;
        }

        [Flags]
        public enum ActivateFlags
        {
            AutoActivate = 1,
            AutoDeactivate = 2
        }

        public enum TriggerTypes
        {
            None = 0,
            UnitStart = 10,
            UnitSkillStart = 11,
            UnitMoveStart = 12,
            UnitSkillEnd = 13,
            UnitEnd = 14,
            TargetChange = 15,
            HotTargetsChange = 0x10,
            UnitSelectDirection = 0x11,
            MapStart = 0x12,
            FinishCameraUnitFocus = 0x13,
            CloseBattleInfo = 20,
            MapEnd = 0x15,
            SkipBattleExplain = 100
        }
    }
}

