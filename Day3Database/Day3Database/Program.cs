using Day3Database.Models;
using Day3Database.Repositories;
using System;
using System.Collections.Generic;

namespace Day3Database
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Applicant
            // var harvey = CreateApplicant("Harvey","Clemente","Burgos",new DateTime(2018,04,14));
            //var batman = CreateApplicant("Bruce", "Gotham", "Wayne", DateTime.Parse("1980,01,25"));

            //DeleteApplicant(harvey.ApplicantID);
            // UpdateApplicant(harvey);
            //var id = batman.ApplicantID;
            //RetrieveApplicant(harvey.ApplicantID);


            //var allApp = RetrieveAllApplicant();
            //foreach(var x in allApp)
            //{
            //    Console.WriteLine("Applicant ID: {0}", x.ApplicantID);
            //    Console.WriteLine("Applicant First Name: {0}", x.FirstName);
            //    Console.WriteLine("Applicant Middle Name: {0}", x.MiddleName);
            //    Console.WriteLine("Applicant Last Name: {0}", x.LastName);
            //    Console.WriteLine("Applicant BirthDate: {0}", x.BirthDate);
            //}
            //allApp.ForEach(x => DeleteApplicant(x.ApplicantID)); // DELETE ALL APPLICANTS

            #endregion

            #region Department

            //var newDepartment = CreateDepartment(new Department
            //{
            //    DepartmentID = Guid.NewGuid(),
            //    DepartmentName = "Accounting",
            //    isActive = true
            //});

            //   UpdateDepartment(newDepartment);

            //     DeleteDepartment(newDepartment.DepartmentID);
            // RetrieveDepartment(newDepartment.DepartmentID);
            //var allDepartment = RetrieveAllDepartment();
            //foreach (var x in allDepartment)
            //{
            //    Console.WriteLine("Department ID: {0}", x.DepartmentID);
            //    Console.WriteLine("Department Name: {0}", x.DepartmentName);
            //    Console.WriteLine("Is Active: {0}", x.isActive);
            //}
            #endregion

            #region Employee

            //CreateEmployee(new Employee
            //{
            //    EmployeeID = Guid.NewGuid(),
            //    FirstName = "Bruce",
            //    MiddleName ="Banner",
            //    LastName = "Banner",
            //    Department = new Department { DepartmentID = new Guid("93003371-AD48-4457-9F9A-495D2C3FF457") },
            //    HireDate = null

            //});

            //DeleteEmployee(new Guid("B4493245-1B9D-4A36-8174-16E2BB58912D"));

            //UpdateEmployee(new Employee
            //{
            //    EmployeeID = new Guid("BA1A462F-B46E-4A44-9F86-E4917B35FBF3"),
            //    FirstName = "Barry",
            //    MiddleName = "Starling",
            //    LastName = "Allen",
            //    Department = new Department { DepartmentID = new Guid("AC2D4134-2E4F-4FE7-9EB5-934331BCAAB4") }
            //});

            //var retrieve = RetrieveEmployee(new Guid("984DCBE8-88B1-4D5E-8690-A7EB333A5F58"));

            //Console.WriteLine("Employee ID: {0}", retrieve.EmployeeID);
            //Console.WriteLine("First Name: {0}", retrieve.FirstName);
            //Console.WriteLine("Middle Name ID: {0}", retrieve.MiddleName);
            //Console.WriteLine("Last Name: {0}", retrieve.LastName);
            //Console.WriteLine("Department: {0}", retrieve.Department.DepartmentName);

            var retrieve = RetrieveAllEmployee();

            foreach (var x in retrieve)
            {
                Console.WriteLine("Employee ID: {0}", x.EmployeeID);
                Console.WriteLine("Employee First Name: {0}", x.FirstName);
                Console.WriteLine("Employee Middle Name: {0}", x.MiddleName);
                Console.WriteLine("Employee Last Name: {0}", x.LastName);
                Console.WriteLine("Employee's Department: {0}", x.Department.DepartmentName);
                Console.WriteLine("Employee's Date of Hired: {0}", x.HireDate);
                Console.WriteLine("---- END -----");


            }
                #endregion

                Console.ReadLine();
        }


        #region ApplicantMethods
        static Applicant CreateApplicant(string firstName,string middleName, string lastName,DateTime birthDate)
        {
            Applicant applicant = new Applicant
            {
                ApplicantID = Guid.NewGuid(),
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                BirthDate = birthDate
            };

            var repository = new ApplicantRepository();
            var newApplicant = repository.Create(applicant);
            return applicant;
        }
        static void DeleteApplicant(Guid id)
        {
            var repository = new ApplicantRepository();
            repository.Delete(id);

        }

        static void UpdateApplicant(Applicant applicant)
        {
            applicant.FirstName = "HABS";
            applicant.LastName = "Updated";
            var repository = new ApplicantRepository();

            repository.Update(applicant);
        }

        static void RetrieveApplicant(Guid id)
        {
            var repository = new ApplicantRepository();
            var applicant = repository.Retrieve(id);

                Console.WriteLine("Applicant ID {0}", applicant.ApplicantID);
                Console.WriteLine("First Name: {0}", applicant.FirstName);
                Console.WriteLine("Middle Name: {0}", applicant.MiddleName);

                Console.WriteLine("Last Name: {0}", applicant.LastName);
                Console.WriteLine("Birth Date: {0}", applicant.BirthDate);
            
        }

       static List<Applicant> RetrieveAllApplicant()
        {
            var repository = new ApplicantRepository();

            return repository.Retrieve();
        }


        #endregion

        #region DepartmentMethods

        static Department CreateDepartment(Department newDepartment)
        {
            var repo = new DepartmentRepository();
            return repo.Create(newDepartment);
        }

        static void UpdateDepartment(Department newDepartment)
        {
            var repo = new DepartmentRepository();
            newDepartment.DepartmentName = "Accounting";
            newDepartment.isActive = false;
            repo.Update(newDepartment);
        }

        static void DeleteDepartment(Guid id)
        {
            var repo = new DepartmentRepository();
            repo.Delete(id);
        }


        static void RetrieveDepartment(Guid id)
        {
            var repo = new DepartmentRepository();
            var result = repo.Retrieve(id);
            Console.WriteLine("Department ID: {0}", result.DepartmentID);
            Console.WriteLine("Department Name: {0}", result.DepartmentName);
            Console.WriteLine("Is Active: {0}", result.isActive);
        }

        static List<Department> RetrieveAllDepartment()
        {
            var repo = new DepartmentRepository();
            return repo.Retrieve();
        }

        #endregion

        #region EmployeeMethods

        static void CreateEmployee(Employee employee)
        {
            var repo = new EmployeeRepository();
            repo.Create(employee);
        }

        static void DeleteEmployee(Guid id)
        {
            var repo = new EmployeeRepository();
            repo.Delete(id);
        }
         
        static void UpdateEmployee(Employee employee)
        {
            var repo = new EmployeeRepository();
            repo.Update(employee);
        }
        static Employee RetrieveEmployee(Guid id)
        {
            var repo = new EmployeeRepository();
            return repo.Retrieve(id);
        }

        static List<Employee> RetrieveAllEmployee()
        {
            var repo = new EmployeeRepository();
            return repo.Retrieve();
        }
        #endregion
    }
}
