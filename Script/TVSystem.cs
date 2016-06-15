﻿using UnityEngine;
using System.Collections;
using System;

namespace TVController {
    public class TVSystem : MonoBehaviour {

        public TVBehaviour currentItem;
        private bool _moveing = false;

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
                    TVBehaviour nextF = currentItem.getByDirection(getDirection(h, v));
                    if (nextF != null) {
                        changeFocus(nextF);
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

        internal void changeFocus(TVBehaviour nextF) {
            currentItem.onLeaveFocus(() => {
                currentItem.focus(false);
            });
            nextF.onFocus(() => {
                nextF.focus(true);
            });
            currentItem = nextF;
        }


    }
}
