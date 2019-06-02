using System.Linq;
using System;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Repository;
using WebApi.Models;
using WebApi.Common;

namespace WebApi.Services.Interface
{
    public interface ICustomerService
    {
        Customer Add(CustomerAddRequest model);
        CustomerResponse Get(Guid id);
        Customer Update(Guid id, CustomerUpdateRequest model);
        void Remove(Guid id);
        CustomerListResponse GetPaged(string keyword, 
            CustomerListItemResponseSortCol sortCol, 
            bool? isArchived, SortPageModel sortPage);
    }
}
