﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.CsvCitac
{
    public class ZahtjevRezervacijeCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nZahtjevi Rezervacije | Učitavam datoteku: {datoteka}...");
            var trenutniDirektorij = System.AppContext.BaseDirectory;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(ZahtjevRezervacije).GetProperties().Length;
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
                        int idBroda = int.Parse(podaci![0]);
                        DateTime datumVrijemeOd = DateTime.Parse(podaci[1]);
                        int trajanjePrivezaUSatima = int.Parse(podaci[2]);

                        ZahtjevRezervacije zahtjevRezervacije = new ZahtjevRezervacije(idBroda, datumVrijemeOd, trajanjePrivezaUSatima);
                        zahtjevRezervacije.DodajUListuZahtjevaRezervacije();
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
