using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttingMiniGameManager : MonoBehaviour
{
   
    public List<VegetableComponent> vegetableList;


    int vegetablesCuts;

    private void Awake()
    {
        
        vegetablesCuts = 0;

    }

    public void CutVegetable()
    {

        vegetablesCuts++;
        print(vegetablesCuts);
        if(vegetablesCuts >= vegetableList.Count)
        {
            FinishMiniGame();
        }

    }

    public void FinishMiniGame()
    {


        print("finished");

    }



}
