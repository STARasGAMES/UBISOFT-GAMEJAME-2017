using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILifes : MonoBehaviour {

    [SerializeField] ScriptablePlayer _player;
    [SerializeField] List<Image> _imgs;
    
	void Update () {
        int c = 0;
	    foreach(Image i in _imgs)
        {
            i.enabled = c < _player._lifes;
            c++;
        }
	}
}
