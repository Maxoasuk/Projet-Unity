using System;
using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Vector2 knockback;
    [SerializeField] private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((LayerMask.GetMask("Mob") & (1 << other.gameObject.layer)) > 0)
        {
            other.gameObject.SendMessage("TakeDamage", damage);
            StartCoroutine(Knockback(other.gameObject));
        }
    }

    private IEnumerator Knockback(GameObject target)
    {
        float duration = 0.05f; // Durée du recul
        float timer = 0;

        while (timer < duration)
        {
            target.transform.Translate(knockback * (25.0f * Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
    
    public Vector2 GetKnockback() { return knockback; }
    public float GetDamage() { return damage; }
}
