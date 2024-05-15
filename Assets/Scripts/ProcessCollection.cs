using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessCollection : MonoBehaviour
{

    public GameObject processObject;
    [SerializeField] private Slider processSlider;
    public bool processing;

    public float processTimeCounted = 0f;
    private float processTime;
    public int coinsToCollect = 4;
    [SerializeField] private Collectable collectable;
    [SerializeField] private Creature creature;

    


    // Update is called once per frame
    void Update()
    {
        if (processing)
        {
            if (processTimeCounted < processTime)
            {
            processTimeCounted += Time.deltaTime;
            processSlider.value = processTimeCounted;
            }
            else
            {
            processObject.SetActive(false);
            processing = false;
            HUD.Instance.GetMoney(coinsToCollect);

            if(creature != null)
            {
                creature.Captured();
            }

            if(collectable != null)
            {
                collectable.Collect();
            }

            }
        }
        else{
            processObject.SetActive(false);
        }
        
    }

        public void StartTimer(int timeToFinish)
    {
        processTimeCounted = 0;
        processTime = timeToFinish;
        processing = true;
        processObject.SetActive(true);
        processSlider.value = 0;
        processSlider.maxValue = timeToFinish;
    }


    public void StopTimer()
    {
        processing = false;
    }
}
