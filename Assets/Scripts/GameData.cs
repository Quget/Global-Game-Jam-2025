using JetBrains.Annotations;
using UnityEngine;

public class GameData
{
    private int health = 3;
    public int Health 
    {
        get {return health; }
    }
    public void GainHealth()
    {
        health += 1;
    }
    public void LoseHealth()
    {
        if (health != 0) {
            health -= 1;
        } else {
            GameOver();
        }
    }
    public void GameOver() 
    {
        // :) Oops we're dead
    }
    private int maxBubbles = 3;
    private int bubbleTimer = 5;
    private bool powerUpActive = true;
    public Bubble[] bubbles;
    //public InventoryItem[] inventory;

}
