using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambioanimaciones : MonoBehaviour
{

    private Animator anim;

    private BatStates currenteState;
    private enum BatStates
    {
        Volar,
        Andar,
        Dejar_Andar
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeState(BatStates newState)
    {
        if (newState == currenteState) return;

        currenteState = newState;
        switch (newState)
        {
            case BatStates.Volar:
                anim.SetTrigger(name: "Volar");
                break;
            case BatStates.Andar:
                anim.SetTrigger(name: "Andar");
                break;
            case BatStates.Dejar_Andar:
                anim.SetTrigger(name: "Dejar_Andar");
                break;
        }
    }
}
