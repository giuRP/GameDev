using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private List<LifeElementUI> heartImages;

    [SerializeField]
    private Sprite fullHeart, emptyHeart;

    [SerializeField]
    private LifeElementUI heartPrefab;

    public void Initialize(int maxHealth)
    {
        heartImages = new List<LifeElementUI>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            var life = Instantiate(heartPrefab);
            life.transform.SetParent(transform, false);
            heartImages.Add(life);
        }
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].SetSprite(fullHeart);
            }
            else
            {
                heartImages[i].SetSprite(emptyHeart);
            }
        }
    }
}
