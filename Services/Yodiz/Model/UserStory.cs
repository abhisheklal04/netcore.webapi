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
    /// UserStory
    /// </summary>
    [DataContract]
    public partial class UserStory :  IEquatable<UserStory>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStory" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UserStory() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStory" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="title">title (required).</param>
        /// <param name="planEstimate">planEstimate (required).</param>
        /// <param name="storyPoints">storyPoints (required).</param>
        /// <param name="effortEstimate">effortEstimate (required).</param>
        /// <param name="effortRemaining">effortRemaining (required).</param>
        /// <param name="effortLogged">effortLogged (required).</param>
        /// <param name="status">status (required).</param>
        /// <param name="owner">owner (required).</param>
        /// <param name="release">release (required).</param>
        /// <param name="epic">epic (required).</param>
        /// <param name="sprint">sprint (required).</param>
        /// <param name="priority">priority (required).</param>
        /// <param name="tags">tags (required).</param>
        /// <param name="createdBy">createdBy (required).</param>
        /// <param name="createdOn">createdOn (required).</param>
        /// <param name="updatedBy">updatedBy (required).</param>
        /// <param name="updatedOn">updatedOn (required).</param>
        public UserStory(int id = default(int), string title = default(string), float planEstimate = default(float), string storyPoints = default(string), float effortEstimate = default(float), float effortRemaining = default(float), float effortLogged = default(float), Status status = default(Status), Owner owner = default(Owner), Release release = default(Release), Epic epic = default(Epic), Sprint sprint = default(Sprint), int priority = default(int), Tags tags = default(Tags), CreatedBy createdBy = default(CreatedBy), DateTime createdOn = default(DateTime), UpdatedBy updatedBy = default(UpdatedBy), DateTime updatedOn = default(DateTime))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "title" is required (not null)
            if (title == null)
            {
                throw new InvalidDataException("title is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Title = title;
            }

            // to ensure "planEstimate" is required (not null)
            if (planEstimate == null)
            {
                throw new InvalidDataException("planEstimate is a required property for UserStory and cannot be null");
            }
            else
            {
                this.PlanEstimate = planEstimate;
            }

            // to ensure "storyPoints" is required (not null)
            if (storyPoints == null)
            {
                throw new InvalidDataException("storyPoints is a required property for UserStory and cannot be null");
            }
            else
            {
                this.StoryPoints = storyPoints;
            }

            // to ensure "effortEstimate" is required (not null)
            if (effortEstimate == null)
            {
                throw new InvalidDataException("effortEstimate is a required property for UserStory and cannot be null");
            }
            else
            {
                this.EffortEstimate = effortEstimate;
            }

            // to ensure "effortRemaining" is required (not null)
            if (effortRemaining == null)
            {
                throw new InvalidDataException("effortRemaining is a required property for UserStory and cannot be null");
            }
            else
            {
                this.EffortRemaining = effortRemaining;
            }

            // to ensure "effortLogged" is required (not null)
            if (effortLogged == null)
            {
                throw new InvalidDataException("effortLogged is a required property for UserStory and cannot be null");
            }
            else
            {
                this.EffortLogged = effortLogged;
            }

            // to ensure "status" is required (not null)
            if (status == null)
            {
                throw new InvalidDataException("status is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Status = status;
            }

            // to ensure "owner" is required (not null)
            if (owner == null)
            {
                throw new InvalidDataException("owner is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Owner = owner;
            }

            // to ensure "release" is required (not null)
            if (release == null)
            {
                throw new InvalidDataException("release is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Release = release;
            }

            // to ensure "epic" is required (not null)
            if (epic == null)
            {
                throw new InvalidDataException("epic is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Epic = epic;
            }

            // to ensure "sprint" is required (not null)
            if (sprint == null)
            {
                throw new InvalidDataException("sprint is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Sprint = sprint;
            }

            // to ensure "priority" is required (not null)
            if (priority == null)
            {
                throw new InvalidDataException("priority is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Priority = priority;
            }

            // to ensure "tags" is required (not null)
            if (tags == null)
            {
                throw new InvalidDataException("tags is a required property for UserStory and cannot be null");
            }
            else
            {
                this.Tags = tags;
            }

            // to ensure "createdBy" is required (not null)
            if (createdBy == null)
            {
                throw new InvalidDataException("createdBy is a required property for UserStory and cannot be null");
            }
            else
            {
                this.CreatedBy = createdBy;
            }

            // to ensure "createdOn" is required (not null)
            if (createdOn == null)
            {
                throw new InvalidDataException("createdOn is a required property for UserStory and cannot be null");
            }
            else
            {
                this.CreatedOn = createdOn;
            }

            // to ensure "updatedBy" is required (not null)
            if (updatedBy == null)
            {
                throw new InvalidDataException("updatedBy is a required property for UserStory and cannot be null");
            }
            else
            {
                this.UpdatedBy = updatedBy;
            }

            // to ensure "updatedOn" is required (not null)
            if (updatedOn == null)
            {
                throw new InvalidDataException("updatedOn is a required property for UserStory and cannot be null");
            }
            else
            {
                this.UpdatedOn = updatedOn;
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
        /// Gets or Sets PlanEstimate
        /// </summary>
        [DataMember(Name="planEstimate", EmitDefaultValue=false)]
        public float PlanEstimate { get; set; }

        /// <summary>
        /// Gets or Sets StoryPoints
        /// </summary>
        [DataMember(Name="storyPoints", EmitDefaultValue=false)]
        public string StoryPoints { get; set; }

        /// <summary>
        /// Gets or Sets EffortEstimate
        /// </summary>
        [DataMember(Name="effortEstimate", EmitDefaultValue=false)]
        public float EffortEstimate { get; set; }

        /// <summary>
        /// Gets or Sets EffortRemaining
        /// </summary>
        [DataMember(Name="effortRemaining", EmitDefaultValue=false)]
        public float EffortRemaining { get; set; }

        /// <summary>
        /// Gets or Sets EffortLogged
        /// </summary>
        [DataMember(Name="effortLogged", EmitDefaultValue=false)]
        public float EffortLogged { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [DataMember(Name="owner", EmitDefaultValue=false)]
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or Sets Release
        /// </summary>
        [DataMember(Name="release", EmitDefaultValue=false)]
        public Release Release { get; set; }

        /// <summary>
        /// Gets or Sets Epic
        /// </summary>
        [DataMember(Name="epic", EmitDefaultValue=false)]
        public Epic Epic { get; set; }

        /// <summary>
        /// Gets or Sets Sprint
        /// </summary>
        [DataMember(Name="sprint", EmitDefaultValue=false)]
        public Sprint Sprint { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name="priority", EmitDefaultValue=false)]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or Sets Tags
        /// </summary>
        [DataMember(Name="tags", EmitDefaultValue=false)]
        public Tags Tags { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy
        /// </summary>
        [DataMember(Name="createdBy", EmitDefaultValue=false)]
        public CreatedBy CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets CreatedOn
        /// </summary>
        [DataMember(Name="createdOn", EmitDefaultValue=false)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy
        /// </summary>
        [DataMember(Name="updatedBy", EmitDefaultValue=false)]
        public UpdatedBy UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedOn
        /// </summary>
        [DataMember(Name="updatedOn", EmitDefaultValue=false)]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserStory {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  PlanEstimate: ").Append(PlanEstimate).Append("\n");
            sb.Append("  StoryPoints: ").Append(StoryPoints).Append("\n");
            sb.Append("  EffortEstimate: ").Append(EffortEstimate).Append("\n");
            sb.Append("  EffortRemaining: ").Append(EffortRemaining).Append("\n");
            sb.Append("  EffortLogged: ").Append(EffortLogged).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  Release: ").Append(Release).Append("\n");
            sb.Append("  Epic: ").Append(Epic).Append("\n");
            sb.Append("  Sprint: ").Append(Sprint).Append("\n");
            sb.Append("  Priority: ").Append(Priority).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
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
            return this.Equals(input as UserStory);
        }

        /// <summary>
        /// Returns true if UserStory instances are equal
        /// </summary>
        /// <param name="input">Instance of UserStory to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UserStory input)
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
                    this.PlanEstimate == input.PlanEstimate ||
                    (this.PlanEstimate != null &&
                    this.PlanEstimate.Equals(input.PlanEstimate))
                ) && 
                (
                    this.StoryPoints == input.StoryPoints ||
                    (this.StoryPoints != null &&
                    this.StoryPoints.Equals(input.StoryPoints))
                ) && 
                (
                    this.EffortEstimate == input.EffortEstimate ||
                    (this.EffortEstimate != null &&
                    this.EffortEstimate.Equals(input.EffortEstimate))
                ) && 
                (
                    this.EffortRemaining == input.EffortRemaining ||
                    (this.EffortRemaining != null &&
                    this.EffortRemaining.Equals(input.EffortRemaining))
                ) && 
                (
                    this.EffortLogged == input.EffortLogged ||
                    (this.EffortLogged != null &&
                    this.EffortLogged.Equals(input.EffortLogged))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.Owner == input.Owner ||
                    (this.Owner != null &&
                    this.Owner.Equals(input.Owner))
                ) && 
                (
                    this.Release == input.Release ||
                    (this.Release != null &&
                    this.Release.Equals(input.Release))
                ) && 
                (
                    this.Epic == input.Epic ||
                    (this.Epic != null &&
                    this.Epic.Equals(input.Epic))
                ) && 
                (
                    this.Sprint == input.Sprint ||
                    (this.Sprint != null &&
                    this.Sprint.Equals(input.Sprint))
                ) && 
                (
                    this.Priority == input.Priority ||
                    (this.Priority != null &&
                    this.Priority.Equals(input.Priority))
                ) && 
                (
                    this.Tags == input.Tags ||
                    (this.Tags != null &&
                    this.Tags.Equals(input.Tags))
                ) && 
                (
                    this.CreatedBy == input.CreatedBy ||
                    (this.CreatedBy != null &&
                    this.CreatedBy.Equals(input.CreatedBy))
                ) && 
                (
                    this.CreatedOn == input.CreatedOn ||
                    (this.CreatedOn != null &&
                    this.CreatedOn.Equals(input.CreatedOn))
                ) && 
                (
                    this.UpdatedBy == input.UpdatedBy ||
                    (this.UpdatedBy != null &&
                    this.UpdatedBy.Equals(input.UpdatedBy))
                ) && 
                (
                    this.UpdatedOn == input.UpdatedOn ||
                    (this.UpdatedOn != null &&
                    this.UpdatedOn.Equals(input.UpdatedOn))
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
                if (this.PlanEstimate != null)
                    hashCode = hashCode * 59 + this.PlanEstimate.GetHashCode();
                if (this.StoryPoints != null)
                    hashCode = hashCode * 59 + this.StoryPoints.GetHashCode();
                if (this.EffortEstimate != null)
                    hashCode = hashCode * 59 + this.EffortEstimate.GetHashCode();
                if (this.EffortRemaining != null)
                    hashCode = hashCode * 59 + this.EffortRemaining.GetHashCode();
                if (this.EffortLogged != null)
                    hashCode = hashCode * 59 + this.EffortLogged.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Owner != null)
                    hashCode = hashCode * 59 + this.Owner.GetHashCode();
                if (this.Release != null)
                    hashCode = hashCode * 59 + this.Release.GetHashCode();
                if (this.Epic != null)
                    hashCode = hashCode * 59 + this.Epic.GetHashCode();
                if (this.Sprint != null)
                    hashCode = hashCode * 59 + this.Sprint.GetHashCode();
                if (this.Priority != null)
                    hashCode = hashCode * 59 + this.Priority.GetHashCode();
                if (this.Tags != null)
                    hashCode = hashCode * 59 + this.Tags.GetHashCode();
                if (this.CreatedBy != null)
                    hashCode = hashCode * 59 + this.CreatedBy.GetHashCode();
                if (this.CreatedOn != null)
                    hashCode = hashCode * 59 + this.CreatedOn.GetHashCode();
                if (this.UpdatedBy != null)
                    hashCode = hashCode * 59 + this.UpdatedBy.GetHashCode();
                if (this.UpdatedOn != null)
                    hashCode = hashCode * 59 + this.UpdatedOn.GetHashCode();
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
