using System;

namespace CustomerApi.Models.Response
{
    public enum CustomerListItemResponseSortCol
    {
        Id,
        CreatedDateTimeUtc,
        UpdatedDateTimeUtc,
        FirstName,
        LastName,
    }
    public class CustomerListItemResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime UpdatedDateTimeUtc { get; set; }
    }
}
