namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.CrashLog;

    public class UnityPerformanceReporting : MonoBehaviour
    {
        public UnityPerformanceReporting()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            string str;
            str = "8d9b4183-a378-4c53-b66a-b5ac3d9a531a";
            CrashReporting.Init(str, MyApplicationPlugin.get_version(), string.Empty);
            return;
        }
    }
}

