using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Entities
{
    public class PersonnageJoueur : Personnage
    {

        public int? PointsVie { get; set; }
        public int? PointsMagie { get; set; }
        public int? Portee { get; set; }
        public int? Degat { get; set; }
        public TypePersonnage TypePersonnage { get; set; }
        // private Personnage[] _personnages { get; set;};

        //public Personnage(Personnage[] pArray)
        //{
        //    _personnages = new Personnage[pArray.Length];
        //    for (int i=0;i<pArray.Length;i++)
        //    {
        //        _personnages[i] = pArray[i];
        //    }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return (IEnumerator)GetEnumerator();
        //}

        //public PersonnageEnum GetEnumerator()
        //{
        //    return new PersonnageEnum(_personnages);
        //}

        public PersonnageJoueur(string pseudo, Cote cote = Cote.Lumineux, int? pointsVie = 150, int? pointsMagie = 0, int? portee = 0, int? degat = 0, TypePersonnage typePersonnage= TypePersonnage.NonLanceurDeSort)
        {
            this.Pseudo = pseudo;
            this.Cote = cote;

            this.PointsVie = pointsVie;
            this.PointsMagie = pointsMagie;
            this.Portee = portee;
            this.Degat = degat;
            this.TypePersonnage = typePersonnage;

        }

        public PersonnageJoueur(PersonnageJoueur personnage)
        {
            this.Pseudo = personnage.Pseudo;
            this.Cote = personnage.Cote;
            this.PointsVie = personnage.PointsVie;
            this.PointsMagie = personnage.PointsMagie;
            this.Portee = personnage.Portee;
            this.Degat = personnage.Degat;
            this.TypePersonnage = personnage.TypePersonnage;
        }

        public PersonnageJoueur()
        {
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗");
            //stringBuilder.Append("║      Pseudo       ║║        Côté       ║║    Points de vie  ║║   Points de Magie ║║       Portée      ║║        Degat      ║");
            //stringBuilder.Append("╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝");
            stringBuilder.Append($" Joueur: {Pseudo}  PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");
            return stringBuilder.ToString();
        }

        //public class PersonnageEnum : IEnumerator
        //{
        //    public Personnage[] _personnages;
        //    int position = -1;

        //    public PersonnageEnum(Personnage[] list)
        //    {
        //        _personnages = list;
        //    }

        //    object IEnumerator.Current
        //    {
        //        get
        //        {
        //            return Current;
        //        }
        //    }

        //    public Personnage Current
        //    {
        //        get
        //        {
        //            try
        //            {
        //                return _personnages[position];
        //            }
        //            catch (IndexOutOfRangeException)
        //            {
        //                throw new InvalidOperationException();
        //            }
        //        }
        //    }

        //    public bool MoveNext()
        //    {
        //        position++;
        //        return (position < _personnages.Length);
        //    }

        //    public void Reset()
        //    {
        //        position = -1;
        //    }
        //}



    }
    public enum TypePersonnage
    {
        LanceurDeSort,
        NonLanceurDeSort
    }
}
