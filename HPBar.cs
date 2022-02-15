using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    
    private PlayerStat _playerStat;

    private Slider _hpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerStat = (PlayerStat) GameManager.Instance.player.characterStat;

        _hpBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _hpBar.value = _playerStat.CurrentHealth / (float) _playerStat.MaxHealth;
        hpText.text = _playerStat.CurrentHealth + "/" + _playerStat.MaxHealth;
    }
}
