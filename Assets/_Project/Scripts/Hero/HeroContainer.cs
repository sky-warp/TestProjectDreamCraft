using UnityEngine;

public class HeroContainer : MonoBehaviour
{
    [field: SerializeField] 
    public HeroMovement HeroMovement { get; private set; }
    
    [field: SerializeField] 
    public HeroAttack HeroAttack { get; private set; }
    
    [field: SerializeField]
    public Health Health { get; private set; }
}