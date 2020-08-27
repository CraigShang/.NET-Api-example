using System;
using System.Collections.Generic;
using System.Transactions;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public class Product:Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] public bool IsNew { get; }

        public Product():base()
        {
            IsNew = true;
        }

        public Product(Guid Id):base()
        {
            IsNew = false;
        }
    }
}
