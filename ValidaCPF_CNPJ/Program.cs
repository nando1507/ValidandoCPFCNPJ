using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ValidaCPF_CNPJ
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = menu();

            while (i != 3)
            {
                switch (i)
                {
                    case 1:
                        Console.WriteLine("Informe CPF:");
                        string CPF = Console.ReadLine();
                        if (ValidaCPF(CPF))
                        {
                            Console.WriteLine("CPF Válido");
                        };
                        break;
                    case 2:
                        Console.WriteLine("Informe CNPJ:");
                        string CNPJ = Console.ReadLine();
                        if (ValidaCNPJ(CNPJ))
                        {
                            Console.WriteLine("CNPJ Válido");
                        };
                        break;
                    case 3:
                        Console.WriteLine("Encerrando");
                        return;
                    default:
                        Console.WriteLine("Opção Invalida");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
                i = menu();
            }
        }

        static int menu()
        {
            Console.WriteLine("Selecione a Opção de Validação");
            Console.WriteLine("1. CPF");
            Console.WriteLine("2. CNPJ");
            Console.WriteLine("3. Sair");
            return int.Parse(Console.ReadLine());
        }

        static bool ValidaCPF(string CPF)
        {
            int[] indice = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            IList<ModeloCalculo> list = new List<ModeloCalculo>();
            for (int i = 0; i < CPF.Length; i++)
            {
                ModeloCalculo mc = new ModeloCalculo();
                mc.id = i + 1;
                mc.digito = int.Parse(CPF.Substring(i, 1));
                mc.posicao1 = CPF.Length - i - 1;
                mc.resultado1 = mc.digito * mc.posicao1;
                mc.posicao2 = CPF.Length - i;
                mc.resultado2 = mc.digito * mc.posicao2;

                list.Add(mc);
            }

            var dvCalc1 = (list.Where(w => w.posicao1 >= 2).Sum(s => s.resultado1) * 10) % 11;
            var dvCalc2 = (list.Where(w => w.posicao2 >= 2).Sum(s => s.resultado2) * 10) % 11;

            var dvOrig1 = list.Where(w => w.id == 10).Select(s => s.digito).FirstOrDefault();
            var dvOrig2 = list.Where(w => w.id == 11).Select(s => s.digito).FirstOrDefault();

            if (dvCalc1 == dvOrig1 && dvCalc2 == dvOrig2)
            {
                return true;
            }
            else
            {
                if (dvCalc1 != dvOrig1)
                {
                    Console.WriteLine($"Digito Invalido {dvOrig1}");
                }
                if (dvCalc2 != dvOrig2)
                {
                    Console.WriteLine($"Digito Invalido {dvOrig2}");
                }
                return false;
            }
        }

        static bool ValidaCNPJ(string CNPJ)
        {
            int[] indice = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            IList<ModeloCalculo> list = new List<ModeloCalculo>();
            for (int i = 0; i < CNPJ.Length; i++)
            {
                ModeloCalculo mc = new ModeloCalculo();
                mc.id = i + 1;
                mc.digito = int.Parse(CNPJ.Substring(i, 1));
                mc.posicao1 = indice[i + 1];
                mc.resultado1 = mc.digito * mc.posicao1;
                mc.posicao2 = indice[i];
                mc.resultado2 = mc.digito * mc.posicao2;

                list.Add(mc);
            }

            int regra1 = list.Where(w => w.id < 13).Sum(s => s.resultado1) % 11;
            int dvCalc1 = regra1 < 2 ? 0 : 11 - regra1;
            int regra2 = list.Where(w => w.id < 14).Sum(s => s.resultado2) % 11;
            int dvCalc2 = regra2 < 2 ? 0 : 11 - regra2;

            var dvOrig1 = list.Where(w => w.id == 13).Select(s => s.digito).FirstOrDefault();
            var dvOrig2 = list.Where(w => w.id == 14).Select(s => s.digito).FirstOrDefault();

            if (dvCalc1 == dvOrig1 && dvCalc2 == dvOrig2)
            {
                return true;
            }
            else
            {
                if (dvCalc1 != dvOrig1)
                {
                    Console.WriteLine($"Digito Invalido {dvOrig1}");
                }
                if (dvCalc2 != dvOrig2)
                {
                    Console.WriteLine($"Digito Invalido {dvOrig2}");
                }
                return false;
            }
        }

    }
}
