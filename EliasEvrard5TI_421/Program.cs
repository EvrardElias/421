using System;

namespace EliasEvrard5TI_421
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodeTraitement outils = new MethodeTraitement();
            int nbrJoueur;
            string recommencer;
            bool ouiNon;
            string avecMemeJoueurs = "Oui";
            int[] tabLancer = new int[3];

            do
            {
                Console.WriteLine("Bienvenue au jeu 421!");

                outils.LireEntier("Combien de joueur sont présent ?", out nbrJoueur);
                Joueur[] mesJoueurs = new Joueur[nbrJoueur];
                outils.Identification(nbrJoueur, out mesJoueurs);
                do
                {
                    Console.WriteLine("Maintenant que tout le monde est prêt le jeu peut commencer !");
                    outils.LaCharge(nbrJoueur, ref mesJoueurs);
                    outils.LaDecharge(nbrJoueur, ref mesJoueurs);
                    Console.WriteLine("Le perdant de cette partie est " + mesJoueurs[0].pseudo + ".");
                    do
                    {
                        Console.WriteLine("Voulez vous recommencer ?");
                        recommencer = Console.ReadLine();
                        if (recommencer == "Oui")
                        {
                            do
                            {
                                Console.WriteLine("Avec les mêmes joueurs ?");
                                avecMemeJoueurs = Console.ReadLine();
                                if (avecMemeJoueurs != "Oui" || avecMemeJoueurs != "Non")
                                {
                                    Console.WriteLine("Veuillez repondre uniquement par Oui ou par Non");
                                    ouiNon = false;
                                }
                                else
                                {
                                    ouiNon = true;
                                }
                            } while (ouiNon == true);
                            ouiNon = true;
                        }
                        else if (recommencer == "Non")
                        {
                            Console.WriteLine("Au revoir");
                            ouiNon = true;
                        }
                        else
                        {
                            Console.WriteLine("Veuillez repondre uniquement par Oui ou par Non");
                            ouiNon = false;
                        }
                    } while (ouiNon == true);

                } while (avecMemeJoueurs == "Non");
            } while (recommencer == "Non");
        }
    }
}
