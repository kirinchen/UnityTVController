using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace TVController {
    [RequireComponent(typeof(GridLayoutGroup))]
    public class TvGridGroup : TVBehaviour {

        private GridLayoutGroup gridLayout;
        private TvGrid[][] gridMtx;

        new void Awake() {
            gridLayout = GetComponent<GridLayoutGroup>();
        }

        public void loadGrids() {

        }

        public override bool onDirection(Direction d) {
            throw new NotImplementedException();
        }
    }
}
