using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILifes : MonoBehaviour {

    [SerializeField] ScriptablePlayer _player;
    [SerializeField] List<Animator> _imgs;
    
	void Update () {
        int c = 0;
	    foreach(Animator i in _imgs)
        {
            i.SetBool("Enabled", c < _player._lifes);
            c++;
        }
	}
}
