using UnityEngine;

public class AttackLifetime : MonoBehaviour
{
    [SerializeField] float attackLifetime;
    float timer = 0;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > attackLifetime)
        {
            Object.Destroy(gameObject);
        }
    }
}
