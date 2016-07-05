using UnityEngine;
using System.Collections;
using System;

namespace TVController {
    public abstract class FocusByPass : TVBehaviour {

        internal override void onFocus(Action focusAction, Direction d) {
            TVBehaviour v = findNexTVBehaviour(d);
            TVSystem.getInstance().changeFocus(d, v);
            base.onFocus(focusAction, d);
        }

        public override void click() {
        }

        public override bool onDirection(Direction d) {
            return true;
        }

        protected abstract  TVBehaviour findNexTVBehaviour(Direction d);
    }
}
