using Microsoft.AspNetCore.Mvc.Rendering;

namespace En_Luna.ViewModels
{
    public class SolicitationDeadlineEditViewModel
    {
        public int Id { get; set; }
        public int SolicitationId { get; set; }
        public int DeadlineTypeId { get; set; }
        public DateTime DeadlineDate { get; set; }

        public SelectList? DeadlineTypes { get; set; }
    }
}
