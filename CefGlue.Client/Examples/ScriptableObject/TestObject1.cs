namespace CefGlue.Client.Examples.ScriptableObject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    // TODO: drop this, make normal jsobject feature samples
    internal sealed class TestObject1
    {
        public bool Enabled { get; set; }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        [JSBind]
        private void ScriptOnlyMethod()
        {
        }

        [JSBind(false)]
        public void HostOnlyMethod()
        {
        }
    }
}
