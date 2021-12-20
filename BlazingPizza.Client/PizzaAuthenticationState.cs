using BlazingPizza;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class PizzaAuthenticationState : RemoteAuthenticationState
{
    public Order Order { get; set; }
}