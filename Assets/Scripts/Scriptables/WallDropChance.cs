using UnityEngine;

[CreateAssetMenu(fileName = "WallDropChance", menuName = "ScriptableObjects/WallDropChance", order = 1)]
public class WallDropChance : ScriptableObject
{
    [Range(0,1)]
    [SerializeField] public float _buffChance;
    
    [Range(0,1)]
    [SerializeField] public float _dpsBuffChance;
}
