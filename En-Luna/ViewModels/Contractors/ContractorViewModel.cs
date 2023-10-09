using System.Collections.ObjectModel;

namespace En_Luna.ViewModels
{
    public class ContractorViewModel
    {
        public int Id { get; set; }
        public int JobTypeJobSubtypeId { get; set; }
        public UserViewModel? Account { get; set; }
        public ICollection<LicenseViewModel> Licenses { get; set; } = new Collection<LicenseViewModel>();
        public ICollection<SolicitationContractorViewModel> Solicitations { get; set; } = new Collection<SolicitationContractorViewModel>();
        public ICollection<SoftwareViewModel> AvailableSoftware { get; set; } = new Collection<SoftwareViewModel>();
    }
}
