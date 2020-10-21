using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankkonto
{
    class Program
    {
        public struct Customer
        {
            public string name;
            public string address;
            public string phone;
            public int numberOfAccs;
            public int[] account;
        }
        static void Main(string[] args)
        {
            Customer[] custs = new Customer[10];
  
            int custNumber = 0;

            //Relative path to executable
            string path = "people.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            int count = 0;
            foreach (string line in lines)
            {
                string[] columns = line.Split(':');
                custs[count].name = columns[0];
                custs[count].address = columns[1];
                custs[count].phone = columns[2];
                string temp = columns[3];
                string[] temp2 = temp.Substring(0,19).Split(',');
                custs[count].account = new int[10];
                for (int i = 0; i < temp2.Length; i++)
                {
                    custs[count].account[i] = int.Parse(temp2[i]);
                }   
                
                custs[count].numberOfAccs = int.Parse(columns[4]);
                count++;
            }


            

            
            string userInput = "";
            while (userInput != "q")
            {
                DrawMenu(custs[custNumber]);
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Från vilket konto?");
                        int.TryParse(Console.ReadLine(), out int acc);
                        if(acc <= custs[custNumber].numberOfAccs)
                        {
                            custs[custNumber].account[acc-1] -= Withdraw(custs[custNumber], acc);
                        }
                        else
                        {
                            Console.WriteLine("Det är inget konto du har");
                        }
                        
                        break;
                    case "2":
                        Console.WriteLine("Till vilket konto?");
                        int.TryParse(Console.ReadLine(), out int ac);
                        if (ac <= custs[custNumber].numberOfAccs)
                        {
                            custs[custNumber].account[ac-1] += Deposit();
                        }
                        else
                        {
                            Console.WriteLine("Det är inget konto du har");
                        }

                        break;
                    case "3":
                        Balance(custs[custNumber]);
                        break;
                    case "4":
                        Information(custs[custNumber]);
                        break;
                    case "5":
                        custs[custNumber].numberOfAccs += NewAccount(custs[custNumber]);
                        break;
                    case "6":
                        Console.WriteLine("Ange ditt kundnummer");
                        int.TryParse(Console.ReadLine(), out int cus);
                        if(custs[cus].name != null)
                        {
                            custNumber = cus;
                        }
                        else
                        {
                            Console.WriteLine("Det kundnumret finns inte");
                        }
                        break;
                    case "q":

                        break;
                }

                
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < custs.Length; i++)
            {
                if (custs[i].account != null)
                {
                    sb.Append(custs[i].name + ":");
                    sb.Append(custs[i].address + ":");
                    sb.Append(custs[i].phone + ":");
                    sb.Append(string.Join(",", custs[i].account) + ":");
                    sb.Append(custs[i].numberOfAccs + ":");
                    sb.Append(Environment.NewLine);

                }



            }

            
            string[] save = sb.ToString().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            System.IO.File.WriteAllLines(path, save);
            Console.ReadKey();
            
        }


        static void DrawMenu(Customer n)
        {
            Console.WriteLine("**************************");
            Console.WriteLine("Hej {0} ", n.name);
            Console.WriteLine("1. Ta ut pengar");
            Console.WriteLine("2. Sätta in pengar");
            Console.WriteLine("3. Se saldo");
            Console.WriteLine("4. Se din information");
            Console.WriteLine("5. Öppna nytt konto");
            Console.WriteLine("6. Logga ut");
        }

        static int Withdraw(Customer n, int acc)
        {
            Console.WriteLine("Du har {0} på kontot", n.account[acc-1]);
            Console.WriteLine("Hur mycket vill du ta ut?");
            if (int.TryParse(Console.ReadLine(), out int numb))
            {
                if (numb < 0)
                {
                    Console.WriteLine("Du har rapporterats for att försoka fuska systemet");
                    return 0;
                }
                if (n.account[acc-1] >= numb)
                {
                    return numb;
                }
                else
                {
                    Console.WriteLine("Du forsökte ta ut mer an du har");
                    
                }

            }
            
         

            return 0;
        }
        static int Deposit()
        {
            Console.WriteLine("Hur mycket vill du sätta in?");
            if(int.TryParse(Console.ReadLine(), out int numb))
            {
                if (numb < 0)
                {
                    Console.WriteLine("Om du vill ta ut pengar välj 2 i menyn");
                    return 0;
                }
                return numb;
            }
           
            Console.WriteLine("Skriv in siffor tack");
            return 0;
        }

        static void Balance(Customer n)
        {
            
            for (int i = 0; i < n.numberOfAccs; i++)
            {
                Console.WriteLine("Du har {0} pa kontot {1}", n.account[i], i + 1);
            }
        }

        static void Information(Customer n)
        {
            Console.WriteLine("**************************");
            Console.WriteLine("Hej {0} ", n.name);
            Console.WriteLine("Du bor på {0} ", n.address);
            Console.WriteLine("Du har telefon {0} ", n.phone);
            Console.WriteLine("Du har {0} konton", n.numberOfAccs);

        }

        static int NewAccount(Customer n)
        {
            Console.WriteLine("Du har {0} konton", n.numberOfAccs);
            if(n.numberOfAccs< 10)
            {
                Console.WriteLine("Vill du oppna ett nytt konto, tryck Y for att öppna ett");
                string val = Console.ReadLine();
                if (val == "y")
                {
                    return 1;
                }
                return 0;
            }

            Console.WriteLine("Du har redan tillräckligt många konton");
            return 0;

        }
    }
}
