/* 
 * Sample API
 *
 * Optional multiline or single-line description in [CommonMark](http://commonmark.org/help/) or HTML.
 *
 * OpenAPI spec version: 0.1.9
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System.Runtime.Serialization;


namespace Common.ViewModels
{
    /// <summary>
    /// Paging
    /// </summary>
    [DataContract]
    public partial class Paging
    {
        
        /// <summary>
        /// Gets or Sets Limit
        /// </summary>
        [DataMember(Name="limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or Sets NextOffSet
        /// </summary>
        [DataMember(Name="nextOffSet")]
        public int NextOffSet { get; set; }

        /// <summary>
        /// Gets or Sets TotalCount
        /// </summary>
        [DataMember(Name="totalCount")]
        public int TotalCount { get; set; }

        
    }

}