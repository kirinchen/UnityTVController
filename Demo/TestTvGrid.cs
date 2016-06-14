using UnityEngine;
using System.Collections;
using TVController;
using System;
using UnityEngine.UI;

public class TestTvGrid : TvGrid {

    private Image img;

    void Awake() {
        img = GetComponent<Image>();
    }

    public override void showSelected(bool b) {
        img.color = b ? Color.green : Color.blue;
    }

}
