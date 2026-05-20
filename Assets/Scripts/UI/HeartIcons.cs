using UnityEngine;
using UnityEngine.UI;

public class HeartIcons : MonoBehaviour
{
    PlayerHealth ph;
    Image heartImage;
    public Sprite fullHeart, halfHeart, emptyHeart;

    private void Awake()
    {
        heartImage = GetComponentInChildren<Image>();
        ph = FindAnyObjectByType<PlayerHealth>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}