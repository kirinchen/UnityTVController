using UnityEngine;
using System.Collections;

public enum Direction  {

    Up,Down,Left,Right

}

public class DirectionF {

    private Direction d;

    public bool isVertical() {
        if (d == Direction.Up || d == Direction.Down) {
            return true;
        }else {
            return false;
        }
    }


    private DirectionF(Direction d) {
        this.d = d;
    }

    public static DirectionF i(Direction d) {
        return new DirectionF(d);
    }

}
