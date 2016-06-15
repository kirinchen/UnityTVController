using UnityEngine;
using System.Collections;
namespace TVController {
    public class GridLoc  {

        public int x,y;

        public GridLoc(int x,int y) {
            this.x = x;
            this.y = y;
        }

        public GridLoc clone() {
            return new GridLoc(x,y);
        }

        public static GridLoc zero() {
            return new GridLoc(0,0);
        }



    }
}
