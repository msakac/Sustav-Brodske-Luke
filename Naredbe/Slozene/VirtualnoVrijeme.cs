﻿using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class VirtualnoVrijeme : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            string novoVirtualnoVrijeme = argumenti[1] + " " + argumenti[2];
            DateTime novoVrijeme = DateTime.Parse(novoVirtualnoVrijeme);
            proxy.Postavi(novoVrijeme);
            IspisPoruke.Uspjeh($"Novo vrijeme virtualnog sata: {proxy.Dohvati()}");
        }
    }
}
