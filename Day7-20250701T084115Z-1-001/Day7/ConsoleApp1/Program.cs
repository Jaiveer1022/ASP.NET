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


    public class Program
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
            //var emps = lstEmp.Select(new Func<Employee, Employee>(GetAllEmployees;

            // var emps = lstEmp.Select(GetAllEmployees);

            var emps = lstEmp.Select(emp => emp);

            foreach (var item in emps)
            {

                Console.WriteLine(item);
            }
        }

        static void Main2()
        {

            AddRecs();

            //var emps = from emp in lstEmp
            //           join dept in lstDept
            //           on emp.DeptNo equals dept.DeptNo
            //           select new { emp.Name, dept.DeptNo };

            //var emps = lstEmp.Join(lstDept, emp => emp.DeptNo, dept => dept.DeptNo, (emp, dept) => new { emp.Name, dept.DeptNo });

            //foreach (var item in emps)
            //{
            //    Console.WriteLine(item.Name + " -> "+ item.DeptNo);
            //}

            //Employee emp = lstEmp.Single(ep => ep.Name == "Vikram");
            //Console.WriteLine(emp);

            var emps = from emp in lstEmp
                       group emp by emp.DeptNo into g1
                       select new { g1, count = g1.Count(), max = g1.Max(emp => emp.Basic), min = g1.Min(emp => emp.Basic) };
            

            foreach (var item in emps) {
                
                Console.WriteLine(item.g1.Key);
                Console.WriteLine(item.count); //count
                Console.WriteLine(item.min); //min
                Console.WriteLine(item.max); //max

                foreach (var item1 in item.g1) { 
                    Console.WriteLine(item1); 
                }
                Console.WriteLine();
            }
        }

        public static void Main() { 
        
            AddRecs2();

            Stopwatch sw = new Stopwatch();

            sw.Start();

            var emps = lstEmp.Select(emp => new { Name = GetName(emp.Name), emp.EmpNo });

            foreach (var emp in emps) {

                Console.WriteLine(emp.Name + "," + emp.EmpNo);
            }
            sw.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", sw.ElapsedMilliseconds);


        }

        public static string GetName(string s) {

            System.Threading.Thread.Sleep(10);
            return s.ToUpper();
        }

        public static void AddRecs2()
        {
            for (int i = 0; i < 200; i++)
            {
                lstEmp.Add(new Employee { EmpNo = i + 1, Name = "Vikram" + i, Basic = 10000, DeptNo = 10, Gender = "M" });
            }
        }

    }
}