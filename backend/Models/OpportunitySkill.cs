namespace Caps.Models
{
    public class OpportunitySkill
    {
        public long Id { get; set; }
        public Opportunity Opportunity { get; set; }
        public Skill Skill { get; set; }
    }
}