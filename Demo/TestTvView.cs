using UnityEngine;
using System.Collections;
using TVController;
using System;
using UnityEngine.UI;

public class TestTvView : TVBehaviour {

    private Text text;

    new void Awake() {
        text = GetComponentInChildren<Text>();
        base.Awake();
    }

    internal override void focus(bool b) {
        base.focus(b);
        text.text = b ? "selected" : "unSelected";
    }

    public override bool onDirection(Direction d) {
        return true;
    }
}
