using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject fillCard;
    //public GameObject powerUpCard;
    //public GameObject animalCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showCards(){
        fillCard.EnableCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
