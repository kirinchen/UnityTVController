using UnityEngine;
using System.Collections;

public class SiftRunInterruper  {

    private InterrupKey key;
    private int _currentId = 0;

    internal InterrupKey ask() {
        if (key == null) {
            key = new InterrupKey();
            key.go = true;
            key.id = _currentId ++;
            return key;
        } else {
            key.go = false;
            return null;
        }
    }

    internal void remove(int id) {
        if(id != key.id) {
            throw new System.Exception("this is id not match id="+id+" key.id="+key.id);
        }
        key = null;
    }

    public class InterrupKey {
        public bool go;
        public int id;
    }

}
