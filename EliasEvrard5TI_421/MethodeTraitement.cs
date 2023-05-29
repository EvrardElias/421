﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace EliasEvrard5TI_421
{
    public struct Joueur
    {
        public int jetonJoueur;
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
                score = score + mesJoueurs[lanceur].score[compteur] + " ";
            }

        }

        public void Identification(int nbrJoueur, out Joueur[] mesJoueurs)
        {
            string bonPseudo;
            bool ouiNon;
            mesJoueurs = new Joueur[nbrJoueur];
            for (int compteur = 1; compteur < nbrJoueur + 1; compteur++)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("Comment t'appelles tu Joueur n°" + compteur + "?");
                        mesJoueurs[compteur - 1].pseudo = Console.ReadLine();
                        Console.WriteLine("Es-tu sur que tu t'appelles " + mesJoueurs[compteur - 1].pseudo + "?");
                        bonPseudo = Console.ReadLine();
                        if (bonPseudo != "Oui" && bonPseudo != "Non")
                        {
                            Console.WriteLine("Veuillez repondre uniquement par Oui ou par Non .");
                            ouiNon = false;
                        }
                        else
                        {
                            ouiNon = true;
                        }
                    } while (ouiNon == false);
                } while (bonPseudo == "Non");
            }
        }

        public void Lancer(int lanceur, ref Joueur[] mesJoueurs)
        {
            Random des = new Random();
            for (int compteur = 0; compteur < 3; compteur++)
            {
                mesJoueurs[lanceur].score[compteur] = des.Next(1, 7);
            }
        }

        public void TriDes(int lanceur, ref Joueur[] mesJoueurs)
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

        public void LaCharge(int nbrJoueur, ref Joueur[] mesJoueurs)
        {
            int jeton = 21;
            do
            {
                for (int compteurJoueur = 0; compteurJoueur < nbrJoueur; compteurJoueur++)
                {
                    int lanceur = compteurJoueur;
                    Console.WriteLine("À toi de jouer" + mesJoueurs[compteurJoueur].pseudo);
                    Lancer(lanceur, ref mesJoueurs);
                    TriDes(lanceur, ref mesJoueurs);
                    ConcatenationDe(lanceur, mesJoueurs, out string score);
                    Console.WriteLine("Voici votre score :" + score);
                }
                mesJoueurs = mesJoueurs.OrderByDescending(joueur => joueur.score.Max()).ToArray();
                DonnerJetonCharge(nbrJoueur, ref jeton, ref mesJoueurs);
            } while (jeton != 0);
        }

        public void LaDecharge(int nbrJoueur, ref Joueur[] mesJoueurs)
        {
            string relancerDe;
            bool ouiNon = true;
            string dernierLancer;
            int nombreLancer = 3;
            do
            {
                if (mesJoueurs[nbrJoueur - 1].jetonJoueur != 0)
                {
                    for (int compteurJoueur = 0; compteurJoueur < mesJoueurs.Length - 1; compteurJoueur++)
                    {
                        int lanceur = compteurJoueur;
                        Console.WriteLine("C'est au tour de" + mesJoueurs[compteurJoueur].pseudo + ".");
                        Lancer(lanceur, ref mesJoueurs);
                        TriDes(lanceur, ref mesJoueurs);
                        ConcatenationDe(lanceur, mesJoueurs, out string score);
                        Console.WriteLine("Voici votre score :" + score);
                        if (nombreLancer > 1)
                        {
                            do
                            {
                                Console.WriteLine("Voulez-vous relancer les dés ?");
                                relancerDe = Console.ReadLine();
                                if (relancerDe == "Oui")
                                {
                                    Relancer(lanceur, ref mesJoueurs);
                                    TriDes(lanceur, ref mesJoueurs);
                                    ConcatenationDe(lanceur, mesJoueurs, out score);
                                    Console.WriteLine("Voici votre score :" + score);
                                    if (nombreLancer > 2)
                                    {
                                        do
                                        {
                                            Console.WriteLine("Voulez-vous relancer les dés une dernière fois ?");
                                            dernierLancer = Console.ReadLine();
                                            if (dernierLancer == "Oui")
                                            {
                                                Relancer(lanceur, ref mesJoueurs);
                                                TriDes(lanceur, ref mesJoueurs);
                                                ConcatenationDe(lanceur, mesJoueurs, out score);
                                                Console.WriteLine("Voici votre score :" + score);
                                                ouiNon = true;
                                            }
                                            else if (dernierLancer == "Non")
                                            {
                                                nombreLancer = 2;
                                                ouiNon = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Veuillez répondre uniquement par Oui ou par Non.");
                                                ouiNon = false;
                                            }
                                        } while (true);
                                    }

                                }
                                else if (relancerDe != "Non")
                                {
                                    nombreLancer = 1;
                                    ouiNon = true;
                                }
                                else
                                {
                                    Console.WriteLine("Veuillez répondre uniquement par Oui ou par Non.");
                                    ouiNon = false;
                                }
                            } while (ouiNon == true);
                        }

                    }
                }
                mesJoueurs = mesJoueurs.OrderByDescending(joueur => joueur.score.Max()).ToArray();
                DonnerJetonDeharge(nbrJoueur, ref mesJoueurs);
            } while (mesJoueurs[nbrJoueur - 1].jetonJoueur == 21);
        }

        /*public void TriScore(int nbrJoueur, ref Joueur[] mesJoueurs)
        {
            
            int passage = 0;
            bool permut = false;
            int verre;
            new int[][] { new int[] { 3, 4, 5 }, new int[] { 1, 1, 2 }, new int[] { 6, 6, 6 } }.OrderByDescending(row => row.Max()).ToArray();

            do
            {
                permut = false;
                for (int compteur = 0; compteur <= mesJoueurs[nbrJoueur].score.Length - passage; compteur++)
                {
                    if (mesJoueurs[compteur].score[1] > mesJoueurs[compteur + 1].score[])
                    {
                        verre = mesJoueurs[compteur].score[];
                        mesJoueurs[compteur].score[] = mesJoueurs[nbrJoueur + 1].score[];
                        mesJoueurs[compteur + 1].score[] = verre;
                        permut = true;
                    }
                }
                passage = passage + 1;
            } while (permut == true);
        }*/

        public void Relancer(int lanceur, ref Joueur[] mesJoueurs)
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
                            Console.WriteLine("Voulez-vous garder de coté le dé contenant la valeur" + mesJoueurs[lanceur].score[compteurChanger] + "?");
                            relancer = Console.ReadLine();
                            if (relancer == "Oui")
                            {
                                mesJoueurs[lanceur].score[compteurChanger] = desRelancer.Next(1, 7);
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
                        mesJoueurs[lanceur].score[compteur] = desRelancer.Next(1, 7);
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

        public void DonnerJetonDeharge(int nbrJoueur, ref Joueur[] mesJoueurs)
        {
            int jetonDonner = 0;
            if (mesJoueurs[nbrJoueur - 1].score[1] == 421)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir tout les jetons de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + mesJoueurs[nbrJoueur - 1].jetonJoueur;
                mesJoueurs[nbrJoueur - 1].jetonJoueur = 0;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 111)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 7 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 7 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 116 || mesJoueurs[nbrJoueur - 1].score[1] == 666)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 6 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 6 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 115 || mesJoueurs[nbrJoueur - 1].score[1] == 555)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 5 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 5 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 114 || mesJoueurs[nbrJoueur - 1].score[1] == 444)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 4 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 4 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 113 || mesJoueurs[nbrJoueur - 1].score[1] == 333)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 3 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 3 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 112 || mesJoueurs[nbrJoueur - 1].score[1] == 222 || mesJoueurs[nbrJoueur - 1].score[1] == 654 || mesJoueurs[nbrJoueur - 1].score[1] == 543 || mesJoueurs[nbrJoueur - 1].score[1] == 432 || mesJoueurs[nbrJoueur - 1].score[1] == 321)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 2 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                do
                {
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
                    jetonDonner = jetonDonner + 1;
                } while (jetonDonner == 2 || mesJoueurs[nbrJoueur - 1].jetonJoueur == 0);
            }
            else
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 1 jetons de la part de " + mesJoueurs[nbrJoueur - 1].pseudo + ".");
                    mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                    mesJoueurs[nbrJoueur - 1].jetonJoueur = mesJoueurs[nbrJoueur - 1].jetonJoueur - 1;
            }
        }


        public void DonnerJetonCharge(int nbrJoueur, ref int jeton, ref Joueur[] mesJoueurs)
        {
            if (mesJoueurs[nbrJoueur - 1].score[1] == 421)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 10 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 10;
                jeton = jeton - 10;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 111)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 7 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 7;
                jeton = jeton - 7;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 116 || mesJoueurs[nbrJoueur - 1].score[1] == 666)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 6 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 6;
                jeton = jeton - 6;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 115 || mesJoueurs[nbrJoueur - 1].score[1] == 555)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 5 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 5;
                jeton = jeton - 5;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 114 || mesJoueurs[nbrJoueur - 1].score[1] == 444)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 4 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 4;
                jeton = jeton - 4;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 113 || mesJoueurs[nbrJoueur - 1].score[1] == 333)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 3 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 3;
                jeton = jeton - 3;
            }
            else if (mesJoueurs[nbrJoueur - 1].score[1] == 112 || mesJoueurs[nbrJoueur - 1].score[1] == 222 || mesJoueurs[nbrJoueur - 1].score[1] == 654 || mesJoueurs[nbrJoueur - 1].score[1] == 543 || mesJoueurs[nbrJoueur - 1].score[1] == 432 || mesJoueurs[nbrJoueur - 1].score[1] == 321)
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 2 jetons");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 2;
                jeton = jeton - 2;
            }
            else
            {
                Console.WriteLine(mesJoueurs[0].pseudo + "va recevoir 1 jeton");
                mesJoueurs[0].jetonJoueur = mesJoueurs[0].jetonJoueur + 1;
                jeton = jeton - 1;
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
