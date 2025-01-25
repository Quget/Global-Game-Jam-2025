using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameData
{
    // maxBubbles: The max amount of bubbles that can be present in the game
    private int maxBubbles = 3;
    public int MaxBubbles
    {
        get {return maxBubbles; }
        set {maxBubbles = value; }
    }
    // bubbleTimer: The life span of a bubble in seconds
    private float bubbleTimer = 5;
    public float BubbleTimer
    {
        get {return bubbleTimer; }
        set {bubbleTimer = value; }
    }

    // bubbles: Array of the bubble objects currently in existence
    private List<Bubble> bubbles = new List<Bubble>();
    public List<Bubble> Bubbles
    {
        get {return bubbles;}
    }
    public void SpawnBubble(Bubble bubble)
    {
        {
            bubbles.Add(bubble);
        }
    }

    public bool CanSpawnBubble()
    {
        return (bubbles.Count < maxBubbles);
    }

    // health: Starting health of the player
    private int health = 3;
    public int Health 
    {
        get {return health; }
        set {health = value; }
    }
    public void GainHealth()
    {
        Debug.Log("GainHealth - Old health: " + health);
        health += 1;
        Debug.Log("GainHealth - New health: " + health);
    }
    public void LoseHealth()
    {
        Debug.Log("LoseHealth - Old health: " + health);
        if (health != 0) {
            health -= 1;
            Debug.Log("LoseHealth - New health: " + health);
        } else {
            Debug.Log("Health can't go lower than 0. Game-over.");
            GameOver();
        }
    }
    public void GameOver() 
    {
        // :) Oj kurwa, we're dead
    }

    /*
    Power up system
    0. Bubbles disabled
    1. Character can shoot bubbles that can be jumped on
    2. Bubbles affect enemies. Jumping on an enemy trapped in the bubble kills it.
    3. Bubbles affect spikes. Player doesn't lose health when jumping on spikes covered by a bubble.
    */
    private int powerUp = 0;
    public int PowerUp
    {
        get {return powerUp; }
        set {powerUp = value; }
    }

    public void pickUpPowerUp()
    {
        powerUp += 1;
    }

    private List<Item> inventory = new List<Item>();
    public List<Item> Inventory{
        get {return inventory; }
        set {inventory = value; }
    }

    private List<Item> itemsInWorld = new List<Item>();
    public List<Item> ItemsInWorld {
        get {return itemsInWorld; }
        set {itemsInWorld = value; }
    }

    public void addItemToWorld()
    {
        itemsInWorld.Add(new Item());
    }

    public void pickUpItem(Item item)
    {
        inventory.Add(item);
        itemsInWorld.Remove(item);
    }
}
