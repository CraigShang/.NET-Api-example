using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public class ProductOption:Entity
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore] public bool IsNew { get; }

        public Product Product { get; set; }

        public ProductOption():base()
        {
            IsNew = true;
        }

        public ProductOption(Guid id):base()
        {
            IsNew = false;
        }
    }
}
