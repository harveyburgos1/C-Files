using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
    {

        #region Queries
        private readonly string insertStatement = @"INSERT INTO Department(
                DepartmentID,
	            DepartmentName,
                IsActive)
	            VALUES
               (@departmentID,
                @departmentName,
                @isActive)";

        private readonly string updateStatement = @"UPDATE [Department] 
	        SET 
	        DepartmentID = @departmentID,
	        DepartmentName = @departmentName,
	        IsActive = @isActive";

        private readonly string deleteStatement = @"DELETE FROM Department WHERE DepartmentID = @departmentID";

        private readonly string retrieveStatement = @"	SELECT 
	        DepartmentID,
	        DepartmentName,
	        IsActive
	        FROM 
	        Department";

        private readonly string retrieveFilter = @" WHERE
                                    DepartmentID = @departmentID";

        #endregion
        public DepartmentRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement;
        }

        #region Parameters
        protected override void LoadInsertParameters(SqlCommand command, Department department)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = department.DepartmentID;
            command.Parameters.Add("@departmentName", SqlDbType.NVarChar, 50).Value = department.DepartmentName;
            command.Parameters.Add("@isActive", SqlDbType.Bit).Value = department.isActive;
        }

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;

        }

        protected override void LoadUpdateParameters(SqlCommand command, Department department)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = department.DepartmentID;
            command.Parameters.Add("@departmentName", SqlDbType.NVarChar, 50).Value = department.DepartmentName;
            command.Parameters.Add("@isActive", SqlDbType.Bit).Value = department.isActive;
        }


        protected override Department LoadEntity(SqlDataReader reader)
        {
            Department department = new Department
            {
                DepartmentID = reader.GetGuid(0),
                DepartmentName = reader.GetString(1),
                isActive = reader.GetBoolean(2)
            };
            return department;
        }

        protected override void LoadRetrieveParameters(SqlCommand command, Guid id)
        {
            base.RetrieveStatement += this.retrieveFilter;
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;

        }
        #endregion





    }

}
