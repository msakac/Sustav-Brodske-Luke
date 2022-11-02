﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.CsvCitac
{
    public class RasporedCsvCitac : ICsvCitac
    {
        public void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nRaspored | Učitavam datoteku: {datoteka}...");
            var trenutniDirektorij = System.AppContext.BaseDirectory;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(StavkaRasporeda).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    IspisPoruke.FatalnaGreska("Broj redaka u datoteci i atributa u klasi su razliciti!");
                }
                string redak;
                int ucitaniPodaci = 0;
                while ((redak = citac.ReadLine()!) != null)
                {
                    try
                    {
                        string[]? podaci = redak?.Split(';');
                        int idVez = int.Parse(podaci![0]);
                        int idBrod = int.Parse(podaci![1]);
                        string[]? daniTjedna = podaci[2].Split(',');
                        List<DayOfWeek> daniUTjednu = new List<DayOfWeek>();
                        foreach(string dan in daniTjedna){
                            DayOfWeek danTjedna = StavkaRasporeda.dohvatiDanTjedna(dan);
                            daniUTjednu.Add(danTjedna);
                        }
                        TimeOnly vrijemeOd = TimeOnly.Parse(podaci[3]);
                        TimeOnly vrijemeDo = TimeOnly.Parse(podaci[4]);

                        StavkaRasporeda stavkaRasporeda = new StavkaRasporeda(idVez, idBrod, daniUTjednu, vrijemeOd, vrijemeDo);
                        stavkaRasporeda.DodajURaspored();
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }

                }
                IspisPoruke.Uspjeh($"|===== Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                IspisPoruke.FatalnaGreska($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
