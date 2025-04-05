using UnityEngine;
using TMPro;

public class Collector : MonoBehaviour
{
    public int coins = 0;
    [SerializeField] TextMeshProUGUI score;

    private void Start()
    {
        coins = 0;
    }

    void Update()
    {
        score.text = coins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            item.Collect();
            coins++;
            
        }
    }
}
