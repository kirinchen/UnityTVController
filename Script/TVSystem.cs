using UnityEngine;
using System.Collections;
using System;

namespace TVController {
    public class TVSystem : MonoBehaviour {

        private static TVSystem instance = null;

        public TVBehaviour currentItem;
        private bool _moveing = false;

        void Awake() {
            instance = this;
        }

        void Start() {
            setInitTvView();
        }

        public void setInitTvView(TVBehaviour t = null) {
            currentItem = t == null ? currentItem : t;
            if (currentItem != null) {
                setAllUnFocus();
                currentItem.focus(true);
            }
        }

        private void setAllUnFocus() {
            foreach (TVBehaviour b in FindObjectsOfType<TVBehaviour>()) {
                b.focus(false);
            }
        }

        void Update() {
            if (currentItem != null) {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                move(moveHorizontal, moveVertical);
                if (Input.GetKeyUp(KeyCode.G)) {
                    currentItem.click();
                }

            }
        }

        private void move(float h, float v) {
            if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) {
                if (!_moveing) {
                    _moveing = true;
                    Direction d = getDirection(h, v);
                    TVBehaviour nextF = currentItem.getByDirection(d);
                    if (nextF != null) {
                        changeFocus(d,nextF);
                    }
                }
            } else {
                _moveing = false;
            }
        }

        private Direction getDirection(float h, float v) {
            if (Mathf.Abs(h) > Mathf.Abs(v)) {
                return h > 0 ? Direction.Right : Direction.Left;
            } else {
                return v > 0 ? Direction.Up : Direction.Down;
            }
        }

        public void changeFocus(Direction d, TVBehaviour nextF) {
            currentItem.onLeaveFocus(() => {
                currentItem.focus(false);
            });
            currentItem = nextF;
            nextF.onFocus(() => {
                nextF.focus(true);
            },d);
        }

        public static TVSystem getInstance() {
            return instance;
        }

    }
}
