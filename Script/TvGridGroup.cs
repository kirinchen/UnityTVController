using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TVController {
    [RequireComponent(typeof(GridLayoutGroup))]
    public class TvGridGroup : TVBehaviour {
        public bool horizontalEndRepeatable = true;
        public bool verticalEndRepeatable = true;
        public bool defaultSelected = true;
        private GridLayoutGroup gridLayout;
        private RectTransform rectTram;
        public int horizontalCount {
            get; private set;
        }
        private List<List<TvGrid>> gridMtx = new List<List<TvGrid>>();
        public GridLoc currentIndex {
            get; private set;
        }

        new public void Awake() {
            base.Awake();
            currentIndex = GridLoc.zero();
            gridLayout = GetComponent<GridLayoutGroup>();
            rectTram = GetComponent<RectTransform>();
        }

        public void Start() {
            loadGrids();
            if (defaultSelected) {
                showCurrentSelected(true);
            }
        }

        /*internal override void focus(bool b) {
            base.focus(b);
            try {
                showCurrentSelected(true);
            } catch (Exception e) {
            }
        }*/

        internal override void onFocus(Action focusAction) {
            base.onFocus(focusAction);
            try {
                showCurrentSelected(true);
            } catch (Exception e) {
            }
        }

        public void loadGrids() {
            horizontalCount = getRowCount();
            TvGrid[] tgs = GetComponentsInChildren<TvGrid>();
            horizontalCount = horizontalCount > tgs.Length ? tgs.Length : horizontalCount;
            List<TvGrid> tga = new List<TvGrid>();
            for (int i = 0; i < tgs.Length; i++) {
                TvGrid tg = tgs[i];
                tga.Add(tg);
                if (tga.Count >= horizontalCount || i == tgs.Length - 1) {
                    gridMtx.Add(tga);
                    tga = new List<TvGrid>();
                }
                tg.showSelected(false);
            }

        }

        private int getRowCount() {
            if (gameObject.name.Equals("TeacherSelect")) {
                Debug.Log("Test");
            }
            float t_s = rectTram.rect.width - gridLayout.spacing.x;
            float wPluss = gridLayout.cellSize.x + gridLayout.spacing.x;
            return Convert.ToInt16(Math.Floor(t_s / wPluss));
        }

        public override bool onDirection(Direction d) {
            GridLoc orGLoc = currentIndex.clone();
            try {
                moveSelect(d);
                return false;
            } catch (Exception e) {
                if (getNextTVBehaviourByDirection(d) != null) {
                    currentIndex = orGLoc;
                    return true;
                } else if (!DirectionF.i(d).isVertical() && horizontalEndRepeatable) {
                    handleRepeatEnd(d);
                    return false;
                } else if (DirectionF.i(d).isVertical() && verticalEndRepeatable) {
                    handleRepeatEnd(d);
                    return false;
                } else {
                    return handleOther(d, orGLoc, currentIndex);
                }
            }
        }

        internal virtual bool handleOther(Direction d, GridLoc orGLoc, GridLoc _currentIndex) {
            currentIndex = orGLoc.clone();
            showCurrentSelected(true);
            return true;
        }

        private void handleRepeatEnd(Direction d) {
            switch (d) {
                case Direction.Down:
                    currentIndex.y = 0;
                    break;
                case Direction.Up:
                    currentIndex.y = gridMtx.Count - 1;
                    break;
                case Direction.Left:
                    currentIndex.x = horizontalCount - 1;
                    break;
                case Direction.Right:
                    currentIndex.x = 0;
                    break;
            }
            showCurrentSelected(true);
        }

        private void moveSelect(Direction d) {
            showCurrentSelected(false);
            shift(d);
            showCurrentSelected(true);
        }

        public void setCurrentSelected(GridLoc loc) {
            showCurrentSelected(false);
            currentIndex = loc;
            showCurrentSelected(true);
        }

        private void shift(Direction d) {
            switch (d) {
                case Direction.Down:
                    currentIndex.y += 1;
                    break;
                case Direction.Up:
                    currentIndex.y -= 1;
                    break;
                case Direction.Left:
                    currentIndex.x -= 1;
                    break;
                case Direction.Right:
                    currentIndex.x += 1;
                    break;
            }
        }

        internal void showCurrentSelected(bool s) {
            get(currentIndex).showSelected(s);
        }

        public TvGrid get(GridLoc v) {
            return gridMtx[v.y][v.x];
        }

        public GridLoc getSize() {
            return new GridLoc(gridMtx[0].Count, gridMtx.Count);
        }

        public override void click() {
            get(currentIndex).click();
        }
    }
}
