using System.Collections.Generic;
using BlazingPizza;

public class OrderState
{
       
    public bool showingConfigureDialog { get; private set; }

    public Pizza configuringPizza { get; private set; }

    public Order order { get; private set; } = new Order();

    
    public void showConfigurePizzaDialog(PizzaSpecial special)
    {
        configuringPizza = new Pizza()
        {
            Special = special,
            SpecialId = special.Id,
            Size = Pizza.DefaultSize,
            Toppings = new List<PizzaTopping>(),
        };

        showingConfigureDialog = true;
    }

    public void CancelConfigurePizzaDialog()
    {
        configuringPizza = null;
        showingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog(){
        order.Pizzas.Add(configuringPizza);
        
        configuringPizza = null;
        showingConfigureDialog = false;
    }

    public void RemoveConfiguredPizza(Pizza pizza){
        order.Pizzas.Remove(pizza);
    }

    public void ResetOrder(){
        order = new Order();
    }

    public void ReplaceOrder(Order __order){
        order = __order;
    }
}