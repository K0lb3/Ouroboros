namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    public struct WWWResult
    {
        private WWW mResult;
        private string mResultValue;
        public WWWResult(WWW www)
        {
            this.mResult = www;
            this.mResultValue = null;
            return;
        }

        public WWWResult(string result)
        {
            this.mResult = null;
            this.mResultValue = result;
            return;
        }

        public string text
        {
            get
            {
                return ((this.mResult == null) ? this.mResultValue : this.mResult.get_text());
            }
        }
    }
}

