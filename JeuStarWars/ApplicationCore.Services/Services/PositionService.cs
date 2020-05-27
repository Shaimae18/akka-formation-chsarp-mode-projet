using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Repository;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MoreLinq;

namespace ApplicationCore.services
{
    public class PositionService : Repository<Position, DataContext>, IPositionService
    {
        private DataContext _context;
        private DbSet<Parametrage> _dbSet { get; set; }
        private readonly List<Position> _defaultListPos = new List<Position>()
            {
                new Position(1, 6, new Joueur() { PointsVie = 50, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(3, 4, new Joueur() { PointsVie = 30, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(10, 2, new Joueur() { PointsVie = 100, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(6, 9, new Joueur() { PointsVie = 10, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(7, 2, new Joueur() { PointsVie = 40, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(9, 3, new Joueur() { PointsVie = 80, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(6, 1, new Joueur() { PointsVie = 20, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(5, 4, new Joueur() { PointsVie = 20, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(1, 1, new Joueur() { PointsVie = 20, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
                new Position(1, 9, new Joueur() { PointsVie = 20, TypeJoueur = TypeJoueur.Adversaire, Etat = Etat.Vivant}),
            };


        public PositionService(DataContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Parametrage>();
        }

        public override IEnumerable<Position> FindAll()
        {
            return _context.Positions
                .Include(j => j.Joueur)
                .ThenInclude(p => p.Personnage);

        }

        public IEnumerable<Position> GetInitialPosition(List<PNJ> Pnjs)
        {

          
            for (int i = 0; i < _defaultListPos.Count; i++)
            {
                Insert(_defaultListPos[i]);
                _defaultListPos[i].Joueur.Personnage = Pnjs[i];
                Update(_defaultListPos[i]);

            }

           // _context.UpdateRange(_defaultListPos);
           //_context.SaveChanges();
            return _defaultListPos;
        }
    }
}
