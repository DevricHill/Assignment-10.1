using System.Xml.Serialization;
using System.Text.Json;

namespace Assignment_10._1
{
    internal class Program
    {
        static void CreateFolder()
        {
            string path = @"C:\Serialized";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        static List<Customer> CreateCustomerList()
        {
            List<Customer> list = new List<Customer>();
            Random rnd = new Random();
            int id = 1;
            int amount = rnd.Next(4, 10);

            for(int i = 0; i < amount; i++)
            {
                Customer customer = new Customer()
                {
                    CustomerId = id,
                    CustomerName = $"Temp Name {rnd.Next(3000)}",
                    CustomerEmail = $"tempemail{rnd.Next(3000)}@gmail.com",
                    IsActive = rnd.Next(10) < 5 ? true : false
                };

                list.Add(customer);
                id++;
            }

            return list;
        }

        static List<Employee> CreateEmployeeList()
        {
            List<Employee> list = new List<Employee>();

            Random rnd = new Random();
            int id = 1;
            int amount = rnd.Next(4, 10);

            for (int i = 0; i < amount; i++)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = id,
                    EmployeeName = $"Temp Name {rnd.Next(3000)}",
                    Department = (EmployeeDepartment)rnd.Next(3)
                };

                id++;
                list.Add(employee);
            }

            
            return list;
        }

        static void JsonSeralizeMethod<T>(string path, List<T> objectList)
        {
            Console.WriteLine("JSON Data Serialized");
            FileStream fs;


            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
  
            JsonSerializer.Serialize(fs, objectList);
              

            fs.Close();
        }

        static void JsonDeseralizeCustomerMethod(string path)
        {
            Console.WriteLine("JSON Data Deserializing");

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            List<Customer> list = JsonSerializer.Deserialize<List<Customer>>(fileStream);

            fileStream.Close();

            Console.WriteLine("JSON List Customer");

            foreach (var obj in list)
            {
                Console.WriteLine($"Customer: {obj.CustomerName} - {obj.CustomerId} - {obj.CustomerEmail}");
            }
        }

        static void JsonDeseralizeEmployeeMethod(string path)
        {
            Console.WriteLine("JSON Data Deserializing");

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            List<Employee> list = JsonSerializer.Deserialize<List<Employee>>(fileStream);

            fileStream.Close();

            Console.WriteLine("JSON List Employee");

            foreach (var obj in list)
            {
                Console.WriteLine($"Employee: {obj.EmployeeName} - {obj.EmployeeId} - {obj.Department.ToString()}");
            }
        }

        static void XMLSerializeMethod<T>(string path, List<T> list)
        {
            Console.WriteLine("XML Data Serializing");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            xmlSerializer.Serialize(fs, list);
            fs.Close();
        }

        static void XMLDeserializeCustomerMethod(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Customer>));
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<Customer> customer = (List<Customer>) xmlSerializer.Deserialize(fs);

            Console.WriteLine("XML List Customer");

            foreach (var obj in customer)
            {
                Console.WriteLine($"Customer: {obj.CustomerName} - {obj.CustomerId} - {obj.CustomerEmail}");
            }


        }

        static void XMLDeserializeEmployeeMethod(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<Employee> employee = (List<Employee>)xmlSerializer.Deserialize(fs);

            Console.WriteLine("XML List Employee");

            foreach (var obj in employee)
            {
                Console.WriteLine($"Employee: {obj.EmployeeName} - {obj.EmployeeId} - {obj.Department.ToString()}");
            }


        }

        static void RunSerialization()
        {
            CreateFolder();

            string customerPath = @"C:\Serialized\CustomerJson";
            string employeePath = @"C:\Serialized\EmployeeJson";

            List<Customer> customers = CreateCustomerList();
            JsonSeralizeMethod(customerPath + ".txt", customers);
            JsonDeseralizeCustomerMethod(customerPath + ".txt");

            XMLSerializeMethod<Customer>(customerPath + ".xml", customers);
            XMLDeserializeCustomerMethod(customerPath + ".xml");

            List<Employee> employees = CreateEmployeeList();
            JsonSeralizeMethod(employeePath + ".txt", employees);
            JsonDeseralizeEmployeeMethod(employeePath + ".txt");

            XMLSerializeMethod<Employee>(employeePath + ".xml", employees);
            XMLDeserializeEmployeeMethod(employeePath + ".xml");

        }

        static string[] WellParentheses(int n)
        {
            List<string> well = new List<string>();

            Recursion(well, "", 0, 0, n);

            return well.ToArray();
        }

        static void Recursion(List<string> list, string word, int open, int close, int n)
        {
            if (open == n && close == n)
            {
                list.Add(word);
                return;
            }

            if(open < n)
            {
                Recursion(list, word + "(", open + 1, close, n);
            }

            if (close < open)
            {
                Recursion(list, word + ")", open, close + 1, n);
            }
        }

        static void Result()
        {
            Console.Write("Select a number: ");
            int count;

            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.Write("Select a number: ");
            }

            string[] result = WellParentheses(count);

            foreach (string s in result)
            {
                Console.Write(s + "  ");
            }
        }

        static void Main(string[] args)
        {
            RunSerialization();
            Console.WriteLine();
            Result();
        }


    }
}
