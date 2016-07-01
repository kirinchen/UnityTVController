using UnityEngine;
using System.Collections;
using TVController;
using System;

public class ViewPagerGridGroup : TvGridGroup {

    private ViewPager viewpager;

    public Func<Direction, TVBehaviour> findUpDownHandler = (d) => { return null; };
 
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
            } else {
                TVBehaviour tb = findUpDownHandler(d);
                if (tb != null) {
                    return tb;
                }
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
            if (viewpager.currentIndex >= c - 1) {
                base.handleOther(d, orGLoc, currentIndex);
                return false;
            } else {
                base.handleOther(d, orGLoc, currentIndex);
                return true;
            }
        } else if (d == Direction.Left) {
            if (viewpager.currentIndex <= 0) {
                base.handleOther(d, orGLoc, currentIndex);
                return false;
            } else {
                base.handleOther(d, orGLoc, currentIndex);
                return true;
            }
        } else {
            base.handleOther(d, orGLoc, currentIndex);
            return true;
        }

    }


}
