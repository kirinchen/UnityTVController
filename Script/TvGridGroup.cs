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
        private GridLayoutGroup gridLayout;
        private RectTransform rectTram;
        public int horizontalCount {
            get;private set;
        }
        private List<TvGrid[]> gridMtx = new List<TvGrid[]>();
        private GridLoc currentIndex = GridLoc.zero();

        new public void Awake() {
            base.Awake();
            gridLayout = GetComponent<GridLayoutGroup>();
            rectTram = GetComponent<RectTransform>();
        }

        new public void Start() {
            loadGrids();
        }

        /*internal override void focus(bool b) {
            base.focus(b);
            showCurrentSelected(true);
        }*/

        public void loadGrids() {
            horizontalCount = getRowCount();
            TvGrid[] tgs = GetComponentsInChildren<TvGrid>();
            TvGrid[] tga = new TvGrid[horizontalCount];
            int ri = 0;
            foreach (TvGrid tg in tgs) {
                tga[ri++] = tg;
                if (ri >= horizontalCount) {
                    gridMtx.Add(tga);
                    ri = 0;
                    tga = new TvGrid[horizontalCount];
                }
                tg.showSelected(false);
            }
            showCurrentSelected(true);
        }

        private int getRowCount() {
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
                if (!DirectionF.i(d).isVertical() && horizontalEndRepeatable) {
                    handleRepeatEnd(d);
                    return false;
                } else if (DirectionF.i(d).isVertical() && verticalEndRepeatable) {
                    handleRepeatEnd(d);
                    return false;
                } else {
                    return handleOther(d, orGLoc, currentIndex) ;
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
                    currentIndex.y =0;
                    break;
                case Direction.Up:
                    currentIndex.y = gridMtx.Count-1;
                    break;
                case Direction.Left:
                    currentIndex.x = horizontalCount -1;
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
            return new GridLoc(gridMtx[0].Length, gridMtx.Count);
        }

        public override void click() {
            get(currentIndex).click();
        }
    }
}
