using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prize", menuName = "ScriptableObjects/Prize", order = 1)]
public class Prize : ScriptableObject
{
    [SerializeField] PrizeOptions prizeOption;
    [SerializeField] string prizeName;
    [SerializeField] Sprite prizeSprite;

    [Header("Only assign for food")]
    [SerializeField] float hungerChange;
    [SerializeField] float happinessChange;

    public Sprite PrizeSprite => prizeSprite;

    public string PrizeName => prizeName;

    public PrizeOptions PrizeOption => prizeOption;

    public float HungerChange => hungerChange;

    public float HappinessChange => happinessChange;
}
