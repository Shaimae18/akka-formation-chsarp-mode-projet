using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Entities
{
    public class Personnage : IEnumerable
    {
        public string Pseudo { get; set; }
        public Cote Cote { get; set; }
        public TypePersonnage TypePersonnage { get; set; }
        public int? PointsVie { get; set; }
        public int? PointsMagie { get; set; }
        public int? Portee { get; set; }
        public int? Degat { get; set; }

        private Personnage[] _personnages;

        public Personnage(Personnage[] pArray)
        {
            _personnages = new Personnage[pArray.Length];
            for (int i=0;i<pArray.Length;i++)
            {
                _personnages[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PersonnageEnum GetEnumerator()
        {
            return new PersonnageEnum(_personnages);
        }

        public Personnage(string pseudo, Cote cote = Cote.Lumineux,TypePersonnage typePersonnage = TypePersonnage.Ennemie, int? pointsVie=150, int? pointsMagie=0, int? portee=0, int? degat=0)
        {
            this.Pseudo = pseudo;
            this.Cote = cote;
            this.TypePersonnage = typePersonnage;
            this.PointsVie = pointsVie;
            this.PointsMagie = pointsMagie;
            this.Portee = portee;
            this.Degat = degat;
            
        }

        public Personnage(Personnage personnage)
        {
            this.Pseudo = personnage.Pseudo;
            this.Cote = personnage.Cote;
            this.TypePersonnage = personnage.TypePersonnage;
            this.PointsVie = personnage.PointsVie;
            this.PointsMagie = personnage.PointsMagie;
            this.Portee = personnage.Portee;
            this.Degat = personnage.Degat;
        }

        public Personnage()
        {
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗");
            //stringBuilder.Append("║      Pseudo       ║║        Côté       ║║    Points de vie  ║║   Points de Magie ║║       Portée      ║║        Degat      ║");
            //stringBuilder.Append("╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝╚═══════════════════╝");

            stringBuilder.Append($" Joueur: {Pseudo}  PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");

            return stringBuilder.ToString() ;
        }
    }

    public class PersonnageEnum : IEnumerator
    {
        public Personnage[] _personnages;
        int position = -1;

        public PersonnageEnum(Personnage[] list)
        {
            _personnages = list;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Personnage Current
        {
            get
            {
                try
                {
                    return _personnages[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < _personnages.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }

    public enum Cote
    {
        Lumineux,
        Obscur
    }
    public enum TypePersonnage
    {
        Hero,
        SpecialHero,
        Ennemie
    }
}
