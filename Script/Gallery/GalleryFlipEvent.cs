using UnityEngine;
using System.Collections;

public class GalleryFlipEvent : MonoBehaviour {

    private Gallery gallery = null;

    private Gallery getGallery() {
        if (gallery == null) {
            gallery = GetComponentInParent<Gallery>();
        }
        return  gallery;
    }

    public void onMouseDown() {
        getGallery().onMouseDown();
    }

    public void onMouseMove() {
        getGallery().onMouseMove();
    }

    public void onMouseUp() {
        getGallery().onMouseUp();
    }


}
