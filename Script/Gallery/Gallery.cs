using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Gallery : MonoBehaviour {
    private RectTransform rectTran;
    public RectTransform[] contents;
    private float _lastPosX,_startPosX;
    private float leftStopX, rightStopX;
    public Action<float> moveedAction = (float f) => { };
    private SiftRunInterruper sri = new SiftRunInterruper();

    void Awake() {
        rectTran = GetComponent<RectTransform>();
        
        leftStopX = 0;
        rightStopX = - (contents.Length-1) * rectTran.rect.width;
    }

    void Start() {
        for (int i=0;i<contents.Length;i++) {
            float posX = i * rectTran.rect.width;
            RectTransform t = createContent(contents[i],posX);
            contents[i] = t;
        }
    }

    private RectTransform createContent(RectTransform reT,float posX) {
        RectTransform t = null;
        if (reT.parent != transform) {
            t = Instantiate(reT, Vector3.zero, Quaternion.identity) as RectTransform;
        } else {
            t = reT;
        }
        t.transform.parent = transform;
        t.localScale = new Vector3(1, 1, 1);
        t.localPosition = Vector3.zero;
       // t.offsetMax = Vector2.zero;
        //t.offsetMin = Vector2.zero;
        t.anchoredPosition = new Vector2(posX, 0);
        return t;
    }

    internal Transform getChild(int i) {
        return contents[i];
    }

    public void onMouseDown() {
        _lastPosX =_startPosX = getMouseX();
    }

    public void onMouseMove() {
        float mx = getMouseX();
        float d = mx - _lastPosX;
        movePosX(d);
        _lastPosX =mx;
    }

    public void onMouseUp() {
        Predicate<float> left = (float f) => { return f >= leftStopX; };
        Predicate<float> right = (float f) => {return f <= rightStopX;};
        if (!setupStop(left, leftStopX) && !setupStop(right,rightStopX)){
            float shiftX = getMouseX() - _startPosX;
            moveedAction(shiftX);
        }
        
    }

    private bool setupStop(Predicate<float> p,float wish) {
        if (p(getPosX())) {
            StartCoroutine(runShiftTo(wish));
            return true;
        } else {
            return false;
        }
    }

    internal IEnumerator runShiftTo(float posX) {
        float d = Mathf.Abs(posX - getPosX());
        SiftRunInterruper.InterrupKey key = null;
        while ((key =sri.ask()) == null) {
            yield return new WaitForSeconds(0.01f);
        }
        while (d > 5f && key.go) {
            float shiftD = (posX - getPosX()) * 0.25f;
            movePosX(shiftD);
            d = Mathf.Abs(posX - getPosX());
            yield return new WaitForSeconds(0.02f);
        }
        setPosX(posX);
        sri.remove(key.id);
    }


    private float getMouseX() {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v = transform.worldToLocalMatrix.MultiplyVector(v);
        return v.x;
    }

    public void movePosX(float d) {
        rectTran.anchoredPosition = new Vector2(getPosX()+d, rectTran.anchoredPosition.y);
    }

    internal void setPosX(float posX) {
        rectTran.anchoredPosition = new Vector2(posX, rectTran.anchoredPosition.y);
    }

    private float getPosX() {
        return rectTran.anchoredPosition.x;
    }

    internal float getPosXByPage(int i) {
        return i * rectTran.rect.width * -1;
    }



}
