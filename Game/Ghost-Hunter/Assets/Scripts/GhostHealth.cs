using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHealth : MonoBehaviour
{
    [Header("Label Setting")]
    public string Label;
    public Color LabelColor;
    public Font LabelFont;

    [Header("Color Settings")]
    public Color NegativeColor;
    public Color PositiveColor;
    public Color MaskColor;
    public Sprite BarBackGroundSprite;

    private Image _bar;
    private Image _barBackground;
    private Image _mask;
    private Text _txtTitle;
    private float _barValue;

    public float BarValue { get => _barValue; set => _barValue = value; }

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
