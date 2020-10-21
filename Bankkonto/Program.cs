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
            custs[custNumber].name = "greta";
            custs[custNumber].address = "Gatan 123";
            custs[custNumber].phone = "0800000";
            custs[custNumber].account = new int[10];
            custs[custNumber].numberOfAccs = 1;
            custs[1].name = "Hans";
            custs[1].address = "V'gen 123";
            custs[1].phone = "0700000";
            custs[1].account = new int[10];
            custs[1].numberOfAccs = 1;


            string userInput = "";
            while (userInput != "q")
            {
                DrawMenu(custs[custNumber]);
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Fr[n vilket konto?");
                        int.TryParse(Console.ReadLine(), out int acc);
                        if(acc <= custs[custNumber].numberOfAccs)
                        {
                            custs[custNumber].account[acc-1] -= Withdraw(custs[custNumber], acc);
                        }
                        else
                        {
                            Console.WriteLine("Det ar inget konto du har");
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
                            Console.WriteLine("Det ar inget konto du har");
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

            Console.ReadKey();
            
        }


        static void DrawMenu(Customer n)
        {
            Console.WriteLine("**************************");
            Console.WriteLine("Hej {0} ", n.name);
            Console.WriteLine("1. Ta ut pengar");
            Console.WriteLine("2. Satta in pengar");
            Console.WriteLine("3. Se saldo");
            Console.WriteLine("4. Se din information");
            Console.WriteLine("5. Oppna nytt konto");
            Console.WriteLine("6. Logga ut");
        }

        static int Withdraw(Customer n, int acc)
        {
            Console.WriteLine("Du har {0} pa kontot", n.account[acc-1]);
            Console.WriteLine("Hur mycket vill du ta ut?");
            if (int.TryParse(Console.ReadLine(), out int numb))
            {
                if (numb < 0)
                {
                    Console.WriteLine("Du har rapporterats for att forsoka fuska systemet");
                    return 0;
                }
                if (n.account[acc-1] >= numb)
                {
                    return numb;
                }
                else
                {
                    Console.WriteLine("Du forsokte ta ut mer an du har");
                    
                }

            }
            
         

            return 0;
        }
        static int Deposit()
        {
            Console.WriteLine("Hur mycket vill du satta in?");
            if(int.TryParse(Console.ReadLine(), out int numb))
            {
                if (numb < 0)
                {
                    Console.WriteLine("Om du vill ta ut pengar v;lj 2 i menyn");
                    return 0;
                }
                return numb;
            }
           
            Console.WriteLine("Skriv in siffor tack?");
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
            Console.WriteLine("Hej {0} med kundnummer {1}", n.name);
            Console.WriteLine("Du bor  {0} ", n.address);
            Console.WriteLine("Du har telefon {0} ", n.phone);
            Console.WriteLine("Du har {0} konton", n.numberOfAccs);

        }

        static int NewAccount(Customer n)
        {
            Console.WriteLine("Du har {0} konton", n.numberOfAccs);
            if(n.numberOfAccs< 10)
            {
                Console.WriteLine("Vill du oppna ett nytt konto, tryck Y for att oppna ett");
                string val = Console.ReadLine();
                if (val == "y")
                {
                    return 1;
                }
                return 0;
            }

            Console.WriteLine("Du har redan tillr'ckligt manga konton");
            return 0;

        }
    }
}
