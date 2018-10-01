using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {

        #region Queries
        protected readonly string insertStatement = @"INSERT INTO [Employee](
                    EmployeeID,
                    FirstName,
                    MiddleName,
                    LastName,
                    DepartmentID,
                    HireDate)
                    VALUES
                    (
                    @EmployeeID,
                    @FirstName,
                    @MiddleName,
                    @LastName,
                    @DepartmentID,
                    @HireDate
                    )";

        protected readonly string deleteStatement = "DELETE FROM [Employee] WHERE EmployeeID = @EmployeeID";

        protected readonly string updateStatement = @"UPDATE [Employee] SET
                        FirstName = @FirstName, 
                        MiddleName = @MiddleName, 
                        LastName=@LastName,
                        DepartmentID = @DepartmentID
                        WHERE EmployeeID=@EmployeeID";

        private readonly string retrieveStatement = @"SELECT 
                E.EmployeeID,
                E.FirstName,
                E.MiddleName,
                E.LastName,
                D.DepartmentID,
                D.DepartmentName,
E.HireDate
                FROM [Employee] AS E
                INNER JOIN DEPARTMENT AS D ON D.DepartmentID = E.DepartmentID";

        private readonly string retrieveFilter = @" WHERE E.EmployeeID = @EmployeeID";

        private readonly string retrieveAllStatement = @"SELECT 
                E.EmployeeID,
                E.FirstName,
                E.MiddleName,
                E.LastName,
                D.DepartmentID,
                D.DepartmentName,
E.HireDate
                FROM [Employee] AS E
                INNER JOIN DEPARTMENT AS D ON D.DepartmentID = E.DepartmentID";

        #endregion



        public EmployeeRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement + retrieveFilter;
            base.RetrieveAllStatement = this.retrieveAllStatement;

        }

        #region Methods

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override Employee LoadEntity(SqlDataReader reader)
        {
            Employee employee = new Employee
            {
                EmployeeID = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                MiddleName = reader.GetString(2),
                LastName = reader.GetString(3),
                Department = new Department
                {
                    DepartmentID = reader.GetGuid(4),
                    DepartmentName = reader.GetString(5)
                }
            };
            if (reader.IsDBNull(6))
            {
                employee.HireDate = null;
            }
            else if (!reader.IsDBNull(6))
            {
                employee.HireDate = reader.GetDateTime(6);
            }

            return employee;
        }

        protected override void LoadInsertParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = employee.EmployeeID;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = employee.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = employee.MiddleName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = employee.LastName;
            command.Parameters.Add("@DepartmentID", SqlDbType.UniqueIdentifier).Value = employee.Department.DepartmentID;

            if (employee.HireDate != null)
            {
                command.Parameters.Add("@HireDate", SqlDbType.DateTime).SqlValue = employee.HireDate;
            }
            else if (employee.HireDate == null)
            {
                command.Parameters.Add("@HireDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            //var parm = command.CreateParameter();

            //parm.IsNullable = true;
            //parm.ParameterName = "@HireDate";
            //parm.SqlDbType = SqlDbType.DateTime;
            //parm.Value =  employee.HireDate == null ? new  Nullable<DateTime>() : employee.HireDate;
            //command.Parameters.Add(parm);
        }

        protected override void LoadRetrieveParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = id;

        }

        protected override void LoadUpdateParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = employee.EmployeeID;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = employee.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = employee.MiddleName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = employee.LastName;
            command.Parameters.Add("@DepartmentID", SqlDbType.UniqueIdentifier).Value = employee.Department.DepartmentID;
        }

        #endregion

    }
}
