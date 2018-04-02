using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongDivision {
    class Program {
        static void Main(string[] args) {
            while (true) {
                Calculation calc = new Calculation(Console.ReadLine());
                foreach(string line in calc.lines) {
                    Console.WriteLine(line);
                }
            }
        }
    }

    class Calculation {
        public List<string> lines = new List<string>();
        public int[] inputs = new int[2];
        private int currentDivisor;
        private int indent;
        public string solution;
        private int period;
        private int iterations;
        public Calculation(string input) {
            inputs = GetVals(input);

            lines.Add(" " + inputs[0] + ":" + inputs[1] + "=");

            int i = 0;
            do {
                if (i < inputs[0].ToString().Length)
                    currentDivisor = int.Parse(currentDivisor.ToString() + inputs[0].ToString()[i]);
                else {
                    currentDivisor = currentDivisor * 10;
                    solution += "0";
                    if (period == 0) {
                        solution += ".";
                        period = i;
                    }
                }
                indent = i+1;
                i++;
            } while (currentDivisor/inputs[1] < 1);

            //Console.WriteLine(currentDivisor);

            DoStep();
            lines[0] += solution.ToString();
        }

        private void DoStep() {
            iterations++;
            if((currentDivisor == 0 && indent > inputs[0].ToString().Length) || iterations == 10) {
                lines.Add(new string(' ', indent - 1) + currentDivisor.ToString().Substring(0,currentDivisor.ToString().Length-1));
                return;
            }
            lines.Add(new string(' ', indent - currentDivisor.ToString().Length + 1) + currentDivisor.ToString());
            int minus = currentDivisor - currentDivisor % inputs[1];
            int difference = currentDivisor % inputs[1];
            lines.Add(new string(' ', indent - minus.ToString().Length) + "-" + minus.ToString());
            lines.Add(new string(' ', indent - currentDivisor.ToString().Length + 1) + new string('-', currentDivisor.ToString().Length));
            solution += minus / inputs[1];
            indent++;

            currentDivisor = difference;
            if(indent > inputs[0].ToString().Length) {
                currentDivisor *= 10;
                if (period == 0) {
                    solution += ".";
                    period = indent-1;
                }
            } else {
                currentDivisor = int.Parse(currentDivisor.ToString() + inputs[0].ToString()[indent - 1]);
            }
            DoStep();
        }

        private int[] GetVals(string input) {
            string[] split = input.Split('/');
            int[] nums = Array.ConvertAll(split, int.Parse);
            return nums;
        }
    }
}
