using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
   public static GameManager Instance { get; private set; }
   public Vector2 lastCheckPointPos;
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(Instance);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
