using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class PropSpawner : MonoBehaviour
{
   public List<PropController> props;
   
   private int spawnIndex;
   private void Start()
   {
      SpawnProp();
   }

   void SpawnProp()
   {
      spawnIndex = Random.Range(0, props.Count);

      var position = transform.position;
      PropController instance = Instantiate(props[spawnIndex], position, Quaternion.identity);
      
      instance.transform.position = new Vector3(position.x,position.y+instance.spawnPointY,position.z);
   }
  
   
   
}
