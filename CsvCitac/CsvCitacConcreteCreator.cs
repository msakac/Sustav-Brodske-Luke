﻿namespace msakac_zadaca_3.CsvCitac
{
    public class CsvCitacConcreteCreator : CsvCitacCreator
    {
        public override AbstractCsvCitac KreirajCitac(string tip)
        {
            switch (tip)
            {
                case "luke":
                    return new LukeCsvCitac();
                case "raspored":
                    return new RasporedCsvCitac();
                case "vezovi":
                    return new VezoviCsvCitac();
                case "brodovi":
                    return new BrodoviCsvCitac();
                case "rezervacije":
                    return new ZahtjevRezervacijeCsvCitac();
                case "kanali":
                    return new KanaliCsvCitac();
                case "molovi":
                    return new MoloviCscCitac();
                case "molvez":
                    return new MolVezoviCsvCitac();
                default:
                    throw new ApplicationException($"Dohvat podataka {tip} nije moguc!");
            }
        }
    }
}
