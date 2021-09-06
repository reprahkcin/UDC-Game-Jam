using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  // ---------------------------------------------------
  // GameObject Connections
  // ---------------------------------------------------
  // Get the player object
  public GameObject player;


  // ---------------------------------------------------
  // Item Variables
  // ---------------------------------------------------
  private int healthBuff = 15;

  private int damageBuff = 0;

  private int speedBuff = 0;

  private int defenseBuff = 0;




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

  // -----------------------------------------------------
  // Constructors
  // -----------------------------------------------------
  public Item(int healthBuff, int damageBuff, int speedBuff, int defenseBuff)
  {
    this.healthBuff = healthBuff;
    this.damageBuff = damageBuff;
    this.speedBuff = speedBuff;
    this.defenseBuff = defenseBuff;
  }
}
