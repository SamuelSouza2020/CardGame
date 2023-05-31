using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class Tamanhoo : MonoBehaviour
{
    public bool iniciar = false;

    void Awake()
    {
        Igualar();
    }
    void OnRectTransformDimensionsChange()
    {
        Igualar();
    }

    void Igualar()
    {
        for (int a = 0; a < gameObject.transform.childCount; a++)
        {
            var ladoX = gameObject.GetComponent<GridLayoutGroup>().cellSize.x;
            gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(ladoX, ladoX+100);
        }
    }
}
