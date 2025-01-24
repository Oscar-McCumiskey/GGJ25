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

    [SerializeField] public Queue<Null> orderQueue {  get; private set; } //order queue
    Null currentOrder;

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

    /// <summary>
    /// create a drink
    /// </summary>
    private void CreateRandomDrink()
    {
        //returns a random drink
    }

    /// <summary>
    /// Allows to add any number of random drinks to queue
    /// </summary>
    /// <param name="numberOfDrinksToAdd"></param>
    public void AddNewRandomDrinkToQueue(int numberOfDrinksToAdd)
    {
        for(int i = 0; i < numberOfDrinksToAdd; i++)
        {
            //orderQueue.Enqueue(CreateRandomDrink());
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
            currentOrder = orderQueue.Dequeue();
        }
    }
}
