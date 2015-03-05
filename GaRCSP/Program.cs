using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaRCSP
{
    class Program
    {
        static void Main(string[] args)
        {
            int workcount;
            /*cout << "Insert number of activities: ";
             * cin >> workcount;*/
            workcount = 8;
            int[] pDurat = new int[workcount];
            /*for (int i = 0; i < workcount; i++){
             * cout << "Insert " << i+1 << " activity duration: ";
             * cin >> pDurat[i];
             * }*/
            pDurat[0] = 2;
            pDurat[1] = 6;
            pDurat[2] = 4;
            pDurat[3] = 2;
            pDurat[4] = 2;
            pDurat[5] = 4;
            pDurat[6] = 2;
            pDurat[7] = 0;
            int[] pWorker = new int[workcount];
            /*for (int i = 0; i < workcount; i++){
             * cout << "Insert " << i + 1 << " activity resource usage: ";
             * cin >> pWorker[i];
             * }*/
            pWorker[0] = 1;
            pWorker[1] = 1;
            pWorker[2] = 1;
            pWorker[3] = 1;
            pWorker[4] = 1;
            pWorker[5] = 1;
            pWorker[6] = 1;
            pWorker[7] = 0;
            int workpreccount;
            workpreccount = 10;
            /*cout << "Insert number of precedence constraints: ";
             * cin >> workpreccount;*/
            PrecCons[] pPrecCons = new PrecCons[workpreccount];
            /*for (int i = 0; i < workpreccount; i++){
             * cout << "Insert " << i + 1 << " precedence constraints: ";
             * cin >> pPrecCons[i].act1 >> pPrecCons[i].act2;
             * }*/
            pPrecCons[0].act1 = 1;
            pPrecCons[1].act1 = 1;
            pPrecCons[2].act1 = 1;
            pPrecCons[3].act1 = 4;
            pPrecCons[4].act1 = 2;
            pPrecCons[5].act1 = 3;
            pPrecCons[6].act1 = 3;
            pPrecCons[7].act1 = 5;
            pPrecCons[8].act1 = 6;
            pPrecCons[9].act1 = 7;
            pPrecCons[0].act2 = 4;
            pPrecCons[1].act2 = 2;
            pPrecCons[2].act2 = 3;
            pPrecCons[3].act2 = 7;
            pPrecCons[4].act2 = 7;
            pPrecCons[5].act2 = 5;
            pPrecCons[6].act2 = 6;
            pPrecCons[7].act2 = 7;
            pPrecCons[8].act2 = 7;
            pPrecCons[9].act2 = 8;
            int maxdur;
            /*cout << "Insert expected min duration: ";
             * cin >> maxdur;*/
            maxdur = 50;
            int reshav;
            reshav = 3;
            CDiophantine dp = new CDiophantine(workcount, pDurat, pWorker, workpreccount, pPrecCons, maxdur, reshav);

            int ans;
            ans = dp.Solve();
            /*if (ans == -1) {
             * cout << "No solution found." << endl;
             * }
             * else {
             * gene gn = dp.GetGene(ans);
             * cout << "The solution set to a+2b+3c+4d=30 is:\n";
             * cout << "a = " << gn.alleles[0] << "." << endl;
             * cout << "b = " << gn.alleles[1] << "." << endl;
             * cout << "c = " << gn.alleles[2] << "." << endl;
             * cout << "d = " << gn.alleles[3] << "." << endl;
             * }*/
            gene gn = dp.GetGene(ans);
            Console.WriteLine("1 = " + gn.alleles[0] + "\r\n");
            Console.WriteLine("2 = " + gn.alleles[1] + "\r\n");
            Console.WriteLine("3 = " + gn.alleles[2] + "\r\n");
            Console.WriteLine("4 = " + gn.alleles[3] + "\r\n");
            Console.WriteLine("5 = " + gn.alleles[4] + "\r\n");
            Console.WriteLine("6 = " + gn.alleles[5] + "\r\n");
            Console.WriteLine("7 = " + gn.alleles[6] + "\r\n");
            Console.WriteLine("8 = " + gn.alleles[7] + "\r\n");
            /*cout << "a = " << gn.alleles[0] << "." << endl;
            cout << "b = " << gn.alleles[1] << "." << endl;
            cout << "c = " << gn.alleles[2] << "." << endl;
            cout << "d = " << gn.alleles[3] << "." << endl;
            cout << "e = " << gn.alleles[4] << "." << endl;
            cout << "f = " << gn.alleles[5] << "." << endl;
            cout << "g = " << gn.alleles[6] << "." << endl;
            system("pause");*/
            Console.ReadKey();
        }
    }
}
