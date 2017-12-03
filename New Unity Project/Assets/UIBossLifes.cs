using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossLifes : MonoBehaviour {

    [SerializeField] Image _image;
    [SerializeField] Boss _boss;
    [SerializeField] float _changeSpeed;

	void Update () {
        _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, ((float)_boss.lifes) / _boss.maxLifes, _changeSpeed*Time.deltaTime);
    }
}
