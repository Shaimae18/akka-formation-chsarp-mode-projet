using System;

namespace Entites
{
    public class Personnage
    {
        public Position position;
        public int pv;
        public int pm;
        public int portee;
        public int degats;
        public enum Cote {
            Lumineux,
            Obscur
        }

        public Personnage(Position position, int pv, int pm,int portee, int degats)
        {
            this.position = new Position(position.GetX(),position.GetY());
            SetPV(pv);
            SetPM(pm);
            SetPortee(portee);
            SetDegats(degats);
        }

        public int GetPV()
        {
            return pv;
        }

        public int GetPM()
        {
            return pm;
        }

        public int GetPortee()
        {
            return portee;
        }

        public int GetDegats()
        {
            return degats;
        }

        public Position GetPosition()
        {
            return position;
        }

        public void SetPV(int pv)
        {
            this.pv = pv;
        }

        public void SetPM(int pm)
        {
            this.pm = pm;
        }

        public void SetPortee(int portee)
        {
            this.portee = portee;
        }

        public void SetDegats(int degats)
        {
            this.degats = degats;
        }
    }
}
