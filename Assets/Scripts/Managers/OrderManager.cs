using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Responsible for Creating and Queuing orders
/// </summary>
public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    [SerializeField] private Transform drinkListParent;
    [SerializeField] private Transform currentDrinkParent;

    public Queue<Drink> orderQueue {  get; private set; } //order queue
    Drink currentOrder;

  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        orderQueue = new Queue<Drink>();
    }

    /// <summary>
    /// preliminary actions to start order queue
    /// </summary>
    public void Init()
    {
        //Add a number of drinks
        AddNewRandomDrinkToQueue(10);

        //set first order (not techniqually completing the order, but setting the next drink in queue to current drink
        CompleteOrder();
    }

  
    /// Allows to add any number of random drinks to queue
    /// </summary>
    /// <param name="numberOfDrinksToAdd"></param>
    public void AddNewRandomDrinkToQueue(int numberOfDrinksToAdd)
    {
        for(int i = 0; i < numberOfDrinksToAdd; i++)
        {
            //create a drink GO
            GameObject newDrink = new GameObject();
            newDrink.transform.parent = drinkListParent;

            newDrink.AddComponent<Drink>();
            newDrink.transform.name = "drink";

            //grab drink script
            Drink drinkScript = newDrink.GetComponent<Drink>();
            drinkScript.RandomizeDrink();

            //enqueue into list
            orderQueue.Enqueue(drinkScript);
        }
    }

    /// <summary>
    /// Calls the score manager when end of session reached (end of queue)
    /// </summary>
    public void CompleteOrder()
    {
        if(orderQueue.Count == 0) //session finished
        {
            ScoreManager.Instance.ReturnScore();
        }
        else //next drink
        {
            //delete current drink
            if (currentDrinkParent.childCount > 0)
            {
                //remove current drink visualisation
                Destroy(currentDrinkParent.GetChild(0));
            }

            //Move GO from queue to current
            if (drinkListParent.childCount > 0)
            {
                drinkListParent.GetChild(0).transform.SetParent(currentDrinkParent);
            }

            currentOrder = orderQueue.Dequeue().GetComponent<Drink>();
        }
    }
}
