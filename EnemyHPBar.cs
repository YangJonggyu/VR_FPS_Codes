using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Canvas canvas;
    public Enemy enemy;
    public Image hpBarBackground;
    public Image hpBarFill;

    private Player _player;
    private Slider _slider;
    private Color _hpBarBackgroundColor;
    private Color _hpBarFillColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = Player.Instance;
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.Normalize(transform.position - _player.transform.position);
        _slider.value = enemy.currentHealth / (float) enemy.characterStat.MaxHealth;
        
        var a = Vector3.Dot(_player.transform.forward.normalized, canvas.transform.forward.normalized);
        _hpBarBackgroundColor = hpBarBackground.color;
        _hpBarFillColor = hpBarFill.color;
        if (Mathf.Abs(a) > 0.97f)
        {
            _hpBarBackgroundColor.a = 1f;
            hpBarBackground.color = _hpBarBackgroundColor;
            _hpBarFillColor.a = 1f;
            hpBarFill.color = _hpBarFillColor;
        }
        else
        {
            _hpBarBackgroundColor.a = 0f;
            hpBarBackground.color = _hpBarBackgroundColor;
            _hpBarFillColor.a = 0f;
            hpBarFill.color = _hpBarFillColor;
        }

    }
}
