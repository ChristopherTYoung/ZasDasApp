﻿using Square.Bookings.CustomAttributeDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
namespace ZasUndDas.Shared.Services;
public class APIService : IAPIService
{
    public bool LoggedIn => key != null;
    public static string? apiKey = "APIKEY";

    public string? key;
    public APIService(HttpClient client)
    {
        Client = client;
    }
    HttpClient Client { get; init; }
    public Uri? BaseAddress
    {
        get
        {
            return Client.BaseAddress;
        }
    }
    public void LogOut()
    {
        apiKey = null;
    }
    public async Task Authorize(AuthRequest request)
    {
        var response = await Client.PostAsJsonAsync("/api/auth/authenticate", request);
        var TempKey = await response.Content.ReadAsStringAsync();
        if (TempKey.Contains(" "))
            throw new Exception(TempKey);
        key = TempKey;
        Client.DefaultRequestHeaders.Add(apiKey, TempKey);
    }
    public async Task CreateAccount(CreateRequest request)
    {
        var response = await Client.PostAsJsonAsync("/api/auth/create", request);
        var TempKey = await response.Content.ReadAsStringAsync();
        if (TempKey.Contains(" "))
            throw new Exception(TempKey);
        Client.DefaultRequestHeaders.Add(apiKey, TempKey);
    }
    public async Task<List<PizzaBaseDTO>> GetPizzas()
    {
        return await Client.GetFromJsonAsync<List<PizzaBaseDTO>>("/api/inventory/getallpizzabase") ?? new List<PizzaBaseDTO>();
    }
    public async Task<List<DrinkBaseDTO>> GetDrinks()
    {
        return await Client.GetFromJsonAsync<List<DrinkBaseDTO>>("/api/inventory/getalldrinkbase") ?? new List<DrinkBaseDTO>();

    }
    public async Task<List<PAddinDTO>> GetPizzaToppings()
    {
        return await Client.GetFromJsonAsync<List<PAddinDTO>>("/api/inventory/getpizzatoppings") ?? new List<PAddinDTO>();
    }

    public async Task<List<DAddinDTO>> GetDrinkAddins()
    {
        return await Client.GetFromJsonAsync<List<DAddinDTO>>("/api/inventory/getdrinkaddins") ?? new List<DAddinDTO>();
    }

    public async Task<List<PizzaSize>> GetPizzaSizes()
    {
        return await Client.GetFromJsonAsync<List<PizzaSize>>("/api/inventory/getpizzasizes") ?? new List<PizzaSize>();
    }

    public async Task<List<Sauce>> GetSauces()
    {
        return await Client.GetFromJsonAsync<List<Sauce>>("/api/inventory/getsauces") ?? new List<Sauce>();
    }

    public async Task Order(OrderDTO order)
    {
        await Client.PostAsJsonAsync("/api/order/sendorder", order);
    }

}



