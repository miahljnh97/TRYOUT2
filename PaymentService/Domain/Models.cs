using System;
using Newtonsoft.Json;

namespace PaymentService.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public int Order_id { get; set; }
        public int Transaction_id { get; set; }
        public string Payment_type { get; set; }
        public string Gross_amount { get; set; }
        public string Bank { get; set; }
        public string Transaction_time { get; set; }
        public string Transaction_status { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Orders orders { get; set; }
    }

    public class Orders
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; } = DateTime.Now;

        public userModel users { get; set; }

    }

    public class Order_details
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Order_id { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Products products { get; set; }
        public Orders orders { get; set; }
    }

    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; } = DateTime.Now;
    }

    public class userModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

    public class RequestData<T>
    {
        public Data<T> Data { get; set; }
    }

    public class Data<T>
    {
        public T Attributes { get; set; }
    }
}
