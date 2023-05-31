using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunctionInterno : MonoBehaviour
{
    public int NumberCard = 0;
    public bool ModoIA = false;

    public void JogarCarta()
    {
        if(!MainController.instance.WaitTurn && !ModoIA)
        {
            gameObject.transform.SetParent(MainController.instance.LocalMesaP.transform, false);
            MainController.instance.WaitTurn = true;
        }
    }
}
