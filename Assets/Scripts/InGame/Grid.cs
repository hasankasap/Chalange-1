using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Grid : MonoBehaviour
    {
        public GridProperties properties;
        public GameObject[,] cells;
        public List<PlacedObject> placedObjects = new List<PlacedObject>();

        public void CellPlacement(int[] index, GameObject target)
        {
            Vector3 position = target.transform.position;
            position.z = properties.placementObject.transform.position.z;
            position.x += .25f;
            GameObject temp = Instantiate(properties.placementObject, position, Quaternion.identity, target.transform);
            PlacedObject newObject = new PlacedObject();
            newObject.placementObject = temp;
            newObject.index= index;
            placedObjects.Add(newObject);
        }
        public int[] FindSlotIndex(GameObject target)
        {
            int range = properties.size;
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    if (cells[i,j] == target)
                    {
                        return new int[] { i, j };
                    }

                }
            }
            return null;
        }

        public bool CheckCellsForScore()
        {
            int count = 0;
            foreach (PlacedObject item in placedObjects)
            {
                count = 0;
                List<int[]> ints = new List<int[]>();
                ints.Add(new int[] { item.index[0], item.index[1] + 1 }); // right
                ints.Add(new int[] { item.index[0], item.index[1] - 1 }); // left
                ints.Add(new int[] { item.index[0] + 1, item.index[1] }); // up
                ints.Add(new int[] { item.index[0] - 1, item.index[1] }); // down
                foreach (int[] index in ints)
                {
                    if (placedObjects.Any(po => po.index.SequenceEqual(index)))
                        count++;
                }
                if (count >= 2)
                    return true;
            }
            return false;
        }

        public void ClearPlacedObjects()
        {
            foreach (PlacedObject item in placedObjects)
            {
                if (item != null && item.placementObject != null)
                Destroy(item.placementObject);
            }
            placedObjects.Clear();
        }
    }
    public class PlacedObject 
    {
        public GameObject placementObject;
        public int[] index;
    }
}