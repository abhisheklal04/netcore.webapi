/* 
 * Sample API
 *
 * Optional multiline or single-line description in [CommonMark](http://commonmark.org/help/) or HTML.
 *
 * OpenAPI spec version: 0.1.9
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// Project
    /// </summary>
    [DataContract]
    public partial class Project :  IEquatable<Project>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Project() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="title">title (required).</param>
        /// <param name="owner">owner (required).</param>
        /// <param name="label">label (required).</param>
        /// <param name="workflowScheme">workflowScheme (required).</param>
        /// <param name="createdOn">createdOn (required).</param>
        public Project(int id = default(int), string title = default(string), Owner owner = default(Owner), string label = default(string), WorkflowScheme workflowScheme = default(WorkflowScheme), DateTime createdOn = default(DateTime))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for Project and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "title" is required (not null)
            if (title == null)
            {
                throw new InvalidDataException("title is a required property for Project and cannot be null");
            }
            else
            {
                this.Title = title;
            }

            // to ensure "owner" is required (not null)
            if (owner == null)
            {
                throw new InvalidDataException("owner is a required property for Project and cannot be null");
            }
            else
            {
                this.Owner = owner;
            }

            // to ensure "label" is required (not null)
            if (label == null)
            {
                throw new InvalidDataException("label is a required property for Project and cannot be null");
            }
            else
            {
                this.Label = label;
            }

            // to ensure "workflowScheme" is required (not null)
            if (workflowScheme == null)
            {
                throw new InvalidDataException("workflowScheme is a required property for Project and cannot be null");
            }
            else
            {
                this.WorkflowScheme = workflowScheme;
            }

            // to ensure "createdOn" is required (not null)
            if (createdOn == null)
            {
                throw new InvalidDataException("createdOn is a required property for Project and cannot be null");
            }
            else
            {
                this.CreatedOn = createdOn;
            }

        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name="title", EmitDefaultValue=false)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [DataMember(Name="owner", EmitDefaultValue=false)]
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or Sets Label
        /// </summary>
        [DataMember(Name="label", EmitDefaultValue=false)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or Sets WorkflowScheme
        /// </summary>
        [DataMember(Name="workflowScheme", EmitDefaultValue=false)]
        public WorkflowScheme WorkflowScheme { get; set; }

        /// <summary>
        /// Gets or Sets CreatedOn
        /// </summary>
        [DataMember(Name="createdOn", EmitDefaultValue=false)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Project {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  WorkflowScheme: ").Append(WorkflowScheme).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Project);
        }

        /// <summary>
        /// Returns true if Project instances are equal
        /// </summary>
        /// <param name="input">Instance of Project to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Project input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) && 
                (
                    this.Owner == input.Owner ||
                    (this.Owner != null &&
                    this.Owner.Equals(input.Owner))
                ) && 
                (
                    this.Label == input.Label ||
                    (this.Label != null &&
                    this.Label.Equals(input.Label))
                ) && 
                (
                    this.WorkflowScheme == input.WorkflowScheme ||
                    (this.WorkflowScheme != null &&
                    this.WorkflowScheme.Equals(input.WorkflowScheme))
                ) && 
                (
                    this.CreatedOn == input.CreatedOn ||
                    (this.CreatedOn != null &&
                    this.CreatedOn.Equals(input.CreatedOn))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                if (this.Owner != null)
                    hashCode = hashCode * 59 + this.Owner.GetHashCode();
                if (this.Label != null)
                    hashCode = hashCode * 59 + this.Label.GetHashCode();
                if (this.WorkflowScheme != null)
                    hashCode = hashCode * 59 + this.WorkflowScheme.GetHashCode();
                if (this.CreatedOn != null)
                    hashCode = hashCode * 59 + this.CreatedOn.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}