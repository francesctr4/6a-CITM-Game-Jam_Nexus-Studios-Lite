using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class juan : MonoBehaviour
{
    bool comeBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            Vector2 targetPosition = comeBack ? new Vector2(0, 0) : new Vector2(-5, 0);
            comeBack = !comeBack;

            transform.DOMove(targetPosition, 1f).SetEase(Ease.OutBounce).Play();
        }
    }
}
