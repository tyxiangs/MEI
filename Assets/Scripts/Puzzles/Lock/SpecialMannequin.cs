using System.Collections;
using UnityEngine;

public class SpecialMannequin : Mannequin
{
    [SerializeField] private int id;
    [SerializeField] private Sprite completeMannequin;
    [SerializeField] private Sprite halfMannequin;
    [SerializeField] private GameObject mannequinBase;
    [SerializeField] private Item requiredItem;
    [SerializeField] private float upDistance = 0.4f;
    [SerializeField] private bool isEquipped = false;
    private bool allEquipped = false;

    public bool CheckCombo(int result)
    {
        return id == result;
    }

    public void SwapSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite == halfMannequin)
        {
            spriteRenderer.sprite = completeMannequin;
            isEquipped = true;
        }
        MannequinInteraction.Instance.DoneEquipped();
    }

    public bool IsEquipped()
    {
        return isEquipped;
    }

    // Handle item usage
    public override IEnumerator UseItem(Item item)
    {
        if (isEquipped)
        {
            Debug.Log($"{name} is already equipped.");
            yield break;
        }

        if (item == requiredItem)
        {
            Debug.Log("sameItem");
            Debug.Log($" Correct item used on {name}");
            SwapSprite();
            Inventory.Instance.Remove(item);
        }
        else
        {
            Debug.Log($" Wrong item used on {name}");
        }

        yield break;
    }

    // Optional interaction logic
    public override IEnumerator Interact()
    {
        //Debug.Log($" Interacting with {name}");

        // You can add additional behavior here:
        // - show dialogue
        // - emit particle effect
        // - toggle between half/completed state, etc.

        yield break;
    }

    public void AllEquippedSetter()
    {
        allEquipped = true;
    }

    public bool HandleClick()
    {
        if (allEquipped)
        {
            return true;
        }
        return false;

    }

    public void Rise()
    {
        mannequinBase.SetActive(true);
        StartCoroutine(RiseSmoothly());
    }

    private IEnumerator RiseSmoothly()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(0, upDistance, 0);
        float duration = 0.8f;  // Time in seconds to complete the rise
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;  // Ensure it ends exactly at target
    }
}