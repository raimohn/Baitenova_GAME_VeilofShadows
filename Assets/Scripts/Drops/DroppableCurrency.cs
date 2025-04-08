using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroppableCurrency : MonoBehaviour, ICollectable
{
    private bool collected;

    private void OnEnable()
    {
        collected = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect(Player player)
    {
        if (collected)
            return;

        collected = true;

        StartCoroutine(MoveTowardsPlayer(player));
    }

    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;
        Vector2 initialPosition = transform.position;

        while (timer < 1)
        {
            Vector2 targetPosition = player.GetCenter();

            transform.position = Vector2.Lerp(initialPosition, targetPosition, timer);

            timer += Time.deltaTime;
            yield return null;
        }

        Collected();
    }

    protected abstract void Collected();
}
