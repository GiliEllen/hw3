using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//great!
public class IntegerManipulator : MonoBehaviour
{
    void Start() {
        MultiplyInteger(5);
        CheckIntegerBiggerThan(5);
        CheckIntegerBiggerThan(11);
        SwitchInteger(3);
    }
        public void MultiplyInteger(int num)
    {
        int result = num * 2; 
        Debug.Log("The result of operating on the integer is: " + result);
    }

    public void CheckIntegerBiggerThan(int num)
    {
        if (num > 10)
        {
            Debug.Log("The number is greater than 10.");
        }
        else
        {
            Debug.Log("The number is 10 or less.");
        }
    }

    public void SwitchInteger(int num)
    {
        switch (num)
        {
            case 1:
                Debug.Log("The number is 1.");
                break;
            case 2:
                Debug.Log("The number is 2.");
                break;
            case 3:
                Debug.Log("The number is 3.");
                break;
            default:
                Debug.Log("The number is something else.");
                break;
        }
    }
}
