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
    
    public partial class Hackathon_Comments
    {
        public int CommentId { get; set; }
        public Nullable<int> VideoId { get; set; }
        public string Comment { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Hackathon_Videos Hackathon_Videos { get; set; }
    }
}
