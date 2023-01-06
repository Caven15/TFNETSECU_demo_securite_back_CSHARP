using Demo.DAL.Data;
using Demo.DAL.Interfaces;
using Demo.DAL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Tools.Connection;

namespace Demo.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly Connection _connection;

        public UtilisateurRepository(Connection connection)
        {
            _connection = connection;
        }

        public UtilisateurData LoginUtilisateur(string email, string password)
        {
            Command command = new Command("spUtilisateurLogin", true);
            command.AddParameter("Email", email);
            command.AddParameter("Password", password);
            return _connection.ExecuteReader(command, er => er.DbToUtilisateur()).SingleOrDefault();
        }

        public void RegisterUtilisateur(UtilisateurData data)
        {
            Command command = new Command("spUtilisateurRegister", true);
            command.AddParameter("Nom", data.Nom);
            command.AddParameter("Prenom", data.Prenom);
            command.AddParameter("Email", data.Email);
            command.AddParameter("DateNaissance", data.DateNaissance);
            command.AddParameter("Password", data.Password);
            _connection.ExecuteNonQuery(command);
        }
    }
}
