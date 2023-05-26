using System;
using System.Collections.Generic;
using System.Text;

namespace EliasEvrard5TI_421
{
    public struct Joueur
    {
        public int  ;
        public string pseudo;
        public int[] score;
    }

    public struct MethodeTraitement
    {

        public void ConcatenationDe(int lanceur, Joueur[] mesJoueurs, out string score)
        {
            score = "";
            for (int compteur = 0; compteur < mesJoueurs[lanceur].score.Length; compteur++)
            {
                score = score + mesJoueurs[compteur] + " ";
            }

        }

        public void Identification(int nbrJoueur, out Joueur[] mesJoueurs)
        {
            string bonPseudo;
            mesJoueurs = new Joueur[nbrJoueur];
            for (int compteur = 0; compteur < nbrJoueur; compteur++)
            {
                do
                {
                    Console.WriteLine("Comment t'appelles tu Joueur n°" + compteur + 1 + "?");
                    mesJoueurs[compteur].pseudo = Console.ReadLine();
                    Console.WriteLine("Es-tu sur que tu t'appelles" + mesJoueurs[compteur].pseudo + "?");
                    bonPseudo = Console.ReadLine();
                } while (bonPseudo == "Non");
            }
        }

        public void Lancer(ref Joueur[] mesJoueurs)
        {
            Random des = new Random();
            for (int compteur = 0; compteur < 3; compteur++)
            {
                tabLancer[compteur] = des.Next(1, 7);
            }
        }

        public void Tri(int lanceur, ref Joueur[] mesJoueurs)
        {
            int passage = 0;
            bool permut = false;
            int verre;

            do
            {
                permut = false;
                for (int compteur = 0; compteur <= mesJoueurs[lanceur].score.Length - passage; compteur++)
                {
                    if (mesJoueurs[lanceur].score[compteur] > mesJoueurs[lanceur].score[compteur + 1])
                    {
                        verre = mesJoueurs[lanceur].score[compteur];
                        mesJoueurs[lanceur].score[compteur] = mesJoueurs[lanceur].score[compteur + 1];
                        mesJoueurs[lanceur].score[compteur + 1] = verre;
                        permut = true;
                    }
                }
                passage = passage + 1;
            } while (permut == true);
        }

        public void LaCharge(int nbrJoueur, int jeton, ref Joueur[] mesJoueurs)
        {
            jeton = 21;
            do
            {
                for (int compteurJoueur = 0; compteurJoueur < nbrJoueur; compteurJoueur++)
                {
                    int lanceur = compteurJoueur;
                    Console.WriteLine("À toi de jouer" + mesJoueurs[compteurJoueur].pseudo);
                    Lancer(ref mesJoueurs);
                    Tri(lanceur, ref mesJoueurs);
                    ConcatenationDe(lanceur, mesJoueurs, out string score);
                    Console.WriteLine("Voici votre score :" + score);
                }
                //TriScore
                //DonneJetonCharge
            } while (jeton != 0);
        }

        public void Relancer(ref int[] tabLancer)
        {
            bool ouiNon;
            string changer;
            Random desRelancer = new Random();
            do
            {
                Console.WriteLine("Voulez-vous garder des dés de cotés ?");
                changer = Console.ReadLine();
                string relancer;

                if (changer == "Oui")
                {
                    for (int compteurChanger = 0; compteurChanger <= 3; compteurChanger++)
                    {
                        do
                        {
                            Console.WriteLine("Voulez-vous garder de coté le dé contenant la valeur" + tabLancer[compteurChanger] + "?");
                            relancer = Console.ReadLine();
                            if (relancer == "Oui")
                            {
                                tabLancer[compteurChanger] = desRelancer.Next(1, 7);
                                ouiNon = true;
                            }
                            else if (relancer == "Non")
                            {
                                ouiNon = true;
                            }
                            else
                            {
                                Console.WriteLine("Veuillez répondre uniquement par Oui ou Non");
                                ouiNon = false;
                            }
                        } while (ouiNon == true);
                    }
                    ouiNon = true;
                }
                else if (changer == "Non")
                {
                    for (int compteur = 0; compteur <= 3; compteur++)
                    {
                        tabLancer[compteur] = desRelancer.Next(1, 7);
                    }
                    ouiNon = true;
                }
                else
                {
                    Console.WriteLine("Veuillez répondre uniquement par Oui ou Non");
                    ouiNon = false;
                }
            } while (ouiNon == true);
        }

        public void DonnerJetonCharge(string[] pseudo, int[] tabScore, int receveur, int nbrJoueur, int resultatJoueur,  )
        {
            if (tabScore[nbrJoueur - 1] == 421)
            {
                Console.WriteLine(pseudo[resultatJoueur - 1]] + "va recevoir 10 jetons");
            }
        }
        
        public void LireEntier(string question, out int n)
        {
            string nUser;
            Console.Write(question);
            nUser = Console.ReadLine();
            while (!int.TryParse(nUser, out n))
            {
                Console.WriteLine("Attention! Vous ne devez taper qu'un nombre entier !");
                nUser = Console.ReadLine();
            }
        }
    }
}
