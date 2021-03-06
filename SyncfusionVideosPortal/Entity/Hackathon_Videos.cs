//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncfusionVideosPortal.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hackathon_Videos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hackathon_Videos()
        {
            this.Hackathon_Comments = new HashSet<Hackathon_Comments>();
        }
    
        public int VideoId { get; set; }
        public string SlugTitle { get; set; }
        public int PlatformId { get; set; }
        public int ControlId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string VideoLink { get; set; }
        public bool IsLatest { get; set; }
        public bool IsFeature { get; set; }
        public string ThumbnailLink { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string platformshortname { get; set; }
        public Nullable<int> LikeCount { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public string Tags { get; set; }
    
        public virtual Hackathon_Control Hackathon_Control { get; set; }
        public virtual Hackathon_Platform Hackathon_Platform { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hackathon_Comments> Hackathon_Comments { get; set; }
    }
}
