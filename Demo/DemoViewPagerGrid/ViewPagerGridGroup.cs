using UnityEngine;
using System.Collections;
using TVController;
using System;

public class ViewPagerGridGroup : TvGridGroup {

    private ViewPager viewpager;

    new void Awake() {
        base.Awake();
        viewpager = GetComponentInParent<ViewPager>();
        findCustomTVBehaviour = findTvGrid;
    }


    public TVBehaviour findTvGrid(Direction d) {
        try {
            Transform t = null;
            if (d == Direction.Right) {
                t = viewpager.getPageView(viewpager.currentIndex + 1);
                viewpager.next();
            } else if (d == Direction.Left) {
                t = viewpager.getPageView(viewpager.currentIndex - 1);
                viewpager.previous();
            }
            ViewPagerGridGroup tpg = t.GetComponent<ViewPagerGridGroup>();

            return tpg;
        } catch (Exception e) {
            return null;
        }
    }

    internal override bool handleOther(Direction d,  GridLoc orGLoc, GridLoc currentIndex) {
        if (d == Direction.Right) {
            int c = viewpager.getCount();
            if (viewpager.currentIndex >= c-1) {
                currentIndex.x -= 1;
                showCurrentSelected(true);
                return false;
            } else {
                base.handleOther(d,orGLoc,currentIndex);
                return true;
            }
        } else {
            if (viewpager.currentIndex <= 0) {
                currentIndex.x += 1;
                showCurrentSelected(true);
                return false;
            } else {
                base.handleOther(d, orGLoc, currentIndex);
                return true;
            }
        }
        
    }


}
