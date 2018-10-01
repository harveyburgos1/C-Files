using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public class ApplicantRepository : RepositoryBase<Applicant>
    {
        #region Queries
        protected readonly string insertStatement = @"INSERT INTO [Applicant](
                    ApplicantID,
                    FirstName,
                    MiddleName,
                    LastName,
                    BirthDate)
                    VALUES
                    (
                    @ApplicantID,
                    @FirstName,
                    @MiddleName,
                    @LastName,
                    @BirthDate
                    )";

        protected readonly string deleteStatement = "DELETE FROM [Applicant] WHERE ApplicantID = @ApplicantID";

        private readonly string updateStatement = @"UPDATE [Applicant] SET
                        FirstName = @FirstName, 
                        MiddleName = @MiddleName, 
                        LastName=@LastName,
                        BirthDate = @BirthDate 
                        WHERE ApplicantID=@ApplicantID  ";

        private readonly string retrieveStatement = @"SELECT 
                ApplicantID,
                FirstName,
                MiddleName,
                LastName,
                BirthDate
                FROM [Applicant]";

        private readonly string retrieveFilter = @" WHERE ApplicantID = @ApplicantID";

        #endregion
        public ApplicantRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement;
       //     base.RetrieveStatement = this.retrieveStatement + this.retrieveFilter;
            
        }

        #region Parameters
        protected override void LoadInsertParameters(SqlCommand command, Applicant applicant)
        {
            command.Parameters.Add("@ApplicantID", SqlDbType.UniqueIdentifier).Value = applicant.ApplicantID;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = applicant.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = applicant.MiddleName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = applicant.LastName;
            command.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = applicant.BirthDate;
        }

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@ApplicantID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override void LoadUpdateParameters(SqlCommand command, Applicant newApplicant)
        {
            command.Parameters.Add("@ApplicantID", SqlDbType.UniqueIdentifier).Value = newApplicant.ApplicantID;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = newApplicant.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = newApplicant.MiddleName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = newApplicant.LastName;
            command.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = newApplicant.BirthDate;
        }

        protected override void LoadRetrieveParameters(SqlCommand command, Guid id)
        {
            base.RetrieveStatement += this.retrieveFilter;
            command.Parameters.Add("@ApplicantID", SqlDbType.UniqueIdentifier).Value = id;
        }

        #endregion

        protected override Applicant LoadEntity(SqlDataReader reader)
        {
            Applicant applicant = new Applicant
            {
                ApplicantID = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                MiddleName = reader.GetString(2),
                LastName = reader.GetString(3),
                BirthDate = reader.GetDateTime(4)
            };

            return applicant;
        }


    }
}
