using System;

namespace EliasEvrard5TI_421
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodeTraitement outils = new MethodeTraitement();
            int nbrJoueur;
            int[] tabLancer = new int[3];

            Console.WriteLine("Bienvenue au jeu 421!");

            outils.LireEntier("Combien de joueur sont présent ?", out nbrJoueur);
            //outils.LancementPartie();
        }
    }
}
