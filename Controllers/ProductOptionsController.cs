using Microsoft.AspNetCore.Mvc;
using System;
using RefactorThis.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefactorThis.Interface;
using RefactorThis.Models.Enum;

namespace RefactorThis.Controllers
{
    [Route("api/products/{productId}/options")]
    [ApiController]
    public class ProductOptionsController:ControllerBase
    {
        ProductOptionRepository repository = new ProductOptionRepository();

        [HttpGet]
        public ReturnValueBase<dynamic> GetOptions(Guid productId)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            ProductOptions model = repository.GetProductOptionsForProduct(productId);
            if (model != null)
            {
                returnValue.data = model;
                returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpGet("{id}")]
        public ReturnValueBase<dynamic> GetOption(Guid productId, Guid id)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            ProductOption model = repository.GetProductOption(productId, id);
            if (model != null)
            {
                returnValue.data = model;
                returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpPost]
        public ReturnValueBase<dynamic> CreateOption(Guid productId, ProductOption option)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            if (option.IsNew)
            {
                if (repository.AddOrUpdateProductOption(productId, option))
                    returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpPut("{id}")]
        public ReturnValueBase<dynamic> UpdateOption(Guid productId, Guid id, ProductOption option)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            option.Id = id;
            if (!option.IsNew)
            {
                if (repository.AddOrUpdateProductOption(productId, option))
                    returnValue.code = (int)ReturnCodeEnum.Success;
            }
            return returnValue;
        }

        [HttpDelete("{id}")]
        public ReturnValueBase<dynamic> DeleteOption(Guid productId, Guid id)
        {
            ReturnValueBase<dynamic> returnValue = new ReturnValueBase<dynamic>();
            if (repository.DeleteProductOption(productId, id))
                returnValue.code = (int)ReturnCodeEnum.Success;
            return returnValue;
        }
    }
}
