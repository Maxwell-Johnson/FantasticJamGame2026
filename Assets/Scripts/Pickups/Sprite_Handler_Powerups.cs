using UnityEngine;

public class Sprite_Handler_Powerups : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite[] powerupSprites;
    private Sprite newSprite;




    public Sprite GrabSprite(int powerupNumber)
    {
        newSprite = powerupSprites[powerupNumber - 1];
            
        return newSprite;
    }
}
