using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCard : MonoBehaviour
{

    private int soulsToGive;
    private int lastTotalCount;
    [SerializeField] private TMP_Text soulsToGiveText;

    //Card est un bouton.
    

    void Awake(){
        //Bouton disabled
        lastTotalCount = 0;
    }

    void Start()
    {
    
    }

    public void setSoulCount(){
        soulsToGive = GameManager.GetSoulCount() - lastTotalCount;
        lastTotalCount = GameManager.GetSoulCount();
    }

    private void OnSelect(){
        well = FindObjectOfType<Puit>();
        well.Balayeuse();
    }

    public void EnableCard(){
        //Enbable bouton
    }

    public void DisableCard(){
        //Disable bouton
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
