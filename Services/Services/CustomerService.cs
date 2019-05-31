using System.Linq;
using Microsoft.Extensions.Configuration;
using Common.ViewModels;
using Repository;
using Common;

namespace Services
{
    public class CustomerService
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
            if (string.IsNullOrWhiteSpace(model.Title?.Trim()))
                throw new RequiredTitleException("Title is required.");
            if (string.IsNullOrWhiteSpace(model.GoogleAddress?.Trim()))
                throw new RequiredGoogleAddressException("GoogleAddress is required.");
            if (string.IsNullOrWhiteSpace(model.Lat?.Trim()) || string.IsNullOrWhiteSpace(model.Lng?.Trim()))
                throw new RequiredLatLngException("Lng/lng is required.");
            new GeoCoordinate(double.Parse(model.Lat), double.Parse(model.Lng)); // Validate lat/lng range and throw ArgumentOutOfRangeException.

            var dbItem = new Warehouse();

            dbItem.Id = Guid.NewGuid();
            dbItem.CreatedDateTimeUtc = DateTime.UtcNow;
            dbItem.UpdatedDateTimeUtc = dbItem.CreatedDateTimeUtc;
            dbItem.Title = model.Title.Trim();
            dbItem.Description = model.Description?.Trim();
            dbItem.GoogleAddress = model.GoogleAddress.Trim();
            dbItem.Lat = model.Lat.Trim();
            dbItem.Lng = model.Lng.Trim();

            _dbContext.Warehouse.Add(dbItem);
            _dbContext.SaveChanges();
            return dbItem;
        }


    }
}
