﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CentralValleyBikes.Domain.Filters
{
    public class ProductFilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var queryParams = bindingContext.HttpContext.Request.Query;

            SearchFilter searchFilter = new SearchFilter();
            searchFilter.SetPagingAttributes(queryParams);

            var entityParams = queryParams.Where(p => !p.Key.StartsWith("page"));

            foreach (var param in entityParams)
            {
                if (string.IsNullOrWhiteSpace(param.Value))
                    continue;

                var criteria = new SearchCriteria(param);

                switch (criteria.FieldName.ToLower())
                {
                    case "brand":
                        criteria.FieldName = "brand.brandname";
                        break;
                    case "category":
                        criteria.FieldName = "category.categoryName";
                        break;
                    default:
                        break;
                }

                criteria.LookupValue = param.Value;
                searchFilter.SearchCriteria.Add(criteria);
            }

            bindingContext.Result = ModelBindingResult.Success(searchFilter);

            return Task.CompletedTask;
        }
    }
}
    