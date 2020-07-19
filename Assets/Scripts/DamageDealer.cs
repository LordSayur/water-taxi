using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int _damage = 100;

    public int GetDamage()
    {
        return _damage;
    }

    public void Hit()
    {

    }
}
