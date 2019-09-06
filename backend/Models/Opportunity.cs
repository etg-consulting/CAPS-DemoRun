using System;
using System.Collections.Generic;

namespace Caps.Models
{
    public class Opportunity
    {
        public long Id { get; set; }
        public string AssociatedRFPControl { get; set; } = String.Empty;
        public DateTime PostingDate { get; set; } = DateTime.Today;
        public DateTime ClosingDate { get; set; }
        public ApprovalStatus Status { get; set; } = ApprovalStatus.Draft;
        public Decimal Value { get; set; } = 0;
        public List<Skill> RequiredSkills { get; set; } = new List<Skill>();
    }
}