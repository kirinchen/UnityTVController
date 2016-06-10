using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace TVController {
    public abstract  class TVBehaviour : MonoBehaviour {
        private TVSystem tvSys;
        public TVBehaviour top, bottom, left, right;
        private bool focused = false;
        public Dictionary<string, TVBehaviour> tb;

        public void Awake() {
            tvSys = FindObjectOfType<TVSystem>();
            focus(false);
        }

        public TVBehaviour getByDirection(Direction d) {
            if (onDirection(d)) {
                TVBehaviour ans = getNextTVBehaviourByDirection(d);
                if (ans == null) {
                    return findCustomTVBehaviour();
                } else {
                    return ans;
                }
            } else {
                return null;
            }
        }

        public virtual TVBehaviour findCustomTVBehaviour() {
            return null;
        }

        public abstract bool onDirection(Direction d);

        public virtual TVBehaviour getNextTVBehaviourByDirection(Direction d) {
            switch (d) {
                case Direction.Up:
                    return top;
                case Direction.Down:
                    return bottom;
                case Direction.Left:
                    return left;
                case Direction.Right:
                    return right;
            }
            throw new Exception("it`s imposible d="+d);

        }

        internal virtual void onLeaveFocus(Action focusAction) {
            focusAction();
        }

        internal virtual void focus(bool b) {
            focused = b;
        }

        internal virtual void onFocus(Action focusAction) {
            focusAction();
        }
    }
}
