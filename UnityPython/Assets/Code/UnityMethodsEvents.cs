using System;
using UnityEngine;

namespace UnityPython.Assets.Code {
    public class UnityMethodsEvents : MonoBehaviour {
        public event EventHandler update;
        public event EventHandler fixedUpdate;
        
        private void Update() {
            if (update != null)
                update.Invoke(this, EventArgs.Empty);
        }

        private void FixedUpdate() {
            if (fixedUpdate != null)
                fixedUpdate.Invoke(this, EventArgs.Empty);
        }
    }
}
