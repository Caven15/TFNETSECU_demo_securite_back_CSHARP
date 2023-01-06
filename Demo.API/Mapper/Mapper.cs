using Demo.API.models;
using Demo.API.models.Forms;
using Demo.BLL.Models;

namespace Demo.API.Mapper
{
    public static class Mapper
    {
        internal static UtilisateurModel ApiToBll(this UtilisateurRegisterForm form)
        {
            return new UtilisateurModel()
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                Email = form.Email,
                DateNaissance = form.DateNaissance,
                Password = form.Password
            };
        }

        internal static UtilisateurViewModel BllToApi(this UtilisateurModel model)
        {
            return new UtilisateurViewModel
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance
            };
        }

        internal static UtilisateurViewModel BllToApî(this UtilisateurModel model)
        {
            return new UtilisateurViewModel()
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance,
            };
        }
    }
}
