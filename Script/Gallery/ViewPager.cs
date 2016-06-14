using UnityEngine;
using System.Collections;
using System;

public class ViewPager : MonoBehaviour {
    private static readonly float SHIFT_PAGE_MAX_DISTANCE = 50f;
    private Gallery self;
    public int currentIndex {
        get;private set;
    }
    public Action<int, int> onPageChnage = (c, m) => { };

    void Awake() {
        self = GetComponent<Gallery>();
        self.moveedAction = onMoved;
    }

    internal void setPage(int page) {
        if (page >= 0 && page < self.contents.Length) {
            currentIndex = page;
            sync();
            onPageChnage(currentIndex, self.contents.Length);
        }
    }

    internal void next() {
        setPage(currentIndex + 1);
    }

    internal void previous() {
        setPage(currentIndex - 1);
    }

    public Transform getPageView(int i) {
        return self.getChild(i);
    }

    private void sync() {
        float x = self.getPosXByPage(currentIndex);
        StartCoroutine(self.runShiftTo(x));
    }

    private void onMoved(float shiftX) {
        if (Mathf.Abs(shiftX) > SHIFT_PAGE_MAX_DISTANCE) {
            if (shiftX < 0) {
                next();
            } else {
                previous();
            }
        } else {
            sync();
        }
    }

    internal int getCount() {
        return self.contents.Length;
    }
}
