using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.IO;
// using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Text;

public class backendUpdate : MonoBehaviour
{
    [SerializeField] profileSO profileso;
    string data;

    [SerializeField] private string endpoint = "https://twis.in/shop/wp-json/wc/v3/orders";
    [SerializeField] string consumer_key = "ck_fca782970fd2bf6c331651f2a92980c84b262893";
    [SerializeField] string consumer_secret = "cs_ba4140afb48da6ae3151ff85b9780c1f09fc04ed";
    public CartSO cart;


    public Dictionary<string, object> formDataDict = new Dictionary<string, object>(){
        { "payment_method", "bacs" },
        { "payment_method_title", "Cash On Delivery" },
        { "set_paid", true },
        { "billing", new Dictionary<string, object>
            {
                { "first_name", "John" },
                { "last_name", "Doe" },
                { "address_1", "969 Market" },
                { "address_2", "" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "postcode", "94103" },
                { "country", "US" },
                { "email", "john.doe@example.com" },
                { "phone", "(555) 555-5555" }
            }
        },
        { "shipping", new Dictionary<string, object>
            {
                { "first_name", "John" },
                { "last_name", "Doe" },
                { "address_1", "969 Market" },
                { "address_2", "" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "postcode", "94103" },
                { "country", "US" }
            }
        },
        { "line_items", new List<Dictionary<string, object>>
            {
                // new Dictionary<string, object>
                // {
                //     { "product_id", 0 },
                //     { "quantity", 2 }
                // },
                // new Dictionary<string, object>
                // {
                //     { "product_id", 1 },
                //     { "quantity", 1 }
                // }
            }
        }
    };

    public void SaveInput()
    {

        ((Dictionary<string, object>)formDataDict["billing"])["first_name"] = profileso.firstName;
        ((Dictionary<string, object>)formDataDict["shipping"])["first_name"] = profileso.firstName;

        ((Dictionary<string, object>)formDataDict["billing"])["last_name"] = profileso.lastName;
        ((Dictionary<string, object>)formDataDict["shipping"])["last_name"] = profileso.lastName;

        ((Dictionary<string, object>)formDataDict["billing"])["address_1"] = profileso.address;
        ((Dictionary<string, object>)formDataDict["shipping"])["address_1"] = profileso.address;

        ((Dictionary<string, object>)formDataDict["billing"])["city"] = profileso.city;
        ((Dictionary<string, object>)formDataDict["shipping"])["city"] = profileso.city;

        ((Dictionary<string, object>)formDataDict["billing"])["state"] = profileso.state;
        ((Dictionary<string, object>)formDataDict["shipping"])["state"] = profileso.state; 

        ((Dictionary<string, object>)formDataDict["billing"])["postcode"] = profileso.postcode;
        ((Dictionary<string, object>)formDataDict["shipping"])["postcode"] = profileso.postcode;

        ((Dictionary<string, object>)formDataDict["billing"])["country"] = profileso.country;
        ((Dictionary<string, object>)formDataDict["shipping"])["country"] = profileso.country;

        ((Dictionary<string, object>)formDataDict["billing"])["email"] = profileso.email;
        ((Dictionary<string, object>)formDataDict["billing"])["phone"] = profileso.phone;

        foreach (ProductSO item in cart.cartItems)
        {
            ((List<Dictionary<string, object>>)formDataDict["line_items"]).Add(new Dictionary<string, object>
            {
                { "product_id", item.id },
                { "quantity", item.quantity }
            });
        }

        data = JsonConvert.SerializeObject(formDataDict);
        Debug.Log(data);
    }


    public void SendData()
    {
        WebRequest request = WebRequest.Create(endpoint);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(consumer_key + ":" + consumer_secret)));

        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(data);
        }

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Debug.Log("Order created successfully");

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string response_content = reader.ReadToEnd();
                    Dictionary<string, object> response_data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response_content);
                    int order_id = Convert.ToInt32(response_data["id"]);
                    Debug.Log("Order ID: " + order_id);
                }
            }
            else
            {
                Console.WriteLine("Order creation failed with status code " + (int)response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    Debug.Log("Response content: " + reader.ReadToEnd());
                }
            }
        }
        catch (WebException ex)
        {
            Debug.Log("API request failed with error:");
            Debug.Log(ex.Message);



        }
    }
}
