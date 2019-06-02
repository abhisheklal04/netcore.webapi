using System.Linq;
using System;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Repository;
using WebApi.Models;
using WebApi.Common;
using WebApi.Services.Interface;

namespace WebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomDbContext _dbContext;

        public CustomerService(
            CustomDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public Customer Add(CustomerAddRequest model)
        {
            if (model == null)
                throw new InvalidModelException("Model is invalid. Please check the swagger docs.");
            if (string.IsNullOrWhiteSpace(model.FirstName?.Trim()))
                throw new RequiredException("First Name is required.");
            if (string.IsNullOrWhiteSpace(model.FirstName?.Trim()))
                throw new RequiredException("First Name is required.");
            if (model.FirstName?.Trim().Length > 50)
                throw new MaxLengthException("Maximum length allowed for First Name is 50.");
            if (model.LastName?.Trim().Length > 50)
                throw new MaxLengthException("Maximum length allowed for Last Name is 50.");

            var existingCustomer = _dbContext.Customers.SingleOrDefault(x => !x.IsArchived && x.FirstName == model.FirstName && x.LastName == model.LastName);

            if (existingCustomer != null)
                throw new CustomerExistsException("Customer with same first and last name already exists");

            var dbItem = new Customer();

            dbItem.Id = Guid.NewGuid();
            dbItem.FirstName = model.FirstName;
            dbItem.LastName = model.LastName;
            dbItem.DateOfBirth = model.DateOfBirth;
            dbItem.CreatedDateTimeUtc = DateTime.UtcNow;
            dbItem.UpdatedDateTimeUtc = dbItem.CreatedDateTimeUtc;

            _dbContext.Customers.Add(dbItem);
            _dbContext.SaveChanges();

            return dbItem;
        }

        public CustomerResponse Get(Guid id)
        {
            var dbItem = _dbContext.Customers.SingleOrDefault(x => !x.IsArchived && x.Id == id);
            if (dbItem == null)
                throw new NotFoundException("Customer not found.");

            return new CustomerResponse()
            {
                Id = dbItem.Id,
                FirstName = dbItem.FirstName,
                LastName = dbItem.LastName,
                DateOfBirth = dbItem.DateOfBirth,
            };
        }

        public Customer Update(Guid id, CustomerUpdateRequest model)
        {
            if (model == null)
                throw new InvalidModelException("Model is invalid. Please check the swagger docs.");
            if (string.IsNullOrWhiteSpace(model.FirstName?.Trim()))
                throw new RequiredException("First Name is required.");
            if (string.IsNullOrWhiteSpace(model.FirstName?.Trim()))
                throw new RequiredException("First Name is required.");
            if (model.FirstName?.Trim().Length > 50)
                throw new MaxLengthException("Maximum length allowed for First Name is 50.");
            if (model.LastName?.Trim().Length > 50)
                throw new MaxLengthException("Maximum length allowed for Last Name is 50.");

            var dbItem = _dbContext.Customers.SingleOrDefault(x => x.Id == id && !x.IsArchived);
            if (dbItem == null)
                throw new NotFoundException("Customer not found.");

            var existingCustomer = _dbContext.Customers
                .SingleOrDefault(x => !x.IsArchived && x.Id != id && x.FirstName == model.FirstName && x.LastName == model.LastName);
            
            if (existingCustomer != null)
                throw new CustomerExistsException("Customer with same first and last name already exists");

            dbItem.UpdatedDateTimeUtc = DateTime.UtcNow;
            dbItem.FirstName = model.FirstName;
            dbItem.LastName = model.LastName;
            dbItem.DateOfBirth = model.DateOfBirth;

            _dbContext.Customers.Update(dbItem);
            _dbContext.SaveChanges();
            return dbItem;
        }

        public void Remove(Guid id)
        {
            var dbItem = _dbContext.Customers.SingleOrDefault(x => x.Id == id && !x.IsArchived);
            if (dbItem == null)
                throw new NotFoundException("Customer not found.");
            dbItem.IsArchived = true;
            _dbContext.Customers.Update(dbItem);
            _dbContext.SaveChanges();
        }

        public CustomerListResponse GetPaged(string keyword, CustomerListItemResponseSortCol sortCol, bool? isArchived, SortPageModel sortPage)
        {
            IQueryable<Customer> results = _dbContext.Customers.AsQueryable();

            if (isArchived.HasValue)
                results = results.Where(x => x.IsArchived == isArchived.Value);

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                results = results.Where(x =>
                    (x.Id + "").ToLower().Contains(keyword)
                    || (x.FirstName + "").ToLower().Contains(keyword)
                    || (x.LastName + "").ToLower().Contains(keyword)
                    );
            }

            switch (sortCol)
            {
                case CustomerListItemResponseSortCol.Id:
                    if (sortPage.SortDesc) results = results.OrderByDescending(x => x.Id);
                    else results = results.OrderBy(x => x.Id);
                    break;
                case CustomerListItemResponseSortCol.FirstName:
                    if (sortPage.SortDesc) results = results.OrderByDescending(x => x.FirstName);
                    else results = results.OrderBy(x => x.FirstName);
                    break;
                case CustomerListItemResponseSortCol.LastName:
                    if (sortPage.SortDesc) results = results.OrderByDescending(x => x.LastName);
                    else results = results.OrderBy(x => x.LastName);
                    break;
                case CustomerListItemResponseSortCol.CreatedDateTimeUtc:
                    if (sortPage.SortDesc) results = results.OrderByDescending(x => x.CreatedDateTimeUtc);
                    else results = results.OrderBy(x => x.CreatedDateTimeUtc);
                    break;
                case CustomerListItemResponseSortCol.UpdatedDateTimeUtc:
                    if (sortPage.SortDesc) results = results.OrderByDescending(x => x.UpdatedDateTimeUtc);
                    else results = results.OrderBy(x => x.UpdatedDateTimeUtc);
                    break;
            }

            long totalItems = results.LongCount();
            results = results.Skip(sortPage.PageSize * (sortPage.PageNumber - 1)).Take(sortPage.PageSize);
            var items = results.Select(x => new CustomerListItemResponse()
            {
                Id = x.Id,
                CreatedDateTimeUtc = x.CreatedDateTimeUtc,
                UpdatedDateTimeUtc = x.UpdatedDateTimeUtc,
                IsArchived = x.IsArchived,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
            }).ToList();

            if (totalItems > 0 && items.Count == 0)
                throw new Exception("Bad join.");
            var mappedResults = new CustomerListResponse
            {
                ItemsPerPage = items.LongCount(),
                CurrentPage = sortPage.PageNumber,
                TotalPages = (long)Math.Ceiling((double)totalItems / sortPage.PageSize),
                TotalItems = totalItems,
                Items = items,
            };
            return mappedResults;
        }

    }
}
