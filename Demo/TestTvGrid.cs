using UnityEngine;
using System.Collections;
using TVController;
using System;
using UnityEngine.UI;

public class TestTvGrid : TvGrid {

    public TabStyleSet selectedStyle;
    public TabStyleSet unselectedStyle;
    private Image img;
    private Text text;
    public Action onClick;

    void Awake() {
        img = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    public override void showSelected(bool b) {
        img.color = b ? selectedStyle.imgColor : unselectedStyle.imgColor;
        text.color = b ? selectedStyle.textColor : unselectedStyle.textColor;
    }

    public override void click() {
        if (onClick != null) {
            onClick();
        }
    }
}
