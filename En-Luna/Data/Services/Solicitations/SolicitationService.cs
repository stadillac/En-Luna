using En_Luna.Data.Models;

namespace En_Luna.Data.Services
{
    /// <inheritdoc />
    public class SolicitationService : BaseService<Solicitation>, ISolicitationService
    {
        public SolicitationService(ApplicationContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public Solicitation Activate(Solicitation solicitation)
        {
            solicitation.IsActive = true;
            _context.Solicitations.Update(solicitation);
            _context.SaveChanges();
            return solicitation;
        }

        /// <inheritdoc />
        public Solicitation Approve(Solicitation solicitation, bool isApproved)
        {
            solicitation.IsApproved = isApproved;
            _context.Solicitations.Update(solicitation);
            _context.SaveChanges();
            return solicitation;
        }

        /// <inheritdoc />
        public Solicitation Cancel(Solicitation solicitation, bool isCancelled)
        {
            solicitation.IsCancelled = isCancelled;
            _context.Solicitations.Update(solicitation);
            _context.SaveChanges();
            return solicitation;
        }

        /// <inheritdoc />
        public Solicitation Complete(Solicitation solicitation, bool isComplete)
        {
            solicitation.IsComplete = isComplete;
            _context.Solicitations.Update(solicitation);
            _context.SaveChanges();
            return solicitation;
        }

        /// <inheritdoc />
        public Solicitation Deactivate(Solicitation solicitation)
        {
            solicitation.IsActive = false;
            _context.Solicitations.Update(solicitation);
            _context.SaveChanges();
            return solicitation;
        }
    }
}
