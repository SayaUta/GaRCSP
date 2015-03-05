using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaRCSP
{
    struct PrecCons
    {
        public int act1;
        public int act2;
    };

    struct gene
    {
        static int WORKCOUNT;
        public int[] alleles;
        public float likelihood;
        public int genes(int tmpworkc)
        {
            /*for (int i = 0; i<100; i++) {
                alleles[i] = -1;
            }*/
            WORKCOUNT = tmpworkc;
            alleles = new int[tmpworkc];
            likelihood = 0;
            return 666;
        }
        //public: gene(int tmpworkc) : WORKCOUNT(tmpworkc){}
        //int* alleles = new int[100];
        //int fitness;
        //int alleles[100];

        // Test for equality.
        public bool equal(gene gn)
        {
            for (int i = 0; i < WORKCOUNT; i++)
            {
                if (alleles[i] != gn.alleles[i]) return false;
            }
            return true;
        }
    };

    class CDiophantine
    {
        int workcount;
        int[] pDurat;
        int[] pWorker;
        /*int pDurat;
         * int pWorker;*/
        int workpreccount;
        PrecCons[] pPrecCons;
        int maxdur;
        int reshav;
        int newMaxDur = 2147483647;
        gene[] population;//[MAXPOP];// Population.
        public const int MAXPOP = 25;

        public CDiophantine(int tworkcount, int[] tpDurat, int[] tpWorker, int tworkpreccount, PrecCons[] tpPrecCons, int tmaxdur, int treshav)
        {// Constructor 
            workcount = tworkcount;
            pDurat = tpDurat;
            pWorker = tpWorker;
            workpreccount = tworkpreccount;
            pPrecCons = tpPrecCons;
            maxdur = tmaxdur;
            reshav = treshav;
            population = new gene[MAXPOP];
            for (int i = 0; i < MAXPOP; i++)
            {
                population[i].genes(tworkcount);
            }
        }
        public int Solve()
        {// Solve the equation.
            //int fitn = INT_MAX;

            // Generate initial population.
            Random rand = new Random();

            for (int i = 0; i < MAXPOP; i++)
            {// Fill the population with numbers between 
                do
                {
                    //int tmp = rand() % (MAXDUR + 1);
                    for (int j = 0; j < workcount; j++)
                    {// 0 and the result.
                        population[i].alleles[j] = /*rand() % (tmp + 1);//*/rand.Next(maxdur + 1);
                    }
                } while (ConstrCheck(population[i]) == 0);
                //cout << "i = " << i << endl;
            }

            /*if (fitness = CreateFitnesses()) {
            return fitness;
            }*/

            int iterations = 0;// Keep record of the iterations.
            while (/*fitness != 0 || */iterations < 50)
            {// Repeat until solution found, or over 50 iterations.
                GenerateLikelihoods();// Create the likelihoods.
                CreateNewPopulation();
                /*if (fitness = CreateFitnesses()) {
                return fitness;
                }*/

                iterations++;
                //cout << "iterations = " << iterations << endl;
                Console.WriteLine("iterations = " + iterations + " duration = " + Fitness(population[0]));
            }

            int ind = CreateFitnesses();
            //return -1;
            return ind;
        }

        // Returns a given gene.
        public gene GetGene(int i)
        {
            return population[i];
        }

        int ConstrCheck(gene gn)
        { //проверка ограничений
            //int durat[WORKCOUNT] = { 1, 1, 2, 1, 1, 1, 0 };
            //int worker[WORKCOUNT] = { 3, 1, 1, 2, 2, 2, 0 };
            /*if (gn.alleles[0] + pDurat[0] > gn.alleles[6])
                return 0;
            if (gn.alleles[2] + pDurat[2] > gn.alleles[6])
                return 0;
            if (gn.alleles[5] + pDurat[5] > gn.alleles[6])
                return 0;
            if (gn.alleles[1] + pDurat[1] > gn.alleles[2])
                return 0;
            if (gn.alleles[3] + pDurat[3] > gn.alleles[4])
                return 0;
            if (gn.alleles[4] + pDurat[4] > gn.alleles[5])
                return 0;*/
            for (int i = 0; i < workpreccount; i++)
            {
                if (gn.alleles[pPrecCons[i].act1 - 1] + pDurat[pPrecCons[i].act1 - 1] > gn.alleles[pPrecCons[i].act2 - 1])
                    return 0;
            }
            for (int i = 0; i < maxdur; i++)
            {
                int res = 0;
                for (int j = 0; j < workcount; j++)
                {
                    if ((gn.alleles[j] <= i) && (gn.alleles[j] + pDurat[j] > i))
                    {
                        res = res + pWorker[j];
                    }
                }
                if (res > reshav)
                    return 0;
            }
            return 1;
        }
        int Fitness(gene gn)
        {// Fitness function.
            return /*gn.fitness = */gn.alleles[workcount-1];
        }
        void GenerateLikelihoods()
        {	// Generate likelihoods.
            float multinv = MultInv();
            float last = 0;
            for (int i = 0; i < MAXPOP; i++)
            {
                population[i].likelihood = last = last + ((1 / ((float)Fitness(population[i])) / multinv) * 100);
            }
        }
        float MultInv()
        {// Creates the multiplicative inverse.
            float sum = 0;
            for (int i = 0; i < MAXPOP; i++)
            {
                sum += 1 / ((float)Fitness(population[i]));
            }
            return sum;
        }
        int CreateFitnesses()
        {
            int minfitness = 2147483647;
            int fitness = 2147483647;
            int minin = -1;
            for (int i = 0; i < MAXPOP; i++)
            {
                fitness = Fitness(population[i]);
                if (fitness < minfitness)
                {
                    minfitness = fitness;
                    minin = i;
                }
            }
            return minin;
        }
        void CreateNewPopulation()
        {
            //cout << "7th point" << endl;
            gene[] temppop = new gene[MAXPOP];
            /*gene* temppop = (gene*)operator new(sizeof(gene)* MAXPOP);
            for (int i = 0; i < MAXPOP; ++i)
                new(&temppop[i]) gene(workcount);*/
            //vector<gene> temppop(MAXPOP);
            //map <int,int> mapfit;
            PrecCons[] mapfit = new PrecCons[MAXPOP];
            for (int i = 0; i < MAXPOP; i++)
            {
                mapfit[i].act1 = i;
                mapfit[i].act2 = Fitness(population[i]);
            }
            Array.Sort(mapfit, new Comparison<PrecCons>((a, b) => a.act2.CompareTo(b.act2)));


            //cout << "8th point" << endl;
            for (int i = 0; i < 12; i++)
            {
                //map <int, int>::iterator cur;
                //cout << "start" << endl;
                //for (cur = mapfit.begin(); cur != mapfit.end(); cur++){
                //cout << "map " << Fitness(population[(*cur).second]) << endl;
                temppop[i] = population[mapfit[i].act1];
            }
            /*int[] tmpnwp = new int[MAXPOP];
            for (int i = 0; i < MAXPOP; i++)
            {
                tmpnwp[i] = 0;
            }

            for (int i = 0; i < 12; i++)
            {
                int minfitness = 2147483647;
                int fitness = 2147483647;
                int minin = -1;
                for (int j = 0; j < MAXPOP; j++)
                {
                    fitness = Fitness(population[j]);
                    if ((fitness < minfitness) && (tmpnwp[j] == 0))
                    {
                        minfitness = fitness;
                        minin = j;
                    }
                }
                tmpnwp[minin] = 1;
                temppop[i] = population[minin];
            }*/
            //int tu = CreateFitnesses();
            newMaxDur = Fitness(temppop[0]);/*population[tu].alleles[workcount-1];*/
            Random rand = new Random();
            //cout << "9th point" << endl;
            for (int i = 12; i < 22; i++)
            {
                int parent1 = 0, parent2 = 0, iterations = 0;
                while (parent1 == parent2 || population[parent1].equal(population[parent2]))
                {
                    //cout << "20th point" << endl;
                    parent1 = GetIndex((float)(rand.Next(101)));
                    parent2 = GetIndex((float)(rand.Next(101)));
                    if (++iterations > 25) break;
                }

                temppop[i] = Breed(parent1, parent2);// Create a child.
                //cout << "i = " << i << endl;
            }

            for (int i = 22; i < MAXPOP; i++)
            {
                //Console.WriteLine("i = " + i);
                temppop[i].genes(workcount);
                int oops = 0;
                do
                {                    
                    //Console.WriteLine("booo");
                    //cout << "booo" << endl;
                    if (oops < 100)
                    {                        
                        for (int j = 0; j < workcount; j++)
                        {
                            temppop[i].alleles[j] = rand.Next(newMaxDur + 1);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < workcount; j++)
                        {
                            temppop[i].alleles[j] = rand.Next(newMaxDur + 2);
                        }
                    }
                    oops = oops + 1;
                } while (ConstrCheck(temppop[i]) == 0);
            }

            for (int i = 0; i < MAXPOP; i++) population[i] = temppop[i];
        }
        int GetIndex(float val)
        {
            float last = 0;
            for (int i = 0; i < MAXPOP; i++)
            {
                if (last <= val && val <= population[i].likelihood) return i;
                else last = population[i].likelihood;
            }

            return 4;
        }

        gene Breed(int p1, int p2)
        {
            //cout << "3rd point" << endl;
            Random rand = new Random();
            int crossover = rand.Next(workcount);// Create the crossover point (not first).

            //int first = rand() % 100;// Which parent comes first?

            gene child = new gene();
            child.genes(workcount);
            //child = population[p1];// Child is all first parent initially.
            Array.Copy(population[p1].alleles, child.alleles,workcount);
            child.likelihood = population[p1].likelihood;
            //child.alleles[0] = 999;
            int[] tmpbr = new int[workcount];// = { 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < workcount; i++)
            {
                tmpbr[i] = 0;
            }

            int flag = 1;

            for (int i = 0; i < workcount; i++)
            {
                if (child.alleles[i] <= child.alleles[crossover])
                    tmpbr[i] = 1;
                else
                    flag = 0;
            }
            //cout << "6th point" << endl;
            while (flag == 0)
            {
                int minal = 2147483647;
                int minin = -1;
                for (int i = 0; i < workcount; i++)
                {
                    if ((tmpbr[i] == 0) && (population[p2].alleles[i] < minal))
                    {
                        minal = population[p2].alleles[i];
                        minin = i;
                    }
                }
                child.alleles[minin] = population[p2].alleles[minin];//nen
                if (ConstrCheck(child) == 0)
                {
                    int oops = 0;
                    do
                    {
                        if (oops < 50)
                        {
                            child.alleles[minin] = rand.Next(/*maxdur*/population[p1].alleles[workcount-1] + 1);
                            oops = oops + 1;
                            //cout << "10th point" << endl;
                        }
                        else
                        {
                            do
                            {
                                for (int j = 0; j < workcount; j++)
                                {
                                child.alleles[j] = rand.Next(maxdur + 1);
                                }
                            } while (ConstrCheck(child) == 0);
                        }
                    } while (ConstrCheck(child) == 0);
                    //cout << "1st point" << endl;
                }
                tmpbr[minin] = 1;
                flag = 1;
                for (int i = 0; i < workcount; i++)
                {
                    if (tmpbr[i] == 0)
                        flag = 0;
                }
                //cout << "flag = " << flag << endl;
            }
            //cout << "5th point" << endl;
            int mutation = rand.Next(workcount);// Create the crossover point (not first).
            if (rand.Next(101) < 15)
            {
                child.alleles[mutation] = rand.Next(maxdur + 1);
                if (ConstrCheck(child) == 0)
                {
                    do
                    {
                        child.alleles[mutation] = rand.Next(maxdur + 1);
                        //cout << "2nd point" << endl;
                    } while (ConstrCheck(child) == 0);
                }
            }

            /*int initial = 0, final = 3;// The crossover boundaries.
            if (first < 50) initial = crossover;	// If first parent first. start from crossover.
            else final = crossover + 1;// Else end at crossover.

            for (int i = initial; i<final; i++) {// Crossover!
            child.alleles[i] = population[p2].alleles[i];
            if (rand() % 101 < 5) child.alleles[i] = rand() % (result + 1);
            }*/
            //cout << "4th point" << endl;
            return child;// Return the kid...
        }
    }
}
