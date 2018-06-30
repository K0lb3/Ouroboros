namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraMove : AnimEvent
    {
        public eCenterType CenterType;
        public eDistanceType DistanceType;

        public CameraMove()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnStart(GameObject go)
        {
            TacticsUnitController controller;
            SceneBattle battle;
            Vector3 vector;
            float num;
            List<Vector3> list;
            TacticsUnitController controller2;
            List<TacticsUnitController>.Enumerator enumerator;
            eCenterType type;
            eDistanceType type2;
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            vector = Vector3.get_zero();
            num = GameSettings.Instance.GameCamera_SkillCameraDistance;
            list = new List<Vector3>();
            switch (this.CenterType)
            {
                case 0:
                    goto Label_005D;

                case 1:
                    goto Label_0080;

                case 2:
                    goto Label_005D;
            }
            goto Label_00CC;
        Label_005D:
            list.Add(controller.CenterPosition);
            if (this.CenterType != 2)
            {
                goto Label_00CC;
            }
            goto Label_0080;
            goto Label_00CC;
        Label_0080:
            enumerator = controller.GetSkillTargets().GetEnumerator();
        Label_008D:
            try
            {
                goto Label_00A9;
            Label_0092:
                controller2 = &enumerator.Current;
                list.Add(controller2.CenterPosition);
            Label_00A9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0092;
                }
                goto Label_00C7;
            }
            finally
            {
            Label_00BA:
                ((List<TacticsUnitController>.Enumerator) enumerator).Dispose();
            }
        Label_00C7:;
        Label_00CC:
            battle.GetCameraTargetView(&vector, &num, list.ToArray());
            switch (this.DistanceType)
            {
                case 0:
                    goto Label_00FD;

                case 1:
                    goto Label_010D;

                case 2:
                    goto Label_011D;
            }
            goto Label_012D;
        Label_00FD:
            num = GameSettings.Instance.GameCamera_SkillCameraDistance;
            goto Label_012D;
        Label_010D:
            num = GameSettings.Instance.GameCamera_DefaultDistance;
            goto Label_012D;
        Label_011D:
            num = GameSettings.Instance.GameCamera_MoreFarDistance;
        Label_012D:
            battle.InterpCameraTarget(vector);
            battle.InterpCameraDistance(num);
            return;
        }

        public enum eCenterType
        {
            Self,
            Targets,
            All
        }

        public enum eDistanceType
        {
            Skill,
            Far,
            MoreFar,
            Auto
        }
    }
}

