using System.Linq;
using System;
using CustomerApi.Models.Request;
using CustomerApi.Models.Response;
using CustomerApi.Repository;
using CustomerApi.Models;
using CustomerApi.Common;

namespace CustomerApi.Services.Interface
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
