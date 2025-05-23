using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    public void Animate(int damage, bool isCriticalHit)
    {
        damageText.text = damage.ToString();
        damageText.color = isCriticalHit ? Color.red : Color.black;

        animator.Play("Animate");
    }
}
