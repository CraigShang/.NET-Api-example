using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Interface;
using RefactorThis.Models;
using RefactorThis.Models.Enum;

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductRepositoty repositoty = new ProductRepositoty();

        [HttpGet]
        public ReturnValueBase<dynamic> Get()
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            Products model = repositoty.GetAllProducts();
            if (model != null)
            {
                returnValue.data = model;
                returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpGet("{id}")]
        public ReturnValueBase<dynamic> Get(Guid id)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            Product model = repositoty.GetProductById(id);
            if (model != null)
            {
                returnValue.data = model;
                returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpPost]
        public ReturnValueBase<dynamic> Post(Product product)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            if (product.IsNew)
            {
                if(repositoty.AddOrUpdateProduct(product))
                    returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpPut("{id}")]
        public ReturnValueBase<dynamic> Update(Guid id, Product product)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            product.Id = id;
            if (!product.IsNew)
            {
                if (repositoty.AddOrUpdateProduct(product))
                    returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpDelete("{id}")]
        public ReturnValueBase<dynamic> Delete(Guid id)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            if (repositoty.DeleteProduct(id))
                returnValue.code = (int)ReturnCodeEnum.Success;
            return returnValue;
        }
    }
}