using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Spawners
{
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
         spawnIndex = Random.Range(0, props.Count); //Prop seç

         var position = transform.position;
         PropController instance = Instantiate(props[spawnIndex], position, Quaternion.identity);//Prop üret
      
         instance.transform.position = new Vector3(position.x,position.y+instance.spawnPointY,position.z);//Propun positionunu düzelt
         instance.transform.SetParent(GameMaster.instance.levelHolder);
      }
      
  
   
   
   }
}
