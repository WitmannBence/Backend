﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WCF_elso_server.Models;

namespace WCF_elso_server.Controllers
{
    public class ZeneszamController
    {
        public List<Zeneszam> ZeneszamLista()
        {
            string[] sorok = File.ReadAllLines(@"C:\Users\user\Desktop\Backend-main\WCF_elso\WCF_elso\zeneszam.txt");
            List<Zeneszam> list = new List<Zeneszam>();
            for (int i = 1; i < sorok.Length; i++)
            {
                string[] bontas = sorok[i].Split(';');
                list.Add(new Zeneszam()
                {
                    ZeneszamAz = int.Parse(bontas[0]),
                    ZeneszamCim = bontas[1],
                    ZeneszamHossz = int.Parse(bontas[2])
                });

            }
            return list;
        }

        public string InsertZeneszam(Zeneszam ujZeneszam)
        {
            ujZeneszam.ZeneszamAz = General();
            StreamWriter sw = new StreamWriter(@"C:\Users\user\Desktop\Backend-main\WCF_elso\WCF_elso\zeneszam.txt", true);
            sw.WriteLine(ujZeneszam.ToString());
            sw.Close();
            return "Sikeresen mentettük a zeneszámot.";
        }

        int General()
        {
            return ZeneszamLista().Select(zeneszam => zeneszam.ZeneszamAz).ToList().Max() + 1;
        }

        public string UpdateZeneszam(Zeneszam modositZeneszam)
        {
            List<Zeneszam> lista = ZeneszamLista();
            int i = 0;
            while (i < lista.Count && lista[i].ZeneszamAz != modositZeneszam.ZeneszamAz)
            {
                i++;
            }
            if (i < lista.Count)
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\user\Desktop\Backend-main\WCF_elso\WCF_elso\zeneszam.txt");
                sw.WriteLine("ZeneszamAz;ZeneszamCim;ZeneszamHossz");
                foreach (var item in lista)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Close();
                return "A módosítás sikeres";

            }
            else
            {
                return "Nincs ilyen azonosítójú zeneszám";
            }

        }

        public string DeleteZeneszam(Zeneszam torolZeneszam)
        {
            List<Zeneszam> lista = ZeneszamLista();
            int i = 0;
            while (i < lista.Count && lista[i].ZeneszamAz != torolZeneszam.ZeneszamAz)
            {
                i++;
            }
            if (i < lista.Count)
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\user\Desktop\Backend-main\WCF_elso\WCF_elsozeneszam.txt");
                sw.WriteLine("elozoAzon;ZeneszamName");
                foreach (var item in lista)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Close();
                return "A törlés sikeres";
            }
            else
            {
                return "Nincs ilyen azonosítójú zeneszám";
            }
        }
    }
}