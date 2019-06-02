﻿using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Services;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(
            ICustomerService service
            )
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public CustomerListResponse GetAll(string keyword = null
            , bool? isArchived = false
            , int pageNumber = 1
            , int pageSize = 20
            , CustomerListItemResponseSortCol sortCol = CustomerListItemResponseSortCol.Id
            , bool sortDesc = false)
        {
            var result = _service.GetPaged(
                sortPage: new SortPageModel { PageNumber = pageNumber, PageSize = pageSize, SortDesc = sortDesc }
                , sortCol: sortCol
                , keyword: keyword
                , isArchived: isArchived
                );
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public CustomerResponse Get(Guid id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public Guid Add([FromBody]CustomerAddRequest item)
        {
            return _service.Add(item).Id;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(Guid id, [FromBody]CustomerUpdateRequest item)
        {
            _service.Update(id, item);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            _service.Remove(id);
        }
    }
}
