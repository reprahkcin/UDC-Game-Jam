using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  // Get the player object
  public GameObject player;

  private int healthBuff = 15;

  private int damageBuff = 0;

  private int speedBuff = 0;

  private int defenseBuff = 0;

  // Start is called before the first frame update
  void Start()
  {
    // Get the player object
    player = GameObject.Find("Player");
  }

  // -----------------------------------------------------
  // Getters
  // -----------------------------------------------------
  public int getHealthBuff()
  {
    return healthBuff;
  }

  public int getDamageBuff()
  {
    return damageBuff;
  }

  public int getSpeedBuff()
  {
    return speedBuff;
  }

  public int getDefenseBuff()
  {
    return defenseBuff;
  }
}
