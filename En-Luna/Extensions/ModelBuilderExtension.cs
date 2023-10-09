using En_Luna.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace En_Luna.Extensions
{
    internal static class ModelBuilderExtension
    {
        public static void ConfigureIdentityTableNames(this ModelBuilder builder) 
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User", "Identity");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role", "Identity");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", "Identity");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });

        }

        public static void DefineRelationships(this ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>()
                .HasOne(x => x.Address)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Contractor)
                .WithOne(x => x.Account)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(x => x.Solicitor)
                .WithOne(x => x.Account)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(x => x.BankAccount)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(x => x.CompanyType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(x => x.ContractorReviews)
                .WithOne(x => x.ContractorUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(x => x.SolicitorReviews)
                .WithOne(x => x.SolicitorUser)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Address

            modelBuilder.Entity<Address>()
                .HasOne(x => x.State)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Contractor

            modelBuilder.Entity<Contractor>()
                .HasOne(x => x.ProfessionDiscipline)
                .WithMany(x => x.Contractors)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Contractor>()
                .HasMany(x => x.Licenses)
                .WithOne(x => x.Contractor)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region ContractorSoftware

            modelBuilder.Entity<ContractorSoftware>()
                .HasOne(x => x.Contractor)
                .WithMany(x => x.AvailableSoftware)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContractorSoftware>()
                .HasOne(x => x.Software)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Document

            modelBuilder.Entity<Document>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Documents)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Discipline

            modelBuilder.Entity<Discipline>()
                .HasMany(x => x.ProfessionDisciplines)
                .WithOne(x => x.Discipline)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Profession

            modelBuilder.Entity<Profession>()
                .HasMany(x => x.ProfessionDisciplines)
                .WithOne(x => x.Profession)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region License

            modelBuilder.Entity<License>()
                .HasOne(x => x.State)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Review 

            modelBuilder.Entity<Review>()
                .HasOne(x => x.ContractorUser)
                .WithMany(x => x.ContractorReviews)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(x => x.SolicitorUser)
                .WithMany(x => x.SolicitorReviews)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Solicitation

            modelBuilder.Entity<Solicitation>()
                .HasOne(x => x.Solicitor)
                .WithMany(x => x.Solicitations)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solicitation>()
                .HasOne(x => x.Deadline)
                .WithOne(x => x.Solicitation)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solicitation>()
                .HasMany(x => x.Contractors)
                .WithOne(x => x.Solicitation)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SolicitationContractor

            modelBuilder.Entity<SolicitationContractor>()
                .HasOne(x => x.Contractor)
                .WithMany(x => x.Solicitations)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SolicitationContractor>()
                .HasMany(x => x.StatusUpdates)
                .WithOne(x => x.Contractor)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SolicitationDeadline

            modelBuilder.Entity<SolicitationDeadline>()
                .HasOne(x => x.DeadlineType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SolicitationRole

            modelBuilder.Entity<SolicitationRole>()
                .HasMany(x => x.ProvidedSoftware)
                .WithOne(x => x.SolicitationRole)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SolicitationRole>()
                .HasMany(x => x.RequiredSoftware)
                .WithOne(x => x.SolicitationRole)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Solicitor

            modelBuilder.Entity<Solicitor>()
                .HasMany(x => x.StatusUpdates)
                .WithOne(x => x.Solicitor)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Specialty

            modelBuilder.Entity<Expertise>()
                .HasOne(x => x.Focus)
                .WithMany(x => x.Expertises)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
