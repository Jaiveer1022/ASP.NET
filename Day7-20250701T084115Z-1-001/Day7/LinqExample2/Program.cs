using System.Diagnostics;

namespace LinqExample2
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public decimal Basic { get; set; }
        public int DeptNo { get; set; }
        public string Gender { get; set; }
        public override string ToString()
        {
            string s = Name + "," + EmpNo.ToString() + "," + Basic.ToString() + "," + DeptNo.ToString();
            return s;
        }
    }
    public class Department
    {
        public int DeptNo { get; set; }
        public string DeptName { get; set; }
    }
    internal class Program
    {
        static List<Employee> lstEmp = new List<Employee>();
        static List<Department> lstDept = new List<Department>();
        public static void AddRecs()
        {
            lstDept.Add(new Department { DeptNo = 10, DeptName = "SALES" });
            lstDept.Add(new Department { DeptNo = 20, DeptName = "MKTG" });
            lstDept.Add(new Department { DeptNo = 30, DeptName = "IT" });
            lstDept.Add(new Department { DeptNo = 40, DeptName = "HR" });

            lstEmp.Add(new Employee { EmpNo = 1, Name = "Vikram", Basic = 10000, DeptNo = 10, Gender = "M" });
            lstEmp.Add(new Employee { EmpNo = 2, Name = "Vishal", Basic = 11000, DeptNo = 10, Gender = "M" });


            lstEmp.Add(new Employee { EmpNo = 3, Name = "Abhijit", Basic = 12000, DeptNo = 20, Gender = "M" });
            lstEmp.Add(new Employee { EmpNo = 4, Name = "Mona", Basic = 11000, DeptNo = 20, Gender = "F" });
            lstEmp.Add(new Employee { EmpNo = 5, Name = "Shweta", Basic = 12000, DeptNo = 20, Gender = "F" });


            lstEmp.Add(new Employee { EmpNo = 6, Name = "Sanjay", Basic = 11000, DeptNo = 30, Gender = "M" });
            lstEmp.Add(new Employee { EmpNo = 7, Name = "Arpan", Basic = 10000, DeptNo = 30, Gender = "M" });


            lstEmp.Add(new Employee { EmpNo = 8, Name = "Shraddha", Basic = 11000, DeptNo = 40, Gender = "F" });
        }
        static Employee GetAllEmployees(Employee emp)
        {
            return emp;
        }
        static void Main1()
        {
            AddRecs();
            //var emps = from emp in lstEmp select emp;

            //var emps = lstEmp.Select(new Func<Employee, Employee>(GetAllEmployees));
            var emps1 = lstEmp.Select(GetAllEmployees);
            var emps2 = lstEmp.Select(delegate (Employee emp)
            {
                return emp;
            });
            var emps3 = lstEmp.Select(emp => emp);
            foreach (var item in emps3)
            {
                Console.WriteLine(item);
            }

        }
        static void Main2()
        {
            AddRecs();
            var emps = from emp in lstEmp select emp.EmpNo;
            var emps3 = lstEmp.Select(emp => emp.EmpNo);
            foreach (var item in emps3)
            {
                Console.WriteLine(item);
            }

        }
        static void Main3()
        {
            AddRecs();
            //var retval = from single_object in collection select one_thing
            var emps = from emp in lstEmp select new { emp.Name, emp.EmpNo };
            var emps2 = lstEmp.Select(delegate (Employee emp) { return new { emp.Name, emp.EmpNo }; });
            var emps3 = lstEmp.Select(emp=> new { emp.Name, emp.EmpNo } );

            //var emps = from emp in lstEmp select emp.EmpNo;

            foreach (var item in emps3)
            {
                Console.WriteLine(item);
            }

        }
        static bool IsBasicGreaterThan10000(Employee emp)
        {
            return emp.Basic > 10000;
        }
        static void Main4()
        {
            AddRecs();
            //var emps = from emp in lstEmp
            //           where emp.Basic > 10000
            //           select emp;

            var emps2 = lstEmp.Where(IsBasicGreaterThan10000);
            var emps3a = lstEmp.Where(emp => emp.Basic > 10000);
            var emps3b = lstEmp.Where(emp => emp.Basic > 10000).Select(emp=>emp);
            var emps3c = lstEmp.Select(emp => emp).Where(emp => emp.Basic > 10000);


            var emps4b = lstEmp.Where(emp => emp.Basic > 10000).Select(emp => emp.Name);
            //var emps4c = lstEmp.Select(emp => emp.Name).Where(emp => emp.Basic > 10000); //error

            var emps5b = lstEmp.Where(emp => emp.Basic > 10000).Select(emp => emp.Basic);
            //var emps5c = lstEmp.Select(emp => emp.Basic).Where(emp => emp.Basic > 10000); //error
            var emps5d = lstEmp.Select(emp => emp.Basic).Where(emp => emp > 10000); //works, but avoid

            foreach (var item in emps2)
            {
                Console.WriteLine(item);
            }
        }
        static void Main5()
        {
            AddRecs();

            var emps = from emp in lstEmp
                           //where emp.Basic > 10000
                       orderby emp.Name
                       select emp;

            var emps2a = lstEmp.OrderBy(emp => emp.Name);
            var emps2b = lstEmp.OrderByDescending(emp => emp.Name);

            //var emps = from emp in lstEmp
            //           orderby emp.Name descending
            //           select emp;

            //var emps = from emp in lstEmp
            //           orderby emp.DeptNo ascending, emp.Name descending
            //           select emp;
            var emps3 = lstEmp.OrderBy(emp => emp.DeptNo).ThenByDescending(emp=>emp.Name) ;

            foreach (var emp in emps)
            {
                Console.WriteLine(emp);
            }

            Console.ReadLine();
        }
        static void Main6()
        {
            AddRecs();


            var emps = from emp in lstEmp
                       join dept in lstDept
                       on emp.DeptNo equals dept.DeptNo
                       select new { emp, dept };

            //var emps = from  in 
            //           join  in 
            //           on  equals 
            //           select;

            var empsa = lstEmp.Join(lstDept, emp => emp.DeptNo, 
                dept => dept.DeptNo, (emp, dept) => new { emp, dept });

            foreach (var item in emps)
            {
                Console.WriteLine(item.emp.Name);
                Console.WriteLine(item.dept.DeptName);
            }


            Console.WriteLine();
            Console.WriteLine();


            var emps2 = from emp in lstEmp
                        join dept in lstDept
                        on emp.DeptNo equals dept.DeptNo
                        select new { emp.Name, dept.DeptName };
            foreach (var item in emps2)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.DeptName);

            }

        }
        //deferred execution
        static void MainDef()
        {
            AddRecs();
            var emps = from emp in lstEmp select emp;

            Console.WriteLine();
            lstEmp.RemoveAt(0);

            foreach (var emp in emps)
            {
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            Console.ReadLine();


            Console.WriteLine();
            lstEmp.Add(new Employee { EmpNo = 9, Name = "New", Basic = 11000, DeptNo = 40, Gender = "F" });
            foreach (var emp in emps)
            {
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            Console.ReadLine();
        }
        //immediate execution
        static void MainImm()
        {
            AddRecs();
            var emps = from emp in lstEmp select emp;
            //var emps = (from emp in lstEmp select emp).ToList();

            //immediate execution
            emps = emps.ToList();  //.ToArray .ToDictionary
            //Employee [] arrEmps = emps.ToArray();
            //Dictionary<int, Employee> d = emps.ToDictionary(e => e.EmpNo);

            Console.WriteLine();
            lstEmp.RemoveAt(0);
            foreach (var emp in emps)
            {
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            Console.ReadLine();
            Console.WriteLine();
            lstEmp.Add(new Employee { EmpNo = 9, Name = "New", Basic = 11000, DeptNo = 40, Gender = "F" });
            foreach (var emp in emps)
            {
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            Console.ReadLine();
        }
        //Single
        static void MainSingle()
        {
            AddRecs();
            Employee emp = lstEmp.Single(e => e.EmpNo == 1); //one record = okay
            //Employee emp = lstEmp.Single(e => e.EmpNo == 10);  //no records = error
            //Employee emp = lstEmp.Single(e => e.Basic > 5000); //multiple records - error


            //Employee emp = lstEmp.SingleOrDefault(e => e.EmpNo == 1); //one record = okay
            //Employee emp = lstEmp.SingleOrDefault(e => e.EmpNo == 10); //no records=null
            //Employee emp = lstEmp.SingleOrDefault(e => e.Basic > 5000);//multiple records - error

            if (emp != null)
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            else
                Console.WriteLine("not found");
            Console.ReadLine();

            //lstEmp.ElementAt(10)
            //lstEmp.ElementAtOrDefault(10)
            //lstEmp.First()
            //lstEmp.First(e => e.Basic > 10000);

            //lstEmp.FirstOrDefault()
            //lstEmp.Last();
            ///lstEmp.LastOrDefault();
        }

        //simple group
        static void MainSimpleGrp()
        {
            AddRecs();
            var emps = from emp in lstEmp
                       group emp by emp.DeptNo;
            foreach (var item in emps)
            {

                Console.WriteLine(item.Key); //deptno
                foreach (var e in item)  //e is an Employee, item is a grouping of Employee
                {
                    Console.WriteLine(e);
                }
                Console.ReadLine();
            }

        }
        //group with name
        static void MainGrpName()
        {
            AddRecs();
            //var emps = from emp in lstEmp
            //           group emp by emp.DeptNo;

            var emps = from emp in lstEmp
                       group emp by emp.DeptNo into group1
                       select group1;


            foreach (var item in emps)
            {

                Console.WriteLine(item.Key); //deptno
                foreach (var e in item)  //e is an Employee, item is a grouping of Employee
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine();
            }

            Console.ReadLine();

        }
        //group with properties
        static void Maingrpnamewithprop()
        {
            //Employee e1 = lstEmp.FirstOrDefault(emp => emp.Basic > 100000);

            AddRecs();

            var emps = from emp in lstEmp
                       group emp by emp.DeptNo into group1
                       select new { group1, Count = group1.Count(), Max = group1.Max(x => x.Basic), Min = group1.Min(x => x.Basic) };


            foreach (var item in emps)
            {

                Console.WriteLine(item.group1.Key); //DeptNo
                Console.WriteLine(item.Count); //count
                Console.WriteLine(item.Min); //min
                Console.WriteLine(item.Max); //max
                //emp.group1.Key;  //DeptNo

                foreach (var e in item.group1)  //e is an Employee
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine();
            }

            Console.ReadLine();

        }

        //plinq
        static void Main()
        {
            //AddRecs();
            AddRecs2();
            //plinq example

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var emps = lstEmp.Select(emp => new { Name = LongRunningFunc(emp.Name), emp.EmpNo });
            //var emps = lstEmp.AsParallel().WithDegreeOfParallelism(4).Select(emp => new { Name = LongRunningFunc(emp.Name), emp.EmpNo });

            //var emps = lstEmp.AsParallel().Select(emp => new { Name = LongRunningFunc(emp.Name), emp.EmpNo });
            //var emps = lstEmp.AsParallel().AsOrdered().Select(emp => new { Name = LongRunningFunc(emp.Name), emp.EmpNo });
            //var emps = lstEmp.AsParallel().AsOrdered().WithDegreeOfParallelism(2).Select(emp => new { Name = LongRunningFunc(emp.Name), emp.EmpNo });

            //var emps2 = lstEmp.AsParallel();

            //Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            foreach (var emp in emps)
            {
                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);


        }
        public static void AddRecs2()
        {
            for (int i = 0; i < 200; i++)
            {
                lstEmp.Add(new Employee { EmpNo = i + 1, Name = "Vikram" + i, Basic = 10000, DeptNo = 10, Gender = "M" });
            }
        }
        public static string LongRunningFunc(string s)
        {
            System.Threading.Thread.Sleep(10);
            return s.ToUpper();
        }
    }
}
