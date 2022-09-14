using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChange;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.T))
        {
            TakeDamage(10);
        }*/
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if(OnHealthChange != null)
        {
            OnHealthChange(maxHealth, currentHealth);
        }
        if( currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // die in some way
        // override as needed
        Debug.Log(transform.name + " died!");
    }
}
